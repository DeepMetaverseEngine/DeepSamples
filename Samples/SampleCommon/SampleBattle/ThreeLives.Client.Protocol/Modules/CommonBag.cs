using System;
using System.Collections.Generic;
using DeepCore;
using TLClient.Protocol.Modules.Package;
using TLProtocol.Data;

namespace TLClient.Protocol.Modules
{
    public enum BaseBagType : byte
    {
        FirstNormal = 1,
        FirstEquiped,
        FirstWarehourse,
        FirstVirtual,
        FirstAuction,
        FirstFate,
        FateEquiped,
    }


    public abstract class CommonBag : BasePackage
    {
        public byte Type { get; }

        public delegate ItemData OnCreateItemDataHandler(EntityItemData data);

        private OnCreateItemDataHandler mCreater;

        protected CommonBag(byte t, OnCreateItemDataHandler creater)
        {
            Type = t;
            mCreater = creater;
        }

        protected CommonBag(byte t)
        {
            Type = t;
        }

        protected virtual ItemData CreateItemData(EntityItemData item)
        {
            if (mCreater != null)
            {
                return mCreater.Invoke(item);
            }

            return new ItemData(item);
        }

        public ItemData FindItemByID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return FindFirstItemAs<ItemData>(it => it.ID == id);
        }

        public PackageSlot FindSlotByID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PackageSlot.InvaildSlot;
            }

            return FindFirstSlotAs(it => it.ID == id);
        }

        public ItemData[] FindItemAs(Predicate<ItemData> it)
        {
            return base.FindItemAs(it);
        }

        public PackageSlot[] FindSlotAs(Predicate<ItemData> it)
        {
            return base.FindSlotAs(it);
        }

        public ItemData FindFirstItemAs(Predicate<ItemData> it)
        {
            return base.FindFirstItemAs(it);
        }

        public PackageSlot FindFirstSlotAs(Predicate<ItemData> it)
        {
            return base.FindFirstSlotAs(it);
        }

        public virtual void InitData(HashMap<int, EntityItemData> slots)
        {
            if (slots == null)
            {
                return;
            }

            var addedList = new PackageSlot[slots.Count];
            var p = 0;
            foreach (var entry in slots)
            {
                addedList[p++] = new PackageSlot {Index = entry.Key, Item = CreateItemData(entry.Value)};
            }

            InitSlots(addedList);
        }

        public virtual void InitData(BagData data)
        {
            InitSize(data.EnableSize, data.MaxSize);
            InitData(data.Slots);
        }
        
        public void InitSize(int enableSize, int maxSize = 0)
        {
            MaxSize = maxSize;
            if (enableSize > 0)
            {
                Size = enableSize;
            }
            else
            {
                MaxSize = 0;
            }
        }

        public BagData BagData
        {
            get
            {
                var ret = new BagData()
                {
                    MaxSize = MaxSize,
                    EnableSize = MaxSize > 0 ? Size : 0,
                    Slots = new HashMap<int, EntityItemData>()
                };
                foreach (var slot in AllSlots)
                {
                    if (!(slot.Item is ItemData itemData))
                    {
                        continue;
                    }
                    ret.Slots.Add(slot.Index, itemData.Data);
                }
                return ret;
            }
        }
    }
}