using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;

namespace TLProtocol.Data
{

    [MessageType(TLConstants.TL_RECHARGE_START + 1)]
    public class RechargeProductSnap : ISerializable
    {
        /// <summary>
        /// 商品ID.
        /// </summary>
        public int s2c_id;
        /// <summary>
        /// 总购买次数.
        /// </summary>
        public int s2c_buy_count_total;
        /// <summary>
        /// 当月或当周购买次数(每日/周限购).
        /// </summary>
        public int s2c_buy_count;
    }

    [MessageType(TLConstants.TL_RECHARGE_START + 2)]
    public class RechargePayResultData : ISerializable
    {
        public string c2s_order_id;
        public HashMap<string, string> c2s_ext_map;
    }
}
