local _M = {}
_M.__index = _M

-- _M.lua  聊天历史信息
-- 作者: zhiyong.xie
-- 2017.5.9

local ChatUtil = require "UI/Chat/ChatUtil"
local ChatModel = require "Model/ChatModel"

--UI不直接参与网络通信，通过model中转
local Util                  = require "Logic/Util"
--local ChatMain              = require "Zeus.UI.Chat.ChatMain"

local faceNum = 48
local facePage = 5
local ib_biaoqing1PosX
local ib_biaoqing1PosY

function _M:Close()
    --关闭
    if self.callback ~= nil then
        self.callback(nil)
    end
    self.ui.menu:Close()
end

function _M:OnClickItem(index)
    if self.callback ~= nil then
        self.callback(self.data[index].common, function( ... )
            self:Close()
        end)
    end
end

function _M:OnEnter(pos,pos2)
    self.data = ChatModel.mCommonItems

    for i=1,#self.recordNodes do
        local textbox = self.recordNodes[i]
        if self.data[i].common then
            local defaultTextAttr = textbox.TextComponent.RichTextLayer.DefaultTextAttribute
            textbox.AText = ChatUtil.HandleChatClientDecode(self.data[i].common, GameUtil.GetTextAttributeFontColorRGB(defaultTextAttr), nil, nil, defaultTextAttr.fontSize)
        else
            textbox.XmlText = ""
        end
    end

    -- self.ui.comps.cvs_history.Position2D = pos or self.defaultPos
    local position
    if pos then
        position = pos
    elseif pos2 then
        position = Vector2(pos2.x,pos2.y - self.Height)
    else
        position = self.defaultPos
    end

    self.ui.comps.cvs_history.Position2D = position

end

function _M:OnInit()
    self.defaultPos = self.ui.comps.cvs_history.Position2D
    self.Height = self.ui.comps.cvs_history.Height

    --print("DungeonMain.init ".. params)
    local index = tonumber(params)
    if index then
        self.default = index
    end
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu.IsInteractive = false
    
    self.ui.menu.event_PointerClick = function() self:Close() end

    self.btn_close = self.ui.menu:GetComponent("btn_close")
    self.btn_close.TouchClick = function() self:Close() end

    self.recordNodes = {}

    for i=1,10 do
        self.recordNodes[i] = self.ui.comps["tbh_text"..tostring(i)]
        self.recordNodes[i].Enable = true
        self.recordNodes[i].IsInteractive = true
        self.recordNodes[i].TextComponent.RichTextLayer:SetEnableMultiline(false)
        self.recordNodes[i].TextComponent.RichTextLayer.Anchor = CommonUI.TextAnchor.L_C
        self.recordNodes[i].event_PointerClick = function()
            self:OnClickItem(i)
        end
    end


end

return _M