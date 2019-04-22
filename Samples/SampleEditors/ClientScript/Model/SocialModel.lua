local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'

local blackList = {}

local function OnShowInteractiveMenu( evtName, params )
    local ui = GlobalHooks.UI.FindUI("InteractiveMenuUI")
    if not ui then 
        ui = GlobalHooks.UI.OpenUI("InteractiveMenuUI")
    end
    ui:SetData(params)
end


--好友列表
function _M.RequestClientGetFriendList(cb)
    -- print('---------RequestClientGetFriendList----------')
    local request = {}
    Protocol.RequestHandler.ClientGetFriendListRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--好友申请请求
function _M.RequestClientApplyFriend(id, cb)
    -- print('---------RequestClientApplyFriend----------')
    local request = {roleID = id}
    Protocol.RequestHandler.ClientApplyFriendRequest(request, function(rsp)
        -- print_r(rsp)
        GameAlertManager.Instance:ShowNotify(Util.GetText('friend_add_wait'))
        if cb then
          cb(rsp)
        end
    end)
end

--删除好友
function _M.RequestClientRemoveFriend(id, cb)
    -- print('---------RequestClientRemoveFriend----------')
    local request = {roleID = id}
    Protocol.RequestHandler.ClientRemoveFriendRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--申请列表
function _M.RequestClientApplyList(cb)
    -- print('---------RequestClientApplyList----------')
    local request = {}
    Protocol.RequestHandler.ClientApplyListRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--回复添加好友请求
function _M.RequestClientReplyFriend(id, isAgree, cb)
    -- print('---------RequestClientReplyFriend----------')
    local request = {roleID = id, type = isAgree and 1 or 0}
    Protocol.RequestHandler.ClientReplyFriendRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--黑名单列表
function _M.RequestClientGetBlackList(cb)
    -- print('---------RequestClientGetBlackList----------')
    local request = {}
    Protocol.RequestHandler.ClientGetBlackListRequest(request, function(rsp)
        -- print_r(rsp)
        blackList = {}
        local listData = rsp.s2c_data.blackList
        for i = 1, #listData do
            blackList[listData[i].roleId] = true
        end
        if cb then
          cb(rsp)
        end
    end)
end

--添加黑名单请求
function _M.RequestClientAddBlackList(id, cb)
    -- print('---------RequestClientAddBlackList----------')
    local request = {roleID = id}
    Protocol.RequestHandler.ClientAddBlackListRequest(request, function(rsp)
        -- print_r(rsp)
        blackList[id] = true
        if cb then
          cb(rsp)
        end
    end)
end

--删除黑名单
function _M.RequestClientRemoveBlackList(id, cb)
    -- print('---------RequestClientRemoveBlackList----------')
    local request = {roleID = id}
    Protocol.RequestHandler.ClientRemoveBlackListRequest(request, function(rsp)
        -- print_r(rsp)
        blackList[id] = nil
        if cb then
          cb(rsp)
        end
    end)
end

--仇人列表
function _M.RequestClientGetEnemyList(cb)
    -- print('---------RequestClientGetEnemyList----------')
    local request = {}
    Protocol.RequestHandler.ClientGetEnemyListRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--删除仇人
function _M.RequestClientRemoveEnemy(id, cb)
    -- print('---------RequestClientRemoveEnemy----------')
    local request = {roleID = id}
    Protocol.RequestHandler.ClientRemoveEnemyRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--设置深仇
function _M.RequestClientSetDeepHatred(id, deep, cb)
    -- print('---------RequestClientSetDeepHatred----------')
    local request = {roleID = id, deepHatred = deep}
    Protocol.RequestHandler.ClientDeepHatredRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--查找好友请求
function _M.RequestClientQueryFriend(sc_name, cb)
    -- print('---------RequestClientQueryFriend----------')
    local request = {name = sc_name}
    Protocol.RequestHandler.ClientQueryFriendRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--亲密度列表
function _M.RequestClientGetRelationData(cb)
    -- print('---------RequestClientGetRelationData----------')
    local request = {}
    Protocol.RequestHandler.ClientGetRelationDataRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--赠送请求
function _M.RequestClientRelationUp(roleId, itemId, itemNum, cb)
    -- print('---------RequestClientRelationUp----------')
    local request = { roleID = roleId, itemTemplateId = itemId, itemNum = itemNum }
    Protocol.RequestHandler.ClientRelationUpRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--多人亲密度请求
function _M.RequestClientGetRelationDataMuti(players, cb)
    -- print('---------RequestClientGetRelationDataMuti----------')
    local request = { players = players }
    Protocol.RequestHandler.ClientGetRelationDataMutiRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end



--获取相册地址
function _M.GetPhotoAddress(cb)
    local msg = { c2s_roleId = 0 }
    Protocol.RequestHandler.ClientPhotoAddressRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--上传照片请求
function _M.UploadPhotoRequest(index, cb)
    local msg = { c2s_Index = index }
    Protocol.RequestHandler.ClientPhotoUploadRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--获取相册信息
function _M.GetPhotoInfo(uuid,cb)
    local msg = { c2s_roleId = uuid }
    Protocol.RequestHandler.ClientPhotoInfoRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--下载自定义头像
function _M.SetHeadIcon(uuid,photoname,cb)
    CacheImage.Instance:DownLoad(uuid,photoname,function(params)
        if params[1] and cb then
            cb(params[3])
        end
    end)
end

--删除照片请求
function _M.DeletePhotoRequest(index,cb)
    local msg = { c2s_Index = index }
    Protocol.RequestHandler.ClientPhotoDeleteRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--上传相册信息设置
function _M.UpdateSoicalRequest(socialdata,cb)
    local msg = { c2s_socialData = socialdata }
    Protocol.RequestHandler.ClientUpdateSocialRequest(msg, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--獲取婚禮預約信息
function _M.RequestClientGetReservationInfo(cb)
    -- print('---------RequestClientGetReservationInfo----------')
    local request = { }
    Protocol.RequestHandler.ClientGetReservationInfoRequest(request, function(rsp)
        print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--請求結婚
function _M.RequestClientHoldingWedding(spouseId, weddingType, date, time, cb)
    -- print('---------RequestClientHoldingWedding----------')
    local request = { spouseId = spouseId, weddingType = weddingType, date = date, time = time }
    Protocol.RequestHandler.ClientHoldingWeddingRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--重新預約
function _M.RequestClientWeddingReservation(spouseId, weddingType, date, time, cb)
    -- print('---------RequestClientWeddingReservation----------')
    local request = { spouseId = spouseId, weddingType = weddingType, date = date, time = time }
    Protocol.RequestHandler.ClientWeddingReservationRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--發送請帖
function _M.RequestClientSendInvitation(slotIndex, friendIds, cb)
    -- print('---------RequestClientSendInvitation----------')
    local request = { slotIndex = slotIndex, friendIds = friendIds }
    Protocol.RequestHandler.ClientSendInvitationRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--夫妻倉庫請求
function _M.RequestClientGetCoupleWarehouseData(cb)
    -- print('---------RequestClientGetCoupleWarehouseData----------')
    local request = { }
    Protocol.RequestHandler.ClientGetCoupleWarehouseDataRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

local warehouseListener = nil
function _M.SetCoupleWarehouseListener( cb )
    warehouseListener = cb
end

--夫妻倉庫存入
function _M.RequestClientCoupleWarehousePutOn(slotIndex, num, cb)
    -- print('---------RequestClientCoupleWarehousePutOn----------')
    local request = { slotIndex = slotIndex, num = num }
    Protocol.RequestHandler.ClientCoupleWarehousePutOnRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
        if warehouseListener then
            warehouseListener()
        end
    end)
end

--夫妻倉庫取出
function _M.RequestClientCoupleWarehousePutOff(warehouseType, slotIndex, cb)
    -- print('---------RequestClientCoupleWarehousePutOff----------')
    local request = { warehouseType = warehouseType, slotIndex = slotIndex }
    Protocol.RequestHandler.ClientCoupleWarehousePutOffRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--離婚
function _M.RequestClientDivorce(wType, cb)
    -- print('---------RequestClientDivorce----------')
    local request = { type = wType }
    Protocol.RequestHandler.ClientDivorceRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--舉辦婚禮
function _M.RequestClientWeddingStart(cb)
    print('---------RequestClientDivorce----------')
    local request = { }
    Protocol.RequestHandler.ClientWeddingStartRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end

--檢查請柬
function _M.RequestClientCheckInvitation(slotIndex, cb)
    print('---------RequestClientCheckInvitation----------')
    local request = { slotIndex = slotIndex }
    Protocol.RequestHandler.ClientCheckInvitationRequest(request, function(rsp)
        -- print_r(rsp)
        if cb then
          cb(rsp)
        end
    end)
end



local isPlayingEffect = false
local effectQueue = {}

--全屏特效队列播放机制
function _M.PlayRelationEffect( res )
    isPlayingEffect = true
    if TLBattleScene.Instance == nil or not TLBattleScene.Instance.IsRunning then
        UnityHelper.WaitForSeconds(1, function()
            _M.PlayRelationEffect(res)
        end)
    else
        local transSet = TransformSet()
        transSet.Layer = Constants.Layer.UI
        transSet.LayerOrder = 5000
        RenderSystem.Instance:PlayEffect(res, transSet, 0, function( ... )
            isPlayingEffect = false
            if #effectQueue > 0 then
                local path = effectQueue[1]
                table.remove(effectQueue, 1)
                _M.PlayRelationEffect(path)
            end
        end)
    end
end

function _M.WaitToPlayRelationEffectByPath( effectPath )
    -- print('----------------WaitToPlayRelationEffectByPath---------------', effectPath)
    if not string.IsNullOrEmpty(effectPath) then --全屏特效
        if isPlayingEffect then
            table.insert(effectQueue, effectPath)
        else
            _M.PlayRelationEffect(effectPath)
        end
    end
end

function _M.WaitToPlayRelationEffect( itemId )
    -- print('----------------WaitToPlayRelationEffect---------------', itemId)
    local giftDb = GlobalHooks.DB.FindFirst('relationItemData', { item_id = itemId })
    if giftDb and giftDb.effect_type ~= 0 then --全屏特效
        _M.WaitToPlayRelationEffectByPath(giftDb.item_effect)
    end
end

function _M.IsInBlackList(id)
    if id and blackList[id] then
        return true
    end
    return false
end

local function OnTLClientMarrySuccessNotify( notify )
    print_r("TLClientMarrySuccessNotify---------------------------------", notify)
    local data = notify.data
    if string.IsNullOrEmpty(data.husbandId) then --離婚
        DataMgr.Instance.UserData.SpouseId = ''
        DataMgr.Instance.UserData.SpouseName = ''
        GlobalHooks.UI.CloseUIByTag('DivorceApply')
    else --結婚
        DataMgr.Instance.UserData.SpouseId = DataMgr.Instance.UserData.RoleID == data.husbandId and data.wifeId or data.husbandId
        DataMgr.Instance.UserData.SpouseName = DataMgr.Instance.UserData.RoleID == data.husbandId and data.wife or data.husband
        GlobalHooks.UI.CloseUIByTag('WeddingReserve')
        GlobalHooks.UI.CloseUIByTag('MarryApply')
        GlobalHooks.UI.OpenUI("MarrySuccess", 0, { husbandId = data.husbandId, wifeId = data.wifeId, husband = data.husband, wife = data.wife })
    end
end

local function OnTLClientRelationUpNotify( notify )
    print_r("TLClientRelationUpNotify---------------------------------", notify)
    for i = 1, notify.record.num do
        _M.WaitToPlayRelationEffect(notify.record.templateId)
    end
end


local function OnTLClientTeamRelationUpNotify( notify )
    print_r("TLClientTeamRelationUpNotify---------------------------------", notify)
    local nameStr = ''
    for roleId, roleName in pairs(notify.players) do
        nameStr = nameStr..roleName..','
    end
    nameStr = string.sub(nameStr, 1, -2)
    local content = Util.GetText('relationship_teamexp', nameStr, notify.addRelation)
    GameAlertManager.Instance:ShowFloatingTips(content)
    local ChatModel = require 'Model/ChatModel'
    ChatModel.AddClientMsg(ChatModel.ChannelState.CHANNEL_SYSTEM, content)
end


function _M.InitNetWork(initNotify)
  -- print('----------SocialModel InitNetWork------------')
    if initNotify then
        Protocol.PushHandler.TLClientRelationUpNotify(OnTLClientRelationUpNotify)
        Protocol.PushHandler.TLClientTeamRelationUpNotify(OnTLClientTeamRelationUpNotify)
        Protocol.PushHandler.TLClientMarrySuccessNotify(OnTLClientMarrySuccessNotify)
    else
        _M.RequestClientGetBlackList()
    end
end

function _M.fin()
  -- print('----------SocialModel fin------------')
    EventManager.Unsubscribe("Event.InteractiveMenu.Show", OnShowInteractiveMenu)
    isPlayingEffect = false
    effectQueue = {}

    FunctionUtil.UnRegister('marryapply')
    FunctionUtil.UnRegister('wedding')
    FunctionUtil.UnRegister('wedding_ceremony')
end

function _M.initial()
   --print('----------SocialModel inital------------')
    local temp = CacheImage.Instance
    EventManager.Subscribe("Event.InteractiveMenu.Show", OnShowInteractiveMenu)

    FunctionUtil.Register('marryapply', function( ... )
        GlobalHooks.UI.OpenUI('MarryApply', 0)
    end)
    
    FunctionUtil.Register('wedding', function( ... )
        _M.RequestClientWeddingStart()
    end)
    
    FunctionUtil.Register('wedding_ceremony', function( ... )
        EventApi.Task.StartEvent(function()
            EventApi.Task.Wait(EventApi.Protocol.Task.Request('StartWeddingAnime',{}))
        end)
    end)
end

return _M
