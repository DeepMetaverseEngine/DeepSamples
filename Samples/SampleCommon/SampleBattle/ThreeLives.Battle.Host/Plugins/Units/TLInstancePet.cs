using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using System;
using TLBattle.Message;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Server.Plugins.Units
{
    public class TLInstancePet : InstancePet
    {
        private bool beginAtk = false;
        private int mRebirthTimeMS;
        public override bool IsActive { get { return false; } }
        public override bool IsAttackable { get { return false; } }

        public TLInstancePet(InstanceZone zone, AddUnit add) : base(zone, add)
        {

        }

        public void SetRebirthTime(int timeMS)
        {
            mRebirthTimeMS = timeMS;
        }

        public override int RebirthTimeMS()
        {
            return mRebirthTimeMS;
        }

        protected override void OnPetUpdate()
        {

            UpdateFollowMaster();
        }

        private void UpdateFollowMaster()
        {
            if (Master != null)
            {
                //最大距离.
                if (!CMath.includeRoundPoint(X, Y, Templates.CFG.PET_FOLLOW_DISTANCE_LIMIT, mMaster.X, mMaster.Y))
                {
                    this.transportToMaster();
                    FollowMaster();
                    return;
                }

                var status = MasterCombatStatus();

                //非战斗状态.
                if (status == CombatStateChangeEventB2C.BattleStatus.None)
                {
                    if (CurrentState is StateIdle)
                    {
                        FollowDistanceCheck(Templates.CFG.PET_FOLLOW_DISTANCE_MAX);
                    }

                }
                else//主人战斗状态.
                {
                    if (CurrentState is StateIdle)
                    {
                        if (FollowDistanceCheck(Templates.CFG.PET_FOLLOW_DISTANCE_LIMIT) == false)
                        {
                            if (beginAtk)
                                AtkMasterEnemy();
                            else
                            {
                                FollowDistanceCheck(Templates.CFG.PET_FOLLOW_DISTANCE_MAX);
                            }
                        }
                    }
                }
            }
        }

        private bool FollowDistanceCheck(float range)
        {
            float d = 0;
            d = Math.Max(range, Master.BodyBlockSize + this.BodyBlockSize);

            if (!CMath.includeRoundPoint(X, Y, d, mMaster.X, mMaster.Y))
            {
                FollowMaster();
                return true;
            }

            return false;
        }

        private void FollowMaster()
        {
            this.followMaster();
            beginAtk = false;
        }

        private CombatStateChangeEventB2C.BattleStatus MasterCombatStatus()
        {
            TLVirtual_Player pv = Master.Virtual as TLVirtual_Player;
            return pv.GetCombatState();
        }

        private void AtkMasterEnemy()
        {
            if (IsNoneSkill) { return; }

            uint id = VirtualPet().GetCurAtkTarget();

            if (id == 0) return;

            var unit = this.Parent.getUnit(id);

            if (unit != null && unit.IsDead == false)
            {
                changeState(new StateFollowAndAttack(this, unit, SkillTemplate.CastTarget.Enemy));
            }

        }

        private TLVirtual_Pet VirtualPet()
        {
            return this.Virtual as TLVirtual_Pet;
        }

        public void MasterCombatstateChange(CombatStateChangeEventB2C.BattleStatus status)
        {
            if (status == CombatStateChangeEventB2C.BattleStatus.None)
            {
                this.followMaster();
                beginAtk = false;
            }
            else
            {
                beginAtk = true;
            }
        }

        public void MasterBeginAtkUnit()
        {
            beginAtk = true;
        }


    }
}
