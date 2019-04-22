
using DeepCore.FuckPomelo;
using DeepCore.FuckPomeloClient;
using DeepCore.IO;
using DeepMMO.Client.Battle;
using DeepMMO.Data;
using DeepMMO.Protocol;
using System;
using System.Collections.Generic;
using DeepMMO.Protocol.Client;
using TLClient;
using TLClient.Battle;
using TLClient.Net;
using SLua;

public class TLNetManage
{

    public enum NetWorkState
    {
        CONNECTED,
        DISCONNECTED,
        ERROR
    }

    public const int WAIT_TIME = 15;

    private static TLNetManage _instance;

    public static TLNetManage Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TLNetManage();
            }
            return _instance;
        }
    }

    private HashSet<IZeusNetObserver> mObserversGate = new HashSet<IZeusNetObserver>();
    private HashSet<IZeusNetObserver> mObserversGame = new HashSet<IZeusNetObserver>();

    public delegate bool OnBeforeRequestEvent(string route, object option);
    public event OnBeforeRequestEvent OnBeforeRequest;
    public delegate void OnRequestStartEvent(string route, object option);
    public event OnRequestStartEvent OnRequestStart;
    public delegate void OnRequestEndEvent(string route, int code, string msg, PomeloException exp, object option);
    public event OnRequestEndEvent OnRequestEnd;
    private Action<RPGBattleClient> event_OnZoneChanged;
    public event Action<RPGBattleClient> OnZoneChanged { add { event_OnZoneChanged += value; } remove { event_OnZoneChanged -= value; } }
    private Action<RPGBattleClient> event_OnZoneLeaved;
    public event Action<RPGBattleClient> OnZoneLeaved { add { event_OnZoneLeaved += value; } remove { event_OnZoneLeaved -= value; } }


    public TLNetClient NetClient { get; private set; }
    public TLLoginHandler LoginHandler { get; private set; }
    public TLBattleClient BattleClient { get; private set; }

    private bool mGateServerConnected;
    private bool mGameServerConnected;
    private bool mGameEntered;

    //网络事件队列.
    private Queue<NetEventData> mNetStateQueue = new Queue<NetEventData>();

    public enum SocketType {
        GateSocket,
        GameSocket
    }

    public TLNetManage()
    {

    }

    [DoNotToLua]
    public void InitNetWork()
    {
        this.Clear();
        var clientInfo = new ClientInfo
        {
            userAgent = PlatformMgr.PluginGetUserAgent(),
            network = PlatformMgr.PluginGetNetworkStatus(),
            deviceId = PlatformMgr.PluginGetUUID(),
            deviceType = PublicConst.OSType.ToString(),
            deviceModel = PlatformMgr.PluginGetDeviceType(),
            region = PublicConst.ClientRegion,
            channel = OneGameSDK.Instance.Channel.ToString(),
            clientVersion = PublicConst.LogicVersion.ToString(),
            sdkVersion = OneGameSDK.Instance.GetPlatformData().GetData(SDKAttName.SDK_VERSION),
            sdkName = OneGameSDK.Instance.GetPlatformData().GetData(SDKAttName.SDK_NAME),
        };

        this.NetClient = new TLNetClient(clientInfo);
        this.LoginHandler = new TLLoginHandler(NetClient);
        LuaSystem.Instance.DoFunc("GlobalHooks.InitNetWork", true);

        NetClient.OnZoneChanged += new Action<RPGBattleClient>((RPGBattleClient client) => {
            BattleClient = client as TLBattleClient;
            if (event_OnZoneChanged != null)
                event_OnZoneChanged(BattleClient);
        });

        NetClient.OnZoneLeaved += new Action<RPGBattleClient>((RPGBattleClient client) =>
        {
            if (event_OnZoneLeaved != null)
                event_OnZoneLeaved(client);
        });

        NetClient.OnGameDisconnected += new Action<PomeloClient, CloseReason>((client, reason) =>
        {
            Debugger.Log("-----------------OnGameDisconnected " + reason);
        });

        NetClient.GateClient.OnConnected += new Action<SystemHandshakeAck, ISerializable>((msg, token) =>
        {
            mNetStateQueue.Enqueue(new NetEventData(SocketType.GateSocket, NetWorkState.CONNECTED));
            mGateServerConnected = true;
        });

        NetClient.GateClient.OnDisconnected += new Action<CloseReason, string>((reason, err) =>
        {
            mNetStateQueue.Enqueue(new NetEventData(SocketType.GateSocket, NetWorkState.DISCONNECTED, reason));
            mGateServerConnected = false;
        });

        NetClient.GameClient.OnConnected += new Action<SystemHandshakeAck, ISerializable>((msg, token) =>
        {
            mNetStateQueue.Enqueue(new NetEventData(SocketType.GameSocket, NetWorkState.CONNECTED));
            mGameServerConnected = true;
        });

        NetClient.GameClient.OnDisconnected += new Action<CloseReason, string>((reason, err) =>
        {
            var netEvtData = new NetEventData(SocketType.GameSocket, NetWorkState.DISCONNECTED, reason);
            netEvtData.errStr = err;
            mNetStateQueue.Enqueue(netEvtData);
            mGameServerConnected = false;
            mGameEntered = false;
            Debugger.Log("-----------------OnDisconnected " + reason);
        });

        NetClient.OnGameEntered += delegate (PomeloClient client, ClientEnterGameResponse response)
        {
            if (response.IsSuccess)
            {
                Debugger.Log("OnGameEntered " + response.s2c_role.name);
                mGameEntered = true;
                LuaSystem.Instance.DoFunc("GlobalHooks.InitNetWork", false);
            }
        };

        NetClient.GameClient.NetError += new Action<Exception>((err) =>
        {
            mNetStateQueue.Enqueue(new NetEventData(SocketType.GameSocket, NetWorkState.ERROR));
            Debugger.LogError(err.Message + err.StackTrace);
        });

        NetClient.GateClient.OnRequestStart += this.RequestStart;
        NetClient.GateClient.OnRequestEnd += this.RequestEnd;
        NetClient.GameClient.OnRequestStart += this.RequestStart;
        NetClient.GameClient.OnRequestEnd += this.RequestEnd;
        NetClient.GameClient.NetHandleNoListeningPush += HandleNoListeningPush;

        //this.NetClient.GameClient.NetHandleMessageImmediately += ((e) =>
        //{
        //    var codec = TLNetClient.TLNetCodec.GetCodec(e.MsgRoute);
        //    Debugger.LogError("666666666666666666666666666 " + codec.MessageType.Name);
        //    return false;
        //});
    }

    private void HandleNoListeningPush(IRecvMessage protocol)
    {
        Debugger.LogError("[HandleNoListeningPush] Msg ID: " + protocol.MsgRoute + " " + protocol.ReadBodyBinary().ToArray().ToString());
    }

    private void BeforeRequest(string route, ISerializable request, object option)
    {
        if (OnBeforeRequest != null)
            OnBeforeRequest(route, option);
    }

    private void RequestStart(string route, ISerializable request, object option)
    {
        if (OnRequestStart != null)
            OnRequestStart(route, option);
    }

    private string mDefaultError;
    private string GetDefaultError()
    {
        if (mDefaultError == null)
        {
            using(var table = (LuaTable) LuaSvr.mainState["MessageCodeAttribute"])
            using (var codeTable = (LuaTable) table["DeepMMO.Protocol.Response"])
            {
                var ret = codeTable[Response.CODE_ERROR];
                mDefaultError = ret.ToString();
            }
        }

        return mDefaultError;
    }

    
    private void RequestEnd(string route, PomeloException excep, ISerializable response, object option)
    {
        if (OnRequestEnd != null)
        {
            if (response != null)
            {
                Response rsp = response as Response;
                if (rsp.s2c_code != 200 && string.IsNullOrEmpty(rsp.s2c_msg))
                {
                    try
                    {
                        using(var table = (LuaTable) LuaSvr.mainState["MessageCodeAttribute"])
                        using (var codeTable = (LuaTable) table[rsp.GetType().FullName])
                        {
                            var ret = codeTable[rsp.s2c_code];
                            rsp.s2c_msg = ret != null ? ret.ToString() : GetDefaultError();
                        }
                    }
                    catch
                    {
                        rsp.s2c_msg = GetDefaultError();
                    }
                }
                OnRequestEnd(route, rsp.s2c_code, rsp.s2c_msg, excep, option);
            }
            else
            {
                OnRequestEnd(route, -1, "", excep, option);
            }
        }
    }

    public void Request<RSP>(ISerializable req, Action<PomeloException, RSP> cb, object option = null) where RSP : ISerializable
    {
        if (NetClient != null)
        {
            NetClient.GameClient.Request<RSP>(req, cb, option);
        }
        else
        {
            throw new Exception("NetClient == null");
        }
    }

    public void Listen<T>(Action<T> action) where T : ISerializable
    {
        if(NetClient != null)
        {
            var pushHandle = NetClient.GameClient.Listen<T>(action);
        }
        else
        {
            throw new Exception("NetClient == null");
        }
    }

    public void RequestBinary(BinaryMessage data, Action<PomeloException, BinaryMessage> action)
    {
        if (NetClient != null)
        {
            NetClient.GameClient.RequestBinary(data, action);
        }
        else
        {
            throw new Exception("NetClient == null");
        }
    }

    public void NotifyBinary(BinaryMessage data)
    {
        if (NetClient != null)
        {
            NetClient.GameClient.NotifyBinary(data);
        }
        else
        {
            throw new Exception("NetClient == null");
        }
    }

    public void ListenBinary(int route, Action<BinaryMessage> action)
    {
        if (NetClient != null)
        {
            var pushHandle = NetClient.GameClient.ListenBinary(route, (rp) =>{
                action.Invoke(rp);
            },false);
        }
        else
        {
            throw new Exception("NetClient == null");
        }
    }

    public void Notify(ISerializable req)
    {
        if (NetClient != null)
        {
            NetClient.GameClient.Notify(req);
        }
    }

    public void Disconnect()
    {
        if (NetClient != null)
        {
            if (mGateServerConnected)
            {
                NetClient.GateClient.Disconnect();
            }
            if (mGameServerConnected)
            {
                NetClient.GameClient.Disconnect();
            }
        }
    }

    /// <summary>
    /// 是否联网模式
    /// </summary>
    /// <returns></returns>
    public bool IsNet
    {
        get
        {
            return NetClient != null;
        }
    }

    public bool IsGateSocketConnected()
    {
        return mGateServerConnected;
    }

    public bool IsGameSocketConnected()
    {
        return mGameServerConnected;
    }

    public bool IsGameEntered()
    {
        return mGameEntered;
    }

    public void AttachObserverGate(IZeusNetObserver ob)
    {
        mObserversGate.Add(ob);
    }

    public void DetachObserverGate(IZeusNetObserver ob)
    {
        mObserversGate.Remove(ob);
    }

    public void AttachObserverGame(IZeusNetObserver ob)
    {
        mObserversGame.Add(ob);
    }

    public void DetachObserverGame(IZeusNetObserver ob)
    {
        mObserversGame.Remove(ob);
    }

    [DoNotToLua]
    public void Update(float deltaTime)
    {
        if (IsNet)
        {
            int ms = (int)(deltaTime * 1000);
            NetClient.Update(ms);
            //处理网络监听事件.
            while (mNetStateQueue.Count > 0)
            {
                NetEventData data = mNetStateQueue.Dequeue();
                HashSet<IZeusNetObserver> obs = new HashSet<IZeusNetObserver>(mObserversGame);
                foreach (IZeusNetObserver ob in obs)
                {
                    ob.OnNetStateChanged(data);
                }
            }
        }
    }

    [DoNotToLua]
    public void Clear()
    {
        if (NetClient != null)
        {
            NetClient.Dispose();
            NetClient = null;
        }
        mGateServerConnected = false;
        mGameServerConnected = false;
        mGameEntered = false;
        mObserversGate.Clear();
        mObserversGame.Clear();
        event_OnZoneChanged = null;
        event_OnZoneLeaved = null;
        OnBeforeRequest = null;
        OnRequestStart = null;
        OnRequestEnd = null;
    }

    public class NetEventData
    {
        public TLNetManage.SocketType Type { get; set; }
        public TLNetManage.NetWorkState State { get; set; }
        public CloseReason Reason { get; set; }
        public string errStr { get; set; }

        public NetEventData(TLNetManage.SocketType type, TLNetManage.NetWorkState state)
        {
            Type = type;
            State = state;
        }
        public NetEventData(TLNetManage.SocketType type, TLNetManage.NetWorkState state, CloseReason reason) : this(type, state)
        {
            Reason = reason;
        }
    }

    public class PackExtData
    {
        public PackExtData(bool waiting, bool showError)
        {
            IsWaiting = waiting;
            IsShowError = showError;
        }
        public PackExtData(bool waiting, bool showError, Action timeoutCb) : this(waiting, showError)
        {
            TimeOutCb = timeoutCb;
        }
        public bool IsWaiting { set; get; }
        public bool IsShowError { set; get; }
        public Action TimeOutCb { set; get; }
    }

}

public interface IZeusNetObserver
{
    void OnNetStateChanged(TLNetManage.NetEventData data);
}
