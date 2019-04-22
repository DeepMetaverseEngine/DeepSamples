using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TLBattle.Common.Data
{

    /// <summary>
    /// 战斗服、客户端公告字段配置.
    /// </summary>
    public class TLCommonConfig
    {
        //战斗服向客户端推送KEY值，客户端转换成对应提示.
        public const string TIPS_EG = "Example";
        public const string TIPS_MP_NOT_ENOUGH = "not_enough_mp";
        //传送点限制
        public const string TIPS_TRANSPORT_ERROR_FORCE = "transport_forceError";
        public const string TIPS_TRANSPORT_ERROR_FULL = "transport_crowded";
        //PVP状态下，无法传送.
        public const string TIPS_TRANSPORT_CONDITION_ERROR = "pvp_ban_transfer";

        public const string TIPS_KILL_WHITE_NAME_PLAYER = "pk_killwhite_msg";
        public const string TIPS_KILL_RED_NAME_PLAYER = "pk_killred_msg";
        public const string TIPS_KILL_GRAY_NAME_PLAYER = "pk_killgray_msg";
    }
}
