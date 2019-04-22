using DeepCore.GameData.Zone;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using DeepCore.Log;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Server.Plugins.TLSkillTemplate.Skills
{

    /// <summary>
    /// BUFF基类.
    /// </summary>
    public abstract class UnitBuff
    {
        protected static Logger log = LoggerFactory.GetLogger("TLBattleBuff");

        /// <summary>
        /// 能力ID(决定buff作用.)
        /// </summary>
        public abstract int GetAbilityID();

        protected int bufftemplateID = 0;

        public virtual void BuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state) { }
        public virtual int BuffHit(TLVirtual hitter, TLVirtual attacker, AttackSource source, ref TLVirtual.AtkAppendData result) { return 0; }
        public virtual void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state) { }

        public void BindTemplate(BuffTemplate buffTemplate)
        {
            bufftemplateID = buffTemplate.ID;
            OnBindTemplate(buffTemplate);
        }
        protected virtual void OnBindTemplate(BuffTemplate buffTemplate)
        {

        }

        public abstract TLBuffData ToBuffData();

    }
}
