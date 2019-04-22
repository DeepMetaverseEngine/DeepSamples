
using DeepCore;
using System;
using System.Collections.Generic;
using ThreeLives.Battle.Data.Data;
using TLBattle.Common.Data;

namespace TLBattle.Plugins
{
    public class TLDataMgr
    {
        private static TLDataMgr mInstance = null;

        /// <summary>
        /// 技能数据管理.
        /// </summary>
        public SkillDataMgr SkillData;
        /// <summary>
        /// 怪物数据管理.
        /// </summary>
        public MonsterDataMgr MonsterData;
        /// <summary>
        /// PK红名数据.
        /// </summary>
        public PKValueDataMgr PKValueData;
        /// <summary>
        /// 战斗公式数据.
        /// </summary>
        public BattleFormulaDataMgr BattleFormulaData;
        /// <summary>
        /// 游戏配置数据.
        /// </summary>
        public GameConfigDataMgr GameConfigData;
        /// <summary>
        /// 地图管理数据.
        /// </summary>
        public TLMapDataMgr MapDataMgr;
        /// <summary>
        /// 经脉数据.
        /// </summary>
        public TLMeridiansMgr MeridiansMgr;

        private TLDataMgr()
        {
            mInstance = this;
        }

        public static TLDataMgr GetInstance()
        {
            if (mInstance == null)
            {
                new TLDataMgr();
            }

            return mInstance;
        }

        public void Init(string dataRoot)
        {
            dataRoot = dataRoot.Replace('\\', '/');
            MonsterData = new MonsterDataMgr(dataRoot);
            PKValueData = new PKValueDataMgr(dataRoot);
            SkillData = new SkillDataMgr(dataRoot);
            BattleFormulaData = new BattleFormulaDataMgr(dataRoot);
            GameConfigData = new GameConfigDataMgr(dataRoot);
            MapDataMgr = new TLMapDataMgr(dataRoot);
            MeridiansMgr = new TLMeridiansMgr(dataRoot);
        }

        /// <summary>
        /// 技能数据配置管理.
        /// </summary>
        public class SkillDataMgr
        {
            const string SKILL_DAMAGE_PATH = "/skill/skill_damage.xlsx";

            private HashMap<int, HashMap<int, TLSkillData>> DataMap = new HashMap<int, HashMap<int, TLSkillData>>();

            public SkillDataMgr(string rootpath)
            {
                try
                {
                    LoadData(rootpath);
                }
                catch (Exception err)
                {
                    throw new Exception("SkillDataMgr Init Error: " + err.ToString());
                }
            }

            private void LoadData(string rootpath)
            {
                int skillID = 0, skillLv = 0;
                try
                {
                    using (var loader = new TemplateLoader.XLSLoader(rootpath + SKILL_DAMAGE_PATH))
                    //using (var loader = new LuaLoader(rootpath + SKILL_DAMAGE_PATH))
                    {
                        var ret = loader.LoadTemplatesAsList<int, TLSkillData>(nameof(TLSkillData.id));

                        TLSkillData data = null;
                        HashMap<int, TLSkillData> subMap = null;
                        for (int i = 0; i < ret.Count; i++)
                        {
                            data = ret[i];

                            skillID = data.skill_id;
                            skillLv = data.skill_lv;

                            if (!DataMap.TryGetValue(data.skill_id, out subMap))
                            {
                                subMap = new HashMap<int, TLSkillData>();
                                DataMap.Add(data.skill_id, subMap);
                            }

                            subMap.Add(data.skill_lv, data);
                        }
                    }
                }
                catch (Exception err)
                {
                    throw new Exception(string.Format("skill config [{0}] error: skill = [{1}] lv = [{2}]", SKILL_DAMAGE_PATH, skillID, skillLv), err);

                }

                foreach (var kvp in DataMap)
                {
                    TestData(kvp.Key, kvp.Value);
                }
            }

            /// <summary>
            /// 获得指定技能的数据.
            /// </summary>
            /// <param name="id"></param>
            /// <param name="lv"></param>
            /// <returns></returns>
            public TLSkillData GetSkillData(int id, int lv)
            {
                TLSkillData ret = null;
                HashMap<int, TLSkillData> submap = null;

                if (DataMap.TryGetValue(id, out submap))
                {
                    if (submap.TryGetValue(lv, out ret))
                    {
                        return ret;
                    }
                }

                return ret;
            }

            public HashMap<int, TLSkillData> GetSkillData(int id)
            {
                HashMap<int, TLSkillData> submap = null;
                if (DataMap.TryGetValue(id, out submap))
                {
                    return submap;
                }

                return null;
            }

            private bool TestData(int id, HashMap<int, TLSkillData> data)
            {
                if (data == null)
                {
                    throw new Exception(string.Format("Skill ConfigData Error: ID", id));
                }
                else
                {
                    //数据个数等同于等级.
                    int lv = data.Count;
                    int sum = calSum(lv);

                    foreach (var d in data)
                    {
                        sum -= d.Key;
                    }

                    if (sum != 0)
                    {
                        //数据配置异常.
                        throw new Exception(string.Format("技能等级配置遗漏: ID", id));
                    }
                }

                return true;
            }

            /// <summary>
            /// 0到某个自然的求和.
            /// </summary>
            /// <param name="n"></param>
            /// <returns></returns>
            private int calSum(int n)
            {
                return n * (n + 1) / 2;
            }

        }

        /// <summary>
        /// 怪物数据配置管理.
        /// </summary>
        public class MonsterDataMgr
        {
            const string MONSTER_PATH = "/monster/monster.xlsx";
            const string MONSTER_DATA = "monster";
            const string MONSTER_COF_DATA = "monster_coefficient";
            private HashMap<int, HashMap<int, TLMonsterData>> DataMap = new HashMap<int, HashMap<int, TLMonsterData>>();
            private HashMap<int, TLMonsterCofData> MonstCofDataMap = new HashMap<int, TLMonsterCofData>();

            const string MONSTER_DROP_PATH = "/monster/monster_drop.xlsx";
            const string MONSTER_DROP_SHEET = "monster_drop";
            const string MONSTER_DROP_HP_SHEET = "monster_hp";

            public MonsterDataMgr(string rootpath)
            {
                try
                {
                    LoadData(rootpath);
                    LoadRewardData(rootpath);
                }
                catch (Exception err)
                {
                    throw new Exception("MonsterDataMgr Init Error: " + err.ToString());
                }
            }

            private void LoadData(string rootpath)
            {

                using (var loader = new TemplateLoader.XLSLoader(rootpath + MONSTER_PATH))
                {
                    var ret = loader.LoadTemplatesAsList<int, TLMonsterData>(nameof(TLMonsterData.id), MONSTER_DATA);
                    MonstCofDataMap = loader.LoadTemplates<int, TLMonsterCofData>(nameof(TLMonsterCofData.lv), MONSTER_COF_DATA);
                    TLMonsterData data = null;
                    HashMap<int, TLMonsterData> subMap = null;
                    for (int i = 0; i < ret.Count; i++)
                    {
                        data = ret[i];

                        if (!DataMap.TryGetValue(data.dungeon_id, out subMap))
                        {
                            subMap = new HashMap<int, TLMonsterData>();
                            DataMap.Add(data.dungeon_id, subMap);
                        }
                        try
                        {
                            subMap.Add(data.monster_id, data);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("MonsterData InitError: monster_id=" + data.monster_id + " data.dungeon_id= " + data.dungeon_id);
                        }

                    }
                }



            }

            public TLMonsterData GetMonsterData(int sceneID, int monsterID)
            {
                TLMonsterData ret = null;
                HashMap<int, TLMonsterData> submap = null;


                if (DataMap.TryGetValue(sceneID, out submap))
                {
                    if (submap.TryGetValue(monsterID, out ret))
                    {
                        return ret;
                    }
                }

                return ret;
            }

            /// <summary>
            /// 怪物动态能力系数.
            /// </summary>
            /// <param name="lv"></param>
            /// <returns></returns>
            public TLMonsterCofData GetMonsterCofData(int lv)
            {
                return MonstCofDataMap.Get(lv);
            }

            private HashMap<int, HashMap<int, TLMonsterDropData>> MonsterDropData = new HashMap<int, HashMap<int, TLMonsterDropData>>();
            private HashMap<int, TLMonsterHPDropData> MonstHPDropMap = new HashMap<int, TLMonsterHPDropData>();

            private void LoadRewardData(string rootpath)
            {
                using (var loader = new TemplateLoader.XLSLoader(rootpath + MONSTER_DROP_PATH))
                {
                    TLMonsterDropData data = null;
                    HashMap<int, TLMonsterDropData> submap = null;
                    var ret = loader.LoadTemplatesAsList<int, TLMonsterDropData>(nameof(TLMonsterDropData.id), MONSTER_DROP_SHEET);
                    if (ret != null)
                    {
                        for (int i = 0; i < ret.Count; i++)
                        {
                            data = ret[i];
                            if (!MonsterDropData.TryGetValue(data.scene_id, out submap))
                            {
                                submap = new HashMap<int, TLMonsterDropData>();
                                MonsterDropData.Add(data.scene_id, submap);
                            }

                            submap.Add(data.monster_id, data);
                        }
                    }

                    MonstHPDropMap = loader.LoadTemplates<int, TLMonsterHPDropData>(nameof(TLMonsterHPDropData.drop_id), MONSTER_DROP_HP_SHEET);
                }

            }

            public KeyValuePair<int, int[]> GetMonsterHPDrop(int sceneID, int monsterID)
            {
                if (MonsterDropData.TryGetValue(sceneID, out var submap))
                {
                    if (submap.TryGetValue(monsterID, out var dropData))
                    {
                        if (MonstHPDropMap.TryGetValue(dropData.drop_id, out var HPDrop))
                        {
                            return new KeyValuePair<int, int[]>(dropData.drop_id, HPDrop.hp.val);
                        }
                    }
                }

                return new KeyValuePair<int, int[]>();
            }
        }

        public class PKValueDataMgr
        {
            const string PATH = "/red_name/red_name.xlsx";
            private HashMap<int, TLPKValueData> DataMap = new HashMap<int, TLPKValueData>();
            private int mRedNameLv = 3;
            public PKValueDataMgr(string rootpath)
            {
                try
                {
                    LoadData(rootpath);
                }
                catch (Exception err)
                {
                    throw new Exception("PKValueDataMgr Init Error: " + err.ToString());
                }
            }

            private void LoadData(string rootpath)
            {
                using (var loader = new TemplateLoader.XLSLoader(rootpath + PATH))
                //using (var loader = new LuaLoader(rootpath + PATH))
                {
                    DataMap = loader.LoadTemplates<int, TLPKValueData>(nameof(TLPKValueData.red_lv));
                }
            }

            public TLPKValueData GetData(int lv)
            {
                TLPKValueData ret = null;

                DataMap.TryGetValue(lv, out ret);

                return ret;
            }

            public int GetLv(int pkvalue)
            {
                int ret = 1;

                int lv = 1;
                TLPKValueData data = null;
                while (true)
                {
                    data = GetData(lv);
                    if (data == null)
                    {
                        break;
                    }
                    else
                    {
                        if (pkvalue >= data.point_min && pkvalue <= data.point_max)
                        {
                            ret = lv;
                            lv++;
                        }
                        else
                        {
                            lv++;
                        }
                    }
                }

                return ret;
            }

            public uint GetColor(int lv)
            {
                var data = GetData(lv);
                if (data != null)
                    return data.name_color;
                return 0;
            }

            public int RedNameLv()
            {
                return mRedNameLv;
            }
        }

        public class BattleFormulaDataMgr
        {
            const string PATH = "/character/battleformula.xlsx";
            private HashMap<int, TLBattleFormulaData> DataMap = new HashMap<int, TLBattleFormulaData>();

            public BattleFormulaDataMgr(string rootpath)
            {
                try
                {
                    LoadData(rootpath);
                }
                catch (Exception err)
                {
                    throw new Exception("PKValueDataMgr Init Error: " + err.ToString());
                }
            }

            private void LoadData(string rootpath)
            {
                using (var loader = new TemplateLoader.XLSLoader(rootpath + PATH))
                {
                    DataMap = loader.LoadTemplates<int, TLBattleFormulaData>(nameof(TLBattleFormulaData.id));
                }
            }


            public TLBattleFormulaData GetData()
            {
                if (DataMap != null)
                {
                    TLBattleFormulaData ret = null;
                    DataMap.TryGetValue(1, out ret);
                    return ret;
                }
                return null;
            }

        }

        public class GameConfigDataMgr
        {
            const string PATH = "/config/game_config.xlsx";
            private const string ANGER_LIMIT = "anger_limit";
            private const string ANGER_RECOVERY = "recovery_anger";
            private const string TP_TIMEMS = "transfer_time";
            private const string TP_FLY_TIMEMS = "transfer_flytime";
            private const string DungeonDynamic = "dungeons_dynamic";
            private List<int> DungeonDynamicLt = null;
            private int mAngerLimitCache = -1;
            private int mAngerRecoverCache = -1;
            private int mTPTimeMS = -1;
            private int mTPFlyTimeMS = -1;
            private int mRedNameReduceTimeCache = -1;
            private int mRedNameReduceValueCache = -1;
            private const string mRedNameReduceTime = "red_point_time";
            private const string mRedNameReduceValue = "red_point_reduce";
            private const string mQuest_speed_up = "quest_speed_up";
            private const string mQuest_speed_buff = "quest_speed_buff";
            private int mQuestSpeedUpLvCache = -1;
            private int mQuestSpeedBuffIDCache = -1;


            private HashMap<string, TLBattleGameConfigData> DataMap = new HashMap<string, TLBattleGameConfigData>();

            public GameConfigDataMgr(string rootpath)
            {
                try
                {
                    LoadData(rootpath);
                }
                catch (Exception err)
                {
                    throw new Exception("GameConfigDataMgr Init Error: " + err.ToString());
                }
            }

            private void LoadData(string rootpath)
            {
                using (var loader = new TemplateLoader.XLSLoader(rootpath + PATH))
                {
                    DataMap = loader.LoadTemplates<string, TLBattleGameConfigData>(nameof(TLBattleGameConfigData.id));
                }


                TLBattleGameConfigData config;
                if (DataMap.TryGetValue(DungeonDynamic, out config))
                {
                    string str = config.paramvalue;
                    DungeonDynamicLt = new List<int>();
                    int temp;
                    string[] ret = str.Split(',');
                    if (ret != null)
                    {
                        for (int i = 0; i < ret.Length; i++)
                        {
                            temp = Convert.ToInt32(ret[i]);
                            DungeonDynamicLt.Add(temp);
                        }
                    }
                }
            }

            private int TryGetValueInt(string key, int defaultValue = 0)
            {
                TLBattleGameConfigData config;
                if (DataMap.TryGetValue(key, out config))
                {
                    int result = defaultValue;
                    if (Int32.TryParse(config.paramvalue, out result))
                    {
                        return result;
                    }
                }
                return defaultValue;
            }

            /// <summary>
            /// 怒气上限.
            /// </summary>
            /// <returns></returns>
            public int GetAngerLimit()
            {
                if (mAngerLimitCache == -1)
                {
                    mAngerLimitCache = TryGetValueInt(ANGER_LIMIT);
                }

                return mAngerLimitCache;
            }

            /// <summary>
            /// 每秒怒气回复.
            /// </summary>
            /// <returns></returns>
            public int GetAngerRecovery()
            {
                if (mAngerRecoverCache == -1)
                {
                    mAngerRecoverCache = TryGetValueInt(ANGER_RECOVERY);
                }

                return mAngerRecoverCache;
            }

            public int GetTPTimesMS()
            {
                if (mTPTimeMS == -1)
                {
                    mTPTimeMS = TryGetValueInt(TP_TIMEMS);
                }

                return mTPTimeMS;
            }

            public int GetTPFlyTimeMS()
            {
                if (mTPFlyTimeMS == -1)
                {
                    mTPFlyTimeMS = TryGetValueInt(TP_FLY_TIMEMS);
                }
                return mTPFlyTimeMS;
            }

            public int GetDungeonDynamicCof(int playerCount)
            {
                int c = playerCount - 1;
                if (c < 0 || playerCount >= DungeonDynamicLt.Count)
                    return 10000;
                else
                    return DungeonDynamicLt[c];
            }

            public int GetRedNameReduceTime()
            {
                if (mRedNameReduceTimeCache == -1)
                {
                    mRedNameReduceTimeCache = TryGetValueInt(mRedNameReduceTime, 1);
                }

                return mRedNameReduceTimeCache;
            }

            public int GetRedNameReduceValue()
            {
                if (mRedNameReduceValueCache == -1)
                {
                    mRedNameReduceValueCache = TryGetValueInt(mRedNameReduceValue, 1);
                }

                return mRedNameReduceValueCache;
            }

            public int GetQuestSpeedUpLvLimit()
            {
                if (mQuestSpeedUpLvCache == -1)
                    mQuestSpeedUpLvCache = TryGetValueInt(mQuest_speed_up, 1);

                return mQuestSpeedUpLvCache;
            }

            public int GetQuestSpeedUpBuffID()
            {
                if (mQuestSpeedBuffIDCache == -1)
                    mQuestSpeedBuffIDCache = TryGetValueInt(mQuest_speed_buff, 0);

                return mQuestSpeedBuffIDCache;
            }
        }

        public class TLMapDataMgr
        {
            const string PATH = "/map/map_data.xlsx";
            private HashMap<int, TLMapData> DataMap = new HashMap<int, TLMapData>();

            public TLMapDataMgr(string rootpath)
            {
                try
                {
                    LoadData(rootpath);
                }
                catch (Exception err)
                {
                    throw new Exception("TLMapDataMgr Init Error: " + err.ToString());
                }
            }

            private void LoadData(string rootpath)
            {
                using (var loader = new TemplateLoader.XLSLoader(rootpath + PATH))
                {
                    DataMap = loader.LoadTemplates<int, TLMapData>(nameof(TLMapData.id));
                }
            }

            public int MapTemplateID(int mapID)
            {
                TLMapData ret = null;

                if (DataMap.TryGetValue(mapID, out ret))
                {
                    return ret.zone_template_id;
                }

                return 0;
            }

            public TLMapData MapData(int mapID)
            {
                TLMapData ret = null;
                DataMap.TryGetValue(mapID, out ret);
                return ret;
            }
        }

        public class TLMeridiansMgr
        {
            const string PATH = "/skill/skill_meridians.xlsx";
            public HashMap<int, HashMap<int, TLMeridiansData>> DataMap = new HashMap<int, HashMap<int, TLMeridiansData>>();


            public TLMeridiansMgr(string rootpath)
            {
                try
                {
                    LoadData(rootpath);
                }
                catch (Exception err)
                {
                    throw new Exception("TLMeridiansMgr Init Error: " + err.ToString());
                }
            }

            private void LoadData(string rootpath)
            {
                using (var loader = new TemplateLoader.XLSLoader(rootpath + PATH))
                {
                    var temp = loader.LoadTemplatesAsList<int, TLMeridiansData>(nameof(TLMeridiansData.id));
                    HashMap<int, TLMeridiansData> subMap = null;
                    foreach (var item in temp)
                    {
                        if (!DataMap.TryGetValue(item.main, out subMap))
                        {
                            subMap = new HashMap<int, TLMeridiansData>();
                            DataMap.Add(item.main, subMap);
                        }

                        subMap.Add(item.skill_id, item);
                    }
                }
            }

            public TLMeridiansData GetData(int meridiansID, int skillID)
            {
                HashMap<int, TLMeridiansData> map = null;

                map = DataMap.Get(meridiansID);

                if (map == null) return null;

                return map.Get(skillID);
            }
        }
    }
}
