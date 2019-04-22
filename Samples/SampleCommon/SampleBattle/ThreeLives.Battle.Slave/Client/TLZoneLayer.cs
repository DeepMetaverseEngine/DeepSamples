using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameData.ZoneClient;
using DeepCore.GameSlave;
using TLBattle.Common;

namespace TLBattle.Client
{
    // ---------------------------------------------------------------------------------------------

    public class TLZoneLayer : ZoneLayer
    {
        public TLZoneLayer(EditorTemplates dataroot, ILayerClient client)
            : base(dataroot, client)
        {
            /*this.IsShareTerrain =*/ 
        }

        protected override bool LoadSceneData(ClientEnterScene msg, out SceneData sdata, out ZoneInfo terrain_src)
        {
            return base.LoadSceneData(msg, out sdata, out terrain_src);
        }

        public override bool IsPickableItem(ZoneActor owner, ZoneItem item, bool no_touch = false)
        {
            var zp = item.Info.Properties as Common.Plugins.TLItemProperties;
            if (zp.AllowAutoPick == true) { return false; }
            return base.IsPickableItem(owner, item, no_touch);
        }

        public override bool IsAttackable(ZoneUnit src, ZoneUnit target, SkillTemplate.CastTarget expectTarget)
        {
            //绝对中立阵营，不能被选为攻击目标.
            if (target.HP <= 0 || target.IsActive == false)
            {
                return false;
            }

            var v = src.Virtual as TLClientVirtual;

            //中立单位.
            if ((target.Virtual as TLClientVirtual).IsNeutrality())
                return false;

            switch (expectTarget)
            {
                case SkillTemplate.CastTarget.Enemy:
                    return v.IsEnemy(target);
                case SkillTemplate.CastTarget.PetForMaster:
                    if (src.Info.UType == UnitInfo.UnitType.TYPE_PET)
                        return (src != target) && (src.Force == target.Force);
                    return false;
                case SkillTemplate.CastTarget.Alias:
                case SkillTemplate.CastTarget.AlliesExcludeSelf:
                    return (src != target) && v.IsAllies(target);
                case SkillTemplate.CastTarget.AlliesIncludeSelf:
                    return v.IsAllies(target);
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