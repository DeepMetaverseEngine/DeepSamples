using DeepCore;
using DeepCore.IO;
using DeepMMO.Attributes;
using DeepMMO.Data;
using DeepMMO.Protocol;
using DeepMMO.Protocol.Client;
using System;
using System.Collections.Generic;
using TLProtocol.Data;

namespace TLProtocol.Protocol.Client
{

    /// <summary>
    /// 好友列表
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 1)]
    public class ClientGetFriendListRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    ///  好友列表
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 2)]
    public class ClientGetFriendListResponse : Response, ILogicProtocol
    {
        public FriendsSnapData s2c_data;
    }

    /// <summary>
    /// 添加好友请求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 3)]
    public class ClientApplyFriendRequest : Request, ILogicProtocol
    {
        public string roleID;
    }

    /// <summary>
    ///  添加好友请求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 4)]
    public class ClientApplyFriendResponse : Response, ILogicProtocol
    {
        [MessageCode("玩家不存在")] public const int ERR_FRIENDAPPLY_PLAYER_NOT_EXIST = 501;
        [MessageCode("对方在你黑名单列表中")] public const int ERR_FRIENDAPPLY_IN_SELF_BLICK = 502;
        [MessageCode("你的好友已达上限")] public const int ERR_FRIENDAPPLY_SELF_FRIEND_MAX = 503;
        [MessageCode("你在对方的黑名单列表中")] public const int ERR_FRIENDAPPLY_IN_THEIR_BLACK = 504;
        [MessageCode("对方好友已达上限")] public const int ERR_FRIENDAPPLY_THEIR_FRIEND_MAX = 505;
        [MessageCode("已在申请列表中")] public const int ERR_FRIENDAPPLY_ALREADY_APPLY = 506;
        [MessageCode("申请列表已满")] public const int ERR_FRIENDAPPLY_APPLY_MAX = 507;
        [MessageCode("对方已经是好友了")] public const int ERR_FRIENDAPPLY_ALREADY_FRIEND = 508;
        [MessageCode("该玩家正在闭关修炼，此时不便结识其他仙友")] public const int ERR_AUTO_REFUSE_APPLY = 509;
    }

    /// <summary>
    /// 删除好友
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 5)]
    public class ClientRemoveFriendRequest : Request, ILogicProtocol
    {
        public string roleID;
    }

    /// <summary>
    ///  删除好友
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 6)]
    public class ClientRemoveFriendResponse : Response, ILogicProtocol
    {
        [MessageCode("此玩家不是好友")] public const int ERR_FRIENDREMOVE_NOT_EXIST = 501;
        [MessageCode("玩家不存在")] public const int ERR_FRIENDREMOVE_PLAYER_NOT_EXIST = 502;
    }

    /// <summary>
    /// 申请列表
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 7)]
    public class ClientApplyListRequest : Request, ILogicProtocol
    {
        
    }

    /// <summary>
    ///  申请列表
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 8)]
    public class ClientApplyListResponse : Response, ILogicProtocol
    {
        public ApplySnapData s2c_data;
    }

    /// <summary>
    /// 回复好友申请请求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 9)]
    public class ClientReplyFriendRequest : Request, ILogicProtocol
    {
        public string roleID;  //空字符串代表一键操作
        public int type;  //1：同意，0：拒绝
    }

    /// <summary>
    ///  回复好友申请请求
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 10)]
    public class ClientReplyFriendResponse : Response, ILogicProtocol
    {
        [MessageCode("玩家不存在")] public const int ERR_FRIENDREPLY_PLAYER_NOT_EXIST = 501;
        [MessageCode("对方在你黑名单列表中")] public const int ERR_FRIENDREPLY_IN_SELF_BLICK = 502;
        [MessageCode("你的好友已达上限")] public const int ERR_FRIENDREPLY_SELF_FRIEND_MAX = 503;
        [MessageCode("你在{0}的黑名单列表中")] public const int ERR_FRIENDREPLY_IN_THEIR_BLACK = 504;
        [MessageCode("{0}的好友数量已达上限")] public const int ERR_FRIENDREPLY_THEIR_FRIEND_MAX = 505;
        [MessageCode("不在申请列表中")] public const int ERR_FRIENDREPLY_NOT_APPLY = 506;
        [MessageCode("你和{0}已经是好友了")] public const int ERR_FRIENDREPLY_ALREADY_FRIEND = 507;
        [MessageCode("无申请玩家")] public const int ERR_FRIENDREPLY_AEPLY_EMPTY = 508;
    }

    /// <summary>
    /// 黑名单列表
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 11)]
    public class ClientGetBlackListRequest : Request, ILogicProtocol
    {
        
    }

    /// <summary>
    ///  黑名单列表
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 12)]
    public class ClientGetBlackListResponse : Response, ILogicProtocol
    {
        public BlackSnapData s2c_data;
    }

    /// <summary>
    /// 添加黑名单请求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 13)]
    public class ClientAddBlackListRequest : Request, ILogicProtocol
    {
        public string roleID;
    }

    /// <summary>
    ///  添加黑名单请求
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 14)]
    public class ClientAddBlackListResponse : Response, ILogicProtocol
    {
        [MessageCode("对方已经在黑名单里了")] public const int ERR_ADDBLACK_ALREADY_BLACK = 501;
        [MessageCode("黑名单已达上限")] public const int ERR_ADDBLACK_SELF_BLACK_MAX = 502;
        [MessageCode("玩家不存在")] public const int ERR_ADDBLACK_PLAYER_NOT_EXIST = 503;
    }

    /// <summary>
    /// 删除黑名单
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 15)]
    public class ClientRemoveBlackListRequest : Request, ILogicProtocol
    {
        public string roleID;
    }

    /// <summary>
    ///  删除黑名单
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 16)]
    public class ClientRemoveBlackListResponse : Response, ILogicProtocol
    {
        [MessageCode("此玩家不在黑名单中")] public const int ERR_BLACKREMOVE_NOT_EXIST = 501;
        [MessageCode("无黑名单玩家")] public const int ERR_BLACKREMOVE_EMPTY = 502;
        [MessageCode("玩家不存在")] public const int ERR_BLACKREMOVE_PLAYER_NOT_EXIST = 503;
    }

    /// <summary>
    /// 仇人列表
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 17)]
    public class ClientGetEnemyListRequest : Request, ILogicProtocol
    {

    }

    /// <summary>
    ///  仇人列表
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 18)]
    public class ClientGetEnemyListResponse : Response, ILogicProtocol
    {
        public EnemySnapData s2c_data;
    }

    /// <summary>
    /// 添加仇人请求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 19)]
    public class ClientAddEnemyRequest : Request, ILogicProtocol
    {
        public string roleID;
    }

    /// <summary>
    ///  添加仇人返回
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 20)]
    public class ClientAddEnemyResponse : Response, ILogicProtocol
    {
        [MessageCode("对方已经在仇人列表里了")] public const int ERR_ADDENEMY_ALREADY_ENEMY = 501;
    }

    /// <summary>
    /// 删除仇人
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 21)]
    public class ClientRemoveEnemyRequest : Request, ILogicProtocol
    {
        public string roleID;
    }

    /// <summary>
    ///  删除仇人
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 22)]
    public class ClientRemoveEnemyResponse : Response, ILogicProtocol
    {
        [MessageCode("此玩家不是仇人")] public const int ERR_ENEMYREMOVE_NOT_EXIST = 501;
        [MessageCode("请先取消深仇")] public const int ERR_ENEMYREMOVE_DEEPHATRED = 502;
        [MessageCode("玩家不存在")] public const int ERR_ENEMYREMOVE_PLAYER_NOT_EXIST = 503;
    }

    /// <summary>
    /// 设置深仇
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 23)]
    public class ClientDeepHatredRequest : Request, ILogicProtocol
    {
        public string roleID;
        public bool deepHatred;
    }

    /// <summary>
    ///  设置深仇
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 24)]
    public class ClientDeepHatredResponse : Response, ILogicProtocol
    {
        [MessageCode("对方不是仇人")] public const int ERR_DEEPHATRED_NOT_ENEMY = 501;
        [MessageCode("对方已经是深仇")] public const int ERR_DEEPHATRED_ALREADY_DEEP = 502;
        [MessageCode("深仇已达上限")] public const int ERR_DEEPHATRED_SELF_ENEMY_MAX = 503;
        [MessageCode("玩家不存在")] public const int ERR_DEEPHATRED_PLAYER_NOT_EXIST = 504;
    }

    /// <summary>
    /// 查找好友请求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 25)]
    public class ClientQueryFriendRequest : Request, ILogicProtocol
    {
        public string name;
    }

    /// <summary>
    ///  查找好友请求
    /// </summary>
    [MessageType(Constants.TL_SHOP_START + 26)]
    public class ClientQueryFriendResponse : Response, ILogicProtocol
    {
        [MessageCode("未找到该玩家，或玩家不在线")] public const int ERR_QUERYFRIEND_NOT_FOUND = 501;
        public PlayerSnapData s2c_data;
    }
    
    [MessageType(Constants.TL_SOCIAL_START + 27)]
    public class ClientPlayerBasicInfoRequest : Request, ILogicProtocol
    {
        public string roleID;
    }

    [MessageType(Constants.TL_SOCIAL_START + 28)]
    public class ClientPlayerBasicInfoResponse : Response, ILogicProtocol
    {
        public int level;
        public int pro;
        public int gender;
    }

    /// <summary>
    /// 获取社交亲密度数据请求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 29)]
    public class ClientGetRelationDataRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_SOCIAL_START + 30)]
    public class ClientGetRelationDataResponse : Response, ILogicProtocol
    {
        public RelationSnapData s2c_data;
    }

    /// <summary>
    /// 赠送礼物请求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 31)]
    public class ClientRelationUpRequest : Request, ILogicProtocol
    {
        public string roleID;
        public int itemTemplateId;
        public int itemNum;
    }

    [MessageType(Constants.TL_SOCIAL_START + 32)]
    public class ClientRelationUpResponse : Response, ILogicProtocol
    {
        [MessageCode("玩家不存在")] public const int ERR_RELATIONUP_PLAYER_NOT_EXIST = 501;
        [MessageCode("参数错误，礼物不存在")] public const int ERR_RELATIONUP_ITEM_NOT_FOUND = 502;
        [MessageCode("礼物数量不足")] public const int ERR_RELATIONUP_COST_NO_ENOUGH = 503;
        [MessageCode("对方还不是好友，请先添加为好友")] public const int ERR_RELATIONUP_NOT_FRIEND = 504;
        public int relationLv;
        public int relationExp;
    }

    /// <summary>
    /// 社交多人亲密度数据请求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 33)]
    public class ClientGetRelationDataMutiRequest : Request, ILogicProtocol
    {
        public List<string> players;
    }

    [MessageType(Constants.TL_SOCIAL_START + 34)]
    public class ClientGetRelationDataMutiResponse : Response, ILogicProtocol
    {
        public HashMap<string, int> data;
    }

    /// <summary>
    /// 獲取婚禮預約信息
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 35)]
    public class ClientGetReservationInfoRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_SOCIAL_START + 36)]
    public class ClientGetReservationInfoResponse : Response, ILogicProtocol
    {
        public DateTime today;
        public List<ReservationSnapData> data;
        public bool expired;
    }

    /// <summary>
    /// 申請結婚
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 37)]
    public class ClientHoldingWeddingRequest : Request, ILogicProtocol
    {
        public string spouseId;
        public int weddingType;
        public DateTime date;
        public int time;
    }

    [MessageType(Constants.TL_SOCIAL_START + 38)]
    public class ClientHoldingWeddingResponse : Response, ILogicProtocol
    {
        [MessageCode("双方的亲密度必须达到{0}级才能结婚")] public const int ERR_MARRY_RELATION_NOT_ENOUGH = 501;
        [MessageCode("您已经有伴侣了，无法再次结婚")] public const int ERR_MARRY_SELF_ALREADY_MARRIAGE = 502;
        [MessageCode("对方已经有伴侣了，无法再次结婚")] public const int ERR_MARRY_SPOUSE_ALREADY_MARRIAGE = 503;
        [MessageCode("结婚双方必须在同一队伍中")] public const int ERR_MARRY_MUST_IN_TEAM = 504;
        [MessageCode("只有队长才可以发起求婚")] public const int ERR_MARRY_MUST_TEAM_LEADER = 505;
        [MessageCode("结婚双方必须同时在线")] public const int ERR_MARRY_MUST_ONLINE = 506;
        [MessageCode("结婚双方必须在同一场景")] public const int ERR_MARRY_MUST_IN_SAME_MAP = 507;
        [MessageCode("参数错误")] public const int ERR_MARRY_ARG_ERROR = 508;
        [MessageCode("货币不足")] public const int ERR_MARRY_ITEM_NOT_ENOUGH = 509;
        [MessageCode("此日期已预约满，请选择其他日期")] public const int ERR_MARRY_DATE_ERR = 510;
        [MessageCode("您已邀请过对方")] public const int ERR_MARRY_ALREADY_INVITE = 511;
        [MessageCode("双方的等级必须达到{0}级才能结婚")] public const int ERR_MARRY_LEVEL_NOT_ENOUGH = 512;
    }

    /// <summary>
    /// 發送請帖
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 39)]
    public class ClientSendInvitationRequest : Request, ILogicProtocol
    {
        public int slotIndex;
        public HashMap<string, string> friendIds;
    }

    [MessageType(Constants.TL_SOCIAL_START + 40)]
    public class ClientSendInvitationResponse : Response, ILogicProtocol
    {
        [MessageCode("您还没有结婚")] public const int ERR_MARRY_NO_MARRIAGE = 501;
        [MessageCode("只有豪华婚礼才能解锁夫妻仓库")] public const int ERR_MARRY_NO_ADVANCE_WEDDING = 502;
        [MessageCode("发送请帖必须选择好友")] public const int ERR_MARRY_SEND_INVITATION_MUST_FRIEND = 503;
        [MessageCode("请帖不存在")] public const int ERR_MARRY_WEDDING_CARD_NOT_FOUND = 504;
        [MessageCode("请帖已過期")] public const int ERR_MARRY_WEDDING_CARD_EXPIRED = 505;
        [MessageCode("发送好友数量已达上限")] public const int ERR_MARRY_SEND_LIMIT = 506;
    }

    /// <summary>
    /// 夫妻倉庫請求
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 41)]
    public class ClientGetCoupleWarehouseDataRequest : Request, ILogicProtocol
    {
        
    }

    [MessageType(Constants.TL_SOCIAL_START + 42)]
    public class ClientGetCoupleWarehouseDataResponse : Response, ILogicProtocol
    {
        [MessageCode("您还没有结婚")] public const int ERR_MARRY_NO_MARRIAGE = 501;
        [MessageCode("只有豪华婚礼才能解锁夫妻仓库")] public const int ERR_MARRY_NO_ADVANCE_WEDDING = 502;
        public HashMap<int, EntityItemData> selfItems;
        public HashMap<int, EntityItemData> mateItems;
    }

    /// <summary>
    /// 夫妻倉庫存入
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 43)]
    public class ClientCoupleWarehousePutOnRequest : Request, ILogicProtocol
    {
        public int slotIndex;
        public int num;
    }

    [MessageType(Constants.TL_SOCIAL_START + 44)]
    public class ClientCoupleWarehousePutOnResponse : Response, ILogicProtocol
    {
        [MessageCode("您还没有结婚")] public const int ERR_MARRY_NO_MARRIAGE = 501;
        [MessageCode("只有豪华婚礼才能解锁夫妻仓库")] public const int ERR_MARRY_NO_ADVANCE_WEDDING = 502;
        [MessageCode("道具不存在")] public const int CODE_ERROR_MARRY_ITEM_NOT_EXSIT = 503;
        [MessageCode("该道具无法存入夫妻仓库")] public const int CODE_ERROR_MARRY_ITEM_CAN_NOT_TRADE = 504;
        [MessageCode("道具数量不足")] public const int CODE_ERROR_MARRY_ITEM_NOT_ENOUGH = 505;
        [MessageCode("仓库已满")] public const int CODE_ERROR_MARRY_WAREHOUSE_FULL = 506;
    }

    /// <summary>
    /// 夫妻倉庫取出
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 45)]
    public class ClientCoupleWarehousePutOffRequest : Request, ILogicProtocol
    {
        //0 自己 1 對方
        public int warehouseType;
        public int slotIndex;
    }

    [MessageType(Constants.TL_SOCIAL_START + 46)]
    public class ClientCoupleWarehousePutOffResponse : Response, ILogicProtocol
    {
        [MessageCode("您还没有结婚")] public const int ERR_MARRY_NO_MARRIAGE = 501;
        [MessageCode("只有豪华婚礼才能解锁夫妻仓库")] public const int ERR_MARRY_NO_ADVANCE_WEDDING = 502;
        [MessageCode("道具不存在")] public const int ERR_MARRY_WAREHOUSE_ITEM_NOT_FOUND = 503;
    }

    /// <summary>
    /// 離婚
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 47)]
    public class ClientDivorceRequest : Request, ILogicProtocol
    {
        public int type;
    }

    [MessageType(Constants.TL_SOCIAL_START + 48)]
    public class ClientDivorceResponse : Response, ILogicProtocol
    {
        [MessageCode("您还没有结婚")] public const int ERR_MARRY_NO_MARRIAGE = 501;
        [MessageCode("货币不足")] public const int ERR_MARRY_ITEM_NOT_ENOUGH = 502;
        [MessageCode("您已向对方发起过申请，请耐心等待")] public const int ERR_MARRY_ALREADY_INVITE = 503;
        [MessageCode("对方拒绝了您的离婚请求")] public const int ERR_MARRY_DIVORCE_REFUSE = 504;
        [MessageCode("您和{0}離婚成功，江湖不見")] public const int ERR_MARRY_DIVORCE_SUCCESS = 505;
    }

    /// <summary>
    /// 舉行婚禮
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 49)]
    public class ClientWeddingStartRequest : Request, ILogicProtocol
    {

    }

    [MessageType(Constants.TL_SOCIAL_START + 50)]
    public class ClientWeddingStartResponse : Response, ILogicProtocol
    {
        [MessageCode("您还没有结婚")] public const int ERR_MARRY_NO_MARRIAGE = 501;
        [MessageCode("您已经举办过婚礼")] public const int ERR_MARRY_ALREADY_WEDDING = 502;
        [MessageCode("当前不在您的婚礼举办时间")] public const int ERR_MARRY_TIME_ERR = 503;
        [MessageCode("结婚双方必须在同一队伍中")] public const int ERR_MARRY_MUST_IN_TEAM = 504;
        [MessageCode("只有队长才可以开始婚礼仪式")] public const int ERR_MARRY_MUST_TEAM_LEADER = 505;
        [MessageCode("结婚双方必须同时在线")] public const int ERR_MARRY_MUST_ONLINE = 506;
        [MessageCode("结婚双方必须在同一场景")] public const int ERR_MARRY_MUST_IN_SAME_MAP = 507;
        [MessageCode("预约日期不在当前范围")] public const int ERR_MARRY_NO_DATE = 508;
    }

    /// <summary>
    /// 重新預約
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 51)]
    public class ClientWeddingReservationRequest : Request, ILogicProtocol
    {
        public string spouseId;
        public int weddingType;
        public DateTime date;
        public int time;
    }

    [MessageType(Constants.TL_SOCIAL_START + 52)]
    public class ClientWeddingReservationResponse : Response, ILogicProtocol
    {
        [MessageCode("您还没有结婚")] public const int ERR_MARRY_NO_MARRIAGE = 501;
        [MessageCode("您已经预约过婚礼，无法再次预约")] public const int ERR_MARRY_ALREADY_RESERVATION = 502;
        [MessageCode("您已经举办过婚礼了，无法再次预约")] public const int ERR_MARRY_SELF_ALREADY_MARRIAGE = 503;
        [MessageCode("结婚双方必须在同一队伍中")] public const int ERR_MARRY_MUST_IN_TEAM = 504;
        [MessageCode("只有队长才可以发起预约")] public const int ERR_MARRY_MUST_TEAM_LEADER = 505;
        [MessageCode("夫妻双方必须同时在线")] public const int ERR_MARRY_MUST_ONLINE = 506;
        [MessageCode("夫妻双方必须在同一场景")] public const int ERR_MARRY_MUST_IN_SAME_MAP = 507;
        [MessageCode("参数错误")] public const int ERR_MARRY_ARG_ERROR = 508;
        [MessageCode("只有豪华结婚才能预约婚礼")] public const int ERR_MARRY_NO_ADVANCE_WEDDING = 509;
        [MessageCode("此日期已预约满，请选择其他日期")] public const int ERR_MARRY_DATE_ERR = 510;
        [MessageCode("您已邀请过对方")] public const int ERR_MARRY_ALREADY_INVITE = 511;
        [MessageCode("重新预约成功")] public const int ERR_MARRY_WEDDING_RESERVATION_SUCCESS = 512;
    }

    /// <summary>
    /// 檢查請柬
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 53)]
    public class ClientCheckInvitationRequest : Request, ILogicProtocol
    {
        public int slotIndex;
    }

    [MessageType(Constants.TL_SOCIAL_START + 54)]
    public class ClientCheckInvitationResponse : Response, ILogicProtocol
    {
        [MessageCode("请帖不存在")] public const int ERR_MARRY_WEDDING_CARD_NOT_FOUND = 501;
        [MessageCode("请帖已過期")] public const int ERR_MARRY_WEDDING_CARD_EXPIRED = 502;
    }

    /// <summary>
    ///  好友状态推送
    /// </summary>
    [MessageType(Constants.TL_SOCIAL_START + 100)]
    public class ClientFriendOnlineNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public string friendId;
        public int onlineState;
    }

    /// <summary>
    /// 好友申请推送
    /// </summary>
    [MessageType(TLConstants.TL_MAIL_START + 101)]
    public class TLClientFriendApplyNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public int applyCount;
    }

    /// <summary>
    /// 亲密度提升播放特效推送
    /// </summary>
    [MessageType(TLConstants.TL_MAIL_START + 102)]
    public class TLClientRelationUpNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public SocialGiftRecordSnap record;
    }

    /// <summary>
    /// 组队亲密度提升推送
    /// </summary>
    [MessageType(TLConstants.TL_MAIL_START + 103)]
    public class TLClientTeamRelationUpNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public HashMap<string, string> players;
        public int addRelation;
    }

    /// <summary>
    /// 結婚成功推送
    /// </summary>
    [MessageType(TLConstants.TL_MAIL_START + 104)]
    public class TLClientMarrySuccessNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public HashMap<string, string> data;
    }

    /// <summary>
    /// 當天婚禮信息推送
    /// </summary>
    [MessageType(TLConstants.TL_MAIL_START + 105)]
    public class TLClientWeddingInfoNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public HashMap<int, string> info;
    }

    /// <summary>
    /// 結婚場景切換
    /// </summary>
    [MessageType(TLConstants.TL_MAIL_START + 106)]
    public class TLClientWeddingSceneChangeNotify : Notify, ILogicProtocol, INetProtocolS2C
    {
        public bool needChangeScene;
    }

}


