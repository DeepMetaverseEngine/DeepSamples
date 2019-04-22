local _M = {}
_M.__index = _M
local ItemModel = require 'Model/ItemModel'
local current_lv
local cost_listener
local function UpdateCostListen(winlv)
    -- print('UpdateCostListen', current_lv, winlv)
    if current_lv == winlv then
        return
    end
    current_lv = winlv
    if cost_listener then
        cost_listener:Dispose()
    end
    local info = GlobalHooks.DB.FindFirst('wings_prop', current_lv)
    local costs = ItemModel.ParseCostAndCostGroup(info)
    GlobalHooks.UI.SetRedTips('wing', ItemModel.IsCostAndCostGroupEnough(costs) and 1 or 0)
    cost_listener =
        ItemModel.ListenManyCost(
        costs,
        function()
            -- print('costs ---- listened', enough)
            for _, v in ipairs(costs) do
                ItemModel.RecalcCostAndCostGroup(v)
            end
            local enough = ItemModel.IsCostAndCostGroupEnough(costs) and 1 or 0
            GlobalHooks.UI.SetRedTips('wing', enough)
        end
    )
end

function _M.RequestWingInfo(cb, errcb)
    Protocol.RequestHandler.ClientGetWingInfoRequest(
        {},
        function(rsp)
            if cb then
                cb(rsp)
            end
        end,
        function()
            if errcb then
                errcb()
            end
        end
    )
end

function _M.RequestEquipWingAvatar(rank, equip, cb, errcb)
    Protocol.RequestHandler.ClientEquipWingAvatarRequest(
        {c2s_equip = equip, c2s_rank = rank},
        function(rsp)
            cb()
        end,
        errcb
    )
end

function _M.RequestAddWingExp(auto, cb, errcb)
    Protocol.RequestHandler.ClientAddWingExpRequest(
        {c2s_auto = auto},
        function(rsp)
            UpdateCostListen(rsp.s2c_wingLv)
            cb(rsp)
        end,
        function()
            if errcb then
                errcb()
            end
        end
    )
end

function _M.OnBagReady()
    _M.RequestWingInfo(
        function(rsp)
            UpdateCostListen(rsp.s2c_wingLv)
        end
    )
end

function _M.fin()
    -- print('fin coming')
    if cost_listener then
        cost_listener:Dispose()
    end
end

return _M
