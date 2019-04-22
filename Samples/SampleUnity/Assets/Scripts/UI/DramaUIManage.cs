using DeepCore.Unity3D.UGUI;
using UnityEngine;

public class DramaUIManage : MonoBehaviour {

    //public GameObject SideTool;
    //public GameObject Caption;

    public HighlightMask highlightMask;

    public static DramaUIManage Instance { get; private set; }

    private static GameObject prefab;
    private static DisplayNode ParentNode;
    void Start () 
    {
        Instance = this;
        var rectTrans = GetComponent<RectTransform>();
        rectTrans.anchorMin = Vector2.zero;
        rectTrans.anchorMax = Vector2.one;
        rectTrans.anchoredPosition = Vector2.zero;
        rectTrans.localScale = Vector3.one;
        rectTrans.pivot = new Vector2(0.5f, 0.5f);
        rectTrans.offsetMin = Vector2.zero;
        rectTrans.offsetMax = Vector2.zero;
        GameGlobal.Instance.FGCtrl.AddFingerHandler(highlightMask, (int)PublicConst.FingerLayer.DramaUILayer);
    }

    void OnDestroy()
    {
        Instance = null;
    }

    public static void Init(DisplayNode parent)
    {
        if(prefab == null)
        {
            prefab = Resources.Load<GameObject>("Prefabs/DramaUI");
        }
        var obj = GameObject.Instantiate(prefab);
        obj.transform.SetParent(parent.Transform);
        ParentNode = parent;
        mMenuRoot = new MenuMgr.MenuRoot("drameUI", 1300);
        ParentNode.AddChild(mMenuRoot);
    }

    public void ShowSideTool(System.Action clickAct)
    {
        //SideTool.SetActive(true);
        //SideTool.GetComponent<BlackSideToolU>().ClickAction = clickAct;
    }

    public void CloseSideTool()
    {
        //SideTool.SetActive(false);
    }


    private void ShowCaptionUI()
    {
        //Caption.SetActive(true);
    }

    public void CloseCaption()
    {
        //Caption.GetComponent<DramaCaptionU>().SetCaptionEnd();
        //Caption.SetActive(false);

    }

    //public void AddCaption(DramaCaptionU.CaptionInfo info)
    //{
    //    ShowCaptionUI();
    //    Caption.GetComponent<DramaCaptionU>().Add(info);
    //}

    //public void RemoveCaption(DramaCaptionU.CaptionInfo info)
    //{
    //    Caption.GetComponent<DramaCaptionU>().Remove(info);
    //}

    private static MenuMgr.MenuRoot mMenuRoot;
    public void AddMenu(MenuBase menu)
    {
        if (ParentNode != null)
        {
            mMenuRoot.AddSubMenu(menu);
        }
    }

    private bool flag = false;

    public void Test1()
    {
    }

}
