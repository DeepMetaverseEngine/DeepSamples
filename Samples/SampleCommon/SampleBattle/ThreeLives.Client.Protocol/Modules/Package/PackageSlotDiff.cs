using System;
using System.Collections.Generic;
using System.Linq;

namespace TLClient.Protocol.Modules.Package
{
    public class PackageSlotDiff
    {
        public enum Operator
        {
            Add,
            Increment,
            Decrement,
            Delete,
        }

        public PackageSlot Slot;
        public Operator Op;

        public static PackageSlotDiff[] MergerTemplateDiffs(PackageSlotDiff[] src)
        {
            var templateIDs = new HashSet<int>(src.Select(e => e.Slot.Item.TemplateID));
            var ret = new List<PackageSlotDiff>();
            foreach (var templateID in templateIDs)
            {
                var all = src.Where(e => e.Slot.Item.TemplateID == templateID).ToList();
                var group = new List<PackageSlotDiff[]>();

                while (all.Count > 0)
                {
                    var cur = all[0];
                    var sameAll = new List<PackageSlotDiff>();
                    for (var i = all.Count - 1; i > 0; i--)
                    {
                        if (all[i].Slot.Item.CompareAttribute(cur.Slot.Item))
                        {
                            sameAll.Add(all[i]);
                            all.RemoveAt(i);
                        }
                    }
                    group.Add(sameAll.ToArray());
                }

                foreach (var diffSlots in group)
                {
                    var diff = new PackageSlotDiff {Slot = diffSlots.First().Slot.Clone()};
                    if (diffSlots.Length > 0)
                    {
                        diff.Slot.Index = -1;
                        long count = 0;
                        foreach (var srcDiff in diffSlots)
                        {
                            if (srcDiff.Op == Operator.Add || srcDiff.Op == Operator.Increment)
                            {
                                count += srcDiff.Slot.Item.Count;
                            }
                            else if (srcDiff.Op == Operator.Decrement || srcDiff.Op == Operator.Delete)
                            {
                                count -= srcDiff.Slot.Item.Count;
                            }
                        }
                        if (count > uint.MaxValue)
                        {
                            throw new ArgumentException();
                        }
                        diff.Slot.Item.Count = (uint) count;
                    }
                    else
                    {
                        diff.Op = diffSlots[0].Op;
                    }
                    ret.Add(diff);
                }
            }
            return ret.ToArray();
        }
    }
}