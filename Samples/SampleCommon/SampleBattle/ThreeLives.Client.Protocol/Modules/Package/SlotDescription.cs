using System;


namespace TLClient.Protocol.Modules.Package
{
    public class SlotDescription
    {
        public int Index;
        public uint Count;

        public CostCondition ToCostCondition()
        {
            return new CostCondition
            {
                SlotIndex = Index,
                Count = Count
            };
        }
    }
}
