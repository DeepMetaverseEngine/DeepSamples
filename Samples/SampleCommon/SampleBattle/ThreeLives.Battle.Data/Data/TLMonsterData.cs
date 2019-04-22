namespace TLBattle.Common.Data
{
    /// <summary>
    /// 怪物数据.
    /// </summary>
    public class TLMonsterData
    {
        public int id;
        public int dungeon_id;
        public string dungeon_name;
        public int monster_id;
        public int dynamic;//0静态1动态.
        public string name;
        public int level;
        public int type;
        public int attack;
        public int defend;
        public int mdef;
        public int maxhp;
        public int through;
        public int block;
        public int hit;
        public int dodge;
        public int crit;
        public int rescrit;
        public int cridamageper;
        public int redcridamageper;
        public int runspeed;
        public int autorecoverhp;
        public int onhitrecoverhp;
        public int goddamage;

        /// <summary>
        /// 掉落类型.
        /// </summary>
        public int reward_type;
        /// <summary>
        /// 称号ID.
        /// </summary>
        public int title;
    }

    public class TLMonsterCofData
    {
        public int lv;

        public int attack;
        public int defend;
        public int mdef;
        public int maxhp;
        public int through;
        public int block;
        public int hit;
        public int dodge;
        public int crit;
        public int rescrit;
        public int cridamageper;
        public int redcirdamageper;
        public int runspeed;
        public int autorecoverhp;
        public int onhitrecoverhp;
        public int goddamage;
    }

    public class TLMonsterDropData
    {
        public int id;
        public int scene_id;
        public int monster_id;
        public int drop_id;
    }

    public class TLMonsterHPDropData
    {
        public int drop_id;
        public HPGroup hp;
    }

    public class HPGroup
    {
        public int[] val;
    }
}
