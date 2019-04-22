using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;
using System;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;

namespace TLProtocol.Data
{
    [MessageType(TLConstants.TL_MAIL_START + 1)]
    public class TLMailSnapInfoData : ISerializable
    {
        public string uuid;
        public byte mail_type;
        public string title;
        public string sender_name;
        public string sender_uuid;
        public DateTime create_time;
        public DateTime read_time;
        public bool attachment;
        public byte mail_status;
        public string receive_uuid;
        public string receive_name;
        public int template_id;
        public bool b_new;
        public bool can_remove;
    }

    [MessageType(TLConstants.TL_MAIL_START + 2)]
    public class TLMailInfoData : ISerializable
    {
        public string uuid;
        public byte mail_type;
        public string sender_uuid;
        public string sender_name;
        public string receiver_uuid;
        public string receiver_name;
        public string title;
        public TLMailContentData content;
        public int template_id;
    }

    /// <summary>
    /// 邮件内容.
    /// </summary>
    [MessageType(TLConstants.TL_MAIL_START + 3)]
    [PersistType]
    public class TLMailContentData : ISerializable
    {
        /// <summary>
        /// 文本内容.
        /// </summary>
        [PersistField]
        public string txt_content;

        /// <summary>
        /// 附件列表.
        /// </summary>
        [PersistField]
        public List<ItemSnapData> attachment_list;
    }

    /// <summary>
    /// 通知客户端有新邮件.
    /// </summary>
    [MessageType(TLConstants.TL_MAIL_START + 4)]
    public class TLClientIncomingMailNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int s2c_new_mail_count;
    }
}
