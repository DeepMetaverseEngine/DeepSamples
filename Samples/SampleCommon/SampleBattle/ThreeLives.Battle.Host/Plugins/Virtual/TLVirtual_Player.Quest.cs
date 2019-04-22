using DeepCore;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Server.Message;

namespace TLBattle.Server.Plugins.Virtual
{
    public partial class TLVirtual_Player
    {
        private HashMap<int, int> interActiveMap = new HashMap<int, int>();

        public int GetInteractiveNum(int targetId)
        {
            return interActiveMap.Get(targetId);
        }

        public void addInterActiveNum(int targetId, int num)
        {
            if (num <= 0)
            {
                return;
            }

            if (interActiveMap.ContainsKey(targetId))
            {
                if (GetInteractiveNum(targetId) < num)
                {
                    interActiveMap[targetId] = num;
                }
            }
            else
            {
                interActiveMap.Add(targetId, num);
            }
        }

        public void ZoneGetInterActiveItem(int targetId, int num)
        {
            if (interActiveMap.ContainsKey(targetId))
            {
                var hasNum = GetInteractiveNum(targetId);
                if (hasNum > 0)
                {
                    if (hasNum - num > 0)
                    {
                        interActiveMap[targetId] = hasNum - num;
                    }
                    else
                    {
                        interActiveMap.RemoveByKey(targetId);
                    }
                }

            }
            else
            {
                interActiveMap.Add(targetId, num);
            }
        }

        //放弃目标
        public void GiveUptInterActive(int targetId)
        {
            interActiveMap.RemoveByKey(targetId);
        }

        /// <summary>

        private void InitQuestData()
        {
            if (mProp.ServerData.Quests == null) { return; }


            if (mUnit is InstancePlayer)
            {
                List<QuestData> tasks = new List<QuestData>();
                foreach (TLQuestData var in mProp.ServerData.Quests)
                {
                    QuestData qData = new QuestData(var.TaskID);
                    if (var.TaskState == "0")
                    {
                        //已接受
                        qData.State = QuestState.Accepted;
                    }
                    if (var.Attributes != null)
                    {
                        //合并相同key的value值
                        foreach (QuestAttribute item in var.Attributes)
                        {
                            updateQuestData(qData, item.Key, item.Value);
                        }
                    }
                    tasks.Add(qData);
                }


                (mUnit as InstancePlayer).InitQuestData(tasks);


            }

        }

        private void updateQuestData(QuestData qData, string Key, string newValue)
        {
            string value;
            if (qData.Attributes.TryGetValue(Key, out value))
            {
                value = value + ';' + newValue;
                qData.Attributes[Key] = value;
            }
            else
            {
                qData.Attributes.Add(Key, newValue);
            }

            addInterActiveNum(Key, newValue);
        }


        private void addInterActiveNum(string Key, string newValue)
        {
            if (Key == TLQuestData.Attribute_InterActive)
            {
                char[] c1 = { ',' };
                string[] result = newValue.Split(c1, StringSplitOptions.RemoveEmptyEntries);

                if (result != null && result.Length == 2)
                {
                    int targetId = 0;
                    int targetNum = 0;
                    if (int.TryParse(result[0], out targetId) && int.TryParse(result[1], out targetNum))
                    {
                        if (targetId > 0 && targetNum > 0)
                        {
                            addInterActiveNum(targetId, targetNum);
                        }
                    }
                }
            }
        }


        private void GiveUptInterActive(QuestData qData)
        {
            string data = GetInterActiveData(qData);
            if (!string.IsNullOrEmpty(data))
            {
                char[] c1 = { ';' };
                char[] c2 = { ',' };


                string[] itemStrs = data.Split(c1, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in itemStrs)
                {

                    string[] result = item.Split(c2, StringSplitOptions.RemoveEmptyEntries);

                    if (result != null && result.Length == 2)
                    {

                        int targetId = 0;
                        int targetNum = 0;

                        if (int.TryParse(result[0], out targetId) && int.TryParse(result[1], out targetNum))
                        {
                            if (targetId > 0 && targetNum > 0)
                            {
                                GiveUptInterActive(targetId);
                            }
                        }
                    }

                }

            }
        }


        public void AcceptQuest(QuestAcceptedR2B msg)
        {
            string playerUUID = msg.playerUUID;
            string questID = msg.questID;

            this.mUnit.Parent.QuestAdapter.OnQuestAcceptedHandler(playerUUID, questID);

            if (msg.status != null)
            {
                QuestData qData = new QuestData(msg.questID);

                //合并相同key的value值
                foreach (var item in msg.status)
                {
                    updateQuestData(qData, item.Key, item.Value);
                }

                foreach (var item in qData.Attributes)
                {
                    this.mUnit.Parent.QuestAdapter.OnQuestStatusChangedHandler(playerUUID, questID, item.Key, item.Value);
                }

            }
        }


        public void DropQuest(QuestDroppedR2B msg)
        {
            string playerUUID = msg.playerUUID;
            string questID = msg.questID;
            var qData = (mUnit as InstancePlayer).GetQuest(questID);
            if (qData != null)
            {
                GiveUptInterActive(qData);
                this.mUnit.Parent.QuestAdapter.OnQuestDroppedHandler(playerUUID, questID);
            }
        }

        public void CommitQuest(QuestCommittedR2B msg)
        {
            string playerUUID = msg.playerUUID;
            string questID = msg.questID;
            var qData = (mUnit as InstancePlayer).GetQuest(questID);
            if (qData != null)
            {
                GiveUptInterActive(qData);
                this.mUnit.Parent.QuestAdapter.OnQuestCommittedHandler(playerUUID, questID);
            }

        }

        //public static int GetIntValueByString(string value)
        //{
        //    int ret = 0;
        //    int.TryParse(value, out ret);
        //    return ret;
        //}

        //public static int GetTaskType(QuestData qData)
        //{
        //    string taskType = qData.Attributes.Get(TLQuestData.Attribute_TaskType);
        //    return GetIntValueByString(taskType);
        //}

        //public static int GetInterActiveTargetID(QuestData qData)
        //{
        //    string targetId = qData.Attributes.Get(TLQuestData.Attribute_InterActiveTargetId);
        //    int result = 0;
        //    if (!string.IsNullOrEmpty(targetId))
        //    {
        //        Int32.TryParse(targetId, out result);
        //    }
        //    return result;
        //}

        public static string GetInterActiveData(QuestData qData)
        {
            return qData.Attributes.Get(TLQuestData.Attribute_InterActive);
        }

        //public static int GetInterActiveTargetNum(QuestData qData)
        //{
        //    string targetNum = qData.Attributes.Get(TLQuestData.Attribute_InterActiveTargetNum);
        //    return GetIntValueByString(targetNum);
        //}

    }
}
