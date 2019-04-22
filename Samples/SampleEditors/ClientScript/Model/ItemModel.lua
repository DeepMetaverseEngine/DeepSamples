local _M = {}
_M.__index = _M
local all_details = {}
local Helper = require 'Logic/Helper.lua'
local Util = require 'Logic/Util.lua'
local SnapExtReader = require 'Model/SnapReader'

local grid_gem_map
local grid_refine_map
local gemHole
local function GetGridRefine(equip_pos)
    return grid_refine_map[equip_pos]
end

local function GetAllRefineList()
    return grid_refine_map
end

local function GetMaxWholeRefineStaticInfo()
    local minInnerLv = -1
    for k, v in pairs(Constants.EquipPartName) do
        local info = GetGridRefine(k)
        local rank = info and info.Rank or 0
        local lv = info and info.Lv or 0
        if minInnerLv < 0 then
            minInnerLv = rank * 1000 + lv
        else
            minInnerLv = math.min(minInnerLv, rank * 1000 + lv)
        end
    end
    local wholeRank = math.floor(minInnerLv / 1000)
    local wholeLv = minInnerLv % 1000
    return GlobalHooks.DB.FindFirst('EquipRefine', {refine_rank = wholeRank, refine_lv = wholeLv})
end

local function GetGridGem(equipPos, gemIndex)
    local gridGem = grid_gem_map[equipPos]
    if not gridGem or not gridGem.Slots then
        error('GetGridGem' .. PrintTable(grid_gem_map))
    end
    for _, v in ipairs(gridGem.Slots) do
        if v.SlotIndex == gemIndex then
            return v
        end
    end
    error('GetGridGem' .. PrintTable(gridGem))
end

local function IsGemSlotLocked(slotIndex)
    if not gemHole then
        local holes = GlobalHooks.DB.Find('equip_gem_hole', {})
        gemHole = {}
        for _, v in ipairs(holes) do
            gemHole[v.hole_order] = v
        end
    end
    local hole = gemHole[slotIndex]
    return DataMgr.Instance.UserData.Level < hole.player_lv
end

local function SetGridGem(equipPos, gemIndex, gem)
    local gridGem = grid_gem_map[equipPos]
    for i, v in ipairs(gridGem.Slots) do
        if v.SlotIndex == gemIndex then
            gridGem.Slots[i] = gem
            break
        end
    end
end

local function IsExistGem(equipPos, gemIndex)
    local gridGem = grid_gem_map[equipPos]
    if not gridGem then
        error('GetGridGem' .. PrintTable(grid_gem_map))
    end
    for _, v in ipairs(gridGem.Slots) do
        if v.SlotIndex == gemIndex then
            return v.GemTemplateID and v.GemTemplateID > 0 or false
        end
    end
    error('IsExistGem' .. PrintTable(gridGem))
end

local function GetFirstEmptyGemSlot(equipPos)
    local gridGem = grid_gem_map[equipPos]
    for _, v in ipairs(gridGem.Slots) do
        if v.GemTemplateID == 0 then
            return v.SlotIndex
        end
    end
end

local function GetAttributeID(v)
    if v.ID and v.ID ~= 0 then
        return v.ID
    elseif not string.IsNullOrEmpty(v.Name) then
        return GlobalHooks.DB.FindFirst('Attribute', {key = v.Name}).id
    else
        error('attr error ' .. PrintTable(v))
    end
end

local function MergerAttributeValueToMap(attrs)
    local allAttrs = {}
    for i, v in ipairs(attrs) do
        local id = GetAttributeID(v)
        if allAttrs[id] then
            allAttrs[id].Value = allAttrs[id].Value + v.Value
        else
            allAttrs[id] = {ID = v.ID, ValueType = v.ValueType, Value = v.Value, Tag = v.Tag, Name = v.Name}
        end
    end
    return allAttrs
end

local function MergerAttributeValue(attrs)
    return table.Values(MergerAttributeValueToMap(attrs))
end

local function DiffAttributesWith(s1, s2)
    local m1 = MergerAttributeValueToMap(s1)
    local m2 = MergerAttributeValueToMap(s2)
    local ret = {}
    for k, v in pairs(m1) do
        local cmp = m2[k]
        if cmp then
            if cmp.Value ~= v.Value then
                cmp.Value = v.Value - cmp.Value
                table.insert(ret, cmp)
            end
        else
            table.insert(ret, v)
        end
    end

    for k, v in pairs(m2) do
        if not m1[k] then
            v.Value = -v.Value
            table.insert(ret, v)
        end
    end
    return ret
end

local function GetGridGemAttribute(equipPos)
    local gridGem = grid_gem_map[equipPos]
    if not gridGem then
        return
    end
    local allAttrs = {}
    for i, v in ipairs(gridGem.Slots) do
        for _, vv in ipairs(v.Properties or {}) do
            table.insert(allAttrs, vv)
        end
    end
    return allAttrs
end

local function CalcAttributesScore(attrs, tag)
    local ret = 0
    for i, v in ipairs(attrs or {}) do
        if not tag or v.Tag == tag then
            local attr
            if v.ID ~= 0 and type(v.ID) == 'number' then
                attr = GlobalHooks.DB.FindFirst('Attribute', {id = v.ID})
            else
                attr = GlobalHooks.DB.FindFirst('Attribute', {key = v.Name})
            end
            local value = v.Value or 0
            -- print_r('calc attrs',attr,v)
            -- print('CalcAttributesScore',i, value, attr.fight, value * (attr.fight / 10000))
            if attr then
                ret = ret + value * (attr.fight / 10000)
            end
        end
    end
    -- print('calc score',ret)
    return math.floor(ret)
end

local function TryAddAttribute(all, key, v, allowzero)
    --print('TryAddAttribute',key, v)
    if (v and v ~= 0) or (v and allowzero) then
        table.insert(all, {Name = key, Tag = 'FixedAttributeTag', Value = v, ValueType = 1})
    end
end

local function GetXlsFixedAttribute(static_data, allowzero)
    local all = {}
    TryAddAttribute(all, 'attack', static_data.attack, allowzero)
    TryAddAttribute(all, 'maxhp', static_data.maxhp, allowzero)
    TryAddAttribute(all, 'defend', static_data.defend, allowzero)
    TryAddAttribute(all, 'mdef', static_data.mdef, allowzero)
    TryAddAttribute(all, 'through', static_data.through, allowzero)
    TryAddAttribute(all, 'block', static_data.block, allowzero)
    TryAddAttribute(all, 'crit', static_data.crit, allowzero)
    TryAddAttribute(all, 'rescrit', static_data.rescrit, allowzero)
    TryAddAttribute(all, 'hit', static_data.hit, allowzero)
    TryAddAttribute(all, 'dodge', static_data.dodge, allowzero)
    TryAddAttribute(all, 'defreduction', static_data.defreduction, allowzero)
    TryAddAttribute(all, 'mdefreduction', static_data.mdefreduction, allowzero)
    TryAddAttribute(all, 'autorecoverhp', static_data.autorecoverhp, allowzero)
    return all
end

local function GetItemFixedAttribute(detail)
    local all = GetXlsFixedAttribute(detail.static_equip)
    local extra_attr = GlobalHooks.DB.Find('EquipExtraAttr', {extra_attr = detail.static_equip.extra_attr})
    for _, v in ipairs(extra_attr or {}) do
        table.insert(all, {ID = v.attr_id, Tag = 'ExtraAttributeTag', Value = v.attr_num, ValueType = v.attr_type})
    end
    detail.dynamicAttrs = all
    detail.score = -1
end

local function CreateTemplateDetail(templateID)
    local detail = {}
    detail.static = GlobalHooks.DB.Find('Item', templateID)
    if detail.static == nil then
        local err = 'item templateId ' .. templateID .. ' is nil'
        print(err)
        detail.static = GlobalHooks.DB.Find('Item', 1)
        detail.static.id = templateID
        detail.static.name = tostring(templateID)
        detail.static.item_type = 1
        detail.static.sec_type = 1
        detail.static.atlas_id = 'nitx.png'
        detail.static.desc = err
        detail.static.using_desc = err
    end

    detail.static_equip = GlobalHooks.DB.Find('Equip', templateID)
    detail.static_consumption = GlobalHooks.DB.Find('item_consumption', templateID)

    if detail.static_equip then
        GetItemFixedAttribute(detail)
    end
    --计算基础属性
    return detail
end

local function IsDetailCached(id)
    return all_details[id] ~= nil
end

-- local function RequestDetailByID(id, cb)
--     if all_details[id] then
--         local detail = all_details[id]
--         cb(detail)
--     else
--         --request
--         Protocol.RequestHandler.ClientItemDataRequest(
--             {c2s_id = id},
--             function(rp)
--                 local snap = rp.s2c_item
--                 all_details[snap.templateId] = all_details[snap.templateId] or CreateTemplateDetail(snap.templateId)
--                 local ret = Helper.copy_table(all_details[snap.templateId])
--                 -- todo bugfix
--                 ret.dynamicAttrs = rp.s2c_properties
--                 ret.score = CalcAttributesScore(ret.dynamicAttrs)
--                 all_details[snap.id] = ret
--                 cb(ret)
--                 -- print_r(rp)
--             end
--         )
--     end
-- end

local function ItemPropertyDataToAttrTable(properties)
    if not properties then
        return nil
    end
    if type(properties) == 'table' then
        return properties
    end
    local attrs = {}
    for v in Slua.iter(properties) do
        attrs = attrs
        local attr = {Index = v.Index, ID = v.ID, ValueType = v.ValueType, Value = v.Value, Tag = v.Tag, Name = v.Name}
        if v.SubAttributes then
            attr.SubAttributes = ItemPropertyDataToAttrTable(v.SubAttributes)
        end
        table.insert(attrs, attr)
    end
    return attrs
end

local function IsAttritbuteLocked(attr)
    for _, v in ipairs(attr.SubAttributes or {}) do
        if v.Tag == ItemPropertyData.IsLockedTag then
            return true
        end
    end
    return false
end

local function GetDetailByItemData(itdata, params)
    if not itdata then
        return
    end
    -- if itdata.ID and all_details[itdata.ID] then
    -- 	local ret = Helper.copy_table(all_details[itdata.ID])
    -- 	return ret
    -- end
    params = params or {}
    if itdata.ID and not params.refine and not params.gem and not params.noGridRefine then
        local slot = DataMgr.Instance.UserData.EquipBag:FindSlotByID(itdata.ID)
        if not slot.IsNull then
            params.refine = _M.GetGridRefine(slot.Index)
            params.gem = grid_gem_map[slot.Index]
        end
    end
    local detail = _M.GetDetailByTemplateID(itdata.TemplateID, itdata.Properties, params.refine, params.gem)
    if not detail then
        return
    end
    detail.bind = not itdata.CanTrade
    detail.id = itdata.ID
    return detail
end

local function FindItemData(id)
    return DataMgr.Instance.UserData:FindItemDataByID(id)
end

local function GetDetailByID(id, refine, gem)
    local itdata = DataMgr.Instance.UserData:FindItemDataByID(id)
    local selfEquiped
    local slot = DataMgr.Instance.UserData.EquipBag:FindSlotByID(id)
    if not refine then
        if not slot.IsNull then
            refine = _M.GetGridRefine(slot.Index)
            gem = grid_gem_map[slot.Index]
        end
    end
    local detail = GetDetailByItemData(itdata, {refine = refine, gem = gem})
    if detail then
        detail.selfEquiped = not slot.IsNull
    end
    return detail
end

local function RequestDetailByID(id, cb, errcb)
    Protocol.RequestHandler.ClientItemDataRequest(
        {c2s_id = id},
        function(rsp)
            local entityitem = rsp.s2c_data
            local detail = _M.GetDetailByTemplateID(entityitem.SnapData.TemplateID, entityitem.Properties.Properties)
            cb(detail)
        end,
        function()
            if errcb then
                errcb()
            end
        end
    )
end

local function GetDetailByEquipBagIndex(index, noGridRefine)
    local itdata = DataMgr.Instance.UserData.EquipBag:getItem(index)
    local refine, gem
    if not noGridRefine then
        refine = _M.GetGridRefine(index)
        gem = grid_gem_map[index]
    end
    local detail = GetDetailByItemData(itdata, {noGridRefine = noGridRefine, refine = refine, gem = gem})
    if detail then
        detail.selfEquiped = true
    end
    return detail
end

local function GetDetailByBagIndex(index)
    local itdata = DataMgr.Instance.UserData.Bag:getItem(index)
    return GetDetailByItemData(itdata, {noGridRefine = true})
end

local function GetBagItemDataByIndex(index)
    return DataMgr.Instance.UserData.Bag:getItem(index)
end

local function FindFirstBagIndexByTemplateID(templateID)
    local index = DataMgr.Instance.UserData.Bag:FindFirstTemplateItemIndex(templateID)
    if index >= 0 then
        return index
    end
end

local function GetDetailByTemplateID(templateID, dynamicAttrs, refine, gem)
    all_details[templateID] = all_details[templateID] or CreateTemplateDetail(templateID)
    if not all_details[templateID] then
        return nil
    end
    local ret = Helper.copy_table(all_details[templateID])
    ret.dynamicAttrs = ItemPropertyDataToAttrTable(dynamicAttrs) or ret.dynamicAttrs or {}
    ret.score = CalcAttributesScore(ret.dynamicAttrs)
    if refine then
        local static_refine = GlobalHooks.DB.FindFirst('EquipRefine', {refine_rank = refine.Rank, refine_lv = refine.Lv})
        for _, v in ipairs(ret.dynamicAttrs) do
            if v.Tag == 'FixedAttributeTag' then
                v.BaseValue = v.Value
                v.Value = math.floor(v.Value * (1 + static_refine.refine_plus / 10000))
            end
        end
        ret.refine = refine
    end
    if gem then
        local gemattrs = {}
        local slots = {}
        for i, v in ipairs(gem.Slots) do
            for _, vv in ipairs(v.Properties or {}) do
                table.insert(ret.dynamicAttrs, vv)
                table.insert(gemattrs, vv)
            end
            slots[v.SlotIndex] = v
        end
        ret.gem = {Slots = slots, Attrs = _M.MergerAttributeValue(gemattrs)}
    end
    return ret
end

local function GetAttributeColorRGB(attr)
    for i, v in ipairs(attr.SubAttributes or {}) do
        if v.Tag == ItemPropertyData.ColorRGBTag then
            return v.Value
        end
    end
    return Constants.Color.detail_extra_attr
end

local function GetAttributeString(v, refine_added)
    if not v then
        return
    end
    local attr
    if v.ID and v.ID ~= 0 then
        attr = unpack(GlobalHooks.DB.Find('Attribute', {id = v.ID}))
    else
        attr = unpack(GlobalHooks.DB.Find('Attribute', {key = v.Name}))
    end

    local value_str
    local target_value
    local append_value
    local append_value_str

    if v.ValueType == 2 or attr.client_showtype == 1 then
        if refine_added and v.BaseValue then
            target_value = (v.Value / 100)
            value_str = target_value .. '%'
            append_value = target_value - (v.BaseValue / 100)
            append_value_str = append_value_str .. '%'
        else
            local cur_v = v.BaseValue or v.Value
            target_value = (cur_v / 100)
            value_str = target_value .. '%'
        end
    else
        if refine_added and v.BaseValue then
            target_value = v.Value
            value_str = tostring(v.Value)
            append_value = target_value - v.BaseValue
            append_value_str = tostring(append_value)
        else
            local cur_v = v.BaseValue or v.Value
            target_value = cur_v
            value_str = tostring(target_value)
        end
    end
    if append_value then
        return Util.GetText(attr.name), append_value_str, append_value
    else
        return Util.GetText(attr.name), value_str, target_value
    end
end

local function GetEquipAttribute(detail, tag, id)
    local ret = {}
    for _, v in ipairs(detail.dynamicAttrs) do
        if v.Tag == tag then
            if not id then
                table.insert(ret, v)
            else
                local isId = type(id) == 'number'
                if isId and v.ID == id then
                    table.insert(ret, v)
                elseif v.Name and v.Name ~= '' and id == v.Name then
                    table.insert(ret, v)
                end
            end
        end
    end
    return ret
end

local function GetItemScore(bag, index)
    local itdata = bag:getItem(index)
    if itdata then
        local detail = GetDetailByItemData(itdata)
        return detail.score
    end
    return 0
end

local function RequestGridGemList(cb)
    Protocol.RequestHandler.ClientGridGemInfoRequest(
        {},
        function(ret)
            grid_gem_map = ret.s2c_data or {}
            if cb then
                cb(grid_gem_map)
            end
        end
    )
end

local function RequestIdentifyEquipPreview(id, cb)
    Protocol.RequestHandler.ClientIdentifyPreviewRequest(
        {c2s_equipID = id},
        function(ret)
            cb(ret.s2c_properties)
        end
    )
end

local function RequestIdentifyPreviewInfo(cb)
    Protocol.RequestHandler.ClientIdentifyPreviewInfoRequest(
        {},
        function(ret)
            cb(ret.s2c_data)
        end
    )
end

local function RequestSaveIdentifyPreview(id, cb)
    Protocol.RequestHandler.ClientSaveIdentifyRequest(
        {c2s_equipID = id},
        function(ret)
            cb()
        end
    )
end

local function RequestLockAttribute(id, attr, isLocked, cb, errcb)
    Protocol.RequestHandler.ClientLockEquipPropertyRequest(
        {
            c2s_equipID = id,
            c2s_isBuff = attr.Tag == ItemPropertyData.ExtraBuffTag,
            c2s_propertyIndex = attr.Index,
            c2s_lock = isLocked
        },
        function(ret)
            if isLocked then
                attr.SubAttributes = attr.SubAttributes or {}
                table.insert(attr.SubAttributes, {Tag = ItemPropertyData.IsLockedTag})
            elseif attr.SubAttributes then
                for i, v in ipairs(attr.SubAttributes) do
                    if v.Tag == ItemPropertyData.IsLockedTag then
                        table.remove(attr.SubAttributes, i)
                    end
                end
            end
            cb()
        end,
        function(ret)
            if errcb then
                errcb()
            end
        end
    )
end

local function RequestDecompose(selects, cb)
    Protocol.RequestHandler.ClientDecomposeItemRequest(
        {c2s_slots = selects},
        function(rp)
            if cb then
                cb()
            end
        end
    )
end

local function ReCalcDetailScore(detail, contains_refine, contains_gem)
    local attrs = {}
    for _, v in ipairs(detail.dynamicAttrs) do
        if v.Tag == 'GridGemAttributeTag' then
            if contains_gem then
                table.insert(attrs, v)
            end
        elseif v.Tag == 'FixedAttributeTag' then
            if contains_refine then
                table.insert(attrs, v)
            else
                local vv = Helper.copy_table(v)
                vv.Value = vv.BaseValue or vv.Value
                table.insert(attrs, vv)
            end
        else
            table.insert(attrs, v)
        end
    end
    detail.score = CalcAttributesScore(attrs)
end

local function CountItemByStaticData(static_item)
    if static_item.item_type == 0 then
        return DataMgr.Instance.UserData.VirtualBag:Count(static_item.id)
    else
        return DataMgr.Instance.UserData.Bag:Count(static_item.id)
    end
end

local function CountItemByTemplateID(id)
    local static_item = GlobalHooks.DB.Find('Item', id)
    return static_item and CountItemByStaticData(static_item) or 0
end

-- 返回cost列表 need需要个数， cur当前背包拥有数量, detail 模板详情
local function ParseCostAndCostGroup(cost_static)
    local cost = {}
    --消耗
    for i, v in ipairs(cost_static.cost.id or {}) do
        if v ~= 0 then
            local detail = GetDetailByTemplateID(v)
            local info = {
                need = cost_static.cost.num[i],
                cur = CountItemByStaticData(detail.static),
                detail = detail,
                id = v
            }
            table.insert(cost, info)
        end
    end
    for i, v in ipairs(cost_static.costgroup and cost_static.costgroup.id or {}) do
        if v ~= 0 then
            --随便找个符合条件的道具
            local groupItems = GlobalHooks.DB.Find('Item', {item_group = v})
            local info = {need = cost_static.costgroup.num[i], cur = 0}
            for _, vv in ipairs(groupItems) do
                info.cur = info.cur + CountItemByStaticData(vv)
            end
            local static_item = groupItems[1]
            info.detail = GetDetailByTemplateID(static_item.id)
            info.group = v
            info.groupItems = {}
            for _, v in ipairs(groupItems) do
                table.insert(info.groupItems, v.id)
            end
            table.insert(cost, info)
        end
    end
    return cost
end

-- canNotEmpty 表示内容为空时是否返回false
local function IsCostAndCostGroupEnough(costs, canNotEmpty)
    if canNotEmpty and not next(costs) then
        return false
    end
    for _, v in ipairs(costs) do
        if v.cur < v.need then
            return false
        end
    end
    return true
end

local function GetCostGroupEnoughCount(costs)
    local count = 99999
    for _, vv in ipairs(costs) do
        count = math.min(count, math.floor(vv.cur / vv.need))
    end
    return count
end

local function RecalcCostAndCostGroup(cost)
    if not cost.id and not cost.group then
        error('must costitemid or costgroupid')
    end
    if cost.id then
        cost.cur = CountItemByTemplateID(cost.id)
    else
        local cur = 0
        for _, vv in ipairs(cost.groupItems) do
            cur = cur + CountItemByTemplateID(vv)
        end
        cost.cur = cur
    end
end

local equip_listener = {}

local function RemoveEquipAttributeListener(id, cb)
    for i, v in ipairs(equip_listener[id] or {}) do
        if cb == v then
            table.remove(equip_listener[id], i)
            break
        end
    end
end

local function AddEquipAttributeListener(id, userdata, cb)
    RemoveEquipAttributeListener(id, cb)
    equip_listener[id] = equip_listener[id] or {}
    table.insert(equip_listener[id], {cb = cb, userdata = userdata})
end

local function OnBagAttritbueChange(eventname, params)
    local item = DataMgr.Instance.UserData.Bag:getItem(params.Index)
    if not item then
        return
    end
    local cbs = equip_listener[item.ID]
    for _, v in ipairs(cbs or {}) do
        v.cb(item.ID, v.userdata)
    end
end

local function OnEquipBagAttritbueChange(eventname, params)
    local item = DataMgr.Instance.UserData.Bag:getItem(params.Index)
    if not item then
        return
    end
    local cbs = equip_listener[item.ID]
    for _, v in ipairs(cbs or {}) do
        v.cb(v.userdata, item.ID)
    end
end

local function RequestComposeItem(itemid, count, cb)
    Protocol.RequestHandler.ClientComposeItemRequest(
        {c2s_itemID = itemid, c2s_count = count},
        function(rp)
            if cb then
                cb()
            end
        end
    )
end

local function UseItem(index, count, detail)
    if not detail then
        detail = GetDetailByBagIndex(index)
    end
    DataMgr.Instance.UserData.Bag:Use(
        index,
        count,
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

local function LoadRoleSnapExtSnap(keys, cb)
    local request = {c2s_rolesID = keys}
    Protocol.RequestHandler.GetRoleSnapExtRequest(
        request,
        function(rsp)
            cb(rsp.s2c_data)
        end
    )
end

local item_listener = {
    main_type = {},
    second_type = {},
    id = {},
    cost = {},
    cost_many = {}
}

local equip_listener = {
    equip_pos = {},
    all = {}
}
local function CreateDipose(map, fn)
    return function()
        map[fn] = nil
    end
end

local function ListenByItemType(type, fn)
    local ret = {Dispose = CreateDipose(item_listener.main_type, fn)}
    item_listener.main_type[fn] = type
    return ret
end

local function ListenByItemSecondType(maintype, sectype, fn)
    local ret = {Dispose = CreateDipose(item_listener.second_type, fn)}
    item_listener.second_type[fn] = {main = maintype, second = sectype}
    return ret
end

local function ListenByTemplateID(templateID, fn)
    local ret = {Dispose = CreateDipose(item_listener.id, fn)}
    item_listener.id[fn] = templateID
    return ret
end

-- local function ListenByTemplateID(templateID, fn)
--     local detail = GetDetailByTemplateID(templateID)
--     local listener
--     if detail.static.item_type == 0 then
--         listener = ItemListener(DataMgr.Instance.UserData.VirtualBag, true, 0)
--     else
--         listener = ItemListener(DataMgr.Instance.UserData.Bag, true, 0)
--     end
--     listener.OnMatch = function(itdata)
--         return itdata.TemplateID == templateID
--     end
--     listener:Start(false, false)
--     listener.OnItemUpdateAction = fn
--     return listener
-- end

local function ListenCost(cost, fn)
    local ret = {Dispose = CreateDipose(item_listener.cost, fn)}
    item_listener.cost[fn] = cost
    return ret
end

local function ListenManyCost(costs, fn)
    local ret = {Dispose = CreateDipose(item_listener.cost_many, fn)}
    item_listener.cost_many[fn] = costs
    return ret
end

local function ListenCostXlsLine(static_info, fn)
    local costs = ParseCostAndCostGroup(static_info)
    return ListenManyCost(costs, fn)
end

local function ListenEquipByPos(pos, fn)
    local ret = {Dispose = CreateDipose(equip_listener.equip_pos, fn)}
    equip_listener.equip_pos[fn] = pos
    return ret
end

local function ListenEquip(fn)
    local ret = {Dispose = CreateDipose(equip_listener.all, fn)}
    equip_listener.all[fn] = true
    return ret
end

local map_items = {}

local function OnCountUpdate(ename, params)
    --params.Virtual 代表是虚道具 params.Count < 0 代表为数量减少
    local currentTemplateID = params.TemplateID
    for fn, templateID in pairs(item_listener.id) do
        if templateID == currentTemplateID then
            fn()
        end
    end

    map_items[currentTemplateID] = map_items[currentTemplateID] or GlobalHooks.DB.Find('Item', currentTemplateID)
    local item_static = map_items[currentTemplateID]
    for fn, main_type in pairs(item_listener.main_type) do
        if item_static.item_type == main_type then
            fn(params)
        end
    end

    for fn, t in pairs(item_listener.second_type) do
        if (not t.main or item_static.item_type == t.main) and item_static.sec_type == t.second then
            fn(params)
        end
    end

    for fn, costinfo in pairs(item_listener.cost) do
        if costinfo.id == currentTemplateID or table.ContainsValue(costinfo.groupItems or {}, currentTemplateID) then
            fn(params)
        end
    end
    for fn, costs in pairs(item_listener.cost_many) do
        local needinvoke = false
        for _, costinfo in ipairs(costs) do
            if costinfo.id == currentTemplateID or table.ContainsValue(costinfo.groupItems or {}, currentTemplateID) then
                -- print_r('costs ', currentTemplateID, costs)
                needinvoke = true
            end
        end
        if needinvoke then
            fn(params)
        end
    end
end

local function OnEquipCountUpdate(ename, params)
    for fn, equip_pos in pairs(equip_listener.equip_pos) do
        if equip_pos == params.EquipPos then
            fn(equip_pos, params.TemplateID)
        end
    end
    for fn, _ in pairs(equip_listener.all) do
        fn(params.EquipPos, params.TemplateID)
    end
end

local grid_listener = {}

local function UpdateRefineListener(equip_pos, record_lv)
    local d = grid_refine_map[equip_pos]
    local listener = grid_listener[equip_pos]
    if listener then
        listener:Dispose()
    end
    if not record_lv then
        local info = GlobalHooks.DB.FindFirst('EquipRefine', {refine_rank = d.Rank, refine_lv = d.Lv})
        if not info then
            return
        end
        record_lv = info.record_lv
    end
    local nextinfo = GlobalHooks.DB.FindFirst('EquipRefine', {record_lv = record_lv + 1})
    -- print_r('####################################info ',equip_pos, nextinfo)
    if nextinfo then
        local costs = ParseCostAndCostGroup(nextinfo)
        GlobalHooks.UI.SetRedTips('strengthen', IsCostAndCostGroupEnough(costs) and 1 or 0, equip_pos)
        grid_listener[equip_pos] =
            ListenManyCost(
            costs,
            function()
                for _, v in ipairs(costs) do
                    RecalcCostAndCostGroup(v)
                end
                local enough = IsCostAndCostGroupEnough(costs) and 1 or 0
                GlobalHooks.UI.SetRedTips('strengthen', IsCostAndCostGroupEnough(costs) and 1 or 0, equip_pos)
            end
        )
    else
        GlobalHooks.UI.SetRedTips('strengthen', 0, equip_pos)
    end
end

local function RequestGridRefine(equip_pos, cb)
    Protocol.RequestHandler.ClientGridRefineRequest(
        {c2s_equipPos = equip_pos},
        function(ret)
            grid_refine_map[equip_pos] = ret.s2c_data
            UpdateRefineListener(equip_pos)
            if cb then
                cb(ret.s2c_data)
            end
        end
    )
end

--缓存
local function RequestGridRefineList(cb)
    Protocol.RequestHandler.ClientGridRefineInfoRequest(
        {},
        function(ret)
            grid_refine_map = ret.s2c_data or {}
            if cb then
                cb(grid_refine_map)
            end
        end
    )
end

local gem_holes = {}
local gem_static_cache = {}
local compose_filtermap = {}
local function CalcGirdGemRedTips(equip_pos)
    local gridGem = grid_gem_map[equip_pos]
    local can_into = 0
    for _, v in ipairs(gridGem.Slots or {}) do
        local lv = gem_holes[v.SlotIndex].player_lv
        if v.GemTemplateID == 0 and DataMgr.Instance.UserData.Level >= lv then
            can_into = 1
        elseif v.GemTemplateID > 0 then
            local compinfo = GlobalHooks.DB.FindFirst('item_synthetic', {item_id = v.GemTemplateID})
            if compinfo then
                local nextGemInfo = GlobalHooks.DB.FindFirst('equip_gem', {item_id = compinfo.target_id})
                if nextGemInfo.gem_lv <= DataMgr.Instance.UserData.Level then
                    local costs = ParseCostAndCostGroup(compinfo)
                    for _, v in ipairs(costs) do
                        if v.id == v.GemTemplateID then
                            v.cur = v.cur + 1
                            break
                        end
                    end
                    local enoughcount = GetCostGroupEnoughCount(costs)
                    if enoughcount > 0 then
                        can_into = 1
                    end
                end
            end
        end
    end
    local gemStatic = gem_static_cache[equip_pos]
    if not gemStatic then
        gemStatic = GlobalHooks.DB.Find('equip_gem', {equip_pos = equip_pos})
        gem_static_cache[equip_pos] = gemStatic
    end
    local enough
    for _, v in ipairs(gemStatic) do
        if CountItemByTemplateID(v.item_id) > 0 then
            enough = true
            break
        end
    end
    if not enough then
        can_into = 0
    end
    GlobalHooks.UI.SetRedTips('gemstone', can_into, equip_pos)
end

local gem_alldata
local function CalcGemCompose(params)
    if not gem_alldata then
        gem_alldata = {}
        local all = GlobalHooks.DB.Find('item_synthetic', {})
        for _, v in ipairs(all) do
            local detail = GetDetailByTemplateID(v.target_id)
            local is_show = compose_filtermap[detail.static.item_type][detail.static.sec_type]
            if is_show then
                gem_alldata[v.target_id] = ParseCostAndCostGroup(v)
            end
        end
    end
    for k, costs in pairs(gem_alldata) do
        for _, v in ipairs(costs) do
            RecalcCostAndCostGroup(v)
        end
        local enough = IsCostAndCostGroupEnough(costs)
        GlobalHooks.UI.SetRedTips('compose', enough and 1 or 0, k)
    end
end

local function CalcGemRedTips(params)
    local equips = DataMgr.Instance.UserData.EquipBag.AllSlots
    local t = CSharpArray2Table(equips)
    -- pprint('grid_gem_map', grid_gem_map)
    for k, v in ipairs(t) do
        local index = v.Index
        if Constants.EquipPartName[index] then
            CalcGirdGemRedTips(index)
        end
    end
end

local function RequestEquipGridGem(equipPos, bagIndex, slotIndex, cb)
    Protocol.RequestHandler.ClientGridEquipGemRequest(
        {c2s_equipPos = equipPos, c2s_gemBagIndex = bagIndex, c2s_gemSlotIndex = slotIndex},
        function(ret)
            SetGridGem(equipPos, slotIndex, ret.s2c_slotData)
            CalcGemRedTips()
            if cb then
                cb(ret.s2c_slotData)
            end
        end
    )
end

local function RequestUnEquipGridGem(equipPos, slotIndex, cb)
    Protocol.RequestHandler.ClientGridUnEquipGemRequest(
        {c2s_equipPos = equipPos, c2s_gemSlotIndex = slotIndex},
        function(ret)
            local gem = GetGridGem(equipPos, slotIndex)
            gem.GemTemplateID = 0
            gem.Properties = nil
            CalcGemRedTips()
            cb()
        end
    )
end

local gem_listeners = {}

local function OnBagReady()
    local holes = GlobalHooks.DB.Find('equip_gem_hole', {})
    for _, v in ipairs(holes) do
        gem_holes[v.hole_order] = v
    end
    RequestGridRefineList(
        function()
            for k, v in pairs(Constants.EquipPartName) do
                UpdateRefineListener(k, not grid_refine_map[k] and 0)
            end
        end
    )
    RequestGridGemList(
        function()
            CalcGemRedTips()
        end
    )

    local compose_ctl = GlobalHooks.DB.Find('item/item_synthetic.xlsx/item_synthetic_redpoint', {})
    for _, v in ipairs(compose_ctl) do
        compose_filtermap[v.item_type] = compose_filtermap[v.item_type] or {}
        compose_filtermap[v.item_type][v.sec_type] = v.is_show == 1 and v.is_redpoint == 1
        gem_listeners[#gem_listeners + 1] = ListenByItemSecondType(v.item_type, v.sec_type, function()
            if v.is_show == 1 and v.is_redpoint == 1 then
                CalcGemCompose()
            end
            CalcGemRedTips()
        end)

    end
    CalcGemCompose()
end

local function initial()
    -- 处理数据
    EventManager.Subscribe('Event.Item.CountUpdate', OnCountUpdate)
    EventManager.Subscribe('Event.Equip.CountUpdate', OnEquipCountUpdate)
    EventManager.Subscribe('Bag.UpdateAttribute', OnBagAttritbueChange)
    EventManager.Subscribe('EquipBag.UpdateAttribute', OnEquipBagAttritbueChange)
end

local function InitNetWork(initNotify)
end

local function fin()
    EventManager.Unsubscribe('Event.Item.CountUpdate', OnCountUpdate)
    EventManager.Unsubscribe('Event.Equip.CountUpdate', OnEquipCountUpdate)
    EventManager.Unsubscribe('Bag.UpdateAttribute', OnBagAttritbueChange)
    EventManager.Unsubscribe('EquipBag.UpdateAttribute', OnEquipBagAttritbueChange)
    for _, v in pairs(grid_listener) do
        v:Dispose()
    end
    for _, v in pairs(gem_listeners) do
        v:Dispose()
    end
end

_M.OnBagReady = OnBagReady
_M.initial = initial
_M.fin = fin
_M.UseItem = UseItem
_M.ListenEquip = ListenEquip
_M.ListenEquipByPos = ListenEquipByPos
_M.ListenByTemplateID = ListenByTemplateID
_M.ListenCost = ListenCost
_M.ListenCostXlsLine = ListenCostXlsLine
_M.ListenByItemType = ListenByItemType
_M.ListenByItemSecondType = ListenByItemSecondType
_M.ListenManyCost = ListenManyCost
_M.GetGridGem = GetGridGem
_M.IsExistGem = IsExistGem
_M.GetMaxWholeRefineStaticInfo = GetMaxWholeRefineStaticInfo
_M.GetGridGemAttribute = GetGridGemAttribute
_M.MergerAttributeValue = MergerAttributeValue
_M.DiffAttributesWith = DiffAttributesWith
_M.IsGemSlotLocked = IsGemSlotLocked
_M.ParseCostAndCostGroup = ParseCostAndCostGroup
_M.RecalcCostAndCostGroup = RecalcCostAndCostGroup
_M.GetBagItemDataByIndex = GetBagItemDataByIndex
_M.GetDetailByEquipBagIndex = GetDetailByEquipBagIndex
_M.GetDetailByBagIndex = GetDetailByBagIndex
_M.GetDetailByItemData = GetDetailByItemData
_M.GetEquipAttribute = GetEquipAttribute
_M.GetDetailByTemplateID = GetDetailByTemplateID
_M.GetAttributeString = GetAttributeString
_M.GetItemScore = GetItemScore
_M.GetGridRefine = GetGridRefine
_M.GetGridRefineList = GetGridRefineList
_M.GetXlsFixedAttribute = GetXlsFixedAttribute
_M.IsCostAndCostGroupEnough = IsCostAndCostGroupEnough
_M.CalcAttributesScore = CalcAttributesScore
_M.ReCalcDetailScore = ReCalcDetailScore
_M.CountItemByStaticData = CountItemByStaticData
_M.CountItemByTemplateID = CountItemByTemplateID
_M.GetFirstEmptyGemSlot = GetFirstEmptyGemSlot
_M.RequestGridRefine = RequestGridRefine
_M.RequestGridRefineList = RequestGridRefineList
_M.RequestIdentifyEquipPreview = RequestIdentifyEquipPreview
_M.RequestIdentifyPreviewInfo = RequestIdentifyPreviewInfo
_M.RequestSaveIdentifyPreview = RequestSaveIdentifyPreview
_M.RequestLockAttribute = RequestLockAttribute
_M.RequestEquipGridGem = RequestEquipGridGem
_M.RequestUnEquipGridGem = RequestUnEquipGridGem
_M.RequestDecompose = RequestDecompose
_M.GetAttributeID = GetAttributeID
_M.IsAttritbuteLocked = IsAttritbuteLocked
_M.SetAttributeLock = SetAttributeLock
_M.FindItemData = FindItemData
_M.GetDetailByID = GetDetailByID
_M.AddEquipAttributeListener = AddEquipAttributeListener
_M.RemoveEquipAttributeListener = RemoveEquipAttributeListener
_M.InitNetWork = InitNetWork
_M.RequestComposeItem = RequestComposeItem
_M.RequestDetailByID = RequestDetailByID
_M.GetAttributeColorRGB = GetAttributeColorRGB
_M.GetCostGroupEnoughCount = GetCostGroupEnoughCount
_M.FindFirstBagIndexByTemplateID = FindFirstBagIndexByTemplateID
_M.SnapExtReader = SnapExtReader.Create(LoadRoleSnapExtSnap)
return _M
