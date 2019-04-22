using DeepCore.GameData.Zone;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using DeepCore.Log;
using ThreeLives.Battle.Skill.Plugins;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    /// <summary>
    /// 血量变更，正值为加血，负值为减血.
    /// </summary>
    public class TLBuff_HPChange : TLBuff
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
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_HPChange begin", null);
            SendTips(hitter, attacker);
            base.BuffBegin(hitter, attacker, state);
        }

        public override void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_HPChange end", null);

            base.BuffEnd(hitter, state);
        }

        public override int BuffHit(TLVirtual hitter, TLVirtual attacker, AttackSource source, ref TLVirtual.AtkAppendData result)
        {
            if (ChangeValueType == 0)
                return -ChangeValue;
            else if (ChangeValueType == 1)
                return (int)(TLFormula.PerToFloat(-ChangeValue) * hitter.mUnit.MaxHP);
            else
                return 0;
        }

        internal override void Init(TLBuffData bd)
        {
            data = bd as TLBuffData_HPChange;
            var d = data as TLBuffData_HPChange;
            if (data != null)
            {
                ChangeValue = d.ChangeValue;
                ChangeValueType = d.ChangeValueType;
            }

            base.Init(bd);
        }
    }
}
