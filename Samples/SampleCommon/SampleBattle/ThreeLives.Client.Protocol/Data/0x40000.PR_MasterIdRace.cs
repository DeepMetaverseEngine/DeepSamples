
using DeepCore;
using DeepCore.IO;
using DeepCore.ORM;
using System;
using System.Collections.Generic;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_PlayRule + 200)]
    public class MasterIdListData : ISerializable
    {
        public List<MasterIdData> masterDataMap;// 身份,数据表
        public List<MasterIdBattleInfoData> battleRecordList;// 战报数据
        public DateTime challengeCD;//挑战cd
        public DateTime renameCD;// 师门赛亲信重命名cd
        public DateTime AppointCD; // 师门赛任命cd
        public DateTime InviteCD; // 师门赛邀请cd
                              
        public int curMasterId;//当前身份
        public string QinXinName;//亲信名字
    }

    [MessageType(TLConstants.TL_PlayRule + 201)]
    public class MasterIdData : ISerializable
    {
       public int index;//位置索引
       public int masterid;//身份id
        //玩家uuid
       public string playeruuid;
    }

    [MessageType(TLConstants.TL_PlayRule + 202)]
    public class MasterIdBattleInfoData : IStructMapping, ISerializable
    {
        //战报信息
        public DateTime dateTime;
        //战报内容
        public string message;
    }

    
    [MessageType(TLConstants.TL_PlayRule + 222)]
    public class MasterResult : ISerializable
    {
        //职业 数据
        public List<MasterIdData> raceDatalist;

        public MasterResult()
        {
            raceDatalist = new List<MasterIdData>();
        }
       
    }
   
}
