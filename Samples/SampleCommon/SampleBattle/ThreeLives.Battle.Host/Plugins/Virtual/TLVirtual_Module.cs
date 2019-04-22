using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using TLBattle.Common.Data;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using static DeepCore.GameHost.Instance.InstanceUnit;

namespace TLBattle.Server.Plugins.Virtual
{
    public partial class TLVirtual
    {
        #region SkillModule 处理技能相关操作.

        public class TLSkillModule
        {
            //普通技能.
            private int baseSkillid = 0;
            //单位拥有的技能.
            private HashMap<int, GameSkill> mSkillMap = new HashMap<int, GameSkill>();
            //宿主.
            private TLVirtual mOwner = null;
            //是否已销毁.
            private bool mIsDispose = false;

            private bool mFinishInit = false;

            private List<TLGameSkillSnap> mLoopList = null;

            private int mLoopListIndex = 0;

            private HashMap<int, UnitSkill> SkillScriptMap = new HashMap<int, UnitSkill>();


            public TLSkillModule(TLVirtual owner)
            {
                mOwner = owner;
            }

            public void Init()
            {
                if (mIsDispose) { return; }
                if (mSkillMap != null) { mSkillMap.Clear(); }
                else { mSkillMap = new HashMap<int, GameSkill>(); }

                AddListener();
                mFinishInit = true;

                var skInfo = mOwner.mProp.ServerData.SkillInfo;
                if (skInfo != null)
                {
                    InitAllSkill(mOwner.GetAllSkillData());
                }

                InitPurAtkScript();
            }

            public bool IsFinishInit()
            {
                return mFinishInit;
            }

            private void AddListener()
            {
                mOwner.mUnit.OnSkillRemoved += MUnit_OnSkillRemoved;
                mOwner.mUnit.OnLaunchSkill += MUnit_OnLaunchSkill;
            }

            private void RemoveListener()
            {
                mOwner.mUnit.OnSkillRemoved -= MUnit_OnSkillRemoved;
                mOwner.mUnit.OnLaunchSkill -= MUnit_OnLaunchSkill;
            }

            private void MUnit_OnSkillRemoved(InstanceUnit unit, InstanceUnit.SkillState sk)
            {
                RemoveSkillByEvent(sk);
            }

            public void Dispose()
            {
                if (mIsDispose == true)
                {
                    return;
                }

                UnInitPurAtkScript();
                RemoveListener();

                mIsDispose = true;
            }

            private void InitAllSkill(List<GameSkill> list)
            {
                if (list == null) { return; }
                if (mIsDispose) { return; }

                try
                {
                    List<GameSkill> active = new List<GameSkill>();
                    List<GameSkill> passive = new List<GameSkill>();
                    GetActivePassiveSkillList(list, ref active, ref passive);

                    //主动技能.
                    InitActiveSkill(active);
                    //被动技能.
                    InitPassiveSkill(passive);
                }
                catch (Exception err)
                {
                    log.Error(string.Format("单位【{0}】InitSkill Error:【{1}】", mOwner.mUnit.Name, err.ToString()));
                }
            }

            /// <summary>
            /// 获得主动、被动技能列表.
            /// </summary>
            /// <param name="src"></param>
            /// <param name="active"></param>
            /// <param name="passive"></param>
            private void GetActivePassiveSkillList(List<GameSkill> src, ref List<GameSkill> active, ref List<GameSkill> passive)
            {
                GameSkill gs = null;

                for (int i = 0; i < src.Count; i++)
                {
                    gs = src[i];
                    if (gs.SkillType == GameSkill.TLSkillType.passive)
                    {
                        passive.Add(gs);
                    }
                    else
                    {
                        active.Add(gs);
                    }
                }
            }

            private void InitActiveSkill(List<GameSkill> list)
            {
                if (list == null || list.Count == 0) { return; }

                UnitSkill us = null;
                SkillTemplate st = null;

                SkillTemplate baseSkill = null;
                List<SkillTemplate> skillList = new List<SkillTemplate>();
                List<UnitSkill> tempUnitSkillLt = new List<UnitSkill>();
                for (int i = 0; i < list.Count; i++)
                {
                    InitUnitSkillScript(list[i], out us, out st);

                    if (list[i].SkillType == GameSkill.TLSkillType.normalAtk)
                    {
                        baseSkill = st;
                        baseSkillid = st.ID;
                    }
                    else
                    {
                        skillList.Add(st);
                    }
                    mSkillMap.Add(list[i].SkillID, list[i]);

                    tempUnitSkillLt.Add(us);
                }

                //init unitskill
                mOwner.mUnit.InitSkills(baseSkill, skillList.ToArray());

                //设置技能CD时间.
                SetSkillCD(skillList);

                for (int i = 0; i < tempUnitSkillLt.Count; i++)
                {
                    tempUnitSkillLt[i].InitOver(mOwner);
                }
            }

            /// <summary>
            /// 设置技能CD时间，跨场景时候技能CD时间要记录.
            /// </summary>
            /// <param name="list"></param>
            private void SetSkillCD(List<SkillTemplate> list)
            {
                if (list == null) { return; }
                GameSkill gs = null;

                for (int i = 0; i < list.Count; i++)
                {
                    gs = mSkillMap.Get(list[i].ID);
                    SetSkillCD(gs.SkillID, gs.SkillTimestampMS);
                }
            }

            private void InitPassiveSkill(List<GameSkill> list)
            {
                if (list == null || list.Count == 0) { return; }

                //TO DO.
            }
            public void ClearSkill(List<int> keeps = null)
            {
                if (mIsDispose) { return; }

                List<GameSkill> ret = new List<GameSkill>();

                if (mSkillMap != null)
                {
                    foreach (KeyValuePair<int, GameSkill> pair in mSkillMap)
                    {
                        if (keeps != null && keeps.Contains(pair.Key))
                        {
                            //DO NOTHING.
                        }
                        else
                            ret.Add(pair.Value);
                    }

                    for (int index = 0; index < ret.Count; index++)
                    {
                        RemoveActiveSkill(ret[index]);
                    }

                }
            }

            private void RemoveActiveSkill(GameSkill gs)
            {
                mOwner.mUnit.RemoveSkill(gs.SkillID);
            }

            private void RemoveSkillByEvent(InstanceUnit.SkillState sk)
            {
                GameSkill gs = null;

                if (mSkillMap.TryGetValue(sk.ID, out gs))
                {
                    mOwner.RemoveEventBySkillID(gs.SkillID);
                    mSkillMap.Remove(sk.ID);
                }

                SkillScriptMap.RemoveByKey(sk.ID);

            }

            private void InitUnitSkillScript(GameSkill gs, out UnitSkill us, out SkillTemplate st)
            {
                UnitSkill ret_us = null;
                SkillTemplate ret_st = null;

                us = ret_us;
                st = ret_st;

                if (SkillScriptMap.TryGetValue(gs.SkillID, out us))
                {
                    st = mOwner.mUnit.getSkill(gs.SkillID);
                    return;
                }


                try
                {
                    if (gs.SkillType == GameSkill.TLSkillType.God)
                    {
                        ret_us = TLBattleSkill.GetGodSkillScript();
                    }
                    else
                    {
                        ret_us = TLBattleSkill.GetUnitSkill(gs.SkillID, true);
                    }

                    ret_st = TLBattleSkill.GetSkillTemplate(gs.SkillID, true);

                    if (ret_us == null)//没有指定脚本使用默认.
                    {
                        var p = (ret_st.Properties as TLSkillProperties);
                        if (p.LoadConfig)
                        {
                            ret_us = TLBattleSkill.GetUnitSkill(TLBattleSkill.DEFAULT_SCRIPT_CONFIG, true);
                        }
                        else
                        {
                            ret_us = TLBattleSkill.GetUnitSkill(TLBattleSkill.DEFAULT_SCRIPT_EDITOR, true);

                        }

                    }

                    ret_us.Init(gs, mOwner, ref ret_st);

                    //保存脚本.
                    AddScript(gs.SkillID, ret_us, true);

                    us = ret_us;
                    st = ret_st;
                }
                catch (Exception err)
                {
                    ThrowTLSkillException(gs.SkillID, mOwner.mInfo.TemplateID, err.ToString());
                }
            }

            /// <summary>
            /// 报错方法.
            /// </summary>
            /// <param name="skill"></param>
            /// <param name="unit"></param>
            /// <param name="error"></param>
            private void ThrowTLSkillException(int skill, int unit, string error)
            {
                string msg = string.Format("TLSkill Init Error id : SkillID = {0} UnitID = {1},Error = {2}", skill, unit, error.ToString());
                log.Error(msg);
                throw new Exception(msg);
            }

            private void SetSkillCD(int skillid, long timestamp)
            {
                InstanceUnit.SkillState ss = mOwner.mUnit.getSkillState(skillid);
                if (ss == null)
                {
                    return;
                }

                int time = CalSkillCDTime(timestamp);

                //大于0代表还处于CD中.
                if (time > 0 && time <= ss.TotalCDTime)
                {
                    mOwner.mUnit.SetSkillPassTime(skillid, ss.TotalCDTime - time);
                }
            }

            private int CalSkillCDTime(long timestamp)
            {
                long curTime = GetTimestampMS();
                long dif = timestamp - curTime;
                return (int)dif;
            }

            public long GetTimestampMS()
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                return (long)ts.TotalMilliseconds;
            }

            /// <summary>
            /// 获得单位当前状态时间戳.
            /// </summary>
            /// <returns></returns>
            public TLSkillStatusData GetSkillStatusData()
            {
                TLSkillStatusData ret = new TLSkillStatusData();
                ret.SkillTimestampMSMap = new HashMap<int, long>();
                //生成同步时间戳.
                long SkillTimestampMS = GetTimestampMS();
                IEnumerable<InstanceUnit.SkillState> sslist = mOwner.mUnit.SkillStatus;
                //主动技能.
                foreach (InstanceUnit.SkillState s in sslist)
                {
                    ret.SkillTimestampMSMap.Add(s.ID, SkillTimestampMS + s.TotalCDTime - s.PassTime);
                }

                return ret;
            }

            public GameSkill GetGameSkill(int id)
            {
                GameSkill ret = null;
                mSkillMap.TryGetValue(id, out ret);
                return ret;
            }

            public List<GameSkill> GameSkills()
            {
                List<GameSkill> lt = new List<GameSkill>();

                foreach (var item in mSkillMap)
                {
                    lt.Add(item.Value);
                }

                return lt;
            }

            public List<TLGameSkillSnap> SkillLoopList
            {
                get { return mLoopList; }
                set
                {
                    mLoopList = value;
                    mLoopListIndex = 0;
                }
            }

            public InstanceUnit.SkillState GetLoopSkill()
            {
                if (mLoopList == null || mLoopListIndex >= mLoopList.Count) return null;
                var ret = mOwner.mUnit.getSkillState(mLoopList[mLoopListIndex].SkillID);

                if (ret.IsCD)
                {
                    //如果在CD.调用普攻.
                    ret = mOwner.mUnit.getSkillState(mOwner.mUnit.DefaultSkill.ID);
                }

                return ret;
            }

            private void MUnit_OnLaunchSkill(InstanceUnit obj, InstanceUnit.SkillState skill)
            {
                if (mLoopList != null && mLoopListIndex < mLoopList.Count)
                {
                    if (skill.ID == mLoopList[mLoopListIndex].SkillID)
                    {
                        mLoopListIndex++;
                        mLoopListIndex %= mLoopList.Count;
                    }
                }
            }

            private void AddScript(int id, UnitSkill us, bool activeSkill)
            {
                if (activeSkill)
                {
                    SkillScriptMap.Add(id, us);
                }
                else
                {
                    //未实现.
                }
            }

            /// <summary>
            /// 添加技能.
            /// </summary>
            /// <param name="list"></param>
            public void AddSkill(List<GameSkill> list)
            {
                for (int index = 0; index < list.Count; index++)
                {
                    AddSkill(list[index]);
                }
            }
            public void AddSkill(GameSkill gs)
            {
                UnitSkill us = null;
                SkillTemplate st = null;

                InitUnitSkillScript(gs, out us, out st);
                if (st != null && us != null)
                {

                    mSkillMap.Add(gs.SkillID, gs);
                    mOwner.mUnit.AddSkill(st);
                    SetSkillCD(st.ID, gs.SkillTimestampMS);
                }

                us.InitOver(mOwner);
            }

            /// <summary>
            /// 移除技能.
            /// </summary>
            /// <param name="list"></param>
            public void RemoveSkill(List<GameSkill> list)
            {
                if (list != null)
                {
                    for (int index = 0; index < list.Count; index++)
                    {
                        RemoveActiveSkill(list[index]);
                    }
                }
            }

            /// <summary>
            /// 重置技能.
            /// </summary>
            /// <param name="list"></param>
            public void ResetSkill(List<GameSkill> list)
            {
                mOwner.mProp.ServerData.SkillInfo.Skills = list;
                //1清除所有技能.
                ClearSkill(null);
                //2重新初始化技能.
                InitAllSkill(list);
            }

            /// <summary>
            /// 替换技能.
            /// </summary>
            /// <param name="list"></param>
            public void ReplaceSkill(List<GameSkill> list)
            {
                UnitSkill us = null;
                GameSkill gs = null;
                SkillTemplate st = null;

                for (int index = 0; index < list.Count; index++)
                {
                    gs = list[index];
                    var dst = GetGameSkill(gs.SkillID);

                    if (dst != null)
                    {
                        dst.SkillLevel = gs.SkillLevel;
                        dst.TalentLevel = gs.TalentLevel;
                    }

                    var ss = mOwner.mUnit.getSkillState(gs.SkillID);
                    if (ss != null)
                    {
                        st = ss.Data;
                        //脚本修改.
                        if (SkillScriptMap.TryGetValue(gs.SkillID, out us) && st != null)
                        {
                            us.Reset(gs, mOwner, ref st);
                        }
                    }
                }
            }

            /// <summary>
            /// 变更技能.
            /// </summary>
            /// <param name="list"></param>
            public void ActiveSkill(HashMap<int, GameSkill> list, List<int> keeps, bool op)
            {
                //判断要激活的技能是否存在.
                GameSkill gs = null;
                if (op == true)
                {
                    List<GameSkill> addLt = new List<GameSkill>();
                    foreach (var item in list)
                    {
                        gs = mOwner.SkillModule.GetGameSkill(item.Key);
                        if (gs == null)
                        {
                            addLt.Add(item.Value);
                        }
                    }
                    //激活没有的技能先执行添加.
                    if (addLt.Count > 0)
                    {
                        AddSkill(addLt);
                    }
                }

                //自定义数据，记录哪些技能激活，哪些技能冻结.
                List<SkillState> s = mOwner.mUnit.SkillStatus as List<SkillState>;
                int id = 0;
                for (int i = 0; i < s.Count; i++)
                {
                    id = s[i].Data.ID;
                    if (keeps != null && keeps.Contains(id))
                    {
                        continue;
                    }
                    else if (list.ContainsKey(id))
                    {
                        mOwner.mUnit.SetSkillActive(id, op, false);
                    }
                    else
                    {
                        mOwner.mUnit.SetSkillActive(id, !op, false);
                    }
                }
            }

            private void MapAddSkill(List<GameSkill> list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    mSkillMap.Add(list[i].SkillID, list[i]);
                }
            }

            private void InitPurAtkScript()
            {
                SkillTemplate st = null;
                TLBattleSkill.GetPurAtkScript().Init(new GameSkill(), mOwner, ref st);
            }

            private void UnInitPurAtkScript()
            {
                mOwner.RemoveEventBySkillID(0);
            }

            public void ResetSkillScript()
            {
                SkillState ss = null;
                SkillTemplate st = null;
                UnitSkill us = null;
                foreach (var item in mSkillMap)
                {
                    ss = mOwner.mUnit.getSkillState(item.Value.SkillID);
                    if (ss != null)
                        st = ss.Data;

                    if (st != null && SkillScriptMap.TryGetValue(item.Value.SkillID, out us))
                    {
                        us.Reset(item.Value, mOwner, ref st);
                    }
                }
            }
        }

        #endregion

        #region TLUnitAutoRecoverModule 自我回复.

        public class TLUnitAutoRecoverModule
        {
            private TLVirtual mOwner = null;

            private int recover_hp;
            private TimeInterval<int> timer = null;
            private bool dispose = false;

            public TLUnitAutoRecoverModule(TLVirtual owner)
            {
                mOwner = owner;
            }

            public void Init()
            {
                recover_hp = mOwner.MirrorProp.AutoRecoverHp;

                if (recover_hp != 0)
                    timer = new TimeInterval<int>(TLEditorConfig.Instance.AUTO_RECOVER_HP_CYCLETIME);
            }

            public void Dispose()
            {
                dispose = true;
                timer = null;
            }

            public void Update(int intervalMS)
            {
                if (dispose == false && timer != null && timer.Update(intervalMS))
                {
                    if (mOwner.mUnit.IsDead == false)
                    {
                        mOwner.AddHP(recover_hp, mOwner.mUnit, false);
                    }
                }
            }
        }

        #endregion

        #region TLBattlePropModule 单位属性(改变/计算/同步/).

        public class TLBattlePropModule
        {
            public const float PER = 10000.0f;
            private TLVirtual mOwner = null;
            private bool IsDispose = false;
            private int ChangeOperationCount = 0;
            private HashMap<int, TLPropObject> OperationMap = new HashMap<int, TLPropObject>();
            private HashMap<TLPropObject.PropType, int> ChangeMask = new HashMap<TLPropObject.PropType, int>();
            private TLUnitProp mMirrorProp;

            public delegate void BattlePropChangeHandler(HashMap<TLPropObject.PropType, int> map);
            public event BattlePropChangeHandler OnBattlePropChange;

            public TLBattlePropModule(TLVirtual owner)
            {
                mOwner = owner;
            }

            public void Init()
            {
                var prop = mOwner.mProp.ServerData.Prop;
                mMirrorProp = prop.Clone() as TLUnitProp;
                UpdateMirrorProp();
                SyncAllBattleProps();
            }

            public void Dispose()
            {
                if (IsDispose) { return; }
                OnBattlePropChange = null;
                IsDispose = true;
            }

            public void Update(int intervalMS)
            {
                SyncBattleProps();
            }

            /// <summary>
            /// 同步所有属性.
            /// </summary>
            public void SyncAllBattleProps()
            {
                HashMap<TLPropObject.PropType, int> map = new HashMap<TLPropObject.PropType, int>();
                var mProp = mOwner.mProp.ServerData.Prop;

                //CurHP不添加.
                SetPropChangeMask(TLPropObject.PropType.maxhp, mProp.MaxHP);

                SetPropChangeMask(TLPropObject.PropType.attack, mProp.Attack);
                SetPropChangeMask(TLPropObject.PropType.defend, mProp.PhyDef);
                SetPropChangeMask(TLPropObject.PropType.mdef, mProp.MagDef);

                SetPropChangeMask(TLPropObject.PropType.hit, mProp.Hit);
                SetPropChangeMask(TLPropObject.PropType.dodge, mProp.Dodge);
                SetPropChangeMask(TLPropObject.PropType.crit, mProp.Crit);
                SetPropChangeMask(TLPropObject.PropType.rescrit, mProp.ResCrit);

                SetPropChangeMask(TLPropObject.PropType.cridamageper, mProp.CriDamagePer);
                SetPropChangeMask(TLPropObject.PropType.runspeed, mProp.RunSpeed);

                SetPropChangeMask(TLPropObject.PropType.autorecoverhp, mProp.AutoRecoverHp);

                SetPropChangeMask(TLPropObject.PropType.totalreducedamageper, mProp.TotalReduceDamagePer);
                SetPropChangeMask(TLPropObject.PropType.totaldamageper, mProp.TotalDamagePer);

                SetPropChangeMask(TLPropObject.PropType.firedamage, mProp.FireDmage);
                SetPropChangeMask(TLPropObject.PropType.thunderdamage, mProp.ThunderDamage);
                SetPropChangeMask(TLPropObject.PropType.soildamage, mProp.SoilDamage);
                SetPropChangeMask(TLPropObject.PropType.icedamage, mProp.IceDamage);

                SetPropChangeMask(TLPropObject.PropType.fireresist, mProp.FireResist);
                SetPropChangeMask(TLPropObject.PropType.thunderresist, mProp.ThunderResist);
                SetPropChangeMask(TLPropObject.PropType.soilresist, mProp.SoilResist);
                SetPropChangeMask(TLPropObject.PropType.iceresist, mProp.IceResist);

                SetPropChangeMask(TLPropObject.PropType.allelementdamage, mProp.AllelementDamage);
                SetPropChangeMask(TLPropObject.PropType.allelementresist, mProp.AllelementResist);

                SetPropChangeMask(TLPropObject.PropType.onhitrecoverhp, mProp.OnHitRecoverHP);
                SetPropChangeMask(TLPropObject.PropType.killrecoverhp, mProp.KillRecoverHP);

                SetPropChangeMask(TLPropObject.PropType.extragoldper, mProp.ExtraGoldPer);
                SetPropChangeMask(TLPropObject.PropType.extraexpper, mProp.ExtraEXPPer);

                SetPropChangeMask(TLPropObject.PropType.targettomonster, mProp.TargetToMonster);
                SetPropChangeMask(TLPropObject.PropType.targettoplayer, mProp.TargetToPlayer);

                SetPropChangeMask(TLPropObject.PropType.winddamage, mProp.WindDamage);
                SetPropChangeMask(TLPropObject.PropType.windresist, mProp.WindResist);

                SetPropChangeMask(TLPropObject.PropType.through, mProp.Through);
                SetPropChangeMask(TLPropObject.PropType.block, mProp.Block);

                SetPropChangeMask(TLPropObject.PropType.redcridamageper, mProp.RedCriDamagePer);
                SetPropChangeMask(TLPropObject.PropType.goddamage, mProp.GodDamage);
                SetPropChangeMask(TLPropObject.PropType.defreduction, mProp.DefReduction);
                SetPropChangeMask(TLPropObject.PropType.mdefreduction, mProp.MDefReduction);
                SetPropChangeMask(TLPropObject.PropType.extracrit, mProp.Extracrit);
            }

            public int AddPropObject(TLPropObject obj)
            {
                if (obj == null || IsDispose) { return -1; }

                int id = GetOperationID();
                OperationMap.Add(id, obj);
                CalPropsChange(obj);

                return id;
            }

            public void RemovePropObject(int id)
            {
                if (OperationMap.Count == 0 || IsDispose) { return; }

                TLPropObject obj = null;

                if (OperationMap.TryGetValue(id, out obj))
                {
                    obj.Value = -obj.Value;
                    CalPropsChange(obj);
                    OperationMap.Remove(id);
                }
            }

            private int GetOperationID()
            {
                ChangeOperationCount++;
                return ChangeOperationCount;
            }

            private void CalPropsChange(TLPropObject obj)
            {
                var prop = mOwner.mProp.ServerData.Prop;

                int ret = 0;

                ret = ChangeProp(prop, mMirrorProp, obj);
                //哪些属性产生变更.
                SetPropChangeMask(obj.Prop, ret);
                UpdateMirrorProp();
            }

            private void CalAllPropsChange(HashMap<int, TLPropObject> map)
            {
                var src = mOwner.mProp.ServerData.Prop;
                mMirrorProp = src.Clone() as TLUnitProp;
                int ret = 0;

                foreach (var kvp in map)
                {
                    ret = ChangeProp(src, mMirrorProp, kvp.Value);
                    SetPropChangeMask(kvp.Value.Prop, ret);
                }

                UpdateMirrorProp();
            }

            private void UpdateMirrorProp()
            {
                SyncMoveSpeed();
                SyncMaxHPMP();
            }

            private void SyncMoveSpeed()
            {
                //速度放大100倍，需要除100.
                float speed = mMirrorProp.RunSpeed / 100.0f;
                this.mOwner.mUnit.SetMoveSpeed(speed);
            }

            private void SyncMaxHPMP()
            {
                mOwner.mUnit.SetMaxHP(mMirrorProp.MaxHP, true);
            }

            public TLUnitProp GetMirrorProp()
            {
                return mMirrorProp;
            }

            private int GetValue(int v, TLPropObject obj)
            {
                int ret = 0;
                if (obj.Type == TLPropObject.ValueType.Percent)
                {
                    ret = (int)(v * (obj.Value / PER));
                }
                else
                {
                    ret = obj.Value;
                }
                return ret;
            }

            private int ChangeProp(TLUnitProp src, TLUnitProp dist, TLPropObject obj)
            {
                int ret = 0;
                switch (obj.Prop)
                {
                    case TLPropObject.PropType.maxhp:
                        dist.MaxHP += GetValue(src.MaxHP, obj);
                        ret = dist.MaxHP;
                        break;
                    case TLPropObject.PropType.attack:
                        dist.Attack += GetValue(src.Attack, obj);
                        ret = dist.Attack;
                        break;
                    case TLPropObject.PropType.defend:
                        dist.PhyDef += GetValue(src.PhyDef, obj);
                        ret = dist.PhyDef;
                        break;
                    case TLPropObject.PropType.mdef:
                        dist.MagDef += GetValue(src.MagDef, obj);
                        ret = dist.MagDef;
                        break;
                    case TLPropObject.PropType.hit:
                        dist.Hit += GetValue(src.Hit, obj);
                        ret = dist.Hit;
                        break;
                    case TLPropObject.PropType.dodge:
                        dist.Dodge += GetValue(src.Dodge, obj);
                        ret = dist.Dodge;
                        break;
                    case TLPropObject.PropType.crit:
                        dist.Crit += GetValue(src.Crit, obj);
                        ret = dist.Crit;
                        break;
                    case TLPropObject.PropType.rescrit:
                        dist.ResCrit += GetValue(src.ResCrit, obj);
                        ret = dist.ResCrit;
                        break;
                    case TLPropObject.PropType.cridamageper:
                        dist.CriDamagePer += GetValue(src.CriDamagePer, obj);
                        ret = dist.CriDamagePer;
                        break;
                    case TLPropObject.PropType.runspeed:
                        dist.RunSpeed += GetValue(src.RunSpeed, obj);
                        ret = dist.RunSpeed;
                        break;

                    case TLPropObject.PropType.autorecoverhp:
                        dist.AutoRecoverHp += GetValue(src.AutoRecoverHp, obj);
                        ret = dist.AutoRecoverHp;
                        break;
                    case TLPropObject.PropType.totalreducedamageper:
                        dist.TotalReduceDamagePer += GetValue(src.TotalReduceDamagePer, obj);
                        ret = dist.TotalReduceDamagePer;
                        break;
                    case TLPropObject.PropType.totaldamageper:
                        dist.TotalDamagePer += GetValue(src.TotalDamagePer, obj);
                        ret = dist.TotalDamagePer;
                        break;

                    case TLPropObject.PropType.firedamage:
                        dist.FireDmage += GetValue(src.FireDmage, obj);
                        ret = dist.FireDmage;
                        break;
                    case TLPropObject.PropType.thunderdamage:
                        dist.ThunderDamage += GetValue(src.ThunderDamage, obj);
                        ret = dist.ThunderDamage;
                        break;
                    case TLPropObject.PropType.soildamage:
                        dist.SoilDamage += GetValue(src.SoilDamage, obj);
                        ret = dist.SoilDamage;
                        break;
                    case TLPropObject.PropType.icedamage:
                        dist.IceDamage += GetValue(src.IceDamage, obj);
                        ret = dist.IceDamage;
                        break;
                    case TLPropObject.PropType.fireresist:
                        dist.FireResist += GetValue(src.FireResist, obj);
                        ret = dist.FireResist;
                        break;
                    case TLPropObject.PropType.thunderresist:
                        dist.ThunderResist += GetValue(src.ThunderResist, obj);
                        ret = dist.ThunderResist;
                        break;
                    case TLPropObject.PropType.soilresist:
                        dist.SoilResist += GetValue(src.SoilResist, obj);
                        ret = dist.SoilResist;
                        break;
                    case TLPropObject.PropType.iceresist:
                        dist.IceResist += GetValue(src.IceResist, obj);
                        ret = dist.IceResist;
                        break;
                    case TLPropObject.PropType.allelementdamage:
                        dist.AllelementDamage += GetValue(src.AllelementDamage, obj);
                        ret = dist.AllelementDamage;
                        break;
                    case TLPropObject.PropType.allelementresist:
                        dist.AllelementResist += GetValue(src.AllelementResist, obj);
                        ret = dist.AllelementResist;
                        break;
                    case TLPropObject.PropType.onhitrecoverhp:
                        dist.OnHitRecoverHP += GetValue(src.OnHitRecoverHP, obj);
                        ret = dist.OnHitRecoverHP;
                        break;

                    case TLPropObject.PropType.killrecoverhp:
                        dist.KillRecoverHP += GetValue(src.KillRecoverHP, obj);
                        ret = dist.KillRecoverHP;
                        break;

                    case TLPropObject.PropType.extragoldper:
                        dist.ExtraGoldPer += GetValue(src.ExtraGoldPer, obj);
                        ret = dist.ExtraGoldPer;
                        break;
                    case TLPropObject.PropType.extraexpper:
                        dist.ExtraEXPPer += GetValue(src.ExtraEXPPer, obj);
                        ret = dist.ExtraEXPPer;
                        break;

                    case TLPropObject.PropType.targettomonster:
                        dist.TargetToMonster += GetValue(src.TargetToMonster, obj);
                        ret = dist.TargetToMonster;
                        break;
                    case TLPropObject.PropType.targettoplayer:
                        dist.TargetToPlayer += GetValue(src.TargetToPlayer, obj);
                        ret = dist.TargetToPlayer;
                        break;

                    case TLPropObject.PropType.through:
                        dist.Through += GetValue(src.Through, obj);
                        ret = dist.Through;
                        break;
                    case TLPropObject.PropType.block:
                        dist.Block += GetValue(src.Block, obj);
                        ret = dist.Block;
                        break;
                    case TLPropObject.PropType.winddamage:
                        dist.WindDamage += GetValue(src.WindDamage, obj);
                        ret = dist.WindDamage;
                        break;
                    case TLPropObject.PropType.windresist:
                        dist.WindResist += GetValue(src.WindResist, obj);
                        ret = dist.WindResist;
                        break;
                    case TLPropObject.PropType.redcridamageper:
                        dist.RedCriDamagePer += GetValue(src.RedCriDamagePer, obj);
                        ret = dist.RedCriDamagePer;
                        break;
                    case TLPropObject.PropType.goddamage:
                        dist.GodDamage += GetValue(src.GodDamage, obj);
                        ret = dist.GodDamage;
                        break;
                    case TLPropObject.PropType.defreduction:
                        dist.DefReduction += GetValue(src.DefReduction, obj);
                        ret = dist.DefReduction;
                        break;
                    case TLPropObject.PropType.mdefreduction:
                        dist.MDefReduction += GetValue(src.MDefReduction, obj);
                        ret = dist.DefReduction;
                        break;
                    case TLPropObject.PropType.extracrit:
                        dist.Extracrit += GetValue(src.Extracrit, obj);
                        ret = dist.Extracrit;
                        break;
                    default:
                        throw new System.Exception("TLBattlePropModule.ChangeProps Can not find Target Props" + obj.Prop);
                }

                return ret;
            }

            private void SetPropChangeMask(TLPropObject.PropType type, int v)
            {
                ChangeMask.Put(type, v);
            }

            private void SyncBattleProps()
            {
                if (ChangeMask.Count != 0)
                {
                    if (OnBattlePropChange != null)
                    {
                        OnBattlePropChange.Invoke(ChangeMask);
                    }

                    ChangeMask.Clear();
                }
            }

            public void PropChange(TLUnitProp prop)
            {
                if (prop != null && IsDispose == false)
                {
                    mOwner.mProp.ServerData.Prop = prop;
                    SyncAllBattleProps();
                    CalAllPropsChange(OperationMap);
                }
            }
        }

        #endregion
    }
}
