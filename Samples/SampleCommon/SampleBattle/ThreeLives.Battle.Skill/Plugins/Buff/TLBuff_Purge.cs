using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using DeepCore.Log;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    public class TLBuff_Purge : TLBuff
    {
        private List<int> BuffList = null;
        private TLBuffData_Purge.PurgeBuffType Purgetype;

        protected override void OnBindTemplate(BuffTemplate buffTemplate)
        {
            base.OnBindTemplate(buffTemplate);
        }

        protected override void OnBuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_Purge begin", null);
            if (BuffList == null)
            {
                hitter.RemoveAllBuffs((bs) =>
                {
                    if (Purgetype == TLBuffData_Purge.PurgeBuffType.All)
                        return true;
                    else if (Purgetype == TLBuffData_Purge.PurgeBuffType.Harmful && bs.Data.IsHarmful == true)
                        return true;
                    else if (Purgetype == TLBuffData_Purge.PurgeBuffType.Beneficial && bs.Data.IsHarmful == false)
                        return true;
                    return false;
                }, "TLBuff_Purge");
            }
            else
            {
                BuffList.FindAll((id) =>
                {
                    hitter.RemoveBuff(id, "TLBuff_Purge");
                    return false;
                });
            }
        }

        protected override void OnBuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_Purge end", null);
        }

        internal override void Init(TLBuffData bd)
        {
            data = bd as TLBuffData_Purge;
            var d = bd as TLBuffData_Purge;

            if (d.BuffList != null)
            {
                BuffList = new List<int>();
                BuffList.AddRange(d.BuffList);
            }

            Purgetype = d.PurgeType;

            base.Init(bd);
        }
    }
}
