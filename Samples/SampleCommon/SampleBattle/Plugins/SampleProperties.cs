using DeepCore.GameData;
using DeepCore.IO;
using DeepCore.Reflection;

namespace SampleBattle.Plugins
{
    [MessageType(0x000B0001)]
    [DescAttribute("单位战斗属性")]
    [ExpandableAttribute]
    public class SampleUnitProperties : IUnitProperties
    {
        [DescAttribute("Test", "Test")]
        public int Test = 0;

        public SampleUnitProperties()
        {

        }
        public override string ToString()
        {
            return "Sample 单位扩展属性";
        }
        public object Clone()
        {
            SampleUnitProperties ret = new SampleUnitProperties();
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(Test);
        }
        public void ReadExternal(IInputStream input)
        {
            Test = input.GetS32();
        }
    }

    [MessageType(0x000B0002)]
    [DescAttribute("攻击属性")]
    [ExpandableAttribute]
    public class SampleAttackProperties : IAttackProperties
    {
        [DescAttribute("技能ID（通过ID获取技能等级）", "技能ID")]
        public int SkillTemplateID = 0;
        [DescAttribute("伤害百分比公式ID", "数值公式ID")]
        public int DamagePerID = 0;
        [DescAttribute("伤害绝对值公式ID", "数值公式ID")]
        public int DamageModiferID = 0;

        public SampleAttackProperties()
        {

        }
        public override string ToString()
        {
            return "Sample 攻击扩展属性";
        }
        public object Clone()
        {
            SampleAttackProperties ret = new SampleAttackProperties();

            ret.SkillTemplateID = this.SkillTemplateID;
            ret.DamagePerID = this.DamagePerID;
            ret.DamageModiferID = this.DamageModiferID;

            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutS32(SkillTemplateID);
            output.PutS32(DamagePerID);
            output.PutS32(DamageModiferID);
        }
        public void ReadExternal(IInputStream input)
        {
            SkillTemplateID = input.GetS32();
            DamagePerID = input.GetS32();
            DamageModiferID = input.GetS32();
        }
    }

    [MessageType(0x000B0003)]
    [DescAttribute("BUFF属性")]
    [ExpandableAttribute]
    public class SampleBuffProperties : IBuffProperties
    {
        public SampleBuffProperties()
        {

        }
        public override string ToString()
        {
            return "Sample BUFF扩展属性";
        }
        public object Clone()
        {
            SampleBuffProperties prop = new SampleBuffProperties();


            return prop;
        }
        public void WriteExternal(IOutputStream output)
        {

        }

        public void ReadExternal(IInputStream input)
        {

        }
    }

    [MessageType(0x000B0004)]
    [DescAttribute("道具属性")]
    [ExpandableAttribute]
    public class SampleItemProperties : IItemProperties
    {
        public SampleItemProperties()
        {

        }
        public override string ToString()
        {
            return "Sample 道具扩展属性";
        }
        public object Clone()
        {
            SampleItemProperties ret = new SampleItemProperties();
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
        }
        public void ReadExternal(IInputStream input)
        {
        }
    }

    [MessageType(0x000B0005)]
    [DescAttribute("技能属性")]
    [ExpandableAttribute]
    public class SampleSkillProperties : ISkillProperties
    {
        public SampleSkillProperties() { }
        public override string ToString()
        {
            return "Sample 技能扩展属性";
        }
        public object Clone()
        {
            SampleSkillProperties ret = new SampleSkillProperties();
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
        }
        public void ReadExternal(IInputStream input)
        {
        }
    }

    [MessageType(0x000B0006)]
    [DescAttribute("法术属性")]
    [ExpandableAttribute]
    public class SampleSpellProperties : ISpellProperties
    {

        public SampleSpellProperties()
        {
        }
        public override string ToString()
        {
            return "Sample 法术扩展属性";
        }
        public object Clone()
        {
            SampleSpellProperties ret = new SampleSpellProperties();

            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {

        }
        public void ReadExternal(IInputStream input)
        {

        }
    }

    [MessageType(0x000B0008)]
    [DescAttribute("场景属性")]
    [ExpandableAttribute]
    public class SampleSceneProperties : ISceneProperties
    {
        [Desc("死亡竞赛模式")]
        public bool DeathMatch = false;

        public SampleSceneProperties()
        {
        }
        public override string ToString()
        {
            return "Sample 场景扩展属性";
        }
        public object Clone()
        {
            var ret = new SampleSceneProperties();
            ret.DeathMatch = this.DeathMatch;
            return ret;
        }
        public void WriteExternal(IOutputStream output)
        {
            output.PutBool(DeathMatch);
        }
        public void ReadExternal(IInputStream input)
        {
            this.DeathMatch = input.GetBool();
        }
    }
    

}
