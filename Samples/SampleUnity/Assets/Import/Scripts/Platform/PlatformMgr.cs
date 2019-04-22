using UnityEngine;
using System.Collections;

public class PlatformMgr : MonoBehaviour 
{

    IPlatform mIPlatform;

    //gu:网络状态数值意义  
    private enum NetworkStatus
    {
      NotConnect = 0,
      WiFi       = 1,
      Network2G  = 2,
      Network3G  = 3,
      Network4G  = 4,
   }
    private const string NOT_CONNECT = "未连接网络";

    private const string UNKNOW_NETWORK_STATUS = "未知的网络类型";

    private const string WIFI = "WiFi";

    private const string NETWORK_2G = "2G";

    private const string NETWORK_3G = "3G";

    private const string NETWORK_4G = "4G";

    static PlatformMgr mPlatformMgr;

    void Awake()
    {
        mPlatformMgr = this;

#if UNITY_IOS && !UNITY_EDITOR
        mIPlatform = this.gameObject.AddComponent<IosPlatform>();
#elif UNITY_ANDROID && !UNITY_EDITOR
        mIPlatform = this.gameObject.AddComponent<AndroidPlugin>();
#else
        mIPlatform = this.gameObject.AddComponent<PcPlatform>();
#endif
    }


    // Use this for initialization
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);

        Debugger.Log("mIPlatform =" + mIPlatform);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public static double PluginGetUsedMemory()
    {
		if (mPlatformMgr.mIPlatform == null)
			return 0;

        return mPlatformMgr.mIPlatform.GetUsedMemory();
    }

    public static double PluginGetAvailableMemory()
    {
        if (mPlatformMgr.mIPlatform == null)
            return 0;

        return mPlatformMgr.mIPlatform.GetAvailableMemory();
    }

    public static string PluginGetDeviceType()
    {
		if (mPlatformMgr.mIPlatform == null)
			return "";
		
		return mPlatformMgr.mIPlatform.GetDeviceType();
    }

	public static string PluginGetUUID()
	{
		if (mPlatformMgr.mIPlatform == null)
			return "000000";

        return mPlatformMgr.mIPlatform.GetMacAddress();

	}

    public static string PluginGetNetworkStatus()
    {
        if (mPlatformMgr.mIPlatform == null)
            return "";
        

		string sPlatform = "";

        switch(mPlatformMgr.mIPlatform.GetNetworkStatus())
        {
            case (int)NetworkStatus.NotConnect:
                sPlatform = NOT_CONNECT;
                break;
            case (int)NetworkStatus.WiFi:
                sPlatform = WIFI;
                break;
            case (int)NetworkStatus.Network2G:
                sPlatform = NETWORK_2G;
                break;
            case (int)NetworkStatus.Network3G:
                sPlatform = NETWORK_3G;
                break;
            case (int)NetworkStatus.Network4G:
                sPlatform = NETWORK_4G;
                break;
            default:
                sPlatform = "";
                break;
        }

        return sPlatform;
    }

    public static string PluginGetSignalStrength()
    {
        if (mPlatformMgr.mIPlatform == null)
            return "";


        string sPlatform = "";


        return sPlatform + mPlatformMgr.mIPlatform.GetSignalStrength();
    }

    public static long GetFreeSpace(string driveDirectoryName)
    {
        if (mPlatformMgr.mIPlatform == null)
            return long.MaxValue;

        driveDirectoryName = System.IO.Path.GetPathRoot(driveDirectoryName);

        return mPlatformMgr.mIPlatform.GetFreeSpace(driveDirectoryName);
    }

    public static long GetHardDiskSpace(string driveDirectoryName)
    {
        if (mPlatformMgr.mIPlatform == null)
			return long.MaxValue;
		
		driveDirectoryName = System.IO.Path.GetPathRoot(driveDirectoryName);

        return mPlatformMgr.mIPlatform.GetHardDiskSpace(driveDirectoryName);
    }

    public static string PluginGetUserAgent()
    {
        if (mPlatformMgr.mIPlatform == null)
            return "";

        return mPlatformMgr.mIPlatform.GetUserAgent();

    }

    public static int PluginGetScreenNotch()
    {
        if (mPlatformMgr.mIPlatform == null)
            return 0;

        return mPlatformMgr.mIPlatform.GetScreenNotch();
    }

    public static void DoUpdate(string url)
    {
        if (mPlatformMgr.mIPlatform != null)
            mPlatformMgr.mIPlatform.DoUpdate(url);
    }

    public static void CaptureScreenshot(string fullPathName)
    {
        if (mPlatformMgr.mIPlatform != null)
            mPlatformMgr.mIPlatform.CaptureScreenshot(fullPathName);
    }

    public static string GetPasteboard()
    {
        return mPlatformMgr.mIPlatform.GetPasteboard();
    }

    public static void SetPasteboard(string text)
    {
        mPlatformMgr.mIPlatform.SetPasteboard(text);
    }

    public static int GetBatteryLeftQuantity()
    {
        return mPlatformMgr.mIPlatform.GetBatteryLeftQuantity();
    }

    /// <summary>
    /// 设置屏幕亮度
    /// </summary>
    /// <param name="value"> 取值范围 0~255   -1为恢复默认亮度</param>
    public static void SetBrightness(int value)
	{
		mPlatformMgr.mIPlatform.SetBrightness(value); 
	}
}
