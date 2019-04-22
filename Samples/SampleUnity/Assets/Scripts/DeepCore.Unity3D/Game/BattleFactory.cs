using DeepCore.Unity3D.Utils;
using DeepCore.GameSlave;
using DeepCore.GameSlave.Client;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public abstract class BattleFactory
    {
        private static BattleFactory gInstance;

        public static BattleFactory Instance
        {
            get { return gInstance; }
        }

        protected BattleFactory()
        {
            gInstance = this;
        }

        public abstract BattleScene CreateBattleScene(AbstractBattle battle);
        public abstract TerrainAdapter TerrainAdapter { get; }
        public abstract ICamera Camera { get; }
        public abstract int StageNavLay { get; }
        public abstract DisplayCell CreateDisplayCell(GameObject root, string name = "DisplayCell");
        public abstract ComAICell CreateComAICell(BattleScene battleScene, ZoneObject obj);
        public abstract BattleDecoration CreateBattleDecoration(BattleScene battleScene, ZoneEditorDecoration zf);
        public abstract void OnError(string msg);
        public abstract void MakeDamplingJoint(GameObject body, GameObject form, GameObject to);
        public abstract void Update(float deltaTime);
    }

}
