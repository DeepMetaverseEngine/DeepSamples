

using Assets.Scripts.Data;


using System.Collections.Generic;
using System;
using Assets.Scripts;
using UnityEngine;
using DeepCore;
using DeepCore.Unity3D.Battle;
using DeepCore.GameSlave;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Utils;
using TLClient.Modules;
using SLua;
using TLBattle.Common.Plugins;

public class TLAINPC : TLAIUnit, INpcQuestInterface
{
    private HashMap<int, TLQuest> QuestList = null;
    private bool ShowInScene = true;
    private bool ShowInQuest = false;
    private TimeExpire<int> timeExpire;
    private float IdleTimeMin = 0;
    private float IdleTimeMax = 0;
    public struct NpcQuestType
    {
        public const int None = 0;
        public const int StoryAsk = 1;
        public const int DailyAsk = 2;
        public const int StorySign = 3;
        public const int DailySign = 4;
        public const int DarkAsk = 5;
        public const int DarkSign = 6;
        public const int Max = 7;
    }
    public TLAINPC(BattleScene battleScene, ZoneUnit obj)
            : base(battleScene, obj)
    {
        IdleTimeMin = GameUtil.GetIntGameConfig("npc_action_limit_time");
        IdleTimeMax = GameUtil.GetIntGameConfig("npc_action_max_time");
        initPlayIdleSpeicalAnim(0);
    }


    public HashMap<int, TLQuest> CurrentQuestList
    {
        get
        {
            if (QuestList == null)
            {
                QuestList = new HashMap<int, TLQuest>();
            }
            return QuestList;
        }
    }

    public List<TLQuest> GetQuestList()
    {
        var list = new List<TLQuest>();
        foreach (var data in CurrentQuestList.Values)
        {
            list.Add(data);
        }
        return list;
    }
    public uint GetObjId()
    {
        return this.ZUnit.ObjectID;
    }
    public int GetTemplateId()
    {
        return this.ZUnit.Info.TemplateID;
    }
    public override string Name()
    {
        var data = GameUtil.GetDBData("npc", this.ZUnit.Info.TemplateID);
        object name;
        if (data != null && data.TryGetValue("npc_name", out name))
        {
            return HZLanguageManager.Instance.GetString((string)name);
        }
        return HZLanguageManager.Instance.GetString("npc_name_" + this.ZUnit.Info.TemplateID);
    }
    protected override void OnLoadModelFinish(FuckAssetObject aoe)
    {
        if (this.IsDisposed)
        {
            aoe.Unload();
            return;
        }
        this.DisplayCell.SetLayer((int)PublicConst.LayerSetting.NpcLayer);
        base.OnLoadModelFinish(aoe);
        initShow();
        DataMgr.Instance.QuestMangerData.AddListener(this);
        UpdateShow();
        //this.ChangeDirection(this.ZUnit.Direction);
        this.ObjectRoot.ZoneRot2UnityRot(this.ZUnit.Direction);

    }
    [DoNotToLua]
    public void initShow()
    {
        if (this.IsDisposed || this.ZUnit == null)
        {
            Debugger.Log("Npc say 88 to u");
            return;
        }
        ShowInScene = DataMgr.Instance.QuestMangerData.IsAlwaysShow(this.TemplateID);
    }
    [DoNotToLua]
    public void UpdateShow()
    {
        if (this.bindBehaviour == null || ShowInScene)
        {
            return;
        }
        if (this.IsDisposed || this.ZUnit == null)
        {
            //Debugger.Log("Npc say 88 to u");
            return;
        }
        ShowInQuest = ShowInQuest||DataMgr.Instance.QuestMangerData.IsShowNpc(this.TemplateID);
        this.bindBehaviour.InfoBar.SetActive(IsShow);
        this.bindBehaviour.gameObject.SetActive(IsShow);

    }
    [DoNotToLua]
    public bool IsShow
    {
        get
        {
            if (ShowInScene)
            {
                return true;
            }
            return ShowInQuest;
        }

    }

    private void initPlayIdleSpeicalAnim(float time)
    {
        float random = UnityEngine.Random.Range(IdleTimeMin, IdleTimeMax) + time;
        int _time = (int)(random * 1000);
        if (timeExpire == null)
        {
            timeExpire = new TimeExpire<int>(_time);
        }
        else
        {
            timeExpire.Reset(_time);
        }

    }
    protected override void SyncState()
    {
        if (mOnPositionChange != null && mHeadTransform != null)
        {
            mOnPositionChange(mHeadTransform);
            mOnPositionChange = null;
        }
        if (ZObj.Parent.TerrainSrc != null)
        {
            this.ObjectRoot.ZonePos2NavPos(ZObj.Parent.TerrainSrc.TotalHeight
                , ZObj.X, ZObj.Y, ZObj.Z);
        }
        if (this.CurrentState != DeepCore.GameData.Data.UnitActionStatus.Idle)
        {
            FaceToDirect = ZObj.Direction;
            this.ObjectRoot.ZoneRot2UnityRot(ZObj.Direction);
        }
        else
        {
            if (!StopFaceToDirect)
            {
                var zp = Info.Properties as TLUnitProperties;

                if (zp.UnitDisplayConfig != null && zp.UnitDisplayConfig.TalkTurnRound)
                    this.ObjectRoot.ZoneRot2UnityRot(FaceToDirect);
            }

        }

    }
    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        UpdatePlayIdleSpeicalAnim(deltaTime);
    }

    private void UpdatePlayIdleSpeicalAnim(float deltaTime)
    {
        if (IsDisposed)
        {
            return;
        }
        if (this.CurrentState == DeepCore.GameData.Data.UnitActionStatus.Idle
            && timeExpire.Update((int)(deltaTime * 1000)))
        {
            float time = this.animPlayer.GetAnimTime("n_talk");
            if (time == 0)
            {
                return;
            }
            PlayIdleSpeicalAnimation("n_talk");
            initPlayIdleSpeicalAnim(time);
        }
    }

    //刷新任务状态和表现
    public void UpdateQuest(int QuestId, int queststate, int questType)
    {
        TLQuest td;

        if (CurrentQuestList.TryGetValue(QuestId, out td))
        {
            td.state = queststate;
        }
        else
        {
            td = new TLQuest();
            td.id = QuestId;
            td.state = queststate;
            td.mainType = questType;
            CurrentQuestList.Add(QuestId, td);
        }
        if (queststate == QuestState.Submited || queststate == QuestState.Remove)
        {
            CurrentQuestList.RemoveByKey(QuestId);
        }

        UpdateHeadShow();
        UpdateShow();
    }
    //[DoNotToLua]
    //public int GetShowQuestID()
    //{
    //    List<TLQuest> quest = null;
    //    foreach (var _quest in CurrentQuestList.Values)
    //    {
    //        if (quest == null)
    //        {
    //            quest = new List<TLQuest>();
    //        }
    //        quest.Add(_quest);
    //    }
    //    if (quest == null)
    //    {
    //        return 0;
    //    }
    //    DataMgr.Instance.QuestMangerData.SortList(quest);
    //    return quest[quest.Count - 1].id;
    //}

    [DoNotToLua]
    public TLQuest GetShowQuest()
    {
        TLQuest tlquest = null;
        foreach (var _quest in CurrentQuestList.Values)
        {

            if (_quest.state == QuestState.CompletedNotSubmited)
            {
                if (DataMgr.Instance.QuestMangerData.NpchasQuest(_quest))
                {
                    return _quest;
                }
                else
                    continue;

            }
            else if (_quest.state == QuestState.NotAccept)
            {
                if (tlquest == null)
                {
                    if (DataMgr.Instance.QuestMangerData.NpchasQuest(_quest))
                    {
                        tlquest = _quest;
                    }

                }
                else
                {
                    continue;
                }
            }
        }
        if (tlquest == null)
        {
            foreach (var _quest in CurrentQuestList.Values)
            {
                if (_quest.state == QuestState.NotCompleted)
                {
                    if (DataMgr.Instance.QuestMangerData.NpchasQuest(_quest))
                    {
                        return _quest;
                    }
                }
            }
        }
        return tlquest;
    }
    //头上符号
    protected void UpdateHeadShow()
    {
        if (this.bindBehaviour == null)
        {
            return;
        }
        //int showQuestid = GetShowQuestID();
        TLQuest data = GetShowQuest();
        if (data != null)
        {
            this.bindBehaviour.ShowQuestStateFlag(data.mainType, data.state);
            return;
        }
        //if (CurrentQuestList.TryGetValue(showQuestid,out data))
        //{
        //    this.bindBehaviour.ShowQuestStateFlag(data.type, data.state);
        //    return;
        //}
        this.bindBehaviour.ShowQuestStateFlag(QuestType.TypeNone, NpcQuestManager.NoneQuestState);
    }



    protected override void OnDispose()
    {
        CurrentQuestList.Clear();
        DataMgr.Instance.QuestMangerData.RemoveListener(this);
        base.OnDispose();
    }

    public void QuestRefreshShow(int questid, int queststate)
    {
        //5 - 任务可接受显示，进行中不显示，立即刷新视野
        //6 - 任务未提交完成时显示，立即刷新视野
        //7 - 任务移除时消失，立即刷新
        var data = DataMgr.Instance.QuestMangerData.GetQuestNpcData(this.TemplateID);
        if (data != null && data.Count > 0)
        {
            var table = data[0]["quest_id"] as LuaTable;
            foreach (var item in table)
            {
                int qid = 0;
                int.TryParse(item.value.ToString(), out qid);
                if (qid == questid)
                {
                    var quest_state = data[0]["quest_state"] as LuaTable;
                    int _queststate = 0;
                    int key = 0;
                    int.TryParse(item.key.ToString(), out key);
                    int.TryParse(quest_state[key].ToString(), out _queststate);
                    if (_queststate == 5)
                    {
                        if (queststate == QuestState.NotAccept)
                        {
                            this.ShowInQuest = true;
                        }
                        else
                        {
                            this.ShowInQuest = false;
                        }
                        break;
                    }
                    else if (_queststate == 6)
                    {
                        if (queststate == QuestState.CompletedNotSubmited)
                        {
                            this.ShowInQuest = true;
                        }
                        else
                        {
                            this.ShowInQuest = false;
                        }
                        break;
                    }
                    else if (_queststate == 7)
                    {
                        if (queststate == QuestState.Submited||queststate == QuestState.Remove)
                        {
                            this.ShowInQuest = false;
                        }
                        else
                        {
                            this.ShowInQuest = true;
                        }
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }
            UpdateShow();
        }
    }
}
