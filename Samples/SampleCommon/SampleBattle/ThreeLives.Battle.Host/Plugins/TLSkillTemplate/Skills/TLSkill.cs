using DeepCore.GameData.Zone;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using DeepCore.Log;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Server.Plugins.TLSkillTemplate.Skills
{
    #region 事件监听.

    //监听事件.
    /// <summary>
    /// 产生伤害监听.
    /// </summary>
    public delegate float IOnHit(float damage, TLVirtual hitted, TLVirtual attacker, AttackSource source, GameSkill sk, ref TLVirtual.AtkAppendData result);

    //     /// <summary>
    //     /// 被动触发监听.
    //     /// </summary>
    //     public delegate void IOnTriggerStart(TLVirtual unit, TLVirtual target, UnitTriggerTemplate trigger, GameSkill sk);
    // 
    //     /// <summary>
    //     /// 测试触发器监听.
    //     /// </summary>
    //     public delegate bool ITestTriggerStart(TLVirtual unit, UnitTriggerTemplate trigger, BaseTriggerEvent evt, AttackSource source, GameSkill sk);

    /// <summary>
    /// 测试Buff监听.
    /// </summary>
    public delegate bool ITestLaunchBuff(TLVirtual unit, BuffTemplate buff, TLVirtual sender, GameSkill sk);

    /// <summary>
    /// 测试Buff监听.
    /// </summary>
    public delegate void IOnBuffEvent(TLVirtual unit, InstanceUnit.BuffState buff, GameSkill sk);

    /// <summary>
    /// 测试技能监听.
    /// </summary>
    public delegate bool ITestLaunchSkill(TLVirtual unit, InstanceUnit.SkillState skill, GameSkill sk);

    /// <summary>
    ///伤害计算监听.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="hitter"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public delegate int ICalDamage(TLVirtual attacker, TLVirtual hitter, AttackSource source, GameSkill gs, ref TLVirtual.AtkAppendData result);

    /// <summary>
    /// 技能释放完毕.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public delegate int ILaunchSkillOver(int costEnergy, TLVirtual attacker, InstanceUnit.SkillState state);

    /// <summary>
    /// 测试攻击目标监听.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="sk"></param>
    /// <returns></returns>
    public delegate TLVirtual IGetAtkTarget(TLVirtual target, GameSkill sk);

    public delegate void IGetSkillDamageInfo(ref float skillDamagePer, ref int skillDamageMod,
                                             TLVirtual attacker, TLVirtual hitter,
                                             AttackSource source, GameSkill gs);

    /// <summary>
    /// 监听施放技能.
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="launcher"></param>
    public delegate bool ITryLaunchSkillEvent(ref InstanceUnit.SkillState skill, TLVirtual launcher, ref InstanceUnit.LaunchSkillParam param);

    //召唤
    public delegate bool ISummonUnit(TLVirtual owner, SummonUnit summon, ref UnitInfo summonUnit, GameSkill sk);


    #endregion

    #region Skill.

    //技能基类.
    public abstract class UnitSkill
    {
        protected static Logger log = LoggerFactory.GetLogger("TLBattleSkill");

        public abstract int SkillID { get; set; }

        /// <summary>
        /// 初始化主动技能
        /// </summary>
        public void Init(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            OnInit(info, unit, ref template);
        }

        protected virtual void OnInit(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {

        }

        public void Reset(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            OnReset(info, unit, ref template);
        }

        protected virtual void OnReset(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {

        }

        /// <summary>
        /// 技能升级动态改变.
        /// </summary>
        /// <param name="gs"></param>
        /// <param name="unit"></param>
        /// <param name="skill"></param>
        public void SkillDataChange(GameSkill gs, TLVirtual unit, ref InstanceUnit.SkillState skill)
        {
            OnSkillDataChange(gs, unit, ref skill);
        }

        protected virtual void OnSkillDataChange(GameSkill gs, TLVirtual unit, ref InstanceUnit.SkillState skill)
        {

        }

        /// <summary>
        /// 输出错误日志.
        /// </summary>
        /// <param name="text"></param>
        protected virtual void LogError(string text = null)
        {
            if (text != null) { log.Error(string.Format("技能配置错误:{0}", text)); }
            else { log.Error(string.Format("技能【{0}】配置错误", SkillID)); }
        }

        /// <summary>
        /// 初始化完所有技能回调
        /// </summary>
        public virtual void InitOver(TLVirtual unit) { OnInitOVer(unit); }

        protected virtual void OnInitOVer(TLVirtual unit) { }

        public virtual bool SkillAutoLaunchTest(InstanceUnit.SkillState ss, TLVirtual launcher) { return true; }

        /// <summary>
        /// 技能被销毁.
        /// </summary>
        public virtual void Dispose() { }
    }


    #endregion





}
