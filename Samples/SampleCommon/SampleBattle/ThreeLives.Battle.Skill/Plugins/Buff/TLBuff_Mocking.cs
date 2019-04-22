using DeepCore.GameHost.Instance;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;
using TLCommonSkill.Plugins.Buff;

namespace TLCommonSkill.Plugins.Buff
{
    /// <summary>
    /// 嘲讽.
    /// </summary>
    public class TLBuff_Mocking : TLBuff
    {
        private int OperationID;
        private TLVirtual MockingUnit;

        public override void BuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            GameSkill gs = new GameSkill();
            gs.SkillID = GetAbilityID();
            gs.SkillLevel = 0;
            MockingUnit = attacker;
            OperationID = hitter.RegistGetAtkTarget(OnGetAtkTarget,gs,true);
        }

        public override void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            MockingUnit = null;
            hitter.UnRegistGetAtkTarget(OperationID);
        }

        private TLVirtual OnGetAtkTarget(TLVirtual target, GameSkill gs)
        {
            //有嘲讽目标且目标仍然有效.
            if (MockingUnit != null && MockingUnit.mUnit.IsActive)
            {
                return MockingUnit;
            }

            return target;
        }

        internal override void Init(TLBuffData bd)
        {
            base.Init(bd);
        }
    }
}
