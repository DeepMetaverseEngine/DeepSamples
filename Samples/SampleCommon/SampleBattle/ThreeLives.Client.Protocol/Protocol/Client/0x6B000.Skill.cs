using DeepCore;
using DeepCore.IO;
using System.Collections.Generic;
using DeepMMO.Protocol.Client;
using DeepMMO.Protocol;
using TLProtocol.Data;
using DeepMMO.Attributes;

namespace TLProtocol.Protocol.Client
{
    /// <summary>
    /// 获得技能列表请求.
    /// </summary>
    [MessageType(Constants.TL_SKILL_START + 1)]
    public class TLClientGetSkillListRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    /// 获得技能列表回执.
    /// </summary>
    [MessageType(Constants.TL_SKILL_START + 2)]
    public class TLClientGetSkillListResponse : Response, ILogicProtocol
    {
        // id level
        public HashMap<int, int> skillMap;

        //public List<SkillData> skillList;
        //public List<int> shortcutList;
    }

    /// <summary>
    /// 获得技能升级请求.
    /// </summary>
    [MessageType(Constants.TL_SKILL_START + 3)]
    public class TLClientSkillUpLevelRequest : Request, ILogicProtocol
    {
        public byte type;  //0：升级，1：一键升级
        public int skillId;
    }

    /// <summary>
    /// 获得技能升级回执.
    /// </summary>
    [MessageType(Constants.TL_SKILL_START + 4)]
    public class TLClientSkillUpLevelResponse : Response, ILogicProtocol
    {
        // id level
        public HashMap<int, int> skillMap;
        [MessageCode("参数错误")] public const int CODE_ARGERROR = 501;
        [MessageCode("技能等级已达当前最高")] public const int CODE_LEVELLIMIT = 502;
        [MessageCode("该技能尚未解锁")] public const int CODE_NOTEXISTITEM = 503;
    }
 

    /// <summary>
    /// 更换快捷键请求.
    /// </summary>
    [MessageType(Constants.TL_SKILL_START + 11)]
    public class TLClientChangeShortcutRequest : Request, ILogicProtocol
    {
        public List<int> skillPos;
    }

    /// <summary>
    /// 更换快捷键回执.
    /// </summary>
    [MessageType(Constants.TL_SKILL_START + 12)]
    public class TLClientChangeShortcutResponse : Response, ILogicProtocol
    {
    }

 
}
 