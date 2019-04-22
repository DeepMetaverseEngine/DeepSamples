using System;

namespace TLClient.Protocol.Modules.Package
{
    public class TemplateItemSnap
    {
        public int TemplateID;
        public long Count;
        public Comparison<IPackageItem> Compare;

        public CostCondition ToCostCondition()
        {
            var c = new CostCondition
            {
                Count = (ulong) Count,
                Compare = Compare,
                Desc = ToString(),
                TemplateID = TemplateID
            };
            return c;
        }

        public override string ToString()
        {
            return $"[TemplateID]:{TemplateID},[Count]:{Count}";
        }
    }

}