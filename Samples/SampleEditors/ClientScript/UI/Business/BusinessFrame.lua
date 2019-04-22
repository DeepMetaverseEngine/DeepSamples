local _M = {}
_M.__index = _M


local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local BusinessModel = require 'Model/BusinessModel'
local DisplayUtil = require"Logic/DisplayUtil"


local CheckedID = nil
local GotoKey = nil
local ShowActivityInfo = nil


local function RefreshTime(self)
    local time = ShowActivityInfo.last_time*86400 - TimeUtil.TimeLeftSec(BusinessModel.GetServerOpenTime())
    self.lastUI.isopen = ShowActivityInfo.last_time == 0 and true or time>0
    if self.lastUI.lb_num then
        if self.lastUI.isopen then
            self.lastUI.lb_num.Text = TimeUtil.FormatToAllCN(time)--TimeUtil.FormatToCN(time)
        else
            self.lastUI.lb_num.Text = Util.GetText("common_end")
        end
    end
end

local function ShowRightCanvas(self,data)
    if self.lastUI then
        self.lastUI:Close()
        self.lastUI = nil
        ShowActivityInfo = nil
    end
    self.activityinfo = {}
    if data.sheet_name then
        local temp = BusinessModel.GetCommonActivity(data.sheet_name)
        self.activityinfo = temp or {}
        if self.activityinfo then
            self.activityinfo.sheet_name = data.sheet_name
        end
    end
    
    self.activityinfo.activitytype = self.activitytype
    local source = {tag = data.sheet_name ,info = {'UI/Business/ActivityType_'..data.client_type ,data.client_xml,self.activityinfo}}
    --用以区分新服累充和福利中心累充
    if source.tag == 'pay' then
        self.lastUI = GlobalHooks.UI.CreateUI(source ,-1,data.activity_id,self.subindex)
    else
        self.lastUI = GlobalHooks.UI.CreateUI(source ,0,data.activity_id,self.subindex)
    end
    self.subindex = nil
    self.lastUI.ui.menu.Enable = false
    self.lastUI.parentui = self
    ShowActivityInfo = data
    self.ui:AddSubUI(self.lastUI)
    self.lastUI.lb_num = self.lastUI.root:FindChildByEditName('lb_num', true)
    RefreshTime(self)
end

local function InitList(self,node, data)
    local tbn_program = node:FindChildByEditName('tbn_program', true)
    local ib_hongdian = node:FindChildByEditName('ib_hongdian', true)
    
    if BusinessModel.cachedata[self.activitytype] and BusinessModel.cachedata[self.activitytype][data.activity_id] then
        ib_hongdian.Visible = BusinessModel.cachedata[self.activitytype][data.activity_id].count > 0
        if data.activity_id == 11 then
            ib_hongdian.Visible = BusinessModel.cachedata[self.activitytype][11].count+BusinessModel.cachedata[self.activitytype][13].count > 0
        end
    else
        ib_hongdian.Visible = false
    end
    
    tbn_program.IsChecked = CheckedID == data.activity_id
    tbn_program.Text = Util.GetText(data.activity_name)
    
    tbn_program.TouchClick = function(sender)
        sender.IsChecked = true
        if CheckedID ~= data.activity_id then
            CheckedID = data.activity_id
            self.pan:RefreshShowCell()
            ShowRightCanvas(self,data)
        end
    end
end

local function ParserAccount(str)
    local src = string.split(str, ':')
    local ret = src[2]
    return ret
end

local function RefreshList(self)
    BusinessModel.FirstRequire(self.activitytype,function()
        self.allactivity = BusinessModel.GetAllActivity(self.activitytype,true)
        table.sort(self.allactivity,function( a,b)
            return a.order < b.order
        end)
        for i, v in pairs(self.allactivity) do
            if v.activity_id == 13 then
                table.remove(self.allactivity,i)
                break
            end
        end
        
        ---------------------------------------------------------------------------------------------------------
        --客户端判断特定获得是否显示 
        ---------------------------------------------------------------------------------------------------------
        local serverID =  DataMgr.Instance.UserData.ServerID
        --print("cur serverID = "..serverID)
        --print_r("activity = ",self.allactivity)
        local cfgData = unpack(GlobalHooks.DB.Find('return_config',{id = 1}))
        local joincfgData = unpack(GlobalHooks.DB.Find('join_config',{id = 1}))
        local remove20001 = false
        local remove20002 = false
        
        local removelist = {}
        
        if serverID ~= cfgData.server_id then ---1
                 for i, v in pairs(self.allactivity) do
                        if v.activity_id == 20001 then
                            table.insert(removelist,v)
                            remove20001 = true
                            print("activity 20001 in removeList serverID wrong")
                        end
                 end
        end
        
        if serverID ~= joincfgData.server_id then
                 for i, v in pairs(self.allactivity) do
                        if v.activity_id == 20002 then
                            table.insert(removelist,v)
                            remove20002 = true
                            print("activity 20002 in removeList serverID wrong")
                        end
                 end
        end
        
         if remove20001 == false or remove20002 == false then

                local accountID = DataMgr.Instance.UserData.AccountID
                --print("cur accountID = ",accountID)
                local account = ParserAccount(accountID)
                --print("parser account = "..account)
                for i, v in pairs(self.allactivity) do
               
                   if v.activity_id == 20001 then--ID 20001 充值返利活动 1服开启 只有CCB1,2充值玩家可见
                    --判断资格    
                        --print("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa = "..v.activity_id)
                        local rechargeData=unpack(GlobalHooks.DB.Find('return_content',{platform_account = account}))
                        --print("cfgData = "..cfgData.record_flag)
                        --print("rechargeData"..rechargeData.price)
                        local canreward = false

                        if cfgData ~= nil then
                            local v = DataMgr.Instance.UserData:GetFreeData(cfgData.record_flag)
                            if v == nil or v == "" then
                                canreward = true
                                print("acitivity 20001 can reward",canreward)
                            end
                        end
                        
                        if rechargeData ~= nil and canreward == true then
                            remove20001 = false
                        else
                            remove20001 = true 
                        end

                        --判断是否已领取.
                    elseif v.activity_id == 20002 then--ID 20002 参与奖励 1服开启 只有CCB1,2玩家可见
                    --判断资格
                        local canreward = false
                        if joincfgData ~= nil then
                            local v = DataMgr.Instance.UserData:GetFreeData(joincfgData.record_flag)
                            if v == nil or v == "" then
                                --print("acitivity 20002 can reward",canreward)
                                canreward = true
                            end
                        end

                        if canreward == true then
                            local joinData = unpack(GlobalHooks.DB.Find('join_content_cb1',{platform_account = account}))--CB1
                            if joinData ~= nil then
                                remove20002 = false
                                print("acitivity 20002 find cb1 joinData")
                            else
                                joinData = unpack(GlobalHooks.DB.Find('join_content_cb2',{platform_account = account}))--CB2
                                if joinData ~= nil then
                                    remove20002 = false
                                    print("acitivity 20002 find cb2 joinData")
                                else
                                    remove20002 = true
                                end
                            end
                        else
                            remove20002 = true
                        end
                    end
                end

                --删除活动列表中的获得.
                 for i, v in pairs(self.allactivity) do
                        if remove20001 == true and v.activity_id == 20001 then
                            table.insert(removelist,v)
                            print("activity 20001 in removeList")
                        elseif remove20002 == true and v.activity_id == 20002 then
                            table.insert(removelist,v)
                            print("activity 20002 in removeList")
                        end
                 end

              
        end
        for i,v in ipairs(removelist) do
                table.removeItem(self.allactivity,v)
        end
        ---------------------------------------------------------------------------------------------------------

        ---------------------------------------------------------------------------------------------------------

        local gotoindex = 1
        if not string.IsNullOrEmpty(GotoKey) then
            local havegoto = false
            for i, v in pairs(self.allactivity) do
                if GotoKey == v.goto_key then
                    gotoindex = i
                    CheckedID = v.activity_id
                    GotoKey = nil
                    havegoto = true
                    break
                end
            end
            if not havegoto then
                GameAlertManager.Instance:ShowNotify(Util.GetText('SpringFestival_ActivityEnd'))
            end
        else
            CheckedID = self.allactivity[1] and self.allactivity[1].activity_id or nil
        end

        
        UIUtil.ConfigVScrollPan(self.pan,self.tempnode, #self.allactivity, function(node, index)
            InitList(self,node, self.allactivity[index])
        end)
        if #self.allactivity > 0 then
            ShowRightCanvas(self,self.allactivity[gotoindex])
            DisplayUtil.lookAt(self.pan, gotoindex)
        end
    end)
end

function _M.OnEnter(self , gotokey, subindex)
    self.RefreshList = RefreshList
    self.subindex = subindex
    GotoKey = gotokey
    if self.timer then
        LuaTimer.Delete(self.timer)
        self.timer = nil
    end
    --ShowActivityInfo.last_time*86400
    self.timer = LuaTimer.Add(0,1000, function()
        if self.lastUI then
            RefreshTime(self)
        end
        return true
    end)

    RefreshList(self)

    --SoundManager.Instance:PlaySoundByKey('uiopen',false)
    -- UnityEngine.Debug.LogError(string.format("no itemsource data %d", 1))
end

function _M.OnInit(self,param)
    self.activitytype = param

    self.tempnode = UIUtil.FindChild(self.ui.comps.cvs_list, 'cvs_program', true)
    self.pan = UIUtil.FindChild(self.ui.comps.cvs_list, 'sp_list', true)
    
    self.lb_num = UIUtil.FindChild(self.root, 'lb_num', true)
    
    self.tempnode.Visible = false

    self.ui.menu.ShowType = UIShowType.HideHudAndMenu
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end

function _M.OnExit( self )
    --if self.timer then
    --    LuaTimer.Delete(self.timer)
    --    self.timer = nil
    --end
    self.lastUI = nil
    GotoKey = nil
    ShowActivityInfo = nil
end

return _M


-- ActivityMain = {'UI/Business/BusinessFrame','xml/business/ui_business_welfare.gui.xml'},