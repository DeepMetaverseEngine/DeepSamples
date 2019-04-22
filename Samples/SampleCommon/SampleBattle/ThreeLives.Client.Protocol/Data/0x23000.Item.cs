using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using Constants = TLProtocol.Data.TLConstants;

namespace TLProtocol.Data
{
    /// <summary>
    /// 实体类型道具，特殊属性
    /// </summary>
    [MessageType(Constants.ITEM_START + 4)]
    public class ItemPropertyData : ISerializable, ICloneable, IEquatable<ItemPropertyData>, IComparable<ItemPropertyData>
    {
        public static readonly string FixedAttributeTag = nameof(FixedAttributeTag);
        public static readonly string ExtraAttributeTag = nameof(ExtraAttributeTag);
        public static readonly string GridGemAttributeTag = nameof(GridGemAttributeTag);
        public static readonly string GridRefineAttributeTag = nameof(GridRefineAttributeTag);
        public static readonly string WingAttributeTag = nameof(WingAttributeTag);
        public static readonly string ExtraBuffTag = nameof(ExtraBuffTag);
        public static readonly string IsLockedTag = nameof(IsLockedTag);
        public static readonly string ColorRGBTag = nameof(ColorRGBTag);

        public static readonly string FateTag = nameof(FateTag);

        /// <summary>
        /// 分类序号
        /// </summary>
        public int Index;

        /// <summary>
        /// ID和Name任选其一
        /// </summary>
        public string Name;

        /// <summary>
        /// ID和Name任选其一
        /// </summary>
        public int ID;

        //1 固定比， 2万分比
        public int ValueType;

        public int Value;
        public string Tag;
        public List<ItemPropertyData> SubAttributes;

        public bool GetIsLocked()
        {
           return  SubAttributes?.Exists(e => e.Tag == IsLockedTag) ?? false;
        }

        public void SetIsLocked(bool value)
        {
            var locked = SubAttributes?.Exists(e => e.Tag == IsLockedTag) ?? false;
            if (value == locked)
            {
                return;
            }
            if (value)
            {
                SubAttributes = SubAttributes ?? new List<ItemPropertyData>();
                SubAttributes.Add(new ItemPropertyData() { Tag = IsLockedTag });
            }
            else
            {
                SubAttributes?.RemoveAll(sub => sub.Tag == IsLockedTag);
            }
        }
         
        public int GetColorRGB()
        {
            return SubAttributes?.Find(e => e.Tag == ColorRGBTag)?.Value ?? 0;
        }
        public void SetColorRGB(int value)
        {
                var attr = SubAttributes?.Find(e => e.Tag == ColorRGBTag);
                if (attr == null)
                {
                    SubAttributes = SubAttributes ?? new List<ItemPropertyData>();
                    SubAttributes.Add(new ItemPropertyData() { Tag = ColorRGBTag, Value = value });
                }
                else
                {
                    attr.Value = value;
                }
            
        }

        public object Clone()
        {
            var ret = (ItemPropertyData)MemberwiseClone();
            if (SubAttributes != null)
            {
                ret.SubAttributes = new List<ItemPropertyData>(SubAttributes.Select(x => (ItemPropertyData)x.Clone()));
            }

            return ret;
        }

        public bool Equals(ItemPropertyData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) &&
                   Name == other.Name &&
                   Value == other.Value &&
                   Tag == other.Tag &&
                   Index == other.Index &&
                   Equals(SubAttributes, other.SubAttributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ItemPropertyData)obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ItemPropertyData other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var nameComparison = string.Compare(Name, other.Name, StringComparison.Ordinal);
            if (nameComparison != 0) return nameComparison;
            var idComparison = ID.CompareTo(other.ID);
            if (idComparison != 0) return idComparison;
            var valueTypeComparison = ValueType.CompareTo(other.ValueType);
            if (valueTypeComparison != 0) return valueTypeComparison;
            var valueComparison = Value.CompareTo(other.Value);
            if (valueComparison != 0) return valueComparison;
            var indexComparison = Index.CompareTo(other.Index);
            if (indexComparison != 0) return indexComparison;
            return string.Compare(Tag, other.Tag, StringComparison.Ordinal);
        }

        public static void WriteExternal(ItemPropertyData prop, TextOutputStream output)
        {
            output.PutS32(prop.Index);
            output.PutUTF(prop.Name);
            output.PutS32(prop.ID);
            output.PutS32(prop.ValueType);
            output.PutS32(prop.Value);
            output.PutUTF(prop.Tag);
            output.PutList(prop.SubAttributes, o =>
            {
                WriteExternal(o, output);
            });
        }
        public static void ReadExternal(ItemPropertyData prop, TextInputStream input)
        {
            prop.Index = input.GetS32();
            prop.Name = input.GetUTF();
            prop.ID = input.GetS32();
            prop.ValueType = input.GetS32();
            prop.Value = input.GetS32();
            prop.Tag = input.GetUTF();
            prop.SubAttributes = input.GetList(() =>
            {
                var o = new ItemPropertyData();
                ReadExternal(o, input);
                return o;
            });
        }
    }

    [MessageType(Constants.ITEM_START + 6)]
    public class ItemPropertiesData : ISerializable, ICloneable, IStructMapping
    {
        public List<ItemPropertyData> Properties;

        public int? Count { get => Properties?.Count; }

        public static implicit operator ItemPropertiesData(List<ItemPropertyData> value)
        {
            return new ItemPropertiesData() { Properties = value };
        }

        public object Clone()
        {
            var ret = new ItemPropertiesData();
            if (Properties != null)
            {
                ret.Properties = new List<ItemPropertyData>(Properties.Select(x => (ItemPropertyData)x.Clone()));
            }
            return ret;
        }
        
    }

    [MessageType(Constants.ITEM_START + 3)]
    [PersistType]
    public class EntityItemData : ISerializable, ICloneable, IObjectMapping
    {
        [PersistField] public ItemSnapData SnapData;
        [PersistField] public ItemPropertiesData Properties;

        [PersistField(PersistStrategy.SaveLoadImmediately)]
        public bool CanTrade;


        public EntityItemData()
        {
        }

        public object Clone()
        {
            var ret = new EntityItemData
            {
                SnapData = (ItemSnapData)SnapData.Clone(),
                CanTrade = CanTrade,
            };

            if (Properties != null)
            {
                ret.Properties = (ItemPropertiesData)this.Properties.Clone();
            }

            return ret;
        }

        public override string ToString()
        {
            return $"[SnapData]:{SnapData},cantrade={CanTrade},[TemplateID]:{SnapData.TemplateID},[Count]:{SnapData.Count}";
        }
    }

    /// <summary>
    /// 道具快照
    /// </summary>
    [MessageType(Constants.ITEM_START + 5)]
    public class ItemSnapData : ISerializable, ICloneable, IStructMapping
    {
        [PersistField] public string ID;
        [PersistField] public int TemplateID;
        public uint MaxStackCount;
        [PersistField] public uint Count;

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(ID))
            {
                return $"{ID},{Count}";
            }

            return $"[TemplateID]:{TemplateID},[Count]:{Count}";
        }
    }
}