using DeepCore.GameData.Zone;
using DeepCore.GameSlave;

namespace DeepCore.Unity3D.Battle
{
    public interface IComAIActor
    {
        uint ObjectID();

    }
    /// <summary>
    /// 主角
    /// </summary>
    public class ComAIActor : ComAIUnit, IComAIActor
    {
        protected ZoneActor ZActor { get { return ZObj as ZoneActor; } }


        public ComAIActor(BattleScene battleScene, ZoneActor obj)
            : base(battleScene, obj)
        {
        }

        public void SendUnitAxisAngle(float angle, float distance, float faceto)
        {
            ZActor.SendUnitAxisAngle(angle, distance, faceto);
        }

        public void SendUnitLaunchSkill(UnitLaunchSkillAction launch)
        {
            ZActor.SendUnitLaunchSkill(launch);
        }

        public void SendUnitLaunchSkill(int skillID)
        {
            ZActor.SendUnitLaunchSkill(skillID);
        }

        public void SendUnitLaunchSkill(int skillID, uint targetObjectID)
        {
            ZActor.SendUnitLaunchSkill(skillID, targetObjectID);
        }

        public void SendUnitLaunchSkill(int skillID, float x, float y)
        {
            ZActor.SendUnitLaunchSkill(skillID, x, y);
        }

        public override bool IsImportant()
        {
            return true;
        }

        uint IComAIActor.ObjectID()
        {
            return this.ObjectID;
        }
    }

}