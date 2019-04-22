-- 2018年4月21日 16点20分

local Util      = require "Logic/Util"
local UIUtil = require 'UI/UIUtil'

local TLChatSend = require "UI/Chat/ChatSend"
local ModalMask = require "UI/Chat/ModalMask"
local ChatModel = require "Model/ChatModel"

local _M = {}

local function OnClickBiaoqing(displayNode, self, pos)
    --表情
    --MenuMgr.Instance:OpenUIByTag(GlobalHooks.UITAG.GameUIChatFace, 0, {callback = faceCallBack})
    if self.uiface == nil or self.currentPopup ~= self.uiface then
        self.uiface = GlobalHooks.UI.OpenUI('ChatUIFace', 0,self.posFace,self.posFace2)
        self.uiface.faceCb = function(index)
            if index == nil then
                self.uiface = nil
            end
            TLChatSend.SendFace(self,index)
        end
        self:ShowPopup(self.uiface)
    else
        self.uiface.ui.menu:Close()
        self.uiface = nil
    end
end


function _M.Init(self,cvs_lable,btn_open)

 
	local function ToolTogFunc(sender)
		local act = sender.Tag
 		print('ToolTogFunc tag:',act)
        --位置
 		if act == ChatModel.ChatAction.ACTION_POSITION then 
            --功能暂未开放
 			
            -- GameAlertManager.Instance:ShowNotify(Util.GetText('chat_noopen'))
            sender.IsChecked = false
            TLChatSend.SendPos(self)
            self:HidePopup()
            
        --动作
 		elseif act == ChatModel.ChatAction.ACTION_ACT then
            local ChatActionUI = GlobalHooks.UI.OpenUI('ChatUIAction', 0, self.posAction,self.posAction2)
            ChatActionUI.faceCb = function(actionData)
                print_r('ChatActionUI ToolTogFunc:',actionData)
                if actionData ~= nil then
                    TLChatSend.MakeTLAction(self,actionData)
                end
                sender.IsChecked = false
            end

            self:ShowPopup(ChatActionUI)

 		--物品展示	
 		elseif act == ChatModel.ChatAction.ACTION_SHOW then

 			local uiShowItem = GlobalHooks.UI.OpenUI("ChatShowItem", 0,self.posItemShow,self.posItemShow2)
        	uiShowItem.callback = function(data)
                if data then
            	   TLChatSend.AddItem(self, data)
                end
                sender.IsChecked = false
       		end
            
            self:ShowPopup(uiShowItem)

        -- 历史
 		elseif act == ChatModel.ChatAction.ACTION_HISTORY then
 			-- local uiHistory = GlobalHooks.UI.OpenUI("ChatUIHistory", 0)
 			local uiHistory = GlobalHooks.UI.OpenUI("ChatUIHistory", 0, self.posHistory,self.posHistory2)
 			uiHistory.callback = function(msg, cb)
                if msg then
            	   TLChatSend.CommonChatMsg(self, msg, cb)
                end
                sender.IsChecked = false
        	end
 			self:ShowPopup(uiHistory)
 		elseif act == ChatModel.ChatAction.ACTION_REDPACKAGE then
            --功能暂未开放
 			-- GameAlertManager.Instance:ShowNotify(Util.GetText('chat_noopen'))
 			-- self:HidePopup()
            sender.IsChecked = false
 		end

        if self.toolBarMask then
            self.toolBarMask.root.Visible = false
        end


	end
 
    local locationTog = self.ui.comps.tbt_location
    locationTog.Tag = ChatModel.ChatAction.ACTION_POSITION

    local actionTog = self.ui.comps.tbt_action
    actionTog.Tag = ChatModel.ChatAction.ACTION_ACT

    local itemShowTog = self.ui.comps.tbt_itemshow
    itemShowTog.Tag = ChatModel.ChatAction.ACTION_SHOW

    local historyTog = self.ui.comps.tbt_history
    historyTog.Tag = ChatModel.ChatAction.ACTION_HISTORY

    -- local redbagTog = self.ui.comps.tbt_redbag
    -- redbagTog.Tag = ChatModel.ChatAction.ACTION_REDPACKAGE
   
	local tbts = {}
    table.insert(tbts,locationTog)
    table.insert(tbts,actionTog)
    table.insert(tbts,itemShowTog)
    table.insert(tbts,historyTog)
    -- table.insert(tbts,redbagTog)

    UIUtil.ConfigToggleButton(tbts, nil, false, ToolTogFunc)

    self.btn_emoji = self.ui.comps.btn_emoji
    self.btn_emoji.TouchClick = function (displayNode, pos)
        OnClickBiaoqing(displayNode, self, pos)
    end

        -- 如果传入了btn_open, 说明这个工具栏默认是隐藏的, 需要创建弹出框遮罩层
    if btn_open then
        self.toolBarMask = ModalMask.Create(self.ui.menu, cvs_lable)
        btn_open.TouchClick = function()
            self.toolBarMask:Show()
            self:HidePopup()
            UIUtil.ConfigToggleButton(tbts, nil, false, ToolTogFunc)
        end
    end

end

return _M