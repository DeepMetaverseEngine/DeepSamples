--上线之后需要推送的信息可以统一写在这里(离线经验，充值活动等)

local _M = {}
_M.__index = _M


--接受服务器推过来的离线经验数据
local function OnOffLineExpNotify(notify)
	params={exp=notify.c2s_exp,
			      time=notify.c2s_time,
                  vipexp=notify.c2s_extra_exp}
	GlobalHooks.UI.OpenMsgBox('OffLineExp',0,params)
end


--师门/仙盟任务是否继续
local function OnQuestGoOnNotify(notify)
    params={type=notify.s2c_type,
                  curTurn=notify.s2c_curTurn}
    if params.type == 1000 or params.type == 1001 then
        GlobalHooks.UI.OpenUI('TaskLoop',0,params)
    end
end


function _M.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.ClientOfflineExpNotify(OnOffLineExpNotify)--监听离线经验消息
        Protocol.PushHandler.ClientQuestGoOnTipsNotify(OnQuestGoOnNotify)--监听循环任务推送
    end
end


return _M