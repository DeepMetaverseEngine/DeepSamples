using UnityEngine;
using System.Collections;

/// <summary>
/// Tasharen Water -- started with the Unity's built-in water, with refraction logic replaced by GrabPass.
/// </summary>

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
[AddComponentMenu("Tasharen/Water")]
public class TasharenWater : MonoBehaviour
{
	static public TasharenWater instance;

	public enum Quality
	{
		Fastest,	// No refraction, skybox reflection
		Low,		// Refraction, skybox reflection
		Medium,		// Refraction, 512 reflection reflecting ships
		High,		// Refraction, 512 reflection reflecting everything
		Uber,		// Refraction, 1024 reflection reflecting everything
	}

	/// <summary>
	/// Active quality level.
	/// </summary>

	public Quality quality = Quality.High;

	/// <summary>
	/// Reflection mask used when the quality is "High" or above.
	/// </summary>

	public LayerMask highReflectionMask = -1;

	/// <summary>
	/// Reflection mask used when the quality is set to "Medium".
	/// </summary>

	public LayerMask mediumReflectionMask = -1;

	/// <summary>
	/// Whether to always reposition the water, always keeping it underneath the main camera.
	/// </summary>

	public bool keepUnderCamera = true;

	/// <summary>
	/// Whether the quality will be retrieved from PlayerPrefs.
	/// </summary>

	public bool automaticQuality = true;

	/// <summary>
	/// Quality of the water.
	/// </summary>

	Transform mTrans;
	Hashtable mCameras = new Hashtable();
	RenderTexture mTex = null;
	int mTexSize = 0;
	Renderer mRen;
	Color mSpecular;
	bool mDepthTexSupport = false;
	bool mStreamingWater = false;

	// Whether rendering is already in progress (stops recursive rendering)
	static bool mIsRendering = false;

	/// <summary>
	/// Whether depth textures are supported or not.
	/// </summary>

	public bool depthTextureSupport { get { return mDepthTexSupport; } }

	/// <summary>
	/// Return the texture size we should be using for reflection.
	/// </summary>

	public int reflectionTextureSize
	{
		get
		{
			switch (quality)
			{
				case Quality.Uber: return 1024;
				case Quality.High:
				case Quality.Medium: return 512;
			}
			return 0;
		}
	}

	/// <summary>
	/// Return the reflection layer mask we should be using.
	/// </summary>

	public LayerMask reflectionMask
	{
		get
		{
			switch (quality)
			{
				case Quality.Uber:
				case Quality.High: return highReflectionMask;
				case Quality.Medium: return mediumReflectionMask;
			}
			return 0;
		}
	}

	/// <summary>
	/// Whether refraction will be used.
	/// </summary>

#if UNITY_IOS || UNITY_ANDROID
	public bool useRefraction { get { return (int)quality > (int)Quality.Low; } }
#else
	public bool useRefraction { get { return (int)quality > (int)Quality.Fastest; } }
#endif

	/// <summary>
	/// Extended sign: returns -1, 0 or 1
	/// </summary>

	static float SignExt (float a)
	{
		if (a > 0.0f) return 1.0f;
		if (a < 0.0f) return -1.0f;
		return 0.0f;
	}

	static Vector3 mTemp = Vector4.one;

	/// <summary>
	/// Adjusts the given projection matrix so that near plane is the given clipPlane
	/// clipPlane is given in camera space. See article in Game Programming Gems 5 and
	/// http://aras-p.info/texts/obliqueortho.html
	/// </summary>

	static void CalculateObliqueMatrix (ref Matrix4x4 projection, Vector4 clipPlane)
	{
		mTemp.x = SignExt(clipPlane.x);
		mTemp.y = SignExt(clipPlane.y);
		Vector4 q = projection.inverse * mTemp;
		Vector4 c = clipPlane * (2.0F / (Vector4.Dot(clipPlane, q)));

		// third row = clip plane - fourth row
		projection[2] = c.x - projection[3];
		projection[6] = c.y - projection[7];
		projection[10] = c.z - projection[11];
		projection[14] = c.w - projection[15];
	}

	/// <summary>
	/// Calculates reflection matrix around the given plane.
	/// </summary>

	static void CalculateReflectionMatrix (ref Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
		reflectionMat.m01 = (-2F * plane[0] * plane[1]);
		reflectionMat.m02 = (-2F * plane[0] * plane[2]);
		reflectionMat.m03 = (-2F * plane[3] * plane[0]);

		reflectionMat.m10 = (-2F * plane[1] * plane[0]);
		reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
		reflectionMat.m12 = (-2F * plane[1] * plane[2]);
		reflectionMat.m13 = (-2F * plane[3] * plane[1]);

		reflectionMat.m20 = (-2F * plane[2] * plane[0]);
		reflectionMat.m21 = (-2F * plane[2] * plane[1]);
		reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
		reflectionMat.m23 = (-2F * plane[3] * plane[2]);

		reflectionMat.m30 = 0F;
		reflectionMat.m31 = 0F;
		reflectionMat.m32 = 0F;
		reflectionMat.m33 = 1F;
	}

	/// <summary>
	/// Get the saved quality level for water.
	/// </summary>

	static public Quality GetQuality ()
	{
		Quality q = (Quality)PlayerPrefs.GetInt("Water", (int)Quality.High);
		return q;
	}

	/// <summary>
	/// Set the water quality, saving it in player prefs as well.
	/// </summary>

	static public void SetQuality (Quality q)
	{
		TasharenWater[] wws = FindObjectsOfType(typeof(TasharenWater)) as TasharenWater[];
		if (wws.Length > 0) foreach (TasharenWater ww in wws) ww.quality = q;
		else PlayerPrefs.SetInt("Water", (int)q);
	}

	/// <summary>
	/// Caching is always a good idea.
	/// </summary>

	void Awake ()
	{
		mTrans = transform;
		mRen = GetComponent<Renderer>();
		mSpecular = new Color32(147, 147, 147, 255); // mRen.GetColor("_Specular") always returns black. Go figure.
		mDepthTexSupport = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth);
	}

	void OnEnable ()
	{
		instance = this;
		mStreamingWater = (PlayerPrefs.GetInt("Streaming Water", 0) == 1);
		if (automaticQuality) quality = GetQuality();
	}

	/// <summary>
	/// Cleanup all the objects we possibly have created.
	/// </summary>

	void OnDisable ()
	{
		Clear();
		foreach (DictionaryEntry kvp in mCameras) DeepCore.Unity3D.UnityHelper.DestroyImmediate(((Camera)kvp.Value).gameObject);
		mCameras.Clear();
		if (instance == this) instance = null;
	}

	/// <summary>
	/// Release the texture and the temporary cameras
	/// </summary>

	void Clear ()
	{
		if (mTex)
		{
            DeepCore.Unity3D.UnityHelper.DestroyImmediate(mTex);
			mTex = null;
		}
	}

	/// <summary>
	/// Copy camera settings from source to destination.
	/// </summary>

	void CopyCamera (Camera src, Camera dest)
	{
		//if (src.clearFlags == CameraClearFlags.Skybox)
		//{
		//    Skybox sky = src.GetComponent<Skybox>();
		//    Skybox mysky = dest.GetComponent<Skybox>();

		//    if (!sky || !sky.material)
		//    {
		//        mysky.enabled = false;
		//    }
		//    else
		//    {
		//        mysky.enabled = true;
		//        mysky.material = sky.material;
		//    }
		//}

		dest.clearFlags = src.clearFlags;
		dest.backgroundColor = src.backgroundColor;
		dest.farClipPlane = src.farClipPlane;
		dest.nearClipPlane = src.nearClipPlane;
		dest.orthographic = src.orthographic;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
		dest.depthTextureMode = DepthTextureMode.None;
		dest.renderingPath = RenderingPath.Forward;
	}

	/// <summary>
	/// Get or create the camera used for reflection.
	/// </summary>

	Camera GetReflectionCamera (Camera current, Material mat, int textureSize)
	{
		if (!mTex || mTexSize != textureSize)
		{
			if (mTex) DeepCore.Unity3D.UnityHelper.DestroyImmediate(mTex);
			mTex = new RenderTexture(textureSize, textureSize, 16);
			mTex.name = "__MirrorReflection" + GetInstanceID();
			mTex.isPowerOfTwo = true;
			mTex.hideFlags = HideFlags.DontSave;
			mTexSize = textureSize;
		}

		Camera cam = mCameras[current] as Camera;

		if (!cam)
		{
			GameObject go = new GameObject("Mirror Refl Camera id" + GetInstanceID() + " for " + current.GetInstanceID(), typeof(Camera), typeof(Skybox));
			go.hideFlags = HideFlags.HideAndDontSave;

			cam = go.GetComponent<Camera>();
			cam.enabled = false;

			Transform t = cam.transform;
			t.position = mTrans.position;
			t.rotation = mTrans.rotation;

			cam.gameObject.AddComponent<FlareLayer>();
			mCameras[current] = cam;
		}

		// Automatically update the reflection texture
		if (mat.HasProperty("_ReflectionTex")) mat.SetTexture("_ReflectionTex", mTex);
		return cam;
	}

	/// <summary>
	/// Given position/normal of the plane, calculates plane in camera space.
	/// </summary>

	Vector4 CameraSpacePlane (Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Matrix4x4 m = cam.worldToCameraMatrix;
		Vector3 cpos = m.MultiplyPoint(pos);
		Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
	}

	/// <summary>
	/// Keep the water underneath the camera.
	/// </summary>

	void LateUpdate ()
	{
#if UNITY_EDITOR
		if (keepUnderCamera && Application.isPlaying)
#else
		if (keepUnderCamera)
#endif
		{
			Camera cam = Camera.main;
			if (cam == null) return;
			Transform camTrans = cam.transform;
			Vector3 cp = camTrans.position;
			cp.y = mTrans.position.y;
			if (mTrans.position != cp) mTrans.position = cp;
		}
	}

	[System.NonSerialized] Vector4 mReflectionPlane;
	[System.NonSerialized] Texture2D mDepthTex = null;
	[System.NonSerialized] bool mDepthTexIsValid = false;

	/// <summary>
	/// Called when the object is being rendered.
	/// </summary>

	void OnWillRenderObject ()
	{
		// Safeguard from recursive reflections
		if (mIsRendering) return;

		if (!enabled || !mRen || !mRen.enabled)
		{
			Clear();
			return;
		}

		Material mat = mRen.sharedMaterial;
		if (!mat) return;

		Camera cam = Camera.current;
		if (!cam) return;

		// Unity doesn't understand setting this value ONCE. Would be easy to set it in Start(), right?
		// NOPE! Not with Unity!
		if (mStreamingWater) mat.SetColor("_Specular", Color.black);
		else mat.SetColor("_Specular", mSpecular);

		// No depth support means only the fastest quality is available
		if (!mDepthTexSupport) quality = Quality.Fastest;

		// No reflection, refraction or depth texture
#if UNITY_IOS || UNITY_ANDROID
		if (quality < Quality.Medium)
#else
		if (quality == Quality.Fastest)
#endif
		{
			// Generate a heightmap that will then be sampled by the shader
			mat.shader.maximumLOD = 100;

#if WINDWARD
			int size = ZoneCreator.terrainSize;
#else
			// NOTE: Change this size to represent the size of your area covered with water (Windward's maps are 256x256)
			int size = 256;
#endif
			float halfSize = size * 0.5f;
			mat.SetFloat("_InvScale", 1f / size);

#if WINDWARD
			if (!UILoadingScreen.isVisible)
#else
			Terrain ter = Terrain.activeTerrain;
			// NOTE: Change this offset to your own "water" level as needed
			float offset = (ter != null) ? ter.transform.position.y : 0f;

			if (ter != null)
#endif
			{
				if (mDepthTex == null)
				{
					mDepthTexIsValid = false;
#if WINDWARD
					ZoneCreator.onTerrainModified += delegate() { mDepthTexIsValid = false; };
#endif
					mDepthTex = new Texture2D(size, size, TextureFormat.Alpha8, false);
				}

				if (!mDepthTexIsValid)
				{
					mDepthTexIsValid = true;
					Color32[] buffer = new Color32[size * size];
					float mult = (float)(size + 1) / size;

					for (int y = 0; y < size; ++y)
					{
						float fy = -halfSize + y * mult;

						for (int x = 0; x < size; ++x)
						{
							float fx = -halfSize + x * mult;
#if WINDWARD
							float f = GameTools.SampleHeight(new Vector3(fx, 0f, fy));
#else
							float f = ter.SampleHeight(new Vector3(fx, 0f, fy)) + offset;
#endif
							if (f < 0f) buffer[x + y * size].a = (byte)Mathf.RoundToInt(255f * Mathf.Clamp01(-f * 0.125f));
							else f = buffer[x + y * size].a = 0;
						}
					}
					mDepthTex.SetPixels32(buffer);
					mDepthTex.wrapMode = TextureWrapMode.Clamp;
					mDepthTex.Apply();
				}
			}
			mat.SetTexture("_DepthTex", mDepthTex);
			return;
		}

		// Low quality: no reflection or refraction, but samples the depth texture
		if (quality == Quality.Low)
		{
			mat.shader.maximumLOD = 200;
			Clear();
			return;
		}

		// From here onwards a depth texture is required
		cam.depthTextureMode |= DepthTextureMode.Depth;

		// Don't try to do anything else if the reflections have been turned off
		LayerMask mask = reflectionMask;
		int textureSize = reflectionTextureSize;

		if (mask == 0 || textureSize < 512)
		{
			// No reflection
			mat.shader.maximumLOD = 300;
			Clear();
		}
		else
		{
			// Refraction and reflection -- we first need to create the reflection texture
			mat.shader.maximumLOD = 400;
			mIsRendering = true;

			// Get the reflection camera for the specified game camera (ie: main camera or scene view camera)
			Camera reflectionCamera = GetReflectionCamera(cam, mat, textureSize);

			// find out the reflection plane: position and normal in world space
			Vector3 pos = mTrans.position;
			Vector3 normal = mTrans.up;

			CopyCamera(cam, reflectionCamera);

			// Reflect camera around the reflection plane
			float d = -Vector3.Dot(normal, pos);
			mReflectionPlane.x = normal.x;
			mReflectionPlane.y = normal.y;
			mReflectionPlane.z = normal.z;
			mReflectionPlane.w = d;
			Matrix4x4 reflection = Matrix4x4.zero;

			CalculateReflectionMatrix(ref reflection, mReflectionPlane);

			Vector3 oldpos = cam.transform.position;
			Vector3 newpos = reflection.MultiplyPoint(oldpos);
			reflectionCamera.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;

			// Setup oblique projection matrix so that near plane is our reflection
			// plane. This way we clip everything below/above it for free.
			Vector4 clipPlane = CameraSpacePlane(reflectionCamera, pos, normal, 1.0f);
			Matrix4x4 projection = cam.projectionMatrix;

			CalculateObliqueMatrix(ref projection, clipPlane);

			reflectionCamera.projectionMatrix = projection;
			reflectionCamera.cullingMask = ~(1 << 4) & mask.value;
			reflectionCamera.targetTexture = mTex;

			GL.SetRevertBackfacing(true);
			{
				reflectionCamera.transform.position = newpos;
				Vector3 euler = cam.transform.eulerAngles;
				euler.x = 0f;
				reflectionCamera.transform.eulerAngles = euler;
				reflectionCamera.Render();
				reflectionCamera.transform.position = oldpos;
			}
			GL.SetRevertBackfacing(false);
			mIsRendering = false;
		}
	}
}
