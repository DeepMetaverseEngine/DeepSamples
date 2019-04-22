using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor;
using DeepCore.Log;
using System;
using TLBattle.Common;
using TLBattle.Plugins;
using TLBattle.Server.Plugins.Quest;
using TLBattle.Server.Plugins.Scene;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using TLBattle.Server.Plugins.Units;
using TLBattle.Server.Plugins.Virtual;
using TLBattle.Server.Scene;

namespace TLBattle.Server
{

    public class TLServerDataFactory : TLDataFactory
    {
        public static string TEMPLATES_LUA_ROOT;

        public override void InitPluginsData(EditorTemplates data_root)
        {
            //BindLogger(LoggerFactory.GetLogger("TLCommonWin32"));

            //战斗用配置表数据. 
            if (TEMPLATES_LUA_ROOT != null)
            {
                TLDataMgr.GetInstance().Init(TEMPLATES_LUA_ROOT);
            }
            else if (EditorTemplates.RUNTIME_IN_SERVER)
            {
                TLDataMgr.GetInstance().Init(data_root.DataRoot + "/../templates_lua");
            }
            else
            {
                TLDataMgr.GetInstance().Init(data_root.DataRoot + "/../../ServerData/templates_lua");
            }

            //反射初始化TLSkill.
            TLBattleSkill.Init(data_root.Templates);
            //反射初始化场景扩展.
            TLSceneFactory.Init();




            //加载怪物能力配置表.
            //TLDataMgr.GetInstance().LoadMonsterData(data_root.DataRoot + "/data_config/monster.xlsx");
            //TLDataMgr.GetInstance().LoadMonsterData(data_root.DataRoot);
            base.InitPluginsData(data_root);
        }
    }

    public class TLServerZoneFactory : InstanceZoneFactory
    {
        private TLFormula mFormula = new TLFormula();
        public override IFormula Formula
        {
            get { return mFormula; }
        }

        public TLServerZoneFactory()
        {
        }
        public override void BindLogger(Logger log)
        {
            TLVirtual.log = log;
        }
        public override EditorScene CreateEditorScene(EditorTemplates templates, InstanceZoneListener listener, SceneData data)
        {
            return TLSceneFactory.CreateScene(templates, listener, data);
        }
        public override IQuestAdapter CreateQuestAdapter(InstanceZone zone)
        {
            return new TLQuestAdapter(zone as TLEditorScene);
        }
        public override ZoneSpaceDivision CreateSpaceDivision(InstanceZone zone)
        {
            return new TLZoneSpaceDivision(zone);
        }
        public override IVirtualUnit CreateUnitVirtual(InstanceUnit owner)
        {
            TLVirtual ret = null;

            switch (owner.Info.UType)
            {
                case UnitInfo.UnitType.TYPE_PLAYER:
                    ret = new TLVirtual_Player(owner);
                    return ret;
                case UnitInfo.UnitType.TYPE_MONSTER:
                    ret = new TLVirtual_Monster(owner);
                    return ret;
                case UnitInfo.UnitType.TYPE_NPC:
                    ret = new TLVirtual_NPC(owner);
                    return ret;
                case UnitInfo.UnitType.TYPE_PET:
                    ret = new TLVirtual_Pet(owner);
                    return ret;
                case UnitInfo.UnitType.TYPE_BUILDING:
                    ret = new TLVirtual_Building(owner);
                    return ret;
                case UnitInfo.UnitType.TYPE_TRIGGER:
                    ret = new TLVirtual_Trigger(owner);
                    return ret;
                case UnitInfo.UnitType.TYPE_PLAYERMIRROR:
                    ret = new TLVirtual_PlayerMirror(owner);
                    return ret;
            }

            ret = new TLVirtual(owner);


            return ret;
        }

        public override InstanceUnit CreateUnit(InstanceZone zone, AddUnit add)
        {
            try
            {
                add.info = add.info.Clone() as UnitInfo;
            }
            catch (System.Exception err)
            {
                throw new Exception("CreateUnit Error ZoneID  = " + zone.SceneData.ID, err);
            }

            switch (add.info.UType)
            {

                //case UnitInfo.UnitType.TYPE_NEUTRALITY:
                case UnitInfo.UnitType.TYPE_NPC:
                    return new TLInstanceNPC(zone, add);
                //case UnitInfo.UnitType.TYPE_SUMMON:
                //    throw new NotImplementedException();
                //return new TLInstanceSummonUnit(zone, add);
                case UnitInfo.UnitType.TYPE_PLAYERMIRROR:
                    return new TLInstancePlayerMirror(zone, add);
                case UnitInfo.UnitType.TYPE_MONSTER:
                    return new TLInstanceMonster(zone, add);
                case UnitInfo.UnitType.TYPE_PLAYER:
                    return new TLInstancePlayer(zone, add);
                case UnitInfo.UnitType.TYPE_PET:
                    return new TLInstancePet(zone, add);
                case UnitInfo.UnitType.TYPE_FOLLOW_UNIT:
                    return new TLInstanceFollowUnit(zone, add);
                case UnitInfo.UnitType.TYPE_TRIGGER:
                    return new TLInstanceTrigger(zone, add);
                case UnitInfo.UnitType.TYPE_BUILDING:
                    return new TLInstanceBuilding(zone, add);
            }

            return base.CreateUnit(zone, add);
        }

        public override InstanceItem CreateItem(InstanceZone zone, AddItem add)
        {
            return new TLInstanceItem(zone, add);
        }

        public override HateSystem CreateHateSystem(InstanceUnit owner)
        {
            return ((owner.Virtual as TLVirtual)).GetHateSystem();
        }

        public static TLVirtual ToVirtual(InstanceUnit unit)
        {
            return (unit.Virtual as TLVirtual);
        }
        //---------------------------------------------------------------------------------------------
        #region Client
        /*
    public static bool ClientSaveMemory
    {
        get { return s_save_memory; }
        set
        {
            s_save_memory = value;
            lock (s_maps)
            {
                if (value == false)
                {
                    s_maps.Clear();
                }
            }
        }
    }
    private static HashMap<int, BotMapInfo> s_maps = new HashMap<int, BotMapInfo>();
    private static bool s_save_memory = true;

    public override ZoneLayer CreateClientZoneLayer(EditorTemplates templates, ILayerClient listener)
    {
        return new BotZoneLayer(templates, listener);
    }
    class BotZoneLayer : HZZoneLayer
    {
        public BotZoneLayer(EditorTemplates dataroot, ILayerClient client)
            : base(dataroot, client)
        {
        }
        protected override void InitTerrain(ClientEnterScene msg, out ZoneManhattanMap terrain_data, out AstarManhattan path_finder, out ManhattanMapAreaGenerator area_gen)
        {
            if (ClientSaveMemory)
            {
                BotMapInfo minfo = null;
                lock (s_maps)
                {
                    minfo = s_maps.Get(this.Data.ID);
                    if (minfo == null)
                    {
                        minfo = new BotMapInfo(base.TerrainSrc.Clone() as ZoneInfo, base.Templates);
                        s_maps.Add(this.Data.ID, minfo);
                    }
                }
                terrain_data = minfo.terrain_data;
                path_finder = minfo.path_finder;
                area_gen = minfo.area_gen;
                Data.Terrain = null;
                this.IsShareTerrain = true;
                this.IsIgnoreTerrainTouch = true;
            }
            else
            {
                base.InitTerrain(msg, out terrain_data, out path_finder, out area_gen);
            }
        }
        protected override void DisposeTerrain()
        {
            if (ClientSaveMemory)
            {
            }
            else
            {
                base.DisposeTerrain();
            }
        }
    }
    class BotMapInfo
    {
        public readonly BotLayerManhattanMap terrain_data;
        public readonly AstarManhattan path_finder;
        public readonly ManhattanMapAreaGenerator area_gen;
        public BotMapInfo(ZoneInfo data, TemplateManager templates)
        {
            this.terrain_data = new BotLayerManhattanMap(templates, data);
            this.path_finder = new AstarManhattan(terrain_data, true, 0);
            this.area_gen = new ManhattanMapAreaGenerator(terrain_data.Data);
        }
    }
    class BotLayerManhattanMap : ZoneManhattanMap
    {
        public BotLayerManhattanMap(TemplateManager templates, ZoneInfo info)
            : base(info, templates.TerrainDefinition)
        {
        }
        public override bool SetValue(int bx, int by, int value)
        {
            return false;
        }
    }
    */
        #endregion
    }


}
