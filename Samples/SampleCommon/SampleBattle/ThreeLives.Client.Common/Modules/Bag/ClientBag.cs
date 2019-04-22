using DeepCore;
using DeepCore.FuckPomeloClient;
using DeepMMO.Protocol;
using System;
using System.Collections.Generic;
using DeepCore.Log;
using TLClient.Protocol.Modules;
using TLClient.Protocol.Modules.Package;
using TLProtocol.Data;
using TLProtocol.Protocol.Client;

namespace TLClient.Modules.Bag
{
    public abstract class ClientBag : CommonBag
    {
        public PomeloClient Client { get; set; }

        private readonly HashMap<int, string> mLastSlotReason = new HashMap<int, string>();

        protected Logger Log;

        protected ClientBag(byte t, PomeloClient client) : base(t)
        {
            Log = LoggerFactory.GetLogger(GetType().Name + t);
            Client = client;
        }

        public string GetLastModifyReason(int index)
        {
            return mLastSlotReason.Get(index);
        }


        public void OnSlotNotify(HashMap<int,EntityItemData> slots, string reason)
        {
            BeginBatchListen();

            foreach (var slot in slots)
            {
                mLastSlotReason[slot.Key] = reason;
                if (slot.Value == null)
                {
                    //remove
                    RemoveItem(slot.Key);
                }
                else
                {
                    var old = GetItemAt<ItemData>(slot.Key);
                    var newItem = CreateItemData(slot.Value);
                    if (old != null)
                    {
                        var noID = string.IsNullOrEmpty(old.ID) && string.IsNullOrEmpty(newItem.ID);
                        if (old.TemplateID == newItem.TemplateID && (noID || old is VirtualItem))
                        {
                            UpdateItemCount(slot.Key, newItem.Count);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(old.ID) && old.ID == newItem.ID)
                            {
                                //更新属性
                                this[slot.Key] = newItem;
                                OnItemAttributeUpdated(slot.Key);
                            }
                            else
                            {
                                RemoveItem(slot.Key);
                                AddItem(slot.Key, newItem);
                            }
                        }
                    }
                    else
                    {
                        AddItem(slot.Key, newItem);
                    }
                }
            }

            foreach (var action in BatchActionList)
            {
                if (action.Index >= 0)
                {
                    action.Reason = mLastSlotReason.Get(action.Index);
                }
            }
            EndBatchListen();
        }

        public virtual void Swap(byte bagType, int index, uint count, Action<bool> resultcb)
        {
            var req = new ClientSwapItemRequest()
            {
                c2s_fromType = Type,
                c2s_toType = bagType,
                c2s_slot = new BagSlotConditon()
                {
                    index = index,
                    count = count
                }
            };
            Client.Request<ClientSwapItemResponse>(req, (ex, rp) =>
            {
                if (resultcb != null) resultcb.Invoke(ex == null && rp.s2c_code == Response.CODE_OK);
            });
        }

        public void PackUpItems(Action<bool> resultcb)
        {
            var req = new ClientPackagePackUpRequest()
            {
                c2s_type = Type
            };
            Client.Request<ClientPackagePackUpResponse>(req, (ex, rp) =>
            {
                if (resultcb != null) resultcb.Invoke(ex == null && rp.s2c_code == Response.CODE_OK);
            });
        }

        public void AddSize(int size, Action<bool> resultcb)
        {
            var req = new ClientAddBagSizeRequest
            {
                c2s_type = Type,
                c2s_targetSize = size
            };
            Client.Request<ClientAddBagSizeResponse>(req, (ex, rp) =>
            {
                var success = ex == null && rp.s2c_code == Response.CODE_OK;
                if (success)
                {
                    Size = rp.s2c_targetSize;
                }
                if (resultcb != null) resultcb.Invoke(success);
            });
        }
    }

    public class ClientNormalBag : ClientBag
    {
        public ClientNormalBag(byte t, PomeloClient client) : base(t, client)
        {
        }

        /// <summary>
        /// 使用某道具
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="resultcb"></param>
        public  void Use(int index, uint count, Action<bool> resultcb)
        {
            var req = new ClientUseItemRequest()
            {
                c2s_items = new List<BagSlotConditon>(1)
                {
                    {new BagSlotConditon() {index = index, count = count}}
                }
            };
            Client.Request<ClientUseItemResponse>(req, (ex, rp) =>
            {
                if (resultcb != null) resultcb.Invoke(ex == null && rp.s2c_code == Response.CODE_OK);
            });
        }

        /// <summary>
        /// 装备某道具
        /// </summary>
        /// <param name="index"></param>
        /// <param name="resultcb"></param>
        public void Equip(int index, Action<bool> resultcb)
        {
            Swap((byte) BaseBagType.FirstEquiped, index, 1, resultcb);
        }

        /// <summary>
        /// 存仓
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="resultcb"></param>
        public void PutToWarehouse(int index, uint count, Action<bool> resultcb)
        {
            Swap((byte) BaseBagType.FirstWarehourse, index, count, resultcb);
        }

        public void Sell(int index, uint count, Action<bool> resultcb)
        {
            var req = new ClientSellItemRequest
            {
                c2s_index = index,
                c2s_count = count,
            };
            Client.Request<ClientSellItemResponse>(req, (ex, rp) =>
            {
                if (resultcb != null) resultcb.Invoke(ex == null && rp.s2c_code == Response.CODE_OK);
            });
        }

        public void Decompose(int index, uint count, Action<bool> resultcb)
        {
            var req = new ClientDecomposeItemRequest
            {
                c2s_slots = new []{new BagSlotConditon{index = index, count = count}}
            };
            Client.Request<ClientDecomposeItemResponse>(req, (ex, rp) =>
            {
                if (resultcb != null) resultcb.Invoke(ex == null && rp.s2c_code == Response.CODE_OK);
            });
        }

    }

    public class ClientFateBag : ClientBag
    {
        public ClientFateBag(byte t, PomeloClient client) : base(t, client)
        {

        }

        /// <summary>
        /// 装备某道具
        /// </summary>
        /// <param name="index"></param>
        /// <param name="resultcb"></param>
        public void Equip(int index, Action<bool> resultcb)
        {
            Swap((byte)BaseBagType.FateEquiped, index, 1, resultcb);
        }
    } 

    public class ClientEquipBag : ClientBag
    { 

        public ClientEquipBag(byte t, PomeloClient client) : base(t, client)
        {
        }

        /// <summary>
        /// 卸下某装备
        /// </summary>
        /// <param name="index"></param>
        /// <param name="resultcb"></param>
        public virtual void UnEquip(int index, Action<bool> resultcb)
        {
            Swap((byte)BaseBagType.FirstNormal, index, 1, resultcb);
        }
    }

    public class ClientFateEquipBag : ClientBag
    {
        public ClientFateEquipBag(byte t, PomeloClient client) : base(t, client)
        {
        }

        /// <summary>
        /// 卸下某装备
        /// </summary>
        /// <param name="index"></param>
        /// <param name="resultcb"></param>
        public virtual void UnEquip(int index, Action<bool> resultcb)
        {
            Swap((byte)BaseBagType.FirstFate, index, 1, resultcb);
        }
    }

    public class ClientWarehourse : ClientBag
    {
        public ClientWarehourse(byte t, PomeloClient client) : base(t, client)
        {
        }

        /// <summary>
        /// 取仓
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="resultcb"></param>
        public virtual void PutToNormalBag(int index, uint count, Action<bool> resultcb)
        {
            Swap((byte) BaseBagType.FirstNormal, index, count, resultcb);
        }
    }


    public class ClientVirtualBag : ClientBag
    {
        public delegate void ModifyActionHandler(ulong count, string reason);
        public ClientVirtualBag(byte t, PomeloClient client) : base(t, client)
        {
        }


        public ulong Silver
        {
            get { return CountItemAs<VirtualItem>(it => it.Key == VirtualItem.SilverKey); }
        }

        public ulong Copper
        {
            get { return CountItemAs<VirtualItem>(it => it.Key == VirtualItem.CopperKey); }
        }

        public ulong Diamond
        {
            get { return CountItemAs<VirtualItem>(it => it.Key == VirtualItem.DiamondKey); }
        }

        public ulong Fate
        {
            get { return CountItemAs<VirtualItem>(it => it.Key == VirtualItem.FateKey); }
        }
        

        protected override ItemData CreateItemData(EntityItemData item)
        {
            return new VirtualItem(item);
        }


        protected override void Disposing()
        {
            base.Disposing();
            mActionMap.Clear();
        }

        private readonly HashMap<string, HashSet<ModifyActionHandler>> mActionMap = new HashMap<string, HashSet<ModifyActionHandler>>();

        public void AddAction(string key, ModifyActionHandler act)
        {
            var list = mActionMap.Get(key);
            if (list == null)
            {
                list = new HashSet<ModifyActionHandler> {act};
                mActionMap.Add(key, list);
            }
            else
            {
                list.Add(act);
            }
        }

        public void RemoveAction(string key, ModifyActionHandler act)
        {
            var list = mActionMap.Get(key);
            list?.Remove(act);
        }

        protected void InvokeAction(string key,string reason)
        {
            var list = mActionMap.Get(key);
            if (list != null)
            {
                var count = CountItemAs<VirtualItem>(it => it.Key == key);
                foreach (var action in list)
                {
                    action.Invoke(count, reason);
                }
            }
        }

        public void SubscribSilver(ModifyActionHandler upAction)
        {
            AddAction(VirtualItem.SilverKey, upAction);
        }

        public void UnSubscribSilver(ModifyActionHandler upAction)
        {
            RemoveAction(VirtualItem.SilverKey, upAction);
        }

        public void SubscribCopper(ModifyActionHandler upAction)
        {
            AddAction(VirtualItem.CopperKey, upAction);
        }

        public void UnSubscribCopper(ModifyActionHandler upAction)
        {
            RemoveAction(VirtualItem.CopperKey, upAction);
        }

        public void SubscribDiamond(ModifyActionHandler upAction)
        {
            AddAction(VirtualItem.DiamondKey, upAction);
        }

        public void UnSubscribDiamond(ModifyActionHandler upAction)
        {
            RemoveAction(VirtualItem.DiamondKey, upAction);
        }

        public void SubscribFate(ModifyActionHandler upAction)
        {
            AddAction(VirtualItem.FateKey, upAction);
        }

        public void UnSubscribFate(ModifyActionHandler upAction)
        {
            RemoveAction(VirtualItem.FateKey, upAction);
        }

        protected override void OnItemCountChanged(int index, uint from)
        {
            base.OnItemCountChanged(index, from);
            var item = GetItemAt<VirtualItem>(index);
            InvokeAction(item.Key, GetLastModifyReason(index));
        }

        protected override void OnItemAdded(int index)
        {
            base.OnItemAdded(index);
            var item = GetItemAt<VirtualItem>(index);
            InvokeAction(item.Key, GetLastModifyReason(index));
        }

        protected override void OnItemRemoved(IPackageItem lastItem, int index)
        {
            base.OnItemRemoved(lastItem, index);
            var item = (VirtualItem)lastItem;
            InvokeAction(item.Key, GetLastModifyReason(index));
        }
    }

    public class ClientSimpleExternBag : ClientBag
    {
        public ClientSimpleExternBag(byte t, PomeloClient client) : base(t, client)
        {
        }

    }
}