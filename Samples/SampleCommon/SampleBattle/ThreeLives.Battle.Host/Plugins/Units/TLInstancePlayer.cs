using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.Helper;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor;
using DeepCore.Vector;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using TLBattle.Server.Plugins.Virtual;
using TLBattle.Server.Scene;

namespace TLBattle.Server.Plugins.Units
{
    public class TLInstancePlayer : InstancePlayer
    {
        public enum GuardMode
        {
            Point,
            Map,
        }

        /// <summary>
        /// 自动战斗模式.
        /// </summary>
        private GuardMode CurGuardMode = GuardMode.Point;
        /// <summary>
        /// 挂机起始点.
        /// </summary>
        private Vector2 GuardPos = new Vector2();
        /// <summary>
        /// 自动战斗指定攻击怪物.
        /// </summary>
        private int GuardMonsterID = 0;

        //索敌范围.
        private int mGuardSearchRange = TLBattle.Plugins.TLEditorConfig.Instance.NORMALSCENE_AUTO_GUARD_RANGE;//走配置.

        //拾取距离.
        private int pickRange = 8;

        private bool checkGuard = false;

        private uint lastAtkUnitID = 0;

        public TLInstancePlayer(InstanceZone zone, AddUnit add) : base(zone, add)
        {
            this.OnGotInstanceItem += TLInstancePlayer_OnGotInstanceItem;
            //this.OnObjectAdded += TLInstancePlayer_OnObjectAdded;
            //玩法复活时间默认为最长,防止被移除.
            this.Info.RebirthTimeMS = int.MaxValue;
        }


        //         private void TLInstancePlayer_OnObjectAdded(InstanceZoneObject obj)
        //         {
        //             if (FollowTargetTemplateID != 0 && FollowNpcTarget == null)
        //             {
        //                 if (obj is InstanceUnit)
        //                 {
        //                     InstanceUnit unit = obj as InstanceUnit;
        //                     FollowNpcTarget = isFindTarget(unit);
        //                 }
        //             }
        //         }

        private void TLInstancePlayer_OnGotInstanceItem(InstanceUnit obj, InstanceItem item)
        {
            //拾取游戏服道具时，需要判断对背包格子数进行改变.
            TLBattle.Common.Data.TLDropItem di = item.GenSyncItemInfo(false).ExtData as TLBattle.Common.Data.TLDropItem;

            //拾取非虚拟道具时，背包预先改变.
            if (di != null && di.VirtualItem == false)
            {
                this.InventorySizePreChange();
            }
        }

        protected override void doPickObject(UnitPickObjectAction pick)
        {
            this.VirtualPlayer.TakeOffMount();
            base.doPickObject(pick);
        }

        protected override void onStateChanged(State old_state, State state)
        {
            if (state is StatePickObject)
            {
                this.VirtualPlayer.TakeOffMount();
            }
            base.onStateChanged(old_state, state);
        }


        public override void OnReconnected(AddUnit add)
        {
            (this.Virtual as TLVirtual_Player).OnUnitReEnter(add);
            base.OnReconnected(add);
            if (this.Parent != null)
                (this.Parent as TLEditorScene).playerReconnected(this);
        }

        protected override void onUpdateRecover()
        {
            //do nothing.
        }

        public override void InitSkills(LaunchSkill baseSkill, LaunchSkill[] skills)
        {
            if (this.Virtual != null && (this.Virtual as TLVirtual).IsFinishModuleInit())
            {
                base.InitSkills(baseSkill, skills);
            }
        }

        protected override void updateGuard()
        {
            if (IsDead) return;

            bool idle = false;
            if (CurrentState is StatePlayerUpdateMove s)
            {
                idle = s.IsIdle;
            }
            else if (CurrentState is StateIdle)
            {
                idle = true;
            }

            if (IsGuard == true && (mFocusTarget == null || !mFocusTarget.IsActive || idle))
            {
                mFocusTarget = null;
                InstanceUnit enemy = null;

                enemy = this.Parent.getObject<InstanceUnit>(lastAtkUnitID);

                if (enemy != null)
                {
                    //超出攻击距离.
                    if (enemy.IsActive == false || CMath.getDistance(this.X, this.Y, enemy.X, enemy.Y) > mGuardSearchRange)
                    {
                        enemy = findGuardTarget(SkillTemplate.CastTarget.Enemy, AttackReason.Look);
                    }
                }
                else
                {
                    enemy = findGuardTarget(SkillTemplate.CastTarget.Enemy, AttackReason.Look);
                }

                if (enemy != null)
                {
                    doFocusAttack(enemy, SkillTemplate.CastTarget.Enemy);
                    return;
                }
            }
            if (mFocusPickItem != null && !mFocusPickItem.IsActive)
            {
                mFocusPickItem = null;
            }

            checkGuard = mCheckGuard.Update(Parent.UpdateIntervalMS);

            //自动拾取.
            if (checkGuard)
            {
                TryDirectPickNearItems();
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

            if (IsGuard)
            {
                // 旋风斩贴近目标 //
                if (mFocusTarget != null && CurrentState is StateSkill)
                {
                    var state = CurrentState as StateSkill;
                    var d = MathVector.getDistance(this.X, this.Y, mFocusTarget.Target.X, mFocusTarget.Target.Y) - mFocusTarget.TargetUnit.BodyHitSize;
                    if (d > state.SkillData.AttackRange)//超出范围靠近.
                    {
                        state.controlMoveTo(mFocusTarget.Target.X, mFocusTarget.Target.Y);
                    }
                    else
                    {
                        state.controlFaceTo(mFocusTarget.Target.X, mFocusTarget.Target.Y);
                    }

                }
                // 没目标定期检测目标 //
                if ((mFocusTarget == null) && (mFocusPickItem == null))
                {
                    if (checkGuard)
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
            }
        }

        #region 自动拾取.

        /// <summary>
        /// 背包尺寸验证.
        /// </summary>
        /// <returns></returns>
        public bool VerifyInventory()
        {
            if (this.Virtual == null) { return false; }
            return (this.Virtual as TLVirtual_Player).VerifyInventory();
        }

        /// <summary>
        /// 背包格子预改变.
        /// </summary>
        /// <returns></returns>
        public uint InventorySizePreChange()
        {
            if (this.Virtual != null)
            {
                return (this.Virtual as TLVirtual_Player).InventorySizePreChange();
            }

            return 0;
        }

        /// <summary>
        /// 自动拾取.
        /// </summary>
        protected void TryDirectPickNearItems()
        {
            if (this.IsDead == true) { return; }

            using (var list = ListObjectPool<TLInstanceItem>.AllocAutoRelease())
            {
                Parent.getObjectsRoundRange(Collider.Object_Pos_IncludeInRound, this.X, this.Y, pickRange, list);
                foreach (var u in list)
                {
                    if (u.AllowAutoPick())
                        u.DirectPickItem(this);
                }
            }
        }

        public override InstanceItem findGuardPickItem()
        {
            return null;
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
        public override InstanceUnit findGuardTarget(SkillTemplate.CastTarget expect = SkillTemplate.CastTarget.Enemy, AttackReason reason = AttackReason.Look)
        {
            InstanceUnit min = null;
            if (getAvailableSkill(expect) != null)
            {
                if (GuardMonsterID != 0)
                {
                    using (var units = findGuardTargets(expect, reason))
                    {
                        //有优先目标取优先目标攻击.
                        min = GetGuardMonster(units, GuardMonsterID);
                        if (min != null) return min;

                        //攻击过自己的单位.
                        min = GetGuardAttackedUnit(expect, reason);
                        if (min != null) return min;

                        //搜索逻辑(距离>血量
                        min = GetGuardDefaultUnit(units);
                        if (min != null) return min;
                    }
                }
                else
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
            }
            return min;
        }

        protected override void onFocusTargetNoWay(StateFollowAndAttack follow)
        {
            lastAtkUnitID = 0;
        }
        protected override void doGuard(UnitGuardAction act)
        {
            if (act.guard == true && !VirtualPlayer.AllowUseAutoGuard())
            {
                return;
            }

            bool old = IsGuard;
            ParseGuardAction(act);

            base.doGuard(act);

            if (old != IsGuard)
            {
                OnGuardStatusChange();
            }
        }
        protected override void OnTransport(float x, float y)
        {
            //传送后重置自动挂机范围.
            if (IsGuard && CurGuardMode == GuardMode.Point)
            {
                GuardPos.x = x;
                GuardPos.y = y;
                cleanFocus();
            }
            base.OnTransport(x, y);
        }


        private void OnGuardStatusChange()
        {
            VirtualPlayer?.OnGuardStatusChange();
        }

        private void ParseGuardAction(UnitGuardAction act)
        {
            GuardMonsterID = 0;

            if (act.guard)
            {
                if (CurGuardMode == GuardMode.Point)
                {
                    GuardPos.x = this.X;
                    GuardPos.y = this.Y;
                }

                if (string.IsNullOrEmpty(act.reason))
                {
                    if (mFocusTarget != null && mFocusTarget.IsActive)
                    {
                        var gpos = GetGuardPos();
                        //单位是否在范围内.
                        float len = MathVector.getDistanceSquare(mFocusTarget.Target.X, mFocusTarget.Target.Y, gpos.x, gpos.y);

                        //超出范围.
                        if (len > (mGuardSearchRange * mGuardSearchRange))
                        {
                            mFocusTarget = null;
                            SetLastAtkUnitID(0);
                        }

                    }
                    return;
                }

                //是否指定攻击特定怪物.
                if (int.TryParse(act.reason, out GuardMonsterID))
                {
                    SetLastAtkUnitID(0);

                    if (GuardMonsterID != 0 &&
                        mFocusTarget != null &&
                        mFocusTarget.IsActive &&
                        mFocusTarget.TargetUnit.Info.ID != GuardMonsterID)
                    {
                        using (var units = findGuardTargets(SkillTemplate.CastTarget.Enemy, AttackReason.Tracing))
                        {
                            var unit = GetGuardMonster(units, GuardMonsterID);
                            if (unit != null)
                            {
                                mFocusTarget = null;
                            }
                        }
                    }

                }
            }
        }

        private TVector2 GetGuardPos()
        {
            switch (CurGuardMode)
            {
                case GuardMode.Map:
                    return new TVector2(this.X, this.Y);
                case GuardMode.Point:
                    return new TVector2(GuardPos.x, GuardPos.y);
                default:
                    return new TVector2(this.X, this.Y);
            }
        }

        //         private float GetGuardPosY()
        //         {
        //             switch (CurGuardMode)
        //             {
        //                 case GuardMode.Map:
        //                     return this.Y;
        //                 case GuardMode.Point:
        //                     return GuardPos.Y;
        //                 default:
        //                     return this.Y;
        //             }
        //         }

        /// <summary>
        /// 优先搜索指定怪物.
        /// </summary>
        /// <param name="monsterID"></param>
        /// <param name="expect"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        private InstanceUnit GetGuardMonster(List<InstanceUnit> units, int monsterID)
        {
            InstanceUnit min = null;
            //有优先目标取优先目标攻击.
            if (monsterID != 0)
            {
                float min_len = float.MaxValue;
                float len;
                foreach (var u in units)
                {
                    var zv = u.Virtual as TLVirtual_Monster;
                    if (zv != null && GuardMonsterID == zv.GetMonsterID())
                    {
                        len = MathVector.getDistanceSquare(u.X, u.Y, this.X, this.Y);
                        min = CompareGuardUnit(ref min_len, len, u, min);
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
            TLHateSystem hs = VirtualPlayer.GetHateSystem() as TLHateSystem;
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

        public override SkillState getRandomLaunchableExpectSkill(SkillTemplate.CastTarget expectTarget)
        {
            if (mFocusTarget != null && mFocusTarget.TargetUnit is InstanceBuilding)
            {
                return this.getSkillState(this.DefaultSkill.ID);
            }

            if (mFocusTarget != null && lastAtkUnitID != mFocusTarget.TargetUnit.ID)
            {
                SetLastAtkUnitID(mFocusTarget.TargetUnit.ID, expectTarget);
            }

            return null;
        }

        public override SkillState getRandomLaunchableExpectSkill(InstanceUnit target, SkillTemplate.CastTarget expectTarget, AttackReason reason = AttackReason.Tracing, bool checkRange = false)
        {
            if (target is InstanceBuilding)//攻击单位建筑物只使用普攻.
            {
                if (DefaultSkill != null) { return this.getSkillState(DefaultSkill.ID); }
                return null;
            }

            return base.getRandomLaunchableExpectSkill(target, expectTarget, reason, checkRange);
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
            if (/*this.CurrentMP > 0 &&*/ mFocusTarget != null)
            {
                List<SkillState> list = SkillStatus as List<SkillState>;
                //优先技能.
                for (int si = list.Count - 1; si >= 0; --si)
                {
                    SkillState sst = list[si];

                    if (DefaultSkill != null && sst.Data.ID != DefaultSkill.ID)
                    {
                        if (!sst.IsCD && sst.IsActive && sst.IsDone && this.VirtualPlayer.AllowLaunchSkillTest(sst))
                        {
                            //切换作用不同的技能，这时需要切换技能目标，会导致丢失原目标.
                            if (mFocusTarget.ExpectTarget != sst.Data.ExpectTarget)
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

        public TLVirtual_Player VirtualPlayer
        {
            get { return base.Virtual as TLVirtual_Player; }
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

        private void SetLastAtkUnitID(uint id)
        {
            if (lastAtkUnitID != id)
            {
                lastAtkUnitID = id;
            }
        }

        public override StateFollowAndAttack doFocusAttack(InstanceUnit target, SkillTemplate.CastTarget expect_target = SkillTemplate.CastTarget.Enemy)
        {
            var ret = base.doFocusAttack(target, expect_target);
            if (ret != null) SetLastAtkUnitID(target.ID);
            else SetLastAtkUnitID(0);
            return ret;
        }

        protected override void doFocusTarget(UnitFocuseTargetAction focus)
        {
            if (!IsGuard)
            {
                SetLastAtkUnitID(focus.targetUnitID);
            }

            base.doFocusTarget(focus);
        }
        #endregion

        /// <summary>
        /// 设置自动战斗索敌范围.
        /// </summary>
        /// <param name="v"></param>
        public void SetGuardSearchRange(int v)
        {
            mGuardSearchRange = v;
        }

        public void SetGuardType(GuardMode mode)
        {
            CurGuardMode = mode;
        }

        public List<QuestData> GetQuests()
        {
            return new List<QuestData>(mQuests.Values);
        }

        //         private InstanceUnit isFindTarget(InstanceUnit u)
        //         {
        //             InstanceUnit target = null;
        //             if (u.Info.TemplateID == FollowTargetTemplateID)
        //             {
        //                 if (this.AoiStatus != null && u.AoiStatus == this.AoiStatus)
        //                 {
        //                     target = u;
        //                 }
        //                 else if (this.AoiStatus == null)
        //                 {
        //                     target = u;
        //                 }
        //             }
        //             return target;
        //         }
        protected override void onUpdate()
        {
            base.onUpdate();
            // updateFollowTarget();
        }

        //         private void updateFollowTarget()
        //         {
        //             if (FollowTargetTemplateID != 0)
        //             {
        //                 if (FollowNpcTarget != null)
        //                 {
        //                     FindTargetEventB2C ftu = new FindTargetEventB2C();
        //                     ftu.TargetObjectID = FollowNpcTarget.ID;
        //                     ftu.TargetX = FollowNpcTarget.X;
        //                     ftu.TargetY = FollowNpcTarget.Y;
        //                     ftu.TemplateID = FollowTargetTemplateID;
        //                     this.queueEvent(ftu);
        //                     FollowNpcTarget = null;
        //                     FollowTargetTemplateID = 0;
        //                 }
        //             }
        // 
        //         }

        protected override void Disposing()
        {
            base.Disposing();
        }

        public void TransToStartRegion()
        {
            EditorScene es = this.Parent as EditorScene;
            ZoneRegion zr = es.GetEditStartRegion(this.Force);

            if (zr != null)
            {
                this.transport(zr.X, zr.Y);
            }
        }

        protected override void doReady(UnitReadyAction act)
        {
            VirtualPlayer.OnPlayerReady();
            base.doReady(act);
        }

        /// <summary>
        /// 动态设置阵营.
        /// </summary>
        /// <param name="force"></param>
        public void SetForce(byte force)
        {
            this.Force = force;
            var evt = new ForceChangeEventB2C() { Force = force };
            queueEvent(evt);
        }

        protected override void onAction(ObjectAction act)
        {
            base.onAction(act);
        }

        public override ActorResponse onRequest(ActorRequest req)
        {
            if (req is GetZonePlayersUUIDRequest r)
            {
                return this.VirtualPlayer.DoGetZonePlayersUUIDRequest(r);
            }
            else if (req is ActorAddSpeedUpBuffRequest)
            {
                VirtualPlayer.OnReceiveAddActorSpeedUpBuff(req as ActorAddSpeedUpBuffRequest);
                return null;
            }
            else if (req is ActorRemoveSpeedUpBuffRequest)
            {
                VirtualPlayer.OnReceiveRemoveActorSpeedUpBuff(req as ActorRemoveSpeedUpBuffRequest);
                return null;
            }
            else if (req is FindTargetUnitRequest ftu)
            {
                var request = req as FindTargetUnitRequest;
                foreach (var u in this.Parent.AllUnits)
                {
                    if (u.Info.TemplateID == ftu.TemplateId)
                    {
                        if (u != this && (!request.IgnoreAoi && Parent.IsVisibleAOI(this, u)))
                        {
                            var rsp = new FindTargetUnitResponse();
                            rsp.TargetObjectID = u.ID;
                            rsp.TargetX = u.X;
                            rsp.TargetY = u.Y;
                            rsp.TemplateID = u.TemplateID;
                            return rsp;
                        }
                    }
                }

                FindTargetUnitResponse ftup = new FindTargetUnitResponse { TemplateID = ftu.TemplateId };
                return ftup;
            }
            else if (req is FindTargetItemRequest fti)
            {
                foreach (var u in this.Parent.AllItems)
                {
                    if (u.Info.TemplateID == fti.TemplateId)
                    {
                        if (Parent.IsVisibleAOI(this, u))
                        {
                            var rsp = new FindTargetItemResponse();
                            rsp.TargetObjectID = u.ID;
                            rsp.TargetX = u.X;
                            rsp.TargetY = u.Y;
                            rsp.TemplateID = u.TemplateID;
                            return rsp;
                        }
                    }
                }
                return new FindTargetItemResponse();
            }
            return base.onRequest(req);
        }

        /// <summary>
        /// 和自身交互（搓炉石）
        /// </summary>
        /// <param name="timeMS"></param>
        /// <param name="done"></param>
        /// <param name="status"></param>
        public StatePickObject StartPickProgressSelf(int timeMS,
                                                     StatePickObject.OnPickDone done,
                                                     string status = null,
                                                     bool force = false,
                                                     bool canBlock = true)
        {
            StatePickObject picking = new StatePickObject(this, this, timeMS, status, done);
            picking.Force = !canBlock;
            changeState(picking, force);
            return picking;
        }
    }
}

