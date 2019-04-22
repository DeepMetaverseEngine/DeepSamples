local _M = {}
_M.__index = _M


_M.ReliveType = {
  None = 0,         --无
  RebirthPoint = 1,   --就近复活点复活
  Insitu = 2,    --原地复活
}


--获取所有复活数据
local function GetAllReliveData() 
    local allReliveData=GlobalHooks.DB.GetFullTable('ReliveData')
    return allReliveData
end


--通过复活ID，获取当前行所有数据
local function GetReliveType(reliveId) 
    local reliveType=unpack(GlobalHooks.DB.Find('ReliveData',{id=reliveId})) 
    return reliveType
end


local function CloseUI(eventname,params)
    if params.uid == DataMgr.Instance.UserData.RoleID then
        GlobalHooks.UI.CloseUIByTag('ReliveMain')
        GlobalHooks.UI.CloseUIByTag('ReliveWithStrongMain')
    end
end


----------------------Net--------------------------

--发送复活类型，请求复活
local function RequestRelive(reliveType, cb)
    local request = { type = reliveType }
        Protocol.RequestHandler.ClientRebirthRequest(request, function(rsp)
        if rsp:IsSuccess() and cb then
            cb(rsp)
        end
    end)
end


--接受服务器推过来的复活数据
local function OnServerPlayerRebirthNotfy(notify)

    local params={rebirthType=notify.s2c_rebirthType,
                  descStr=notify.s2c_descStr,
                  leftRebirthTimes=notify.s2c_btn_1_RecordTimes,
                  leftRebirthTimeStamp=notify.s2c_btn_1_timeStamp,
                  rightRebirthTimes=notify.s2c_btn_2_RecordTimes,
                  rightRebirthTimeStamp=notify.s2c_btn_2_timeStamp}

    --通过复活id，取得表中is_support_strong属性，判断打开哪种UI
    local relive=GetReliveType(params.rebirthType)
    MenuMgr.Instance:CloseAllMenu()
    if relive.is_support_strong == 1 then
        GlobalHooks.UI.OpenHud('ReliveWithStrongMain',0,params)
    else
        GlobalHooks.UI.OpenHud('ReliveMain',0,params)
    end
end


function _M.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.ServerPlayerRebirthNotfy(OnServerPlayerRebirthNotfy)
    end
end


--添加监听
local function initial()
    EventManager.Subscribe("Event.PlayerRebirth",CloseUI)
end


--注销监听
local function fin()
    EventManager.Unsubscribe("Event.PlayerRebirth",CloseUI)
end


_M.initial=initial
_M.fin=fin
_M.GetReliveType=GetReliveType
_M.GetAllReliveData=GetAllReliveData
_M.RequestRelive=RequestRelive


return _M