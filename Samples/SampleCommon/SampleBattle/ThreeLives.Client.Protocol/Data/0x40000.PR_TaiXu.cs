
using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_PlayRule + 300)]
    public class TaiXuData : ISerializable
    {
        public int groupid;
        public int curTimes;
        public int MaxTimes;
    }

  
}
