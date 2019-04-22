using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using DeepCore.Log;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    public class TLBuff_MPChange : TLBuff
    {
        /// <summary>
        /// 正为加血，负为减血.
        /// </summary>
        public int ChangeValue;

        /// <summary>
        /// 改变类型0绝对值，1万分比.
        /// </summary>
        public byte ChangeValueType;

        protected override void OnBindTemplate(BuffTemplate buffTemplate)
        {
            if (ChangeValue > 0)
            {
                buffTemplate.IsHarmful = false;
            }
            else
            {
                buffTemplate.IsHarmful = true;
            }

            base.OnBindTemplate(buffTemplate);
        }

        public override void BuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_MPChange begin", null);

            if (ChangeValueType == 0)
                hitter.AddMP(ChangeValue);
            else
                hitter.AddMPPct(ChangeValue / 100);

            SendTips(hitter, attacker);
            base.BuffBegin(hitter, attacker, state);
        }

        public override void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_MPChange end", null);

            base.BuffEnd(hitter, state);
        }

        internal override void Init(TLBuffData bd)
        {
            data = bd as TLBuffData_MPChange;
            var d = data as TLBuffData_MPChange;
            if (data != null)
            {
                ChangeValue = d.ChangeValue;
                ChangeValueType = d.ChangeValueType;
            }

            base.Init(bd);
        }
    }
}
