using UnityEngine;
using System.Collections.Generic;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;

public class BattleLoaderMgr
{
    
    /// <summary>
    /// 全局控制是否使用场景缓存，具体的机制可以是 分析内存、硬件平台、机型等，待完善
    /// </summary>
    public const bool UseSceneCache = false;

	public bool localBattle = true;

    /// <summary>
    /// 当前正在使用的场景
    /// </summary>
    private BattleLoader mBattleLoader;

    /// <summary>
    /// 所有已加载场景的集合
    /// </summary>
    private Dictionary<int, BattleLoader> mBattleLoaders = new Dictionary<int, BattleLoader>();
    /// <summary>
    /// 所有正在销毁的缓存场景的集合
    /// </summary>
    private Dictionary<int, BattleLoader> mDestroyBLQueue = new Dictionary<int, BattleLoader>();

    private int mWaitToPreLoadSceneId;

    private static BattleLoaderMgr mInstance = null;

    public static BattleLoaderMgr Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = new BattleLoaderMgr();
            return mInstance;
        }
    }

	public bool LoadSceneById(int sceneId, int userId = 1)
    {
        bool needLoadScene = false;
        if (!IsUseCache())
        {
            DestroyCacheExcept(sceneId);
        }
        mBattleLoaders.TryGetValue(sceneId, out mBattleLoader);
        if (mBattleLoader == null)
        {
            needLoadScene = true;
			mBattleLoader = new BattleLoader(sceneId, localBattle, userId);
            mBattleLoaders.Add(sceneId, mBattleLoader);
        }
        mBattleLoader.StartLoadScene();
        return needLoadScene;
    }

    private bool IsUseCache()
    {
        if (!MemoryEnouth())
            return false;

        //目前使用缓存的条件为：当正在加载的场景类型为副本、单挑王、5v5时，就缓存（不销毁）上一个场景
        //int sceneType = DataMgr.Instance.UserData.SceneType;
        //if (sceneType == (int)PublicConst.SceneType.Dungeon || sceneType == (int)PublicConst.SceneType.MultiVS || sceneType == (int)PublicConst.SceneType.SOLO)
        //{
        //    return true;
        //}
        return UseSceneCache;
    }

    /// <summary>
    /// 预加载（协程）一个场景，加载完成后为不可见状态
    /// </summary>
    /// <param name="sceneId"></param>
    /// <param name="userId"></param>
    public void PreLoadScene(int sceneId)
    {
        if (!MemoryEnouth())
            return;

        if (!mBattleLoaders.ContainsKey(sceneId))
        {
            if (!IsUseCache())
            {
                //出于内存考虑，始终只预加载一个场景
                DestroyCache(); //把老的缓存场景销毁
                if (mDestroyBLQueue.Count > 0)  //还有缓存正在销毁中的，等待销毁完成后再加载新的
                {
                    mWaitToPreLoadSceneId = sceneId;
                    return;
                }
            }

			BattleLoader bl = new BattleLoader(sceneId, localBattle);
            bl.PreLoadScene();
            mBattleLoaders.Add(sceneId, bl);
        }
    }

    /// <summary>
    /// 当前正在使用的场景的加载进度
    /// </summary>
    /// <returns></returns>
    public int GetCurLoadProcess()
    {
        int process = 0;
        if (mBattleLoader != null)
            process = mBattleLoader.IsActive ? 100 : mBattleLoader.GetLoadProcess();
        return process;
    }

    /// <summary>
    /// 指定id的缓存预场景的加载进度
    /// </summary>
    /// <param name="sceneId"></param>
    /// <returns></returns>
    public int GetPreLoadProcess(int sceneId)
    {
        int process = 0;
        BattleLoader bl = null;
        if (mBattleLoaders.TryGetValue(sceneId, out bl))
        {
            if (bl != null)
                process = bl.IsActive ? 100 : bl.GetLoadProcess();
        }
        return process;
    }

    /// <summary>
    /// 当前正在使用的场景是否加载完成
    /// </summary>
    /// <returns></returns>
    public bool IsLoadFinish()
    {
        if(mBattleLoader != null)
            return mBattleLoader.IsLoadFinish();
        return false;
    }

    public UnitInfo GetUnitInfo()
    {
        if (mBattleLoader != null)
            return mBattleLoader.UnitInfoData;
        return null;
    }

    /// <summary>
    /// 当前场景的SceneData
    /// </summary>
    /// <returns></returns>
    public SceneData GetCurrentSceneData()
    {
        if(mBattleLoader != null)
            return mBattleLoader.mSceneData;
        return null;
    }

    public void SetFxEnable(bool enable)
    {
        //if (mBattleLoader != null)
        //{
        //    mBattleLoader.SetFxEnable(enable);
        //}
    }

    //public void ChangeFog(FogSetting fs)
    //{
    //    if (mBattleLoader != null)
    //    {
    //        mBattleLoader.InitFogSetting(fs);
    //    }
    //}

    private bool MemoryEnouth()
    {
#if UNITY_ANDROID
        if (SystemInfo.systemMemorySize < 1500)
        {
            return false;
        }
#endif
        return true;
    }

    /// <summary>
    /// 销毁所有未使用的缓存场景
    /// </summary>
    public void DestroyCache()
    {
        mWaitToPreLoadSceneId = 0;
        int curSceneId = mBattleLoader != null ? mBattleLoader.SceneId : 0;
        DestroyCacheExcept(curSceneId);
    }

    /// <summary>
    /// 延迟销毁所有缓存场景
    /// </summary>
    /// <param name="sec"></param>
    public void DelayDestroyCache(float sec)
    {
        GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(sec, ()=>
        {
            DestroyCache();
        }));
    }

    /// <summary>
    /// 销毁指定id以外的所有缓存场景
    /// </summary>
    /// <param name="sceneId"></param>
    private void DestroyCacheExcept(int sceneId)
    {
        BattleLoader tmp = null;
        foreach (KeyValuePair<int, BattleLoader> p in mBattleLoaders)
        {
            BattleLoader bl = p.Value;
            if (p.Key == sceneId)
                tmp = bl;
            else if(!mDestroyBLQueue.ContainsKey(p.Key))
            {
                bl.DestroyFinish += OnCacheBLDestroy;
                mDestroyBLQueue.Add(p.Key, bl);
                bl.Destroy();
            }
        }
        mBattleLoaders.Clear();
        if (tmp != null)
        {
            mBattleLoaders.Add(sceneId, tmp);
        }
    }

    private void OnCacheBLDestroy(BattleLoader bl)
    {
        mDestroyBLQueue.Remove(bl.SceneId);
        if (mDestroyBLQueue.Count == 0 && mWaitToPreLoadSceneId != 0)
        {
            PreLoadScene(mWaitToPreLoadSceneId);
        }
    }

    /// <summary>
    /// 销毁所有场景
    /// </summary>
    private void Destroy()
    {
        mWaitToPreLoadSceneId = 0;
        foreach (KeyValuePair<int, BattleLoader> bl in mBattleLoaders)
        {
            bl.Value.Destroy();
        }
        mBattleLoaders.Clear();
        mBattleLoader = null;
    }

    /// <summary>
    /// 切图时调用，重新登录时销毁所有场景，否则仅把当前场景设为未使用状态
    /// </summary>
    /// <param name="reLogin"></param>
    public void Clear(bool reLogin)
    {
        if(mBattleLoader != null)
            mBattleLoader.Clear();

        if (reLogin)
        {
            Destroy();
        }
    }

}
