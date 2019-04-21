using DeepCore.GameSlave;

namespace DeepCore.Unity3D.Battle
{
    public class BattleFlag : BattleObject
    {
        protected readonly ZoneFlag ZFlag;

        public string Name { get { return ZFlag.Name; } }
        public bool Enable { get { return ZFlag.Enable; } }

        public BattleFlag(BattleScene battleScene, ZoneFlag zf)
            : base(battleScene, string.Format("{0}_{1}", zf.GetType().Name, zf.EditorData.Name))
        {
            this.ZFlag = zf;
        }
    }
}