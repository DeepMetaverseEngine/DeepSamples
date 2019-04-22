using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    public class TLBuff : UnitBuff
    {
        public BattleAtkNumberEventB2C.AtkNumberType TipsType = BattleAtkNumberEventB2C.AtkNumberType.None;
        protected TLBuffData data = null;

        public override int GetAbilityID()
        {
            return 0;
        }

        public override void BuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            OnBuffBegin(hitter, attacker, state);
            base.BuffBegin(hitter, attacker, state);
            SendTips(hitter, attacker);
        }
        public override void BuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            OnBuffEnd(hitter, state);

            base.BuffEnd(hitter, state);
        }
        public override int BuffHit(TLVirtual hitter, TLVirtual attacker, AttackSource source, ref TLVirtual.AtkAppendData result)
        {
            return OnBuffHit(hitter, attacker, source, ref result);
        }

        protected virtual void OnBuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {

        }
        protected virtual void OnBuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {

        }
        protected virtual int OnBuffHit(TLVirtual hitter, TLVirtual attacker, AttackSource source, ref TLVirtual.AtkAppendData result)
        {
            return 0;
        }

        internal virtual void Init(TLBuffData bd)
        {
            TipsType = bd.TipsType;
        }

        public override TLBuffData ToBuffData()
        {
            return data;
        }

        protected void SendTips(TLVirtual hitter, TLVirtual attacker)
        {
            if (TipsType == TLBattle.Message.BattleAtkNumberEventB2C.AtkNumberType.None)
                return;
            hitter.SendBattleAtkNumberEventB2C(TipsType, attacker.mUnit.ID);
        }
    }
}
