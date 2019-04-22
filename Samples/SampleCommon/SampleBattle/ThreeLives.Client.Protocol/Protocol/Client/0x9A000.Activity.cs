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
    /// 获取活跃度信息.
    /// </summary>
    [MessageType(Constants.TL_ACTIVITY_START + 1)]
    public class ClientGetActivityDataRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 获取活跃度信息.
    /// </summary>
    [MessageType(Constants.TL_ACTIVITY_START + 2)]
    public class ClientGetActivityDataResponse : Response, ILogicProtocol
    {
        public TLClientActivityData s2c_data;
    }

    /// <summary>
    /// 获取活跃度信息.
    /// </summary>
    [MessageType(Constants.TL_ACTIVITY_START + 3)]
    public class ClientActivityDataChangeNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public List<TLClientActivitySnap> s2c_list;
    }

    /// <summary>
    /// 获取活跃度奖励.
    /// </summary>
    [MessageType(Constants.TL_ACTIVITY_START + 4)]
    public class ClientGetActivityRewardRequest : Request, ILogicProtocol
    {
        public int c2s_pointLv;
    }

    /// <summary>
    /// 获取活跃度奖励.
    /// </summary>
    [MessageType(Constants.TL_ACTIVITY_START + 5)]
    public class ClientGetActivityRewardResponse : Response, ILogicProtocol
    {
        public int s2c_pointLv;
    }
}
