using DeepCore;
using DeepCore.GameSlave;

namespace DeepCore.Unity3D.Battle
{
    public partial class BattleScene
    {
        HashMap<string, BattleFlag> mFlags = new HashMap<string, BattleFlag>();

        public T GetFlag<T>(string name) where T : BattleFlag
        {
            return mFlags.Get(name) as T;
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
