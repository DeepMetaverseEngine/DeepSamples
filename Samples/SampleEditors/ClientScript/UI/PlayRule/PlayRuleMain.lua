---作者：任祥建
---时间：2018/9/11

local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local DisplayUtil = require 'Logic/DisplayUtil'
local TimeUtil = require 'Logic/TimeUtil'
local PlayRuleModel = require 'Model/PlayRuleModel'
local SocialModel = require 'Model/SocialModel'



local showedJob = 1
local defultData = nil
local BattleList = nil
local selectedRelative = nil
local basedata = nil
local mypro = nil
local myroleid = nil
local friendList = nil
local min_bgposX = nil
local IsCD = true
local ib_arrowup = nil
local ib_arrowdown = nil

local function SetPracticeLv(node,level)
    if type(level) == "number" and level ~= 0 then
        local detail = unpack(GlobalHooks.DB.Find('practice', {artifact_stage = level}))
        level = detail.chat_icon
        node.Visible = true
    elseif level == 0 then
        node.Visible = false
    end
    UIUtil.SetImage(node,level)
end

local function GettFriendList( self,cb )
    if not friendList then
        SocialModel.RequestClientGetFriendList(function(rsp)
            friendList = rsp.s2c_data.friendList
            local ids = {}
            for i, v in ipairs(friendList) do
                table.insert(ids,v.roleId)
            end
            Util.GetManyRoleSnap(ids,function(datas)
                for i, v in ipairs(friendList) do
                    v.photoname = datas[i].Options['Photo0']
                end
                if cb then
                    cb()
                end
            end)
        end)
    else
        if cb then
            cb()
        end
    end
end

local function Release3DModel(self,modelindex)
    if self.model then
        if self.model[modelindex].info and self.model[modelindex] then
            UI3DModelAdapter.ReleaseModel(self.model[modelindex].info.Key)
            if self.model[modelindex].titleinfo then
                UI3DModelAdapter.ReleaseModel(self.model[modelindex].titleinfo.Key)
            end
        end
    end
end

local function Init3DModel(self, parentCvs,infonode,titleinfo, pos2d, scale, menuOrder, avatar, filte,modelindex)
    if type(avatar) == "number" then
        avatar = GameUtil.GetAvatarByTemplateId(avatar)
    end
    local info = UI3DModelAdapter.AddAvatar(parentCvs, pos2d, scale, menuOrder, avatar, filte)
    info.Callback = function(model)
        model.RootTrans.localPosition = Vector3(0,0,-500)
    end
    self.model[modelindex] = {}
    self.model[modelindex].info = info
    self.model[modelindex].infonode = infonode
    self.model[modelindex].titleinfo = titleinfo
    infonode.Visible = true
end

local function GetDefultOneData(job,job_type,index)
    if not defultData then
        defultData = PlayRuleModel.GetDefultData()
    end
    for i, v in pairs(defultData) do
        if v.job == job and v.imagearena_type == job_type and v.imagearena_num == index then
            return v
        end 
    end
    return nil
end

--重建数据
local function ReBuildData(self,cb)
    local uuids = {}
    for i, v in pairs(self.masterdatalist.masterDataMap) do
        if not string.IsNullOrEmpty(v.playeruuid) then
            table.insert(uuids,v.playeruuid)
        end
    end
    basedata = PlayRuleModel.GetShowData(showedJob)
    local tempavatar = {}
    Util.GetManyRoleSnap(uuids,function (snaps)
        for i, v in pairs(snaps) do
            table.insert(tempavatar,{
                id = uuids[i],
                photoname = v.Options['Photo0'],
                avatar = v.Avatar,
                playername = v.Name,
                gender = v.Gender,
                practiceLv = v.PracticeLv,
                guildname = v.GuildName,
                titleid = v.TitleID,
                cpname = v.Options["TitleNameExt"] or ""
            })
        end


        for i, v in pairs(self.masterdatalist.masterDataMap) do
            for _, v2 in pairs(basedata) do
                if v2.imagearena_type == v.masterid then
                    v.order = v2.order
                    v.officename = Util.GetText(v2.name)
                    v.name_frame = v2.name_frame
                    v.picture_frame = v2.picture_frame
                    if not string.IsNullOrEmpty(self.masterdatalist.QinXinName) and v.masterid == 7 then
                        v.officename = self.masterdatalist.QinXinName
                    end
                    v.pro = v2.job
                    if v.playeruuid then
                        if string.IsNullOrEmpty(v.playeruuid)then
                            local DefultOneData = GetDefultOneData(showedJob,v.masterid,v.index)
                            if DefultOneData then
                                v.playername = Util.GetText(DefultOneData.template_name)
                                v.avatar = DefultOneData.monster_id
                                v.practiceLv = DefultOneData.practice_res
                                v.head_icon = DefultOneData.head_icon
                            else
                                v.playername = nil
                                v.avatar = nil
                            end
                            v.titleid = 0
                            v.playeruuid = nil
                        else
                            for _, v3 in pairs(tempavatar) do
                                if v3.id == v.playeruuid then
                                    v.playername = v3.playername
                                    v.gender = v3.gender
                                    v.avatar = v3.avatar
                                    v.photoname = v3.photoname
                                    v.guildname = v3.guildname
                                    v.titleid = v3.titleid
                                    v.cpname = v3.cpname
                                    v.practiceLv = v3.practiceLv
                                end
                            end
                        end
                    end
                end
            end
        end
        table.sort(self.masterdatalist.masterDataMap,function (a,b)
            if a.order == b.order then
                return a.index < b.index
            else
                return a.order < b.order
            end
        end)
        
        if cb then
            cb()
        end
    end)
end

local function InitFriendList(self,node,data,index)
    local lb_name = node:FindChildByEditName('lb_name', true)
    local ib_icon = node:FindChildByEditName('ib_icon', true)
    local lb_lv = node:FindChildByEditName('lb_lv', true)
    local ib_job = node:FindChildByEditName('ib_job', true)
    local ib_sex = node:FindChildByEditName('ib_sex', true)
    local tbt_select = node:FindChildByEditName('tbt_select', true)
    tbt_select.IsChecked = false
    tbt_select.IsChecked = index == selectedRelative

    lb_lv.Text = data.level
    lb_name.Text = data.roleName
    if not string.IsNullOrEmpty(data.photoname) then
        SocialModel.SetHeadIcon(data.roleId,data.photoname,function(UnitImg)
            if not self.root.IsDispose then
                UIUtil.SetImage(ib_icon,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
            end
        end)
    else
        UIUtil.SetImage(ib_icon, 'static/target/'..data.pro..'_'..data.gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
    end
    UIUtil.SetImage(ib_job, '$dynamic/TL_login/output/TL_login.xml|TL_login|prol_'.. data.pro)
    UIUtil.SetImage(ib_sex, '$static/TL_staticnew/output/TL_staticnew.xml|TL_static|gen_'..data.gender)

    tbt_select.TouchClick = function(sender)
        selectedRelative = index
        self.sp_friendlist:RefreshShowCell()
    end
end

local function SetModel(self, node, avatar, index)

    local filter =  bit.lshift(1,GameUtil.TryEnumToInt(TLAvatarInfo.TLAvatar.Ride_Avatar01))

    if not self.model[index] then
        --设置模型附加属性
        local cvs_info = node:FindChildByEditName('cvs_info', true)
        cvs_info.Visible = false
        local infonode = cvs_info:Clone()
        infonode.Visible = true

        local lb_playername = infonode:FindChildByEditName('lb_playername', true)
        local lb_guildname = infonode:FindChildByEditName('lb_guildname', true)
        local ib_practicelv = infonode:FindChildByEditName('ib_practicelv', true)
        local lb_titlename = infonode:FindChildByEditName('lb_titlename', true)

        if self.masterdatalist.masterDataMap[index].playername then
            lb_playername.Text = self.masterdatalist.masterDataMap[index].playername
            lb_playername.Visible = true
        else
            lb_playername.Visible = false
        end
        if not string.IsNullOrEmpty(self.masterdatalist.masterDataMap[index].guildname) then
            lb_guildname.Text = self.masterdatalist.masterDataMap[index].guildname
            lb_guildname.Visible = true
        else
            lb_guildname.Visible = false
        end
        if self.masterdatalist.masterDataMap[index].practiceLv then
            ib_practicelv.X = -(lb_playername.PreferredSize[1]/2+ib_practicelv.Size2D[1])
            SetPracticeLv(ib_practicelv,self.masterdatalist.masterDataMap[index].practiceLv)
            ib_practicelv.Visible = true
        else
            ib_practicelv.Visible = false
        end
        local titleinfo = nil
        if self.masterdatalist.masterDataMap[index].titleid > 0 then
            if not lb_guildname.Visible then
                lb_titlename.Position2D = Vector2(lb_titlename.X,lb_guildname.Y)
            end
            lb_titlename.Visible = true
            
            titleinfo = UIUtil.SetTitle(self,lb_titlename,self.masterdatalist.masterDataMap[index].titleid,self.masterdatalist.masterDataMap[index].cpname)
        else
            lb_titlename.Visible = false
        end
        
        
        node:AddChild(infonode)
        
        Init3DModel(self,node,infonode,titleinfo, Vector2(0,0), 80, self.ui.menu.MenuOrder, avatar, filter,index)
    end
    self.model[index].node = node
    node.UserTag = index
end

--主滑动列表
local function InitMainList1(self,node,maxindex)
    local nodes = {}
    for i = 1, 4 do
        local index = maxindex*4 + i - 1
        nodes[i] = node:FindChildByEditName('cvs_roleinfo'..i, true)
        if index == 0 then
            nodes[i].Visible = false
        else
            if index == 1 then
                nodes[i].X = node.Size2D[1]/2 - nodes[i].Size2D[1]/2
            end
            nodes[i].Visible = true
            local cvs_anchor = nodes[i]:FindChildByEditName('cvs_anchor', true)
            local tb_blankpos = nodes[i]:FindChildByEditName('tb_blankpos', true)
            local lb_position = nodes[i]:FindChildByEditName('lb_position', true)
            local ib_di1 = nodes[i]:FindChildByEditName('ib_di1', true)
            
            UIUtil.SetImage(lb_position,self.masterdatalist.masterDataMap[index].name_frame)
            UIUtil.SetImage(ib_di1,self.masterdatalist.masterDataMap[index].picture_frame)
            lb_position.Visible = true
            ib_di1.Visible = true
            
            tb_blankpos.Visible = not self.masterdatalist.masterDataMap[index].playername
            lb_position.Text = self.masterdatalist.masterDataMap[index].officename

            --SetModel(self,cvs_anchor,self.masterdatalist.masterDataMap[index].avatar,index)
            
            nodes[i].TouchClick = function(sender)
                local args = {}
                local temp = self.ui.root:GlobalToLocal(sender:LocalToGlobal(),true)
                args.uipos = Vector2(temp.x+sender.Size2D.x,temp.y+sender.Size2D.y/2)
                args.playerId = self.masterdatalist.masterDataMap[index].playeruuid
                args.playerName = self.masterdatalist.masterDataMap[index].playername

                if args.playerId then
                    --点击自己的模型
                    if args.playerId == myroleid then
                        if self.masterdatalist.masterDataMap[index].masterid == 7 and self.masterdatalist.curMasterId == 1 then
                            args.menuKey = 'shimen_noappoint'
                        else
                            GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.PlayRule.ClickSelfTips))
                        end
                    else --点击其他玩家模型
                        args.menuKey = 'shimen_stranger'
                        for i, v in pairs(friendList) do
                            if args.playerId == v.roleId then
                                args.menuKey = 'shimen_friend'
                                if self.masterdatalist.masterDataMap[index].masterid == 7 and self.masterdatalist.curMasterId == 1 then
                                    args.menuKey = 'shimen_noappoint'
                                end
                            end
                        end
                    end
                    --未设置亲信
                elseif not self.masterdatalist.masterDataMap[index].playername and self.masterdatalist.curMasterId == 1 then
                    args.menuKey = 'shimen_appoint'
                end

                if args.menuKey then
                    EventManager.Fire("Event.InteractiveMenu.Show", args)
                end
            end
        end
    end  
end

local function InitMainList2(self,node,maxindex)
    local nodes = {}
    for i = 1, 4 do
        local index = maxindex*4 + i - 1
        if index > 0 then
            nodes[i] = node:FindChildByEditName('cvs_roleinfo'..i, true)
            local cvs_anchor = nodes[i]:FindChildByEditName('cvs_anchor', true)
            SetModel(self,cvs_anchor,self.masterdatalist.masterDataMap[index].avatar,index)
        end
    end
end

local function ShowPageScrollPan(self,spannode,tempnode,pagecount)
    local nodes = {}
    local function eachCreateCb(node, index)
        nodes[index+1] = {}
        nodes[index+1].obj = node
        nodes[index+1].noloaded = true
        InitMainList1(self,nodes[index+1].obj,index)
    end

    local function eachUpdateCB(index)
        if nodes[index+1].noloaded then
            InitMainList2(self,nodes[index+1].obj,index)
            nodes[index+1].noloaded = true
        end
        self.bt_up.Visible = index > 0
        self.bt_down.Visible = index < pagecount-1
    end
    UIUtil.ConfigPageScrollPan(spannode, tempnode, pagecount,true, eachCreateCb, eachUpdateCB)
end

local function SetCenterInfo(self,cb)
    self.bt_up.Visible = true
    PlayRuleModel.RequestPlayRule(showedJob,function (rsp)
        self.masterdatalist = rsp.s2c_data
        ReBuildData(self,function()
            ShowPageScrollPan(
                    self,
                    self.sp_rolelist,
                    self.cvs_rolemax,
                    math.ceil((#self.masterdatalist.masterDataMap+1)/4))
            self.cvs_rolemax.Visible = false
            if cb then
                cb()
            end
        end)
    end)

    self.bt_up.TouchClick = function(sender)
        self.sp_rolelist:ShowPrevPage()
    end
    
    self.bt_down.TouchClick = function(sender)
        self.sp_rolelist:ShowNextPage()
    end
end

local function InitBattle(self,node,data)
    local lb_name = node:FindChildByEditName('lb_name', true)
    local ib_headicon = node:FindChildByEditName('ib_headicon', true)
    local ib_class = node:FindChildByEditName('ib_class', true)
    local lb_posname = node:FindChildByEditName('lb_posname', true)
    local bt_fight = node:FindChildByEditName('bt_fight', true)

    if data.gender then
        if not string.IsNullOrEmpty(data.photoname) then
            SocialModel.SetHeadIcon(data.playeruuid,data.photoname,function(UnitImg)
                if not self.root.IsDispose then
                    UIUtil.SetImage(ib_headicon,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                end
            end)
        else
            UIUtil.SetImage(ib_headicon, 'static/target/'..data.pro..'_'..data.gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
        end
    else
        UIUtil.SetImage(ib_headicon, data.head_icon,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
    end
    SetPracticeLv(ib_class,data.practiceLv)

    lb_posname.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
    lb_posname.Text = data.officename
    lb_name.Text = data.playername
    
    bt_fight.Enable = not IsCD
    bt_fight.IsGray = IsCD

    bt_fight.TouchClick = function(sender)
        local extdata = {}
        extdata['masterid'] = data.masterid
        extdata['index'] = data.index
        local gotoresult = FunctionUtil.OpenFunction('gotomasterrace',true,{arg = extdata})
        if gotoresult == true then
            _M.SetBattleList(self)
        end
    end
end

local function RefreshCD(self,challengeCD,cb)
    if self.timer then
        LuaTimer.Delete(self.timer)
        self.timer = nil
    end
    self.timer = LuaTimer.Add(0,1000,function()
        local cd = -TimeUtil.TimeLeftSec( challengeCD )
        if cd < 1 then
            if cb then
                cb(0)
            end
            LuaTimer.Delete(self.timer)
            self.timer = nil
            return false
        else
            if cb then
                cb(cd)
            end
            return true
        end
    end)
end

local function ShowCell(panel, column, row, cell)
    if row == 0 then
        ib_arrowup.Visible = false
    end
    if row == #BattleList - 1 then
        ib_arrowdown.Visible = false
    end
end

local function HideCell(panel, column, row, cell)
    if row == 0 then
        ib_arrowup.Visible = true
    end
    if row == #BattleList - 1 then
        ib_arrowdown.Visible = true
    end
end

function _M.SetBattleList(self)
    self.cvs_cdtime = self.cvs_battlelist:FindChildByEditName('cvs_cdtime', true)
    self.lb_opentime = self.cvs_battlelist:FindChildByEditName('lb_opentime', true)
    self.lb_num = self.cvs_battlelist:FindChildByEditName('lb_num', true)
    self.tb_desc = self.cvs_battlelist:FindChildByEditName('tb_desc', true)
    self.tb_leaderdesc.Visible = self.masterdatalist.curMasterId == 1
    self.lb_opentime.Text = Util.GetText('masterrace_ui_time')
    self.tb_desc.Text = string.gsub(Util.GetText('masterrace_ui_text'),"\\n","\n")
    ib_arrowup.Visible = true
    ib_arrowdown.Visible = true
    BattleList = {}
    if mypro == showedJob then
        for i, v in pairs(self.masterdatalist.masterDataMap) do
            if self.masterdatalist.curMasterId-1 == v.masterid then
                table.insert(BattleList,v)
            end
        end
    end
    UIUtil.ConfigVScrollPan(self.sp_targetlist, self.cvs_target, #BattleList, function(node,index)
        InitBattle(self,node,BattleList[index])
    end)
    --刷新cd
    RefreshCD(self,self.masterdatalist.challengeCD,function (cd)
        if cd < 1 then
            self.lb_opentime.Visible = true
            self.cvs_cdtime.Visible = false
            IsCD = false
            self.sp_targetlist:RefreshShowCell()
        else
            self.lb_opentime.Visible = false
            self.cvs_cdtime.Visible = true
            self.lb_num.Text = TimeUtil.FormatToCN(cd)
            if not IsCD then
                IsCD = true
                self.sp_targetlist:RefreshShowCell()
            end
        end
    end)
    self.sp_targetlist.Scrollable.event_HideCell = {'+=', HideCell}
    self.sp_targetlist.Scrollable.event_ShowCell = {'+=', ShowCell}
    self.cvs_target.Visible = false
end

local function InitBattleInfo(self,node,data)
    local lb_time = node:FindChildByEditName('lb_time', true)
    local lb_detail = node:FindChildByEditName('lb_detail', true)

    lb_time.Text = Util.GetText(Constants.PlayRule.BattleTimeShow,TimeUtil.FormatToCN( TimeUtil.TimeLeftSec( data.dateTime ) ) )
    lb_detail.Text = data.message
end

local function ShowBattleInfo(self)
    local hudui = HudManager.Instance:GetHudUI("MainHud")
    if hudui then
        local pushnode = hudui:FindChildByEditName("cvs_hud_identitybattle", true)
        pushnode.Visible = false
    end
    if self.masterdatalist then
        UIUtil.ConfigVScrollPan(self.sp_infolist, self.cvs_detailinfo, #self.masterdatalist.battleRecordList, function(node,index)
            InitBattleInfo(self,node,self.masterdatalist.battleRecordList[index])
        end)
    end
    self.cvs_detailinfo.Visible = false

    self.btn_close.TouchClick = function(sender)
        self.cvs_battleinfo.Visible = false
    end
end

local function ClearUI(self)
    if self.model then
        for i, v in pairs(self.model) do
            Release3DModel(self,i)
            v.infonode:RemoveFromParent(true)
        end
    end
    if self.timer then
        LuaTimer.Delete(self.timer)
        self.timer = nil
    end
    self.model = {}
end

local function SetOtherInfo(self)
    local tempotherinfo = PlayRuleModel.GetShowData(mypro)
    for _, v in pairs(tempotherinfo) do
        if v.job == mypro and v.imagearena_type == self.masterdatalist.curMasterId then
            self.lb_mypos.Text = Util.GetText(v.name)
        end
    end
end

local function ShowAction(self,job)
    local tempx = 0
    local startemply = 45
    local endemply = 20
    local tempgap = (self.min_bg.Size2D[1]- startemply-endemply)/2
    for i, v in pairs(self.cvs_pro) do
        if type(i) == "number" then
            if job ~= i then
                self.min_bg:AddChild(v.node)
                v.node.Scale = Vector2(0.6,0.6)
                v.node.Position2D = Vector2(tempx*tempgap+ startemply,15)
                tempx = tempx + 1
            else
                self.cvs_pro.parent:AddChild(v.node)
                v.node.Scale = Vector2.one
                v.node.Position2D = Vector2.zero
            end
        end
    end

    local startpos,endpos,isshow
    if self.cvs_pro.isopen then
        startpos,endpos = self.min_bg.X,self.min_bg.X-self.min_bg.Size2D[1]
    else
        startpos,endpos = self.min_bg.X,self.min_bg.X+self.min_bg.Size2D[1]
    end
    local move = MoveAction()
    move.Duration = 0.1
    move.TargetX = endpos
    move.TargetY = self.min_bg.Y
    move.ActionEaseType = EaseType.linear
    move.ActionFinishCallBack = function(sender)
        self.cvs_pro.isopen = not self.cvs_pro.isopen
        if not self.cvs_pro.isopen and showedJob ~= job then
            showedJob = job
            UIUtil.SetImage(self.ib_background,self.cvs_pro[showedJob].node.UserData)
            self:StartSetUI()
        end
    end

    local fadeaction = FadeAction()
    fadeaction.Duration = self.cvs_pro.isopen and 0.03 or 0.3
    fadeaction.TargetAlpha = self.cvs_pro.isopen and 0 or 1
    fadeaction.ActionEaseType = EaseType.linear
    fadeaction.ActionFinishCallBack = function(sender)

    end
    self.min_bg:AddAction(fadeaction)
    self.min_bg:AddAction(move)

end

local function SelectFaction(self)
    self.cvs_pro = {}
    local resData = PlayRuleModel.GetJobRes()
    local cvs_shimenname = self.cvs_switchshimen:FindChildByEditName('cvs_shimenname', true)
    self.min_bg = self.cvs_switchshimen:FindChildByEditName('cvs_bg', true)
    if not min_bgposX then
        min_bgposX = self.min_bg.X - self.min_bg.Size2D[1]
    end

    for i, v in pairs(resData) do
        self.cvs_pro[v.job] = {}
        self.cvs_pro[v.job].node = cvs_shimenname:Clone()
        self.min_bg:AddChild(self.cvs_pro[v.job].node)
        self.cvs_pro[v.job].node.UserTag = v.job
        self.cvs_pro[v.job].node.UserData = v.backgroud_res
        UIUtil.SetImage(self.cvs_pro[v.job].node,v.art_res)
        self.cvs_pro[v.job].node.TouchClick = function(sender)
            ShowAction(self,sender.UserTag)
        end
        if v.job == showedJob then
            UIUtil.SetImage(self.ib_background,v.backgroud_res)
        end
    end
    
    self.cvs_pro.parent = cvs_shimenname.Parent
    self.cvs_pro.isopen = false
    self.min_bg.Alpha = 0
    self.min_bg.X = min_bgposX
    cvs_shimenname.Visible = false
    self.cvs_pro.parent:AddChild(self.cvs_pro[showedJob].node)

    self.cvs_pro.parent.Parent.TouchClick = function(sender)
        ShowAction(self,showedJob)
    end
end

function _M.StartSetUI(self,param)
    ClearUI(self)
    GettFriendList( self,function ()
        SetCenterInfo(self,function ()
            _M.SetBattleList(self)
            SetOtherInfo(self)
            if param == 'battlelist' then
                ShowBattleInfo(self)
                self.cvs_battleinfo.Visible = true
            end
        end)
    end)
end

local function ShowReward(self)
    self.cvs_check.Visible = true
    local temptable = {}
    for i, v1 in ipairs(basedata) do
        if v1.is_showreward == 1 then
            local temp = {}
            for i, v2 in pairs(v1.item_show) do
                if v2 ~= 0 then
                    table.insert(temp,v2)
                end
            end
            v1.item_show = temp
            table.insert(temptable,v1)
        end
    end
    UIUtil.ConfigVScrollPan(self.sp_checklist,self.cvs_rewardlist,#temptable,function(node,index)
        local lb_titletips = node:FindChildByEditName('lb_titletips', true)
        local cvs_checkitem = {}
        for i = 1, 4 do
            cvs_checkitem[i] = node:FindChildByEditName('cvs_checkitem'..i, true)
            if temptable[index].item_show[i] then
                if temptable[index].item_show[i] == 0 then
                    cvs_checkitem[i].Visible = false
                else
                    cvs_checkitem[i].Visible = true
                    UIUtil.SetItemShowTo(cvs_checkitem[i],temptable[index].item_show[i],temptable[index].item_show_num[i])
                    cvs_checkitem[i].TouchClick = function(sender)
                        UIUtil.ShowTips(self,sender,temptable[index].item_show[i])
                    end
                end
            else
                cvs_checkitem[i].Visible = false
            end
        end

        lb_titletips.Text = Util.GetText(temptable[index].name)..Util.GetText("common_award")

        --UIUtil.ConfigHScrollPan(sp_checkreward,cvs_checkitem,#temptable[index].item_show,function (node1,index1)
        --    UIUtil.SetItemShowTo(node1,temptable[index].item_show[index1],temptable[index].item_show_num[index1])
        --end)
        cvs_checkitem.Visible = false
    end)
    self.cvs_rewardlist.Visible = false
end

function _M.OnEnter(self,param)
    self.model = {}
    mypro = DataMgr.Instance.UserData.Pro
    myroleid = DataMgr.Instance.UserData.RoleID
    showedJob = mypro
    if not defultData then
        defultData = PlayRuleModel.GetDefultData()
    end

    self:StartSetUI(param)
    SelectFaction(self)

    
    
    self.bt_battleinfo.TouchClick = function(sender)
        ShowBattleInfo(self)
        self.cvs_battleinfo.Visible = true
    end

    local refreshtime = GlobalHooks.DB.GetGlobalConfig('masterrace_refresh')
    local time = refreshtime
    self.refreshbtntimer = LuaTimer.Add(0,1000,function()
        if time>0 then
            time = time -1
        end
        return true
    end)
    self.ui.comps.btn_refresh.TouchClick = function(sender)
        if time <= 0 then
            time = refreshtime
            self:StartSetUI()
        else
            GameAlertManager.Instance:ShowNotify(Util.GetText("masterrace_refresh_text"))
        end
    end
    
    self.ui.comps.btn_help.TouchClick = function(sender)
        self.ui.comps.cvs_help.Visaible = true
    end
    local btn_close = self.ui.comps.cvs_help:FindChildByEditName('btn_close', true)
    btn_close.TouchClick = function(sender)
        self.ui.comps.cvs_help.Visible = false
    end

    self.cvs_check.Visible = false
    self.btn_check.TouchClick = function(sender)
        ShowReward(self)
    end

    self.btn_close2.TouchClick = function(sender)
        self.cvs_check.Visible = false
    end
end

function _M.OnInit( self )
    self.cvs_main = self.root:FindChildByEditName('cvs_main', true)
    self.cvs_role = self.root:FindChildByEditName('cvs_role', true)
    self.cvs_battlelist = self.root:FindChildByEditName('cvs_battlelist', true)
    self.cvs_chooserelative = self.root:FindChildByEditName('cvs_chooserelative', true)
    self.cvs_battleinfo = self.root:FindChildByEditName('cvs_battleinfo', true)
    self.cvs_changename = self.root:FindChildByEditName('cvs_changename', true)
    self.ib_background = self.root:FindChildByEditName('ib_background', true)
    self.cvs_check = self.root:FindChildByEditName('cvs_check', true)
    

    --左面信息栏
    self.cvs_switchshimen = self.cvs_main:FindChildByEditName('cvs_switchshimen', true)
    self.bt_battleinfo = self.cvs_main:FindChildByEditName('bt_battleinfo', true)
    self.lb_mypos = self.cvs_main:FindChildByEditName('lb_mypos', true)
    self.btn_check = self.cvs_main:FindChildByEditName('btn_check', true)
    

    --中间展示区域
    self.sp_rolelist = self.cvs_role:FindChildByEditName('sp_rolelist', true)
    self.bt_up = self.cvs_role:FindChildByEditName('bt_up', true)
    self.bt_down = self.cvs_role:FindChildByEditName('bt_down', true)
    self.cvs_roleinfo = self.cvs_role:FindChildByEditName('cvs_roleinfo', true)
    self.cvs_rolemax = self.cvs_role:FindChildByEditName('cvs_rolemax', true)

    --挑战列表信息
    self.sp_targetlist = self.cvs_battlelist:FindChildByEditName('sp_targetlist', true)
    self.cvs_target = self.cvs_battlelist:FindChildByEditName('cvs_target', true)
    self.tb_leaderdesc = self.cvs_battlelist:FindChildByEditName('tb_leaderdesc', true)
    ib_arrowup = self.cvs_battlelist:FindChildByEditName('ib_arrowup', true)
    ib_arrowdown = self.cvs_battlelist:FindChildByEditName('ib_arrow', true)

    --选择亲信tips
    self.bt_confirm = self.cvs_chooserelative:FindChildByEditName('bt_confirm', true)
    self.sp_friendlist = self.cvs_chooserelative:FindChildByEditName('sp_friendlist', true)
    self.cvs_friend = self.cvs_chooserelative:FindChildByEditName('cvs_friend', true)
    self.bt_close = self.cvs_chooserelative:FindChildByEditName('bt_close', true)

    --战斗信息tips
    self.cvs_detailinfo = self.cvs_battleinfo:FindChildByEditName('cvs_detailinfo', true)
    self.sp_infolist = self.cvs_battleinfo:FindChildByEditName('sp_infolist', true)
    self.btn_close = self.cvs_battleinfo:FindChildByEditName('btn_close', true)
    
    --更改称谓tips
    self.ti_newname = self.cvs_changename:FindChildByEditName('ti_newname', true)
    self.lb_countdown = self.cvs_changename:FindChildByEditName('lb_countdown', true)
    self.lb_countdowntext = self.cvs_changename:FindChildByEditName('lb_countdowntext', true)
    self.btn_no = self.cvs_changename:FindChildByEditName('btn_no', true)
    self.btn_confirm = self.cvs_changename:FindChildByEditName('btn_confirm', true)
    
    --奖励预览
    self.btn_close2 = self.cvs_check:FindChildByEditName('btn_close2', true)
    self.sp_checklist = self.cvs_check:FindChildByEditName('sp_checklist', true)
    self.cvs_rewardlist = self.cvs_check:FindChildByEditName('cvs_rewardlist', true)

    UILayerMgr.SetPositionZ(self.cvs_battleinfo.UnityObject,-2000)
    UILayerMgr.SetPositionZ(self.cvs_chooserelative.UnityObject,-2000)
    UILayerMgr.SetPositionZ(self.cvs_changename.UnityObject,-2000)
    UILayerMgr.SetPositionZ(self.ui.comps.cvs_help.UnityObject,-2000)
    UILayerMgr.SetPositionZ(self.cvs_check.UnityObject,-2000)
    UILayerMgr.SetPositionZ(self.cvs_switchshimen.UnityObject,-1000)
    UILayerMgr.SetPositionZ(self.bt_up.UnityObject,-1000)
    UILayerMgr.SetPositionZ(self.bt_down.UnityObject,-1000)
    UILayerMgr.SetPositionZ(self.ib_background.UnityObject,500)
    DisplayUtil.adaptiveFullSceen(self.ib_background)
end

function _M.CancelRelative(cb)
    local content = Util.GetText(Constants.PlayRule.CancelRelativeTips)
    UIUtil.ShowConfirmAlert(content, nil, function ()
        if cb then
            cb()
        end
    end)
end

function _M.ChooseRelative(self,cb)
    if self.masterdatalist then
        local time = -TimeUtil.TimeLeftSec( self.masterdatalist.AppointCD)
        if time > 0 then
            local cdtime = TimeUtil.FormatToCN( time )
            GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.PlayRule.CanNotSelectRelative,cdtime))
        else
            selectedRelative = nil
            GettFriendList( self,function()
                self.cvs_chooserelative.Visible = true
                UIUtil.ConfigVScrollPan(self.sp_friendlist, self.cvs_friend, #friendList, function(node,index)
                    InitFriendList(self,node,friendList[index],index)
                end)
                self.cvs_friend.Visible = false

                self.bt_confirm.TouchClick = function(sender)
                    if selectedRelative then
                        local content = Util.Format1234(Constants.PlayRule.AddRelativeTips,friendList[selectedRelative].roleName)
                        local alertKey = UIUtil.ShowConfirmAlert(content, nil, function ()
                            if cb then 
                                cb(friendList[selectedRelative].roleId,self.cvs_chooserelative)
                            end
                        end)
                        GameAlertManager.Instance.AlertDialog:SetDialogAnchor(alertKey,CommonUI.TextAnchor.C_C)
                    else
                        GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.PlayRule.NotSelectFrient))
                    end
                end
            end)
            self.bt_close.TouchClick = function(sender)
                self.cvs_chooserelative.Visible = false
            end
        end
    end
end

function _M.ChangeName(self,cb)
    self.cvs_changename.Visible = true
    local time = nil
    if self.renametimer then
        LuaTimer.Delete(self.renametimer)
        self.renametimer = nil
    end
    self.ti_newname.Input.characterLimit = GlobalHooks.DB.GetGlobalConfig('masterrace_friendname_max')
    self.renametimer = LuaTimer.Add(0,1000,function()
        time = -TimeUtil.TimeLeftSec(self.masterdatalist.renameCD)
        if time < 0 then
            self.lb_countdown.Visible = false
            self.lb_countdowntext.Visible = false
            self.btn_confirm.Enable = true
            self.btn_confirm.IsGray = false
            LuaTimer.Delete(self.renametimer)
            return false
        else
            self.lb_countdown.Visible = true
            self.lb_countdowntext.Visible = true
            self.lb_countdown.Text = TimeUtil.FormatToCN( time )
            self.btn_confirm.Enable = false
            self.btn_confirm.IsGray = true
            return true
        end
    end)
    self.btn_no.TouchClick = function(sender)
        if self.renametimer then
            LuaTimer.Delete(self.renametimer)
            self.renametimer = nil
        end
        self.cvs_changename.Visible = false
    end
    self.btn_confirm.TouchClick = function(sender)
        if cb then
            cb(Util.FilterBlackWord(self.ti_newname.Text),self.cvs_changename)
            if self.renametimer then
                LuaTimer.Delete(self.renametimer)
                self.renametimer = nil
            end
        end
    end
end

function _M.OnExit(self)
    --self.sp_rolelist.Scrollable.event_HideCell = {"-=",HideEvent}
    if self.model then
        for i, v in pairs(self.model) do
            Release3DModel(self,i)
            v.infonode:RemoveFromParent(true)
        end
    end

    if self.renametimer then
        LuaTimer.Delete(self.renametimer)
        self.renametimer = nil
    end

    if self.timer then
        LuaTimer.Delete(self.timer)
        self.timer = nil
    end
    
    if self.refreshbtntimer then
        LuaTimer.Delete(self.refreshbtntimer)
        self.refreshbtntimer = nil
    end

    if self.cvs_pro then
        for i, v in pairs(self.cvs_pro) do
            if type(i) == "number" then
                v.node:RemoveFromParent(true)
            end
            v = nil
        end
        self.cvs_pro = nil
    end
    self.sp_targetlist.Scrollable.event_HideCell = {'-=', HideCell}
    self.sp_targetlist.Scrollable.event_ShowCell = {'-=', ShowCell}

    friendList = nil
end
return _M