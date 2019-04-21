using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;

namespace SampleBattle.Plugins
{
    public class SampleUnitVirtual : IVirtualUnit
    {

        public void OnInit(InstanceUnit owner)
        {

            //             var sk = owner.DefaultSkill;
            //             if (sk != null)
            //             {
            //                 sk = sk.Clone() as SkillTemplate;
            //                 foreach (var t in sk.ActionQueue)
            //                 {
            //                     t.TotalTimeMS = 5000;
            //                 }
            //                 owner.InitSkills(sk);
            //             }


            owner.OnSkillChanged += owner_OnSkillChanged;
        }

        public void OnDispose(InstanceUnit owner)
        {

        }

        void owner_OnSkillChanged(InstanceUnit obj, InstanceUnit.SkillState baseSkill, InstanceUnit.SkillState[] skills)
        {

        }
    }
}
