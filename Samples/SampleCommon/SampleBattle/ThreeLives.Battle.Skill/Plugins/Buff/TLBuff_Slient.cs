using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using DeepCore.Log;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    /// <summary>
    /// 沉默.
    /// </summary>
    public class TLBuff_Slient : TLBuff
    {
        protected override void OnBindTemplate(BuffTemplate buffTemplate)
        {
            buffTemplate.IsSilent = true;
            base.OnBindTemplate(buffTemplate);
        }

        protected override void OnBuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_Slient begin", null);
        }

        protected override void OnBuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_Slient end", null);
        }

        internal override void Init(TLBuffData bd)
        {
            data = bd as TLBuffData_Slient;
            base.Init(bd);
        }
    }
}
