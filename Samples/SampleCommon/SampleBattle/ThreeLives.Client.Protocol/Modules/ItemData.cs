using System.Collections.Generic;
using System.Linq;
using TLClient.Protocol.Modules.Package;
using TLProtocol.Data;

namespace TLClient.Protocol.Modules
{
    public class ItemData : IPackageItem
    {
        public EntityItemData Data { get; protected set; }

        public bool CanTrade => Data.CanTrade;
        public  int TemplateID => Data.SnapData.TemplateID;
        public  uint MaxStackCount => Data.SnapData.MaxStackCount;
        public uint PreCount { get; private set; }
        public string ID => Data.SnapData.ID;
        public string Flag { get; set; }
        public List<ItemPropertyData> Properties => Data.Properties?.Properties;

        public uint Count
        {
            get { return Data.SnapData.Count; }
            set
            {
                PreCount = Count;
                Data.SnapData.Count = value;
            }
        }

        public int SlotIndex { get; set; }

        public ItemData(EntityItemData item)
        {
            Data = item;
        }

        public virtual object Clone()
        {
            return new ItemData((EntityItemData)Data.Clone());
        }

        public override string ToString()
        {
            return Data.ToString();
        }

        public  bool CompareAttribute(IPackageItem other)
        {
            var itemData = other as ItemData;

            if (itemData == null)
            {
                return false;
            }
            if (Properties == null)
            {
                return itemData.Properties == null;
            }
            if (itemData.Properties == null)
            {
                return false;
            }
            itemData.Properties.Sort();
            Properties.Sort();
            return Properties.SequenceEqual(itemData.Properties);
            //return new HashSet<ItemPropertyData>(Properties).SetEquals(itemData.Properties);
        }

        public virtual int CompareTo(IPackageItem other)
        {
            if (other == null)
            {
                return -1;
            }
            return TemplateID.CompareTo(other.TemplateID);
        }
    }

    public interface IVirtualItem
    {
        string Key { get; }
    }
    public class VirtualItem : ItemData, IVirtualItem
    {
        public const string DiamondKey = "Diamond";
        public const string CopperKey = "Copper";
        public const string SilverKey = "Silver";
        public const string ContributionKey = "Contribution";
        

        public const string FateKey = "Fate";
        /// <summary>
        /// 战场功勋
        /// </summary>
        public const string ExploitKey = "Exploit";
        
        public const string LuckyMoneyKey = "LuckyMoney";//福气币

        public static EntityItemData CreateEntityItemData(string key, int templateId, uint count)
        {
            return new EntityItemData()
            {
                SnapData = new ItemSnapData() {ID = key, TemplateID = templateId, MaxStackCount = uint.MaxValue, Count = count},
            };
        }
          
        public VirtualItem(string key, int templateId)
            : base(CreateEntityItemData(key, templateId, 0))

        {
        }

        public VirtualItem(EntityItemData item) : base(item)
        {
            
        }

        public override object Clone()
        {
            return new VirtualItem((EntityItemData)Data.Clone());
        }

        public string Key => Data.SnapData.ID;
    }
}