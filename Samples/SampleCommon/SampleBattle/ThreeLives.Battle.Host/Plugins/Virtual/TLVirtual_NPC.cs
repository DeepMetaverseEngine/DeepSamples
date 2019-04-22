using DeepCore.GameHost.Instance;
using TLBattle.Message;

namespace TLBattle.Server.Plugins.Virtual
{
    public class TLVirtual_NPC : TLVirtual
    {
        public TLVirtual_NPC(InstanceUnit unit) : base(unit)
        {
        }
        public override void SetCombatState(CombatStateChangeEventB2C.BattleStatus value, byte reason = 0)
        {
            //怪物强制转为PVE.
            if (value == CombatStateChangeEventB2C.BattleStatus.PVP)
            {
                value = CombatStateChangeEventB2C.BattleStatus.PVE;
            }
            base.SetCombatState(value, reason);
        }
    }
}
