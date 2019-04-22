using DeepCore;
using DeepCore.GameData.Data;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using SLua;
using System;
using System.Collections;
using System.Collections.Generic;
using TLBattle.Client;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using UnityEngine;


public enum UnitHitEventState
{
    Normal = 0,//普通攻击.
    Crit = 1,//暴击.
    Dodge = 2,//闪避.
    Parry = 3,//招架.
    Cure = 4,//治疗
    DefEnergy = 99, //防御槽伤害
    ShieldDmg = 100,//护盾伤害
}

public partial class TLAIUnit : ComAIUnit, IUnit
{
    public delegate void OnShowSkillActionHandler(SkillActionStatus status, UnitActionData action);


    [DoNotToLua]
    public OnShowSkillActionHandler OnShowSkillAction;

    public delegate void OnPositionChangeHandler(Transform target);
    protected OnPositionChangeHandler mOnPositionChange;
    [DoNotToLua]
    public event OnPositionChangeHandler OnPositionChange { add { mOnPositionChange += value; } remove { mOnPositionChange -= value; } }
    //目标(chenjie)
    [DoNotToLua]
    public UnitTarget Target { get; set; }

    //这个状态同步比实际获得死亡状态要晚 so 改一下
    //    [DoNotToLua]
    //    public bool IsDead { get { return this.ZUnit.HP == 0 ? true : false; } protected set { } }
    [DoNotToLua]
    public bool IsDead
    {
        get;
        protected set;
    }


    [DoNotToLua]
    public TLAIBehaviour bindBehaviour;

    protected Transform mHeadTransform = null;
    public Transform HeadTransform { set { mHeadTransform = value; } get { return mHeadTransform; } }
    protected TLClientVirtual ClientVirtual { get { return ZUnit.Virtual != null ? ZUnit.Virtual as TLClientVirtual : null; } }

    public RenderVehicle Vehicle;
    public SkillTemplate SkillT { get; private set; }

    private float mFaceto;
    private float mOrginFaceto;
    public float FaceToDirect { get { return mFaceto; } set { mFaceto = value; } }
    public bool StopFaceToDirect { get; set; }
    private DynamicBone[] mDynamicBonelist;
    private int disappeartime = 1200;//渐隐消失

    private HashMap<int, PlayerInfoHud.ClientBuffInfo> mBuffInfoList = null;
    public HashMap<int, PlayerInfoHud.ClientBuffInfo> BuffInfoList { get { return mBuffInfoList; } }

    public bool ShowModel { get; protected set; }

    public bool IsPlayIdleSpeicalAnim { get; set; }
    public string lastAnimName;
    public float SpeicalAnimTime;
    public bool DynamicBoneEnable
    {
        get
        {
            if (mDynamicBonelist != null && mDynamicBonelist.Length > 0)
            {
                return mDynamicBonelist[0].enabled;
            }
            return false;
        }
        set
        {

            if (mDynamicBonelist != null && mDynamicBonelist.Length > 0)
            {
                foreach (var mDynamicBone in mDynamicBonelist)
                {
                    mDynamicBone.enabled = value;
                }
            }

        }
    }
    public Action<Vector3> OnPositonChange
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    protected new TLBattleScene BattleScene { get { return base.BattleScene as TLBattleScene; } }

    [DoNotToLua]
    public TLAIUnit(BattleScene battleScene, ZoneUnit obj)
                : base(battleScene, obj)
    {
        this.ObjectRoot.name += ClientVirtual != null ? ClientVirtual.GetName() : "";
        this.Target = new UnitTarget(this.ObjectID);
        this.IsPlayIdleSpeicalAnim = false;
        bindBehaviour = TLAIBehaviour.Bind<TLAIBehaviour>(this, this.ZObj);

        if (ClientVirtual != null)
        {
            ClientVirtual.OnCombatStateChangeHandle += OnCombatStateChangeEvent;
            RefreshCombatState(ClientVirtual.GetBattleStatus());
        }
        if (this.ZUnit.Info.BaseSkillID != null)
        {
            this.SkillT = Templates.GetSkill(this.ZUnit.Info.BaseSkillID.SkillID);
        }
        this.mFaceto = this.ZObj.Direction;
        ShowModel = true;


        Vehicle = new RenderVehicle(DisplayRoot, this.DisplayCell);
        this.animPlayer.AddAnimator(this.Vehicle);
    }

    public virtual bool IsActor()
    {
        return false;
    }

    protected virtual bool GetShadowCaster()
    {
        return false;
    }
    protected override void OnLoadModelFinish(FuckAssetObject aoe)
    {
        if (aoe)
        {
            base.OnLoadModelFinish(aoe);
            InitShadowCaster(aoe.gameObject, GetShadowCaster());
            this.mDynamicBonelist = aoe.gameObject.GetComponentsInChildren<DynamicBone>(true);
            DynamicBoneEnable = false;
            var soundobj = aoe.gameObject.GetComponent<DummyPlaySound>();
            if (soundobj == null)
            {
                soundobj = aoe.gameObject.AddComponent<DummyPlaySound>();
            }

            mHeadTransform = GetDummyNode("Head_Name").transform;
            bindBehaviour.Init();
            bindBehaviour.ShowHpBar(IsShowHPBar());
        }

    }

    public virtual void AddBubbleChat(string msg, string talkType, float time)
    {
        if (!IsDisposed)
        {
            //mBubbleNode = GetDummyNode("Head_Name");
            //BubbleChatU bc = BubbleChatU.Add(msg, mBubbleNode != null ? mBubbleNode.transform : this.DisplayCell.ObjectRoot.transform, Vector3.zero, time / 1000f);

        }
    }

    public virtual string Name()
    {
        if (this.ClientVirtual != null)
        {
            return this.ClientVirtual.GetName();
        }
        Debug.LogError("public string Name() this.ClientVirtual == null");
        return "";
    }

    protected override void OnDispose()
    {
        //BubbleChatU.StopBubbleChat(mBubbleNode != null ? mBubbleNode.transform : this.DisplayCell.ObjectRoot.transform);
        mOnPositionChange = null;

        if (bindBehaviour)
        {
            bindBehaviour.Dispose();
        }
        if (this.Vehicle != null)
        {
            this.Vehicle.Dispose();
            this.Vehicle = null;
        }

        if (ClientVirtual != null)
        {
            ClientVirtual.OnCombatStateChangeHandle -= OnCombatStateChangeEvent;
            RefreshCombatState(ClientVirtual.GetBattleStatus());
        }

        if (Target != null) //清除目标关系链 
        {
            Target.Destroy();
        }
        base.OnDispose();


    }

    private void BuffChange(ZoneUnit.BuffState buff, PlayerInfoHud.BuffReasonType reason)
    {
        var prop = buff.Data.Properties as TLBuffProperties;
        if (prop.IsShowLabel)
        {
            if (mBuffInfoList == null)
            {
                mBuffInfoList = new HashMap<int, PlayerInfoHud.ClientBuffInfo>();
            }
            PlayerInfoHud.ClientBuffInfo cb = new PlayerInfoHud.ClientBuffInfo();
            cb.Data = buff.Data;
            cb.id = buff.Data.TemplateID;
            cb.PassTime = (int)(buff.CDPercent * buff.Data.LifeTimeMS);
            cb.TotalTime = buff.Data.LifeTimeMS;
            cb.curDateTime = DateTime.Now;
            if (reason != PlayerInfoHud.BuffReasonType.Removed)
            {
                var _info = mBuffInfoList.GetOrAdd(cb.id, (id) =>
                {
                    return cb;
                });
                _info = cb.Clone();
            }
            else
            {
                mBuffInfoList.RemoveByKey(cb.id);
            }
        }

    }

    protected override void ZUnit_OnBuffRemoved(ZoneUnit unit, ZoneUnit.BuffState buff)
    {

        BuffChange(buff, PlayerInfoHud.BuffReasonType.Removed);
        base.ZUnit_OnBuffRemoved(unit, buff);
    }

    protected override void ZUnit_OnBuffChanged(ZoneUnit unit, ZoneUnit.BuffState buff)
    {
        BuffChange(buff, PlayerInfoHud.BuffReasonType.Change);
        base.ZUnit_OnBuffChanged(unit, buff);
    }

    protected override void ZUnit_OnBuffAdded(ZoneUnit unit, ZoneUnit.BuffState buff)
    {
        BuffChange(buff, PlayerInfoHud.BuffReasonType.Add);
        base.ZUnit_OnBuffAdded(unit, buff);
    }

    protected override void OnBeginUpdate(float deltaTime)
    {
        base.OnBeginUpdate(deltaTime);
        if (mBuffInfoList != null)
        {
            foreach (var bi in mBuffInfoList)
            {
                if (bi.Value != null)
                {
                    var time = bi.Value.PassTime + (DateTime.Now - bi.Value.curDateTime).Milliseconds;
                    time = Math.Min(time, bi.Value.TotalTime);
                    bi.Value.PassTime = time;
                    bi.Value.curDateTime = DateTime.Now;
                }

            }
        }


    }

    protected override void RegistAllObjectEvent()
    {
        base.RegistAllObjectEvent();
        RegistObjectEvent<BattleAtkNumberEventB2C>(ObjectEvent_BattleAtkNumberEvent);
        RegistObjectEvent<BattleSplitHitEventB2C>(ObjectEvent_BattleSplitHitEvent);
        RegistObjectEvent<UnitDynmicEffectB2C>(ObjectEvent_UnitDynmicEffectB2C);
        RegistObjectEvent<UnitRebirthEvent>(ObjectEvent_UnitRebirthEvent);
    }

    private void ObjectEvent_UnitRebirthEvent(UnitRebirthEvent obj)
    {
        var renderUnit = this.DisplayCell as RenderUnit;
        if (renderUnit != null)
        {
            renderUnit.InitMatManager();
        }

        IsDead = false;
    }

    private void ObjectEvent_UnitDynmicEffectB2C(UnitDynmicEffectB2C ev)
    {
        this.ObjectRoot.ZoneRot2UnityRot(ZObj.Direction);
        PlayEffect(ev.Effect, EffectRoot.Position(), EffectRoot.Rotation());
    }


    protected override void ObjectEvent_UnitHitEvent(UnitHitEvent ev)
    {
        base.ObjectEvent_UnitHitEvent(ev);

        OnHit(ev);
    }

    protected virtual void ObjectEvent_BattleAtkNumberEvent(BattleAtkNumberEventB2C ev)
    {
        if (ev.Value == 0)
        {
            ShowHintNumber(ev.Value, (byte)ev.Type, false);
        }
    }

    protected virtual void ObjectEvent_BattleSplitHitEvent(BattleSplitHitEventB2C ev)
    {
        RecordDamage(ev.sendID, ev.TotalDamage);

        if (ev.sendID != BattleScene.Actor.ObjectID && this.ObjectID != BattleScene.Actor.ObjectID)
            return;

        BattleNumberManager.Instance.ShowSplitHit(GetDummyNode("Chest_Buff").transform, ev.hitInfo, this is TLAIActor, (time) =>
        {
            if (this.IsDisposed)
                return;
            var renderUnit = this.DisplayCell as RenderUnit;
            renderUnit.MatHit(500);
            PlayHitEffect(ev.sendID, ev.effect);
        });
    }
    
    protected override void ObjectEvent_UnitDeadEvent(UnitDeadEvent ev)
    {
        if (this.ZUnit.Info.UType == UnitInfo.UnitType.TYPE_MONSTER)
        {
            //如果是核心本,则怪物死亡需要生成特效
            if (GameSceneMgr.Instance.IsHeXinBen)
            {
                GameSceneMgr.Instance.Count += 1;
                //编辑器坐标转unity坐标,再转屏幕坐标
                var deadPos = Extensions.ZonePos2UnityPos(this.ZUnit.Parent.TerrainSrc.TotalHeight, this.ZUnit.X, this.ZUnit.Y, this.ZUnit.Z);
                var p = Camera.main.WorldToScreenPoint(deadPos);
                CreateBounsEffect(p);
            }
        }
        IsDead = true;
    }
    public virtual void RemoveUnit()
    {
        if (this.ZUnit.Info.UType == UnitInfo.UnitType.TYPE_MONSTER && IsDead)
        {
            // float deadTime = this.ZUnit.Info.DeadTimeMS;
            this.DisposeDelayMS = (int)(disappeartime);
            //            this.fadeOutTime = 0;//deadTime / 1000;
            //            this.startFadeOut = true;
            doFadeOut();
        }
    }

    //核心本特效
    private void CreateBounsEffect(Vector3 startpos)
    {
        if (IsDisposed)
            return;

        Vector2 uipos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GameSceneMgr.Instance.UICamera.transform.parent.GetComponent<RectTransform>(),
            startpos,
            GameSceneMgr.Instance.UICamera,
            out uipos);

        var transSet = new TransformSet();
        transSet.Pos = uipos;
        transSet.LayerOrder = 1500;
        transSet.Layer = (int)PublicConst.LayerSetting.UI;
        transSet.Parent = HZUISystem.Instance.GetPickLayer().Transform;

        RenderSystem.Instance.LoadGameObject(GameSceneMgr.Instance.EffName, transSet, (aoe) =>
          {
              var flytobag = aoe.gameObject.GetComponent<FlyToBag>();
              if (flytobag)
              {
                  flytobag.Fly(FlyFinish);
              }
              else
              {
                  var fb = aoe.gameObject.AddComponent<FlyToBag>();
                  fb.Fly(FlyFinish);
              }
          });
    }

    private void FlyFinish(GameObject eff)
    {
        RenderSystem.Instance.Unload(eff);
        eff.SetActive(false);
        EventManager.Fire("Event.Effect.EffectFinish");
    }
    
    [DoNotToLua]
    protected override void ZUnit_OnActionChanged(ZoneUnit unit, UnitActionStatus status, object msg)
    {
        base.ZUnit_OnActionChanged(unit, status,msg);
    }

    protected override void SyncState()
    {
        base.SyncState();

        if (mOnPositionChange != null && mHeadTransform != null)
        {
            mOnPositionChange(mHeadTransform);
            mOnPositionChange = null;
        }

    }

    [DoNotToLua]
    public void FaceToOrgin()
    {
        if (this.ObjectRoot != null && this.ObjectRoot.gameObject != null)
        {
            mFaceto = mOrginFaceto;
            ChangeDirection(mOrginFaceto);
            this.SetDirection(mOrginFaceto, false);
        }

    }
    [DoNotToLua]
    public void FaceTo(ComAICell target)
    {
        mOrginFaceto = this.Direction;
        float angle = UnityEngine.Mathf.Atan2(target.Y - this.Y, target.X - this.X);
        ChangeDirection(angle);
    }
    [DoNotToLua]
    public void ChangeDirection(float d, bool IsSmooth = false)
    {
        //立即转向，摄像机跟随.
        mFaceto = d;
        this.SetDirection(d, IsSmooth);
        //Debug.LogError("direction=" + d + " , " + this.ZObj.Direction);
        //this.ObjectRoot.ZoneRot2UnityRot(mFaceto);

    }
    [DoNotToLua]
    public void ChangeCameraWithDirection(float d, bool IsSmooth = false)
    {
        ChangeDirection(d, IsSmooth);
        BattleFactory.Instance.Camera.RotateWithActorDirection(this.ObjectRoot.Forward());
    }

    private void OnCombatStateChangeEvent(TLClientVirtual clientVirtual, CombatStateChangeEventB2C.BattleStatus status)
    {
        OnCombatStateChange(status);
    }

    [DoNotToLua]
    public virtual void ShowSkillAction(SkillActionStatus status, UnitActionData action)
    {
        if (OnShowSkillAction != null)
        {
            OnShowSkillAction(status, action);
        }
    }

    [DoNotToLua]
    public virtual void BattleReady()
    {
        bindBehaviour.enabled = true;
    }

    [DoNotToLua]
    public virtual void OnHit(UnitHitEvent me)
    {
        RecordDamage(me.AttackerID, me.hp);

        if (me.AttackerID != BattleScene.Actor.ObjectID && this.ObjectID != BattleScene.Actor.ObjectID)
            return;

        if (BattleNumberManager.Instance)
        {
            bool isBuff = false;
            if (me.HasSourceAttack && me.SourceAttack != null)
            {
                if (me.SourceAttack.Buff != null)
                {
                    isBuff = true;
                }
            }
            ShowHintNumber(me.hp, me.client_state, isBuff);
        }

        if (me.hp != 0)
        {
            //TODO 受击效果 

            //if (this.ZUnit.Info.UType == this.ZUnit.Info.UnitType.TYPE_MONSTER)
            //if ((int)this.ZUnit.Info.UType == 3)
            {
                var renderUnit = this.DisplayCell as RenderUnit;
                renderUnit.MatHit(500);
            }

            CheckShowHPBanner(false);
        }
    }

    protected virtual bool IsShowHPBar()
    {
        bool ret = false;

        if (!IsDisposed)
        {
            var p = this.ZUnit.Info.Properties as TLUnitProperties;
            if (p.UnitDisplayConfig != null)
            {
                switch (p.UnitDisplayConfig.HPBannerCfg)
                {
                    case TLUnitDisplayConfig.HPBannerStatus.ShowInCombat:
                        if (ClientVirtual != null)
                            return (ClientVirtual.GetBattleStatus() != CombatStateChangeEventB2C.BattleStatus.None);
                        return false;
                    case TLUnitDisplayConfig.HPBannerStatus.Hide:
                        return false;
                    case TLUnitDisplayConfig.HPBannerStatus.Always:
                        return true;
                }
            }
        }

        return ret;
    }

    private void ShowHintNumber(float number, byte state, bool isBuff)
    {
        if (Camera.main == null)
            return;

        if (BattleNumberManager.Instance)
        {
            if (state == (byte)BattleAtkNumberEventB2C.AtkNumberType.Normal || state == (byte)BattleAtkNumberEventB2C.AtkNumberType.Crit || state == (byte)BattleAtkNumberEventB2C.AtkNumberType.Block)
            {
                BattleNumberManager.Instance.ShowHintNum(GetDummyNode("Chest_Buff").transform, number, state, isBuff, this is TLAIActor);
            }
            else //纯文字
            {
                BattleNumberManager.Instance.ShowState(GetDummyNode("Chest_Buff").transform, state, this is TLAIActor);
            }
        }
    }

    [DoNotToLua]
    public uint GetCurrentTarget()
    {
        return ZUnit.CurrentTarget;
    }

    public int Level()
    {
        if (this.ClientVirtual != null)
        {
            return this.ClientVirtual.GetLv();
        }
        Debugger.LogError(" public int Level() this.ClientVirtual == null");
        return 0;
    }

    //   private bool startFadeOut = false;

    //    private float fadeOutTime = 0;

    private void UpdateFadeOut(float deltaTime)
    {
        //        if (startFadeOut)
        //        {
        //            fadeOutTime -= deltaTime;
        //            if (fadeOutTime <= 0)
        //            {
        //                doFadeOut();
        //                startFadeOut = false;
        //            }
        //        }
    }

    private void doFadeOut()
    {
        var renderUnit = this.DisplayCell as RenderUnit;
        //应小石的要求，死亡渐隐效果 写死的1200 
        renderUnit.AddMatState(StateMaterial.DISSOLOVE, disappeartime);
    }

    public void CheckShowHPBanner(bool isCheckType)
    {
        if (bindBehaviour != null && bindBehaviour.HasInit)
        {
            bindBehaviour.ShowHpBar(IsShowHPBar());
            if (isCheckType)
                bindBehaviour.CheckHpBarType();
        }

    }

    #region Debuger相关.
    //预警
    public override void UpdateDebugGuard()
    {
        if (this.ObjectRoot != null)
        {
            GLGizmos.DrawSphere(this.ObjectRoot.transform.position,
                                this.ZUnit.Info.GuardRange,
                                Color.yellow);
        }
    }
    public override void UpdateDebugBody()
    {
        if (this.ObjectRoot != null)
        {
            GLGizmos.DrawSphere(this.ObjectRoot.transform.position,
                                this.ZUnit.Info.BodySize,
                                Color.white);
        }
    }
    //预警
    public override void UpdateDebugAttack()
    {
        if (this.ObjectRoot != null && SkillT != null)
        {

            GLGizmos.DrawArc(this.ObjectRoot.transform,
                             ZUnit.GetSkillAttackRange(SkillT),
                             SkillT.AttackAngle * Mathf.Rad2Deg,
                             Color.yellow);
        }
    }

    public void PlayIdleSpeicalAnimation(string name)
    {
        if (!IsPlayIdleSpeicalAnim || !string.Equals(name, lastAnimName))
        {
            lastAnimName = name;
            IsPlayIdleSpeicalAnim = true;
            RegistAction(this.CurrentState, new TLSpeicalActionStatus(this.CurrentState, "sp_" + name, name, true, false, 1, () => { IsPlayIdleSpeicalAnim = false; }));
            ChangeAction(this.CurrentState);
        }
    }

    public void PlaySpeicalAnimationByScript(ActionStatus action)
    {
        this.SetLockActionStatus(action);
    }
    public bool PlayAnim(string name, bool crossFade, WrapMode wrapMode = WrapMode.Once, float speed = 1)
    {
        this.animPlayer.speed = speed;
        if (crossFade)
        {
            this.animPlayer.CrossFade(name, 0.15f, -1, 0f);
        }
        else
        {
            this.animPlayer.Play(name);
        }
        return true;
    }

    public Vector3 EulerAngles()
    {
        return this.ObjectRoot.transform.eulerAngles;
    }

    public Vector3 TransformDirection(Vector3 v)
    {
        return this.ObjectRoot.transform.TransformDirection(v);
    }

    //public void ITweenMoveTo(Hashtable args)
    //{
    //    iTween.MoveTo(ObjectRoot, args);
    //}

    //public void iTweenRotateTo(Hashtable args)
    //{
    //    iTween.RotateTo(ObjectRoot, args);
    //}

    public void AddBubbleTalkInfo(string content, string TalkActionType, int keepTimeMS)
    {

    }
    [DoNotToLua]
    public int GetAnimLength(string name)
    {
        if (animPlayer == null)
        {
            return 0;
        }
        return (int)(this.animPlayer.GetAnimTime(name) * 1000);

    }
    #endregion

    #region Avatar扩展.

    protected override void OnLoadModel()
    {
        var p = this.Info.Properties as TLUnitProperties;

        //优先读avatar特殊配置，没有则默认.
        if (p.ServerData != null && p.ServerData.AvatarList != null)
        {
            var aMap = GameUtil.ToAvatarMap(p.ServerData.AvatarList);

            string bodyFile = GameUtil.GetPartFile(aMap, (int)TLAvatarInfo.TLAvatar.Avatar_Body);

            if (string.IsNullOrEmpty(bodyFile))
            {
                base.OnLoadModel();
                return;
            }
            else
            {
                (DisplayCell as RenderUnit).ChangeBody(bodyFile, (aoe) =>
                {
                    if (aoe == null)
                    {
                        Debugger.LogWarning(bodyFile + "__TLAIUnit OnLoadModel ChangeBody this.IsDisposed:" + this.IsDisposed);
                        return;
                    }

                    if (this.IsDisposed)
                    {
                        return;
                    }

                    OnLoadModelFinish(aoe);
                    this.ChangeAction(ZUnit.CurrentState, true);

                    foreach (var item in aMap.Values)
                    {
                        string FileName = item.FileName;
                        if (item.PartTag != TLAvatarInfo.TLAvatar.Ride_Avatar01
                           && item.PartTag != TLAvatarInfo.TLAvatar.Avatar_Body
                           && !string.IsNullOrEmpty(FileName))
                        {
                            (DisplayCell as RenderUnit).ChangeAvatar(FileName, (int)item.PartTag, animPlayer, (succ) =>
                            {
                                if (this.IsDisposed)
                                {
                                    return;
                                }
                                if (succ && item.PartTag != TLAvatarInfo.TLAvatar.Foot_Buff)
                                {
                                    InitShadowCaster(DisplayCell.ObjectRoot, false);
                                    this.ChangeAction(ZUnit.CurrentState, true);
                                }
                            });
                        }
                    }
                });
            }
        }
        else
        {
            base.OnLoadModel();
        }

    }

    protected virtual void InitShadowCaster(GameObject obj, bool toSelfLayer)
    {
        GameUtil.ReplaceLayer(obj, (int)PublicConst.LayerSetting.SelfLayer, (int)PublicConst.LayerSetting.CharacterUnlit);
    }

    #endregion

    #region 伤害记录

    protected void RecordDamage(uint objID, int damage)
    {
        if (!BattleScene.RecordDamgeEnable())
            return;

        var obj = BattleScene.GetBattleObject(objID);
        if (obj == null) return;
        if (obj is TLAIPlayer || obj is TLAIActor)//记录玩家的伤害
        {
            BattleScene.RecordPlayerDamage(objID, damage);
        }
    }

    #endregion
}