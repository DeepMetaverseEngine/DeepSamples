local DisplayUtil = require("Logic/DisplayUtil")


local RadioGroupExt = {}
DisplayUtil.warpOOPSelf(RadioGroupExt)

-- datas <{isLock:bool, errorMsg:string, *}>
function RadioGroupExt.new(redioBtns, defaultBtn, datas, cb, isShowMsg)
    local o = {}
    setmetatable(o, RadioGroupExt)
    o:_init(redioBtns, defaultBtn, datas, cb, isShowMsg)
    return o
end

function RadioGroupExt:setEnable(enable)
    self._enable = not not enable
    for i,v in ipairs(self._btns) do
        v.Enable = self._enable
    end
end

function RadioGroupExt:selectedIdx()
    return self._selectedIdx, self._datas[self._selectedIdx], self._btns[self._selectedIdx]
end

function RadioGroupExt:select(btn)
    local idx = btn and table.indexOf(self._btns, btn) or nil
    self:selectIdx(idx)
end

function RadioGroupExt:selectIdx(idx)
    -- if idx == self._selectedIdx then return end

    local oldIdx = self._selectedIdx
    self._selectedIdx = idx
    for i,v in ipairs(self._btns) do
        local ok = i == idx
        v.IsChecked = ok
    end
end

function RadioGroupExt:_init(btns, defaultBtn, datas, cb, isShowMsg)
    self._btns = btns
    self._datas = datas or {}
    self._cb = cb
    self._isShowMsg = isShowMsg
    self._defaultBtn = defaultBtn
    self._selectedIdx = -1
    self:setEnable(true)
    for i,v in ipairs(self._btns) do
        v.event_PointerClick = self._self__onBtnClick
    end
    self:select(defaultBtn)
end

function RadioGroupExt:_onBtnClick(btn)
    if not self._enable then
        btn.IsChecked = not btn.IsChecked
        return
    end

    local idx = btn and table.indexOf(self._btns, btn) or nil
    local data = self._datas[idx]
    if data and type(data) == 'table' then
        if data.isLock then
            btn.IsChecked = not btn.IsChecked
            if self._isShowMsg and data.errMsg then
                GameAlertManager.Instance:ShowNotify(tostring(data.errMsg))
            end
            return
        end
    end

    local oldIdx = self._selectedIdx
    self:select(btn)
    if self._cb and oldIdx ~= self._selectedIdx then
        self._cb(self._selectedIdx)
    end
end


return RadioGroupExt
