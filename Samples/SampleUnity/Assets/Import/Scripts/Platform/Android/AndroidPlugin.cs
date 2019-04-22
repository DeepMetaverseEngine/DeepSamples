using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public class AndroidPlugin : MonoBehaviour, IPlatform
{
    private const string PLUGIN_NAME = "AndroidPlugin";

    /// <summary>
    /// Native层注册的网络状态变更广播类型
    /// </summary>
    private const int BROADCAST_TYPE_NETWORK = 1;

    /// <summary>
    /// Native层注册的电量变化广播类型
    /// </summary>
    private const int BROADCAST_TYPE_BATTERY = 2;

    static string _storagePath = string.Empty;

	static string _macAddress = string.Empty;

	static string _deviceModel = string.Empty;

    static string _userAgent = string.Empty;

    static long _freeStorageSpace = long.MaxValue;

    static long _totalStorageSpace = long.MaxValue;

    /// <summary>
    /// 0没有网络 1 wifi 2 2G 3 3G 4 4G
    /// </summary>
    static int _networktype = 0;

    /// <summary>
    /// 电量
    /// </summary>
    static int _batteryLife = 100;


    public static string GetStoragePath() { return _storagePath; }

    public string GetMacAddress() { return _macAddress; }

    public double GetUsedMemory() { return -1; }

    public string GetDeviceType() { return _deviceModel; }


#if UNITY_ANDROID
	private AndroidJavaObject jo = null;
	
	private static AndroidPlugin instance;
	
	void Awake()
	{
        instance = this;

        Init();

        crateNetWorkBroadcast();

        crateBatteryBroadcast();

        _getNetWorkType();

        _getBatteryLeftQuantity();

        _getStoragePath();

        _getMacAddress();

        _getDeviceModel();

        _getUserAgent();

        _getDirFreeStorageSpace();

        _getDirTotalStorageSpace();

        _detectScreenNotch();
    }
	
	void Start () 
    {
        GameObject.DontDestroyOnLoad(gameObject);
        gameObject.name = PLUGIN_NAME;
    }

    private void Init()
    {
        try
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Init JavaClass Object == false");
        }
    }

    private T NativeCall<T>(string javaClass, string method, params object[] args)
    {
        try
        {
            using (AndroidJavaClass obj = new AndroidJavaClass(javaClass))
            {
                var result = obj.CallStatic<T>(method, args);
                Debug.Log("NativeCall Class=" + javaClass + " Method=" + method + " params: " + MiniJSON.Json.Serialize(args) + " result: " + result);
                return result;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return default(T);
    }

    private void NativeCall(string javaClass, string method, params object[] args)
    {
        try
        {
            using (AndroidJavaClass obj = new AndroidJavaClass(javaClass))
            {
                Debug.Log("NativeCall Class=" + javaClass + " method=" + method + " params: " + MiniJSON.Json.Serialize(args));
                obj.CallStatic(method, args);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void crateNetWorkBroadcast()
    {
        NativeCall("com.onegame.NetWorkUtil", "crateNetWorkBroadcast", jo);
    }

    private void crateBatteryBroadcast()
    {
        NativeCall("com.onegame.SystemUtils", "crateBatteryBroadcast", jo);
    }

    private void destoryNetWorkBroadcast()
    {
        NativeCall("com.onegame.NetWorkUtil", "destoryNetWorkBroadcast", jo);
    }

    private void _getStoragePath()
    {
#if !UNITY_EDITOR
        _storagePath = NativeCall<string>("com.onegame.SystemUtils", "getStoragePath", jo);

#else
        _storagePath = Application.persistentDataPath;
#endif
    }

    private void _DoUpdate(string url){

        Application.OpenURL(url);
	}

    private void _CaptureScreenshot(string filename,string path){
        //转存相册
        NativeCall("com.onegame.AlbumManager", "saveScreenToAlbums", filename, path, jo);
    }

    /// <summary>
    /// 获取网络状态，wifi,wap,2g,3g,4g
    /// </summary>
    /// <returns></returns>
    private void _getNetWorkType()
    {
        _networktype = NativeCall<int>("com.onegame.NetWorkUtil", "getNetWorkType", jo);
    }


    private void _getMacAddress()
    {
        _macAddress = NativeCall<string>("com.onegame.NetWorkUtil", "getLocalMacAddress", jo);
    }

    private void _getDeviceModel()
    {
        _deviceModel = NativeCall<string>("com.onegame.SystemUtils", "getProductModel");
    }

    private void _getUserAgent()
    {
        _userAgent = NativeCall<string>("com.onegame.SystemUtils", "getUserAgent", jo);
    }

    /// <summary>
    /// 获取指定路径所在磁盘可用空间
    /// </summary>
    /// <param name="path">Example: /mnt/sdcard</param>
    /// <returns></returns>
    private void _getDirFreeStorageSpace()
    {
        _freeStorageSpace = NativeCall<long>("com.onegame.SystemUtils", "getDirFreeMemory", _storagePath);
    }

    /// <summary>
    /// 获取指定路径所在磁盘总空间
    /// </summary>
    /// <param name="path">Example: /mnt/sdcard</param>
    /// <returns></returns>
    private void _getDirTotalStorageSpace()
    {
        _totalStorageSpace = NativeCall<long>("com.onegame.SystemUtils", "getDirTotalMemory", _storagePath);
    }

    private void AndroidReceive(string data)
    {
        Debug.Log("AndroidPlugin AndroidReceive:" + data);
        var resultMap = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
        if(resultMap != null)
        {
            int type,value;
            int.TryParse(resultMap["type"].ToString(), out type);
            int.TryParse(resultMap["value"].ToString(), out value);
            if (type == BROADCAST_TYPE_NETWORK)
                _networktype = value;
            else if (type == BROADCAST_TYPE_BATTERY)
                _batteryLife = value;
        }
    }

    private void _getBatteryLeftQuantity()
    {
        _batteryLife = NativeCall<int>("com.onegame.SystemUtils", "getBatteryLeftQuantity", jo);
    }

    private void _detectScreenNotch()
    {
       NativeCall("com.onegame.SystemUtils", "detectScreenNotch", jo);
    }
#endif

    public int GetNetworkStatus()
    {
        return _networktype;
    }

    public int GetSignalStrength()
    {
        return 0;
    }

    public int GetBatteryLeftQuantity() { return _batteryLife; }

    public long GetFreeSpace(string driveDirectoryName) { return _freeStorageSpace; }

    public long GetHardDiskSpace(string driveDirectoryName) { return _totalStorageSpace; }

    public string GetUserAgent() { return _userAgent; }

    public void DoUpdate(string url)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _DoUpdate(url);
#endif
    }

    public void CaptureScreenshot(string fullPathName)
    {
        int index = 0;
        index = fullPathName.LastIndexOf('/');
        string filename = fullPathName.Substring(index + 1, fullPathName.Length - index - 1);
        string path = fullPathName.Substring(0, fullPathName.LastIndexOf('/') + 1);

        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
        byte[] imagebytes = tex.EncodeToPNG();//转化为png图
        tex.Compress(false);//对屏幕缓存进行压缩
        File.WriteAllBytes(fullPathName, imagebytes);//存储png图
#if UNITY_ANDROID && !UNITY_EDITOR
        _CaptureScreenshot(filename, path);
#endif
    }

    //获取粘贴板内容
    public string GetPasteboard()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var _pastedText = NativeCall<string>("com.onegame.SystemUtils", "getPasteboard", jo);
        return _pastedText;
#endif
        return "";
    }

    //设置粘贴板内容
    public void SetPasteboard(string text)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        NativeCall("com.onegame.SystemUtils", "setPasteboard", jo, text);
#endif
    }

    public double GetAvailableMemory()
    {
        return _freeStorageSpace;
    }

    /// <summary>
    /// 取值范围 0~255   -1为恢复默认亮度
    /// </summary>
    /// <param name="value"></param>
    public void SetBrightness(int value)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        NativeCall("com.onegame.SystemUtils", "setBrightness", jo, value);
#endif
    }

    public int GetScreenNotch()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return NativeCall<int>("com.onegame.SystemUtils", "getScreenNotch");
#endif
        return 0;
    }
}
