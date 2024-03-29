---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by xujing.xu.
--- DateTime: 2019/1/16 17:13
---春节活动
local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local ServerTime = require 'Logic/ServerTime'
local ItemModel = require 'Model/ItemModel'


_M.SpringFestivalOpenTime= GameUtil.GetStringGameConfig("SpringFestivalOpenTime")
_M.SpringFestivalCloseTime = GameUtil.GetStringGameConfig("SpringFestivalCloseTime")

--通过Id查找信息
function _M.GetActInfoById(id)
    local act = unpack(GlobalHooks.DB.Find('SpringFestivalInfo',{id = id}))
    return act
end
--通过活动Id查找信息
function _M.GetActInfoByActivityId(activityid)
    local act = unpack(GlobalHooks.DB.Find('SpringFestivalInfo',{activity_id = activityid}))
    return act
end
function _M.GetAllSpringAct()
    local all = GlobalHooks.DB.GetFullTable('SpringFestivalInfo')
    return all
end
function _M.GetSpringActBySheetName(name)
    local all = GlobalHooks.DB.GetFullTable(name)
    return all
end
--通过tag来查找活动信息
function _M.GetActInfoByTag(tag)
    local act = unpack(GlobalHooks.DB.Find('SpringFestivalInfo',{sheet_name = tag}))
    return act
end
--设置帮助界面
function _M.SetHelpCvsVisibleOrInvisible(btn,cvs)
    btn.TouchClick=function()
        cvs.Visible = true
        cvs.TouchClick =function()
            cvs.Visible = false
        end
    end
end
--设置活动时间
function _M.SetOpeningTime(node,startTime,endTime)
    local starttime = System.DateTime.Parse(startTime)
    local endtime = System.DateTime.Parse(endTime)
    node.Text = Util.GetText(Constants.SpringFestival.MonthDayTime,
            starttime.Month,starttime.Day,starttime.Hour..':'..(starttime.Minute == 0 and '00' or starttime.Minute))..'-'..
            Util.GetText(Constants.SpringFestival.MonthDayTime,
                    endtime.Month,endtime.Day,endtime.Hour..':'..(endtime.Minute == 0 and '00' or endtime.Minute))
end
--播放全屏烟花/金币雨特效
--'/res/effect/ui/ef_ui_rain_gold.assetbundles'
--'/res/effect/ui/ef_ui_chunjie01.assetbundles'
function _M.PlayFullScreenEffect(res,order)
    local transSet = TransformSet()
    transSet.Layer = Constants.Layer.UI
    transSet.LayerOrder = order
    transSet.Pos= Vector3(0,200,0)
    local  eff = RenderSystem.Instance:PlayEffect(res,transSet,0,function(...)
    end)
    return eff
end

function _M.GetSpringFestivalPacketsInfo(count)
    local all = GlobalHooks.DB.GetFullTable('packets')
    for i, v in ipairs(all) do
        if v.consume_min <= count and v.consume_max >=count then
            return all[i]
        end
    end
    return all[1]
end
--检查单个活动是否开放(false为已结束,true为开启中)
function _M.CheckIsOpening(endTime)
    local time = System.DateTime.Parse(endTime)
    local now = ServerTime.getServerTime():ToLocalTime()
    return time > now
end

-------------------------------Net---------------------------------

--请求新春祈福信息
function _M.RequestSpringFestivalPrayInfo(cb)
    Protocol.RequestHandler.ClientGetSFXCQFInfoRequest({},function (rsp)
        if cb then
            cb(rsp)
        end
    end)
end
--请求新春祈福奖励
function _M.RequestSpringFestivalPrayReward(cb)
    Protocol.RequestHandler.ClientGetSFXCQFRewardRequest({},function (rsp)
        if cb then
            cb(rsp)
        end
    end)
end
--请求红包信息
function _M.RequestSpringFestivalPacketsInfo(cb)
    Protocol.RequestHandler.ClientGetSFHBInfoRequest({},function (rsp)
        if cb then
            cb(rsp)
        end
    end)
end
--迎财神
function _M.RequestSpringFestivalCaishenInfo(type,index,cb)
    local msg = {c2s_type=type,c2s_index=index}
    Protocol.RequestHandler.TLClientExchargeTurnTableRequest(msg,function (rsp)
        if cb then
            cb(rsp)
        end
    end)
end


---------------------------------------------------------------------
--财神记录板
_M.RewardRecord = {}
local function OnClientMessageContentNotify( notify )
    --2019/01/23 16:54 notify.s2c_noticeid 不会报空   by老蔡
    if notify.s2c_type ~= 3 or notify.s2c_noticeid ~= 154 then
        return
    end
    
    local args = {}
    if notify.s2c_args then
        for _, val in ipairs(notify.s2c_args) do
            table.insert(args, Util.GetText(val))
        end
    end
    local content = Util.GetText(notify.s2c_data, unpack(args))
    if #_M.RewardRecord < 20 then
        table.insert(_M.RewardRecord,content)
    else
        table.remove(_M.RewardRecord,1)
        table.insert(_M.RewardRecord,content)
    end
    EventManager.Fire("Event.SpringFestival.UpdateRecord", {})
end


function _M.InitNetWork(initNotify)
    if initNotify then
        Protocol.PushHandler.ClientMessageContentNotify(OnClientMessageContentNotify)
    end
end

function _M.initial()
    if not GameGlobal.Instance.netMode then
        return
    end
    local info = _M.GetSpringActBySheetName('pray')
    _M.RequestSpringFestivalPrayInfo(function (rsp)
        if rsp.s2c_times > 0 and ItemModel.CountItemByTemplateID(info[1].cost.id[1]) > 0 then
            GlobalHooks.UI.SetRedTips("springfestival",1)
        else
            GlobalHooks.UI.SetRedTips("springfestival",0)
        end
    end)
end

function _M.fin()

end

return _M