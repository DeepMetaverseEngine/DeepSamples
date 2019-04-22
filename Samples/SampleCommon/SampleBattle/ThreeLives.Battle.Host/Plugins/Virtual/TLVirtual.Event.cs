using DeepCore;
using DeepCore.GameHost.Formula;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using static DeepCore.GameHost.Instance.InstanceUnit;

namespace TLBattle.Server.Plugins.Virtual
{
    partial class TLVirtual
    {
        //OnHitOther//
        readonly private List<OnHitHandler> mOnHitOtherLt = new List<OnHitHandler>();
        //OnHitDamage//
        readonly private List<OnHitHandler> mOnHitDamageLt = new List<OnHitHandler>();
        //OnCalDamage//
        readonly private HashMap<int, OnCalDmageHandler> mOnCalDamageMap = new HashMap<int, OnCalDmageHandler>();
        //OnLaunchSkillOver//
        readonly private List<OnLaunchSkillOverHandler> mOnLaunchSkillOverLt = new List<OnLaunchSkillOverHandler>();

        readonly private List<OnTryLaucnSkill> mOnTryLaunchSkillLt = new List<OnTryLaucnSkill>();

        #region 不确定模块.

        //OnBuffEvent//
        readonly private List<OnBuffEventHandler> mOnBuffEventLt = new List<OnBuffEventHandler>();
        //OnGetAtkTarget//
        readonly private List<OnGetAtkTargetHandler> mOnGetAtkTargetLt = new List<OnGetAtkTargetHandler>();
        //OnGetSkillDamageInfo //
        readonly private List<OnGetSkillDamageInfo> mOnGetSkillDamageInfoLt = new List<OnGetSkillDamageInfo>();
        //OnSummonUnit//
        readonly private List<OnSummonUnit> mOnSummonUnitLt = new List<OnSummonUnit>();

        #endregion

        private HashMap<int, IHandle> mHandleMap = new HashMap<int, IHandle>();
        private int mHandleUUID = 0;

        #region Handler

        abstract class IHandle
        {
            public GameSkill m_skill;
        }

        class OnHitHandler : IHandle
        {
            public readonly IOnHit m_hit;
            public OnHitHandler(IOnHit hit, GameSkill skill)
            {
                m_hit = hit;
                m_skill = skill;
            }
        }

        class OnBuffEventHandler : IHandle
        {
            public readonly IOnBuffEvent m_tg;
            public OnBuffEventHandler(IOnBuffEvent tg, GameSkill skill)
            {
                m_tg = tg;
                m_skill = skill;
            }
        }

        class OnCalDmageHandler : IHandle
        {
            public readonly ICalDamage m_cal;
            public OnCalDmageHandler(ICalDamage tg, GameSkill skill)
            {
                m_cal = tg;
                m_skill = skill;
            }

        }

        class OnTryLaucnSkill : IHandle
        {
            public readonly ITryLaunchSkillEvent m_hand;

            public bool ListenAllSkill = false;

            public OnTryLaucnSkill(ITryLaunchSkillEvent tg, GameSkill skill, bool listenaAllSkill)
            {
                m_hand = tg;
                m_skill = skill;
                ListenAllSkill = listenaAllSkill;
            }
        }

        class OnLaunchSkillOverHandler : IHandle
        {
            public readonly ILaunchSkillOver m_hand;

            public bool ListenAllSkill = false;

            public OnLaunchSkillOverHandler(ILaunchSkillOver tg, GameSkill skill, bool listenAllSkill)
            {
                m_hand = tg;
                m_skill = skill;
                ListenAllSkill = listenAllSkill;
            }
        }

        class OnGetAtkTargetHandler : IHandle
        {
            public readonly IGetAtkTarget m_hand;

            public readonly bool mListenAllSkill;

            public OnGetAtkTargetHandler(IGetAtkTarget tg, GameSkill skill, bool listenAllSkill)
            {
                m_hand = tg;
                m_skill = skill;
                mListenAllSkill = listenAllSkill;
            }
        }

        class OnGetSkillDamageInfo : IHandle
        {
            public readonly IGetSkillDamageInfo m_hand;

            public OnGetSkillDamageInfo(IGetSkillDamageInfo tg, GameSkill skill)
            {
                m_hand = tg;
                m_skill = skill;
            }
        }

        class OnSummonUnit : IHandle
        {
            public readonly ISummonUnit m_Summon;
            public OnSummonUnit(ISummonUnit tg, GameSkill skill)
            {
                m_Summon = tg;
                m_skill = skill;
            }

        }

        #endregion

        #region 计算伤害时触发.

        public int RegistCalDamage(ICalDamage cal, GameSkill sk)
        {
            OnCalDmageHandler handle = new OnCalDmageHandler(cal, sk);
            int ret = HandleUUIDCreate();
            mOnCalDamageMap.Add(sk.SkillID, handle);
            mHandleMap.Add(ret, handle);
            return ret;
        }

        public bool UnRegistCalDamage(int handleUUID)
        {
            bool ret = false;
            IHandle handle = null;

            if (mHandleMap.TryGetValue(handleUUID, out handle))
            {
                if (handle != null && handle is OnCalDmageHandler)
                {
                    ret = mOnCalDamageMap.Remove(handle.m_skill.SkillID);
                    mHandleMap.Remove(handleUUID);
                }
            }

            return ret;
        }



        #endregion

        #region 单位打到别人时开发.

        /// <summary>
        /// 单位打到别人时触发
        /// </summary>
        /// <param name="hit"></param>
        /// <param name="sk"></param>
        public int RegistOnHitOther(IOnHit hit, GameSkill sk)
        {
            OnHitHandler handle = new OnHitHandler(hit, sk);
            int ret = HandleUUIDCreate();
            mOnHitOtherLt.Add(handle);
            mHandleMap.Add(ret, handle);
            return ret;
        }

        public bool UnRegistOnHitOther(int handleUUID)
        {
            bool ret = false;
            IHandle handle = null;

            if (mHandleMap.TryGetValue(handleUUID, out handle))
            {
                if (handle != null && handle is OnHitHandler)
                {
                    ret = mOnHitOtherLt.Remove(handle as OnHitHandler);
                    mHandleMap.Remove(handleUUID);
                }
            }

            return ret;
        }

        #endregion

        #region 单位受到攻击时触发.

        /// <summary>
        /// 单位收到攻击时触发
        /// </summary>
        /// <param name="hit"></param>
        /// <param name="sk"></param>
        public int RegistOnHitDamage(IOnHit hit, GameSkill sk)
        {
            OnHitHandler handle = new OnHitHandler(hit, sk);
            mOnHitDamageLt.Add(handle);
            int ret = HandleUUIDCreate();
            mHandleMap.Add(ret, handle);
            return ret;
        }

        public bool UnRegistOnHitDamage(int handleUUID)
        {
            bool ret = false;
            IHandle handle = null;

            if (mHandleMap.TryGetValue(handleUUID, out handle))
            {
                if (handle != null && handle is OnHitHandler)
                {
                    ret = mOnHitDamageLt.Remove(handle as OnHitHandler);
                    mHandleMap.Remove(handleUUID);
                }
            }

            return ret;
        }


        #endregion

        #region BUFF触发.

        /// <summary>
        /// 单位启动Buff后触发
        /// </summary>
        /// <param name="tg"></param>
        /// <param name="sk"></param>
        public int RegistOnBuffEvent(IOnBuffEvent tg, GameSkill sk)
        {
            OnBuffEventHandler handle = new OnBuffEventHandler(tg, sk);
            mOnBuffEventLt.Add(handle);
            int ret = HandleUUIDCreate();
            mHandleMap.Add(ret, handle);
            return ret;
        }

        public bool UnRegistOnBuffEvent(int handleUUID)
        {
            bool ret = false;
            IHandle handle = null;

            if (mHandleMap.TryGetValue(handleUUID, out handle))
            {
                if (handle != null && handle is OnBuffEventHandler)
                {
                    ret = mOnBuffEventLt.Remove(handle as OnBuffEventHandler);
                    mHandleMap.Remove(handleUUID);
                }
            }

            return ret;
        }

        #endregion

        #region 监听技能施放.

        public int RegistTryLaunchSkillEvent(ITryLaunchSkillEvent tg, GameSkill sk, bool listenAllSkill = false)
        {
            OnTryLaucnSkill handle = new OnTryLaucnSkill(tg, sk, listenAllSkill);
            mOnTryLaunchSkillLt.Add(handle);
            int ret = HandleUUIDCreate();
            mHandleMap.Add(ret, handle);
            return ret;
        }

        public bool UnRegistTryLaunchSkillEvent(int handleUUID)
        {
            bool ret = false;
            IHandle handle = null;

            if (mHandleMap.TryGetValue(handleUUID, out handle))
            {
                if (handle != null && handle is OnTryLaucnSkill)
                {
                    ret = mOnTryLaunchSkillLt.Remove(handle as OnTryLaucnSkill);
                    mHandleMap.Remove(handleUUID);
                }
            }

            return ret;
        }

        #endregion


        #region 释放完技能后.

        /// <summary>
        /// 注册技能释放完毕的监听.
        /// </summary>
        /// <param name="call"></param>
        /// <param name="sk"></param>
        /// <param name="listenAllSkill"></param>
        /// <returns></returns>
        public int RegistLaunchSkillOver(ILaunchSkillOver call, GameSkill sk, bool listenAllSkill)
        {
            OnLaunchSkillOverHandler handle = new OnLaunchSkillOverHandler(call, sk, listenAllSkill);
            int ret = HandleUUIDCreate();
            mOnLaunchSkillOverLt.Add(handle);
            mHandleMap.Add(ret, handle);
            return ret;
        }

        /// <summary>
        /// 反注册技能释放完毕的监听.
        /// </summary>
        /// <param name="handleUUID"></param>
        /// <returns></returns>
        public bool UnRegistLaunchSkillOver(int handleUUID)
        {
            bool ret = false;
            IHandle handle = null;

            if (mHandleMap.TryGetValue(handleUUID, out handle))
            {
                if (handle != null && handle is OnLaunchSkillOverHandler)
                {
                    ret = mOnLaunchSkillOverLt.Remove(handle as OnLaunchSkillOverHandler);
                    mHandleMap.Remove(handleUUID);
                }
            }

            return ret;
        }

        #endregion

        #region 获得攻击目标.

        /// <summary>
        /// 注册获取攻击目标监听.
        /// </summary>
        /// <param name="call"></param>
        /// <param name="sk"></param>
        /// <returns></returns>
        public int RegistGetAtkTarget(IGetAtkTarget call, GameSkill sk, bool listenAllSkill)
        {
            OnGetAtkTargetHandler handle = new OnGetAtkTargetHandler(call, sk, listenAllSkill);
            int ret = HandleUUIDCreate();
            mOnGetAtkTargetLt.Add(handle);
            mHandleMap.Add(ret, handle);
            return ret;
        }

        /// <summary>
        /// 反注册获取攻击目标监听.
        /// </summary>
        /// <param name="call"></param>
        /// <param name="sk"></param>
        /// <returns></returns>
        public bool UnRegistGetAtkTarget(int handleUUID)
        {
            bool ret = false;
            IHandle handle = null;

            if (mHandleMap.TryGetValue(handleUUID, out handle))
            {
                if (handle != null && handle is OnGetAtkTargetHandler)
                {
                    ret = mOnGetAtkTargetLt.Remove(handle as OnGetAtkTargetHandler);
                    mHandleMap.Remove(handleUUID);
                }
            }

            return ret;
        }

        #endregion

        #region 注册获得技能伤害.

        public int RegistGetSkillDamageInfo(IGetSkillDamageInfo call, GameSkill sk)
        {
            OnGetSkillDamageInfo handle = new OnGetSkillDamageInfo(call, sk);
            int ret = HandleUUIDCreate();
            mOnGetSkillDamageInfoLt.Add(handle);
            mHandleMap.Add(ret, handle);
            return ret;
        }

        public bool UnRegistGetSkillDamageInfo(int handleUUID)
        {
            bool ret = false;
            IHandle handle = null;

            if (mHandleMap.TryGetValue(handleUUID, out handle))
            {
                if (handle != null && handle is OnGetSkillDamageInfo)
                {
                    ret = mOnGetSkillDamageInfoLt.Remove(handle as OnGetSkillDamageInfo);
                    mHandleMap.Remove(handleUUID);
                }
            }

            return ret;
        }

        #endregion

        #region 单位召唤时触发

        /// <summary>
        /// 单位召唤时触发
        /// </summary>
        /// <param name="summon"></param>
        /// <param name="sk"></param>
        public int RegistOnSummonUnit(ISummonUnit summon, GameSkill sk)
        {
            OnSummonUnit handle = new OnSummonUnit(summon, sk);
            mOnSummonUnitLt.Add(handle);
            int ret = HandleUUIDCreate();
            mHandleMap.Add(ret, handle);
            return ret;
        }

        public bool UnRegistOnSummonUnit(int handleUUID)
        {
            bool ret = false;
            IHandle handle = null;

            if (mHandleMap.TryGetValue(handleUUID, out handle))
            {
                if (handle != null && handle is OnSummonUnit)
                {
                    ret = mOnSummonUnitLt.Remove(handle as OnSummonUnit);
                    mHandleMap.Remove(handleUUID);
                }
            }

            return ret;
        }


        #endregion

        private int HandleUUIDCreate()
        {
            return ++mHandleUUID;
        }

        //攻击者监听:打到其他单位.
        private float DispatchHitOtherEvent(float damage, TLVirtual target, AttackSource source, ref AtkAppendData data)
        {
            if (this.mUnit.IsActive)
            {
                for (int i = mOnHitOtherLt.Count - 1 ; i >= 0 ; i--)
                {
                    OnHitHandler hitend = mOnHitOtherLt[i];
                    damage = hitend.m_hit.Invoke(damage, target, this, source, hitend.m_skill, ref data);
                    break;
                }
            }
            return damage;
        }

        //受击者监听:受到伤害.
        private float DispatchHitDamageEvent(float damage, TLVirtual attacker, AttackSource source, ref AtkAppendData data)
        {
            if (this.mUnit.IsActive)
            {
                for (int i = mOnHitDamageLt.Count - 1 ; i >= 0 ; i--)
                {
                    OnHitHandler hitend = mOnHitDamageLt[i];
                    damage = hitend.m_hit.Invoke(damage, this, attacker, source, hitend.m_skill, ref data);
                    break;
                }
            }
            return damage;
        }

        /// <summary>
        /// 分发技能施放事件.
        /// </summary>
        /// <param name="st"></param>
        private void DispatchTryLaunchSkillEvent(ref InstanceUnit.SkillState skill, ref bool result, ref InstanceUnit.LaunchSkillParam param)
        {
            bool ret = true;

            for (int i = 0 ; i < mOnTryLaunchSkillLt.Count ; i++)
            {
                OnTryLaucnSkill hitend = mOnTryLaunchSkillLt[i];

                if (hitend.ListenAllSkill) //是否监听所有技能.
                {
                    ret = hitend.m_hand.Invoke(ref skill, this, ref param);

                    if (ret == false)
                    {
                        break;
                    }
                }
                else if (hitend.m_skill.SkillID == skill.ID)//指定监听技能.
                {
                    ret = hitend.m_hand.Invoke(ref skill, this, ref param);

                    if (ret == false)
                    {
                        break;
                    }
                }
            }

            result = ret;
        }

        /// <summary>
        /// 派发技能施放完成事件.
        /// </summary>
        /// <param name="costEnergy"></param>
        /// <param name="attacker"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private int DispatchLaunchsSkillOverEvent(int costEnergy, TLVirtual attacker, SkillState state)
        {
            if (attacker.mUnit.IsActive)
            {
                for (int i = 0 ; i < mOnLaunchSkillOverLt.Count ; i++)
                {
                    OnLaunchSkillOverHandler hitend = mOnLaunchSkillOverLt[i];

                    if (hitend.ListenAllSkill)
                    {
                        costEnergy = hitend.m_hand.Invoke(costEnergy, attacker, state);
                    }
                    else if (hitend.m_skill.SkillID == state.ID)
                    {
                        costEnergy = hitend.m_hand.Invoke(costEnergy, attacker, state);
                    }
                }
            }

            return costEnergy;
        }

        //回调技能伤害监听.
        private int DispatchCalDamageEvent(TLVirtual hitter, AttackSource source, ref AtkAppendData data)
        {
            //特殊情况单位是技能触发伤害，则单位必须活着，如果是spell或BUFF无视单位自身状态.
            if (this.mUnit.IsDead == true && source.FromSkillState != null)
            {
                //单位已死亡，技能造成的伤害不算.
                return 0;
            }

            bool findCalBack = false;
            int ret = 0;

            TLAttackProperties prop = (source.Attack.Properties as TLAttackProperties);
            int id = prop.SkillTemplateID;

            if (this.mUnit is InstanceTriggerUnit || this.mUnit.IsDead == false)
            {
                OnCalDmageHandler hand = null;
                hand = mOnCalDamageMap.Get(id);
                if (hand != null && hand.m_cal != null)
                {
                    ret = hand.m_cal.Invoke(this, hitter, source, hand.m_skill, ref data);
                    FormatLog(DeepCore.Log.LoggerLevel.INFO, "【{0}】对目标【{1}】使用技能【{2}】效果 = 【{3}】", this.mInfo.Name, hitter.mInfo.Name, id, ret);
                    findCalBack = true;
                }
                if (findCalBack == false)
                {
                    TLVirtual.FormatLog(DeepCore.Log.LoggerLevel.WARNNING, "未找到单位【{0}】技能【{1}】注册的伤害回调", this.mInfo.Name, id);
                }

            }

            return ret;
        }

        private bool DoOnBuffStart(InstanceUnit.BuffState buff)
        {
            for (int i = mOnBuffEventLt.Count - 1 ; i >= 0 ; --i)
            {
                OnBuffEventHandler tg = mOnBuffEventLt[i];
                tg.m_tg.Invoke(this, buff, tg.m_skill);
            }
            return true;
        }

        private TLVirtual DispatchGetAtkUnitEvent(TLVirtual unit)
        {
            TLVirtual ret = unit;

            if (this.mUnit.IsActive)
            {
                for (int i = mOnGetAtkTargetLt.Count - 1 ; i >= 0 ; --i)
                {
                    OnGetAtkTargetHandler hitend = mOnGetAtkTargetLt[i];
                    //if (hitend.mListenAllSkill)
                    {
                        ret = hitend.m_hand.Invoke(unit, hitend.m_skill);
                    }
                }
            }

            return ret;
        }

        public void DoSkillDamage(ref float skillDamagePer, ref int skillDamageMod, TLVirtual attacker,
                                   TLVirtual hitter, AttackSource source)
        {

            for (int i = mOnGetSkillDamageInfoLt.Count - 1 ; i >= 0 ; i--)
            {
                OnGetSkillDamageInfo hitend = mOnGetSkillDamageInfoLt[i];
                hitend.m_hand.Invoke(ref skillDamagePer, ref skillDamageMod, attacker, hitter, source, hitend.m_skill);
                break;
            }

        }

        /// <summary>
        /// 清理注册信息.
        /// </summary>
        public void ClearRegistEvent()
        {
            mHandleMap.Clear();
            mOnHitOtherLt.Clear();
            mOnHitDamageLt.Clear();
            mOnBuffEventLt.Clear();
            mOnCalDamageMap.Clear();
            mOnTryLaunchSkillLt.Clear();
            mOnLaunchSkillOverLt.Clear();
            mOnGetAtkTargetLt.Clear();
            mOnGetSkillDamageInfoLt.Clear();
            mOnSummonUnitLt.Clear();
            mHandleUUID = 0;
            event_OnCombatStateChangeHandle = null;
        }

        /// <summary>
        /// 删除技能相关事件.
        /// </summary>
        /// <param name="skillID"></param>
        public void RemoveEventBySkillID(int skillID)
        {
            try
            {
                using (var removeList = ListObjectPool<int>.AllocAutoRelease())
                {
                    foreach (KeyValuePair<int, IHandle> pair in mHandleMap)
                    {
                        if (pair.Value != null && pair.Value.m_skill != null && skillID == pair.Value.m_skill.SkillID)
                        {
                            removeList.Add(pair.Key);
                        }
                    }
                    for (int i = 0 ; i < removeList.Count ; i++)
                    {
                        RemoveEventByUUID(removeList[i]);
                    }
                }

            }
            catch (Exception error)
            {
                throw new Exception(error.StackTrace);
            }
        }

        private void RemoveEventByUUID(int uuid)
        {
            IHandle handle = null;

            if (mHandleMap.TryGetValue(uuid, out handle))
            {
                if (handle is OnHitHandler)
                {
                    UnRegistOnHitOther(uuid);
                    UnRegistOnHitDamage(uuid);
                }
                else if (handle is OnBuffEventHandler)
                {
                    UnRegistOnBuffEvent(uuid);
                }
                else if (handle is OnCalDmageHandler)
                {
                    UnRegistCalDamage(uuid);
                }
                else if (handle is OnLaunchSkillOverHandler)
                {
                    UnRegistLaunchSkillOver(uuid);
                }
                else if (handle is OnGetAtkTargetHandler)
                {
                    UnRegistGetAtkTarget(uuid);
                }
                else if (handle is OnGetSkillDamageInfo)
                {
                    UnRegistGetSkillDamageInfo(uuid);
                }
                else if (handle is OnTryLaucnSkill)
                {
                    UnRegistTryLaunchSkillEvent(uuid);
                }
                else if (handle is OnSummonUnit)
                {
                    UnRegistOnSummonUnit(uuid);
                }
            }
        }

        /// <summary>
        /// 治愈回调.
        /// </summary>
        public delegate void OnHealHandler(TLVirtual attacker, TLVirtual hitter, int value, AttackSource source, ref AtkAppendData appendData);
        public event OnHealHandler OnHealEvent;
        protected virtual void OnHeal(TLVirtual attcker, int value, AttackSource source, ref AtkAppendData result)
        {
            if (OnHealEvent != null)
            {
                OnHealEvent.Invoke(attcker, this, value, source, ref result);
            }
        }

    }

}
