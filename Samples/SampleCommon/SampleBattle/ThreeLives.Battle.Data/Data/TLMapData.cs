using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeLives.Battle.Data.Data
{
    /// <summary>
    /// 场景地图-模板关联数据
    /// </summary>
    public class TLMapData
    {
        public int id;
        public int zone_template_id;
        /// <summary>
        /// 0静态，1动态.
        /// </summary>
        public int team_dynaic;
        /// <summary>
        /// 所属难度的标识
        ///1=普通副本难度（Dungeon-Normal强度表）.
        ///2=精英副本难度（Dungeon-NightMare强度表）.
        ///3=英雄副本难度（Dungeon-Hell强度表）.
        /// </summary>
        public int hard;
        /// <summary>
        /// 是否能自动战斗
        /// 0=允许自动战斗
        ///(进入后默认关闭自动战斗)
        /// 1=允许自动战斗
        ///(进入后默认开启自动战斗)
        /// 2=通关一次后可以自动战斗
        ///(通关后再次进入默认关闭自动战斗)
        /// 3=始终不允许自动战斗.
        /// </summary>
        public int auto_fight;
        /// <summary>
        /// 是否能使用仙侣技能.0不允许，1允许.
        /// </summary>
        public int use_god;
        /// <summary>
        /// 0计算PK值，1不计算PK值
        /// </summary>
        public int ignore_pk;
        /// <summary>
        /// 0不允许改变，1允许改变.
        /// </summary>
        public int change_pk;
        /// <summary>
        /// 是否允许公会追杀 0不允许1允许.
        /// </summary>
        public int is_guildchase;

    }
}
