using TLBattle.Common.Plugins;

namespace TLBattle.Client.Client
{
    public class TLClientVirtual_NPC : TLClientVirtual
    {
        public override int GetLv()
        {
            return mOwner.Level;
        }

        public override string GetName()
        {
            return (mOwner.Info.Name);
        }
    }
}
