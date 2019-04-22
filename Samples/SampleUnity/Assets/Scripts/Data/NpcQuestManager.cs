using DeepCore;
using SLua;
using System;
using System.Collections.Generic;
using DeepCore.Unity3D;
using ThreeLives.Client.Common.Modules.Quest;
using TLClient.Modules;
using UnityEngine;
using DeepCore.Unity3D.Battle;

namespace Assets.Scripts.Data
{

    public struct QuestAutoMaticType
    {
        //接取、完成全手动 寻路自动 ：0
        //接取、完成全自动：1
        //接取、完成全手动，寻路接取自动 完成后手动寻路：2
        //接取不自动、其余自动：3
        public const int NoAuto = 0;
        public const int AllAuto = 1;
        public const int AutoAccept = 2;
        public const int NoAutoAccetpButAutoDone = 3;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public interface INpcQuestInterface
    {
        uint GetObjId();
        int GetTemplateId();
        void UpdateQuest(int QuestId, int QuestState,int Questtype);
        void QuestRefreshShow(int QuestId, int QuestState);
    }

    public interface IMapNpcQuestInterface
    {
        int GetTemplateId();
        void UpdateQuest(int QuestId, int QuestState, int Questtype);
    }

    public class NpcQuestManager:QuestDataListener
    {
        private static HashMap<uint, INpcQuestInterface> mNpcInSceneQuestManagerlist;
        private static HashMap<int, IMapNpcQuestInterface> mMapQuestManagerlist;
        private static HashMap<int, List<int>> mNpcQuestList;
        private static bool bInitGlobalHook = false;
        private static bool bIsShowComplete = false;
        public const int NoneQuestState = -9999;
        public NpcQuestManager()
        {
            //string path = "dynamic/effect/mission_complete/output/mission_complete.xml";
            //GameAlertManager.Instance.CpjAnime.PlayCacheCpjAnime(null,
            //    path, "mission_complete", -UGUIMgr.SCREEN_WIDTH, -UGUIMgr.SCREEN_HEIGHT, 1, null);//预加载一次
            bIsShowComplete = false;
            EventManager.Subscribe("Event.Scene.FirstInitFinish", InitQuestListener);
        }

        private void InitQuestListener(EventManager.ResponseData res)
        {
            bInitGlobalHook = true;
            DataMgr.Instance.QuestData.AttachObserver(this);
            EventManager.Subscribe(GameEvent.SYS_REMOVE_UNIT, RemoveUnit);
            //EventManager.Subscribe("Event.Quest.AddQuest", UpdateQuest);
            //EventManager.Subscribe("Event.Quest.RemoveQuest", RemoveQuest);
            //EventManager.Subscribe("Event.Quest.Submited", SubmitedQuest);
            //EventManager.Subscribe("Event.Quest.Accept", UpdateQuest);
            //EventManager.Subscribe("Event.Quest.Complete", UpdateQuest);
            EventManager.Subscribe("Event.Scene.ChangeFinish", ChangeScene);
            //EventManager.Subscribe("CloseNpcCamera", CloseNpcTalkCamera);
            EventManager.Subscribe("RefreshNpc", RefreshNpc);
        }

        [DoNotToLua]
        public void InitNetWork()
        {

        }

        private void RefreshNpc(EventManager.ResponseData res)
        {

            foreach (var elem in new List<ComAICell>(TLBattleScene.Instance.BattleObjects.Values))
            {
                var unit = elem as TLAINPC;
                if (unit != null)
                {
                    unit.UpdateShow();//IsShowNpc(unit.TemplateID);
                }
            }
        }

        [DoNotToLua]
        public List<Dictionary<string, object>> GetQuestNpcData(int NpcTemplateId)
        {
            if (!GameGlobal.Instance.netMode || TLBattleScene.Instance.Actor == null)
            {
                return null;
            }
            return GameUtil.GetDBData2("NpcShowModel", "{ npc_id = " + NpcTemplateId + " ,scene_id=" + DataMgr.Instance.UserData.MapTemplateId + "}");
        }
        [DoNotToLua]
        public bool IsAlwaysShow(int NpcTemplateId)
        {
            if (!GameGlobal.Instance.netMode)
            {
                return true;
            }
            var npcflag = GetQuestNpcData(NpcTemplateId);
            if (npcflag == null)
            {
                return true;
            }
            return npcflag.Count == 0;
        }


        [DoNotToLua]
        public bool IsShowNpc(int NpcTemplateId)
        {
            //List<QuestShowNpcData> ShowNpcDataList = null;
            bool isShow = false;
            var data = GetQuestNpcData(NpcTemplateId);
            if (data != null && data.Count > 0)
            {
                var table = data[0]["quest_id"] as LuaTable;
                foreach (var item in table)
                {
                    int qid = 0;
                    int.TryParse(item.value.ToString(), out qid);
                    if (qid != 0)
                    {
                        TLQuest quest = DataMgr.Instance.QuestData.GetQuest(qid);
                        if (quest != null)
                        {
                            //1 - 任务全程显示
                            //2 - 任务接受显示，进行中不显示
                            //3 - 任务接受时不显示，进行中及完成显示
                            //4 - 任务完成时显示
                            //5 - 任务接受显示，进行中不显示，立即刷新视野
                            var quest_state = data[0]["quest_state"] as LuaTable;
                            int _queststate = 0;
                            int key = 0;
                            int.TryParse(item.key.ToString(), out key);
                            int.TryParse(quest_state[key].ToString(), out _queststate);
                            if (_queststate == 1)
                            {
                                isShow = true;
                                break;
                            }
                            else if (_queststate == 2 && quest.state == QuestState.NotAccept)
                            {
                                isShow = true;
                                break;
                            }
                            else if (_queststate == 3 && quest.state != QuestState.NotAccept)
                            {
                                isShow = true;
                                break;
                            }
                            else if (_queststate == 4 && quest.state == QuestState.CompletedNotSubmited)
                            {
                                isShow = true;
                                break;
                            }

                        }
                    }
                }
                return isShow;
            }
            return false;
        }
       
        public void Clear(bool reLogin, bool reConnect)
        {
           
            bIsShowComplete = false;
            onCompare = null;
            if (reLogin )
            {
                if (mNpcInSceneQuestManagerlist != null)
                {
                    mNpcInSceneQuestManagerlist.Clear();
                }
                if (CurrentMapQuestList != null)
                {
                    CurrentMapQuestList.Clear();
                }
                if (mNpcQuestList != null)
                {
                    mNpcQuestList.Clear();
                }
                DataMgr.Instance.QuestData.DetachObserver(this);
                EventManager.Unsubscribe(GameEvent.SYS_REMOVE_UNIT, RemoveUnit);
               // EventManager.Unsubscribe("Event.Quest.AddQuest", UpdateQuest);
                //EventManager.Unsubscribe("Event.Quest.RemoveQuest", RemoveQuest);
                //EventManager.Unsubscribe("Event.Quest.Submited", SubmitedQuest);
                //EventManager.Unsubscribe("Event.Quest.Accept", UpdateQuest);
                //EventManager.Unsubscribe("Event.Quest.Complete", UpdateQuest);
                EventManager.Unsubscribe("Event.Scene.ChangeFinish", ChangeScene);
                //EventManager.Unsubscribe("CloseNpcCamera", CloseNpcTalkCamera);
                EventManager.Unsubscribe("RefreshNpc", RefreshNpc);
                EventManager.Unsubscribe("Event.Scene.FirstInitFinish", InitQuestListener);

            }
        }
        private void showCompleteAnim()
        {
            //string path = "dynamic/effect/mission_complete/output/mission_complete.xml";
            //GameAlertManager.Instance.CpjAnime.PlayCacheCpjAnime(null,
            //    path, "mission_complete", 0, -UGUIMgr.SCREEN_HEIGHT * 1 / 4, 1, null);

            TransformSet info = new TransformSet();
            info.Pos = new Vector3(0, UGUIMgr.SCREEN_HEIGHT * 1 / 4 + 60,0);
            info.Layer = (int)PublicConst.LayerSetting.UI;
            string path = "/res/effect/ui/ef_ui_task_finish.assetbundles";
            RenderSystem.Instance.PlayEffect(path,info);
            path = "/res/sound/static/uisound/renwuwancheng.assetbundles";
            SoundManager.Instance.PlaySound(path);
        }


        public void ShowCompleteAnim()
        {
            if (bIsShowComplete)
            {
                showCompleteAnim();
                bIsShowComplete = false;
            }
        }
        //private void CloseNpcTalkCamera(EventManager.ResponseData res)
        //{
        //    if (bIsShowComplete)
        //    {
        //        showCompleteAnim();
        //        bIsShowComplete = false;
        //    }
        //}
        private void AcceptQuest(EventManager.ResponseData res)
        {
            Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
            object id;
            if (data.TryGetValue("QuestId", out id))
            {
                int questid = Convert.ToInt32(id);
                TLQuest quest = DataMgr.Instance.QuestData.GetQuest(questid);
                if (quest != null)
                {
                    quest.state = QuestState.NotCompleted;
                    UpdateNpcQuestList(quest);
                    int SubTemplateId = GetTemplateIDByState(questid, QuestState.Submited);
                    int AcceptTemplateId = GetTemplateIDByState(questid, QuestState.NotAccept);
                    foreach (var npc in NpcInSceneQuest.Values)
                    {

                        if (npc.GetTemplateId() == AcceptTemplateId && AcceptTemplateId != SubTemplateId)
                        {
                            npc.UpdateQuest(questid, QuestState.Remove, GetQuestType(questid));
                        }
                        if (npc.GetTemplateId() == SubTemplateId)
                        {
                            npc.UpdateQuest(questid, GetQuestState(questid), GetQuestType(questid));
                        }
                        
                    }

                    foreach (var npc in CurrentMapQuestList.Values)
                    {
                        if (npc.GetTemplateId() == AcceptTemplateId && AcceptTemplateId != SubTemplateId)
                        {
                            updateMapQuestState(npc);
                        }
                        if (npc.GetTemplateId() == SubTemplateId)
                        {
                            updateMapQuestState(npc);
                        }
                    }
                }

            }
        }
        private void SubmitedQuest()
        {
            //Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
            //object id;
            //if (data.TryGetValue("QuestId", out id))
            //{
            //    int questid = Convert.ToInt32(id);
            //    TLQuest quest = DataMgr.Instance.QuestData.GetQuest(questid);
            //    if (quest != null)
            //    {
            //        RemoveQuest(quest);
            //        DataMgr.Instance.QuestData.RemoveQuest(questid);
            //    }

            //}
            if (!bIsShowComplete)
            {
                showCompleteAnim();
            }
            else
            {
                bIsShowComplete = true;
            }
            //Quest task = QuestData.ParseEvent(res);
            //if(task.GetStatic().GetInt("npc_submit") == 0)
            //{
            //    if (!bIsShowComplete)
            //    {
            //        showCompleteAnim();
            //    }
            //}
            //else
            //{
            //    bIsShowComplete = true;
            //}

        }
        private void RemoveQuest(Quest quest)
        {
            try
            {
                UpdateNpcQuestList(quest);
                int templateId = GetTemplateIDCurrentQuestState(quest.id, quest);
                foreach (var npc in NpcInSceneQuest.Values)
                {
                    if (npc.GetTemplateId() == templateId)
                    {
                        npc.UpdateQuest(quest.id, quest.state, quest.mainType);
                    }
                }
                foreach (var npc in CurrentMapQuestList.Values)
                {
                    if (npc.GetTemplateId() == templateId)
                    {
                        npc.UpdateQuest(0, quest.state, quest.mainType);
                    }
                }
            }
            catch (Exception e)
            {
                Debugger.LogError(e.Message + "\n" + e.StackTrace);
            }

        }
      
        //更新任务数据
        private void UpdateQuest(string eventName, TLQuest quest)
        {
            try
            {
                if (!bInitGlobalHook)
                {
                    return;
                }
                int questid = quest.id;
                if (quest == null)
                {
                    Debugger.LogError("questid [" + questid + "] is not exist");
                    return;
                }
                if (quest.GetStatic() == null)
                {
                    Debugger.LogError("questid [" + questid + "]'s StaticData is null");
                    return;
                }
                UpdateNpcQuestList(quest);
                int SubTemplateId = GetTemplateIDByState(questid, QuestState.Submited);
                int AcceptTemplateId = GetTemplateIDByState(questid, QuestState.NotAccept);
                foreach (var npc in NpcInSceneQuest.Values)
                {
                    if (eventName == "Event.Quest.Accept")
                    {
                        if (npc.GetTemplateId() == AcceptTemplateId && AcceptTemplateId != SubTemplateId)
                        {
                            npc.UpdateQuest(questid, QuestState.Remove, GetQuestType(questid));
                        }
                    }
                    if (npc.GetTemplateId() == SubTemplateId)
                    {
                        npc.UpdateQuest(questid, GetQuestState(questid), GetQuestType(questid));
                    }

                    npc.QuestRefreshShow(questid, quest.state);
                }

                foreach (var npc in CurrentMapQuestList.Values)
                {
                    if (eventName == "Event.Quest.Accept")
                    {
                        if (npc.GetTemplateId() == AcceptTemplateId && AcceptTemplateId != SubTemplateId)
                        {
                            updateMapQuestState(npc);
                        }
                    }
                    if (npc.GetTemplateId() == SubTemplateId)
                    {
                        updateMapQuestState(npc);
                    }
                }

            }
            catch (Exception e)
            {
                Debugger.LogError(e.Message + "\n" + e.StackTrace);
            }
        }

      

        //移除小地图npc任务
        private void RemoveUnit(EventManager.ResponseData res)
        {
            try
            {
                Dictionary<string, object> data = (Dictionary<string, object>)res.data[1];
                object _unitid = 0;
                if (data.TryGetValue("ObjectID", out _unitid))
                {
                    uint uid = (uint)_unitid;
                    this.NpcInSceneQuest.RemoveByKey(uid);
                }
            }
            catch (Exception e)
            {
                Debugger.LogError(e.Message + "\n" + e.StackTrace);
            }
        }
        //初始化任务数据 小地图npc标志
        private void ChangeScene(EventManager.ResponseData res)
        {
            InitData();
        }
        //移除npc任务列表
        private bool removeNpcQuestList(Quest quest)
        {
            int npcTemplate = 0;
            //Debugger.LogError("removeNpcQuestList = " + quest.id + " state= " + quest.state);
            if (quest.state != QuestState.NotAccept)
            {
                npcTemplate = quest.GetStatic().GetInt("npc_push");
                List<int> mList;
                if (CurrentNpcQuestList.TryGetValue(npcTemplate, out mList))
                {
                    mList.Remove(quest.id);
                }
                if (quest.state >= QuestState.CompletedNotSubmited)
                {
                    int subTemplate = 0;
                    subTemplate = quest.GetStatic().GetInt("npc_submit");
                    if (subTemplate == 0)
                    {
                        npcTemplate = quest.GetStatic().GetInt("npc_push");
                        if (CurrentNpcQuestList.TryGetValue(npcTemplate, out mList))
                        {
                            mList.Remove(quest.id);
                        }
                    }
                }
            }
            if (quest.state == QuestState.Submited || quest.state == QuestState.Remove)
            {
                //int subTemplate = quest.GetStatic().GetInt("npc_submit");
                //List<int> mList;
                //if (CurrentNpcQuestList.TryGetValue(subTemplate, out mList))
                //{
                //    mList.Remove(quest.id);

                //}
                var iter = CurrentNpcQuestList.GetEnumerator();
                while (iter.MoveNext())
                {
                    if(iter.Current.Value.Contains(quest.id))
                    {
                        iter.Current.Value.Remove(quest.id);
                    }
                }
                return true;
            }

           


            return false;
        }
        //更新npc任务列表
        private void UpdateNpcQuestList(Quest quest)
        {
            if (quest == null)
            {
                return;
            }
            int npcTemplate = 0;
            if (quest.state == QuestState.NotAccept)
            {
                npcTemplate = quest.GetStatic().GetInt("npc_push");
            }
            else
            {
                if (quest.state == QuestState.NotCompleted)
                {
                    foreach (var questdata in quest.progress)
                    {
                        if (questdata.Type == TLQuestCondition.FindNPC)
                        {
                            npcTemplate = questdata.Arg1;
                            break;
                        }
                    }
                }
                if (npcTemplate == 0)
                    npcTemplate = quest.GetStatic().GetInt("npc_submit");
            }

            //Debugger.LogError("update quest id=" + quest.id +" tasksate ="+quest.state);
            if (removeNpcQuestList(quest))
            {
                return;
            }
            if (npcTemplate == 0)
            {
                //Debugger.Log("updateNpcQuestList npcTemplate = 0 with questid = " + quest.id);
                return;
            }
            List<int> mList;
            if (CurrentNpcQuestList.TryGetValue(npcTemplate, out mList))
            {
                if (!mList.Contains(quest.id))
                {
                    mList.Add(quest.id);
                }
            }
            else
            {
                mList = new List<int>();
                mList.Add(quest.id);
                CurrentNpcQuestList.Add(npcTemplate, mList);
            }
        }

        // 初始化任务数据
        private void InitData()
        {
            if (!bInitGlobalHook)
                return;

            Dictionary<int, Quest> questlist = DataMgr.Instance.QuestData.AllQuests;
            //Debug.LogError("questlist"+questlist.Count);
            if (questlist != null)
            {
                foreach (var quest in questlist.Values)
                {
                    //Debugger.Log("pushNpc = " + quest.GetStatic()["npc_push"] + " SumbitNpc = " + quest.GetStatic()["npc_submit"]);
                    if (quest.GetStatic() == null)
                    {
                        continue;
                    }
                    if (quest.state == QuestState.NotAccept)
                    {
                        initNpcSign(quest.id, quest.state, quest.GetStatic().GetInt("npc_push"));
                    }
                    else
                    {
                        initNpcSign(quest.id, quest.state, quest.GetStatic().GetInt("npc_submit"));
                    }
                    UpdateNpcQuestList(quest);
                }
                foreach (var mapnpc in CurrentMapQuestList.Values)
                {
                    updateMapQuestState(mapnpc);
                }

            }
        }

        //初始化npc头上标志
        private void initNpcSign(int questid, int queststate, int templateId)
        {
            foreach (var npc in NpcInSceneQuest.Values)
            {
                if (npc.GetTemplateId() == templateId)
                {
                    npc.UpdateQuest(questid, queststate, GetQuestType(questid));
                }
            }

        }

        private HashMap<uint, INpcQuestInterface> NpcInSceneQuest
        {
            get {
                if (mNpcInSceneQuestManagerlist == null)
                {
                    mNpcInSceneQuestManagerlist = new HashMap<uint, INpcQuestInterface>();
                }
                return mNpcInSceneQuestManagerlist;
            }
        }

        private HashMap<int, IMapNpcQuestInterface> CurrentMapQuestList
        {
            get
            {
                if (mMapQuestManagerlist == null)
                {
                    mMapQuestManagerlist = new HashMap<int, IMapNpcQuestInterface>();
                }
                return mMapQuestManagerlist;
            }
        }

        private HashMap<int, List<int>> CurrentNpcQuestList
        {
            get
            {
                if (mNpcQuestList == null)
                {
                    mNpcQuestList = new HashMap<int, List<int>>();
                }
                return mNpcQuestList;
            }
        }
        //场景npc添加任务监听
        public void AddListener(INpcQuestInterface unit)
        {
            if (NpcInSceneQuest.TryAdd(unit.GetObjId(), unit))
            {
                initQuestState(unit);
            }
            else
            {
                //Debugger.Log("NpcQuestInterface templateid =" + unit.GetTemplateId() + " has exist");
            }

        }
        //场景npc移除任务监听
        public void RemoveListener(INpcQuestInterface unit)
        {
            NpcInSceneQuest.RemoveByKey(unit.GetObjId());
        }

        //小地图npc添加任务监听
        public void AddMapNpcListener(IMapNpcQuestInterface unit)
        {

            if (CurrentMapQuestList.TryAdd(unit.GetTemplateId(), unit))
            {
                updateMapQuestState(unit);
            }
            else
            {
                //Debugger.Log("MapNpcQuestInterface templateid =" + unit.GetTemplateId()+" has exist");
            }

        }

        //小地图npc移除任务监听
        public void RemoveMapListener(IMapNpcQuestInterface unit)
        {
            CurrentMapQuestList.RemoveByKey(unit.GetTemplateId());
        }
        //小地图npc更新任务状态
        private void updateMapQuestState(IMapNpcQuestInterface unit)
        {
            if (!bInitGlobalHook)
                return;

            List<TLQuest> questlist = GetNpcQuestData((unit.GetTemplateId()));
            if (questlist.Count > 0)
            {
                var quest = questlist[questlist.Count - 1];
                unit.UpdateQuest(quest.id, quest.state, quest.mainType);
            }
            else
            {
                unit.UpdateQuest(0, QuestState.Remove, QuestType.TypeStory);
            }

        }
        //npc初始化
        private void initQuestState(INpcQuestInterface unit)
        {
            if (!bInitGlobalHook)
                return;
            List<int> questlist = GetQuestIdByTemplateId(unit.GetTemplateId());
            if (questlist != null)
            {
                foreach (var questid in questlist)
                {
                    unit.UpdateQuest(questid, GetQuestState(questid), GetQuestType(questid));
                }
            }

        }


        //任务id获得任务状态
        public int GetQuestState(int questid)
        {
            Quest td = DataMgr.Instance.QuestData.GetQuest(questid);
            if (td != null)
            {
                return td.state;
            }
            //Debugger.LogError("questid " + questid + " is not exist");
            return NoneQuestState;
        }

        public int GetQuestType(int questid)
        {
            Quest td = DataMgr.Instance.QuestData.GetQuest(questid);
            if (td != null)
            {
                return td.mainType;
            }
            Debugger.LogError("questid " + questid + " is not exist");
            return NoneQuestState;
        }
        ////任务id获得任务对话内容
        //public string GetNpcTaskConnent(int TemplateId,int taskid)
        //{
        //    if(taskid != 0)
        //    {
        //        switch (GetTaskState(taskid))
        //        {
        //            case QuestState.NotCompleted:
        //                return HZLanguageManager.Instance.GetString(taskid + "_nofinish");
        //            case QuestState.NotAccept:
        //                return HZLanguageManager.Instance.GetString(taskid + "_desc_accept");
        //            case QuestState.CompletedNotSubmited:
        //                return HZLanguageManager.Instance.GetString(taskid + "_desc_subquest");
        //        }
        //    }
        //    return GetNpcDefaultTalk(TemplateId);
        //}

        ////任务id获得任务对话内容
        //public string GetNpcTaskConnentWithState(int taskid,int state)
        //{
        //    if (taskid != 0)
        //    {
        //        switch (state)
        //        {
        //            case QuestState.NotCompleted:
        //                return HZLanguageManager.Instance.GetString(taskid + "_nofinish");
        //            case QuestState.NotAccept:
        //                return HZLanguageManager.Instance.GetString(taskid + "_desc_accept");
        //            case QuestState.CompletedNotSubmited:
        //                return HZLanguageManager.Instance.GetString(taskid + "_desc_subquest");
        //        }
        //    }
        //    return "No State "+ state + " Content with id=" + taskid;
        //}

        ////默认对白
        //public string GetNpcDefaultTalk(int TemplateId)
        //{
        //    return HZLanguageManager.Instance.GetString("npc_" + TemplateId);
        //}
        ////任务名字
        //public string GetNpcTaskNameByKey(string key)
        //{
        //    return HZLanguageManager.Instance.GetString(key);
        //}

        ////任务id获得任务名字
        //public string GetNpcTaskName(int QuestId)
        //{
        //    return HZLanguageManager.Instance.GetString(QuestId+"_name");
        //}
        //字符解析
        private string[] GetContextBySplit(string str, params char[] separator)
        {
            string[] arraydata = str.Split(separator);
            return arraydata;
        }
        //是否有交谈任务
        public bool HasQuestByTalk(int Templateid)
        {
            List<int> mNpcQuestList;

            if (CurrentNpcQuestList.TryGetValue(Templateid, out mNpcQuestList))
            {
                foreach (var questid in mNpcQuestList)
                {
                    Quest quest = DataMgr.Instance.QuestData.GetQuest(questid);
                    if (quest != null)
                    {
                        if (quest.state == QuestState.NotCompleted)
                        {
                            foreach (var questprogdata in quest.progress)
                            {
                                if (questprogdata.Type == TLQuestCondition.FindNPC)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }



        /// <summary>
        ///根据模板获得当前任务
        /// </summary>
        private List<int> GetQuestIdByTemplateId(int Templateid)
        {
            List<int> questidList;
            if (CurrentNpcQuestList.TryGetValue(Templateid, out questidList))
            {
                return questidList;
            }
            else
            {
                //Debugger.Log("npc "+ Templateid+" haven't any quest");
            }
            return null;
        }
        //获得接取任务npc模板id
        private int GetTemplateIDByState(int questid, int state)
        {
            try
            {
                Quest quest = DataMgr.Instance.QuestData.GetQuest(questid);
                if (quest != null)
                {
                    if (state == (int)QuestState.NotAccept)
                    {
                        return quest.GetStatic().GetInt("npc_push");
                    }
                    else
                    {
                        return quest.GetStatic().GetInt("npc_submit");
                    }

                }
            }
            catch (Exception e)
            {
                Debugger.LogError(e.Message + "\n" + e.StackTrace);
            }

            return 0;
        }
        //根据任务id获得对应任务状态npc模板id
        private int GetTemplateIDCurrentQuestState(int questid, Quest quest = null)
        {
            try
            {
                if (quest == null)
                {
                    quest = DataMgr.Instance.QuestData.GetQuest(questid);
                }

                if (quest != null)
                {
                    if (quest.state == QuestState.NotAccept)
                    {
                        return quest.GetStatic().GetInt("npc_push");
                    }
                    else
                    {
                        return quest.GetStatic().GetInt("npc_submit");
                    }
                }
            }
            catch (Exception e)
            {
                Debugger.LogError(e.Message + "\n" + e.StackTrace);
            }

            return 0;
        }
        public bool NpchasQuest(Quest questdata)
        {
            Dictionary<string, object> npcquestdata = null;
            if (questdata == null)
            {
                return false;
            }
            if (questdata.state == QuestState.Remove || questdata.state == QuestState.Submited)
            {
                return false;
            }
            //Debugger.LogError("questdataid =  "+ questdata.id+ " queststate= "+questdata.state);
          
            npcquestdata = GameUtil.GetDBData("Quest", questdata.id);
            
            if (npcquestdata == null)
            {
                Debugger.Log("No this quest withid=" + questdata.id);
                return false;
            }
            
            var acceptmapid = Convert.ToInt32(npcquestdata["npc_push_sceneid"]);
            if (questdata.state == QuestState.NotAccept )
            {
                if (acceptmapid != 0 && acceptmapid == DataMgr.Instance.UserData.MapTemplateId)
                {
                    return true;
                }
                else if(acceptmapid == 0)
                {
                    //Debugger.Log("npc_push_sceneid is 0 with questid " + questdata.id);
                    return false;
                }
            }
            acceptmapid = Convert.ToInt32(npcquestdata["npc_submit_sceneid"]);
            if ((questdata.state == QuestState.CompletedNotSubmited || questdata.state == QuestState.NotCompleted) )
            {
                if (acceptmapid != 0 && acceptmapid == DataMgr.Instance.UserData.MapTemplateId)
                {
                    return true;
                }
                else if (acceptmapid == 0)
                {
                    //Debugger.Log("npc_submit_sceneid is 0 with questid " + questdata.id);
                    return false;
                }
            }
            return false;
        }
        //获得npc身上的任务列表
        public List<TLQuest> GetNpcQuestData(int TemplateId)
        {
            List<TLQuest> npcDataList = new List<TLQuest>();
            try
            { 
                List<int> questidlist = GetQuestIdByTemplateId(TemplateId);
                if (questidlist != null)
                {
                    foreach (var questid in questidlist)
                    {
                        TLQuest value = DataMgr.Instance.QuestData.GetQuest(questid);
                        if (value == null)
                        {
                            Debugger.LogError("Quest is nil with id "+ questid);
                        }
                        else
                        {
                            if (NpchasQuest(value))
                            {
                                npcDataList.Add(value);
                            }
                            else if (value.state == QuestState.NotCompleted)
                            {
                                foreach (var prog in value.progress)
                                {
                                    if (prog.CurValue != prog.TargetValue)
                                    {
                                        if (prog.Type == "eFindNPC")
                                        {
                                            if (DataMgr.Instance.UserData.MapTemplateId == prog.Arg2 && prog.Arg1 == TemplateId)
                                            {
                                                npcDataList.Add(value);
                                            }
                                        }

                                    }
                                }
                            }
                           
                        }
                    }
                    SortList(npcDataList);
                }
            }
             catch (Exception e)
            {
                Debugger.LogError(e.Message + "\n" + e.StackTrace);
            }
            return npcDataList;
        }

        //获得npc身上的任务列表
        public List<TLQuest> GetNpcLoopQuestData(int TemplateId)
        {
            List<TLQuest> npcDataList = new List<TLQuest>();
            try
            {
                List<int> questidlist = GetQuestIdByTemplateId(TemplateId);
                if (questidlist != null)
                {
                    foreach (var questid in questidlist)
                    {
                        TLQuest value = DataMgr.Instance.QuestData.GetQuest(questid);
                        if (NpchasQuest(value))
                        {
                            npcDataList.Add(value);
                        }
                    }
                }
                SortList(npcDataList);
            }
            catch (Exception e)
            {
                Debugger.LogError(e.Message + "\n" + e.StackTrace);
            }
            return npcDataList;
        }

        //获得任务接取状态
        private int GetQuestAutoData(int questid)
        {

            Quest quest = DataMgr.Instance.QuestData.GetQuest(questid);
            if (quest != null)
            {
                return quest.GetStatic().GetInt("automatic");
            }
            Debugger.Log("no questid = "+ questid);
            return 0;
        }
        public delegate int OnSortCompareHandle(TLQuest ntd1, TLQuest ntd2);
        public OnSortCompareHandle onCompare = null;
        [DoNotToLua]
        public void SortList(List<TLQuest> quest)
        {
            quest.Sort((TLQuest ntd1, TLQuest ntd2) =>
            {
                if (onCompare != null)
                {
                   return onCompare(ntd1, ntd2);
                }
                if (ntd1.mainType != ntd2.mainType)
                {
                    return ntd2.id.CompareTo(ntd1.id);
                }
                if (ntd1.state == ntd2.state)
                    return ntd2.id.CompareTo(ntd1.id);
                if (ntd1.state >= QuestState.CompletedNotSubmited)
                    return -1;
                if (ntd1.state == QuestState.NotAccept)
                {
                    return -1;
                }
                return ntd2.state.CompareTo(ntd1.state);
            });
        } 
        //打开npc对话框
        [DoNotToLua]
        public void TalkWithNpcByTemplateId(int TemplateId,int questid,bool OpenFunction = false)
        {
            var unit = TLBattleScene.Instance.GetUnitByTemplateId(TemplateId);
            if ( unit != null && unit is TLAINPC)
            {
                if (OpenFunction)
                {
                    TalkWithNpcByUnit(unit, questid);
                }
                else
                {
                    var list = GetNpcQuestData(TemplateId);
                    var quest = list.Find((e) =>
                    {
                        return e.id == questid;
                    });
                    if (quest != null)
                    {
                        TalkWithNpcByUnit(unit, questid);
                    }
                }
               
                
            }
            else
            {
                Debugger.Log("Not Found Npc by TemplateId = " + TemplateId);
            }
        }
        

        public void TalkWithNpcByUnit(TLAIUnit unit,int questid = 0)
        {
            
            if (unit == null)
            {
                Debugger.LogError("TalkWithNpcByUnit arg is null ");
                return;
            }
            if (unit is TLAINPC)
            {
                //unit.PlayAnim("n_talk", true, WrapMode.Once);
                //Debug.LogError("TalkWithNpcByUnit "+ unit.Info.TemplateID+ "   "+questid);
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("TemplateId", unit.Info.TemplateID);
                param.Add("QuestId", questid);
                param.Add("NpcUnitName", unit.Name());
                EventManager.Fire("EVENT_UI_NPCTALK", param);
            }
        }

        public override void Notify(string evtName, TLQuest quest)
        {
            
            UpdateQuest(evtName,quest);
            if (evtName == "Event.Quest.Submited")
            {
                SubmitedQuest();
            }
            else if (evtName == "Event.Quest.RemoveQuest")
            {
                RemoveQuest(quest);
            }
            
        }
    }
}
