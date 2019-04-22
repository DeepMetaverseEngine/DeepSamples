using DeepCore.IO;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_GRID_REFINE_START + 1)]
    [PersistType]
    public class GridRefineData : ISerializable
    {
        [PersistField]
        public int EquipPos;

        [PersistField]
        public int Rank;

        [PersistField]
        public int Lv;

        //基础属性万分比加成
        [PersistField]
        public int BaseAttrPlus;

        [PersistField]
        public List<ItemPropertyData> Properties;

        public override string ToString()
        {
            return GetType().Name + EquipPos;
        }
    }

    [MessageType(TLConstants.TL_GRID_REFINE_START + 2)]
    [PersistType]
    public class GridGemSlotData : ISerializable
    {
        [PersistField]
        public int SlotIndex;

        [PersistField]
        public int GemTemplateID;

        [PersistField]
        public List<ItemPropertyData> Properties;
    }

    [MessageType(TLConstants.TL_GRID_REFINE_START + 3)]
    [PersistType]
    public class GridGemData : ISerializable
    {
        [PersistField]
        public int EquipPos;

        [PersistField]
        public List<GridGemSlotData> Slots;

        public override string ToString()
        {
            return GetType().Name + EquipPos;
        }
    }
}