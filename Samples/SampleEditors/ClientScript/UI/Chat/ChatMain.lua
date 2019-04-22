local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'

local ChatScrollView = require 'UI/Chat/ChatScrollView'
local TLChatSend = require 'UI/Chat/ChatSend'
local ChatToolBar = require 'UI/Chat/ChatToolBar'

local ChatModel = require 'Model/ChatModel'
local ChatData = ChatModel.ChatData

local ModalMask = require 'UI/Chat/ModalMask'

local _M = {}
_M.__index = _M


local function privateChatRole(self,roleId,roleName)
    -- body
    self.ui.comps.lb_siliaoname.Text = Util.GetText('chat_siliao',roleName)
    self.privateChatScrollView:setPrivateData(roleId)
    ChatModel.AddChatRoleMap(roleId,roleName)
end  

local function doChatRole(self,node,acceptData,roleData,index,redPoint)
    self.SelectedRoleNode = node
    self.SelectedRoleNode.IsChecked = true
    self.selectedRoleId = roleData.roleId
    TLChatSend.SetAcceptRoleData(self,acceptData)
    privateChatRole(self,roleData.roleId,roleData.roleName,index)
    ChatModel.PrivateChatRed[roleData.roleId] = false
    redPoint.Visible = false
end

local function ShowPrivateChatRole(self,cvs,index)
    local node = UIUtil.FindChild(cvs,'btn_siliao1')
    local redPoint = UIUtil.FindChild(cvs,'lb_siliaored1')
    redPoint.Visible = false

    local roleData = ChatModel.ChatRoles[index]
    if string.utf8len(roleData.roleName) > 3 then
        node.Text = string.utf8sub(roleData.roleName,1,3) .. '...'
    else
        node.Text = roleData.roleName
    end

    local acceptData = {}
    acceptData.playerId = roleData.roleId
    acceptData.name = roleData.roleName
    if  (self.selectedIndex == nil and self.selectedRoleId == nil and index == 1) then
        doChatRole(self,node,acceptData,roleData,index,redPoint)
    elseif roleData.roleId == self.selectedRoleId then
        doChatRole(self,node,acceptData,roleData,index,redPoint)         
    else
        redPoint.Visible = ChatModel.PrivateChatRed[roleData.roleId] and true or false
        node.IsChecked = false
    end

    node.TouchClick = function (sender)
        -- body
        if self.SelectedRoleNode ~= nil then
            self.SelectedRoleNode.IsChecked = false
        end
        
        doChatRole(self,node,acceptData,roleData,index,redPoint)

    end

end

local function CleanPrivateChat(self)
    -- body
    self.ui.comps.btn_delete.Visible = false
    self.ui.comps.lb_siliaoname.Text = ""
    self.privateChatScrollView:ClearMessage()
    TLChatSend.SetAcceptRoleData(self,nil)
end  

local function showRoleScrollPan(self)
    local function eachupdatecb(node, index)
        ShowPrivateChatRole(self,node,index)
    end

    local roleCount = #ChatModel.ChatRoles < 20 and #ChatModel.ChatRoles or 20
    -- print('#ChatModel.ChatRoles:',#ChatModel.ChatRoles)
    -- print('roleCount:',roleCount)
    -- print_r('ChatModel.ChatRoles:',ChatModel.ChatRoles)

    if self.selectedIndex and self.selectedIndex > roleCount then
        self.selectedIndex = roleCount
    end

    UIUtil.ConfigVScrollPan(self.rolePan,self.roleTemp,roleCount,eachupdatecb)

    if roleCount == 0 then
        CleanPrivateChat(self)
    else
        self.ui.comps.btn_delete.Visible = true
    end
end

local function OnPrivateChatAddRole(self,eventname, params)
    -- local roleCount = #ChatModel.ChatRoles < 20 and #ChatModel.ChatRoles or 20
    -- print('OnPrivateChatAddRole params:',params)
    if self.currentChannel == ChatModel.ChannelState.CHANNEL_PRIVATE then
        if params.byself == true then
            self.selectedRoleId = params.roleId
        end
        showRoleScrollPan(self)
    else
        self.ui.comps.lb_siliaored.Visible = true
    end
end

local function ShowPrivateChat(self)
    -- body
    self.currentChannel = ChatModel.ChannelState.CHANNEL_PRIVATE
    TLChatSend.InitChannel(self,ChatModel.ChannelState.CHANNEL_PRIVATE)

    self.privateMenuCvs.Visible = true
    self.privateContentCvs.Visible = true
 
    self.menuCvs.Visible = false 
    self.contentCvs.Visible = false

    showRoleScrollPan(self)
end

local function OnPrivateChatSendMessage(self,eventname, params)
    -- local roleCount = #ChatModel.ChatRoles < 20 and #ChatModel.ChatRoles or 20
    -- print_r('OnPrivateChatSendMessage params:',params)
    if self.currentChannel ~= ChatModel.ChannelState.CHANNEL_PRIVATE then
        ChatModel.AddChatRole(params.roleId,params.roleName)
        self.privateChatBtn.IsChecked = true
        ShowPrivateChat(self)
    end
end

local function DeleteMessage(self)
    -- body
    if self.selectedRoleId then
        ChatModel.RemovePrivateChat(self.selectedRoleId)
    end
    showRoleScrollPan(self)
end

local function ShowOtherChat(self)
    -- body
    self.ui.comps.btn_an1.IsChecked = true

    self.menuCvs.Visible = true
    self.contentCvs.Visible = true

    self.privateMenuCvs.Visible = false
    self.privateContentCvs.Visible = false
end

local function PrivatChatClick(self)
    -- body
     if self.currentChannel ~= ChatModel.ChannelState.CHANNEL_PRIVATE then
        self.ui.comps.lb_siliaored.Visible = false
        ShowPrivateChat(self)
    else
        ShowOtherChat(self)
    end
end

function _M:switchChannel(channel)
    self.currentChannel = channel
    if(channel == ChatModel.ChannelState.CHANNEL_PRIVATE) then
        self.ui.comps.lb_siliaored.Visible = false
    else
        self.selectedIndex = nil
        self.selectedRoleId = nil
        self.ui.comps.lb_siliaored.Visible = ChatModel.GetPrivateChannelRed()
    end
    TLChatSend.InitChannel(self,channel)
    self.chatScrollView:setData(channel)
end

function _M:chatPushCb(param)
    if(param.channel_type == self.currentChannel) then
        if param.channel_type == ChatModel.ChannelState.CHANNEL_PRIVATE then
            if self.selectedRoleId == param.from_uuid or self.selectedRoleId == param.to_uuid then
                self.privateChatScrollView:AppendMessage(param)
            end
        else
            self.chatScrollView:AppendMessage(param)
            -- ChatModel.RedPoint[param.channel_type].showPoint = false
            -- ChatModel.RemoveMessageData(self.currentChannel)
        end
    
    else
        -- if self["ib_" .. param.channel_type] then
        --     self["ib_" .. param.channel_type].Visible = ChatModel.RedPoint[param.channel_type].showPoint
        -- end
    end
end

function _M:SmoothClose()
    local move_pingfen = MoveAction()
    move_pingfen.Duration = 0.3
    move_pingfen.TargetX = -500
    move_pingfen.TargetY = self.ui.menu.Position2D.y
    move_pingfen.ActionEaseType = EaseType.linear

    move_pingfen.ActionFinishCallBack = function(sender)
        self.ui:Close()
        self.ui.menu.X = 0
    end
    self.ui.menu:AddAction(move_pingfen)
end

-- 弹出框互斥显示(同一时间只能有一个弹出框显示)
-- 如果弹出框需要互斥, 就要调用这个来显示
function _M:ShowPopup(popui)
    print('ChatMainSmall:',popui)
    if self.currentPopup ~= nil and self.currentPopup.ui ~= nil and self.currentPopup ~= popui then
        self.currentPopup.ui:Close()
        self.currentPopup = nil
    end
    self.currentPopup = popui
end

-- 关闭弹出框
function _M:HidePopup()
    if self.currentPopup ~= nil and self.currentPopup.ui ~= nil then
        self.currentPopup.ui:Close()
        self.currentPopup = nil
    end
end


local function OnChatMakeAction(self,eventname,params)
    local ChatActionUI = GlobalHooks.UI.OpenUI('ChatUIAction', 0)
    ChatActionUI.faceCb = function(actionData)
        TLChatSend.MakeTLAction(self,actionData,params)
    end
    self:ShowPopup(ChatActionUI)
end

function _M:OnEnter(channel,selectedRoleId)
    local function ChannelTogFunc(sender)
        local tag = sender.Tag
        self:switchChannel(tag)
    end

    self.currentChannel = channel
    self.selectedRoleId = selectedRoleId
    local defaultTog = self.tbts[self.currentChannel] or self.ui.comps.btn_an1
    UIUtil.ConfigToggleButton(self.tbts, defaultTog, false, ChannelTogFunc)
    TLChatSend.OnEnter(self,self.usingHorn)
    ChatModel.AddChatPushListener("ChatMainPush", function(msg) 
        self:chatPushCb(msg) 
    end)

    function _M.OnChatAddRole(eventname, params)
        -- print('OnChatAddRole:',params)
        OnPrivateChatAddRole(self,eventname,params)
    end

    function _M.OnChatSendMessage(eventname, params)
        -- print('OnChatAddRole:',params)
        OnPrivateChatSendMessage(self,eventname,params)
    end

    function _M.OnMakeAction(eventname, params)
        -- print('OnMakeAction:',params)
        OnChatMakeAction(self,eventname,params)
    end

    EventManager.Subscribe('Event.Chat.AddPrivateChat', _M.OnChatAddRole)
    EventManager.Subscribe('Event.Chat.SendPrivateMsg', _M.OnChatSendMessage)
    EventManager.Subscribe('Event.Chat.MakeAction', _M.OnMakeAction)


    if channel == ChatModel.ChannelState.CHANNEL_PRIVATE then
        self.privateChatBtn.IsChecked = true
        self.ui.comps.lb_siliaored.Visible = false
        ShowPrivateChat(self)
    end

 
    self.tbts[4].Visible = true
    self.tbts[7].Visible = false 

    local MapIds = GlobalHooks.DB.GetGlobalConfig('chat_battlechat')
    for k,v in ipairs(string.split(MapIds,',') or {}) do
         -- print(k,v)
        if DataMgr.Instance.UserData.MapTemplateId == tonumber(v) then
            self.tbts[4].Visible = false 
            self.tbts[7].Visible = true
        end
    end

end

function _M:OnExit()
    self:HidePopup()
    self.ui.comps.btn_change.IsChecked = false
    self.menuCvs.Visible = true
    self.contentCvs.Visible = true

    self.privateMenuCvs.Visible = false
    self.privateContentCvs.Visible = false

    EventManager.Unsubscribe('Event.Chat.AddPrivateChat',_M.OnChatAddRole)
    EventManager.Unsubscribe('Event.Chat.SendPrivateMsg',_M.OnChatSendMessage)
    EventManager.Unsubscribe('Event.Chat.MakeAction', _M.OnMakeAction)

    ChatModel.RemoveChatPushListener("ChatMainPush")

    ChatModel.SaveChatData()
    self.selectedIndex = nil
    self.selectedRoleId = nil
end

local function InitChat(self)
    --左侧按钮
    self.menuCvs = self.ui.comps.cvs_zonghe
    self.privateMenuCvs = self.ui.comps.cvs_siliao

    --右侧聊天内容
    self.contentCvs = self.ui.comps.cvs_message
    self.privateContentCvs = self.ui.comps.cvs_messagesiliao

    self.privateChatBtn = self.ui.comps.btn_change
    self.privateChatBtn.TouchClick = function (sender)
        PrivatChatClick(self)
    end

    local addBtn = self.ui.comps.btn_add
    addBtn.TouchClick = function (sender)
        GlobalHooks.UI.OpenUI('ChatInvite', 0)
    end

    local chatNodeCvs = self.ui.comps.cvs_role1
    chatNodeCvs.Visible = false

    local chatPan = self.ui.comps.scl_message
    local chatTips = self.ui.comps.cvs_tips1
    local tipsLabel = self.ui.comps.lb_text1
    self.chatScrollView = ChatScrollView.Create(chatPan,chatNodeCvs, chatTips,tipsLabel)
    

    local privateChatPan = self.ui.comps.scl_messagesiliao
    local privateChatTips = self.ui.comps.cvs_tips2
    local privateTipsLabel = self.ui.comps.lb_text2

    self.privateChatScrollView = ChatScrollView.Create(privateChatPan,chatNodeCvs, privateChatTips,privateTipsLabel)


    self.rolePan = self.ui.comps.sp_siliao
    self.roleTemp = self.ui.comps.cvs_siliao1
    self.roleTemp.Visible = false
end



function _M:OnInit()
    self.ui.comps.btn_close.TouchClick = function()
        self:HidePopup()
        self.ui:Close()
    end

    self.ui.comps.tbt_shrink.TouchClick = function( ... )
        self:HidePopup()
        self.ui:Close()
        GlobalHooks.UI.OpenUI('ChatMainSmall', 0, self.currentChannel,self.selectedRoleId)
    end

    local tbts = {}
    for i=1,ChatModel.ChannelState.CHANNEL_MAX do
        local togButton = self.ui.comps['btn_an' .. i]
        if togButton then
            togButton.Tag = i
            tbts[i] = togButton
        end
    end
    self.tbts = tbts

   

    TLChatSend.Init(self)
    ChatToolBar.Init(self,self.ui.comps.cvs_lable)

    InitChat(self)

    self.ui.comps.btn_delete.TouchClick = function ( ... )
        -- body
        DeleteMessage(self)
    end

    self.posAction = Vector2(709, 327)
    self.posFace = Vector2(709, 327)
    self.posHistory = Vector2(709, 327)
    self.posItemShow = Vector2(709, 327)
end

function _M:OnDestroy()
    ChatModel.SaveChatData()
    self.selectedIndex = nil
    self.selectedRoleId = nil
end

return _M
