local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local DungeonModel= require 'Model/DungeonModel'
local Util   = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'
local TeamModel = require 'Model/TeamModel'

_M.DungeonLevel={
    Common = 1,
    Hero = 2,
}

local btn_myteam

function _M.OnInit(self)
    btn_myteam = self.comps.btn_quicksuit
end


--通过组队监听推送，显示按钮文字(我的队伍/快速匹配)
function _M.Notify(status,teamdata,opt)
    if teamdata.HasTeam then
        btn_myteam.Text=Constants.Dungeon.MyTeam
        btn_myteam.TouchClick=function (sender)
            GlobalHooks.UI.OpenUI('TeamFrame',0,'TeamInfo')
        end
    else
        btn_myteam.Text=Constants.Dungeon.QuickMatching
        btn_myteam.TouchClick=function (sender)
            GlobalHooks.UI.OpenUI('TeamFrame',0,'TeamPlatform')
        end
    end
end

local function UpDateItem(self,node,index)
    local itemdetail=ItemModel.GetDetailByTemplateID(tonumber(self.dungeonData[self.selectIndex].awardshow.id[index]))
    local itshow=UIUtil.SetItemShowTo(node,itemdetail,1)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
        UIUtil.ShowNormalItemDetail({detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})
    end
end

local function InitDetail(self)
    self.comps.lb_name.Text = Util.GetText(self.dungeonData[self.selectIndex].dungen_name)
    self.comps.lb_normal.Visible = self.dungeonLevel == _M.DungeonLevel.Common
    self.comps.lb_hero.Visible = self.dungeonLevel == _M.DungeonLevel.Hero
    self.comps.tb_introduce.Text = Util.GetText(self.dungeonData[self.selectIndex].dungeon_desc)
    
    self.comps.lb_person.Text = Util.GetText(self.dungeonData[self.selectIndex].num_desc)
    self.comps.lb_fight.Text = self.dungeonData[self.selectIndex].dungeon_fight
    UIUtil.SetImage(self.comps.ib_img,self.dungeonData[self.selectIndex].pic_res,false,UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)
    
    self.comps.cvs_dpitem.Visible=false
    for i = #self.dungeonData[self.selectIndex].awardshow.id, 1, -1 do
        if self.dungeonData[self.selectIndex].awardshow.id[i] == 0 then
            table.remove(self.dungeonData[self.selectIndex].awardshow.id,i)
        end
    end
    local function eachupdatecb(node,index)
        UpDateItem(self,node,index)
    end
    UIUtil.ConfigHScrollPan(self.comps.sp_drops,self.comps.cvs_dpitem,#self.dungeonData[self.selectIndex].awardshow.id,eachupdatecb)
    
    --匹配or组队
    if DataMgr.Instance.TeamData.HasTeam then
        btn_myteam.Text=Constants.Dungeon.MyTeam
        btn_myteam.TouchClick=function (sender)
            GlobalHooks.UI.OpenUI('TeamFrame',0,'TeamInfo')
        end
    else
        btn_myteam.Text=Constants.Dungeon.QuickMatching
        btn_myteam.TouchClick=function (sender)
            local targetid = DungeonModel.GetTargetId(self.dungeonData[self.selectIndex].id)
            if targetid then
                TeamModel.RequestAutoMatch(targetid)
            else
                GlobalHooks.UI.OpenUI('TeamFrame',0,'TeamPlatform')
            end
            self.ui:Close()
        end
    end

    --前往
    self.comps.btn_start.TouchClick = function()
        --如果是队长，则发起进入副本请求
        if DataMgr.Instance.TeamData:IsLeader() then
            DungeonModel.RequestEnterDungeon(self.dungeonData[self.selectIndex].id,function(rsp)
            end)
            self.ui:Close()
        elseif not DataMgr.Instance.TeamData.HasTeam then
            --如果不在队伍内，提示是否单人进入副本
            GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL,Util.GetText('dungeon_enter_alone'),'','',nil,
                function ()
                    DungeonModel.RequestEnterDungeon(self.dungeonData[self.selectIndex].id,function(rsp)
                        self.ui:Close()
                    end)
                end
            ,nil)
        else
            --如果不是队长，漂字提示
            GameAlertManager.Instance:ShowNotify(Util.GetText('dungeon_enter_button'))
        end
    end
end


local function IsLock(limit,node)
    local isLock = limit > DataMgr.Instance.UserData.Level
    node.IsInteractive = not isLock
    node.Enable = not isLock
    node.EnableChildren = not isLock
    node.IsGray = isLock
end

local function SetEach(self,node,index)
    local tbt_fbselect = node:FindChildByEditName('tbt_fbselect',true)
    tbt_fbselect.IsChecked = self.selectIndex==index
    local ib_fbpic = node:FindChildByEditName('ib_fbpic',true)
    UIUtil.SetImage(ib_fbpic,self.dungeonData[index].head_res,false,UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)
    local lb_fbname = node:FindChildByEditName('lb_fbname',true)
    lb_fbname.Text = Util.GetText(self.dungeonData[index].dungen_name)
    local lb_openlevel = node:FindChildByEditName('lb_openlevel',true)
    lb_openlevel.Text = Util.GetText('skill_lv', self.dungeonData[index].show_limit)
    local lb_lock = node:FindChildByEditName('lb_lock',true)
    lb_lock.Visible = self.dungeonData[index].show_limit >DataMgr.Instance.UserData.Level
    IsLock(self.dungeonData[index].show_limit,node)
    tbt_fbselect.TouchClick=function(sender)
        tbt_fbselect.IsChecked = true
        if self.selectIndex == index then
            return
        end
        self.selectIndex = index
        self.comps.sp_fblist:RefreshShowCell()
        InitDetail(self)
    end
end

local function InitList(self)
    self.comps.lb_count.Text = self.dungeonLevel == _M.DungeonLevel.Common 
        and Util.GetText('pvp_rewardcount',self.normalCount) 
        or Util.GetText('pvp_rewardcount',self.heroCount)
    self.comps.cvs_fb.Visible=false
    local function eachupdatecb(node,index)
        SetEach(self,node,index)
    end
    --滑动控件
    UIUtil.ConfigVScrollPan(self.comps.sp_fblist,self.comps.cvs_fb,#self.dungeonData,eachupdatecb)
    
    local function ToggleFunc(sender)
        sender.TouchClick = function ()
            if sender:Equals(self.comps.tbt_normal) then
                self.dungeonLevel = _M.DungeonLevel.Common
            else
                self.dungeonLevel = _M.DungeonLevel.Hero
            end
                self.dungeonData = DungeonModel.GetDungeonDataByLevel(self.dungeonLevel)
                self.selectIndex = 1
                self.comps.sp_fblist:RefreshShowCell()
                InitDetail(self)
                self.comps.lb_count.Text = self.dungeonLevel == _M.DungeonLevel.Common
                    and Util.GetText('pvp_rewardcount',self.normalCount)
                    or Util.GetText('pvp_rewardcount',self.heroCount)
        end
    end
    UIUtil.ConfigToggleButton({self.comps.tbt_normal,self.comps.tbt_hero},self.dungeonLevel ==_M.DungeonLevel.Common and self.comps.tbt_normal or self.comps.tbt_hero,false,ToggleFunc)
end


local function InitComp(self)
    InitList(self)
    InitDetail(self)
end

local function MathDungeon(self,id)
    for i, v in ipairs(self.dungeonData) do
        if v.id == id then
            return v.order
        end
    end
end

function _M.OnEnter(self,...)
    local params={...}
    --获取副本额外奖励次数
    DungeonModel.RequestDungeonBounsCount(function(rsp)
        --默认普通本
        self.dungeonLevel = params[1] or _M.DungeonLevel.Common
        self.dungeonData = DungeonModel.GetDungeonDataByLevel(self.dungeonLevel)
        --默认选中第一个
        self.selectIndex = MathDungeon(self,params[2]) or 1
        self.normalCount = rsp.s2c_tickets
        self.heroCount = rsp.s2c_hard_tickets
        InitComp(self)
    end)
    --注册事件，监听组队
    DataMgr.Instance.TeamData:AttachLuaObserver('DungeonMain', self)
end


function _M.OnExit(self)
    --注销事件
    DataMgr.Instance.TeamData:DetachLuaObserver('DungeonMain')
end


return _M