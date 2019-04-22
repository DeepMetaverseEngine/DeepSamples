using DeepCore;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using TLBattle.Message;
using TLBattle.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Server.Plugins
{
    #region TLHateSystem
    public class TLHateSystem : HateSystem
    {
        public readonly TLVirtual Virtual;

        private bool mRecordAttackData = false;
        public string FirstAtkPlayerUUID = null;
        public string LastAtkPlayerUUID = null;
        public HashMap<string, int> AtkDataMap = null;

        private TLKillInfo killInfo;
        private InstanceUnit mLockAtkUnit;

        private int[] hpRankInfo;
        private int curHPDropState;
        public event Action<int[]> HPDropInfoHandle;
        public event Action<string, string> FirstAtkerChangeHandle;


        public TLHateSystem(TLVirtual owner)
            : base(owner.mUnit)
        {
            Virtual = owner;
            owner.mUnit.OnDead += MUnit_OnDead;
            RecordAttackData = true;
        }

        private void MUnit_OnDead(InstanceUnit unit, InstanceUnit attacker)
        {
            var v = attacker.Virtual as TLVirtual;
            string uuid = v.GetPlayerUUID();

            if (string.IsNullOrEmpty(uuid) == false)
            {
                LastAtkPlayerUUID = uuid;
            }

            CreateKillInfo();
            ClearAtkData();
        }

        public TLKillInfo GetKillInfo()
        {
            return killInfo;
        }

        private void CreateKillInfo()
        {
            if (mRecordAttackData)
            {
                killInfo = new TLKillInfo();
                killInfo.SceneID = Virtual.mUnit.Parent.TerrainSrc.TemplateID;
                killInfo.FirstAtkPlayerUUID = FirstAtkPlayerUUID;
                killInfo.LastAtkPlayerUUID = LastAtkPlayerUUID;

                if (AtkDataMap != null)
                {
                    HashMap<string, TLKillInfo.AttackData> map = new HashMap<string, TLKillInfo.AttackData>();
                    TLKillInfo.AttackData temp = null;

                    foreach (var kvp in AtkDataMap)
                    {
                        temp = new TLKillInfo.AttackData() { Damage = kvp.Value, PlayerUUID = kvp.Key };
                        map.Add(kvp.Key, temp);
                    }

                    killInfo.StatisticMap = map;
                }
            }
        }

        protected override void onOwnerHitted(HateInfo attacker, AttackSource attack, int reduceHP)
        {
            //是伤害再增加仇恨值.
            if (reduceHP > 0)
            {
                RecordAtkData(attacker, attack, reduceHP);
                reduceHP = (int)(reduceHP * TLEditorConfig.Instance.DAMAGE_TO_HATE_VALUE);
                AddHate(attacker, attack, reduceHP);
                CheckDamageInfo(reduceHP);
            }
        }

        private void AddHate(HateInfo attacker, AttackSource attack, int hate)
        {
            attacker.HateValue += hate;
        }

        protected override void onTargetAdded(HateInfo target)
        {
            var v = (target.Unit.Virtual as TLVirtual);
            v.OnHealEvent += TLHateSystem_OnHealEvent;
            v.OnCombatStateChangeHandle += V_OnCombatStateChangeHandle;
            base.onTargetAdded(target);
        }

        protected override void onTargetRemoved(HateInfo target)
        {
            //敌方单位死亡不清空伤害.
            //RemoveAtkData(target);
            var v = (target.Unit.Virtual as TLVirtual);

            if (RecordAttackData && Owner.IsDead == false)
                ClearFirstAtkcer(v.GetPlayerUUID());

            v.OnHealEvent -= TLHateSystem_OnHealEvent;
            v.OnCombatStateChangeHandle -= V_OnCombatStateChangeHandle;
            base.onTargetRemoved(target);
        }

        private void TLHateSystem_OnHealEvent(TLVirtual attacker, TLVirtual hitter, int value, AttackSource source, ref TLVirtual.AtkAppendData appendData)
        {
            //治疗转为x倍仇恨.
            value = -(int)(value * TLEditorConfig.Instance.HEAL_TO_HATE_VALUE);
            AddHate(Add(attacker.mUnit), source, value);
            Sort();
        }

        /// <summary>
        /// 脱战的单位移除.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="status"></param>
        private void V_OnCombatStateChangeHandle(TLVirtual unit, CombatStateChangeEventB2C.BattleStatus status)
        {
            if (status == CombatStateChangeEventB2C.BattleStatus.None)
            {
                Remove(unit.mUnit);
            }
        }

        public InstanceUnit TransHateTo(int index)
        {
            if (index == 0)
            {
                return GetHated();
            }
            int i = 0;
            using (var hateinfolist = ListObjectPool<HateInfo>.AllocAutoRelease())
            {
                this.GetHateList(hateinfolist);
                foreach (var hateinfo in hateinfolist)
                {
                    if (i == index)
                    {
                        return hateinfo.Unit;
                    }
                    i++;
                }
            }
            return null;
        }

        public delegate void ForHateListAction(InstanceUnit unit, ref bool cancel);
        public void ForEachHateList(ForHateListAction action)
        {
            bool cancel = false;

            using (var hateinfolist = ListObjectPool<HateInfo>.AllocAutoRelease())
            {
                this.GetHateList(hateinfolist);
                for (int i = 0; i < hateinfolist.Count; i++)
                {
                    action.Invoke(hateinfolist[i].Unit, ref cancel);
                    if (cancel) break;
                }
            }
        }

        public bool RecordAttackData
        {
            get { return mRecordAttackData; }
            set
            {
                mRecordAttackData = value;
                if (value)
                {
                    AtkDataMap = new HashMap<string, int>();
                }
            }
        }

        private void SaveFirstAtker(string uuid)
        {
            if (FirstAtkPlayerUUID == null)
            {
                FirstAtkPlayerUUID = uuid;
                FirstAtkerChangeHandle?.Invoke(null, uuid);
            }
        }

        public void ClearFirstAtkcer(string uuid)
        {
            if (FirstAtkPlayerUUID == uuid)
            {
                FirstAtkPlayerUUID = null;
                FirstAtkerChangeHandle?.Invoke(uuid, null);
            }
        }

        private void RecordAtkData(HateInfo attacker, AttackSource attack, int reduceHP)
        {
            if (RecordAttackData)
            {
                var v = (attacker.Unit.Virtual) as TLVirtual;
                string uuid = v.GetPlayerUUID();
                if (string.IsNullOrEmpty(uuid) == false)
                {
                    SaveFirstAtker(uuid);
                    int damage = 0;

                    if (AtkDataMap.TryGetValue(uuid, out damage))
                    {
                        damage += reduceHP;
                        AtkDataMap[uuid] = damage;
                    }
                    else
                    {
                        AtkDataMap.Add(uuid, reduceHP);
                    }
                }
            }
        }

        private void RemoveAtkData(HateInfo target)
        {
            if (Owner.IsDead) { return; }

            if (RecordAttackData)
            {
                var v = (target.Unit.Virtual) as TLVirtual;
                string uuid = v.GetPlayerUUID();
                if (string.IsNullOrEmpty(uuid) == false)
                {
                    ClearFirstAtkcer(uuid);
                    AtkDataMap.Remove(uuid);
                }
            }
        }

        private void ClearAtkData()
        {
            if (AtkDataMap != null)
            {
                AtkDataMap.Clear();
                FirstAtkPlayerUUID = null;
                LastAtkPlayerUUID = null;
            }
        }

        public override void Clear()
        {
            base.Clear();
            if (!Owner.IsDead)
                ClearAtkData();

            ClearDamageDropRecord();
       
        }

        /// <summary>
        /// 获得随机目标.
        /// </summary>
        /// <returns></returns>
        public uint GetRandomTarget()
        {
            using (var list = ListObjectPool<HateInfo>.AllocAutoRelease())
            {
                this.GetHateList(list);
                return list[Owner.RandomN.Next(0, list.Count)].Unit.ID;
            }
        }

        public override InstanceUnit GetHated()
        {
            InstanceUnit ret = base.GetHated();

            if (ret != null)
            {
                ret = Virtual.GetAtkTarget(ret);
            }

            if (mLockAtkUnit != null)
            {
                if (!mLockAtkUnit.IsActive || mLockAtkUnit.IsDead)
                    mLockAtkUnit = null;

                if (mLockAtkUnit != null)
                    ret = mLockAtkUnit;
            }

            return ret;
        }

        public void LockAtkUnit(InstanceUnit unit)
        {
            mLockAtkUnit = unit;
        }

        public void SetHPDropInfo(int[] info)
        {
            hpRankInfo = info;
        }

        private void CheckDamageInfo(int reduceHP)
        {
            if (hpRankInfo != null && reduceHP > 0)
            {

                int v = (int)(Owner.CurrentHP_Pct * 100);

                if (curHPDropState < hpRankInfo.Length)
                {
                    List<int> reward = new List<int>();

                    for (int i = curHPDropState; i < hpRankInfo.Length; i++)
                    {
                        if (v <= hpRankInfo[i])
                        {
                            curHPDropState = i + 1;
                            reward.Add(hpRankInfo[i]);
                        }
                    }

                    if (reward.Count > 0)
                    {
                        DispatchHPDrop(reward.ToArray());
                    }
                }
            }
        }

        private void DispatchHPDrop(int[] info)
        {
            if (info != null && info.Length > 0)
            {
                if (HPDropInfoHandle != null)
                {
                    HPDropInfoHandle.Invoke(info);
                }
            }
        }

        private void ClearDamageDropRecord()
        {
            curHPDropState = 0;
        }

        public List<string> GetAtkerList()
        {
            List<string> ret = null;

            if (AtkDataMap != null)
            {
                ret = new List<string>();
                foreach (var item in AtkDataMap)
                {
                    ret.Add(item.Key);
                }
            }

            return ret;
        }

    }
    #endregion 

    #region TLHitList

    public class TLHitSystem
    {
        protected readonly HashMap<uint, InstancePlayer> mUnitMap = new HashMap<uint, InstancePlayer>();
        protected readonly List<InstancePlayer> mUnitList = new List<InstancePlayer>();

        internal void OnHited(InstanceUnit info)
        {
            InstanceUnit unit = info;
            //如果是宠物,找它的主人
            //if (unit is TLInstancePet)
            //{
            //    unit = (unit as TLInstancePet).SummonerUnit;
            //}
            ////如果是召唤物,找它的主人
            //if (unit is TLInstanceSummon)
            //{
            //    unit = (unit as TLInstanceSummon).SummonerUnit;
            //}

            //不是玩家则跳过
            if (!(unit is InstancePlayer))
            {
                return;
            }

            if (!mUnitMap.ContainsKey(info.ID))
            {
                mUnitMap.Add(info.ID, unit as InstancePlayer);
                mUnitList.Add(unit as InstancePlayer);
            }
        }
        internal void Clear()
        {
            mUnitMap.Clear();
            mUnitList.Clear();
        }

        /// <summary>
        /// 获取被攻击列表.
        /// </summary>
        /// <returns></returns>
        public void GetHitList(List<InstancePlayer> ret)
        {
            for (int i = mUnitList.Count - 1; i >= 0; --i)
            {
                var u = mUnitList[i];
                if (u.IsActive)
                {
                    ret.Add(u);
                }
                else
                {
                    mUnitList.RemoveAt(i);
                    mUnitMap.Remove(u.ID);
                }
            }
        }

    }

    #endregion
}
