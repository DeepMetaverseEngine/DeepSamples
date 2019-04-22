-- File: TagFrame.lua
-- Author: Ying Zuo
local UIUtil = require 'UI/UIUtil.lua'
local _M = {}
_M.__index = _M

function _M.OnEnter( self, subTag, ...)
    self.currentTag = nil
    self.enterParams = {...}
    local function ToggleFunc(sender)        
        local tag = sender.UserData
        if sender.Tag ~= nil then --如果有固定参数，就插入到参数列表的第一个位置
            table.insert(self.enterParams, 1, sender.Tag)
        end
        local subui = GlobalHooks.UI.CreateUI(tag, 0, unpack(self.enterParams))
        self.enterParams = {}
        if not subui then
            sender.IsChecked = false
        else
            if self.subui then
                self.subui:Close()
            end
            
            self.currentTag = sender.UserTag
            subui.ui.menu.Enable = false
            self.ui:AddSubUI(subui.ui)
            self.subui = subui.ui
            self.subui:SubscribOnExit('TagFrame.ToggleFunc',function() self.subui = nil end)
        end
    end
    
    local default
    for k, v in pairs(self.initParams) do
        if v == subTag then
            default = self.ui.comps[k]
            print("default--------------",k,v,subTag)
        end
    end
    UIUtil.ConfigToggleButton(self.tbts, default, false, ToggleFunc)
end

function _M.OnExit(self)
    
end

function _M.OnInit(self, initParams)
    local tbts = {}
    self.initParams = initParams
    for k,v in pairs(initParams) do
        print(k, v)
        if k == 'OnInit' then
            v(self)
        else
            local comp = self.ui.comps[k]
            if type(v) == 'table' then
                comp.UserData = v.tag
                comp.Tag = v.arg
            else
                comp.UserData = v
            end
            table.insert(tbts, comp)
        end
    end
    self.ui.comps.menu.Enable = false
    self.tbts = tbts
    if self.ui.comps.btn_close then
        self.ui.comps.btn_close.TouchClick = function()
            self.ui:Close()
        end
    end

    
end

return _M
