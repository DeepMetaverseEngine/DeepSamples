using DeepCore;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.Instance.Abilities;
using System;
using System.Collections.Generic;
using ThreeLives.Battle.Data.Data;
using TLBattle.Server.Plugins.Units;
using TLBattle.Server.Scene;
using Vector2 = DeepCore.Geometry.Vector2;

namespace TLCommonServer.Plugin.Scene
{
    /// <summary>
    /// 由玩家创建的私有位面
    /// </summary>
    public class PlayerAOI : ObjectAoiStatus
    {
        private readonly InstancePlayer m_owner;
        private readonly InstanceZone m_zone;

        public delegate void OnAOIDisposeHandle(PlayerAOI aoi, AoiExitCondition reason);

        private OnAOIDisposeHandle mOnAOIExitHandle;

        public event OnAOIDisposeHandle OnDispose
        {
            add { mOnAOIExitHandle += value; }
            remove { mOnAOIExitHandle -= value; }
        }

        /// <summary>
        /// 玩家自己
        /// </summary>
        public InstancePlayer Owner
        {
            get { return m_owner; }
        }

        public InstanceZone Zone => m_zone;

        /// <summary>
        /// 别人可以看到自己
        /// </summary>
        public bool CanSeeMe { get; set; }

        /// <summary>
        /// 自己可以看到别人
        /// </summary>
        public bool CanSeeOther { get; set; }

        /// <summary>
        /// 位面玩家不会死亡, 死亡转换成保留一点血量
        /// </summary>
        public bool PlayerDontDead { get; set; }

        /// <summary>
        /// 最高存在的玩家数量
        /// </summary>
        public int MaxEnteredPlayer { get; private set; }

        /// <summary>
        /// 进入aoi延迟. TODO
        /// </summary>
        //public int EnterDelayMS { get; set; }
        private readonly TimeTaskMS mTickTask;

        public int CurrentPlayerCount { get; private set; }

        private List<ExitCondition> mExitConds = new List<ExitCondition>(2);

        //// 任意玩家死亡
        //PlayerDead,
        //ForceAllDead,
        //// 所有者死亡
        //OwnerDead,
        //// 到期
        //TimeExpire,
        //// 范围
        //Range,
        public abstract class ExitCondition
        {
        }

        /// <summary>
        /// 玩家离开aoi条件
        /// </summary>
        public abstract class PlayerExitCondition : ExitCondition
        {
            public abstract bool Check(PlayerAOI aoi, InstancePlayer obj);
        }

        /// <summary>
        /// aoi 退出检测
        /// </summary>
        public abstract class AoiExitCondition : ExitCondition
        {
            public bool AoiExit;
            public int DelayMS;

            /// <summary>
            /// 开始检测
            /// </summary>
            public virtual void BeginCheck(PlayerAOI aoi)
            {
            }

            /// <summary>
            /// 遍历所有obj，收集信息
            /// </summary>
            /// <param name="obj"></param>
            public virtual void Collect(PlayerAOI aoi, InstanceZoneObject obj)
            {
            }

            /// <summary>
            /// 遍历完成，计算结果
            /// </summary>
            public virtual void EndCheck(PlayerAOI aoi)
            {
            }
        }

        /// <summary>
        /// aoi 所有者离开aoi
        /// </summary>
        public class AoiOwnerLeave : AoiExitCondition
        {
        }

        /// <summary>
        /// aoi 绑定单位死亡或移除
        /// </summary>
        public class AoiBindUnitLeave : AoiExitCondition
        {
        }

        /// <summary>
        /// 某阵营存活数量判断
        /// </summary>
        public class ForceAllDeadCondition : AoiExitCondition
        {
            public readonly byte Force;
            private int mAliveCount;
            private int mAliveCountLimit;

            public ForceAllDeadCondition(byte force, int limitAlive)
            {
                Force = force;
                mAliveCountLimit = limitAlive;
            }

            public override void BeginCheck(PlayerAOI aoi)
            {
                mAliveCount = 0;
            }

            public override void Collect(PlayerAOI aoi, InstanceZoneObject obj)
            {
                var unit = obj as InstanceUnit;
                if (unit != null && unit.Force == Force && !unit.IsDead)
                {
                    mAliveCount += 1;
                }
            }

            public override void EndCheck(PlayerAOI aoi)
            {
                AoiExit = mAliveCount <= mAliveCountLimit;
            }
        }


        /// <summary>
        /// 玩家死亡, 包括掉线, 切图
        /// </summary>
        public class PlayerDeadCondition : AoiExitCondition
        {
            private int mDeadCountLimit;
            private int mDeadCount;

            public PlayerDeadCondition(int deadCount)
            {
                mDeadCountLimit = deadCount;
            }

            public override void Collect(PlayerAOI aoi, InstanceZoneObject obj)
            {
                var unit = obj as InstancePlayer;
                if (unit != null && unit.IsDead)
                {
                    mDeadCount += 1;
                }
            }

            public override void EndCheck(PlayerAOI aoi)
            {
                var leaveCount = aoi.MaxEnteredPlayer - aoi.CurrentPlayerCount;
                AoiExit = leaveCount + mDeadCount >= mDeadCountLimit;
            }
        }

        public class CustomPlayerCondition : PlayerExitCondition
        {
            public delegate bool CheckHandle(PlayerAOI aoi, InstancePlayer obj);

            private CheckHandle mAct;

            public CustomPlayerCondition(CheckHandle act)
            {
                mAct = act;
            }

            public override bool Check(PlayerAOI aoi, InstancePlayer obj)
            {
                return mAct.Invoke(aoi, obj);
            }
        }

        public class RangeExitCondition : PlayerExitCondition
        {
            /// <summary>
            ///  AOI 指定范围, 超过范围和警示时间，自动出aoi
            /// </summary>
            private float RadiusSize;

            /// <summary>
            /// AOI 指定坐标, 默认为玩家坐标
            /// privatesummary>
            private Vector2 Pos;

            /// <summary>
            /// 超出范围保留aoi最大时间 ms
            /// </summary>
            private int OutKeepTimeMS;

            private Dictionary<string, TimeTaskMS> mOutDelayTask = new Dictionary<string, TimeTaskMS>(2);

            public delegate void OnObjectOutOfRangeHandle(PlayerAOI aoi, InstanceZoneObject o);

            public delegate void OnObjectEnterRangeHandle(PlayerAOI aoi, InstanceZoneObject o);


            private OnObjectOutOfRangeHandle m_OnPlayerOutOfRange;

            public event OnObjectOutOfRangeHandle OnPlayerOutOfRange
            {
                add { m_OnPlayerOutOfRange += value; }
                remove { m_OnPlayerOutOfRange -= value; }
            }

            private OnObjectEnterRangeHandle m_OnPlayerEnterRange;

            public event OnObjectEnterRangeHandle OnPlayerEnterRange
            {
                add { m_OnPlayerEnterRange += value; }
                remove { m_OnPlayerEnterRange -= value; }
            }

            private List<string> mTriggerList = new List<string>(2);

            public RangeExitCondition(Vector2 p, float r, int keepMS)
            {
                Pos = p;
                RadiusSize = r;
                OutKeepTimeMS = keepMS;
            }

            public override bool Check(PlayerAOI aoi, InstancePlayer u)
            {
                if (mTriggerList.Count > 0)
                {
                    var index = mTriggerList.FindIndex(s => u.PlayerUUID == s);
                    if (index >= 0)
                    {
                        mTriggerList.RemoveAt(index);
                        return true;
                    }
                }
                if (!CMath.includeRoundPoint(Pos.X, Pos.Y, RadiusSize, u.X, u.Y))
                {
                    if (OutKeepTimeMS <= 0)
                    {
                        return true;
                    }
                    else if (!mOutDelayTask.ContainsKey(u.PlayerUUID))
                    {
                        var handle = new TickHandler((dtask) => { mTriggerList.Add(dtask.UserData as string); });
                        var task = u.Parent.AddTimeDelayMS(OutKeepTimeMS, handle);
                        task.UserData = u.PlayerUUID;
                        mOutDelayTask.Add(u.PlayerUUID, task);
                        m_OnPlayerOutOfRange?.Invoke(aoi, u);
                    }
                }
                else
                {
                    TimeTaskMS task;
                    if (mOutDelayTask.TryGetValue(u.PlayerUUID, out task))
                    {
                        task.Dispose();
                        mOutDelayTask.Remove(u.PlayerUUID);
                        m_OnPlayerEnterRange?.Invoke(aoi, u);
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// selector返回true则退出循环
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        public void ForeachAs<T>(Predicate<T> selector) where T : InstanceZoneObject
        {
            using (var units = ListObjectPool<InstanceZoneObject>.AllocAutoRelease(Objects))
            {
                foreach (var u in units)
                {
                    if (!(u is T) || !selector((T) u)) continue;
                    break;
                }
            }
        }

        public PlayerAOI(InstancePlayer player, bool can_see_me = false, bool can_see_other = false)
        {
            this.m_owner = player;
            this.m_zone = player.Parent;
            this.CanSeeMe = can_see_me;
            this.CanSeeOther = can_see_other;
            mTickTask = m_zone.AddTimePeriodicMS(500, Check_Tick);
        }

        private WeakReference mBindUnit;

        /// <summary>
        /// 位面生成一个位面外的单位，并与位面同生共死
        /// </summary>
        public void BindUnit(int templateID, string name, byte force, float x, float y)
        {
            var temp = Zone.Templates.GetUnit(templateID);
            if (temp == null) return;
            var unit = Zone.AddUnit(new AddUnit()
            {
                info = temp,
                editor_name = name,
                force = force,
                level = 1,
                pos = new DeepCore.Vector.Vector2(x, y),
                direction = 0
            });
            if (unit != null)
            {
                //将位面所有者信息同步到客户端
                unit.SetEnvironmentVar("owner", Owner.PlayerUUID, true);
                mBindUnit = new WeakReference(unit);
            }
        }

        private readonly List<Ability> mAbilities = new List<Ability>(2);

        public void AddRegionAbilities(ZoneRegion region, List<RegionAbilityData> rds)
        {
            foreach (var abilityData in rds)
            {
                if (abilityData is SpawnUnitAbilityData)
                {
                    var tg = region.AddAbility(abilityData) as SpawnUnitAbility;
                    tg.OnSpawnUnit += (r, t, u) => { u?.setAoiStatus(this); };
                    mAbilities.Add(tg);
                }
                else if (abilityData is SpawnItemAbilityData)
                {
                    var tg = region.AddAbility(abilityData) as SpawnItemAbility;
                    tg.OnSpawnUnit += (r, t, u) => { u?.setAoiStatus(this); };
                    mAbilities.Add(tg);
                }
                else if (abilityData is UnitTransportAbilityData)
                {
                    var tg = region.AddAbility(abilityData) as TransportUnitAbility;
                    tg.OnSelect += (zoneRegion, unit) => unit.AoiStatus == this;
                    mAbilities.Add(tg);
                }
                else if (abilityData is TLUnitTransportData)
                {
                    var tg = new TLTransportTrigger(abilityData as TLUnitTransportData, Zone, abilityData.Name);
                    tg.bindToRegion(region);
                    tg.OnSelect += (zoneRegion, unit) => unit.AoiStatus == this;
                    mAbilities.Add(tg);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public int GetSpawnAliveCount(ZoneRegion region)
        {
            int ret = 0;
            foreach (var ability in mAbilities)
            {
                if (ability is AbstractSpawnAbility)
                {
                    var trigger = ability as AbstractSpawnAbility;
                    if (trigger.BindingRegion == region)
                    {
                        ret += trigger.AliveCount;
                    }
                }
            }
            return ret;
        }


        public void AddExitCondition(ExitCondition data)
        {
            mExitConds.Add(data);
        }

        private void Check_Tick(TimeTaskMS task)
        {
            if (Owner.AoiStatus != this)
            {
                //aoi所有者离开
                SetAoiExit(new AoiOwnerLeave());
                return;
            }

            if (mBindUnit != null)
            {
                if (!mBindUnit.IsAlive || (mBindUnit.Target as InstanceUnit).IsDisposed)
                {
                    SetAoiExit(new AoiBindUnitLeave());
                    return;
                }
            }
            foreach (var cond in mExitConds)
            {
                (cond as AoiExitCondition)?.BeginCheck(this);
            }

            using (var units = ListObjectPool<InstanceZoneObject>.AllocAutoRelease(Objects))
            {
                foreach (var u in units)
                {
                    foreach (var cond in mExitConds)
                    {
                        if (cond is PlayerExitCondition && u is InstancePlayer)
                        {
                            var ret = (cond as PlayerExitCondition).Check(this, u as InstancePlayer);
                            if (ret)
                            {
                                u.setAoiStatus(null);
                            }
                        }
                        else
                        {
                            (cond as AoiExitCondition)?.Collect(this, u);
                        }
                    }
                }
            }

            foreach (var cond in mExitConds)
            {
                var aoiCond = cond as AoiExitCondition;
                if (aoiCond != null)
                {
                    aoiCond.EndCheck(this);
                    if (aoiCond.AoiExit)
                    {
                        SetAoiExit(aoiCond);
                    }
                }
            }
        }

        private AoiExitCondition mLastExitCondition;

        public void SetAoiExit(AoiExitCondition reason)
        {
            if (mLastExitCondition != null)
            {
                return;
            }
            mLastExitCondition = reason;
            if (reason.DelayMS > 0)
            {
                Zone.AddTimeDelayMS(reason.DelayMS, (t) => { Dispose(); });
            }
            else
            {
                Dispose();
            }
        }

        protected override void onObjectLeave(InstanceZoneObject o)
        {
            base.onObjectLeave(o);
            var p = o as TLInstancePlayer;
            if (p != null)
            {
                CurrentPlayerCount -= 1;
                p.SetEnvironmentVar("aoi", false, true);
            }
        }

        protected override void onObjectEnter(InstanceZoneObject o)
        {
            base.onObjectEnter(o);
            var p = o as TLInstancePlayer;
            if (p != null)
            {
                MaxEnteredPlayer += 1;
                CurrentPlayerCount += 1;
                p.SetEnvironmentVar("aoi", true, true);
            }
        }

        protected override void Disposing()
        {
            mOnAOIExitHandle?.Invoke(this, mLastExitCondition);
            mTickTask.Dispose();
            mOnAOIExitHandle = null;
            //清理所有刷新点
            foreach (var ability in mAbilities)
            {
                if (ability is AbstractSpawnAbility)
                {
                    var trigger = ability as AbstractSpawnAbility;
                    var tgs = trigger.BindingRegion.GetSpawnTriggers() as List<AbstractSpawnAbility>;
                    tgs?.Remove(trigger);
                }
                ability.Dispose();
            }

            //杀掉绑定单位
            if (mBindUnit != null && mBindUnit.IsAlive)
            {
                var u = mBindUnit.Target as InstanceUnit;
                if (u != null && (!u.IsDead && !u.IsDisposed))
                {
                    u.kill();
                }
            }

            //清理所有位面单位
            using (var list = ListObjectPool<InstanceZoneObject>.AllocAutoRelease(base.Objects))
            {
                foreach (var obj in list)
                {
                    if (obj is InstancePlayer)
                    {
                        (obj as InstancePlayer).setAoiStatus(null);
                    }
                    else
                    {
                        m_zone.RemoveObjectByID(obj.ID);
                    }
                }
            }
            base.Disposing();
        }
    }
}