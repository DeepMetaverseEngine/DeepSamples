using DeepCore;
using DeepCore.GameData.Data;
using DeepCore.GameData.RTS;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using DeepCore.Vector;
using System;

namespace TLBattle.Server.Plugins.Units
{
    public class TLGuardInPosition : InstanceUnit.State
    {
        private readonly float mRange;
        private readonly Vector2 mOrginPos;
        private readonly TimeExpire<int> mHoldTimeExpire;
        private readonly TimeExpire<int> mMoveTimeExpire;
        private readonly ZoneMoveAI mMoveAI;

        private readonly int minHoldTimeMS;
        private readonly int maxHoldTimeMS;
        private readonly int minMoveTimeMS;
        private readonly int maxMoveTimeMS;

        private bool moveState = true;

        private MoveBlockResult mLastResult;
        public MoveBlockResult LastMoveResult { get { return mLastResult; } }

        public TLGuardInPosition(InstanceUnit unit, Vector2 orginPos,
                                    int minHoldTimeMS, int maxHoldTimeMS,
                                    int minMoveTimeMS, int maxMoveTimeMS,
                                    float range)
            : base(unit)
        {

            this.minHoldTimeMS = minHoldTimeMS;
            this.maxHoldTimeMS = maxHoldTimeMS;
            this.minMoveTimeMS = minMoveTimeMS;
            this.maxMoveTimeMS = maxMoveTimeMS;

            this.mHoldTimeExpire = new TimeExpire<int>(GetHoldTimeMS());
            this.mMoveTimeExpire = new TimeExpire<int>(GetMoveTimeMS());

            this.mMoveAI = new ZoneMoveAI(unit, false);
            this.mRange = Math.Abs(range);
            this.mOrginPos = orginPos;
        }

        public override bool onBlock(InstanceUnit.State new_state)
        {
            return true;
        }

        protected override void onStart()
        {
            MoveStart();
        }

        protected override void onStop()
        {

        }

        protected override void onUpdate()
        {
            unit.Pause(!unit.IsNearPlayer());

            if (moveState)
            {
                if (mMoveTimeExpire.Update(zone.UpdateIntervalMS))
                {
                    mMoveTimeExpire.Reset(GetMoveTimeMS());
                    moveState = false;
                    HoldStart();
                }
                else
                {
                    mLastResult = mMoveAI.Update();
                    if ((mLastResult.result & MoveResult.RESULTS_MOVE_END) != 0)
                    {
                        MoveStart();
                    }
                }
            }
            else
            {
                if (mHoldTimeExpire.Update(zone.UpdateIntervalMS))
                {
                    mHoldTimeExpire.Reset(GetHoldTimeMS());
                    MoveStart();
                }
                else
                {
                    //Do Nothing.
                }
            }
        }

        private void MoveStart()
        {
            Vector2 target = this.FindTargetPos();
            if (target != null)
            {
                this.mMoveAI.FindPath(target.x, target.y);
                unit.SetActionStatus(UnitActionStatus.Move);
                moveState = true;
            }
        }

        private void HoldStart()
        {
            unit.SetActionStatus(UnitActionStatus.Idle);
            moveState = false;
        }

        private int GetMoveTimeMS()
        {
            return zone.RandomN.Next(minMoveTimeMS, maxMoveTimeMS);
        }

        private int GetHoldTimeMS()
        {
            return zone.RandomN.Next(minHoldTimeMS, maxHoldTimeMS);
        }

        /// <summary>
        /// 搜索要去的地方
        /// </summary>
        /// <returns></returns>
        protected virtual Vector2 FindTargetPos()
        {
            return zone.PathFinderTerrain.FindNearRandomMoveableNode(zone.RandomN, mOrginPos.X, mOrginPos.Y, mRange);
        }
    }
}
