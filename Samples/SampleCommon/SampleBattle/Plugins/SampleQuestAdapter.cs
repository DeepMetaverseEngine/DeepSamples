using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;

namespace SampleBattle.Plugins
{
    public class SampleQuestAdapter : IQuestAdapter
    {
        public SampleQuestAdapter(InstanceZone zone)
            : base(zone)
        {

        }

        public override void DoAcceptQuest(string playerUUID, string quest, string args)
        {
        }
        public override void DoCommitQuest(string playerUUID, string quest, string args)
        {
        }
        public override void DoDropQuest(string playerUUID, string quest, string args)
        {
        }
        public override void DoUpdateQuestStatus(string playerUUID, string quest, string key, string value)
        {
        }
    }
}
