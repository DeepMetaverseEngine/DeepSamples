
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;
using DeepCore;

namespace TLProtocol.Data
{
    /// <summary>
    /// 任务快照.
    /// </summary>
    [MessageType(TLConstants.TL_PlayRule + 600)]
    public class OpeningEventDataSnap : AchievementDataSnap
    {
       

    }

  
    [MessageType(TLConstants.TL_PlayRule + 601)]
    public class OpeningEventReward : AchievementReward
    {
        
    }
    
}
