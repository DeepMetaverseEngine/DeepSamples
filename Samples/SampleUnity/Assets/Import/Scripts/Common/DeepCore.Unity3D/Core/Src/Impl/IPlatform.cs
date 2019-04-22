using System;
using UnityEngine;
using DeepCore.Concurrent;
using DeepCore.MPQ.Updater;
using DeepCore.GUI.Gemo;

namespace DeepCore.Unity3D.Platform
{
    public interface IUnityPlatform
    {
        void Assert(string msg);

        Texture2D SysFontTexture(
            string text,
            bool readable,
            GUI.Display.FontStyle style,
            float fontSize,
            uint fontColor,
            int borderTime,
            uint borderColor,
            Size2D expectSize,
            out int boundW,
            out int boundH);

        bool TestTextLineBreak(string text, float size, GUI.Display.FontStyle style,
            int borderTime,
            float testWidth,
            out float realWidth,
            out float realHeight);

        void CopyPixels(Texture2D src, int sx, int sy, int sw, int sh, Texture2D dst, int dx, int dy);

#if HZUI
        void OpenIME(DeepCore.GUI.UI.UITextInput input);
        void CloseIME();
#endif

        long GetAvaliableSpace(string path);

        long GetTotalSpace(string path);

        bool NativeDecompressFile(MPQUpdater updater, MPQUpdater.RemoteFileInfo zip_file, MPQUpdater.RemoteFileInfo mpq_file, AtomicLong current_unzip_bytes);
        bool NativeDecompressMemory(ArraySegment<byte> src, ArraySegment<byte> dst);
        bool NativeGetFileMD5(string fullname, out string md5);
    }
}
