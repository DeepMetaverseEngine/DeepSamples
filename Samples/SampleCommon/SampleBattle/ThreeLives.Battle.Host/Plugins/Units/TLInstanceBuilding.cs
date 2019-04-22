using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using System.Text;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Server.Plugins.Units
{
    public class TLInstanceBuilding : InstanceBuilding
    {
        public override bool IntersectObj => true;

        public TLInstanceBuilding(InstanceZone zone, AddUnit add) : base(zone, add)
        {
        }

        public override void InitSkills(LaunchSkill baseSkill, LaunchSkill[] skills)
        {
            if (this.Virtual != null && (this.Virtual as TLVirtual).IsFinishModuleInit())
            {
                base.InitSkills(baseSkill, skills);
            }
        }
    }
}
