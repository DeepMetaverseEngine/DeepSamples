
using System.Xml.Serialization;

namespace TLClient.Net
{

    public enum UserCenterMsgID
    {
        Register = 1,       //注册
        Login = 2,          //登录
    }

    public enum GameCenterMsgID
    {
        Init = 1,           //初始化，版本更新
        ServerList = 2,     //刷新服务器列表
        CloudImage = 3,     //万象优图
        Activation = 4,     //账号激活
    }

    public class TLHttpMessage
    {
        private int mstatus;
        [XmlElement("status")]
        public int status
        {
            get {
                return mstatus;
            }
            set
            {
                mstatus = value;
            }
        }

        private string mmessage;
        [XmlElement("message")]
        public string message
        {
            get
            {
                return mmessage;
            }
            set
            {
                mmessage = value;
            }
        }
    }

    [XmlRoot("root")]
    public class RegisterMessage : TLHttpMessage
    {
        private string musername;
        [XmlElement("username")]
        public string username
        {
            get
            {
                return musername;
            }
            set
            {
                musername = value;
            }
        }

        private string mchannelName;
        [XmlElement("channelName")]
        public string channelName
        {
            get
            {
                return mchannelName;
            }
            set
            {
                mchannelName = value;
            }
        }

        private string mtime;
        [XmlElement("time")]
        public string time
        {
            get
            {
                return mtime;
            }
            set
            {
                mtime = value;
            }
        }

        private string msign;
        [XmlElement("sign")]
        public string sign
        {
            get
            {
                return msign;
            }
            set
            {
                msign = value;
            }
        }
    }

    [XmlRoot("root")]
    public class LoginMessage : TLHttpMessage
    {
        private string musername;
        [XmlElement("username")]
        public string username
        {
            get
            {
                return musername;
            }
            set
            {
                musername = value;
            }
        }

        private string mchannelName;
        [XmlElement("channelName")]
        public string channelName
        {
            get
            {
                return mchannelName;
            }
            set
            {
                mchannelName = value;
            }
        }

        private ulong mtime;
        [XmlElement("time")]
        public ulong time
        {
            get
            {
                return mtime;
            }
            set
            {
                mtime = value;
            }
        }

        private string msign;
        [XmlElement("sign")]
        public string sign
        {
            get
            {
                return msign;
            }
            set
            {
                msign = value;
            }
        }
    }

    [XmlRoot("root")]
    public class SrvListMessage : TLHttpMessage
    {
        private string msrvList;
        [XmlElement("srvList")]
        public string srvList
        {
            get
            {
                return msrvList;
            }
            set
            {
                msrvList = value;
            }
        }

        private string mrecom;
        [XmlElement("recom")]
        public string recom
        {
            get
            {
                return mrecom;
            }
            set
            {
                mrecom = value;
            }
        }

        private string mposition;
        [XmlElement("position")]
        public string position
        {
            get
            {
                return mposition;
            }
            set
            {
                mposition = value;
            }
        }

        private string mrolebasic;
        [XmlElement("rolebasic")]
        public string rolebasic
        {
            get
            {
                return mrolebasic;
            }
            set
            {
                mrolebasic = value;
            }
        }

        private string mactivateWebsite;
        [XmlElement("activateWebsite")]
        public string activateWebsite
        {
            get
            {
                return mactivateWebsite;
            }
            set
            {
                mactivateWebsite = value;
            }
        }
    }


    [XmlRoot("root")]
    public class UpdateVersionMessage : TLHttpMessage
    {
        //客户端更新
        [XmlElement("update_type")]
        public int update_type;
        [XmlElement("update_url")]
        public string update_url;

        //分段资源
        [XmlElement("res_type")]
        public int res_type;

        //资源更新
        [XmlElement("cdn_url")]
        public string cdn_url;

        //维护公告
        [XmlElement("repair_notice_state")]
        public int repair_notice_state;

        //维护公告列表
        [XmlElement("repair_content")]
        public RepairContent[] repair_content;

        //系统公告
        [XmlElement("sys_notice_state")]
        public int sys_notice_state;
        [XmlElement("sys_content")]
        public string sys_content;

        //MPQ公告
        [XmlElement("mpq_notice_state")]
        public int mpq_notice_state;
        [XmlElement("mpq_content")]
        public string mpq_content;
    }

    [XmlRoot("root")]
    public class ActivationMessage : TLHttpMessage
    {
        
    }

    public class RepairContent
    {
        [XmlElement("is_top")]
        public bool is_top;

        [XmlElement("title")]
        public string title;

        [XmlElement("content")]
        public string content;

        [XmlElement("started_at")]
        public string started_at;

        [XmlElement("ended_at")]
        public string ended_at;
    }

}