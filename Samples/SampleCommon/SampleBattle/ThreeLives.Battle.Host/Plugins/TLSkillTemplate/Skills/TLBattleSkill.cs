using DeepCore.GameData.Zone;
using DeepCore.Log;
using DeepCore.Reflection;
using System;
using System.Collections.Generic;
using TLBattle.Common.Plugins;

namespace TLBattle.Server.Plugins.TLSkillTemplate.Skills
{

    public static class TLBattleSkill
    {
        public const int DEFAULT_SCRIPT_CONFIG = 10000;
        public const int DEFAULT_SCRIPT_EDITOR = 10001;
        public const int DEFAULT_SCRIPT_PARTNER = 10002;
        private static bool FinishInit = false;
        private static TemplateManager mTemplates = null;
        private static Logger log = LoggerFactory.GetLogger("TLBattleSkill");
        private static IDictionary<int, UnitSkill> UnitSkillMap = new SortedDictionary<int, UnitSkill>();
        // private static List<UnitSkill> UnitSkillAllTypes = new List<UnitSkill>();
        private static UnitSkill purAtkScript = null;

        static TLBattleSkill()
        {

        }

        public static void Init(TemplateManager mgr)
        {
            mTemplates = mgr;
            //反射并初始化所有继承Skill的类.
            LoadSkillDll();
            //加载战斗计算公式所有参数配置.
            InitBattleFormula();
        }

        private static void LoadSkillDll()
        {
            if (FinishInit) { return; }

            bool findDll = false;
            {
                //主动技能.
                try
                {
                    Type ctype = typeof(UnitSkill);
                    foreach (Type stype in ReflectionUtil.GetNoneVirtualSubTypes(ctype))
                    {

                        UnitSkill sk = ReflectionUtil.CreateInstance(stype) as UnitSkill;
                        log.Info("Regist skill : " + stype.FullName);
                        //UnitSkillAllTypes.Add(sk);


                        if (UnitSkillMap.ContainsKey(sk.SkillID))
                        {
                            throw new Exception("主动技能脚本ID重复:" + sk.SkillID);
                        }

                        UnitSkillMap.Add(sk.SkillID, sk);

                        if (sk.SkillID == 0 && purAtkScript == null)
                        {
                            purAtkScript = sk;
                            findDll = true;
                        }
                    }
                }
                catch (Exception err)
                {
                    log.Error("ActiveSkillInit Error" + err.ToString());
                    throw;
                }
            }

            try
            {
                Type ctype = typeof(SkillHelper);
                foreach (Type stype in ReflectionUtil.GetNoneVirtualSubTypes(ctype))
                {
                    SkillHelper sk = ReflectionUtil.CreateInstance(stype) as SkillHelper;
                }
            }
            catch (Exception err)
            {
            }
            /*
            //被动技能.
            try
            {
                Type type = typeof(UnitPassiveSkill);

                foreach (Type stype in ReflectionUtil.GetNoneVirtualSubTypes(type))
                {
                    UnitPassiveSkill sk = ReflectionUtil.CreateInstance(stype) as UnitPassiveSkill;
                    log.Info("Regist PassiveSkill : " + stype.FullName);

                    if (UnitPassiveSkillMap.ContainsKey(sk.SkillID))
                    {
                        throw new Exception("被动技能脚本ID重复:" + sk.SkillID);
                    }

                    UnitPassiveSkillMap.Add(sk.SkillID, stype);

                    sk.InitSkillParam();
                }
            }
            catch (Exception err)
            {
                log.Error("LoadPassiveSkillDll" + err.ToString());
                throw;
            }
            */
            FinishInit = true;

            if (findDll == false)
            {
                throw new Exception("can not find Skill Plugin DLL");
            }
        }

        private static void InitBattleFormula()
        {
            var d = TLBattle.Plugins.TLDataMgr.GetInstance().BattleFormulaData.GetData();
            ThreeLives.Battle.Host.Plugins.TLSkillTemplate.Skills.TLBattleFormula.Init(d);
        }

        #region 获取模板数据.

        public static SkillTemplate GetSkillTemplate(int skillTemplateID, bool isClone)
        {
            SkillTemplate template = mTemplates.GetSkill(skillTemplateID);

            if (isClone == true && template != null)
            {
                template = template.Clone() as SkillTemplate;
            }

            return template;
        }

        public static SpellTemplate GetSpellTemplate(int spellTemplateID)
        {
            SpellTemplate template = mTemplates.GetSpell(spellTemplateID);
            if (template != null)
            {
                template = template.Clone() as SpellTemplate;
            }
            return template;
        }

        public static BuffTemplate GetBuffTemplate(int templateID, bool isClone)
        {
            BuffTemplate template = mTemplates.GetBuff(templateID);
            if (template != null && isClone)
            {
                template = template.Clone() as BuffTemplate;
            }
            return template;
        }

        public static UnitInfo GetUnitInfo(int templateID)
        {
            return mTemplates.GetUnit(templateID);
        }

        #endregion

        #region 获取TLSkill.

        public static UnitSkill GetUnitSkill(int id, bool isClone = false)
        {
            UnitSkill ret = null;
            UnitSkillMap.TryGetValue(id, out ret);

            if (isClone && ret != null)
            {
                var type = ret.GetType();
                UnitSkill sk = ReflectionUtil.CreateInstance(type) as UnitSkill;
                ret = sk;
            }

            return ret;
        }

        public static UnitSkill GetPurAtkScript()
        {
            return purAtkScript;
        }

        public static UnitSkill GetGodSkillScript()
        {
            UnitSkill ret = null;
            UnitSkillMap.TryGetValue(DEFAULT_SCRIPT_PARTNER, out ret);
            if (ret != null)
            {
                var type = ret.GetType();
                UnitSkill sk = ReflectionUtil.CreateInstance(type) as UnitSkill;
                ret = sk;
            }

            return ret;
        }

        #endregion
    }


    public class SkillHelper
    {
        private static SkillHelper mInstance = null;

        public static SkillHelper Instance
        {
            get { return mInstance; }
        }

        public SkillHelper()
        {
            mInstance = this;
        }

        public virtual UnitBuff GetUnitBuff(TLBuffData bd)
        {
            return null;
        }
    }

    #region 技能描述字段.

    //SkillTypeAttribute("狂战", "雕文技能")
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldConfigurable : System.Attribute
    {
        public readonly string Desc;
        public FieldConfigurable(string desc)
        {
            this.Desc = desc;
        }
    }

    //【顺势】毁灭打击伤害的X%伤害.
    [AttributeUsage(AttributeTargets.Class)]
    public class SkillTypeAttribute : System.Attribute
    {
        public readonly string Desc;
        public readonly string Category;
        public SkillTypeAttribute(string desc, string catgory)
        {
            this.Desc = desc;
            this.Category = catgory;
        }
    }

    #endregion


}
