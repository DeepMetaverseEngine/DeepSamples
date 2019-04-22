

using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using System.Collections;
using DeepCore.Unity3D;
using UnityEngine;

public class BattleLoader
{

    public delegate void DestroyFinishCallback(BattleLoader bl);
    public event DestroyFinishCallback DestroyFinish;

    public SceneData mSceneData { get; private set; }

    public UnitInfo UnitInfoData { get; private set; }

    public int SceneId { get; set; }

    private int mUserId;

    private GameObject mMapObj;
    private GameObject mMapCamera;

    private DefaultTerrainAdapter mTerrainAdapter;


    private bool mLoadFinish = false;
    public bool IsActive { get; private set; }

    private int mProcess = -1;
    private int mCurFrameRate;

    private bool mPreloading = false;
    private bool mWaitToDestroy = false;
	private bool mLocalBattle = false;

    private enum LoadProcess
    {
        Start = 0,

        TemplateStart = 1,

        MapStart = 2,
        MapEnd = 3,

        StaticObjStart = 4,

        Complete = 5,
    }

	public BattleLoader(int sceneId, bool local, int userId = 1)
    {
        this.SceneId = sceneId;
        this.mUserId = userId;
		mLocalBattle = local;
        //WeakManager.Instance["BattleLoader" + GetHashCode().ToString()] = this;
    }

    ~BattleLoader()
    {
        //Debugger.Log("---------- ~BattleLoader ---------");
    }

    public int InitBattle()
    {
        switch (mProcess)
        {
            case (int)LoadProcess.Start:
                break;
            case (int)LoadProcess.TemplateStart:
                if (!LoadTemplate())
                    return mProcess;
                break;
            case (int)LoadProcess.MapStart:
                LoadMap();
                break;
            case (int)LoadProcess.MapEnd:
                //所有end卡住进度
                return mProcess;
        }

        mProcess++;
        if (mProcess >= (int)LoadProcess.Complete)
        {
            mProcess = (int)LoadProcess.Complete;
            LoadFinish();
        }
        return mProcess;
    }

    public int GetLoadProcess()
    {
        return mProcess == -1 ? 0 : mProcess;
    }

    public void PreLoadScene()
    {
        if (mProcess == -1)
        {
            GameGlobal.Instance.StartCoroutine(StartLoading(true));
        }
    }

    public void StartLoadScene()
    {
        mPreloading = false;
        if (IsLoadFinish() && !IsActive)
        {
            Reset();
        }
        else
        {
            if (mProcess == -1)
            {
                GameGlobal.Instance.StartCoroutine(StartLoading(false));
            }
        }
    }

    IEnumerator StartLoading(bool preload)
    {
        mPreloading = preload;
        mProcess = 0;
        while (mProcess < (int)LoadProcess.Complete)
        {
            InitBattle();
            //Debugger.Log("---------- " + mProcess);
            yield return 1;
        }
        if (mPreloading)
        {
            mPreloading = false;
            //GameAlertManager.Instance.ShowNotify(SceneId + " 预加载完成！");
        }
    }

    public bool IsLoadFinish()
    {
        return mLoadFinish;
    }

    private void LoadFinish()
    {
        mLoadFinish = true;
        if (mWaitToDestroy)
        {
            Destroy();
            mWaitToDestroy = false;
        }
        else
        {
            Reset();
        }
    }

    public virtual bool LoadTemplate()
    {
        EditorTemplates templates = TLClient.TLClientBattleManager.DataRoot;
        if (templates != null)
        {
			this.mSceneData = templates.LoadScene(SceneId, true, !mLocalBattle, true);

            UnitInfoData = templates.Templates.GetUnit(mUserId);
        }
        return templates != null;
    }

    void SetMeshColliderRecursively(GameObject obj, int newLayer, Shader shader)
    {
        if (null == obj)
        {
            return;
        }
        obj.layer = newLayer;

        MeshRenderer render = obj.GetComponent<MeshRenderer>();
        if (render)
        {
            for (int i = 0 ; i < render.materials.Length ; ++i)
            {
                if (render.materials[i].shader.name == "Mobile/Diffuse")
                    render.materials[i].shader = shader;
            }
            if (obj.GetComponent<BoxCollider>() == null)
                obj.AddComponent<BoxCollider>();
            //obj.AddMissingComponent<BoxCollider>();
        }

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetMeshColliderRecursively(child.gameObject, newLayer, shader);
        }
    }

    private void LoadMap()
    {
        mTerrainAdapter = new DefaultTerrainAdapter(true);
        string path = mSceneData.FileName;
        if (DataMgr.Instance.UserData.ChangeMarryScene)
            path = mSceneData.FileName.Replace("tiangong_map01", "tiangong_map02");
        mTerrainAdapter.Load(path, LoadMapCB);
        //DeepCore.Unity3D.Battle.BattleFactory.Instance.TerrainAdapter.Load(mSceneData.FileName, LoadMapCB);
    }

    private void LoadMapCB(bool isLoadOK, GameObject o)
    {
        if (isLoadOK)
        {
            mMapObj = o;
            TerrainCollider c = mMapObj.GetComponentInChildren<TerrainCollider>();
            if (c != null)
                c.gameObject.layer = LayerMask.NameToLayer("NavLayer");

            GameObject MapObject = GameObject.Find("MapObject");
            if (MapObject == null)
            {
                MapObject = new GameObject("MapObject");
                MapObject.transform.position = Vector3.zero;
                MapObject.transform.localRotation = Quaternion.identity;
            }
            mMapObj.transform.parent = MapObject.transform;
            if (mSceneData.ZoneData.ResourceRotate != 0)
            {
                int width = mSceneData.ZoneData.XCount;// TotalWidth;
                int height = mSceneData.ZoneData.YCount;// TotalHeight;
                mMapObj.transform.position = new Vector3(width * 0.5f, 0, height * 0.5f);   //为了居中旋转，临时把坐标设居中
                mMapObj.transform.localRotation = Quaternion.Euler(new Vector3(0, mSceneData.ZoneData.ResourceRotate, 0));
            }
            mMapObj.transform.position = new Vector3(mSceneData.ZoneData.ResourceOffsetX, 0, mSceneData.ZoneData.ResourceOffsetY);
            mMapObj.SetActive(false);
        }
        else
        {
            //LoadSceneOther(null);
            Debugger.Log("load unit Error");
        }

        mProcess = (int)LoadProcess.MapEnd + 1;
    }

    public void Reset()
    {
        if (mMapObj != null && !IsActive)
        {
            if (!mPreloading)
            {
                IsActive = true;
                mMapObj.SetActive(true);
                mTerrainAdapter.InitLightMap();
                mTerrainAdapter.InitFogParam();
            }
        }
    }

    public void Clear()
    {
        if (mMapObj != null)
        {
            mMapObj.SetActive(false);
        }
        RenderSettings.fog = false;
        IsActive = false;
    }

    public void Destroy()
    {
        if (!mLoadFinish)
        {
            mWaitToDestroy = true;
            return;
        }
        var skys = GameSceneMgr.Instance.SceneCamera.gameObject.GetComponents<Skybox>();
        foreach (var item in skys)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(item);
        }
        if (mMapObj != null)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(mMapObj);
            if (mTerrainAdapter != null)
            {
                mTerrainAdapter.Clear();
                mTerrainAdapter = null;
            }

            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            //HZUnityAssetBundleManager.GetInstance().UnloadUnusedAssets();
        }
        if (mMapCamera != null)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(mMapCamera);
        }
        if (DestroyFinish != null)
        {
            DestroyFinish(this);
            DestroyFinish = null;
        }
    }

}
