
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;
using DeepCore;

namespace TLProtocol.Data
{
    
    [MessageType(TLConstants.TL_PlayRule + 701)]
    public class TurnTableReward : AchievementReward
    {
        
    }
    
    [MessageType(TLConstants.TL_PlayRule + 702)]
    public class TurnTableGotReward : ISerializable
    {
        public int index;//索引
        public int state;//状态 
    }
    
}
