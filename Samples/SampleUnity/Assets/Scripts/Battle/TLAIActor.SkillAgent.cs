using Assets.Scripts;
using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using SLua;
using System;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using UnityEngine;

public partial class TLAIActor : SkillTouchHandler.SkillLaunchListener
{
    [DoNotToLua]
    public delegate void SkillChangedHandler(PlayerSkillChangedEvent evt);
    [DoNotToLua]
    public SkillChangedHandler OnSkillChanged;
    [DoNotToLua]
    public ComAIUnit.SkillActionStatus showingSkillStatus;  // 正在读条的技能
    private SkillTargetSelect mSkillTargetSelect;
    private SkillTouchHandler mSkillTouchHandler;
    private static GameObject mSelectorPrefab;

    [DoNotToLua]
    public void GetSkillStatus(List<ZoneUnit.SkillState> ret)
    {
        ZActor.GetSkillStatus(ret);
    }

    private void InitSkillSelector()
    {
        //初始化技能目标选择器
        mSkillTouchHandler = new SkillTouchHandler(this);
        GameObject selector = GameObject.Find("SkillTargetSelect");
        if (selector == null)
        {
            if (mSelectorPrefab == null)
                mSelectorPrefab = Resources.Load("Prefabs/SkillTargetSelect") as GameObject;
            selector = GameObject.Instantiate(mSelectorPrefab) as GameObject;
            selector.name = "SkillTargetSelect";
        }
        //selector.transform.parent = this.ObjectRoot.transform;
        selector.transform.localPosition = Vector3.zero;
        selector.transform.localRotation = Quaternion.identity;
        mSkillTargetSelect = selector.GetComponent<SkillTargetSelect>();
        mSkillTargetSelect.SetActive(false);
    }

    //初始化技能
    [DoNotToLua]
    public void InitSkill(PlayerSkillInfoEventB2C evt)
    {
        List<GameSkill> skills = evt.SkillList;
        if (evt != null && skills != null)
        {
            List<SkillBarHud.SkillData> skillsData = new List<SkillBarHud.SkillData>();
            ArrayList<LaunchSkill> skilllist = new ArrayList<LaunchSkill>();
            DeepCore.GameData.Zone.ZoneEditor.EditorTemplates templates = TLClient.TLClientBattleManager.DataRoot;
            foreach (GameSkill gs in skills)
            {
                LaunchSkill ls = new LaunchSkill();
                ls.AutoLaunch = true;
                ls.SkillID = gs.SkillID;
                skilllist.Add(ls);
                if (gs.SkillType == GameSkill.TLSkillType.normalAtk)
                {
                    this.ZUnit.Info.BaseSkillID = ls;
                }

                SkillKeyStruct ss = new SkillKeyStruct();
                if (gs.SkillID != -1 && gs.SkillID != -2)
                {
                    SkillTemplate st = templates.Templates.GetSkill(gs.SkillID);
                    ss.icon = st.IconName;
                    ss.name = st.Name;
                }
                ss.baseSkillId = gs.SkillID;
                ss.advancedSkillId = gs.SkillID;
                ss.keyPos = gs.SkillIndex;
                ss.skillType = gs.SkillType;
                ss.buffId = gs.SkillType == GameSkill.TLSkillType.God ? evt.BuffID : -1;
                if (gs.SkillID == -2)
                {
                    ss.flag = (int)TL.UGUI.Skill.SkillButton.IconType.Lock;
                    ss.unlockLevel = gs.SkillLevel;
                }
                else
                {
                    ss.flag = (int)TL.UGUI.Skill.SkillButton.IconType.Icon;
                    ss.unlockLevel = 0;
                }
                SkillBarHud.SkillData sd = new SkillBarHud.SkillData(ss);
                skillsData.Add(sd);
            }
            this.ZUnit.Info.Skills = new ArrayList<LaunchSkill>(skilllist);
            DataMgr.Instance.UserData.SetAttribute(UserData.NotiFyStatus.SKILLDATA, skillsData);

            if (OnSkillChanged != null)
            {
                OnSkillChanged.Invoke(new PlayerSkillChangedEvent());
            }
        }
    }

    [DoNotToLua]
    public override void ShowSkillAction(SkillActionStatus status, UnitActionData action)
    {
        base.ShowSkillAction(status, action);

        //TLSkillProperties property = (TLSkillProperties)status.SkillAction.SkillData.Properties;
        //if (property.progressType != TLSkillProperties.SkillProgressType.Normal && status.Skill.PassTimeMS < property.progressTimeMS)
        //{
        //    showingSkillStatus = status;
        //    //之后这里要重新实现，先留着用用
        //    HudManager.Instance.SkillProcess.ShowSkillAction(this, status, property.progressTimeMS);
        //}
        //else
        {
            // TODO 改到吟唱结束事件触发循环
            showingSkillStatus = null;
            if (waitingLaunchSkill != null)
            {
                LaunchSkillWithTarget(waitingLaunchSkill.Data.ID, Target.TargetId);
                waitingLaunchSkill = null;
            }
        }
    }
    [DoNotToLua]
    public bool GetShowSkillPasstime(out ComAIUnit.SkillActionStatus status, out float elapseTime, out float totalTime)
    {
        status = null;
        elapseTime = 0;
        totalTime = 0;

        if (CurrentActionStatus != showingSkillStatus)
        {
            return false;
        }
        // 
        //         int passTime = showingSkillStatus.SkillAction.pa;
        // 
        //         status = showingSkillStatus;
        //         TLSkillProperties property = (TLSkillProperties)status.Data.Properties;

        //if (passTime < property.progressTimeMS)
        //{
        //    elapseTime = (float)passTime / 1000f;
        //    totalTime = (float)property.progressTimeMS / 1000f;

        //    return true;
        //}

        return false;
    }

    ZoneUnit.SkillState waitingLaunchSkill;

    private uint LastSpellTargetUUID;
    private Vector3 LastSpellTargetPos;
    //private bool LastSpellTargetIsBoss;

    private Vector3 GetLastSpellTargetPos(out bool targetAlive)
    {
        targetAlive = false;
        if (LastSpellTargetUUID != 0)
        {
            var unit = this.BattleScene.GetBattleObject(LastSpellTargetUUID) as TLAIUnit;
            if (unit != null)
            {
                targetAlive = unit.HP > 0;
                LastSpellTargetPos = unit.Position;
            }
        }
        return LastSpellTargetPos;
    }

    /// <summary>
    /// 自动选目标
    /// </summary>
    /// <param name="tId">指定目标（当指定了目标时，第二个参数无效）</param>
    /// <param name="isNext">true:下一个 false:前一个</param>
    [DoNotToLua]
    public void ChangeTarget(uint tId, bool isNext)
    {
        //67说要选中自己 
        //if (tId == ZUnit.ObjectID)
        //{
        //    return;
        //}

        uint targetId = 0;

        if (tId != 0)
        {
            targetId = tId;
        }
        else
        {
            targetId = Target.ChangeTargetToNearEnemy(this, AtkRange, AtkAngle, isNext);
        }

        if (targetId > 0)
        {
            Target.SetTarget(targetId);
        }
    }
    [DoNotToLua]
    public const float AtkRange = 45;
    [DoNotToLua]
    public const float AtkAngle = 360;
    [DoNotToLua]
    public SkillTemplate mSkillTemplate = null;

    private ZoneUnit.SkillState GetSkillState(int skillIndex)
    {
        int skillId = GetSkillIDByIndex(skillIndex);
        return this.ZUnit.GetSkillState(skillId);
    }

    /// <summary>
    /// 外部调用技能施放入口.
    /// </summary>
    /// <param name="skillIndex"></param>
    /// <param name="simpleMode"></param>
    /// <returns></returns>
    [DoNotToLua]
    public uint ReadyToLaunchSkillByIndex(int skillIndex, bool simpleMode)
    {
        int skillId = GetSkillIDByIndex(skillIndex);
        return ReadyToLaunchSkillById(skillId, simpleMode);
    }

    /// <summary>
    /// 遥感调用.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="px"></param>
    /// <param name="py"></param>
    [DoNotToLua]
    public void OnSkillRockerStop(float x, float y, float px, float py)
    {
        DoSkillRockerStop(x, y, px, py);
        return;

        /*
        if (OnSkillRockerStopHandler != null)
        {
            OnSkillRockerStopHandler.Invoke(x, y, px, py);
        }
        */
    }

    private void DoSkillRockerStop(float x, float y, float px, float py)
    {
        Vector2 fingerPos = new Vector2(x, y);

        //如果鼠标挪到了取消上抬起应该取消
        if (HudManager.Instance.SkillBar.IsInCancelBtnArea(new Vector2(px, py)))
        {
            HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(false);
        }
        else
        {
            HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(false);
            float angle = Mathf.Atan2(y, x);
            DeepCore.Vector.Vector2 v = new DeepCore.Vector.Vector2();
            TLSkillProperties zsp = (TLSkillProperties)mSkillTemplate.Properties;
            if (x == 0 && y == 0)
            {
                //现在箭头指哪里打哪里
                Vector3 zzz = mSkillTargetSelect.Direction.eulerAngles;
                v = new DeepCore.Vector.Vector2(ZActor.X, ZActor.Y);
                if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Arrow || zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Fan)
                {
                    DeepCore.Vector.MathVector.movePolar(v, (zzz.y - 90) * Mathf.Deg2Rad, mSkillTemplate.AttackRange);
                    StartLaunchSkill(mSkillTemplate.ID, Target.TargetId, v);
                }
                else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Area)
                {
                    var temp = mSkillTargetSelect.CircleDamageRange.transform.position;
                    Vector3 target = Vector3.MoveTowards(ObjectRoot.transform.position, temp, mSkillTemplate.AttackRange);
                    Vector2 zPos = DeepCore.Unity3D.Utils.Extensions.UnityPos2ZonePos((this.BattleScene as TLBattleScene).TotalHeight, temp);
                    v.X = zPos.x;
                    v.Y = zPos.y;
                    StartLaunchSkill(mSkillTemplate.ID, Target.TargetId, v);
                }
                else if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_None)
                {
                    if (mSkillTemplate != null && zsp.TargetType == TLSkillProperties.TLSkillTargetType.None)
                    {
                        StartLaunchSkill(mSkillTemplate.ID, 0);
                    }
                    else
                    {
                        StartLaunchSkill(mSkillTemplate.ID, Target.TargetId);
                    }
                }
            }
            else
            {
                if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_None)
                {
                    if (mSkillTemplate != null && zsp.TargetType == TLSkillProperties.TLSkillTargetType.None)
                    {
                        StartLaunchSkill(mSkillTemplate.ID, 0); //无目标无位置
                    }
                    else
                    {
                        StartLaunchSkill(mSkillTemplate.ID, Target.TargetId);   //有目标无位置
                    }
                }
                else
                {
                    Vector3 pos = ObjectRoot.transform.position;
                    Vector3 v2 = Camera.main.WorldToScreenPoint(pos);//人物屏幕位置 
                    Vector3 v3 = v2 + new Vector3(x, y, 0) * 1000;//移动后屏幕位置  
                    Vector3 v4 = Camera.main.ScreenToWorldPoint(v3);//移动后位置 
                    v4.y = pos.y;
                    Vector3 v5 = (v4 - pos).normalized;
                    Vector3 v6 = v5 * (new Vector3(x, 0, y)).magnitude;
                    //Debug.Log("OnRockerMove " + new Vector3(x, 0, y) + " " + (new Vector3(x, 0, y)).magnitude);
                    Vector2 fingerPos2 = new Vector2(v6.x, v6.z);
                    Vector3 target = SetTargetPosition(pos, fingerPos2, mSkillTemplate.AttackRange);
                    target.y = pos.y;
                    Vector2 zPos = DeepCore.Unity3D.Utils.Extensions.UnityPos2ZonePos((this.BattleScene as TLBattleScene).TotalHeight, target);
                    v = new DeepCore.Vector.Vector2(zPos.x, zPos.y);

                    //Ray ray = GameSceneMgr.Instance.SceneCamera.ScreenPointToRay(new Vector2(px, py));
                    //RaycastHit hit;
                    //Physics.Raycast(ray, out hit, 10000, 1 << (int)PublicConst.LayerSetting.STAGE_NAV);
                    //if (hit.collider != null)
                    //{
                    //    Vector3 target = hit.point;
                    //    if (Vector3.Distance(target, pos) > mSkillTemplate.AttackRange)
                    //    {
                    //        Vector3 dir = (target - pos).normalized;
                    //        target = pos + dir * mSkillTemplate.AttackRange;
                    //    }

                    //    Vector2 p = DeepCore.Unity3D.Utils.Extensions.UnityPos2ZonePos((this.BattleScene as TLBattleScene).TotalHeight, target);
                    //    v = new DeepCore.Vector.Vector2(p.x, p.y);
                    //}

                    StartLaunchSkill(mSkillTemplate.ID, 0, v);  //无目标有位置
                }
            }
        }

        StopPreLaunchSkill();
    }

    /// <summary>
    /// 遥感调用.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="px"></param>
    /// <param name="py"></param>
    [DoNotToLua]
    public void OnSkillRockerMove(float x, float y, float px, float py)
    {
        if (x == 0 && y == 0) return;

        DoSkillRockerMove(x, y, px, py);
        return;

        /*
        ChangeManualAttackState();

        if (OnSkillRockerMoveHandler != null)
        {
            OnSkillRockerMoveHandler.Invoke(x, y, px, py);
        }
        */
    }

    private void DoSkillRockerMove(float x, float y, float px, float py)
    {
        Vector3 pos = ObjectRoot.transform.position;
        Vector3 v2 = Camera.main.WorldToScreenPoint(pos);//人物屏幕位置 
        Vector3 offset = new Vector3(x, y, 0) * 1000;
        Vector3 v3 = v2 + offset;//移动后屏幕位置  
        Vector3 v4 = Camera.main.ScreenToWorldPoint(v3);//移动后位置 
        v4.y = pos.y;
        Vector3 v5 = (v4 - pos).normalized;
        Vector3 v6 = v5 * (new Vector3(x, 0, y)).magnitude;
        float dis = (new Vector3(x, 0, y)).magnitude * mSkillTemplate.AttackRange;
        Vector3 target = pos + v5 * dis;
        //Vector2 fingerPos = new Vector2(v6.x, v6.z);
        //Vector3 target = SetTargetPosition(pos, fingerPos, mSkillTemplate.AttackRange);
        target.y = pos.y;
        mSkillTargetSelect.Update(target, pos, HudManager.Instance.SkillBar.IsInCancelBtnArea(new Vector2(px, py)));

        //Vector3 target = Vector3.zero;
        //bool isCancel = false;
        //Ray ray = GameSceneMgr.Instance.SceneCamera.ScreenPointToRay(new Vector2(px, py));
        //RaycastHit hit;
        //Physics.Raycast(ray, out hit, 10000, 1 << (int)PublicConst.LayerSetting.STAGE_NAV);
        //if (hit.collider != null)
        //{
        //    target = hit.point;
        //    if (Vector3.Distance(target, pos) > mSkillTemplate.AttackRange)
        //    {
        //        Vector3 dir = (target - pos).normalized;
        //        target = pos + dir * mSkillTemplate.AttackRange;
        //        isCancel = true;
        //    }
        //}

        //mSkillTargetSelect.Update(target, pos, isCancel ? true : HudManager.Instance.SkillBar.IsInCancelBtnArea(new Vector2(px, py)));
    }

    private int GetSkillIDByIndex(int index)
    {
        if (index == 0)
        {
            return Info.BaseSkillID.SkillID;
        }

        if (index >= Info.Skills.Count)
        {
            return -1;
        }

        return Info.Skills[index].SkillID;
    }

    private uint ReadyToLaunchSkillById(int skillID, bool simpleMode)
    {
        //判断当前是否有该技能.
        ZoneUnit.SkillState state = this.ZUnit.GetSkillState(skillID);
        uint targetID = 0;
        ReadyToLaunchSkillByState(state, simpleMode, ref targetID);
        return targetID;
    }

    private bool ReadyToLaunchSkillByState(ZoneUnit.SkillState state, bool simpleMode, ref uint targetID)
    {
        mSkillTemplate = null;
        if (state == null) return false;

        int skillID = state.Data.ID;
        #region 可取消技能特殊判断.

        //当前技能状态.
        ZoneUnit.ISkillAction curSkillAction = this.ZUnit.CurrentSkillAction;

        if (curSkillAction != null)
        {
            //自己取消自己的技能，施放期间其他技能不响应.
            if (curSkillAction.SkillData.IsManuallyCancelable == true && skillID != curSkillAction.SkillData.ID)
            {
                return false;
            }
        }
        #endregion

        #region 判断是否在CD中.

        if (state.IsCD)
        {
            if (state.Data.IsManuallyCancelable && curSkillAction == null && curSkillAction.SkillData.ID == state.Data.ID)
            {
                //可自我的技能不拦截.
            }
            else
                return false;
        }

        //变身技能释放中
        SkillBarHud.SkillData sd = HudManager.Instance.SkillBar.GetSkillData(skillID);
        if (sd.Data.skillType == TLBattle.Common.Plugins.GameSkill.TLSkillType.God)
        {
            var data = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
            object obj = null;
            data.TryGetValue("use_god", out obj);
            if (obj != null)
            {
                int v = Convert.ToInt32(obj);
                if (v == 0)
                {
                    var str = HZLanguageManager.Instance.GetString("scene_not_use_god");
                    GameAlertManager.Instance.ShowFloatingTips(str);
                    return false;
                }
            }


            ZoneUnit.BuffState bs = this.ZActor.GetBuff(sd.Data.buffId);
            if (bs != null)
                return false;
        }

        #endregion

        mSkillTemplate = state.Data;

        targetID = 0;
        if (Target != null)
            targetID = Target.TargetId;

        if (simpleMode == false)
        {
            targetID = CalSkillTarget(skillID, targetID);
        }
        else
        {

            targetID = StartLaunchSkill(skillID, Target.TargetId, null);
        }

        return true;
    }

    private uint CalSkillTarget(int skillID, uint targetID)
    {
        ZoneUnit.SkillState state = this.ZUnit.GetSkillState(skillID);
        if (state == null)
        {
            return 0;
        }

        TLAIUnit targetUnit = null;

        var targetType = state.Data.ExpectTarget;


        if (targetID == 0)
        {
            //以后可能要搜索友军单位.
            targetID = Target.ChangeTargetToNearEnemy(this, AtkRange, AtkAngle, true, targetType);
        }

        if (targetID != 0)//已经选中了单位.
        {
            if (targetID == this.ObjectID)
            {
                targetUnit = this;
            }

            targetUnit = this.BattleScene.GetBattleObject(targetID) as TLAIUnit;
        }

        if (targetUnit != null)
        {
            Target.SetTarget(targetID);
            return targetID;
        }

        return 0;
    }

    [DoNotToLua]
    public void PreLunchSkill(SkillTemplate skillTemplate)
    {
        TLSkillProperties zsp = (TLSkillProperties)skillTemplate.Properties;

        Quaternion q;
        Vector3 pos = Vector3.zero;
        if (Target.TargetId != 0)
        {
            ComAICell u = this.BattleScene.GetBattleObject(Target.TargetId);
            ZoneObject o = u.ZObj;
            float degree = DeepCore.Vector.MathVector.getDegree(new DeepCore.Vector.Vector2(this.ZUnit.X, this.ZUnit.Y), new DeepCore.Vector.Vector2(o.X, o.Y));
            q = Quaternion.Euler(0, degree * Mathf.Rad2Deg + 90, 0);
            pos = u.ObjectRoot.transform.position;
            if (zsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Area)
            {
                //选坐标的情况
                if (Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(this.ObjectRoot.transform.position.x, this.ObjectRoot.transform.position.z)) > skillTemplate.AttackRange)
                {
                    //超出射程
                    pos = Vector3.MoveTowards(this.ObjectRoot.transform.position, pos, skillTemplate.AttackRange);
                }
            }
        }
        else
        {
            q = Quaternion.Euler(0, this.ZUnit.Direction * Mathf.Rad2Deg + 90, 0);
        }

        if (zsp.LaunchModeData.ShowLaunchGuide)//是否技能引导光圈.
        {
            this.mSkillTargetSelect.SetActive(true);
        }

        this.mSkillTargetSelect.Reset(skillTemplate, q, pos);
    }

    internal void LaunchSkillWithTarget(int skillID, uint targetID, DeepCore.Vector.Vector2 pos = null)
    {
        UnitLaunchSkillAction launch = new UnitLaunchSkillAction(ObjectID, skillID, true, targetID, pos);
        if (targetID != this.ZUnit.ObjectID && targetID != 0)
        {
            Target.SetTarget(targetID);
        }

        var unit = this.BattleScene.GetBattleObject(targetID) as TLAIUnit;
        if (unit != null)
        {
            LastSpellTargetUUID = unit.ObjectID;
            LastSpellTargetPos = this.BattleScene.GetBattleObject(LastSpellTargetUUID).Position;
        }

        this.ZActor.SendUnitLaunchSkill(launch);
        TryChangeManualOperateState();
    }

    private bool IsInAtkRange(ComAIUnit unit, float atkRange)
    {
        if (unit == null) { return false; }
        return CMath.intersectRound(this.ZObj.X, this.ZObj.Y, atkRange, unit.X, unit.Y, unit.Info.BodyHitSize);
    }

    private bool InSafeArea()
    {
        return false;
    }

    [DoNotToLua]
    public Vector2 OnReadyToLaunchSkill(int index)
    {
        var state = GetSkillState(index);

        if (state == null)
            return Vector2.zero;

        mSkillTemplate = state.Data;

        uint targetId = 0;

        TLSkillProperties dsp = (TLSkillProperties)mSkillTemplate.Properties;

        if (dsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_None)
        {
            ReadyToLaunchSkillByState(state, true, ref targetId);
            return Vector2.zero;
        }
        else
        {
            if (ReadyToLaunchSkillByState(state, false, ref targetId) == false)
            {
                return Vector2.zero;
            }
        }

        Quaternion q;
        Vector3 pos = Vector3.zero;
        if (targetId != 0)
        {
            ComAICell u = this.BattleScene.GetBattleObject(targetId);
            ZoneObject o = u.ZObj;
            float degree = DeepCore.Vector.MathVector.getDegree(new DeepCore.Vector.Vector2(this.ZUnit.X, this.ZUnit.Y), new DeepCore.Vector.Vector2(o.X, o.Y));
            q = Quaternion.Euler(0, degree * Mathf.Rad2Deg + 90, 0);
            pos = u.ObjectRoot.transform.position;
            if (dsp.LaunchModeData.Mode == TLSkillLaunchModeData.TLSkillLaunchMode.Mode_Area)
            {
                //选坐标的情况
                if (Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(this.ObjectRoot.transform.position.x, this.ObjectRoot.transform.position.z)) > mSkillTemplate.AttackRange)
                {
                    //超出射程
                    pos = Vector3.MoveTowards(this.ObjectRoot.transform.position, pos, mSkillTemplate.AttackRange);
                }
            }
        }
        else
        {
            q = Quaternion.Euler(0, this.ZUnit.Direction * Mathf.Rad2Deg + 90, 0);
        }

        mSkillTargetSelect.Reset(mSkillTemplate, q, pos);
        if (dsp.LaunchModeData.ShowLaunchGuide)//是否技能引导光圈.
        {
            mSkillTargetSelect.SetActive(true);
        }
        if (!SkillTargetSelect.IsNormalAtk(dsp.SkillType))
        {
            HudManager.Instance.SkillBar.ShowSkillRockerByIndex(index);
        }

        if (pos != Vector3.zero)
        {
            Vector3 selfPos = ObjectRoot.transform.position;

            Vector3 v1 = Camera.main.WorldToScreenPoint(pos);//怪物屏幕坐标
            Vector3 v2 = Camera.main.WorldToScreenPoint(selfPos);//人物屏幕坐标 
            float dis = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(selfPos.x, selfPos.z));
            float rockerDis = dis * 1 / mSkillTemplate.AttackRange;
            Vector3 dir3D = (pos - selfPos).normalized;
            Vector3 dir2D = v1 - v2;
            Vector3 dir2D1 = dir2D.normalized;
            Vector2 v = Camera.main.WorldToScreenPoint(pos - selfPos).normalized;
            Vector2 rockerPos = dir2D1 * rockerDis;
            //Debugger.Log("dis " + rockerDis + ", dir2D " + dir2D + ", normal " + dir2D1);

            //Vector2 dir = new Vector2(pos.x, pos.z) - new Vector2(selfPos.x, selfPos.z);
            //float radians = Mathf.Atan2(dir.y, dir.x);
            //float angle = 90 - Mathf.Rad2Deg * radians;
            //radians = angle * Mathf.Deg2Rad;
            //Debugger.Log("---------------- " + angle);
            //rockerPos = new Vector2(Mathf.Cos(radians) * rockerDis, Mathf.Sin(radians) * rockerDis);


            return rockerPos;
        }
        return Vector2.zero;
    }

    private void StopPreLaunchSkill()
    {
        HudManager.Instance.SkillBar.cancelSkillBtn.gameObject.SetActive(false);
        mSkillTargetSelect.SetActive(false);
        TLSkillProperties dsp = (TLSkillProperties)mSkillTemplate.Properties;
        if (!SkillTargetSelect.IsNormalAtk(dsp.SkillType))
        {
            HudManager.Instance.SkillBar.ShowSkillRockerByIndex(-1);
        }
    }

    private Vector3 SetTargetPosition(Vector3 pos, Vector2 offset, float distance)
    {
        Vector3 o = new Vector3(offset.x, 0, offset.y) * distance;
        return pos + o;
    }

    private void UpdateSkill(float deltaTime)
    {
        mSkillTouchHandler.OnUpdate();
        if (mSkillTargetSelect.gameObject.activeSelf)
        {
            Vector3 v = ObjectRoot.Position();
            mSkillTargetSelect.transform.position = v;
        }
    }


    private uint StartLaunchSkill(int skillID, uint targetID, DeepCore.Vector.Vector2 pos = null)
    {
        ZoneUnit.SkillState state = this.ZUnit.GetSkillState(skillID);

        if (state == null) return 0;


        if (InSafeArea())
        {
            //攻击性技能不可施放.
            if (state != null && state.Data.ExpectTarget == SkillTemplate.CastTarget.Enemy)
            {
                GameAlertManager.Instance.ShowNotify("now in safe area!");
                return 0;
            }
        }
        TLSkillProperties zsp = state.Data.Properties as TLSkillProperties;
        bool needTarget = zsp.TargetType == TLSkillProperties.TLSkillTargetType.NeedTarget ? true : false;


        //是否需要目标.
        if (needTarget)
        {
            TLAIUnit targetUnit = GetMatchTarget(targetID, state);
            if (targetUnit != null)
            {
                return LaunchSkill(targetUnit, state);
            }
            else
            {
                //判断技能施法条件，增益类技能，没有单位情况默认选择自己.
                if (state.Data.ExpectTarget == SkillTemplate.CastTarget.AlliesIncludeSelf)
                {
                    //这类技能必定能有目标（没有选自己）.
                    targetID = this.ZUnit.ObjectID;
                    LaunchSkillWithTarget(skillID, targetID);
                    return targetID;
                }
                else
                {
                    targetID = Target.ChangeTargetToNearEnemy(this, AtkRange, AtkAngle, true, state.Data.ExpectTarget);
                    targetUnit = this.BattleScene.GetBattleObject(targetID) as TLAIUnit;
                    if (targetUnit != null)
                    {
                        return LaunchSkill(targetUnit, state);
                    }

                    if (GameGlobal.Instance.netMode)
                    {
                        //提示需要一个施法单位.
                        var str = HZLanguageManager.Instance.GetString("skill_needtarget");
                        GameAlertManager.Instance.ShowNotify(str);
                    }
                    return 0;
                }
            }
        }
        else //不需要释放目标.
        {
            if (zsp.TargetType == TLSkillProperties.TLSkillTargetType.Self)
            {
                LaunchSkillWithTarget(skillID, this.ObjectID);
                return 0;
            }

            if (pos != null)
            {
                LaunchSkillWithTarget(skillID, 0, pos);
                return 0;
            }
            else if (targetID != 0)
            {
                LaunchSkillWithTarget(skillID, targetID);
                return targetID;
            }
            else
            {
                LaunchSkillWithTarget(skillID, 0);
                return 0;
            }
        }
    }

    private TLAIUnit GetMatchTarget(uint targetID, ZoneUnit.SkillState state)
    {
        if (targetID == 0) return null;

        TLAIUnit targetUnit = null;

        if (targetID != this.ObjectID)
        {
            targetUnit = this.BattleScene.GetBattleObject(targetID) as TLAIUnit;
            if (targetUnit != null)
            {
                //技能目标不匹配.
                if (Target.SkillTargetMatch(state.Data.ExpectTarget, this, targetUnit) == false)
                {
                    targetUnit = null;
                    targetID = 0;
                    if (state.Data.ExpectTarget == SkillTemplate.CastTarget.AlliesIncludeSelf)
                    {
                        targetUnit = this;
                        targetID = targetUnit.ObjectID;
                    }
                }
                else
                {
                    LastSpellTargetUUID = targetUnit.ObjectID;
                    LastSpellTargetPos = this.BattleScene.GetBattleObject(LastSpellTargetUUID).Position;
                }
            }
        }

        return targetUnit;
    }

    private uint LaunchSkill(TLAIUnit targetUnit, ZoneUnit.SkillState state)
    {
        uint ret = 0;
        if (targetUnit == null) return ret;
        ret = targetUnit.ObjectID;
        LastSpellTargetUUID = ret;
        LastSpellTargetPos = this.BattleScene.GetBattleObject(LastSpellTargetUUID).Position;

        this.FaceTo(targetUnit);

        //当前正在施放技能.
        var curSkill = this.ZActor.CurrentSkillState;
        if (curSkill != null)
        {
            bool curIsNormalSkill = false;
            bool targetIsNormalSkill = false;

            //要施放的不是普攻.
            if (curSkill.Data.ID == ZActor.BaseSkillID)
                curIsNormalSkill = true;

            if (state.Data.ID == ZActor.BaseSkillID)
                targetIsNormalSkill = true;


            if (curIsNormalSkill && targetIsNormalSkill) return 0; //普攻不能打断普攻.
            else if (curIsNormalSkill && !targetIsNormalSkill) { /*DO NOTHING*/}//非普攻技能可打断普攻.
            else//判断当前是否可打断.
            {
                var action = this.ZActor.CurrentSkillAction;
                if (action.IsCancelableBySkill == false)
                    return 0;
            }
        }

        if (CurGState is ManualOperateState)
        {
            bool f = (CurGState as ManualOperateState).CheckAndLaunchSkill(targetUnit, state);
            if (f)
                return ret;
        }

        //自动靠近.
        ChangeActorState(new MoveAndAttackState(this, state.Data.ID, targetUnit, state.Data.AttackRange));
        return ret;
    }

}
