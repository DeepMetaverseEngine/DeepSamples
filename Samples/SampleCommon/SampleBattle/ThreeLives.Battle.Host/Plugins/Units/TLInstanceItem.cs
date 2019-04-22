using DeepCore.GameHost.Instance;
using TLBattle.Common.Plugins;

namespace TLBattle.Server.Plugins.Units
{
    public class TLInstanceItem : InstanceItem
    {
        public TLInstanceItem(InstanceZone zone, AddItem add) : base(zone, add)
        {
        }

        public bool AllowAutoPick()
        {
            var zp = Info.Properties as TLItemProperties;
            return zp.AllowAutoPick;
        }

        //TODO 封装成AOI方法
        public string PrivatePlayerUUID { get; set; }

        public override bool IsPickable(InstanceUnit u)
        {
            if (base.IsPickable(u))
            {
                var zp = Info.Properties as TLItemProperties;

                //不是同阵营且不是公共物品无法拾取.
                if (zp.PickUnitForce != 0 && u.Force != zp.PickUnitForce)
                {
                    return false;
                }


                //保护时间.
                TLBattle.Common.Data.TLDropItem di = GenSyncItemInfo(false).ExtData as TLBattle.Common.Data.TLDropItem;

                if (di == null)
                {
                    return true;
                }

                if (this.PassTimeMS < di.FreezeTime)   //未过保护时间.
                {
                    return false;
                }

                TLInstancePlayer player = u as TLInstancePlayer;

                if (this.PassTimeMS < di.ProtectTime) //归属者保护时间.
                {
                    if (di.HeirsList != null && di.HeirsList.Count > 0)
                    {
                        //判断拾取者是否为归属者.
                        if (di.HeirsList != null && player.PlayerUUID != null && di.HeirsList.Contains(player.PlayerUUID))
                        {
                            return VerifyInventory(di, player);
                        }
                        else { return false; }
                    }
                    else
                    {
                        return VerifyInventory(di, player);
                    }
                }
                else
                {
                    return VerifyInventory(di, player);
                }
            }

            return false;
        }

        private bool VerifyInventory(Common.Data.TLDropItem item, TLInstancePlayer picker)
        {
            bool ret = false;

            if (item.VirtualItem)
            {
                ret = true;
            }
            else
            {
                ret = picker.VerifyInventory();
            }

            return ret;
        }
    }
}
