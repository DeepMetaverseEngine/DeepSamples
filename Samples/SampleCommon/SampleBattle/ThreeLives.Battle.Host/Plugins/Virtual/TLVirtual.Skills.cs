using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using DeepCore.IO;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;

namespace TLBattle.Server.Plugins.Virtual
{
    /// <summary>
    public partial class TLVirtual
    {
        /// <summary>
        /// 技能状态信息.
        /// </summary>
        public class TLSkillStatusData : IExternalizable
        {
            /// <summary>
            /// 技能（技能ID，到期时间戳）.
            /// </summary>.
            public HashMap<int, long> SkillTimestampMSMap = null;

            public void ReadExternal(IInputStream input)
            {
                SkillTimestampMSMap = input.GetMap<int, long>(input.GetS32, input.GetS64);
            }

            public void WriteExternal(IOutputStream output)
            {
                output.PutMap(SkillTimestampMSMap, output.PutS32, output.PutS64);
            }
        }

        /// <summary>
        /// 拦截spell.
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        internal bool TryLaunchSpell(ref SpellTemplate spell)
        {
            //    DispatchTrySendSpellEvent(ref spell);
            return true;
        }

        /// <summary>
        /// 拦截buff.
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        internal bool TrySendBuff(AddBuff add)
        {
            //判断单位是否无敌.
            //todo.
            if (add != null && add.template != null)
            {
                var v = add.template.Properties as TLBuffProperties;

                if (v.buffData != null && v.buffData.Count > 0)
                {
                    List<UnitBuff> list = new List<UnitBuff>();
                    UnitBuff ub = null;
                    for (int index = 0; index < v.buffData.Count; index++)
                    {
                        ub = SkillHelper.Instance.GetUnitBuff(v.buffData[index]);

                        if (ub != null)
                        {
                            ub.BindTemplate(add.template);
                            list.Add(ub);
                        }
                    }

                    add.tag = list;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断是否可放技能.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        /// 
        internal virtual bool TryLaunchSkill(InstanceUnit.SkillState skill, ref InstanceUnit.LaunchSkillParam param)
        {
            if (mUnit.IsDead) return false;

            var skdata = skill.Data.Properties as TLSkillProperties;
            //需要目标的单位，如果死亡，则该技能停止释放.
            if (skdata.TargetType == TLSkillProperties.TLSkillTargetType.NeedTarget)
            {
                InstanceUnit t = mUnit.Parent.getUnit(param.TargetUnitID);
                if (t == null || t.IsDead)
                {
                    return false;
                }
            }

            //沉默状态不能施放技能.
            if (this.mUnit.IsSilent &&
                this.mUnit.DefaultSkill != null &&
                skill.ID != this.mUnit.DefaultSkill.GetID())
            {
                return false;
            }

            bool ret = true;

            DispatchTryLaunchSkillEvent(ref skill, ref ret, ref param);

            return ret;
        }

        /// <summary>
        /// 拦截召唤.
        /// </summary>
        /// <param name="owner_prop"></param>
        /// <param name="summon"></param>
        /// <param name="summonUnit"></param>
        /// <returns></returns>
        internal bool TrySummonUnit(TLVirtual owner_prop, SummonUnit summon, ref UnitInfo summonUnit)
        {
            return true;
        }

        /// <summary>
        /// 攻击判定逻辑.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        internal virtual int OnHit(TLVirtual attacker, AttackSource source)
        {
            AtkAppendData data = new AtkAppendData();



            int damage = 0;

            damage = this.On_hit_unit_vs_unit(attacker, this, source, ref data);
            bool isHarmful = HitIsHarmful(attacker, source, damage);
            if (damage < 0)
            {
                //加血.
                OnHeal(attacker, damage, source, ref data);
            }
            else if (damage > 0)
            {
                //伤害.
                attacker.OnHitOther();
            }

            if (damage == 0)//伤害为0不产生受击.
            {
                source.OutIsDamage = false;
            }

            if (data.HitInfo != null)
            {
                this.SendBattleSplitHitEventB2C(data.HitInfo, data.HitInfoTotalDamage, attacker.mUnit.ID, source.Attack);
            }

            //改变战斗状态.
            attacker.ChangeCombatStateOnHitOther(this, isHarmful);
            this.ChangeCombatStateFromAtk(attacker, isHarmful);

            return damage;
        }

        private bool HitIsHarmful(TLVirtual attacker, AttackSource source, int damage)
        {
            if (damage > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 是否允许自动施放.
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public bool AllowLaunchSkillTest(InstanceUnit.SkillState ss)
        {
            var gs = SkillModule.GetGameSkill(ss.ID);
            if (gs.SkillType == GameSkill.TLSkillType.hideActive)
            {
                return false;
            }

            if (gs.SkillType == GameSkill.TLSkillType.God)
            {
                return ss.TryLaunch();
            }

            return true;
        }

        private void OnHitOther()
        {
            //命中单位回血.
            if (MirrorProp.OnHitRecoverHP > 0)
            {
                AddHP(MirrorProp.OnHitRecoverHP, this.mUnit, true);
            }
        }

        private void OnKillUnit()
        {
            //击杀单位回血.
            if (MirrorProp.KillRecoverHP > 0)
            {
                AddHP(MirrorProp.KillRecoverHP, this.mUnit, true);
            }
        }

    }
}
