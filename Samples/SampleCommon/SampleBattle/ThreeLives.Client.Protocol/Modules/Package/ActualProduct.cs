using System.Collections.Generic;

namespace TLClient.Protocol.Modules.Package
{
    public class ActualProduct
    {
        public List<PackageSlot> Added;
        public List<IPackageItem> OutOfSize;

        /// <summary>
        /// Value为0 表示删除
        /// </summary>
        public Dictionary<int, uint> UpdateCount;

        /// <summary>
        /// Result为OutOfBagSize情况下，OutOfSize填充超出背包部分的道具
        /// </summary>
        public ErrorCode Result;
    }
}
