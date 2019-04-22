using System;
using System.Collections.Generic;
using DeepCore;
using DeepCore.IO;
using DeepMMO.Data;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_ISLAND_START + 1)]
    public class ChekPointSnapData : ISerializable
    {
        public int state;
        // 关卡
        public int CheckPointId;

        public string PassName1st;

        public double PassTime1st;

    }

  
}
