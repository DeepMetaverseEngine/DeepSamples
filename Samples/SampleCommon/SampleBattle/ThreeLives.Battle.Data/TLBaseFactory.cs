using DeepCore;
using DeepCore.GameData;
using DeepCore.GameData.RTS;
using DeepCore.GameData.RTS.Manhattan;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using TLBattle.Common.Plugins;
using TLBattle.Plugins;

namespace TLBattle.Common
{
    /// <summary>
    /// 负责创建数据.
    /// </summary>
    public class TLDataFactory : ZoneDataFactory
    {
        public override ICommonConfig CreateCommonCFG()
        {
            return new TLEditorConfig();
        }

        public override IAttackProperties CreateAttackProperties()
        {
            return new TLAttackProperties();
        }

        public override IBuffProperties CreateBuffProperties()
        {
            return new TLBuffProperties();
        }
        public override IItemProperties CreateItemProperties()
        {
            return new TLItemProperties();
        }

        public override ISceneProperties CreateSceneProperties()
        {
            return new TLSceneProperties();
        }

        public override ISkillProperties CreateSkillProperties()
        {
            return new TLSkillProperties();
        }

        public override ISpellProperties CreateSpellProperties()
        {
            return new TLSpellProperties();
        }

        public override IUnitProperties CreateUnitProperties()
        {
            return new TLUnitProperties();
        }

        //----------------------------------------------------------------------------------------------------------------------

        public static bool ShareTerrain
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
        private static HashMap<int, ZoneManhattanAstar> s_maps = new HashMap<int, ZoneManhattanAstar>();
        private static bool s_save_memory = false;
        public override RTSAstar CreateAstarTerrain(object owner, ZoneInfo mesh, int spaceDiv, EditorTemplates data_root)
        {
            if (ShareTerrain)
            {
                ZoneManhattanAstar astar = null;
                lock (s_maps)
                {
                    if (s_maps.TryGetValue(mesh.ID, out astar) == false)
                    {
                        astar = base.CreateAstarTerrain(owner, mesh, spaceDiv, data_root) as ZoneManhattanAstar;
                        astar.IsShareTerrain = true;
                        s_maps.Add(mesh.ID, astar);
                    }
                }
                return astar;
            }
            else
            {
                return base.CreateAstarTerrain(owner, mesh, spaceDiv, data_root);
            }
        }
        
        //             protected override void InitTerrain(ClientEnterScene msg, out ZoneManhattanMap terrain_data, out AstarManhattan path_finder, out ManhattanMapAreaGenerator area_gen)
        //             {
        //                 if (SaveMemory)
        //                 {
        //                     BotMapInfo minfo = null;
        //                     lock (s_maps)
        //                     {
        //                         minfo = s_maps.Get(this.Data.ID);
        //                         if (minfo == null)
        //                         {
        //                             minfo = new BotMapInfo(base.TerrainSrc.Clone() as ZoneInfo, base.Templates);
        //                             s_maps.Add(this.Data.ID, minfo);
        //                         }
        //                     }
        //                     terrain_data = minfo.terrain_data;
        //                     path_finder = minfo.path_finder;
        //                     area_gen = minfo.area_gen;
        //                     Data.Terrain = null;
        //                     this.IsShareTerrain = true;
        //                     this.IsIgnoreTerrainTouch = true;
        //                 }
        //                 else
        //                 {
        //                     base.InitTerrain(msg, out terrain_data, out path_finder, out area_gen);
        //                 }
        //             }

    }
}
