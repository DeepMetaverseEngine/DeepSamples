
using Assets.Scripts;
using DeepCore;
using DeepCore.GameData.Data;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using DeepCore.GameSlave.Agent;
using DeepCore.Unity3D.Battle;
using SLua;
using System;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using TLBattle.Plugins;
using TLProtocol.Protocol.Client;
using UnityEngine;

public partial class TLAIActor
{
    //移动监听.
    private Action<float, float> mUnitAxisAngleHandler;
    private event Action<float, float> OnUnitAxisAngleHandler
    {
        add { mUnitAxisAngleHandler += value; }
        remove { mUnitAxisAngleHandler -= value; }
    }

    //移动停止监听.
    private System.Action mUnitStopMoveHandler;
    private event System.Action OnUnitStopMoveHandler
    {
        add { mUnitStopMoveHandler += value; }
        remove { mUnitStopMoveHandler -= value; }
    }

    //技能按键.
    private Func<int, bool, uint> mLaunchSkillByIndexHandler;
    private event Func<int, bool, uint> OnLaunchSkillByIndexHandler
    {
        add { mLaunchSkillByIndexHandler += value; }
        remove { mLaunchSkillByIndexHandler -= value; }
    }

    //技能遥感.
    private Action<float, float, float, float> mSkillRockerHandler;
    private event Action<float, float, float, float> OnSkillRockerMoveHandler;
    private event Action<float, float, float, float> OnSkillRockerStopHandler;

    private GState mCurGState = null;
    [DoNotToLua]
    public GState CurGState
    {
        get
        {
            return mCurGState;
        }
    }

    private ManualOperateState mManualOperate = null;
    private AutoAttackState mAutoAttackState = null;


    private ManualOperateState GetManualOperateState()
    {
        if (mManualOperate == null)
            mManualOperate = new ManualOperateState(this);

        return mManualOperate;
    }

    private AutoAttackState GetAutoAttackState()
    {
        if (mAutoAttackState == null)
            mAutoAttackState = new AutoAttackState(this);

        return mAutoAttackState;
    }

    [DoNotToLua]
    public bool ChangeActorState(GState s, string reason = null)
    {

        bool isblock = true;
        if (mCurGState != null)
        {
            isblock = mCurGState.Block(s, reason);
            if (isblock == true)
            {
                mCurGState.Leave();
                if (TLUnityDebug.DEBUG_MODE)
                {
                    //Debugger.LogWarning("mCurGState" + mCurGState.GetType().FullName + " nextState=" + s.GetType().FullName);
                }

            }
            else
            {
                if (TLUnityDebug.DEBUG_MODE)
                {
                    //Debugger.LogWarning("blockfalsemCurGState" + mCurGState.GetType().FullName + " nextState=" + s.GetType().FullName);
                }

            }
        }
        else
        {
            if (TLUnityDebug.DEBUG_MODE)
            {
                //Debugger.LogWarning("blockfalsemCurGState mCurGState = null nextState=" + s.GetType().FullName);
            }
        }



        if (isblock == true && s != null)
        {
            mCurGState = s;
            mCurGState.Enter();
        }

        return isblock;
    }

    private void UpdateActorState(float d)
    {
        if (mCurGState != null)
        {
            mCurGState.Update(d);
        }
    }

    private void TryChangeManualOperateState(string reason = null)
    {
        if (CurGState is ManualOperateState) return;
        ChangeManualState(reason);
    }

    private void ChangeManualState(string reason = null)
    {
        ChangeActorState(GetManualOperateState(), reason);
    }

    public void ChangeAutoAttackState(string order)
    {
        if (mCurGState is AutoAttackState) return;
        var state = GetAutoAttackState();
        state.SetOrder(order);
        ChangeActorState(state);
    }

    [DoNotToLua]
    public abstract class GState
    {
        protected TLAIActor unit = null;
        private bool isDispose = false;

        public bool IsLeaved
        {
            get { return isDispose; }
        }
        public GState(TLAIActor unit)
        {
            this.unit = unit;
        }
        internal void Enter() { isDispose = false; OnEnter(); }
        internal void Leave() { isDispose = true; OnLeave(); }
        internal void Update(float delta) { if (isDispose == false) OnUpdate(delta); }
        internal bool Block(GState newstate, string reason = null) { return OnBlock(newstate, reason); }

        protected abstract bool OnBlock(GState newstate, string reason = null);
        protected abstract void OnEnter();
        protected abstract void OnLeave();
        protected abstract void OnUpdate(float delta);
    }

    /// <summary>
    /// 闲置状态.
    /// </summary>
    [DoNotToLua]
    public class IdleState : GState
    {
        public IdleState(TLAIActor unit) : base(unit)
        {
        }

        protected override bool OnBlock(GState newstate, string reason)
        {
            if (newstate is IdleState) { return false; }
            return true;
        }

        protected override void OnEnter()
        {

        }

        protected override void OnLeave()
        {

        }

        protected override void OnUpdate(float delta)
        {

        }
    }

    /// <summary>
    /// 移动到攻击范围攻击目标.
    /// </summary>
    [DoNotToLua]
    public class MoveAndAttackState : GState
    {
        private int skillID;
        private TLAIUnit targetUnit;
        private float attackRange;
        private bool finish = false;
        private bool isNormalAtk = false;
        public MoveAndAttackState(TLAIActor unit, int skillID, TLAIUnit targetUnit, float attackRange) : base(unit)
        {
            this.skillID = skillID;
            this.targetUnit = targetUnit;
            this.attackRange = attackRange;
            this.isNormalAtk = unit.GetSkillIDByIndex(0) == skillID ? true : false;
        }

        protected override bool OnBlock(GState newstate, string reason)
        {
            if (newstate is MoveAndAttackState)
            {
                //同一个目标不打断.
                MoveAndAttackState ss = newstate as MoveAndAttackState;
                if (ss.targetUnit.ObjectID == this.targetUnit.ObjectID) { return false; }
            }

            return true;
        }

        protected override void OnEnter()
        {
            if (targetUnit == null) { unit.ChangeManualState(); }
            else
                unit.Target.SetTarget(targetUnit.ObjectID);
        }

        protected override void OnLeave()
        {

        }

        protected override void OnUpdate(float delta)
        {
            if (finish) { return; }

            if (targetUnit != null && targetUnit.IsDead == true)
            {
                finish = true;
                unit.SendUnitStopMove();
            }

            if (IsInSkillRange(targetUnit))
            {
                unit.SendUnitStopMove();
                finish = true;
            }
            if (targetUnit != null)
            {
                float mx = targetUnit.X - unit.X;
                float my = targetUnit.Y - unit.Y;
                unit.SendUnitAxisAngle(mx, my);
            }


            if (finish == true)
            {
                var s = unit.GetManualOperateState();
                s.SetAtkData(targetUnit.ObjectID, skillID);
                unit.ChangeManualState();
            }
        }

        private bool IsInSkillRange(ComAIUnit target)
        {
            if (unit == null || unit.ZObj == null)
            {
                return false;
            }

            return unit.IsInAtkRange(target, attackRange);
        }
    }

    /// <summary>
    /// 手动战斗（玩家自己施放技能）
    /// </summary>
    [DoNotToLua]
    public class ManualOperateState : GState
    {
        private int NormalAtkSkillID = 0;
        private bool AutoNormalAtk = false;
        private TLAIUnit LastNormalAtkTarget = null;
        private uint AtkTargetObjeID;
        private int AtkSkillID;
        /// <summary>
        /// 自动自动战斗定制器.
        /// </summary>
        private TimeExpire<int> autoGuardTimer;

        public ManualOperateState(TLAIActor unit) : base(unit)
        {
            var ss = unit.ZUnit.GetSkillState(this.unit.GetSkillIDByIndex(0));
            if (ss != null)
            {
                NormalAtkSkillID = ss.Data.ID;
            }
        }

        protected override bool OnBlock(GState newstate, string reason)
        {
            if (newstate is ManualOperateState)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void OnEnter()
        {
            EventManager.Fire("Event.Quest.StopQuestAutoRun", EventManager.defaultParam);
            unit.OnUnitAxisAngleHandler += Unit_OnUnitAxisAngleHandler;
            unit.OnUnitStopMoveHandler += Unit_OnUnitStopMoveHandler;
            unit.ZActor.OnLaunchSkill += ZActor_OnLaunchSkill;
            CheckAtkData();
        }

        protected override void OnUpdate(float delta)
        {
            UpdateAutoGuard(delta);

            UpdateNormalAtk(delta);
        }

        protected override void OnLeave()
        {
            CleanAutoNormalAtkArgs();
            unit.OnUnitAxisAngleHandler -= Unit_OnUnitAxisAngleHandler;
            unit.OnUnitStopMoveHandler -= Unit_OnUnitStopMoveHandler;
            unit.ZActor.OnLaunchSkill -= ZActor_OnLaunchSkill;

        }

        private void Unit_OnSkillRockerStopHandler(float arg1, float arg2, float arg3, float arg4)
        {
            unit.DoSkillRockerStop(arg1, arg2, arg3, arg4);
        }

        private void Unit_OnSkillRockerMoveHandler(float arg1, float arg2, float arg3, float arg4)
        {
            unit.DoSkillRockerMove(arg1, arg2, arg3, arg4);
        }

        private uint Unit_OnLaunchSkillByIndexHandler(int arg1, bool arg2)
        {
            int skillId = unit.GetSkillIDByIndex(arg1);
            return unit.ReadyToLaunchSkillById(skillId, arg2);
        }

        private void Unit_OnUnitStopMoveHandler()
        {
            unit.SendUnitStopMove();
        }

        private void Unit_OnUnitAxisAngleHandler(float arg1, float arg2)
        {
            unit.SendUnitAxisAngle(arg1, arg2);

            //自动普攻状态下，如果移动则取消自动普攻.
            if (unit.ZUnit.CurrentSkillAction != null && unit.ZUnit.CurrentSkillAction.IsControlMoveable == false)
                AutoNormalAtk = false;
        }

        private void ZActor_OnLaunchSkill(ZoneUnit unit, DeepCore.GameSlave.ZoneUnit.SkillState skill, UnitLaunchSkillEvent evt)
        {
            var p = skill.Data.Properties as TLSkillProperties;
            bool normalSkill = false;
            if (unit.Info.BaseSkillID != null)
            {
                if (unit.Info.BaseSkillID.SkillID == skill.Data.ID)
                {
                    normalSkill = true;
                }
            }
            if (normalSkill || skill.Data.ID == NormalAtkSkillID || p.SkillType == GameSkill.TLSkillType.normalAtk)
            {
                AutoNormalAtk = true;

                if (LastNormalAtkTarget == null || LastNormalAtkTarget.ObjectID != evt.target_object_id)
                {
                    LastNormalAtkTarget = this.unit.BattleScene.GetBattleObject(evt.target_object_id) as TLAIUnit;
                }

                if (LastNormalAtkTarget == null || LastNormalAtkTarget.IsDead)
                {
                    CleanAutoNormalAtkArgs();
                }
            }
            else
            {
                CleanAutoNormalAtkArgs();
            }
        }

        private void CleanAutoNormalAtkArgs()
        {
            AutoNormalAtk = false;
            LastNormalAtkTarget = null;
        }

        public void SetAtkData(uint targetObjID, int SkillID)
        {
            AtkTargetObjeID = targetObjID;
            AtkSkillID = SkillID;
        }

        private void CheckAtkData()
        {
            if (AtkTargetObjeID != 0 && AtkSkillID != 0)
            {
                unit.LaunchSkillWithTarget(AtkSkillID, AtkTargetObjeID);
                AtkTargetObjeID = 0;
                AtkSkillID = 0;
            }
        }

        private void UpdateAutoGuard(float deltaTime)
        {
            if (unit.CurGuardStatus == AutoGuardStatus.StartByMoveStop)
            {
                if (unit.CurrentState == UnitActionStatus.Idle)
                {
                    if (autoGuardTimer != null && autoGuardTimer.Update((int)(deltaTime * 1000)))
                    {
                        unit.ChangeAutoAttackState(null);
                    }
                }
                else
                {
                    InitGuardTimer();
                }
            }
            #region 镇魔曲版自动战斗.
            /*
            if (unit.curGuardStatus == AutoGuardStatus.StartByMoveStop)
            {
                if (unit.CurrentState == UnitActionStatus.Idle)
                {
                    if (autoGuardTimer != null && autoGuardTimer.Update((int)(deltaTime * 1000)))
                    {
                        unit.SetAutoGuard(AutoGuardStatus.Start);
                    }
                }
                else
                {
                    InitGuardTimer();
                }
                if (unit.IsOutOfGuardRange())
                {
                    unit.SetAutoGuard(AutoGuardStatus.StartBySkill);
                }
                else
                {
                    if (unit.CurrentState == UnitActionStatus.Idle)
                    {
                        if (autoGuardTimer != null && autoGuardTimer.Update((int)(deltaTime * 1000)))
                        {
                            unit.SetAutoGuard(AutoGuardStatus.Start);
                        }
                    }
                    else
                    {
                        InitGuardTimer();
                    }
                }
                
            }
            else if (unit.curGuardStatus == AutoGuardStatus.StartBySkill)
            {
                var skillaction = unit.ZUnit.CurrentSkillAction;
                if (skillaction != null)
                {
                    if (skillaction.IsCancelableByMove || skillaction.IsCancelableBySkill || skillaction.IsDone)
                    {
                        unit.TryChangeAutoAttackState();
                        return;
                    }
                }
            }
            */
            #endregion
        }

        private void InitGuardTimer()
        {
            if (autoGuardTimer == null)
                autoGuardTimer = new TimeExpire<int>(500);
            else
                autoGuardTimer.Reset();
        }

        private void UpdateNormalAtk(float deltaTime)
        {
            if (AutoNormalAtk && unit.CurGuardStatus == AutoGuardStatus.Stop)
            {
                var skillaction = unit.ZUnit.CurrentSkillAction;

                //切换目标后，停止自动战斗.
                if (unit.Target.TargetId != LastNormalAtkTarget.ObjectID)
                {
                    CleanAutoNormalAtkArgs();
                    return;
                }

                if (skillaction != null && skillaction.SkillData.ID == NormalAtkSkillID)
                {
                    if (skillaction.IsCancelableByMove || skillaction.IsCancelableBySkill)
                    {
                        LaunchNormalAtk(skillaction.SkillData.AttackRange);
                    }
                }
                else if (skillaction == null)
                {
                    var st = unit.ZUnit.GetSkillState(NormalAtkSkillID);
                    if (st != null)
                    {
                        LaunchNormalAtk(st.Data.AttackAngle);
                    }
                }
            }
        }

        private void LaunchNormalAtk(float atkRange)
        {
            if (LastNormalAtkTarget.IsDead == false)
            {

                //判断距离.
                if (unit.IsInAtkRange(LastNormalAtkTarget, atkRange))
                {
                    this.unit.LaunchSkillWithTarget(NormalAtkSkillID, LastNormalAtkTarget.ObjectID);
                }
                else
                {
                    this.unit.ChangeActorState(new MoveAndAttackState(unit, NormalAtkSkillID, LastNormalAtkTarget, atkRange));
                }
            }
            else
            {
                CleanAutoNormalAtkArgs();
            }
        }

        public bool CheckAndLaunchSkill(ComAIUnit targetUnit, ZoneUnit.SkillState state)
        {
            if (unit.IsInAtkRange(targetUnit, state.Data.AttackRange))
            {
                unit.LaunchSkillWithTarget(state.Data.ID, targetUnit.ObjectID);
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 自动战斗.
    /// </summary>
    [DoNotToLua]
    public class AutoAttackState : GState
    {
        public const string BTN_CLOSE = "BTN_CLOSE";
        public string order = null;

        public AutoAttackState(TLAIActor unit) : base(unit)
        {

        }

        public void SetOrder(string order)
        {
            this.order = order;
        }

        protected override bool OnBlock(GState newstate, string reason)
        {
            bool ret = true;

            if (string.IsNullOrEmpty(reason))
            {
                bool isMapMove = false;
                if (newstate is AutoRunState)
                {
                    var moveendaction = newstate as AutoRunState;

                    if (moveendaction.action is MapTouchMoveAction)
                    {
                        isMapMove = true;
                    }
                }

                if (newstate is ManualOperateState ||
                    isMapMove ||
                    newstate is MoveAndAttackState)
                {
                    if (!GameGlobal.Instance.netMode || DataMgr.Instance.UserData.IsFuncOpen(AUTO_BATTLE_OPEN_TAG))
                    {
                        unit.CurGuardStatus = AutoGuardStatus.StartByMoveStop;
                    }
                }

            }
            //Debugger.LogWarning("AutoAttackStateblock " + newstate.GetType().FullName);
            return ret;
        }

        protected override void OnEnter()
        {
            if (GameGlobal.Instance.netMode && DataMgr.Instance.UserData.IsFuncOpen(AUTO_BATTLE_OPEN_TAG) == false)
            {
                return;
            }

            //UI特效.
            this.unit.CurGuardStatus = AutoGuardStatus.Start;
            this.unit.ZActor.SendUnitStopMove();
            this.unit.ZActor.SendUnitGuard(true, order);

        }

        protected override void OnLeave()
        {
            this.unit.ZActor.SendUnitGuard(false);

            if (unit.curGuardStatus != AutoGuardStatus.StartByMoveStop)
            {
                //怎么判断不同步按钮状态.
                this.unit.CurGuardStatus = AutoGuardStatus.Stop;
            }

        }

        protected override void OnUpdate(float delta)
        {

        }
    }

    [DoNotToLua]
    public class PreparePickState : GState
    {
        private float mPickTimeMS;

        private ZoneItem mZoneItem = null;
        private TLItemProperties itemProperties = null;
        private int itemTemplateId;

        public PreparePickState(TLAIActor unit, ZoneItem zoneItem) : base(unit)
        {
            this.mZoneItem = zoneItem;
            this.itemProperties = mZoneItem.Info.Properties as TLItemProperties;
            this.itemTemplateId = mZoneItem.Info.TemplateID;

            if (!itemProperties.HandPick && itemProperties.PreTimes > 0)
            {
                this.mPickTimeMS = (float)(this.itemProperties.PreTimes * 0.001);
            }
            else
            {
                this.mPickTimeMS = -1;
            }
        }

        protected override bool OnBlock(GState newstate, string reason)
        {
            return true;
        }

        protected override void OnEnter()
        {
            unit.UnMount();
            unit.SendUnitStopMove(false);
            //HudManager.Instance.SkillBar.Visible = false;


            if (itemProperties.HandPick)
            {
                this.ShowPrePick();
            }
            else if (this.mPickTimeMS > 0)
            {
                HudManager.Instance.Interactive.setAutoImag(this.itemProperties.PickIcon);
                HudManager.Instance.Interactive.ShowDialog(true, (int)this.mPickTimeMS);
            }
            else
            {
                this.PickItem();
            }

            HudManager.Instance.Interactive.OnPickBtnClick = PickItem;

        }

        private string GetTranslatedWaitPickBtnName(int templateId)
        {
            var map = GameUtil.GetDBData("itemtemplate", templateId);
            if (map == null)
            {
                return templateId + " not found";
            }
            var key = Convert.ToString(map["WaitPickBtnName"]);
            if(string.IsNullOrEmpty(key))
            {
                return HZLanguageManager.Instance.GetString(Convert.ToString(map["item_name"]));
            }
            return HZLanguageManager.Instance.GetString(key);           
        }

        private string GetTranslatedItemName(int templateId)
        {
            var map = GameUtil.GetDBData("itemtemplate", templateId);
            if (map == null)
            {
                return templateId + " not found";
            }
            var key = Convert.ToString(map["item_name"]);
            return HZLanguageManager.Instance.GetString(key);
        }

        private void ShowPrePick()
        {
            if (!GameGlobal.Instance.netMode)
                return;
            HudManager.Instance.Interactive.ShowHandPickButton(this.itemProperties.PickIcon);
            //var btn_name = GetTranslatedItemName(this.itemTemplateId);
            var btn_name = GetTranslatedWaitPickBtnName(this.itemTemplateId);
            if (!string.IsNullOrEmpty(btn_name))
            {
                HudManager.Instance.Interactive.ShowPick(btn_name);
            }
            else if (!string.IsNullOrEmpty(this.itemProperties.WaitPickBtnName))
            {
                HudManager.Instance.Interactive.ShowPick(this.itemProperties.WaitPickBtnName);
            }
        }

        public void PickItem()
        {
            //float angle = Mathf.Atan2(this.mZoneItem.Y - unit.ZActor.Y, unit.ZActor.X - unit.ZActor.X);
            ////client.Actor.SetDirection(angle);
            //unit.ZActor.SendUnitFaceTo(angle);

            unit.ZActor.SendUnitPickObject(this.mZoneItem.ObjectID);
        }


        protected override void OnLeave()
        {
            HudManager.Instance.Interactive.ShowDialog(false);
            HudManager.Instance.Interactive.OnPickBtnClick = null;
            //HudManager.Instance.SkillBar.Visible = true;
        }


        protected override void OnUpdate(float delta)
        {
            if (!itemProperties.HandPick && this.mPickTimeMS > 0)
            {
                mPickTimeMS -= delta;
                HudManager.Instance.Interactive.ShowDialog(true, (int)this.mPickTimeMS);
                if (mPickTimeMS <= 0)
                {
                    this.PickItem();
                    mPickTimeMS = -1;
                }
            }
        }
    }

    [DoNotToLua]
    public class StartPickState : GState
    {
        //拾取目标
        private TLAIItem target = null;
        private TLItemProperties zp = null;
        //拾取时间.
        private TimeExpire<UnitStartPickObjectEvent> mPickTime = null;

        public StartPickState(TLAIActor unit, TLAIItem target, TimeExpire<UnitStartPickObjectEvent> pickTime) : base(unit)
        {
            this.target = target;
            this.zp = target.GetItemProperties();
            if (zp != null && zp.ShowPickPercentGauge)
            {
                this.mPickTime = pickTime;
            }
        }

        protected override bool OnBlock(GState newstate, string reason)
        {
            Debugger.LogWarning("StartPickState block" + newstate.GetType().FullName);
            return true;
        }

        protected override void OnEnter()
        {
            unit.UnMount();
            unit.FaceTo(this.target);
            //HudManager.Instance.SkillBar.Visible = false;
            HudManager.Instance.Interactive.ShowDialog(false);


            var templateID = this.target.ZObj.TemplateID;
            var map = GameUtil.GetDBData("itemtemplate", templateID);
            if (map != null)
            {
                var PickBtnName = Convert.ToString(map["PickBtnName"]);
                var piackName = HZLanguageManager.Instance.GetString(PickBtnName);
                HudManager.Instance.Interactive.ShowPick(piackName);
            }
            else
            {
                HudManager.Instance.Interactive.ShowPick(zp.PickBtnName);
            }




            HudManager.Instance.Interactive.setAutoImag(zp.PickIcon);

            if (zp != null && !string.IsNullOrEmpty(zp.PickTriggerEvt) && zp.PickTriggerEvt.IndexOf("lua") != -1)
            {
                TLBattleScene.Instance.DoParseLuaEvent(zp.PickTriggerEvt);
            }
        }

        private void doEnd()
        {
            this.mPickTime = null;
            HudManager.Instance.Interactive.ShowGauge(false);
            HudManager.Instance.Interactive.SetPercent(0);
            //HudManager.Instance.SkillBar.Visible = true;

            if (zp != null && !string.IsNullOrEmpty(zp.PickTriggerEvt) && zp.PickTriggerEvt.IndexOf("lua") != -1)
            {
                TLBattleScene.Instance.StopLuaEventByMessage(zp.PickTriggerEvt);
            }

            if (zp != null && !string.IsNullOrEmpty(zp.FinishPickTriggerEvt) && zp.FinishPickTriggerEvt.IndexOf("lua") != -1)
            {
                TLBattleScene.Instance.DoParseLuaEvent(zp.FinishPickTriggerEvt);
            }
        }

        protected override void OnLeave()
        {
            this.doEnd();
        }

        protected override void OnUpdate(float delta)
        {
            if (this.mPickTime != null)
            {
                if (this.mPickTime.Percent > 0)
                {
                    if (this.mPickTime.Percent == 1)
                    {
                        this.mPickTime = null;
                        HudManager.Instance.Interactive.ShowGauge(false);
                        HudManager.Instance.Interactive.SetPercent(0);
                    }
                    else
                    {
                        HudManager.Instance.Interactive.ShowGauge(true);
                        HudManager.Instance.Interactive.SetPercent(this.mPickTime.Percent, this.mPickTime.ExpireTimeMS);
                    }


                }
                else
                {
                    HudManager.Instance.Interactive.ShowGauge(false);
                    HudManager.Instance.Interactive.SetPercent(0);

                }
            }
        }
    }

    /// <summary>
    /// 自动跟随寻路
    /// </summary>
    [DoNotToLua]
    public class AutoRunByFollowTarget : GState
    {
        private FindTargetAndMove action;
        private bool isFollowing = false;
        private Vector2 LastPos = Vector2.zero;
        public AutoRunByFollowTarget(TLAIActor unit, FindTargetAndMove _action) : base(unit)
        {
            this.action = _action;
            this.action.Action = DoActionCallBack;
            EventManager.Subscribe(GameEvent.SYS_ADD_UNIT, AddUnit);
            DoActionCallBack(_action);

        }

        protected override void OnEnter()
        {
            if (action.TargetUnit == null)
            {
                if (action.TargetObjID != 0)
                {
                    action.TargetUnit = unit.BattleScene.GetBattleObject(action.TargetObjID) as TLAIUnit;
                }
                if (action.TargetUnit == null)
                {
                    action.TargetUnit = unit.GetUnitByTemplateId(action.TemplateId);
                }

                if (action.TargetUnit == null)
                {
                    unit.StartAutoRunByAction(action, false);
                    isFollowing = false;
                }
                else
                {
                    isFollowing = true;
                }
            }
            Debugger.Log("isFollowing =" + isFollowing);

        }

        private void DoActionCallBack(MoveEndAction action)
        {
            FindTargetAndMove ftm = action as FindTargetAndMove;
            if (ftm != null && ftm.TargetUnit == null &&
                ftm.MapId == unit.BattleScene.SceneID
                && ftm.TargetObjID == 0)
            {
                unit.FindTargetByBattleServer(ftm.TemplateId);
            }
        }

        private void AddUnit(EventManager.ResponseData res)
        {
            if (action.TargetUnit == null && action.TargetObjID != 0)
            {
                var dict = (Dictionary<object, object>)res.data[1];
                object value;
                if (dict.TryGetValue("ObjectID", out value))
                {
                    if ((uint)value == action.TargetObjID)
                    {
                        object unit;
                        if (dict.TryGetValue("Unit", out unit))
                        {
                            TLAIUnit u = unit as TLAIUnit;
                            if (u != null)
                            {
                                action.TargetUnit = u;
                            }
                        }
                    }
                }
            }

        }

        protected override bool OnBlock(GState newstate, string reason)
        {
            return true;
        }


        protected override void OnLeave()
        {
            //todo.
            unit.stopAutoRun(action);
            EventManager.Unsubscribe(GameEvent.SYS_ADD_UNIT, AddUnit);
        }

        protected override void OnUpdate(float delta)
        {
            if (action.TargetUnit != null)
            {
                Vector2 actorPos = new Vector2(this.unit.X, this.unit.Y);
                Vector2 targetPos = new Vector2(action.TargetUnit.X, action.TargetUnit.Y);
                Vector2 aimPos = new Vector2(action.AimX, action.AimY);

                if (Vector2.Distance(actorPos, targetPos) > TLEditorConfig.Instance.PLAYER_FOLLOWNPC_DISTANCE_MAX)
                {
                    if (Vector2.Distance(aimPos, targetPos) > TLEditorConfig.Instance.PLAYER_FOLLOWNPC_DISTANCE_MAX)
                    {
                        action.AimX = targetPos.x;
                        action.AimY = targetPos.y;
                    }
                    //todo.
                    if (LastPos.x != action.AimX || LastPos.y != action.AimY)
                    {
                        action.AimX = action.TargetUnit.X;
                        action.AimY = action.TargetUnit.Y;
                        LastPos.x = action.AimX;
                        LastPos.y = action.AimY;
                        unit.StartAutoRunByAction(action, false);
                        isFollowing = true;
                    }
                }
                else
                {
                    if (isFollowing)
                    {
                        if (LastPos.x != action.AimX || LastPos.y != action.AimY)
                        {
                            action.AimX = action.TargetUnit.X;
                            action.AimY = action.TargetUnit.Y;
                            LastPos.x = action.AimX;
                            LastPos.y = action.AimY;
                            unit.StartAutoRunByAction(action, false);
                            isFollowing = false;
                        }
                    }

                }
            }
            else
            {
                //Debugger.Log("action.TargetUnit is null");
            }

        }
    }

    /// <summary>
    /// 自动寻路.
    /// </summary>
    [DoNotToLua]
    public class AutoRunState : GState
    {
        public MoveEndAction action;

        public bool checkRide = false;
        public bool NextStateIsAutoRun = false;
        public bool NeedStopForceQuest = true;
        /// <summary>
        /// 自动自动战斗定制器.
        /// </summary>
        private TimeExpire<int> autoGuardTimer;
        public AutoRunState(TLAIActor unit, MoveEndAction action) : base(unit)
        {

            //Debugger.LogError("action="+action.GetType().FullName);
            this.action = action;
            this.action.orgActorX = unit.X;
            this.action.orgActorY = unit.Y;
        }
        protected override bool OnBlock(GState newstate, string reason)
        {
            if (newstate is AutoRunState)
            {
                //目的地是否相同.
                AutoRunState newar = newstate as AutoRunState;
                //if (newar.action.IsSame(action))
                //{
                //    return false;
                //}
                //else
                if (newar.action != null)
                {
                    if (newar.action.MoveType != (int)AutoMoveType.MapTouch)
                    {
                        unit.CurGuardStatus = AutoGuardStatus.Stop;
                    }
                }

                if (newstate is AutoRunState)
                {
                    NextStateIsAutoRun = true;
                }

            }

            if (newstate is PreparePickState || newstate is PickSelfState || newstate is StartPickState)
            {
                NeedStopForceQuest = false;
            }
            
            //Debugger.LogWarning("AutoRunStateblock=" + newstate.GetType().FullName);
            return true;
        }

        private void initMountStatus()
        {
            if (!GameGlobal.Instance.netMode || !DataMgr.Instance.UserData.IsFuncOpen("MountFrame"))
            {
                return;
            }
            if (!unit.Vehicle.IsRiding)
            {
                if (unit.isNoBattleStatus() && unit.NeedRide(action))
                {
                    unit.ReqMount();
                    checkRide = false;
                }
                else
                {
                    checkRide = true;
                }
            }
        }

        private void UpdateMountStatus()
        {
            if (checkRide)
            {
                if ((unit.Vehicle == null || !unit.Vehicle.IsRiding) && unit.isNoBattleStatus() && unit.NeedRide(action))
                {
                    unit.ReqMount();
                    checkRide = false;
                }
            }
        }

        protected override void OnEnter()
        {
            //Debugger.Log("moveactiontypeof = " + action);
            unit.IsAutoRun = true;
            unit.StartAutoRunByAction(action);
            this.initMountStatus();
        }

        protected override void OnLeave()
        {
            //todo.
            unit.IsAutoRun = false;
            unit.stopAutoRun(action, NextStateIsAutoRun,NeedStopForceQuest);
        }

//        private void InitGuardTimer()
//        {
//            if (autoGuardTimer == null)
//                autoGuardTimer = new TimeExpire<int>(500);
//            else
//                autoGuardTimer.Reset();
//        }


        private void UpdateAutoGuard(float deltaTime)
        {
            if (unit.CurGuardStatus == AutoGuardStatus.StartByMoveStop)
            {
                if (autoGuardTimer == null)
                {
                    autoGuardTimer = new TimeExpire<int>(500);
                }
                if (unit.CurrentState == UnitActionStatus.Idle)
                {
                    if (autoGuardTimer != null && autoGuardTimer.Update((int)(deltaTime * 1000)))
                    {
                        unit.ChangeAutoAttackState(null);
                    }
                }
                else
                {
                    autoGuardTimer.Reset();
                }
                
            }
            #region 镇魔曲版自动战斗.
            /*
            if (unit.curGuardStatus == AutoGuardStatus.StartByMoveStop)
            {
                if (unit.CurrentState == UnitActionStatus.Idle)
                {
                    if (autoGuardTimer != null && autoGuardTimer.Update((int)(deltaTime * 1000)))
                    {
                        unit.SetAutoGuard(AutoGuardStatus.Start);
                    }
                }
                else
                {
                    InitGuardTimer();
                }
                if (unit.IsOutOfGuardRange())
                {
                    unit.SetAutoGuard(AutoGuardStatus.StartBySkill);
                }
                else
                {
                    if (unit.CurrentState == UnitActionStatus.Idle)
                    {
                        if (autoGuardTimer != null && autoGuardTimer.Update((int)(deltaTime * 1000)))
                        {
                            unit.SetAutoGuard(AutoGuardStatus.Start);
                        }
                    }
                    else
                    {
                        InitGuardTimer();
                    }
                }
                
            }
            else if (unit.curGuardStatus == AutoGuardStatus.StartBySkill)
            {
                var skillaction = unit.ZUnit.CurrentSkillAction;
                if (skillaction != null)
                {
                    if (skillaction.IsCancelableByMove || skillaction.IsCancelableBySkill || skillaction.IsDone)
                    {
                        unit.TryChangeAutoAttackState();
                        return;
                    }
                }
            }
            */
            #endregion
        }

        protected override void OnUpdate(float delta)
        {
            UpdateAutoGuard(delta);
            UpdateMountStatus();
            if (action != null)
            {
                action.OnUpdate();
            }


        }
    }

    [DoNotToLua]
    public class PlayCGState : GState
    {
        public PlayCGState(TLAIActor unit) : base(unit)
        {
            unit.curGuardStatus = AutoGuardStatus.Stop;
            unit.SendUnitStopMove();
        }
        protected override bool OnBlock(GState newstate, string reason)
        {
            if (CutSceneManager.Instance.IsPlaying)
            {
                CutSceneManager.Instance.LastState = newstate;
                return false;
            }
            return true;
        }

        protected override void OnEnter()
        {
        }

        protected override void OnLeave()
        {
        }

        protected override void OnUpdate(float delta)
        {

        }
    }

    public class PickSelfState : GState
    {
        TimeExpire<UnitStartPickObjectEvent> pickTimeExpire;

        public PickSelfState(TLAIActor unit, TimeExpire<UnitStartPickObjectEvent> pickTime) : base(unit)
        {
            pickTimeExpire = pickTime;
        }

        protected override bool OnBlock(GState newstate, string reason = null)
        {
            if (newstate is PreparePickState || newstate is StartPickState)
                return false;
            return true;
        }

        protected override void OnEnter()
        {

            if (pickTimeExpire.Tag.PickTimeMS > 0)
            {
                //TODO设置传送图片和文字.

                HudManager.Instance.Interactive.ShowGauge(true);
                HudManager.Instance.Interactive.SetPercent(0);
                HudManager.Instance.Interactive.setAutoImag(GameUtil.GetStringGameConfig("transfer_icon"));
                HudManager.Instance.Interactive.ShowPick(HZLanguageManager.Instance.GetString("transfer_msg"));
            }
        }

        protected override void OnLeave()
        {
            HudManager.Instance.Interactive.ShowGauge(false);
            HudManager.Instance.Interactive.SetPercent(0);
        }

        protected override void OnUpdate(float delta)
        {
            if (this.pickTimeExpire != null)
            {
                if (this.pickTimeExpire.Percent > 0)
                {
                    HudManager.Instance.Interactive.ShowGauge(true);
                    HudManager.Instance.Interactive.SetPercent(this.pickTimeExpire.Percent, this.pickTimeExpire.ExpireTimeMS);
                }
                else
                {
                    HudManager.Instance.Interactive.ShowGauge(false);
                    HudManager.Instance.Interactive.SetPercent(0);
                }
            }
        }
    }

    public class FollowUnitState : GState
    {
        protected float mPassTime;
        protected uint mTargetID;
        private float mCheckIntervalSec = 1.5f;
        private float mDisatanceToFollow = 2f;
        private float mEndDistance = 1f;
        private float mTimeOutSec = 20f;
        public float CheckIntervalSec
        {
            get { return mCheckIntervalSec; }
            set { mCheckIntervalSec = value; }
        }

        public float DisatanceToFollow
        {
            get { return mDisatanceToFollow; }
            set { mDisatanceToFollow = value; }
        }

        public float EndDistance
        {
            get { return mEndDistance; }
            set { mEndDistance = value; }
        }

        public float TimeOutSec
        {
            get { return mTimeOutSec; }
            set { mTimeOutSec = value; }
        }

        private float mPosTimeoutSec = 5.0f;
        private float mFailedPosCount = 0;
        public float PosTimeoutSec { get { return mPosTimeoutSec; } set { mPosTimeoutSec = value; } }

        protected bool IsInGetPos { get; private set; }

        public const float FIX_DISTANCE = 5;
        private float mLastDistance;
        public bool IsTimeout { get; set; }
        public FollowUnitState(TLAIActor unit, uint id) : base(unit)
        {
            mTargetID = id;
        }


        protected override bool OnBlock(GState newstate, string reason = null)
        {
            return true;
        }

        protected override void OnEnter()
        {
            mFailedPosCount = 0;
            IsInGetPos = false;
            mPassTime = CheckIntervalSec;
            IsTimeout = false;
        }

        protected override void OnLeave()
        {
            if (mMoveAgent != null)
            {
                ((ZoneActor)unit.ZUnit).RemoveAgent(mMoveAgent);
            }
        }

        protected override void OnUpdate(float delta)
        {
            mPassTime += delta;
            if (mPassTime > CheckIntervalSec && !IsTimeout)
            {
                if (TimeOutSec > 0 && mPassTime + mFailedPosCount * PosTimeoutSec >= TimeOutSec)
                {
                    IsTimeout = true;
                    OnTimeout();
                }
                else if (OnIntervalUpdate())
                {
                    mPassTime = 0;
                }
            }
        }

        private ActorMoveAgent mMoveAgent;
        protected void Seek(float x, float y)
        {
            IsInGetPos = false;
            mFailedPosCount = 0;
            if (float.IsNaN(x) || float.IsNaN(y))
            {
                //退出
                //unit.StopTeamFollow();
            }
            else
            {
                //distance
                var distance = CMath.getDistance(x, y, unit.ZUnit.X, unit.ZUnit.Y);
                if (BeforeSeek(distance, x, y))
                {
                    if (mMoveAgent == null || mMoveAgent.IsEnd || Math.Abs(mLastDistance - distance) > FIX_DISTANCE)
                    {
                        if (mMoveAgent != null)
                        {
                            ((ZoneActor)unit.ZUnit).RemoveAgent(mMoveAgent);
                        }
                        mMoveAgent = new ActorMoveAgent(x, y, EndDistance, UnitActionStatus.Move, true, null);
                        ((ZoneActor)unit.ZUnit).AddAgent(mMoveAgent);
                    }
                }
            }
        }

        protected virtual bool BeforeSeek(float dis, float x, float y)
        {
            return dis >= DisatanceToFollow;
        }

        protected virtual void TryGetPostion(Action<float, float> cb)
        {
            cb(float.NaN, float.NaN);
        }

        protected bool OnIntervalUpdate()
        {
            if (!IsInGetPos)
            {
                var u = TLBattleScene.Instance.GetBattleObject(mTargetID);
                if (u != null)
                {
                    Seek(u.ZObj.X, u.ZObj.Y);
                }
                else
                {
                    IsInGetPos = true;
                    TryGetPostion(Seek);
                }
                return true;
            }
            if (mPassTime >= PosTimeoutSec)
            {
                mFailedPosCount = mFailedPosCount + 1;
                IsInGetPos = false;
                return true;
            }
            return false;
        }

        protected virtual void OnTimeout()
        {
            Seek(float.NaN, float.NaN);
        }

    }

    public class TeamFollowState : FollowUnitState
    {
        private float mDistanceToSyncAutoFight = 15;
        /// <summary>
        /// 指定的视野内范围大小
        /// </summary>
        public const int CHECK_IN_VIEW = 20;
        public float DistanceToSyncAutoFight
        {
            get { return mDistanceToSyncAutoFight; }
            set { mDistanceToSyncAutoFight = value; }
        }

        public string TargetPlayerUUID { get; private set; }

        private int mTransportFrame = 0;
        public void TryReEnter()
        {
            if (!IsLeaved)
            {
                return;
            }
            bool needEnter = false;
            if (mTargetID == 0)
            {
                needEnter = true;
            }
            else
            {
                var u = TLBattleScene.Instance.GetBattleObject(mTargetID) as TLAIPlayer;

                if (u != null && u.PlayerVirtual != null)
                {
                    var distance = CMath.getDistance(unit.X, unit.Y, u.X, u.Y);
                    var autoGuard = u.PlayerVirtual.IsGuard();
                    //目标挂机,自己未挂机，使用DisatanceToFollow作为距离判断
                    if (autoGuard && !unit.PlayerVirtual.IsGuard() && distance > DisatanceToFollow)
                    {
                        needEnter = true;
                    }
                    //目标未挂机，使用DistanceToSyncAutoFight作为距离判断
                    else if (!autoGuard && distance > DistanceToSyncAutoFight)
                    {
                        needEnter = true;
                    }
                    needEnter = needEnter && (!IsInGetPos || unit.CurGState is IdleState);
                }
                else
                {
                    needEnter = true;
                }
            }
            if (needEnter && !TryTransport)
            {
                unit.ChangeActorState(this);
            }
            if (TryTransport)
            {
                mTransportFrame++;
                if (mTransportFrame > 120)
                {
                    TryTransport = false;
                    mTransportFrame = 0;
                }
            }
        }

        protected override bool OnBlock(GState newstate, string reason = null)
        {
            if (newstate is PreparePickState)
                return false;
            return true;
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            if (unit.PlayerVirtual.IsGuard())
            {
                unit.SyncUIBtnState(false);
            }
        }

        protected override bool BeforeSeek(float dis, float x, float y)
        {
            //if (DramaUIManage.Instance.GuideMask.gameObject.activeSelf)
            //{
            //    return false;
            //}
            if (mTargetID == 0)
            {
                //尝试通过uuid获取objid
                if (dis <= CHECK_IN_VIEW)
                {
                    var u = TLBattleScene.Instance.GetPlayerUnitByUUID(TargetPlayerUUID);
                    mTargetID = u.ObjectID;
                }
            }
            else
            {
                var u = TLBattleScene.Instance.GetBattleObject(mTargetID) as TLAIPlayer;

                if (u != null && u.PlayerVirtual != null)
                {
                    var autoGuard = u.PlayerVirtual.IsGuard();
                    bool syncGuard = dis <= DisatanceToFollow;
                    //|| (autoGuard && !DataMgr.Instance.UserData.AutoFight && dis <= DistanceToSyncAutoFight);
                    bool syncRide = dis > DisatanceToFollow;


                    var bs = u.PlayerVirtual.GetBattleStatus();
                    if (bs != CombatStateChangeEventB2C.BattleStatus.None && !autoGuard)
                    {
                        //选中队长正在打的目标
                        var targetID = u.PlayerVirtual.CurAtkTarget();
                        if (targetID != 0)
                        {
                            unit.SendUnitFocuseTarget(targetID);
                            autoGuard = true;
                        }
                    }

                    if (syncGuard && autoGuard)
                    {
                        //同步自动挂机
                        unit.BtnSetAutoGuard(true);
                        unit.SyncUIBtnState(true);
                        return false;
                    }

                    if (syncRide)
                    {
                        var zUnit = (ZoneUnit)u.ZObj;
                        var offset = zUnit.MoveSpeedSEC - unit.ZUnit.MoveSpeedSEC;
                        // 同步骑乘
                        if (zUnit.CurrentState == UnitActionStatus.Move &&
                            Math.Abs(offset) > 2 && DataMgr.Instance.UserData.IsFuncOpen("MountFrame"))
                        {
                            var willRide = offset > 0;
                            if (willRide != unit.PlayerVirtual.IsRiding())
                            {
                                unit.ReqMount(willRide);
                            }
                        }
                    }
                }
            }
            if (dis > CHECK_IN_VIEW && !unit.PlayerVirtual.IsRiding() && DataMgr.Instance.UserData.IsFuncOpen("MountFrame"))
            {
                unit.ReqMount(true);
            }
            return base.BeforeSeek(dis, x, y);
        }

        protected override void OnTimeout()
        {
            //DataMgr.Instance.TeamData.RequestTeamFollow(0, null);
        }

        public TeamFollowState(TLAIActor unit, string uuid) : base(unit, 0)
        {
            //屏蔽其他操作
            TargetPlayerUUID = uuid;
            CheckIntervalSec = 1.5f;
            TimeOutSec = -1;
            var u = TLBattleScene.Instance.GetPlayerUnitByUUID(TargetPlayerUUID);
            if (u != null)
            {
                mTargetID = u.ObjectID;
            }
        }

        public bool TryTransport { get; private set; }

        protected override void TryGetPostion(Action<float, float> cb)
        {
            if (string.IsNullOrEmpty(TargetPlayerUUID))
            {
                // error
                Debugger.LogError("playerUUID is null or empty");
            }
            var u = TLBattleScene.Instance.GetPlayerUnitByUUID(TargetPlayerUUID);
            if (u != null)
            {
                mTargetID = u.ObjectID;
                cb.Invoke(u.X, u.Y);
                return;
            }

            DataMgr.Instance.UserData.RoleSnapReader.Load(TargetPlayerUUID, snap =>
            {
                if (snap.ZoneUUID != DataMgr.Instance.UserData.ZoneUUID)
                {
                    TryTransport = true;
                    mTransportFrame = 0;
                    TLNetManage.Instance.Request<ClientChangeSceneResponse>(new ClientChangeSceneRequest
                    {
                        c2s_MapId = snap.MapTemplateID,
                        c2s_MapUUID = snap.ZoneUUID,
                    }, (ex, rp) => { }, new TLNetManage.PackExtData(false, false));
                }
                else
                {
                    TryTransport = false;
                    cb.Invoke(float.NaN, float.NaN);
                }
            });
        }

    }

}

