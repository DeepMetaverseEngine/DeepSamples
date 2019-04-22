using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
#if UNITY_IOS
public class IosPlatform : MonoBehaviour, IPlatform
{
//    [DllImport("__Internal")]
//	private static extern double _getUsedMemory();
//
//    [DllImport("__Internal")]
//    private static extern double _getAvailableMemory();
    
    [DllImport("__Internal")]
	private static extern string  _getDeviceType();	
	
	[DllImport("__Internal")]
	private static extern string  _getDeviceUUID();	


	[DllImport("__Internal")]
	private static extern int  _getNetworkStatus();	

	[DllImport("__Internal")]
	private static extern int  _getSignalStrength();
	

	[DllImport("__Internal")]
	private static extern int  _getScreenScaleFactor();

//	[DllImport("__Internal")]
//	private static extern double  _getFreeDiskSpace();

//    [DllImport("__Internal")]
//    private static extern void _DoUpdate(string url);

//    [DllImport("__Internal")]
//    private static extern void _saveIamgeToAlbums(string filename);

//    [DllImport("__Internal")]
//    private static extern string _getPasteboard();

//    [DllImport("__Internal")]
//    private static extern void _setPasteboard(string text);

    [ DllImport( "__Internal" )]
    private static extern float _getiOSBatteryLevel();

//	[ DllImport( "__Internal" )]
//	private static extern void _setBrightness(float val);
//
//	[ DllImport( "__Internal" )]
//	private static extern float _getBrightness();


	[DllImport("__Internal")]
	private static extern string _getUserAgent();



    /// <summary>
    /// 获取自身已用内存.
    /// </summary>
    /// <returns></returns>
    public double GetUsedMemory()
    {
		return double.MaxValue;
    }

    /// <summary>
    /// 获取可用内存.
    /// </summary>
    /// <returns></returns>
    public double GetAvailableMemory()
    {
		return double.MaxValue;
		//return _getAvailableMemory() / 1024 / 1024;
    }
	string _device = string.Empty;

    /// <summary>
    /// 获取可用内存.
    /// </summary>
    /// <returns></returns>
	public string GetDeviceType()
    {
		if (_device == string.Empty)
			_device = _getDeviceType();
		
		return _device;
    }

	string _macaddress = string.Empty;

	/// <summary>
	/// 获取mac地址
	/// </summary>
	/// <returns>The mac address.</returns>
    public string GetMacAddress()
    {
		if (_macaddress == string.Empty)
			_macaddress = _getDeviceUUID();

		return _macaddress;
    }

    //gu:添加网络状态和信号强度  

    int _networkStatus = 0;

    /// <summary>
    /// 获取网络状态
    /// </summary>
	/// <returns>The network status.</returns>GetScreenNotch
    public int GetNetworkStatus()
    {
		if (_networkStatus == 0)
			_networkStatus = _getNetworkStatus();

		return _networkStatus;
    }

    int _signalStrength = 0;

    /// <summary>
    /// 获取信号强度
    /// </summary>
    /// <returns>The signal strength.</returns>
    public int GetSignalStrength()
    {
		_signalStrength = _getSignalStrength();

		return _signalStrength;
    }

    // Use this for initialization
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);

#if UNITY_I

        iPhoneSettings.screenCanDarken = false;
#endif

    }

    public long GetFreeSpace(string driveDirectoryName)
    {
		return long.MaxValue;
    }

    public long GetHardDiskSpace(string driveDirectoryName)
    {
        return long.MaxValue;
    }

	public string GetUserAgent()
	{
#if UNITY_IOS
		return _getUserAgent();
#else
		return "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
#endif
	}

    public void DoUpdate(string url)
    {
		Application.OpenURL(url);
    }

    public void CaptureScreenshot(string filename)
    {
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0, true);
        byte[] imagebytes = tex.EncodeToPNG();//转化为png图
        tex.Compress(false);//对屏幕缓存进行压缩
        File.WriteAllBytes(filename, imagebytes);//存储png图

		//_saveIamgeToAlbums(filename);
    }

    //获取粘贴板内容
    public string GetPasteboard()
    {
		return "";
       // return _getPasteboard();
    }

    //设置粘贴板内容
    public void SetPasteboard(string text)
    {
        //_setPasteboard(text);
    }
    
    public int GetBatteryLeftQuantity()
    {
        return (int)(_getiOSBatteryLevel());
    }

	public void SetBrightness(int percent)
	{
        float value = (float)percent / 100.0f;
//		_setBrightness(value);
	}

	public int GetBrightness()
	{
        int brightness = -1;
//		brightness = (int)_getBrightness();
//        brightness = brightness * 100;
		return brightness;
	}

    public void SetAutoBrightness(bool auto) { }

    public bool IsAutoBrightness()
    {
        return false;
    }

	public int GetScreenNotch()
	{
		return _getScreenScaleFactor ();
	}

}
#endif