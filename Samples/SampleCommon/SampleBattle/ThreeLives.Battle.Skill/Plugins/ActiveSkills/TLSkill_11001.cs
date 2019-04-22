using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;
using TLCommonSkill.Plugins.ActiveSkills;

namespace ThreeLives.Battle.Skill.Plugins.ActiveSkills
{
    public class TLSkill_11001 : TLSkillBase
    {
        protected override bool LoadData => true;

        public int ID = 11001;
        public float Range = 2.5f;
        public int MoveSkillID = 99991;


        public override int SkillID
        {
            get { return ID; }
            set { ID = value; }
        }

        public override void InitOver(TLVirtual unit)
        {
            base.InitOver(unit);

            var gs = new GameSkill();
            gs.SkillID = MoveSkillID;
            gs.SkillLevel = unit.SkillModule.GetGameSkill(ID).SkillLevel;
            gs.SkillType = GameSkill.TLSkillType.hideActive;
            var lt = new List<GameSkill>();
            lt.Add(gs);
            unit.GetPlayerUnit()?.DataAddSkill(lt, true);
            var ss = unit.mUnit.getSkillState(gs.SkillID);
            ss.LaunchSkill.AutoLaunch = false;
        }

        protected override void OnSkillCoefficientEvent(BattleParams param)
        {

        }

        protected override void OnRegisterUnitEvent(GameSkill info, TLVirtual unit, ref SkillTemplate template)
        {
            base.OnRegisterUnitEvent(info, unit, ref template);

            unit.RegistTryLaunchSkillEvent(OnTryLaunchSkillEvent, info, false);
        }

        private bool OnTryLaunchSkillEvent(ref InstanceUnit.SkillState skill, TLVirtual launcher, ref InstanceUnit.LaunchSkillParam param)
        {
            var unit = launcher.mUnit.Parent.getUnit(param.TargetUnitID);
            if (unit != null)
            {
                var dis = CMath.getDistance(unit.X, unit.Y, launcher.mUnit.X, launcher.mUnit.Y);
                var d = dis - unit.BodyHitSize - launcher.mUnit.BodyHitSize;
                if (d > Range)
                {
                    if (d < skill.Data.AttackRange)
                    {
                        var pa = new InstanceUnit.LaunchSkillParam();
                        pa.TargetUnitID = param.TargetUnitID;
                        launcher.mUnit.launchSkill(MoveSkillID, pa);
                    }

                    return false;
                }
            }

            return true;
        }
    }
}
