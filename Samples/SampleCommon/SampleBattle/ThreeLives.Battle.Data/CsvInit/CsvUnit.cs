using DeepCore;
using DeepCore.IO;
using DeepCore.Log;
using TLBattle.Common.Plugins;
using System;

namespace TLBattle.Plugins.CsvInit
{
    public class CsvUnit
    {
        #region 常量

        private const string MONSTER_PATH = "/data_config/monster/monster.csv";
        private const string DUNGEON_BATTLE_PATH = "/data_config/monster/dungeon_battle_propty.csv";
        private const string MONSTER_BATTLE_PATH = "/data_config/monster/monster_battle_propty.csv";

        #endregion

        #region 属性

        private Logger log = LoggerFactory.GetLogger("Heros CsvUnit");

        private HashMap<int, Monster> MonsterCsv;
        private HashMap<int, DungeonBattle> DungeonBattleCsv;
        private HashMap<int, MonsterBattle> MonsterBattleCsv;

        #endregion

        #region 加载

        public void LoadInRuntime(string data_dir)
        {
            //加载monster表
            string monsterPath = Resource.FormatPath(data_dir + MONSTER_PATH);
            string textMonster = Resource.LoadAllText(monsterPath);

            if (textMonster != null)
            {
                MonsterCsv = new HashMap<int, Monster>();
                CsvLoader.Load<int, Monster>(textMonster, ref MonsterCsv, "ID");
            }
            else
            {
                throw new Exception("无法加载怪物配置文件 ：" + monsterPath);
            }

            //加载dungeon_battle表
            string dungeonBattlePath = Resource.FormatPath(data_dir + DUNGEON_BATTLE_PATH);
            string textDungeonBattle = Resource.LoadAllText(dungeonBattlePath);

            if (textDungeonBattle != null)
            {
                DungeonBattleCsv = new HashMap<int, DungeonBattle>();
                CsvLoader.Load<int, DungeonBattle>(textDungeonBattle, ref DungeonBattleCsv, "dungeon_level");
            }
            else
            {
                throw new Exception("无法加载怪物配置文件 ：" + monsterPath);
            }

            //加载monster表
            string monsterBattlePath = Resource.FormatPath(data_dir + MONSTER_BATTLE_PATH);
            string textMonsterBattle = Resource.LoadAllText(monsterBattlePath);

            if (textMonsterBattle != null)
            {
                MonsterBattleCsv = new HashMap<int, MonsterBattle>();
                CsvLoader.Load<int, MonsterBattle>(textMonsterBattle, ref MonsterBattleCsv, "ID");
            }
            else
            {
                throw new Exception("无法加载怪物配置文件 ：" + monsterPath);
            }


        }

        #endregion

        #region 读取

        public void SetUnitProp(int sceneId, uint sceneEntryLv, TLSceneProperties.SceneType sceneType, int tempID, int lv, TLUnitProperties.TLUnitType monsterType, ref TLUnitProp prop)
        {
            foreach (var monster in MonsterCsv)
            {
                if (monster.Value.Monster_ID == tempID && monster.Value.Dungeon_ID == sceneId)
                {
                    monster.Value.SetProp(ref prop);
                    return;
                }
            }
            foreach (var monsterBattle in MonsterBattleCsv)
            {
                if (monsterBattle.Value.dungeon_level == sceneEntryLv)
                {
                    foreach (var dungeonBattle in DungeonBattleCsv)
                    {
                        if (dungeonBattle.Value.dungeon_type == (int)sceneType && dungeonBattle.Value.monster_elite_type == (int)monsterType)
                        {
                            CalcCommonMonsterProp(monsterBattle.Value, dungeonBattle.Value, ref prop);
                            return;
                        }
                    }
                }
            }
        }

        public void CalcCommonMonsterProp(MonsterBattle monster, DungeonBattle dun, ref TLUnitProp prop)
        {
            //prop.getPropList(ExtraPropList.ExtraPropType.MaxHP).BasicValue = (int)(monster.MaxHP  * dun.MaxHP / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.CurHP).BasicValue = (int)(monster.MaxHP  * dun.MaxHP / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.Attack).BasicValue = (int)(monster.Attack * dun.Attack / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.Defend).BasicValue = (int)(monster.Defend * dun.Defend / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.MAtk).BasicValue = (int)(monster.MAtk * dun.MAtk / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.MDef).BasicValue = (int)(monster.MDef * dun.MDef / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.CriRate).BasicValue = (int)(monster.CriRate  * dun.CriRate / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.CriResist).BasicValue = (int)(monster.AnicriRate * dun.AnicriRate / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.ToGloveDamage).BasicValue = (int)(monster.ToGloveDamage  * dun.ToGloveDamage / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.ToScytheDamage).BasicValue = (int)(monster.ToScytheDamage * dun.ToScytheDamage / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.ToStaffDamage).BasicValue = (int)(monster.ToStaffDamage * dun.ToStaffDamage / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.GodDamage).BasicValue = (int)(monster.GodDamage * dun.GodDamage / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.MaxDefSlot).BasicValue = (int)(monster.MaxDefSlot * dun.MaxDefSlot / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.TargetToPlayer).BasicValue = (int)(monster.TargetToPlayer * dun.TargetToPlayer / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.AutoRecoverAnger).BasicValue = (int)(monster.AutoRecoverAnger * dun.AutoRecoverAnger / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.Arp).BasicValue = (int)(monster.Arp * dun.Arp / 100f);
            //prop.getPropList(ExtraPropList.ExtraPropType.Mgp).BasicValue = (int)(monster.Mgp * dun.Mgp / 100f);
        }

        #endregion

    }



    public class Monster
    {
        public int ID;//序号id
        public int Monster_ID;//怪物模版id
        public string Name;//怪物名称
        public int Level;//怪物等级
        public int Dungeon_ID;//关卡ID
        public int Type;//怪物类型
        public int MaxHP;//最大生命值
        public int Attack;//攻击力
        public int Defend;//物理防御值
        public int MAtk;//魔法攻击力
        public int MDef;//魔法防御值
        public int CriRate;//暴击率
        public int AnicriRate;//抗暴率
        public int ToGloveDamage;//对拳套伤害
        public int ToScytheDamage;//对镰刀伤害
        public int ToStaffDamage;//对法杖伤害
        public int GodDamage;//神圣伤害
        public int MaxDefSlot;//最大防御槽
        public int TargetToPlayer;//对玩家伤害
        public int AutoRecoverAnger;//自动回怒
        public int Arp;//自动回怒
        public int Mgp;//自动回怒

        public void SetProp(ref TLUnitProp prop)
        {
            //prop.getPropList(ExtraPropList.ExtraPropType.MaxHP).BasicValue = MaxHP;
            //prop.getPropList(ExtraPropList.ExtraPropType.CurHP).BasicValue = MaxHP;
            //prop.getPropList(ExtraPropList.ExtraPropType.Attack).BasicValue = Attack;
            //prop.getPropList(ExtraPropList.ExtraPropType.Defend).BasicValue = Defend;
            //prop.getPropList(ExtraPropList.ExtraPropType.MAtk).BasicValue = MAtk;
            //prop.getPropList(ExtraPropList.ExtraPropType.MDef).BasicValue = MDef;
            //prop.getPropList(ExtraPropList.ExtraPropType.CriRate).BasicValue = CriRate;
            //prop.getPropList(ExtraPropList.ExtraPropType.CriResist).BasicValue = AnicriRate;
            //prop.getPropList(ExtraPropList.ExtraPropType.ToGloveDamage).BasicValue = ToGloveDamage;
            //prop.getPropList(ExtraPropList.ExtraPropType.ToScytheDamage).BasicValue = ToScytheDamage;
            //prop.getPropList(ExtraPropList.ExtraPropType.ToStaffDamage).BasicValue = ToStaffDamage;
            //prop.getPropList(ExtraPropList.ExtraPropType.GodDamage).BasicValue = GodDamage;
            //prop.getPropList(ExtraPropList.ExtraPropType.MaxDefSlot).BasicValue = MaxDefSlot;
            //prop.getPropList(ExtraPropList.ExtraPropType.TargetToPlayer).BasicValue = TargetToPlayer;
            //prop.getPropList(ExtraPropList.ExtraPropType.AutoRecoverAnger).BasicValue = AutoRecoverAnger;
            //prop.getPropList(ExtraPropList.ExtraPropType.Arp).BasicValue = Arp;
            //prop.getPropList(ExtraPropList.ExtraPropType.Mgp).BasicValue = Mgp;
        }
    }

    //加成百分比
    public class DungeonBattle
    {
        public int dungeon_type;//关卡模板等级
        public int monster_elite_type;//怪物模板等级
        public int MaxHP;//最大生命值
        public int Attack;//攻击力
        public int Defend;//物理防御值
        public int MAtk;//魔法攻击力
        public int MDef;//魔法防御值
        public int CriRate;//暴击率
        public int AnicriRate;//抗暴率
        public int ToGloveDamage;//对拳套伤害
        public int ToScytheDamage;//对镰刀伤害
        public int ToStaffDamage;//对法杖伤害
        public int GodDamage;//神圣伤害
        public int MaxDefSlot;//最大防御槽
        public int TargetToPlayer;//对玩家伤害
        public int AutoRecoverAnger;//自动回怒
        public int Arp;//自动回怒
        public int Mgp;//自动回怒
    }

    
    public class MonsterBattle
    {
        public int dungeon_level;//副本等级
        public int MaxHP;//最大生命值
        public int Attack;//攻击力
        public int Defend;//物理防御值
        public int MAtk;//魔法攻击力
        public int MDef;//魔法防御值
        public int CriRate;//暴击率
        public int AnicriRate;//抗暴率
        public int ToGloveDamage;//对拳套伤害
        public int ToScytheDamage;//对镰刀伤害
        public int ToStaffDamage;//对法杖伤害
        public int GodDamage;//神圣伤害
        public int MaxDefSlot;//最大防御槽
        public int TargetToPlayer;//对玩家伤害
        public int AutoRecoverAnger;//自动回怒
        public int Arp;//自动回怒
        public int Mgp;//自动回怒
    }
}
