
using System;
using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.GameData.Zone;
using System.Collections.Generic;
using System.Linq;
using DeepCore;
using UnityEngine;
using Cache;

public class PlayerInfoHud : DisplayNode, IObserver<UserData>
{

    public delegate void OnTargetTouchEvent();
    public event OnTargetTouchEvent OnTargetTouch;

    private HZRoot mRoot = null;
    public HZRoot Root { get { return mRoot; } }

    private HeroInfoUI mHeroInfo;
    private TargetInfoUI mTargetInfo;
    private HZCanvas[] mTargetUIRoot;

    public enum BuffReasonType
    {
        Add,
        Change,
        Removed
    }

    public PlayerInfoHud()
    {
        OnInit();
    }

    private void OnInit()
    {
        HZUISystem.SetNodeFullScreenSize(this);
        this.Enable = false;
        this.EnableChildren = true;
        mRoot = (HZRoot)HZUISystem.CreateFromFile("xml/hud/ui_hud_role.gui.xml");
        if (mRoot != null)
        {
            this.AddChild(mRoot);
        }
        HudManager.Instance.InitAnchorWithNode(mRoot, HudManager.HUD_TOP | HudManager.HUD_LEFT);
        HudManager.Instance.AddHudUI(mRoot, "HeroInfoHud");
        InitCompmont();
    }

    //每次进场景前会调用
    public void OnEnterScene()
    {
        if (mRoot == null)
        {
            return;
        }
        bool showTarget = true;
        var mapData = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
        if (mapData != null && mapData.Count > 0)
        {
            int mapType = Convert.ToInt32(mapData["type"]);
            var mapSetting = GameUtil.GetDBData2("MapSetting", string.Format("{{ type = {0} }}", mapType));
            if (mapSetting != null && mapSetting.Count > 0)
            {
                showTarget = Convert.ToInt32(mapSetting[0]["target_menu"]) == 1;
            }
        }
        mRoot.FindChildByEditName<HZCanvas>("cvs_target").Visible = showTarget;
        Reset();
    }

    private void InitCompmont()
    {
        InitHeroInfo();
        InitTargetInfo();
    }

    private void InitHeroInfo()
    {
        if (mRoot == null)
        {
            return;
        }
        mHeroInfo = new HeroInfoUI(mRoot.FindChildByEditName<HZCanvas>("cvs_role"));
        mHeroInfo.OnFrameTouch = OnHeroTouch;
        DataMgr.Instance.UserData.AttachObserver(this);
        EventManager.Subscribe("Event.User.SwapHeadIcon", OnUserHeadChange);
    }

    private void InitTargetInfo()
    {
        if (mRoot == null)
        {
            return;
        }
        string[] cvsName = { "cvs_monster", "cvs_normal", "cvs_boss" };
        mTargetUIRoot = new HZCanvas[cvsName.Length];
        for (int i = 0; i < cvsName.Length; ++i)
        {
            mTargetUIRoot[i] = mRoot.FindChildByEditName<HZCanvas>(cvsName[i]);
            mTargetUIRoot[i].Visible = false;
        }
        mTargetInfo = new TargetInfoUI(mTargetUIRoot[0]);
        mTargetInfo.OnFrameTouch = OnTargtTouch;
    }

    private void Reset()
    {
        mHeroInfo.SetLevel(DataMgr.Instance.UserData.TryGetIntAttribute(global::UserData.NotiFyStatus.LEVEL));
        mHeroInfo.SetFightPower(DataMgr.Instance.UserData.TryGetIntAttribute(global::UserData.NotiFyStatus.FIGHTPOWER));
        mHeroInfo.ChangeHead(DataMgr.Instance.UserData.RoleID, GameUtil.GetHeadIcon(DataMgr.Instance.UserData.Pro, DataMgr.Instance.UserData.Gender));
        mHeroInfo.SetVipLv();
        mHeroInfo.SetVipBtn();
    }

    private void OnUserHeadChange(EventManager.ResponseData res)
    {
        object state = null;
        Dictionary<object, object> data = (Dictionary<object, object>) res.data[1];
        if (data.TryGetValue("path", out state))
        {
            string path = (string)state;
            if (string.IsNullOrEmpty(path))
            {
                path = GameUtil.GetHeadIcon(DataMgr.Instance.UserData.Pro, DataMgr.Instance.UserData.Gender);
            }

            mHeroInfo.ChangeHead(DataMgr.Instance.UserData.RoleID, path);
        }
    }

    public void SetBufChange(TLAIUnit unit, ClientBuffInfo buff, BuffReasonType reason)
    {
        UnitInfoUI unitui = null;
        if (unit.IsActor())
        {
            unitui = mHeroInfo;
        }
        else
        {
            unitui = mTargetInfo;
        }
        switch (reason)
        {
            case BuffReasonType.Add:
                unitui.AddBuff(buff);
                break;
            case BuffReasonType.Change:
                unitui.ChangeBuff(buff);
                break;
            case BuffReasonType.Removed:
                unitui.RemoveBuff(buff);
                break;
        }
    }

    public void InitTargetBuff(TLAIUnit unit)
    {
        if (unit.BuffInfoList != null)
        {
            UnitInfoUI unitui = null;
            if (unit.IsActor())
            {
                unitui = mHeroInfo;
            }
            else
            {
                unitui = mTargetInfo;
            }
            if (unitui != null)
                unitui.InitBuffInfo();
            foreach (var buff in unit.BuffInfoList)
            {
                SetBufChange(unit, buff.Value, BuffReasonType.Add);
            }
        }
    }

    public void SetUserHP(int oldHP, int newHP, int maxHP)
    {
        mHeroInfo.SetHP(newHP, maxHP);
    }

    public void SetUserLevel(int level)
    {
        mHeroInfo.SetLevel(level);
    }

    public void SetTargetHP(int oldHP, int newHP, int maxHP)
    {
        mTargetInfo.SetHP(newHP, maxHP);
    }

    public void ChangeTarget(string playerUUID, int mType, string head, string name, int level, int curHP, int maxHP, int curMP, int maxMP)
    {
        mType = mType <= 3 ? mType : 2;
        mTargetInfo.ResetRootNode(mTargetUIRoot[mType - 1]);
        mTargetInfo.ChangeHead(playerUUID, head);
        mTargetInfo.SetLevel(level);
        mTargetInfo.SetName(name);
        mTargetInfo.SetHP(curHP, maxHP);
        mTargetInfo.ShowInfo(true);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (mHeroInfo != null)
        {
            mHeroInfo.Update();
        }
        if (mTargetInfo != null)
        {
            mTargetInfo.Update();
        }
    }

    public void RemoveTarget()
    {
        if (mTargetInfo != null)
            mTargetInfo.ShowInfo(false);
    }

    private void OnHeroTouch()
    {
        //弹出功能菜单
        //TLBattleScene.Instance.Actor.ChangeTarget(TLBattleScene.Instance.Actor.ObjectID, false);
        MenuMgr.Instance.OpenUIByTag("AttributeMain", 0);
    }

    private void OnTargtTouch()
    {
        //弹出交互菜单
        if (OnTargetTouch != null)
            OnTargetTouch();
    }


    public bool Notify(long status, UserData subject)
    {
        if ((status & (long)global::UserData.NotiFyStatus.LEVEL) != 0)
        {
            mHeroInfo.SetLevel(subject.TryGetIntAttribute(global::UserData.NotiFyStatus.LEVEL));
        }
        else if ((status & (long)global::UserData.NotiFyStatus.FIGHTPOWER) != 0)
        {
            mHeroInfo.SetFightPower(subject.TryGetIntAttribute(global::UserData.NotiFyStatus.FIGHTPOWER));
            mHeroInfo.RefreshPracticeInfo();
        }
        else if ((status & (long)global::UserData.NotiFyStatus.PRACTICELV) != 0)
        {
            mHeroInfo.RefreshPracticeInfo();
        }
        else if ((status & (long)global::UserData.NotiFyStatus.VIP) != 0)
        {
            mHeroInfo.SetVipLv();
        }
        return false;
    }

    public void BeforeDetach(UserData subject) { }
    public void BeforeAttach(UserData subject) { }



    public void Clear(bool reLogin, bool reConnect)
    {
        RemoveTarget();
        mHeroInfo.Clear(reLogin, reConnect);
        mTargetInfo.Clear(reLogin, reConnect);
        if (reLogin)
        {
            EventManager.Unsubscribe("Event.User.SwapHeadIcon", OnUserHeadChange);
        }
    }


    private class UnitInfoUI
    {

        public delegate void OnFrameTouchEvent();
        public OnFrameTouchEvent OnFrameTouch;

        protected HZCanvas mRoot = null;

        protected HZGauge mHP = null;
        protected HZImageBox mFace = null;
        protected HZLabel mName = null;
        protected HZLabel mLevel = null;
        protected HZLabel mFightPower = null;
        protected HZLabel mVipLv = null;
        protected HZTextButton mVipBtn = null;
        private HZCanvas mBuffList = null;
        private HZCanvas mBuffnode = null;
        private HashMap<int, BuffIconInfo> mBuffInfoList = new HashMap<int, BuffIconInfo>();
        private const int ShowBuffIconMax = 6;
        private static string LastOpenUI;

        public UnitInfoUI(HZCanvas root)
        {
            ResetRootNode(root);
        }

        public void ResetRootNode(HZCanvas root)
        {
            if (mRoot != root)
            {
                if(mRoot != null)
                    mRoot.Visible = false;
                mRoot = root;
                OnInit();
                InitBuffInfo();
            }
        }

        public void InitBuffInfo()
        {
            mBuffList = mRoot.FindChildByEditName<HZCanvas>("cvs_bufflist");
            mBuffnode = mRoot.FindChildByEditName<HZCanvas>("cvs_buffnode");
            mBuffList.TouchClick = (sender) =>
            {

                if (mBuffInfoList != null && mBuffInfoList.Count > 0)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("BuffInfoList", mBuffInfoList);
                    dic.Add("Rootname", mRoot.Name);
                    EventManager.Fire("ShowBuffList", dic);
                }
            };

            LastOpenUI = string.Empty;
            mBuffList.Visible = true;
            mBuffList.RemoveAllChildren(true);
            mBuffnode.Visible = false;
            mBuffInfoList.Clear();
            EventManager.Unsubscribe("CloseBuffList", CloseBuffList);
            EventManager.Subscribe("CloseBuffList", CloseBuffList);
        }

        private void CloseBuffList(EventManager.ResponseData res)
        {
            Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
            object value;
            if (data.TryGetValue("Rootname", out value))
            {
                if (LastOpenUI == (string)value)
                {
                    LastOpenUI = string.Empty;
                }
            }
        }

        public void AddBuff(ClientBuffInfo buff)
        {
            // Debugger.LogError("AddBuff" + buff.id);
           
            BuffIconInfo buffinfo;
            if (mBuffInfoList.TryGetValue(buff.id,out buffinfo))
            {
                buffinfo.UIGauge.Value = buff.PassTime;
            }
            else
            {
                var bii = new BuffIconInfo
                {
                    Buffinfo = buff.Clone(),
                    UICanvas = (HZCanvas) mBuffnode.Clone()
                };
                bii.UIGauge = bii.UICanvas.FindChildByEditName<HZGauge>("gg_buff1");
                bii.CurTime = DateTime.Now;
                if (!string.IsNullOrEmpty(buff.Data.IconName))
                {
                    bii.UIGauge.Layout = HZUISystem.CreateLayoutFromAtlas(buff.Data.IconName, UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
                }

                bii.UIGauge.SetGaugeMinMax(0, buff.TotalTime);
                bii.UIGauge.Value = buff.PassTime;
                mBuffInfoList.Add(buff.id,bii);
                CheckAddUI(bii);
            }
            
//            var buffinfo = mBuffInfoList.GetOrAdd(buff.id, (id) =>
//             {
//                 return bii;
//             });
//            buffinfo = bii;
//           CheckAddUI(bii);
        }

        private void CheckAddUI(BuffIconInfo iconinfo)
        {
            if (mBuffList.NumChildren < ShowBuffIconMax)
            {
                iconinfo.UICanvas.X = mBuffList.NumChildren * iconinfo.UICanvas.Width;
                iconinfo.UICanvas.Y = 0;
                iconinfo.UICanvas.Visible = true;
                mBuffList.AddChild(iconinfo.UICanvas);
                iconinfo.IsShow = true;
            }
        }

        private void CheckRemoveUI(BuffIconInfo iconinfo)
        {

            if (iconinfo.UICanvas != null && iconinfo.IsShow)
            {
                iconinfo.UICanvas.RemoveFromParent(true);
            }
            else
            {
                return;
            }

            for (int i = 0 ; i < mBuffList.NumChildren ; i++)
            {
                var _node = mBuffList.GetChildAt(i);
                _node.X = i * _node.Width;
            }
        }

        public void ChangeBuff(ClientBuffInfo buff)
        {
            //Debugger.LogError("ChangeBuff "+ buff.id);
            var buffinfo = mBuffInfoList.Get(buff.id);
            if (buffinfo != null)
            {
                buffinfo.Buffinfo = buff.Clone();
                buffinfo.UIGauge.SetGaugeMinMax(0, buff.TotalTime);
                buffinfo.UIGauge.Value = buff.PassTime;
                buffinfo.Buffinfo.PassTime = buff.PassTime;
            }
        }

        public void RemoveBuff(ClientBuffInfo buff)
        {
            //Debugger.LogError("RemoveBuff" + buff.id);
            var buffinfo = mBuffInfoList.Get(buff.id);
            mBuffInfoList.RemoveByKey(buff.id);
            if (buffinfo != null)
            {
                CheckRemoveUI(buffinfo);
            }
            
            foreach (var _bi in mBuffInfoList)
            {
                if (!_bi.Value.IsShow)
                {
                    CheckAddUI(_bi.Value);
                    break;
                }
            }
        }
        
        
        public void RemoveBuff(BuffIconInfo buff)
        {
            //Debugger.LogError("RemoveBuff" + buff.id);
            mBuffInfoList.RemoveByKey(buff.Buffinfo.id);
            CheckRemoveUI(buff);
        }

        public void ClearBuff()
        {
            if (mBuffInfoList != null && mBuffInfoList.Count > 0)
            {
                var list = new List<BuffIconInfo>();
                foreach (var data in mBuffInfoList)
                {
                    list.Add(data.Value);
                }
                for (int i = list.Count - 1;i>= 0;i--)
                {
                    RemoveBuff(list[i]);
                }
            }
            
            
           
        }
        

        public void Update()
        {
            if (mBuffInfoList.Count > 0)
            {
                foreach (var bi in mBuffInfoList)
                {
                    if (bi.Value != null)
                    {
                        var time = bi.Value.UIGauge.Value + (DateTime.Now - bi.Value.CurTime).Milliseconds;
                        time = Math.Min(time, bi.Value.UIGauge.GaugeMaxValue);
                        bi.Value.UIGauge.Value = (int)time;
                        bi.Value.Buffinfo.PassTime = (int)time;
                        bi.Value.CurTime = DateTime.Now;
                    }
                }
            }
        }

        protected virtual void OnInit()
        {
            //公共部分在这里初始化

            mHP = mRoot.FindChildByEditName<HZGauge>("gg_hp");
            if (mHP != null)
            {
                mHP.SetGaugeMinMax(0, 100);
                mHP.SetFillMode(UnityEngine.UI.Image.FillMethod.Horizontal, (int)UnityEngine.UI.Image.OriginHorizontal.Left);
                mHP.Value = 1.0f;
            }

            mFace = mRoot.FindChildByEditName<HZImageBox>("ib_target");
            if (mFace != null)
            {
                mFace.Enable = true;
                mFace.IsInteractive = true;
                mFace.TouchClick = OnTouch;
            }
            
            mLevel = mRoot.FindChildByEditName<HZLabel>("lb_lv");
            mVipLv = mRoot.FindChildByEditName<HZLabel>("lb_vip");
            mFightPower = mRoot.FindChildByEditName<HZLabel>("lb_fight");
            mName = mRoot.FindChildByEditName<HZLabel>("lb_name");
            mVipBtn = mRoot.FindChildByEditName<HZTextButton>("btn_vip");
        }

        public void SetHP(int newHP, int maxHP)
        {
            if (mHP == null)
            {
                return;
            }

            if (newHP > maxHP)
            {
                newHP = maxHP;
            }

            if (newHP < 0)
            {
                newHP = 0;
            }

            mHP.SetGaugeMinMax(0, maxHP);
            mHP.Value = newHP;
            float v = 100f * newHP / maxHP;
            mHP.Text = string.Format("{0}/{1}", newHP, maxHP);
        }


        public void SetFace(string path)
        {
            if (mFace == null)
                return;
            UILayout layout = HZUISystem.CreateLayoutFromFile(path, UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
            if (layout != null)
            {
                mFace.Layout = layout;
            }
        }

        public void SetName(string name)
        {
            if (mName == null)
                return;

            mName.Text = name;
        }

        public void SetLevel(int lv)
        {
            if (mLevel == null)
                return;

            mLevel.Text = "" + lv;
        }
        
        public void SetVipLv()
        {
            if (mVipLv==null)
                return;
            mVipLv.Text = DataMgr.Instance.UserData.VipLv == 13 ? "t" : "z" + DataMgr.Instance.UserData.VipLv;
        }

        public void SetVipBtn()
        {
            object[] param = {"RechargeVip"};
            mVipBtn.TouchClick = (sender) =>
            {
                MenuMgr.Instance.OpenUIByTag("Recharge",0,param);
            };
        }

        public void SetFightPower(int fightPower)
        {
            mFightPower.Text = "z" + fightPower;
        }

        public void ChangeHead(string playerUUID, string head)
        {
            string curUUID = playerUUID;
            if (!string.IsNullOrEmpty(head))
            {
                this.SetFace(head);
            }
            if (!string.IsNullOrEmpty(playerUUID))
            {
                CacheImage.Instance.DownLoad(playerUUID, (args) =>
                {
                    if (mFace == null || !(bool)args[0] || !string.Equals(curUUID, playerUUID))
                        return;
                    UILayout layout = HZUISystem.CreateLayout((string)args[2], UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
                    if (layout != null)
                    {
                        mFace.Layout = layout;
                    }
                });
            }
        }

        public void ShowInfo(bool visible)
        {
            if (mRoot == null)
                return;

            mRoot.Visible = visible;
        }

        protected void OnTouch(DisplayNode sender)
        {
            if (OnFrameTouch != null)
                OnFrameTouch();
        }

        public virtual void Clear(bool reLogin, bool reConnect)
        {

        }

    }


    private class HeroInfoUI : UnitInfoUI
    {

        private int mEffectGauge;
        private int mEffectArrow;

        public HeroInfoUI(HZCanvas root) : base(root)
        {
            RefreshPracticeInfo();
        }

        public void RefreshPracticeInfo()
        {
            int practiceLv = DataMgr.Instance.UserData.TryGetIntAttribute(global::UserData.NotiFyStatus.PRACTICELV);
            HZCanvas practiceCvs = mRoot.FindChildByEditName<HZCanvas>("cvs_practice");
            practiceCvs.Visible = practiceLv > 0;
            if (practiceLv > 0)
            {
                int fightPower = DataMgr.Instance.UserData.TryGetIntAttribute(global::UserData.NotiFyStatus.FIGHTPOWER);
                MenuBase.SetLabelText(mRoot, "lb_practice", GameUtil.GetPracticeName(practiceLv, fightPower), 0, 0);
                int minPower = 0;
                if (practiceLv > 1)
                {
                    var dbPre = GameUtil.GetDBData2("practice_stage", string.Format("{{ artifact_stage = {0}, stage_lv = {1} }}", practiceLv - 1, 4));
                    minPower = int.Parse(dbPre[0]["power"].ToString());
                }
                var dbCur = GameUtil.GetDBData2("practice_stage", string.Format("{{ artifact_stage = {0}, stage_lv = {1} }}", practiceLv, 4));
                int maxPower = int.Parse(dbCur[0]["power"].ToString());
                HZGauge gg = mRoot.FindChildByEditName<HZGauge>("gg_practice");
                gg.SetGaugeMinMax(minPower, maxPower);
                gg.Value = UnityEngine.Mathf.Clamp(fightPower, minPower, maxPower);
                gg.IsShowPercent = true;
                
                var dbLv = GameUtil.GetDBDataFull("practice");
                HZTextButton up = mRoot.FindChildByEditName<HZTextButton>("btn_up");
                up.Visible = gg.ValuePercent == 100 && practiceLv < dbLv.Count;
                LuaSystem.Instance.DoFunc("GlobalHooks.UI.SetRedTips", new object[] { "practice_break", up.Visible ? 1 : 0, null });
                up.TouchClick = (sender) =>
                {
                    MenuMgr.Instance.OpenUIByTag("PracticeMain");
                };

                if (fightPower >= maxPower)
                {
                    if (mEffectGauge == 0)
                    {
                        var transSet = new TransformSet();
                        transSet.Pos = new Vector3(gg.Width * 0.5f, -gg.Height * 0.5f, -5);
                        transSet.Parent = gg.Transform;
                        transSet.Layer = (int)PublicConst.LayerSetting.UI;
                        mEffectGauge = RenderSystem.Instance.PlayEffect("/res/effect/ui/ef_ui_hint_strip.assetbundles", transSet);
                    }

                    if (mEffectArrow == 0)
                    {
                        var transSet = new TransformSet();
                        transSet.Pos = new Vector3(up.Width * 0.5f, -up.Height * 0.5f, -5);
                        transSet.Parent = up.Transform;
                        transSet.Layer = (int)PublicConst.LayerSetting.UI;
                        mEffectArrow = RenderSystem.Instance.PlayEffect("/res/effect/ui/ef_ui_hint_arrowhead.assetbundles", transSet);
                    }
                }
                else
                {
                    ReleaseEffect();
                }
            }
        }

        private void ReleaseEffect()
        {
            if (mEffectGauge != 0)
            {
                RenderSystem.Instance.Unload(mEffectGauge);
                mEffectGauge = 0;
            }
            if (mEffectArrow != 0)
            {
                RenderSystem.Instance.Unload(mEffectArrow);
                mEffectArrow = 0;
            }
        }

        public override void Clear(bool reLogin, bool reConnect)
        {
            if(reLogin)
                ReleaseEffect();
            ClearBuff();
        }

    }

    private class TargetInfoUI : UnitInfoUI
    {

        public TargetInfoUI(HZCanvas root) : base(root)
        {

        }

        protected override void OnInit()
        {
            base.OnInit();
        }

    }

    public class ClientBuffInfo
    {

        public BuffTemplate Data;
        public int id;
        public int TotalTime;
        public int PassTime;
        public DateTime curDateTime;
        public ClientBuffInfo Clone()
        {
            ClientBuffInfo buffInfo = new ClientBuffInfo();
            buffInfo.Data = (BuffTemplate)this.Data.Clone();
            buffInfo.id = this.id;
            buffInfo.PassTime = this.PassTime;
            buffInfo.TotalTime = this.TotalTime;
            return buffInfo;
        }

    }

    private class BuffIconInfo
    {
        public ClientBuffInfo Buffinfo;
        public HZCanvas UICanvas;
        public HZGauge UIGauge;
        public DateTime CurTime;

        public bool IsShow { get; set; }
    }

}
