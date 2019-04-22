using DeepCore;
using DeepCore.IO;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeLives.Client.Unity3D.Data;
using TLClient;

namespace ThreeLives.Client.Unity3D
{

    public static class TLDataPathHelper
    {
        public static string ResRoot { get; private set; }
        public static bool IsUseMPQ { get; private set; }
        public static LanguageManager Language { get; private set; }

        /// <summary>
        /// 战斗编辑器数据地址
        /// </summary>
        public static string GAME_EDITOR_DATA_ROOT { get; private set; }
        /// <summary>
        /// StreamingAssets资源地址
        /// </summary>
        public static string STREAMING_ASSETS_ROOT { get; private set; }
        /// <summary>
        /// UI编辑器根地址
        /// </summary>
        public static string UI_EDITOR_ROOT { get; private set; }
        /// <summary>
        /// UI编辑器资源地址
        /// </summary>
        public static string UI_EDITOR_RES_ROOT { get; private set; }
        /// <summary>
        /// UI编辑器XML地址
        /// </summary>
        public static string UI_EDITOR_XML_ROOT { get; private set; }
        /// <summary>
        /// 客户端Lua脚本地址
        /// </summary>
        public static string CLIENT_SCRIPT_ROOT { get; private set; }

        /// <summary>
        /// 下载MPQ资源后缀地址
        /// </summary>
        public static string HTTP_DOWNLOAD_MPQ_SUFFIX { get; private set; }

        /// <summary>
        /// 图片重定向后缀
        /// </summary>
        public static string REDIRECT_IMAGE_SUFFIX { get; private set; }


        public static void Init(TLClientResourceData data)
        {
            IsUseMPQ = data.useMPQ;
            if (IsUseMPQ)
            {
                ResRoot = "mpq://";
            }
            else if (System.IO.File.Exists(UnityEngine.Application.dataPath + "/../resroot.txt"))
            {
                ResRoot = UnityEngine.Application.dataPath + "/.." + System.IO.File.ReadAllText(UnityEngine.Application.dataPath + "/../resroot.txt").Trim();
            }
            else if (data.editorMode)
            {
                ResRoot = UnityEngine.Application.dataPath + "/../../../..";
            }
            else
            {
                ResRoot = UnityEngine.Application.dataPath + "/../../ThreeLives_PL/GameEditors";
                if (UnityEngine.Application.isEditor == false)
                {
                    switch (UnityEngine.Application.platform)
                    {
                        case UnityEngine.RuntimePlatform.Android:
                            ResRoot = UnityEngine.Application.streamingAssetsPath;
                            break;
                        case UnityEngine.RuntimePlatform.IPhonePlayer:
                            ResRoot = UnityEngine.Application.streamingAssetsPath;
                            break;
                    }
                }
            }
            TLClientTemplateManager.SERVER_LIST_URL = data.serverListUrl;
            TLClient.TLClientTemplateManager.BATTLE_DATA_ROOT = ResRoot;
            UnityEngine.Debug.Log("ResRoot=" + ResRoot);
            GAME_EDITOR_DATA_ROOT = ResRoot + "/GameEditor/data";
            STREAMING_ASSETS_ROOT = ResRoot + "/GameEditor";
            UI_EDITOR_ROOT = ResRoot + "/UIEdit/";
            UI_EDITOR_RES_ROOT = ResRoot + "/UIEdit/res/";
            UI_EDITOR_XML_ROOT = ResRoot + "/UIEdit/xml/";
            CLIENT_SCRIPT_ROOT = ResRoot + "/ClientScript/";
            HTTP_DOWNLOAD_MPQ_SUFFIX = "updates_png/";
            REDIRECT_IMAGE_SUFFIX = ".png";
            switch (UnityEngine.Application.platform)
            {
                case UnityEngine.RuntimePlatform.Android:
                    STREAMING_ASSETS_ROOT = ResRoot + "/StreamingAssets/Android";
                    HTTP_DOWNLOAD_MPQ_SUFFIX = "updates_etc/";
                    REDIRECT_IMAGE_SUFFIX = ".etc.m3z";
                    if (TLDataPathHelper.IsUseMPQ)
                    {
                        DeepCore.Unity3D.Impl.UnityDriver.UnityInstance.RedirectImage = RedirectImage;
                    }
                    break;
                case UnityEngine.RuntimePlatform.IPhonePlayer:
                    STREAMING_ASSETS_ROOT = ResRoot + "/StreamingAssets/iOS";
                    HTTP_DOWNLOAD_MPQ_SUFFIX = "updates_pvr/";
                    REDIRECT_IMAGE_SUFFIX = ".pvr.m3z";
                    if (TLDataPathHelper.IsUseMPQ)
                    {
                        DeepCore.Unity3D.Impl.UnityDriver.UnityInstance.RedirectImage = RedirectImage;
                    }
                    break;
                default:
#if UNITY_ANDROID
                    STREAMING_ASSETS_ROOT = ResRoot + "/StreamingAssets/Android";
                    HTTP_DOWNLOAD_MPQ_SUFFIX = "updates_etc/";
                    REDIRECT_IMAGE_SUFFIX = ".etc.m3z";
                    if (TLDataPathHelper.IsUseMPQ)
                    {
                        DeepCore.Unity3D.Impl.UnityDriver.UnityInstance.RedirectImage = RedirectImage;
                    }
#elif UNITY_IOS
                    STREAMING_ASSETS_ROOT = ResRoot + "/StreamingAssets/iOS";
                    HTTP_DOWNLOAD_MPQ_SUFFIX = "updates_pvr/";
                    REDIRECT_IMAGE_SUFFIX = ".pvr.m3z";
                    if (TLDataPathHelper.IsUseMPQ)
                    {
                        DeepCore.Unity3D.Impl.UnityDriver.UnityInstance.RedirectImage = RedirectImage;
                    }
#endif
                    break;
            }
        }


        public static string RedirectImage(string resource)
        {
            try
            {
                return resource.Substring(0, resource.LastIndexOf(".")) + REDIRECT_IMAGE_SUFFIX;
            }
            catch
            {
                return resource;
            }
        }

        public static void InitLanguage(string local_code)
        {
//            var messageManager = new DeepMMO.Protocol.MessageCodeManager(TLClient.TLNetClient.TLNetCodec);
            var path = CLIENT_SCRIPT_ROOT + "Data/lang/" + local_code + "/lang.properties";
            if (Resource.ExistData(path))
            {
                Language = new LanguageManager();
                Language.InitWithPath(path);
            }
        }

    }
}
