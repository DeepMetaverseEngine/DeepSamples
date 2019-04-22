using DeepCore;
using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System.Collections.Generic;
using System.ComponentModel;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 消息处理
    /// </summary>
    [MessageType(Constants.TL_MESSAGE_START + 1)]
    public class ClientHandleMessageRequest : Request, ILogicProtocol
    {
        public List<MessageHandleData> c2s_data;
    }

    /// <summary>
    /// 消息处理
    /// </summary>
    [MessageType(Constants.TL_MESSAGE_START + 2)]
    public class ClientHandleMessageResponse : Response, ILogicProtocol
    {
    }

    /// <summary>
    /// 通知客户端消息信息
    /// </summary>
    [MessageType(Constants.TL_MESSAGE_START + 3)]
    public class ClientMessageAddNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public MessageSnap s2c_data;
    }

    /// <summary>
    /// 通知客户端消息信息
    /// </summary>
    [MessageType(Constants.TL_MESSAGE_START + 4)]
    public class ClientMessageContentNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int s2c_type;
        public string s2c_data;
        public short[] s2c_show_channel;
        public string[] s2c_args;
        public HashMap<string, string> s2c_customArgs;
        public int s2c_noticeid;
    }

    /// <summary>
    /// 获取类型消息
    /// </summary>
    [MessageType(Constants.TL_MESSAGE_START + 5)]
    public class ClientSendedAlertMessageRequest : Request, ILogicProtocol
    {
        public AlertMessageType c2s_type;
    }


    [MessageType(Constants.TL_MESSAGE_START + 6)]
    public class ClientSendedAlertMessageResponse : Response, ILogicProtocol
    {
        public MessageSnap[] s2c_data;
    }

    [MessageType(Constants.TL_MESSAGE_START + 7)]
    public class ClientAlertMessageNotify : Response, ILogicProtocol
    {
        public string s2c_id;
        public string s2c_roleID;
        public bool s2c_agree;
    }

    /// <summary>
    /// 通知客户端红点提示信息
    /// </summary>
    [MessageType(Constants.TL_MESSAGE_START + 50)]
    public class ClientRedTipsNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        /// <summary>
        /// advancetips表里advance_ui中的red_key字段
        /// </summary>
        public string s2c_key;

        /// <summary>
        /// 红点显示规则
        /// 大于0表示显示红点
        /// 0表示消除红点
        /// 当数字大于1时，客户端会同时显示出数字
        /// </summary>
        public int s2c_count;
    }

}