using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TLBattle.Common.Data
{
    public class TLPKValueData
    {
        /// <summary>
        /// 红名等级.
        /// </summary>
        public int red_lv;
        /// <summary>
        ///杀戮值下限.
        /// </summary>
        public int point_min;
        /// <summary>
        /// 杀戮值上限.
        /// </summary>
        public int point_max;
        /// <summary>
        /// 攻击增加杀戮值.
        /// </summary>
        public int point_attack;
        /// <summary>
        /// 击杀增加杀戮值.
        /// </summary>
        public int kill_point;
        /// <summary>
        /// 红名颜色.
        /// </summary>
        public uint name_color;
    }
}
