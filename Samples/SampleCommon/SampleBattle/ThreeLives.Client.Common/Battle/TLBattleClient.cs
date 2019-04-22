using DeepMMO.Client;
using DeepMMO.Client.Battle;
using DeepMMO.Protocol.Client;

namespace TLClient.Battle
{
    public class TLBattleClient : RPGBattleClient
    {
        public TLBattleClient(RPGClient client, ClientEnterZoneNotify sd) : base(client, sd)
        {
        }
    }
}
