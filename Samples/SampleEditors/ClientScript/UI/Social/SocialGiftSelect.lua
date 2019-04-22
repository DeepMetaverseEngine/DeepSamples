local _M = {}
_M.__index = _M
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local SocielModel = require 'Model/SocialModel'

local function Draw(self)
    self.comps.lb_num.Text = self.cur
end

local function DoUse(self)
    if self.cb then
        self.cb(self.cur)
    end
    self:Close()
end

local function SetCount(self, count)
    self.cur = count
    self.cur = math.min(self.cur, self.max)
    self.cur = math.max(self.cur, self.min)
    Draw(self)
end

function _M.OnEnter(self, detail, count, cb)
    self.min = 1
    self.max = count
    self.cur = 1
    self.detail = detail
    self.cb = cb
    self.comps.lb_name.Text = Util.GetText(detail.static.name)
    self.comps.lb_name.FontColorRGB = Constants.QualityColor[detail.static.quality]
    local itshow = UIUtil.SetItemShowTo(self.comps.cvs_item, detail, count)
    itshow.EnableTouch = true
    local pos = self.menu:LocalToUIGlobal(self.comps.lb_num)
    local posParam = { pos = { x = pos.x, y = pos.y + self.comps.lb_num.Height }, anchor = { x = 0, y = 0 } }

    itshow.TouchClick = function()
        GlobalHooks.UI.OpenUI(
            'NumInput',
            0,
            1,
            count,
            posParam,
            function(value)
                print('value ',value)
                SetCount(self, value)
            end
        )
    end
    self.comps.lb_num.event_PointerClick=function()
        GlobalHooks.UI.OpenUI('NumInput', 0, 1, count, {pos={x=410,y=350}, anchor=nil}, function(value)
            SetCount(self,value)
        end)
    end
    Draw(self)
end

function _M.OnExit(self)
    self.cb = nil
end

function _M.OnInit(self)
    self.menu.ShowType = UIShowType.Cover
    self.ui.comps.btn_alluse.Visible = false
    self.ui.comps.btn_present.Visible = true

    self.comps.btn_max.TouchClick = function()
        SetCount(self, self.max)
    end
    self.comps.btn_min.TouchClick = function()
        SetCount(self, self.min)
    end
    self.comps.btn_no.TouchClick = function()
        self:Close()
    end

    self.comps.btn_less.TouchClick = function()
        SetCount(self, self.cur - 1)
    end

    self.comps.btn_more.TouchClick = function()
        SetCount(self, self.cur + 1)
    end

    self.comps.btn_present.TouchClick = function()
        DoUse(self)
    end

    self.comps.lb_num.Enable = true
    self.comps.lb_num.IsInteractive = true
end

return _M
