using TLBattle.Common.Data;
using TLBattle.Server.Plugins.Virtual;
using TLCommonSkill.Plugins.Buff;

namespace TLCommonSkill.Plugins.ActiveSkills
{
    public class TLActiveSkillTest : TLSkillBase
    {
        public int ID = -999;

        public override int SkillID
        {
            get { return ID; }
            set { ID = value; }
        }

        protected override void OnSkillLogicEvent(BattleParams param)
        {
            param.AttackType = TLVirtual.AttackType.phyAtk;
            //获取技能数据.
            TLSkillData data = GetSkillData(param);
            //血量变更.
            TLBuff_HPChange buff = new TLBuff_HPChange();
            //+100.
            buff.ChangeValue = 100;
            //添加buff.
            param.Hitter.AddTLBuff(90000, param.Attacker, buff);

        }
    }
}
