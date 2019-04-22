using DeepCore.GameData.Zone;
using UnityEngine;


public class SceneTouchHandler : FingerHandlerInterface, PinchHandlerInterface
{

    public delegate void OnUnitSelectedHandler(uint id);
    public event OnUnitSelectedHandler OnUnitSelected;
    public delegate void OnMapTouchHandler(Vector3 pos);
    public event OnMapTouchHandler OnMapTouch;

    private bool mFingerMove = false;
    //private bool mCanPinch = false;
    private int mFingerIndex = -1;

    public enum Priority
    {
        NPC = 0,        //NPC
        PlayerE = 1,    //敌方玩家.
        Monster = 2,    //怪物.
        PlayerF = 3,    //友方玩家.
        BuildingF = 4,  //友方建筑物.
        BuildingE = 5,  //敌方建筑物.
        UGUI = 50,      //UGUI.
        Background = 100,  //地面.
    }

    public SceneTouchHandler()
    {
        GameGlobal.Instance.FGCtrl.AddFingerHandler(this, (int)PublicConst.FingerLayer.BattleScene);
        GameGlobal.Instance.FGCtrl.AddPinchHandler(this, (int)PublicConst.FingerLayer.BattleScene);
    }

    public static void AddUnitTouchLayer(TLAIUnit unit)
    {
        //陷阱不需要点击响应.
        if (unit.Info.UType == UnitInfo.UnitType.TYPE_TRIGGER)
            return;

        var list = unit.ObjectRoot.GetComponentsInChildren<BoxCollider>(true);
        foreach (var colliderTrans in list)
        {
            colliderTrans.gameObject.layer = (int)PublicConst.LayerSetting.SelectableUnit;
            TouchUnit touchUnit = colliderTrans.gameObject.GetComponent<TouchUnit>();
            if (touchUnit == null)
                touchUnit = colliderTrans.gameObject.AddComponent<TouchUnit>();
            touchUnit.objId = unit.ObjectID;
            touchUnit.type = (int)unit.Info.UType;
            switch (unit.Info.UType)
            {
                case UnitInfo.UnitType.TYPE_NPC:
                    touchUnit.priority = (int)Priority.NPC;
                    break;
                case UnitInfo.UnitType.TYPE_MONSTER:
                    touchUnit.priority = (int)Priority.Monster;
                    break;
                case UnitInfo.UnitType.TYPE_PLAYER:
                    if (unit.Force == DataMgr.Instance.UserData.Force)
                        touchUnit.priority = (int)Priority.PlayerF;
                    else
                        touchUnit.priority = (int)Priority.PlayerE;
                    break;
                case UnitInfo.UnitType.TYPE_BUILDING:
                    if (unit.Force == DataMgr.Instance.UserData.Force)
                        touchUnit.priority = (int)Priority.BuildingF;
                    else
                        touchUnit.priority = (int)Priority.BuildingE;
                    break;
            }
        }
    }

    public RaycastHit[] CheckHit(Vector2 screenPos, float distance, int layerMask)
    {
        Ray ray = GameSceneMgr.Instance.SceneCamera.ScreenPointToRay(screenPos);
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, layerMask);
        return hits;
    }

    private bool CheckHitSkillBar(Vector2 pos)
    {
        if (!HudManager.Instance.SkillBar.Visible)
            return false;

        RectTransform[] trans = UGUIMgr.TouchRects.skillBar;
        for (int i = 0 ; i < trans.Length ; ++i)
        {
            if (UGUIMgr.CheckInRect(trans[i], pos))
                return true;
        }
        return false;
    }

    private int SelectUnit(RaycastHit[] units, Vector2 pos)
    {
        if (CheckHitSkillBar(pos))
        {
            return (int)Priority.UGUI;
        }

        int priority = 100;
        TouchUnit selUnit = null;
        float navY = -999999;
        Vector3 navPoint = Vector3.zero;
        for (int i = 0 ; i < units.Length ; ++i)
        {
            GameObject obj = units[i].collider.gameObject;
            if (!DeepCore.Unity3D.UnityHelper.IsObjectExist(obj))   //防止sb unity获取到野指针...
            {
                continue;
            }
            if (obj.layer == (int)PublicConst.LayerSetting.UI)  //UGUI
            {
                return (int)Priority.UGUI;
            }
            else if (obj.layer == (int)PublicConst.LayerSetting.SelectableUnit) //单位 
            {
                TouchUnit tu = obj.GetComponent<TouchUnit>();
                if (tu != null && tu.priority < priority)
                {
                    priority = tu.priority;
                    selUnit = tu;
                }
            }
            else if (obj.layer == (int)PublicConst.LayerSetting.STAGE_NAV) //地表 
            {
                if (units[i].point.y > navY)
                {
                    navPoint = units[i].point;
                    navY = navPoint.y;
                }
            }
        }
        if (selUnit != null && OnUnitSelected != null)
        {
            OnUnitSelected(selUnit.objId);
            return selUnit.priority;
        }
        else if (!navPoint.Equals(Vector3.zero))
        {
            if (OnMapTouch != null)
            {
                OnMapTouch(units[0].point);
            }
        }
        return (int)Priority.Background;
    }

    public bool OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        //if (mFingerIndex == -1)
        {
            mFingerMove = false;
        }
        return false;
    }

    public bool OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        mFingerMove = true;
        return false;
    }

    public bool OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        if (!mFingerMove)
        {
            //mFingerIndex = -1;

            //3D碰撞检测.
            int layerMask = (1 << (int)PublicConst.LayerSetting.SelectableUnit | 1 << (int)PublicConst.LayerSetting.STAGE_NAV | 1 << (int)PublicConst.LayerSetting.UI);
            RaycastHit[] hits = CheckHit(fingerPos, 10000, layerMask);
            if (hits != null && hits.Length > 0)
            {
                int ret = SelectUnit(hits, fingerPos);
                return true;
            }
        }
        return false;
    }

    public void OnFingerClear()
    {
        mFingerMove = false;
        mFingerIndex = -1;
    }

    #region FingerGestures_Pinch

    public bool OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        mFingerMove = true;
        return false;
    }

    public bool OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        return false;
    }

    public bool OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        return false;
    }

    #endregion

    public void Destroy()
    {
        GameGlobal.Instance.FGCtrl.RemoveFingerHandler(this);
        GameGlobal.Instance.FGCtrl.RemovePinchHandler(this);
    }

}

class TouchUnit : MonoBehaviour
{
    public uint objId;
    public int type;
    public int priority;
}
