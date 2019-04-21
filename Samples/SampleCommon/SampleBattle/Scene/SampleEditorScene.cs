using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.Helper;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor;
using SampleBattle.Plugins;
using System;

namespace SampleBattle.Scene
{
    public static class MapBlockValue
    {
        public const int AREA_SAFE = -8355712;
    }

    public class SampleEditorScene : EditorScene
    {
        private SampleSceneProperties mSceneProp;

        public SampleEditorScene(EditorTemplates dataroot, InstanceZoneListener listener, SceneData data)
            : base(dataroot, listener, data, DateTime.Now.Millisecond)
        {
            this.mSceneProp = data.Properties as SampleSceneProperties;
        }

        public override bool IsAttackable(InstanceUnit src, InstanceUnit target, SkillTemplate.CastTarget expectTarget, AttackReason reason, ITemplateData weapon)
        {
            if (!IsVisibleAOI(src, target))
                return false;
            if (!target.IsVisible)
                return false;
            if (target.IsInvincible)
                return false;

            if (target.CanWhiplashDeadBody)
            {
                if (!target.IsAttackable)
                    return false;
            }
            else
            {
                if (!target.IsActive)
                    return false;
            }
            var sa = src.CurrentArea;
            if (sa != null && sa.CurrentMapNodeValue == MapBlockValue.AREA_SAFE)
            {
                return false;
            }
            var da = target.CurrentArea;
            if (da != null && da.CurrentMapNodeValue == MapBlockValue.AREA_SAFE)
            {
                return false;
            }

            switch (expectTarget)
            {
                case SkillTemplate.CastTarget.Enemy:
                    if (mSceneProp.DeathMatch)
                    {
                        return src != target;
                    }
                    return src.Force != target.Force;
                case SkillTemplate.CastTarget.PetForMaster:
                    if (src is InstancePet)
                    {
                        return (src as InstancePet).Master == target;
                    }
                    return false;
                case SkillTemplate.CastTarget.Alias:
                    return (src != target) && (src.Force == target.Force);

                case SkillTemplate.CastTarget.AlliesIncludeSelf:
                    return (src.Force == target.Force);
                case SkillTemplate.CastTarget.AlliesExcludeSelf:
                    return (src != target) && (src.Force == target.Force);

                case SkillTemplate.CastTarget.EveryOne:
                    return true;
                case SkillTemplate.CastTarget.EveryOneExcludeSelf:
                    return (src != target);

                case SkillTemplate.CastTarget.Self:
                    return src == target;
                case SkillTemplate.CastTarget.NA:
                default:
                    return false;
            }
        }
    }
}
