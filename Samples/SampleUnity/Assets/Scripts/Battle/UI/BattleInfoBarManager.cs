using UnityEngine;
using System.Collections;

public class BattleInfoBarManager : MonoBehaviour {

    public Transform infoBarRoot;
    public GameObject prefab;
    public static Transform mShowNode = null;
    public static Transform mCacheNode = null;
    public static Camera uiCamera = null;
    public float infoBarAOIDistance = 60.0f;
    public float infoBarScaleMin = 0.02f;
    public float infoBarScaleMax = 0.06f;
    public static float AOIDistance;
    public static float ScaleMin;
    public static float ScaleMax;

    private static Transform mTrans;
    private static GameObject infobarPrefab;

    public static readonly Vector3 HideCamera = new Vector3(100000, 100000, 100000);

    private static BattleInfoBar mActorInfoBar = null;
    public static BattleInfoBar ActorInfoBar
    {
        get { return mActorInfoBar; }
        set
        {
            mActorInfoBar = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        mTrans = transform;

        if (infobarPrefab == null)
            infobarPrefab = prefab;

        //创建根节点.
        CreateRootNode();

        //获取ngui相机实例.
        if (!uiCamera)
            uiCamera = GameSceneMgr.Instance.UICamera;

        GameSceneMgr.Instance.SceneCameraNode.AddZoomEventListener(OnZoomEventHandler);

        AOIDistance = infoBarAOIDistance;
        ScaleMin = infoBarScaleMin;
        ScaleMax = infoBarScaleMax;
    }

    private static void CreateRootNode()
    {
        //创建显示根节点.
        if (mShowNode == null)
        {
            mShowNode = new GameObject("InfoBarShowNode").transform;
            mShowNode.parent = mTrans;
            mShowNode.localScale = Vector3.one;
            mShowNode.transform.localPosition = HideCamera;
        }
        //创建缓存根节点.
        if (mCacheNode == null)
        {
            mCacheNode = new GameObject("InfoBarCacheNode").transform;
            mCacheNode.parent = mTrans;
            mCacheNode.localPosition = HideCamera;
            mCacheNode.localScale = Vector3.one;
            mCacheNode.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update ()
    {
#if UNITY_EDITOR
        AOIDistance = infoBarAOIDistance;
        ScaleMin = infoBarScaleMin;
        ScaleMax = infoBarScaleMax;
#endif
    }

    public static BattleInfoBar AddInfoBar(Transform parent, Vector3 offset, bool isUser, bool showHpBar)
    {
        GameObject obj;
        //先尝试从缓存取.
        if (mCacheNode.childCount > 0)
        {
            obj = mCacheNode.GetChild(0).gameObject;
        }
        else
        {
            obj = (GameObject)GameObject.Instantiate(infobarPrefab,
            new Vector3(1000, 1000, 1000), Quaternion.identity);
        }
        BattleInfoBar infoBar = InitInfoBar(obj, parent, offset, isUser, showHpBar);
        //infoBar.SetActive(false);
        return infoBar;

    }

    private static BattleInfoBar InitInfoBar(GameObject obj, Transform parent, Vector3 offset, bool isUser, bool showHpBar)
    {
        //obj.transform.parent = mShowNode;
        obj.transform.SetParent(mShowNode);
        BattleInfoBar infoBar = obj.GetComponent<BattleInfoBar>();

        infoBar.Parent = parent;
        infoBar.Offset = offset;

        obj.transform.localEulerAngles = Vector3.zero;
        float f = uiCamera.orthographicSize;
        obj.transform.localScale = new Vector3(f, f, f);

        obj.transform.position = Vector3.one * 10000;

        //血条
        UIFollowTarget3D t3d = infoBar.GetComponent<UIFollowTarget3D>();
        t3d.target = parent;
        t3d.offset = offset;
        t3d.aoiDistance = -1;
        t3d.gameCamera = GameSceneMgr.Instance.SceneCamera.transform;

        //名字.
        infoBar.LabelText = TextLabel.AddTextLabel(infoBar.showName.transform, Vector3.zero, AOIDistance);
        TextLabel.SetSize(infoBar.LabelText, BattleInfoBar.FontSizeDefault);
        TextLabel.SetAlignment(infoBar.LabelText, TextAnchor.LowerCenter);
        infoBar.GameCamera = GameSceneMgr.Instance.SceneCamera;

        //注册场景缩放监听.
        //GameSceneMgr.Instance.SceneCameraNode.AddZoomEventListener(infoBar.ScaleChange);

        infoBar.mNeedShowHpBar = showHpBar;
        infoBar.HideHp(!showHpBar);

        //读取系统设置.
        infoBar.RefreshByConfig();
        
        if (isUser && ActorInfoBar == null)
        {
            infoBar.ShowHpCtrl = true;
            ActorInfoBar = infoBar;
            ActorInfoBar.GetComponent<RectTransform>().SetAsFirstSibling();
            ActorInfoBar.LabelText.GetComponent<RectTransform>().SetAsFirstSibling();
        }

        return infoBar;
    }

    public static void ChangeShowHpCtrl(bool alwaysShow)
    {
        if (mShowNode != null)
        {
            for (int i = mShowNode.childCount - 1; i >= 0; --i)
            {
                GameObject obj = mShowNode.GetChild(i).gameObject;
                BattleInfoBar infoBar = obj.GetComponent<BattleInfoBar>();
                if (infoBar != ActorInfoBar)
                {
                    infoBar.RefreshByConfig();
                }
            }
        }
    }

    public static void HideAllHPBar(bool hide)
    {
        if (mShowNode != null)
        {
            for (int i = mShowNode.childCount - 1; i >= 0; --i)
            {
                GameObject obj = mShowNode.GetChild(i).gameObject;
                BattleInfoBar infoBar = obj.GetComponent<BattleInfoBar>();
                //infoBar.HideHp(hide);
                infoBar.RefreshByConfig();
            }
        }
    }

    public static void HideAllTitle(bool hide)
    {
        if (mShowNode != null)
        {
            for (int i = mShowNode.childCount - 1; i >= 0; --i)
            {
                GameObject obj = mShowNode.GetChild(i).gameObject;
                BattleInfoBar infoBar = obj.GetComponent<BattleInfoBar>();
                //infoBar.HideTitle(hide);
                infoBar.RefreshByConfig();
            }
        }
    }

    public static void HideAllName(bool hide)
    {
        if (mShowNode != null)
        {
            for (int i = mShowNode.childCount - 1; i >= 0; --i)
            {
                GameObject obj = mShowNode.GetChild(i).gameObject;
                BattleInfoBar infoBar = obj.GetComponent<BattleInfoBar>();
                infoBar.HideName(hide);
                infoBar.RefreshByConfig();
            }
        }
    }

    public static void HideAllGuild(bool hide)
    {
        if (mShowNode != null)
        {
            for (int i = mShowNode.childCount - 1; i >= 0; --i)
            {
                GameObject obj = mShowNode.GetChild(i).gameObject;
                BattleInfoBar infoBar = obj.GetComponent<BattleInfoBar>();
                //infoBar.HideGuild(hide);
                infoBar.RefreshByConfig();
            }
        }
    }

    public static void HideAllNPCSign(bool hide)
    {
        if (mShowNode != null)
        {
            for (int i = mShowNode.childCount - 1; i >= 0; --i)
            {
                GameObject obj = mShowNode.GetChild(i).gameObject;
                BattleInfoBar infoBar = obj.GetComponent<BattleInfoBar>();
                infoBar.HideNpcFlag(hide);
            }
        }
    }
    
    private void OnZoomEventHandler(float zoom)
    {
        float scale = Mathf.Lerp(infoBarScaleMin, infoBarScaleMax, 1 - zoom);
        infoBarRoot.localScale = Vector3.one * scale;
    }

    /// <summary>
    /// 销毁所有正在显示的和缓存中的实例对象
    /// </summary>
    public static void Clear()
    {
        if (mShowNode != null)
        {
            for (int i = mShowNode.childCount - 1; i >= 0; --i)
            {
                GameObject obj = mShowNode.GetChild(i).gameObject;
                BattleInfoBar infoBar = obj.GetComponent<BattleInfoBar>();
                infoBar.Remove();
            }
        }
        if (mCacheNode != null)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(mCacheNode.gameObject, 0.3f);
            mCacheNode = null;
        }

        CreateRootNode();
    }

}
