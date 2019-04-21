﻿using DeepCore.Concurrent;
using DeepCore.GUI.Display;
using DeepCore.GUI.Gemo;
using DeepCore.IO;
using DeepCore.Log;
using DeepCore.MPQ;
using DeepCore.MPQ.Updater;
using DeepCore.Reflection;
using DeepCore.Unity3D.Platform;
using System;
using System.IO;
using UnityEngine;

namespace DeepCore.Unity3D.Impl
{

    public partial class UnityDriver : Driver, IResourceLoader
    {
        public static bool IsDebug = false;

        private static UnityDriver sInstance;
        private static IUnityPlatform sPlatform = new DummyUnityPlatform();
        public static IUnityPlatform Platform
        {
            get { return sPlatform; }
        }

        public static UnityDriver UnityInstance
        {
            get
            {
                if (sInstance == null)
                {
                    sInstance = new UnityDriver();
                }
                return sInstance;
            }
        }

        public static void SetDirver()
        {
            if (sInstance == null)
            {
                sInstance = new UnityDriver();
            }

            if (IsIOS)
            {
                SetDirver("DeepCore.Unity3D_IOS.UnityPlatformIOS");
            }
            else if (IsAndroid)
            {
                SetDirver("DeepCore.Unity3D_Android.UnityPlatformAndroid");
            }
            else
            {
                SetDirver("DeepCore.Unity3D_Win32.UnityPlatformWin32");
            }
        }
        public static void SetDirver(string platformDriver)
        {
            if (sPlatform is DummyUnityPlatform)
            {
                try
                {
                    Type driver = ReflectionUtil.GetType(platformDriver);
                    if (driver != null)
                    {
                        sPlatform = (IUnityPlatform)ReflectionUtil.CreateInstance(driver);
                        Debug.Log("- Create Platform Driver : " + platformDriver);
                    }
                    else
                    {
                        Debug.LogError("- Can Not Create Platform Driver : " + platformDriver);
                    }
                }
                catch (Exception err)
                {
                    Debug.LogError(err.Message + "\n" + err.StackTrace);
                }
            }
        }
        public static void SetDirver(IUnityPlatform platform)
        {
            if (sPlatform is DummyUnityPlatform)
            {
                Debug.Log("- Set Platform Driver : " + platform);
                sPlatform = platform;
            }
        }
        public static void SetUnityDriver(UnityDriver unityDriver)
        {
            sInstance = unityDriver;
        }
        public UnityDriver() : base()
        {
            Resource.SetLoader(this);
            UnityShaders.InitShaders();
            LoggerFactory.SetFactory(new UnityLoggerFactory());
            mDefaultLoader = new DefaultResourceLoader(Application.dataPath);
        }
        public override void Assert(bool cond, string msg)
        {
            if (!cond)
            {
                Debug.LogError("Assert: " + msg);
                UnityDriver.Platform.Assert("Assert: " + msg);
            }
        }
        public static bool IsObjectExists(UnityEngine.Object go)
        {
            return go != null && !go.Equals(null);
        }


        // ---------------------------------------------------------------------------------


        #region PLATFORM_MACRO
        public static bool IsWin32
        {
            get
            {
                return Application.platform == RuntimePlatform.WindowsEditor ||
                    Application.platform == RuntimePlatform.WindowsPlayer;
            }
        }
        public static bool IsIOS
        {
            get
            {
                return Application.platform == RuntimePlatform.IPhonePlayer;
            }
        }
        public static bool IsAndroid
        {
            get
            {
                return Application.platform == RuntimePlatform.Android;
            }
        }
        #endregion


        // ---------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------------

        #region GFX

        public delegate string RedirectImagePath(string resource);
        public RedirectImagePath RedirectImage;
        public delegate Image GetDefaultImg(string resource);
        public GetDefaultImg OnGetDefaultImg;


        public override void ReloadImage(Image img)
        {
            try
            {
                UnityImage ret = img as UnityImage;
                if (ret != null && !string.IsNullOrEmpty(ret.ResourceStr))
                {
                    string resource = ret.ResourceStr;

                    if (resource.StartsWith(PREFIX_MPQ))
                    {
                        byte[] edata = mFileSystem.getData(resource.Substring(PREFIX_MPQ.Length));
                        if (edata != null)
                        {
                            ret.ResestTexture2D(edata, resource);
                        }
                    }
                    else if (resource.StartsWith(PREFIX_RES))
                    {
                        string res_path = resource.Substring(PREFIX_RES.Length);
                        object obj = LoadObjectFromResources(res_path);
                        if (obj is Texture2D)
                        {
                            ret.ResestTexture2D(obj as Texture2D, resource);
                        }
                        if (obj is TextAsset)
                        {
                            TextAsset ta = (obj as TextAsset);
                            ret.ResestTexture2D(ta.bytes, resource);
                        }
                    }
                    else if (resource.StartsWith(PREFIX_FILE))
                    {
                        FileInfo finfo = new FileInfo(resource.Substring(PREFIX_FILE.Length));
                        if (finfo.Exists)
                        {
                            byte[] data = File.ReadAllBytes(finfo.FullName);
                            ret.ResestTexture2D(data, resource);
                        }
                    }
                    else
                    {
                        byte[] data = Resource.LoadData(resource);
                        if (data != null)
                        {
                            ret.ResestTexture2D(data, resource);
                        }
                        else
                        {
                            Texture2D tex = LoadFromResources<Texture2D>(resource);
                            if (tex != null)
                            {
                                ret.ResestTexture2D(tex, resource);
                            }
                        }
                    }
                }


            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        public override Image createImage(string resource)
        {
            try
            {
                UnityImage ret = null;
                if (RedirectImage != null)
                {
                    resource = RedirectImage(resource);
                }

                if (resource.StartsWith(PREFIX_MPQ))
                {
                    byte[] edata = mFileSystem.getData(resource.Substring(PREFIX_MPQ.Length));
                    if (edata != null)
                    {
                        ret = new UnityImage(edata, resource, resource);
                    }
                }
                else if (resource.StartsWith(PREFIX_RES))
                {
                    string res_path = resource.Substring(PREFIX_RES.Length);
                    object obj = LoadObjectFromResources(res_path);
                    if (obj is Texture2D)
                    {
                        return new UnityImage(obj as Texture2D, resource, resource);
                    }
                    if (obj is TextAsset)
                    {
                        TextAsset ta = (obj as TextAsset);
                        ret = new UnityImage(ta.bytes, resource, resource);
                    }
                }
                else if (resource.StartsWith(PREFIX_FILE))
                {
                    FileInfo finfo = new FileInfo(resource.Substring(PREFIX_FILE.Length));
                    if (finfo.Exists)
                    {
                        byte[] data = File.ReadAllBytes(finfo.FullName);
                        ret = new UnityImage(data, resource, resource);
                    }
                }
                else
                {
                    byte[] data = Resource.LoadData(resource);
                    if (data != null)
                    {
                        ret = new UnityImage(data, resource, resource);
                    }
                    else
                    {
                        Texture2D tex = LoadFromResources<Texture2D>(resource);
                        if (tex != null)
                        {
                            ret = new UnityImage(tex, resource, resource);
                        }
                    }
                }
                return ret;
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError(string.Format("Resource Read Error : {0}\n", resource, e.Message));
                UnityEngine.Debug.LogException(e);
            }
            //Assert(false, string.Format("Resource Read Error : {0}\n", resource));
            if (OnGetDefaultImg != null) { return OnGetDefaultImg(resource); }
            return null;
        }

        public override Image createImage(System.IO.Stream stream)
        {
            if (stream == null)
            {
                UnityEngine.Debug.Log("Invalid Param : create Image from stream");
                return null;
            }
            try
            {
                //  U3D Texture2D
                byte[] imageData = new byte[stream.Length];
                IOUtil.ReadToEnd(stream, imageData, 0, imageData.Length);
                return new UnityImage(imageData, "createImage(stream)");
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError("Stream Read Error " + e.Message);
                UnityEngine.Debug.LogException(e);
            }
            return null;
        }

        public override Image createImage(byte[] imageData, int imageOffset, int imageLength)
        {
            try
            {
                if (imageLength == imageData.Length)
                {
                    return new UnityImage(imageData, "createImage(byte[])");
                }
                else
                {
                    byte[] data = new byte[imageLength];
                    System.Array.Copy(imageData, imageOffset, data, 0, imageLength);
                    //  To UnityImage
                    return new UnityImage(data, "createImage(byte[])");
                }
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError("ImageData Read Error " + e.Message);
                UnityEngine.Debug.LogException(e);
            }

            return null;

        }

        public override Image createRGBImage(uint[] rgba, int width, int height)
        {
            UnityEngine.Texture2D destTex = new UnityEngine.Texture2D(width, height, TextureFormat.ARGB32, false, true);
            int i = 0;
            UnityEngine.Color color = UnityEngine.Color.white;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++, i++)
                {
                    GUI.Display.Color.toRGBAF(rgba[i], out color.r, out color.g, out color.b, out color.a);
                    destTex.SetPixel(x, y, color);
                }
            }
            destTex.Apply();
            return (Image)(new UnityImage(destTex, string.Format("createRGBImage({0},{1})", width, height)));
        }

        public override Image createRGBImage(int width, int height)
        {
            UnityEngine.Texture2D destTex = new UnityEngine.Texture2D(width, height, TextureFormat.ARGB32, false, true);
            UnityEngine.Color color = new UnityEngine.Color(0, 0, 0, 0);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    destTex.SetPixel(x, y, color);
                }
            }
            destTex.Apply();
            return (Image)(new UnityImage(destTex, string.Format("createRGBImage({0},{1})", width, height)));
        }

        public override TextLayer createTextLayer(string text, float size, GUI.Display.FontStyle style)
        {
            return new UnityTextLayer(text, style, size);
        }

        public override bool testTextLineBreak(string text, float size, GUI.Display.FontStyle style,
            int borderTime,
            float testWidth,
            out float realWidth,
            out float realHeight)
        {
            return sPlatform.TestTextLineBreak(text, size, style, borderTime, testWidth, out realWidth, out realHeight);
        }

        public override VertexBuffer createVertexBuffer(int capacity)
        {
            return new UnityVertexBuffer(capacity);
        }

        #endregion

        // ---------------------------------------------------------------------------------

        #region Resource
        private DefaultResourceLoader mDefaultLoader;
        static private MPQFileSystem mFileSystem;
        static private string TestDataPath = String.Empty;
        static public void SetTestDataPath(string path)
        {
            TestDataPath = path;
        }

        static public void AddFileSystem(MPQFileSystem fs)
        {
            mFileSystem = fs;
        }
        private static T LoadFromResources<T>(string path) where T : UnityEngine.Object
        {
            int index = path.LastIndexOf(".");

            if (index < 0) { return null; }
            // Unity TextAsset
            string assetpath = path.Substring(0, index);
            while (assetpath.StartsWith("/"))
            {
                assetpath = assetpath.Substring(1);
            }
            T ta = UnityEngine.Resources.Load<T>(assetpath);
            //Debug.Log("LoadFromAsserts ==========> " + assetpath + " --- " + ta);
            return ta;
        }
        private static object LoadObjectFromResources(string path)
        {
            // Unity TextAsset
            string assetpath = path.Substring(0, path.LastIndexOf("."));
            while (assetpath.StartsWith("/"))
            {
                assetpath = assetpath.Substring(1);
            }
            return UnityEngine.Resources.Load(assetpath);
        }
        //-----------------------------------------------------------------------------------------------------------------

        public static bool LOAD_ASSETBUNDLE_USE_STREAM = true;

        public virtual AssetBundleCreateRequest LoadAssetBundle(string path, out int size)
        {
            size = 0; try
            {
                if (LOAD_ASSETBUNDLE_USE_STREAM)
                {
                    var stream = LoadDataAsStream(path);
                    if (stream != null)
                    {
                        size = (int)stream.Length;
                        var request = AssetBundle.LoadFromStreamAsync(stream);
                        request.completed += (e) => { stream.Dispose(); };
                        return request;
                    }
                }
                else
                {
                    byte[] bin = LoadData(path);
                    if (bin != null)
                    {
                        size = bin.Length;
                        var request = AssetBundle.LoadFromMemoryAsync(bin);
                        return request;
                    }
                }
            }
            catch (Exception err)
            {
                Assert(false, "LoadAssetBundle : Error " + path + "\n" + err.Message);
            }
            return null;
        }

        public virtual AssetBundleCreateRequest LoadAssetBundle(string path)
        {
            try
            {
                if (LOAD_ASSETBUNDLE_USE_STREAM)
                {
                    var stream = LoadDataAsStream(path);
                    if (stream != null)
                    {
                        var request = AssetBundle.LoadFromStreamAsync(stream);
                        request.completed += (e) => { stream.Dispose(); };
                        return request;
                    }
                }
                else
                {
                    byte[] bin = LoadData(path);
                    if (bin != null)
                    {
                        var request = AssetBundle.LoadFromMemoryAsync(bin);
                        return request;
                    }
                }
            }
            catch (Exception err)
            {
                Assert(false, "LoadAssetBundle : Error " + path + "\n" + err.Message);
            }
            return null;
        }

        public virtual AssetBundle LoadAssetBundleImmediate(string path)
        {
            try
            {
                if (LOAD_ASSETBUNDLE_USE_STREAM)
                {
                    var stream = LoadDataAsStream(path);
                    if (stream != null)
                    {
                        using (stream)
                        {
                            var request = AssetBundle.LoadFromStream(stream);
                            return request;
                        }
                    }
                }
                else
                {
                    byte[] bin = LoadData(path);
                    if (bin != null)
                    {
                        return AssetBundle.LoadFromMemory(bin);
                    }
                }
            }
            catch (Exception err)
            {
                Assert(false, "LoadAssetBundleImmediate : Error " + path + "\n" + err.Message);
            }
            return null;
        }


        #endregion

        //-----------------------------------------------------------------------------------------------------------------
        #region IResourceLoader

        #region load

        private bool TryLoadFromTest(string path, ref byte[] ret)
        {
            string fullpath = TestDataPath + "/" + path;
            try
            {
                fullpath = System.IO.Path.GetFullPath(fullpath);
                if (System.IO.File.Exists(fullpath))
                {
                    ret = File.ReadAllBytes(fullpath);
                    if (ret != null)
                    {
                        if (IsDebug)
                        {
                            Debug.Log("Load Data From Test Path : " + fullpath + " -> " + ret.Length + " (bytes)");
                        }
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                Debug.LogError(err.Message);
                Debug.LogError("Load Data From Test Path : " + fullpath + " -> " + ret.Length + " (bytes)");
            }
            return false;
        }
        private bool TryLoadFromFileSystem(string path, ref byte[] ret)
        {
            string fullpath = path;
            ret = File.ReadAllBytes(fullpath);
            if (ret != null)
            {
                if (IsDebug)
                {
                    Debug.Log("Load Data From Path : " + fullpath + " -> " + ret.Length + " (bytes)");
                }
                return true;
            }
            return false;
        }
        private bool TryLoadFromMPQ(string path, ref byte[] ret)
        {
            if (mFileSystem != null)
            {
                ret = mFileSystem.getData(path);
                if (ret != null)
                {
                    if (IsDebug)
                    {
                        Debug.Log("Load Data From MPQ : " + path + " -> " + ret.Length + " (bytes)");
                    }
                    return true;
                }
            }
            return false;
        }
        private bool TryLoadFromResources(string path, ref byte[] ret)
        {
            TextAsset data = LoadFromResources<TextAsset>(path);
            if (data != null)
            {
                ret = data.bytes;
                if (IsDebug)
                {
                    Debug.Log("Load Data From Unity Resources : " + path + " -> " + ret.Length + " (bytes)");
                }
                return true;
            }
            return false;
        }
        private bool TryLoadFromJAR(string path, ref byte[] ret)
        {
            var data = DeepCore.Unity3D_Android.WWWHelper.getJavaData(path);
            //yield return data;
            if (data != null)
            {
                ret = data;
                if (IsDebug)
                {
                    Debug.Log("Load Data From JAR : " + path + " -> " + ret.Length + " (bytes)");
                }
                return true;
            }
            return false;
        }

        #endregion
        //------------------------------------------------------------------------------------------------------------------------
        #region stream

        private bool TryOpenFromTest(string path, ref Stream ret)
        {
            string fullpath = TestDataPath + "/" + path;
            try
            {
                fullpath = System.IO.Path.GetFullPath(fullpath);
                if (System.IO.File.Exists(fullpath))
                {
                    ret = new FileStream(fullpath, FileMode.Open, FileAccess.Read, FileShare.Read);// File.ReadAllBytes(fullpath);
                    if (ret != null)
                    {
                        if (IsDebug)
                        {
                            Debug.Log("Load Data From Test Path : " + fullpath + " -> " + ret.Length + " (bytes)");
                        }
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                Debug.LogError(err.Message);
                Debug.LogError("Load Data From Test Path : " + fullpath + " -> " + ret.Length + " (bytes)");
            }
            return false;
        }
        private bool TryOpenFromFileSystem(string path, ref Stream ret)
        {
            string fullpath = path;
            ret = new FileStream(fullpath, FileMode.Open, FileAccess.Read, FileShare.Read); //File.ReadAllBytes(fullpath);
            if (ret != null)
            {
                if (IsDebug)
                {
                    Debug.Log("Load Data From Path : " + fullpath + " -> " + ret.Length + " (bytes)");
                }
                return true;
            }
            return false;
        }
        private bool TryOpenFromMPQ(string path, ref Stream ret)
        {
            if (mFileSystem != null)
            {
                ret = mFileSystem.openStream(path);
                if (ret != null)
                {
                    if (IsDebug)
                    {
                        Debug.Log("Load Data From MPQ : " + path + " -> " + ret.Length + " (bytes)");
                    }
                    return true;
                }
            }
            return false;
        }
        private bool TryOpenFromResources(string path, ref Stream ret)
        {
            return false;
        }
        private bool TryOpenFromJAR(string path, ref Stream ret)
        {
            return false;
        }

        #endregion
        //------------------------------------------------------------------------------------------------------------------------
        #region exist

        private bool TryExistDataFromMPQ(string path)
        {
            if (mFileSystem != null)
            {
                if (mFileSystem.findEntry(path) != null)
                {
                    return true;
                }
            }
            return false;
        }
        private bool TryExistDataFromFileSystem(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }

            return false;
        }
        private bool TryExistDataFromTestPath(string path)
        {
            if (Directory.Exists(TestDataPath))
            {
                string fullpath = TestDataPath + "/" + path;
                try
                {
                    fullpath = System.IO.Path.GetFullPath(fullpath);
                    if (System.IO.File.Exists(fullpath))
                    {
                        return true;
                    }
                }
                catch (Exception err) { Assert(false, "ExitDataFromTestPath Error:" + err.ToString() + path.ToString()); }
            }
            return false;
        }
        private bool TryExistDataFromJAR(string path)
        {
            return DeepCore.Unity3D_Android.WWWHelper.isFileExists(path);
        }

        #endregion
        //------------------------------------------------------------------------------------------------------------------------

        public const string PREFIX_FILE = "file://";
        public const string PREFIX_MPQ = "mpq://";
        public const string PREFIX_RES = "res://";
        public const string PREFIX_JAR = "jar://";
        //"jar:file://" + Application.dataPath + "!/assets/";


        public bool ExistData(string path)
        {
            bool rlt = false;
            //From MPQ.
            if (path.StartsWith(PREFIX_MPQ))
            {
                rlt = TryExistDataFromMPQ(path.Substring(PREFIX_MPQ.Length));
                return rlt;
            }
            if (path.StartsWith(PREFIX_FILE))
            {
                rlt = TryExistDataFromFileSystem(path.Substring(PREFIX_FILE.Length));
                return rlt;
            }
            if (path.StartsWith(PREFIX_JAR))
            {
                rlt = TryExistDataFromJAR(path.Substring(PREFIX_JAR.Length));
                return rlt;
            }
            if (IsAndroid && path.StartsWith(Application.streamingAssetsPath))
            {
                rlt = TryExistDataFromJAR(path.Substring(Application.streamingAssetsPath.Length));
                return rlt;
            }

            //From MPQ.
            rlt = TryExistDataFromMPQ(path);
            if (rlt) { return rlt; }

            //From Test.
            rlt = TryExistDataFromTestPath(path);
            if (rlt) { return rlt; }

            //From File.
            rlt = TryExistDataFromFileSystem(path);
            if (rlt) { return rlt; }
            return rlt;
        }

        public byte[] LoadData(string path)
        {

            byte[] ret = null;

            // Specify Prefix.
            if (path.StartsWith(PREFIX_MPQ))
            {
                TryLoadFromMPQ(path.Substring(PREFIX_MPQ.Length), ref ret);
                return ret;
            }
            if (path.StartsWith(PREFIX_FILE))
            {
                TryLoadFromFileSystem(path.Substring(PREFIX_FILE.Length), ref ret);
                return ret;
            }
            if (path.StartsWith(PREFIX_RES))
            {
                TryLoadFromResources(path.Substring(PREFIX_RES.Length), ref ret);
                return ret;
            }
            if (path.StartsWith(PREFIX_JAR))
            {
                TryLoadFromJAR(path.Substring(PREFIX_JAR.Length), ref ret);
                return ret;
            }
            if (IsAndroid && path.StartsWith(Application.streamingAssetsPath))
            {
                TryLoadFromJAR(path.Substring(Application.streamingAssetsPath.Length), ref ret);
                return ret;
            }


            // Just Test.
            if (Directory.Exists(TestDataPath) && TryLoadFromTest(path, ref ret))
            {
                return ret;
            }
            // MPQ.
            if (mFileSystem != null && TryLoadFromMPQ(path, ref ret))
            {
                return ret;
            }
            // File.
            if (File.Exists(path) && TryLoadFromFileSystem(path, ref ret))
            {
                return ret;
            }
            // Applicateion Data Path.
            if (TryLoadFromResources(path, ref ret))
            {
                return ret;
            }

            if (IsDebug)
            {
                Debug.LogWarning("Can Not Read Resource : " + path);
            }

            return ret;
        }

        public virtual Stream LoadDataAsStream(string path)
        {
            Stream ret = null;

            // Specify Prefix.
            if (path.StartsWith(PREFIX_MPQ))
            {
                TryOpenFromMPQ(path.Substring(PREFIX_MPQ.Length), ref ret);
                return ret;
            }
            if (path.StartsWith(PREFIX_FILE))
            {
                TryOpenFromFileSystem(path.Substring(PREFIX_FILE.Length), ref ret);
                return ret;
            }


            // Just Test.
            if (Directory.Exists(TestDataPath) && TryOpenFromTest(path, ref ret))
            {
                return ret;
            }
            // MPQ.
            if (mFileSystem != null && TryOpenFromMPQ(path, ref ret))
            {
                return ret;
            }
            // File.
            if (File.Exists(path) && TryOpenFromFileSystem(path, ref ret))
            {
                return ret;
            }

            if (IsDebug)
            {
                Debug.LogWarning("Can Not Read Resource : " + path);
            }

            return ret;
        }

        public virtual string[] ListFiles(string path)
        {
            return mDefaultLoader.ListFiles(path);
        }


        #endregion
        // ---------------------------------------------------------------------------------


        // ---------------------------------------------------------------------------------

        #region MPQ

        public class MPQAdapter : MPQDirver
        {
            public override long GetAvaliableSpace(string path)
            {
                return sPlatform.GetAvaliableSpace(path);
            }
            public override long GetTotalSpace(string path)
            {
                return sPlatform.GetTotalSpace(path);
            }
            public override bool RunGetFileMD5(string fullname, out string md5)
            {
                return sPlatform.NativeGetFileMD5(fullname, out md5);
            }
            public override bool RunUnzipSingle(MPQUpdater updater, MPQUpdater.RemoteFileInfo zip, MPQUpdater.RemoteFileInfo mpq, AtomicLong process)
            {
                return sPlatform.NativeDecompressFile(updater, zip, mpq, process);
            }
        }

        public static MPQUpdater CreateMPQUpdater(
            Uri remote_version_url,
            string[] remote_version_prefix,
            string version_suffix,
            DirectoryInfo local_save_root,
            DirectoryInfo local_bundle_root,
            bool validate_md5,
            MPQUpdaterListener listener)
        {
            sPlatform.GetAvaliableSpace(local_save_root.FullName);
            sPlatform.GetTotalSpace(local_save_root.FullName);
            var ret = new MPQUpdater(new MPQAdapter());
            ret.Init(
                 remote_version_url,
                 remote_version_prefix,
                 version_suffix,
                 local_save_root,
                 local_bundle_root,
                 validate_md5,
                 listener);
            return ret;
        }
        public static MPQUpdater CreateMPQUpdater(
            string[] remote_version_prefix,
            string version_suffix,
            DirectoryInfo local_save_root,
            DirectoryInfo local_bundle_root,
            bool validate_md5,
            MPQUpdaterListener listener)
        {
            sPlatform.GetAvaliableSpace(local_save_root.FullName);
            sPlatform.GetTotalSpace(local_save_root.FullName);
            var ret = new MPQUpdater(new MPQAdapter());
            ret.Init(
                 remote_version_prefix,
                 version_suffix,
                 local_save_root,
                 local_bundle_root,
                 validate_md5,
                 listener);
            return ret;
        }

        #endregion
        // ---------------------------------------------------------------------------------

        public class DummyUnityPlatform : IUnityPlatform
        {
            public bool IsNativeUnzip { get { return false; } }

            public void Assert(string msg) { }
            public Texture2D SysFontTexture(string text, bool readable, GUI.Display.FontStyle style, int fontSize, uint fontColor, int borderTime, uint borderColor, Size2D expectSize, out int boundW, out int boundH)
            {
                boundW = 8;
                boundH = 8;
                return new Texture2D(8, 8, TextureFormat.ARGB32, false, true);
            }
            public bool TestTextLineBreak(string text, float size, GUI.Display.FontStyle style, int borderTime, float testWidth, out float realWidth, out float realHeight)
            {
                realWidth = 8;
                realHeight = 8;
                return false;
            }
            public void CopyPixels(Texture2D src, int sx, int sy, int sw, int sh, Texture2D dst, int dx, int dy) { }

            public long GetAvaliableSpace(string path) { return long.MaxValue; }
            public long GetTotalSpace(string path) { return long.MaxValue; }

            public bool NativeDecompressFile(MPQUpdater updater, MPQUpdater.RemoteFileInfo zip_file, MPQUpdater.RemoteFileInfo mpq_file, AtomicLong current_unzip_bytes)
            {
                throw new NotImplementedException();
            }
            public bool NativeDecompressMemory(ArraySegment<byte> src, ArraySegment<byte> dst)
            {
                throw new NotImplementedException();
            }
            public bool NativeGetFileMD5(string fullname, out string md5)
            {
                throw new NotImplementedException();
            }

        }
    }


}
