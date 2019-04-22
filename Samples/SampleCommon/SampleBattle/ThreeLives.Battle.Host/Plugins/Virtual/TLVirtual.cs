using DeepCore;
using DeepCore.Concurrent;
using DeepCore.GameData.Zone;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor;
using DeepCore.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using static DeepCore.GameHost.Instance.InstanceUnit;

namespace TLBattle.Server.Plugins.Virtual
{
    public partial class TLVirtual : IVirtualUnit
    {
        #region 须知.

        //Unit.Dummy_0用来同步战斗状态.CombatStateChangeEventB2C.BattleStatus None/PVP/PVE.
        //Unit.Dummy_1用来同步骑乘状态.
        #endregion

        #region Log.

        public static Logger log = new BlankLogger();

        public static bool log_enable = false;

        public static void FormatLog(LoggerLevel level, string txt, params object[] args)
        {
            if (args != null)
            {
                switch (level)
                {
                    case LoggerLevel.ERROR:
                        log.Error(string.Format(txt, args));
                        break;
                    case LoggerLevel.WARNNING:
                        log.Warn(string.Format(txt, args));
                        break;
                    default:
                        log.Debug(string.Format(txt, args));
                        break;
                }
            }
            else
            {
                switch (level)
                {
                    case LoggerLevel.ERROR:
                        log.Error(txt);
                        break;
                    case LoggerLevel.WARNNING:
                        log.Warn(txt);
                        break;
                    default:
                        log.Debug(txt);
                        break;
                }
            }
        }

        #endregion

        public readonly InstanceUnit mUnit;

        /// <summary>
        /// 模板数据.
        /// </summary>
        public UnitInfo mInfo;

        /// <summary>
        /// 角色原始属性.
        /// </summary>
        public TLUnitProperties mProp;

        /// <summary>
        ///场景数据. 
        /// </summary>
        protected TLServerSceneData ServerSceneData;

        /// <summary>
        /// 仇恨系统.
        /// </summary>
        readonly public TLHateSystem mHateSystem;

        /// <summary>
        /// 受击列表.
        /// </summary>
       // public TLHitSystem HitSystem;

        /// <summary>
        /// 镜像数据.
        /// </summary>
        public TLUnitProp MirrorProp { get { return PropModule.GetMirrorProp(); } }

        /// <summary>
        /// 技能模块.
        /// </summary>
        public TLSkillModule SkillModule;

        /// <summary>
        /// 自恢复模块.
        /// </summary>
        protected TLUnitAutoRecoverModule RecoverModule;

        /// <summary>
        /// 属性计算模块.
        /// </summary>
        public TLBattlePropModule PropModule;

        protected bool mPKGrayStatus = false;

        private bool mInSafeArea = false;

        private TimeInterval<int> recovery_timer = null;

        #region 进战、脱战.

        //当前战斗状态.
        private CombatStateChangeEventB2C.BattleStatus mCombatState = CombatStateChangeEventB2C.BattleStatus.None;
        //脱战计时器.
        private TimeExpire<int> mCombatTimeTimeExpire = null;
        /// <summary>
        /// 战斗状态监听.
        /// </summary>
        public delegate void OnCombatStateChangeEvent(TLVirtual unit, CombatStateChangeEventB2C.BattleStatus status);
        /// <summary>
        /// 战斗状态监听.
        /// </summary>
        protected OnCombatStateChangeEvent event_OnCombatStateChangeHandle;
        /// <summary>
        /// 战斗状态变更.
        /// </summary>
        public event OnCombatStateChangeEvent OnCombatStateChangeHandle
        {
            add { event_OnCombatStateChangeHandle += value; }
            remove { event_OnCombatStateChangeHandle -= value; }
        }

        #endregion


        public TLVirtual(InstanceUnit unit)
        {
            mUnit = unit;
            mInfo = unit.Info;
            mProp = unit.Info.Properties as TLUnitProperties;
            mHateSystem = new TLHateSystem(this);
            unit.OnDead += Unit_OnDead;
        }

        //死亡.
        protected virtual void Unit_OnDead(InstanceUnit unit, InstanceUnit attacker)
        {
            SetCombatState(CombatStateChangeEventB2C.BattleStatus.None);
        }

        public TLUnitProperties getTLUnitProperties()
        {
            return this.mProp;
        }

        public TLUnitData getTLUnitData()
        {
            return this.mProp.ServerData;

        }

        public TLDropRewardPickUp getTLDropRewardPickUp()
        {
            return this.mProp.ServerData.dropReward;
        }


        public void OnInit(InstanceUnit unit)
        {
            FormatLog(DeepCore.Log.LoggerLevel.INFO, "- 初始化单位 : {0} - {0} ", unit.ID, unit.Name);

            //检测创建时间.
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                //初始化单位其他模块.
                //添加监听.
                AddListener(mUnit);
                //1.场景信息.
                InitSceneInfo();
                //2.打击记录.
                // InitHitSystem();
                //3.子类初始化.
                DoInit();
                //4.基本信息.
                InitBaseData(mProp.ServerData.Prop);

                //5.属性模块.
                InitBattlePropModule();
                //6.技能信息.
                InitSkillModule(ref mProp.ServerData.SkillInfo);
                //7.自我回复.
                InitRecoverModule();
                //战斗状态.
                InitCombatTimeTimeExpire();

                DoInitFinish();
            }

            finally
            {
                sw.Stop();

                if (sw.ElapsedMilliseconds > 10)
                {
                    FormatLog(DeepCore.Log.LoggerLevel.INFO, "TLVirtual create overload , stopwatch time {0} > 10ms", sw.ElapsedMilliseconds);
                }

            }
        }

        /// <summary>
        /// 子类重铸.
        /// </summary>
        protected virtual void DoInit()
        {

        }

        protected virtual void DoInitFinish()
        {

        }

        /// <summary>
        /// 初始化攻击记录器.
        /// </summary>
        //protected void InitHitSystem()
        //{
        //    HitSystem = new TLHitSystem();
        //}

        /// <summary>
        /// 初始化场景数据.
        /// </summary>
        protected virtual void InitSceneInfo()
        {
            EditorScene es = mUnit.Parent as EditorScene;
            TLSceneProperties zp = es.Data.Properties as TLSceneProperties;
            ServerSceneData = zp.ServerSceneData;
        }

        private void InitSkillModule(ref TLUnitSkillInfo info)
        {
            //数据验证，编辑器数据转换.
            SkillInfoCheck(ref info);

            SkillModule = new TLSkillModule(this);
            SkillModule.Init();
        }

        private void SkillInfoCheck(ref TLUnitSkillInfo info)
        {
            #region 过滤data 防止无效的GameSkill

            if (info != null && info.Skills != null)
            {
                List<GameSkill> ls = info.Skills;

                for (int i = ls.Count - 1; i >= 0; i--)
                {
                    if (ls[i].SkillID == 0)
                    {
                        ls.RemoveAt(i);
                    }
                }
            }

            #endregion

            if (info == null || info.Skills == null || info.Skills.Count == 0)
            {
                List<LaunchSkill> list = this.mInfo.Skills;
                GameSkill gs = null;
                List<GameSkill> retList = new List<GameSkill>();
                //单位普攻.
                gs = null;
                if (mInfo.BaseSkillID != null && mInfo.BaseSkillID.SkillID != 0)
                {
                    gs = new GameSkill();
                    gs.SkillID = this.mInfo.BaseSkillID.SkillID;
                    SkillTemplate st = TLBattleSkill.GetSkillTemplate(gs.SkillID, false);
                    if (st != null)
                    {
                        TLSkillProperties zsp = (st.Properties as TLSkillProperties);
                        gs.SkillType = zsp.SkillType;
                        gs.SkillLevel = 1;
                        gs.SkillIndex = 0;
                        retList.Add(gs);
                    }
                    else
                    {
                        FormatLog(LoggerLevel.ERROR, "Unit:{0} GetSkillTemplate Error Can Not Find Skill:{1}", mInfo.ID, gs.SkillID);
                    }
                }

                //单位技能.
                for (int i = 0; i < list.Count; i++)
                {
                    gs = new GameSkill();
                    gs.SkillID = list[i].SkillID;
                    gs.SkillLevel = 1;
                    gs.SkillIndex = 1 + i;
                    gs.AutoLaunch = list[i].AutoLaunch;
                    SkillTemplate st = TLBattleSkill.GetSkillTemplate(gs.SkillID, false);
                    if (st != null)
                    {
                        TLSkillProperties zsp = (st.Properties as TLSkillProperties);
                        gs.SkillType = zsp.SkillType;
                        if (zsp.SkillType != GameSkill.TLSkillType.normalAtk)
                        {
                            retList.Add(gs);
                        }
                    }
                }

                if (info == null)
                {
                    info = new TLUnitSkillInfo();
                }

                info.Skills = retList;

            }
        }

        private void InitRecoverModule()
        {
            /*
            RecoverModule = new TLUnitAutoRecoverModule(this);
            RecoverModule.Init();
            */
            recovery_timer = new TimeInterval<int>(TLEditorConfig.Instance.AUTO_RECOVER_HP_CYCLETIME);
        }

        private void UpdateRecoverModule(int intervalMS)
        {
            /*
            if (RecoverModule != null)
            {
                RecoverModule.Update(intervalMS);
            }
            */

            if (recovery_timer != null && recovery_timer.Update(intervalMS))
            {
                OnRecoveryTimerTick();
            }
        }

        protected virtual void OnRecoveryTimerTick()
        {
            if (mUnit.IsDead == false)
            {
                AddHP(MirrorProp.AutoRecoverHp, mUnit, false);
            }
        }

        protected virtual void InitBattlePropModule()
        {
            PropModule = new TLBattlePropModule(this);
            PropModule.Init();
        }

        protected virtual void UpdateBattlePropModule(int intervalMS)
        {
            if (PropModule != null)
            {
                PropModule.Update(intervalMS);
            }
        }

        public void OnDispose(InstanceUnit owner)
        {
            DoDispose(owner);
        }

        protected virtual void DoDispose(InstanceUnit owner)
        {
            owner.OnDead -= Unit_OnDead;
            RemoveListener(owner);
            if (RecoverModule != null)
                RecoverModule.Dispose();
            if (SkillModule != null)
                SkillModule.Dispose();
            if (PropModule != null)
                PropModule.Dispose();
            ClearRegistEvent();
        }

        public virtual GameSkill GetGodMainSkill()
        {

            return null;
        }

        public virtual HashMap<int, GameSkill> GetGodSkill()
        {
            return null;
        }

        public virtual string Name()
        {
            return mInfo.Name;
        }
        #region 单位监听.

        private void AddListener(InstanceUnit unit)
        {
            if (unit != null)
            {
                mUnit.OnLaunchSkill += OnLaunchSkillHandler;
                mUnit.OnSkillChanged += OnSkillChangeHandler;
                mUnit.OnUpdate += MUnit_OnUpdate;
                OnCombatStateChangeHandle += TLVirtual_OnCombatStateChangeHandle;
                mUnit.OnSkillAdded += MUnit_OnSkillAdded;
            }
        }

        private void TLVirtual_OnCombatStateChangeHandle(TLVirtual unit, CombatStateChangeEventB2C.BattleStatus status)
        {
            this.mUnit.Dummy_0 = (byte)status;
        }

        private void MUnit_OnUpdate(InstanceUnit unit)
        {
            OnUpdate(unit.Parent.UpdateIntervalMS);
        }

        protected virtual void OnUpdate(int intervalMS)
        {
            UpdateCombatState(intervalMS);
            UpdateRecoverModule(intervalMS);
            UpdateBattlePropModule(intervalMS);
            UpdateAreaState(intervalMS);
        }

        private void RemoveListener(InstanceUnit unit)
        {
            if (unit != null)
            {
                mUnit.OnLaunchSkill -= OnLaunchSkillHandler;
                mUnit.OnSkillChanged -= OnSkillChangeHandler;
                mUnit.OnUpdate -= MUnit_OnUpdate;
                mUnit.OnSkillAdded -= MUnit_OnSkillAdded;
                OnCombatStateChangeHandle -= TLVirtual_OnCombatStateChangeHandle;
                OnHealEvent = null;
            }
        }

        //单位施放技能监听.
        private void OnLaunchSkillHandler(InstanceUnit obj, SkillState skill)
        {
            //用来计算技能消耗.扣蓝.

            int cost = 0;
            cost = DispatchLaunchsSkillOverEvent(cost, this, skill);
            mUnit.AddMP(-cost);
        }

        //技能改变监听.
        public void OnSkillChangeHandler(InstanceUnit obj, SkillState baseSkill, SkillState[] skills)
        {

        }

        private void MUnit_OnSkillAdded(InstanceUnit unit, SkillState sk)
        {
            var gs = this.SkillModule.GetGameSkill(sk.ID);
            if (gs != null)
            {
                sk.LaunchSkill.AutoLaunch = gs.AutoLaunch;
            }
        }

        #endregion

        #region 常用API.

        internal bool PKGrayStatus
        {
            set { mPKGrayStatus = value; }
            get { return mPKGrayStatus; }
        }

        /// <summary>
        /// 是否在安全区域.
        /// </summary>
        public bool InSafeArea
        {
            get { return (mInSafeArea && mUnit.AoiStatus == null); }
            set { mInSafeArea = value; }
        }

        protected virtual void InitBaseData(TLUnitProp data)
        {
            this.mUnit.SetMaxHP(data.MaxHP, false);
            if (data.CurHP >= 0)
            {
                this.mUnit.CurrentHP = data.CurHP;
            }
        }

        /// <summary>
        /// 获得角色原始属性.
        /// </summary>
        /// <returns></returns>
        public TLUnitProp GetOriginProp()
        {
            return mProp.ServerData.Prop;
        }

        public int GetUnitLv()
        {
            if (this.mProp.ServerData.BaseInfo.UnitLv == 0)
            {
                return 1;
            }

            return this.mProp.ServerData.BaseInfo.UnitLv;
        }

        //仇恨系统
        public HateSystem GetHateSystem()
        {
            return mHateSystem;
        }

        /// <summary>
        /// 加血.
        /// </summary>
        /// <param name="hp">血量</param>
        /// <param name="sender">加血者</param>
        /// <param name="sendMsg">是否发送协议</param>
        public void AddHP(int hp, InstanceUnit sender = null, bool sendMsg = true)
        {
            this.mUnit.ReduceHP(-hp, sender, sendMsg);
        }

        /// <summary>
        /// 百分比加血.
        /// </summary>
        /// <param name="per"></param>
        /// <param name="sender"></param>
        /// <param name="sendMsg"></param>
        public void AddHPPct(int per, InstanceUnit sender = null, bool sendMsg = true)
        {
            int hp = (int)(per / 100f * mUnit.MaxHP);
            this.mUnit.ReduceHP(-hp, sender, sendMsg);
        }

        /// <summary>
        /// 加能量.
        /// </summary>
        /// <param name="mp"></param>
        public void AddMP(int mp, bool sendMsg = true)
        {
            //飘字协议.
            mUnit.AddMP(mp);
        }

        public void AddMPPct(int v, bool SendMsg = true)
        {
            int mp = (int)(v / 100f * mUnit.MaxMP);
            mUnit.AddMP(mp);
        }

        /// <summary>
        /// 判断一定概率是否触发概率单位为万分比:填写1000表示百分之10.
        /// </summary>
        /// <param name="percent"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public bool RandomPercent(float percent)
        {
            bool ret;

            int r = this.mUnit.RandomN.Next(10000);

            ret = (r < percent) ? true : false;

            return ret;
        }

        public TLUnitBaseInfo.ProType GetProType()
        {
            if (mProp.ServerData.BaseInfo == null)

            {
                return TLUnitBaseInfo.ProType.None;
            }

            return mProp.ServerData.BaseInfo.RolePro;
        }

        public virtual bool IsPlayerUnit()
        {
            return false;
        }

        public virtual bool IsBuilding()
        {
            return false;
        }

        public virtual TLVirtual GetPlayerUnit()
        {
            return null;
        }

        public virtual void DataAddSkill(List<GameSkill> lt, bool syncSkillModule)
        {

        }
        public virtual void DataRemoveSkill(List<GameSkill> lt, bool syncSkillModule)
        {

        }

        public virtual string GetPlayerUUID()
        {
            return "";
        }

        public bool IsFinishModuleInit()
        {
            if (SkillModule == null) { return false; }
            return SkillModule.IsFinishInit();
        }

        public virtual string GuildUUID()
        {
            return null;
        }
        /// <summary>
        /// 向战斗服发送提示信息.
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsgToClient(string msg)
        {
            if (msg != null)
            {
                ShowTipsEventB2C evt = new ShowTipsEventB2C();
                evt.Msg = msg;
                this.mUnit.queueEvent(evt);
            }
        }

        public void SendMsgToClient(string msg, List<string> param)
        {
            if (msg != null)
            {
                ShowTipsEventB2C evt = new ShowTipsEventB2C();
                evt.Msg = msg;
                evt.Params = param;

                this.mUnit.queueEvent(evt);
            }
        }

        /// <summary>
        /// 战斗特殊文字飘字.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void SendBattleAtkNumberEventB2C(BattleAtkNumberEventB2C.AtkNumberType type, uint visibleUnit = 0, int value = 0)
        {
            var evt = new BattleAtkNumberEventB2C();
            evt.Type = type;
            evt.Value = value;
            evt.VisibleUnit = visibleUnit;
            this.mUnit.queueEvent(evt);
        }

        public void SendBattleSplitHitEventB2C(List<SplitHitInfo> list, int totalDamage, uint sendID, AttackProp ap)
        {
            var evt = new BattleSplitHitEventB2C();
            evt.hitInfo = list;
            evt.sendID = sendID;
            var p = ap.Properties as TLAttackProperties;
            evt.effect = p.SplitData.hitEffect;
            evt.TotalDamage = totalDamage;

            this.mUnit.queueEvent(evt);
        }

        public uint GetRandomTarget()
        {
            if (mHateSystem == null) { return 0; }

            return mHateSystem.GetRandomTarget();
        }

        /// <summary>
        /// 获得攻击目标.
        /// </summary>
        /// <returns></returns>
        public InstanceUnit GetAtkTarget(InstanceUnit target)
        {
            var unit_prop = target.Virtual as TLVirtual;
            return DispatchGetAtkUnitEvent(unit_prop).mUnit;
        }

        protected virtual PKInfo.PKMode GetPKMode()
        {
            return PKInfo.PKMode.Peace;
        }

        public virtual int GetPKLevel()
        {
            return 0;
        }

        protected virtual int GetPKValue()
        {
            return 0;
        }

        protected virtual PKInfo GetPKInfo()
        {
            return null;
        }

        private void UpdateAreaState(int intervalMS)
        {
            var area = this.mUnit.CurrentArea;
            if (area != null && area.CurrentMapNodeValue == TLConstants.MAP_BLOCK_VALUE_SAFE)
            {
                InSafeArea = true;
            }
            else
            {
                InSafeArea = false;
            }
        }

        public long GetCurTimeStampMS()
        {
            if (SkillModule != null)
                return SkillModule.GetTimestampMS();
            return 0;
        }

        public TLUnitBaseInfo GetBaseInfo()
        {
            if (mProp != null && mProp.ServerData != null)
            {
                return mProp.ServerData.BaseInfo;
            }

            return null;
        }

        public virtual List<GameSkill> GetAllSkillData()
        {
            return GetSkillData();
        }

        public List<GameSkill> GetSkillData()
        {
            var src = mProp.ServerData.SkillInfo.Skills;
            List<GameSkill> lt = new List<GameSkill>(src);
            return lt;
        }

        public bool InAvatarChange()
        {
            return ContainChangeAvatarBuff();
        }

        public bool InDebuffStatus()
        {
            bool ret = false;

            if (this.mUnit.IsSilent || this.mUnit.IsStun || this.mUnit.IsFallingDown || this.mUnit.IsDamageFallingDown || InAvatarChange())
            {
                ret = true;
            }

            return ret;
        }

        public bool IsBusy()
        {
            bool ret = false;

            if (this.mUnit.CurrentState != null && (this.mUnit.CurrentState is InstanceUnit.StateSkill))
            {
                ret = true;
            }

            return ret;
        }

        public bool RemoveBuff(int buffid, string reason)
        {
            var bs = this.mUnit.GetBuffByID(buffid);

            if (bs == null) return false;

            var bp = bs.Data.Properties as TLBuffProperties;

            if (!bp.CanBePurged)
                return false;

            this.mUnit.removeBuff(buffid, reason);
            return true;
        }

        public void RemoveAllBuffs(Predicate<BuffState> match, string reason)
        {
            if (match == null) return;

            using (var buffList = ListObjectPool<InstanceUnit.BuffState>.AllocAutoRelease())
            {
                this.mUnit.GetAllBuffStatus(buffList);

                if (buffList != null)
                {
                    buffList.FindAll((bs) =>
                    {
                        var bp = bs.Data.Properties as TLBuffProperties;
                        if (bp.CanBePurged && match.Invoke(bs))
                        {
                            this.mUnit.removeBuff(bs.ID, reason);
                        }
                        return false;
                    });
                }
            }
        }

        public void RecordAttackData(bool active)
        {
            if (mHateSystem != null)
                mHateSystem.RecordAttackData = active;
        }

        public TLKillInfo GetKillInfo()
        {
            return mHateSystem.GetKillInfo();
        }

        public void LockAtkUnit(InstanceUnit target)
        {
            mHateSystem.LockAtkUnit(target);
        }

        #endregion

        #region 进战脱战.

        public CombatStateChangeEventB2C.BattleStatus GetCombatState()
        {
            return mCombatState;
        }

        //脱战计时器.
        private void InitCombatTimeTimeExpire()
        {
            if (mCombatTimeTimeExpire == null)
            {
                mCombatTimeTimeExpire = new TimeExpire<int>(TLEditorConfig.Instance.COMBAT_TIME_EXPIRE);
            }
        }

        private void UpdateCombatState(int time)
        {
            if (mCombatState != CombatStateChangeEventB2C.BattleStatus.None && mCombatTimeTimeExpire != null && mCombatTimeTimeExpire.Update(time))
            {
                SetCombatState(CombatStateChangeEventB2C.BattleStatus.None);
            }
        }

        /// <summary>
        /// 设置改变战斗状态.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="reason"></param>
        public virtual void SetCombatState(CombatStateChangeEventB2C.BattleStatus value, byte reason = 0)
        {
            if (value != CombatStateChangeEventB2C.BattleStatus.None)//进入战斗.
            {
                mCombatTimeTimeExpire.Reset();
            }

            if (mCombatState != value)
            {
                //战斗状态允许等级上升不允许下降.
                if (value == CombatStateChangeEventB2C.BattleStatus.None || value > mCombatState)
                {
                    if (VerifyCombatStateChange(value, reason))
                    {
                        ChangeCombatStatue(value, reason);
                    }
                }
            }
        }

        /// <summary>
        /// 控制状态能否改变.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        protected virtual bool VerifyCombatStateChange(CombatStateChangeEventB2C.BattleStatus status, byte reason)
        {
            bool ret = true;
            //默认为0，特殊情况不为0.
            if (reason != 0)
            {
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 改变战斗状态.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="reason"></param>
        private void ChangeCombatStatue(CombatStateChangeEventB2C.BattleStatus value, byte reason = 0)
        {
            mCombatState = value;
            CombatStateConnect(value, reason);
            OnCombatStateChange(value);
        }

        /// <summary>
        /// 战斗状态关联(队友间相互影响，召唤物影响）.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="reason"></param>
        protected virtual void CombatStateConnect(CombatStateChangeEventB2C.BattleStatus value, byte reason = 0)
        {

        }

        /// <summary>
        /// 状态改变触发逻辑调用.
        /// </summary>
        /// <param name="status"></param>
        protected virtual void OnCombatStateChange(CombatStateChangeEventB2C.BattleStatus status)
        {
            //脱战时，清空仇恨列表.
            if (status == CombatStateChangeEventB2C.BattleStatus.None && this.mUnit.IsDead == false)
            {
                mHateSystem.Clear();

                FormatLog(LoggerLevel.INFO, "{0}脱离战斗", mInfo.Name);
            }

            //状态变更时，需要同步告知客户端做响应表现.
            if (status != CombatStateChangeEventB2C.BattleStatus.None)
            {
                FormatLog(LoggerLevel.INFO, "{0}进入战斗", mInfo.Name);
            }

            if (event_OnCombatStateChangeHandle != null)
            {
                event_OnCombatStateChangeHandle.Invoke(this, status);
            }
        }

        internal virtual void ChangeCombatStateFromAtk(TLVirtual attacker, bool isHarmful)
        {
            if (attacker.mUnit.ID == this.mUnit.ID)
            {
                return;
            }

            var state = CombatStateChangeEventB2C.BattleStatus.PVE;
            var atkState = attacker.GetCombatState();
            if (atkState > state) { state = atkState; }

            this.SetCombatState(state);
        }

        internal virtual void ChangeCombatStateOnHitOther(TLVirtual hitter, bool isHarmful)
        {
            if (hitter.mUnit.ID == this.mUnit.ID)
                return;

            var state = CombatStateChangeEventB2C.BattleStatus.PVE;

            if (isHarmful == false)
            {
                //增益技能（BUFF）.
                //攻击方 = 受击方 当前战斗状态.
                //受击方 = 攻击方 当前战斗状态.
                state = hitter.GetCombatState();
            }
            else
            {
                bool isPlayer = hitter.IsPlayerUnit();

                if (isPlayer) { state = CombatStateChangeEventB2C.BattleStatus.PVP; }

                this.SetCombatState(state);
            }
        }

        #endregion

        #region Formula状态相关.

        internal void OnUnitRemoved() { }

        internal virtual void OnUnitDead(TLVirtual deader, TLVirtual killer)
        {
            if (deader.GetPlayerUUID() != killer.GetPlayerUUID())
                OnKillUnit();
        }

        internal void OnHandleNetMessage(ObjectAction action)
        {

        }

        #endregion

        #region 敌友关系判断.

        /// <summary>
        /// 是否是敌人
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool IsEnemy(TLVirtual target)
        {
            if (target.IsNeutrality()) { return false; }

            bool ret = !(this.mUnit.Force == target.mUnit.Force ? true : false);
            return ret;
        }

        /// <summary>
        /// 是否是友军.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="includeSelf"></param>
        /// <returns></returns>
        public virtual bool IsAllies(TLVirtual target, bool includeSelf = true)
        {
            if (target.IsNeutrality()) { return false; }

            bool ret = false;

            if (this.mUnit.ID == target.mUnit.ID)
            {
                if (includeSelf)
                    return true;
                else
                    return false;
            }

            ret = !IsEnemy(target);

            return ret;
        }

        /// <summary>
        /// 是否中立.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsNeutrality()
        {
            return (this.mUnit.Force == 0);
        }

        /// <summary>
        /// 是否为队伍成员.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool IsTeamMember(TLVirtual target)
        {
            return false;
        }

        public virtual bool IsTeamMember(uint objID)
        {
            return false;
        }

        /// <summary>
        /// 是否为公会成员.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected virtual bool IsGuildMember(TLVirtual target)
        {
            return false;
        }

        /// <summary>
        /// 是否为红名.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool IsRedName()
        {
            return false;
        }

        #endregion

        /// <summary>
        /// 获得穴位信息.
        /// </summary>
        /// <returns></returns>
        public virtual HashMap<int, int> GetMeridiansInfo()
        {
            return null;
        }
    }
}