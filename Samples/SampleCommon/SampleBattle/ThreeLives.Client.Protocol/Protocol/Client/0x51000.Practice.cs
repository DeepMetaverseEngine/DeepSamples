using DeepCore.IO;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using DeepMMO.Attributes;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{

    [MessageType(Constants.TL_PRACTICE_START + 1)]
    public class ClientGetPracticeInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_PRACTICE_START + 2)]
    public class ClientGetPracticeInfoResponse : Response, ILogicProtocol
    {
        public PracticeInfoData s2c_data;
    }

    [MessageType(Constants.TL_PRACTICE_START + 3)]
    public class ClientPracticeUpRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_PRACTICE_START + 4)]
    public class ClientPracticeUpResponse : Response, ILogicProtocol
    {
        [MessageCode("战力不足,请先提升战力")] public const int ERR_PRACTICE_POWER_NOT_ENOUGH = 501;
        [MessageCode("请先完成渡劫任务")] public const int ERR_PRACTICE_NEED_QUEST = 502;
        [MessageCode("已升至最高境界")] public const int ERR_PRACTICE_LV_MAX = 503;
    }

    [MessageType(Constants.TL_PRACTICE_START + 5)]
    public class ClientPracticeQuestRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_PRACTICE_START + 6)]
    public class ClientPracticeQuestResponse : Response, ILogicProtocol
    {
        [MessageCode("已有渡劫任务，请不要重复接取")] public const int ERR_PRACTICE_QUEST_REPEAT = 501;
        [MessageCode("没有渡劫任务")] public const int ERR_PRACTICE_QUEST_NOT_FOUND = 502;
        [MessageCode("渡劫任务已完成")] public const int ERR_PRACTICE_QUEST_FINISHED = 503;
        [MessageCode("渡劫任务接取失败")] public const int ERR_PRACTICE_QUEST_ACCEPT_FAILED = 504;
    }

}
