using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameLoadMgr
{

    private int mProcess = -1;
    
    public bool LoadFinish { private set; get; }

    private enum LoadProcess
    {
        Start = 0,

        WaitToLoadScene = 75,

        LoadHudStart = 76,
        LoadHudEnd = 88,

        LoadUICacheStart = 89,
        LoadUICacheEnd = 99,

        Complete = 100,
    }

    private static GameLoadMgr mInstance = null;

    public static GameLoadMgr Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = new GameLoadMgr();
            return mInstance;
        }
    }

    public int Loading()
    {
        switch (mProcess)
        {
            case (int)LoadProcess.Start:
                //mCurFrameRate = Application.targetFrameRate;
                //Application.targetFrameRate = 120;
                break;
            case (int)LoadProcess.WaitToLoadScene:
                if (!BattleLoaderMgr.Instance.IsLoadFinish())
                    return mProcess;
                break;
            case (int)LoadProcess.LoadHudStart:
                HudManager.Instance.InitHud(false);
                break;
            case (int)LoadProcess.LoadHudEnd:
                if (!HudManager.Instance.InitFinish)
                    return mProcess;
                break;
            case (int)LoadProcess.LoadUICacheStart:
                MenuMgr.Instance.PreLoadUI(false);
                break;
            default:
                if (mProcess < (int)LoadProcess.WaitToLoadScene)
                {
                    if (BattleLoaderMgr.Instance.IsLoadFinish())    //场景已经预加载了就直接跳到下一阶段 
                        mProcess = (int)LoadProcess.WaitToLoadScene;
                }
                break;
        }

        mProcess++;
        if (mProcess >= (int)LoadProcess.Complete)
        {
            mProcess = (int)LoadProcess.Complete;
            //Application.targetFrameRate = mCurFrameRate;
            LoadFinish = true;
        }
        return mProcess;
    }

    public void StartGameLoad()
    {
        GameGlobal.Instance.StartCoroutine(StartLoading());
    }

    IEnumerator StartLoading()
    {
        LoadFinish = false;
        mProcess = 0;
        while (mProcess < (int)LoadProcess.Complete)
        {
            Loading();
            yield return 1;
        }
    }

    public int GetLoadProcess()
    {
        return mProcess == -1 ? 0 : mProcess;
    }

}
