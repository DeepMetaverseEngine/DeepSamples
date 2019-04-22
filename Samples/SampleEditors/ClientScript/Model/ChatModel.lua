local cjson = require "cjson"
-- local bit = require "bit"
local ringbuffer    = require "Logic/ringbuffer"
local Util      = require "Logic/Util"
local SceneModel = require 'Model/SceneModel'

local bit = require "bit"
local SysColor = 3425613
local NormalColor = 5521965
local defaultFontSize = 18

local _M = {}
_M.__index = _M

 
--服务端频道
_M.ChannelState = {   
  --无效
  CHANNEL_INVALID = -1,
   -- 私聊频道
  CHANNEL_PRIVATE = 0, 
  -- 世界
  CHANNEL_WORLD = 1,
  -- 附近
  CHANNEL_AREA = 2,
  -- 工会
  CHANNEL_GUILD = 3,
  -- 队伍
  CHANNEL_TEAM = 4,
  -- 平台频道
  CHANNEL_PLATFORM = 5,
   -- 系统
  CHANNEL_SYSTEM = 6,
  -- 战场 同阵营的频道
  CHANNEL_BATTLE = 7,
  -- 师门频道
  CHANNEL_SHIMEN = 8, 
  --喇叭频道
  CHANNEL_HORN = 9,

  CHANNEL_MAX = 10, 
}

_M.ChatAction = {   
  --无效
  ACTION_INVALID = 0,
  -- 位置
  ACTION_POSITION = 1,
  --红包
  ACTION_REDPACKAGE = 2,
  -- 动作
  ACTION_ACT = 3,
  -- 展示
  ACTION_SHOW = 4,
  -- 历史
  ACTION_HISTORY = 5,
}
 
-- --0为普通消息，1为滚动的系统消息，2为喇叭消息，3为不滚动的系统消息
-- _M.ChatMsgEnum = {
--   ChatMsgEnumNormal = 0,
--   ChatMsgEnumScrollSys = 1,
--   ChatMsgEnumHorm = 2,
--   ChatMsgEnumNorSys = 3,
-- }

_M.FuncType = {
  NONE = 0,
  TYPE_BLACK = 1,
  TYPE_FLOAT = 2,
  TYPE_SCROLL = 3,
}

-- --1掷骰 2宝藏 3队伍招募消息 4动作 5红包
-- _M.ChatMsgFuntype = {
--   -- FuntypeThrowDice  = 1,
--   -- FuntypeTreasure = 2,
--   -- FuntypeGuildRecruit = 3,
--   -- FuntypeAction = 4,
--   -- FuntypeRedEnvelope = 5
-- }
 
--消息缓存队列，所有聊天消息都保存在这里
_M.messageMaxCout = 500
_M.ChatData = {
	-- {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}, {}
}

--聊天消息push回调
_M.ChatPushCallback = {
  chatPushCb = {},
}



--聊天设置，文字颜色等等，是否许可匿名聊天
_M.mSettingItems = {}

--聊天常用语，保存上次说话的记录
_M.mCommonItems = {}

--是否显示红点
_M.RedPoint = {
  {showPoint = false},
  {showPoint = false},
  {showPoint = false},
  {showPoint = false},
  {showPoint = false},
  {showPoint = false},
  {showPoint = false},
  {showPoint = false},
}

--上一句发送聊天时间
_M.sendTime = {}

_M.ChatRoles = {}
_M.ChatRoleMap = {}
_M.PrivateChatData = {}
_M.PrivateChatRed = {}

function _M.GetPrivateChannelRed()
    local result = false
    for k, v in pairs(_M.PrivateChatRed or  {}) do
        if v == true then
            result = true
            break
        end
    end
    return result
end

--初始化聊天本地各种保存设置
function _M.InitBaseSetData()
    -- UnityEngine.PlayerPrefs.DeleteAll()
    if not DataMgr.Instance.UserData.RoleID then
        return
    end 

    -- body
    local chatSettings = GlobalHooks.DB.Find('ChatSetting', {})
    for i = 1, #chatSettings do
        local channelSets = chatSettings[i]
        _M.mSettingItems[channelSets.id] = channelSets


        channelSets.IsHide = (1 == UnityEngine.PlayerPrefs.GetInt(channelSets.id .. "chatchannel", channelSets.ishide))
        channelSets.curColor =  tonumber(UnityEngine.PlayerPrefs.GetString(i .. "channelcolor", "0"))
        --channelSets.Barrage = UnityEngine.PlayerPrefs.GetInt(channelSets.id .. "barrage", channelSets.Barrage)

        channelSets.Lefttimes = UnityEngine.PlayerPrefs.GetInt(DataMgr.Instance.UserData.RoleID .. i .. System.DateTime.Today:ToShortDateString() .. "Lefttimes", -1)
        if channelSets.Lefttimes == -1 then   --剩余次数，如果本地没有保存，则是重新开启次数
            channelSets.Lefttimes = tonumber(channelSets.chat_cd)
        end
    end

    _M.mCommonItems = {}
    for i = 1, 10 do
        _M.mCommonItems[i] = {}
        _M.mCommonItems[i].common = UnityEngine.PlayerPrefs.GetString(DataMgr.Instance.UserData.RoleID .. i .. "chatCommon", "")
        _M.mCommonItems[i].commontimes = UnityEngine.PlayerPrefs.GetInt(DataMgr.Instance.UserData.RoleID .. i .. "chatCommontimes", 0)
    end

    _M.InitChatData()
   

    _M.ChatData[_M.ChannelState.CHANNEL_WORLD] = ringbuffer:new(_M.messageMaxCout)
    _M.ChatData[_M.ChannelState.CHANNEL_AREA] = ringbuffer:new(_M.messageMaxCout)
    _M.ChatData[_M.ChannelState.CHANNEL_GUILD] = ringbuffer:new(_M.messageMaxCout)
    _M.ChatData[_M.ChannelState.CHANNEL_TEAM] = ringbuffer:new(_M.messageMaxCout)
    _M.ChatData[_M.ChannelState.CHANNEL_PLATFORM] = ringbuffer:new(_M.messageMaxCout)
    _M.ChatData[_M.ChannelState.CHANNEL_SYSTEM] = ringbuffer:new(_M.messageMaxCout)
    _M.ChatData[_M.ChannelState.CHANNEL_BATTLE] = ringbuffer:new(_M.messageMaxCout)
    _M.ChatData[_M.ChannelState.CHANNEL_SHIMEN] = ringbuffer:new(_M.messageMaxCout)
    _M.ChatData[_M.ChannelState.CHANNEL_HORN] = ringbuffer:new(_M.messageMaxCout)
    
end

--保存聊天本地各种保存设置
function _M.SaveBaseSetData()
    for i = 1, #_M.mCommonItems do
        UnityEngine.PlayerPrefs.SetString(DataMgr.Instance.UserData.RoleID .. i .. "chatCommon", _M.mCommonItems[i].common)
        UnityEngine.PlayerPrefs.SetInt(DataMgr.Instance.UserData.RoleID .. i .. "chatCommontimes", _M.mCommonItems[i].commontimes)
    end
end

function _M.SaveSettings()
    -- body
  for k,v in pairs(_M.mSettingItems) do
        UnityEngine.PlayerPrefs.SetInt(v.id .. "chatchannel", v.IsHide and 1 or 0)
        UnityEngine.PlayerPrefs.SetString(v.id .. "channelcolor", tostring(v.curColor))
      --UnityEngine.PlayerPrefs.SetInt(_M.mSettingItems[i].id .. "barrage", _M.mSettingItems[i].Barrage)
        UnityEngine.PlayerPrefs.SetInt(DataMgr.Instance.UserData.RoleID .. v.id .. System.DateTime.Today:ToShortDateString() .. "Lefttimes", v.Lefttimes)
      --UnityEngine.PlayerPrefs.SetInt(_M.mSettingItems[i].id .. "anonymousState", _M.mSettingItems[i].AnonymousState)
  end
end


local function UpdateRecents(str)
  	-- body
  	for i = 1, #_M.mCommonItems do
    	if string.gsub(str, "|", "") == string.gsub(_M.mCommonItems[i].common, "|", "") then
            table.insert(_M.mCommonItems, 1, table.remove(_M.mCommonItems, i))
            return
        elseif _M.mCommonItems[i].common == "" then
            break
    	end
  	end

    table.insert(_M.mCommonItems, 1, table.remove(_M.mCommonItems, #_M.mCommonItems))
    _M.mCommonItems[1].common = str
end

function _M.GetLastSpeakPerson(channel)
    -- body
    local datalist = _M.ChatData[channel]
    local chatacterlist = {}
    for i = 1, #datalist do 
        local data = datalist[#datalist + 1 - i]
        if data.s2c_playerId ~= DataMgr.Instance.UserData.RoleID then
          return data
        end
    end
end

function _M.RemoveChatPushListener(key)
  	_M.ChatPushCallback.chatPushCb[key] = nil
end

function _M.AddChatPushListener(key, cb)
  	_M.ChatPushCallback.chatPushCb[key] = cb
end

function _M.AddChatRoleMap(roleId,roleName)
    _M.ChatRoleMap[roleId] = roleName
end

function _M.AddChatRole(roleId,roleName)
  -- body
    if _M.ChatRoles[1] ~=nil and _M.ChatRoles[1].roleId == roleId and _M.ChatRoles[1].roleName ~= nil then
       return false
    end
    for i, v in ipairs(_M.ChatRoles) do
        if v.roleId == roleId then
            table.remove(_M.ChatRoles, i)    
        end
    end
    local roleData = {roleId = roleId,roleName = roleName}
    table.insert(_M.ChatRoles,1,roleData)
    return true
end

function _M.RemovePrivateChat(roleId)
  -- body
    for i, v in ipairs(_M.ChatRoles) do
        if v.roleId == roleId then
            table.remove(_M.ChatRoles, i)
        end
    end
    if _M.PrivateChatData[roleId] then
       _M.PrivateChatData[roleId] = ringbuffer:new(_M.messageMaxCout)
    end
end

function _M.InitChatData()
    _M.ChatRoleMap = {}
    _M.ChatRoles = {}

    local ChatRole = UnityEngine.PlayerPrefs.GetString('chatrole__'.. DataMgr.Instance.UserData.RoleID)
    -- print_r('InitChatData ChatRole:',ChatRole)
    if string.IsNullOrEmpty(ChatRole) then
        return
    end

    local ChatRoleMap =  cjson.decode(ChatRole)
    for k,v in ipairs(ChatRoleMap) do
        if v.roleId and v.roleName then
            table.insert(_M.ChatRoles,1,v)
        end
    end
 

    for k,v in ipairs(_M.ChatRoles) do
        local jsonChatData = UnityEngine.PlayerPrefs.GetString(v.roleId)
        if  jsonChatData ~= nil then
            local ChatData = cjson.decode(jsonChatData)
            if ChatData ~= nil then
                -- print('InitChatData v.roleId:',v.roleId)
                -- print_r('InitChatData ChatData:',ChatData)
                local ChatRingBuffer = ringbuffer:new(_M.messageMaxCout)
                for k,v in pairs(ChatData) do
                    ChatRingBuffer:push(v)
                end
                -- print_r('InitChatData ChatRingBuffer:',ChatRingBuffer)
                _M.PrivateChatData[v.roleId] = ChatRingBuffer
            end
        end
    end

end

function _M.SaveChatData()
    if not DataMgr.Instance.UserData.RoleID then
        return
    end
    local ChatRole = cjson.encode(_M.ChatRoles)
    -- print_r('SaveChatData ChatRole:',ChatRole)
    UnityEngine.PlayerPrefs.SetString('chatrole__' .. DataMgr.Instance.UserData.RoleID, ChatRole)


    for k,v in ipairs(_M.ChatRoles) do

        local ChatRingBuffer = _M.PrivateChatData[v.roleId]
        if ChatRingBuffer ~= nil then
            local ChatData = {}
            UnityEngine.PlayerPrefs.SetString(v.roleId, "")
            for i = ChatRingBuffer.length,1,-1 do
                local tempData = ChatRingBuffer[i]
                local data = {}
                data.channel_type = tempData.channel_type
                data.content = tempData.content 
                data.from_name = tempData.from_name
                data.from_uuid = tempData.from_uuid
                data.to_uuid = tempData.to_uuid
                data.to_name = tempData.to_name
                data.func_type = tempData.func_type
                data.is_myself = tempData.is_myself
                data.show_type = tempData.show_type
                data.isSys = tempData.isSys
                table.insert(ChatData,data)
            end
            local jsonChatData = cjson.encode(ChatData)
            UnityEngine.PlayerPrefs.SetString(v.roleId, jsonChatData)
        end
    end
end


function _M.GetPrivateChatData(roleId)
    if _M.PrivateChatData[roleId] == nil then
        _M.PrivateChatData[roleId] = ringbuffer:new(_M.messageMaxCout)
    end 
    return _M.PrivateChatData[roleId]
end

local function insertPrivateData(param)
    if param.channel_type ~= _M.ChannelState.CHANNEL_PRIVATE  then
        return false
    end

    local playerId
    if  param.to_uuid == DataMgr.Instance.UserData.RoleID then
        playerId = param.from_uuid
        local resut = _M.AddChatRole(playerId,param.from_name)
        EventManager.Fire('Event.Chat.AddPrivateChat',{roleId = playerId, roleName = param.from_name})
        _M.PrivateChatRed[playerId] = true
    else
        playerId = param.to_uuid
        --我发给其他玩家的
        if not string.IsNullOrEmpty(param.to_name) then
          local resut = _M.AddChatRole(playerId,param.to_name)
          _M.AddChatRoleMap(playerId, param.to_name)
          EventManager.Fire('Event.Chat.AddPrivateChat',{roleId = playerId, roleName = param.to_name})
        end
    end

    -- print('playerId:',playerId)
    if  _M.PrivateChatData[playerId] == nil then
        _M.PrivateChatData[playerId] = ringbuffer:new(_M.messageMaxCout)
    end
    _M.PrivateChatData[playerId]:push(param)
     
    return true
end

function _M.GetChatData(channelType)
    if _M.ChatData[channelType] == nil then
        _M.ChatData[channelType] = ringbuffer:new(_M.messageMaxCout)
    end
    return _M.ChatData[channelType]
end

local function GetNewMessage(param,showChannleType)
    -- print_r('GetNewMessage(param,showChannleType):',param,showChannleType)
    local message = {}
    message.from_name =  param.from_name
    message.from_uuid = param.from_uuid
    message.to_uuid =  param.to_uuid
    message.to_name = param.to_name
    message.channel_type = showChannleType
    message.content = param.content
    message.show_type = param.show_type
    message.func_type = param.func_type
    message.is_myself = param.is_myself
    message.AText = param.AText
    message.isSys = param.isSys
    return message
end

local function isBlackList(roleId)
    if string.IsNullOrEmpty(roleId) then
        return false
    end
    local SocielModel = require 'Model/SocialModel'
    return SocielModel.IsInBlackList(roleId)
end

local function insertData(param) 
    -- local message = Util.FilterBlackWord(param.content)
    local message = param.content

    --有langkey肯定是多语言
    if param.langKey then
        local args = {}
        for _, val in ipairs(param.langKey) do
            table.insert(args, Util.GetText(val))
        end
        param.content = Util.GetText(message, unpack(args))
    --系统消息也可能是多语言 玩家消息不需要转
    elseif string.IsNullOrEmpty(param.from_uuid) or string.IsNullOrEmpty(param.from_name) then
        param.content = Util.GetText(message)
    end

    local ChatUtil  = require 'UI/Chat/ChatUtil'
    if string.IsNullOrEmpty(param.from_uuid) or string.IsNullOrEmpty(param.from_name) then
        --系统消息 
        param.AText = ChatUtil.HandleChatClientDecode(param.content, SysColor, nil, nil, defaultFontSize,true)
    else 
        param.AText = ChatUtil.HandleChatClientDecode(param.content, NormalColor, nil, nil, defaultFontSize,param.isSys)
    end

    if param.channel_type == _M.ChannelState.CHANNEL_PRIVATE then
        return insertPrivateData(param)
    end
    if _M.ChatData[param.channel_type] == nil then
      _M.ChatData[param.channel_type] = ringbuffer:new(_M.messageMaxCout)
    end
    _M.ChatData[param.channel_type]:push(param)

    -- print_r('insertData               :',param)
    for k,newChannel in ipairs(param.show_channel or {}) do
     
      local copeMessage = GetNewMessage(param,newChannel)
      -- print_r('k , newChannel',copeMessage)
      if _M.ChatData[newChannel] == nil then
          _M.ChatData[newChannel] = ringbuffer:new(_M.messageMaxCout)
      end
      _M.ChatData[newChannel]:push(copeMessage)
    end
  
    return true
end


local function chatErrorDeal(scope)
 
    local channelConfig = _M.mSettingItems[scope]

    -- 为了测试方便注释掉时间限制先
    if _M.sendTime[scope] ~= nil then
        local time = math.floor((System.DateTime.Now - _M.sendTime[scope]).TotalSeconds)
        if channelConfig ~= nil and channelConfig.chat_cd ~= nil and time < channelConfig.chat_cd then 
            local str = Util.GetText('chat_waitcd', channelConfig.chat_cd - time)
            GameAlertManager.Instance:ShowNotify("<f>" .. str .. "</f>")
            return true
        end
    end

    _M.sendTime[scope] = System.DateTime.Now

    if scope == _M.ChannelState.CHANNEL_SYSTEM then
        GameAlertManager.Instance:ShowNotify(Util.GetText('chat_nosystem'))
        return true
    elseif scope == _M.ChannelState.CHANNEL_TEAM and DataMgr.Instance.TeamData.HasTeam == false then
        GameAlertManager.Instance:ShowNotify(Util.GetText('message2'))
        return true
    elseif scope == _M.ChannelState.CHANNEL_GUILD and string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildId) then
        GameAlertManager.Instance:ShowNotify(Util.GetText('message3'))
        return true
    elseif scope == _M.ChannelState.CHANNEL_BATTLE and DataMgr.Instance.UserData.allyId == "" then
        GameAlertManager.Instance:ShowNotify(Util.GetText('no_ally'))
        return true
    end

  -- local roleLevel = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0)
    local roleLevel = DataMgr.Instance.UserData.Level
    if channelConfig ~= nil and channelConfig.chat_level ~= nil and roleLevel < channelConfig.chat_level then 
        local str = Util.GetText('chat_level_limit', channelConfig.chat_level)
        GameAlertManager.Instance:ShowNotify("<f>" .. str .. "</f>")
        return true
    end

    return false
end

function _M.ChatShout(message,mainChannel,showChannels,cb)
  -- body
    local mainChannel =  mainChannel or _M.ChannelState.CHANNEL_PLATFORM
    local errorReturn = chatErrorDeal(mainChannel)
    if errorReturn then
        return
    end

    local input = {}
    input.show_type = 0
    input.func_type = 0

    input.channel_type = mainChannel  
    input.show_channel = showChannels or {}
    
    input.from_name = DataMgr.Instance.UserData.Name
    input.from_uuid = DataMgr.Instance.UserData.RoleID
    input.content = message

    Protocol.RequestHandler.ClientChatRequest(input, function(ret)
        GameAlertManager.Instance:ShowNotify(Util.GetText('chat_yelling'))
        if cb ~= nil then
           cb(ret)
        end
    end)
end

function _M.ReqChatMessage(scope, content, acceptRoleData, functype, cb)
 
    --记录最近发言
    -- local isVoice = IsVoiceMsg(content)
    if _M.mCommonItems ~= nil and isVoice == nil and (functype == nil or functype == 0) then
        UpdateRecents(content)
    end

    local errorReturn = chatErrorDeal(scope)
    if errorReturn then
        return
    end

    local input = {}
    input.channel_type = scope
    input.show_type = 0
	  input.show_channel = {}
    -- input.show_channel = {_M.ChannelState.CHANNEL_PLATFORM}
    -- table.insert(input.show_channel,_M.ChannelState.CHANNEL_PLATFORM)
    input.from_name = DataMgr.Instance.UserData.Name
    input.from_uuid = DataMgr.Instance.UserData.RoleID
    input.content = content
    input.func_type = functype or 0 

  	if acceptRoleData == nil or acceptRoleData.playerId == nil or string.gsub(acceptRoleData.playerId, " ", "") == "" then
        input.to_uuid = ""
    	  if scope == _M.ChannelState.CHANNEL_PRIVATE then
        	  GameAlertManager.Instance:ShowNotify(Util.GetText('chat_object'))
        	 return
      	end
  	else
      	input.to_uuid = acceptRoleData.playerId
  	end

    Protocol.RequestHandler.ClientChatRequest(input, function(ret)
        if cb ~= nil then
           cb(ret)
        end
    end)
 
end

function _M.StartsWith(str,Start)
    return string.starts(str, Start)
end

function _M.EndsWith(str, End)
    return string.ends(str, End)
end

function _M.GetContent(item)
    local length = string.len(item)
    --local pos = string.find(item, res)
    local content = string.sub(item, 4, length - 5)
    --print("eeeeeeeeeeeeeeeeeeeeeee =" , content, " item ", item)
    return content
end


local function dealChatMsg(param)

    if param.from_uuid == DataMgr.Instance.UserData.RoleID then
        param.is_myself = true
    end
 
    insertData(param)
 
  	for key,val in pairs(_M.ChatPushCallback.chatPushCb) do
        val(param)
  	end

    if param.func_type and param.func_type ~= _M.FuncType.NONE then
        if not string.IsNullOrEmpty(param.from_name) then
            local ChatUtil  = require 'UI/Chat/ChatUtil'
            local message = param.from_name .. ": " ..  ChatUtil.HandleChatClientDecodeFace(param.content)
            SceneModel.ShowTips(param.func_type, message)
        else
            SceneModel.ShowTips(param.func_type, param.content)
        end
    elseif param.channel_type == _M.ChannelState.CHANNEL_HORN then
        -- if not string.IsNullOrEmpty(param.from_name) then
        --     local ChatUtil  = require 'UI/Chat/ChatUtil'
        --     local message = param.from_name .. ": " ..  ChatUtil.HandleChatClientDecodeFace(param.content)
        --     SceneModel.ShowTips(_M.FuncType.TYPE_SCROLL, message)
        -- else
        --     SceneModel.ShowTips(_M.FuncType.TYPE_SCROLL, param.content)
        -- end

        EventManager.Fire('Event.SysChat.OnHornMessage',param)
    end
end

local function OnTLClientChatNotify(notify)
    -- print('TLClientChatNotify---------------------------------')
    -- print_r(notify)
    if(notify ~= nil)then
       -- print_r('TLClientChatNotify notify:',notify)
        if isBlackList(notify.from_uuid) then
            return
        end
       dealChatMsg(notify)
    end
end
 
 
function _M.AddClientMsg(channelType,message)
     local notify = {}
     notify.channel_type = channelType
     notify.content = message
     --客户端加的消息都是程序操作的
     notify.isSys = true
     OnTLClientChatNotify(notify)
end
 
function _M.OpenPrivateChatUI(playerId,playerName)
    if GlobalHooks.UI.FindUI('ChatMainSmall') == nil and GlobalHooks.UI.FindUI('ChatMain') == nil then
        _M.AddChatRole(playerId,playerName)
        GlobalHooks.UI.OpenUI('ChatMainSmall', 0, _M.ChannelState.CHANNEL_PRIVATE,playerId) 
    else
        EventManager.Fire('Event.Chat.SendPrivateMsg',{roleId = playerId, roleName = playerName, byself = true})
    end
end

function _M.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.TLClientChatNotify(OnTLClientChatNotify)
        -- Protocol.PushHandler.ClientMessageContentNotify(OnClientMessageContentNotify)
    end
end

local function OnPlayerGetItem(eventname,params)
    local templateId = params.TemplateID
    local count = params.Count
    local ChatUtil  = require 'UI/Chat/ChatUtil'
    local content = ChatUtil.AddItemByTemplateId(templateId)
    local message = Util.GetText(Constants.Text.chat_getItem,content,count)
    local chatData = {}
    chatData.channel_type = _M.ChannelState.CHANNEL_SYSTEM
    chatData.content = message
    insertData(chatData)
end

function _M.initial()
  
  _M.ChatRoles = {}
  _M.ChatRoleMap = {}
  _M.PrivateChatData = {}
  _M.PrivateChatRed = {}
  _M.InitBaseSetData()

   EventManager.Subscribe("Event.SysChat.GetItem",OnPlayerGetItem)

end

function _M.fin()
  _M.SaveChatData()
  EventManager.Unsubscribe("Event.SysChat.GetItem", OnPlayerGetItem)
  
end

return _M
