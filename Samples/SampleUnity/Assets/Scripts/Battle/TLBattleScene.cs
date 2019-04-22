
using Assets.Scripts;
using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameData.ZoneClient;
using DeepCore.GameSlave;
using DeepCore.GameSlave.Client;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using DeepMMO.Protocol;
using SLua;
using System;
using System.Collections.Generic;
using System.IO;
using TLBattle.Client;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using TLProtocol.Protocol.Client;
using UnityEngine;

public enum ACTIONMODE
{
    NORMALMODE,
    ATTACKMODE,
    SEEKMODE,
}

public partial class TLBattleScene : BattleScene
{
    public static TLBattleScene Instance { get; private set; }

    [DoNotToLua]
    public int TotalHeight { get { return Terrain != null ? Terrain.TotalHeight : 190; } }

    private SceneTouchHandler mSceneTouch;

    public bool IsRunning { get; private set; }

    Dictionary<string, LuaTable> mLuaObservers = new Dictionary<string, LuaTable>();



    private TLAIActor mTLActor;
    public TLAIActor Actor
    {
        get
        {
            if (mTLActor == null)
                mTLActor = GetActor() as TLAIActor;
            return mTLActor;
        }
    }


    public bool IsPickUp { get; set; }

    public int ActorHPPercent()
    {
        if (Actor == null) return 100;
        float v = (Actor.HP / (1.0f * Actor.MaxHP) * 100);
        if (v > 0 && v < 1)
            v = 1;

        return (int)(v);
    }

    ~TLBattleScene()
    {
        Debugger.Log("----------------------- ~TLBattleScene ---------------------------");
    }

    [DoNotToLua]
    public TLBattleScene(AbstractBattle client)
        : base(client)
    {
        Instance = this;
        TLNetManage.Instance.OnZoneLeaved += OnZoneLeave;
        //初始化场景触摸层.
        mSceneTouch = new SceneTouchHandler();
        mSceneTouch.OnUnitSelected += OnUnitSelected;
        mSceneTouch.OnMapTouch += OnMapTouch;
        TLUnityDebug.GetInstance().SetInterface(this);

    }

    public new ComAICell GetBattleObject(uint id)
    {
        return base.GetBattleObject(id);
    }

    public TLAIPlayer GetAIPlayer(string uuid)
    {
        var u = GetPlayerUnitByUUID(uuid);
        if (u != null)
        {
            return GetBattleObject(u.ObjectID) as TLAIPlayer;
        }

        return null;
    }

    protected override void RegistAllZoneEvent()
    {
        base.RegistAllZoneEvent();
        RegistZoneEvent<LockActorEvent>(ZoneEvent_LockActorEvent);
        RegistZoneEvent<SyncEnvironmentVarEvent>(ZoneEvent_SyncEnvironmentVarEvent);
        RegistZoneEvent<AddDynamicEffectB2C>(ZoneEvent_AddDynamicEffectB2C);
        RegistScriptEvent();
    }

    public ZoneFlag GetZoneFlag(string flagName)
    {
        return Battle.Layer.GetFlag(flagName);
    }
    private void ZoneEvent_AddDynamicEffectB2C(AddDynamicEffectB2C ev)
    {
        PlayEffectWithZoneCoord(ev.Effect, ev.X, ev.Y, ev.Direction);
    }

    private void ZoneEvent_SyncEnvironmentVarEvent(SyncEnvironmentVarEvent obj)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("key", obj.Key);
        dic.Add("value", obj.Value);
        EventManager.Fire("Event.SyncEnvironmentVarEvent", dic);
    }

    //更新技能CD状态.
    private void UpdateSkillsCD()
    {
        List<SkillBarHud.SkillData> skills = (List<SkillBarHud.SkillData>)DataMgr.Instance.UserData.GetAttribute(UserData.NotiFyStatus.SKILLDATA);
        if (skills != null)
        {
            ZoneUnit.SkillState state = null;
            for (int i = 0; i < skills.Count; i++)
            {
                SkillBarHud.SkillData sd = skills[i];
                int index = sd.Data.keyPos;
                state = this.Actor.ZActor.GetSkillState(sd.Data.baseSkillId);
                if (state != null)
                {
                    //update skill cd
                    if (IsShowSkillCD(state))
                    {
                        HudManager.Instance.SkillBar.UpdateCD(index, state.FullCDTimeMS, 1 - state.CDPercent);
                    }
                }
                if (sd.Data.skillType == TLBattle.Common.Plugins.GameSkill.TLSkillType.God && sd.Data.buffId != 0)
                {
                    ZoneUnit.BuffState bs = Actor.ZActor.GetBuff(sd.Data.buffId);
                    if (bs != null)
                        HudManager.Instance.SkillBar.UpdateBufCD(index, 1 - bs.CDPercent);
                    else
                        HudManager.Instance.SkillBar.UpdateBufCD(index, (float)this.Actor.ZActor.MP / this.Actor.ZActor.MaxMP);
                    HudManager.Instance.SkillBar.ShowBanByIndex(index, bs != null);
                }
            }
        }
    }

    private bool IsShowSkillCD(ZoneUnit.SkillState state)
    {
        bool ret = true;

        if (state.ActiveState == DeepCore.GameData.Data.SkillActiveState.Hide ||
            state.ActiveState == DeepCore.GameData.Data.SkillActiveState.Deactive ||
            state.ActiveState == DeepCore.GameData.Data.SkillActiveState.ActiveAndHide ||
            state.ActiveState == DeepCore.GameData.Data.SkillActiveState.DeactiveAndPause)
        {
            ret = false;
        }

        //是否主动取消，技能未结束前，不显示CD时间.
        if (state.Data.IsManuallyCancelable == true && Actor.ZActor.CurrentSkillAction != null && Actor.ZActor.CurrentSkillAction.SkillData.ID == state.Data.ID)
        {
            ret = false;
        }

        if (state.Data.IsCoolDownWithAction == true)
        {
            ret = false;
        }

        return ret;
    }

    private void OnActorLoadOK()
    {
        if (IsRunning)
        {
            return;
        }
        GameSceneMgr.Instance.SceneCameraNode.Reset();
        GameSceneMgr.Instance.SceneCameraNode.SetFollowTarget(Actor.ObjectRoot.transform, true);

        GameGlobal.Instance.FGCtrl.AddFingerHandler(HudManager.Instance.Rocker, (int)PublicConst.FingerLayer.Rocker);
        GameGlobal.Instance.FGCtrl.AddPinchHandler(HudManager.Instance.Rocker, (int)PublicConst.FingerLayer.Rocker);

        HudManager.Instance.Rocker.OnRockerMove += DoAxis;
        HudManager.Instance.Rocker.OnRockerStop += StopMove;
        HudManager.Instance.SkillBar.OnSkillBtnDown += OnSkillKeyDown;
        HudManager.Instance.SkillBar.OnSkillBtnUp += OnSkillKeyUp;
        HudManager.Instance.SkillBar.OnTargetBtnClick += OnChangeTarget;
        HudManager.Instance.SkillBar.OnItemBtnClick += OnItemBtnClick;
        if (GameGlobal.Instance.netMode)
        {
            HudManager.Instance.SmallMap.InitSmallMap(Actor.ZObj.Parent);
            HudManager.Instance.SmallMap.AddUnit(Actor as TLAIActor);

            Actor.bindBehaviour.OnHPChange += HudManager.Instance.PlayerInfo.SetUserHP;
            Actor.bindBehaviour.OnBuffChange += HudManager.Instance.PlayerInfo.SetBufChange;

            HudManager.Instance.PlayerInfo.InitTargetBuff(Actor);
            ////初始化HPMP
            HudManager.Instance.PlayerInfo.SetUserHP(0, Actor.HP, Actor.MaxHP);

            //监听目标点击事件.
            HudManager.Instance.PlayerInfo.OnTargetTouch += OnTargetUITouch;
            Actor.Target.OnAddTarget += (TLAIUnit unit) =>
            {
                string icon;
                int mType;
                string playerUUID = null;
                if (unit is TLAIPlayer)
                {
                    int pro = (int)(((TLAIPlayer)unit).PlayerVirtual.RolePro());
                    int gen = (int)(((TLAIPlayer)unit).PlayerVirtual.RoleGender());
                    icon = GameUtil.GetHeadIcon(pro, gen);
                    mType = (int)MonsterVisibleDataB2C.MonsterType.Elite;
                    playerUUID = unit.PlayerUUID;
                }
                else if (unit is TLAIMonster)
                {
                    icon = string.Format("static/target/{0}.png", (unit.Info.Properties as TLUnitProperties).UnitDisplayConfig.HeadIcon);
                    var mv = unit.Virtual as TLBattle.Client.Client.TLClientVirtual_Monster;
                    mType = (int)mv.GetMonsterType();
                }
                else
                {
                    icon = string.Format("static/target/{0}.png", (unit.Info.Properties as TLUnitProperties).UnitDisplayConfig.HeadIcon);
                    mType = (int)MonsterVisibleDataB2C.MonsterType.Elite;
                }


                HudManager.Instance.PlayerInfo.ChangeTarget(playerUUID, mType, icon, unit.Name(), unit.Level(), unit.HP, unit.MaxHP, unit.MP, unit.MaxMP);
                HudManager.Instance.PlayerInfo.InitTargetBuff(unit);
                unit.bindBehaviour.OnHPChange += HudManager.Instance.PlayerInfo.SetTargetHP;
                unit.bindBehaviour.OnBuffChange += HudManager.Instance.PlayerInfo.SetBufChange;
                unit.bindBehaviour.SetFocus(true);

            };

            //注册监听移除目标事件.
            Actor.Target.OnRemoveTarget += (TLAIUnit unit) =>
            {
                HudManager.Instance.PlayerInfo.RemoveTarget();
                unit.bindBehaviour.OnHPChange -= HudManager.Instance.PlayerInfo.SetTargetHP;
                unit.bindBehaviour.OnBuffChange -= HudManager.Instance.PlayerInfo.SetBufChange;
                unit.bindBehaviour.SetFocus(false);
            };
            mRecordDamageEnable = IsOpenRecordDamage();
        }
        

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN)
        //PC键盘监听.
        GameGlobal.Instance.FGCtrl.OnMoveKeyDown += DoAxis;
        GameGlobal.Instance.FGCtrl.OnMoveKeyUp += StopMove;
        GameGlobal.Instance.FGCtrl.OnKeyDown += OnKeyboardEvent;
#endif

        Actor.ZActor.OnStartPickObject += OnStartPickObject;
        Actor.ZActor.OnStopPickObject += OnStopPickObject;
        mBattleInteractive = new BattleInteractive(this, Battle.Layer, Actor);

        IsRunning = true;
        IsPickUp = false;

       
        Actor.ZActor.SendReady();


    }

    private void OnItemBtnClick(int id, SkillBarHud.ItemBtnController controller)
    {
        if (id != 0 && Actor != null)
        {
            var data = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
            object obj = null;
            data.TryGetValue("use_agent", out obj);
            if (obj != null)
            {
                int v = Convert.ToInt32(obj);
                if (v == 0)
                {
                    var str = HZLanguageManager.Instance.GetString("scene_not_use_agent");
                    GameAlertManager.Instance.ShowFloatingTips(str);
                    return;
                }
            }

            if (Actor.HP == 0)
            {
                var str = HZLanguageManager.Instance.GetString("medicine_hint");
                GameAlertManager.Instance.ShowFloatingTips(str);
                return;
            }

            ClientTakeMedicineRequest req = new ClientTakeMedicineRequest();
            req.c2s_itemID = id;

            TLNetManage.PackExtData extData = new TLNetManage.PackExtData(false, true);
            TLNetManage.Instance.Request<ClientTakeMedicineResponse>(req, (err, rsp) =>
            {
                if (Response.CheckSuccess(rsp))
                {
                    var ts = rsp.s2c_CoolDownTimeStamp - GameSceneMgr.Instance.syncServerTime.GetServerTimeUTC();
                    controller.Reset((int)ts.TotalMilliseconds);
                }
                controller.EndSendCD();
            }, extData);
        }
    }

    protected override void OnComAIActorAdded(IComAIActor iActor)
    {
        base.OnComAIActorAdded(iActor);

        TLAIActor actor = iActor as TLAIActor;
        actor.OnLoadOK = OnActorLoadOK;

    }


    private BattleInteractive mBattleInteractive;

    private void OnStartPickObject(ZoneUnit unit, TimeExpire<UnitStartPickObjectEvent> start)
    {
        if (mBattleInteractive != null)
        {
            mBattleInteractive.OnStartPickObject(unit, start);
        }
        IsPickUp = true;
    }

    private void OnStopPickObject(ZoneUnit unit, UnitStopPickObjectEvent stop)
    {
        if (mBattleInteractive != null)
        {
            mBattleInteractive.OnStopPickObject(unit, stop);
        }
        IsPickUp = false;
    }

    protected override void OnComAICellAdded(ComAICell o)
    {
        base.OnComAICellAdded(o);

        ComAIUnit u = o as ComAIUnit;
        if (u != null)
        {
            var dict = new Dictionary<object, object>();
            dict.Add("ObjectID", u.ObjectID);
            dict.Add("Name", u.SyncInfo.Name);
            dict.Add("Level", u.SyncInfo.Level);
            dict.Add("Unit", o);
            EventManager.Fire(GameEvent.SYS_ADD_UNIT, dict);

            var prelist = (u.Info.Properties as TLUnitProperties).PreLoadResList;
            if (prelist != null)
            {
                foreach (var entry in prelist)
                {
                    FuckAssetObject.PreLoad(entry, Path.GetFileNameWithoutExtension(entry));
                }
            }
        }

        //CameraManager.Instance.followTarget = o.ObjectRoot.transform;
        if (o is TLAIActor)
        {
            LoopBattleObject((ComAICell cell) =>
            {
                var unit = cell as TLAIUnit;
                if (unit != null)
                {
                    if (Actor.Force == unit.Force && unit != Actor)
                    {
                        //如果是队友就添加到队伍界面, 临时做法
                        //TLHud.Instance.teamHud.AddFriend(unit);
                    }

                    unit.BattleReady();
                }

                return true;
            });
        }
        else
        {
            var unit = o as TLAIUnit;
            if (unit != null && Actor != null)
            {
                if (Actor.Force == unit.Force)
                {
                    //TLHud.Instance.teamHud.AddFriend(unit);
                }

                if (GameGlobal.Instance.netMode)
                {
                    HudManager.Instance.SmallMap.AddUnit(unit);
                }
              

                unit.BattleReady();
            }
        }
    }

    public TLAIUnit GetUnitByTemplateId(int TemplateId)
    {
        TLAIUnit _unit = null;
        LoopBattleObject((ComAICell cell) =>
        {
            var unit = cell as TLAIUnit;
            if (unit != null)
            {
                if (unit.Info.TemplateID == TemplateId)
                {
                    _unit = unit;
                    return false;
                }

            }
            return true;

        });
        return _unit;
    }

    protected override void OnComAICellRemoved(ComAICell o)
    {
        base.OnComAICellRemoved(o);
        TLAINPC u = o as TLAINPC;
        if (u != null)
        {
            var dict = new Dictionary<string, object>();
            dict.Add("ObjectID", u.ObjectID);
            EventManager.Fire(GameEvent.SYS_REMOVE_UNIT, dict);
        }
        TLAIUnit unit = o as TLAIUnit;
        if (unit != null)
        {
            if (unit.Target != null) //清除目标关系链 
            {
                unit.Target.Destroy();
            }

            unit.RemoveUnit();
        }

        //TLAIPlayer player = o as TLAIPlayer;
        if (o is TLAIPlayer || o is TLAINPC || o is TLAIMonster)
        {
            HudManager.Instance.SmallMap.RemoveUnit(o.ObjectID);
        }

    }

    /// <summary>
    /// 可以在函数内做删除操作 
    /// 返回true 表示继续 返回false break
    /// </summary>
    /// <param name="action"></param>
    protected void LoopBattleObject(System.Func<ComAICell, bool> func)
    {
        if (func != null)
        {
            foreach (var elem in new List<ComAICell>(BattleObjects.Values))
            {
                if (!func(elem)) break;
            }
        }
    }

    protected virtual void ZoneEvent_LockActorEvent(LockActorEvent ev)
    {
        OnLockActorEvent(ev);
    }

    protected void OnLockActorEvent(LockActorEvent evt)
    {
        Debugger.Log("OnLockActorEvent");
        Actor.OnSkillChanged = OnActorSkillChanged;
    }

    protected void OnActorSkillChanged(PlayerSkillChangedEvent evt)
    {
        if (evt != null)
        {
            HudManager.Instance.SkillBar.InitSkill();
        }
    }

    public ZoneUnit GetPlayerUnitByUUID(string uuid)
    {
        if (Battle.Layer != null && Battle.Layer.IsLoaded)
        {
            return Battle.Layer.GetPlayerUnit(uuid);
        }
        return null;
    }

    /// <summary>
    /// 控制摇杆位置.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    [DoNotToLua]
    public void GetYaoGanPos(ref float x, ref float y)
    {
        if (Camera.main != null && Actor != null)
        {
            Vector3 v1 = Actor.Position;//人物位置  
            var cameraTransform = Camera.main.transform;

            // ↑前进方向
            var forward = cameraTransform.TransformDirection(new Vector3(0, 1, 1));
            forward.y = 0;
            forward = forward.normalized;
            // →向右方向
            var right = new Vector3(forward.z, 0, -forward.x);

            // 移动向量
            var targetDirection = x * right + y * forward;

            Vector3 v4 = v1 + targetDirection; // 位移后的位置
            Vector2 v5 = DeepCore.Unity3D.Utils.Extensions.UnityPos2ZonePos(this.TotalHeight, v1);//人物服务器位置
            Vector2 v6 = DeepCore.Unity3D.Utils.Extensions.UnityPos2ZonePos(this.TotalHeight, v4);//移动后服务器位置
            x = v6.x - v5.x;
            y = v6.y - v5.y;

            float max = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
            if (max > 1)
            {
                x /= max;
                y /= max;
            }
        }
    }

    [DoNotToLua]
    public void DoAxis(float dx, float dy, float px, float py)
    {
        if (Battle != null)
        {
            if (dx != 0 || dy != 0)
            {
                GetYaoGanPos(ref dx, ref dy);
            }
            if (Actor != null)
            {
                Actor.SendUnitMove(dx, dy);
            }
        }
    }

    /// <summary>
    /// 停止移动.
    /// </summary>
    [DoNotToLua]
    public void StopMove(float dx, float dy, float px, float py)
    {
        if (Battle != null)
        {
            if (Actor != null)
            {
                Actor.SendUnitStop();
            }
        }
    }

    /// <summary>
    /// PC键盘响应事件.
    /// </summary>
    /// <param name="key"></param>
    private void OnKeyboardEvent(KeyCode key)
    {
        var stringKey = key.ToString();
        LuaSystem.Instance.DoFunc("GlobalHooks.OnKeyDown", stringKey);
        switch (key)
        {
            case KeyCode.J:
                OnSkillKeyTouch(0);
                break;
            case KeyCode.K:
                OnSkillKeyTouch(1);
                break;
            case KeyCode.L:
                OnSkillKeyTouch(2);
                break;
            case KeyCode.Semicolon:
                OnSkillKeyTouch(3);
                break;
            case KeyCode.H:
                OnSkillKeyTouch(4);
                break;
            case KeyCode.Tab:
                //ChangeTarget(true);
                break;
            case KeyCode.F:
                OnSkillKeyTouch(5);
                //PickItem();
                break;
        }
    }

    private void OnSkillKeyTouch(int index)
    {
        DoSkillWithIndex(index);
    }

    private void DoSkillWithIndex(int index)
    {
        Actor.ReadyToLaunchSkillByIndex(index, true);
    }

    private void OnChangeTarget(bool isNext)
    {
        if (Actor != null)
        {
            Actor.ChangeTarget(0, isNext);
        }
    }

    private void OnTargetUITouch()
    {
        if (Actor != null && Actor.Target.HasTarget())
        {
            if (Actor.Target.TargetId == Actor.ObjectID)
                return;
            ComAIUnit unit = GetBattleObject(Actor.Target.TargetId) as ComAIUnit;
            if (unit != null)
            {
                UnitInfo.UnitType type = unit.Info.UType;
                if (type == UnitInfo.UnitType.TYPE_PLAYER)
                {
                    string uuid = unit.PlayerUUID;
                    TLClientVirtual_Player vp = unit.Virtual as TLClientVirtual_Player;
                    string name = vp.GetName();
                    string menuType = "stranger";
                    int showCfg = 1;
                    if (DataMgr.Instance.TeamData.IsTeamMember(uuid))
                    {
                        if (DataMgr.Instance.TeamData.IsLeader())
                            menuType = "teamlead";
                        else
                            menuType = "teammate";
                    }
                    else
                    {
                        if (DataMgr.Instance.TeamData.HasTeam)
                            showCfg = 1;
                        else
                            showCfg = string.IsNullOrEmpty(vp.TeamUUID) ? 1 : 2;
                    }
                    Dictionary<string, object> args = new Dictionary<string, object>();
                    args.Add("playerId", uuid);
                    args.Add("playerName", name);
                    args.Add("menuKey", menuType);
                    args.Add("showCfg", showCfg);
                    EventManager.Fire("Event.InteractiveMenu.Show", args);
                }
            }
        }
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (Battle.Layer.IsDisposed)
            return;

        base.OnUpdate(deltaTime);

        UpdateActor(deltaTime);
        StoryUpdate((int)(deltaTime * 1000));

        UpdateLayer(deltaTime);
        UpdateScriptEvent(deltaTime);
        UpdateSyncServerTime(deltaTime);
    }

    public void UpdateLayer(float delta)
    {

        if (mBattleInteractive != null)
        {
            mBattleInteractive.Update(delta);
        }
    }

    private void UpdateActor(float delta)
    {
        if (Actor == null)
            return;

        UpdateSkillsCD();
        HudManager.Instance.SkillBar.UpdateItemCD((int)(delta * 1000));
        UpdateMedicinePoolSendExpire(delta);
    }

    private void OnSkillKeyUp(int index, bool isDragEvent, Vector2 fingerPos)
    {
        if (!isDragEvent)
        {
            if (index == 0)   //多功能交互按钮.
            {

            }
        }
    }

    /// <summary>
    /// 技能键触发时调用
    /// </summary>
    /// <param name="index"></param>
    private void OnSkillKeyDown(int index)
    {
        DoSkillWithIndex(index);
    }

    private void OnMapTouch(Vector3 pos)
    {
        if (this.IsRunning && !this.Actor.IsDead)
        {
            var moveaction = new TLAIActor.MapTouchMoveAction();
            Vector2 p = DeepCore.Unity3D.Utils.Extensions.UnityPos2ZonePos(this.TotalHeight, pos);
            moveaction.AimX = p.x;
            moveaction.AimY = p.y;
            moveaction.MapId = DataMgr.Instance.UserData.MapTemplateId;
            moveaction.direction = Mathf.Atan2(p.y - Actor.Y, p.x - Actor.X);
            moveaction.IsShow = false;
            this.Actor.ChangeDirection(moveaction.direction);
            this.Actor.AutoRunByAction(moveaction);
        }
    }

    private void OnUnitSelected(uint objId)
    {
        TLAIUnit unit = this.GetBattleObject(objId) as TLAIUnit;
        if (unit != null)
        {
            Actor.SendUnitFocuseTarget(objId);
            Actor.ChangeTarget(objId, true);
            if (UnitCanTalk(objId))
            {
                NpcTalk(objId);
            }
        }
    }

    private bool UnitCanTalk(uint objId)
    {
        TLAIUnit cu = this.GetBattleObject(objId) as TLAIUnit;
        if (cu != null)
        {
            UnitInfo.UnitType uType = cu.Info.UType;
            int force = cu.Force;
            // TYPE_MANUAL 也是可交互对象
            if (uType == UnitInfo.UnitType.TYPE_NPC || uType == UnitInfo.UnitType.TYPE_NEUTRALITY || uType == UnitInfo.UnitType.TYPE_MANUAL)
            {
                if (force == Actor.Force || force == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void NpcTalk(uint objId)
    {
        TLAINPC npc = this.GetBattleObject(objId) as TLAINPC;
        if (npc != null)
        {
            if (Vector3.Distance(Actor.Position, npc.ObjectRoot.transform.position) > 2)
            {
                var moveaction = new TLAIActor.MoveAndNpcTalk(npc.TemplateID);
                moveaction.AimX = npc.X;
                moveaction.AimY = npc.Y;
                moveaction.AimDistance = 2;
                moveaction.MapId = this.SceneID;
                moveaction.Action = (action) =>
                {
                    DataMgr.Instance.QuestMangerData.TalkWithNpcByUnit(npc as TLAINPC, action.QuestId);
                };
                EventManager.Fire("Event.Quest.StopQuestAutoRun", EventManager.defaultParam);
                this.Actor.AutoRunByAction(moveaction);
                return;
            }
            DataMgr.Instance.QuestMangerData.TalkWithNpcByUnit(npc as TLAINPC, 0);
        }
    }

    /// <summary>
    /// 挂在UI层下的3D动画
    /// </summary>
    /// <param name="path">完整路径</param>
    /// <param name="duration">播放时长，0表示默认</param>
    /// <param name="size">缩放参数</param>
    public void PlayUI3DEffect(string path, float duration, float size)
    {
        if (!string.IsNullOrEmpty(path))
        {
            FuckAssetObject.GetOrLoad(path, System.IO.Path.GetFileNameWithoutExtension(path), (loader) =>
            {
                if (IsDisposed)
                {
                    loader.Unload();
                    return;
                }
                if (loader)
                {
                    Transform trans = loader.transform;
                    trans.SetParent(HZUISystem.Instance.GetPickLayer().Transform);
                    trans.localPosition = Vector3.zero;
                    trans.localEulerAngles = Vector3.zero;
                    trans.localScale = Vector3.one * size;
                    var script = DeepCore.Unity3D.Utils.EffectAutoDestroy.GetOrAdd(loader.gameObject);
                    script.aoeHandler = loader;
                    script.duration = duration;
                    script.checkDisable = true;
                    UILayerMgr.SetLayerOrder(loader.gameObject, 1000, false, (int)PublicConst.LayerSetting.UI);
                }
            });
        }
    }

    public void OnZoneLeave(DeepMMO.Client.Battle.RPGBattleClient client)
    {
        this.IsRunning = false;
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        mLuaObservers.Clear();
        ScenePlayerDamageMap.Clear();
        this.IsRunning = false;

        UnRegistScriptEvent();

        TLNetManage.Instance.OnZoneLeaved -= OnZoneLeave;
        if (mTLActor != null)
        {
            mTLActor.ZActor.OnStartPickObject -= OnStartPickObject;
            mTLActor.ZActor.OnStopPickObject -= OnStopPickObject;
            mTLActor.OnSkillChanged = null;
            mTLActor.bindBehaviour.OnHPChange -= HudManager.Instance.PlayerInfo.SetUserHP;
            mTLActor.bindBehaviour.OnBuffChange -= HudManager.Instance.PlayerInfo.SetBufChange;
            //mTLActor.Dispose();
            mTLActor = null;
        }

        if (mSceneTouch != null)
        {
            mSceneTouch.OnUnitSelected -= OnUnitSelected;
            mSceneTouch.OnMapTouch -= OnMapTouch;
            mSceneTouch.Destroy();
            mSceneTouch = null;
        }

        if (mBattleInteractive != null)
        {
            mBattleInteractive.Destroy();
            mBattleInteractive = null;
        }

        //remove events
        GameGlobal.Instance.FGCtrl.RemoveFingerHandler(HudManager.Instance.Rocker);
        GameGlobal.Instance.FGCtrl.RemovePinchHandler(HudManager.Instance.Rocker);
        HudManager.Instance.Rocker.OnRockerMove -= DoAxis;
        HudManager.Instance.Rocker.OnRockerStop -= StopMove;

        HudManager.Instance.SkillBar.OnSkillBtnDown -= OnSkillKeyDown;
        HudManager.Instance.SkillBar.OnSkillBtnUp -= OnSkillKeyUp;
        HudManager.Instance.SkillBar.OnTargetBtnClick -= OnChangeTarget;
        HudManager.Instance.SkillBar.OnItemBtnClick -= OnItemBtnClick;

        HudManager.Instance.PlayerInfo.OnTargetTouch -= OnTargetUITouch;

#if (UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN)
        GameGlobal.Instance.FGCtrl.OnMoveKeyDown -= DoAxis;
        GameGlobal.Instance.FGCtrl.OnMoveKeyUp -= StopMove;
        GameGlobal.Instance.FGCtrl.OnKeyDown -= OnKeyboardEvent;
#endif

        GameSceneMgr.Instance.SceneCameraNode.Clear();
        TLUnityDebug.GetInstance().SetInterface(null);
        this.StoryOver();
        Instance = null;
    }

    /// <summary>
    /// 单位是否为敌人.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool TargetIsEnemy(ComAIUnit target)
    {
        if (Actor == null || target == null)
            return false;

        bool ret = false;

        //ret = (Actor.ZActor.Parent as TLBattle.Client.TLZoneLayer).IsAttackable(Actor.ZActor,
        //                                                                            target.ZObj as ZoneUnit,
        //                                                                            SkillTemplate.CastTarget.Enemy);

        ret = (Actor.ZActor.Virtual as TLClientVirtual).IsEnemy(target.ZObj as ZoneUnit);

        return ret;
    }

    public void UpdateSyncServerTime(float delta)
    {
        GameSceneMgr.Instance.syncServerTime.SyncTime((int)(delta * 1000));
    }

    #region 归属权事件
    private List<uint> OwnerShipMonsterLt;

    [DoNotToLua]
    public void AddOwnerShipMonster(uint monsterID)
    {
        if (OwnerShipMonsterLt == null)
            OwnerShipMonsterLt = new List<uint>();

        if (!OwnerShipMonsterLt.Contains(monsterID))
            OwnerShipMonsterLt.Add(monsterID);

        OwnerShipChange();
    }

    [DoNotToLua]
    public void RemoveOwnerShipMonster(uint monsterID)
    {
        if (OwnerShipMonsterLt == null)
        {
            OwnerShipMonsterLt = new List<uint>();
            return;
        }

        if (!OwnerShipMonsterLt.Contains(monsterID))
            OwnerShipMonsterLt.Remove(monsterID);

        OwnerShipChange();
    }

    [DoNotToLua]
    public void ClearOwnerShipMonsterLt()
    {
        if (OwnerShipMonsterLt == null)
            return;
    }

    public List<uint> GetOwnerShipMonsterID()
    {
        return OwnerShipMonsterLt;
    }

    public void OwnerShipChange()
    {
        if (OwnerShipChangeHandler != null)
        {
            OwnerShipChangeHandler.Invoke();
        }
    }

    public System.Action OwnerShipChangeHandler;

    #endregion

    #region 血池

    private TimeExpire<int> mMedicinePoolSendExpire = null;
    private bool mAllowUseMedicinePool = false;

    public void UseMedicinePool()
    {
        if (mMedicinePoolSendExpire == null)
        {
            mMedicinePoolSendExpire = new TimeExpire<int>(1500);
        }

        if (mMedicinePoolSendExpire.ExpireTimeMS <= 0)
        {
            ClientUseMedicinePoolRequest req = new ClientUseMedicinePoolRequest();
            TLNetManage.PackExtData extData = new TLNetManage.PackExtData(false, false);
            TLNetManage.Instance.Request<ClientUseMedicinePoolResponse>(req, (err, rsp) =>
            {
                if (Response.CheckSuccess(rsp))
                {
                    DataMgr.Instance.UserData.MedicinePoolCurCount = rsp.s2c_count;
                    DataMgr.Instance.UserData.GameOptionsData.MedicinePoolTimeStamp = rsp.s2c_cdTimeStamp;
                }
            }, extData);

            mMedicinePoolSendExpire.Reset();
        }

    }

    private void UpdateMedicinePoolSendExpire(float delta)
    {
        if (mMedicinePoolSendExpire != null)
        {
            var t = (int)(delta * 1000);
            mMedicinePoolSendExpire.Update(t);
        }
    }

    public bool AllowUseMedicinePool()
    {
        return mAllowUseMedicinePool;
    }

    public void CheckAllowUseMedicinePool()
    {
        if (!GameGlobal.Instance.netMode)
        {
            return;
        }
        var data = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
        if (data != null)
        {
            object obj = null;
            if (data.TryGetValue("use_reserve", out obj))
            {
                int v = Convert.ToInt32(obj);
                mAllowUseMedicinePool = (v == 1);
                return;
            }
        }

        mAllowUseMedicinePool = false;
    }

    #endregion

    #region 伤害记录.

    private HashMap<uint, long> ScenePlayerDamageMap = new HashMap<uint, long>();
    private bool mRecordDamageEnable = false;
    [DoNotToLua]
    public void RecordPlayerDamage(uint objID, int damage)
    {
        if (!mRecordDamageEnable)
            return;

        if (damage > 0)
        {
            var d = ScenePlayerDamageMap.Get(objID);
            d += damage;
            ScenePlayerDamageMap.Put(objID, d);
        }

    }

    public HashMap<uint, long> GetScenePlayerDamageMap()
    {
        return ScenePlayerDamageMap;
    }

    private bool IsOpenRecordDamage()
    {
        var data = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
        object obj = null;
        data.TryGetValue("is_damagerank", out obj);
        int v = Convert.ToInt32(obj);
        if (v == 1) return true;
        else return false;
    }

    [DoNotToLua]
    public bool RecordDamgeEnable()
    {
        return mRecordDamageEnable;
    }

    #endregion
}