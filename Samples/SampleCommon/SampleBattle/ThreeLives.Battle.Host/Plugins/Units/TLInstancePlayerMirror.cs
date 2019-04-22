using System.Collections.Generic;
using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.Helper;
using DeepCore.GameHost.Instance;
using DeepCore.Vector;
using TLBattle.Common.Data;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Server.Plugins.Units
{
    partial class TLInstancePlayerMirror : TLInstanceMonster
    {
        private uint lastAtkUnitID = 0;
        //索敌范围.
        private int mGuardSearchRange = TLBattle.Plugins.TLEditorConfig.Instance.NORMALSCENE_AUTO_GUARD_RANGE;//走配置.
        public enum GuardMode
        {
            Point,
        }

        /// <summary>
        /// 自动战斗模式.
        /// </summary>
        private GuardMode CurGuardMode = GuardMode.Point;
        public TLInstancePlayerMirror(InstanceZone zone, AddUnit add) : base(zone, add)
        {
            if ((this.Info.Properties as TLUnitProperties).ServerData.BaseInfo.PlayerMirrorType == TLUnitBaseInfo.MirrorType.Monster)
            {
                mGuardSearchRange = (int)this.Info.GuardRange;
            }

            this.Info.GuardRangeLimit = 0;
        }


        protected override void onUpdateAI()
        {
            updateTracingTarget();
            updateGuard();
            updateCurrentSkill();
            updateRunningPath();
            updateHate();
            updateView();
            
        }
        

        protected virtual void updateCurrentSkill()
        {
            if (CurrentState is StateSkill)
            {
                var current = CurrentState as StateSkill;

                if ((mTracingTarget != null))
                {
                    tryLaunchRandomSkillAndCancelCurrentSkill(mTracingTarget.TargetUnit, true);
                }
                
            }
        }
        private void SetLastAtkUnitID(uint id)
        {
            if (lastAtkUnitID != id)
            {
                lastAtkUnitID = id;
            }
        }

        public override SkillState getRandomLaunchableSkill()
        {
            return base.getRandomLaunchableSkill();
        }

        public override SkillState getRandomLaunchableExpectSkill(InstanceUnit target, SkillTemplate.CastTarget expectTarget,
            AttackReason reason = AttackReason.Tracing, bool checkRange = false)
        {
            return base.getRandomLaunchableExpectSkill(target, expectTarget, reason, checkRange);
        }

        public override SkillState getRandomLaunchableExpectSkill(SkillTemplate.CastTarget expectTarget)
        {
            return base.getRandomLaunchableExpectSkill(expectTarget);
        }

        private void SetLastAtkUnitID(uint id, SkillTemplate.CastTarget expect)
        {
            if (id != lastAtkUnitID)
            {
                lastAtkUnitID = id;
                //if (lastAtkUnitID != 0 && lastAtkUnitID != this.ID)
                //{
                //    queueEvent(new PlayerFocuseTargetEvent(this.ID, lastAtkUnitID, expect));
                //}
            }
        }
        /// <summary>
        /// 是否允许自动施放.
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public bool AllowLaunchSkillTest(InstanceUnit.SkillState ss)
        {
            var gs = (this.Virtual as TLVirtual_PlayerMirror).SkillModule.GetGameSkill(ss.ID);
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

        
        /// <summary>
        /// 获得当前能够使用的技能.
        /// </summary>
        /// <returns></returns>
        protected SkillState GetAvailableAutoLaunchSkill(InstanceUnit oldTarget, out InstanceUnit newTarget)
        {
            SkillState ret = null;

            //新的攻击目标.
            InstanceUnit nt = null;
            newTarget = oldTarget;

            //没蓝直接转普攻.
            if (/*this.CurrentMP > 0 &&*/ mTracingTarget != null)
            {
                List<SkillState> list = SkillStatus as List<SkillState>;
                //优先技能.
                for (int si = list.Count - 1; si >= 0; --si)
                {
                    SkillState sst = list[si];

                    if (DefaultSkill != null && sst.Data.ID != DefaultSkill.ID)
                    {
                        if (!sst.IsCD && sst.IsActive && sst.IsDone && AllowLaunchSkillTest(sst))
                        {
                            //切换作用不同的技能，这时需要切换技能目标，会导致丢失原目标.
                            if (mTracingTarget.ExpectTarget != sst.Data.ExpectTarget)
                            {
                                nt = findGuardTarget(sst.Data.ExpectTarget, AttackReason.Look);

                                //有目标且在施法范围内.
                                if (nt != null)
                                {
                                    if (sst.Data.ExpectTarget != SkillTemplate.CastTarget.Enemy && !IsTargetInSkillRange2(sst.Data, nt))
                                    {
                                        ret = null;
                                        continue;
                                    }
                                    newTarget = nt;
                                }
                            }
                            ret = sst;
                            break;
                        }
                    }
                }
            }

            //没有技能时施放普攻.
            if (ret == null && DefaultSkill != null && oldTarget != null)
            {
                var t = getSkillState(DefaultSkill.ID);
                if (t != null && IsTargetInSkillRange2(t.Data, oldTarget))
                {
                    ret = t;
                }
            }

            return ret;
        }
        
        /// <summary>
        /// 判断当前目标在攻击范围内
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool IsTargetInSkillRange2(SkillTemplate skill, InstanceUnit unit)
        {
            if (skill == null || unit == null) { return false; }

            float rg = GetSkillAttackRange(skill);
            float dr = skill.AttackAngle / 2;
            if (Collider.Object_BlockBody_TouchRound(unit, X, Y, rg))
            {
                return true;
            }
            return false;
        }
        protected  void updateGuard()
        {
            bool idle = CurrentState is StateIdle;
            
            if (mTracingTarget == null || !mTracingTarget.IsActive || idle)
            {
                mTracingTarget = null;
                InstanceUnit enemy = null;

                enemy = this.Parent.getObject<InstanceUnit>(lastAtkUnitID);

                if (enemy != null)
                {
                    //超出攻击距离.
                    if (enemy.IsActive == false || CMath.getDistance(this.X, this.Y, enemy.X, enemy.Y) > mGuardSearchRange)
                    {
                        SetLastAtkUnitID(0);
                        enemy = findGuardTarget();
                    }
                }
                else
                {
                    SetLastAtkUnitID(0);
                    enemy = findGuardTarget();
                }

                if (enemy != null)
                {
                    doFocusAttack(enemy);
                    return;
                }
            }
            if (CurrentState is StateSkill)
            {
                var state = CurrentState as StateSkill;
                if (state.TargetUnit != null && state.TargetUnit.IsDead)
                {
                    var sp = (state.SkillData.Properties) as TLSkillProperties;
                    if (sp.StopSkillOnUnitDead)
                    {
                        state.block();
                        doSomething();
                    }
                }
            }

            // 旋风斩贴近目标 //
            if (mTracingTarget != null && CurrentState is StateSkill)
            {
                var state = CurrentState as StateSkill;
                var d = MathVector.getDistance(this.X, this.Y, mTracingTarget.Target.X, mTracingTarget.Target.Y) -
                        mTracingTarget.TargetUnit.BodyHitSize;
                if (d > state.SkillData.AttackRange) //超出范围靠近.
                {
                    state.controlMoveTo(mTracingTarget.Target.X, mTracingTarget.Target.Y);
                }
                else
                {
                    state.controlFaceTo(mTracingTarget.Target.X, mTracingTarget.Target.Y);
                }
            }

            // 没目标定期检测目标 //
            if (mTracingTarget == null)
            {
                var enemy = findGuardTarget(SkillTemplate.CastTarget.Enemy, AttackReason.Look);
                if (enemy != null)
                {
                    doFocusAttack(enemy, SkillTemplate.CastTarget.Enemy);
                    return;
                }

                var alias = findGuardTarget(SkillTemplate.CastTarget.AlliesIncludeSelf, AttackReason.Look);
                if (alias != null)
                {
                    doFocusAttack(alias, SkillTemplate.CastTarget.AlliesIncludeSelf);
                    return;
                }
            }
        }
        public void doFocusAttack(InstanceUnit target, SkillTemplate.CastTarget expect_target = SkillTemplate.CastTarget.Enemy)
        {
            if (Parent.IsAttackable(this, target, expect_target, AttackReason.Tracing, Info))
            {
                if (mTracingTarget != null)
                {
                    if (mTracingTarget.TargetUnit != target)
                    {
                        mTracingTarget = new StateFollowAndAttack(this, target, expect_target, true);
                        queueEvent(new PlayerFocuseTargetEvent(this.ID, target.ID, expect_target));
                        changeState(mTracingTarget);
                    }
                }
                else
                {
                    mTracingTarget = new StateFollowAndAttack(this, target, expect_target, true);
                    queueEvent(new PlayerFocuseTargetEvent(this.ID, target.ID, expect_target));
                    changeState(mTracingTarget);
                }
            }
            else
            {
                mTracingTarget = null;
            }
            if (mTracingTarget != null) SetLastAtkUnitID(target.ID);
            else SetLastAtkUnitID(0);
        }
        
        public override StateSkill launchRandomSkill(SkillTemplate.CastTarget expectTarget, LaunchSkillParam param)
        {
            //找能放的技能.
            InstanceUnit newTarget = null;
            var ss = GetAvailableAutoLaunchSkill(null, out newTarget);

            if (ss == null) return null;

            if (newTarget != null && newTarget.ID != param.TargetUnitID)
            {
                SetLastAtkUnitID(newTarget.ID, ss.Data.ExpectTarget);
                return this.launchSkill(ss.Data.ID, new LaunchSkillParam(newTarget.ID, null, param.AutoFocusNearTarget));
            }
            else
            {
                SetLastAtkUnitID(param.TargetUnitID, ss.Data.ExpectTarget);
                return this.launchSkill(ss.Data.ID, param);
            }
        }

        protected override void updateTracingTarget()
        {
            if (mTracingTarget != null)
            {
                if (Parent.IsAttackable(this, mTracingTarget.TargetUnit, SkillTemplate.CastTarget.Enemy, AttackReason.Tracing, Info))
                {
                    //有攻击目标//
                    if ((CurrentState is StateSkill))
                    {
                        tryLaunchRandomSkillAndCancelCurrentSkill(TracingTarget, true);
                    }
                    else if (CurrentState is StateIdle)
                    {
                        tryMoveScatterTarget(TracingTarget);
                    }
                }
                else
                {
                    mHateSystem.Remove(mTracingTarget.TargetUnit);
                    if (mTracingTarget == CurrentState)
                    {
                        doSomething();
                    }
                    mTracingTarget = null;
                }
            }
        }

        public override bool tryLaunchRandomSkillAndCancelCurrentSkill(InstanceUnit target, bool autoFocusNearTarget = false)
        {
            if (target is InstanceBuilding)//攻击单位建筑物只使用普攻.
            {
                return false;
            }
            //只允许普通攻击打断.
            StateSkill current = CurrentState as StateSkill;

            if (DefaultSkill != null && current != null && current.SkillData.ID == DefaultSkill.ID)
            {
                if (!current.IsChanting && current.IsCancelableBySkill)
                {
                    //找能放的技能.
                    InstanceUnit newTarget = null;
                    var ss = GetAvailableAutoLaunchSkill(target, out newTarget);

                    //记录上次攻击的目标.
                    if (ss != null)
                    {
                        SetLastAtkUnitID(newTarget.ID, ss.Data.ExpectTarget);
                        LaunchSkillParam param = new LaunchSkillParam(newTarget.ID, null, autoFocusNearTarget);
                        var st = this.launchSkill(ss.Data.ID, param);
                        if (st != null)
                            return true;
                    }
                }
            }

            return false;
        }

        private TVector2 GetGuardPos()
        {
           return new TVector2(this.X, this.Y);
        }
        
        public ListObjectPool<InstanceUnit>.AutoRelease findGuardTargets(SkillTemplate.CastTarget expect, AttackReason reason)
        {
            var units = ListObjectPool<InstanceUnit>.AllocAutoRelease();
            var gpos = GetGuardPos();
            Parent.getObjectsRoundRange<InstanceUnit>((u, x, y, r) =>
            {
                if (Collider.Object_Pos_IncludeInRound(u, x, y, r))
                {
                    return Parent.IsAttackable(this, u as InstanceUnit, expect, reason, Info);
                }
                return false;
            }, gpos.x, gpos.y, mGuardSearchRange, units);
            return units;
        }
        
        
        
        public  InstanceUnit findGuardTarget(SkillTemplate.CastTarget expect = SkillTemplate.CastTarget.Enemy, AttackReason reason = AttackReason.Look)
        {
            InstanceUnit min = null;
            if (getAvailableSkill(expect) != null)
            {
                    //攻击过自己的单位.
                    min = GetGuardAttackedUnit(expect, reason);
                    if (min != null) return min;

                    using (var units = findGuardTargets(expect, reason))
                    {
                        //搜索逻辑(距离>血量
                        min = GetGuardDefaultUnit(units);
                        if (min != null) return min;
                    }
                
            }
            return min;
        }
        
        private InstanceUnit CompareGuardUnit(ref float min_len, float len, InstanceUnit u, InstanceUnit min)
        {
            if (min == null)
            {
                return u;
            }
            else
            {
                //优先过滤建筑物.
                if (min is InstanceBuilding && !(u is InstanceBuilding))
                {
                    return u;
                }
                if (!(min is InstanceBuilding) && (u is InstanceBuilding))
                {
                    return min;
                }

                if (min_len > len) //距离最近.
                {
                    min_len = len;
                    min = u;
                }
                else if (min_len == len)//距离最近取血量最少.
                {
                    if (min != null && u.CurrentHP < min.CurrentHP)
                    {
                        min = u;
                    }
                }
            }
            return min;
        }
        /// <summary>
        /// 默认距离，血量逻辑.
        /// </summary>
        /// <param name="expect"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        private InstanceUnit GetGuardDefaultUnit(List<InstanceUnit> units)
        {
            InstanceUnit min = null;
            float min_len = float.MaxValue;
            float len;
            foreach (var u in units)
            {
                len = MathVector.getDistanceSquare(u.X, u.Y, this.X, this.Y);
                min = CompareGuardUnit(ref min_len, len, u, min);
            }
            return min;
        }
        /// <summary>
        /// 优先搜索攻击自己的单位.
        /// </summary>
        /// <param name="expect"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        private InstanceUnit GetGuardAttackedUnit(SkillTemplate.CastTarget expect, AttackReason reason)
        {
            InstanceUnit min = null;
            float min_len = float.MaxValue;
            float len;
            TLHateSystem hs = (this.Virtual as TLVirtual_PlayerMirror).GetHateSystem() as TLHateSystem;
            if (hs != null)
            {
                var gpos = GetGuardPos();
                hs.ForEachHateList((InstanceUnit u, ref bool cancel) =>
                {
                    //可攻击.
                    if (Parent.IsAttackable(this, u, expect, reason, Info))
                    {
                        len = MathVector.getDistanceSquare(u.X, u.Y, gpos.x, gpos.y);
                        if (len < mGuardSearchRange * mGuardSearchRange)
                        {
                            min = CompareGuardUnit(ref min_len, len, u, min);
                        }
                    }
                });
            }
            return min;
        }
//        public override bool followAndAttack(InstanceUnit src, AttackReason reason)
//        {
//            if (IsNoneSkill) return false;
//            if ((src != null))
//            {
//                if (Parent.IsAttackable(this, src, SkillTemplate.CastTarget.Enemy, reason, Info))
//                {
//                    this.SetEnableView(false);
//                    mHateSystem.Add(src);
//                    if (TracingTarget != src)
//                    {
//                        mTracingTarget = new StateFollowAndAttack(this, src);
//                    }
//                    changeState(mTracingTarget);
//                    return true;
//                }
//                else
//                {
//                    mHateSystem.Remove(src);
//                }
//            }
//            return false;
//        }
    }
}
