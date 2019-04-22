using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PcPlatform : MonoBehaviour, IPlatform
{
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);

    }

    string macAdress = string.Empty;

    public string GetMacAddress()
    {
        if (!string.IsNullOrEmpty(macAdress))
            return macAdress;

        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface adapter in nics)
        {

            PhysicalAddress address = adapter.GetPhysicalAddress();

            if (address.ToString() != "")
            {
                macAdress = address.ToString();

            }

        }

        return macAdress;
    }

    public double GetUsedMemory()
    {
        return 0;
    }

    public string GetDeviceType()
    {
        return "PC";
    }

    //gu:添加网络状态和信号强度  
    public int GetNetworkStatus()
    {
        return 1;
    }

    public int GetSignalStrength()
    {
        return 0;
    }


    public long GetFreeSpace(string driveDirectoryName)
    {
        return long.MaxValue;

        //long freefreeBytesAvailable = 0;

        //try
        //{
        //    //string[] drives = Directory.GetLogicalDrives();
        //    //foreach (string drive in drives)
        //    //{
        //    //    Debuger.Log("PcPlatform.GetFreeSpace is " + drive);
        //    //}

        //    //Debuger.Log("PcPlatform.GetFreeSpace driveDirectoryName is " + driveDirectoryName);

        //    System.IO.DriveInfo driver = new DriveInfo(driveDirectoryName);

        //    freefreeBytesAvailable = driver.TotalFreeSpace;

        //    //return freefreeBytesAvailable;



        //    //System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
        //    //foreach (System.IO.DriveInfo drive in drives)
        //    //{
        //    //    if (drive.Name.ToLower() == driveDirectoryName.ToLower())
        //    //    {
        //    //        freefreeBytesAvailable = drive.TotalFreeSpace;
        //    //    }
        //    //}
        //}
        //catch (System.Exception error)
        //{
        //    //string msg = "[InnerException:]" + error.InnerException +
        //    //"[Exception:]" + error.Message +
        //    //"[Source:]" + error.Source +
        //    //"[StackTrace:]" + error.StackTrace;

        //    Debug.LogException(error, this);
        //}
            
        //Debuger.Log("PcPlatform.GetFreeSpace" + freefreeBytesAvailable);

        //return freefreeBytesAvailable;

    }

    /// <summary>
    /// 获取指定驱动器的空间总大小(单位为B) 
    /// </summary>
    /// <param name="driveDirectoryName">驱动器名 </param> 
    /// <returns>驱动器的空间总大小(单位为B) </returns>
    public long GetHardDiskSpace(string driveDirectoryName)
    {
        return long.MaxValue;

        long totalSize = new long();
        string[] drives = Directory.GetLogicalDrives();
        foreach (string drive in drives)
            Debugger.Log("PcPlatform.GetHardDiskSpace is " + drive);

        Debugger.Log("PcPlatform.GetHardDiskSpace" + totalSize + " " + driveDirectoryName);


        System.IO.DriveInfo driver = new DriveInfo(driveDirectoryName);

        totalSize = driver.TotalFreeSpace;

        return totalSize;
        //long totalSize = new long();
        //System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
        //foreach (System.IO.DriveInfo drive in drives)
        //{
        //    if (drive.Name.ToLower() == driveDirectoryName.ToLower())
        //    {
        //        totalSize = drive.TotalSize;
        //    }
        //}
        return totalSize;
    }

    public string GetUserAgent()
    {
        return "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";    //pc的随便赋个值，跟WebClient里的一样 
    }

    public void DoUpdate(string url)
    {

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
        //Application.CaptureScreenshot(filename);
    }

    //获取粘贴板内容
    public string GetPasteboard()
    {
        TextEditor te = new TextEditor();
        //te.OnFocus();
        te.Paste();
        return te.text;
    }

    //设置粘贴板内容
    public void SetPasteboard(string text)
    {
        TextEditor te = new TextEditor();
        te.text = text;
        te.OnFocus();
        te.Copy();
    }

    public double GetAvailableMemory()
    {
        return SystemInfo.systemMemorySize;
    }

    public int GetBatteryLeftQuantity()
    {
        return 100;
    }


	public void SetBrightness(int percent)
	{

	}

	public int GetBrightness()
	{
		return -1;
    }
    public void SetAutoBrightness(bool auto) { }
    public bool IsAutoBrightness()
    {
        return false;
    }

    public int GetScreenNotch()
    {
        return 0;
    }
}
