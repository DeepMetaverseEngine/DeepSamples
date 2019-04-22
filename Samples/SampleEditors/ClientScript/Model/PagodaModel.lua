local _M = {}
_M.__index = _M

_M.pagodaStory = 0
_M.lastmaptype = nil
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'

local function GetPagodaNameByFloor(PagodaFloor)
	local data=unpack(GlobalHooks.DB.Find('PagodaData', {floor = PagodaFloor}))
	if data then
		return data.difficulty_name,data.floor_name
	end
end

--给副本描述用的
function _M.GetNameByFloor(PagodaFloor)
	local data=unpack(GlobalHooks.DB.Find('PagodaData', {floor = PagodaFloor}))
	if data then
		return data.difficulty_name,data.infloor
	end
	return '',1
end

function _M.GetDoubleFlyTowerLayerName(PagodaFloor)
	local data=unpack(GlobalHooks.DB.Find('CPTowerData', {floor = PagodaFloor}))
	if data then
		return data.floor_name
	end
end

function _M.GetDoubleFlyTowerLayer(PagodaFloor)
	local data=unpack(GlobalHooks.DB.Find('CPTowerData', {floor = PagodaFloor}))
	if data then
		return data.infloor
	end
end

local function GetPagodaData()
    local temp = {}
	local detail = GlobalHooks.DB.GetFullTable('PagodaData')
    local index = 1
    for i, v in pairs(detail) do
        if index ~= v.difficulty then
            index = v.difficulty
        end
        if temp[index] == nil then
            temp[index] = {}
        end
        temp[index][v.infloor] = v
    end
	return temp
end

local function GetPagodaDifficult()
	local detail = GlobalHooks.DB.GetFullTable('PagodaDifficult')
	return detail
end

local function GetCPTowerData(mode)
	local detail = GlobalHooks.DB.Find('CPTowerData', {difficulty= mode})
	for _, v1 in ipairs(detail) do
		local id = {}
		for _, v2 in ipairs(v1.preview.id) do
			if v2 ~= 0 then
				table.insert(id,v2)
			end
		end
		v1.preview.id = id
		id = {}
		for _, v2 in ipairs(v1.first_reward.id) do
			if v2 ~= 0 then
				table.insert(id,v2)
			end
		end
		v1.first_reward.id = id
	end
	return detail
end

local function GetPagodaStoryData()
	local detail = GlobalHooks.DB.GetFullTable('PagodaStoryData')
	return detail
end

local function GetPagodaStoryClueData(id1)
	local detail = GlobalHooks.DB.Find('PagodaStoryClueData', {story_id = id1})
	return detail
end

local function GetPagodaBoxData()
	local detail = unpack(GlobalHooks.DB.GetFullTable('PagodaBoxData'))
	return detail
end

--local function PayResetCount(functionid,cb)
--	local number = unpack(GlobalHooks.DB.Find('vip_cost',{event_key = functionid})).cost.num[1]
--	local content = Util.GetText("vip_buy_extra_count",number)
--	UIUtil.ShowConfirmAlert(content,nil,function()
--		local msg = { c2s_functionid = functionid }
--		Protocol.RequestHandler.ClientBuyDailyTicketsRequest(msg, function(rsp)
--			if cb then
--				cb(rsp)
--			end
--		end)
--	end)
--end

local function SweepTower(mode,cb)
	local msg = { s2c_index = mode }
	Protocol.RequestHandler.TLClientDemonTowerSweepRequest(msg, function(rsp)
		if cb then
			cb(rsp)
		end
	end)
end

--请求难度数据
function _M.RequireDiffcultData(cb )
	local msg = { }
	Protocol.RequestHandler.TLClientGetDTPassedInfoRequest(msg, function(rsp)
		if cb then
			cb(rsp.s2c_data)
		end
	end)
end

--请求镇妖塔数据
local function RequirePagodaData(mode,cb )
	local msg = {c2s_mode = mode }
	Protocol.RequestHandler.TLClientGetDTInfoRequest(msg, function(rsp)
	    if cb then
	        cb(rsp.s2c_data)
	    end
    end)
end

--请求秘闻数据
local function RequireStoryData(cb,force)
	if force == nil then
		force = true
	end
    local msg = { }
	Protocol.RequestHandler.TLClientGetDTSecertBookInfoRequest(msg, function(rsp)
		if rsp.s2c_data.secretBookList then
			local count = 0
			for i, v in pairs(rsp.s2c_data.secretBookList) do
				if v.state == 2 or v.state == 0 then
					local tempdata = GetPagodaStoryClueData(i)
					if #v.ClueList == #tempdata then
						count = count + 1
					end
				end
			end
			_M.pagodaStory = count
			GlobalHooks.UI.SetRedTips("pagoda",count)
		end
	    if cb then
	        cb(rsp.s2c_data)
	    end
    end,PackExtData(force, force))
end

--重置次数
local function SetEnterCount( cb )
	local msg = { }
	Protocol.RequestHandler.TLClientResetDTRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

--进入镇妖塔
local function EnterPagoda( cb )
	local msg = { }
	Protocol.RequestHandler.TLClientEnterDTRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

--领取首通奖励
local function GetSuccessReward( giftindex, cb)
	local msg = { c2s_giftid = giftindex }
	Protocol.RequestHandler.TLClientGetFirstGiftDTRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end


--激活秘闻
local function ActiveStory( bookIndex, cb )
	local msg = { c2s_bookId = bookIndex }
	Protocol.RequestHandler.TLClientActiveSecertBookRequest(msg, function(rsp)
		_M.pagodaStory = _M.pagodaStory - 1
	    if cb then
	        cb(rsp)
	    end
    end)
end

--通关结算
local function SuccessSettle( nextLayer, cb )
	local msg = { s2c_nextLayer = nextLayer }
	Protocol.RequestHandler.TLClientDTPassRewardNotify(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

--获取盒子数据
local function GetBoxData( cb )
	local msg = { }
	Protocol.RequestHandler.TLClientGetMagicBoxInfoRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

--上钥匙
local function GetKeyData( cb )
	local msg = { }
	Protocol.RequestHandler.TLClientUseKeyRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

--宝箱宝箱快开启
local function OpenBox( cb )
	local msg = { }
	Protocol.RequestHandler.TLClientOpenBoxRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

--退出镇妖塔
local function ExitPagoda( cb )
	local msg = { }
	Protocol.RequestHandler.TLClientExitDTRequest(msg, function(rsp)
	    if cb then
	        cb(rsp)
	    end
    end)
end

--领取秘闻奖励
local function GetStoryReward( id,cb )
	local msg = { c2s_bookId = id }
	Protocol.RequestHandler.TLClientGetSecertRewardRequest(msg, function(rsp)
		_M.pagodaStory = _M.pagodaStory - 1
		if cb then
			cb(rsp)
		end
	end)
end

local function ChangeSceneFinish()
	if _M.lastmaptype == 4 then
		RequireStoryData()
		_M.lastmaptype = nil
	end
	local sceneid = DataMgr.Instance.UserData.MapTemplateId
	if sceneid == 0 then
		sceneid = GameGlobal.Instance.SceneID
	end
	local mapData = GlobalHooks.DB.FindFirst('MapData',{ id = sceneid })

	_M.lastmaptype = mapData.type
	
end

--添加监听
function _M.initial()
	EventManager.Subscribe("Event.Scene.ChangeFinish",ChangeSceneFinish)
end


--注销监听
function _M.fin()
	EventManager.Unsubscribe("Event.Scene.ChangeFinish",ChangeSceneFinish)
end

--请求双修镇塔数据
local function RequireCPTowerdData(mode, cb )
	local msg = {c2s_mode = mode }
	Protocol.RequestHandler.TLClientGetCPDTInfoRequest(msg, function(rsp)
		if cb then
			cb(rsp.s2c_data)
		end
	end)
end

--领取首通奖励
local function GetCPReward(giftid,mode, cb )
	local msg = {c2s_giftid = giftid,c2s_mode = mode}
	Protocol.RequestHandler.TLClientGetFirstGiftCPDTRequest(msg, function(rsp)
		if cb then
			cb(rsp)
		end
	end)
end

_M.RequireCPTowerdData = RequireCPTowerdData
_M.GetCPReward = GetCPReward
_M.GetPagodaDifficult = GetPagodaDifficult
_M.GetStoryReward = GetStoryReward
_M.GetPagodaNameByFloor=GetPagodaNameByFloor
_M.GetPagodaData = GetPagodaData
_M.GetPagodaStoryData = GetPagodaStoryData
_M.GetPagodaStoryClueData = GetPagodaStoryClueData
_M.GetPagodaBoxData = GetPagodaBoxData
_M.RequirePagodaData = RequirePagodaData
_M.SetEnterCount = SetEnterCount
_M.EnterPagoda = EnterPagoda
_M.GetSuccessReward = GetSuccessReward
_M.ActiveStory = ActiveStory
_M.SuccessSettle = SuccessSettle
_M.GetBoxData = GetBoxData
_M.GetKeyData = GetKeyData
_M.OpenBox = OpenBox
_M.ExitPagoda = ExitPagoda
_M.RequireStoryData = RequireStoryData
_M.SweepTower = SweepTower
_M.GetCPTowerData = GetCPTowerData
return _M