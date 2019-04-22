using UnityEngine;
using System.Collections.Generic;
using TL.UGUI.Skill;
using DeepCore.GameSlave;
using TLBattle.Common.Plugins;
using UnityEngine.UI;
using System;
using TLProtocol.Data;
using TLClient.Protocol.Modules.Package;
using DeepCore;
using Assets.Scripts;
using DeepCore.Unity3D;
using uTools;

public class SkillBarHud : MonoBehaviour, ButtonTouchListener, IObserver<UserData>
{

    public SkillButton[] skills = null;
    public ItemButton[] items = null;
    public CellButton target = null;
    public int skillCount = 5;
    public Vector2[] sklRockerPos;
    public RockerHud skillRocker;
    public RectTransform cancelSkillBtn;
    public RectTransform[] vaildArea;

    public delegate void OnSkillBtnDownEvent(int index);
    public delegate void OnSkillBtnUpEvent(int index, bool isDragEvent, Vector2 pos);
    public delegate void OnTargetBtnClickEvent(bool isNext);
    public delegate void OnNPCTalkEvent(uint npcId);
    public delegate void OnPickItemEvent();
    public delegate void OnItemBtnClickEvent(int id, ItemBtnController controller);
    public event OnSkillBtnDownEvent OnSkillBtnDown;
    public event OnSkillBtnUpEvent OnSkillBtnUp;
    public event OnTargetBtnClickEvent OnTargetBtnClick;
    public event OnNPCTalkEvent OnNPCTalk;
    public event OnPickItemEvent OnPickItem;
    public event OnItemBtnClickEvent OnItemBtnClick;

    public Rect[] SkillBarRect { get; private set; }

    public OkBtnType CurOkType { get; private set; }
    private string mAttackIcon;

    public int CurGroup { get; private set; }

    private bool mWillShowOpenEffect;
    private int mHideCount;
    private float mDownTime;
    private bool isBufSkillDown = false;
    private float bufSkillCD;
    private float bufSkillID;
    private Image BufSkillImgObj = null;
    private GameObject buffskillobj = null;
    private GameObject effobj = null;
    private Vector2 nowfignerpos = Vector2.zero;

    private uTweener[] mUtweens;
    private bool mShowAnime;

    public enum OkBtnType
    {
        Attack = 1,
        Talk = 2,
        Pick = 3,
    }

    void Start()
    {
        Init();
    }

    void OnEnable()
    {
        if (mUtweens == null)
        {
            mUtweens = this.GetComponents<uTweener>();
        }
        else if (mShowAnime)
        {
            mShowAnime = false;
            for (int i = 0; i < mUtweens.Length; ++i)
            {
                if (mUtweens[i] is uTweenAlpha)
                    (mUtweens[i] as uTweenAlpha).ResetRenders();
                mUtweens[i].Replay();
            }
        }
    }

    //初始化技能按键.
    private void Init()
    {
        CalculateVaildArea();
        if (items != null)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                items[i].SetTouchListener(this);
            }
        }
        if (target != null)
        {
            target.SetTouchListener(this);
        }

        CurOkType = OkBtnType.Attack;
        CurGroup = 0;

        GameGlobal.Instance.FGCtrl.AddFingerHandler(skillRocker, (int)PublicConst.FingerLayer.SKillBarEnd);
        EventManager.Subscribe("Event.Hud.ShowFunctionMenu", OnFunctionMenuShow);
    }

    //每次进场景前会调用
    public void OnEnterScene()
    {
        DataMgr.Instance.UserData.SendMedicinePoolTips = false;
        TLBattleScene.Instance.CheckAllowUseMedicinePool();
    }

    private void CalculateVaildArea()
    {
        //支持多个矩形区域 
        SkillBarRect = new Rect[vaildArea.Length];
        float scale = UGUIMgr.Scale;
        for (int i = 0; i < vaildArea.Length; ++i)
        {
            //vaildArea的anchor是右下 
            float x = -vaildArea[i].anchoredPosition.x * scale;
            float y = vaildArea[i].anchoredPosition.y * scale;
            float w = vaildArea[i].rect.width * scale;
            float h = vaildArea[i].rect.height * scale;
            SkillBarRect[i] = Rect.MinMaxRect(Screen.width - x - w, y, Screen.width - x, y + h);
        }
    }

    public void InitSkill()
    {
        DataMgr.Instance.UserData.AttachObserver(this);
        ResetSkill(true);
    }

    private List<SkillData> mCurrentSkills;
    public void ResetSkill(bool force = false)
    {
        if (DataMgr.Instance.UserData.GetAttribute(UserData.NotiFyStatus.SKILLDATA) != null)
        {
            List<SkillData> skills = (List<SkillData>)DataMgr.Instance.UserData.GetAttribute(UserData.NotiFyStatus.SKILLDATA);
            if (mCurrentSkills != skills || force)
            {
                mCurrentSkills = skills;
                for (int i = 0; i < skills.Count; ++i)
                {
                    SkillData skill = skills[i];
                    InitSkillOne(skill);
                }
            }
        }
        else
        {
            InitSkillByBattleServer();
        }
    }

    /// <summary>
    /// 从战斗服初始化技能信息，一般用于单机模式或其他特殊用途
    /// </summary>
    public void InitSkillByBattleServer()
    {
        List<ZoneUnit.SkillState> ssList = new List<ZoneUnit.SkillState>();
        TLAIActor actor = DataMgr.Instance.UserData.GetActor();
        actor.ZActor.GetSkillStatus(ssList);
        List<SkillData> skills = new List<SkillData>();
        Dictionary<int, ZoneUnit.SkillState> skMap = new Dictionary<int, ZoneUnit.SkillState>();
        for (var index = ssList.Count - 1; index >= 0; index--)
        {
            var ss = ssList[index];
            var prop = ss.Data.Properties as TLSkillProperties;
            if (!skMap.ContainsKey(index))
            {
                skMap.Add(index, ss);
            }
        }

        for (int i = 0; i < skillCount; i++)
        {
            var data = new SkillKeyStruct();
            data.keyPos = i;
            ZoneUnit.SkillState ss = null;
            if (skMap.Count == ssList.Count)
            {
                if (skMap.TryGetValue(i, out ss))
                {
                    ss = ssList[i];
                }
            }
            else if (i < ssList.Count)
            {
                ss = ssList[i];
            }

            if (ss != null && i >= 0)
            {
                data.advancedSkillId = ss.Data.ID;
                data.icon = ss.Data.IconName;
                data.flag = (int)SkillButton.IconType.Icon;
            }
            else
            {
                data.advancedSkillId = 0;
                data.flag = (int)SkillButton.IconType.Icon;
                data.baseSkillId = -1;
            }
            SkillData sd = new SkillData(data);
            InitSkillOne(sd);
            skills.Add(sd);
        }
        mCurrentSkills = skills;
    }

    public void InitSkillOne(SkillData sd)
    {
        int index = sd.Data.keyPos;
        int startIndex = CurGroup * skillCount;
        int endIndex = startIndex + skillCount;
        if (index == 0 || index > startIndex && index <= endIndex)
        {
            index = index == 0 ? 0 : (index - 1) % skillCount + 1;
            SkillButton skill = skills[index];
            skill.Init(sd);
            skill.SetTouchListener(this);
            if (sd.Data.keyPos == 0)
            {
                mAttackIcon = sd.Data.icon;
            }
            if (sd.IsUnlock)
            {
                sd.IsUnlock = false;
                //技能解锁特效
            }
            if (sd.Data.buffId != -1)
            {
                bufSkillID = sd.Data.buffId;
                BufSkillImgObj = skill.buf;
                buffskillobj = skill.gameObject.transform.parent.gameObject;
                if (effobj == null)
                {
                    string assetpath = "/res/effect/ui/ef_ui_partner_activation.assetbundles";
                    PlayUI3DEffect(assetpath);
                }
            }
        }
    }

    public void SwitchSkillBar()
    {
        List<SkillData> shortcut = mCurrentSkills;
        if (shortcut == null)
            return;

        Vector2 endPos = skills[0].DefaultPos + new Vector2(-20, 20);
        CurGroup = 1 - CurGroup;
        for (int i = 1; i <= skillCount; i++)
        {
            SkillButton skill = skills[i];
            int index = CurGroup * skillCount + i;
            if (index < shortcut.Count)
            {
                skill.PlayShrink(endPos, shortcut[index], () =>
                {
                    if (shortcut[index].IsUnlock)
                    {
                        shortcut[index].IsUnlock = false;
                    }
                });
            }
        }
    }

    public void ChangeOkBtn(OkBtnType type)
    {
        CurOkType = type;
        SkillButton ok = skills[0];
        switch (type)
        {
            case OkBtnType.Attack:
                ok.SetIcon(mAttackIcon);
                break;
            case OkBtnType.Talk:
                ok.SetIcon("main_button_talk", SkillButton.IconType.Interactive);
                break;
            case OkBtnType.Pick:
                ok.SetIcon("img_shiqu", SkillButton.IconType.Interactive);
                break;
        }
    }

    /// <summary>
    /// 更新技能CD时间.
    /// </summary>
    public void UpdateCD(int index, int funllCdTimeMS, float percent)
    {
        if (index >= 0 && index < skills.Length)
        {
            SkillButton skill = skills[index];
            if (skill.gameObject.activeSelf && skill.CDPercent != percent)
            {
                if (skill.miracleEffect != null && skill.miracleEffect.activeInHierarchy)
                {
                    skill.miracleEffect.SetActive(false);
                }
                skill.SetCD(percent * funllCdTimeMS * 0.001f, percent);
            }
        }
    }

    /// <summary>
    /// 更新技能BUF CD时间.
    /// </summary>
    public void UpdateBufCD(int index, float percent)
    {
        if (index >= 0 && index < skills.Length)
        {
            SkillButton skill = skills[index];
            //float _percent = (1 - percent);

            if (skill.BufCDPercent != percent)
            {
                skill.SetBufCD(percent);
            }
        }

        if (effobj != null)
        {
            if (BufSkillImgObj.fillAmount == 1)
            {
                if (!effobj.activeSelf)
                {
                    effobj.SetActive(true);
                }
            }
            else
            {
                if (effobj.activeSelf)
                {
                    effobj.SetActive(false);
                }
            }
        }

        //对长按怒气显示UI的判定
        if (isBufSkillDown)
        {
            if (Time.time - mDownTime > 0.3)
            {
                if (IsOnSkillButton(buffskillobj))
                    ShowNuQiUI();
                isBufSkillDown = false;
            }
        }
    }

    public void ShowSkillRockerByIndex(int index)
    {
        if (index >= 0 && index < skills.Length)
        {
            skillRocker.gameObject.SetActive(true);
            SkillButton sklBtn = skills[index];
            Vector2 center = new Vector2(sklBtn.DefaultPos.x - sklBtn.ShowSize.x * 0.5f, sklBtn.DefaultPos.y + sklBtn.ShowSize.y * 0.5f);

            if (center.x - skillRocker.Radius < -UGUIMgr.Size.x)
                center.x = -UGUIMgr.Size.x + skillRocker.Radius;
            else if (center.x + skillRocker.Radius > 0)
                center.x = -skillRocker.Radius;
            if (center.y - skillRocker.Radius < 0)
                center.y = skillRocker.Radius;
            else if (center.y + skillRocker.Radius > UGUIMgr.Size.y)
                center.y = UGUIMgr.Size.y - skillRocker.Radius;

            skillRocker.ChangeDefaultPos(center);
        }
        else
        {
            skillRocker.gameObject.SetActive(false);
        }
    }

    public void ShowBanByIndex(int index, bool isShow)
    {
        if (mCurrentSkills != null)
        {
            SkillData sd = mCurrentSkills[index];
            ShowBan(sd, isShow);
        }
    }

    public void ShowBanById(int skillID, bool isShow)
    {
        SkillData sd = GetSkillData(skillID);
        ShowBan(sd, isShow);
    }

    private void ShowBan(SkillData sd, bool isShow)
    {
        if (sd != null && sd.IsShowBan != isShow)
        {
            sd.IsShowBan = isShow;
            int keyPos = sd.Data.keyPos;
            int startIndex = CurGroup * skillCount;
            int endIndex = startIndex + skillCount;
            if (keyPos == 0 || keyPos > startIndex && keyPos <= endIndex)
            {
                int index = keyPos == 0 ? 0 : (keyPos - 1) % skillCount + 1;
                SkillButton skill = skills[index];
                skill.ShowBan(isShow);
            }
        }
    }

    public void ShowBanExceptSkillID(int skillID, bool isShow)
    {
        if (skills == null) { return; }

        List<SkillData> temp = mCurrentSkills;
        if (temp != null)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                SkillData skill = temp[i];
                if (skill.Data.advancedSkillId != skillID && skill.Data.keyPos != 0 && skill.Data.flag == 1 && skill.Data.baseSkillId != -1)
                {
                    ShowBanByIndex(i, isShow);
                }
            }
        }
    }

    public bool Visible
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            mHideCount = value ? mHideCount - 1 : mHideCount + 1;
            mHideCount = System.Math.Max(0, mHideCount);
            bool isHide = mHideCount > 0;
            if (gameObject.activeSelf != !isHide)
                gameObject.SetActive(!isHide);
        }
    }

    /// <summary>
    /// 技能按钮点击.
    /// </summary>
    /// <param name="obj"></param>
    public void OnButtonDown(GameObject obj, Vector2 fingerPos)
    {
        //GameObject obj = eventData.selectedObject;
        //Debugger.Log("[pointerId] " + eventData.pointerId);
        if (OnSkillBtnDown != null)
        {
            if (obj.name == "Attack")
            {
                if (CurOkType == OkBtnType.Pick)
                {
                    //OnPickItem();
                }
                else if (CurOkType == OkBtnType.Talk)
                {
                    //OnNPCTalk(0);
                }
                else
                {
                    OnSkillBtnDown(0);
                }
            }
            else if (obj.name == "Skill_1")
            {
                OnSkillBtnDown(1);
            }
            else if (obj.name == "Skill_2")
            {
                OnSkillBtnDown(2);
            }
            else if (obj.name == "Skill_3")
            {
                OnSkillBtnDown(3);
            }
            else if (obj.name == "Skill_4")
            {
                OnSkillBtnDown(4);
            }
            else if (obj.name == "Skill_5")
            {
                OnSkillBtnDown(5);
            }
            else if (obj.name == "Skill_6")
            {
                OnSkillBtnDown(6);
            }
            else if (obj.name == "Skill_7")
            {
                if (BufSkillImgObj != null)
                {
                    bufSkillCD = BufSkillImgObj.fillAmount;
                }
                if (bufSkillCD != 1)
                {
                    isBufSkillDown = true;
                    mDownTime = Time.time;
                }
                OnSkillBtnDown(7);
            }
        }
    }
    public void TargetBtnClick(GameObject obj)
    {
        if (OnTargetBtnClick != null)
        {
            OnTargetBtnClick(true);
        }
    }

    public void OnButtonUp(GameObject obj, Vector2 fingerPos)
    {
        if (obj.name == "Attack")
        {
            bool isDragEvent = false;
            //Vector2 dragEndPos = fingerPos;
            //if (dragEndPos.y - mDragPos.y < -10)   //往下划 next
            //{
            //    SwitchSkillBar();
            //    isDragEvent = true;
            //}
            //else if (dragEndPos.y - mDragPos.y > 10)   //往上划 pre
            //{

            //}

            if (CurOkType == OkBtnType.Pick && !isDragEvent)
            {
                OnPickItem();
            }
            else if (CurOkType == OkBtnType.Talk && !isDragEvent)
            {
                OnNPCTalk(0);
            }
            else if (OnSkillBtnUp != null)
            {
                OnSkillBtnUp(0, isDragEvent, fingerPos);
            }
        }
        else if (OnSkillBtnUp != null)
        {
            if (obj.name == "Skill_1")
            {
                OnSkillBtnUp(1, false, fingerPos);
            }
            else if (obj.name == "Skill_2")
            {
                OnSkillBtnUp(2, false, fingerPos);
            }
            else if (obj.name == "Skill_3")
            {
                OnSkillBtnUp(3, false, fingerPos);
            }
            else if (obj.name == "Skill_4")
            {
                OnSkillBtnUp(4, false, fingerPos);
            }
            else if (obj.name == "Skill_5")
            {
                OnSkillBtnUp(5, false, fingerPos);
            }
            else if (obj.name == "Skill_6")
            {
                OnSkillBtnUp(6, false, fingerPos);
            }
            else if (obj.name == "Skill_7")
            {
                OnSkillBtnUp(7, false, fingerPos);
                if (isBufSkillDown)
                {
                    isBufSkillDown = false;
                }
            }
        }
        if (obj.name == "Item2")
        {
            ItemBtnClick();
        }
        if (obj.name == "Target")
        {
            TargetBtnClick(obj);
        }
        nowfignerpos = Vector2.zero;
    }

    public void OnButtonDrag(GameObject obj, Vector2 pos)
    {
        nowfignerpos = pos;
    }

    public void OnButtonLongPressed(GameObject obj)
    {
        if (obj.name == "Item2")
        {
            MenuMgr.Instance.OpenUIByTag("MedicineXianDan");
        }
    }

    public bool IsInCancelBtnArea(Vector2 pos)
    {
        if (!cancelSkillBtn.gameObject.activeInHierarchy)
        {
            return false;
        }
        Camera c = GameSceneMgr.Instance.UICamera;
        if (c != null)
        {
            Vector3[] v = new Vector3[4];
            cancelSkillBtn.GetWorldCorners(v);//左下，右下，左上，右上
            Vector3 p = c.ScreenToWorldPoint(new Vector3(pos.x, pos.y, v[0].z));
            //Debug.Log(p.ToString() + v[0].ToString() + v[1].ToString() + v[2].ToString() + v[3].ToString());
            if (p.y < v[0].y || (p.y > v[1].y))
            {
                return false;
            }
            if (p.x < v[0].x || (p.x > v[3].x))
            {
                return false;
            }
            return true;
        }

        return false;
    }

    public int GetSkillID(int index)
    {
        if (skills != null && index >= 0 && index < skills.Length)
        {
            return skills[index].Id;
        }

        return -1;
    }

    public int GetSkillIndex(int skillID)
    {
        if (skills == null) { return -1; }

        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].Id == skillID)
            {
                return i;
            }
        }

        return -1;
    }

    public SkillData GetSkillData(int skillID)
    {
        List<SkillData> skills = mCurrentSkills;
        if (skills != null)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                SkillData skill = skills[i];
                if (skill.Data.advancedSkillId == skillID)
                {
                    return skill;
                }
            }
        }

        return null;
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        if (reLogin)
        {
            mHideCount = 0;
        }

        mShowAnime = false;
        ChangeOkBtn(SkillBarHud.OkBtnType.Attack);
        OnSkillBtnDown = null;
        OnTargetBtnClick = null;
        OnNPCTalk = null;

        if (reLogin || reConnect)
        {
            DataMgr.Instance.UserData.OnBagUpdateAction -= BagOnItemUpdateAction;
            DataMgr.Instance.UserData.OnGameOptionDataChange -= OnGameOptionDataChange;
            medicineMap = null;
            btnController = null;
        }

    }


    public bool IsShowCD(int index)
    {
        index = CurGroup * skillCount + index;

        bool ret = false;

        if (skills != null && index < skills.Length)
        {
            ret = skills[index].IsCD();
        }

        return ret;
    }

    public bool Notify(long status, UserData subject)
    {
        if ((status & (long)global::UserData.NotiFyStatus.SKILLDATA) != 0)
        {
            List<SkillData> skills = (List<SkillData>)DataMgr.Instance.UserData.GetAttribute(UserData.NotiFyStatus.SKILLDATA);
            if (mCurrentSkills == skills)
            {
                //正常技能状态才允许notify
                ResetSkill(true);
            }
        }
        return true;
    }

    private void OnFunctionMenuShow(EventManager.ResponseData res)
    {
        var dict = (Dictionary<object, object>)res.data[1];
        object obj;
        if (dict.TryGetValue("isShow", out obj))
        {
            if ((bool)obj)
                mShowAnime = true;
        }
    }

    public void BeforeDetach(UserData subject) { }
    public void BeforeAttach(UserData subject) { }

    void OnDestroy()
    {
        DataMgr.Instance.UserData.DetachObserver(this);
        EventManager.Unsubscribe("Event.Hud.ShowFunctionMenu", OnFunctionMenuShow);
    }

    public class SkillData
    {
        public SkillKeyStruct Data { get; set; }
        public bool IsShowBan { get; set; }
        public bool IsUnlock { get; set; }
        public SkillData(SkillKeyStruct data)
        {
            Reset(data);
        }
        public void Reset(SkillKeyStruct data)
        {
            Data = data;
            IsShowBan = false;
            IsUnlock = false;
        }
    }

    //这里调用怒气UI
    private void ShowNuQiUI()
    {
        if (bufSkillID != -1)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("id", bufSkillID);
            args.Add("cd", (int)(bufSkillCD * 100));
            EventManager.Fire("ShowBufSkillTips", args);
        }
    }

    //显示技能特效
    public void PlayUI3DEffect(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            FuckAssetObject.GetOrLoad(path, System.IO.Path.GetFileNameWithoutExtension(path), (loader) =>
            {
                if (loader)
                {
                    effobj = loader.gameObject;
                    Transform trans = loader.transform;
                    trans.SetParent(BufSkillImgObj.transform);
                    trans.localPosition = new Vector3(0, 0, -60);
                    trans.localEulerAngles = Vector3.zero;
                    trans.localScale = Vector3.one;
                    loader.gameObject.SetActive(false);
                    UILayerMgr.SetLayerOrder(loader.gameObject, 1000, false, (int)PublicConst.LayerSetting.UI);
                }
            });
        }
    }


    /// <summary>
    /// 判断触摸位置是否还在技能按钮上
    /// </summary>
    /// <param name="skillobj">需要判断的技能</param>
    /// <returns></returns>
    private bool IsOnSkillButton(GameObject skillobj)
    {
        Vector2 a = GameSceneMgr.Instance.UICamera.WorldToScreenPoint(skillobj.transform.position);
        Vector2 _size = skillobj.GetComponent<RectTransform>().rect.size;
        a = new Vector2(a.x - _size.x / 2, a.y + _size.y / 2);
        if (nowfignerpos != Vector2.zero)
        {
            if (Math.Abs(nowfignerpos.x - a.x) > _size.x / 2 || Math.Abs(nowfignerpos.y - a.y) > _size.y / 2)
                return false;
        }
        return true;
    }

    #region 自动吃药按钮刷新逻辑.

    private bool fireEvent = false;
    private ItemBtnController btnController = null;
    private HashMap<int, int> medicineMap = null;
    private void ItemBtnClick()
    {
        if (btnController != null)
        {
            int count = GetItemBtnCount();
            if (count <= 0)
            {
                //打开界面.
                MenuMgr.Instance.OpenUIByTag("MedicineXianDan");
                //打开吃药列表界面.
            }
            else if (count > 0 && btnController.InCD() == false)
            {
                int p = TLBattleScene.Instance.ActorHPPercent();

                if (p == 100)
                {
                    var str = HZLanguageManager.Instance.GetString("common_hpmax");
                    GameAlertManager.Instance.ShowNotify(str);
                    return;
                }

                if (OnItemBtnClick != null)
                    OnItemBtnClick.Invoke(DataMgr.Instance.UserData.GameOptionsData.itemID, btnController);
            }
        }
    }

    public void InitMedicineSlot()
    {
        if (!GameGlobal.Instance.netMode || medicineMap != null)
            return;

        //初始化药品MAP.
        medicineMap = new HashMap<int, int>();
        var ret = GameUtil.GetDBDataFull("MedicineItemData");
        object itemID = 0;
        for (int i = 0; i < ret.Count; i++)
        {
            if (ret[i].TryGetValue("item_id", out itemID))
            {
                medicineMap.Add(Convert.ToInt32(itemID), 1);
            }
        }

        DataMgr.Instance.UserData.OnBagUpdateAction += BagOnItemUpdateAction;
        DataMgr.Instance.UserData.OnGameOptionDataChange += OnGameOptionDataChange;
        var options = DataMgr.Instance.UserData.GameOptionsData;
        var data = CreateItemData(options);
        btnController = new ItemBtnController(data);
        if (options.itemID != 0)
        {
            SetBtnInfo(data.itemID, (uint)DataMgr.Instance.UserData.Bag.Count(data.itemID));
        }
        else
        {
            SetBtnInfo(0, 0);
        }
    }

    private void OnGameOptionDataChange(TLGameOptionsData data)
    {
        if (btnController == null) return;

        if (btnController.mData.itemID != data.itemID)
        {
            btnController.mData.itemID = data.itemID;
            uint count = (uint)DataMgr.Instance.UserData.Bag.Count(data.itemID);
            //if (count == 0 && DataMgr.Instance.UserData.GameOptionsData.SmartSelect)
            //    EventManager.Fire("DoSmartSelectItem");
            SetBtnInfo(data.itemID, count);
        }
        else
        {
            //if (btnController.mData.itemID != 0)
            //{
            //    uint count = (uint)DataMgr.Instance.UserData.Bag.Count(data.itemID);
            //    if (count == 0 && DataMgr.Instance.UserData.GameOptionsData.SmartSelect)
            //        EventManager.Fire("DoSmartSelectItem");
            //}
        }

        fireEvent = false;
    }
    private ItemData CreateItemData(TLGameOptionsData data)
    {
        var ret = new ItemData();
        ret.itemCD = data.ItemCoolDownTimeStamp;
        ret.itemID = data.itemID;
        var db = GameUtil.GetDBData("GameConfig", "medicine_cd");
        ret.cdFullTimeMS = Convert.ToInt32((string)db["paramvalue"]);
        return ret;
    }
    private string GetItemIcon(int itemID)
    {
        string atlas_id = null;
        var obj = GameUtil.GetDBData2("MedicineItemData", "{item_id =" + itemID + "}")[0]["icon"];
        if (obj != null)
        {
            atlas_id = Convert.ToString(obj);
        }

        return atlas_id;
    }
    private void BagOnItemUpdateAction(ItemUpdateAction action)
    {
        switch (action.Type)
        {
            case ItemUpdateAction.ActionType.Add:
            case ItemUpdateAction.ActionType.Remove:
            case ItemUpdateAction.ActionType.UpdateCount:
                if (action.TemplateSnap != null)
                {
                    var options = DataMgr.Instance.UserData.GameOptionsData;
                    if (options.itemID == action.TemplateSnap.TemplateID)//吃药的道具.
                    {
                        var itemCount = (uint)DataMgr.Instance.UserData.Bag.Count(options.itemID);
                        SetItemBtnCount(options.itemID, itemCount);
                        //if (itemCount == 0)
                        //{
                        //    if (DataMgr.Instance.UserData.GameOptionsData.SmartSelect)
                        //        EventManager.Fire("DoSmartSelectItem");
                        //}
                    }
                    else//智能选择物品，当物品更新时，自动更新到按钮.
                    {
                        //if (DataMgr.Instance.UserData.GameOptionsData.SmartSelect &&
                        //                                   GetItemBtnCount() == 0 &&
                        //                                   IsMedicineItem(action.TemplateSnap.TemplateID))
                        //{
                        //    EventManager.Fire("DoSmartSelectItem");
                        //}
                    }
                }
                break;
        }
    }
    private void SetBtnInfo(int itemID, uint count)
    {
        if (items != null && items.Length > 0)
        {
            var btn = items[0];
            if (btn != null)
            {

                if (count != 0)
                    btn.SetIcon(GetItemIcon(itemID));
                else
                    btn.SetIcon(null, ItemButton.IconType.UnEquip);

                btn.SetCount((int)count);
            }
        }
    }
    private void SetItemBtnCount(int itemID, uint count)
    {
        if (items != null && items.Length > 0)
        {
            var btn = items[0];
            if (btn != null)
            {
                //TODO.
                if (count == 0)
                {
                    btn.SetCD(0, 0);
                    btn.SetIcon(null, ItemButton.IconType.UnEquip);
                    btn.SetCount(0);
                }
                else
                {
                    if (btn.GetCount() == 0)//替换图片状态.
                    {
                        btn.SetIcon(GetItemIcon(itemID));
                    }

                    btn.SetCount((int)count);
                }
            }
        }
    }
    private int GetItemBtnCount()
    {
        if (items != null && items.Length > 0)
        {
            var btn = items[0];
            if (btn != null)
            {
                return btn.GetCount();
            }
        }
        return 0;
    }
    private bool IsMedicineItem(int itemID)
    {
        return medicineMap.ContainsKey(itemID);
    }
    public void UpdateItemCD(int timeInvervalMS)
    {
        if (btnController != null)
        {
            //吃药CD显示.
            //if (time > 0 || btnController.LastExpireTime != 0)
            {
                var time = btnController.ExpireTimeMS();
                var btn = items[0];
                bool f = (time > 0 || btn.IsCD());
                if (btn != null && f && btn.GetCount() != 0)
                {
                    float p = 1 - btnController.Percent();
                    btn.SetCD(btnController.ExpireTimeMS() / 1000.0f, p);
                }
            }
            btnController.Update(timeInvervalMS);
        }

        if (GameGlobal.Instance.netMode)
        {
            UpdateMedicinePool();
        }
    }

    #region 血池. 

    private void UpdateMedicinePool()
    {
        //血池逻辑.
        if (DataMgr.Instance.UserData.GameOptionsData.AutoUseItem && TLBattleScene.Instance.AllowUseMedicinePool())
        {
            //判断条件修改改为调用血池
            int itemCount = DataMgr.Instance.UserData.MedicinePoolCurCount;
            if (itemCount == 0 && !DataMgr.Instance.UserData.SendMedicinePoolTips)
            {
                DataMgr.Instance.UserData.SendMedicinePoolTips = true;
                var str = HZLanguageManager.Instance.GetString("medicine_pool_empty");
                GameAlertManager.Instance.ShowNotify(str, 0, new Vector2(-1, -1), 5);
            }
            else
            {
                int p = TLBattleScene.Instance.ActorHPPercent();
                byte v = DataMgr.Instance.UserData.GameOptionsData.UseThreshold;
                if (p > 0 && p <= v)//血量百分比低于阀值.
                {
                    //判断数量 自动吃药.
                    var dt = DataMgr.Instance.UserData.GameOptionsData.MedicinePoolTimeStamp;
                    if (DateTime.Compare(dt, GameSceneMgr.Instance.syncServerTime.GetServerTimeUTC()) < 0)
                    {
                        if (itemCount > 0)//吃药.
                        {
                            DataMgr.Instance.UserData.SendMedicinePoolTips = false;
                            TLBattleScene.Instance.UseMedicinePool();
                        }
                    }
                }
            }
        }
    }

    #endregion

    public class ItemBtnController
    {
        private DeepCore.TimeExpire<int> mTimeExpire;

        public ItemData mData;

        private int lastExpireTime;
        private new DeepCore.TimeExpire<int> sendMsgExpireTime;

        public int LastExpireTime
        {
            get
            {
                return lastExpireTime;
            }

            set
            {
                lastExpireTime = value;
            }
        }

        public ItemBtnController(ItemData data)
        {
            Init(data);
        }

        private void Init(ItemData data)
        {
            mData = data;
            var ts = data.itemCD - GameSceneMgr.Instance.syncServerTime.GetServerTimeUTC();
            int s = ts.Milliseconds;
            if (s > 0)
            {
                mTimeExpire = new DeepCore.TimeExpire<int>(s);
            }
            else
            {
                mTimeExpire = new DeepCore.TimeExpire<int>(0);
            }

            //发送内置CD.
            sendMsgExpireTime = new DeepCore.TimeExpire<int>(1500);
        }

        public int ExpireTimeMS()
        {
            return mTimeExpire.ExpireTimeMS;
        }

        public bool InCD()
        {
            return mTimeExpire.ExpireTimeMS > 0;
        }

        public float Percent()
        {
            return mTimeExpire.Percent;
        }

        public void Reset(int timeMS)
        {
            mTimeExpire.Reset(timeMS);
        }

        public void Update(int timeInervalMS)
        {
            mTimeExpire.Update(timeInervalMS);

            LastExpireTime = mTimeExpire.ExpireTimeMS;
            sendMsgExpireTime.Update(timeInervalMS);
        }

        public bool InSendCD()
        {
            return sendMsgExpireTime.ExpireTimeMS > 0;
        }

        public void EndSendCD()
        {
            sendMsgExpireTime.End();
        }

        public void ResetSendCD()
        {
            sendMsgExpireTime.Reset();
        }
    }

    public class ItemData
    {
        public DateTime itemCD;
        public int cdFullTimeMS;
        public int itemID;
    }

    #endregion
}

//临时代替游戏服协议里的数据结构
public class SkillKeyStruct
{
    public int advancedSkillId { get; set; }
    public int baseSkillId { get; set; }
    public int buffId { get; set; }
    public int flag { get; set; }
    public string icon { get; set; }
    public int keyPos { get; set; }
    public string name { get; set; }
    public int unlockLevel { get; set; }
    public GameSkill.TLSkillType skillType;
}