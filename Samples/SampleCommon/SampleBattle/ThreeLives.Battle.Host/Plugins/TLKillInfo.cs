using DeepCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TLBattle.Server.Plugins
{
    public class TLKillInfo
    {
        public int SceneID;
        public string FirstAtkPlayerUUID;
        public string LastAtkPlayerUUID;
        public HashMap<string, AttackData> StatisticMap;

        public class AttackData
        {
            public string PlayerUUID;
            public int Damage;
        }
    }
}
