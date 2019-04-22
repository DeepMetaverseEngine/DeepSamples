using UnityEngine;
using System.Collections;

public class UITools {

    private static Camera[] mAllCameras;

    static public T[] FindActive<T>() where T : Component
    {
        return GameObject.FindObjectsOfType(typeof(T)) as T[];
    }

    static public Camera FindCameraForLayer(int layer)
    {
        int layerMask = 1 << layer;

        Camera cam = Camera.main;
        if (cam && (cam.cullingMask & layerMask) != 0) return cam;

        cam = null;
#if UNITY_4_3 || UNITY_FLASH
		mAllCameras = UITools.FindActive<Camera>();
		for (int i = 0, imax = mAllCameras.Length; i < imax; ++i)
#else
        if (mAllCameras == null || Camera.allCamerasCount > mAllCameras.Length)
        {
            mAllCameras = new Camera[Camera.allCamerasCount];
        }
        int camerasFound = Camera.GetAllCameras(mAllCameras);
        for (int i = 0; i < camerasFound; ++i)
#endif
        {
            Camera c = mAllCameras[i];
            if (c && c.enabled && (c.cullingMask & layerMask) != 0)
                cam = c;
            mAllCameras[i] = null;
        }
        
        return cam;
    }

    static public void SetActiveSelf(GameObject go, bool state)
    {
        go.SetActive(state);
    }

    static void Activate(Transform t) 
    { 
        Activate(t, false); 
    }

    static void Activate(Transform t, bool compatibilityMode)
    {
        SetActiveSelf(t.gameObject, true);

        if (compatibilityMode)
        {
            // If there is even a single enabled child, then we're using a Unity 4.0-based nested active state scheme.
            for (int i = 0, imax = t.childCount; i < imax; ++i)
            {
                Transform child = t.GetChild(i);
                if (child.gameObject.activeSelf) return;
            }

            // If this point is reached, then all the children are disabled, so we must be using a Unity 3.5-based active state scheme.
            for (int i = 0, imax = t.childCount; i < imax; ++i)
            {
                Transform child = t.GetChild(i);
                Activate(child, true);
            }
        }
    }

    static void Deactivate(Transform t) 
    { 
        SetActiveSelf(t.gameObject, false); 
    }

    static public void SetActive(GameObject go, bool state) 
    { 
        SetActive(go, state, true); 
    }

    static public void SetActive(GameObject go, bool state, bool compatibilityMode)
    {
        if (go)
        {
            if (state)
            {
                Activate(go.transform, compatibilityMode);
            }
            else Deactivate(go.transform);
        }
    }
}
