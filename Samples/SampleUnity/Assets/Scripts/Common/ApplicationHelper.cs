using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public static class ApplicationHelper
{
    //[DllImport("__Internal")]
    //private static extern IntPtr URLAddingPercentEscapes(string url);

    public static void OpenURL(string url)
    {
//#if UNITY_IOS && !UNITY_EDITOR
//        IntPtr encoded_url = URLAddingPercentEscapes(url);
//        Application.OpenURL(Marshal.PtrToStringAnsi(encoded_url));
//#else
        Application.OpenURL(url);
//#endif
    }

    public static byte[] LoadBytesStreamingAssets(string path)
    {
        byte[] data = null;
#if UNITY_ANDROID && !UNITY_EDITOR
        data = DeepCore.Unity3D_Android.WWWHelper.getJavaData(path);
#else
        data = System.IO.File.ReadAllBytes(System.IO.Path.Combine(Application.streamingAssetsPath, path));
#endif
        return data;
    }

    public static string LoadTextStreamingAssets(string path)
    {
        var bytes = LoadBytesStreamingAssets(path);
        if (bytes == null)
            return null;
        if (bytes.Length > 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
        {
            return UTF8Encoding.UTF8.GetString(bytes,3, bytes.Length - 3);
        }
        return UTF8Encoding.UTF8.GetString(bytes);
    }
}