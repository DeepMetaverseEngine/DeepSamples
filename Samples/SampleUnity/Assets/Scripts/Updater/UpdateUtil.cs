using Assets.Scripts.Setting;
using System;
using UnityEngine;
using System.IO;
using DeepCore.MPQ.Updater;
using System.Collections.Generic;
using TLClient.Net;
using DeepCore.Unity3D.Impl;

public static class UpdateUtil
{

    public static void CheckVersion(Action<HttpRequest.ResultType, UpdateVersionMessage> act)
    {
        int msgId = (int)HttpRequest.GameCenterMsgID.Init;
        Dictionary<string, string> arg = new Dictionary<string, string>();
        arg.Add("type", msgId.ToString());
        arg.Add("appid", OneGameSDK.Instance.AppId.ToString());
        arg.Add("sdkName", OneGameSDK.Instance.AppId.ToString());
        arg.Add("channel", OneGameSDK.Instance.Channel.ToString());
        arg.Add("childrenChannel", "0");
        arg.Add("ostype", PublicConst.OSType.ToString());
        arg.Add("mac", PlatformMgr.PluginGetUUID());
        arg.Add("version", PublicConst.LogicVersion.ToString());
        arg.Add("apnsID", "");
        arg.Add("clientUA", PlatformMgr.PluginGetUserAgent());
        arg.Add("returntype", "0");

        HttpRequest mHttpRequest = GameGlobal.Instance.gameObject.AddComponent<HttpRequest>();
        mHttpRequest.RequestPost(new Uri(ProjectSetting.GMT_URL) + "api/client/check_update", arg, msgId, (data) =>
        {
            if (data.Result == HttpRequest.ResultType.Success)
            {
                //string content = HtmlHelper.htmlDecode(data.Content);
                UpdateVersionMessage msg = HttpMsgUtils.ParseXmlString<UpdateVersionMessage>(data.Content) as UpdateVersionMessage;

                act.Invoke(data.Result, msg);
            }
            else
            {
                act.Invoke(data.Result, null);
            }
            mHttpRequest.Destroy();
        }, false);

    }

    public static System.Collections.IEnumerator GetWWWTxt(string path, System.Action<WWW> act)
    {

        WWW www = new UnityEngine.WWW(path);


        yield return www;
        act.Invoke(www);
        www.Dispose();
        www = null;

    }
	#if !UNITY_IOS
    public static void CopyAndroidLocalMPQ(System.Action<string> act)
    {
        string path =  ProjectSetting.MPQLocalPath +"/MPQ/"+ ProjectSetting.UPDATE_FILE_NAME;
        GameGlobal.Instance.StartCoroutine(GetWWWTxt(path, (www) =>
        {
            Debugger.Log("GetWWWTxt ok Text: " + www.text);
            if (string.IsNullOrEmpty(www.error))
            {
                string version_text = www.text;
                //取出资源列表
                char[] spc = { ':' };
                string[] lines = version_text.Split('\n');
                List<string> copylist = new List<string>();
                foreach (string line in lines)
                {
                    string[] kv = line.Split(spc, 3);
                    if (kv.Length == 3)
                    {
                        string key = kv[2].Trim().Replace('\\', '/');
                        string md5 = kv[0].Trim();
                        uint fsize = uint.Parse(kv[1].Trim());

                        string filepath = ProjectSetting.MPQSavedPath + key;
                        if (!File.Exists(filepath) && key.ToLower().EndsWith(".mpq"))
                        {
                            copylist.Add(key);
                        }
                    }
                }
                if (copylist.Count > 0)
                {
                    string zipfile = Application.dataPath;
                    FileUtils.CopyZipFiles(zipfile, copylist, ProjectSetting.MPQSavedPath);
                    act.Invoke("");
                }
                else
                {
                    FileUtils.TryResetCopy();
                    act.Invoke("NoCopy");
                    return;
                }
            }
            else
            {
                act.Invoke("NoCopy");
                Debug.LogError("GetWWWTxt error Text: " + www.error);
            }
        }));

    }
	#endif




    static private bool _IsFileSystemInit = false;

    public static void InitMPQFileSystem(MPQUpdater updater)
    {
        if(_IsFileSystemInit)
        {
            return;
        }
        DeepCore.MPQ.MPQFileSystem fsys = new DeepCore.MPQ.MPQFileSystem();

        var alist = updater.GetAllRemoteFiles();

        int chapterIndex = -1;
        // 取最大
        foreach (var info in alist)
        {
            int index = -1;
            if (int.TryParse(info.userdata, out index))
            {
                chapterIndex = Math.Max(chapterIndex, index);
            }
            else
            {
                // 全章节资源
                chapterIndex = -1;
                break;
            }
        }

        SetChapterForce(chapterIndex);

        if (fsys.init(updater))
        {
            _IsFileSystemInit = true;
            Debugger.Log("MPQ 初始化成功");
            UnityDriver.AddFileSystem(fsys);
        }
        else
        {
            Debugger.Log("MPQ 初始化失败");
        }

    }



    /// <summary>
    /// 不经过更新步骤的MPQ初始化
    /// </summary>
    private static void InitMPQFileSystem(DirectoryInfo filetest)
    {
        if (filetest.Exists)
        {
            DeepCore.MPQ.MPQFileSystem fsys = new DeepCore.MPQ.MPQFileSystem();

            if (fsys.init(filetest))
            {
                Debugger.Log("MPQ init success");
#if !UNITY_IOS
                UnityDriver driver = UnityDriver.UnityInstance;
                UnityDriver.SetDirver();
#endif
                UnityDriver.AddFileSystem(fsys);
            }
            else
            {
                Debugger.Log("MPQ init failed");
            }
        }
        else
        {
            Debugger.Log("MPQ 初始化失败");
        }

    }

    public static void InitLoclMPQFileSystem(System.Action<string> act)
    {
        if (_IsFileSystemInit)
        {
            act.Invoke("");
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        UpdateUtil.CopyAndroidLocalMPQ(txt =>
        {
            DirectoryInfo filetest = new DirectoryInfo(ProjectSetting.MPQSavedPath +
                        ProjectSetting.PLATFORM_DIR_NAME);
            InitMPQFileSystem(filetest);
            act.Invoke(txt);
        });
#else
        DirectoryInfo filetest = new DirectoryInfo(ProjectSetting.MPQLocalPath);
        InitMPQFileSystem(filetest);
        act.Invoke("");
#endif

    }

    private static int mChapterIndex = -1;
    //private static int mChapterIndex = 0;

    /// <summary>
    /// -1 表示是全资源状态
    /// </summary>
    public static int ChapterIndex
    {
        get { return mChapterIndex; }
        private set { mChapterIndex = value; }
    }

    public static bool IsFullRes
    {
        get { return ChapterIndex == -1; }
    }

    public static bool OnCheckValid(MPQUpdater.RemoteFileInfo remoteFile)
    {
        //全资源状态全下
        if (ChapterIndex < 0)
        {
            return true;
        }
        //章节状态只下<=本章节的资源
        int curIndex;
        return !string.IsNullOrEmpty(remoteFile.userdata) &&
                int.TryParse(remoteFile.userdata, out curIndex) &&
                curIndex <= ChapterIndex;
    }

    public static void OnBeforeCheckRemoteFiles(List<MPQUpdater.RemoteFileInfo> alist )
    {
        //TODO 通过渠道号等，指定当前的资源状态，直接设置Chapter值，或者指定
        if (PlayerPrefs.HasKey("Chapter"))
        {
            ChapterIndex = PlayerPrefs.GetInt("Chapter");
        }
        else
        {
            int chapterIndex = -1;
            //userdata 表示章节序号
            foreach (var info in alist)
            {
                int index = -1;
                if (int.TryParse(info.userdata, out index))
                {
                    if (info.file.Exists)
                    {
                        //本地存在，取最大值
                        chapterIndex = Math.Max(chapterIndex, index);
                    }
                    else
                    {
                        //本地不存在
                        chapterIndex = Math.Max(chapterIndex, 0);
                    }
                }
                else if (info.file.Exists)
                {
                    // 全章节资源
                    chapterIndex = -1;
                    break;
                }
            }

            SetChapterForce(chapterIndex);
        }
    }

    public static MPQUpdater.UpdateInfo CheckNeedUpdateInfo(string version_url)
    {

        DirectoryInfo save_root = new DirectoryInfo(ProjectSetting.MPQSavedPath);
        DirectoryInfo bundle_root = new DirectoryInfo(ProjectSetting.MPQLocalPath);
        return MPQUpdater.CheckNeedUpdate(new Uri(version_url),
                                    // auto update file name
                                    ProjectSetting.UPDATE_FILE_NAME,
                                    // save to local path
                                    save_root,
                                    bundle_root,
                                    OnBeforeCheckRemoteFiles,
                                    OnCheckValid);
    }

    public struct UpdaterCreateInfo
    {
        public Uri remote_version_url;
        public string[] remote_version_prefix;
        public string version_suffix;
        public DirectoryInfo local_save_root;
        public DirectoryInfo local_bundle_root;
        public bool validate_md5;
    }

    private static UpdaterCreateInfo LastUpdaterInfo;

    public static MPQUpdater CreateMpqUpdater(UpdaterCreateInfo info, MPQUpdaterListener listener)
    {
        MPQUpdater updater = UnityDriver.CreateMPQUpdater(info.remote_version_url,
            // auto update remote root url
            info.remote_version_prefix,
            // auto update file name
            info.version_suffix,
            info.local_save_root,
            info.local_bundle_root,
            info.validate_md5,
            listener);
        updater.OnCheckVaild += OnCheckValid;

        LastUpdaterInfo = info;
        return updater;
    }

    /// <summary>
    /// 下载指定扩展资源
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="chapterIndex"></param>
    /// <returns></returns>
    public static MPQUpdater CreateMpqUpdater(MPQUpdaterListener listener, int chapterIndex)
    {
        MPQUpdater updater = UnityDriver.CreateMPQUpdater(LastUpdaterInfo.remote_version_url,
            // auto update remote root url
            LastUpdaterInfo.remote_version_prefix,
            // auto update file name
            LastUpdaterInfo.version_suffix,
            LastUpdaterInfo.local_save_root,
            LastUpdaterInfo.local_bundle_root,
            LastUpdaterInfo.validate_md5,
            listener);
        _IsFileSystemInit = false;
        updater.OnCheckVaild += (remoteFile) =>
        {
            //全资源状态全下
            if (chapterIndex < 0)
            {
                return true;
            }
            //章节状态只下<=本章节的资源
            int curIndex;
            return !string.IsNullOrEmpty(remoteFile.userdata) &&
                    int.TryParse(remoteFile.userdata, out curIndex) &&
                    curIndex <= chapterIndex;
        };

        return updater;
    }


    public static MPQUpdater.UpdateInfo CheckNeedUpdateInfo(int chapterIndex)
    {
        return MPQUpdater.CheckNeedUpdate(LastUpdaterInfo.remote_version_url,
                                    // auto update file name
                                    LastUpdaterInfo.version_suffix,
                                    // save to local path
                                    LastUpdaterInfo.local_save_root,
                                    LastUpdaterInfo.local_bundle_root,
                                    null,
            (remoteFile) =>
            {
                //全资源状态全下
                if (chapterIndex < 0)
                {
                    return true;
                }
                //章节状态只下<=本章节的资源
                int curIndex;
                return !string.IsNullOrEmpty(remoteFile.userdata) &&
                        int.TryParse(remoteFile.userdata, out curIndex) &&
                        curIndex <= chapterIndex;
            });
    }

    public static void SetChapterForce(int chapterIndex,bool save = true)
    {
        ChapterIndex = chapterIndex;
        if (save)
        {
            PlayerPrefs.SetInt("Chapter", ChapterIndex);
        }
    }
}