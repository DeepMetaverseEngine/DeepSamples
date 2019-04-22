using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using TLBattle.Common.Data;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.ActiveSkills
{
    #region 战斗数据.

    public class BattleParams
    {
        public enum FormulaType : byte
        {
            Damage,   //伤害.
            Heal,     //治愈.
            None,     //无伤.
            Modify,   //自定义.
        }

        public enum ElementType : byte
        {
            None,    //无.
            Fire,    //火元素.
            Thunder, //雷元素.
            Posion,  //毒元素.
            Ice,     //冰元素.
            Wind,    //风元素.
            Soil,    //土元素.
        }

        private static readonly ObjectPool<BattleParams> s_ListPool = new ObjectPool<BattleParams>(s_ListPool_OnCreate);

        private TLVirtual mAttacker;
        private TLVirtual mHitter;
        private AttackSource mSource;
        private GameSkill mGameSkill;
        private TLAttackProperties mProp;
        private TLVirtual.AtkAppendData mAtkAppendData;

        #region 计算结果.

        private TLVirtual.AttackType mAttackType = TLVirtual.AttackType.phyAtk;
        private int mThreatValue;

        private int mSkillDamagePer;
        private int mSkillDamageModify;
        private int mFinalDamage;

        private int mSkillDamageAdd;
        private int mHitResult;
        private int mHitDamge;
        private FormulaType mFormulaType = FormulaType.Damage;
        private ElementType mElementType = ElementType.None;
        private int mElementSkillDamagePer;
        private int mElementSkillDamage;

        #endregion

        private BattleParams() { }

        public static BattleParams AllocAutoRelease()
        {
            BattleParams ret = s_ListPool.Get();
            return ret;
        }

        private static BattleParams s_ListPool_OnCreate()
        {
            return new BattleParams();
        }

        public void Dispose()
        {
            OnDispose();
            s_ListPool.Release(this);
        }

        private void OnDispose()
        {
            mAttacker = null;
            mHitter = null;
            mSource = null;
            mGameSkill = null;
            mProp = null;

            mAttackType = TLVirtual.AttackType.phyAtk;
            mThreatValue = 0;

            mSkillDamagePer = 0;
            mSkillDamageModify = 0;


            mFinalDamage = 0;
            mSkillDamageAdd = 0;

            mHitDamge = 0;
            mHitResult = 0;

            mFormulaType = FormulaType.Damage;
            mElementSkillDamagePer = 0;
            mElementType = ElementType.None;
        }

        /// <summary>
        /// 攻击者.
        /// </summary>
        public TLVirtual Attacker
        {
            set { mAttacker = value; }
            get { return mAttacker; }
        }

        /// <summary>
        /// 被攻击者.
        /// </summary>
        public TLVirtual Hitter
        {
            get
            {
                return mHitter;
            }

            set
            {
                mHitter = value;
            }
        }

        /// <summary>
        /// 攻击源信息.
        /// </summary>
        public AttackSource Source
        {
            get
            {
                return mSource;
            }

            set
            {
                mSource = value;
                mProp = (mSource.Attack.Properties as TLAttackProperties);
            }
        }

        /// <summary>
        /// 攻击信息.
        /// </summary>
        public TLAttackProperties AtkProp
        {
            get { return mProp; }
        }

        public TLVirtual.AtkAppendData AtkAppendData
        {
            get { return mAtkAppendData; }
            set { mAtkAppendData = value; }
        }

        /// <summary>
        /// 技能信息.
        /// </summary>
        public GameSkill GameSkill
        {
            get
            {
                return mGameSkill;
            }

            set
            {
                mGameSkill = value;
            }
        }

        /// <summary>
        /// 技能伤害百分比.
        /// </summary>
        public int SkillDamagePer
        {
            get
            {
                return mSkillDamagePer;
            }

            set
            {
                mSkillDamagePer = value;
            }
        }

        /// <summary>
        /// 技能伤害绝对值.
        /// </summary>
        public int SkillDamageModify
        {
            get
            {
                return mSkillDamageModify;
            }

            set
            {
                mSkillDamageModify = value;
            }
        }

        /// <summary>
        /// 最终技能伤害.
        /// </summary>
        public int FinalSkillDamage
        {
            get
            {
                return mFinalDamage;
            }

            set
            {
                mFinalDamage = value;
            }
        }

        /// <summary>
        /// 伤害类型.
        /// </summary>
        public TLVirtual.AttackType AttackType
        {
            get
            {
                return mAttackType;
            }

            set
            {
                mAttackType = value;
            }
        }

        /// <summary>
        /// 威胁值.
        /// </summary>
        public int ThreatValue
        {
            get
            {
                return mThreatValue;
            }

            set
            {
                mThreatValue = value;
            }
        }

        /// <summary>
        /// 使用计算公式类型.
        /// </summary>
        public FormulaType UseFormulaType
        {
            get
            {
                return mFormulaType;
            }

            set
            {
                mFormulaType = value;
            }
        }

        /// <summary>
        /// 技能伤害增加.
        /// </summary>
        public int SkillDamageAdd
        {
            get
            {
                return mSkillDamageAdd;
            }

            set
            {
                mSkillDamageAdd = value;
            }
        }

        /// <summary>
        /// Hit结果，（伤害计算计算BUFF或其他异常状态作用最终效果.）
        /// </summary>
        public int HitResult
        {
            get
            {
                return mHitResult;
            }

            set
            {
                mHitResult = value;
            }
        }

        /// <summary>
        /// 打击伤害（FinalDamage经过暴击计算的值.）
        /// </summary>
        public int HitDamge
        {
            get
            {
                return mHitDamge;
            }

            set
            {
                mHitDamge = value;
            }
        }

        /// <summary>
        /// 元素伤害类型.
        /// </summary>
        public ElementType ElementDamageType
        {
            get
            {
                return mElementType;
            }

            set
            {
                mElementType = value;
            }
        }

        /// <summary>
        /// 技能元素伤害.
        /// </summary>
        public int ElementSkillDamagePer
        {
            get
            {
                return mElementSkillDamagePer;
            }

            set
            {
                mElementSkillDamagePer = value;
            }
        }

        public int ElementSkillDamage
        {
            get
            {
                return mElementSkillDamage;
            }

            set
            {
                mElementSkillDamage = value;
            }
        }

        /*
        #region 暂时关闭.

        /// <summary>
        /// 暴击增加.
        /// </summary>
        public int CritAdd
        {
            get
            {
                return mCritAdd;
            }

            set
            {
                mCritAdd = value;
            }
        }

        /// <summary>
        /// 命中增加.
        /// </summary>
        public int HitAdd
        {
            get
            {
                return mHitAdd;
            }

            set
            {
                mHitAdd = value;
            }
        }


        #endregion
          */

    }

    #endregion

    public partial class TLSkillBase : UnitSkill
    {
        #region 回调事件.

        protected enum EventType : int
        {
            HitRateAdd = 1,
            CritRateAdd = 2,
            SkillCoefficientModify = 4,

            ThreadValue = 6,
            SkillLogic = 7,

            SkillLogicAftercalDamage = 11,
            SkillCoefficient = 12,
        }
        public delegate void ScriptHandler(BattleParams param);
        /// <summary>
        /// 计算闪避回调.
        /// </summary>
        protected ScriptHandler event_CalHitRateAdd;
        /// <summary>
        /// 计算暴击回调.
        /// </summary>
        protected ScriptHandler event_CalCritRateAdd;
        /// <summary>
        /// 计算技能伤害.
        /// </summary>
        protected ScriptHandler event_SkillCoefficient;
        /// <summary>
        /// 计算技能仇恨.
        /// </summary>
        protected ScriptHandler event_SkillThreatValue;
        /// <summary>
        /// 计算技能逻辑.
        /// </summary>
        protected ScriptHandler event_SkillLogic;
        /// <summary>
        /// 技能附加伤害加成.
        /// </summary>
        protected ScriptHandler event_DamageModifyAdd;
        /// <summary>
        /// 是否走伤害流程.
        /// </summary>                                           
        protected ScriptHandler event_UseFormulaType;
        /// <summary>
        /// 计算伤害之后的技能逻辑回调.
        /// </summary>
        protected ScriptHandler event_SkillLogicAfterCalDamage;

        #endregion 

        #region 计算流程回调方法.

        private void OnCalHitRateAddHandler(BattleParams param)
        {
            if (event_CalHitRateAdd != null)
            {
                event_CalHitRateAdd.Invoke(param);
            }
        }
        private void OnFinalCritHandler(BattleParams param)
        {
            if (event_CalCritRateAdd != null)
            {
                event_CalCritRateAdd.Invoke(param);
            }
        }
        private void OnGetSkillCoefficient(BattleParams param)
        {
            if (param.AtkProp.UseConfig)
            {
                param.AttackType = AtkType;
                param.ElementDamageType = ElementType;
                param.ElementSkillDamagePer = Element_coefficient;
                param.ElementSkillDamage = Element_damage;
                param.SkillDamagePer = ski11_coefficient;
                param.SkillDamageModify = Skill_damage;
            }
            else
            {
                param.AttackType = GetAtkType(param.AtkProp.SkillArgu_1);
                param.ElementDamageType = GetElementType(param.AtkProp.ElementType);
                param.ElementSkillDamagePer = param.AtkProp.ElementCoefficient;
                param.ElementSkillDamage = param.AtkProp.ElementDamage;
                param.SkillDamagePer = param.AtkProp.SkillCoefficient;
                param.SkillDamageModify = param.AtkProp.SkillDamage;
            }

            if (event_SkillCoefficient != null)
            {
                event_SkillCoefficient.Invoke(param);
            }
        }
        private void OnGetSkillThreatValue(BattleParams param)
        {
            if (event_SkillThreatValue != null)
            {
                event_SkillThreatValue.Invoke(param);
            }
        }
        private void OnGetSkillLogic(BattleParams param)
        {
            if (event_SkillLogic != null)
            {
                event_SkillLogic.Invoke(param);
            }
        }
        private void OnUseDamageFormula(BattleParams param)
        {
            if (event_UseFormulaType != null)
            {
                event_UseFormulaType.Invoke(param);
            }
        }
        protected virtual void OnGetSkillLogicAfterCalDamage(BattleParams param)
        {
            if (event_SkillLogicAfterCalDamage != null)
            {
                event_SkillLogicAfterCalDamage.Invoke(param);
            }
        }

        #endregion

        private bool IsInRandomRange(Random r, float v)
        {
            bool ret = false;

            var rv = r.NextDouble();

            if (rv <= v) { ret = true; }

            return ret;
        }

        /// <summary>
        /// 是否读取配置表.
        /// </summary>
        private bool mLoadData = true;

        protected virtual bool LoadData
        {
            get { return mLoadData; }
        }

        public override int SkillID
        {
            get { return -1; }
            set { }
        }
        /// <summary>
        /// 蓝耗.
        /// </summary>
        private int mMPCost = 0;
        /// <summary>
        /// 怒气回复.
        /// </summary>
        private int mRecoverAnger = 0;
        /// <summary>
        /// 怒气消耗.
        /// </summary>
        private int mCostAnger = 0;
        /// <summary>
        /// 对怪物额外伤害.
        /// </summary>
        private int mExtra_Monster = 0;
        /// <summary>
        /// 对玩家额外伤害.
        /// </summary>
        private int mExtra_Player = 0;

        /// <summary>
        /// 蓝耗量.
        /// </summary>
        public int MPCost
        {
            private set { mMPCost = value; }
            get { return mMPCost; }
        }
        /// <summary>
        /// 怒气值回复.
        /// </summary>
        public int RecoverAnger
        {
            private set { mRecoverAnger = value; }
            get { return mRecoverAnger; }
        }
        /// <summary>
        /// 怒气值消耗.
        /// </summary>
        public int CostAnger
        {
            private set { mCostAnger = value; }
            get { return mCostAnger; }
        }
        /// <summary>
        /// 对玩家额外伤害.
        /// </summary>
        public int Extra_Player { get => mExtra_Player; set => mExtra_Player = value; }
        /// <summary>
        /// 对怪物额外伤害.
        /// </summary>
        public int Extra_Monster { get => mExtra_Monster; set => mExtra_Monster = value; }
        /// <summary>
        /// 伤害类型.
        /// </summary>
        protected TLVirtual.AttackType AtkType;

        protected BattleParams.ElementType ElementType;

        /// <summary>
        /// 技能伤害系数.
        /// </summary>
        public int ski11_coefficient;

        /// <summary>
        /// 技能伤害绝对值.
        /// </summary>
        public int Skill_damage;

        /// <summary>
        /// 元素伤害系数.
        /// </summary>
        protected int Element_coefficient;

        /// <summary>
        /// 元素伤害绝对值.
        /// </summary>
        protected int Element_damage;


        protected void RegistEvent(EventType type, ScriptHandler callBack)
        {
            switch (type)
            {
                case EventType.CritRateAdd:
                    event_CalCritRateAdd = callBack;
                    break;
                case EventType.HitRateAdd:
                    event_CalHitRateAdd = callBack;
                    break;
                case EventType.SkillCoefficient:
                    event_SkillCoefficient = callBack;
                    break;
                case EventType.ThreadValue:
                    event_SkillThreatValue = callBack;
                    break;
                case EventType.SkillLogic:
                    event_SkillLogic = callBack;
                    break;

                case EventType.SkillLogicAftercalDamage:
                    event_SkillLogicAfterCalDamage = callBack;
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 初始化回调，用于注册技能
        /// eg:RegistSkillEvent
        /// </summary>
        /// <param name="info"></param>
        /// <param name="unit"></param>
        /// <param name="template"></param>
        protected override void OnInit(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            SkillID = info.SkillID;
            //注册脚本回调事件.
            RegistScriptEvent();

            //技能脚本数据初始化.
            InitData(info, unit, ref template);

            //初始化单位注册事件.
            RegisterUnitEvent(info, unit, ref template);
        }

        /// <summary>
        /// 技能等级变化参数重置.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="unit"></param>
        /// <param name="template"></param>
        protected override void OnReset(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            //技能脚本数据初始化.
            InitData(info, unit, ref template);
        }

        private void OnDispose()
        {
            event_CalHitRateAdd = null;
            event_CalCritRateAdd = null;
            event_SkillCoefficient = null;
            event_SkillThreatValue = null;
            event_SkillLogic = null;
            event_SkillLogicAfterCalDamage = null;

        }

        /// <summary>
        /// 注册脚本回调事件.
        /// </summary>
        private void RegistScriptEvent()
        {
            this.RegistEvent(EventType.SkillCoefficient, OnSkillCoefficientEvent);
            this.RegistEvent(EventType.ThreadValue, OnThreatValueEvent);
            this.RegistEvent(EventType.SkillLogic, OnSkillLogicEvent);

            OnRegistScriptEvent();
        }

        /// <summary>
        /// 子类添加新的注册事件.
        /// </summary>
        protected virtual void OnRegistScriptEvent()
        {

        }

        /// <summary>
        /// 初始化技能数据.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="unit"></param>
        /// <param name="template"></param>
        private void InitData(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            if (LoadData)
            {
                var data = GetSkillData(info.SkillID, info.SkillLevel);

                if (data == null)
                {
                    var t = string.Format("未找到技能ID=[{0}]LV =[{1}]对应的配置表数据", info.SkillID, info.SkillLevel);
                    LogError(t);
                }
                else
                {
                    template.CoolDownMS = data.skill_cd;
                    AtkType = GetAtkType(data.damage_type);
                    ElementType = GetElementType(data.element_type);

                    ski11_coefficient = data.skill_coefficient;
                    Skill_damage = data.skill_damage;

                    Element_coefficient = data.element_coefficient;
                    Element_damage = data.element_damage;

                    RecoverAnger = data.recover_anger;
                    CostAnger = data.cost_anger;

                    Extra_Monster = data.extra_monster;
                    Extra_Player = data.extra_player;

                    #region 经脉功能.

                    HashMap<int, int> meridians = unit.GetMeridiansInfo();
                    if (unit != null && meridians != null)
                    {
                        var mgr = TLDataMgr.GetInstance().MeridiansMgr;
                        int skill_decrease_cd = 0;

                        if (mgr != null)
                        {
                            TLMeridiansData meridiansData = null;

                            foreach (var item in meridians)
                            {
                                meridiansData = mgr.GetData(item.Key, info.SkillID);
                                if (meridiansData != null)
                                {
                                    RecoverAnger += meridiansData.recover_anger;
                                    ski11_coefficient += meridiansData.skill_coefficient;
                                    Skill_damage += meridiansData.skill_damage;
                                    Element_coefficient += meridiansData.element_coefficient;
                                    Element_damage += meridiansData.element_damage;
                                    Extra_Monster += meridiansData.extra_monster;
                                    Extra_Player += meridiansData.extra_player;
                                    skill_decrease_cd += meridiansData.skill_cd;
                                }
                            }

                            if (skill_decrease_cd != 0)
                            {
                                var ss = unit.mUnit.getSkillState(template.ID);
                                if (ss != null)
                                    ss.SetDecreaseTotalTimeMS(-skill_decrease_cd);//已初始化的技能改变CD时间
                                else
                                    template.CoolDownMS += skill_decrease_cd;//未初始化的技能直接改变技能原有CD.
                            }
                        }
                    }

                    #endregion

                    OnInitData(data, info, unit, ref template);
                }

            }

        }

        /// <summary>
        /// 初始化技能数据.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="info"></param>
        /// <param name="unit"></param>
        /// <param name="template"></param>
        protected virtual void OnInitData(TLSkillData data, GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {

        }

        /// <summary>
        /// 初始化单位注册事件.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="unit"></param>
        /// <param name="template"></param>
        private void RegisterUnitEvent(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            OnRegisterUnitEvent(info, unit, ref template);
        }

        /// <summary>
        /// 初始化单位注册事件.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="unit"></param>
        /// <param name="template"></param>
        protected virtual void OnRegisterUnitEvent(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            //伤害监听.
            unit.RegistCalDamage(OnCallDamageProcess, info);
            //技能MP消耗预判.
            unit.RegistTryLaunchSkillEvent(OnUnitLaunchSkillEvent, info, false);
            //技能MP消耗.
            unit.RegistLaunchSkillOver(OnUnitLaunchSkillOver, info, false);
        }

        #region 常态注册.

        /// <summary>
        /// 得到仇恨.
        /// </summary>
        /// <param name="param"></param>
        protected virtual void OnThreatValueEvent(BattleParams param)
        {
            param.ThreatValue = 0;
        }

        /// <summary>
        /// 得到技能百分比.
        /// </summary>
        /// <param name="param"></param>
        protected virtual void OnSkillCoefficientEvent(BattleParams param)
        {

        }

        /// <summary>
        /// 技能特殊逻辑.
        /// </summary>
        /// <param name="param"></param>
        protected virtual void OnSkillLogicEvent(BattleParams param)
        {

        }

        private BattleParams GetParams(TLVirtual attacker, TLVirtual hitter, AttackSource source, GameSkill gs, ref TLVirtual.AtkAppendData data)
        {
            BattleParams param = BattleParams.AllocAutoRelease();
            param.Attacker = attacker;
            param.Hitter = hitter;
            param.Source = source;
            param.GameSkill = gs;
            param.AtkAppendData = data;
            return param;
        }

        protected virtual int OnCallDamageProcess(TLVirtual attacker, TLVirtual hitter, AttackSource source, GameSkill gs, ref TLVirtual.AtkAppendData data)
        {
            BattleParams param = null;
            int ret = 0;


            var sd = (source.Attack.Properties as TLAttackProperties).SplitData;

            if (sd != null && sd.SplitFrameMSList != null && sd.SplitFrameMSList.Count > 1)
            {
                List<int> t = sd.SplitFrameMSList;
                List<SplitHitInfo> hitList = new List<SplitHitInfo>();
                SplitHitInfo tempHit = null;
                //客户端通过其他协议模拟伤害.
                source.OutSendEvent = false;
                int totalDamage = 0;
                for (int index = 0; index < t.Count; index++)
                {
                    param = GetParams(attacker, hitter, source, gs, ref data);
                    OnDoSkillHitProcess(ref param);
                    tempHit = new SplitHitInfo();
                    tempHit.FrameMS = t[index];
                    tempHit.hitType = param.Source.OutClientState;
                    tempHit.Damage = param.HitResult;
                    hitList.Add(tempHit);
                    ret += tempHit.Damage;
                    totalDamage += tempHit.Damage;
                    data.ThreatValue += param.ThreatValue;
                    param.Dispose();
                }

                data.HitInfoTotalDamage = totalDamage;
                data.HitInfo = hitList;
            }
            else
            {
                param = GetParams(attacker, hitter, source, gs, ref data);
                OnDoSkillHitProcess(ref param);

                data.ThreatValue = param.ThreatValue;
                ret = param.HitResult;
                param.Dispose();
            }
            return ret;

        }

        protected virtual bool OnUnitLaunchSkillEvent(ref InstanceUnit.SkillState skill, TLVirtual launcher, ref InstanceUnit.LaunchSkillParam param)
        {
            bool ret = true;
            /*
            if (skill.ID == SkillID)
            {
                ret = MPCostCheck(ref skill, launcher);
            }
            */

            if (skill.ID == SkillID)
            {
                ret = FuryCostCheck(ref skill, launcher);
            }

            if (ret == false)
            {
                //发送提示.
                launcher.SendMsgToClient(TLCommonConfig.TIPS_MP_NOT_ENOUGH);
            }

            return ret;
        }

        protected virtual int OnUnitLaunchSkillOver(int costEnergy, TLVirtual attacker, InstanceUnit.SkillState state)
        {
            /*
            if (state.ID == SkillID)
            {
                costEnergy = GetMPCost(costEnergy, attacker, state);
            }
            */

            if (state.ID == SkillID)
            {
                costEnergy = GetCostAnger(costEnergy, attacker, state);
                costEnergy = costEnergy - RecoverAnger;
            }

            return costEnergy;
        }
        #endregion

        #region 耗蓝.

        protected bool MPCostCheck(ref InstanceUnit.SkillState skill, TLVirtual launcher)
        {
            return launcher.mUnit.CurrentMP >= MPCost;

        }
        protected virtual int GetMPCost(int costEnergy, TLVirtual attacker, InstanceUnit.SkillState state)
        {
            return MPCost;
        }
        protected virtual int GetMPCost(int skillid, int skilllv, TLVirtual launcher)
        {
            return MPCost;
        }

        protected bool FuryCostCheck(ref InstanceUnit.SkillState skill, TLVirtual launcher)
        {
            return launcher.mUnit.CurrentMP >= CostAnger;
        }
        protected virtual int GetCostAnger(int costEnergy, TLVirtual attacker, InstanceUnit.SkillState state)
        {
            return CostAnger;
        }
        protected virtual int GetCostAnger(int skillid, int skilllv, TLVirtual launcher)
        {
            return CostAnger;
        }

        #endregion

        #region 辅助

        protected TLSkillData GetSkillData(int skillid, int lv)
        {
            return TLDataMgr.GetInstance().SkillData.GetSkillData(skillid, lv);
        }

        protected TLSkillData GetSkillData(BattleParams param)
        {
            return TLDataMgr.GetInstance().SkillData.GetSkillData(param.GameSkill.SkillID, param.GameSkill.SkillLevel);
        }

        private TLVirtual.AttackType GetAtkType(int type)
        {
            if (type == 1)
                return TLVirtual.AttackType.phyAtk;
            else if (type == 2)
                return TLVirtual.AttackType.magAtk;
            else
                return TLVirtual.AttackType.phyAtk;
        }

        private BattleParams.ElementType GetElementType(int type)
        {
            switch (type)
            {
                case 1:
                    return BattleParams.ElementType.Thunder;
                case 2:
                    return BattleParams.ElementType.Wind;
                case 3:
                    return BattleParams.ElementType.Ice;
                case 4:
                    return BattleParams.ElementType.Fire;
                case 5:
                    return BattleParams.ElementType.Soil;
            }
            return BattleParams.ElementType.Thunder;
        }

        #endregion

        #region BUFF

        protected void AddBuff(TLVirtual sender, TLVirtual target, int id, UnitBuff buff = null)
        {
            target.AddTLBuff(id, sender, buff);
        }

        protected void AddBuff(TLVirtual sender, TLVirtual target, BuffTemplate bt, UnitBuff buff = null)
        {
            target.AddTLBuff(bt, sender, buff);
        }

        protected void AddBuff(TLVirtual sender, TLVirtual target, int id, List<UnitBuff> list)
        {
            target.AddTLBuff(id, sender, list);
        }

        protected void AddBuff(TLVirtual sender, TLVirtual target, BuffTemplate bt, List<UnitBuff> list, int lifeTimeMS, int passTimeMS, byte overlayLevel)
        {
            target.AddTLBuff(bt, sender, list, lifeTimeMS, passTimeMS, overlayLevel);
        }

        #endregion

        #region 伤害计算流程.

        protected void OnDoSkillHitProcess(ref BattleParams param)
        {
            try
            {
                //判断是否命中/普攻/暴击/格挡.
                OnCalAtkResult(param);

                if (param.Source.OutClientState != (byte)TLBattle.Message.BattleAtkNumberEventB2C.AtkNumberType.Dodge)
                {
                    //获得技能伤害百分比.
                    OnGetSkillCoefficient(param);

                    //技能逻辑调用.
                    OnGetSkillLogic(param);


                    switch (param.UseFormulaType)
                    {
                        case BattleParams.FormulaType.Damage:
                            OnGetDamage(param);
                            break;
                        case BattleParams.FormulaType.Heal:
                            OnGetHeal(param);
                            break;
                        case BattleParams.FormulaType.None:
                            param.FinalSkillDamage = 0;
                            break;
                        default: break;
                    }

                    OnCalFinalHitDamage(param);

                    OnGetHitResult(param);

                }
            }
            catch (Exception error)
            {
                LogError(string.Format("skill {0} OnCallDamageProcess error:{1} - {2}", param.GameSkill.SkillID, error.ToString(), error.StackTrace));
            }

        }


        /// <summary>
        /// 计算本次攻击是否命中:否暴.击格挡.
        /// </summary>
        /// <param name="param"></param>
        private void OnCalAtkResult(BattleParams param)
        {
            //是否命中.
            if (IsMissAtk(param))
            {
                param.Source.OutClientState = (byte)TLBattle.Message.BattleAtkNumberEventB2C.AtkNumberType.Dodge;
            }
            else
            {
                if (IsBlockAtk(param)) //格挡.
                {
                    param.Source.OutClientState = (byte)TLBattle.Message.BattleAtkNumberEventB2C.AtkNumberType.Block;
                }
                else if (IsCritAtk(param)) //暴击.
                {
                    param.Source.OutClientState = (byte)TLBattle.Message.BattleAtkNumberEventB2C.AtkNumberType.Crit;
                }
                else //普攻
                {
                    param.Source.OutClientState = (byte)TLBattle.Message.BattleAtkNumberEventB2C.AtkNumberType.Normal;
                }
            }
        }

        private bool IsMissAtk(BattleParams param)
        {
            TLVirtual attacker = param.Attacker;
            TLVirtual hitter = param.Hitter;

            //友军技能通常为增益技能故为必中.
            if (attacker.IsAllies(hitter, true))
            {
                return false;
            }

            bool ret = false;

            ret = IsMissAtk(attacker.mUnit.RandomN, CalHitRate(attacker.MirrorProp.Hit, hitter.MirrorProp.Dodge));

            return ret;
        }

        //是否未命中.
        private bool IsMissAtk(Random r, float hitrate)
        {
            bool ret = false;

            ret = !IsInRandomRange(r, hitrate);

            return ret;
        }
        //是否暴击.
        private bool IsCritAtk(BattleParams param)
        {
            TLVirtual attacker = param.Attacker;
            TLVirtual hitter = param.Hitter;

            bool ret = false;

            ret = IsCritAtk(attacker.mUnit.RandomN, CalCritRate(attacker.MirrorProp.Crit, hitter.MirrorProp.ResCrit, attacker.MirrorProp.Extracrit));

            return ret;
        }
        //是否暴击.
        private bool IsCritAtk(Random r, float critrate)
        {
            bool ret = false;

            ret = IsInRandomRange(r, critrate);

            return ret;
        }
        //是否格挡.
        private bool IsBlockAtk(BattleParams param)
        {
            TLVirtual attacker = param.Attacker;
            TLVirtual hitter = param.Hitter;

            //友军技能通常为增益技能故为必中.
            if (attacker.IsAllies(hitter, false))
            {
                return false;
            }

            bool ret = false;

            ret = IsBlockAtk(attacker.mUnit.RandomN, CalBlockRate(attacker.GetUnitLv(), hitter.MirrorProp.Block));

            return ret;
        }
        //是否格挡.
        private bool IsBlockAtk(Random r, float blockRate)
        {
            bool ret = false;

            ret = IsInRandomRange(r, blockRate);

            return ret;
        }
        /// <summary>
        /// 获取元素伤害.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected int GetElementDamage(BattleParams param, float totalDamagePerRate)
        {
            return GetElementDamage(param.ElementDamageType,
                                     param.ElementSkillDamagePer,
                                     param.ElementSkillDamage,
                                     param.Attacker,
                                     param.Hitter, totalDamagePerRate);
        }

        protected int GetElementDamage(BattleParams.ElementType elementType, int elementSkillDamagePer, int elementSkill, TLVirtual attacker, TLVirtual hitter, float totalDamagePerRate)
        {
            int damage = 0;
            int resist = 0;

            switch (elementType)
            {
                case BattleParams.ElementType.Fire:
                    damage = attacker.MirrorProp.FireDmage;
                    resist = hitter.MirrorProp.FireResist;
                    break;
                case BattleParams.ElementType.Ice:
                    damage = attacker.MirrorProp.IceDamage;
                    resist = hitter.MirrorProp.IceResist;
                    break;
                case BattleParams.ElementType.Wind:
                    damage = attacker.MirrorProp.WindDamage;
                    resist = hitter.MirrorProp.WindResist;
                    break;
                case BattleParams.ElementType.Thunder:
                    damage = attacker.MirrorProp.ThunderDamage;
                    resist = hitter.MirrorProp.ThunderResist;
                    break;
                case BattleParams.ElementType.Soil:
                    damage = attacker.MirrorProp.SoilDamage;
                    resist = hitter.MirrorProp.SoilResist;
                    break;
                default:
                    return 0;
            }
            float elementDamageRate = CalElementDamageRate(damage, resist);


            return CalElementDamage(attacker.mUnit.RandomN, totalDamagePerRate, elementSkillDamagePer, elementSkill, elementDamageRate);

        }

        private int GetGodDamage(BattleParams param)
        {
            return param.Attacker.MirrorProp.GodDamage;
        }

        //玩家/怪物额外加成.
        private int GetTargetTypeDamage(BattleParams param)
        {
            float skillDamage = PerToFloat(param.SkillDamagePer);
            if (param.Hitter.IsPlayerUnit())
            {
                return (int)(skillDamage * param.Attacker.MirrorProp.TargetToPlayer + Extra_Player);
            }
            else if (param.Hitter.IsBuilding())//建筑无加成.
            {
                return 0;
            }
            return (int)(skillDamage * param.Attacker.MirrorProp.TargetToMonster + Extra_Monster);
        }

        private void OnGetDamage(BattleParams param)
        {
            TLVirtual attacker = param.Attacker;
            TLVirtual hitter = param.Hitter;

            float totalDamagePerRate = CalTotalDamagePerRate(attacker.MirrorProp.TotalDamagePer, hitter.MirrorProp.TotalReduceDamagePer);

            #region 技能伤害.

            int skillDamage = CalSkillDamage(attacker.MirrorProp.Attack, param.SkillDamagePer, param.SkillDamageModify);

            int targetTypeDamage = GetTargetTypeDamage(param);

            float damageReduceRate = 0;

            switch (param.AttackType)
            {
                case TLVirtual.AttackType.magAtk:
                    damageReduceRate = CalMagDamageReduceRate(attacker.GetUnitLv(), attacker.MirrorProp.Through, hitter.MirrorProp.MagDef, hitter.MirrorProp.MDefReduction);
                    break;
                case TLVirtual.AttackType.phyAtk:
                    damageReduceRate = CalPhyDamageReduceRate(attacker.GetUnitLv(), attacker.MirrorProp.Through, hitter.MirrorProp.PhyDef, hitter.MirrorProp.DefReduction);
                    break;
                default: break;
            }

            #endregion

            int damage = CalDamage(attacker.mUnit.RandomN, totalDamagePerRate, skillDamage, targetTypeDamage, damageReduceRate);

            if (IsPVP(param))
            {
                damage = CalPVPDamage(damage);
            }
            else
            {
                damage = CalPVEDamage(damage);
            }

            //元素伤害.
            int elementDamage = GetElementDamage(param, totalDamagePerRate);

            //神圣伤害.
            int godDamage = GetGodDamage(param);

            param.FinalSkillDamage = damage + elementDamage + godDamage;
        }

        private void OnCalFinalHitDamage(BattleParams param)
        {
            int damage = param.FinalSkillDamage;

            if (param.Source.OutClientState == (int)TLBattle.Message.BattleAtkNumberEventB2C.AtkNumberType.Crit)
            {
                float critPer = 0;

                switch (param.UseFormulaType)
                {
                    case BattleParams.FormulaType.Damage:
                        critPer = CalCritDamagePer(param.Attacker.MirrorProp.CriDamagePer,
                                              param.Hitter.MirrorProp.RedCriDamagePer);
                        damage = (int)(critPer * damage);
                        break;
                    case BattleParams.FormulaType.Heal:
                        critPer = CalCritHealRate(param.Attacker.MirrorProp.Crit);
                        damage = (int)(critPer * damage);
                        break;
                }
            }
            else if (param.Source.OutClientState == (int)TLBattle.Message.BattleAtkNumberEventB2C.AtkNumberType.Block)
            {
                damage = CalBlockDamage(param.Attacker.mUnit.RandomN, damage);

            }

            param.HitDamge = damage;
        }

        /// <summary>
        /// 获取最终结果.
        /// </summary>
        /// <param name="param"></param>
        private void OnGetHitResult(BattleParams param)
        {
            int result = param.HitDamge;

            TLVirtual.AtkAppendData r = param.AtkAppendData;

            param.Attacker.DispatchHitEvents(ref result, param.Attacker, param.Hitter, param.Source, ref r);

            param.HitResult = result;
        }

        private void OnGetHeal(BattleParams param)
        {

        }

        private bool IsPVP(BattleParams param)
        {
            if (param.Hitter.IsPlayerUnit())
                return true;
            else
                return false;
        }

        #endregion

        #region 经脉加成.



        #endregion
    }
}
