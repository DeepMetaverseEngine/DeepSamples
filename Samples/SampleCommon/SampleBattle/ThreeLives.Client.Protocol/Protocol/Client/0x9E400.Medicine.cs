using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 充能血池.
    /// </summary>
    [DeepCore.IO.MessageType(Constants.TL_MEDICINE_START + 1)]
    public class ClientRechargeMedicinePoolRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 充能血池.
    /// </summary>
    [DeepCore.IO.MessageType(Constants.TL_MEDICINE_START + 2)]
    public class ClientRechargeMedicinePoolResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("配置数据错误")]
        public const int CODE_DATA_ERR = 501;
        [MessageCodeAttribute("血池已满")]
        public const int CODE_FULL = 502;

        public int s2c_count;
    }

    [DeepCore.IO.MessageType(Constants.TL_MEDICINE_START + 3)]
    public class ClientUseMedicinePoolRequest : Request, ILogicProtocol
    {

    }

    [DeepCore.IO.MessageType(Constants.TL_MEDICINE_START + 4)]
    public class ClientUseMedicinePoolResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("没有次数")]
        public const int CODE_NO_TIMES = 501;
        [MessageCodeAttribute("血池冷却中")]
        public const int CODE_CD = 502;

        public int s2c_count;
        public DateTime s2c_cdTimeStamp;
    }

    /// <summary>
    /// 血池上限变更通知.
    /// </summary>
    [DeepCore.IO.MessageType(Constants.TL_MEDICINE_START + 5)]
    public class ClientMedicinePoolUpgradeNotify : Notify, ILogicProtocol
    {
        public int s2c_limit;
    }
}
