using Assets.Scripts;
using DeepCore;
using DeepCore.GameData.Data;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using SLua;
using System;
using System.Collections.Generic;
using System.IO;
using TLBattle.Client;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using UnityEngine;

public partial class TLAIActor : TLAIPlayer, IComAIActor
{
    /// <summary>
    /// 自动战斗状态.
    /// </summary>
    /// 
    [DoNotToLua]
    public enum AutoGuardStatus : byte
    {
        Stop,               //停止自动战斗.
        StartBySkill,       //需要释放一次技能激活自动战斗.
        StartByMoveStop,    //停止移动后激活自动战斗.
        Start,              //开启中.
    }
    [DoNotToLua]
    public ZoneActor ZActor { get { return ZObj as ZoneActor; } }
    [DoNotToLua]
    public delegate void OnLoadOKCallback();
    [DoNotToLua]
    public OnLoadOKCallback OnLoadOK;

    public bool IsAutoRun { get; private set; }

    private HumanFocus mHumanFocus;
    private static string strPickItemEffect = "/res/effect/ui/ef_ui_tailing.assetbundles";

    public const string AUTO_BATTLE_OPEN_TAG = "AutoBattle";


    /// <summary>
    /// 主角加载是否不使用缓存
    /// </summary>
    public const bool DontUseCache = false;
    #region 自动战斗.

    /// <summary>
    /// 当前自动战斗状态.
    /// </summary>
    private AutoGuardStatus curGuardStatus = AutoGuardStatus.Stop;

    /// <summary>
    /// 自动挂机坐标.
    /// </summary>
    private float guardPosX, guardPosY;

    private float GuardMoveRange = 15;

    #endregion


    /// <summary>
    /// 开启雷达寻宝
    /// </summary>
    private FuckAssetObject mTreasureObject = null;

    //目标锁定最远配置距离，-1表示走AOI
    private float mTargetRangeLimit = -1;

    private HumanFocus HumanFocus
    {
        get
        {
            if (mHumanFocus == null)
            {
                //mHumanFocus = this.ObjectRoot.GetComponentInChildren<HumanFocus>();
            }
            return mHumanFocus;
        }
    }

    public bool Dead()
    {
        return IsDead;
    }

    [DoNotToLua]
    public TLAIActor(BattleScene battleScene, ZoneActor obj)
            : base(battleScene, obj)
    {

        //单位初始朝向设置.
        ChangeCameraWithDirection(obj.Direction);
        //InitSkillSelector();
        InitAutoMove();
        obj.OnLaunchSkill += Obj_OnLaunchSkill;
        //初始化目标锁定距离配置.
        var db = GameUtil.GetDBData("GameConfig", "scene_selectmaxdistance");
        mTargetRangeLimit = db != null ? float.Parse((string)db["paramvalue"]) : mTargetRangeLimit;

        ShowModel = true;
        InitMoveAgent();
        InitRadarManger();
    }
    private void PlayerVirtual_OnRevengeListChanged()
    {
        bindBehaviour.CheckAllPlayersInRevenge(PlayerVirtual);
    }
    private void PlayerVirtual_OnPKValueChanged(int obj)
    {
        DataMgr.Instance.UserData.PKValue = PlayerVirtual.GetPKValue();
    }
    protected override void PlayerVirtual_OnPKLevelChanged(int obj)
    {
        base.PlayerVirtual_OnPKLevelChanged(obj);

        DataMgr.Instance.UserData.PKValue = PlayerVirtual.GetPKValue();
    }
    private void PlayerVirtual_OnCurTargetChanged(uint obj)
    {
        if (obj != this.ObjectID)
            Target.SetTarget(obj);
    }

    private void Obj_OnLaunchSkill(ZoneUnit unit, ZoneUnit.SkillState skill, UnitLaunchSkillEvent evt)
    {
        var skillprop = skill.Data.Properties as TLSkillProperties;
        if (skillprop != null)
        {
            if (!string.IsNullOrEmpty(skillprop.TriggerEvent))
            {
                string filename = "effect/" + skillprop.TriggerEvent + ".lua";
                TLBattleScene.Instance.DoParseLuaEventByFileName(filename);
            }
        }
    }

    protected override void ZUnit_OnBuffAdded(ZoneUnit unit, ZoneUnit.BuffState state)
    {
        base.ZUnit_OnBuffAdded(unit, state);
        var prop = state.Data.Properties as TLBuffProperties;
        if (prop.IsShowWord && !string.IsNullOrEmpty(prop.CustomEffectRes))
        {
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add("id", state.BuffID);
            dict.Add("cd", state.CDPercent);
            dict.Add("lifetime", state.Data.LifeTimeMS);
            dict.Add("res", prop.CustomEffectRes);
            EventManager.Fire("Event.Buff.Add", dict);
        }
    }
    protected override void ZUnit_OnBuffChanged(ZoneUnit unit, ZoneUnit.BuffState state)
    {
        base.ZUnit_OnBuffChanged(unit, state);
        //EventManager.Fire("Event.Buff.Change", OnShowChangeBuff)
        var prop = state.Data.Properties as TLBuffProperties;
        if (prop.IsShowWord && !string.IsNullOrEmpty(prop.CustomEffectRes))
        {
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add("id", state.BuffID);
            dict.Add("cd", state.CDPercent);
            dict.Add("lifetime", state.Data.LifeTimeMS);
            EventManager.Fire("Event.Buff.Change", dict);
        }
    }
    protected override void ZUnit_OnBuffRemoved(ZoneUnit unit, ZoneUnit.BuffState state)
    {
        var prop = state.Data.Properties as TLBuffProperties;
        if (prop.IsShowWord && !string.IsNullOrEmpty(prop.CustomEffectRes))
        {
            Dictionary<object, object> dict = new Dictionary<object, object>();
            dict.Add("id", state.BuffID);
            EventManager.Fire("Event.Buff.Remmove", dict);
        }
        base.ZUnit_OnBuffRemoved(unit, state);
        //EventManager.Fire("Event.Buff.Remmove", OnShowRemoveBuff)

    }

    public override bool IsActor()
    {
        return true;
    }
    public override bool SoundImportant()
    {
        return true;
    }

    protected override bool GetShadowCaster()
    {
        return true;
    }

    protected override void OnLoadModelFinish(FuckAssetObject aoe)
    {
        base.OnLoadModelFinish(aoe);
        if (PlayerVirtual != null)
        {
            PlayerVirtual.OnCurTargetChanged += PlayerVirtual_OnCurTargetChanged;
            PlayerVirtual.OnPKValueChanged += PlayerVirtual_OnPKValueChanged;
            PlayerVirtual.OnRevengeListChanged += PlayerVirtual_OnRevengeListChanged;
        }

        this.DynamicBoneEnable = true;
        SyncAutoGuardState();
        SyncPKInfo();
        InitFindTreasure(this.ObjectRoot);
        InitRadar();
        if (OnLoadOK != null)
        {
            OnLoadOK();
        }

        if (this.Vehicle.IsRiding)
        {
            float H = 0;
            if (this.Vehicle.Setting != null)
            {
                H = this.Vehicle.Setting.Hegiht;
            }
            GameSceneMgr.Instance.SceneCameraNode.FollowOffsetY = H;

            InitShadowCaster(this.Vehicle.ObjectRoot, this.Vehicle.IsRiding);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("isRideFailed", false);
            dic.Add("rideStatus", true);
            EventManager.Fire("Event.UI.RideStatusChange", dic);
        }
        else
            GameSceneMgr.Instance.SceneCameraNode.FollowOffsetY = 0;

        //检查是否处于跟随状态
        if (DataMgr.Instance.TeamData.IsFollowLeader)
        {
            StartTeamFollow(DataMgr.Instance.TeamData.LeaderID);
        }
    }

    protected override void OnLoadVehicleFinish(RenderVehicle vehicle)
    {
        bindBehaviour.InitInfoBar(true);

        GameSceneMgr.Instance.SceneCameraNode.FollowOffsetY = vehicle.IsRiding ? (vehicle.Setting ? vehicle.Setting.Hegiht : 0) : 0;

        InitShadowCaster(vehicle.ObjectRoot, vehicle.IsRiding);
    }

    protected override void OnChangeBodyFinish(FuckAssetObject aoe)
    {
        if (aoe)
        {
            base.OnChangeBodyFinish(aoe);
            InitShadowCaster(this.ObjectRoot, true);
            if (this.Vehicle.IsRiding)
            {
                GameSceneMgr.Instance.SceneCameraNode.FollowOffsetY = this.Vehicle.IsRiding ? (this.Vehicle.Setting ? this.Vehicle.Setting.Hegiht : 0) : 0;
                InitShadowCaster(Vehicle.ObjectRoot, Vehicle.IsRiding);
            }
        }
    }

    protected override void ZUnit_OnAddAvatarFinish()
    {
        base.ZUnit_OnAddAvatarFinish();
        InitShadowCaster(this.ObjectRoot, true);
        if (this.Vehicle != null && this.Vehicle.IsRiding)
        {
            GameSceneMgr.Instance.SceneCameraNode.FollowOffsetY = this.Vehicle.IsRiding ? (this.Vehicle.Setting ? this.Vehicle.Setting.Hegiht : 0) : 0;
            InitShadowCaster(Vehicle.ObjectRoot, Vehicle.IsRiding);
        }
    }

    protected override void InitShadowCaster(GameObject obj, bool toSelfLayer)
    {
        //        if (IsDisposed)
        //        {
        //            return;
        //        }

        GameObject shadowObj = GameObject.Find("/MapObject/MapNode/ShadowNode/ShadowCaster");
        if (shadowObj != null)
        {
            if (toSelfLayer)
                GameUtil.ReplaceLayer(obj, (int)PublicConst.LayerSetting.CharacterUnlit, (int)PublicConst.LayerSetting.SelfLayer);
            else
                GameUtil.ReplaceLayer(obj, (int)PublicConst.LayerSetting.SelfLayer, (int)PublicConst.LayerSetting.CharacterUnlit);

            RenderShadow renderShadow = shadowObj.GetComponent<RenderShadow>();
            if (renderShadow == null || this.ObjectRoot == null)
            {
                Debugger.LogError("renderShadow=" + renderShadow + " this.ObjectRoot = " + this.ObjectRoot);
            }
            else
            {
                if (renderShadow.target != this.ObjectRoot.transform)
                    renderShadow.target = this.ObjectRoot.transform;
            }


        }
        var soundobj = this.DisplayRoot.GetComponentInChildren<TLPlaySound>();
        if (soundobj != null)
        {
            soundobj.CanPlay = true;
        }
    }

    protected override void OnBaseInfoChanged(TLUnitBaseInfo info)
    {
        DataMgr.Instance.UserData.Name = info.Name;
        base.OnBaseInfoChanged(info);
    }

    private float mAngle;
    private float mDistance;

    [DoNotToLua]
    public void SendUnitMove(float dx, float dy)
    {
        TryChangeManualOperateState();

        if (mUnitAxisAngleHandler != null)
            mUnitAxisAngleHandler.Invoke(dx, dy);
    }

    public void SendUnitStop()
    {
        if (mUnitStopMoveHandler != null)
        {
            mUnitStopMoveHandler.Invoke();
        }
    }

    private void SendUnitAxisAngle(float dx, float dy)
    {
        float angle = 0;
        float distance = 0;
        float faceto = 0;
        Vector2 direct = new Vector2(dx, dy);
        angle = DeepCore.Vector.MathVector.getDegree(dx, dy);
        faceto = angle;
        distance = 10f;
        SendUnitAxisAngle(angle, distance, faceto);
    }

    private void SendUnitAxisAngle(float angle, float distance, float faceto)
    {
        mAngle = angle;
        mDistance = distance;
        //Debugger.LogError(" ZActor.IsCanControlFaceTo=" + ZActor.IsCanControlFaceTo+ "  ZActor.IsCanControlMove=" + ZActor.IsCanControlMove);
        if (ZActor.IsCanControlFaceTo)
        {
            FaceToDirect = faceto;
        }
        if (!this.IsDead)
        {
            this.ZActor.SendUnitAxisAngle(mAngle, mDistance, FaceToDirect);
        }

    }

    private void SendUnitStopMove(bool isFollowCamera = true)
    {
        mDistance = 0;
        if (ZActor.CurrentState != UnitActionStatus.Skill && isFollowCamera)
        {
            //mFaceto = Extensions.UnityRot2ZoneRot(Camera.main.gameObject.Rotation());
        }
    }

    protected override void OnBeginUpdate(float deltaTime)
    {
        //this.ZActor.SendUnitAxisAngle(mAngle, mDistance, mFaceto);
        base.OnBeginUpdate(deltaTime);
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        UpdateActorState(deltaTime);
        UpdateTeamFollow(deltaTime);
        //UpdateSkill(deltaTime);
        CheckTargetRange();
        UpdateFindTreasure(deltaTime);
    }

    private TeamFollowState mTeamFollowState;


    public void StopTeamFollow()
    {
        if (mTeamFollowState != null)
        {
            ChangeActorState(new IdleState(this));
            mTeamFollowState = null;
        }

    }

    public bool StartTeamFollow(string uuid)
    {
        if (string.IsNullOrEmpty(uuid))
        {
            return false;
        }
        if (mTeamFollowState == null)
        {
            mTeamFollowState = new TeamFollowState(this, uuid) { CheckIntervalSec = 0.5f };
        }
        return ChangeActorState(mTeamFollowState);
    }

    public void UpdateTeamFollow(float delta)
    {
        if (mTeamFollowState != null)
        {
            mTeamFollowState.TryReEnter();
        }
    }


    protected override void RegistAllObjectEvent()
    {
        base.RegistAllObjectEvent();
        base.RegistObjectEvent<PlayerSkillInfoEventB2C>(ObjectEvent_PlayerSkillChangedEvent);
        base.RegistObjectEvent<PlayerBattlePropChangeEventB2C>(ObjectEvent_PlayerBattlePropChangeEventB2C);
        //base.RegistObjectEvent<UnitGotInstanceItemEvent>(ObjectEvent_UnitGotInstanceItemEvent);
        base.RegistObjectEvent<PKModeChangeEventB2C>(PlayerEvent_ChangePKModeB2C);
        base.RegistObjectEvent<ShowTipsEventB2C>(PlayerEvent_ShowTipsEventB2C);
        base.RegistObjectEvent<AutoChangePKModeEventB2C>(PlayerEvent_AutoChangePKModeEventB2C);
        base.RegistObjectEvent<FindTargetUnitResponse>(ObjectEvent_FindTargetEvent);
        base.RegistObjectEvent<RadarEventB2C>(PlayerEvent_RadarEventB2C);
        base.RegistObjectEvent<PlayerGuildChaseListChangeB2C>(OnPlayerGuildChaseListChangeB2C);
        EventManager.Subscribe("EVENT_UI_NPCTALK", TalkToNpc);
        //EventManager.Subscribe("CloseNpcCamera", CloseNpcTalkEvent);
    }

    protected override void ObjectEvent_UnitDeadEvent(UnitDeadEvent ev)
    {
        base.ObjectEvent_UnitDeadEvent(ev);
        EventManager.Fire("Event.Actor.DeadEvent", EventManager.defaultParam);
        EventManager.Fire("NpcTalkClose", EventManager.defaultParam);
    }


    private void CloseNpcTalkEvent(EventManager.ResponseData res)
    {
        Dictionary<object, object> dic = (Dictionary<object, object>)res.data[1];
        object value;
        if (dic.TryGetValue("unit", out value))
        {
            var iter = TLBattleScene.Instance.BattleObjects.GetEnumerator();
            while (iter.MoveNext())
            {
                var _unit = iter.Current.Value as TLAIUnit;
                if (_unit != null && !_unit.IsDisposed && _unit.TemplateID == Convert.ToInt32(value))
                {
                    _unit.FaceToOrgin();
                    break;
                }
            }
        }
    }
    private void TalkToNpc(EventManager.ResponseData res)
    {
        Dictionary<string, object> dic = (Dictionary<string, object>)res.data[1];
        object value;
        if (dic.TryGetValue("TemplateId", out value))
        {
            var iter = TLBattleScene.Instance.BattleObjects.GetEnumerator();
            while (iter.MoveNext())
            {
                var _unit = iter.Current.Value as TLAIUnit;
                if (_unit != null && !_unit.IsDisposed && _unit.TemplateID == Convert.ToInt32(value))
                {
                    FaceTo(_unit);
                    _unit.FaceTo(this);
                    break;
                }
            }
        }

    }


    protected virtual void ObjectEvent_PlayerSkillChangedEvent(PlayerSkillInfoEventB2C evt)
    {
        InitSkill(evt);
    }

    protected virtual void ObjectEvent_PlayerBattlePropChangeEventB2C(PlayerBattlePropChangeEventB2C ev)
    {
        DataMgr.Instance.UserData.RefreshRoleProp(ev.PropList);
    }

    private void PlayerEvent_ChangePKModeB2C(PKModeChangeEventB2C evt)
    {
        //UI刷新.
        SyncPKInfo();
        //选中效果刷新.
        SyncSelectEffect();
        //遍历视野中所有单位，能攻击的单位显示血条.
        CheckAllComAIPlayerHPBanner();
    }

    private void PlayerEvent_ShowTipsEventB2C(ShowTipsEventB2C evt)
    {
        var str = HZLanguageManager.Instance.GetString(evt.Msg);
        if (evt.Params != null)
            str = string.Format(str, evt.Params.ToArray());

        GameAlertManager.Instance.ShowFloatingTips(str);
    }

    private void PlayerEvent_AutoChangePKModeEventB2C(AutoChangePKModeEventB2C evt)
    {
        Dictionary<string, object> args = new Dictionary<string, object>();
        args.Add("s2c_mode", (int)evt.s2c_mode);
        EventManager.Fire("Event.AutoChangePKModeEventB2C", args);
    }

    protected override void PlayerEvent_TeamMemberListChangeEvtB2C(TeamMemberListChangeEvtB2C evt)
    {
        SyncSelectEffect();
        CheckAllTLUnitHPBanner();
        base.PlayerEvent_TeamMemberListChangeEvtB2C(evt);
    }

    private void OnPlayerGuildChaseListChangeB2C(PlayerGuildChaseListChangeB2C evt)
    {
        //选中效果刷新.
        SyncSelectEffect();
        //遍历视野中所有单位，能攻击的单位显示血条.
        CheckAllComAIPlayerHPBanner();
    }

    [DoNotToLua]
    public CombatStateChangeEventB2C.BattleStatus CurBattleStatus
    {
        get;
        private set;
    }

    public AutoGuardStatus CurGuardStatus
    {
        get
        {
            return curGuardStatus;
        }

        set
        {
            curGuardStatus = value;

            if (curGuardStatus == AutoGuardStatus.Stop)
                this.SyncUIBtnState(false);
            else if (curGuardStatus == AutoGuardStatus.Start)
            {
                this.SetGuardPos();
                this.SyncUIBtnState(true);
            }

        }
    }

    [DoNotToLua]
    public bool NeedRide(MoveEndAction action)
    {
        if (action == null || (action.AimX == 0 && action.AimY == 0))
        {
            return false;
        }

        if (!DataMgr.Instance.UserData.IsFuncOpen("MountFrame"))
        {
            return false;
        }

        Vector2 pos = new Vector2(this.X, this.Y);
        Vector2 pos1 = new Vector2(action.AimX, action.AimY);
        if (Vector2.Distance(pos, pos1) >= DataMgr.Instance.UserData.GetMountDistance())
        {
            return true;
        }
        return false;
    }

    public override bool IsImportant()
    {
        return true;
    }
    public bool isNoBattleStatus()
    {
        if (CurBattleStatus != CombatStateChangeEventB2C.BattleStatus.None)
        {
            return false;
        }
        return true;
    }

    public void UnMount()
    {
        if (this.Vehicle != null && this.Vehicle.IsRiding)
        {
            this.ReqMount(false);
        }
    }

    [DoNotToLua]
    public void ReqMount(bool rideStatus = true)
    {
        //if (this.Dead() == true)
        //{
        //    return;
        //}

        Dictionary<string, object> args = new Dictionary<string, object>();
        args.Add("ride", rideStatus);
        EventManager.Fire("Event.Mount.RideMount", args);

        //TLProtocol.Protocol.Client.ClientRidingMountRequest request = new TLProtocol.Protocol.Client.ClientRidingMountRequest();
        //request.ride = rideStatus;
        //TLNetManage.Instance.Request<TLProtocol.Protocol.Client.ClientRidingMountResponse>(request, (err, rsp) =>
        //     {

        //     });
    }

    protected override void OnCombatStateChange(CombatStateChangeEventB2C.BattleStatus status)
    {
        CurBattleStatus = status;
        base.OnCombatStateChange(status);
        BattleNumberManager.Instance.ShowCombat(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), status != CombatStateChangeEventB2C.BattleStatus.None);
    }

    private void CheckTargetRange()
    {
        if (Target.HasTarget() && mTargetRangeLimit != -1)
        {
            TLAIUnit t = (TLAIUnit)BattleScene.GetBattleObject(Target.TargetId);
            if (Vector3.Distance(ObjectRoot.transform.position, t.ObjectRoot.transform.position) > mTargetRangeLimit)
            {
                Target.RemoveTarget();
            }
        }
    }

    //玩家形象变化通知
    protected override void UpdateAvatarData(HashMap<int, TLAvatarInfo> aMap)
    {
        base.UpdateAvatarData(aMap);
        Dictionary<string, object> args = new Dictionary<string, object>();
        args.Add("avatar", DataMgr.Instance.UserData.GetAvatarListClone());
        EventManager.Fire("EVENT_ACTOR_AVATAR_CHANGE", args);
    }

    protected override void OnDispose()
    {
        if (mTreasureObject != null)
        {
            mTreasureObject.Unload();
            mTreasureObject = null;
        }
        if (mSkillTargetSelect != null)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(mSkillTargetSelect.gameObject);
            mSkillTargetSelect = null;
        }
        this.ZActor.OnLaunchSkill -= Obj_OnLaunchSkill;
        EventManager.Unsubscribe("EVENT_UI_NPCTALK", TalkToNpc);
        EventManager.Unsubscribe("EVENT_UI_FindTreasure", FindTreasure);
        //EventManager.Unsubscribe("CloseNpcCamera", CloseNpcTalkEvent);
        ClearAutoMoveListen();
        ClearRadarManager();
        base.OnDispose();

        OnLoadOK = null;
    }

    private void CheckAllComAIPlayerHPBanner()
    {
        TLBattleScene.Instance.FindBattleObjectAs<TLAIPlayer>(m =>
        {
            m.CheckShowHPBanner(true);
            return false;
        });
    }

    private void CheckAllTLUnitHPBanner()
    {
        TLBattleScene.Instance.FindBattleObjectAs<TLAIUnit>(m =>
        {
            m.CheckShowHPBanner(true);
            return false;
        });
    }

    protected override void PlayerVirtual_OnTitleIdChanged(int titleId, string nameExt)
    {
        DataMgr.Instance.UserData.SetTitleExt(titleId, nameExt);
        base.PlayerVirtual_OnTitleIdChanged(titleId, DataMgr.Instance.UserData.TitleNameExt);
    }

    /*
    public void CheckAllPlayerOwerShip(string playerUUID, int range)
    {
        TLBattleScene.Instance.FindBattleObjectAs<TLAIPlayer>(m =>
        {
            m.OnTeamMemberListChange += OnOwerShipPlayerTeamInfoChange;

            if (m.bindBehaviour.InfoBar == null)
                return false;

            if (playerUUID == null)
            {
                if (m.bindBehaviour.InfoBar != null)
                    m.bindBehaviour.InfoBar.ShowOwnership(false);
            }
            else
            {
                if (m.PlayerVirtual.HasMonsterOwnerShip(playerUUID, range))
                    m.bindBehaviour.InfoBar.ShowOwnership(true);
                else
                    m.bindBehaviour.InfoBar.ShowOwnership(false);
            }
            return false;
        });
    }

    private void OnOwerShipPlayerTeamInfoChange(TLAIPlayer player)
    {

    }
    */
    #region 自动战斗.

    private bool IsOutOfGuardRange()
    {
        return CMath.getDistance(this.X, this.Y, guardPosX, guardPosY) > GuardMoveRange ? true : false; ;
    }

    private void SetGuardPos()
    {
        guardPosX = this.ZActor.X;
        guardPosY = this.ZActor.Y;
    }

    public override void OnCreate()
    {
        if (DontUseCache)
        {
            DisplayCell.DontUseCache = true;
            Vehicle.DontUseCache = true;
        }
        base.OnCreate();

    }

    private bool TryChangeAutoAttackState()
    {
        if (CurGuardStatus == AutoGuardStatus.StartBySkill)
        {
            ChangeAutoAttackState(null);
            return true;
        }

        return false;
    }

    public void BtnSetAutoGuard(bool flag)
    {
        if (GameGlobal.Instance.netMode && DataMgr.Instance.UserData.IsFuncOpen(AUTO_BATTLE_OPEN_TAG) == false)
        {
            return;
        }

        if (flag)
            ChangeAutoAttackState(null);
        else
        {
            this.CurGuardStatus = AutoGuardStatus.Stop;
            ChangeManualState(AutoAttackState.BTN_CLOSE);
        }


        SaveGuardSetting(flag);
    }

    /// <summary>
    /// 保存角色自动战斗设置.
    /// </summary>
    /// <param name="flag"></param>
    private void SaveGuardSetting(bool flag)
    {
        /*
        string accountid = DataMgr.Instance.UserData.AccountID;
        string scenetype = GetSceneType();
        int v = flag == true ? 1 : 0;
        string key = GetSaveKey(accountid, scenetype);
        PlayerPrefs.SetInt(key, v);
        PlayerPrefs.Save();
        */
    }

    /// <summary>
    /// 获取角色自动战斗设置.
    /// </summary>
    /// <returns></returns>
    private bool GetCurSceneGuardSetting()
    {
        var data = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
        if (data != null)
        {
            object obj = null;
            if (data.TryGetValue("auto_fight", out obj))
            {
                int v = Convert.ToInt32(obj);
                return (v == 1);
            }
        }

        return false;
    }

    private string GetSaveKey(string accountid, string sceneType)
    {
        return accountid + "_GuardSetting_" + sceneType; ;
    }

    private string GetSceneType()
    {
        var type = (BattleScene.SceneData.Properties as TLSceneProperties).CurSceneType;
        string scenetype = null;

        if (type == TLSceneProperties.SceneType.Dungeon)
            scenetype = type.ToString();
        else
            scenetype = TLSceneProperties.SceneType.None.ToString();

        return scenetype;
    }

    /// <summary>
    /// 向UI同步自动设置.
    /// </summary>
    [DoNotToLua]
    public void SyncAutoGuardState()
    {
        bool guard = GetCurSceneGuardSetting();

        //SceneAllowAutoGuard(ref guard);

        SyncUIBtnState(guard);

        if (guard)
        {
            ChangeAutoAttackState(null);
        }

    }

    public void SyncUIBtnState(bool guard)
    {
        //通知UI更新.
        Dictionary<string, object> args = new Dictionary<string, object>();
        args.Add("IsGuard", guard);
        EventManager.Fire(GameEvent.UI_HUD_SYNC_GUARD_STATE, args);
    }

    /// <summary>
    /// 场景是否允许自动战斗.
    /// </summary>
    /// <param name="guard"></param>
    private void SceneAllowAutoGuard(ref bool guard)
    {
        if (guard)
        {
            var data = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
            if (data != null)
            {
                object obj = null;
                if (data.TryGetValue("auto_fight", out obj))
                {
                    int v = Convert.ToInt32(obj);
                    if (v == 4 || v == 3)
                    {
                        guard = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 同步PK信息.
    /// </summary>
    private void SyncPKInfo()
    {
        if (!GameGlobal.Instance.netMode)
        {
            return;
        }
        //通知UI更新.
        Dictionary<string, object> args = new Dictionary<string, object>();
        args.Add("Mode", GetPKMode());
        EventManager.Fire(GameEvent.UI_HUD_SYNC_PK_MODE, args);
    }

    private void SyncSelectEffect()
    {
        if (Target != null)
        {
            Target.Refresh();
        }
    }

    public void SendUnitFocuseTarget(uint targetID)
    {
        (this.ZUnit as ZoneActor).SendUnitFocuseTarget(targetID);
    }

    #endregion

    #region PK模式.

    public PKInfo.PKMode GetPKMode()
    {
        var v = ZUnit.Virtual as TLClientVirtual_Player;
        return v.GetPKMode();
    }

    uint IComAIActor.ObjectID()
    {
        return this.ObjectID;
    }

    public void GetZonePlayersUUID(Action<GetZonePlayersUUIDResponse> action)
    {
        GetZonePlayersUUIDRequest req = new GetZonePlayersUUIDRequest();
        ZActor.SendRequest(req, (r) =>
        {
            var rsp = r.ResponseMessage as GetZonePlayersUUIDResponse;
            if (action != null)
            {
                action.Invoke(rsp);
            }
        }, null);
    }

    #endregion
}
