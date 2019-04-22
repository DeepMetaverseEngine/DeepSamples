using DeepCore.Log;
using DeepMMO.Client;
using System;
using System.Collections.Generic;
using ThreeLives.Client.Common.Modules.Quest;
using TLClient.Modules.Bag;
using TLClient.Protocol.Modules;
using TLClient.Protocol.Modules.Package;
using TLProtocol.Data;
using TLProtocol.Protocol.Client;

namespace TLClient.Modules
{
    public delegate void TLQuestNotify(string eventName, Quest quest);
    public delegate Quest TLQuestCreateQuest(QuestDataSnap snap);
    public delegate ItemSnapData TLCreateQuestItemSnap(int questId, int itemId, int count);


    public struct QuestState
    {
        public const int NotAccept = 1;
        public const int NotCompleted = 2;
        public const int CompletedNotSubmited = 3;
        public const int Fail = 4;
        public const int Remove = 5;
        public const int Submited = 6;
    }

    public class Quest : IDisposable
    {
        public int id;
        public int state;
        public int mainType;
        public int subType;
        public int curLoopNum;
        public int MaxLoopNum;
        public DateTime TimeLife;
        public List<TLProgressData> progress;
        private bool isCalcQuestItem = false;

        public List<int> questItemIds { get; set; }

        public bool hasQuestItem
        {
            get
            {
                if (!isCalcQuestItem)
                {
                    bool hasQuestItem = false;
                    questItemIds = new List<int>();
                    for (int i = 0; i < progress.Count; i++)
                    {
                        if (progress[i].Type == TLQuestCondition.GetVirtualItem)
                        {
                            hasQuestItem = true;
                            questItemIds.Add(progress[i].Arg1);
                        }
                        else
                            questItemIds.Add(0);
                    }
                    if (!hasQuestItem)
                        questItemIds = null;
                    isCalcQuestItem = true;
                }
                return questItemIds != null;
            }

        }

        public int GetQuestItemCount(int itemId)
        {
            if (!hasQuestItem)
                return -1;
            for (int i = 0; i < questItemIds.Count; i++)
            {
                if (questItemIds[i] == itemId)
                    return progress[i].CurValue;
            }
            return -1;
        }

        public bool IsConditionFinish(int index)
        {
            if (progress != null && index < progress.Count)
            {
                return progress[index].TargetValue == progress[index].CurValue;
            }
            return false;
        }

        public virtual void Dispose()
        {

        }
    }

    public class TLQuestModule : RPGClientModule
    {
        protected Logger mLogger;

        public Dictionary<int, Quest> AllQuests { get; private set; }
        public List<Quest> Accepts { get; private set; }
        public List<Quest> NotAccepts { get; private set; }
        public TLQuestNotify Notify;
        public TLQuestCreateQuest CreateQuest;
        public TLCreateQuestItemSnap CreateQuestItemSnap;

        public enum QuestType
        {
            TypeStory = 1,
            TypeGuide = 2,
            TypeDaily = 3,
            TypeTip = 100,// 提醒类任务
        }
        private DeepCore.FuckPomeloClient.PushHandler pushHandler;

        public ClientSimpleExternBag Bag;

        public TLQuestModule(RPGClient client) : base(client)
        {
            mLogger = LoggerFactory.GetLogger(GetType().Name);

            AllQuests = new Dictionary<int, Quest>();
            Accepts = new List<Quest>();
            NotAccepts = new List<Quest>();
            Bag = new ClientSimpleExternBag(99, client.GameClient);
            Bag.Size = 100;
            pushHandler = game_client.Listen<TLClientQuestDataNotify>(OnQuestDataNotify);
        }

        public override void OnStart()
        {
        }

        public override void OnStop()
        {

        }


        protected override void Disposing()
        {
            if (pushHandler != null)
            {
                pushHandler.Clear();
                pushHandler = null;
            }

            foreach (var pair in AllQuests)
            {
                pair.Value.Dispose();
            }
            AllQuests.Clear();
            Accepts.Clear();
            NotAccepts.Clear();
            Bag.Dispose();
        }

        protected virtual Quest DoCreateTask(QuestDataSnap snap)
        {
            if (CreateQuest != null)
                return CreateQuest.Invoke(snap);

            Quest quest = new Quest()
            {
                id = snap.id,
                state = snap.state,
                mainType = snap.mainType,
                subType = snap.subType,
                curLoopNum = snap.curLoopNum,
                MaxLoopNum = snap.MaxLoopNum,
                TimeLife = snap.TimeLife,
                progress = snap.ProgressList
            };
            return quest;
        }

        protected virtual ItemSnapData DoCreateQuestItemSnap(int questId, int itemId, int count)
        {
            if (CreateQuestItemSnap != null)
                return CreateQuestItemSnap.Invoke(questId, itemId, count);

            var it = new ItemSnapData();
            it.ID = questId.ToString();
            it.TemplateID = itemId;
            it.Count = (uint)count;
            //public int maxStack;
            //public string name;
            //public byte quality;
            //public string icon;
            return it;
        }

        public virtual void OnQuestDataNotify(TLClientQuestDataNotify notify)
        {
            
            OnQuestNotify(notify.s2c_data);
            if (notify.OperatorType == TLClientQuestDataNotify.Op_Init)
            {
                if (Notify != null)
                    Notify.Invoke("Event.Quest.Init", null);
            }
        }
        
        public virtual void OnQuestNotify(List<QuestDataSnap> snaps)
        {

            if (snaps == null) return;

            for (int i = 0; i < snaps.Count; i++)
            {
                //log.Debug("ReciveOnQuestNotify " + snaps[i].id + snaps[i].state);
                UpdateOneSnap(snaps[i]);
            }
        }

        private void UpdateBagItem(Quest quest)
        {
            if (!quest.hasQuestItem)
                return;

            int muilt = quest.state == QuestState.NotCompleted || quest.state == QuestState.CompletedNotSubmited ? 1 : 0;

            var list = quest.questItemIds;
            for (int i = 0; i < list.Count; i++)
                UpdateBagItem(list[i], quest.id, quest.GetQuestItemCount(list[i]) * muilt);
        }
        public bool IsAlwaysInAcceptTask(Quest snap)
        {
            if (snap.mainType == (int)QuestType.TypeStory || snap.mainType == (int)QuestType.TypeDaily)
            {
                return true;
            }
            return false;
        }
        private void UpdateBagItem(int itemId, int questId, int count)
        {
            int index = -1;
            int i = 0;
            IPackageItem item = null;
            Bag.FindItemAs<ItemData>((v) =>
            {
                if (item != null)
                    return false;

                if (v != null && v.TemplateID == itemId && v.ID == questId.ToString())
                {
                    item = v;
                    index = i;
                    return false;
                }
                if (index == -1 && v == null)
                    index = i;
                i++;
                return false;
            });

            if (item != null)
            {
                if (item.Count != count)
                    Bag.UpdateItemCount(index, (uint)count);
                return;
            }
            if (count <= 0)
                return;

            ItemSnapData it = DoCreateQuestItemSnap(questId, itemId, count);
            var itemNew = new ItemData(new EntityItemData() { SnapData = it });
            Bag.AddItem(index, itemNew);
        }
        //private Logger log = LoggerFactory.GetLogger("quest");
        public virtual void UpdateOneSnap(QuestDataSnap snap)
        {
            Quest quest = null;
            //log.Debug("UpdateQuest " + snap.id + " queststate " + snap.state);
            if (AllQuests.TryGetValue(snap.id, out quest))
            {

                UpdateQuest(quest, snap);
            }
            else if (snap.state >= QuestState.NotAccept || snap.state <= QuestState.CompletedNotSubmited)
            {
                //log.Debug("DoCreateTask "+snap.id+ " quest.state "+ snap.state);
                quest = DoCreateTask(snap);//CreateQuest(snap);
                AllQuests.Add(quest.id, quest);

                if (quest.state == QuestState.NotAccept
                    && !IsAlwaysInAcceptTask(quest))
                {
                    NotAccepts.Add(quest);
                }
                else if (IsAlwaysInAcceptTask(quest) || quest.state == QuestState.NotCompleted || quest.state == QuestState.CompletedNotSubmited)
                {
                    Accepts.Add(quest);
                    UpdateBagItem(quest);
                }
                if (Notify != null)
                    Notify.Invoke("Event.Quest.AddQuest", quest);

            }
        }

        protected virtual void UpdateQuest(Quest quest, QuestDataSnap snap)
        {
            int oldState = quest.state;
            quest.state = snap.state;
            quest.progress = snap.ProgressList;

            UpdateBagItem(quest);

            if (oldState != quest.state)
                UpdateQuestState(quest);
            else if (Notify != null)
                Notify.Invoke("Event.Quest.ProgressesChange", quest);
        }

        protected virtual void UpdateQuestState(Quest quest)
        {
            if (quest.state == QuestState.Submited)
            {
                if (Notify != null)
                    Notify.Invoke("Event.Quest.Submited", quest);
                return;
            }
            if (quest.state == QuestState.Remove)
            {
                Accepts.Remove(quest);
                NotAccepts.Remove(quest);
                AllQuests.Remove(quest.id);
                if (Notify != null)
                    Notify.Invoke("Event.Quest.RemoveQuest", quest);
                quest.Dispose();
            }
            else if (quest.state == QuestState.NotCompleted || quest.state == QuestState.CompletedNotSubmited)
            {
                NotAccepts.Remove(quest);
                if (!Accepts.Contains(quest))
                {
                    Accepts.Add(quest);
                }
                if (Notify != null)
                {
                    if (quest.state == QuestState.NotCompleted)
                    {
                        Notify.Invoke("Event.Quest.Accept", quest);
                    }
                    else if (quest.state == QuestState.CompletedNotSubmited)
                    {
                        Notify.Invoke("Event.Quest.Complete", quest);
                    }
                }

            }
            else if (quest.state == QuestState.NotAccept)
            {
                if (!IsAlwaysInAcceptTask(quest) && Accepts.Remove(quest))
                {
                    if (Notify != null)
                        Notify.Invoke("Event.Quest.RemoveQuest", quest);
                }

                if (!NotAccepts.Contains(quest) && !IsAlwaysInAcceptTask(quest))
                    NotAccepts.Add(quest);
                if (Notify != null)
                    Notify.Invoke("Event.Quest.AddQuest", quest);
            }
            else
            {
                mLogger.ErrorFormat("incorrect quest state {0} for quest {1}", quest.state, quest.id);
            }
        }

    }
}