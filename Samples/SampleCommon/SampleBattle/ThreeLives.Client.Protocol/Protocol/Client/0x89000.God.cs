using DeepCore;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System.Collections.Generic;
using ThreeLives.Client.Protocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 召唤仙侣.
    /// </summary>
    [MessageType(Constants.TL_GOD_START + 1)]
    public class ClientSummonGodRequest : Request, ILogicProtocol
    {
        public int c2s_god_id;
    }
    /// <summary>
    /// 召唤仙侣.
    /// </summary>
    [MessageType(Constants.TL_GOD_START + 2)]
    public class ClientSummonGodResponse : Response, ILogicProtocol
    {
        public int s2c_god_id;
        public int s2c_god_lv;
    }

    /// <summary>
    /// 获取仙侣列表.
    /// </summary>
    [MessageType(Constants.TL_GOD_START + 3)]
    public class ClientGetGodListRequest : Request, ILogicProtocol
    {

    }
    [MessageType(Constants.TL_GOD_START + 4)]
    public class ClientGetGodListResponse : Response, ILogicProtocol
    {
        public List<ClientGodSnap> s2c_list;
    }

    /// <summary>
    /// 升级仙侣列表.
    /// </summary>
    [MessageType(Constants.TL_GOD_START + 5)]
    public class ClientGodUpgradeRequest : Request, ILogicProtocol
    {
        public int c2s_god_id;
        public int c2s_god_lv;
    }
    [MessageType(Constants.TL_GOD_START + 6)]
    public class ClientGodUpgradeResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("仙侣已培养至顶级")]
        public const int CODE_MAX_LEVEL = 501;

        public int s2c_god_lv;
    }

    /// <summary>
    /// 侠侣出战请求.
    /// </summary>
    [MessageType(Constants.TL_GOD_START + 7)]
    public class ClientGodFightRequest : Request, ILogicProtocol
    {
        public int c2s_god_id;
    }
    [MessageType(Constants.TL_GOD_START + 8)]
    public class ClientGodFightResponse : Response, ILogicProtocol
    {
        public int s2c_god_id;
    }

    [MessageType(Constants.TL_GOD_START + 9)]
    public class ClientGetGodBookInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_GOD_START + 10)]
    public class ClientGetGodBookInfoResponse : Response, ILogicProtocol
    {
        public List<int> s2c_books;
        public long s2c_fightpower;
        public HashMap<string, int> s2c_props;
    }

    [MessageType(Constants.TL_GOD_START + 11)]
    public class ClientActiveGodBookNotify : Notify, ILogicProtocol
    {
        public List<int> s2c_new_books;
    }
}
