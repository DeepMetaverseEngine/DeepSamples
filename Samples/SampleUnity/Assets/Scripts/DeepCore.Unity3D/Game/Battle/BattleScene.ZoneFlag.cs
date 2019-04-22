using System.Security.Policy;
using DeepCore;
using DeepCore.GameData.RTS;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost.Instance;
using DeepCore.GameSlave;

namespace DeepCore.Unity3D.Battle
{
    public partial class BattleScene
    {
        HashMap<string, BattleFlag> mFlags = new HashMap<string, BattleFlag>();
        HashMap<string, ZoneEditorRegion> mFlagsRegions = new HashMap<string, ZoneEditorRegion>();

        public T GetFlag<T>(string name) where T : BattleFlag
        {
            return mFlags.Get(name) as T;
        }
        
        public ZoneEditorRegion GetEditorRegionFlag(string name) 
        {
            return mFlagsRegions.Get(name);
        }

        private void InitRegeion()
        {
            foreach (var zf in mBattle.Layer.Flags)
            {
                if (zf is ZoneEditorRegion)
                {
                    if (!string.IsNullOrEmpty(zf.Name))
                    {
                        mFlagsRegions.Add(zf.Name, zf as ZoneEditorRegion);
                        
                    }
                }
                
            }
        }
        
        
        public bool IsInRegion(RegionData regionData,float x, float y)
        {
            if (regionData.RegionType == RegionData.Shape.RECTANGLE)
            {
                return CMath.includeRectPoint(regionData.X - regionData.W/2, regionData.Y - regionData.H/2, regionData.X + regionData.W/2, regionData.Y + regionData.H/2, x, y);
            }
            else
            {
                return CMath.includeRoundPoint(regionData.X, regionData.Y, regionData.Radius, x, y);
            }
        }
        
        protected void InitDecoration()
        {
            foreach (var zf in mBattle.Layer.Flags)
            {
                if (zf is ZoneEditorDecoration)
                {
                    BattleDecoration dc = BattleFactory.Instance.CreateBattleDecoration(this, zf as ZoneEditorDecoration);
                    mFlags.Add(dc.Name, dc);
                    dc.OnChanged();
                }
            }

            InitRegeion();
        }
        protected void DisposeDecoration()
        {
            foreach (var zf in mFlags)
            {
                zf.Value.Dispose();
            }
        }
        private void Layer_DecorationChanged(ZoneLayer layer, ZoneEditorDecoration ed)
        {
            var dc = GetFlag<BattleDecoration>(ed.Name);
            if (dc != null)
            {
                dc.OnChanged();
            }
        }

    }
}
