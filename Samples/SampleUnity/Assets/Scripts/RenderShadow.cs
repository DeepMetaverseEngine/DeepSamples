using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(RenderShadow))]
public class RenderShadowEditor : Editor
{
    RenderShadow mTarget;
    string[] layers;

    enum ShadowType
    {
        None = 0,
        Simple = 1,
    }

    void OnEnable()
    {
        mTarget = target as RenderShadow;
        List<string> layerNameList = new List<string>();
        for (int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);
            if (string.IsNullOrEmpty(layerName)) continue;
            layerNameList.Add(layerName);
        }
        layers = layerNameList.ToArray();
    }

    public override void OnInspectorGUI()
    {

        SetField((t) => mTarget.target = t, () => (Transform)EditorGUILayout.ObjectField("目标中心", mTarget.target, typeof(Transform), true), "Set Target");

        SetField((t) => mTarget.projectorDistance = t, () => EditorGUILayout.FloatField("投射器距离", mTarget.projectorDistance), "Set Distance");

        float angle = mTarget.transform.eulerAngles.y;
        angle = angle > 180 ? angle - 360 : angle;
        float height = mTarget.transform.eulerAngles.x;
        height = height > 180 ? height - 360 : height;

        SetField((t) => mTarget.shadowDirection = new Vector3(mTarget.transform.eulerAngles.x, t, 0),
            () => EditorGUILayout.Slider("光照方向", angle, -180, 180), "Set Angle");

        SetField((t) => mTarget.shadowDirection = new Vector3(t, mTarget.transform.eulerAngles.y, 0),
            () => EditorGUILayout.Slider("光照垂直", height, -89, 89), "Set Vertical");

        SetField((t) => mTarget.quality = Mathf.Clamp((int)t, 0, 3),
            () => (ShadowType)EditorGUILayout.EnumPopup("阴影方式", (ShadowType)mTarget.quality), "Set Quality");

        SetField((v) => mTarget.cullingMask = v,
            () =>{
                int mask = 0;
                for (int i = 0; i < layers.Length; i++)
                    if ((mTarget.cullingMask & (1 << LayerMask.NameToLayer(layers[i]))) != 0)//该层已经选中
                        mask |= 1 << i;//记录选过哪些层

                mask = EditorGUILayout.MaskField("投影监视层", mask, layers);//获得新的选择层
                LayerMask layer = 0;
                for (int i = 0; i < layers.Length; i++)
                    if ((mask & (1 << i)) != 0)//选中该层                     
                        layer |= 1 << LayerMask.NameToLayer(layers[i]);

                return layer;
            }, "set CullingMask");

        if (mTarget.quality == 1 || mTarget.quality == 2)
        {
            if (mTarget.quality == 1)
            {
                SetField((t) => mTarget.renderPlane = t, 
                    () => (Renderer)EditorGUILayout.ObjectField("阴影接收面", mTarget.renderPlane, typeof(Renderer), true), "Set renderPlane");
                SetField((t) => mTarget.planeHeight = t,
                    () => EditorGUILayout.FloatField("阴影接收面高度", mTarget.planeHeight), "Set PlaneHeight");
            }
            else if(mTarget.quality == 2)
            {
                SetField((v) => mTarget.ignoreLayers = v,
                    () =>
                    {
                        int mask = 0;
                        for (int i = 0; i < layers.Length; i++)
                            if ((mTarget.ignoreLayers & (1 << LayerMask.NameToLayer(layers[i]))) != 0)//该层已经选中
                                mask |= 1 << i;//记录选过哪些层

                        mask = EditorGUILayout.MaskField("投影忽略层", mask, layers);//获得新的选择层
                        LayerMask layer = 0;
                        for (int i = 0; i < layers.Length; i++)
                            if ((mask & (1 << i)) != 0)//选中该层                     
                                layer |= 1 << LayerMask.NameToLayer(layers[i]);

                        return layer;
                    }, "set IgnareLayers");
            }

            SetField((t) => mTarget.nearClipPlane = Mathf.Max(0.01f, t),
                () => EditorGUILayout.FloatField("近切面", mTarget.nearClipPlane), "Set NearClip");
            SetField((t) => mTarget.farClipPlane = Mathf.Max(mTarget.nearClipPlane + 0.01f, t),
                () => EditorGUILayout.FloatField("远切面", mTarget.farClipPlane), "Set FarClip");


            SetField((t) => mTarget.orthographic = t, () => EditorGUILayout.Toggle("非透视", mTarget.orthographic), "Set Orthographic");
            SetField((t) => mTarget.range = Mathf.Max(0.01f, t), () => EditorGUILayout.FloatField("视野", mTarget.range), "Set Range");
            if (!mTarget.orthographic)
                SetField((t) => mTarget.fieldOfView = t, () => EditorGUILayout.Slider("视角", mTarget.fieldOfView, 1, 179), "Set FOV");

            SetField((t) => mTarget.shadowColor = t, () => EditorGUILayout.ColorField("阴影色", mTarget.shadowColor), "Set ShadowColor");
            SetField((t) => mTarget.shadowResLevel = t, () => EditorGUILayout.IntSlider("阴影精度(10=1024)", mTarget.shadowResLevel, 1, 10), "Set ShadowResLevel");
        }
        else if (mTarget.quality == 3)
        {
            SetField((t) => mTarget.lightIntensity = Mathf.Max(0.01f, t), () => EditorGUILayout.FloatField("光照强度", mTarget.lightIntensity), "Set LightIntensity");
            SetField((t) => mTarget.lightColor = t, () => EditorGUILayout.ColorField("光色彩", mTarget.lightColor), "Set LightColor");
            SetField((t) => mTarget.lightShadowStrength = Mathf.Max(0.01f, t), () => EditorGUILayout.FloatField("阴影强度", mTarget.lightShadowStrength), "Set ShadowStrength");
        }
    }

    void SetField<T>(System.Action<T> value, System.Func<T> func, string undoTip)
    {
        EditorGUI.BeginChangeCheck();
        T result = func();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(mTarget, undoTip);
            value(result);
            EditorUtility.SetDirty(mTarget);
        }
    }
}

#endif

public class RenderShadow : MonoBehaviour {

    public static RenderShadow mInstance;
    public static RenderShadow Instance
    {
        get { return mInstance; }
        set { mInstance = value; }
    }

    [SerializeField]
    private Transform mTarget;
    public Transform target
    {
        get{ return mTarget; }
        set{ mTarget = value; }
    }

    [SerializeField]
    private float mProjectorDistance = 9.5f;
    public float projectorDistance
    {
        get { return mProjectorDistance; }
        set
        {
            mProjectorDistance = value;
        }
    }

    [SerializeField]
    private int mQuatlity;
    public int quality
    {
        get { return mQuatlity; }
        set
        {
            mQuatlity = value;

            switch (value)
            {
                case 0://None
                    {
                        if (renderPlane)
                            renderPlane.gameObject.SetActive(false);                      
                        if (Application.isPlaying)
                        {
                            if (shadowlight)
                            {
                                DeepCore.Unity3D.UnityHelper.Destroy(shadowlight);
                                shadowlight = null;
                            }
                            if (shadowCastCamera.targetTexture)
                            {
                                SetTargetTexture(null);
                            }
                        }  
                    }
                    break;
                case 1://Simple
                    {
                        if (renderPlane && renderPlane.sharedMaterial)
                            renderPlane.gameObject.SetActive(true);
                        if (Application.isPlaying)
                        {
                            ResizeCookie();
                            if (shadowlight)
                            {
                                DeepCore.Unity3D.UnityHelper.Destroy(shadowlight);
                                shadowlight = null;
                            }
                        }
                    }
                    break;
            }
        }
    }

    [SerializeField]
    private float mNearClipPlane = 0.1f;
    public float nearClipPlane
    {
        get { return mNearClipPlane; }
        set
        {
            mNearClipPlane = value;
            shadowCastCamera.nearClipPlane = value;
        }
    }

    [SerializeField]
    private float mFarClipPlane = 20f;
    public float farClipPlane
    {
        get { return mFarClipPlane; }
        set
        {
            mFarClipPlane = value;
            shadowCastCamera.farClipPlane = value;
        }
    }

    [SerializeField]
    private float mFieldOfView = 45f;
    public float fieldOfView
    {
        get { return mFieldOfView; }
        set
        {
            mFieldOfView = value;
            shadowCastCamera.fieldOfView = value;
        }
    }

    [SerializeField]
    private bool mOrthographic = true;
    public bool orthographic
    {
        get { return mOrthographic; }
        set
        {
            mOrthographic = value;
            shadowCastCamera.orthographic = value;
        }
    }

    [SerializeField]
    private float mRange = 5;
    public float range
    {
        get { return mRange; }
        set
        {
            mRange = value;
            shadowCastCamera.orthographicSize = value;
            float angle = transform.eulerAngles.x > 180 ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
            angle = Mathf.Abs(angle * Mathf.Deg2Rad);
            if (angle != 0)
                renderPlane.transform.localScale = new Vector3(value, 1, value / Mathf.Sin(angle)) * 2;
        }
    }

    [SerializeField]
    private float mPlaneHeight = 0;
    public float planeHeight
    {
        get { return mPlaneHeight; }
        set { mPlaneHeight = value; }
    }

    [SerializeField]
    private Color mShadowColor = Color.black;
    public Color shadowColor
    {
        get { return mShadowColor; }
        set
        {
            mShadowColor = value;
            if (Application.isPlaying)
            {
                if (renderPlane.material)
                    renderPlane.material.SetColor("_ShadowColor", value);
            }
            else
            {
                if (renderPlane.sharedMaterial)
                    renderPlane.sharedMaterial.SetColor("_ShadowColor", value);
            }
        }
    }

    [SerializeField]
    private int mShadowResLevel = 8;
    [SerializeField]
    private int mShadowRes = 256;
    private int mLastSize;
    public int shadowResLevel
    {
        get { return mShadowResLevel; }
        set
        {
            mShadowResLevel = value;
            mShadowRes = (int)Mathf.Pow(2, value);

            if (Application.isPlaying)
                ResizeCookie();
        }
    }

    [SerializeField]
    private LayerMask mCullingMask;
    public LayerMask cullingMask
    {
        get { return mCullingMask; }
        set
        {
            mCullingMask = value;
            shadowCastCamera.cullingMask = value;
            if (shadowlight)
                shadowlight.cullingMask = value;
        }
    }

    [SerializeField]
    private LayerMask mIgnoreLayers;
    public LayerMask ignoreLayers
    {
        get { return mIgnoreLayers; }
        set
        {
            mIgnoreLayers = value;
        }
    }

    [SerializeField]
    private float mLightIntensity = 1;
    public float lightIntensity
    {
        get { return mLightIntensity; }
        set
        {
            mLightIntensity = value;
            if (shadowlight)
                shadowlight.intensity = value;
        }
    }

    [SerializeField]
    private Color mLightColor = Color.white;
    public Color lightColor
    {
        get { return mLightColor; }
        set
        {
            mLightColor = value;
            if (shadowlight)
                shadowlight.color = value;
        }
    }

    [SerializeField]
    private float mLightShadowStrength = 0.5f;
    public float lightShadowStrength
    {
        get { return mLightShadowStrength; }
        set
        {
            mLightShadowStrength = value;
            if (shadowlight)
                shadowlight.shadowStrength = value;
        }
    }

    private Camera mShadowCastCamera;
    public Camera shadowCastCamera
    {
        get
        {
            if (!mShadowCastCamera)
                mShadowCastCamera = GetComponent<Camera>();
            return mShadowCastCamera;
        }
    }


    private Light shadowlight;
    private Vector3 mShadowDirection;
    public Vector3 shadowDirection
    {
        get{ return mShadowDirection; }
        set
        {
            mShadowDirection = value;
            transform.eulerAngles = value;
            float angle = value.x > 180 ? value.x - 360 : value.x;
            angle = Mathf.Abs(angle * Mathf.Deg2Rad);
            if (angle != 0)
                renderPlane.transform.localScale = new Vector3(range, 1, range / Mathf.Sin(angle)) * 2;
        }
    }

    [SerializeField]
    private Renderer mRenderPlane;
    public Renderer renderPlane
    {
        get { return mRenderPlane; }
        set { mRenderPlane = value; }
    }

    //private RenderTexture targetTexture;

    private void Awake()
    {
        if (mInstance)
            DeepCore.Unity3D.UnityHelper.Destroy(mInstance);
        mInstance = this;
    }

    void OnEnable ()
    {
        quality = quality;
    }

    void LateUpdate()
    {
        if (!target)
            return;

        Vector3 tPos = target.position;

        if (shadowCastCamera)
            shadowCastCamera.transform.position = tPos - shadowCastCamera.transform.forward * projectorDistance;

        renderPlane.transform.position = tPos + Vector3.up * planeHeight;
        renderPlane.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    void TemplyChangeTarget(ref Vector3 position)
    {
        Transform rayCast = Camera.main.transform;
        RaycastHit[] hits = Physics.RaycastAll(rayCast.position, rayCast.forward, 20);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider is MeshCollider)
            {
                position = hits[i].point;
                break;
            }
        }          
    }

    void ResizeCookie()
    {
        if (mLastSize != mShadowRes || shadowCastCamera.targetTexture == null)
        {
            SetTargetTexture(RenderTexture.GetTemporary(mShadowRes, mShadowRes, 0, RenderTextureFormat.R8));
            mLastSize = mShadowRes;
        }
    }

    void SetTargetTexture(RenderTexture texture)
    {
        if (shadowCastCamera.targetTexture != null)
        {
            ReleaseTexture();
        }
        shadowCastCamera.targetTexture = texture;
        if (renderPlane.material)
            renderPlane.material.SetTexture("_ShadowTex", texture);
    }

    private void ReleaseTexture()
    {
        if (shadowCastCamera.targetTexture != null)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(shadowCastCamera.targetTexture);
            shadowCastCamera.targetTexture = null;
        }
    }

    void OnDestroy()
    {
        ReleaseTexture();
        mInstance = null;
    }
}
