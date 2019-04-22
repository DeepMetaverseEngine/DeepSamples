local DisplayUtil = require("Logic/DisplayUtil")


local ToggleButtonExt = {}
DisplayUtil.warpOOPSelf(ToggleButtonExt)

-- datas <{isLock:bool, errorMsg:string, *}>
function ToggleButtonExt.new(btns, defaultBtn, datas, cb)
    local o = {}
    setmetatable(o, ToggleButtonExt)
    o:_init(btns, defaultBtn, datas, cb)
    return o
end

function ToggleButtonExt:setEnable(enable)
    self._enable = not not enable
end

function ToggleButtonExt:selectedIdx()
    return self._selectedIdx, self._datas[self._selectedIdx], self._btns[self._selectedIdx]
end

function ToggleButtonExt:select(btn)
    local idx = btn and table.indexOf(self._btns, btn) or nil
    self:selectIdx(idx)
end

function ToggleButtonExt:selectIdx(idx)
    self._selectedIdx = idx
    for i,v in ipairs(self._btns) do
        v.Visible = i == idx
    end
end

function ToggleButtonExt:_init(btns, defaultBtn, datas, cb)
    self._btns = btns
    self._datas = datas or {}
    self._cb = cb
    self._defaultBtn = defaultBtn or btns[1]
    self._selectedIdx = -1
    self:setEnable(true)
    for i,v in ipairs(self._btns) do
        v.TouchClick = self._self__onBtnClick
    end
    self:select(defaultBtn)
end

function ToggleButtonExt:_onBtnClick(btn)
    if not self._enable then
        return
    end

    local idx = self._selectedIdx % #self._btns + 1
    self:selectIdx(idx)
    if self._cb and oldIdx ~= self._selectedIdx then
        self._cb(self._selectedIdx, self._datas[self._selectedIdx])
    end
end

return ToggleButtonExt
