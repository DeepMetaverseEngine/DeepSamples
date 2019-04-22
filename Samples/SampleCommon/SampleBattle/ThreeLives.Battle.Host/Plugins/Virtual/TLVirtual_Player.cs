
using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameData.ZoneClient;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using ThreeLives.Battle.Data.Data;
using TLBattle.Common.Data;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using TLBattle.Plugins;
using TLBattle.Server.Message;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using TLBattle.Server.Plugins.Units;
using TLBattle.Server.Scene;
using static DeepCore.GameHost.Instance.InstanceUnit;

namespace TLBattle.Server.Plugins.Virtual
{
    public partial class TLVirtual_Player : TLVirtual
    {
        /// <summary>
        /// 当前背包数量.
        /// </summary>
        private uint mInventorySize = 1000;
        public uint InventorySize { private set { mInventorySize = value; } get { return mInventorySize; } }

        /// <summary>
        /// PKValue检查时间.
        /// </summary>
        private const int PKValueUpdateTime = 2000;
        /// <summary>
        /// 杀戮值更新计时器.
        /// </summary>
        private TimeInterval<int> mPKValueTimer = null;
        /// <summary>
        /// 发送PK值变化计时器.
        /// </summary>
        private TimeInterval<int> mSendPKValueTimer = null;
        /// <summary>
        /// 是否发PK值变更协议.
        /// </summary>
        private bool SendPKValueChangeFlag = false;

        private int mRedNameAttackPoint = 0;

        /// <summary>
        /// 宠物.
        /// </summary>
        private TLVirtual_Pet mPet = null;

        /// <summary>
        /// 上一次攻击的单位.
        /// </summary>
        private uint mLastAtkUnitID = 0;

        /// <summary>
        /// 仇人列表.
        /// </summary>
        private HashMap<string, int> RevengeMap;
        /// <summary>
        /// 组队列表.
        /// </summary>
        private HashMap<string, TeamMemberSnap> mTeamMap;
        /// <summary>
        /// 跟随的单位[模板ID,实例ID]
        /// </summary>
      //  private HashMap<int, uint> FollowUnitMap = new HashMap<int, uint>();

        //允许自动战斗.
        private bool mUseAutoGuard;
        //允许使用仙侣技能.
        private bool mUseGodSkill;
        //计算PK值.
        private bool mCalPKValue;
        //自动切换PK模式.
        private bool mAutoChangePKMode;

        public delegate void OnAtkUnitHandel();

        private OnAtkUnitHandel mOnAtkUnit;

        public event OnAtkUnitHandel OnAtkUnit
        {
            add { mOnAtkUnit += value; }
            remove { mOnAtkUnit -= value; }
        }

        public TLVirtual_Player(InstanceUnit unit) : base(unit)
        {
            unit.OnRebirth += Unit_OnRebirth;
            unit.OnRemoved += Unit_OnRemoved;
            unit.OnLaunchSkill += Unit_OnLaunchSkill;
            this.OnRidingStatusChangeEvtHandler += TLVirtual_Player_OnRidingStatusChangeEvtHandler;
        }

        /// <summary>
        /// 坐骑状态变更.
        /// </summary>
        private void TLVirtual_Player_OnRidingStatusChangeEvtHandler()
        {
            this.mUnit.Dummy_1 = IsRiding == true ? 1 : 0;
        }

        private void Unit_OnLaunchSkill(InstanceUnit obj, InstanceUnit.SkillState skill)
        {
            //施放技能进入战斗状态.
            SetCombatState(CombatStateChangeEventB2C.BattleStatus.PVE);

            var target = skill.State.TargetUnit;
            if (target != null && !IsAllies((target.Virtual as TLVirtual), false))
            {
                //记录攻击单位.
                SetLastAtkUnit(target.ID);
            }
        }

        private void SetLastAtkUnit(uint id)
        {
            mLastAtkUnitID = id;
            this.mUnit.CurrentTarget = id;
            OnPlayerAtkUnit();
        }

        private void OnPlayerAtkUnit()
        {
            mOnAtkUnit?.Invoke();
        }

        public uint GetLastAtkUnit()
        {
            return mLastAtkUnitID;
        }

        public override string Name()
        {
            return GetBaseInfo().Name;
        }

        public override void SetCombatState(CombatStateChangeEventB2C.BattleStatus value, byte reason = 0)
        {
            if (value != CombatStateChangeEventB2C.BattleStatus.None)//进入战斗.
            {
                this.TakeOffMount();
            }
            base.SetCombatState(value, reason);
        }

        protected override void DoInit()
        {
            base.DoInit();
            //玩家单位不记录攻击数据.
            mHateSystem.RecordAttackData = false;

            //玩家单位初始化.
            //改到ready中调用.
            InitQuestData();

            //坐骑信息记录
            CheckMountStatus(mProp);

            var data = CreateVisibleData(mProp);

            this.mUnit.Level = data.BaseInfo.UnitLv;
            //推送给客户端.
            SetVisibleInfo(mUnit, data);

            CheckPKInfo();

            CheckTeamInfo();

            CheckRevengeInfo();

            ChangePet(mProp.ServerData.UnitPetData);

            //MOCK.
            //MockGod();
            //MOCK
            //AddFollowUnit(11111);
            InitGodSkill(mProp.ServerData.UnitGodData);

            InitGuildChaseMap(mProp.ServerData.BaseInfo.GuildChaseList);
        }
        protected override void DoInitFinish()
        {
            base.DoInitFinish();
            SendClientSkillPanelInfo();
            InitBattleBuffInfo(mProp.ServerData.BattleBuffInfo);
        }
        protected override void DoDispose(InstanceUnit owner)
        {
            base.DoDispose(owner);
            owner.OnRebirth -= Unit_OnRebirth;
            owner.OnRemoved -= Unit_OnRemoved;
            owner.OnLaunchSkill -= Unit_OnLaunchSkill;
            mOnAtkUnit = null;
            this.OnRidingStatusChangeEvtHandler -= TLVirtual_Player_OnRidingStatusChangeEvtHandler;
        }

        public void ClearAnger()
        {
            //死亡时候怒气清零0.
            this.mUnit.CurrentMP = 0;
        }
        //死亡.
        protected override void Unit_OnDead(InstanceUnit unit, InstanceUnit attacker)
        {
            ClearAnger();
            base.Unit_OnDead(unit, attacker);
        }
        //离开场景.
        private void Unit_OnRemoved(InstanceUnit unit)
        {
            //主人离开场景，移除宠物.
            DismissPet();
        }
        //重生.
        private void Unit_OnRebirth(InstanceUnit unit)
        {
            //重生.
            this.mUnit.queueEvent(new BattleServerPlayerRebirthNotifyB2R());
        }

        private PlayerVisibleDataB2C CreateVisibleData(TLUnitProperties prop)
        {
            PlayerVisibleDataB2C ret = new PlayerVisibleDataB2C();

            ret.BaseInfo = prop.ServerData.BaseInfo;
            ret.AvatarMap = prop.ServerData.AvatarMap;
            ret.UnitPKInfo = prop.ServerData.UnitPKInfo;

            return ret;
        }

        private void SetVisibleInfo(InstanceUnit unit, IUnitVisibleData data)
        {
            unit.SetVisibleInfo(data);
        }

        private TLMapData GetMapData()
        {
            if (mProp.ServerData.SceneDataInfo == null)
            {
                return null;
            }

            TLMapData ret = TLDataMgr.GetInstance().MapDataMgr.MapData(mProp.ServerData.SceneDataInfo.MapID);
            return ret;
        }

        public int GetMapID()
        {
            if (mProp.ServerData.SceneDataInfo == null)
            {
                return 0;
            }
            return mProp.ServerData.SceneDataInfo.MapID;
        }

        /// <summary>
        /// 客户端重连.
        /// </summary>
        public void OnUnitReEnter(AddUnit add)
        {
            //重新上线时，推送属性.
            this.PropModule.SyncAllBattleProps();
            SendClientSkillPanelInfo();
            CheckTeamInfo();
            CheckRevengeInfo();
        }

        public override TLVirtual GetPlayerUnit()
        {
            return this;
        }

        public override bool IsPlayerUnit()
        {
            return true;
        }

        public override string GetPlayerUUID()
        {
            return (this.mUnit as Units.TLInstancePlayer).PlayerUUID;
        }

        public override string GuildUUID()
        {
            var t = GetBaseInfo();
            if (t == null) return null;
            return t.GuildUUID;
        }

        public string GuildName()
        {
            var t = GetBaseInfo();
            return t?.GuildName;
        }
        internal bool VerifyInventory()
        {
            return mInventorySize - 1 >= 0;
        }

        internal uint InventorySizePreChange()
        {
            mInventorySize--;

            return mInventorySize;
        }

        public void OnInventorySizeChange(InventorySizeChangeEventR2B msg)
        {
            InventorySize = msg.curInventorySize;
        }

        protected override void OnUpdate(int intervalMS)
        {
            base.OnUpdate(intervalMS);
            UpdatePKValueTimer(intervalMS);
        }

        internal override int OnHit(TLVirtual attacker, AttackSource source)
        {
            //加入仇恨列表.
            int v = base.OnHit(attacker, source);
            if (v > 0) mHateSystem.OnHitted(attacker.mUnit, source, v);
            //判断是否开启自动变更PK模式.
            if (v > 0 &&
                attacker.IsPlayerUnit() &&                   //被玩家攻击.
                this.GetPKMode() != PKInfo.PKMode.Justice && //非正义模式.
                this.GetPKMode() == PKInfo.PKMode.Peace &&   //必须处于和平模式.
                mAutoChangePKMode == false &&
                (GetMapData() != null && GetMapData().change_pk != 0))
            {
                var evt = new AutoChangePKModeEventB2C();
                evt.s2c_mode = PKInfo.PKMode.Justice;
                this.mUnit.queueEvent(evt);
                mAutoChangePKMode = true;
            }

            return v;
        }

        protected override void InitSceneInfo()
        {
            base.InitSceneInfo();

            if (ServerSceneData != null)
            {
                switch (ServerSceneData.SceneType)
                {
                    case TLConstants.SceneType.Normal:
                        (this.mUnit as TLInstancePlayer).SetGuardSearchRange(TLBattle.Plugins.TLEditorConfig.Instance.NORMALSCENE_AUTO_GUARD_RANGE);
                        break;
                    case TLConstants.SceneType.Dungeon:
                        (this.mUnit as TLInstancePlayer).SetGuardSearchRange(TLBattle.Plugins.TLEditorConfig.Instance.DUNGEON_AUTO_GUARD_RANGE);
                        (this.mUnit as TLInstancePlayer).SetGuardType(TLInstancePlayer.GuardMode.Map);
                        break;
                    default:
                        break;
                }
            }

            var data = GetMapData();
            if (data != null)
            {
                #region 自动战斗限制.

                if (data != null)
                {
                    switch (data.auto_fight)
                    {
                        case 0:
                        //(进入后默认关闭自动战斗).
                        case 1:
                            //(进入后默认开启自动战斗).
                            mUseAutoGuard = true;
                            break;
                        case 2:
                            //(通关后再次进入默认关闭自动战斗)
                            //TODO,LOGIC需要接入是否通关副本.
                            mUseAutoGuard = false;
                            break;
                        case 3:
                            //始终不能允许自动战斗.
                            mUseAutoGuard = false;
                            break;
                        default:
                            break;
                    }
                }

                #endregion

                #region 仙侣技能限制.

                if (data.use_god == 0)
                    mUseGodSkill = false;
                else if (data.use_god == 1)
                    mUseGodSkill = true;

                #endregion

                #region 计算PK值限制.

                if (data.ignore_pk == 0) mCalPKValue = true;
                else if (data.ignore_pk == 1) mCalPKValue = false;

                #endregion
            }
        }

        /// <summary>
        /// 是否允许自动战斗.
        /// </summary>
        /// <returns></returns>
        public bool AllowUseAutoGuard()
        {
            return (mUseAutoGuard || TemplateManager.IsEditor);
        }

        /// <summary>
        /// 是否能使用godSkill.
        /// </summary>
        /// <returns></returns>
        public bool AllowUseGodSkill()
        {
            return (mUseGodSkill || TemplateManager.IsEditor);
        }

        /// <summary>
        /// 是否计算PK值.
        /// </summary>
        /// <returns></returns>
        public bool CalPKValue()
        {
            return mCalPKValue;
        }

        public bool InHateSytem(uint id)
        {
            //是否攻击过自己.
            return mHateSystem.ContainsID(id);
        }

        /// <summary>
        /// 是否在复仇名单中.
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public bool InRevengeList(string uuid)
        {
            if (RevengeMap.Count == 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(uuid))
                return false;

            return RevengeMap.ContainsKey(uuid);
        }

        private void InitRevengeList(HashMap<string, int> map, List<string> lt)
        {
            map.Clear();

            if (lt != null)
            {
                for (int i = 0; i < lt.Count; i++)
                {
                    map.Put(lt[i], 0);
                }
            }

            var evt = new RevengeListChangeEvtB2C();
            evt.list = lt;
            mUnit.queueEvent(evt);
        }

        private void InitTeamMap(HashMap<string, TeamMemberSnap> map, List<TeamMemberSnap> lt, string teamLeaderUUID, string teamUUID)
        {
            map.Clear();

            if (lt != null)
            {
                for (int i = 0; i < lt.Count; i++)
                {
                    map.Put(lt[i].UUID, lt[i]);
                }
            }

            var info = GetBaseInfo();
            if (info != null)
            {
                info.list = lt;
                info.teamLeaderUUID = teamLeaderUUID;
                info.teamUUID = teamUUID;
            }

            var evt = new TeamMemberListChangeEvtB2C();
            evt.list = lt;
            evt.teamLeaderUUID = teamLeaderUUID;
            evt.teamUUID = teamUUID;

            mUnit.queueEvent(evt);
        }

        protected override void InitBaseData(TLUnitProp data)
        {
            base.InitBaseData(data);

            this.mUnit.SetMaxMP(TLDataMgr.GetInstance().GameConfigData.GetAngerLimit(), false);
            if (data.CurAnger >= 0)
            {
                this.mUnit.CurrentMP = data.CurAnger;
            }
        }

        protected override void OnRecoveryTimerTick()
        {
            base.OnRecoveryTimerTick();

            if (mUnit.IsDead == false && InGodStatus() == false)
            {
                AddMP(TLDataMgr.GetInstance().GameConfigData.GetAngerRecovery(), false);
            }
        }

        private bool InGodStatus()
        {
            int buffID = GetGodBuffID();
            if (buffID == 0) return false;

            var b = this.mUnit.GetBuffByID(buffID);
            if (b == null)
                return false;
            return true;
        }

        public void StartTP(int NextMapID, string NextMapPosition, float x = -1, float y = -1, bool force = false, string guildUUID = null, string zoneUUID = null, HashMap<string, string> ext = null)
        {
            //当前已在传送状态时不再执行.
            if (mUnit.CurrentState is StatePickObject curs)
            {
                if (curs.Force) return;
            }

            //PVP状态下无法传送.
            if (GetCombatState() == CombatStateChangeEventB2C.BattleStatus.PVP)
            {
                SendMsgToClient(TLCommonConfig.TIPS_TRANSPORT_CONDITION_ERROR);
                return;
            }

            TakeOffMount();
            int mapTemplateID = TLDataMgr.GetInstance().MapDataMgr.MapTemplateID(NextMapID);

            //同场景传送.
            if (guildUUID == null && mapTemplateID == mUnit.Parent.TerrainSrc.ID)
            {
                if (string.IsNullOrEmpty(NextMapPosition))//无FLAG走坐标.
                {
                    if (x == y && x == -1)
                    {
                        (mUnit as TLInstancePlayer).TransToStartRegion();
                    }
                    else
                    {
                        mUnit.transport(x, y);
                    }
                }
                else
                {
                    var flag = mUnit.Parent.getFlag(NextMapPosition);
                    if (flag != null)
                    {
                        mUnit.transport(flag.Pos.x, flag.Pos.y);
                    }
                    else
                    {
                        (mUnit as TLInstancePlayer).TransToStartRegion();
                    }
                }

            }
            else
            {
                int timeMS = TLDataMgr.GetInstance().GameConfigData.GetTPTimesMS();
                string status = "";

                var tp = new TPB2R();
                tp.NextMapID = NextMapID;
                tp.NextMapPosition = NextMapPosition;
                tp.X = x;
                tp.Y = y;
                tp.GuildUUID = guildUUID;
                tp.ZoneUUID = zoneUUID;
                tp.Ext = ext;
                if (force)
                {
                    this.mUnit.queueEvent(tp);
                }
                else
                {
                    int flyTimeMS = TLDataMgr.GetInstance().GameConfigData.GetTPFlyTimeMS();
                    (mUnit as TLInstancePlayer).StartPickProgressSelf(timeMS, (s, p) =>
                    {
                        status = "tp";
                        //上天无敌.
                        this.mUnit.SetInvincibleTimeMS(flyTimeMS);
                        var forcePickState = (mUnit as TLInstancePlayer).StartPickProgressSelf(flyTimeMS, (s2, p2) =>
                        {
                            this.mUnit.queueEvent(tp);
                            this.mUnit.startIdle(true);
                        }, status, true, false);
                    }, status, true, true);

                }

            }
        }

        public void StartTP(TLTPData data)
        {
            StartTP(data.NextMapID, data.NextMapPosition, data.X, data.Y, data.Force, data.GuildUUID, null);
        }

        public void OnStartTP(StartTPEventR2B evt)
        {
            StartTP(evt.NextSceneID, evt.NextScenePosition, evt.x, evt.y, evt.force, evt.guildUUID, evt.ZoneUUID, evt.ext);
        }

        #region 属性同步.

        protected override void InitBattlePropModule()
        {
            PropModule = new TLBattlePropModule(this);
            PropModule.OnBattlePropChange += PropModule_OnBattlePropChange;
            PropModule.Init();

            CheckMountSpeed();
        }

        private void PropModule_OnBattlePropChange(HashMap<TLPropObject.PropType, int> map)
        {
            SyncBattleProps(map);
        }

        private void SyncBattleProps(HashMap<TLPropObject.PropType, int> map)
        {
            PlayerBattlePropChangeEventB2C evt = new PlayerBattlePropChangeEventB2C();

            TLPropObject temp = null;
            foreach (var kvp in map)
            {
                temp = new TLPropObject(kvp.Key, TLPropObject.ValueType.Value, kvp.Value);
                evt.PropList.Add(temp);
            }

            mUnit.queueEvent(evt);
        }

        public void OnBaseInfoChange(TLUnitBaseInfo info)
        {
            if (info != null)
            {
                //升级，血量重新刷新.
                if (info.UnitLv != GetUnitLv())
                {
                    InitBaseData(mProp.ServerData.Prop);
                }

                mProp.ServerData.BaseInfo = info;
                SyncPlayerVisibleData();

                PlayerBaseInfoChangeEventB2C evt = new PlayerBaseInfoChangeEventB2C();
                evt.info = info;
                this.mUnit.Level = info.UnitLv;
                this.mUnit.queueEvent(evt);
            }
        }

        protected void SyncPlayerVisibleData()
        {
            if (this.mUnit.VisibleInfo is PlayerVisibleDataB2C pvd)
            {
                pvd.BaseInfo = this.mProp.ServerData.BaseInfo;
                pvd.AvatarMap = this.mProp.ServerData.AvatarMap;
                pvd.UnitPKInfo = this.mProp.ServerData.UnitPKInfo;
            }
        }

        public void OnTitleChanged(int TitleID, string TitleNameExt = "")
        {
            this.mProp.ServerData.BaseInfo.TitleID = TitleID;
            if(!string.IsNullOrEmpty(TitleNameExt))
            {
                this.mProp.ServerData.BaseInfo.TitleNameExt = TitleNameExt;
            } 
            SyncPlayerVisibleData();


            PlayerPropChangeB2C b2c = new PlayerPropChangeB2C();
            b2c.changes = new HashMap<string, int>();
            b2c.changes.Put(PlayerPropChangeB2C.TitleID, TitleID);
            // 只同步改变，不同步为空。
            if (!string.IsNullOrEmpty(TitleNameExt))
            {
                b2c.changeExts = new HashMap<string, string>();
                b2c.changeExts.Put(PlayerPropChangeB2C.TitleNameExt, TitleNameExt);
            }
             
            this.mUnit.queueEvent(b2c);
        }

        public void OnUnitPropChange(TLUnitProp prop)
        {
            if (PropModule != null)
            {
                PropModule.PropChange(prop);
            }
        }

        /// <summary>
        /// 战斗状态变更.
        /// </summary>
        public void OnGuardStatusChange()
        {
            var info = GetBaseInfo();
            info.IsGuard = (this.mUnit as Units.TLInstancePlayer).IsGuard;

            //同步可见数据.
            SyncPlayerVisibleData();
            //向客户端同步.
            SendGuardStatusChangeNotify();
        }

        private void SendGuardStatusChangeNotify()
        {
            var evt = new GuardStatusChangeEventB2C();
            evt.b2c_guard = (this.mUnit as Units.TLInstancePlayer).IsGuard;
            this.mUnit.queueEvent(evt);
        }

        public void OnPetChange(PetData data)
        {
            ChangePet(data);
        }

        public SaveBattleRoleInfoNotify GetBattleRoleInfo()
        {
            SaveBattleRoleInfoNotify ret = new SaveBattleRoleInfoNotify();
            ret.pkInfo = GetPKStampInfo();
            ret.skillInfo = SkillModule.GetSkillStatusData();
            ret.curHP = this.mUnit.CurrentHP;
            ret.buffInfo = GetBuffSnaps();
            ret.curAnger = this.mUnit.CurrentMP;
            return ret;
        }

        public void OnLogicPropChange(PlayerPropChangeR2B msg)
        {
            if (msg.changes == null)
                return;

            int hasChanges = 0;

            if (msg.changes.ContainsKey(PlayerPropChangeR2B.PracticeLv))
            {
                GetBaseInfo().PracticeLv = msg.changes.Get(PlayerPropChangeR2B.PracticeLv);
                hasChanges++;
            }

            if (hasChanges > 0)
            {
                SyncPlayerVisibleData();

                PlayerPropChangeB2C b2c = new PlayerPropChangeB2C();
                b2c.changes = msg.changes;
                this.mUnit.queueEvent(b2c);
            }

        }


        #endregion

        #region 技能. 

        public void OnSkillChange(SkillChangeEventR2B msg)
        {
            switch (msg.operateType)
            {
                case SkillChangeEventR2B.Operate.Add:
                    DataAddSkill(msg.SkillList, true);
                    break;
                case SkillChangeEventR2B.Operate.Remove:
                    DataRemoveSkill(msg.SkillList, true);
                    break;
                case SkillChangeEventR2B.Operate.Replace:
                    this.SkillModule.ReplaceSkill(msg.SkillList);
                    break;
                case SkillChangeEventR2B.Operate.Reset:
                    mProp.ServerData.SkillInfo.Skills = msg.SkillList;
                    if (InGodStatus() == false)
                        this.SkillModule.ResetSkill(msg.SkillList);
                    break;
            }

            if (InGodStatus() == false)
                SendClientSkillPanelInfo();
        }

        public override void DataAddSkill(List<GameSkill> lt, bool syncSkillModule)
        {
            List<GameSkill> l = new List<GameSkill>();
            //避免重复添加技能.
            for (int i = 0; i < lt.Count; i++)
            {
                if (this.SkillModule.GetGameSkill(lt[i].SkillID) == null)
                {
                    l.Add(lt[i]);
                }
                else//添加已拥有的技能强制激活.
                {
                    this.mUnit.SetSkillActive(lt[i].SkillID, true);
                }
            }

            var src = mProp.ServerData.SkillInfo.Skills;
            src.AddRange(l);
            if (syncSkillModule && InGodStatus() == false)
            {
                this.SkillModule.AddSkill(l);
            }
        }

        public override void DataRemoveSkill(List<GameSkill> lt, bool syncSkillModule)
        {
            var src = mProp.ServerData.SkillInfo.Skills;

            List<GameSkill> rmlist = new List<GameSkill>();

            for (int i = 0; i < lt.Count; i++)
            {
                for (int k = 0; k < src.Count; k++)
                {
                    if (lt[i].SkillID == src[k].SkillID)
                    {
                        rmlist.Add(src[k]);
                    }
                }
            }

            for (int i = 0; i < rmlist.Count; i++)
            {
                src.Remove(rmlist[i]);
            }

            if (syncSkillModule && InGodStatus() == false)
            {
                this.SkillModule.RemoveSkill(lt);
            }
        }

        public void DataResetSkill(List<GameSkill> lt, bool syncSkillModule)
        {
            mProp.ServerData.SkillInfo.Skills = lt;
            if (syncSkillModule && InGodStatus() == false)
            {
                this.SkillModule.ResetSkill(lt);
            }
        }

        private void MockSkillChange()
        {
            //MOCK测试技能变更.
            List<GameSkill> list = new List<GameSkill>();
            GameSkill gs = new GameSkill();
            gs.SkillID = 14040;
            gs.SkillType = GameSkill.TLSkillType.active;
            gs.SkillLevel = 99;
            list.Add(gs);
            //  this.SkillModule.AddSkill(list);
            //  this.SkillModule.RemoveSkill(list);
            //  this.SkillModule.ReplaceSkill(list);
            //  this.SkillModule.ResetSkill(list);
        }

        internal override bool TryLaunchSkill(InstanceUnit.SkillState skill, ref InstanceUnit.LaunchSkillParam param)
        {
            TakeOffMount();

            var gs = SkillModule.GetGameSkill(skill.Data.ID);
            if (gs.SkillType == GameSkill.TLSkillType.God && AllowUseAutoGuard() == false)
            {
                return false;
            }

            return base.TryLaunchSkill(skill, ref param);
        }

        public void SendClientSkillPanelInfo()
        {
            //主动技能 + 神器技能.
            var skillstatust = this.mUnit.SkillStatus;
            List<GameSkill> ret = new List<GameSkill>();
            GameSkill gs = null;

            foreach (var item in skillstatust)//当前激活的技能.
            {
                if (item.IsActive)
                {
                    gs = this.SkillModule.GetGameSkill(item.ID);
                    if (gs.SkillType != GameSkill.TLSkillType.hideActive)//隐藏技能不向客户端推送.
                        ret.Add(gs);
                }
            }

            List<GameSkill> SkillSlotLt = new List<GameSkill>();
            bool inGodStatus = InGodStatus();
            int slotCount = 8;

            for (int i = 0; i < slotCount; i++)
            {
                gs = new GameSkill();
                gs.SkillIndex = i;
                gs.SkillID = -1;//代表不显示.
                SkillSlotLt.Add(gs);
            }

            //变身状态无视技能栏锁定设置.
            //非变身状态,默认将技能开放等级赋值.
            if (inGodStatus == false)
            {
                var slotLt = SkillSlots();
                SkillSlot ss = null;
                if (slotLt != null && slotLt.Count != 0)
                {
                    for (int i = 0; i < slotLt.Count; i++)
                    {
                        ss = slotLt[i];
                        gs = SkillSlotLt[ss.Index];
                        gs.SkillLevel = ss.OpenLv;
                        gs.SkillID = -2;//未解锁.
                    }
                }
            }

            //已经有的数据将覆盖技能栏数据.
            GameSkill slot = null;
            int index = 1;
            for (int i = 0; i < ret.Count; i++)
            {
                gs = ret[i];

                if (gs.SkillType == GameSkill.TLSkillType.God)
                {
                    slot = SkillSlotLt[SkillSlotLt.Count - 1];
                    slot.SkillID = gs.SkillID;
                    slot.SkillLevel = gs.SkillLevel;
                    slot.SkillType = gs.SkillType;
                }
                else
                {
                    if (gs.SkillIndex < SkillSlotLt.Count)
                    {
                        slot = SkillSlotLt[gs.SkillIndex];
                        slot.SkillID = gs.SkillID;
                        slot.SkillLevel = gs.SkillLevel;
                        slot.SkillType = gs.SkillType;
                        index++;
                    }
                }
            }

            SyncSkillInfo(SkillSlotLt, GetGodBuffID());
        }

        /// <summary>
        /// 同步技能信息.
        /// </summary>
        /// <param name="list"></param>
        private void SyncSkillInfo(List<GameSkill> list, int buffID)
        {
            var evt = new PlayerSkillInfoEventB2C();
            evt.SkillList = list;
            evt.BuffID = buffID;
            mUnit.queueEvent(evt);
        }

        private List<SkillSlot> SkillSlots()
        {
            List<SkillSlot> ret = null;
            if (mProp.ServerData.SkillInfo != null)
            {
                ret = mProp.ServerData.SkillInfo.Slots;
            }

            return ret;
        }

        #endregion

        #region 组队.

        public void OnReceiveTeamInfoChange(TeamInfoChangeEventR2B msg)
        {
            var d = this.mProp.ServerData.UnitTeamData;
            if (d != null)
            {
                d.MemberList = msg.TeamList;
                d.TeamLeaderUUID = msg.TeamLeader;
                d.TeamUUID = msg.TeamUUID;
            }

            InitTeamMap(mTeamMap, msg.TeamList, msg.TeamLeader, msg.TeamUUID);
        }

        public void SyncMsgToTeamMember()
        {
            if (this.mTeamMap != null)
            {
                foreach (var item in mTeamMap)
                {
                    var player = this.mUnit.Parent.getPlayerByUUID(item.Key);
                    //TODO
                    //SEND TEAMSYNC INFO.

                }
            }
        }

        public HashMap<string, TeamMemberSnap> TeamMap()
        {
            return mTeamMap;
        }

        public string TeamLeaderUUID()
        {
            if (mProp.ServerData.UnitTeamData != null)
                return mProp.ServerData.UnitTeamData.TeamLeaderUUID;

            return null;
        }

        public string TeamUUID()
        {
            if (mProp.ServerData.UnitTeamData != null)
                return mProp.ServerData.UnitTeamData.TeamUUID;

            return null;
        }

        #endregion

        #region PK模式变更.

        /// <summary>
        /// PK模式变更.
        /// </summary>
        /// <param name="msg"></param>
        public void OnPKModeChanged(PKModeChangeEventR2B msg)
        {
            SetPKMode(msg);
            SyncPlayerVisibleData();
            SendPKModeChangeEventB2C();
        }

        /// <summary>
        /// 通知客户端PK模式变更.
        /// </summary>
        private void SendPKModeChangeEventB2C()
        {
            var evt = new PKModeChangeEventB2C();
            evt.mode = GetPKMode();
            this.mUnit.queueEvent(evt);
        }

        /// <summary>
        /// 通知客户端红名等级变更.
        /// </summary>
        private void SendPKInfoChangeEventB2C()
        {
            var evt = new PKInfoChangeEventB2C();

            var info = GetPKInfo();

            evt.b2c_level = info.CurPKLevel;
            evt.b2c_pkvalue = info.CurPKValue;
            evt.b2c_color = info.CurColor;

            this.mUnit.queueEvent(evt);

            SendPKValueChangeFlag = false;
        }

        /// <summary>
        /// PK值变更.
        /// </summary>
        private void SendPKValueChangeEventB2C()
        {
            var evt = new PKValueChangeEventB2C();
            evt.b2c_pkvalue = GetPKInfo().CurPKValue;
            this.mUnit.queueEvent(evt);
            SendPKValueChangeFlag = false;
        }

        /// <summary>
        /// 设置PK模式.
        /// </summary>
        /// <param name="msg"></param>
        private void SetPKMode(PKModeChangeEventR2B msg)
        {
            if (mProp != null)
            {
                mProp.ServerData.UnitPKInfo.CurPKMode = msg.mode;
            }
        }

        public override bool IsAllies(TLVirtual target, bool includeSelf = true)
        {
            if (target is TLVirtual_Player)
            {
                //判断PK模式.
                //判断队伍.
                var mode = GetPKMode();

                //自己或者宠物、召唤物判断.
                if (target.GetPlayerUUID() == this.GetPlayerUUID())
                {
                    if (target == this)
                    {
                        if (includeSelf) return true;
                        else return false;
                    }
                    return true;
                }

                switch (mode)
                {
                    case PKInfo.PKMode.Team:
                    case PKInfo.PKMode.All:
                    case PKInfo.PKMode.Peace:
                    case PKInfo.PKMode.Guild://不是队伍、公会成员.
                    case PKInfo.PKMode.Justice:
                    case PKInfo.PKMode.Revenger:
                        if (IsTeamMember(target))
                            return true;
                        return false;
                    default:
                        return false;
                }
            }
            else
            {
                //怪物、NPC、建筑物.
                return base.IsAllies(target);
            }
        }

        public override bool IsEnemy(TLVirtual target)
        {
            if (target is TLVirtual_Player)
            {
                //自己或自己的宠物，召唤物不可能为敌人.
                if (target.GetPlayerUUID() == this.GetPlayerUUID())
                {
                    return false;
                }

                if (!IsTeamMember(target) && CheckGuildChase(target))
                {
                    return true;
                }

                //判断PK模式.
                //判断队伍.
                var mode = GetPKMode();

                switch (mode)
                {
                    case PKInfo.PKMode.Peace:
                        return (base.IsEnemy(target));
                    case PKInfo.PKMode.Guild://不是队伍、公会成员.
                        if (IsTeamMember(target) || IsGuildMember(target))
                            return false;
                        return true;
                    case PKInfo.PKMode.Team:
                    case PKInfo.PKMode.All:
                        return !IsTeamMember(target);
                    case PKInfo.PKMode.Justice:
                        if (IsTeamMember(target))
                            return false;
                        return (target.IsRedName());
                    case PKInfo.PKMode.Revenger://复仇模式只能攻击仇人和红名.
                        if (IsTeamMember(target))
                            return false;
                        return (this.InRevengeList(target.GetPlayerUUID()));
                    default:
                        return true;
                }
            }
            else
            {
                //怪物、NPC、建筑物.
                return base.IsEnemy(target);
            }
        }

        public override bool IsTeamMember(TLVirtual target)
        {
            if (mTeamMap == null || target == null) return false;
            string playerUUID = target.GetPlayerUUID();
            if (string.IsNullOrEmpty(playerUUID))
            {
                log.Error("TeamMemberFind null UUID, Virtual Type is" + target.GetType()?.ToString());
                return false;
            }

            return mTeamMap.ContainsKey(playerUUID);
        }

        public override bool IsTeamMember(uint objID)
        {
            if (mTeamMap == null) return false;
            var unit = mUnit.Parent.getUnit(objID);
            if (unit == null) return false;
            var v = (unit.Virtual as TLVirtual).GetPlayerUnit();
            return IsTeamMember(v);
        }

        protected override bool IsGuildMember(TLVirtual target)
        {
            string id1 = GuildUUID();
            string id2 = target.GuildUUID();

            if (string.IsNullOrEmpty(id1) || string.IsNullOrEmpty(id2))
                return false;

            return (id1 == id2);
        }

        public override bool IsRedName()
        {
            return (GetPKLevel() > 1);
        }

        /// <summary>
        /// 红名
        /// </summary>
        /// <returns></returns>
        public bool IsRealRedName()
        {
            return (GetPKLevel() > 2);
        }

        public override int GetPKLevel()
        {
            if (mProp != null && mProp.ServerData.UnitPKInfo != null)
            {
                return mProp.ServerData.UnitPKInfo.CurPKLevel;
            }

            return base.GetPKLevel();
        }

        protected override PKInfo.PKMode GetPKMode()
        {
            if (mProp != null && mProp.ServerData.UnitPKInfo != null)
            {
                return mProp.ServerData.UnitPKInfo.CurPKMode;
            }

            return base.GetPKMode();
        }

        protected override int GetPKValue()
        {
            if (mProp != null && mProp.ServerData.UnitPKInfo != null)
            {
                return mProp.ServerData.UnitPKInfo.CurPKValue;
            }

            return base.GetPKValue();
        }

        protected override PKInfo GetPKInfo()
        {
            if (mProp != null && mProp.ServerData != null)
            {
                return mProp.ServerData.UnitPKInfo;
            }

            return null;
        }

        #endregion

        #region 复活.

        public void PlayerRebirth(RebirthEventR2B evt)
        {
            TLEditorScene es = this.mUnit.Parent as TLEditorScene;

            switch (evt.type)
            {
                case RebirthEventR2B.RebirthType.Insitu:
                    this.mUnit.startRebirth(this.mUnit.MaxHP, 1);
                    break;
                case RebirthEventR2B.RebirthType.StartRegion:
                    ZoneRegion zr = es.GetEditStartRegion(this.mUnit.Force);

                    if (zr != null)
                        this.mUnit.transport(zr.X, zr.Y);
                    else
                        log.WarnFormat("无法找到场景ID为{0}阵营为{1}的出生点", es.Data.ID, this.mUnit.Force);

                    this.mUnit.startRebirth(this.mUnit.MaxHP, 1);
                    break;
                case RebirthEventR2B.RebirthType.RebirthPoint:
                    this.mUnit.startRebirth(this.mUnit.MaxHP, 1);
                    var region = es.GetNearestRebirthRegion(this.mUnit.X, this.mUnit.Y, this.mUnit.Force);

                    if (region != null)
                        this.mUnit.transport(region.X, region.Y);
                    else
                        log.WarnFormat("无法找到场景ID为{0}阵营为{1}的复活点", es.Data.ID, this.mUnit.Force);

                    break;
                case RebirthEventR2B.RebirthType.TransMainCity:
                    break;
            }

        }

        #endregion

        #region 杀戮值计算.

        internal override void OnUnitDead(TLVirtual dead, TLVirtual killer)
        {
            base.OnUnitDead(dead, killer);
        }

        private int ChangePKValue(int delta)
        {
            var srcV = GetPKValue();
            var curV = srcV;

            if (delta == 0)
                return curV;

            var curLv = GetPKLevel();

            var pkTemplateData = TLDataMgr.GetInstance().PKValueData.GetData(curLv);

            if (pkTemplateData == null) return curV;

            curV = curV + delta;

            curV = Math.Max(0, curV);
            curV = Math.Min(pkTemplateData.point_max, curV);

            var info = GetPKInfo();
            info.CurPKValue = curV;

            var newLv = TLBattle.Plugins.TLDataMgr.GetInstance().PKValueData.GetLv(curV);
            if (newLv != curLv)
            {
                info.CurPKLevel = newLv;
                info.CurColor = TLDataMgr.GetInstance().PKValueData.GetColor(newLv);
                OnPKLevelChange(newLv);
            }
            else
            {
                if (srcV != curV)
                    OnPKValueChange();
            }

            return curV;
        }

        private void OnPKValueChange()
        {
            SyncPlayerVisibleData();
            SendPKValueChangeFlag = true;
        }

        private void OnPKLevelChange(int newLv)
        {
            SyncPlayerVisibleData();
            SendPKInfoChangeEventB2C();
            InitPKValueTimer(newLv);
        }

        public void CalPKLevelChange(TLVirtual dead, TLVirtual killer)
        {
            if (dead.GetPlayerUUID() == killer.GetPlayerUUID())
                return;

            if (dead is TLVirtual_Player)
            {
                if (CalPKValue())
                {
                    var v = dead as TLVirtual_Player;

                    //对方非红名,且不是仇人.
                    if (v.IsRedName() == false &&
                        v.InRevengeList(v.GetPlayerUUID()) == false &&
                        v.InGuildChaseList(v.GuildUUID()) == false)
                    {
                        //击杀直接升级为红名.
                        var pklv = TLDataMgr.GetInstance().PKValueData.RedNameLv();
                        GetPKInfo().CurPKLevel = pklv;
                        GetPKInfo().CurColor = TLDataMgr.GetInstance().PKValueData.GetColor(pklv);

                        var pkData = TLDataMgr.GetInstance().PKValueData.GetData(pklv);

                        if (pkData != null)
                        {
                            //加杀戮值.
                            this.ChangePKValue(pkData.kill_point);
                        }

                        OnPKLevelChange(pklv);

                        List<string> p = new List<string>();
                        p.Add(pkData.kill_point.ToString());
                        SendMsgToClient(TLCommonConfig.TIPS_KILL_WHITE_NAME_PLAYER, p);
                    }
                    else
                    {
                        int lv = v.GetPKLevel();
                        if (lv == 2)
                            SendMsgToClient(TLCommonConfig.TIPS_KILL_GRAY_NAME_PLAYER);
                        else
                            SendMsgToClient(TLCommonConfig.TIPS_KILL_RED_NAME_PLAYER);
                    }
                }
            }
        }

        internal override void ChangeCombatStateOnHitOther(TLVirtual hitter, bool isHarmful)
        {
            //对非红名玩家或该玩家的宠物造成伤害.
            if (CalPKValue() &&
                isHarmful &&
                hitter.IsPlayerUnit() &&
                hitter.IsRedName() == false &&
                this.InRevengeList(hitter.GetPlayerUUID()) == false &&
                InGuildChaseList(hitter.GuildUUID()) == false)
            {
                //加杀戮值.
                this.ChangePKValue(mRedNameAttackPoint);
            }


            base.ChangeCombatStateOnHitOther(hitter, isHarmful);
        }

        protected override void OnCombatStateChange(CombatStateChangeEventB2C.BattleStatus status)
        {
            base.OnCombatStateChange(status);
            if (status == CombatStateChangeEventB2C.BattleStatus.None)
            {
                mAutoChangePKMode = false;
            }
        }

        private void CheckPKInfo()
        {
            //每2秒update.
            mSendPKValueTimer = new TimeInterval<int>(2000);

            var info = GetPKInfo();

            //红名颜色.
            InitPKValueTimer(info.CurPKLevel);

        }

        private void InitPKValueTimer(int pkLv)
        {
            var data = TLDataMgr.GetInstance().PKValueData.GetData(pkLv);
            if (data == null) return;
            mRedNameAttackPoint = data.point_attack;

            int v = GetPKValue();

            if (v == 0)
            {
                mPKValueTimer = null;
                return;
            }


            int t = TLDataMgr.GetInstance().GameConfigData.GetRedNameReduceTime() * 1000;

            if (mPKValueTimer == null)
            {
                mPKValueTimer = new TimeInterval<int>(t);
                mPKValueTimer.FirstTimeEnable = false;
            }
            else
            {
                mPKValueTimer.Reset();
            }
        }

        private void UpdatePKValueTimer(int intervalMS)
        {
            if (mPKValueTimer != null && mPKValueTimer.Update(intervalMS))
            {
                int k = -TLDataMgr.GetInstance().GameConfigData.GetRedNameReduceValue();
                ChangePKValue(k);


            }

            if (mSendPKValueTimer != null && mSendPKValueTimer.Update(intervalMS))
            {

                if (SendPKValueChangeFlag)
                {
                    SendPKValueChangeEventB2C();
                }
            }
        }

        public PKInfo GetPKStampInfo()
        {
            var ret = GetPKInfo();
            return ret;
        }

        private void CheckTeamInfo()
        {
            mTeamMap = new HashMap<string, TeamMemberSnap>();
            if (mProp != null && mProp.ServerData != null)
            {
                var data = mProp.ServerData.UnitTeamData;
                if (data != null)
                    InitTeamMap(mTeamMap, data.MemberList, data.TeamLeaderUUID, data.TeamUUID);
            }
        }

        private void CheckRevengeInfo()
        {
            RevengeMap = new HashMap<string, int>();
            InitRevengeList(RevengeMap, RevengeList);
        }

        private List<string> RevengeList
        {
            get
            {
                List<string> ret = null;
                if (mProp != null)
                {
                    ret = mProp.ServerData.RevengeList;
                }
                return ret;
            }
            set
            {
                if (mProp != null && mProp.ServerData != null)
                {
                    mProp.ServerData.RevengeList = value;
                }
            }
        }

        public void OnRevengeListChange(RevengeListChangeEventR2B evt)
        {
            switch (evt.op)
            {
                case RevengeListChangeEventR2B.OpType.Add:
                    if (evt.list != null)
                    {
                        RevengeList.AddRange(evt.list);
                        InitRevengeList(RevengeMap, RevengeList);
                    }
                    break;
                case RevengeListChangeEventR2B.OpType.Remove:
                    if (evt.list != null)
                    {
                        var lt = RevengeList;
                        for (int i = 0; i < evt.list.Count; i++)
                        {
                            lt.Remove(evt.list[i]);
                        }
                        InitRevengeList(RevengeMap, RevengeList);
                    }
                    break;
                case RevengeListChangeEventR2B.OpType.Reset:
                    RevengeList = evt.list;
                    InitRevengeList(RevengeMap, RevengeList);
                    break;
                default:
                    break;
            }
        }

        public void OnPlayerReady()
        {

        }

        protected override void UnitBuff_OnBuffBegin(InstanceUnit.BuffState buff, TLVirtual sender)
        {
            base.UnitBuff_OnBuffBegin(buff, sender);

            //如果是变身，则打断坐骑状态.
            if (buff.Data.MakeAvatar)
            {
                TakeOffMount();
            }
        }

        #endregion

        #region 宠物.

        private void MockPet()
        {
            PetData pd = new PetData();
            pd.BaseInfo = new PetBaseInfo();

            pd.UnitProp = new TLUnitProp();
            pd.UnitProp.MaxHP = 1000;
            pd.UnitProp.CurHP = 1000;

            pd.SkillInfo = new TLUnitSkillInfo();
            pd.SkillInfo.Skills = new List<GameSkill>();
            var gs = new GameSkill();
            gs.SkillID = 330061;
            gs.SkillLevel = 1;
            gs.SkillType = GameSkill.TLSkillType.normalAtk;


            pd.SkillInfo.Skills.Add(gs);
            ChangePet(pd);
        }

        private void ChangePet(PetData data)
        {
            DismissPet();

            SummonPet(data);
        }

        private void DismissPet()
        {
            if (mPet != null)
            {
                mPet.mUnit.OnDead -= Pet_OnDead;
                this.mUnit.Parent.RemoveObjectByID(mPet.mUnit.ID);
                mPet = null;
            }
        }

        private void SummonPet(PetData data)
        {
            if (data == null) return;

            //添加宠物.
            UnitInfo info = TLBattleSkill.GetUnitInfo(data.EditorID);

            if (info == null) return;

            var pos = mUnit.Parent.PathFinder.Terrain.FindNearRandomMoveableNode(
                  mUnit.RandomN,
                      mUnit.X,
                      mUnit.Y,
                      2, true);

            TLUnitProperties prop = info.Properties as TLUnitProperties;

            prop.ServerData.BaseInfo.UnitLv = data.BaseInfo.level;
            prop.ServerData.BaseInfo.Name = data.BaseInfo.name;

            prop.ServerData.Prop = data.UnitProp;
            prop.ServerData.SkillInfo = data.SkillInfo;

            var evt = new AddUnit();
            {
                evt.info = info;
                evt.editor_name = info.Name;
                evt.player_uuid = info.Name;
                evt.force = mUnit.Force;
                evt.level = data.BaseInfo.level;
                evt.pos = pos;
                evt.direction = mUnit.Direction;
                evt.summoner = mUnit;
            }

            var unit = mUnit.Parent.AddUnit(evt) as TLInstancePet;
            unit.OnDead += Pet_OnDead;
            unit.SetRebirthTime(data.RebirthTimeMS);

            if (unit != null)
            {
                var petVirtual = (unit.Virtual) as TLVirtual_Pet;
                mPet = petVirtual;
                data.BaseInfo.MasterID = this.mUnit.ID;
                mPet.InitVisibleData(data.BaseInfo);
                mPet.SetMaster(this);
            }
        }

        private void Pet_OnDead(InstanceUnit unit, InstanceUnit attacker)
        {
            //通知逻辑服仙侣死亡.
            //变更仙侣状态.
        }

        //宠物基础属性变更.
        public void UpdatePetBaseInfo(PetBaseInfo info)
        {
            //基础信息更新.
            if (mPet != null)
            {
                mPet.UpdateBaseInfo(info);
            }
        }

        public void UpdatePetPropInfo(TLUnitProp prop)
        {
            if (mPet != null)
            {
                mPet.UpdatePetProps(prop);
            }
        }

        public override HashMap<int, GameSkill> GetGodSkill()
        {
            HashMap<int, GameSkill> ret = new HashMap<int, GameSkill>();
            var map = GetGodData().SkillInfo;
            foreach (var item in map)
            {
                if (item.Value.SkillType != GameSkill.TLSkillType.God)
                {
                    ret.Add(item.Key, item.Value);
                }
            }
            //MOCK.
            /*
            var gs = new GameSkill();
            gs.SkillID = 201;
            gs.SkillLevel = 1;
            gs.SkillType = GameSkill.TLSkillType.normalAtk;
            ret.Add(gs.SkillID, gs);
            gs = new GameSkill();
            gs.SkillID = 202;
            gs.SkillLevel = 1;
            gs.SkillType = GameSkill.TLSkillType.active;
            ret.Add(gs.SkillID, gs);
            gs = new GameSkill();
            gs.SkillID = 203;
            gs.SkillLevel = 1;
            gs.SkillType = GameSkill.TLSkillType.active;
            ret.Add(gs.SkillID, gs);
            */
            return ret;
        }

        public override List<GameSkill> GetAllSkillData()
        {
            var lt = base.GetAllSkillData();
            var gs = GetGodMainSkill();
            if (gs != null)
                lt.Add(gs);
            return lt;
        }
        #endregion

        #region 仙侣.

        private void MockGod()
        {
            HashMap<int, GameSkill> ret = new HashMap<int, GameSkill>();
            var gs = new GameSkill();
            gs.SkillID = 400001;
            gs.SkillType = GameSkill.TLSkillType.God;
            ret.Add(gs.SkillID, gs);
            gs = new GameSkill();
            gs.SkillID = 201;
            gs.SkillLevel = 1;
            gs.SkillType = GameSkill.TLSkillType.normalAtk;
            ret.Add(gs.SkillID, gs);
            gs = new GameSkill();
            gs.SkillID = 202;
            gs.SkillLevel = 1;
            gs.SkillType = GameSkill.TLSkillType.active;
            ret.Add(gs.SkillID, gs);
            gs = new GameSkill();
            gs.SkillID = 203;
            gs.SkillLevel = 1;
            gs.SkillType = GameSkill.TLSkillType.active;
            ret.Add(gs.SkillID, gs);
            var d = new GodData();
            d.SkillInfo = ret;
            d.BuffID = 400001;
            this.mProp.ServerData.UnitGodData = d;
        }
        private GodData GetGodData()
        {
            return mProp.ServerData.UnitGodData;
        }
        public void UpdateGodSkillInfo(HashMap<int, GameSkill> info)
        {
            var d = GetGodData();

            if (d != null && d.SkillInfo != null)
            {
                d.SkillInfo = info;
            }

            if (info != null)
            {
                List<GameSkill> lt = new List<GameSkill>();

                foreach (var item in info)
                {
                    lt.Add(item.Value);
                }

                SkillModule.ReplaceSkill(lt);
            }
        }
        public void InitGodSkill(GodData data)
        {
            this.mProp.ServerData.UnitGodData = data;
        }
        public override GameSkill GetGodMainSkill()
        {
            var data = GetGodData();
            if (data != null && data.SkillInfo != null)
            {
                foreach (var item in data.SkillInfo)
                {
                    if (item.Value.SkillType == GameSkill.TLSkillType.God)
                    {
                        return item.Value;
                    }
                }
            }

            return null;
        }
        public void ChangeGod(GodData god)
        {
            //战斗状态，切换仙侣无效,客户端做屏蔽.
            if (this.mProp.ServerData.UnitGodData != null &&
                this.GetCombatState() != CombatStateChangeEventB2C.BattleStatus.None)
            {
                return;
            }

            if (this.mProp.ServerData.UnitGodData == null && god == null)
            {
                return;
            }

            //仙侣变更 技能需要移除.
            //可能出现没有仙侣.
            var gs = this.GetGodMainSkill();
            if (gs != null)//旧的技能冻结.
            {
                this.mUnit.SetSkillActive(gs.SkillID, false, false);
            }

            this.mProp.ServerData.UnitGodData = god;
            gs = this.GetGodMainSkill();
            if (gs != null)
            {
                var st = this.mUnit.getSkill(gs.SkillID);
                if (st == null)
                {
                    this.SkillModule.AddSkill(gs);
                }
                else
                {
                    this.mUnit.SetSkillActive(gs.SkillID, true);
                }
            }

            SendClientSkillPanelInfo();
        }
        private int GetGodBuffID()
        {
            if (mProp.ServerData != null && mProp.ServerData.UnitGodData != null)
            {
                return mProp.ServerData.UnitGodData.BuffID;
            }

            return 0;
        }

        #endregion

        #region BUFF.

        private void InitBattleBuffInfo(List<BuffSnap> list)
        {
            AddTLBuff(this, list);
        }

        public void OnReceiveAddActorSpeedUpBuff(ActorAddSpeedUpBuffRequest req)
        {
            var lv = TLDataMgr.GetInstance().GameConfigData.GetQuestSpeedUpLvLimit();
            if (GetUnitLv() <= lv)
            {
                var buffid = TLDataMgr.GetInstance().GameConfigData.GetQuestSpeedUpBuffID();
                this.AddTLBuff(buffid, this);
            }
        }

        public void OnReceiveRemoveActorSpeedUpBuff(ActorRemoveSpeedUpBuffRequest req)
        {
            var lv = TLDataMgr.GetInstance().GameConfigData.GetQuestSpeedUpLvLimit();
            if (GetUnitLv() <= lv)
            {
                var buffid = TLDataMgr.GetInstance().GameConfigData.GetQuestSpeedUpBuffID();
                this.RemoveBuff(buffid, "OnReceiveRemoveActorSpeedUpBuff");
            }
        }

        public void OnReceivePlayerAddBuffEvtR2B(PlayerAddBuffEvtR2B msg)
        {
            if (msg.r2b_buffID != 0)
            {
                if (msg.r2b_buffData == null)
                {
                    AddTLBuff(msg.r2b_buffID, this);
                }
                else
                {
                    //TODO 高级扩展 .logic自定义BUFFDATA.
                }
            }
        }

        #endregion

        #region 吃药.

        public void OnTriggerMedicineEffect(TriggerMedicineEffectR2B evt)
        {
            switch (evt.medicineType)
            {
                case TriggerMedicineEffectR2B.MedicineType.HP:
                    switch (evt.arg1)
                    {
                        case 1:
                            AddHP(evt.arg2, this.mUnit, true);
                            //播放特效.
                            this.mUnit.UseItem(9999);
                            break;
                        case 2:
                            AddHPPct((int)(evt.arg2 / 100.0f), this.mUnit, true);
                            //播放特效.
                            this.mUnit.UseItem(9999);
                            break;
                    }
                    break;
                case TriggerMedicineEffectR2B.MedicineType.Atk:
                    //TODO.
                    break;
                case TriggerMedicineEffectR2B.MedicineType.Def:
                    //TODO.
                    break;
                case TriggerMedicineEffectR2B.MedicineType.Hot:
                    TLBuffData_HPChange bd = new TLBuffData_HPChange();
                    bd.ChangeValueType = 0;
                    bd.ChangeValue = (int)(evt.arg2);
                    var ub = SkillHelper.Instance.GetUnitBuff(bd);
                    this.AddTLBuff(10001, this, ub);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 添加跟随单位.

        public InstanceUnit AddFollowUnit(int templateID, bool playerOnly = true, float x = 0, float y = 0)
        {
            //添加宠物.
            UnitInfo info = TLBattleSkill.GetUnitInfo(templateID);
            if (info == null) return null;

            //uint id = 0;
            //if (FollowUnitMap.TryGetValue(templateID, out id))
            //{
            //    return this.mUnit.Parent.getUnit(id);
            //}
            DeepCore.Vector.Vector2 pos;
            if (x == y & y == 0)
            {
                pos = mUnit.Parent.PathFinder.Terrain.FindNearRandomMoveableNode(mUnit.RandomN, mUnit.X, mUnit.Y, 2, true);
                if (pos == null)
                    pos = new DeepCore.Vector.Vector2(mUnit.X, mUnit.Y);
            }
            else
            {
                pos = new DeepCore.Vector.Vector2(x, y);
            }

            var evt = new AddUnit();
            {
                evt.info = info;
                evt.editor_name = info.Name;
                evt.player_uuid = info.Name;
                evt.force = mUnit.Force;
                evt.level = 0;
                evt.pos = pos;
                evt.direction = mUnit.Direction;
                evt.summoner = mUnit;
            }

            var unit = mUnit.Parent.AddUnit(evt);

            //if (unit != null)
            //{
            //    AddFollowUnitMap(unit.Info.ID, unit.ID);
            //}

            return unit;
        }

        public void RemoveFollowUnit(int templateID)
        {
            /*
            uint id = FollowUnitMap.RemoveByKey(templateID);
            if (id != 0)
            {
                this.mUnit.Parent.RemoveObjectByID(id);
            }
            */
        }

        private void AddFollowUnitMap(int templateID, uint instanceID)
        {
            /*
            FollowUnitMap.Add(templateID, instanceID);
            */
        }

        #endregion

        #region 发送同场景内的玩家UUID.

        //同场景内的玩家UUID.
        public GetZonePlayersUUIDResponse DoGetZonePlayersUUIDRequest(GetZonePlayersUUIDRequest req)
        {
            GetZonePlayersUUIDResponse rsp = new GetZonePlayersUUIDResponse();

            var list = mUnit.Parent.AllPlayers;

            if (list != null)
            {
                List<string> ret = new List<string>();

                foreach (var item in list)
                {
                    ret.Add(item.PlayerUUID);
                }
                rsp.b2c_list = ret;
            }
            return rsp;
        }

        #endregion

        #region PK值变更.

        public void OnReceivePKValueChangeEventR2B(PKValueChangeEventR2B evt)
        {
            ChangePKValue(evt.changeValue);
        }

        #endregion

        #region 脱离卡死.

        public void OnEscapeUnmoveable()
        {
            TLEditorScene es = this.mUnit.Parent as TLEditorScene;
            ZoneRegion zr = es.GetEditStartRegion(this.mUnit.Force);

            if (zr != null)
                this.mUnit.transport(zr.X, zr.Y);
            else
                log.WarnFormat("脱离地图失败,无法找到场景ID为{0}阵营为{1}的出生点", es.Data.ID, this.mUnit.Force);
        }

        #endregion

        #region 穴位.

        public void OnReceivePlayerMeridiansChangeEvtR2B(PlayerMeridiansChangeEvtR2B evt)
        {
            var p = mProp.ServerData;
            if (mProp != null && p != null)
            {
                if (p.MeridiansInfo == null)
                    p.MeridiansInfo = new HashMap<int, int>();

                foreach (var item in evt.r2b_datas)
                {
                    p.MeridiansInfo.Put(item.Key, item.Value);
                }

                SkillModule?.ResetSkillScript();
            }
        }

        public override HashMap<int, int> GetMeridiansInfo()
        {
            if (mProp != null && mProp.ServerData != null)
                return mProp.ServerData.MeridiansInfo;

            return null;
        }

        #endregion

        #region 仙盟追杀.

        private bool CheckGuildChase(TLVirtual tv)
        {
            if (tv == null) return false;
            var pv = tv.GetPlayerUnit();

            if (pv == null) return false;
            var guildUUID = pv.GuildUUID();

            //当前场景是否允许仙盟追杀.
            if (GetMapData()?.is_guildchase == 0)
                return false;

            //公会ID是否在追杀名单中.
            return InGuildChaseList(guildUUID);
        }

        public bool InGuildChaseList(string guildUUID)
        {
            if (string.IsNullOrEmpty(guildUUID) || string.IsNullOrEmpty(GetBaseInfo()?.GuildUUID))
                return false;

            if (GuildChaseMap != null)
                GuildChaseMap.ContainsKey(guildUUID);

            return false;
        }

        private HashMap<string, byte> GuildChaseMap = null;
        private void InitGuildChaseMap(List<string> lt)
        {
            GuildChaseMap?.Clear();
            if (lt == null || lt.Count == 0) return;

            if (GuildChaseMap == null)
                GuildChaseMap = new HashMap<string, byte>();

            for (int i = 0; i < lt.Count; i++)
            {
                GuildChaseMap.Put(lt[i], 0);
            }
        }

        public void OnRecievePlayerGuildChaseListChangeR2B(PlayerGuildChaseListChangeR2B evt)
        {
            InitGuildChaseMap(evt.r2b_list);
            var e = new PlayerGuildChaseListChangeB2C();
            e.s2c_list = evt.r2b_list;
            this.mUnit.queueEvent(e);
        }

        #endregion

        public class TLTPData
        {
            public float X, Y = -1;
            public int NextMapID;
            public string NextMapPosition;
            public bool Force = false;
            public string GuildUUID;
        }

    }
}
