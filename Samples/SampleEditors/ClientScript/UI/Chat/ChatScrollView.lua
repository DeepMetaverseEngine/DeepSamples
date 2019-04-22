local _M = {}

local ringbuffer = require "Logic/ringbuffer"
local Util = require "Logic/Util"
local UIUtil = require 'UI/UIUtil'
local ChatRecordNode = require 'UI/Chat/ChatRecordNode'
local Api = EventApi

--到了15条之后 滑动之后再来消息就消失为未读消息
local showSize = 10
local cacheSize =  15

local function ConfigAutoScrollPan(pan, tempnode, row,expandSize, eachupdatecb,Size2D)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gy + 1)
    end
    local s = Size2D or tempnode.Size2D
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

function _M.Create(pan, template, tipsCvs,tipsLabel)
    local self = {}
    setmetatable(self, {__index = _M})
    self:Init(pan, template, tipsCvs,tipsLabel)
    return self
end

local function CreateRecordNode(self,node,index)
    -- index = index % ChatModel.messageMaxCout
    if index > self.chatRecords.length then
        index = index % self.chatRecords.length
    end
    local dataIndex = self.chatRecords.length + 1 - index
    local msg = self.chatRecords[dataIndex]
    if not msg then
        print('sth error self.chatRecords.length,index:',self.chatRecords.length,index)
    end
    local item = ChatRecordNode.Create(node,self.size_default,self.recordCvsBounds2D,self.content1Size2D,self.sysContentSize2D)
    item:ResetX(self.ChatNodeCvs.X, self.thb_content.X,self.ib_bgimg2.X,self.ib_target1.X,self.ib_bgimg3.X,self.lbl_name.X,self.lb_lv.X)
    item:setData(msg)
end

local function showScrollPan(self,channel)
    local function eachupdatecb(node, index)
        CreateRecordNode(self,node,index)
    end

    cacheSize = Api.GetExcelConfig('chat_msgload') or 15
    -- print('channel:',channel)
    -- print('channel == ChatModel.ChannelState.CHANNEL_SYSTEM:',channel == ChatModel.ChannelState.CHANNEL_SYSTEM)
    -- print_r('self.sysCvs.Size2D:',self.sysCvs.Size2D)
    local ChatModel = require 'Model/ChatModel'
    if channel == ChatModel.ChannelState.CHANNEL_SYSTEM then
        ConfigAutoScrollPan(self.pan,self.template,self.chatRecords.length,cacheSize,eachupdatecb,self.sysCvs.Size2D)
    else
        ConfigAutoScrollPan(self.pan,self.template,self.chatRecords.length,cacheSize,eachupdatecb)
    end
    
end

function _M:Init(pan, template, tipsCvs, tipsLabel)
	
    self.pan = pan
    self.template = template
    
    local normalCvs =  UIUtil.FindChild(template,'cvs_showtype1') 
    self.ChatNodeCvs =  UIUtil.FindChild(normalCvs,'cvs_record1') 
    self.thb_content = UIUtil.FindChild(self.ChatNodeCvs,'thb_content') 
    
    self.ib_bgimg2 =  UIUtil.FindChild(normalCvs,'ib_bgimg2')   
    self.ib_target1 =  UIUtil.FindChild(normalCvs,'ib_target1')     
    self.ib_bgimg3 =  UIUtil.FindChild(normalCvs,'ib_bgimg3')
    self.lbl_name = UIUtil.FindChild(normalCvs,'lb_name1')        
    self.lb_lv =  UIUtil.FindChild(normalCvs,'lb_lv')

    self.size_default = self.template.Size2D 
    self.recordCvsBounds2D =  self.ChatNodeCvs.Bounds2D
    self.content1Size2D = self.thb_content.Size2D


    local sysCvs  = UIUtil.FindChild(template,'cvs_showtype2') 
    self.sysCvs = sysCvs
    self.sysContent = UIUtil.FindChild(sysCvs,'thb_content2') 
    self.sysContentSize2D = self.sysContent.Size2D

    -- local sysCvs  = UIUtil.FindChild(template,'cvs_showtype2') 
    -- self.thb_content2 = UIUtil.FindChild(sysCvs,'thb_content') 

    self.tipsLabel = tipsLabel

    self.tips = tipsCvs
    self.tips.Visible = false

    self.tips.Enable = true
    self.tips.IsInteractive = true
    self.tips.TouchClick = function()
        self.pan.Scrollable:ToBottom(self.newMessageCount)
        self.tips.Visible = false
        self.newMessageCount = 0
        UnityHelper.WaitForEndOfFrame(function()
            self.pan.Scrollable:LookAt(Vector2(0, self.pan.ContainerPanel.Size2D.y - self.pan.ScrollRect2D.height))
        end)
        -- self.autoMessage = true
    end

    -- self.tips_text = tips:FindChildByEditName("lb_text1", true)
    self.pan.Scrollable.event_OnEndDrag = function (sender,e)
        -- print("event_OnEndDrag 333333333333333333333333333333333333333333333333333333333333 ")
        if self.pan.Scrollable:IsBottom() then
            self.newMessageCount = 0
            self.tips.Visible = false
        end
    end
    -- updae里的回调，必然会进好多次呀
    -- self.pan.Scrollable.event_ScrollEnd = function (sender,e)
         
    --     print("event_ScrollEnd 1111111111111111111111111111111111111111111111111111111111111111 ")
    --     -- self.autoMessage = false
    --     -- self.newMessageCount = 0
    --     -- self.tips.Visible = false
    --     -- print('event_ScrollEnd11111111111111111111111111111111111')
    -- end


    -- self.pan.Scrollable.event_Scrolled = function (sender,e)
    --     print("event_Scrolled 2222222222222222222222222222222222222222222222222222222222222222 ")
         
    -- end

end

function _M:ClearMessage()
    local function eachupdatecb(node, index)
        CreateRecordNode(self,node,index)
    end
    ConfigAutoScrollPan(self.pan,self.template,0,4,eachupdatecb)
    self.newMessageCount = 0
    self.autoMessage = true
end


function _M:setPrivateData(roleId)
    local ChatModel = require 'Model/ChatModel'
    self.chatRecords = ChatModel.GetPrivateChatData(roleId)
    showScrollPan(self)

    self.newMessageCount = 0
    self.autoMessage = true

    UnityHelper.WaitForEndOfFrame(function()
        self.pan.Scrollable:LookAt(Vector2(0, self.pan.ContainerPanel.Size2D.y - self.pan.ScrollRect2D.height))
    end)
end

function _M:setData(channel)
    local ChatModel = require 'Model/ChatModel'
	self.chatRecords = ChatModel.GetChatData(channel)
    showScrollPan(self,channel)

    self.newMessageCount = 0
    self.autoMessage = true

    UnityHelper.WaitForEndOfFrame(function()
        self.pan.Scrollable:LookAt(Vector2(0, self.pan.ContainerPanel.Size2D.y - self.pan.ScrollRect2D.height))
    end)
end

function _M:AppendMessage(data)
    
    self.pan.Scrollable:AppendData(self.chatRecords.length,1)
    if data.is_myself then
        if self.pan.Scrollable:IsBottom() then
            self.tips.Visible = false
            self.newMessageCount = 0
 
            UnityHelper.WaitForEndOfFrame(function()
                self.pan.Scrollable:LookAt(Vector2(0, self.pan.ContainerPanel.Size2D.y - self.pan.ScrollRect2D.height))
            end)
        else
            self.tips.Visible = false
            self.pan.Scrollable:ToBottom(self.newMessageCount)
            UnityHelper.WaitForEndOfFrame(function()
                self.pan.Scrollable:LookAt(Vector2(0, self.pan.ContainerPanel.Size2D.y - self.pan.ScrollRect2D.height))
                self.newMessageCount = 0
                self.autoMessage = true
            end)
        end
    elseif self.pan.Scrollable:IsBottom() then
        self.tips.Visible = false
        self.newMessageCount = 0
        
        UnityHelper.WaitForEndOfFrame(function()
            self.pan.Scrollable:LookAt(Vector2(0, self.pan.ContainerPanel.Size2D.y - self.pan.ScrollRect2D.height))
        end)
    else
        
        self.newMessageCount = self.newMessageCount + 1
        -- print('newMessageCount222222222222222222:',self.newMessageCount)
        self.tips.Visible = true
        self.tipsLabel.Text = Util.GetText(Constants.Text.chat_noread, self.newMessageCount)
    end
end

return _M