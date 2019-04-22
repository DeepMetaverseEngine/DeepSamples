
local _M = {}
_M.__index = _M

function _M.OnEnter( self, ...)
    self.enterParams = {...}
    for k, v in pairs(self.initParams) do
        -- print("v--------------",k,v)
        if k ~= 'OnInit' then
            local params = self.enterParams
            if k ~= 'subui_1' then
               params = {}
            end
            local subui =  GlobalHooks.UI.CreateUI(v, 0, unpack(params))
            subui:EnableTouchFrame(false)
            local subClose = subui.menu:FindChildByEditName('btn_close', true)
            if subClose then
                subui.menu.OnCloseEvent = function( ... )
                    self.ui:Close()
                end
            end
            self:AddSubUI(subui)
        end
    end
    --SoundManager.Instance:PlaySoundByKey('uiopen',false)
end

function _M.OnExit(self)
    
end

function _M.OnInit(self, initParams)
    self.initParams = initParams
    for k,v in pairs(initParams) do
        print(k, v)
        if k == 'OnInit' then
            v(self)
        end
    end
    self.ui.comps.btn_close.TouchClick = function()
        self.ui:Close()
    end
end

return _M