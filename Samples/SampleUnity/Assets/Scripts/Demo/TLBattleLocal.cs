#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && !UNITY_ANDROID && !UNITY_IOS

using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameData.ZoneClient;
using DeepCore.GameData.ZoneServer;
using DeepCore.GameHost;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor;
using DeepCore.GameSlave.Client;
using DeepCore.Log;
using DeepCore.Protocol;

using TLClient;
/// <summary>
/// 战斗编辑器用的纯客户端
/// </summary>
public class TLBattleLocal : AbstractBattle, InstanceZoneListener
{
    private InstanceUnit mSender;
    private EditorScene mZone;

    private int intervalMS;

    //public override int CurrentPing { get { return 0; } }
    public override long RecvPackages { get { return 0; } }
    public override long SendPackages { get { return 0; } }
    public override bool IsNet { get { return false; } }
    public override KickedByServerNotifyB2C KickMessage { get { return null; } }

    protected readonly Logger log;

    public EditorScene Zone
    {
        get { return mZone; }
    }
    public SceneData SceneData
    {
        get { return Zone.Data; }
    }

    internal TLBattleLocal(SceneData scene)
        : base(TLClientBattleManager.DataRoot)
    {
        this.log = LoggerFactory.GetLogger(GetType().Name);
        this.Layer.ActorSyncMode = SyncMode.MoveByClient_PreSkillByClient;
        this.mZone = InstanceZoneFactory.Factory.CreateEditorScene(DataRoot, this, scene);
        this.mZone.IsSyncZ = true;
        this.Layer.ProcessMessage(new ClientEnterScene(Zone.UUID,scene.ID, mZone.SpaceDivSize, DataRoot.Templates.ResourceVersion));
    }

    protected override void Disposing()
    {
        base.Disposing();
        this.mSender = null;
        this.mZone.Dispose();
        this.mZone = null;
    }

    public override void BeginUpdate(int intervalMS)
    {
        base.BeginUpdate(intervalMS);
        this.intervalMS = intervalMS;

    }
    public override void Update()
    {
        mZone.Update(this.intervalMS);
        base.Update();
    }

    public override void SendAction(Action action)
    {

        //log.Debug("SendAction:" + action.ToString());

        if (mSender == null && Layer.Actor != null)
        {
            mSender = Zone.getUnit(Layer.Actor.ObjectID);
        }
        action.sender = mSender;
        Zone.pushAction(action);
    }
    public void onEventHandler(Event e)
    {
        //log.Debug("onEventHandler:" + e.ToString());
        Layer.ProcessMessage(e as IMessage);
    }
    public void onResponseHandler(InstanceZoneObject obj, ActorRequest req, ActorResponse rsp)
    {
        //log.Debug("onResponseHandler:" + obj.ToString() + ";req:" + req.ToString() + ";rsp:" + rsp.ToString());
        Layer.ProcessMessage(rsp as IMessage);
    }

}
#endif