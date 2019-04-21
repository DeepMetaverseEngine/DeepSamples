#if (UNITY_ANDROID)
using System;
using DeepCore.Unity3D.Platform;
using UnityEngine;
using System.Runtime.InteropServices;
using DeepCore.Concurrent;
using System.IO;
using DeepCore;
using DeepCore.MPQ.Updater;

namespace DeepCore.Unity3D_Android
{
    public class UnityPlatformAndroid : IUnityPlatform
    {
        private AndroidSysFont mSysFont;
        private long mTotalSpace = long.MaxValue;
        private long mAvaliableSpace = long.MaxValue;


        public UnityPlatformAndroid()
        {
            mSysFont = new AndroidSysFont();
        }

        public void Assert(string msg)
        {
            Debug.LogError(msg);
        }

        #region TEXT

        private const string fontName = "Helvetica";

        class AndroidSysFont : IDisposable
        {
            [DllImport("UnityPlugin")]
            static extern void Argb2Rgba(IntPtr argb, int length);

            private AndroidJavaObject SysFont = new AndroidJavaObject("com.morefun.SysFont");

            public void Dispose()
            {
                SysFont.Dispose();
            }

            public void sysSetFont(string fontName, int fontStyle, int fontSize)
            {
                try
                {
                    object[] args = new object[]
                    {
                        fontName,
                        fontStyle,
                        fontSize,
                    };
                    SysFont.CallStatic("sysSetFont", args);
                }
                catch (Exception err)
                {
                    Debug.LogException(err);
                }
            }

            public bool sysFontTexture2(
                string pText,
                int fontColorRGBA,
                int bgCount,
                int bgColorRGBA,
                int expectSizeW,
                int expectSizeH,
                int glTextureID)
            {
                try
                {
                    bool ret = SysFont.CallStatic<bool>("sysFontTexture2",
                        pText,
                        fontColorRGBA,
                        bgCount,
                        bgColorRGBA,
                        expectSizeW,
                        expectSizeH,
                        glTextureID);
                    return ret;
                }
                catch (Exception err)
                {
                    Debug.LogException(err);
                }
                return false;
            }

            public bool sysFontGetPixels(
                    string pText,
                    int fontColorRGBA,
                    int bgCount,
                    int bgColorRGBA,
                    int pixelW,
                    int pixelH,
                    out byte[] rgba)
            {
                try
                {
                    AndroidJavaObject ret = SysFont.CallStatic<AndroidJavaObject>("sysFontGetPixels",
                        pText,
                        fontColorRGBA,
                        bgCount,
                        bgColorRGBA,
                        pixelW,
                        pixelH);
                    if (ret.GetRawObject().ToInt32() != 0)
                    {
                        rgba = AndroidJNIHelper.ConvertFromJNIArray<byte[]>(ret.GetRawObject());
                        return true;
                    }
                }
                catch (Exception err)
                {
                    Debug.LogException(err);
                }
                rgba = null;
                return false;
            }

            public bool sysFontTest(
                string pText,
                int bgCount,
                int expectSizeW,
                int expectSizeH,
                ref int outW,
                ref int outH)
            {
                try
                {
                    bool ret = SysFont.CallStatic<bool>("sysFontTest",
                        pText,
                        bgCount,
                        expectSizeW,
                        expectSizeH);
                    if (ret)
                    {
                        outW = SysFont.GetStatic<int>("sysFontTestW");
                        outH = SysFont.GetStatic<int>("sysFontTestH");
                        //Debug.Log(string.Format(">>>sysFontTest<<< {0}x{1}", outW, outH));
                    }
                    return ret;
                }
                catch (Exception err)
                {
                    Debug.LogException(err);
                }
                return false;
            }
        }

        // ----------------------------------------------------------------------------------------------

        public Texture2D SysFontTexture(
                string text,
                bool readable,
                DeepCore.GUI.Display.FontStyle style,
                int fontSize,
                uint fontColor,
                int borderTime,
                uint borderColor,
                DeepCore.GUI.Gemo.Size2D expectSize,
                out int boundW,
                out int boundH)
        {
            //Debug.Log(">>>UnityPlatformAndroid.SysFontTexture<<< " + text);
            text = text + "";
            int _expectW = boundW = 0;
            int _expectH = boundH = 0;
            if (expectSize != null)
            {
                _expectW = boundW = (int)expectSize.width;
                _expectH = boundH = (int)expectSize.height;
            }
            mSysFont.sysSetFont(fontName, (int)style, fontSize);
            if (mSysFont.sysFontTest(text, borderTime, _expectW, _expectH, ref boundW, ref boundH))
            {
                int _pixelW = boundW;
                int _pixelH = boundH;
                byte[] rgba;
                if (mSysFont.sysFontGetPixels(
                    text,
                    (int)fontColor,
                    (int)borderTime,
                    (int)borderColor,
                    (int)_pixelW,
                    (int)_pixelH,
                    out rgba))
                {
                    Texture2D mTexture = new UnityEngine.Texture2D(_pixelW, _pixelH, TextureFormat.RGBA32, false, true);
                    mTexture.filterMode = FilterMode.Bilinear;
                    mTexture.wrapMode = TextureWrapMode.Clamp;
                    mTexture.anisoLevel = 0;
                    mTexture.mipMapBias = 0;
                    mTexture.LoadRawTextureData(rgba);
                    if (readable)
                    {
                        mTexture.Apply(false, false);
                    }
                    else
                    {
                        mTexture.Apply(false, true);
                    }
                    return mTexture;
                }
            }
            Texture2D tex = new UnityEngine.Texture2D(8, 8, TextureFormat.RGBA32, false, true);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;
            boundW = 8;
            boundH = 8;
            tex.Apply();
            return tex;
        }

        public bool TestTextLineBreak(string text, float size, DeepCore.GUI.Display.FontStyle style,
            int borderTime,
            float testWidth,
            out float realWidth,
            out float realHeight)
        {
            int tw = 0;
            int th = 0;
            mSysFont.sysSetFont(fontName, (int)style, (int)size);
            mSysFont.sysFontTest(text, borderTime, 0, 0, ref tw, ref th);
            realWidth = tw;
            realHeight = th;
            if (realWidth > testWidth)
            {
                mSysFont.sysFontTest(text, borderTime, (int)testWidth, 0, ref tw, ref th);
                realWidth = tw;
                return true;
            }
            return false;
        }

        #endregion

        // ----------------------------------------------------------------------------------------------

        private static void InnRect(Texture2D src, ref int sx, ref int sy)
        {
            if (sx < 0)
                sx = 0;
            if (sx >= src.width)
                sx = src.width - 1;
            if (sy < 0)
                sy = 0;
            if (sy >= src.height)
                sy = src.height - 1;
        }

        public void CopyPixels(Texture2D src, int sx, int sy, int sw, int sh, Texture2D dst, int dx, int dy)
        {
            int sx2 = sx + sw;
            int sy2 = sy + sh;
            int dx2 = dx + sw;
            int dy2 = dy + sh;
            InnRect(src, ref sx, ref sy);
            InnRect(src, ref sx2, ref sy2);
            InnRect(dst, ref dx, ref dy);
            InnRect(dst, ref dx2, ref dy2);

            sw = Mathf.Min(sx2 - sx, dx2 - dx);
            sh = Mathf.Min(sy2 - sy, dy2 - dy);

            try
            {
                if (sw > 0 && sh > 0)
                {
                    UnityEngine.Color[] colors = src.GetPixels(sx, sy, sw, sh);
                    dst.SetPixels(dx, dy, sw, sh, colors);
                    dst.Apply();
                }
            }
            catch (Exception err)
            {
                Debug.LogError(err.Message);
                Debug.LogException(err);
            }
        }

        // ----------------------------------------------------------------------------------------------
        #region IME
        // ----------------------------------------------------------------------------------------------
#if HZUI

        private GameObject mGameObject = null;
        private UnityPlatformAndroidTextInput mTextInput = null;

        public void OpenIME(DeepCore.GUI.UI.UITextInput input)
        {
            if (mGameObject == null)
            {
                mGameObject = new GameObject();
                GameObject.DontDestroyOnLoad(mGameObject);
            }

            if (mTextInput == null)
            {
                mTextInput = mGameObject.AddComponent<UnityPlatformAndroidTextInput>();
            }

            mTextInput.SetInput(input);
        }


        public void CloseIME()
        {
            if (mTextInput != null) { mTextInput.SetInput(null); }
        }
#endif
        #endregion

        public long GetAvaliableSpace(string path)
        {
            lock (this)
            {
                if (mAvaliableSpace == long.MaxValue)
                {
                    try
                    {
                        using (AndroidJavaObject obj = new AndroidJavaObject("com.morefun.sdcard.MFAssetsHelp"))
                        {
                            mAvaliableSpace = obj.CallStatic<long>("getDirFreeMemory", path);
                        }
                    }
                    catch (Exception err)
                    {
                        Debug.LogError(err.Message + "\n" + err.StackTrace);
                    }
                }
                return mAvaliableSpace;
            }
        }

        public long GetTotalSpace(string path)
        {
            lock (this)
            {
                if (mTotalSpace == long.MaxValue)
                {
                    try
                    {
                        using (AndroidJavaObject obj = new AndroidJavaObject("com.morefun.sdcard.MFAssetsHelp"))
                        {
                            mTotalSpace = obj.CallStatic<long>("getDirTotalMemory", path);
                        }
                    }
                    catch (Exception err)
                    {
                        Debug.LogError(err.Message + "\n" + err.StackTrace);
                    }
                }
                return mTotalSpace;
            }
        }

        public bool NativeDecompressFile(MPQUpdater updater, MPQUpdater.RemoteFileInfo zip_file, MPQUpdater.RemoteFileInfo mpq_file, AtomicLong current_unzip_bytes)
        {
            return SharpZipLib.Unzip.SharpZipLib_RunUnzipMPQ(updater, zip_file, mpq_file, current_unzip_bytes);
        }
        public bool NativeDecompressMemory(ArraySegment<byte> src, ArraySegment<byte> dst)
        {
            return SharpZipLib.Unzip.SharpZipLib_DecompressZ(src, dst);
        }
        public bool NativeGetFileMD5(string fullname, out string md5)
        {
            using (var fs = new FileStream(fullname, FileMode.Open, FileAccess.Read))
            {
                md5 = CMD5.CalculateMD5(fs);
            }
            return true;
        }
    }
}
#endif
