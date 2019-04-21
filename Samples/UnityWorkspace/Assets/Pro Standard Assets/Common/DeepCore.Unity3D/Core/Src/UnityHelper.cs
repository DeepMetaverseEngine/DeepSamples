using UnityEngine;
using System;

namespace DeepCore.Unity3D
{
    public class UnityHelper
    {
        private static GameObject mHelperGameObject;

        private static GameObject HelperGameObject
        {
            get
            {
                if (!mHelperGameObject)
                {
                    mHelperGameObject = new GameObject("FuckAssetLoaderHelper");
                    UnityEngine.Object.DontDestroyOnLoad(mHelperGameObject);
                }
                return mHelperGameObject;
            }
        }


        public static void WaitForEndOfFrame(Action cb)
        {
            WaitForSeconds(0, cb);
        }

        public static void WaitForSeconds(float sec, Action cb)
        {
            var handler = HelperGameObject.GetComponent<UnityDelayInvoke>();
            if (!handler)
            {
                handler = HelperGameObject.AddComponent<UnityDelayInvoke>();
            }
            handler.Delay(sec, cb);
        }

        public static void WaitForEndOfFrame<T>(T obj, Action<T> cb)
        {
            WaitForSeconds(0,obj, cb);
        }

        public static void WaitForSeconds<T>(float sec, T obj, Action<T> cb)
        {
            var handler = HelperGameObject.GetComponent<UnityDelayInvoke>();
            if (!handler)
            {
                handler = HelperGameObject.AddComponent<UnityDelayInvoke>();
            }
            handler.Delay(sec, obj, cb);
        }
    }
}