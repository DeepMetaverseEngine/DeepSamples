local _M = {}
_M.__index = _M

local Util =require('Logic/Util')

--通过副本ID获取单条数据
local function GetDungeonDataById(dungeonId)
	local dungeonData=unpack(GlobalHooks.DB.Find('DungeonData',{id=dungeonId}))
    return dungeonData
end


--获取所有副本数据,并返回数据的长度
local function GetAllDungeonData()
	local allDungeoData=GlobalHooks.DB.GetFullTable('DungeonData')
    local length=GlobalHooks.DB.Find('DungeonData',{did=id})
    table.sort(allDungeoData,function(a,b) return a.order<b.order end)
	return allDungeoData,#length
end


--通过副本ID获取地图类型(2-单人副本，3-组队副本，4-镇妖塔,5-仙灵岛,13-神兽陪练,14-核心产出副本,20-双飞塔)
--获取是否显示倒计时(0-不显示，1-显示)
--获取打开某种类型UI
--获取是否显示伤害统计
local function GetMapType(mapId)
    local mapData=unpack(GlobalHooks.DB.Find('MapData',{id=mapId}))
      if mapData==nil then 
          print("mapdata error with id = ",mapId)
        return nil,nil,nil,nil
      end
     return mapData.type,mapData.is_countdown,mapData.uitag,mapData.is_damagerank
end

--获取某一难度所有的副本数据
function _M.GetDungeonDataByLevel(lv)
    local mapData = GlobalHooks.DB.Find('DungeonData',function (fb)
        return fb.sheet == lv
    end)
    table.sort(mapData,function (a,b)
        return a.order < b.order
    end)
    return mapData
end

function _M.GetTargetId(mapid)
    local target = unpack(GlobalHooks.DB.Find('team_target', {mapid = mapid}))
    if target then
        return target.id
    end
    return nil
end

------------------------------Net---------------------------------


--通过mapId，请求进入副本
local function RequestEnterDungeon (mapId,cb)

  local request ={}
    if type(mapId) =='number' then
       request= {c2s_MapId=mapId,c2s_FuncId=''}
    elseif type(mapId) =='string' then
       request= {c2s_FuncId=mapId,c2s_MapId=0}
    end
  Protocol.RequestHandler.ClientEnterDungeonRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end


--请求副本奖励次数
local function RequestDungeonBounsCount (cb)
	  local request = {}
      Protocol.RequestHandler.ClientGetTeamDungeonTicketsRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end


--请求离开副本
local function RequestLeaveDungeon (cb)
    local request = {}
      Protocol.RequestHandler.ClientLeaveDungeonRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end


--离开副本弹出二次确认框
local function ShowExitConfirmTips()
    local mapdata=unpack(GlobalHooks.DB.Find('MapData',{id=DataMgr.Instance.UserData.MapTemplateId}))
    local content=mapdata.exit_notice
    local ui =mapdata.uitag
    GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, Util.GetText(content),'','',nil,
            function ()
                RequestLeaveDungeon(function(resp)
                    GlobalHooks.UI.CloseUIByTag(ui)
                end)
            end
    ,nil)
end

--添加Notify监听事件，弹出结算界面
local function OnClientSettlementNotify(notify)
  
    local params = {exp=notify.s2c_exp,
                    gold=notify.s2c_gold,
                    counttime=notify.s2c_counttime,
                    noAward=notify.s2c_noAward,
                    status=notify.s2c_status,
                    itemList=notify.s2c_itemList,
                    finishtime=notify.s2c_finishtime_sec,
                    extramap=notify.s2c_ext,
                    cb=function()
                        RequestLeaveDungeon(function(resp)
                      end)
                    end}

    --弹结算时，发送通知
    EventManager.Fire('Event.UI.NotifyResult',params)
    
    if params.extramap then
        if params.extramap['passlayer'] then --双飞塔专用结算
            GlobalHooks.UI.OpenUI('DoubleFlyResult',0,params)
        else--核心本/神兽陪练结算界面
          local source = {tag = 'DungeonResult',info = {'UI/Dungeon/DungeonResult',params.extramap.ui}}
          GlobalHooks.UI.OpenUI(source,0,params)
        end
    else
        if notify.s2c_status==0 then 
          GlobalHooks.UI.OpenUI('DungeonFail',0,params)
        else
          GlobalHooks.UI.OpenUI('DungeonPass',0,params)
        end
    end
end


--扫荡结算界面
local function OnClientSweepNotify(notify)
    params={
        drop=notify.c2s_data
    }
    GlobalHooks.UI.OpenUI('GainItem',0,params.drop,5)
end


--初始化网络，监听结算事件
function _M.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.ClientSettlementNotify(OnClientSettlementNotify)
        Protocol.PushHandler.TLClientSweepNotify(OnClientSweepNotify)
    end
end


--判断条件，是否需要初始化离开按钮
local function InitDunAndPagDetail()
    local mapData={}
    local mapType
    local matpIsTime
    local mapUItag=nil
    local mapDam = 0
    
    --通过mapId获取类型，是否显示倒计时，tag  
    mapType,matpIsTime,mapUItag,mapDam = GetMapType(DataMgr.Instance.UserData.MapTemplateId)    
    
    mapData.mapType=mapType
    mapData.isTime=matpIsTime

    --通过表里的tag打开不同的ui
    if mapUItag ==nil or mapUItag =='' then
        return
    else
        GlobalHooks.UI.OpenHud(mapUItag,-1,mapData)
    end

    if mapDam ~= 0 then
        GlobalHooks.UI.OpenHud('DamageRecount',0)
    end
end


--添加监听
local function initial()
   EventManager.Subscribe("Event.Scene.ChangeFinish",InitDunAndPagDetail)
end


--注销监听
local function fin()
    EventManager.Unsubscribe("Event.Scene.ChangeFinish",InitDunAndPagDetail)
end


_M.initial=initial
_M.fin=fin
_M.GetMapType=GetMapType
_M.RequestLeaveDungeon=RequestLeaveDungeon
_M.ShowExitConfirmTips=ShowExitConfirmTips
_M.RequestDungeonBounsCount=RequestDungeonBounsCount
_M.RequestEnterDungeon=RequestEnterDungeon
_M.GetDungeonDataById=GetDungeonDataById
_M.GetAllDungeonData=GetAllDungeonData

return _M