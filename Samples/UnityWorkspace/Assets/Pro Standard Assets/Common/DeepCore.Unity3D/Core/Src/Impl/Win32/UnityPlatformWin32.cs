#if ((UNITY_STANDALONE || UNITY_EDITOR) || (UNITY_STANDALONE && (!UNITY_IOS) && (!UNITY_ANDROID)))

using CommonUI_Win32;
using DeepCore.Concurrent;
using DeepCore.GUI.Gemo;
using DeepCore.MPQ.Updater;
using DeepCore.Unity3D.Platform;
using System;
using System.IO;
using UnityEngine;

namespace DeepCore.Unity3D_Win32
{
    public class UnityPlatformWin32 : IUnityPlatform
    {
        public bool IsNativeUnzip { get { return false; } }


        public void Assert(string msg)
        {
            Debug.LogError(msg);
        }

        public Texture2D SysFontTexture(
            string text, bool readable,
            DeepCore.GUI.Display.FontStyle style,
            int fontSize,
            uint fontColor,
            int borderTime,
            uint borderColor,
            DeepCore.GUI.Gemo.Size2D expectSize,
            out int boundW,
            out int boundH)
        {

            byte[] rgba = null;
            int txtw = 0;
            int txth = 0;
            Size2D size = SysFontWin32.SysFontTexture(
                text, style, fontSize, fontColor, borderTime, borderColor, expectSize,
                out rgba,
                out txtw,
                out txth);
            if (rgba != null)
            {
                Texture2D mTexture = new UnityEngine.Texture2D(
                    txtw,
                    txth,
                    TextureFormat.RGBA32,
                    false,
                    true);
                mTexture.filterMode = FilterMode.Bilinear;
                mTexture.wrapMode = TextureWrapMode.Clamp;
                mTexture.anisoLevel = 0;
                mTexture.mipMapBias = 0;
                mTexture.LoadRawTextureData(rgba);
                mTexture.Apply(false, !readable);
                boundW = txtw;
                boundH = txth;
                return mTexture;
            }
            boundW = 0;
            boundH = 0;
            return null;
        }

        public bool TestTextLineBreak(string text, float size, DeepCore.GUI.Display.FontStyle style,
            int borderTime,
            float testWidth,
            out float realWidth,
            out float realHeight)
        {
            return SysFontWin32.TestTextLineBreak(text, size, style, borderTime, testWidth, out realWidth, out realHeight);
        }


        private static void InnRect(Texture2D src, ref int sx, ref int sy)
        {
            if (sx < 0) sx = 0;
            if (sx >= src.width) sx = src.width - 1;
            if (sy < 0) sy = 0;
            if (sy >= src.height) sy = src.height - 1;
        }

        public void CopyPixels(Texture2D src, int sx, int sy, int sw, int sh, Texture2D dst, int dx, int dy)
        {
            if (sw > 0 && sh > 0)
            {
                UnityEngine.Color[] colors = src.GetPixels(sx, sy, sw, sh);
                dst.SetPixels(dx, dst.height - sh - dy, sw, sh, colors);
            }
        }

#if HZUI

        private GameObject mGameObject = null;
        private UnityPlatformWin32TextInput mTextInput = null;


        public void OpenIME(DeepCore.GUI.UI.UITextInput input)
        {
            if (mGameObject == null)
            {
                mGameObject = new GameObject();
                GameObject.DontDestroyOnLoad(mGameObject);
            }
            if (mTextInput == null)
            {
                mTextInput = mGameObject.AddComponent<UnityPlatformWin32TextInput>();
            }
            mTextInput.SetInput(input);
        }

        public void CloseIME()
        {
            if (mTextInput != null) { mTextInput.SetInput(null); }
        }

#endif

        public long GetAvaliableSpace(string path)
        {
            try
            {
                DriveInfo drive = new DriveInfo(Directory.GetDirectoryRoot(path));
                return drive.AvailableFreeSpace;
            }
            catch (Exception)
            {
                return long.MaxValue;
            }
        }

        public long GetTotalSpace(string path)
        {
            try
            {
                DriveInfo drive = new DriveInfo(Directory.GetDirectoryRoot(path));
                return drive.TotalSize;
            }
            catch (Exception)
            {
                return long.MaxValue;
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
#if HZUI

    public class UnityPlatformWin32TextInput : MonoBehaviour
    {
        private DeepCore.GUI.UI.UITextInput mInput = null;
        private Rect mRect;
        private int mMaxLength = 100;
        private string mText = "";

        public void SetInput(DeepCore.GUI.UI.UITextInput input)
        {
            if (input == null && mInput != null)
            {
                mInput.ShowText(true);
                mInput.Text = mText;
                mInput.SetInputFinish(mText);
            }

            mInput = input;

            if (mInput != null)
            {
                mInput.ShowText(false);
                Rectangle2D tempRect = input.LocalToGlobal(input.Bounds);
                mRect.x = tempRect.x;
                mRect.y = tempRect.y;
                mRect.width = tempRect.width;
                mRect.height = tempRect.height;
                mMaxLength = input.MaxLength;
                mText = mInput.Text;
            }
        }

        void OnGUI()
        {
            if (mInput == null) { return; }
            GUI.SetNextControlName("MyTextField");
            GUI.FocusControl("MyTextField");

            string text = GUI.TextArea(mRect, mText, mMaxLength);

            if (text == null)
            {
                text = "";
            }

            if (mText != text)
            {
                mText = "";

                for (int i = 0; i < text.Length; ++i)
                {
                    char ch = text[i];
                    ch = mInput.DoValidator(mText, ch);
                    if (ch != 0) mText += ch;
                }

                if (mMaxLength > 0 && mText.Length > mMaxLength) mText = mText.Substring(0, mMaxLength);
            }

        }

    }
#endif
}
#endif