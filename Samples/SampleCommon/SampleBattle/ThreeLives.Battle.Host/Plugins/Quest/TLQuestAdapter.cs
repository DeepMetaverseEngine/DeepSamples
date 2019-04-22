using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using TLBattle.Server.Message;

namespace TLBattle.Server.Plugins.Quest
{
    public class TLQuestAdapter : IQuestAdapter
    {
        public TLQuestAdapter(InstanceZone zone)
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
            InstancePlayer p = Zone.getPlayerByUUID(playerUUID);
            if (p != null)
            {
                QuestStatusChangedNotify message = new QuestStatusChangedNotify();
                message.questID = quest;
                message.Key = key;
                message.value = value;
                p.queueEvent(message);
            }         
        }
    }
}
