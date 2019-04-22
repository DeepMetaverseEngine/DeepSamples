using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using SLua;


using TLProtocol.Data;
using TLProtocol.Protocol.Client;

public class MsgData : ISubjectExt<MsgData>
{

    public enum NotiFyStatus : int
    {
        AddMsg = 0,
        RemoveMsg = 1,
        AgreeSync = 2,
        //所有标志位
        ALL = int.MaxValue,
    }

    public enum MsgResult
    {
        Accept = 1,
        Refuse = 2
    }

    private Dictionary<AlertMessageType, List<MsgInfo>> mMsgQueue = new Dictionary<AlertMessageType, List<MsgInfo>>();

    private HashSet<IObserverExt<MsgData>> mObservers = new HashSet<IObserverExt<MsgData>>();
    private Dictionary<string, LuaTable> mLuaObservers = new Dictionary<string, LuaTable>();

    public List<MsgInfo> GetAlertMessageByType(AlertMessageType type)
    {
        if (mMsgQueue.ContainsKey(type))
        {
            return mMsgQueue[type];
        }
        return null;
    }



    public void InitNetWork()
    {
        TLNetManage.Instance.Listen<ClientMessageAddNotify>(OnMessageAddReceived);
        TLNetManage.Instance.Listen<ClientAlertMessageNotify>(OnClientAlertMessageNotify);
    }

    public MsgInfo GetMessage(string id)
    {
        foreach (var entry in mMsgQueue)
        {
            foreach (var info in entry.Value)
            {
                if (info.Id == id)
                {
                    return info;
                }
            }
        }
        return null;
    }

    private void OnClientAlertMessageNotify(ClientAlertMessageNotify ntf)
    {
        var msg = GetMessage(ntf.s2c_id);
        if (msg != null && msg.SyncPlayers != null)
        {
            var p = msg.SyncPlayers.FirstOrDefault(m => m.id == ntf.s2c_roleID);
            if (p != null)
            {
                p.agree = ntf.s2c_agree ? AlertHandlerType.Agree : AlertHandlerType.Cancel;
                Notify((int)NotiFyStatus.AgreeSync, p);
            }

        }
    }

    private void OnMessageAddReceived(ClientMessageAddNotify msg)
    {
        MessageSnap mData = msg.s2c_data;
        if (mData != null)
        {
            MsgInfo info = new MsgInfo();
            info.Id = mData.id;
            info.Type = mData.type;
            info.Content = mData.strMsg;
            info.WaitTime = mData.lifeTimeMS / 1000f;
            info.FromRoleID = mData.fromRoleID;
            info.TargetRoleID = mData.targetRoleID;
            info.SyncPlayers = mData.syncPlayers;

            AddMessage(info);
            RefreshIcon(NotiFyStatus.AddMsg);
            if (info.SyncPlayers != null)
            {
                foreach (var player in info.SyncPlayers)
                {
                    Debug.Log(info.Id + " player " + info.Id + " " + player.agree);
                }
            }
            if (info.Type == AlertMessageType.TeamEnterMap || info.Type == AlertMessageType.EventEntermap)
            {
                info.Content = HZLanguageManager.Instance.GetString(info.Content);
                MenuMgr.Instance.OpenUIByTag("PlayerEnterMap", 0, new object[] { info });
            }
            else if (info.Type == AlertMessageType.PlayerEnterMap)
            {
                MenuMgr.Instance.OpenUIByTag("EnterMapPopup", 0, new object[] { info });
            }
            else if (info.Type == AlertMessageType.MarryInvite)
            {
                var dialog = AlertDialogUI.CreateFromXml("xml/marry/ui_marry_propose.gui.xml");
                dialog.SetContent(info.Content);
                dialog.SetAnchor(DeepCore.GUI.Data.TextAnchor.C_C);
                dialog.OnOkBtnClick = OnMsgAccept;
                dialog.OnCancelBtnClick = OnMsgRefuse;
                GameAlertManager.Instance.AlertDialog.ShowAlertDialog(dialog, AlertDialog.PRIORITY_NORMAL, info);
            }
            else if (info.Type == AlertMessageType.DivorceInvite)
            {
                var dialog = AlertDialogUI.CreateFromXml("xml/marry/ui_marry_agreedivorce.gui.xml");
                dialog.SetContent(info.Content);
                dialog.SetAnchor(DeepCore.GUI.Data.TextAnchor.C_C);
                dialog.OnOkBtnClick = OnMsgAccept;
                dialog.OnCancelBtnClick = OnMsgRefuse;
                GameAlertManager.Instance.AlertDialog.ShowAlertDialog(dialog, AlertDialog.PRIORITY_NORMAL, info);
            }
            else if (info.Type == AlertMessageType.MarryRefuse)
            {
                var dialog = AlertDialogUI.CreateFromXml("xml/marry/ui_marry_proposeagain.gui.xml");
                //dialog.SetContent(info.Content);
                GameAlertManager.Instance.AlertDialog.ShowAlertDialog(dialog, AlertDialog.PRIORITY_NORMAL, info);
            }
            else if (info.Type != AlertMessageType.TeamApply && info.Type != AlertMessageType.TeamInvite)
            {
                ShowSimpleAlert(info.Type);
            }

        }
    }

    public void AddSimulationMessage(AlertMessageType Type, string id)
    {
        MsgInfo info = new MsgInfo();
        info.Type = Type;
        info.WaitTime = 60;
        info.Id = id;

        AddMessage(info);
        RefreshIcon(NotiFyStatus.AddMsg);
    }

    public void AddSimulationMessage(AlertMessageType Type)
    {
        AddSimulationMessage(Type, "");
    }

    private void AddMessage(MsgInfo info)
    {
        if (!mMsgQueue.ContainsKey(info.Type))
        {
            mMsgQueue.Add(info.Type, new List<MsgInfo>());
        }
        List<MsgInfo> list = mMsgQueue[info.Type];
        list.Add(info);
    }

    /// <summary>
    /// 删除单条消息.
    /// </summary>
    /// <param name="info"></param>
    private void RemoveMessage(MsgInfo info)
    {
        RemoveMessage(info.Id, info.Type);
    }

    public string GetFirstMessageId(AlertMessageType type)
    {
        if (mMsgQueue.ContainsKey(type))
        {
            return mMsgQueue[type][0].Id;
        }
        return null;
    }


    /// <summary>
    /// 删除单条消息.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    public void RemoveMessage(string id, AlertMessageType type)
    {
        if (mMsgQueue.ContainsKey(type))
        {
            List<MsgInfo> list = mMsgQueue[type];
            for (int i = 0; i < list.Count; ++i)
            {
                MsgInfo tmp = list[i];
                if (tmp.Id == id)
                {
                    list.RemoveAt(i);
                    RefreshIcon(NotiFyStatus.RemoveMsg);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 删除一类消息.
    /// </summary>
    public void RemoveList(AlertMessageType type)
    {
        if (mMsgQueue.ContainsKey(type))
        {
            List<MsgInfo> list = mMsgQueue[type];
            if (list.Count > 0)
            {
                var request = new ClientHandleMessageRequest { c2s_data = new List<MessageHandleData>() };

                foreach (var info in list)
                {
                    request.c2s_data.Add(new MessageHandleData
                    {
                        id = info.Id,
                        agree = AlertHandlerType.Cancel
                    });
                }

                TLNetManage.Instance.Request<ClientHandleMessageResponse>(request, (err, rsp) => { });
                list.Clear();
            }
            RefreshIcon(NotiFyStatus.RemoveMsg);
        }
    }

    private void RefreshIcon(NotiFyStatus status)
    {
        Notify((int)status);
    }

    public int GetMessageCount(AlertMessageType type)
    {
        if (mMsgQueue.ContainsKey(type))
        {
            return mMsgQueue[type].Count;
        }
        return 0;
    }

    public void ShowSimpleAlert(AlertMessageType type)
    {
        if (mMsgQueue.ContainsKey(type))
        {
            List<MsgInfo> list = mMsgQueue[type];
            if (list.Count > 0)
            {
                MsgInfo info = list[0];
                string acceptStr = HZLanguageManager.Instance.GetString("common_accept");
                string refuseStr = HZLanguageManager.Instance.GetString("common_refuse");
                var content1 = HZLanguageManager.Instance.GetString(info.Content);
                var roleSnap = DataMgr.Instance.UserData.RoleSnapReader.GetCache(info.FromRoleID);
                var content = HZLanguageManager.Instance.GetFormatString(info.Content, roleSnap != null ? roleSnap.Name : "unknown");
                GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, content, acceptStr, refuseStr, info, OnMsgAccept, OnMsgRefuse);
                list.RemoveAt(0);
                RefreshIcon(NotiFyStatus.RemoveMsg);
            }
        }
    }


    private void OnMsgAccept(object param)
    {
        MsgInfo info = param as MsgInfo;
        if (info != null)
        {
            RequestMessageResult(info.Id, (int)MsgResult.Accept, (ClientHandleMessageResponse msg) =>
            {

            });
        }
    }

    private void OnMsgRefuse(object param)
    {
        MsgInfo info = param as MsgInfo;
        if (info != null)
        {
            RequestMessageResult(info.Id, (int)MsgResult.Refuse, null);
        }
    }


    /// <summary>
    /// 发送消息处理协议.
    /// </summary>
    /// <param name="id">消息id</param>
    /// <param name="result">操作结果</param>
    /// <param name="action">回调函数</param>
    public void RequestMessageResult(string id, int result, Action<ClientHandleMessageResponse> action)
    {
        ClientHandleMessageRequest request = new ClientHandleMessageRequest();
        MessageHandleData msg = new MessageHandleData();
        msg.id = id;
        msg.agree = result == 1 ? AlertHandlerType.Agree : AlertHandlerType.Cancel;
        List<MessageHandleData> list = new List<MessageHandleData>();
        list.Add(msg);
        request.c2s_data = list;
        TLNetManage.Instance.Request<ClientHandleMessageResponse>(request, (err, rsp) =>
        {
            if (action != null && rsp != null)
            {
                action(rsp);
                OnClientAlertMessageNotify(new ClientAlertMessageNotify { s2c_agree = msg.agree == AlertHandlerType.Agree, s2c_roleID = DataMgr.Instance.UserData.RoleID, s2c_id = msg.id });
            }
        });
    }

    [DoNotToLua]
    public void AttachObserver(IObserverExt<MsgData> ob)
    {
        mObservers.Add(ob);
    }

    [DoNotToLua]
    public void DetachObserver(IObserverExt<MsgData> ob)
    {
        mObservers.Remove(ob);
    }

    public void AttachLuaObserver(string key, LuaTable t)
    {
        mLuaObservers[key] = t;
    }

    public void DetachLuaObserver(string key)
    {
        mLuaObservers.Remove(key);
    }

    public void Notify(int status, object opt = null)
    {
        foreach (var ob in mObservers)
        {
            ob.Notify(status, this, opt);
        }

        foreach (var ob in mLuaObservers)
        {
            ob.Value.invoke("Notify", new object[] { (NotiFyStatus)status, this, ob.Value, opt });
        }
    }


    public void Update(float deltaTime)
    {
        bool queueChange = false;
        //刷新每个消息的剩余时间.
        foreach (KeyValuePair<AlertMessageType, List<MsgInfo>> q in mMsgQueue)
        {
            List<MsgInfo> list = q.Value;
            for (int i = 0; i < list.Count; ++i)
            {
                MsgInfo info = list[i];
                if (info.WaitTime != -1)
                {
                    info.WaitTime -= deltaTime;
                    if (info.WaitTime <= 0)
                    {
                        RemoveMessage(info);
                        queueChange = true;
                    }
                }
            }
        }
        //队列有变化，刷新图标.
        if (queueChange)
        {
            RefreshIcon(NotiFyStatus.RemoveMsg);
        }
    }

    public void RequestSendedMessage(AlertMessageType t, Action<List<MsgInfo>> cb)
    {
        TLNetManage.Instance.Request<ClientSendedAlertMessageResponse>(new ClientSendedAlertMessageRequest { c2s_type = t }, (ex, rp) =>
          {
              if (rp == null || rp.s2c_data == null || rp.s2c_data.Length == 0)
              {
                  cb(null);
              }
              else
              {
                  var list = rp.s2c_data.Select(m =>
                  {
                      MsgInfo info = new MsgInfo();
                      info.Id = m.id;
                      info.Type = m.type;
                      info.Content = m.strMsg;
                      info.WaitTime = m.lifeTimeMS / 1000f;
                      info.FromRoleID = m.fromRoleID;
                      info.TargetRoleID = m.targetRoleID;
                      info.SyncPlayers = m.syncPlayers;
                      return info;
                  }).ToList();
                  cb(list);
              }
          });
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        if (reLogin)
        {
            mMsgQueue.Clear();
            mObservers.Clear();
            mLuaObservers.Clear();
        }
    }

    public class MsgInfo
    {
        public string Id { get; set; }
        public AlertMessageType Type { get; set; }
        public string Content { get; set; }
        public double WaitTime { get; set; }
        public string FromRoleID { get; set; }
        public string TargetRoleID { get; set; }
        public MessageHandleData[] SyncPlayers { get; set; }
    }

}
