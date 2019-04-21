using DeepCore.MPQ.Updater;
using DeepCore.Unity3D.Impl;
using System.IO;
using UnityEngine;

public class Updater : MonoBehaviour, MPQUpdaterListener
{

    public string MPQ_PREFIX = "http://192.168.1.81/mpq/z/";
    public string MPQ_SUFFIX = "updates_pvr/update_version.txt";
    public string ZIP_EXT = ".z";

    private MPQUpdater updater;
    private string updater_status = "";
    private bool is_done = false;

    void Start()
    {
        UnityDriver.UnityInstance.ToString();

#if UNITY_IOS || UNITY_ANDROID
        if (!Directory.Exists(Application.persistentDataPath + "/http_res"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/http_res");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/res"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/res");
        }
        DirectoryInfo save_root = new DirectoryInfo(Application.persistentDataPath + "/http_res");
        DirectoryInfo bundle_root = new DirectoryInfo(Application.persistentDataPath + "/res");
#else
        DirectoryInfo save_root = new DirectoryInfo(Path.GetFullPath("./http_res"));
        DirectoryInfo bundle_root = new DirectoryInfo(Path.GetFullPath("./res"));
#endif
        MPQUpdater.ZIP_EXT = ZIP_EXT;
        updater = UnityDriver.CreateMPQUpdater(new string[] { MPQ_PREFIX }, MPQ_SUFFIX, save_root, bundle_root, true, this);
        updater.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (updater != null)
        {
            updater.Update();
        }
    }

    void OnGUI()
    {
        int sw = 400;
        int sh = 200;
        int sx = (Screen.width - sw) / 2;
        int sy = (Screen.height - sh) / 2;

        GUI.Box(new Rect(sx, sy, sw, sh), "");

        if (updater != null)
        {
            string download_info = updater.CurrentDownloadFile +
                "\n(" + updater.CurrentDownloadBytes + "/" + updater.TotalDownloadBytes + ")" +
                " " + (updater.CurrentDownloadSpeed / 1024) + "KB/S";

            string unzip_info = updater.CurrentUnzipFile +
                "\n(" + updater.CurrentUnzipBytes + "/" + updater.TotalUnzipBytes + ")" +
                " " + (updater.CurrentUnzipSpeed / 1024) + "KB/S";

            GUI.Label(new Rect(
                sx + 8,
                sy + 8 + 50 * 0,
                sw - 16, 50),
                download_info);

            GUI.Label(new Rect(
                sx + 8,
                sy + 8 + 50 * 1,
                sw - 16, 50),
                unzip_info);

            GUI.Label(new Rect(
                sx + 8,
                sy + 8 + 50 * 2,
                sw - 16, 50),
                " status : " + updater_status + "\n" +
                "running : " + updater.IsRunning + "");
        }

        if (is_done)
        {
            if (GUI.Button(new Rect(
                sx + (sw - 100) / 2,
                sy + sh - 8 - 30,
                100, 30),
                "Enter"))
            {
                Application.LoadLevel("MyCanvas");
            }
        }
    }


    public void onEvent(MPQUpdater updater, MPQUpdaterEvent e)
    {
        switch (e.EventType)
        {
            case MPQUpdaterEvent.TYPE_COMPLETE:
                updater_status = "Complete";
//                 MPQFileSystem fs = new MPQFileSystem();
//                 fs.init(updater);
//                UnityDriver.AddFileSystem(fs);
                break;
            case MPQUpdaterEvent.TYPE_ERROR:
                updater_status = e.ToString();
                break;
            case MPQUpdaterEvent.TYPE_NOT_ENOUGH_SPACE:
                updater_status = e.ToString();               
				break;
        }
    }

}
