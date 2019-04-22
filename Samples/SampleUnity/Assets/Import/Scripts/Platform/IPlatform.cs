using UnityEngine;
using System.Collections;

public interface IPlatform
{
    /// <summary>
    /// 获取mac地址
    /// </summary>
    string GetMacAddress();

    /// <summary>
    /// 获取占用内存.
    /// </summary>
    double GetUsedMemory();


    /// <summary>
    /// 获取系统可用内存.
    /// </summary>
    double GetAvailableMemory();

    /// <summary>
    /// 获取设备类型.
    /// </summary>
    string GetDeviceType();


    //gu:添加网络状态和信号强度    
    /// <summary>
    /// 获取网络状态.
    /// </summary>
    int GetNetworkStatus();

    /// <summary>
    /// 获取信号强度.
    /// </summary>
    int GetSignalStrength();

    /// <summary>
    /// 获取刘海高度
    /// </summary>
    /// <returns></returns>
    int GetScreenNotch();

    long GetFreeSpace(string driveDirectoryName);

    long GetHardDiskSpace(string driveDirectoryName);

    string GetUserAgent();

    //强制更新
    void DoUpdate(string url);

    //截图
    void CaptureScreenshot(string filename);

    //获取粘贴板内容
    string GetPasteboard();
    //设置粘贴板内容
    void SetPasteboard(string text);

    int GetBatteryLeftQuantity();

    /// <summary>
    /// 设置屏幕亮度
    /// </summary>
    /// <param name="value"> 取值范围 0~255   -1为恢复默认亮度</param>
	void SetBrightness(int value);
}
