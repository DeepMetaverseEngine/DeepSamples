
using UnityEngine;



using TLBattle.Client;
using DeepCore.GameSlave;
using DeepCore.GameData.Zone;
using TLClient.Modules;
using System;
using DeepCore;
using TLBattle.Client.Client;
using TLBattle.Common.Plugins;
using TLBattle.Message;

// 用于展示显示单位信息
public class TLAIBehaviour : MonoBehaviour
{
    private TLAIUnit AIUnit;
    private ZoneUnit ZUnit;
    private UnitInfo Info { get { return this.ZUnit.Info; } }
    public BattleInfoBar InfoBar { get; private set; }
    private UnitShadow mShadowEffect;
    public bool HasInit { private set; get; }

    public delegate void OnHPMPChangeHandle(int oldHP, int newHP, int maxHP);
    public event OnHPMPChangeHandle OnHPChange;
    public event OnHPMPChangeHandle OnMPChange;


    public delegate void OnBuffChangeHandle(TLAIUnit unit, PlayerInfoHud.ClientBuffInfo buff, PlayerInfoHud.BuffReasonType reason);
    public event OnBuffChangeHandle OnBuffChange;
    protected TLClientVirtual ClientVirtual { get { return ZUnit.Virtual as TLClientVirtual; } }

    public static T Bind<T>(TLAIUnit unit, ZoneObject zobj) where T : TLAIBehaviour
    {
        T component = unit.ObjectRoot.AddComponent<T>((T ret) =>
        {
            ret.AIUnit = unit;
            ret.ZUnit = zobj as ZoneUnit;
        });

        return component;
    }

    protected virtual void Awake()
    {
        this.enabled = false;
    }

    public void Start()
    {
        this.ZUnit.OnHPChanged += HPChange;
        this.ZUnit.OnMaxHPChanged += MaxHPChange;
        this.ZUnit.OnMPChanged += MPChange;
        this.ZUnit.OnMaxMPChanged += MaxMPChange;
        this.ZUnit.OnBuffAdded += ZUnit_OnBuffAdded;
        this.ZUnit.OnBuffChanged += ZUnit_OnBuffChanged;
        this.ZUnit.OnBuffRemoved += ZUnit_OnBuffRemoved;
    }


    private void BuffChange(TLAIUnit unit, ZoneUnit.BuffState buff, PlayerInfoHud.BuffReasonType reason)
    {
        var prop = buff.Data.Properties as TLBuffProperties;
        if (prop.IsShowLabel && OnBuffChange != null)
        {
            PlayerInfoHud.ClientBuffInfo cb = new PlayerInfoHud.ClientBuffInfo();
            cb.Data = buff.Data;
            cb.id = buff.Data.TemplateID;
            cb.PassTime = (int)(buff.CDPercent * buff.Data.LifeTimeMS);
            cb.TotalTime = buff.Data.LifeTimeMS;


            if (reason != PlayerInfoHud.BuffReasonType.Removed)
            {
                var _info = unit.BuffInfoList.GetOrAdd(cb.id, (id) =>
                {
                    return cb;
                });
                _info = cb.Clone();
            }
            else
            {
                unit.BuffInfoList.RemoveByKey(cb.id);
            }

            OnBuffChange(unit, cb, reason);
        }

    }
    private void ZUnit_OnBuffRemoved(ZoneUnit unit, ZoneUnit.BuffState buff)
    {

        BuffChange(AIUnit, buff, PlayerInfoHud.BuffReasonType.Removed);
    }

    private void ZUnit_OnBuffChanged(ZoneUnit unit, ZoneUnit.BuffState buff)
    {
        BuffChange(AIUnit, buff, PlayerInfoHud.BuffReasonType.Change);
    }

    private void ZUnit_OnBuffAdded(ZoneUnit unit, ZoneUnit.BuffState buff)
    {
        BuffChange(AIUnit, buff, PlayerInfoHud.BuffReasonType.Add);
    }

    protected virtual void OnDestroy()
    {

        if (InfoBar != null)
        {
            InfoBar.Remove();
            InfoBar = null;
        }
        if (mShadowEffect != null)
        {
            mShadowEffect.DoSelect(false);
            mShadowEffect = null;
        }
        OnHPChange = null;
        OnMPChange = null;
        OnBuffChange = null;
    }

    protected virtual void HPChange(ZoneUnit unit, int oldHP, int newHP)
    {
        if (InfoBar != null)
        {
            InfoBar.SetHP(newHP, unit.MaxHP);
        }

        if (OnHPChange != null)
        {
            OnHPChange(0, unit.HP, unit.MaxHP);
        }


    }

    protected virtual void MaxHPChange(ZoneUnit unit, int oldMaxHP, int newMaxHP)
    {
        if (InfoBar != null)
        {
            InfoBar.SetHP(unit.HP, unit.MaxHP);
        }
        if (OnHPChange != null)
        {
            OnHPChange(0, unit.HP, unit.MaxHP);
        }
    }

    protected virtual void MPChange(ZoneUnit unit, int oldMP, int newMP)
    {
        if (OnMPChange != null)
        {
            OnMPChange(0, unit.MP, unit.MaxMP);
        }
    }

    protected virtual void MaxMPChange(ZoneUnit unit, int oldMaxMP, int newMaxMP)
    {
        if (OnMPChange != null)
        {
            OnMPChange(0, unit.MP, unit.MaxMP);
        }
    }

    public void Init()
    {
        InitInfoBar(true);
        InitShadow();
        SceneTouchHandler.AddUnitTouchLayer(AIUnit);
        HasInit = true;
    }

    public void InitInfoBar(bool reset = false)
    {
        if (reset && InfoBar != null)
        {
            InfoBar.Remove();
            InfoBar = null;
        }

        Transform head_name = null;
        if (InfoBar == null)
        {
            if (AIUnit.Vehicle != null && AIUnit.Vehicle.IsRiding)
            {
                var _object = AIUnit.Vehicle.GetNode("Head_Name_Ride");
                if (_object != null)
                {
                    head_name = _object.transform;
                    AIUnit.HeadTransform = head_name;
                }
            }
            else
            {
                head_name = AIUnit.GetDummyNode("Head_Name").transform;
                AIUnit.HeadTransform = head_name;
            }
            head_name = head_name == null ? transform : head_name;
            //head_name = head_name ?? transform;

            this.InfoBar = BattleInfoBarManager.AddInfoBar(head_name, Vector3.zero, AIUnit is TLAIActor, true);
        }

        InfoBar.ShowHpCtrl = false;
        InfoBar.SetHP(this.ZUnit.HP, this.ZUnit.MaxHP);
        CheckHpBarType();
    }

    protected virtual void InitShadow()
    {
        //添加脚底光圈和阴影 
        if (AIUnit is TLAIActor)
        {
            return;
        }

        mShadowEffect = AIUnit.ObjectRoot.GetComponent<UnitShadow>();
        if (mShadowEffect == null)
        {
            mShadowEffect = AIUnit.ObjectRoot.AddComponent<UnitShadow>();
            //mShadowEffect.Init(foot, ZUnit.Info.BodySize, ZUnit.Force);
            mShadowEffect.Init(AIUnit);
        }
    }

    private bool mDisposed = false;
    public void Dispose()
    {
        if (mDisposed)
        {
            return;
        }
        mDisposed = true;
        if (mShadowEffect)
        {
            mShadowEffect.Dispose();
        }
    }

    public void ShowQuestStateFlag(int npcQuesttype, int questState)
    {
        if (InfoBar != null)
        {
            int questflagtype = TLAINPC.NpcQuestType.None;
            switch (npcQuesttype)
            {
                case QuestType.TypeStory:
                    if (questState == QuestState.NotAccept)
                    {
                        questflagtype = TLAINPC.NpcQuestType.StorySign;
                    }
                    else if (questState == QuestState.CompletedNotSubmited)
                    {
                        questflagtype = TLAINPC.NpcQuestType.StoryAsk;
                    }
                    else if (questState == QuestState.NotCompleted)
                    {
                        questflagtype = TLAINPC.NpcQuestType.DarkAsk;
                    }
                    else
                    {
                        Debugger.Log("QuestType.TypeStory queststate=" + questState);
                    }
                    break;
                case QuestType.TypeDaily:
                    if (questState == QuestState.NotAccept)
                    {
                        questflagtype = TLAINPC.NpcQuestType.DailySign;
                    }
                    else if (questState == QuestState.CompletedNotSubmited)
                    {
                        questflagtype = TLAINPC.NpcQuestType.DailyAsk;
                    }
                    else if (questState == QuestState.NotCompleted)
                    {
                        questflagtype = TLAINPC.NpcQuestType.DarkAsk;
                    }
                    else
                    {
                        Debugger.Log("QuestType.TypeDaily queststate=" + questState);
                    }
                    break;
                case QuestType.TypeTip:
                    if (questState == QuestState.NotCompleted)
                    {
                        questflagtype = TLAINPC.NpcQuestType.DailySign;
                    }
                    break;
                default:
                    questflagtype = TLAINPC.NpcQuestType.None;
                    break;

            }
            if (questflagtype == TLAINPC.NpcQuestType.None)
            {
                InfoBar.ShowNPCQuest(-1);
            }
            else
            {
                int[] tileid = { 163, 164, 160, 161, 162 };

                InfoBar.ShowNPCQuest(tileid[questflagtype - 1]);
            }
        }
    }
    public void ShowHpBar(bool var)
    {
        if (InfoBar != null)
            InfoBar.ShowHpCtrl = var;
    }

    public void CheckHpBarType()
    {
        if (this.Info.UType == UnitInfo.UnitType.TYPE_MONSTER)
        {
            TLBattle.Client.Client.TLClientVirtual_Monster vm = ZUnit.Virtual as TLBattle.Client.Client.TLClientVirtual_Monster;
            if (vm != null)
            {
                if (TLBattleScene.Instance.TargetIsEnemy(AIUnit) == false)
                {
                    InfoBar.SetHPBarType(BattleInfoBar.HPBarType.GREEN);
                }
                else
                {
                    MonsterVisibleDataB2C.AtkTendency atkTendency = vm.GetMonsterTendency();
                    MonsterVisibleDataB2C.MonsterType type = vm.GetMonsterType();

                    if (vm.IsOwnerShipMonster())
                    {
                        var actor = DataMgr.Instance.UserData.GetActor();
                        var shareRange = GameUtil.GetIntGameConfig("shared_range");
                        bool hasOwnerShip = actor.PlayerVirtual.HasMonsterOwnerShip(vm.GetOwnerShipUUID(), shareRange);
                        ////通知主角扫所有单位显示归属权.
                        //actor.CheckAllPlayerOwerShip(vm.GetOwnerShipUUID(), shareRange);
                        ////显示图标.
                        //bool showOwnerShip = (hasOwnerShip && vm.GetOwnerShipUUID() != null);

                        if (hasOwnerShip)
                            InfoBar.SetHPBarType(atkTendency == MonsterVisibleDataB2C.AtkTendency.Active ? BattleInfoBar.HPBarType.RED : BattleInfoBar.HPBarType.YELLOW);
                        else
                            InfoBar.SetHPBarType(BattleInfoBar.HPBarType.GRAY);
                    }
                    else
                    {
                        InfoBar.SetHPBarType(atkTendency == MonsterVisibleDataB2C.AtkTendency.Active ? BattleInfoBar.HPBarType.RED : BattleInfoBar.HPBarType.YELLOW);
                    }

                    switch (type)
                    {
                        case MonsterVisibleDataB2C.MonsterType.Normal:
                            break;
                        case MonsterVisibleDataB2C.MonsterType.Elite:
                            InfoBar.SetName(AIUnit.Name()); //精英怪和BOSS添加怪物名字显示
                            InfoBar.SetMonsterType(BattleInfoBar.MonsterType.Elite);
                            break;
                        case MonsterVisibleDataB2C.MonsterType.Boss:
                            InfoBar.SetName(AIUnit.Name()); //精英怪和BOSS添加怪物名字显示
                            InfoBar.SetMonsterType(BattleInfoBar.MonsterType.Boss);
                            break;
                    }
                    InfoBar.SetTitle(vm.TitleID());
                }
            }
        }
        else if (this.Info.UType == UnitInfo.UnitType.TYPE_BUILDING)
        {
            InfoBar.SetName(AIUnit.Name());
        }
        else if (this.Info.UType == UnitInfo.UnitType.TYPE_PLAYERMIRROR)
        {
            TLClientVirtual_PlayerMirror vp = ZUnit.Virtual as TLClientVirtual_PlayerMirror;
            if (TLBattleScene.Instance.TargetIsEnemy(AIUnit))
                InfoBar.SetHPBarType(BattleInfoBar.HPBarType.RED);

            InfoBar.SetName(AIUnit.Name(), vp.GetPKColor());
            InfoBar.SetGuild(vp.GuildName(), false);
            InfoBar.SetPractice(vp.PracticeLv());
            InfoBar.SetTitle(vp.TitleID(), vp.TitleNameExt());
            var mapData = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
            if (mapData != null)
            {
                if (Convert.ToInt32(mapData["show_forceflag"]) == 1) //战场
                    InfoBar.SetPVPIcon(ZUnit.Force);
                else if (Convert.ToInt32(mapData["show_carriage"]) == 1) //押镖
                    InfoBar.SetCarriage(ZUnit.Force);
                else if (Convert.ToInt32(mapData["can_revenge"]) == 1) //可复仇场景
                {
                    if (AIUnit.IsActor())
                    {
                        CheckAllPlayersInRevenge(vp);
                    }
                    else
                    {
                        var actor = DataMgr.Instance.UserData.GetActor();
                        if (actor.PlayerVirtual.InRevengeList(vp))
                        {
                            InfoBar.SetRevenge(true);
                        }
                    }
                }
            }
        }
        else if (this.Info.UType == UnitInfo.UnitType.TYPE_PLAYER)
        {
            TLClientVirtual_Player vp = ZUnit.Virtual as TLClientVirtual_Player;
            if (TLBattleScene.Instance.TargetIsEnemy(AIUnit))
                InfoBar.SetHPBarType(BattleInfoBar.HPBarType.RED);

            InfoBar.SetName(AIUnit.Name(), vp.GetPKColor());
            InfoBar.SetGuild(vp.GuildName(), false);
            InfoBar.SetPractice(vp.PracticeLv());
            InfoBar.SetTitle(vp.TitleID(), vp.TitleNameExt());
            var mapData = GameUtil.GetDBData("MapData", DataMgr.Instance.UserData.MapTemplateId);
            if (mapData != null)
            {
                if (Convert.ToInt32(mapData["show_forceflag"]) == 1) //战场
                    InfoBar.SetPVPIcon(ZUnit.Force);
                else if (Convert.ToInt32(mapData["show_carriage"]) == 1) //押镖
                    InfoBar.SetCarriage(ZUnit.Force);
                else if (Convert.ToInt32(mapData["can_revenge"]) == 1) //可复仇场景
                {
                    if (AIUnit.IsActor())
                    {
                        CheckAllPlayersInRevenge(vp);
                    }
                    else
                    {
                        var actor = DataMgr.Instance.UserData.GetActor();
                        if (actor.PlayerVirtual.InRevengeList(vp))
                        {
                            InfoBar.SetRevenge(true);
                        }
                    }
                }

                if (Convert.ToInt32(mapData["is_guildchase"]) == 1)//公会追杀令.
                {
                    if(AIUnit.IsActor())
                    {
                        CheckAllPlayerInGuildChaseMap(vp);
                    }
                    else
                    {
                        var actor = DataMgr.Instance.UserData.GetActor();
                        if(actor.PlayerVirtual.InGuildChaseMap(vp))
                        {
                            InfoBar.SetChaseOrder(true);
                        }
                    }
                }
            }
        }
        else if (this.Info.UType == UnitInfo.UnitType.TYPE_PET)
        {
            InfoBar.SetName(AIUnit.Name());
        }
        else if (this.Info.UType == UnitInfo.UnitType.TYPE_NPC)
        {
            if (TLBattleScene.Instance.TargetIsEnemy(AIUnit))
            {
                InfoBar.SetHPBarType(BattleInfoBar.HPBarType.RED);
                InfoBar.SetName(AIUnit.Name(), GameUtil.ARGB_To_RGBA(0xffff5858));
            }
            else
                InfoBar.SetName(AIUnit.Name());
        }

        InfoBar.SetForce(this.ZUnit.Force, this.Info.UType, ZUnit is ZoneActor, this.ZUnit.HP == 0);
        if (this.Info.UType == UnitInfo.UnitType.TYPE_PLAYER && GameUtil.IsShowRedName(DataMgr.Instance.UserData.MapTemplateId))
        {
            TLClientVirtual_Player vp = ZUnit.Virtual as TLClientVirtual_Player;
            InfoBar.SetName(vp.GetName(), vp.GetPKColor());
        }
    }

    public void CheckAllPlayersInRevenge(TLClientVirtual_Player vp)
    {
        var playerList = GameSceneMgr.Instance.BattleScene.BattleObjects;
        foreach (var cell in playerList.Values)
        {
            if (cell is TLAIPlayer)
            {
                var player = cell as TLAIPlayer;
                if (player.bindBehaviour.HasInit)
                {
                    player.bindBehaviour.InfoBar.SetRevenge(vp.InRevengeList(player.Virtual as TLClientVirtual_Player));
                }
            }
        }
    }

    public void CheckAllPlayerInGuildChaseMap(TLClientVirtual_Player vp)
    {
        var playerList = GameSceneMgr.Instance.BattleScene.BattleObjects;
        foreach (var cell in playerList.Values)
        {
            if (cell is TLAIPlayer)
            {
                var player = cell as TLAIPlayer;
                if (player.bindBehaviour.HasInit)
                {
                    bool f = vp.InGuildChaseMap(player.Virtual as TLClientVirtual_Player);
                    player.bindBehaviour.InfoBar.SetChaseOrder(f);
                }
            }
        }
    }

    void Update()
    {

    }

    public void SetFocus(bool value)
    {
        //InfoBar.SetFocus(value);
        if (mShadowEffect != null)
            mShadowEffect.DoSelect(value);
    }
}
