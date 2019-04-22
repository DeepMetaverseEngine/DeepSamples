using System.Collections.Generic;
using SLua;
using System;
using TLBattle.Common.Data;
using TLClient.Modules.Bag;
using DeepMMO.Protocol.Client;
using DeepCore;
using TLBattle.Common.Plugins;
using DeepMMO.Data;
using TLClient.Protocol.Modules;
using TLClient.Protocol.Modules.Package;
using TLProtocol.Protocol.Client;
using TLProtocol.Data;
using DeepCore.GameSlave.Agent;
using UnityEngine;
using DeepMMO.Protocol;

public class UserData : ISubject<UserData>
{

    public enum NotiFyStatus : int
    {
        NULL = 0,
        PRO = 1 << 1,
        LEVEL = 1 << 2,
        PROP = 1 << 3,
        COPPER = 1 << 4,
        DIAMOND = 1 << 5,
        VIP = 1 << 6,
        EXP = 1 << 7,
        NEEDEXP = 1 << 8,
        SKILLDATA = 1 << 9,
        //银币
        SILVER = 1 << 10,
        //性别
        GENDER = 1 << 11,
        //战斗力.
        FIGHTPOWER = 1 << 12,
        //增加的经验.
        ADDEXP = 1 << 13,
        //修行之道.
        PRACTICELV = 1 << 14,
        VIPCUREXP = 1 << 15,
        //血池剩余次数
        MEDICINEPOOLCURCOUNT = 1 << 16,
        //累积充值.
        ACCUMULATIVECOUNT = 1 << 17,
        //溢出经验.
        OVERFLOWEXP = 1<<18,
        //溢出的经验
        OVERFLOWADDEXP = 1<<19,
        //所有标志位
        ALL = int.MaxValue,
    };

    public const string COPPER = "Copper";
    public const string SILVER = "Silver";
    public const string DIAMOND = "Diamond";
    public const string EXP = "Exp";

    static Dictionary<string, NotiFyStatus> mKeyToAttrMap = new Dictionary<string, NotiFyStatus>();

    Dictionary<long, object> mAttributes = new Dictionary<long, object>();

    HashSet<IObserver<UserData>> mObservers = new HashSet<IObserver<UserData>>();
    Dictionary<string, LuaTable> mLuaObservers = new Dictionary<string, LuaTable>();
    public uint ObjectId { get; set; }  //对象ID

    public string RoleID { get; set; }  //角色ID(uuid)
    public int MasterId { get; set; }
    public string AccountID { get; set; }  //用户ID
    public string DigitID { get; set; }  //数字ID
    public ServerInfo Serverinfo { get; set; }  //服务器信息
    public string ServerID { get; set; } //服务器id

    public string RoleCreateTime { get; set; } // 角色创建时间戳秒

    public string Name { get; set; }  //名字

    public int Pro { get; set; }  //职业

    public int Gender { get; set; }  //性别

    public string MapName { get; set; }
    public int MapTemplateId { get; set; }
    public int ZoneTemplateId { get; set; }
    public string ZoneUUID { get; set; }
    public string ZoneGuildId { get; set; }
    public int RoleTemplateId { get; set; }
    public int SceneType { get; set; }  //场景类型
    public int ZoneLineIndex { get; set; }
    public int PKValue { get; set; }
    public int TargetLv { get; set; }//目标等级系统
    public string ServerName { get; set; }
    public HashMap<string, byte> FuncOpen { get; private set; }
    public string GuildId { get; set; }  //公会id
    public int TitleID { get; private set; }//称号ID
    public string TitleNameExt { get; set; }//称号ID
    public string SpouseId { get; set; }
    public string SpouseName { get; set; }
    public bool ChangeMarryScene { get; set; }

    public string CurSceneGuildName { get; set; }
    private string mGuildName;
    private int mMedicinePoolCurCount;
    public string GuildName
    {
        get { return mGuildName; }
        set
        {
            mGuildName = value;
            if (GetActor() != null && GetActor().bindBehaviour.HasInit)
            {
                GetActor().bindBehaviour.InfoBar.SetGuild(mGuildName);
            }
        }
    }  //公会名字

    public AbstractMoveAgent LastActorMoveAI = null;
    public AbstractMoveAgent LastMapTouchMoveAI = null;
    public List<TLSceneNextLink> LastSceneNextlink = null;
    public TLAIActor.MoveEndAction LastMoveEndAction = null;//跨地图寻路

    private HashMap<string, string> mRoleFreeData = new HashMap<string, string>();
    private TLGameOptionsData mGameOptionsData;
    public Action<TLGameOptionsData> OnGameOptionDataChange;
    [DoNotToLua]
    public bool SendMedicinePoolTips
    {
        get; set;
    }
    //任务用雷达信息
    private HashMap<string, TLAIActor.RadarData> mRadarDatas = new HashMap<string, TLAIActor.RadarData>();
    public HashMap<string, TLAIActor.RadarData> RadarDatas
    {
        get
        {
            return mRadarDatas;
        }
    }

    public TLAIActor.RadarData GetCurMapRadarData(int mapid)
    {
        foreach (var data in mRadarDatas)
        {
            if (data.Value.mapid == mapid)
            {
                return data.Value;
            }
        }
        return null;
    }

    public int MedicinePoolCurCount
    {
        get { return mMedicinePoolCurCount; }
        set
        {
            mMedicinePoolCurCount = value;
            Notify((int)NotiFyStatus.MEDICINEPOOLCURCOUNT);
        }
    }
    
    public TLGameOptionsData GameOptionsData
    {
        get { return mGameOptionsData; }
        set
        {
            mGameOptionsData = value;
            if (OnGameOptionDataChange != null)
            {
                OnGameOptionDataChange.Invoke(mGameOptionsData);
            }
        }
    }

    public HashMap<string, string> RoleFreeData
    {
        get
        {
            if (mRoleFreeData == null)
            {
                mRoleFreeData = new HashMap<string, string>();
            }
            return mRoleFreeData;
        }
    }

    public int Force  //阵营
    {
        get
        {
            TLAIActor actor = GetActor();
            if (actor != null)
                return actor.Force;
            return 0;
        }
    }
    public int Level
    {
        get { return TryGetIntAttribute(NotiFyStatus.LEVEL, 0); }
    }
    
    public int VipLv
    {
        get { return TryGetIntAttribute(NotiFyStatus.VIP, 0); }
    }
    
    public long OverFlowExp
    {
        get { return TryGetLongAttribute(NotiFyStatus.OVERFLOWEXP, 0); }
        set {SetAttribute(NotiFyStatus.OVERFLOWEXP,value);}
    }
    
    
    public int VipCurExp
    {
        get { return TryGetIntAttribute(NotiFyStatus.VIPCUREXP, 0); }
    }

    private Dictionary<int, TLPropObject> mRoleProp;  //角色属性集合
    public List<int> LastUpdate { get; private set; }   //最后更新的角色属性

    public ClientNormalBag Bag
    {
        get { return TLNetManage.Instance.NetClient.bagModule.Bag; }
    }

    public ClientFateBag FateBag
    {
        get { return TLNetManage.Instance.NetClient.bagModule.FateBag; }
    }

    public ClientFateEquipBag FateEquipBag
    {
        get { return TLNetManage.Instance.NetClient.bagModule.FateEquipBag; }
    }

    public ClientEquipBag EquipBag
    {
        get { return TLNetManage.Instance.NetClient.bagModule.EquipedBag; }
    }

    public ClientWarehourse Warehourse
    {
        get { return TLNetManage.Instance.NetClient.bagModule.WarehourseBag; }
    }



    public ClientVirtualBag VirtualBag
    {
        get { return TLNetManage.Instance.NetClient.bagModule.VirtualBag; }
    }

    public ClientSimpleExternBag QuestBag
    {
        get { return TLNetManage.Instance.NetClient.questModule.Bag; }
    }

    public ClientPublicSnapReader<TLClientRoleSnap> RoleSnapReader { get; private set; }


    public UserData()
    {
        mRoleProp = new Dictionary<int, TLPropObject>();
        LastUpdate = new List<int>();
        FuncOpen = new HashMap<string, byte>();
        MapTemplateId = GameGlobal.Instance.SceneID;
        RoleTemplateId = GameGlobal.Instance.ActorTemplateID;
        if (mKeyToAttrMap.Count == 0)
        {
            //网络消息到枚举的映射
            mKeyToAttrMap.Add("pro", NotiFyStatus.PRO);
            mKeyToAttrMap.Add("level", NotiFyStatus.LEVEL);
            mKeyToAttrMap.Add("vipLv", NotiFyStatus.VIP);
            mKeyToAttrMap.Add("vipCurExp", NotiFyStatus.VIPCUREXP);
            mKeyToAttrMap.Add(EXP, NotiFyStatus.EXP);
            mKeyToAttrMap.Add("needExp", NotiFyStatus.NEEDEXP);
            mKeyToAttrMap.Add("gender", NotiFyStatus.GENDER);
            mKeyToAttrMap.Add("fightpower", NotiFyStatus.FIGHTPOWER);
            mKeyToAttrMap.Add("AddExp", NotiFyStatus.ADDEXP);
            mKeyToAttrMap.Add(COPPER, NotiFyStatus.COPPER);
            mKeyToAttrMap.Add(DIAMOND, NotiFyStatus.DIAMOND);
            mKeyToAttrMap.Add(SILVER, NotiFyStatus.SILVER);
            mKeyToAttrMap.Add("practiceLv", NotiFyStatus.PRACTICELV);
            mKeyToAttrMap.Add("AccumulativeCount", NotiFyStatus.ACCUMULATIVECOUNT);
            mKeyToAttrMap.Add("OverflowExp", NotiFyStatus.OVERFLOWEXP);
            mKeyToAttrMap.Add("OverflowAddExp",NotiFyStatus.OVERFLOWADDEXP);
        }
    }

    public ItemData FindItemDataByID(string id)
    {
        var item = EquipBag.FindItemByID(id);
        if (item != null)
        {
            return item;
        }
        item = Bag.FindItemByID(id);
        if (item != null)
        {
            return item;
        }
        item = Warehourse.FindItemByID(id);
        return item;
    }

    public void LuaSaveOptionsData(string key, string val)
    {
        GameOptionsData.Options[key] = val;
    }

    public void InitNetWork()
    {
        //监听网络消息
        TLNetManage.Instance.Listen<PlayerDynamicNotify>(OnPlayerDynamicNotify);
        TLNetManage.Instance.Listen<ClientFunctionOpenNotify>(OnFunctionOpenNotify);
        TLNetManage.Instance.NetClient.bagModule.OnInit += BagModuleOnInit;
        ////发送网络请求例子
        //CommonRPG.Protocol.Client.ClientGetRandomNameRequest request = new CommonRPG.Protocol.Client.ClientGetRandomNameRequest();
        //request.c2s_role_template_id = 3;
        //TLNetManage.Instance.Request<CommonRPG.Protocol.Client.ClientGetRandomNameResponse>(request, (err, rsp) =>
        //{
        //    string rdnName = rsp.s2c_name;
        //    //do logic
        //});
    }

    public void ReadRoleData(TLProtocol.Data.TLClientRoleData roleData)
    {
        RoleID = roleData.uuid;
        MasterId = roleData.MasterId;
        AccountID = roleData.account_uuid;
        DigitID = roleData.digitID;
        Name = roleData.name;
        Pro = (int)roleData.proType;
        Gender = (int)roleData.gender;
        GuildId = roleData.guildId;
        GuildName = roleData.guildName;
        RoleTemplateId = roleData.role_template_id;
        MapTemplateId = roleData.last_map_template_id;
        ZoneUUID = roleData.last_zone_uuid;
        mRoleFreeData = roleData.ClientModifyData;
        GameOptionsData = roleData.gameOptionsData;
        FuncOpen = roleData.funcOpen;
        PKValue = roleData.PKValue;
        TargetLv = roleData.TargerSystemLv;
        ServerList sl = new ServerList();
        ServerInfo si = sl.GetServerById(roleData.server_name);
        Serverinfo = si;
        ServerID = si.id;
        ServerName = si.name;
        RoleCreateTime =
            (roleData.create_time - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0)))
            .TotalMilliseconds.ToString();
        MedicinePoolCurCount = roleData.MedicinePoolCurCount;
        
        SetAttribute(NotiFyStatus.LEVEL, roleData.level);
        SetAttribute(NotiFyStatus.EXP, roleData.exp);
        SetAttribute(NotiFyStatus.NEEDEXP, roleData.needExp);
        SetAttribute(NotiFyStatus.GENDER, (int)roleData.gender);
        SetAttribute(NotiFyStatus.FIGHTPOWER, roleData.FightPower);
        SetAttribute(NotiFyStatus.PRACTICELV, roleData.practiceLv);
        SetAttribute(NotiFyStatus.PRO, (int)roleData.proType);
        SetAttribute(NotiFyStatus.VIP, roleData.VipLv);
        SetAttribute(NotiFyStatus.VIPCUREXP, roleData.VipCurExp);
        SetAttribute(NotiFyStatus.ACCUMULATIVECOUNT, roleData.AccumulativeCount);
        SetAttribute(NotiFyStatus.OVERFLOWEXP, roleData.overflowExp);

        this.TitleID = roleData.TitleID;
        this.TitleNameExt = roleData.TitleNameExt;

        this.SpouseId = roleData.spouseId;
        this.SpouseName = roleData.spouseName;

        if (RoleSnapReader == null)
        {
            RoleSnapReader = new ClientPublicSnapReader<TLClientRoleSnap>(RoleSnapRequest);
        }
    }

    public void FixRoleSnap(TLClientRoleSnap snap)
    {
        var p = TLBattleScene.Instance.GetAIPlayer(snap.ID);
        if (p == null)
        {
            return;
        }
        snap.Level = p.Level();
        snap.MapTemplateID = DataMgr.Instance.UserData.MapTemplateId;
        snap.ZoneUUID = DataMgr.Instance.UserData.ZoneUUID;
    }

 
    private void RoleSnapRequest(string[] keys, Action<Exception, TLClientRoleSnap[]> act)
    {
        if (keys == null || keys.Length == 0)
        {
            return;
        }
        TLNetManage.Instance.Request<GetRoleSnapResponse>(new GetRoleSnapRequest { c2s_rolesID = keys }, (ex, rps) =>
        {
            var all = new TLClientRoleSnap[keys.Length];
            if (ex != null || !rps.IsSuccess)
            {
                act.Invoke(ex, all);
            }
            else
            {
                for (var i = 0; i < all.Length; i++)
                {
                    all[i] = rps.s2c_data[i];
                }
                act.Invoke(null, all);
            }
    
        }, new TLNetManage.PackExtData(false, true));
    }

    private ItemListener mBagListener;
    private ItemListener mEquipedListener;

    private ItemListener mVirtualListener;

    public event Action<ItemUpdateAction> OnBagUpdateAction;

    private void BagModuleOnInit()
    {
        SetAttribute(NotiFyStatus.COPPER, VirtualBag.Copper);
        SetAttribute(NotiFyStatus.DIAMOND, VirtualBag.Diamond);
        SetAttribute(NotiFyStatus.SILVER, VirtualBag.Silver);

        VirtualBag.SubscribCopper((u, reason) =>
        {
            SetAttribute(NotiFyStatus.COPPER, u, true);
        });

        VirtualBag.SubscribSilver((u, reason) =>
        {
            SetAttribute(NotiFyStatus.SILVER, u, true);
        });

        VirtualBag.SubscribDiamond((u, reason) =>
        {
            SetAttribute(NotiFyStatus.DIAMOND, u, true);
        });

        mBagListener = new ItemListener(Bag, false);
        mBagListener.OnItemUpdateAction += BagOnItemUpdateAction;
        mEquipedListener = new ItemListener(EquipBag, false);
        mEquipedListener.OnItemUpdateAction += EquipBagOnItemUpdateAction;

        mVirtualListener = new ItemListener(VirtualBag, false);
        mVirtualListener.OnItemUpdateAction += VirtualBagOnItemUpdateAction;

        mBagListener.Start(false, false);
        mEquipedListener.Start(false, false);
        mVirtualListener.Start(false, false);

        LuaSystem.Instance.DoFunc("GlobalHooks.BagInitOk");
        HudManager.Instance.SkillBar.InitMedicineSlot();
    }

    private void BagOnItemUpdateAction(ItemUpdateAction action)
    {
        if (OnBagUpdateAction != null)
            OnBagUpdateAction.Invoke(action);

        switch (action.Type)
        {
            case ItemUpdateAction.ActionType.Init:
                break;
            case ItemUpdateAction.ActionType.Add:

                string reason = action.Reason;

                //自动装备弹窗检测
                Dictionary<string, object> equipArgs = new Dictionary<string, object>();
                equipArgs.Add("index", action.Index);
                equipArgs.Add("reason", reason);
                EventManager.Fire("AddEquip", equipArgs);
                break;
            case ItemUpdateAction.ActionType.Remove:
                Dictionary<string, object> removeObj = new Dictionary<string, object>();
                removeObj.Add("index", action.Index);
                removeObj.Add("reason", action.Reason);
                EventManager.Fire("RemoveEquip", removeObj);
                break;
            case ItemUpdateAction.ActionType.UpdateCount:
                break;
            case ItemUpdateAction.ActionType.UpdateAttribute:
                var p = new Dictionary<string, object>
                {
                    {"Index", action.Index}
                };
                EventManager.Fire("Bag.UpdateAttribute", p);
                break;
            case ItemUpdateAction.ActionType.ChangeSize:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (action.Type == ItemUpdateAction.ActionType.Add || action.Type == ItemUpdateAction.ActionType.Remove || action.Type == ItemUpdateAction.ActionType.UpdateCount)
        {
            var templateSnap = action.TemplateSnap;
            var count = templateSnap.Count;
            if (action.Type == ItemUpdateAction.ActionType.Remove)
            {
                count = -count;
            }

            var p = new Dictionary<string, object>
            {
                {"Index", action.Index},
                {"Virtual", false},
                {"Count", count},
                {"TemplateID", templateSnap.TemplateID},
                {"Reason",  action.Reason}
            };
            EventManager.Fire("Event.Item.CountUpdate", p);

            if (count > 0)
            {
                if (action.Reason == "SwapItem")
                {
                    return;
                }
                if (action.Reason == "TurnTable")
                {
                    return;
                }
                
                if (action.Reason != "monsterDrop" && action.Reason != "guild_openGift")
                {
                    GameUtil.ShowGetItemTip(templateSnap.TemplateID, count);
                }
                PushGetItemMsg2SysChat(templateSnap.TemplateID, count);
            }
        }
        
    }

    private void EquipBagOnItemUpdateAction(ItemUpdateAction action)
    {
        switch (action.Type)
        {
            case ItemUpdateAction.ActionType.Init:
                break;
            case ItemUpdateAction.ActionType.Add:
                break;
            case ItemUpdateAction.ActionType.Remove:
                break;
            case ItemUpdateAction.ActionType.UpdateCount:
                break;
            case ItemUpdateAction.ActionType.UpdateAttribute:
                var p = new Dictionary<string, object>
                {
                    {"Index", action.Index}
                };
                EventManager.Fire("EquipBag.UpdateAttribute", p);
                break;
            case ItemUpdateAction.ActionType.ChangeSize:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (action.Type == ItemUpdateAction.ActionType.Add || action.Type == ItemUpdateAction.ActionType.Remove || action.Type == ItemUpdateAction.ActionType.UpdateCount)
        {
            var templateSnap = action.TemplateSnap;
            var count = templateSnap.Count;
            if (action.Type == ItemUpdateAction.ActionType.Remove)
            {
                count = -count;
            }
            var p = new Dictionary<string, object>
            {
                {"EquipPos", action.Index},
                {"TemplateID", templateSnap.TemplateID},
                {"Count", count}
            };
            EventManager.Fire("Event.Equip.CountUpdate", p);
        }
    }

    private void VirtualBagOnItemUpdateAction(ItemUpdateAction action)
    {
        switch (action.Type)
        {
            case ItemUpdateAction.ActionType.Init:
                break;
            case ItemUpdateAction.ActionType.Add:
                break;
            case ItemUpdateAction.ActionType.Remove:
                break;
            case ItemUpdateAction.ActionType.UpdateCount:
                break;
            case ItemUpdateAction.ActionType.UpdateAttribute:
                break;
            case ItemUpdateAction.ActionType.ChangeSize:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (action.Type == ItemUpdateAction.ActionType.Add || action.Type == ItemUpdateAction.ActionType.Remove || action.Type == ItemUpdateAction.ActionType.UpdateCount)
        {
            var templateSnap = action.TemplateSnap;
            var count = templateSnap.Count;
            if (action.Type == ItemUpdateAction.ActionType.Remove)
            {
                count = -count;
            }

            var p = new Dictionary<string, object>
            {
                {"Index", action.Index},
                {"Virtual", true},
                {"Count", count},
                {"TemplateID", templateSnap.TemplateID},
                {"Reason",  action.Reason}
            };
            EventManager.Fire("Event.Item.CountUpdate", p);

            if (count > 0)
            {
                if (action.Reason == "SwapItem")
                {
                    return;
                }
                if (action.Reason == "TurnTable")
                {
                    return;
                }
                
                if (action.Reason != "monsterDrop" && action.Reason != "guild_openGift")
                {
                    GameUtil.ShowGetItemTip(templateSnap.TemplateID, count);
                }
                PushGetItemMsg2SysChat(templateSnap.TemplateID, count);
            }
        }
    }

    public void SetTitleID(int titleId)
    {
        this.TitleID = titleId;
    }

    public void SetTitleExt(int titleId,string nameExt)
    {
        this.TitleID = titleId;
        if(!string.IsNullOrEmpty(nameExt))
        {
            this.TitleNameExt = nameExt;
        }
    }

    private void OnPlayerDynamicNotify(PlayerDynamicNotify msg)
    {
        int size = msg.s2c_data.Count;
        int status = 0;
        for (int i = 0; i < size; ++i)
        {
            PropertyStruct p = msg.s2c_data[i];
            object val;
            long int_val = 0;
            if (p.type == 1)
            {
                long.TryParse(p.value, out int_val);
                val = int_val;
            }
            else
            {
                val = p.value;
            }

          
            if (mKeyToAttrMap.ContainsKey(p.key))
            {
                status |= SetAttribute(mKeyToAttrMap[p.key], val);
            }
            
            if (p.key == "AddExp")
            {
                //战斗飘字
                //BattleNumberManager.Instance.ShowExpNum(new Vector2(Screen.width / 2, Screen.height / 2), int_val);

                int TemplateID = 4;
                GameUtil.ShowGetItemTip(TemplateID, int_val);
                PushGetItemMsg2SysChat(TemplateID, int_val);
            }
            else if (p.key== "OverflowAddExp")//溢出经验专用漂字
            {
                GameUtil.ShowOverFlowExpTips(int_val);
            }
            else if (p.key == "level")
            {
                var localUserData = OneGameSDK.Instance.GetUserData();
                localUserData.SetData(SDKAttName.ROLE_LEVEL, (int)int_val);
                localUserData.SetData(SDKAttName.VIP_LEVEL, VipLv);
                localUserData.SetData(SDKAttName.DATE_TYPE, RoleDateType.levelUp);
                OneGameSDK.Instance.UpdatePlayerInfo();
            }
        }
        Notify(status);
    }

    private void OnFunctionOpenNotify(ClientFunctionOpenNotify notify)
    {
        foreach (var p in notify.s2c_funList)
        {
            FuncOpen[p.Key] = p.Value;
        }
    }

    public TLAIActor GetActor()
    {
        if (GameSceneMgr.Instance.BattleScene != null)
        {
            TLAIActor actor = GameSceneMgr.Instance.BattleScene.GetActor() as TLAIActor;
            return actor;
        }
        return null;
    }


    //// x.y是坐标 z存了angle 给小地图用
    //public UnityEngine.Vector3 GetActorPos()
    //{
    //    var actor = GetActor();
    //    if (actor == null)
    //    {
    //        return UnityEngine.Vector3.one;
    //    }
    //    float direction = actor.Direction;
    //    float angle = 0;
    //    if (direction < 0)
    //    {
    //        angle = (float)Math.Abs((direction * 180 / Math.PI)) - 90;
    //    }
    //    else
    //    {
    //        angle = (float)((Math.PI - direction) * 180 / Math.PI) + 90;
    //    }
    //    return new UnityEngine.Vector3(actor.X, actor.Y, angle);
    //}


    public HashMap<int, TLAvatarInfo> GetAvatarList()
    {
        HashMap<int, TLAvatarInfo> model = null;
        TLAIActor actor = GetActor();
        if (actor != null)
        {
            model = actor.GetAvatarMap();   //(actor.ZActor.SyncInfo.VisibleInfo as PlayerVisibleDataB2C).AvatarMap;
        }
        return model;
    }

    public HashMap<int, TLAvatarInfo> GetAvatarListClone()
    {
        HashMap<int, TLAvatarInfo> aMap = new HashMap<int, TLAvatarInfo>();
        var avatarMap = GetAvatarList();
        if (avatarMap == null)
        {
            return aMap;
        }
        foreach (var mapinfo in avatarMap)
        {
            TLAvatarInfo aInfo = new TLAvatarInfo();
            aInfo.PartTag = (TLAvatarInfo.TLAvatar)mapinfo.Key;
            aInfo.FileName = mapinfo.Value.FileName;
            aMap.Add((int)aInfo.PartTag, aInfo);
        }
        return aMap;
    }


    public HashMap<int, TLAvatarInfo> GetNewAvatar(SLua.LuaTable avatar)
    {
        return GameUtil.GetNewAvatar(avatar, this.GetAvatarListClone());
    }

    public List<PlayMapUnitData> GetTeamInfoList()
    {
        if (DataMgr.Instance.TeamData.HasTeam == false || DataMgr.Instance.TeamData.AllMembers.Count == 0)
            return null;

        if (GameSceneMgr.Instance.BattleRun.Client == null)
            return null;
        List<PlayMapUnitData> list = new List<PlayMapUnitData>();
        foreach (var teamMember in DataMgr.Instance.TeamData.AllMembers)
        {
            if (teamMember.RoleID == DataMgr.Instance.UserData.RoleID)
            {
                continue;
            }

            if (!DataMgr.Instance.TeamData.IsSameScene(teamMember.RoleID))
            {
                continue;
            }

            var zu = GameSceneMgr.Instance.BattleRun.Client.GetPlayerUnitByUUID(teamMember.RoleID);
            if (zu != null)
            {
                PlayMapUnitData td = new PlayMapUnitData();
                td.isTeamMate = true;
                td.ID = zu.ObjectID;
                td.Name = zu.Name;
                td.X = zu.X;
                td.Y = zu.Y;
                list.Add(td);
            }
        }

        return list;
    }

    [DoNotToLua]
    public HashMap<uint, PlayMapUnitData> GetTeamInfoMap()
    {
        if (DataMgr.Instance.TeamData.HasTeam == false || DataMgr.Instance.TeamData.AllMembers.Count == 0)
            return null;

        if (GameSceneMgr.Instance.BattleRun.Client == null)
            return null;
        HashMap<uint, PlayMapUnitData> teamData = new HashMap<uint, PlayMapUnitData>();
        foreach (var teamMember in DataMgr.Instance.TeamData.AllMembers)
        {
            if (teamMember.RoleID == DataMgr.Instance.UserData.RoleID)
            {
                continue;
            }

            var zu = GameSceneMgr.Instance.BattleRun.Client.GetPlayerUnitByUUID(teamMember.RoleID);
            if (zu != null)
            {
                PlayMapUnitData td = new PlayMapUnitData();
                td.isTeamMate = true;
                td.ID = zu.ObjectID;
                td.Name = zu.Name;
                td.X = zu.X;
                td.Y = zu.Y;
                teamData.Add(zu.ObjectID, td);
            }
        }

        return teamData;
    }



    public void Clear(bool reLogin, bool reConnect)
    {
        if (reLogin)
        {
            mAttributes.Clear();
            mObservers.Clear();
            mLuaObservers.Clear();
            mRadarDatas.Clear();
            if (mBagListener != null)
            {
                mBagListener.Dispose();
            }
            if (mEquipedListener != null)
            {
                mEquipedListener.Dispose();
            }
            if (mVirtualListener != null)
            {
                mVirtualListener.Dispose();
            }
            mBagListener = null;
            mEquipedListener = null;
            mVirtualListener = null;
            OnBagUpdateAction = null;
            OnGameOptionDataChange = null;
        }
        if (reConnect || reLogin)
        {
            LastActorMoveAI = null;
            LastMapTouchMoveAI = null;
            LastSceneNextlink = null;
            LastMoveEndAction = null;//跨地图寻路
        }
    }
    //删除雷达信息
    public bool RemoveRadarData(string radarkey)
    {
        return mRadarDatas.Remove(radarkey);
    }
    //添加雷达信息
    public bool AddRadarData(string radarkey, TLAIActor.RadarData radar)
    {
        return mRadarDatas.TryAddOrUpdate(radarkey, radar);
    }

    public object GetAttribute(NotiFyStatus s)
    {
        if (mAttributes.ContainsKey((long)s))
            return mAttributes[(long)s];
        else
            return null;
    }

    public string StatusToKey(NotiFyStatus s)
    {
        foreach (var item in mKeyToAttrMap)
        {
            if (item.Value == s)
            {
                return item.Key;
            }
        }
        return null;
    }

    public NotiFyStatus Key2Status(string key)
    {
        NotiFyStatus s = NotiFyStatus.NULL;
        mKeyToAttrMap.TryGetValue(key, out s);
        return s;
    }

    public object GetAttribute(string key)
    {
        NotiFyStatus s;
        if (mKeyToAttrMap.TryGetValue(key, out s))
        {
            return GetAttribute(s);
        }
        else
        {
            return null;
        }
    }

    public int TryGetIntAttribute(NotiFyStatus s, int defaultValue = 0)
    {
        int value = defaultValue;
        object obj = GetAttribute(s);
        if (obj != null)
        {
            value = Convert.ToInt32(obj);
        }
        return value;
    }

    public long TryGetLongAttribute(NotiFyStatus s, int defaultValue = 0)
    {
        long value = defaultValue;
        object obj = GetAttribute(s);
        if (obj != null)
        {
            value = Convert.ToInt64(obj);
        }
        return value;
    }

    public int SetAttribute(NotiFyStatus s, object val, bool notify = false)
    {
        int notify_key = (int)s;
        mAttributes[notify_key] = val;
        if (notify)
            Notify(notify_key);

        return (int)(s);
    }

    public bool ContainsKey(NotiFyStatus status, NotiFyStatus key)
    {
        long sl = (long)status;
        long kl = (long)key;
        if ((sl & kl) != 0)
        {
            return true;
        }
        return false;
    }

    [DoNotToLua]
    public void RefreshRoleProp(List<TLPropObject> objs)
    {
        LastUpdate.Clear();
        for (int i = 0; i < objs.Count; ++i)
        {
            TLPropObject obj = objs[i];
            int propType = (int)obj.Prop;
            int valueType = (int)obj.Type;
            int value = obj.Value;
            mRoleProp[propType] = obj;
            LastUpdate.Add(propType);
        }
        SetAttribute(UserData.NotiFyStatus.PROP, LastUpdate);
        Notify((int)NotiFyStatus.PROP);
    }

    public bool TryGetRoleProp(int propType, out int valueType, out int value)
    {
        TLPropObject obj;
        if (mRoleProp.TryGetValue(propType, out obj))
        {
            valueType = (int)obj.Type;
            value = obj.Value;
            return true;
        }
        else
        {
            valueType = 0;
            value = 0;
        }
        return false;
    }

    [DoNotToLua]
    public void AttachObserver(IObserver<UserData> ob)
    {
        mObservers.Add(ob);
    }

    [DoNotToLua]
    public void DetachObserver(IObserver<UserData> ob)
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

    public void Notify(int status = (int)NotiFyStatus.ALL)
    {
        foreach (var ob in mObservers)
        {
            ob.Notify(status, this);
        }

        foreach (var ob in mLuaObservers)
        {
            ob.Value.invoke("Notify", new object[] { (NotiFyStatus)status, this, ob.Value });
        }
    }

    private int GetItemIDByKey(string key)
    {
        var obj = GameUtil.GetDBData2("Item", "{item_key ='" + key + "'}")[0]["id"];
        if (obj != null)
        {
            return Convert.ToInt32(obj);
        }
        return 0;
    }

    public void SetFreeData(string key, string value)
    {

        if (!GameGlobal.Instance.netMode)
        {
            return;
        }
        ClientSetModifyDataRequest request = new ClientSetModifyDataRequest();
        request.c2s_key = key;
        request.c2s_value = value;
        TLNetManage.Instance.Request<ClientSetModifyDataResponse>(request, (err, rsp) =>
        {
            if (Response.CheckSuccess(rsp))
            {
                if (RoleFreeData.ContainsKey(key))
                {
                    RoleFreeData[key] = value;
                }
                else
                {
                    this.RoleFreeData.TryAdd(key, value);
                }

            }
        });
    }

    public string GetFreeData(string key)
    {
        if (!GameGlobal.Instance.netMode)
        {
            return null;
        }
        string value = string.Empty;
        if (RoleFreeData.TryGetValue(key, out value))
        {
            return value;
        }
        return null;
    }

    private int mountDistance = 0;
    public int GetMountDistance()
    {
        if (mountDistance == 0)
        {
            if (GameGlobal.Instance.netMode)
            {
                var data = GameUtil.GetDBData2("GameConfig", "{id='mount_mindistance'}")[0]["paramvalue"];
                int.TryParse(data.ToString(), out mountDistance);
            }
            else
            {
                mountDistance = 99999;
            }
        }

        return mountDistance;
    }

    public ItemData GetItem(int bagIndex)
    {
        var d = mBagListener.GetItemData(bagIndex);
        return d;
    }

    public ulong GetItemCountByMatch(Predicate<ItemData> handle)
    {
        if (Bag == null)
        {
            return 0;
        }
        return Bag.CountItemAs<ItemData>(handle);

    }

    public bool IsFuncOpen(string key)
    {
        byte ret;
        if (FuncOpen.TryGetValue(key, out ret))
        {
            return ret > 0;
        }
        return false;
    }


    public void PushGetItemMsg2SysChat(int TemplateID, long Count)
    {
        Dictionary<object, object> dict = new Dictionary<object, object>();
        dict.Add("TemplateID", TemplateID);
        dict.Add("Count", Count);
        EventManager.Fire("Event.SysChat.GetItem", dict);
    }
}
