using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;
using DeepMMO.Protocol.Client;
using DeepMMO.Protocol;
using TLProtocol.Data;
using DeepMMO.Attributes;
using System;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 获得邮箱内容请求.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 1)]
    public class TLClientGetMailBoxInfoRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 获得邮箱内容回执.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 2)]
    public class TLClientGetMailBoxInfoResponse : Response, ILogicProtocol
    {
        public List<TLMailSnapInfoData> s2c_mailsnap_list;
        public int s2c_max_count;
    }

    /// <summary>
    /// 删除mail请求.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 3)]
    public class TLClientDeleteMailRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 全部删除.
        /// </summary>
        public bool c2s_delete_all;

        /// <summary>
        /// 指定删除的邮件.
        /// </summary>
        public List<string> c2s_remove_uuid_list;
    }

    /// <summary>
    /// 删除mail回执.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 4)]
    public class TLClientDeleteMailResponse : Response, ILogicProtocol
    {
        public List<TLMailSnapInfoData> s2c_mailsnap_list;
    }


    /// <summary>
    /// 获取mail详情.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 5)]
    public class TLClientGetMailDetailRequest : Request, ILogicProtocol
    {
        public string c2s_mailuuid;
    }

    /// <summary>
    /// 获取mail详情回执.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 6)]
    public class TLClientGetMailDetailResponse : Response, ILogicProtocol
    {
        public TLMailInfoData s2c_mail_detail;
        [MessageCode("邮件不存在")] public const int CODE_ERROR_MAIL_NOT_EXSIT = 501;
    }


    /// <summary>
    /// 获取邮件附件.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 7)]
    public class TLClientGetMailAttachmentRequest : Request, ILogicProtocol
    {
        public string c2s_mailuuid;
    }

    /// <summary>
    /// 获取邮件附件回执.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 8)]
    public class TLClientGetMailAttachmentResponse : Response, ILogicProtocol
    {

    }

    /// <summary>
    /// 发送邮件请求.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 9)]
    public class TLClientSendMailRequest : Request, ILogicProtocol
    {
        /// <summary>
        /// 接受者ID.
        /// </summary>
        public string c2s_receiver_uuid;

        /// <summary>
        /// 标题.
        /// </summary>
        public string c2s_title;

        /// <summary>
        /// 邮件内容.
        /// </summary>
        public TLMailContentData c2s_content;
    }

    /// <summary>
    /// 发送邮件回执.
    /// </summary>
    [MessageType(Constants.TL_MAIL_START + 10)]
    public class TLClientSendMailResponse : Response, ILogicProtocol
    {
        public const int CODE_ERROR_ITEM_NOT_ENOUGH = 501;
    }
}