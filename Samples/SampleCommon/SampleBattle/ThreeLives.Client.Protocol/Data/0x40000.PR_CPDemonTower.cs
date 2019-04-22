
using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_PlayRule + 5)]
    public class CPDemonTowerData : ISerializable
    {
        public int curPlayTimes;// 可参与次数
        public int maxPlayTimes = 1;//最大参与次数
        public int maxLayer;// 最大通关层数
        public List<int> giftData;//首通奖励领取
    }
    
}
