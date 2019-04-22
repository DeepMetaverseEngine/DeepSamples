using DeepMMO.Protocol.Client;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Protocol;
using DeepCore;

namespace TLProtocol.Protocol.Client
{
 
    //获取列表信息
    [MessageType(Constants.TL_ARTIFACT_START + 1)]
    public class TLClientGetArtifactListRequest : Request, ILogicProtocol
    {
    }

    [MessageType(Constants.TL_ARTIFACT_START + 2)]
    public class TLClientGetArtifactListResponse : Response, ILogicProtocol
    {
        // id level
        public HashMap<int, int> artifactMap;

        // zhu shen qi
        public int MainEquipId = 0;
 
        // fushenqi
        public int SecondEquipId = 0;
    }

    // 激活神器
    [MessageType(Constants.TL_ARTIFACT_START + 3)]
    public class TLClientGetArtifactRequest : Request, ILogicProtocol
    {
        public int c2s_artifactId;
    }

    [MessageType(Constants.TL_ARTIFACT_START + 4)]
    public class TLClientGetArtifactResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("参数错误，没有这个神器")]
        public const int CODE_ARG_ID_ERROR = 501;
        [MessageCodeAttribute("已经拥有这个神器，重复获得")]
        public const int CODE_REGET_AGAIN = 502;
    
    }


    [MessageType(Constants.TL_ARTIFACT_START + 5)]
    public class TLClientEquipArtifactRequest : Request, ILogicProtocol
    {
        public int c2s_pos;
        public int c2s_artifactId;
    }

    [MessageType(Constants.TL_ARTIFACT_START + 6)]
    public class TLClientEquipArtifactResponse : Response, ILogicProtocol
    {
        [MessageCodeAttribute("参数错误，没有这个神器")]
        public const int CODE_ARG_ID_ERROR = 501;
        [MessageCodeAttribute("参数错误，装备位置错误")]
        public const int CODE_ARG_INDEX_ERROR = 502;
        [MessageCode("该神器尚未激活")]
        public const int CODE_NOTEXISTITEM = 503;

        [MessageCode("该神器栏位尚未开放")]
        public const int CODE_INDEXNOTOPEN = 504;

        // zhu shen qi
        public int MainEquipId = 0;

        // fushenqi
        public int SecondEquipId = 0;

    }



    [MessageType(Constants.TL_ARTIFACT_START + 7)]
    public class TLClientArtifactLevelUpRequest : Request, ILogicProtocol
    {
        public int c2s_artifactId;
    }

    [MessageType(Constants.TL_ARTIFACT_START + 8)]
    public class TLClientArtifactLevelUpResponse : Response, ILogicProtocol
    {
        // id level
        public HashMap<int, int> artifactMap;

        [MessageCodeAttribute("参数错误，没有这个神器")]
        public const int CODE_ARG = 501;
        [MessageCodeAttribute("参数错误，尚未获得这个神器")]
        public const int CODE_ARG_ID_ERROR = 502;
        [MessageCodeAttribute("神器已培养至顶级")]
        public const int CODE_MAX_LEVEL = 503;
    }


}