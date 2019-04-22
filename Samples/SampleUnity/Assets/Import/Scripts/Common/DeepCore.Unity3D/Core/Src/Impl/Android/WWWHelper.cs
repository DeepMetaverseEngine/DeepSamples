using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


namespace DeepCore.Unity3D_Android
{
    public class WWWHelper
    {
#if UNITY_ANDROID
        private static AndroidJavaClass _helper;
        private static AndroidJavaClass helper
        {
            get
            {
                if (_helper != null) return _helper;
                _helper = new AndroidJavaClass("com.onegame.WWWHelper");
                using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    object jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                    _helper.CallStatic("init", jo);
                }
                return helper;
            }
        }

#endif
        public static bool isFileExists(string path)
        {
#if UNITY_ANDROID
            return helper.CallStatic<bool>("isFileExists", path);
#else
            return false;
#endif
        }

        public static byte[] getJavaData(string path)
        {
#if UNITY_ANDROID
            byte[] imageByte = helper.CallStatic<byte[]>("getBytes", path);
            return imageByte;
#else

            return null;
#endif
        }

    }
}
