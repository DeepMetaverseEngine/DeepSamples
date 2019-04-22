using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.Helper;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using DeepCore.Vector;
using TLBattle.Common.Data;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Server.Plugins.Units
{
    public class TLInstanceMonster : InstanceGuard
    {
        private TLUnitGuardConfig mGuardConfig;

        private TLUnitProperties mUnitProperties;
        public TLInstanceMonster(InstanceZone zone, AddUnit add) : base(zone, add)
        {
            AddListener();
            mUnitProperties = (add.info.Properties as TLUnitProperties);
            InitGuardConfig(mUnitProperties.UnitGuardConfig);
        }
        protected override void onUpdate()
        {
            if (IsPaused)
            {
                if (this.IsNearPlayer())
                {
                    this.Pause(false);
                    this.doSomething();
                }
            }
            base.onUpdate();
        }

        protected override void Disposing()
        {
            this.RemoveListener();
            base.Disposing();
        }
        public override void guardInPosition(Vector2 pos)
        {
            if (mGuardConfig != null)
                ChangeTLGuardInPosition(pos);
            else
                base.guardInPosition(pos);
        }
        public override void InitSkills(LaunchSkill baseSkill, LaunchSkill[] skills)
        {
            if (this.Virtual != null && (this.Virtual as TLVirtual).IsFinishModuleInit())
            {
                base.InitSkills(baseSkill, skills);
            }
        }

        public override void doSomething()
        {
            if (!mUnitProperties.DoNothingWhenHappy)
            {
                base.doSomething();
            }
            else
            {
                if (CurrentState is StateSkill)
                {
                    var target = TracingTarget;
                    if (target != null)
                    {
                        if (tryMoveScatterTarget(target)) { return; }
                    }
                }
                if (followAndAttack(mHateSystem.GetHated(), AttackReason.Tracing))
                {
                    return;
                }
                mTracingTarget = null;
                SetEnableView(true);
                if (mGuardTarget != null && mGuardTarget.IsActive)
                {
                    changeState(this.mGuardTarget);
                    return;
                }
                if (mRunningPath != null && !mRunningPath.IsDone)
                {
                    changeState(this.mRunningPath);
                    return;
                }
                base.startIdle();
            }
        }

        private void AddListener()
        {

        }

        private void RemoveListener()
        {

        }

        protected override void onUpdateRecover()
        {
            //do nothing.
        }

        public override SkillState getRandomLaunchableExpectSkill(SkillTemplate.CastTarget expectTarget)
        {
            var ss = (this.Virtual as TLVirtual).SkillModule.GetLoopSkill();

            if (ss != null)
            {
                return ss;
            }
            else
            {
                return base.getRandomLaunchableExpectSkill(expectTarget);
            }

        }

        public override SkillState getRandomLaunchableExpectSkill(InstanceUnit target, SkillTemplate.CastTarget expectTarget, AttackReason reason = AttackReason.Tracing, bool checkRange = false)
        {
            var ss = (this.Virtual as TLVirtual).SkillModule.GetLoopSkill();

            if (ss != null)
            {
                return ss;
            }
            else
            {
                return base.getRandomLaunchableExpectSkill(target, expectTarget, reason, checkRange);
            }
        }

        private void InitGuardConfig(TLUnitGuardConfig config)
        {
            if (config == null || config.IsPatrolNpc == false) { return; }
            mGuardConfig = config;
        }

        private void ChangeTLGuardInPosition(Vector2 pos)
        {
            var state = new TLGuardInPosition(this, pos,
                                               mGuardConfig.PatrolWaitMinTime, mGuardConfig.PatrolWaitMaxTime,
                                               mGuardConfig.PatrolRunMinTime, mGuardConfig.PatrolRunMaxTime,
                                               mGuardConfig.PatrolDistance);

            int OperationID = 0;

            //巡逻状态下速度变更.
            state.OnStart += (obj, s) =>
            {
                TLPropObject prop = new TLPropObject();
                prop.Type = TLPropObject.ValueType.Percent;
                prop.Prop = TLPropObject.PropType.runspeed;
                prop.Value = mGuardConfig.PatrolRunSpeed;
                OperationID = (this.Virtual as TLVirtual).PropModule.AddPropObject(prop);
            };

            state.OnStop += (obj, s) =>
            {
                (this.Virtual as TLVirtual).PropModule.RemovePropObject(OperationID);
            };

            changeState(state);
        }

        protected override void onNewStateBeginChange(State old_state, ref State new_state)
        {
            StateChangeLogic(old_state, ref new_state);

            base.onNewStateBeginChange(old_state, ref new_state);
        }

        private void StateChangeLogic(State old_state, ref State new_state)
        {
            //返回原点,恢复血量.
            if (new_state is StateBackToPosition && mGuardConfig != null)
            {
                if (mGuardConfig.ReturnToFullHP)
                {
                    //回血.
                    this.AddHP(this.MaxHP);
                    this.AddMP(this.MaxMP);
                    //去BUFF.
                    this.clearBuffs();
                }
            }
        }

        protected override void onDamaged(InstanceUnit attacker, AttackSource attack, int reduceHP)
        {
            // 被攻击转火
            mHateSystem.OnHitted(attacker, attack, reduceHP);

            if (IsNoneSkill)
                return;

            onAddEnemy(attacker, true, AttackReason.Damaged);
        }
    }
}
