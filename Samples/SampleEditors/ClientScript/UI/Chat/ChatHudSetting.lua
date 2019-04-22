 local _M = {}
_M.__index = _M

local ChatModel = require "Model/ChatModel"
--UI不直接参与网络通信，通过model中转
function _M:Close()
    self.ui.menu:Close()
end

function _M:OnCheckOption(index)
    self.options[index].IsHide = not self.options[index].IsHide
    self.checkboxes[index].IsChecked = not self.options[index].IsHide
end

function _M:OnEnter()

end

function _M:OnExit( ... )
    EventManager.Fire('Event.ChatHud.Settings', self.options)

    ChatModel.SaveSettings()
end

function _M:OnInit()

    self.ui.menu.ShowType = UIShowType.Cover

    self.ui.menu.event_PointerClick = function() 
        self:Close() 
    end

    local btn_close = self.ui.comps.btn_close
    btn_close.TouchClick = function() 
        self:Close() 
    end

    self.options = ChatModel.mSettingItems
    self.checkboxes = {}

    local function initCheckbox(channel, ui)
        self.checkboxes[channel] = self.ui.comps[ui]
        self.checkboxes[channel].IsChecked = not self.options[channel].IsHide
        self.checkboxes[channel].event_PointerClick = function()
            self:OnCheckOption(channel)
        end
    end

    initCheckbox(ChatModel.ChannelState.CHANNEL_WORLD, 'tbt_opt1')
    initCheckbox(ChatModel.ChannelState.CHANNEL_AREA, 'tbt_opt2')
    initCheckbox(ChatModel.ChannelState.CHANNEL_GUILD, 'tbt_opt3')
    initCheckbox(ChatModel.ChannelState.CHANNEL_TEAM, 'tbt_opt4')
    initCheckbox(ChatModel.ChannelState.CHANNEL_PRIVATE, 'tbt_opt5')
    initCheckbox(ChatModel.ChannelState.CHANNEL_SYSTEM, 'tbt_opt6')
    initCheckbox(ChatModel.ChannelState.CHANNEL_PLATFORM, 'tbt_opt7')
end

return _M