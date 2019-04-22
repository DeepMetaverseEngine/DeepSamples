---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by xujing.xu.
--- DateTime: 2019/1/9 18:21
---天降财神

local _M = {}
_M.__index = _M

local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local ActivityModel = require('Model/ActivityModel')
local Util = require 'Logic/Util'


function _M.OnInit(self)
    self.fastTime = 20
    self.midTime = 100
    self.slowTime = 150
end

--                                                  起始/结束/一圈几个/几圈
local function PlayTurnTable(self,startpos,endpos,max,cycle,cb)
    local step = startpos
    self.playTime = LuaTimer.Add(0,self.fastTime,
            function()
                self.turnTable[startpos].IsChecked =true
                startpos = startpos + 1
                step = step +1
                startpos = startpos % max == 0 and max or startpos % max
                if step > (cycle - 2)*max + endpos then
                    LuaTimer.Delete(self.playTime)
                    self.playTime = nil
                    self.playTime = LuaTimer.Add(self.fastTime,self.midTime,
                            function()
                                self.turnTable[startpos].IsChecked =true
                                startpos = startpos + 1
                                step = step +1
                                startpos = startpos % max == 0 and max or startpos % max
                                if step > (cycle - 1)*max + endpos then
                                    LuaTimer.Delete(self.playTime)
                                    self.playTime = nil
                                    self.playTime = LuaTimer.Add(self.midTime,self.slowTime,
                                            function()
                                                self.turnTable[startpos].IsChecked =true
                                                startpos = startpos + 1
                                                step = step +1
                                                startpos = startpos % max == 0 and max or startpos % max
                                                if step > cycle*max+endpos then
                                                    if cb then
                                                        cb()
                                                    end
                                                    return false
                                                end
                                                return true
                                            end)
                                    return false
                                end
                                return true
                            end)
                    return false
                end
                return true
            end)
end

--点完刷新数据
local function UpDateTurn(self)
    for i, v in ipairs(self.turnTable) do
        v.Text = self.turnInfo.item_show_num[i]
    end
    self.comps.lb_costnum.Text = self.turnInfo.cost_num
    self.comps.lb_desc2.Text = Util.GetText(Constants.OpeningEvent.TurnTime,self.turnInfo.cost_num)
    self.comps.lb_numoftimes.Text ='('..Util.GetText(Constants.OpeningEvent.TurnTimeLeft,self.totalTime - self.time)..')'
end

--设置可点/不可点
local function EnableAndGrayBtn(self,onoroff)
    self.comps.btn_investment.Enable = onoroff
    self.comps.btn_investment.IsGray = not onoroff
end

local function InitBtn(self)
    self.endpos = 1
    EnableAndGrayBtn(self,true)
    self.comps.btn_investment.TouchClick = function()
        --次数用完，给提示
        if self.time >= self.totalTime then
            GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.OpeningEvent.TurnTimeMax))
            return
        end
        --钱不够，弹框去充值
        if self.turnInfo.cost_num > ItemModel.CountItemByTemplateID(self.turnInfo.cost_id) then
            FunctionUtil.Goto('not_enough_gold')
            return
        end

        EnableAndGrayBtn(self,false)
        ActivityModel.RequestGetTurnTableReward(self.turnType,function (rsp)
            local startpos = self.endpos
            for i, v in ipairs(self.turnInfo.item_show_num) do
                if v == rsp.s2c_data[1].num then
                    self.endpos = i
                    break
                end
            end

            self.time = rsp.s2c_times - 1
            self.turnInfo = ActivityModel.GetTurnTableInfoByTime(self.turnType,rsp.s2c_times >= self.totalTime and self.totalTime or rsp.s2c_times)
            
            self.comps.cvs_mask.Visible = true
            PlayTurnTable(self,startpos,self.endpos,#self.turnTable,math.random(3,5),function ()
                self.updateTime = LuaTimer.Add(
                        1000,
                        function()
                            --重组掉落
                            local drop ={}
                            table.insert(drop,{id = rsp.s2c_data[1].templateid,num = rsp.s2c_data[1].num})
                            self.comps.cvs_mask.Visible = false
                            GlobalHooks.UI.OpenUI('GainItem',0,drop)
                            EnableAndGrayBtn(self,true)
                            self.turnTable[self.endpos].IsChecked =true
                            --刷新信息
                            UpDateTurn(self)
                            if self.updateTime then
                                LuaTimer.Delete(self.updateTime)
                                self.updateTime = nil
                            end
                        end)
            end)
        end)
    end
end


local function InitTurn(self)
    --转盘合集
    self.turnTable={}
    for i = 1, 8 do
        local turn= self.comps.cvs_lunpan:FindChildByEditName('tbt_'..i,true)
        turn.UserTag = i
        turn.Enable =false
        turn.Text = self.turnInfo.item_show_num[i]
        table.insert(self.turnTable,turn)
    end
    --初始化信息
    self.comps.lb_costnum.Text = self.turnInfo.cost_num
    self.comps.lb_desc2.Text = Util.GetText(Constants.OpeningEvent.TurnTime,self.turnInfo.cost_num)
    self.comps.lb_numoftimes.Text = '('..Util.GetText(Constants.OpeningEvent.TurnTimeLeft,self.totalTime - self.time)..')'
--[[    local time = DataMgr.Instance.UserData.Serverinfo.open_at:AddDays(GameUtil.GetIntGameConfig("openmaxday"))
            - ServerTime.getServerTime():ToLocalTime()
    self.comps.lb_acttime.Text = TimeUtil.FormatToAllCN2(time.TotalSeconds)]]
    local cost = ItemModel.GetDetailByTemplateID(self.turnInfo.cost_id)
    UIUtil.SetImage(self.comps.ib_numicon,'static/item/'..cost.static.atlas_id,false,UILayoutStyle.IMAGE_STYLE_BACK_4)

    UIUtil.ConfigToggleButton(self.turnTable,self.turnTable[1],false,TogBtn)
end

function _M.OnEnter(self,params)
    self.turnType = params or 16001
    self.comps.cvs_mask.Visible = false
    ActivityModel.RequestGetTurnTableInfo(self.turnType,function (rsp)
        self.totalTime = ActivityModel.GetTotalTime(self.turnType)
        --rsp.s2c_times 下一次的次数
        --self.time 已经抽奖的次数
        self.time = rsp.s2c_times - 1
        self.turnInfo = ActivityModel.GetTurnTableInfoByTime(self.turnType,rsp.s2c_times >= self.totalTime and self.totalTime or rsp.s2c_times)
        InitTurn(self)
        InitBtn(self)
    end)
end

function _M.OnExit(self)
    if self.playTime then
        LuaTimer.Delete(self.playTime)
        self.playTime = nil
    end
    if self.updateTime then
        LuaTimer.Delete(self.updateTime)
        self.updateTime = nil
    end
end


return _M