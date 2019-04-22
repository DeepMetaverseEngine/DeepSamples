using System;

namespace TLClient.Protocol.Modules.Package
{
    public struct PackageSlot
    {
        public int Index;
        public IPackageItem Item;

        public string Tag;
        public bool Invaild => Index < 0;
        public bool IsNull => Item == null || Invaild;

        public static PackageSlot InvaildSlot = new PackageSlot() {Index = -1, Item = null};

        public PackageSlot Clone()
        {
            return new PackageSlot {Index = Index, Item = Item?.Clone() as IPackageItem, Tag = Tag};
        }

        public PackageSlotDiff Diff(PackageSlot other)
        {
            if (other.Item == null && Item != null)
            {
                //add
                return new PackageSlotDiff {Op = PackageSlotDiff.Operator.Add, Slot = Clone()};
            }
            if (other.Item != null && Item == null)
            {
                //delete
                return new PackageSlotDiff {Op = PackageSlotDiff.Operator.Delete, Slot = other.Clone()};
            }
            if (other.Item != null && Item != null && other.Item.Count != Item.Count)
            {
                //updateCount
                var newSlots = Clone();
                newSlots.Item.Count = (uint) Math.Abs(other.Item.Count - Item.Count);
                if (other.Item.Count > Item.Count)
                {
                    return new PackageSlotDiff {Op = PackageSlotDiff.Operator.Decrement, Slot = newSlots};
                }
                return new PackageSlotDiff {Op = PackageSlotDiff.Operator.Increment, Slot = newSlots};
            }
            return null;
        }
    }
}