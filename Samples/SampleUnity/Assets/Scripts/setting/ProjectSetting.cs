using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using DeepCore.GUI.Display;
using ThreeLives.Client.Unity3D;
using ThreeLives.Client.Unity3D.Data;

namespace Assets.Scripts.Setting
{
    
    public static class ProjectSetting
    {        
        //游戏版本号
        public static bool CommandMode { set; get; }

        public static string UPDATE_RES_VERSION;
        public static string GMT_URL;
        public static string SERVERLIST_URL;
        public static string UPDATE_URL;


        public static string IMG_LOAD_ERROR = "/static/item/nitx";
        /// <summary>
        /// 图片加载错误时，增加默认图片.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Image OnGetDefaultImage(string path)
        {   
            if (Debugger.UseDebug)
            {
                Debugger.Log("Load img error: " + path);
            }
            return GameUtil.CreateImage(IMG_LOAD_ERROR, ".png");
        }

        public static void Init()
        {
            InitGameFPS();
            GMT_URL = GameConfig.Instance.GetString("gmt_url");
            UPDATE_URL = GameConfig.Instance.GetString("update_url");
            SERVERLIST_URL = new Uri(GMT_URL) + "api/client/server_list";
            TLClientResourceData data = new TLClientResourceData();
            data.useMPQ = GameGlobal.Instance.useMpq;
            data.serverListUrl = SERVERLIST_URL;
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            data.editorMode = !GameGlobal.Instance.netMode;
            TLDataPathHelper.Init(data);
#else
            data.editorMode = false;
            TLDataPathHelper.Init(data);
#endif

        }
        
        public static string MPQSavedPath {
            //get { return Application.persistentDataPath; }
            get
            {
#if UNITY_STANDALONE || UNITY_EDITOR
                
                return Path.GetFullPath(".").Replace("\\", "/") + "/MPQ/";
#elif UNITY_ANDROID &&  !UNITY_EDITOR
                return AndroidPlugin.GetStoragePath() + "/MPQ/";
#else
                return Application.persistentDataPath + "/MPQ/";
#endif
            }
        }
        
        public static string MPQLocalPath { get { return Application.streamingAssetsPath; } } 


        public static string UPDATE_FILE_NAME
        {
            get
            {
                return  TLDataPathHelper.HTTP_DOWNLOAD_MPQ_SUFFIX+ "update_version.txt";
            }
        }


        //所有资源根路径，平台相关前缀及CommandModed都在此判断，其余路径全都是基于该路径的相对路径
        public static string RootPath
        {
            get
            {
                return TLDataPathHelper.ResRoot;
                //throw new NotImplementedException();
            }
        }

        public static string LuaPath
        {
            get
            {
                return TLDataPathHelper.CLIENT_SCRIPT_ROOT;
                //throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 战斗编辑器资源加载路径.
        /// </summary>
        /// <returns></returns>
        public static string GAME_EDITOR_DATA_ROOT
        {
            get
            {
                return TLDataPathHelper.GAME_EDITOR_DATA_ROOT;
                //throw new NotImplementedException();
            }
        }

        public static string UIEditorPath
        {
            get
            {
                return TLDataPathHelper.UI_EDITOR_ROOT;
                //throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 语言路径
        /// </summary>
        public static string LangPath
        {
            get
            {
                return UIEditorPath+"/lang";
            }
        }

        /// <summary>
        /// Assetbundle资源加载路径.
        /// </summary>
        /// <returns></returns>
        public static string GameEditorABPath
        {
            get
            {
                return TLDataPathHelper.STREAMING_ASSETS_ROOT;
                //throw new NotImplementedException();
            }
        }


        public static  string PLATFORM_DIR_NAME
        {
            get
            {
                return TLDataPathHelper.HTTP_DOWNLOAD_MPQ_SUFFIX;
                
            }
        }

        public static int LAYER_SMALLITEM
        {
            get
            {
                return LayerMask.NameToLayer("SMALLITEM");
            }
        }
#region 游戏帧数设置. TODO 移到Global中

        //游戏帧数.
        //private static int GAME_FPS = 15;
        private static int GAME_FPS = 30;
        private static void InitGameFPS()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = GAME_FPS;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

#endregion
    }
}
