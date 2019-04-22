local _M = {}

local ChatModel  = require 'Model/ChatModel'
local ItemModel = require 'Model/ItemModel'

local Util      = require 'Logic/Util'
local QuestUtil = require 'UI/Quest/QuestUtil'
local ChatUtil  = require "UI/Chat/ChatUtil"
-- local ChatSendVoice  = require "UI/Chat/ChatSendVoice"
local hornString = Constants.Text.ChatHornMaxString
local MaxLength25 = 25
local MaxLength50 = 50

--local voice_path = "http://voicetest.morefuntek.com/upload"
local function clearTips(self)
    if self.mTextInput.Input.Text == hornString then
        self.mTextInput.Input.Text = ''
    end
end

local function InitRichTextLabel(self)
  
    if(self.mHtmlText == nil) then
        local canvas = HZCanvas()
        canvas.Size2D = self.mTextInput.Size2D - Vector2(4,4)
        canvas.Position2D = Vector2(2, 2)
        canvas.Layout =  HZUISystem.CreateLayout("#dynamic/TL_chatnew/output/TL_chatnew.xml|TL_chatnew|13", UILayoutStyle.IMAGE_STYLE_ALL_9, 9)
        local mask = canvas.UnityObject:AddComponent(UnityEngine.UI.Mask)
        mask.showMaskGraphic = false

        self.mHtmlText = HZRichTextPan();
        self.mHtmlText.Size2D = canvas.Size2D - Vector2(4,4)
        self.mHtmlText.RichTextLayer.UseBitmapFont = true
        self.mHtmlText.RichTextLayer:SetEnableMultiline(false)
        self.mHtmlText.RichTextLayer.FontSize = 20
        self.mHtmlText.TextPan.Width = self.mTextInput.Size2D.x
        self.mHtmlText.RichTextLayer.Anchor = CommonUI.TextAnchor.L_C
        canvas:AddChild(self.mHtmlText)
        self.mTextInput:AddChild(canvas)
        self.mHtmlText.Visible = false
        self.mHtmlText.X = 2
        self.mHtmlText.Y = 2
    end
end

local function CountLength(str, self)
    -- body
    if self.m_contentText == nil then
        self.m_contentText = HZRichTextPan()
        self.m_contentText.Size2D = self.mTextInput.Size2D
        self.m_contentText.RichTextLayer.UseBitmapFont = true
        self.m_contentText.RichTextLayer:SetEnableMultiline(false)
        self.m_contentText.TextPan.Width = self.mTextInput.Size2D.x
        self.mTextInput:AddChild(self.m_contentText)
        self.m_contentText.Visible = false;
        self.m_contentText.X = 10
        self.m_contentText.Y = 10
    end

    local linkdata = ChatUtil.HandleChatClientDecode(str, self.fontColor)
    self.m_contentText.RichTextLayer:SetString(linkdata)
    if self.m_contentText.RichTextLayer.ContentWidth > 1200 then
        return false
    else
        return true
    end
end

local function AddStringInput(self,msg,copy)
    msg = string.trim(msg)
    if not string.IsNullOrEmpty(msg) or string.gsub(self.m_StrTmpOriginal, " ", "") ~= "" then
        self.mHtmlText.Visible = true
    else
        self.mHtmlText.Visible = false
    end
 
    if copy then
        if ChatUtil.StartsWith(msg, "|") then
            self.m_StrTmpOriginal = self.m_StrTmpOriginal .. msg
        else
            self.m_StrTmpOriginal = self.m_StrTmpOriginal .. "|" .. msg .. "|"
        end 
    else
        self.m_StrTmpOriginal = self.m_StrTmpOriginal .. msg
    end

    local message = self.m_StrTmpOriginal
    -- if  self.m_curChannel == ChatModel.ChannelState.CHANNEL_HORN and string.len(message) > MaxLength25 then
    --     message =  string.utf8sub(message,0,25)
    -- end
    local linkdata = ChatUtil.HandleChatClientDecode(message, self.fontColor)
    self.mHtmlText.RichTextLayer:SetString(linkdata)
end

local function clearMsg(self)
    self.m_StrTmpOriginal = ""
    AddStringInput(self,"")
    self.mTextInput.Input.Text = ""
end


local function gfw1gv5871b(msg)   
    if string.starts(msg, "@gm") then
        return msg
    end
    return Util.FilterBlackWord(msg)
end

-- local function OnClickSendHorn(self, msgContent)
--     if msgContent == nil or string.gsub(msgContent, " ", "") == "" then
--         GameAlertManager.Instance:ShowNotify(Util.GetText('chat_input_null'))
--         return
--     end
 
--     if self.m_curChannel ~= ChatModel.ChannelState.CHANNEL_WORLD then
--         GameAlertManager.Instance:ShowNotify(Util.GetText('chat_usehorn'))  --('世界频道才能刷喇叭')
--         return
--     end

--     local funcType = ChatModel.FuncType.TYPE_SCROLL
--     ChatModel.ReqChatMessage(ChatModel.ChannelState.CHANNEL_WORLD, msgContent, self.privateRoleData,funcType, function (param)
--         clearMsg(self)
--         self.ui.comps.tbt_horn.IsChecked = false
--         self.usingHorn = false
--         local itemId = GlobalHooks.DB.GetGlobalConfig('chat_horn')
--         -- self.ui.comps.lb_homnum.Text = ItemModel.CountItemByTemplateID(itemId)
--     end)
-- end
 
local function OnClickSendMessage(self, msgContent)
    if msgContent == nil or string.gsub(msgContent, " ", "") == "" then
        GameAlertManager.Instance:ShowNotify(Util.GetText('chat_input_null'))
        return
    end
 
    -- msgContent = gfw1gv5871b(msgContent)

    if self.m_curChannel == ChatModel.ChannelState.CHANNEL_PRIVATE and self.privateRoleData == nil then
        -- GameAlertManager.Instance:ShowNotify(Util.GetText('chat_object'))
        GameAlertManager.Instance:ShowNotify(Constants.Text.chat_target)
        return
    end

    if  self.privateRoleData == nil or self.privateRoleData.isOnline == nil or self.privateRoleData.isOnline ~= 0 then 
       
        if self.m_curChannel == ChatModel.ChannelState.CHANNEL_HORN then
            local itemId = GlobalHooks.DB.GetGlobalConfig('chat_horn')
            local itemNum =  ItemModel.CountItemByTemplateID(itemId)  
            if itemNum <= 0 then
                GameAlertManager.Instance:ShowNotify(Constants.Text.chat_noItem)
                self.lb_laba.Text = 0
                return
            else
                self.lb_laba.Text = itemNum - 1
            end
        end

        ChatModel.ReqChatMessage(self.m_curChannel, msgContent, self.privateRoleData,nil, function (param)
            if self.m_curChannel == ChatModel.ChannelState.CHANNEL_HORN then
                local itemId = GlobalHooks.DB.GetGlobalConfig('chat_horn')
                local itemNum =  ItemModel.CountItemByTemplateID(itemId)  
                self.lb_laba.Text = itemNum
            end
            clearMsg(self)
            self:HidePopup()
        end)
    end
end


local function OnClickYuyin(displayNode, self)
     GameAlertManager.Instance:ShowNotify(Util.GetText('暂不支持语音'))
end

local function HandleTxtInputPrivate(displayNode, self)
    local text = self.mTextInput.Input.Text
    text = string.trim(text)
    if string.IsNullOrEmpty(text) then
        self.m_StrInput = ChatUtil.HandleOriginalToInput(self.m_StrTmpOriginal)
        self.mTextInput.Input.Text = self.m_StrInput

        if self.mHtmlText ~= nil then
            self.mHtmlText.Visible = false
        end
    elseif self.mTextInput.Input.Text == hornString then
        self.mTextInput.Input.Text = ''
    end
end

local function HandleInputFinishCallBack(displayNode, self)
    --输入完成
    local text = self.mTextInput.Input.Text
    if self.mTextInput.Input.Text == " " then
        return
    end
    -- text = string.gsub(text,'<','＜')
    -- text = string.gsub(text,'>','＞')
     
    -- text = Util.FilterBlackWord(text)
    local msg = ChatUtil.HandleInputToOriginal(text)
    self.m_StrTmpOriginal = ""
     
    AddStringInput(self,msg)
    self.mTextInput.Input.Text = " "
end


function _M.SetAcceptRoleData(self,data)
    --print(PrintTable(data))
    if data ~= nil and data.playerId == DataMgr.Instance.UserData.RoleID then
        return
    end
    self.privateRoleData = data

    local msg = ChatUtil.HandleInputToOriginal(self.mTextInput.Input.Text)
    AddStringInput(self,msg)
end

-- 位置
function _M.SendPos(self)
    local msg = ChatUtil.GetActorPos()
    OnClickSendMessage(self, msg)
end

local function AddChatAction(self,actionData,receiver)

    local sender = {}
    sender.s2c_playerId = DataMgr.Instance.UserData.RoleID
    sender.s2c_name = DataMgr.Instance.UserData.Name
    --data.s2c_level = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0)
    sender.Quality = 1 -- TODO: 角色名称颜色

    local str1 = ChatUtil.AddPersonByData(sender)
    local message

    if receiver == nil then
        message = Util.GetText(actionData.to_null,str1)
    else
        local accepter = {}
        accepter.s2c_playerId = receiver.playerId
        accepter.s2c_name = receiver.playerName
        accepter.Quality = 1  
        local str2 = ChatUtil.AddPersonByData(accepter)
        message = Util.GetText(actionData.to_other,str1,str2)
    end

    OnClickSendMessage(self, message)
end


--做动作
function _M.MakeTLAction(self,actionData,receiver)
    if actionData == nil then
        return
    end

    AddChatAction(self,actionData,receiver)

end

local function AddFace(self,index)
    --local num = math.random(0, 2) + 1000
    local msg = ChatUtil.AddFaceByIndex(index)
    local num = ChatUtil.HandleOriginalToInput(self.m_StrTmpOriginal .. msg)
   if (self.m_curChannel == ChatModel.ChannelState.CHANNEL_HORN and string.len(num) <= MaxLength25) or string.len(num) <= MaxLength50 then
        AddStringInput(self,msg)
    else
        --字符数太长
        GameAlertManager.Instance:ShowNotify(Util.GetText('chat_input_limit'))
    end
end

--表情
function _M.SendFace(self,index)
    if index ~= nil then
        clearTips(self)
        AddFace(self,index)
    end
end

--展示物品
function _M.AddItem(self, data)
    -- body 
    clearTips(self)

    local msg = ChatUtil.AddItemByData(data)
    local num = ChatUtil.HandleOriginalToInput(self.m_StrTmpOriginal .. msg)
    if (self.m_curChannel == ChatModel.ChannelState.CHANNEL_HORN and string.len(num) <= MaxLength25) or string.len(num) <= MaxLength50 then
        AddStringInput(self,msg)
    else
        --字符数太长
        GameAlertManager.Instance:ShowNotify(Util.GetText('chat_input_limit'))
    end
end

local function SetCopyData(self,str)
    local limit = CountLength(self.m_StrTmpOriginal .. str, self)
    if limit then
        AddStringInput(self,str,true)
    else
        --字符数太长
        GameAlertManager.Instance:ShowNotify(Util.GetText('chat_input_limit'))
    end
end

-- 历史
function _M.CommonChatMsg(self, msg, cb)
    -- body
    self.mTextInput.Input.Text = ""
    self.m_StrTmpOriginal = ""
    SetCopyData(self,msg)
    if cb ~= nil then
        cb()
    end
end


function _M.InitChannel(self,channel)
    
    clearTips(self)

    -- if channel  == ChatModel.ChannelState.CHANNEL_WORLD then 
    --     local roleLevel = DataMgr.Instance.UserData.Level
    --     local channelConfig = ChatModel.mSettingItems[channel]
    --     if channelConfig ~= nil and channelConfig.chat_level ~= nil and roleLevel < channelConfig.chat_level then 
    --         self.ui.comps.tbt_horn.Visible = false
    --         self.ui.comps.lb_homnum.Visible = false
    --     else
    --         -- self.ui.comps.tbt_horn.Visible = true
    --         --self.ui.comps.lb_homnum.Visible = true
    --         --CCB不要小喇叭功能
    --         self.ui.comps.tbt_horn.Visible = false
    --         self.ui.comps.lb_homnum.Visible = false
    --     end
    -- else
    --     self.ui.comps.tbt_horn.Visible = false
    --     self.ui.comps.lb_homnum.Visible = false
    -- end
 
    self.m_curChannel = channel
    self.privateRoleData = nil
 
 

    if self.m_curChannel == ChatModel.ChannelState.CHANNEL_HORN then
        self.mTextInput.Input.characterLimit = MaxLength25
    else
        self.mTextInput.Input.characterLimit = MaxLength50
    end


    -- 队伍或系统
    self.cvs_input.Visible = false
    self.cvs_tips.Visible = true
    if (self.m_curChannel == ChatModel.ChannelState.CHANNEL_TEAM and DataMgr.Instance.TeamData.HasTeam == false)
        or self.m_curChannel == ChatModel.ChannelState.CHANNEL_SYSTEM then
        if self.m_curChannel == ChatModel.ChannelState.CHANNEL_TEAM then
           self.lb_tips.Text = Util.GetText('chat_noteam')
           self.lb_tips.Visible = true
        else
           self.lb_tips.Text = Util.GetText('chat_nosystem')
           self.lb_tips.Visible = true
        end
    -- 公会
    elseif self.m_curChannel == ChatModel.ChannelState.CHANNEL_GUILD and string.IsNullOrEmpty(DataMgr.Instance.UserData.GuildId) then
        self.lb_tips.Text = Util.GetText('chat_noguild')
        self.lb_tips.Visible = true
 
    elseif self.m_curChannel == ChatModel.ChannelState.CHANNEL_AREA then
        local TemplateID = DataMgr.Instance.UserData.MapTemplateId
        local mapData = GlobalHooks.DB.FindFirst('MapData',{ id = TemplateID })
        if mapData.chat_channel == 0 then
            self.lb_tips.Text = Util.GetText('chat_nonearby')
            self.lb_tips.Visible = true
        else
            self.cvs_input.Visible = true
            self.cvs_tips.Visible = false
            self.lb_tips.Visible = false
        end
    elseif self.m_curChannel == ChatModel.ChannelState.CHANNEL_BATTLE then
        -- self.lb_tips.Text = Util.GetText('chat_noopen')
        -- self.lb_tips.Visible = true

    elseif self.m_curChannel == ChatModel.ChannelState.CHANNEL_PLATFORM then
        self.lb_tips.Text = Util.GetText('chat_noplatform')
        self.lb_tips.Visible = true
    else
        --
        self.cvs_input.Visible = true
        self.cvs_tips.Visible = false
        self.lb_tips.Visible = false
    end

    local msg = ChatUtil.HandleInputToOriginal(self.mTextInput.Input.Text)
    AddStringInput(self,msg)
    if string.IsNullOrEmpty(self.m_StrTmpOriginal) and self.m_curChannel == ChatModel.ChannelState.CHANNEL_HORN then
        self.mTextInput.Input.Text = hornString
    end
end
  
local function InitCompnent(self)
        
    self.lb_laba = self.ui.comps.lb_laba
    self.btn_talk = self.ui.comps.btn_talk
    
    self.btn_ok = self.ui.comps.btn_ok
    self.btn_voice = self.ui.comps.btn_voice
    self.cvs_input = self.ui.comps.cvs_input
    self.cvs_tips = self.ui.comps.cvs_tips
    self.lb_tips = self.ui.comps.lb_tips

    self.lb_tips.Visible = false

    self.btn_emoji = self.ui.comps.btn_emoji
    self.btn_emoji.TouchClick = function (displayNode, pos)
        OnClickBiaoqing(displayNode, self, pos)
    end

    self.ui.comps.tbt_horn.TouchClick = function ( )
        -- body
        self.usingHorn = self.ui.comps.tbt_horn.IsChecked
        -- print('self.usingHorn:',self.usingHorn)
    end

 
    self.btn_ok.TouchClick = function (displayNode, pos)
        self:HidePopup()
        -- if  self.usingHorn and self.m_curChannel == ChatModel.ChannelState.CHANNEL_WORLD then
        --     OnClickSendHorn(self, self.m_StrTmpOriginal)
        -- else
            OnClickSendMessage(self, self.m_StrTmpOriginal)
        -- end
    end

    self.btn_voice.TouchClick = function (displayNode, pos)
        OnClickYuyin(displayNode, self)
    end
 
    --UETextInput框
    -- 输入框
    self.mTextInput = self.ui.comps.ti_import      
    self.mTextInput.Input.characterLimit = MaxLength50
    self.mTextInput.Input.Text = " "
    self.mTextInput.Input.lineType = UnityEngine.UI.InputField.LineType.MultiLineSubmit
    self.mTextInput.InputTouchClick = function(displayNode)
        HandleTxtInputPrivate(displayNode, self)
    end

    self.mTextInput.event_endEdit = function(displayNode)
        HandleInputFinishCallBack(displayNode, self)
    end
 
    InitRichTextLabel(self)


    self.m_StrTmpOriginal = ""
    self.m_IsVioceOn = false
    self.fontColor = 0x7d7666
    
end


function _M.OnDestory(self)
    -- body
    -- ChatSendVoice.OnDestory(self)
end

function _M.OnEnter(self,usingHorn)

    -- self.usingHorn = usingHorn or false
    --CCB隐藏小喇叭
    self.usingHorn = false
    self.ui.comps.tbt_horn.Visible = false
    -- self.ui.comps.tbt_horn.IsChecked = false

    local itemId = GlobalHooks.DB.GetGlobalConfig('chat_horn')
    self.lb_laba.Text = ItemModel.CountItemByTemplateID(itemId)
    -- self.ui.comps.lb_homnum.Text = ItemModel.CountItemByTemplateID(itemId)
    -- self.ui.comps.lb_homnum.Visible = false

    -- clearMsg(self)
    -- ChatSendVoice.OnEnter(self)
end

function _M.Init(self)
    InitCompnent(self)
end

return _M