using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;
using static TLBattle.Server.TLZoneSpaceDivision;

namespace TLBattle.Server.Plugins.Units
{
    public class TLInstanceNPC : InstanceGuard
    {
        private bool mIsBattleable;
        public override bool IsNature { get { return !this.mIsBattleable; } }
        public override bool IsActive { get { return this.mIsBattleable; } }
        public override bool IsAttackable { get { return this.mIsBattleable; } }

        private TLUnitProperties mUnitProperties;

        public TLInstanceNPC(InstanceZone zone, AddUnit add) : base(zone, add)
        {
            //阵营为0无法被攻击.
            if (add.force == 0)
            {
                this.mIsBattleable = false;
            }
            mUnitProperties = (add.info.Properties as TLUnitProperties);

        }
        protected override void onUpdate()
        {
            //if (this.IsNearPlayer())
            {
                base.onUpdate();
            }
        }

        public override void doSomething()
        {
            if (!mUnitProperties.DoNothingWhenHappy)
            {
                base.doSomething();
            }
        }
        public override void InitSkills(LaunchSkill baseSkill, params LaunchSkill[] skills)
        {
            if (this.Virtual == null ||
               (this.Virtual as TLVirtual).IsFinishModuleInit() == false)
            {
                return;
            }

            base.InitSkills(baseSkill, skills);
        }
        protected override void onNewStateBeginChange(State old_state, ref State new_state)
        {
            if (new_state is StateBackToPosition)
            {
                //回血.
                this.AddHP(this.MaxHP);
                this.AddMP(this.MaxMP);
                //去BUFF.
                this.clearBuffs();

            }

            base.onNewStateBeginChange(old_state, ref new_state);
        }

    }
}
