using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeLives.Client.Protocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 获取排行榜界面选项卡.
    /// </summary>
    [MessageType(Constants.TL_RANKING_START + 1)]
    public class ClientGetRankBoardDataRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取排行榜界面选项卡.
    /// </summary>
    [MessageType(Constants.TL_RANKING_START + 2)]
    public class ClientGetRankBoardDataResponse : Response, ILogicProtocol
    {
        public List<TLClientRankBoardData> s2c_data;
    }


    /// <summary>
    /// 获取排行榜信息.
    /// </summary>
    [MessageType(Constants.TL_RANKING_START + 3)]
    public class ClientGetRanklistDataRequest : Request, ILogicProtocol
    {
        public int group_id;
        public int child_id;
    }

    [MessageType(Constants.TL_RANKING_START + 4)]
    public class ClientGetRanklistDataResponse : Response, ILogicProtocol
    {
        public int group_id;
        public int child_id;
        public List<TLClientRankData> s2c_list;
    }
}
