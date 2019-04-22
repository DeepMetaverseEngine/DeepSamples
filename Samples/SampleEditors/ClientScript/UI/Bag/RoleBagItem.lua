local _M = {}
_M.__index = _M
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local Util = require 'Logic/Util'

local function GetItemDetailMenu(self)
    local lastMenu = self.detailMenu
    local detailMenu = GlobalHooks.UI.CreateUI('ItemDetail')
    self:AddSubUI(detailMenu)

    local function DetailMenuOnExit(menu)
        if menu == self.detailMenu then
            self.detailMenu = nil
            if self.list then
                self.list:CleanSelect()
            end
        end
    end

    detailMenu:SubscribOnExit(
        function()
            DetailMenuOnExit(detailMenu)
        end
    )
    self.detailMenu = detailMenu

    if lastMenu and lastMenu ~= detailMenu then
        lastMenu:Close()
    end
end

local function UseItem(self, detail, count)
    DataMgr.Instance.UserData.Bag:Use(
        self.select_index,
        1,
        function(ret)
            if ret then
                local text = Constants.Text.UseSuccess
                if Util.ContainsTextKey(detail.static_consumption.success_hint) then
                    text = Util.GetText(detail.static_consumption.success_hint)
                end
                GameAlertManager.Instance:ShowNotify(text)
            end
        end
    )
end

local function OnShowDetail(self, detail, count)
    if not detail then
        return
    end

    GetItemDetailMenu(self)
    if self._on_init_detail then
        self.detailMenu:Reset({detail = detail, count = count, index = self.select_index, autoHeight = true, globalTouch = true})
        self._on_init_detail(self.detailMenu)
    else
        -- self.detailMenu:EnableTouchFrame(false)
        GetItemDetailMenu(self)
        self.detailMenu:EnableTouchFrameClose(false)
        -- self.detailMenu:SetAutoHeight(false)
        local btns = {}
        -- TODO, item_type 放入const

        if not string.IsNullOrEmpty(detail.static.item_goto) then
            table.insert(
                btns,
                {
                    Text = Constants.Text.detail_btn_use,
                    cb = function()
                        FunctionUtil.OpenFunction(detail.static.item_goto)
                    end
                }
            )
        end

        if detail.static.item_type == 4 then
            table.insert(
                    btns,
                    {
                        Text = Constants.Text.detail_btn_use,
                        cb = function()
                            if detail.static_consumption.can_batch == 1 then
                                GlobalHooks.UI.OpenUI('RoleBagBatchUse', 0, detail, self.detailMenu and self.detailMenu.count or count, self.select_index)
                            else
                                UseItem(self, detail, self.detailMenu and self.detailMenu.count or count)
                            end
                        end
                    }
            )
        elseif detail.static.item_type == 7 then
            table.insert(
                    btns,
                    {
                        Text = Constants.Text.detail_btn_use,
                        cb = function()
                            GlobalHooks.UI.OpenUI('RenamePerson', 0,self.select_index)
                        end
                    }
            )
        elseif detail.static.item_type == 8 then
            table.insert(
                    btns,
                    {
                        Text = Constants.Text.detail_btn_use,
                        cb = function()
                            GlobalHooks.UI.OpenUI('RenameGuild', 0,self.select_index)
                        end
                    }
            )
        elseif detail.static.item_type == 2 and detail.static.sec_type == 98 then   
            table.insert( 
                    btns,
                    {
                        Text = Constants.Text.detail_btn_use,
                        cb = function()
                            local new_detail = ItemModel.GetDetailByBagIndex(self.select_index)
                            -- 發送請帖
                            local HusbandId
                            local WifeId
                            local Husband
                            local Wife
                            local Date1
                            local Times
                            local Time
                            local Friends
                            for _, v in ipairs(new_detail.dynamicAttrs or {}) do
                                if v.Tag == 'HusbandId' then
                                    HusbandId = v.Name
                                elseif v.Tag == 'WifeId' then
                                    WifeId = v.Name
                                elseif v.Tag == 'Husband' then
                                    Husband = v.Name
                                elseif v.Tag == 'Wife' then
                                    Wife = v.Name
                                elseif v.Tag == 'Date' then
                                    Date1 = v.Name
                                elseif v.Tag == 'Times' then
                                    Times = v.Value
                                elseif v.Tag == 'Time' then
                                    Time = v.Value
                                elseif v.Tag == 'Friends' then
                                    Friends = v.SubAttributes
                                end
                            end

                            GlobalHooks.UI.OpenUI('MarryInvite', 0, {HusbandId = HusbandId, WifeId = WifeId, Husband = Husband, Wife = Wife, Date1 = Date1, Times = Times, Time = Time, slotIndex = self.select_index, Friends = Friends })
                        end
                    }
            )
        elseif detail.static.item_type == 2 and detail.static.sec_type == 99 then   
            table.insert( 
                    btns,
                    {
                        Text = Constants.Text.detail_btn_use,
                        cb = function()
                            -- 發送請帖
                            local HusbandId
                            local WifeId
                            local Husband
                            local Wife
                            local Date1
                            local Times
                            local Time
                            for _, v in ipairs(detail.dynamicAttrs or {}) do
                                if v.Tag == 'HusbandId' then
                                    HusbandId = v.Name
                                elseif v.Tag == 'WifeId' then
                                    WifeId = v.Name
                                elseif v.Tag == 'Husband' then
                                    Husband = v.Name
                                elseif v.Tag == 'Wife' then
                                    Wife = v.Name
                                elseif v.Tag == 'Date' then
                                    Date1 = v.Name
                                elseif v.Tag == 'Times' then
                                    Times = v.Value
                                elseif v.Tag == 'Time' then
                                    Time = v.Value
                                end
                            end
                            GlobalHooks.UI.OpenUI('WeddingInfo', 0, {HusbandId = HusbandId, WifeId = WifeId, Husband = Husband, Wife = Wife, Date1 = Date1, Times = Times, Time = Time, slotIndex = self.select_index})
                        end
                    }
            )
        elseif detail.static.item_type == 2 then
            local curbtn = {
                Text = Constants.Text.detail_btn_equip,
                cb = function()
                    local equipBagIndex = detail.static_equip.equip_pos
                    GlobalHooks.UI.CloseUIByTag('BagMain')
                    GlobalHooks.UI.OpenUI('AttributeMain', 0, equipBagIndex, self.select_index)
                    -- DataMgr.Instance.UserData.Bag:Equip(
                    --     self.select_index,
                    --     function(success)
                    --         if success then
                    --             GlobalHooks.UI.CloseUIByTag('BagMain')
                    --             GlobalHooks.UI.OpenUI('AttributeMain', 0, equipBagIndex)
                    --         end
                    --     end
                    -- )
                end
            }
            table.insert(btns, curbtn)
            if detail.static_equip.profession ~= 0 and detail.static_equip.profession ~= DataMgr.Instance.UserData.Pro then
                curbtn.Enable = false
            end
        end
            if detail.static.price > 0 then
                table.insert(
                        btns,
                        {
                            Text = Constants.Text.detail_btn_sell,
                            cb = function()
                                local function AlertOK()
                                    print('onsell ', self.select_index, count)
                                    DataMgr.Instance.UserData.Bag:Sell(
                                            self.select_index,
                                            count,
                                            function(success)
                                            end
                                    )
                                end
                                if detail.static.quality >= Constants.SellConfirmQuality then
                                    UIUtil.ShowConfirmAlert(Constants.Text.confirm_sell, Constants.Text.confirm_sell_title, AlertOK)
                                else
                                    AlertOK()
                                end
                            end
                        }
                )
            end
            if detail.static.can_synthesizing == 1 then
                local ret = GlobalHooks.DB.FindFirst('item_synthetic', {item_id = detail.static.id})
                table.insert(
                        btns,
                        {
                            Text = Constants.Text.detail_btn_combine,
                            cb = function()
                                -- 合成逻辑
                                GlobalHooks.UI.OpenUI('SmithyMain', 0, 'SmithyCompose', ret.target_id)
                            end
                        }
                )
            end
            --分解
            if detail.static.can_decompose == 1 then
                table.insert(
                        btns,
                        {
                            Text = Constants.Text.detail_btn_decompose,
                            cb = function()
                                DataMgr.Instance.UserData.Bag:Decompose(
                                        self.select_index,
                                        count,
                                        function(success)
                                        end
                                )
                            end
                        }
                )
            end

            self.detailMenu:Reset(
                    {detail = detail, nobackground = true, count = count, index = self.select_index, x = 596, y = 123, w = 339, h = 468, buttons = btns}
            )
        end
end

local function OnBagSizeChange(self)
    local bag = DataMgr.Instance.UserData.Bag
    self.comps.lb_num.Text = string.format('%d/%d', bag.Size - bag.EmptySlotCount, bag.Size)
end

local function OnItemSingleSelect(self, new, old)
    if not new or new.IsEmpty then
        local autoSelected
        if not self._on_init_detail and self.detailMenu then
            autoSelected = self.list:SelectFirst()
        end
        if not autoSelected and self.detailMenu then
            print('OnItemSingleSelect ', self.detailMenu)
            self.detailMenu:Close()
        end
    else
        local itdata = self.listener:GetItemData(new)
        self.select_index = self.listener:GetSourceIndex(new.Index)
        OnShowDetail(self, ItemModel.GetDetailByItemData(itdata), itdata.Count)
    end
end

local function SwitchTo(self, info, order)
    if self.listener then
        self.listener:Stop(false)
    end
    self.currentInfo = info
    -- print_r(listener)
    self.listener = info.listener
    order = info.OnMatch and true or order
    self.listener:Start(order, false)
    self.list:Init(self.listener, DataMgr.Instance.UserData.Bag.MaxSize)
    if not self._on_init_detail then
        self.list:SelectFirst()
    end
    self.listener.OnItemUpdateAction = function(act)
        if act.Type == ItemUpdateAction.ActionType.UpdateCount and self.detailMenu and self.detailMenu.index == act.Index then
            local itdata = ItemModel.GetBagItemDataByIndex(act.Index)
            self.detailMenu:SetCount(itdata.Count)
        end
    end
    OnBagSizeChange(self)
    -- select first
    --self.list:Select(0)
end

function _M.SetOnInitDetailCB(self, cb)
    if self._on_init_detail == cb then
        return
    end
    self._on_init_detail = cb
    if self.detailMenu then
        self.detailMenu:Close()
    end
    if not cb and not self.list.EnableMultiSelect then
        self.list:SelectFirst()
    end
end


function _M.OnExit(self)
    self.listener:Stop(false)
    self.detailMenu = nil
    for k, v in pairs(self.swith_listeners) do
        v.listener:Dispose()
    end
end

function _M.OnDestory(self)
    if self.timeid then
        LuaTimer.Delete(self.timeid)
    end
end

local function AddPackUpTimer(self)
    self.timeid =
        LuaTimer.Add(
        1000,
        1000,
        function()
            if not self._packup_cooldown then
                return
            end
            self._packup_cooldown = self._packup_cooldown - 1
            if self._packup_cooldown <= 0 then
                self._packup_cooldown = nil
                local txt = Util.ContainsTextKey('common_arrange') and Util.GetText('common_arrange') or '整理'
                self.comps.btn_neaten.Text = txt
                self.timeid = nil
                return false
            else
                self.comps.btn_neaten.Text = string.format('00:%02d', self._packup_cooldown)
                return true
            end
        end
    )
end

local function OnAddBagSizeTo(self, count)
    local function AlertOK()
        DataMgr.Instance.UserData.Bag:AddSize(
            count,
            function(success)
                OnBagSizeChange(self)
            end
        )
    end

    local bag = DataMgr.Instance.UserData.Bag

    local function CheckCost(e)
        local ret = e.bag_min > bag.Size and e.bag_min <= count
        return ret
    end

    print('OnAddBagSizeTo ', count, bag.Size)
    local costs_static = GlobalHooks.DB.Find('bag_config', CheckCost)
    if #costs_static == 0 then
        UIUtil.ShowErrorMessage(Constants.Text.warn_extrasize)
        return
    end

    --合并数量
    local costs = {}
    for _, v in ipairs(costs_static) do
        costs[v.cost_id] = costs[v.cost_id] or 0
        costs[v.cost_id] = costs[v.cost_id] + v.cost_num
    end
    local useId, useCount = next(costs)
    local detail = ItemModel.GetDetailByTemplateID(useId)
    local itemIcon = 'static/item/' .. detail.static.atlas_id

    local content = Util.Format1234(Constants.Text.confirm_bagsize, itemIcon, useCount)
    UIUtil.ShowConfirmAlert(content, Constants.Text.confirm_bagsize_title, AlertOK)
end

function _M.OpenRoleBagDecompose(self)
    GlobalHooks.UI.CloseUIByTag('RoleBagDecompose')
    self.comps.tbt_decompose.IsChecked = true
    local ui = GlobalHooks.UI.CreateUI('RoleBagDecompose')
    self:AddSubUI(ui)
end

function _M.OnEnter(self)
    
    --类别
    local tbts = {
        self.comps.tbt_all,
        self.comps.tbt_equip,
        self.comps.tbt_consume,
        self.comps.tbt_collect
    }
    local swith_listeners = {
        tbt_all = {},
        tbt_equip = {adscription_tab = 1},
        tbt_consume = {adscription_tab = 2},
        tbt_collect = {adscription_tab = 3}
    }

    for k, v in pairs(swith_listeners) do
        local listener = ItemListener(DataMgr.Instance.UserData.Bag, false, DataMgr.Instance.UserData.Bag.Size)
        listener.OnMatch = function(itdata)
            if self.currentInfo.OnMatch then
                return self.currentInfo.OnMatch(itdata)
            end
            local detail = ItemModel.GetDetailByTemplateID(itdata.TemplateID)
            if not detail then
                print('no detail ...', itdata.TemplateID)
                return false
            end
            local ret = true
            for kk, vv in pairs(v) do
                if detail.static[kk] and detail.static[kk] ~= vv then
                    ret = false
                    break
                end
            end
            return ret
        end
        v.listener = listener
        listener.OnFilledSizeChange = function(size)
            OnBagSizeChange(self, size)
        end
    end

    self.swith_listeners = swith_listeners
    -- setup
    UIUtil.ConfigToggleButton(
        tbts,
        self.comps.tbt_all,
        false,
        function(sender)
            if sender.IsChecked and self.ui.IsRunning then
                local info = self.swith_listeners[sender.EditName]
                local is_all = sender == self.comps.tbt_all
                SwitchTo(self, info, not is_all)
            end
        end
    )
    
    self.comps.tbt_all.IsChecked = true
    self.comps.tbt_decompose.IsChecked = false
    if self.timeid and not self._packup_cooldown then
        LuaTimer.Delete(self.timeid)
    end
    -- UIUtil.NumberPlusPlus(self.comps.btn_neaten, 54,6000,2)
end

function _M.OnInit(self)
    print('rolebagitem init')
    self.ui:EnableTouchFrame(false)
    self.comps.tbt_decompose.TouchClick = function(sender)
        if sender.IsChecked then
            local ui = GlobalHooks.UI.CreateUI('RoleBagDecompose')
            self:AddSubUI(ui)
        else
            GlobalHooks.UI.CloseUIByTag('RoleBagDecompose')
        end
    end
    local cvs_item = self.comps.cvs_item
    --cvs_item.Visible = false
    self.list = ItemList(cvs_item.Size2D, Constants.Item.DefaultSize, 5)
    self.list.Position2D = cvs_item.Position2D
    self.list.ShowBackground = true
    self.list.EnableSelect = true
    self.list.OnItemSingleSelect = function(new, old)
        OnItemSingleSelect(self, new, old)
    end
    self.list.OnItemClick = function(it)
        if it.Status == ItemStatus.Lock then
            OnAddBagSizeTo(self, it.Index + 1)
        end
    end
    self.list.OnItemInit = function(it)
        local itdata = self.listener:GetItemData(it)
        if itdata and itdata.ID ~= nil and itdata.ID ~= '' then
            local detail = ItemModel.GetDetailByItemData(itdata)
            local lvlimt = detail.static.level_limit
            if lvlimt > DataMgr.Instance.UserData.Level then
                it.LevelLimit = true
                return
            end
            if not detail.static_equip then
                return
            end
            if detail.static_equip.profession ~= 0 and detail.static_equip.profession ~= DataMgr.Instance.UserData.Pro then
                return
            end
            local score = ItemModel.GetItemScore(DataMgr.Instance.UserData.EquipBag, detail.static_equip.equip_pos)
            if score < detail.score then
                it.IsArrowUp = true
            end
        end
    end
    cvs_item.Parent:AddChild(self.list)


    -- 整理
    self.comps.btn_neaten.TouchClick = function()
        if not self._packup_cooldown then
            DataMgr.Instance.UserData.Bag:PackUpItems(
                function()
                    if not self._on_init_detail then
                        self.list:SelectFirst()
                    end
                    if self.currentInfo and self.currentInfo.OnPackup then
                        self.currentInfo.OnPackup()
                    end
                end
            )
            self._packup_cooldown = Constants.BagPackUpCoolDownSec
            self.comps.btn_neaten.Text = string.format('00:%2d', Constants.BagPackUpCoolDownSec)
            AddPackUpTimer(self)
        end
    end
end

function _M.LockCategory(self, comp, matchfn, packupfn)
    -- local info = self.swith_listeners.tbt_equip
    self.comps.cvs_an.EnableChildren = false
    self.swith_listeners[comp].OnMatch = matchfn
    self.swith_listeners[comp].OnPackup = packupfn
    self.comps[comp].IsChecked = true
    -- SwitchTo(self, info, false)
end

function _M.UnlockCategory(self, comp)
    self.comps.cvs_an.EnableChildren = true
    self.swith_listeners[comp].OnMatch = nil
    self.swith_listeners[comp].OnPackup = nil
    if self.comps.tbt_all.IsChecked then
        SwitchTo(self, self.currentInfo, false)
    else
        self.comps.tbt_all.IsChecked = true
    end
end

function _M.EnableMultiSelect(self, val, fn)
    self.list.EnableMultiSelect = val
    if val then
        self.list.OnItemMultiSelect = function(itnew, itold)
            if fn then
                fn(self.listener, itnew)
            end
        end
        if self.detailMenu then
            self.detailMenu:Close()
        end
    else
        self.list.OnItemMultiSelect = nil
        self.list:SelectFirst()
    end
end

function _M.CleanAllSelect(self)
    self.list:CleanSelect()
end

return _M
