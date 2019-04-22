
using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_PlayRule + 401)]
    public class SweepRewardData : ISerializable
    {
        public int id;
        public int num;
    }

  
}
