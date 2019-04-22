local _M = {}
_M.__index = _M
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local QuestModel = require 'Model/QuestModel'
local ItemModel = require 'Model/ItemModel'

local function UpdateEachItem(self, node, data)
    local btn_getcarriage = UIUtil.FindChild(node, 'btn_getcarriage')
    local lb_cashrnum = UIUtil.FindChild(node, 'lb_cashrnum', true)
    local lb_name = UIUtil.FindChild(node, 'lb_name', true)
    local ib_show = UIUtil.FindChild(node, 'ib_show')
    local lb_cash = UIUtil.FindChild(node, 'lb_cash')
    local cvs_cost = UIUtil.FindChild(node, 'cvs_cost')
    lb_name.Text = Util.GetText(data.carriage_name)
    lb_cash.Text = Util.GetText(data.text_desc)
    if not string.IsNullOrEmpty(data.carriage_pic) then
        UIUtil.SetImage(ib_show, data.carriage_pic)
    end

    btn_getcarriage.TouchClick = function()
        QuestModel.RequestClientAcceptCarriage(data.quest_id,function()
            self:Close()
        end)
    end
    local costs = ItemModel.ParseCostAndCostGroup(data)
    local itshow = UIUtil.SetItemShowTo(cvs_cost, costs[1].detail)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
        local pos = self.menu:LocalToUIGlobal(itshow)
        local params = {x = pos.x - 150, y = pos.y, detail = costs[1].detail, itemShow = itshow, autoHeight = true, anchor = 'l_b'}
        UIUtil.ShowNormalItemDetail(params)
    end
    UIUtil.SetEnoughLabel(self, lb_cashrnum, costs[1].cur, costs[1].need, true)
    local enough = true
    for _, v in ipairs(costs) do
        if v.cur < v.need then
            enough = false
        end
    end
    btn_getcarriage.Enable = enough
    btn_getcarriage.IsGray = not enough

    local reward_xls = EventApi.GetExcelByEventKey(data.eventreward)
    if reward_xls then
        for i = 1, 3 do
            local cvs_item = UIUtil.FindChild(node, 'cvs_item' .. i, true)
            if reward_xls.show_item[i] > 0 then
                cvs_item.Visible = true
                local detail = ItemModel.GetDetailByTemplateID(reward_xls.show_item[i])
                local itshow = UIUtil.SetItemShowTo(cvs_item, detail)
                itshow.EnableTouch = true
                itshow.TouchClick = function()
                    local pos = self.menu:LocalToUIGlobal(itshow)
                    local params = {x = pos.x - 150, y = pos.y, detail = detail, itemShow = itshow, autoHeight = true, anchor = 'l_b'}
                    UIUtil.ShowNormalItemDetail(params)
                end
            else
                cvs_item.Visible = false
            end
        end
    end
end

function _M.OnEnter(self)
    QuestModel.RequestTodayCarriageCount(
        function(count)
            local limitCount = GlobalHooks.DB.GetGlobalConfig('transport_times')
            self.comps.lb_normalnum.Text = count .. '/' .. limitCount
        end
    )

    local ele = GlobalHooks.DB.FindFirst('personal_carriage', {is_deducttimes = 0})
    local costs = ItemModel.ParseCostAndCostGroup(ele)
    local count = 0
    for _, v in ipairs(costs) do
        count = math.min(math.floor(v.cur / v.need))
    end
    self.comps.lb_luckynum.Text = count
    local all = GlobalHooks.DB.Find('personal_carriage',{carriage_type = 1})

    UIUtil.ConfigHScrollPan(
        self.comps.sp_info,
        self.comps.cvs_type,
        #all,
        function(node, index)
            UpdateEachItem(self, node, all[index])
        end
    )
end

function _M.OnExit(self)
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.comps.cvs_type.Visible = false
end

return _M
