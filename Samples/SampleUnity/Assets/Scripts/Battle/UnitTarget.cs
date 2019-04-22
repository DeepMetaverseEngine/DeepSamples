using UnityEngine;
 
using System.Collections.Generic;
using DeepCore.GameData.Zone;
using DeepCore;
using DeepCore.Unity3D.Battle;
using DeepCore.GameSlave;

public class UnitTarget
{

    public delegate void OnAddTargetEvent(TLAIUnit unit);
    public event OnAddTargetEvent OnAddTarget;
    public delegate void OnRemoveTargetEvent(TLAIUnit unit);
    public event OnRemoveTargetEvent OnRemoveTarget;

    private uint mObjectId;  //自己的对象ID                          

    public uint TargetId { get; protected set; } //选中的目标对象ID

    private HashMap<uint, uint> mInverseList = new HashMap<uint, uint>();  //被选中的单位列表

    private List<uint> mTargetQueue = new List<uint>();   //切换目标队列 
    private int mTargetIndex;   //当前目标在队列中的位置.
    private float mLastChangeTargetTime;

    private bool mIsDestory = false;

    public UnitTarget(uint objectId)
    {
        mObjectId = objectId;
    }

    public void SetTarget(uint targetId)
    {
        if (targetId != this.TargetId && targetId != mObjectId)
        {
            RemoveTarget();

            TLAIUnit unit = GameSceneMgr.Instance.BattleScene.GetBattleObject(targetId) as TLAIUnit;
            if (unit != null)
            {
                this.TargetId = targetId;
                unit.Target.SetInverse(mObjectId);
                if (OnAddTarget != null)
                {
                    OnAddTarget(unit);
                }
            }
        }
    }

    public void RemoveTarget()
    {
        TLAIUnit unit = GameSceneMgr.Instance.BattleScene.GetBattleObject(TargetId) as TLAIUnit;
        if (unit != null)
        {
            this.TargetId = 0;
            unit.Target.RemoveInverse(mObjectId);

            //移除目标UI信息
            if (OnRemoveTarget != null)
            {
                OnRemoveTarget(unit);
            }
        }
    }

    public void SetInverse(uint objId)
    {
        mInverseList[objId] = objId;
    }

    public void RemoveInverse(uint objId)
    {
        if (mIsDestory)
            return;
        if (mInverseList.ContainsKey(objId))
        {
            mInverseList.RemoveByKey(objId);
        }
    }

    public bool HasTarget()
    {
        return TargetId != 0;
    }

    private bool IsUser()
    {
        return DataMgr.Instance.UserData.ObjectId == mObjectId;
    }

    /// <summary>
    /// 单位被移除时调用，清除与所有其他单位的目标关系链 
    /// </summary>
    public void Destroy()
    {
        mIsDestory = true;
        foreach (uint objId in mInverseList.Values)
        {
            TLAIUnit unit = GameSceneMgr.Instance.BattleScene.GetBattleObject(objId) as TLAIUnit;
            if (unit != null)
            {
                //删除对方被选列表中的自己 
                unit.Target.RemoveInverse(mObjectId);

                //如果对方选了自己，删除对方的选中目标 
                if (unit.Target.TargetId == mObjectId)
                {
                    unit.Target.RemoveTarget();
                }
            }
        }
        OnAddTarget = null;
        OnRemoveTarget = null;
        //Debug.Log("----------  target Destory   ------------");
    }

    /// <summary>
    /// 按条件搜索最近的目标
    /// </summary>
    /// <param name="self">对象自己</param>
    /// <param name="range">搜索半径</param>
    /// <param name="uType">目标类型</param>
    /// <param name="sameForce">是否统一阵营</param>
    /// <returns>目标对象id（ZObj.ObjectID）</returns>
    public uint SearchTarget(TLAIUnit self, float range, UnitInfo.UnitType uType, bool sameForce)
    {
        HashMap<uint, ComAICell> allUnits = GameSceneMgr.Instance.BattleScene.BattleObjects;

        float disMin = 99999;
        uint targetId = 0;
        Vector2 posSelf = new Vector2(self.ZObj.X, self.ZObj.Y);
        Vector2 posUnit = Vector2.zero;
        foreach (ComAICell u in allUnits.Values)
        {
            TLAIUnit unit = u as TLAIUnit;
            if (unit == null)
                continue;
            if (unit.Info.UType != uType) //过滤掉不同类型的.
                continue;
            if ((sameForce && self.Force != unit.Force) || (!sameForce && self.Force == unit.Force)) //过滤掉阵营不符合要求的.
                continue;
            if (unit.IsDead)    //过滤掉死的.
            {
                continue;
            }
            posUnit.Set(unit.ZObj.X, unit.ZObj.Y);
            float dis = Vector2.Distance(posSelf, posUnit) - (self.Info.BodySize + unit.Info.BodySize);
            if (dis <= range)   //视野内
            {
                if (dis < disMin)
                {
                    disMin = dis;
                    targetId = unit.ZObj.ObjectID;
                }
            }
        }
        return targetId;
    }

    /// <summary>
    /// 按条件搜索最近的目标
    /// </summary>
    /// <param name="self">对象自己</param>
    /// <param name="range">搜索半径</param>
    /// <param name="uType">目标类型</param>
    /// <param name="sameForce">是否统一阵营</param>
    /// <returns>目标对象id（ZObj.ObjectID）</returns>
    public uint SearchTarget(TLAIUnit self, float range, UnitInfo.UnitType uType, byte force)
    {
        HashMap<uint, ComAICell> allUnits = GameSceneMgr.Instance.BattleScene.BattleObjects;

        float disMin = 99999;
        uint targetId = 0;
        Vector2 posSelf = new Vector2(self.ZObj.X, self.ZObj.Y);
        Vector2 posUnit = Vector2.zero;
        foreach (ComAICell u in allUnits.Values)
        {
            TLAIUnit unit = u as TLAIUnit;
            if (unit == null)
                continue;
            if (unit.Info.UType != uType) //过滤掉不同类型的.
                continue;
            if (force != unit.Force)//过滤掉阵营不符合要求的.
                continue;
            if (unit.IsDead)    //过滤掉死的.
                continue;

            posUnit.Set(unit.ZObj.X, unit.ZObj.Y);
            float dis = Vector2.Distance(posSelf, posUnit) - (self.Info.BodySize + unit.Info.BodySize);
            if (dis <= range)   //视野内
            {
                if (dis < disMin)
                {
                    disMin = dis;
                    targetId = unit.ZObj.ObjectID;
                }
            }
        }
        return targetId;
    }

    /// <summary>
    /// 选择临近目标.
    /// </summary>
    /// <param name="self">自己</param>
    /// <param name="range">搜索的距离</param>
    /// <param name="angle">搜索的角度</param>
    /// <param name="isNext">true:下一个 false:前一个</param>
    /// <returns></returns>
    public uint ChangeTargetToNearEnemy(TLAIUnit self, float range, float angle, bool isNext, SkillTemplate.CastTarget type = SkillTemplate.CastTarget.Enemy)
    {
        if (Time.realtimeSinceStartup - mLastChangeTargetTime > 3)
        {
            mTargetQueue.Clear();
            mTargetIndex = 0;
        }
        mLastChangeTargetTime = Time.realtimeSinceStartup;
        //先尝试直接从缓存队列里取.
        if (mTargetQueue.Count > 1)
        {
            if (isNext && mTargetIndex < mTargetQueue.Count - 1)
            {
                return mTargetQueue[++mTargetIndex];
            }
            else if (!isNext && mTargetIndex > 0)
            {
                return mTargetQueue[--mTargetIndex];
            }
        }

        //队列中没找到，开始进行搜索.
        HashMap<uint, ComAICell> allUnits = GameSceneMgr.Instance.BattleScene.BattleObjects;
        float disMin = 99999;
        float angleMin = angle * 0.5f;
        uint targetId = 0;
        Vector2 posSelf = new Vector2(self.ZObj.X, self.ZObj.Y);
        Vector2 posUnit = Vector2.zero;
        List<float> list_dis = new List<float>();
        List<uint> list_id = new List<uint>();
        int[] targetTypeCount = { 0, 0, 0, 0 }; //每一类对象的个数，下标分别代表：0.BOSS 1.atk target 2.player 3.monster


        foreach (ComAICell u in allUnits.Values)
        {
            TLAIUnit unit = u as TLAIUnit;
            if (unit == null)
                continue;
            if (unit.IsDead)
                continue;
            if (self.ObjectID == unit.ObjectID)
                continue;
            if (SkillTargetMatch(type, self, unit) == false)
                continue;

            //检测在扇形区域内
            Vector3 pos = self.ObjectRoot.transform.InverseTransformPoint(unit.ObjectRoot.transform.position);
            float angleCur = Mathf.Abs(Mathf.Atan2(pos.x, pos.z) * Mathf.Rad2Deg);
            if (angleCur > angleMin)
                continue;

            posUnit.Set(unit.ZObj.X, unit.ZObj.Y);
            float dis = Vector2.Distance(posSelf, posUnit) - (self.Info.BodySize + unit.Info.BodySize);
            if (dis <= range)   //视野内
            {
                Vector3 vp = Camera.main.WorldToViewportPoint(unit.ObjectRoot.transform.position);
                if (vp.x < 0f || vp.x > 1f || vp.y < 0f || vp.y > 1f)
                {
                    continue;
                }
                if (dis < disMin)
                {
                    int typeIndex = 0;
                    if (unit.GetCurrentTarget() == self.ObjectID)
                    {
                        typeIndex = 1;
                    }
                    else if (unit.Info.UType == UnitInfo.UnitType.TYPE_PLAYER)   //玩家
                    {
                        typeIndex = 2;
                    }
                    else    //其他敌对单位
                    {
                        typeIndex = 3;
                    }

                    //找出此类型在队列里的起始位置 
                    int startAt = 0;
                    for (int j = 0 ; j < typeIndex ; ++j)
                    {
                        startAt += targetTypeCount[j];
                    }

                    //在队列里的同一类型中作插入排序 
                    bool isInsert = false;
                    for (int a = startAt ; a < startAt + targetTypeCount[typeIndex] ; ++a)
                    {
                        float disTmp = list_dis[a];
                        if (dis < disTmp)
                        {
                            list_dis.Insert(a, dis);
                            list_id.Insert(a, unit.ObjectID);
                            isInsert = true;
                            break;
                        }
                    }
                    if (!isInsert)
                    {
                        list_dis.Insert(startAt + targetTypeCount[typeIndex], dis);
                        list_id.Insert(startAt + targetTypeCount[typeIndex], unit.ObjectID);
                    }
                    targetTypeCount[typeIndex]++;
                }
            }
        }

        if (list_id.Count < 1)
        {
            return 0;
        }
        //检查首次切换时，是否已经默认选了目标 
        if (mTargetQueue.Count <= 0)
        {
            TLAIUnit unit = self as TLAIUnit;
            if (unit != null && unit.Target.HasTarget())
            {
                mTargetQueue.Add(unit.Target.TargetId);
            }
        }
        //重新整理缓存队列，保证缓存队列数量小于当前可选人数 
        int selMaxCount = 5;	//默认最多在selMaxCount个之间来回切换 
        int checkCount = (list_id.Count < selMaxCount ? list_id.Count : selMaxCount) - 1;
        if (mTargetQueue.Count > checkCount)
        {
            for (int i = 0 ; i < mTargetQueue.Count - checkCount ; ++i)
            {
                if (isNext)
                    mTargetQueue.RemoveAt(0);
                else
                    mTargetQueue.RemoveAt(mTargetQueue.Count - 1);
            }
        }
        //按距离从小到大，遍历每一个目标 
        List<uint> tmpTargetQueue = new List<uint>();
        for (int i = 0 ; i < list_id.Count ; ++i)
        {
            bool isRepeat = false;
            uint objId = list_id[i];
            //检查此目标在缓存队列中是否有重复 
            tmpTargetQueue.Clear();
            tmpTargetQueue.AddRange(mTargetQueue);
            while (tmpTargetQueue.Count > 0)
            {
                uint tmpId = tmpTargetQueue[0];
                tmpTargetQueue.RemoveAt(0);
                if (tmpId == objId)
                {
                    isRepeat = true;
                    break;
                }
            }
            if (!isRepeat)
            {	//未重复，找到目标 
                if (isNext)
                {
                    mTargetIndex = mTargetQueue.Count;
                    mTargetQueue.Add(objId);
                }
                else
                {
                    mTargetIndex = 0;
                    mTargetQueue.Insert(0, objId);
                }
                targetId = objId;
                break;
            }
        }
        return targetId;
    }

    public bool SkillTargetMatch(SkillTemplate.CastTarget type, TLAIUnit self, TLAIUnit target)
    {
        var f = (self.ZObj.Parent as TLBattle.Client.TLZoneLayer).IsAttackable(self.ZObj as ZoneUnit,
                                                                           target.ZObj as ZoneUnit,
                                                                            type);
        return f;
    }


    public void Refresh()
    {
        if (TargetId != 0)
        {
            TLAIUnit unit = GameSceneMgr.Instance.BattleScene.GetBattleObject(TargetId) as TLAIUnit;
            if (unit != null)
            {
                unit.bindBehaviour.SetFocus(true);
            }
        }

    }
}
