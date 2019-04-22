using DeepMMO.Protocol.Client;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using TLProtocol.Data;
using DeepCore;
using ThreeLives.Client.Protocol.Data;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 命石抽奖
    /// </summary>
    [MessageType(Constants.TL_PHOTO + 1)]
    public class ClientPhotoAddressRequest : Request, ILogicProtocol
    {
        public string c2s_roleId;
    }

    [MessageType(Constants.TL_PHOTO + 2)]
    public class ClientPhotoAddressResponse : Response, ILogicProtocol
    { 
        public string s2c_prefix;
        
        public HashMap<string, string> s2c_data;

        [MessageCode("参数错误")]
        public const int CODE_ARGERR = 501;
        [MessageCode("获取相册地址失败")]
        public const int CODE_FAILED = 502;

    }


    [MessageType(Constants.TL_PHOTO + 3)]
    public class ClientPhotoInfoRequest : Request, ILogicProtocol
    {
        public string c2s_roleId;
    }

    [MessageType(Constants.TL_PHOTO + 4)]
    public class ClientPhotoInfoResponse : Response, ILogicProtocol
    {
        public string s2c_url;

        public string s2c_prefix;

        public RolePhotoData s2c_datas;

        [MessageCode("参数错误")]
        public const int CODE_ARGERR = 501;

        [MessageCode("角色ID不能为空")]
        public const int CODE_EMPTY = 502;

    }
     

    [MessageType(Constants.TL_PHOTO + 5)]
    public class ClientPhotoUploadRequest : Request, ILogicProtocol
    {
        public int c2s_Index;

        public string c2s_suffix;
    }

    [MessageType(Constants.TL_PHOTO + 6)]
    public class ClientPhotoUploadResponse : Response, ILogicProtocol
    {
        public string s2c_photoName;

        [MessageCode("参数错误")]
        public const int CODE_ARGERR = 501;

        [MessageCode("文件类型错误")]
        public const int CODE_FILEERR = 502;

        [MessageCode("你只能上传6张图片")]
        public const int CODE_SIZEERR = 503;
    }

    [MessageType(Constants.TL_PHOTO + 7)]
    public class ClientPhotoDeleteRequest : Request, ILogicProtocol
    {
        public int c2s_Index;
        
    }

    [MessageType(Constants.TL_PHOTO + 8)]
    public class ClientPhotoDeleteResponse : Response, ILogicProtocol
    {
        public string s2c_photoName;

        [MessageCode("参数错误")]
        public const int CODE_ARGERR = 501;
    }

    [MessageType(Constants.TL_PHOTO + 9)]
    public class ClientUpdateSocialRequest : Request, ILogicProtocol
    {
        public HashMap<string,string> c2s_socialData;
        
    }

    [MessageType(Constants.TL_PHOTO + 10)]
    public class ClientUpdateSocialResponse : Response, ILogicProtocol
    {
        //public HashMap<string, string> s2c_socialData;

        [MessageCode("参数错误")]
        public const int CODE_ARGERR = 501;
    }

    [MessageType(Constants.TL_PHOTO + 11)]
    public class ClientGetPhotoUrlRequest : Request, ILogicProtocol
    {
        
    }

    [MessageType(Constants.TL_PHOTO + 12)]
    public class ClientGetPhotoUrlResponse : Response, ILogicProtocol
    {
        public string s2c_url;

        public string s2c_prefix;

    }
}