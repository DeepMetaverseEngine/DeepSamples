using DeepCore.GameData;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor;
using DeepCore.GameSlave;
using SampleBattle.Scene;

namespace SampleBattle.Plugins
{
    public class SampleDataFactory : ZoneDataFactory
    {
        public override IUnitProperties CreateUnitProperties()
        {
            return new SampleUnitProperties();
        }
        public override IAttackProperties CreateAttackProperties()
        {
            return new SampleAttackProperties();
        }
        public override IBuffProperties CreateBuffProperties()
        {
            return new SampleBuffProperties();
        }
        public override IItemProperties CreateItemProperties()
        {
            return new SampleItemProperties();
        }
        public override ISkillProperties CreateSkillProperties()
        {
            return new SampleSkillProperties();
        }
        public override ISpellProperties CreateSpellProperties()
        {
            return new SampleSpellProperties();
        }
        public override ISceneProperties CreateSceneProperties()
        {
            return new SampleSceneProperties();
        }
        public override ICommonConfig CreateCommonCFG()
        {
            return new SampleConfig();
        }
    }

    public class SampleZoneFactory : InstanceZoneFactory
    {
        private SampleFormula mFormula = new SampleFormula();
        public override IFormula Formula
        {
            get { return mFormula; }
        }
        public SampleZoneFactory()
        {
        }
        public override IVirtualUnit CreateUnitVirtual(InstanceUnit owner)
        {
            return new SampleUnitVirtual();
        }
        public override IQuestAdapter CreateQuestAdapter(InstanceZone zone)
        {
            return new SampleQuestAdapter(zone);
        }
        public override EditorScene CreateEditorScene(EditorTemplates dataroot, InstanceZoneListener listener, SceneData data)
        {
            return new SampleEditorScene(dataroot, listener, data);
        }
        public override HateSystem CreateHateSystem(InstanceUnit owner)
        {
            return new DeepCore.GameHost.Simple.SimpleHateSystem(owner);
        }
    }

    public class SampleClientFactory : ClientZoneFactory
    {

    }
}
