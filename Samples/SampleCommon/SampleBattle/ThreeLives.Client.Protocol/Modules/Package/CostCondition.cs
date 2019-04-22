using System;
using System.Collections.Generic;
using System.Text;

namespace TLClient.Protocol.Modules.Package
{
    public class CostCondition
    {
        public ulong Count;
        public int TemplateID;
        /// <summary>
        /// 直接指定消耗某位置道具
        /// </summary>
        public int SlotIndex = -1;

        public Predicate<IPackageItem> Match;
        public Comparison<IPackageItem> Compare;
        public string Desc;

        public override string ToString()
        {
            return Desc;
        }

        public CostCondition()
        {
            
        }
    }
}