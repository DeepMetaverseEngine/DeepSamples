using System;

namespace TLBattle.Common.Plugins
{
    public class TLConstants
    {
        public const int BATTLE_START = 0x000B0000;

        public const int BATTLE_MSG_B2C_START = 0x000C0000;
        public const int BATTLE_MSG_C2B_START = 0x000D0000;

        public const int BATTLE_MSG_B2R_START = 0x000E0000;
        public const int BATTLE_MSG_R2B_START = 0X000F0000;

        public enum SceneType : byte
        {
            Normal = 1,         //野外.
            Dungeon = 2,        //副本.
        }
        /// <summary>
        /// 安全区域色值定义.
        /// </summary>
        public const int MAP_BLOCK_VALUE_SAFE = unchecked((int)0xFF404040);
        
    }
}
