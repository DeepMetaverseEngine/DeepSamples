using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using TLBattle.Message;

namespace TLBattle.Client
{
    /// <summary>
    /// 客户端中间层.
    /// </summary>
    public class TLClientVirtual : IVirtualClientUnit
    {
        /// <summary>
        /// 战斗状态监听.
        /// </summary>
        public delegate void OnCombatStateChangeEvent(TLClientVirtual clientVirtual, CombatStateChangeEventB2C.BattleStatus status);
        /// <summary>
        /// 战斗状态监听.
        /// </summary>
        protected OnCombatStateChangeEvent event_OnCombatStateChangeHandle;
        /// <summary>
        /// 战斗状态变更.
        /// </summary>
        public event OnCombatStateChangeEvent OnCombatStateChangeHandle
        {
            add { event_OnCombatStateChangeHandle += value; }
            remove { event_OnCombatStateChangeHandle -= value; }
        }

        private uint mLastTarget;
        private System.Action<uint> mCurTargetChaned;
        /// <summary>
        /// 当前目标变更.
        /// </summary>
        public event System.Action<uint> OnCurTargetChanged
        {
            add { mCurTargetChaned += value; }
            remove { mCurTargetChaned -= value; }
        }

        protected ZoneUnit mOwner = null;
        private CombatStateChangeEventB2C.BattleStatus mLastBattleStatus = CombatStateChangeEventB2C.BattleStatus.None;

        public void OnInit(ZoneUnit owner)
        {
            DoInit(owner);
            mOwner = owner;
            mOwner.OnDoEvent += MOwner_OnDoEvent;
        }

        public virtual void OnUpdate(int intervalMS)
        {
            OnBattleStatusUpdate(intervalMS);
            UpdateCurTarget();
        }

        public void OnDispose(ZoneUnit owner)
        {
            DoDispose(owner);
            if (mOwner != null)
            {
                mOwner.OnDoEvent -= MOwner_OnDoEvent;
                mOwner = null;
            }
        }

        protected virtual void OnBattleStatusUpdate(int intervalMS)
        {
            if (mOwner != null)
            {
                if (mLastBattleStatus != (CombatStateChangeEventB2C.BattleStatus)mOwner.Dummy_0)
                {
                    mLastBattleStatus = (CombatStateChangeEventB2C.BattleStatus)mOwner.Dummy_0;

                    if (event_OnCombatStateChangeHandle != null)
                    {
                        event_OnCombatStateChangeHandle.Invoke(this, mLastBattleStatus);
                    }
                }
            }
        }

        protected void UpdateCurTarget()
        {
            if (mLastTarget != this.mOwner.CurrentTarget)
            {
                mLastTarget = this.mOwner.CurrentTarget;
                if (mCurTargetChaned != null)
                    mCurTargetChaned.Invoke(this.mOwner.CurrentTarget);
            }
        }

        protected virtual void MOwner_OnDoEvent(ZoneObject obj, ObjectEvent e)
        {

        }

        protected virtual void DoInit(ZoneUnit owner)
        {

        }

        protected virtual void DoDispose(ZoneUnit owner)
        {
            mCurTargetChaned = null;
            event_OnCombatStateChangeHandle = null;
        }

        public virtual int GetLv()
        {
            return 0;
        }

        public virtual string GetName()
        {
            return "";
        }

        /// <summary>
        /// 获取当前战斗状态 PVP/PVE/None.
        /// </summary>
        /// <returns></returns>
        public CombatStateChangeEventB2C.BattleStatus GetBattleStatus()
        {
            return mLastBattleStatus;
        }

        public virtual bool IsEnemy(ZoneUnit target)
        {
            if ((target.Virtual as TLClientVirtual).IsNeutrality())
                return false;

            return this.mOwner.Force != target.Force;
        }

        public virtual bool IsAllies(ZoneUnit target)
        {
            return this.mOwner.Force == target.Force;
        }

        public bool IsNeutrality()
        {
            if (this.mOwner != null)
                return this.mOwner.Force == 0;
            return false;
        }
    }
}
