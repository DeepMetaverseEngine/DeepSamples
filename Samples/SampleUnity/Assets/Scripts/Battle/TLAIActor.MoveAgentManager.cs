using Assets.Scripts;
using DeepCore.GameData.Data;
using DeepCore.GameData.RTS;
using DeepCore.GameSlave;
using DeepCore.GameSlave.Agent;
using SLua;
using System;
using System.Collections.Generic;
using DeepCore;
using DeepCore.Formula;
using TLBattle.Message;
using TLBattle.Plugins;
using TLClient.Modules;
using TLProtocol.Protocol.Client;
using UnityEngine;
using UnityEngine.SceneManagement;
using DeepMMO.Protocol;

public partial class TLAIActor
{    
    public class TLActorMoveFuckWay : ActorMoveFuckWay
    {
        public MoveEndAction EndAction { get; set; }
        public bool SpeedUp { get; set; }

        public bool HasWay = false;
        public TLActorMoveFuckWay(MoveEndAction action, float targetX, float targetY, float endDistance = 0,
            Predicate<ZoneEditorPoint> select = null, UnitActionStatus st = UnitActionStatus.Move, bool autoAdjust = true, object ud = null)
            : base(targetX, targetY, endDistance, select, st, autoAdjust, ud)
        {
            EndAction = action;
            SpeedUp = false;
        }
    }

//    private class TransportRecord
//    {
//        public int mapid;
//        public int mapuuid;
//        public string flag;
//
//        public bool isSame(TransportRecord tr)
//        {
//            if (mapid == tr.mapid && mapuuid == tr.mapuuid && flag == tr.flag)
//            {
//                return true;
//            }
//
//            return false;
//        }
//    }
    
    public delegate void MoveEndDoAction(MoveEndAction action);
    private int speedupdistance;
    private int speeduplevellimit;
    private bool bCanSpeedUp = false;
    private bool bManualMove = false;
    private bool isTransfer = false;
    private bool isTransferGuild = false;
    //private TransportRecord LastTransprotRecord;
    public void InitMoveAgent()
    {
        speedupdistance = GameUtil.GetIntGameConfig("quest_speed_distance");
        speeduplevellimit = GameUtil.GetIntGameConfig("quest_speed_up");
        isTransfer = false;
        isTransferGuild = false;
        //LastTransprotRecord = null;
    }



    private void ManualMoveClick(EventManager.ResponseData res)
    {
        bManualMove = true;
    }

    public class MoveEndAction
    {
        public float AimX;
        public float AimY;
        public float AimDistance;
        public int MapId;
        public string RoadName;
        public string hints;
        public int QuestId;
        public int MoveType = (int)AutoMoveType.Normal;
        public bool IsShow = true;
        public string MapUUid = string.Empty;
        public float orgActorX;
        public float orgActorY;
        public MoveEndDoAction Action;
        public bool IsAutoRun = true;
        public int Radar = 0;
        public bool IsBreak = false;//是否寻路被打断

        public int EnterGuild = 0;//是否进自己公会
        public string QuickTransPortFlag;
        public int QuickTransPort = 0;//快速传送
       
        public float OrgAimDistance { set; get; }
        public int TargetMapId = 0;
        public virtual MoveEndAction CreateIntance()
        {
            return new MoveEndAction();
        }
        public bool DoEnd(MoveEndAction action)
        {

            if (TLBattleScene.Instance.Actor != null)
            {
                Vector2 playerpos = new Vector2(TLBattleScene.Instance.Actor.X, TLBattleScene.Instance.Actor.Y);
                Vector2 Aimpos = new Vector2(AimX, AimY);
                int distancefix = 1;
                //Debug.LogError("AimX="+ AimX+ " AimY="+ AimY + " AimDistance="+ AimDistance);
                if (action is MoveAndNpcTalk)
                {
                    var _action = action as MoveAndNpcTalk;
                    var unit = TLBattleScene.Instance.Actor.ZActor.Parent.GetUnitByTemplateID(_action.NpcTemplateId);
                    if (unit != null)
                    {
                        //Debug.Log("MoveAndNpcTalk Npc [" + action.RoadName + "]");
                        Aimpos.x = unit.X;
                        Aimpos.y = unit.Y;
                    }
                    distancefix = 3;
                }
                //Debug.LogError("AimDistance=" + Vector2.Distance(playerpos, Aimpos));
                if (Vector2.Distance(playerpos, Aimpos) <= AimDistance + distancefix
                    && DataMgr.Instance.UserData.MapTemplateId == MapId)
                {
                    if (Action != null)
                    {
                        Action.Invoke(this);
                    }
                    //Debug.Log("DoActionName=" + this.GetType().FullName);
                    DoAction();
                    return true;
                }
                


            }
            return false;
        }
        public virtual void OnUpdate()
        {

        }
        public virtual void DoAction()
        {
            //if(!(this is MoveAndBattle))
            //    TLBattleScene.Instance.Actor.ChangeManualState();
        }

        public bool IsSame(MoveEndAction action)
        {
            //todo
            if (action.GetType() == this.GetType())
            {
                if (action is MoveAndNpcTalk)
                {
                    var _action = action as MoveAndNpcTalk;
                    var _orgaction = this as MoveAndNpcTalk;
                    if (_action.MapId == DataMgr.Instance.UserData.MapTemplateId
                        && _action.NpcTemplateId == _orgaction.NpcTemplateId)
                    {
                        return true;
                    }
                }
                if (action.MapId == MapId
                    && action.AimX == AimX
                    && action.AimY == AimY
                    && action.RoadName == RoadName )
                {
                    return true;
                }

            }
            
            return false;
        }

        public virtual MoveEndAction Clone()
        {
            MoveEndAction moveaction = CreateIntance();
            moveaction.AimX = this.AimX;
            moveaction.AimY = this.AimY;
            moveaction.AimDistance = this.AimDistance;
            moveaction.OrgAimDistance = this.OrgAimDistance;
            moveaction.MapId = this.MapId;
            moveaction.RoadName = this.RoadName;
            moveaction.QuestId = this.QuestId;
            moveaction.MoveType = this.MoveType;
            moveaction.IsShow = this.IsShow;
            moveaction.MapUUid = this.MapUUid;
            moveaction.orgActorX = this.orgActorX;
            moveaction.orgActorY = this.orgActorY;
            moveaction.IsAutoRun = this.IsAutoRun;
            moveaction.TargetMapId = this.TargetMapId;
            return moveaction;
        }
    }
    public class TransPortMoveAction : MoveEndAction
    {
        
        public TransPortMoveAction(int mapid, string mapuuid = ""):base()
        {
            this.MapId = mapid;
            this.MapUUid = mapuuid;
            this.MoveType = (int)AutoMoveType.TransPort;
        }
        public override MoveEndAction CreateIntance()
        {
            return new TransPortMoveAction(MapId,MapUUid);
        }
    }
    public class MoveAndBattle : MoveEndAction
    {
        public string monsterIds;

        public MoveAndBattle(string monsterIds = null)
        {
            this.monsterIds = monsterIds;
        }
        public override MoveEndAction CreateIntance()
        {
            return new MoveAndBattle();
        }
        public override void DoAction()
        {
            //Debug.Log("MoveAndBattle= "+ monsterIds);
            
            if (TLBattleScene.Instance.Actor != null && (TargetMapId != 0 && TargetMapId == DataMgr.Instance.UserData.MapTemplateId)
                ||(TargetMapId == 0 && MapId == DataMgr.Instance.UserData.MapTemplateId))
                TLBattleScene.Instance.Actor.ChangeAutoAttackState(monsterIds);
        }

        public override MoveEndAction Clone()
        {
            MoveAndBattle moveaction = (MoveAndBattle)base.Clone();
            moveaction.monsterIds = this.monsterIds;
            return moveaction;
        }
    }

    //进入工会找npc
    public class EnterGuildAndNpcTalk : MoveAndNpcTalk
    {

        public EnterGuildAndNpcTalk(int NpcTemplateId) : base(NpcTemplateId)
        {
            this.MapId = GameUtil.GetIntGameConfig("guild_guildmap");
            this.MoveType = (int)AutoMoveType.EnterGuild;

        }
        public override MoveEndAction CreateIntance()
        {
            return new EnterGuildAndNpcTalk(NpcTemplateId);
        }

    }


    //进入工会找npc
    public class EnterGuildAction : MoveEndAction
    {
        public bool isSelf = true;
        public EnterGuildAction(bool _isSelf)
        {
            isSelf = _isSelf;
            this.MapId = GameUtil.GetIntGameConfig("guild_guildmap");
            if (isSelf)
            {
                this.MoveType = (int)AutoMoveType.EnterGuild;
            }
            else
            {
                this.MoveType = (int)AutoMoveType.EnterOtherGuild;
            }

        }
        public override MoveEndAction CreateIntance()
        {
            return new EnterGuildAction(isSelf);
        }

        public override MoveEndAction Clone()
        {
            EnterGuildAction moveaction = (EnterGuildAction)base.Clone();
            moveaction.isSelf = this.isSelf;
            return moveaction;
        }

    }

    public class MoveAndNpcTalk : MoveEndAction
    {
        public int NpcTemplateId;
        public int WaitState = 0;
        public bool bPlayAnimation = false;
        public bool OpenFunction { get; set; }
        public MoveAndNpcTalk(int NpcTemplateId)
        {
            this.NpcTemplateId = NpcTemplateId;
            this.AimDistance = 2;
            //Debugger.LogWarning("MoveAndNpcTalk"+ NpcTemplateId);
        }

        public override MoveEndAction CreateIntance()
        {
            return new MoveAndNpcTalk(NpcTemplateId);
        }
        public override void DoAction()
        {
            //DataMgr.Instance.QuestMangerData.AutoDoneWithNpcTemplateId(NpcTemplateId);
            if (TLBattleScene.Instance == null)
            {
                return;
            }
            WaitState = 1;
            CheckNpc();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            CheckNpc();
        }

        private void CheckNpc()
        {
            if (WaitState == 2) return;
            var unit = TLBattleScene.Instance.GetUnitByTemplateId(NpcTemplateId);
            if (unit != null && unit is TLAINPC && this.MapId == DataMgr.Instance.UserData.MapTemplateId)
            {

                Vector2 playerpos = new Vector2(TLBattleScene.Instance.Actor.X, TLBattleScene.Instance.Actor.Y);
                Vector2 Aimpos = new Vector2(unit.X, unit.Y);

                if (!bPlayAnimation && Vector2.Distance(playerpos, Aimpos) <= 2.5)
                {
                    if (unit.CurrentState == DeepCore.GameData.Data.UnitActionStatus.Idle)
                    {
                        unit.PlayIdleSpeicalAnimation("n_talk");
                    }
                    bPlayAnimation = true;
                }

                if (WaitState == 1)
                {
                    //Debug.LogError("CheckNpcAimX=" + AimX+ " AimY="+ AimY + " AimDistance="+ AimDistance);
                    if (Vector2.Distance(playerpos, Aimpos) <= AimDistance + 3)
                    {
                        //
                        //TLBattleScene.Instance.Actor.ClearAutoRun();
                        //if (!(TLBattleScene.Instance.Actor.CurGState is PickSelfState))
                        //{
                        //    TLBattleScene.Instance.Actor.TryChangeManualOperateState();
                        //}

                        DataMgr.Instance.QuestMangerData.TalkWithNpcByTemplateId(NpcTemplateId, QuestId, OpenFunction);
                        WaitState = 2;
                    }

                }
            }
        }
        public override MoveEndAction Clone()
        {
            MoveAndNpcTalk moveaction = (MoveAndNpcTalk)base.Clone();
            moveaction.NpcTemplateId = this.NpcTemplateId;
            moveaction.OpenFunction = this.OpenFunction;
            moveaction.WaitState = this.WaitState;
            return moveaction;
        }
    }


    public class FindTargetAndMove : MoveEndAction
    {
        public TLAIUnit TargetUnit = null;
        public uint TargetObjID = 0;
        public int TemplateId = 0;
        public FindTargetAndMove()
        {
            this.MoveType = (int)AutoMoveType.FollowNpc;
        }
        public override void DoAction()
        {
            if (Action != null)
                Action.Invoke(this);
        }
    }
    public class MapTouchMoveAction : MoveEndAction
    {
        public float direction;
        public MapTouchMoveAction()
        {
            this.MoveType = (int)AutoMoveType.MapTouch;
            TLBattleScene.Instance.Actor.DoStopForceQuest();
            TLBattleScene.Instance.Actor.ClearAutoRun();

        }
    }

    private float AimX;
    private float AimY;
    private float AimDistance;
    //private MoveEndAction mMoveAction;
    private MoveAndNpcTalk mFindAction;
    private List<ZoneFlag> mBigDickRoadList = null;
    private bool mPlayCG;
    private GState LastState;
    private Coroutine mQuickTransferCorout;
    private Coroutine mEnterGuildCorout;
    private HashMap<int, List<TLSceneNextLink>> cacheLinkMap = new HashMap<int, List<TLSceneNextLink>>();
    public enum AutoMoveType
    {
        Normal,
        MapTouch,
        FollowNpc,
        PickItem,
        EnterGuild,
        EnterOtherGuild,
        SmallMapTouch,
        TransPort
    }

    [DoNotToLua]
    public void InitAutoMove()
    {
        mPlayCG = false;
        LastState = null;

        EventManager.Subscribe("AutoMoveByTarget", AutoMoveByTarget);
        EventManager.Subscribe("Event.Scene.ChangeFinish", ChangeScene);
        //EventManager.Subscribe("EVENT_PLAYCG_START", EventPlayCG);
        EventManager.Subscribe("Event.Quest.ManualMove", ManualMoveClick);
        EventManager.Subscribe("Event.Npc.NpcTalk", TalkStopQuestAutoRun);
        EventManager.Subscribe("Event.Npc.DialogueTalk", TalkStopQuestAutoRun);
        initBigDickRoad();

    }

    private void TalkStopQuestAutoRun(EventManager.ResponseData res)
    {
        Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
        object value;
        if (data.TryGetValue("isTalk", out value))
        {
            if ((bool)value)
            {
                ClearAutoRun();
            }
        }
    }


    [DoNotToLua]
    public void ClearAutoMoveListen()
    {
        EventManager.Unsubscribe("AutoMoveByTarget", AutoMoveByTarget);
        EventManager.Unsubscribe("Event.Scene.ChangeFinish", ChangeScene);
        //EventManager.Unsubscribe("EVENT_PLAYCG_START", EventPlayCG);
        EventManager.Unsubscribe("Event.Quest.ManualMove", ManualMoveClick);
        EventManager.Unsubscribe("Event.Npc.NpcTalk", TalkStopQuestAutoRun);
        EventManager.Unsubscribe("Event.Npc.DialogueTalk", TalkStopQuestAutoRun);
        DoAutoRun(false, 0);
        if (mQuickTransferCorout != null)
        {
            GameGlobal.Instance.StopCoroutine(mQuickTransferCorout);
            mQuickTransferCorout = null;
        }
        if (mEnterGuildCorout != null)
        {
            GameGlobal.Instance.StopCoroutine(mEnterGuildCorout);
            mEnterGuildCorout = null;
        }

        if (cacheLinkMap != null)
        {
            cacheLinkMap.Clear();
        }
        
       
    }
    private void initBigDickRoad()
    {
        if (mBigDickRoadList != null)
        {
            mBigDickRoadList.Clear();
            mBigDickRoadList = null;
        }

        foreach (var road in this.ZActor.Parent.Flags)
        {
            if (road.Name.IndexOf("bdr") != -1)
            {
                if (mBigDickRoadList == null)
                {
                    mBigDickRoadList = new List<ZoneFlag>();
                }
                mBigDickRoadList.Add(road);
            }
        }
    }

    private void ChangeScene(EventManager.ResponseData res)
    {
        if (!(this.CurGState is AutoRunState)
            && !(this.CurGState is AutoRunByFollowTarget)
            && !(this.CurGState is AutoAttackState))
        {
            CheckMoveFinish(true);
        }
        else
        {
            //切地图就开启自动战斗的清除寻路
            if (this.CurGState is AutoAttackState)
            {
                DataMgr.Instance.UserData.LastMoveEndAction = null;
            }
        }
    }
    private bool CanMove(MoveEndAction action, bool isShowTips)
    {
        if (action != null && action.MoveType != (int)AutoMoveType.MapTouch
           && action.MoveType != (int)AutoMoveType.SmallMapTouch
           && ((!string.IsNullOrEmpty(action.RoadName) && action.RoadName.Equals("0"))))
        {
            DoAutoRun(false, 0);
            DoStopForceQuest();
            if (isShowTips && !action.IsAutoRun)
            {
                
                GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString(!string.IsNullOrEmpty(action.hints)?action.hints:"notsupportautomove"));
                
            }
            
                
            //Debugger.LogError(action.GetType().FullName+" haven't coordinates @ small stone with mapid = "+ action.MapId + " questid ="+action.QuestId);
            return false;
        }
        return true;
    }
    private void CheckMoveFinish(bool ischangeScene = false)
    {
        if (mPlayCG || TLBattleScene.Instance == null || TLBattleScene.Instance.Actor == null)
        {
            return;
        }
        if (LastState != null)
        {
            this.ChangeActorState(LastState);
            LastState = null;
        }
        else
        if (DataMgr.Instance.UserData.LastMoveEndAction != null)
        {
            if (isCurrentMap(DataMgr.Instance.UserData.LastMoveEndAction, ischangeScene))
            {
                if (DataMgr.Instance.UserData.LastMoveEndAction is FindTargetAndMove)
                {
                    this.ChangeActorState(new AutoRunByFollowTarget(this, DataMgr.Instance.UserData.LastMoveEndAction as FindTargetAndMove));
                }
                else
                {
                    if (RoadNameToAim(DataMgr.Instance.UserData.LastMoveEndAction))
                    {
                        this.ChangeActorState(new AutoRunState(this, DataMgr.Instance.UserData.LastMoveEndAction));
                    }

                }

                var action = DataMgr.Instance.UserData.LastMoveEndAction;
                if (action != null)
                {
                    Dictionary<string, object> args = new Dictionary<string, object>();
                    args.Add("id", action.QuestId);
                    EventManager.Fire("Event.Quest.ChangeScene", args);
                    DataMgr.Instance.UserData.LastMoveEndAction = null;
                }
            }
            //点击地图移动 遇到传送门出地图 特殊处理
            else if (DataMgr.Instance.UserData.LastMoveEndAction.MoveType == (int)AutoMoveType.SmallMapTouch)
            {
                TLBattleScene.Instance.Actor.ChangeManualState();
            }
        }
        else if (DataMgr.Instance.UserData.LastActorMoveAI != null)
        {

            this.AutoRunByAgent(DataMgr.Instance.UserData.LastActorMoveAI);

        }
    }

    private void ObjectEvent_FindTargetEvent(FindTargetUnitResponse objEvent)
    {
        //Debug.LogError("ObjectEvent_FindTargetEvent" + mMoveAction.GetType().FullName);
        if (mFindAction != null)
        {
            var moveaction = mFindAction.Clone();
            moveaction.AimX = objEvent.TargetX;
            moveaction.AimY = objEvent.TargetY;
            var _State = new AutoRunState(this, moveaction);
            if (mPlayCG)
            {
                LastState = _State;
            }
            else
                this.ChangeActorState(_State);
            mFindAction = null;
        }
        else
        {
            var moveaction = new FindTargetAndMove();
            moveaction.AimDistance = TLEditorConfig.Instance.PLAYER_FOLLOWNPC_DISTANCE_MIN;
            moveaction.MapId = this.BattleScene.SceneID;
            moveaction.AimX = objEvent.TargetX;
            moveaction.AimY = objEvent.TargetY;
            moveaction.TargetObjID = objEvent.TargetObjectID;
            moveaction.TemplateId = objEvent.TemplateID;
            //Debug.Log("FindTargetEvent objEvent.Targetx=" + objEvent.TargetX + " objEvent.Targety=" + objEvent.TargetY + "  moveaction.TargetObjID" + moveaction.TargetObjID);
            var _State = new AutoRunByFollowTarget(this, moveaction);
            if (mPlayCG)
            {
                LastState = _State;
            }
            else
            {
                this.ChangeActorState(_State);
            }

        }


    }

    //跟随目标
    [DoNotToLua]
    public TLAIUnit GetUnitByTemplateId(int templateId)
    {
        foreach (var unit in this.BattleScene.BattleObjects.Values)
        {
            if (unit is TLAIUnit)
            {
                TLAIUnit _unit = (unit as TLAIUnit);
                if (_unit.TemplateID == templateId)
                {
                    return _unit;
                }
            }
        }
        return null;
    }

    private void AutoMoveByTarget(EventManager.ResponseData res)
    {
        Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
        object _TemplateID;
        object _SceneID;

        if (data.TryGetValue("TemplateID", out _TemplateID))
        {
            int TargetTemplateID = int.Parse(_TemplateID.ToString());
            if (data.TryGetValue("SceneID", out _SceneID))
            {
                int SceneID = int.Parse(_SceneID.ToString());

                var moveaction = new FindTargetAndMove();
                moveaction.AimDistance = TLEditorConfig.Instance.PLAYER_FOLLOWNPC_DISTANCE_MIN;
                moveaction.MapId = SceneID;
                moveaction.TargetUnit = GetUnitByTemplateId(TargetTemplateID); ;
                moveaction.TemplateId = TargetTemplateID;
                if (isCurrentMap(moveaction))
                {
                    this.ChangeActorState(new AutoRunByFollowTarget(this, moveaction));
                }
            }
        }
    }
    [DoNotToLua]
    public void FindTargetByBattleServer(int TargetTemplateID,bool ignoreaoi = true)
    {
        ///Debugger.Log("FindTargetByBattleServer TargetTemplateID =" + TargetTemplateID);
        FindTargetUnitRequest ftu = new FindTargetUnitRequest(this.ObjectID, TargetTemplateID);
        ftu.IgnoreAoi = ignoreaoi;
        this.ZActor.SendAction(ftu);
    }

    private void StartCrossSceneMove(MoveEndAction action)
    {
        if (DataMgr.Instance.UserData.LastSceneNextlink != null && DataMgr.Instance.UserData.LastSceneNextlink.Count > 0)
        {
            MoveEndAction _action = action.Clone();
            string rdnName = DataMgr.Instance.UserData.LastSceneNextlink[0].from_flag_name;
            _action.RoadName = rdnName;
            _action.OrgAimDistance = _action.AimDistance;
            _action.AimDistance = 0.1f;
            if (_action.TargetMapId == 0)
            {
                _action.TargetMapId = _action.MapId;
            }
            _action.MapId = DataMgr.Instance.UserData.MapTemplateId;//TLBattleScene.Instance.SceneID;
            _action.AimX = 0;
            _action.AimY = 0;
            DataMgr.Instance.UserData.LastSceneNextlink.RemoveAt(0);
            AutoRunByAction(_action);
        }
    }
    private void QuickTransfer(int mapid, string mapuuid, string flag)
    {//采集+传送会导致一定概率传送失败 服务器有个idle 待机打断了连续采集的状态 所以这里等0.5f
        
        if (!isTransfer)
        {
            isTransfer = true;
            BreakAutoRunAgent();
            if (CurBattleStatus == CombatStateChangeEventB2C.BattleStatus.PVP)
            {
                isTransfer = false;
                GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("pvp_ban_transfer"));
                return;
            }
            
            MenuMgr.Instance.CloseAllMenu();
            mQuickTransferCorout = GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(0.5f, () =>
            {
                mQuickTransferCorout = null;
                isTransfer = false;
                //if (CurrentState != UnitActionStatus.Idle)
                //{
                //    return;
                //}
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("mapId", mapid);
                dic.Add("mapUUid", mapuuid);
                dic.Add("flag", flag);
                EventManager.Fire("Event.Map.ChangeScene", dic);
                
            }));
           
        }

    }

    private void QuickTransferGuild(MoveEndAction action, bool isSelf)
    {//采集+传送会导致一定概率传送失败 服务器有个idle 待机打断了连续采集的状态 所以这里等0.5f
     //BreakAutoRunAgent();
        
        if (!isTransferGuild)
        {
            isTransferGuild = true;
            ChangeManualState();
            if (string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildId))
            {
                isTransferGuild = false;
                GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("guild_noguild"));
                return;
            }
            if (CurBattleStatus == CombatStateChangeEventB2C.BattleStatus.PVP)
            {
                isTransferGuild = false;
                GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("pvp_ban_transfer"));
                return;
            }
            DataMgr.Instance.UserData.LastMoveEndAction = action;
            MenuMgr.Instance.CloseAllMenu();
            mEnterGuildCorout = GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(0.5f, () =>
            {
                mEnterGuildCorout = null;
                isTransferGuild = false;
                //if (CurrentState != UnitActionStatus.Idle)
                //{
                //    return;
                //}
                Dictionary<string, object> args = new Dictionary<string, object>();
                args.Add("isSelf", isSelf);
                EventManager.Fire("Event.Guild.EnterGuild", args);
            }));
        }
    }

    private List<TLSceneNextLink> CloneSceneLink(List<TLSceneNextLink> sceneNextLinks)
    {
        var link = new List<TLSceneNextLink>();
        foreach (var linkdata in sceneNextLinks)
        {
            var data = new TLSceneNextLink()
            {
                from_flag_name = linkdata.from_flag_name,
                to_flag_name = linkdata.to_flag_name,
                to_map_id = linkdata.to_map_id
            };
            link.Add(data);
        }

        return link;
    }
    private bool isCurrentMap(MoveEndAction action, bool ischangeScene = false)
    {
        if (!GameGlobal.Instance.netMode)
        {
            return true;
        }
        if (action.MoveType == (int)AutoMoveType.TransPort)
        {
            QuickTransfer(action.MapId, action.MapUUid, action.QuickTransPortFlag);
            return false;
        }
        if (action.MapId == DataMgr.Instance.UserData.MapTemplateId && ischangeScene)
        {
            if (DataMgr.Instance.UserData.LastSceneNextlink != null)
            {
                DataMgr.Instance.UserData.LastSceneNextlink.Clear();
                DataMgr.Instance.UserData.LastSceneNextlink = null;
            }

        }
        //仙盟破坏后返回自己仙盟
        if (DataMgr.Instance.UserData.MapTemplateId == GameUtil.GetIntGameConfig("guild_guildmap")
            && action.MapId == DataMgr.Instance.UserData.MapTemplateId)
        {
            
            bool isInSelfGuild = DataMgr.Instance.UserData.GuildId.Equals(DataMgr.Instance.UserData.ZoneGuildId);
            if (isInSelfGuild && action.MoveType == (int)AutoMoveType.EnterOtherGuild)
            {
                QuickTransferGuild(action, false);
                return false;
            }
            else if (!isInSelfGuild && action.MoveType == (int)AutoMoveType.EnterGuild)
            {
                QuickTransferGuild(action, true);
                return false;
            }
        }
        
        if (action.MapId != DataMgr.Instance.UserData.MapTemplateId)
        {//跨地图寻路           


            if (DataMgr.Instance.UserData.LastSceneNextlink != null
                && DataMgr.Instance.UserData.LastMoveEndAction != null && DataMgr.Instance.UserData.LastMoveEndAction.QuestId != 0
                && DataMgr.Instance.UserData.LastMoveEndAction.QuestId == action.QuestId && ischangeScene)
            {
                StartCrossSceneMove(DataMgr.Instance.UserData.LastMoveEndAction);
                return false;
            }

//            if (DataMgr.Instance.UserData.MapTemplateId == GameUtil.GetIntGameConfig("guild_guildmap"))
//            {
//                DataMgr.Instance.UserData.LastMoveEndAction = action;
//                QuickTransfer(action.MapId, action.MapUUid, action.QuickTransPortFlag);
//                return false;
//            }
            
            if(action.MoveType == (int)AutoMoveType.EnterGuild || action.MoveType == (int)AutoMoveType.EnterOtherGuild)
            {
                DataMgr.Instance.UserData.LastMoveEndAction = action;
                QuickTransferGuild(action, action.MoveType == (int)AutoMoveType.EnterGuild);
                return false;
            }

            List<TLSceneNextLink> result;
            if (cacheLinkMap.TryGetValue(action.MapId, out result))
            {
                DataMgr.Instance.UserData.LastMoveEndAction = action.Clone();
                DataMgr.Instance.UserData.LastSceneNextlink = CloneSceneLink(result);
                StartCrossSceneMove(DataMgr.Instance.UserData.LastMoveEndAction);
                return false;
            }
            
            //Debugger.LogError("action.MapId=="+action.MapId);
            TLClientQueryLoadWayRequest request = new TLClientQueryLoadWayRequest();
            request.c2s_toAreaId = action.MapId;
            TLNetManage.Instance.Request<TLClientQueryLoadWayResponse>(request, (err, rsp) =>
            {
                if (Response.CheckSuccess(rsp))
                {
                    if (TLBattleScene.Instance.IsRunning)
                    {
                        DataMgr.Instance.UserData.LastMoveEndAction = action.Clone();
                        if (DataMgr.Instance.UserData.LastSceneNextlink != null)
                        {
                            DataMgr.Instance.UserData.LastSceneNextlink.Clear();
                        }

                        var sceneNextlink = rsp.s2c_wayList;

                        if (action.QuickTransPort == 1 || sceneNextlink == null || sceneNextlink.Count >= 2 || sceneNextlink.Count == 0)
                        {

                           QuickTransfer(action.MapId, action.MapUUid, action.QuickTransPortFlag);
                           return;
                        }
                        

                        var link = CloneSceneLink(sceneNextlink);
                        cacheLinkMap.TryAdd(action.MapId, link);
                        DataMgr.Instance.UserData.LastSceneNextlink = sceneNextlink;
                        StartCrossSceneMove(DataMgr.Instance.UserData.LastMoveEndAction);
                    }
                    //this.ChangeActorState(new AutoRunState(this, _action));
                }
                else
                {
                    if (err != null)
                    {
                        Debugger.LogError("ClientQueryLoadWayResponse =" + err.Message.ToString());
                    }

                }
                //do logic
            });
            return false;
        }
        else
        {
            if (action.OrgAimDistance != 0)
            {
                action.AimDistance = action.OrgAimDistance;
                action.OrgAimDistance = 0;
            }

            if (action.Radar == 1)// 同场景启动雷达
            {
                Dictionary<object, object> dic = new Dictionary<object, object>();
                dic.Add("isShow", true);
                dic.Add("mapid", action.MapId);
                dic.Add("flag", action.RoadName);
                dic.Add("x", action.AimX);
                dic.Add("y", action.AimY);
                EventManager.Fire("EVENT_UI_FindTreasure", dic);
            }

        }
        return true;
    }

    //private void EventPlayCG(EventManager.ResponseData res)
    //{
    //    Dictionary<object, object> dic = (Dictionary<object, object>)res.data[1];
    //    object value;
    //    if (dic.TryGetValue("PlayCG", out value))
    //    {
    //        mPlayCG = (bool)value;
    //        if (mPlayCG)
    //        {
    //            StopAutoRun(res);
    //        }
    //    }
    //}
    //[DoNotToLua]
    //private void StopAutoRun(EventManager.ResponseData res)
    //{
    //    if (DataMgr.Instance.UserData.LastActorMoveAI != null)
    //    {
    //        this.ZActor.RemoveAgent(DataMgr.Instance.UserData.LastActorMoveAI);
    //    }
    //}
    [DoNotToLua]
    public void ClearAutoRun(bool needclean = false)
    {
        //Debugger.Log("ClearAutoRun---------------------------");
        BreakAutoRunAgent();
        if (DataMgr.Instance.UserData.LastMapTouchMoveAI != null)
        {
            //Debugger.Log("ClearAutoRun---------------------------");
            this.ZActor.RemoveAgent(DataMgr.Instance.UserData.LastMapTouchMoveAI);
            DataMgr.Instance.UserData.LastMapTouchMoveAI = null;
        }
        //LastRailWayMoveAI = null;

        //mMoveAction = null;
        //Debugger.LogWarning("ClearAutoRun");

        if (needclean)
        {
            DataMgr.Instance.UserData.LastMoveEndAction = null;
            DataMgr.Instance.UserData.LastSceneNextlink = null;
            LastState = null;
            this.ChangeActorState(new IdleState(this));
        }



    }

    public void AutoRunByAction(MoveEndAction action)
    {
        //Debugger.LogWarning("AutoRunByActionDistance="+action.GetType().FullName);
        if (bManualMove)
        {
            action.IsAutoRun = false;
            bManualMove = false;
        }
        if (action.QuestId != 0)
        {
            Quest quest = DataMgr.Instance.QuestData.GetQuest(action.QuestId);
            if (quest != null)
            {
                
                action.QuickTransPort = quest.GetStatic().GetInt("quicktranport");
                action.EnterGuild = quest.GetStatic().GetInt("enterguild");
                if(quest.subType == 2000)
                {
                    if (quest.state != QuestState.CompletedNotSubmited)
                    {
                        action.MoveType = (int)AutoMoveType.EnterOtherGuild;
                    }
                    else
                    {//和说好的不一样了  判断下交接任务的地图id不是公会就随便了
                        if(action.MapId == GameUtil.GetIntGameConfig("guild_guildmap"))
                        action.MoveType = (int)AutoMoveType.EnterGuild;
                    }
                }
                else
                {
                    if (action.EnterGuild == 1 && action.MapId == GameUtil.GetIntGameConfig("guild_guildmap"))
                    {
                        action.MoveType = (int)AutoMoveType.EnterGuild;
                    }
                }
                
                if (quest.state == QuestState.NotAccept)
                {
                    action.QuickTransPortFlag = quest.GetStatic().GetString("push_sceneflag");
                }
                if (quest.state == QuestState.CompletedNotSubmited)
                {
                    action.QuickTransPortFlag = quest.GetStatic().GetString("submit_sceneflag");
                }
            }
            
        }
        if (isCurrentMap(action))
        {
            //Debugger.LogWarning("AutoRunByActionDistance1=" + action.GetType().FullName);
            if (RoadNameToAim(action, true))
            {
                //Debugger.LogWarning("AutoRunByActionDistance2=" + action.GetType().FullName);
                var state = new AutoRunState(this, action);
                if (mPlayCG)
                {
                    LastState = state;
                }
                else
                {
                    //Debugger.LogWarning("AutoRunByActionDistance3=" + action.GetType().FullName);
                    ChangeActorState(state);
                }
            }
            else
            {
                IsAutoRun = false;
            }

        }

    }
    //路点转坐标
    private bool RoadNameToAim(MoveEndAction action, bool isShowtips = false)
    {
        //Debugger.LogError("RoadNameToAimAction"+action.GetType().Name);
        
        if (!CanMove(action, isShowtips))
        {
            return false;
        }
        if (!string.IsNullOrEmpty(action.RoadName) && !action.RoadName.Equals("0") && action.AimX == 0 && action.AimY == 0)
        {
            var flag = this.ZActor.Parent.GetFlag(action.RoadName);
            if (flag != null)
            {
                //Debug.Log("start auto RoadName [" + action.RoadName + "]");
                action.AimX = flag.X;
                action.AimY = flag.Y;
                //Debugger.LogError("action.AimY ["+ action.AimX +" aimy" + action.AimY + "]");
            }
            else
            {
                Debugger.LogError("Not found RoadPath with actionMap =[" + action.MapId + "] actionNpcTemplateId =[" + action.RoadName + "]");
            }
        }
        else if (string.IsNullOrEmpty(action.RoadName))
        {
            if (action is MoveAndNpcTalk)
            {
                MoveAndNpcTalk _action = action as MoveAndNpcTalk;
                if (_action.NpcTemplateId != 0)
                {
                    if (action.AimX == 0 && action.AimY == 0)
                    {

                        var unit = this.ZActor.Parent.GetUnitByTemplateID(_action.NpcTemplateId);
                        if (unit != null)
                        {
                            //Debug.Log("MoveAndNpcTalk Npc [" + action.RoadName + "]");
                            action.AimX = unit.X;
                            action.AimY = unit.Y;
                        }
                        else
                        {
                            
                            if (mFindAction == null || (mFindAction != null && !mFindAction.IsSame(_action)))
                            {
                                mFindAction = _action;
                                FindTargetByBattleServer(_action.NpcTemplateId);
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        
      
        return true;
    }

    private void StartAutoRunByAction(MoveEndAction action, bool isRailWay = true)
    {
        AutoRun(action, isRailWay);
    }
    private void AutoRun(MoveEndAction action, bool isRailWay = true)
    {
        //Debugger.LogWarning("AutoRun"+action.QuestId);
        AimX = action.AimX;
        AimY = action.AimY;
        AimDistance = action.AimDistance;
        action.IsBreak = false;
        TLActorMoveFuckWay moveai = null;

        Vector2 playerpos = new Vector2(TLBattleScene.Instance.Actor.X, TLBattleScene.Instance.Actor.Y);
        Vector2 aimpos = new Vector2(AimX, AimY);
        //Debug.LogError("AimX="+ AimX+ " AimY="+ AimY + " AimDistance="+ AimDistance);
        if (action.DoEnd(action))
        {
            this.IsAutoRun = false;
            return;
        }

        moveai = new TLActorMoveFuckWay(action, AimX, AimY, AimDistance, (e) => { return e.Name.StartsWith("bdr"); });


        if (action.MoveType == (int)AutoMoveType.MapTouch
            || action.MoveType == (int)AutoMoveType.SmallMapTouch
            || action.MoveType == (int)AutoMoveType.FollowNpc)
        {
            bCanSpeedUp = false;
        }
        else
        {
            bCanSpeedUp = true;
        }
        AutoRunByAgent(moveai);
        if (action.MoveType == (int)AutoMoveType.MapTouch)
        {
            DataMgr.Instance.UserData.LastMapTouchMoveAI = moveai;
            DataMgr.Instance.UserData.LastActorMoveAI = null;
        }
        DoAutoRun(action.IsShow, action.MoveType);
        //this.mMoveAction = action;
    }
    private void AutoRunByAgent(AbstractMoveAgent moveai)
    {

        BreakAutoRunAgent(moveai as TLActorMoveFuckWay != null ? (moveai as TLActorMoveFuckWay).EndAction : null);
        //Debugger.LogWarning("AutoRunByAgent");
        this.ZActor.AddAgent(moveai);
        moveai.OnEnd += Moveai_OnEnd;
        moveai.OnStart += Moveai_OnStart;

    }

    public void BreakAutoRunAgent(MoveEndAction nextAction = null)
    {

       
        //mMoveAction = null;
        if (DataMgr.Instance.UserData.LastActorMoveAI != null)
        {
            var moveai = DataMgr.Instance.UserData.LastActorMoveAI as TLActorMoveFuckWay;
            if (moveai != null)
            {
                if (nextAction == null || (nextAction != null
                    && !nextAction.IsSame(moveai.EndAction)))
                {
                    moveai.EndAction.IsBreak = true;
                    mFindAction = null;
                }

            }
            //Debugger.LogWarning("BreakAutoRunAgent");
            ///Debugger.LogWarning("RemoveAgent0");
            this.ZActor.RemoveAgent(DataMgr.Instance.UserData.LastActorMoveAI);
            DataMgr.Instance.UserData.LastActorMoveAI = null;
        }

    }

    private void DoAutoRun(bool isRun, int movetype)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("isRun", isRun);
        dic.Add("MoveType", movetype);
        EventManager.Fire("Event.AutoRun.Change", dic);


    }
    private void ClearAutoSmallMapRun(int movetype)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Clear();
        dic.Add("IsRun", false);
        dic.Add("MoveType", movetype);
        EventManager.Fire("Event.AutoRun.RoadPoint", dic);
    }
    public void DoStopForceQuest()
    {
        //Debugger.LogWarning("DoStopForceQuest");
        EventManager.Fire("Event.Quest.StopQuestAutoRun", EventManager.defaultParam);
    }


    [DoNotToLua]
    public void stopAutoRun(MoveEndAction moveaction, bool NextStateIsAutoRun = false,bool needstopForceQuest = true)
    {
        if (!NextStateIsAutoRun)
        {
            //mMoveAction = null;
            //Debugger.LogWarning("stopAutoRun123");
            ClearAutoRun();
            DoAutoRun(false, moveaction.MoveType);
            ClearAutoSmallMapRun(moveaction.MoveType);
            if (needstopForceQuest)
            {
                DoStopForceQuest();
            }
        }


    }


    private float Distance(Vector2 v1, Vector2 v2)
    {
        var a2 = Mathf.Pow(v2.x - v1.x, 2) + Mathf.Pow(v2.y - v1.y, 2);
        var result = Mathf.Sqrt(a2);
        return result;
    }


    private List<Vector2> getAllPoint(List<Vector2> wayPoint)
    {
        List<Vector2> newPoint = new List<Vector2>();
        for (int i = 0; i < wayPoint.Count - 1; i++)
        {
            var v1 = wayPoint[i];
            var v2 = wayPoint[i + 1];

            var distance = Distance(v1, v2);
            newPoint.Add(v1);
            if (distance > 10)
            {
                var cout = distance / 10;
                for (int j = 1; j < cout; j++)
                {
                    var PP = Vector2.Lerp(v1, v2, j / cout);
                    newPoint.Add(PP);
                }
            }
        }

        return newPoint;
    }

    private void Moveai_OnStart(AbstractAgent agent)
    {
        DataMgr.Instance.UserData.LastActorMoveAI = (AbstractMoveAgent)agent;
        WayPoint waypoint = null;
        Vector2 target = Vector2.zero;
        TLActorMoveFuckWay tlagent = null;


        if (agent is TLActorMoveFuckWay)
        {
            tlagent = agent as TLActorMoveFuckWay;
            waypoint = tlagent.WayPoints;
            if (waypoint == null || waypoint.Tail == null)
            {
                return;
            }
            target = new Vector2(waypoint.Tail.PosX, waypoint.Tail.PosY);
            tlagent.HasWay = true;
        }
        if (DataMgr.Instance.UserData.Level > speeduplevellimit)
        {
            bCanSpeedUp = false;
        }
        if (waypoint != null)
        {
            int count = waypoint.Count;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("IsRun", true);

            List<Vector2> roadPoint = new List<Vector2>();
            var wp = waypoint;
            while (wp != null)
            {
                roadPoint.Add(new Vector2(wp.PosX, wp.PosY));
                wp = wp.Next;
            }

            List<Vector2> fillPoint = getAllPoint(roadPoint);

            dic.Add("waypoint", fillPoint);
            dic.Add("target", target);
            EventManager.Fire("Event.AutoRun.RoadPoint", dic);

            float dis = waypoint.GetTotalDistance();
            //Debugger.LogError("waypoint.GetTotalDistance()" + dis);



            if (tlagent != null)
            {
                if (tlagent.EndAction != null)
                {
                    var moveType = tlagent.EndAction;
                    if (IsNeedCheckMoveActionType(moveType))
                    {
                        bCanSpeedUp = false;
                    }
                }
            }

            if (bCanSpeedUp && dis >= speedupdistance)
            {
                ActorAddSpeedUpBuffRequest reqspeedup = new ActorAddSpeedUpBuffRequest();
                this.ZActor.SendRequest(reqspeedup, (e) => { });
                tlagent.SpeedUp = true;
            }


        }



    }
    private bool IsNeedCheckMoveActionType(MoveEndAction action)
    {

        switch (action.MoveType)
        {
            case (int)AutoMoveType.SmallMapTouch:
            case (int)AutoMoveType.MapTouch:
                return true;
            default:
                return false;
        }

    }
    private void Moveai_OnEnd(AbstractAgent agent)
    {

        TLActorMoveFuckWay tlmf = agent as TLActorMoveFuckWay;
        if (tlmf != null && tlmf.SpeedUp)
        {
            ActorRemoveSpeedUpBuffRequest req = new ActorRemoveSpeedUpBuffRequest();
            this.ZActor.SendRequest(req, (e) => { });
        }
        if (agent.IsEnd && tlmf != null)
        {
            if (tlmf.EndAction != null)
            {
                //Debugger.LogWarning("stop auto run" + agent.IsEnd + " ENDACTION="+ tlmf.EndAction.QuestId);
                DoAutoRun(false, tlmf.EndAction.MoveType);
                if (!tlmf.HasWay || IsNeedCheckMoveActionType(tlmf.EndAction))
                {
                    if (tlmf.HasWay)
                        ClearAutoSmallMapRun(tlmf.EndAction.MoveType);
                    if (tlmf.EndAction.orgActorX == this.X
                        && tlmf.EndAction.orgActorY == this.Y
                        && !IsNeedCheckMoveActionType(tlmf.EndAction)
                        && !(tlmf.EndAction is MoveAndBattle)
                        && !(CurGState is AutoAttackState))
                    {
                        Debugger.LogWarning(tlmf.EndAction.GetType().FullName + "　autorun　roadname [" + tlmf.EndAction.RoadName + "] x=" + tlmf.EndAction.AimX + " y=" + tlmf.EndAction.AimY + " exception with questid = " + tlmf.EndAction.QuestId + " @small stone to check it out");
                        DoStopForceQuest();
                        //this.ChangeManualState();
                    }
                }
                if (!tlmf.EndAction.IsBreak)
                {
                    tlmf.EndAction.DoEnd(tlmf.EndAction);
                }
                this.IsAutoRun = false;
                tlmf.EndAction = null;
            }

        }
        DataMgr.Instance.UserData.LastActorMoveAI = null;
    }


}
