using System;
using System.Collections.Generic;
using DeepCore;
using DeepCore.IO;
using DeepMMO.Data;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_WARDROBE_START + 1)]
    public class RequireSnapData : ISerializable
    {
        public bool result;
        public int curVal;
        public int minVal;
        public int maxVal;
        public string reason;
    }

    [MessageType(TLConstants.TL_WARDROBE_START + 2)]
    public class RequireInfo : ISerializable
    {
        public List<RequireSnapData> requireList;
    }

    [MessageType(TLConstants.TL_WARDROBE_START + 3)]
    public class ExpireDay : ISerializable
    {
        public int last_time;
        public DateTime endDate;
    }
}
