
using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_PlayRule + 1)]
    public class DemonTowerData : ISerializable
    {
        public int resetTime;// 重置次数
        public int maxResetTime = 1;//最大重置次数
        public int curLayer;// 当前层数
        public List<int> giftData;//首通奖励领取
        public List<int> alonePassTime ;//个人最佳通关时间
        public List<ServerPassData> serverPassData;//服务器最佳时间
    }
    [MessageType(TLConstants.TL_PlayRule + 2)]
    public class SecretBookData : ISerializable
    {
        public List<int> ClueList;
        //0 未激活 1 任务进行中 2待领取 3 已领取
        public int state;
    }
    
    [MessageType(TLConstants.TL_PlayRule + 3)]
    public class ServerPassData : ISerializable
    {
        public string name;
        public int sec;
        public string playeruuid;
    }

    [MessageType(TLConstants.TL_PlayRule + 4)]
    public class SecretBookDataList : ISerializable
    {
        public HashMap<int, SecretBookData> secretBookList;
    }
    
}
