local _M = {}
 
local PAGESIZE = 20
local EDGESIZE = 0

local EXPAND_OFFSET = 160
local SHRINK_OFFSET = -56

local ringbuffer    = require "Logic/ringbuffer"
local List    = require "Logic/List"
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local ChatUtil  = require "UI/Chat/ChatUtil"
local ChatModel = require 'Model/ChatModel'
local ItemModel = require 'Model/ItemModel'
local ChatHudNode = require 'UI/Chat/ChatHudNode'

local self = {}
function _M.Bind(root)
	setmetatable(self, {__index=_M})
	self:Init(root)
	return self
end

local function MakeText(contentNode,msg)
    if msg.HudAText then
        return msg.HudAText
    end
    contentNode.TextComponent.RichTextLayer:SetLineSpace(1)
    local defaultTextAttr = contentNode.TextComponent.RichTextLayer.DefaultTextAttribute
    local channel_img = ChatModel.mSettingItems[msg.channel_type].channel_img
    if msg.channel_type == ChatModel.ChannelState.CHANNEL_PRIVATE and msg.from_name == nil then
        channel_img = ChatModel.mSettingItems[ChatModel.ChannelState.CHANNEL_SYSTEM].channel_img
    end
    local xmlText = string.format('<recipe><h img="%s" >T</h></recipe>',string.gsub(channel_img, '|', ','))
    local Atext = UIFactory.Instance:DecodeAttributedString(xmlText, defaultTextAttr)
    local nameTextAttr = TextAttribute(defaultTextAttr)
    GameUtil.SetTextAttributeFontColorRGB(nameTextAttr,Constants.QualityColor[1])
    if msg.from_name ~= nil then
        if msg.channel_type == ChatModel.ChannelState.CHANNEL_PRIVATE then
            if msg.from_uuid == DataMgr.Instance.UserData.RoleID then
                --我对{0}说
                local toName = ChatModel.ChatRoleMap[msg.to_uuid]
                local nameStr = Util.GetText('chat_siliao_msg1',toName)
                Atext:Append(nameStr .. ": ", nameTextAttr)
            else
                --{0}对你说
                local nameStr = Util.GetText('chat_siliao_msg2',msg.from_name) 
                Atext:Append(nameStr .. ": ", nameTextAttr)
            end
        else
           Atext:Append(msg.from_name .. ": ", nameTextAttr)     
        end
    end
    
    Atext:Append(ChatUtil.HandleChatClientDecode(msg.content, GameUtil.GetTextAttributeFontColorRGB(defaultTextAttr), nil, nil, defaultTextAttr.fontSize,not msg.from_name or msg.isSys))
    msg.HudAText = Atext
    return Atext
end

local  function MakeLabaText(contentNode,msg)
    local defaultTextAttr = contentNode.TextComponent.RichTextLayer.DefaultTextAttribute
    if msg.from_name ~= nil then
        local Atext = AttributedString()
        local nameTextAttr = TextAttribute(defaultTextAttr)
        GameUtil.SetTextAttributeFontColorRGB(nameTextAttr,Constants.QualityColor[1])
        local name = AttributedString(msg.from_name .. ": ", nameTextAttr)
        Atext:Append(name)
        local message = ChatUtil.HandleChatClientDecode(msg.content, GameUtil.GetTextAttributeFontColorRGB(defaultTextAttr), nil, nil, defaultTextAttr.fontSize,msg.isSys)
        Atext:Append(message)
        return Atext
    else
        local message = ChatUtil.HandleChatClientDecode(msg.content, GameUtil.GetTextAttributeFontColorRGB(defaultTextAttr), nil, nil, defaultTextAttr.fontSize,msg.isSys)
        
        return message
    end
end

local function showChatHudNode(self,node,index)
    -- index = index % ChatModel.messageMaxCout
    if index > self.chatRecords.length then
        index = index % self.chatRecords.length
    end

    local dataIndex = self.chatRecords.length + 1 - index
  
    local msg = self.chatRecords[dataIndex]
    
    if msg == nil then
        print('self.chatRecords.length ,dataIndex,index:',self.chatRecords.length,dataIndex,index)
        return
    end

    node.event_PointerClick = function(sender)
        if msg == nil then
            print('self.chatRecords.length ,dataIndex,index:',self.chatRecords.length,dataIndex,index)
            return
        end

        if GlobalHooks.UI.FindUI('ChatMainSmall') == nil then
            if msg.channel_type == ChatModel.ChannelState.CHANNEL_PRIVATE then
                --TODO
                local selectedRoleId
                if msg.is_myself == true then
                    selectedRoleId = msg.to_uuid
                else
                    selectedRoleId = msg.from_uuid
                end
                -- print('ChatMainSmall11111111111111111111111:',selectedRoleId)
                GlobalHooks.UI.OpenUI('ChatMainSmall', 0, msg.channel_type,selectedRoleId)
            elseif msg.channel_type == ChatModel.ChannelState.CHANNEL_SYSTEM then
                GlobalHooks.UI.OpenUI('ChatMainSmall', 0, ChatModel.ChannelState.CHANNEL_WORLD)
            else
                GlobalHooks.UI.OpenUI('ChatMainSmall', 0, msg.channel_type)
            end 
        end
    end

    local contentNode = UIUtil.FindChild(node,'thb_content')
    contentNode.AText = MakeText(contentNode,msg)
    contentNode.Height = contentNode.TextComponent.RichTextLayer.ContentHeight
    node.Height = contentNode.Height + self.offset 
end


local function ConfigAutoScrollPan(pan, tempnode, row,expandSize, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gy + 1)
    end
    local s = tempnode.Size2D
    pan:Initialize(
        s.x,
        s.y,
        row,
        expandSize,
        tempnode,
        UpdateListItem,
        function()
        end
    )
end

local function showScrollPan(self)
    local function eachupdatecb(node, index) 
        showChatHudNode(self,node,index)
    end
    
    self.chatRecords = self.chatRecords or ringbuffer:new(ChatModel.messageMaxCout)
    ConfigAutoScrollPan(self.scrollPan,self.template,self.chatRecords.length,8,eachupdatecb)
end

function _M:Init(root)
	-- ChatModel = require 'Model/ChatModel'
 --    ItemModel = require 'Model/ItemModel'
 --    ChatData = ChatModel.ChatData

	-- ChatHudNode = require 'UI/Chat/ChatHudNode'
	self.root = root

    self.cvs_laba = root:FindChildByEditName("cvs_laba", true)
    self.tbh_laba = root:FindChildByEditName("tbh_laba", true)
    self.cvs_laba.Visible = false
    self.labaX = self.tbh_laba.X
    self.labaY = self.tbh_laba.Y
    self.cvs_laba.TouchClick = function()
         if GlobalHooks.UI.FindUI('ChatMainSmall') == nil then
           GlobalHooks.UI.OpenUI('ChatMainSmall', 0, ChatModel.ChannelState.CHANNEL_HORN)
        end
    end

	self.scrollPan = root:FindChildByEditName("sp_huadong", true)
	self.template = root:FindChildByEditName("cav_record", true)
	self.template.Visible = false
    self.templateY = self.template.Size2D.y
    local contentNode = UIUtil.FindChild(self.template,'thb_content')
    self.offset = (self.template.Height - contentNode.Height) * 2

    self.cvs_liaotian = self.root:FindChildByEditName("cvs_liaotian", true)

    self.tbt_kuozhan = self.root:FindChildByEditName("tbt_kuozhan", true)
    self.tbt_kuozhan.IsChecked = false
    self.tbt_kuozhan.event_PointerClick = function()
        -- if self.expand <= 0 then
        --     self:ExpandPanel(EXPAND_OFFSET)
        -- else
        --     self:ExpandPanel(self.shrink_mode and SHRINK_OFFSET or 0)
        -- end
    end

    self.lastScrollY = 0
    self.scrollPan.Scrollable.event_PointerClick = function (sender, e)
        -- print('聊天e.dragging:',e.dragging)
        if not e.dragging then -- 判断一下是否处于拖动状态
            if GlobalHooks.UI.FindUI('ChatMainSmall') == nil then
                GlobalHooks.UI.OpenUI('ChatMainSmall', 0)
                SoundManager.Instance:PlaySoundByKey('uiopen',false)
            else
               -- print('ChatMainSmall还存在!!!!!!!')
            end
        end
    end

    self.reading = false
    self.scrollPan.Scrollable.event_PointerDown = function ( ... )
        self.reading = true
    end

    self.scrollPan.Scrollable.event_PointerUp = function ( ... )
        self.reading = false
    end

    self.smallSize = true

    self.scrollPan.Scrollable.event_Scrolled = function(panel, e)
         
        -- if panel.Container.Height + panel.Container.Y <= panel.ScrollRect2D.height + self.ui.comps.cvs_letter.Height then --底部
        --     self.ui.comps.ib_down.Visible = false
        -- end
        -- print('1111111111111111111111111111111')
        -- if (panel.Container.Height + panel.Container.Y <= panel.ScrollRect2D.height) then
        --     print('222222222222222222222222222222222222222222')
        --     panel:LookAt(Vector2(0, panel.Container.Y - panel.ScrollRect2D.height));
        -- end

    end



    self.ib_line = self.root:FindChildByEditName("ib_line", true)
    self.lb_yuyin = self.root:FindChildByEditName("lb_yuyin", true)
    self.btn_yuyin = self.root:FindChildByEditName("btn_yuyin", true)
    self.btn_shezhi = self.root:FindChildByEditName("btn_shezhi", true)

   
    self.btn_shezhiY = self.btn_shezhi.Y
    self.lb_yuyinY = self.lb_yuyin.Y
    self.btn_yuyinY = self.btn_yuyin.Y
    self.ibHeight = self.ib_line.Height

   
    self.expand = 0

    -- 原始大小
    self.default_rect = self.root.Bounds2D
    self.scrollPanHeight = self.scrollPan.Height
    self.cvsHeight = self.cvs_liaotian.Height
    -- 禁止滑动
    self.scrollPan.Scrollable.Scroll.vertical = true

    local scorllInit = false
    self.ChatTimeId = LuaTimer.Add(5000,1000,function(id)
        if not root.UnityObject.activeInHierarchy then
            return
        end
        if scorllInit == false then
            -- print('111111111111111111111111111111111111')
            showScrollPan(self)
            self.newMessageCount = 0
            scorllInit = true
        else
            if self.newMessageCount > 0 then
                -- print('222222222222222222222222222222222')
                if self.newMessageCount < 5 and self.scrollPan.Scrollable:IsBottom() then
                    self.scrollPan.Scrollable:AppendData(self.chatRecords.length,self.newMessageCount)
                    self.newMessageCount = 0
                    UnityHelper.WaitForEndOfFrame(function()
                        self:ScrollToBottom()
                    end)
                    -- print('3333333333333333333333333333333333333')
                else
                    -- print('444444444444444444444444')
                    showScrollPan(self)
                    UnityHelper.WaitForEndOfFrame(function()
                        self:ScrollToBottom()
                    end)
                    self.newMessageCount = 0
                end 
            end
        end

        _M.UpdateLaba()
    end)

end

function _M:chatPushCb(msg)
    self:AppendMessageData(msg)
end

function _M:AppendMessageData(msg)
    if not self:NeedIgnore(msg) then
        self.chatRecords:push(msg)
        self.newMessageCount = self.newMessageCount + 1
        if self.newMessageCount > ChatModel.messageMaxCout then
            self.newMessageCount = ChatModel.messageMaxCout
        end
    end
end

 
local function show1(self,expand)
    -- body
    self.btn_shezhi.Y = self.btn_shezhiY + expand
    self.lb_yuyin.Y = self.lb_yuyinY + expand
    self.btn_yuyin.Y = self.btn_yuyinY + expand
    self.ib_line.Height =  self.ibHeight + expand

    self.smallSize = false
end

local function showDefault(self)
    self.btn_shezhi.Y = self.btn_shezhiY
    self.lb_yuyin.Y = self.lb_yuyinY
    self.btn_yuyin.Y = self.btn_yuyinY 
    self.ib_line.Height =  self.ibHeight

    self.smallSize = true
end 

--收缩/展开
function _M:ExpandPanel(expand)
    if self.expand ~= expand then
         
        self.expand = expand

        local scrollPos = self.scrollPan.ContainerPanel.Position2D
        if expand > 0.1 then

             
            local oldScrollHeight = self.scrollPan.Height

            self.root.Y = self.default_rect.y - expand
            self.root.Height = self.default_rect.height + expand

            show1(self,expand)
 
            self.cvs_liaotian.Height = self.cvsHeight + expand
            self.scrollPan.Height = self.scrollPanHeight + expand

            self.scrollPan.Scrollable.Scroll.vertical = true
            self.scrollPan.Scrollable.Bounds2D = self.scrollPan.ViewRect2D --立即更新Scrollable的尺寸

            scrollPos.y = -scrollPos.y + oldScrollHeight - self.scrollPan.Height  -- 保持聊天底部位置不变
            self.scrollPan.Scrollable:LookAt(scrollPos)
        elseif expand < -0.1 then
            
            self.root.Y = self.default_rect.y - expand
            self.root.Height = self.default_rect.height + expand

            self.cvs_liaotian.Height = self.cvsHeight + expand
            self.scrollPan.Height = self.scrollPanHeight + expand

            self.scrollPan.Scrollable.Scroll.vertical = false
            self.scrollPan.Scrollable.Bounds2D = self.scrollPan.ViewRect2D --立即更新Scrollable的尺寸

            self:ScrollToBottom()
        else
             
            showDefault(self)

            self.root.Y = self.default_rect.y
            self.root.Height = self.default_rect.height

            self.cvs_liaotian.Height = self.cvsHeight
            self.scrollPan.Height = self.scrollPanHeight

            self.scrollPan.Scrollable.Scroll.vertical = false
            self.scrollPan.Scrollable.Bounds2D = self.scrollPan.ViewRect2D --立即更新Scrollable的尺寸

            self:ScrollToBottom()
        end
    end
end


--检查消息是否需要屏蔽
function _M:NeedIgnore(msg)
    local channelSets = ChatModel.mSettingItems[msg.channel_type]
    return channelSets and channelSets.IsHide or false
end


-- 滚动到底部
function _M:ScrollToBottom()
    self.scrollPan.Scrollable:LookAt(Vector2(0, self.scrollPan.ContainerPanel.Size2D.y - self.scrollPan.ScrollRect2D.height))
end

-- local function OnPlayerGetItem(self,eventname,params)
--     local templateId = params.TemplateID
--     local count = params.Count
--     ChatUtil.ShowPlayerGetItem(templateId,count)
-- end

-- function _M.OnGetItem(eventname, params)
--     OnPlayerGetItem(self,eventname,params)
-- end

local function MoveInAction(self,node)
    local moveIn = MoveAction()
    moveIn.Duration = 1
    moveIn.TargetX = self.labaX
    moveIn.TargetY = self.labaY
    node:AddAction(moveIn)
    return moveIn
end

local function FadeInAction(self,node)
    local fadeIn = FadeAction()
    fadeIn.ActionEaseType = EaseType.linear
    fadeIn.Duration = 1
    fadeIn.TargetAlpha = 1
    node:AddAction(fadeIn)
    return fadeIn
end

local function FadeOutAction(self,node)
    local fadeOut = FadeAction()
    fadeOut.ActionEaseType = EaseType.linear
    fadeOut.Duration = 0.8
    fadeOut.TargetAlpha = 0
    node:AddAction(fadeOut)
    return fadeOut
end

local function MoveOutAction(self,node)
    local moveOut = MoveAction()
    moveOut.Duration = 1
    moveOut.TargetX = self.labaX
    moveOut.TargetY = - self.labaY  
    node:AddAction(moveOut)
    moveOut.ActionFinishCallBack = function(sender)
        node.Visible = false
        --node:RemoveFromParent(true)
        List.pushright(self.labaUI,node)
    end
    return moveOut
end

function _M.UpdateLaba()
    local msg = List.popleft(self.labaDatas)
    if not msg then
        return
    end

    self.cvs_laba.Visible = true
    if self.BroadTimeId ~= nil then
        self.cvs_laba:RemoveAllAction(true)
        LuaTimer.Delete(self.BroadTimeId)
        self.BroadTimeId = nil
        if self.hornHtml then
                self.hornHtml:RemoveAllAction(false)
                self.hornHtml.X = self.labaX
                self.hornHtml.Y = self.labaY
                FadeOutAction(self,self.hornHtml)
                MoveOutAction(self,self.hornHtml)
                self.hornHtml = nil
        end
    end

    self.cvs_laba.Visible = true
    local hornHtml = List.popleft(self.labaUI)
    if not hornHtml then
        hornHtml = self.tbh_laba:Clone()
    end
    self.cvs_laba:AddChild(hornHtml)

    self.hornHtml = hornHtml
    self.hornHtml.Visible = true
    self.hornHtml.AText = MakeLabaText(self.hornHtml,msg)
    self.hornHtml.Alpha = 0.1
    self.hornHtml.X = self.labaX
    self.hornHtml.Y = self.labaY + 15 
     
     
    FadeInAction(self,self.hornHtml)
    MoveInAction(self,self.hornHtml)
      
    self.BroadTimeId = LuaTimer.Add(120000,function(id)
        if self.hornHtml then
            self.hornHtml:RemoveFromParent(true)
            self.hornHtml = nil
        end
        self.cvs_laba.Visible = false
    end)
end

function _M.OnHornMessage(eventname, msg)
    List.pushright(self.labaDatas,msg)
end

local function fin()
    print (" lua ChatHud fin ")
    if self.ChatTimeId ~= nil then
        LuaTimer.Delete(self.ChatTimeId)
        self.ChatTimeId = nil
    end

    if self.BroadTimeId ~= nil then
        LuaTimer.Delete(self.BroadTimeId)
        if self.hornHtml then
            self.hornHtml:RemoveAllAction(false)
            self.hornHtml = nil
        end
        self.cvs_laba:RemoveAllAction(false)
        self.BroadTimeId = nil
    end

    local hornHtml = List.popleft(self.labaUI)
    while  hornHtml do
        hornHtml:RemoveFromParent(true)
        hornHtml = List.popleft(self.labaUI)
    end
    
    ChatModel.RemoveChatPushListener('ChatHud')
    -- EventManager.Unsubscribe("Event.SysChat.GetItem", _M.OnGetItem)
    EventManager.Unsubscribe("Event.SysChat.OnHornMessage", _M.OnHornMessage)
end

local function initial()
 
    self.newMessageCount = 0    
    self.Scrolled = false

    self.chatRecords = ringbuffer:new(ChatModel.messageMaxCout)
    self.labaDatas = List:new()
    self.labaUI = List:new()

    ChatModel.AddChatPushListener('ChatHud', function(msg)
       self:chatPushCb(msg)
    end)

  
        
    -- EventManager.Subscribe("Event.SysChat.GetItem", _M.OnGetItem)
    EventManager.Subscribe("Event.SysChat.OnHornMessage", _M.OnHornMessage)
end

_M.fin = fin
_M.initial = initial
return _M