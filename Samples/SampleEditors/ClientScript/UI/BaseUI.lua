---------------------------------
--! @file
--! @brief a Doxygen::Lua BaseUI.lua
---------------------------------
local _M = {}
_M.__index = _M

local CompGet = {}
CompGet.__index = function(self, key)
    return self.menu:GetComponent(key)
end

local function OnLoad(self, callback)
    if self._on_load_cbs then
        for _, v in pairs(self._on_load_cbs or {}) do
            v(self, callback)
        end
    else
        callback:Invoke(true)
    end
end

local function OnEnter(self)
    self.IsRunning = true
    for _, v in pairs(self._on_enter_cbs or {}) do
        v(self)
    end
end

local function OnExit(self)
    self.IsRunning = false
    for _, v in pairs(self._on_exit_cbs or {}) do
        v(self)
    end
    for _, v in pairs(self._on_exit_once_cbs or {}) do
        v(self)
    end
    self._on_exit_once_cbs = nil
end

local function OnDestory(self)
    for _, v in pairs(self._on_destory_cbs or {}) do
        v(self)
    end
    self._on_exit_cbs = nil
    self._on_destory_cbs = nil
    self._on_load_cbs = nil
    self._on_enter_cbs = nil
end

local function OnEnable(self)
    for _, v in pairs(self._on_enable_cbs or {}) do
        v(self)
    end
end

local function OnDisable(self)
    for _, v in pairs(self._on_disable_cbs or {}) do
        v(self)
    end
end

local function InitComponent(self, tag, xmlpath)
    self.menu = MenuBase.Create(tag, xmlpath)
    self.tag = tag
    if not self.menu then
        error('MenuBase create error ' .. xmlpath)
    end
    self.comps = setmetatable({menu = self.menu}, CompGet)
    self.root = self.menu.mRoot
    self.menu.OnExitEvent = function()
        OnExit(self)
    end

    self.menu.OnLoadEvent = function(callback)
        OnLoad(self, callback)
    end

    self.menu.OnEnterEvent = function()
        OnEnter(self)
    end

    self.menu.OnDestoryEvent = function()
        OnDestory(self)
    end

    self.menu.OnEnableEvent = function()
        OnEnable(self)
    end

    self.menu.OnDisableEvent = function()
        OnDisable(self)
    end
end

local function SubscribOnLoad(self, key, cb)
    self._on_load_cbs = self._on_load_cbs or {}
    if type(key) == 'function' then
        cb = key
        key = 'default'
    end
    self._on_load_cbs[key] = cb
end

local function SubscribOnEnter(self, key, cb)
    self._on_enter_cbs = self._on_enter_cbs or {}
    if type(key) == 'function' then
        cb = key
        key = 'default'
    end
    self._on_enter_cbs[key] = cb
end

local function SubscribOnExit(self, key, cb)
    self._on_exit_cbs = self._on_exit_cbs or {}
    if type(key) == 'function' then
        cb = key
        key = 'default'
    end
    self._on_exit_cbs[key] = cb
end

local function SubscribOnExitOnce(self, key, cb)
    if type(key) == 'function' then
        cb = key
        key = tostring(cb)
    end
    self._on_exit_once_cbs = self._on_exit_once_cbs or {}
    self._on_exit_once_cbs[key] = cb
end

local function SubscribOnDestory(self, key, cb)
    self._on_destory_cbs = self._on_destory_cbs or {}
    if type(key) == 'function' then
        cb = key
        key = 'default'
    end
    self._on_destory_cbs[key] = cb
end

local function SubscribOnEnable(self, key, cb)
    self._on_enable_cbs = self._on_enable_cbs or {}
    if type(key) == 'function' then
        cb = key
        key = 'default'
    end
    self._on_enable_cbs[key] = cb
end

local function SubscribOnDisable(self, key, cb)
    self._on_disable_cbs = self._on_disable_cbs or {}
    if type(key) == 'function' then
        cb = key
        key = 'default'
    end
    self._on_disable_cbs[key] = cb
end

local function UnSubscribOnLoad(self, key)
    if type(key) == 'function' then
        key = 'default'
    end
    self._on_load_cbs[key] = nil
end

local function UnSubscribOnEnter(self, key)
    if type(key) == 'function' then
        key = 'default'
    end
    self._on_enter_cbs[key] = nil
end

local function UnSubscribOnExit(self, key)
    if type(key) == 'function' then
        key = 'default'
    end
    if self._on_exit_cbs then
        self._on_exit_cbs[key] = nil
    end
    if self._on_exit_once_cbs then
        self._on_exit_once_cbs[key] = nil
    end
end

local function UnSubscribOnDestory(self, cb)
    if type(key) == 'function' then
        key = 'default'
    end
    self._on_destory_cbs[key] = nil
end

local function UnSubscribOnEnable(self, key)
    if type(key) == 'function' then
        key = 'default'
    end
    self._on_enable_cbs[key] = nil
end

local function UnSubscribOnDisable(self, key)
    if type(key) == 'function' then
        key = 'default'
    end
    self._on_disable_cbs[key] = nil
end

local function Close(self)
    self.menu:Close()
end

local function AddSubUI(self, ui)
    self.menu:AddSubMenu(ui.menu)
end

local function EnableTouchFrame(self, val)
    self.menu.Enable = val
    if self.menu.mRoot then
        self.menu.mRoot.Enable = val
    end
end

local function EnableChildren(self, val)
    self.menu.EnableChildren = val
end

local function EnableTouchFrameClose(self, val)
    self.menu.Enable = val == true
    self.menu.mRoot.Enable = not self.menu.Enable
    if val then
        self.menu.event_PointerClick = function()
            self:Close()
        end
    else
        self.menu.event_PointerClick = nil
    end
end

local function Create(tag, xml)
    local ret = {}
    setmetatable(ret, _M)
    InitComponent(ret, tag, xml)
    return ret
end

_M.Close = Close
_M.Create = Create
_M.SubscribOnLoad = SubscribOnLoad
_M.SubscribOnEnter = SubscribOnEnter
_M.SubscribOnExit = SubscribOnExit
_M.SubscribOnDestory = SubscribOnDestory
_M.SubscribOnExitOnce = SubscribOnExitOnce
_M.SubscribOnEnable = SubscribOnEnable
_M.SubscribOnDisable = SubscribOnDisable
_M.UnSubscribOnLoad = UnSubscribOnLoad
_M.UnSubscribOnEnter = UnSubscribOnEnter
_M.UnSubscribOnExit = UnSubscribOnExit
_M.UnSubscribOnDestory = UnSubscribOnDestory
_M.UnSubscribOnEnable = UnSubscribOnEnable
_M.UnSubscribOnDisable = UnSubscribOnDisable
_M.AddSubUI = AddSubUI
_M.EnableTouchFrame = EnableTouchFrame
_M.EnableChildren = EnableChildren
_M.EnableTouchFrameClose = EnableTouchFrameClose
return _M
