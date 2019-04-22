local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local BusinessModel = require 'Model/BusinessModel'


local function BuildSortData(self)
    table.sort(self.serverdata,function(a,b)
        if a.state ~= 1 and b.state == 1 then
            return false
        elseif a.state == 1 and b.state ~= 1 then
            return true
        elseif a.requireList[2].curVal > a.requireList[2].maxVal and b.requireList[2].curVal <= b.requireList[2].maxVal then
            return false
        elseif a.requireList[2].curVal <= a.requireList[2].maxVal and b.requireList[2].curVal > b.requireList[2].maxVal then
            return true
        else
            return a.id<b.id
        end
    end)
end

local function InitList(self,node,index,data)
    local lb_cost = node:FindChildByEditName('lb_cost', true)
    local btn_get = node:FindChildByEditName('btn_get', true)
    local lb_desc = node:FindChildByEditName('lb_desc', true)
    local cvs_showreward = {}
    local lb_count = node:FindChildByEditName('lb_count', true)
    for i = 1, 4 do
        cvs_showreward[i] = node:FindChildByEditName('cvs_showreward'..i, true)
        if self.serverdata[index].staticdata.show.item[i] then
            cvs_showreward[i].Visible = true
            UIUtil.SetItemShowTo(cvs_showreward[i],self.serverdata[index].staticdata.show.item[i],self.serverdata[index].staticdata.show.itemnum[i])
            cvs_showreward[i].TouchClick = function(sender)
                UIUtil.ShowTips(self,sender,self.serverdata[index].staticdata.show.item[i])
            end
        else
            cvs_showreward[i].Visible = false
        end
    end

    lb_cost.Text = Util.GetText(self.serverdata[index].staticdata.lbtext)
    lb_count.Text = tostring(self.serverdata[index].requireList[2].curVal).."/"..tostring(self.serverdata[index].requireList[2].maxVal+1)

    lb_desc.Text = Util.GetText(self.serverdata[index].staticdata.counttext)
    cvs_showreward.Visible = false
    btn_get.Enable = self.serverdata[index].state == 1
    btn_get.IsGray = self.serverdata[index].state ~= 1

    btn_get.TouchClick = function(sender)
        if self.serverdata[index].state == 1 then
            BusinessModel.RequireGet(self.activitytype,self.activityid ,self.serverdata[index].id,function()
                BusinessModel.RequireData(self.activitytype,self.activityid,function(rsp)
                    local temptable = {}
                    for _,v in pairs(rsp.activityMap) do
                        table.insert(temptable,v)
                    end
                    table.sort(temptable,function( a,b)
                        return a.id < b.id
                    end)
                    for i1, v1 in ipairs(self.staticdata) do
                        for i2, v2 in ipairs(v1.show.item) do
                            if v2 == 0 then
                                v2 = nil
                                break
                            end
                        end
                    end
                    for i, v in pairs(temptable) do
                        v.staticdata = self.staticdata[i]
                    end
                    self.serverdata = temptable
                    BuildSortData(self)
                    self.lb_jifennum.Text =self.serverdata[1].requireList[1].curVal
                    self.sp_list:RefreshShowCell()
                    self.parentui.pan:RefreshShowCell()
                end)
            end,self)
        end
    end
    
end

local function StartSetUI(self)
    
    UIUtil.ConfigVScrollPan(self.sp_list,self.cvs_reward,#self.serverdata,function (node,index)
        InitList(self,node,index,self.serverdata[index])
    end)
    self.cvs_reward.Visible = false
end

function _M.OnEnter( self,activityid,subindex )
    self.activityid = activityid
    BusinessModel.RequireData(self.activitytype,self.activityid,function(rsp)
        local temptable = {}
        for _,v in pairs(rsp.activityMap) do
            table.insert(temptable,v)
        end
        table.sort(temptable,function( a,b)
            return a.id < b.id
        end)
        for i1, v1 in ipairs(self.staticdata) do
            for i2, v2 in ipairs(v1.show.item) do
                if v2 == 0 then
                    v2 = nil
                    break
                end
            end
        end
        for i, v in pairs(temptable) do
            v.staticdata = self.staticdata[i]
        end
        self.serverdata = temptable
        BuildSortData(self)
        self.lb_jifennum.Text =self.serverdata[1].requireList[1].curVal
        StartSetUI(self)
    end)

end

function _M.OnInit( self,params )
    self.activitytype = params.activitytype
    self.staticdata = params
    
    self.cvs_info = self.root:FindChildByEditName('cvs_info', true)
    self.sp_list = self.root:FindChildByEditName('sp_list', true)
    self.cvs_reward = self.root:FindChildByEditName('cvs_reward', true)
    
    
    self.lb_time = self.cvs_info:FindChildByEditName('lb_time', true)
    self.lb_jifennum = self.cvs_info:FindChildByEditName('lb_jifennum', true)
end

function _M.OnExit( self )
    
end

return _M