using DeepCore.GameSlave;
using TLBattle.Message;

namespace TLBattle.Client
{
    public class TLClientVirtual_Pet : TLClientVirtual
    {
        private PetVisibleDataB2C mData = null;

        protected override void DoInit(ZoneUnit owner)
        {
            base.DoInit(owner);
            mData = owner.SyncInfo.VisibleInfo as PetVisibleDataB2C;
        }

        public override string GetName()
        {
            string ret = null;

            if (mData != null)
            {
                ret = mData.BaseInfo.name;
            }
            else
            {
                ret = mOwner.Info.Name;
            }

            return ret;
        }

        public override int GetLv()
        {
            int ret = 0;

            if (mData != null)
            {
                ret = mData.BaseInfo.level;
            }
            else
            {
                ret = mOwner.Level;
            }

            return ret;
        }
    }
}
