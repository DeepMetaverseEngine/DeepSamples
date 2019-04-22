
local Helper = require'Logic/Helper'
local ItemModel = require 'Model/ItemModel'
local PartnerUtil = require 'UI/Partner/FallenPartnerUtil'
local ServerTime = require 'Logic/ServerTime'
local TimeUtil = require 'Logic/TimeUtil'
--test
-- local PartnerData1 = {

--     ID = 1001,
--     Lv = 1,
--     isActive = true,
-- }

--  local PartnerData2 = Helper.copy_table(PartnerData1)
--  PartnerData2.ID = 1002
--  PartnerData2.Lv = 5

local _M = {
	PartnerList = {
		-- PartnerData1,
		-- PartnerData2,
	},
}
_M.__index = _M

_M.time = nil
local AllPartner={}
_M.CurHavePartnerList={}
-- /// <summary>
-- /// 召唤仙侣.
-- /// </summary>
-- [MessageType(Constants.TL_GOD_START + 1)]
-- public class ClientSummonGodRequest : Request, ILogicProtocol
-- {
--     public int c2s_god_id;
-- }
-- /// <summary>
-- /// 召唤仙侣.
-- /// </summary>
-- [MessageType(Constants.TL_GOD_START + 2)]
-- public class ClientSummonGodResponse : Response, ILogicProtocol
-- {
-- public int s2c_god_id;
-- public int s2c_god_lv;
-- }


function _M.PartnerActiveRequest(id,cb)
	local msg = {c2s_god_id = id}
	Protocol.RequestHandler.ClientSummonGodRequest(msg, function(ret)
		--print("ClientSummonGodRequest")
		if ret.s2c_code == 200 then

			local partner = {ID = ret.s2c_god_id, Lv = ret.s2c_god_lv, state = GodStatus._EIdle}
			table.insert( _M.PartnerList, partner)
			--print_r("ClientSummonGodResponseOk", _M.PartnerList)
			--print("ClientSummonGodResponseOk")
		end
		if cb then cb() end
	end)
end


-- /// <summary>
-- /// 升级仙侣列表.
-- /// </summary>
-- [MessageType(Constants.TL_GOD_START + 5)]
-- public class ClientGodUpgradeRequest : Request, ILogicProtocol
-- {
--     public int c2s_god_id;
--     public int c2s_god_lv;
-- }
-- [MessageType(Constants.TL_GOD_START + 6)]
-- public class ClientGodUpgradeResponse : Response, ILogicProtocol
-- {
--     public int s2c_god_lv;
-- }

function _M.PartnerUpgradeRequest(id,lv,cb)
	local msg = {c2s_god_id = id,c2s_god_lv = lv}
	Protocol.RequestHandler.ClientGodUpgradeRequest(msg, function(ret)
		--print("PartnerUpgradeRequest")
		if ret.s2c_code == 200 then

			for k,v in pairs(_M.PartnerList) do
				if v.ID == id then
					v.Lv = ret.s2c_god_lv
					if cb then cb(v.Lv) end
					return
				end
			end
			--GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, "物品不足", '', nil, nil)
		end

	end)
end

-- /// <summary>
-- /// 侠侣出战请求.
-- /// </summary>
-- [MessageType(Constants.TL_GOD_START + 7)]
-- public class ClientGodFightRequest : Request, ILogicProtocol
-- {
--     public int c2s_god_id;
-- }
-- [MessageType(Constants.TL_GOD_START + 8)]
-- public class ClientGodFightResponse : Response, ILogicProtocol
-- {

-- }
function _M.PartnerFightRequest(id,cb,cberr)
	local msg = {c2s_god_id = id}
	Protocol.RequestHandler.ClientGodFightRequest(msg, function(ret)

		if ret.s2c_code == 200 then

			--print("ClientGodFightResponse",ret.s2c_god_id)
			for i,v in ipairs( _M.PartnerList) do
				if v.ID == ret.s2c_god_id then
					v.state = GodStatus._EFight
				else
					v.state = GodStatus._EIdle
				end
			end
			if cb then cb() end
		end

	end,function()
		if cberr then cberr() end
	end)
end
-- /// <summary>
-- /// 获取仙侣列表.
-- /// </summary>
-- [MessageType(Constants.TL_GOD_START + 3)]
-- public class ClientGetGodListRequest : Request, ILogicProtocol
-- {

-- }
-- [MessageType(Constants.TL_GOD_START + 4)]
-- public class ClientGetGodListResponse : Response, ILogicProtocol
-- {
--     public List<ClientGodSnap> s2c_list;
-- }
function _M.PartnerGetListRequest(cb)
	local msg = {}
	--print("PartnerGetListRequest")
	Protocol.RequestHandler.ClientGetGodListRequest(msg, function(ret)
		if ret.s2c_code == 200 then
			local num = #ret.s2c_list
			--print("PartnerGetListRequestlist",num)
			_M.PartnerList = {}
			if #ret.s2c_list > 0 then
				for i = 1, num do
					local v = ret.s2c_list[i]
					local partner = {ID = v.s2c_god_id, Lv = v.s2c_god_lv, state = v.s2c_god_status}
					table.insert(_M.PartnerList, partner)
				end
			end
			--print_r(_M.PartnerList)
		end
		if cb then cb() end
	end)
end

function _M.GetFallenPartnetAttr(partnetId,level)
	local attrData = GlobalHooks.DB.FindFirst('FallenPartnerData',{god_id = partnetId,god_lv = level})
	return attrData
end


--检查未激活的仙侣列表
_M.Unactivated={}
local function ContainsValue(t, value)
	for k, v in pairs(t) do
		if v.s2c_god_id == value then
			return true
		end
	end
	return false
end
local function DeleteActivePartner(all,active)
	for i, v in ipairs(all) do
		if not ContainsValue(active,v.god_id) then
			table.insert(_M.Unactivated,v)
		end
	end
end


--判断hud上仙侣icon是否要显示红点
local function OnShowRedPoint(eventname,params)

	--判断进背包或者消耗的物品是否是材料
	local item = ItemModel.GetDetailByTemplateID(params.TemplateID)
	--整片和碎片都算(激活)
	if item.static.sec_type == 14 or item.static.sec_type ==15 then
		for i, v in ipairs(_M.Unactivated) do
			if v.god_item == params.TemplateID and params.Count >= v.item_num then
				GlobalHooks.UI.SetRedTips('partner',1)
				return
			end
		end
		GlobalHooks.UI.SetRedTips('partner',0)
	end
	--培养
	if item.static.adscription_tab == 2 then
		for i, v in ipairs(_M.CurHavePartnerList) do
			local isshow=PartnerUtil.CheckRedPoint(_M.CurHavePartnerList[i].s2c_god_id,_M.CurHavePartnerList[i].s2c_god_lv)
			if isshow==1 then
				GlobalHooks.UI.SetRedTips('partner',1)
				return
			end
		end
		GlobalHooks.UI.SetRedTips('partner',0)
	end
end


local function TryAddAttribute(all, key, v,index)
	table.insert(all, {Name = key, Value = v,index = index,Tag = 'FixedAttributeTag'})
end

function _M.GetXlsFixedAttribute(static_data)
	local all = {}
	TryAddAttribute(all, 'attack', static_data.attack,1)
	TryAddAttribute(all, 'maxhp', static_data.maxhp,2)
	TryAddAttribute(all, 'defend', static_data.defend,3)
	TryAddAttribute(all, 'mdef', static_data.mdef,4)

	TryAddAttribute(all, 'thunderdamage', static_data.thunderdamage,5)
	TryAddAttribute(all, 'winddamage', static_data.winddamage,6)
	TryAddAttribute(all, 'icedamage', static_data.icedamage,7)
	TryAddAttribute(all, 'firedamage', static_data.firedamage,8)
	TryAddAttribute(all, 'soildamage', static_data.soildamage,9)
	TryAddAttribute(all, 'thunderresist', static_data.thunderresist,10)
	TryAddAttribute(all, 'windresist', static_data.windresist,11)
	TryAddAttribute(all, 'iceresist', static_data.iceresist,12)
	TryAddAttribute(all, 'fireresist', static_data.fireresist,13)
	TryAddAttribute(all, 'soilresist', static_data.soilresist,14)

	return all
end

local function OnFirstInitFinish()
	if not GlobalHooks.IsFuncOpen('PartnerFrame') then return end
	
	local functionid ='onlinetip4'
	local openendtime = unpack(GlobalHooks.DB.Find('Function_OpenTimeData', {function_id = functionid}))
	if not openendtime then
		UnityEngine.Debug.LogError("Function_id ".. functionid .." is Error!")
		return
	end

	--判断38折扣是否在活动时间内
	local Three_Eight_Discount = TimeUtil.inTime(System.DateTime.Parse(openendtime.start_day),
			System.DateTime.Parse(openendtime.end_day),
			ServerTime.getServerTime():ToLocalTime())
	
	local issell = HudManager.Instance:GetHudUI("FunctionHud"):FindChildByEditName('cvs_partner',true):FindChildByEditName('ib_huodong',true)
	if not issell then
		return
	end
	issell.Visible = Three_Eight_Discount
	
	if Three_Eight_Discount then
		local surplusTime = System.DateTime.Parse(openendtime.end_day) - ServerTime.getServerTime():ToLocalTime()
		if surplusTime.TotalHours <= 4 then
			_M.time = LuaTimer.Add(math.floor(surplusTime.TotalSeconds)*1000+10000,
				function()
					Three_Eight_Discount = TimeUtil.inTime(System.DateTime.Parse(openendtime.start_day),
							System.DateTime.Parse(openendtime.end_day),
							ServerTime.getServerTime():ToLocalTime())
					issell.Visible = Three_Eight_Discount
					if _M.time then
						LuaTimer.Delete(_M.time)
						_M.time = nil
					end
				end)
		end
	end
end

function _M.initial()
	EventManager.Subscribe("Event.Scene.FirstInitFinish", OnFirstInitFinish)
	EventManager.Subscribe('Event.Item.CountUpdate', OnShowRedPoint)
end

function _M.fin()
	EventManager.Unsubscribe("Event.Scene.FirstInitFinish", OnFirstInitFinish)
	EventManager.Unsubscribe('Event.Item.CountUpdate', OnShowRedPoint)
	if _M.time then
		LuaTimer.Delete(_M.time)
		_M.time = nil
	end
end

local function GetAllPartnerInfo()
	local all=GlobalHooks.DB.GetFullTable('FallenPartnerListData')
	return all
end

local function isActive(Roleid)
	for i,v in ipairs(_M.PartnerList) do
		if v.ID == Roleid then
			return true,v.Lv
		end
	end
	return false,0
end

function _M.GetActInfoById(id)
	local pe = unpack(GlobalHooks.DB.Find('FallenPartnerEntanglement',{id = id}))
	return pe
end

local function OnActiveGodBookNotify(notify)
	local Util = require 'Logic/Util'
	local pe = _M.GetActInfoById(notify.s2c_new_books[1])
	GameAlertManager.Instance:ShowNotify(Util.GetText('god_book_updata',Util.GetText(pe.rank_name)))
end

--初始化网络时，请求仙侣列表，判断是否添加红点
function _M.InitNetWork(initNotify)
	if not GlobalHooks.IsFuncOpen('PartnerFrame') then return end
	
	if initNotify then
		Protocol.PushHandler.ClientActiveGodBookNotify(OnActiveGodBookNotify)
		return
	end
	
	AllPartner=GetAllPartnerInfo()
	Protocol.RequestHandler.ClientGetGodListRequest({}, function(rsp)
		_M.CurHavePartnerList=rsp.s2c_list
		DeleteActivePartner(AllPartner,rsp.s2c_list)
		
		--可培养仙侣
		for i, v in ipairs(_M.CurHavePartnerList) do
			local isshow=PartnerUtil.CheckRedPoint(_M.CurHavePartnerList[i].s2c_god_id,_M.CurHavePartnerList[i].s2c_god_lv)
			if isshow==1 then
				GlobalHooks.UI.SetRedTips('partner',1)
				return
			end
		end
		--可激活仙侣
		for i, v in ipairs(_M.Unactivated) do
			local isshow = isActive(v.god_id)
			if not isshow then
				local Num = PartnerUtil.GetItemCountByItemID(tonumber(_M.Unactivated[i].god_item))
				if Num >= _M.Unactivated[i].item_num then --如果数量大于激活数量，返回可激活
					GlobalHooks.UI.SetRedTips('partner',1)
					return
				end
			end
		end
		
		GlobalHooks.UI.SetRedTips('partner',0)
	end)
end

--通过groupid和阶级获取某一条羁绊
function _M.GetPartnerEntanglementByGroupAndRank(groupid,rank)
	local pe=unpack(GlobalHooks.DB.Find('FallenPartnerEntanglement',function (pe)
		return pe.group_id == groupid and pe.client_rank == rank
	end))
	return pe
end

--获取一共几种组合
function _M.GetTotalPartnerEntanglementCount()
	local all = GlobalHooks.DB.GetFullTable('FallenPartnerEntanglement')
	local finish = all[#all]
	local temp={}
	for i = 1, finish.group_id do
		local a = _M.GetPartnerEntanglementByGroupAndRank(i,1)
		table.insert(temp,a)
	end
	return temp
end

function _M.GetMaxCountByGroupId(groupid)
	local pe = GlobalHooks.DB.Find('FallenPartnerEntanglement',{group_id = groupid})
	return pe
end

--请求激活的仙侣羁绊
function _M.RequestGetGodBookInfo(cb)
	Protocol.RequestHandler.ClientGetGodBookInfoRequest({}, function(rsp)
		if cb then
			cb(rsp) 
		end
	end)
end

function _M.GetPartnerInfoById(id)
	local par=unpack(GlobalHooks.DB.Find('FallenPartnerListData',{god_id = id }))
	return par
end


return _M
