using DeepCore;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using System.Text;
using TLBattle.Message;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Server.Plugins.Units
{
    public class TLInstanceFollowUnit : InstancePet
    {
        public override bool IsActive { get { return false; } }
        public override bool IsAttackable { get { return false; } }
        public override bool IsVisible => false;
        public bool mFollowUnit = true;
        public TLInstanceFollowUnit(InstanceZone zone, AddUnit add) : base(zone, add)
        {
            if (add.summoner != null)
            {
                add.summoner.OnRemoved += Summoner_OnRemoved;
            }
        }

        private void Summoner_OnRemoved(InstanceUnit unit)
        {
            if (!this.IsDisposed)
            {
                this.removeFromParent();
            }
        }

        protected override void onRemoved()
        {
            base.onRemoved();
            if (SummonerUnit != null && !SummonerUnit.IsDisposed)
            {
                SummonerUnit.OnRemoved -= Summoner_OnRemoved;
            }
        }

        protected override void OnPetUpdate()
        {
            if (mFollowUnit)
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

                if (CurrentState is StateIdle)
                {
                    FollowDistanceCheck(Templates.CFG.PET_FOLLOW_DISTANCE_MAX);
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
        }

        private CombatStateChangeEventB2C.BattleStatus MasterCombatStatus()
        {
            TLVirtual_Player pv = Master.Virtual as TLVirtual_Player;
            return pv.GetCombatState();
        }

        /// <summary>
        /// 能否被看见.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public bool IsVisibled(InstanceZoneObject src)
        {
            if (Master != null && Master.ID == src.ID)
                return true;
            else
                return false;
        }

        public void PauseFollow()
        {
            mFollowUnit = false;
        }

        public void ResumFollow()
        {
            mFollowUnit = true;
        }
    }
}
