using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SLua;
using TLProtocol.Data;
using TLClient.Modules;
using ThreeLives.Client.Common.Modules.Quest;
using System;

public struct QuestType
{
    public const int TypeNone = 0;
    public const int TypeStory = 1;
    public const int TypeGuide = 2;
    public const int TypeDaily = 3;
    public const int TypeTip = 100;
}

public static class QuestExtension
{
    public static LuaTable GetStatic(this Quest quest)
    {
        TLQuest quest1 = quest as TLQuest;
        return quest1 != null ? quest1.Static : null;
    }
}


public class TLQuest : Quest
{
    private LuaTable luaData;
    public bool tracing;
    public TLAIActor.MoveEndAction TempAction;
    public LuaTable Static
    {
        get
        {   
            
            if (luaData == null )
            {
                string lua = string.Format("return GlobalHooks.DB.Find('Quest', {0})", id);
                //Debugger.LogError("lua=" + lua);
                luaData = (LuaTable)LuaSystem.Instance.LoadString(lua);
                if (luaData == null)
                {
                    Debugger.LogError("quest id=" + id+" is not exist");
                }
            }
            return luaData;
        }
    }

    public int ProgressMax
    {
        get
        {
            var count = 0;
            foreach(var prog in progress){
                count += prog.TargetValue;
            }
            return count;
            //if (progress.Count == 0)
            //    return 0;
            //if (progress.Count == 1)
            //    return progress[0].TargetValue;

            //return progress.Count;
        }
    }
    public int ProgressCur
    {
        get
        {
            //if (progress.Count == 0)
            //    return 0;
            //if (progress.Count == 1)
            //    return progress[0].CurValue;
            var count = 0;
            foreach (var prog in progress)
            {
                count += prog.CurValue;
            }
            return count;
            //return 0;
            //int n = 0;
            //foreach (var v in progress)
            //{
            //    if (v.IsEnough())
            //        n++;
            //}
            //return n;
        }
    }

    public int ConditionCount { get { return progress.Count; } }

    public bool IsEnoughCondition(int idx)
    {
        if (idx < 0 || idx > progress.Count)
            return false;
        return progress[idx].CurValue == progress[idx].TargetValue;
    }

    public int ConditionCur(int idx)
    {
        if (idx < 0 || idx > progress.Count)
            return 0;
        return progress[idx].CurValue;
    }

    public int ConditionMax(int idx)
    {
        if (idx < 0 || idx > progress.Count)
            return 0;
        return progress[idx].TargetValue;
    }

    public TLQuest(QuestDataSnap snap)
    {
        id = snap.id;
        state = snap.state;
        progress = snap.ProgressList;
        curLoopNum = snap.curLoopNum;
        MaxLoopNum = snap.MaxLoopNum;
        TimeLife = snap.TimeLife;
        mainType = snap.mainType;//Static.GetInt("pir_type");//snap.type;
        subType = snap.subType;//Static.GetInt("sub_type");
        luaData = null;
        tracing = true;
    }
    public TLQuest()
    {
        luaData = null;
    }

    public override void Dispose()
    {
        base.Dispose();
        if (luaData != null)
        {
            luaData.Dispose();
            luaData = null;
        }
    }
    ~TLQuest()
    {
        Dispose();
    }
}
public abstract class QuestDataListener
{
    public abstract void Notify(string evtName, TLQuest quest);
}


public class QuestData 
{
    private Dictionary<string, LuaTable> mLuaObservers = new Dictionary<string, LuaTable>();
    private List<QuestDataListener> mObservers = new List<QuestDataListener>();
    public static TLQuestModule QuestModule()
    {
        var client = TLNetManage.Instance.NetClient;
        if (client != null)
            return client.questModule;
        return null;
    }

    public Dictionary<int, Quest> AllQuests
    {
        get
        {
            if (QuestModule() != null)
            {
                return QuestModule().AllQuests;
            }
            return null;
        }
    }

    public List<Quest> Accepts
    {
        get
        {
            if (QuestModule() != null)
                return QuestModule().Accepts;
            return null;
        }
    }
    public List<Quest> NotAccepts
    {
        get
        {
            if (QuestModule() != null)
                return QuestModule().NotAccepts;
            return null;
        }
    }

    private List<LuaFunction> luaObservers = new List<LuaFunction>();
    public bool IsInit { get; set; }
    
    public TLQuest GetQuest(int id)
    {
       

        var quests = AllQuests;
        if (quests != null)
        {
            Quest quest;
            quests.TryGetValue(id, out quest);
            return quest as TLQuest;
        }
        return null;
    }

    public void RemoveQuest(int id)
    {
        var quests = AllQuests;
        if (quests != null)
        {
            quests.Remove(id);
        }
           
    }

    public bool GetQuestConditionTypeIsNotEnough(string conditionType, int targetId)
    {

        if (Accepts != null)
        {
            foreach (var quest in Accepts)
            {

                foreach (var p in quest.progress)
                {
                   
                    if (p.Type == conditionType && p.Arg1 == targetId)
                    {
                        return p.CurValue < p.TargetValue;
                        //if (p.IsEnough() == false)
                        //{
                        //    return true;
                        //}
                    }
                }
            }

        }
        return false;
    }

    public QuestData()
    {
        // test
        //QuestDataSnap snap = new QuestDataSnap();
        //snap.id = 1001;
        //snap.type = 1;
        //snap.state = QuestState.NotCompleted;
        //snap.progressList = new List<TaskProgress>();
        //snap.progressList.Add(new TaskProgress());
        //snap.progressList[0].objTargetNum = 100;
        //snap.progressList[0].progressData = new TaskProgressData();
        //snap.progressList[0].progressData.objNum = 1;
        //snap.progressList[0].progressData.objType = 1;
        //snap.progressList[0].progressData.objId = 0;
        //UpdateOneSnap(snap);

    }

    public void InitNetWork()
    {

        QuestModule().CreateQuest = CreateQuest;
        //Debug.LogError("QuestDataInitNetWork---------------");
        QuestModule().Notify = Notify;
        ((TLQuestModule)QuestModule()).CreateQuestItemSnap = CreateQuestItemSnap;
        //EventManager.Fire("Event.Quest.InitNetWork", EventManager.defaultParam);
       
    }

    //public void OnQuestNotify(List<QuestDataSnap> snaps)
    //{
        //IsInit = true;
        
        //EventManager.Fire("Event.Quest.ClearData", EventManager.defaultParam);
        //var questModle = QuestModule();

        //Quest quest;
        //if (snaps == null)
        //{
        //    Debugger.Log("tasklsit is null");
        //    return;
        //}
        //for (int i = 0; i < snaps.Count; i++)
        //{
        //    if (!questModle.AllQuests.TryGetValue(snaps[i].id, out quest))
        //    {
        //        questModle.UpdateOneSnap(snaps[i]);
        //    }
        //}
        //IsInit = false;
        //GameGlobal.Instance.StartCoroutine(
        //    GameGlobal.WaitForSeconds(0,
        //        () =>
        //        {
        //            IsInit = true;
                    
        //            IsInit = false;
        //        }
        //    ));
    //}

    public Quest CreateQuest(QuestDataSnap snap)
    {
        return new TLQuest(snap);
    }

    public ItemSnapData CreateQuestItemSnap(int questId, int itemId, int count)
    {
        string lua = string.Format("return GlobalHooks.DB.Find('Item', {0})", itemId);
        LuaTable luaData = (LuaTable)LuaSystem.Instance.LoadString(lua);

        var it = new ItemSnapData()
        {
            ID = questId.ToString(),
            TemplateID = itemId,
            Count = (uint)count,
        };

        if (luaData == null)
        {
            Debug.LogErrorFormat("can not found taskItemId:{0} taskId:{1} in excel", itemId, questId);
        }
        //public byte quality;
        return it;
    }


    public void Clear(bool reLogin, bool reConnect)
    {
        if (reLogin)
        {
            //foreach (var fun in luaObservers)
            //{
            //    if (fun != null)
            //        fun.Dispose();
            //}
            //luaObservers.Clear();
            mLuaObservers.Clear();
            mObservers.Clear();
        }
    }
    public void AttachLuaObserver(string key, LuaTable t)
    {
        mLuaObservers[key] = t;
    }

    public void DetachLuaObserver(string key)
    {
        mLuaObservers.Remove(key);
    }

    public void AttachObserver(QuestDataListener ob)
    {
        mObservers.Add(ob);
    }

    public void DetachObserver(QuestDataListener ob)
    {
        mObservers.Remove(ob);
    }

   
    public void Notify(string evtName, Quest quest)
    {
       
        //EventManager.Fire(evtName, EventManager.SimpleParam("quest", quest));
        if (evtName == "Event.Quest.Init")
        {
            foreach (var ob in mLuaObservers)
            {
                ob.Value.invoke("Notify", new object[] { evtName, null, null });
            }
            return;
        }
        foreach (var ob in mObservers)
        {
            ob.Notify(evtName, (TLQuest)quest);
        }
        foreach (var ob in mLuaObservers)
        {
            ob.Value.invoke("Notify", new object[] { evtName, quest.id, quest });
        }
       
        //foreach (var ob in luaObservers)
        //{
        //    ob.call(evtName, this, quest.id, quest);
        //}
    }

    [DoNotToLua]
    public static TLQuest ParseEvent(EventManager.ResponseData res)
    {
        var dict = res.data[1] as Dictionary<string, object>;
        if (dict != null)
        {
            object t;
            if (dict.TryGetValue("quest", out t))
            {
                return (TLQuest)t;
            }
        }
        return null;
    }
    

   
}
