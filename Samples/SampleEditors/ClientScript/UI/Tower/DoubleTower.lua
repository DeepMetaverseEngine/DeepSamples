local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local PagodaModel = require 'Model/PagodaModel'
local SocialModel = require 'Model/SocialModel'
local TeamModel = require 'Model/TeamModel'
local DisplayUtil = require 'Logic/DisplayUtil'

local selfui = nil
local Checkedfloor = 1

local function SetTeamInfo(self)
    _M.Notify()
    Util.GetRoleSnap(DataMgr.Instance.UserData.RoleID,function (data)
        local photoname = data.Options['Photo0']
        if not string.IsNullOrEmpty(photoname) then
            SocialModel.SetHeadIcon(data.ID,photoname,function(UnitImg)
                if not self.root.IsDispose then
                    UIUtil.SetImage(self.ib_headpic,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                end
            end)
        else
            UIUtil.SetImage(self.ib_headpic, 'static/target/'..data.Pro..'_'..data.Gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
        end
    end)
    self.lb_myname.Text = DataMgr.Instance.UserData.Name
    self.lb_fnum.Text = DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.FIGHTPOWER)
    local textkey = self.cbdata.maxLayer == 0 and "tower_no_clearance" or "tower_clearance"
    self.lb_record.Text = Util.GetText(textkey,self.cbdata.maxLayer)
end

local function InitFriendList(self,node,data)
    local ib_target = node:FindChildByEditName('ib_target', true)
    local ib_job = node:FindChildByEditName('ib_job', true)
    local lb_powernum = node:FindChildByEditName('lb_powernum', true)
    local lb_lv = node:FindChildByEditName('lb_lv', true)
    local lb_name = node:FindChildByEditName('lb_name', true)
    local lb_invite = node:FindChildByEditName('lb_invite', true)
    local btn_invite = node:FindChildByEditName('btn_invite', true)
    local lb_relatlv = node:FindChildByEditName('lb_relatlv', true)
    
    if not string.IsNullOrEmpty(data.photoname) then
        SocialModel.SetHeadIcon(data.roleId,data.photoname,function(UnitImg)
            if not self.root.IsDispose then
                UIUtil.SetImage(ib_target,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
            end
        end)
    else
        UIUtil.SetImage(ib_target, 'static/target/'..data.pro..'_'..data.gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
    end
    UIUtil.SetImage(ib_job, '$dynamic/TL_login/output/TL_login.xml|TL_login|prol_'.. data.pro)
    lb_powernum.Text = data.fightPower
    lb_lv.Text = data.level
    lb_name.Text = data.roleName
    lb_relatlv.Text = data.relationLv
    
    
    lb_invite.Visible = data.invited
    btn_invite.Visible = not data.invited
    btn_invite.TouchClick = function(sender)
        TeamModel.RequestInviteTeam(data.roleId, function()
            lb_invite.Visible = true
            sender.Visible = false
            data.invited = true
        end)
    end
end

local function ShowFriendList(self)
    SocialModel.RequestClientGetFriendList(function(users)
        local ids = {}
        for i, v in ipairs(users.s2c_data.friendList) do
            table.insert(ids,v.roleId)
        end
        Util.GetManyRoleSnap(ids,function(datas)
            self.cvs_cplist.Visible = true
            local activeplayers = {}
            if users.s2c_data.friendCount ~= 0 then
                for i, v in ipairs(users.s2c_data.friendList) do
                    if System.DateTime.MaxValue == v.leaveTime then
                        v.invited = false
                        v.photoname = datas[i].Options['Photo0']
                        table.insert(activeplayers,v)
                    end
                end
                if #activeplayers>1 then
                    table.sort(activeplayers,function (a,b)
                        return a.relationExp>b.relationExp
                    end)
                end
            end
            if #activeplayers >0 then
                self.cvs_nothing.Visible = false
                self.cvs_invite.Visible = true
                UIUtil.ConfigVScrollPan(self.sp_oar,
                        self.cvs_role,
                        #activeplayers,
                        function(node,index)
                            InitFriendList(self,node,activeplayers[index])
                        end)
            else
                self.cvs_nothing.Visible = true
                self.cvs_invite.Visible = false
            end
        end)
    end)
end

local function InitCompents(self)
    selfui = self
    self.cvs_role.Visible = false
    self.lb_red_miwen.Visible = false
    self.cvs_specific.Visible = true
    self.cvs_cplist.Visible = false
    self.cvs_step.Visible = false
    self.cvs_firstpass.Visible = false
    self.rightX = self.sp_tower.Size2D.x - self.cvs_step.Size2D.x
    self.nodewidth = self.cvs_step.Size2D.x
    if DataMgr.Instance.TeamData:IsLeader() == false and DataMgr.Instance.TeamData.HasTeam then
        self.btn_start.IsGray = true
        self.btn_start.Enable = false
    else
        self.btn_start.IsGray = false
        self.btn_start.Enable = true
    end
    self.btn_back.TouchClick = function(sender)
        self.cvs_select.Visible = true
        self.cvs_other.Visible = false
    end
end

local function SetFirstReward(self)
    local sp_passlist = self.cvs_firstpass:FindChildByEditName('sp_passlist', true)
    local cvs_rewardlist = self.cvs_firstpass:FindChildByEditName('cvs_rewardlist', true)
    cvs_rewardlist.Visible = false
    UIUtil.ConfigVScrollPan(sp_passlist,cvs_rewardlist,#self.staticdata,function (node,index)
        local lb_stagename = node:FindChildByEditName('lb_stagename', true)
        local btn_getreward = node:FindChildByEditName('btn_getreward', true)
        local lb_invite = node:FindChildByEditName('lb_invite', true)
        local sp_firstpass = node:FindChildByEditName('sp_firstpass', true)
        local cvs_firstitem = node:FindChildByEditName('cvs_firstitem', true)
        lb_stagename.Text = Util.GetText(self.staticdata[index].floor_name)
        lb_invite.Visible = self.cbdata.giftData[index] == 2
        btn_getreward.Visible = self.cbdata.giftData[index] ~= 2
        btn_getreward.IsGray = self.cbdata.giftData[index] == 0
        btn_getreward.Enable = self.cbdata.giftData[index] == 1

        UIUtil.ConfigHScrollPan(sp_firstpass,cvs_firstitem,#self.staticdata[index].first_reward.id,function(node1,index1)
            UIUtil.SetItemShowTo(node1,self.staticdata[index].first_reward.id[index1],self.staticdata[index].first_reward.num[index1])
            node1.TouchClick = function(sender)
                UIUtil.ShowTips(self,sender,self.staticdata[index].first_reward.id[index1])
            end
        end )
        cvs_firstitem.Visible = false

        btn_getreward.TouchClick = function(sender)
            PagodaModel.GetCPReward(self.staticdata[index].floor,self.mode,function(rsp)
                self.cbdata.giftData[index] = 2
                sp_passlist:RefreshShowCell()
                self.btn_getall.IsGray = true
                self.btn_getall.Enable = false
                self.lb_red_miwen.Visible = false
                for i, v in ipairs(self.cbdata.giftData) do
                    if v == 1 then
                        self.btn_getall.IsGray = false
                        self.btn_getall.Enable = true
                        self.lb_red_miwen.Visible = true
                        break
                    end
                end
            end)
        end
    end)


    self.btn_getall.IsGray = true
    self.btn_getall.Enable = false
    for i, v in ipairs(self.cbdata.giftData) do
        if v == 1 then
            self.btn_getall.IsGray = false
            self.btn_getall.Enable = true
            break
        end
    end
    self.btn_getall.TouchClick = function(sender)
        PagodaModel.GetCPReward(0,self.mode,function(rsp)
            for i = 1, #self.cbdata.giftData do
                if self.cbdata.giftData[i] == 1 then
                    self.cbdata.giftData[i] = 2
                end
            end
            self.lb_red_miwen.Visible = false
            sp_passlist:RefreshShowCell()
            sender.IsGray = true
            sender.Enable = false
        end)
    end
end

local function ClickEvent(self)
    self.btn_findcp.TouchClick = function(sender)
        ShowFriendList(self)
    end
    self.btn_close1.TouchClick = function(sender)
        self.cvs_cplist.Visible = false
    end
    self.btn_fpass.TouchClick = function(sender)
        self.cvs_firstpass.Visible = true
        SetFirstReward(self)
    end
    self.btn_close2.TouchClick = function(sender)
        self.cvs_firstpass.Visible = false
    end
    self.btn_start.TouchClick = function(sender)
        local extdata = {}
        extdata['mode'] = self.mode
        FunctionUtil.OpenFunction('gotocptower',true,{arg = extdata})
    end
    self.btn_quickcp.TouchClick = function(sender)
        GlobalHooks.UI.OpenUI("TeamMain",0,"TeamPlatform")
    end
end

local function SetDetailInfo(self,data)
    self.lb_name.Text = Util.GetText(data.floor_name)
    self.lb_fightnum.Text = data.power
    UIUtil.ConfigHScrollPan(self.sp_reward,self.cvs_item,#data.preview.id,function (node,index)
        UIUtil.SetItemShowTo(node,data.preview.id[index])
        node.TouchClick = function(sender)
            UIUtil.ShowTips(self,sender,data.preview.id[index])
        end
    end)
    
end

local function InitLayerData(self,node,index,staticdata)
    local cvs_pic = node:FindChildByEditName('cvs_pic', true)
    local cvs_stepinfo = node:FindChildByEditName('cvs_stepinfo', true)
    local ib_select = node:FindChildByEditName('ib_select', true)
    local lb_stepname = node:FindChildByEditName('lb_stepname', true)
    local ib_steppass = node:FindChildByEditName('ib_steppass', true)
    local cvs_player = node:FindChildByEditName('cvs_player', true)
    local ib_head = node:FindChildByEditName('ib_head', true)

    cvs_player.Visible = false
    ib_head.Visible = false

    if self.cbdata.maxLayer < index then
        node.IsGray = true
        ib_steppass.Visible = false
    else
        node.IsGray = false
        ib_steppass.Visible = true
    end
    lb_stepname.Text = Util.GetText(staticdata.floor_name)
    ib_select.Visible = Checkedfloor == index
    --设置位置(右侧)
    if index%2 == 0 then
        node.X = self.rightX
        cvs_pic.X = self.nodewidth - self.cvs[1]
        cvs_stepinfo.X = self.nodewidth - self.cvs[2] - cvs_stepinfo.Size2D.x
        cvs_pic.Scale = Vector2(-1,1)
    else--(左侧)
        cvs_pic.X = self.cvs[1]
        cvs_stepinfo.X = self.cvs[2]
        cvs_pic.Scale = Vector2(1,1)
    end
    
    cvs_stepinfo.TouchClick = function(sender)
        Checkedfloor = index
        self.sp_tower:RefreshShowCell()
        SetDetailInfo(self,staticdata)
    end
end

local function SetLayer(self,mode)
    self.lb_red_miwen.Visible = false
    self.mode = mode
    PagodaModel.RequireCPTowerdData(mode,function(rsp)
        self.cvs_select.Visible = false
        self.cvs_other.Visible = true
        self.cbdata = rsp
        SetTeamInfo(self)
        Checkedfloor = 1
        UIUtil.ConfigVScrollPan(self.sp_tower,self.cvs_step,#self.staticdata,function(node,index)
            InitLayerData(self,node,#self.staticdata-index+1,self.staticdata[#self.staticdata-index+1])
        end)
        --DisplayUtil.lookAt(self.sp_tower,#self.staticdata)
        SetDetailInfo(self,self.staticdata[Checkedfloor])
        self.lb_count.Text = "("..self.cbdata.curPlayTimes.."/"..self.cbdata.maxPlayTimes..")"
        
        for i, v in ipairs(self.cbdata.giftData) do
            if v == 1 then
                self.lb_red_miwen.Visible = true
                break
            end
        end
    end)
end

local function SelectMode(self)
    self.cvs_select.Visible = true
    self.cvs_other.Visible = false
    local level = DataMgr.Instance.UserData.Level

    local lb_lock2 = self.cvs_leveltwo:FindChildByEditName('lb_lock2', true)
    local lb_join2 = self.cvs_leveltwo:FindChildByEditName('lb_join2', true)
    lb_lock2.Visible = level < 131
    lb_join2.Visible = level >= 131
    

    self.cvs_levelone.TouchClick = function(sender)
        self.staticdata = PagodaModel.GetCPTowerData(1)
        SetLayer(self,1)
    end

    self.cvs_leveltwo.TouchClick = function(sender)
        if level >= 131 then
            self.staticdata = PagodaModel.GetCPTowerData(2)
            SetLayer(self,2)
        else
            GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.PagodaMain.LevelNotEnoughTips))
        end
    end
    
    
    
end
function _M.OnEnter(self)
    DataMgr.Instance.TeamData:AttachLuaObserver('TeamInfo',self)
    
    InitCompents(self)
    SelectMode(self)
    ClickEvent(self)
end

function _M.OnInit(self)
    self.cvs_towinfo = self.root:FindChildByEditName('cvs_towinfo', true)
    self.cvs_floor = self.root:FindChildByEditName('cvs_floor', true)
    self.cvs_self = self.root:FindChildByEditName('cvs_self', true)
    self.cvs_nobody = self.root:FindChildByEditName('cvs_nobody', true)
    self.cvs_cpinfo = self.root:FindChildByEditName('cvs_cpinfo', true)
    self.cvs_cplist = self.root:FindChildByEditName('cvs_cplist', true)
    self.cvs_other = self.root:FindChildByEditName('cvs_other', true)
    self.cvs_firstpass = self.root:FindChildByEditName('cvs_firstpass', true)
    self.cvs_select = self.root:FindChildByEditName('cvs_select', true)
    self.cvs_specific = self.root:FindChildByEditName('cvs_specific', true)
    
    self.btn_back = self.root:FindChildByEditName('btn_back', true)
    
    local lb_towername1 = self.cvs_select:FindChildByEditName('lb_towername1', true)
    local lb_towername2 = self.cvs_select:FindChildByEditName('lb_towername2', true)
    lb_towername1.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
    lb_towername2.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
    
    self.cvs_levelone = self.cvs_select:FindChildByEditName('cvs_levelone', true)
    self.cvs_leveltwo = self.cvs_select:FindChildByEditName('cvs_leveltwo', true)
    
    --首通奖励
    self.btn_close2 = self.cvs_firstpass:FindChildByEditName('btn_close2', true)
    self.btn_getall = self.cvs_firstpass:FindChildByEditName('btn_getall', true)
    
    --层数控件
    self.cvs_step = self.cvs_towinfo:FindChildByEditName('cvs_step', true)
    self.sp_tower = self.cvs_towinfo:FindChildByEditName('sp_tower', true)
    self.cvs = {}
    self.cvs[1] = self.cvs_step:FindChildByEditName('cvs_pic', true).X
    self.cvs[2] = self.cvs_step:FindChildByEditName( 'cvs_stepinfo', true).X
    
    --单层详细信息
    self.lb_name = self.cvs_floor:FindChildByEditName('lb_name', true)
    self.lb_fightnum = self.cvs_floor:FindChildByEditName('lb_fightnum', true)
    self.sp_reward = self.cvs_floor:FindChildByEditName('sp_reward', true)
    self.cvs_item = self.cvs_floor:FindChildByEditName('cvs_item', true)
    self.cvs_item.Visible = false

    --自己信息控件
    self.ib_headpic = self.cvs_self:FindChildByEditName('ib_headpic', true)
    self.lb_myname = self.cvs_self:FindChildByEditName('lb_myname', true)
    self.lb_record = self.cvs_self:FindChildByEditName('lb_record', true)
    self.lb_fnum = self.cvs_self:FindChildByEditName('lb_fnum', true)

    --cp信息控件
    self.ib_cpheadpic = self.cvs_cpinfo:FindChildByEditName('ib_cpheadpic', true)
    self.lb_cpname = self.cvs_cpinfo:FindChildByEditName('lb_cpname', true)
    self.lb_cprecord = self.cvs_cpinfo:FindChildByEditName('lb_cprecord', true)
    self.lb_cpfnum = self.cvs_cpinfo:FindChildByEditName('lb_cpfnum', true)
    
    --邀请好友控件
    self.btn_findcp = self.cvs_nobody:FindChildByEditName('btn_findcp', true)
    self.cvs_nothing = self.cvs_cplist:FindChildByEditName('cvs_nothing', true)
    self.cvs_invite = self.cvs_cplist:FindChildByEditName('cvs_invite', true)
    self.cvs_role = self.cvs_invite:FindChildByEditName('cvs_role', true)
    self.sp_oar = self.cvs_invite:FindChildByEditName('sp_oar', true)
    self.btn_close1 = self.cvs_cplist:FindChildByEditName('btn_close1', true)
    
    --主界面按钮
    self.btn_fpass = self.cvs_other:FindChildByEditName('btn_fpass', true)
    self.lb_red_miwen = self.cvs_other:FindChildByEditName('lb_red_miwen', true)
    self.btn_start = self.cvs_other:FindChildByEditName('btn_start', true)
    self.btn_quickcp = self.cvs_other:FindChildByEditName('btn_quickcp', true)
    self.lb_count = self.cvs_other:FindChildByEditName('lb_count', true)

end

function _M.OnExit(self)
    DataMgr.Instance.TeamData:DetachLuaObserver('TeamInfo')
end

function _M:UpdateTeamData(cpinfo)
    selfui.cvs_cplist.Visible = false
    selfui.cvs_nobody.Visible = false
    selfui.cvs_cpinfo.Visible = true
    local photoname = cpinfo.Options['Photo0']
    if not string.IsNullOrEmpty(photoname) then
        SocialModel.SetHeadIcon(cpinfo.roleId,photoname,function(UnitImg)
            if not selfui.root.IsDispose then
                UIUtil.SetImage(selfui.ib_cpheadpic,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
            end
        end)
    else
        UIUtil.SetImage(selfui.ib_cpheadpic, 'static/target/'..cpinfo.Pro..'_'..cpinfo.Gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
    end
    selfui.lb_cpname.Text = cpinfo.Name
    selfui.lb_cpfnum.Text = cpinfo.FightPower
    local textkey = cpinfo.CPTowerLayer == 0 and "tower_no_clearance" or "tower_clearance"
    selfui.lb_cprecord.Text = Util.GetText(textkey,cpinfo.CPTowerLayer)
end

function _M:ClearCpinfo()
    selfui.cvs_nobody.Visible = true
    selfui.cvs_cpinfo.Visible = false
end

function _M.Notify()
    if DataMgr.Instance.TeamData:IsLeader() == false and DataMgr.Instance.TeamData.HasTeam then
        selfui.btn_start.IsGray = true
        selfui.btn_start.Enable = false
        selfui.btn_quickcp.IsGray = true
        selfui.btn_quickcp.Enable = false
    else
        selfui.btn_start.IsGray = false
        selfui.btn_start.Enable = true
        selfui.btn_quickcp.IsGray = false
        selfui.btn_quickcp.Enable = true
    end
    local members = DataMgr.Instance.TeamData.AllMembers
    local allRoles = {}
    if members then
        for i = 0, members.Count - 1 do
            local m = members:getItem(i)
            if m.RoleID ~= DataMgr.Instance.UserData.RoleID then
                table.insert(allRoles, m.RoleID)
            end
        end
    end
    if #allRoles > 0 then
        Util.GetManyRoleSnap(
                allRoles,
                function(data)
                    _M:UpdateTeamData(data[1])
                end
        )
    else
        _M:ClearCpinfo()
    end
end

return _M



