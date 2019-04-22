local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
function _M.RequestChangePKMode(mode, cb)
  print('---------RequestChangePKMode----------')
  local request = {c2s_mode = mode}
  Protocol.RequestHandler.ClientChangePKModeRequest(request,function (rsp)
	print_r(rsp)
		if cb then
      		cb(rsp)
    	end
	end)
end

function _M.ShowTips( msgType, content, customArgs )
    if msgType == 1 then
        GameAlertManager.Instance:ShowNotify(content)
    elseif msgType == 2 then  
        GameAlertManager.Instance:ShowFloatingTips(content)
    elseif msgType == 3 then
        GameAlertManager.Instance:ShowGoRoundTipsXml(content, nil)
    elseif msgType == 4 then
        GameAlertManager.Instance:ShowGoRoundTipsXml(content, nil)
        if customArgs then
            local SocialModel = require 'Model/SocialModel'
            local itemId = tonumber(customArgs.itemId)
            local itemNum = tonumber(customArgs.itemNum)
            for i = 1, itemNum do
                SocialModel.WaitToPlayRelationEffect(itemId)
            end
        end
    else
        GameAlertManager.Instance:ShowNotify(content)
    end
end

function _M.ShowTipBroadCast(msgType,content,channel)
    _M.ShowTips( msgType, content)
    if channel then
        local ChatModel = require 'Model/ChatModel'
        for _,newChannel in ipairs(channel or {}) do
            ChatModel.AddClientMsg(newChannel,content)
        end
    end
end

function _M.BroadCastNotice(noticeID)
  -- body
    local noticeData = GlobalHooks.DB.FindFirst('Notice',{notice_id = noticeID})
    if not noticeData then
        return
    end
    local content = Util.GetText(noticeData.text)
    _M.ShowTips(3,content)
    if noticeData.channel then
        local ChatModel = require 'Model/ChatModel'
        for _,newChannel in ipairs(noticeData.newChannel or {}) do
            ChatModel.AddClientMsg(newChannel,content)
        end
    end
end

local function OnClientMessageContentNotify( notify )
    print("---------ClientMessageContentNotify------------")
    -- print_r(notify)
    local args = {}
    if notify.s2c_args then
        for _, val in ipairs(notify.s2c_args) do
            table.insert(args, Util.GetText(val))
        end
    end
    local content = Util.GetText(notify.s2c_data, unpack(args))
    _M.ShowTips(notify.s2c_type, content, notify.s2c_customArgs)

    if notify.s2c_show_channel then
        local ChatModel = require 'Model/ChatModel'
        for _,newChannel in ipairs(notify.s2c_show_channel or {}) do
            if newChannel > 0 then
                ChatModel.AddClientMsg(newChannel,content)
            end
        end
    end
end

function _M.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.ClientMessageContentNotify(OnClientMessageContentNotify)
    end
end


local function ReqChangeScene(mapId,mapuuid,flag,cb)
    local request = {c2s_MapId = mapId,c2s_MapUUID = mapuuid,c2s_NextMapPosition = flag}
    Protocol.RequestHandler.ClientChangeSceneRequest(request,function (rsp)
        if  rsp:IsSuccess() and cb then
            cb(rsp)
        end
    end)
end

function _M.RequestGetZoneInfoSnaps(cb)
  print('---------RequestGetZoneInfoSnaps----------')
  local request = {}
  Protocol.RequestHandler.ClientGetZoneInfoSnapRequest(request,function(rsp)
    if rsp:IsSuccess() and cb then
      cb(rsp)
      end
    end)
end

function _M.RequestChangeZoneLine(zoneuuid,cb)
  print('---------RequestChangeZoneLine----------')
  local request = {c2s_zoneuuid = zoneuuid}
 Protocol.RequestHandler.ClientChangeZoneLineRequest(request,function(rsp)
    if cb then
      cb(rsp)
      end
    end)
end

_M.ReqChangeScene = ReqChangeScene

return _M