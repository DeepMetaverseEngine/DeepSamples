using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.BAG_START + 1)]
    public class BagSlotData : ISerializable
    {
        public int index;
        public EntityItemData item;
    }


    [MessageType(TLConstants.BAG_START + 2)]
    public class BagData : ISerializable
    {
        public int MaxSize;
        public int EnableSize;
        public HashMap<int, EntityItemData> Slots;

    }

    [MessageType(TLConstants.BAG_START + 3)]
    public class BagSlotConditon : ISerializable
    {
        public int index;
        public uint count;
    }

    public class BagElement : IStructMapping
    {
        public string ID;
        public int TemplateID;
        public uint Count;
        public bool CanTrade;
        public ItemPropertiesData Properties;
        public static BagElement FromEntityItem(EntityItemData item)
        {
            return new BagElement
            {
                ID = item.SnapData.ID,
                TemplateID = item.SnapData.TemplateID,
                Count = item.SnapData.Count,
                CanTrade = item.CanTrade,
                Properties = item.Properties
            };
        }
    }

    [PersistType]
    public class BagStoreData : IObjectMapping
    {
        [PersistField] public int MaxSize;
        [PersistField] public int EnableSize;
        [PersistField] public HashMap<int, BagElement> Slots;
    }

}