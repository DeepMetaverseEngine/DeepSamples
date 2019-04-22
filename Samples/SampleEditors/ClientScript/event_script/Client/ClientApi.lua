--! @addtogroup Client
--! @{
local Api = {}
Api.Task = {}
Api.Listen = {}
Api.UI = {Task = {}, Listen = {}}
Api.Scene = {Task = {}, Listen = {}}
local BaseUI = require 'UI/BaseUI'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local ItemModel = require 'Model/ItemModel'
local QuestUtil = require 'UI/Quest/QuestUtil'
local QuestModel = require 'Model/QuestModel'
local QuestNpcDataModel = require 'Model/QuestNpcDataModel'
local event_uis = {}

local keyPath = _total_global.path .. 'event_script/ScriptKey'
package.loaded[keyPath] = nil
local key_scriptMap = require(keyPath)

local function GetUINode(compID)
    local node = EventApi.GetCacheData(compID)
    if event_uis[compID] then
        if type(node.obj) == 'table' then
            return node.obj.root, compID, node.custom
        else
            return node.obj, compID, node.custom
        end
    end
    assert(node ~= nil and node.type == 'ui', tostring(compID))
    local root = EventApi.GetCacheData(node.root)
    return node.obj, node.root, root.custom
end

local function SaveUINode(comp, root)
    local id = EventApi.AddCacheData({root = root, obj = comp, type = 'ui'})
    table.insert(event_uis[root], id)
    return id
end

local function ExistUINode(compID)
    local node = EventApi.GetCacheData(compID)
    if event_uis[compID] then
        return true
    end
    return node ~= nil and node.type == 'ui'
end
local function SaveGameObject(obj)
    return EventApi.AddCacheData({obj = obj, type = 'gameObject'})
end

local function GetGameObject(id)
    local node = EventApi.GetCacheData(id)
    assert(node ~= nil and node.type == 'gameObject' and UnityHelper.IsObjectExist(node.obj))
    return node.obj
end

local function ExistsGameObject(id)
    local node = EventApi.GetCacheData(id)
    return node ~= nil and node.type == 'gameObject' and UnityHelper.IsObjectExist(node.obj)
end

local function SaveTransfom(obj)
    return EventApi.AddCacheData({obj = obj, type = 'transfom'})
end

local function GetTransfom(id)
    local node = EventApi.GetCacheData(id)
    assert(node ~= nil and node.type == 'transfom')
    return UnityHelper.IsObjectExist(node.obj) and node.obj or nil
end

local function Vector3ToTableV3(p)
    return {x = p.x, y = p.y, z = p.z}
end

local function Vector2ToTableV2(p)
    return {x = p.x, y = p.y}
end

local function TableV2ToVector2(p)
    return Vector2(p.x or 0, p.y or 0)
end

local function TableV3ToVector3(p)
    return Vector3(p.x or 0, p.y or 0, p.z or 0)
end

local function Vector3MergerTableV3(v3, tv3)
    if tv3.x then
        v3.x = tv3.x
    end
    if tv3.y then
        v3.y = tv3.y
    end
    if tv3.z then
        v3.z = tv3.z
    end
    return v3
end

local function OnMenuExit(id)
    local comps = event_uis[id]
    if comps and EventApi then
        EventApi.RemoveCacheData(id)
        for _, v in ipairs(comps) do
            EventApi.RemoveCacheData(v)
        end
    end
    event_uis[id] = nil
end

local function SaveUI(ui, custom)
    local id
    if type(ui) == 'table' then
        ui:SubscribOnExit(
            'event.script',
            function()
                OnMenuExit(id)
            end
        )
    else
        ui.Parent.event_ChildRemoved = function(parent, sender)
            if sender == ui then
                parent.event_ChildRemoved = nil
                OnMenuExit(id)
            end
        end
    end
    local info = {obj = ui, type = 'ui_root', custom = custom}
    id = EventApi.AddCacheData(info)
    info.root = id
    event_uis[id] = {}
    return id
end

local function GetUI(id, noassert)
    if not noassert then
        assert(event_uis[id])
    end
    local data = EventApi.GetCacheData(id)
    return data and data.obj
end

function Api.UI.OpenByTag(uitag)
    GlobalHooks.UI.OpenUI(uitag)
end

function Api.UI.CloseByTag(uitag)
    GlobalHooks.UI.CloseUIByTag(uitag)
end

function Api.UI.FindByTag(uitag)
    local ret = GlobalHooks.UI.FindUI(uitag)
    if ret then
        return SaveUI(ret)
    end
end

function Api.UI.FindByXml(xml)
    local ret = MenuMgr.Instance:FindMenuByXml(xml)
    if ret then
        if ret.LuaTable then
            return SaveUI(ret.LuaTable)
        else
            return SaveUI(ret)
        end
    else
        return EventApi.UI.FindHud(xml)
    end
end

function Api.UI.FindHud(xmlname)
    local hud = HudManager.Instance:FindByXmlName(xmlname)
    if hud then
        return SaveUI(hud)
    end
end

function Api.UI.Exists(id)
    if type(id) == 'number' then
        return GetUI(id, true) ~= nil
    else
        local ret = GlobalHooks.UI.FindUI(id)
        return ret ~= nil
    end
end

--! @brief 以xml的方式打开一个UI
--! @param xml xml路径
--! @param showType UIShowType的枚举: Cover,HideBackMenu,HideBackScene,HideBackHud,HideHudAndMenu (默认为HideHudAndMenu)...
--! @param otherparams 其他参数， {Layer = 'MessageBox','Drama'：是否添加到MessageBox或者Drama层，AnimeType:动画类型枚举（UIAnimeType）}
function Api.UI.Open(xml, showType, otherparams)
    -- local cache = MenuMgr.Instance:GetCacheUIByTag(xml)
    local cache = false
    if type(showType) == 'table' then
        otherparams = showType
        showType = nil
    end
    local ui
    local layer = otherparams and otherparams.Layer
    local animType = otherparams and otherparams.AnimeType
    local enableframe = true
    if otherparams and otherparams.EnableFrame ~= nil then
        enableframe = otherparams.EnableFrame
    end
    if not cache then
        ui = BaseUI.Create(xml, xml)
        ui.menu.LuaTable = ui
        ui.menu.CacheLevel = -1
    else
        ui = cache.LuaTable
    end

    if showType then
        ui.menu.ShowType = showType
    end
    if animType then
        ui.menu:SetCompAnime(ui.menu, animType)
    end
    if not enableframe then
        ui:EnableTouchFrame(false)
    end
    -- if not showType and layer == 'Hud' then
    --     showType = UIShowType.Cover
    -- end
    if layer == 'MessageBox' then
        MenuMgr.Instance:AddMsgBox(ui.menu)
    elseif layer == 'Drama' then
        DramaUIManage.Instance:AddMenu(ui.menu)
    elseif layer == 'Hud' then
        MenuMgr.Instance:AddHudMenu(ui.menu)
    else
        MenuMgr.Instance:AddMenu(ui.menu)
    end
    if ui.comps.btn_close then
        ui.comps.btn_close.TouchClick = function()
            ui:Close()
        end
    end
    return SaveUI(ui, true)
end

function Api.UI.Close(id)
    local ui = GetUI(id, true)
    if ui then
        ui:Close()
    end
end

function Api.UI.SetFrameEnable(id, var)
    local ui = GetUI(id)
    ui:EnableTouchFrame(var)
end

function Api.UI.EnableTouchFrameClose(id, var)
    local ui = GetUI(id)
    ui:EnableTouchFrameClose(var)
end

function Api.UI.SetRootVisible(id, var)
    local ui = GetUI(id)
    ui.root.Visible = var
end

function Api.UI.AddSub(parent, xml)
    local parentUI = GetUI(parent)
    if parentUI then
        local ui = BaseUI.Create(xml, xml)
        ui.menu.CacheLevel = -1
        ui.menu.ShowType = UIShowType.Cover
        parentUI:AddSubUI(ui)
        return SaveUI(ui, true)
    end
end

--! @brief 使用句柄监听时，当MenuExit发生，会释放UI模块持有的句柄，所以只允许监听一次
function Api.UI.Listen.MenuExit(ui, fn)
    if type(ui) == 'string' then
        local eid
        local function main()
            local function OnMenuEnter()
            end
            local function OnMenuExit()
                EventApi.InvokeListenCallBack(eid, fn)
            end
            MenuMgr.Instance:AttachLuaObserver(ui, 'UI.Listen.MenuExit', {OnMenuEnter = OnMenuEnter, OnMenuExit = OnMenuExit})
            EventApi.Task.WaitAlways()
        end
        local function clean()
            MenuMgr.Instance:DetachLuaObserver(ui, 'UI.Listen.MenuExit')
        end
        eid = EventApi.Task.AddEvent({main = main, clean = clean})
        return eid
    else
        local menu = GetUI(ui)
        local eid
        local function main()
            menu:SubscribOnExit(
                'UI.Listen.MenuExit',
                function()
                    EventApi.InvokeListenCallBack(eid, fn)
                end
            )
            EventApi.Task.WaitAlways()
        end

        local function clean()
            menu:UnSubscribOnExit('UI.Listen.MenuExit')
        end
        eid = EventApi.Task.AddEvent({main = main, clean = clean})
        return eid
    end
end

function Api.UI.Listen.MenuEnter(uitag, fn)
    local eid
    local function main()
        local function OnMenuEnter()
            local ui = EventApi.UI.FindByTag(uitag)
            EventApi.SetEventOutput(eid, ui)
            EventApi.InvokeListenCallBack(eid, fn, ui)
        end
        local function OnMenuExit()
        end
        MenuMgr.Instance:AttachLuaObserver(uitag, 'UI.Listen.MenuEnter', {OnMenuEnter = OnMenuEnter, OnMenuExit = OnMenuExit})
        EventApi.Task.WaitAlways()
    end
    local function clean()
        MenuMgr.Instance:DetachLuaObserver(uitag, 'UI.Listen.MenuEnter')
    end
    eid = EventApi.Task.AddEvent({main = main, clean = clean})
    return eid
end

local function FindChildByString(data, key, recursive)
    local comp
    if type(data.obj) == 'table' then
        comp = data.obj.comps[key]
    else
        comp = UIUtil.FindChild(data.obj, key, recursive)
    end
    if not comp then
        return
    end
    return SaveUINode(comp, data.root)
end

local function FindChildByField(data, key, recursive)
    local function check(child, k, v)
        return child[k] == v
    end

    local function Predicate(child)
        for k, v in pairs(iter) do
            local state, ret = pcall(check, child, k, v)
            if not state or not ret then
                return false
            end
        end
        return true
    end
    local parent = type(data.obj) == 'table' and data.obj.menu or data.obj
    local comp = MenuBase.FindComponentAs(parent, Predicate, recursive)
    if comp then
        return SaveUINode(comp, data.root)
    end
end

local function FindChildByFunction(data, key, recursive)
    local function Predicate(child)
        return key(SaveUINode(child, data.root))
    end
    local parent = type(data.obj) == 'table' and data.obj.menu or data.obj
    local comp = MenuBase.FindComponentAs(parent, Predicate, recursive)
    if comp then
        return SaveUINode(comp, data.root)
    end
end
-- Api.UI.FinChild(ui,'lb_name')
-- Api.UI.FinChild(cvs,{Text = 'yyyyyhhh'})
-- Api.UI.FinChild(cvs,function(child) return Api.UI.GetText(child) == 'yyyyyhhh' end)
function Api.UI.FindChild(parent, key, recursive)
    if recursive == nil then
        recursive = true
    end
    local data = EventApi.GetCacheData(parent)
    if not data then
        return nil
    end
    local t = type(key)
    if t == 'string' then
        return FindChildByString(data, key, recursive)
    elseif t == 'table' then
        return FindChildByField(data, key, recursive)
    elseif t == 'function' then
        return FindChildByFunction(data, key, recursive)
    end
end

function Api.UI.DoPointerClick(compID)
    local comp = GetUINode(compID)
    comp.Selectable.OnPointerClick(comp.Selectable.LastPointerDown)
end

function Api.UI.GetParent(compID)
    local comp, root = GetUINode(compID)
    return SaveUINode(comp.Parent, root)
end

function Api.UI.SetNodeTag(compID, tag)
    GetUINode(compID).UserTag = tag
end

function Api.UI.GetNodeTag(compID)
    return GetUINode(compID).UserTag
end
function Api.UI.SetName(compID, name)
    local comp = GetUINode(compID)
    comp.Name = name
end

function Api.UI.SetScreenAnchor(id, anchor)
    local comp = GetUINode(id)
    local bit_anchor = 0
    local x_anchor, y_anchor = unpack(string.split(anchor, '_'))
    if x_anchor == 'l' then
        bit_anchor = HudManager.HUD_LEFT
    elseif x_anchor == 'c' then
        bit_anchor = HudManager.HUD_XCENTER
    elseif x_anchor == 'r' then
        bit_anchor = HudManager.HUD_RIGHT
    end
    if bit_anchor == 0 and string.IsNullOrEmpty(y_anchor) then
        y_anchor = x_anchor
    end
    if y_anchor == 't' then
        bit_anchor = bit.bor(bit_anchor, HudManager.HUD_TOP)
    elseif y_anchor == 'c' then
        bit_anchor = bit.bor(bit_anchor, HudManager.HUD_YCENTER)
    elseif y_anchor == 'b' then
        bit_anchor = bit.bor(bit_anchor, HudManager.HUD_BOTTOM)
    end
    HudManager.Instance:InitAnchorWithNode(comp, bit_anchor)
end

function Api.UI.SetSiblingIndex(compID, index)
    local comp = GetUINode(compID)
    comp:SetParentIndex(index)
end

function Api.UI.SetScrollList(compID, tempnodeID, count, cb)
    local pan = GetUINode(compID)
    local tempnode, root = GetUINode(tempnodeID)
    local configfn = pan.Scrollable.Scroll.vertical and UIUtil.ConfigVScrollPan or UIUtil.ConfigHScrollPan
    configfn(
        pan,
        tempnode,
        count,
        function(node, index)
            local curid = SaveUINode(node, root)
            cb(curid, index)
        end
    )
end

function Api.UI.SetVScrollEnable(compID,val)
    GetUINode(compID).Scrollable.Scroll.vertical = val
end

function Api.UI.SetHScrollEnable(compID,val)
    GetUINode(compID).Scrollable.Scroll.horizontal = val
end

function Api.UI.SetScrollGrid(compID, tempnodeID, col, count, cb)
    local pan = GetUINode(compID)
    local tempnode, root = GetUINode(tempnodeID)
    local configfn = pan.Scrollable.Scroll.vertical and UIUtil.ConfigGridVScrollPan or UIUtil.ConfigGridHScrollPan
    configfn(
        pan,
        tempnode,
        col,
        count,
        function(node, index)
            local curid = SaveUINode(node, root)
            cb(curid, index)
        end
    )
end

function Api.UI.GetScrollListCell(compID, cellIndex)
    local pan, root = GetUINode(compID)
    local gx = (cellIndex - 1) % pan.Columns
    local gy = math.floor((cellIndex - 1) / pan.Columns)
    local node = pan.Scrollable:GetCell(gx, gy)
    if node then
        return SaveUINode(node, root)
    end
end

function Api.UI.Task.MoveToScrollListCell(compID, cellIndex)
    local pan = GetUINode(compID)
    UIUtil.MoveToScrollCell(pan, cellIndex)
    return EventApi.Task.DelaySec(0.1)
end

function Api.UI.Task.CarriageBackHud()
    local ui = EventApi.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
    local cvs_carriage = EventApi.UI.FindChild(ui, 'cvs_carriage')
    local function main()
        local btn_back = EventApi.UI.FindChild(ui, 'btn_back')
        local btn_follow = EventApi.UI.FindChild(ui, 'btn_follow')
        EventApi.UI.SetVisible(btn_follow, false)
        EventApi.UI.SetVisible(btn_back, true)
        EventApi.UI.SetVisible(cvs_carriage, true)
        EventApi.UI.Listen.TouchClick(
            btn_back,
            function()
                Api.QuickTransport(100040)
            end
        )
        EventApi.UI.SetQuestHudVisible(false)
        EventApi.Task.Wait(EventApi.Listen.ActorLeaveMap())
    end

    local function clean()
        EventApi.UI.SetVisible(cvs_carriage, false)
    end
    return EventApi.Task.AddEvent({main = main, clean = clean})
end

function Api.UI.SetImage(compID, ...)
    local comp = GetUINode(compID)
    UIUtil.SetImage(comp, ...)
end

function Api.UI.SetItemShow(compID, templateID, count, showdetail, detailparams)
    local comp = GetUINode(compID)
    local itshow = UIUtil.SetItemShowTo(comp, templateID, count)
    if showdetail then
        itshow.EnableTouch = true
        itshow.TouchClick = function()
            local detail = ItemModel.GetDetailByTemplateID(templateID)
            detailparams = detailparams or {}
            local params = {detail = detail, itemShow = itshow, autoHeight = true, x = detailparams.x, y = detailparams.y, anchor = detailparams.anchor}
            UIUtil.ShowNormalItemDetail(params)
        end
    end
end

function Api.UI.SetEnoughItemShowAndLabel(compID, labelID, cost, tipsparams)
    local itshow, root = GetUINode(compID)
    local lblable = GetUINode(labelID)
    UIUtil.SetEnoughItemShowAndLabel({ui = GetUI(root)}, itshow, lblable, cost, tipsparams)
end

function Api.UI.SetEnoughLabel(labelID, cur, need, notshowcur)
    local comp = GetUINode(compID)
    UIUtil.SetEnoughLabel({}, comp, cur, need, notshowcur)
end

function Api.UI.Task.AlphaTo(compID, to, duration)
    local comp = GetUINode(compID)
    local d = to - comp.Alpha
    --comp.Alpha = to
    local once = 0.03
    local duration = duration or 1
    local speed = d / (duration / once)
    return EventApi.Listen.AddPeriodicSec(
        once,
        duration,
        function()
            local nextAlpha = comp.Alpha + speed
            if speed > 0 then
                comp.Alpha = math.min(to, nextAlpha)
            end
            if speed < 0 then
                comp.Alpha = math.max(to, nextAlpha)
            end
        end
    )
end

function Api.UI.Task.MoveTo(compID, duration, x, y)
    local comp = GetUINode(compID)
    local dx = x - comp.X
    local dy = y - comp.Y
    local once = 0.03
    local speedx = dx / (duration / once)
    local speedy = dy / (duration / once)
    return EventApi.Task.AddEvent(
        function()
            EventApi.Listen.AddPeriodicSec(
                once,
                function()
                    comp.X = comp.X + speedx
                    comp.Y = comp.Y + speedy
                end
            )
            EventApi.Task.Sleep(duration)
            comp.X = x
            comp.Y = y
        end
    )
end

function Api.UI.SetPositionX(compID, x)
    local node = GetUINode(compID)
    node.X = x
end

function Api.UI.SetPositionY(compID, y)
    local node = GetUINode(compID)
    node.Y = y
end

function Api.UI.SetPosition(compID, x, y)
    local node = GetUINode(compID)
    node.Position2D = Vector2(x, y)
end

function Api.UI.SetPositionZ(compID, z)
    local node = GetUINode(compID)
    node.Transform.localPosition = Vector3(node.X, -node.Y, z)
end

function Api.UI.GetPositionX(compID)
    local node = GetUINode(compID)
    return node.X
end

function Api.UI.GetPositionY(compID)
    local node = GetUINode(compID)
    return node.Y
end

function Api.UI.GetPosition(compID)
    local node = GetUINode(compID)
    local p = node.Position2D
    return p.x, p.y
end

function Api.UI.SetText(compID, text)
    GetUINode(compID).Text = text
end

function Api.UI.SetXmlText(compID, text)
    GetUINode(compID).XmlText = '<f>' .. text .. '</f>'
end

function Api.UI.GetTextPreferredSize(compID)
    local comp = GetUINode(compID)
    local t = comp:GetType().Name
    if t == 'HZLabel' then
        return comp.PreferredSize.x, comp.PreferredSize.y
    elseif t == 'HZTextBox' or t == 'HZTextBoxHtml' then
        return comp.TextComponent.PreferredSize.x, comp.TextComponent.PreferredSize.y
    end
end
function Api.UI.SetSize(compID, w, h)
    local node = GetUINode(compID)
    node.Size2D = Vector2(w, h)
end

function Api.UI.SetWidth(compID, w)
    local node = GetUINode(compID)
    node.Width = w
end

function Api.UI.SetHeight(compID, w)
    local node = GetUINode(compID)
    node.Height = w
end

function Api.UI.SetTextAnchor(compID, anchor)
    local comp = GetUINode(compID)
    local ctype = comp:GetType().Name
    if ctype == 'HZLabel' then
        comp.EditTextAnchor = anchor
    elseif ctype == 'HZTextBox' then
        comp.TextComponent.Anchor = anchor
    elseif ctype == 'HZTextBoxHtml' then
        comp.TextComponent.Anchor = anchor
    elseif ctype == 'HZTextButton' then
        comp.EditTextAnchor = anchor
    elseif ctype == 'HZToggleButton' then
        comp.EditTextAnchor = anchor
    else
        error('type error ' .. ctype)
    end
end

function Api.UI.SetGray(compID, val)
    local comp = GetUINode(compID)
    comp.IsGray = val
end

function Api.UI.IsGray(compID, val)
    local comp = GetUINode(compID)
    return comp.IsGray
end

function Api.UI.SetAlpha(compID, alpha)
    local comp = GetUINode(compID)
    comp.Alpha = alpha
end

function Api.UI.GetAlpha(compID)
    local comp = GetUINode(compID)
    return comp.Alpha
end

function Api.UI.GetUserTag(compID)
    local comp = GetUINode(compID)
    return comp.UserTag
end

function Api.UI.GetUserData(compID)
    local comp = GetUINode(compID)
    return comp.UserData
end

function Api.UI.GetNodeName(compID)
    local comp = GetUINode(compID)
    return comp.Name
end

function Api.UI.SetEnable(compID, val)
    local comp = GetUINode(compID)
    comp.Enable = val
end

function Api.UI.IsEnable(compID)
    return GetUINode(compID).Enable
end

function Api.UI.IsInteractive(compID)
    return GetUINode(compID).IsInteractive
end
function Api.UI.SetEnableChildren(compID, val)
    local comp = GetUINode(compID)
    comp.EnableChildren = val
end

function Api.UI.SetVisible(compID, visible)
    local node = GetUINode(compID)
    node.Visible = visible
end

function Api.UI.ToggleGroup(tbts, default, fn)
    local tbt_comps = {}
    if type(default) == 'function' then
        default = nil
        fn = default
    end
    for _, v in ipairs(tbts) do
        tbt_comps[#tbt_comps + 1] = GetUINode(v)
        default = default or v
    end
    local default_comp = GetUINode(default)
    UIUtil.ConfigToggleButton(
        tbt_comps,
        default_comp,false,
        function(sender)
            for i, v in ipairs(tbt_comps) do
                if v == sender then
                    fn(tbts[i])
                    break
                end
            end
        end
    )
end

function Api.UI.GetText(compID)
    local node = GetUINode(compID)
    return node.Text
end

function Api.UI.GetSize(compID)
    local comp = GetUINode(compID)
    return comp.Width, comp.Height
end

function Api.UI.IsChecked(compID)
    local comp = GetUINode(compID)
    return comp.IsChecked
end

function Api.UI.SetChecked(compID, val)
    local comp = GetUINode(compID)
    comp.IsChecked = val
end

function Api.UI.IsVisible(compID)
    local comp = GetUINode(compID)
    return comp.Visible
end

function Api.UI.IsActiveInHierarchy(compID)
    if not compID then
        return false
    end
    local comp = GetUINode(compID)
    return comp.UnityObject.activeInHierarchy
end

-- FillMethod.Horizontal,
-- FillMethod.Vertical,
-- FillMethod.Radial90,
-- FillMethod.Radial180,
-- FillMethod.Radial360,
function Api.UI.SetGaugeFillMode(compID, fillMode, fillOrigin, fillClockwise, fillCenter)
    local comp = GetUINode(compID)
    fillClockwise = fillClockwise or false
    fillCenter = fillCenter or true
    comp:SetFillMode(fillMode, GameUtil.TryEnumToInt(fillOrigin), fillClockwise, fillCenter)
end

function Api.UI.SetGaugeMinMax(compID, min, max)
    local comp = GetUINode(compID)
    comp:SetGaugeMinMax(min, max)
end

function Api.UI.SetGaugeValue(compID, value)
    local comp = GetUINode(compID)
    comp.Value = value
end

function Api.UI.GetGaugeValue(compID)
    return GetUINode(compID).Value
end

function Api.UI.GetUnityObject(compID)
    local obj = GetUINode(compID).UnityObject
    return SaveGameObject(obj)
end

function Api.UI.IsUIObject(compID)
    local data = EventApi.GetCacheData(compID)
    return data ~= nil and (data.type == 'ui' or data.type == 'ui_root')
end

function Api.UI.GetRoot(id)
    if event_uis[id] then
        return id
    else
        local comp, root = GetUINode(id)
        return root
    end
end

--Constraint.Flexible
--Constraint.FixedColumnCount
--Constraint.FixedRowCount
function Api.UI.SetGridLayout(compID, constrain, count, spaceX, spaceY, padding)
    local comp = GetUINode(compID)
    if comp.NumChildren == 0 then
        error('no child')
    end
    local child = comp:GetChildAt(0)
    local cs = child.Size2D
    local space = Vector2(spaceX or 0, spaceY or 0)
    padding = padding or {}
    local rectOffset = RectOffset(padding.left or 0, padding.right or 0, padding.top or 0, padding.bottom or 0)
    comp:SetGridLayout(cs, space, rectOffset, constrain, count)
end

function Api.UI.DisableGridLayout(compID)
    local comp = GetUINode(compID)
    comp:DisableGridLayout()
end

function Api.UI.GetChildCount(compID)
    local comp = GetUINode(compID)
    return comp.NumChildren
end

function Api.UI.GetAllChildren()
    local comp = GetUINode(compID)
    local all = HZUISystem:GetAllChildren(comp)
    all = CSharpList2Table(all)
    return all
end

function Api.UI.GetChildAt(compID, index)
    index = index or 0
    local comp, root = GetUINode(compID)
    if comp.NumChildren < index then
        local child = comp:GetChildAt(index)
        return SaveUINode(child, root)
    end
end

function Api.UI.Clone(compID)
    local comp, root = GetUINode(compID)
    local cloneComp = comp:Clone()
    comp.Parent:AddChild(cloneComp)
    return SaveUINode(cloneComp, root)
end

function Api.UI.RemoveFromParent(compID)
    GetUINode(compID):RemoveFromParent(true)
end

function Api.UI.Task.PlayCPJOnce(compID, index)
    local comp = GetUINode(compID)
    local id = EventApi.Task.CreateWaitAlways()
    UIUtil.PlayCPJOnce(
        comp,
        index,
        function(sender)
            EventApi.Task.StopEvent(id)
        end
    )
    return id
end

function Api.UI.SetScaleAndAdjustPos(compID, scaleX, scaleY)
    local comp = GetUINode(compID)
    local scale = comp.Scale
    local x, y = comp.Position2D.x, comp.Position2D.y
    local w, h = comp.Size2D.x, comp.Size2D.y
    local offx = (scale.x - scaleX) * w * 0.5
    local offy = (scaleY - scale.y) * h * 0.5
    comp.Scale = Vector2(scaleX, scaleY)
    comp.Position2D = Vector2(x + offx, y + offy)
end

function Api.UI.GetRotation(compID)
    local comp = GetUINode(compID)
    local v = comp.Transform.localEulerAngles
    return v.x, v.y, v.z
end

function Api.UI.GetScale(compID)
    local comp = GetUINode(compID)
    return comp.Scale.x, comp.Scale.y
end

function Api.UI.SetRotation(compID, x, y, z)
    local comp = GetUINode(compID)
    comp.Transform.localEulerAngles = Vector3(x or 0, y or 0, z or 0)
end

--todo scale合并下的坐标位置适配
function Api.UI.SetRotationAndAdjustPos(compID, x, y, z)
    local comp = GetUINode(compID)
    local function CalcSize(deg, s, scale)
        local rad = math.rad(deg)
        return math.abs(math.cos(rad) * s * scale)
    end

    local posx, posy = comp.Position2D.x, comp.Position2D.y
    local w, h = comp.Size2D.x, comp.Size2D.y
    local startx, starty = posx, posy
    local lastRx = comp.Transform.localEulerAngles.x
    local lastRy = comp.Transform.localEulerAngles.y
    local scale = comp.Scale
    if lastRy ~= 0 then
        local lastTw = CalcSize(lastRy, w, scale.x)
        local off_w = (w - lastTw) * 0.5
        if scale.x == -1 then
            off_w = -off_w
        end
        if scale.x == -1 then
            startx = -posx - off_w
        else
            startx = posx - off_w
        end
    end
    if lastRx ~= 0 then
        local lastTh = CalcSize(lastRx, h, scale.y)
        starty = posy - (h - lastTh) * 0.5
    end
    local tw = CalcSize(y, w, scale.x)
    local th = CalcSize(x, h, scale.y)
    local off_w = (w - tw) * 0.5
    local off_h = (h - th) * 0.5
    if scale.x == -1 then
        off_w = -off_w
    end
    local pos = Vector2(startx + off_w, starty + off_h)
    if scale.x == -1 then
        pos.x = -pos.x
    end
    comp.Position2D = pos
    comp.Transform.localEulerAngles = Vector3(x or 0, y or 0, z or 0)
end

function Api.UI.SetScale(compID, scaleX, scaleY)
    scaleY = scaleY or scaleX
    local comp = GetUINode(compID)
    comp.Scale = Vector2(scaleX, scaleY)
end

function Api.UI.SetScaleX(compID, scaleX)
    local comp = GetUINode(compID)
    comp.Scale = Vector2(scaleX, comp.Scale.y)
end

function Api.UI.SetScaleY(compID, scaleY)
    local comp = GetUINode(compID)
    comp.Scale = Vector2(comp.Scale.x, scaleY)
end

function Api.UI.CloseAll()
    MenuMgr.Instance:CloseAllMenu()
end

function Api.UI.SetNodesToCenterVertical(w, cw, welt, nodesID, start)
    local nodes = {}
    for i, v in ipairs(nodesID) do
        local comp = GetUINode(compID)
        table.insert(nodes, comp)
    end
    UIUtil.SetNodesToCenterStyle(w, cw, welt, nodes, true, start)
end

function Api.UI.SetNodesToCenterHorizontal(w, cw, welt, nodesID, start)
    local nodes = {}
    for i, v in ipairs(nodesID) do
        local comp = GetUINode(v)
        table.insert(nodes, comp)
    end
    UIUtil.SetNodesToCenterStyle(w, cw, welt, nodes, false, start)
end

function Api.UI.AdjustToCenter(parentID, childID, offx, offy)
    local parent = GetUINode(parentID)
    local child = GetUINode(childID)
    UIUtil.AdjustToCenter(parent, child, offx, offy)
end

function Api.UI.SetBackground(ui, a, r, g, b)
    local menu = GetUI(ui).menu
    a = a or 0.5
    r = r or 0
    g = g or 0
    b = b or 0
    menu:SetFullBackground(UILayout.CreateUILayoutColor(UnityEngine.Color(r, g, b, a), UnityEngine.Color(r, g, b, a)))
end

function Api.UI.StartEraserMode(compID, brush)
    local comp = GetUINode(compID)
    comp:StartEraserMode(brush)
end

function Api.UI.StopEraserMode(compID)
    local comp = GetUINode(compID)
    comp:StopEraserMode(brush)
end

function Api.UI.GetEraserPercent(compID)
    local comp = GetUINode(compID)
    return comp.EraserPercent
end

function Api.UI.Task.PlayScreenCPJOnce(path, animeName, offx, offy)
    local id = EventApi.Task.CreateWaitAlways()
    UIUtil.PlayScreenCPJOnce(
        path,
        animeName,
        offx or 0,
        offy or 0,
        function()
            EventApi.Task.StopEvent(id)
        end
    )
    return id
end

function Api.UI.Listen.TouchClick(compID, fn)
    local comp, root, custom = GetUINode(compID)
    if custom then
        local id = EventApi.Task.CreateWaitAlways()
        comp.TouchClick = function(sender)
            EventApi.InvokeListenCallBack(id, fn)
        end
        return id
    else
        return EventApi.Scene.Listen.TouchClick(EventApi.UI.GetUnityObject(compID), fn)
    end
end

function Api.UI.Listen.PointerDown(compID, fn)
    local comp, root, custom = GetUINode(compID)
    if custom then
        local id = EventApi.Task.CreateWaitAlways()
        comp.event_PointerDown = function(sender)
            EventApi.InvokeListenCallBack(id, fn)
        end
        return id
    else
        return EventApi.Scene.Listen.PointerDown(EventApi.UI.GetUnityObject(compID), fn)
    end
end

function Api.UI.SetHudVisible(var)
    MenuMgr.Instance:HideHud(not var)
end
function Api.UI.SetGoRoundVisible(var)
    GameAlertManager.Instance.GoRound:SetEnable(var)
end

function Api.UI.SetQuestHudVisible(var)
    HudManager.Instance.TeamQuest.Visible = var
end

function Api.UI.CheckRaycast(compID)
    local comp = GetUINode(compID)
    return GameUtil.CheckGameObjectRaycast(comp.UnityObject)
end
------------------------------------------Asset -------------------------------------------
--todo info.Parent 为transform 的情况
local function CreateTransformSet(t)
    local tt = EventApi.copy_table(t)
    if t.Parent then
        if ExistsGameObject(t.Parent) then
            local obj = GetGameObject(t.Parent)
            tt.Parent = obj.transform
        elseif ExistUINode(t.Parent) then
            local comp, uiid = GetUINode(t.Parent)
            tt.Parent = comp.Transform
            if not tt.Layer then
                tt.UILayer = true
            end
            local ui = GetUI(uiid)
            if not tt.LayerOrder then
                tt.LayerOrder = ui.menu.MenuOrder
            end
        end
    end
    return Util.CreateTransformSet(tt)
end

function Api.PlayEffect(fileName, t, duration)
    local info = CreateTransformSet(t)
    local effid = RenderSystem.Instance:PlayEffect(fileName, info, duration or 0)
    return effid
end

function Api.GetPlayEffectObject(effid)
    return RenderSystem.Instance:GetAssetGameObject(effid)
end

function Api.SetPlayEffectActive(effid, show)
    local aoe = EventApi.GetPlayEffectObject(effid)
    if aoe then
        aoe.gameObject:SetActive(show)
        return true, aoe
    else
        return false
    end
end

function Api.RemovePlayEffect(effid)
    RenderSystem.Instance:Unload(effid)
end

function Api.SetEffectPos(resID, pos)
    local aoe
    if type(resID) == 'number' then
        aoe = RenderSystem.Instance:GetAssetGameObject(resID)
    elseif type(resID) == 'userdata' then
        aoe = resID
    end
    if aoe and aoe.transform ~= nil then
        aoe.transform.localPosition = pos
    end
end

function Api.Task.PlayEffect(fileName, t, duration)
    local id
    local effid
    local autostoped
    local info = CreateTransformSet(t)
    local effid = RenderSystem.Instance:PlayEffect(fileName, info, duration or 0)

    local function main()
        local lid =
            EventApi.Listen.AddPeriodicSec(
            0.5,
            function()
                if RenderSystem.Instance:IsFinishPlayEffect(effid) then
                    autostoped = true
                    EventApi.Task.StopEvent(id)
                end
            end
        )
        EventApi.Task.Wait(lid)
    end

    local function clean()
        if not autostoped then
            RenderSystem.Instance:Unload(effid)
        end
    end

    id = EventApi.Task.AddEvent({main = main, clean = clean})
    return id
end

--{Scale =, Pos = {x,y,z}, Deg = {x,y,z},Parent=uiid or assetID}
function Api.Scene.SetTransformSet(resID, t)
    local info = CreateTransformSet(t)
    local aoe
    if type(resID) == 'number' then
        aoe = RenderSystem.Instance:GetAssetGameObject(resID)
    elseif type(resID) == 'userdata' then
        aoe = resID
    end
    RenderSystem.Instance:SetTransform(aoe, info)
end

function Api.Scene.FindGameObject(path)
    -- print('FindGameObject',tostring(GameObject))
    local obj = GameObject.Find(path)
    if obj then
        return SaveGameObject(obj)
    end
end

function Api.Scene.FindChild(objID, path)
    local obj = GetGameObject(objID)
    local child = obj.transform:Find(path)
    if child then
        return SaveGameObject(child.gameObject)
    else
        local aoe = obj:GetComponent(AssetGameObject)
        if aoe then
            child = aoe:FindNode(path)
            if child then
                return SaveGameObject(child)
            end
        end
    end
end

function Api.Scene.SetImageFillAmount(objID, value)
    local obj = GetGameObject(objID)
    local img = obj:GetComponent(UnityEngine.UI.Image)
    img.fillAmount = value
end

function Api.Scene.IsActiveInHierarchy(objID)
    local obj = GetGameObject(objID)
    return obj.activeInHierarchy
end

function Api.Scene.SetActive(objID, val)
    local obj = GetGameObject(objID)
    return obj:SetActive(val)
end
function Api.Scene.CheckRaycast(objID)
    local obj = GetGameObject(objID)
    return GameUtil.CheckGameObjectRaycast(obj)
end

function Api.Scene.Listen.Visible(ui, fn)
    local eid
    --print('listenVisible', ui)
    local function main()
        local id =
            EventApi.Listen.AddPeriodicSec(
            0.3,
            function()
                if EventApi.Scene.IsActiveInHierarchy(ui) then
                    EventApi.InvokeListenCallBack(eid, fn)
                end
            end
        )
        EventApi.Task.Wait(id)
    end
    eid = EventApi.Task.AddEvent({main = main})
    EventApi.Task.Wait(id)
end

function Api.Scene.RotateColliderZ(dragObjID, colliderObjID)
    local obj = GetGameObject(dragObjID)
    local colliderObj = GetGameObject(colliderObjID)
    local collider = colliderObj:GetComponent(UnityEngine.Collider)
    local crz = obj:AddComponent(ColliderRotateZ)
    crz.collider = collider
end

function Api.Scene.Task.LoadGameObject(fileName, t)
    local id = EventApi.Task.CreateWaitAlways()
    local info = CreateTransformSet(t)
    RenderSystem.Instance:LoadGameObject(
        fileName,
        info,
        function(aoe)
            EventApi.SetEventOutput(id, SaveGameObject(aoe.gameObject))
            EventApi.Task.StopEvent(id)
        end
    )
    return id
end

function Api.Scene.SetAsEffect(objID, duration)
    local obj = GetGameObject(objID)
    local aoe = obj:GetComponent(AssetGameObject)
    duration = duration or -1
    aoe:SetAsEffect(duration)
end

function Api.Scene.Unload(objID)
    local obj = GetGameObject(objID)
    RenderSystem.Instance:Unload(obj)
end

function Api.Scene.Task.MoveTo(objID, pos, duration)
    local obj = GetGameObject(objID)
    local last_runningsec = 0
    local startpos = obj.transform.localPosition
    local targetPos = TableV3ToVector3(pos)
    local function UpdatePos(running_sec)
        local t = running_sec / duration
        t = math.min(t, 1)
        obj.transform.localPosition = Vector3.Lerp(startpos, targetPos, t)
    end
    return EventApi.Listen.AddPeriodicSec(0, duration, UpdatePos)
end

function Api.Scene.SetWorldPosition(objID, pos)
    local v3 = TableV3ToVector3(pos)
    local obj = GetGameObject(objID)
    obj.transform.position = v3
end
function Api.Scene.SetAnchoredPosition(objID, pos)
    local obj = GetGameObject(objID)
    obj.transform.anchoredPosition = TableV2ToVector2(pos)
end

function Api.Scene.GetLocalEulerAngles(objID)
    local obj = GetGameObject(objID)
    return Vector3ToTableV3(obj.transform.localEulerAngles)
end

function Api.Scene.SetLocalEnlerAngles(objID, pos)
    local obj = GetGameObject(objID)
    local last = obj.transform.localEulerAngles
    obj.transform.localEulerAngles = Vector3MergerTableV3(last, pos)
end

--int,string
function Api.Scene.Task.LoadAvatarGameObject(avatarMap, t)
    local eid = EventApi.Task.CreateWaitAlways()
    local info = CreateTransformSet(t)
    RenderSystem.Instance:LoadGameUnit(
        avatarMap,
        info,
        function(aoe)
            EventApi.Scene.SetTransformSet(aoe, t)
            local objid = SaveGameObject(aoe.gameObject)
            EventApi.SetEventOutput(eid, objid)
            EventApi.Task.StopEvent(eid)
        end,
        function()
            EventApi.Task.StopEvent(eid, false, 'load failed')
        end
    )
    return eid
end

function Api.Scene.RemoveComponents(objID, strcomp)
    local obj = GetGameObject(objID)
    local comps = obj:GetComponents(_G[strcomp])
    if comps then
        local compstable = CSharpArray2Table(comps)
        for _, v in ipairs(compstable) do
            UnityHelper.Destroy(v, 0)
        end
    end
end

function Api.Scene.Destroy(objID)
    local obj = GetGameObject(objID)
    UnityHelper.Destroy(obj, 0)
end

function Api.Scene.ExistsGameObject(objID)
    return ExistsGameObject(objID)
end

function Api.Scene.ToMFUIPosition(v)
    return Vector2(v.x, -v.y)
end

function Api.Scene.PlayAnimation(objID, name)
    local obj = GetGameObject(objID)
    local aoe = obj:GetComponent(AssetGameObject)
    if aoe then
        aoe:Play(name)
    else
        local animator = obj:GetComponent(UnityEngine.Animator)
        if not animator then
            error('animator not found')
        end
        animator:Play(name, 0, 0)
    end
end

function Api.Scene.GetPivot(objID)
    local obj = GetGameObject(objID)
    return Vector2ToTableV2(obj.transform.pivot)
end

function Api.Scene.GetWorldSpace(objID)
    local obj = GetGameObject(objID)
    return Vector3ToTableV3(obj.transform.position)
end

function Api.Scene.WorldSpaceToLoaclSpace(objID, wp)
    local obj = GetGameObject(objID)
    return Vector3ToTableV3(obj.transform:InverseTransformPoint(TableV3ToVector3(wp)))
end

function Api.Scene.GetRectSize(objID)
    local obj = GetGameObject(objID)
    return Vector2ToTableV2(obj.transform.sizeDelta)
end

function Api.Scene.GetChildCount(objID)
    local obj = GetGameObject(objID)
    return obj.transform.childCount
end

function Api.Scene.Listen.TouchClick(objID, fn)
    local obj = GetGameObject(objID)
    local eid
    local function OnTouchUp(touchObj, point)
        -- print('touchObj', touchObj, obj, touchObj == obj)
        if touchObj == obj then
            EventApi.InvokeListenCallBack(eid, fn)
        end
    end
    local globalTouchKey = GameGlobal.Instance.FGCtrl:AddGlobalTouchUpHandler('Scene.Listen.TouchClick', OnTouchUp)
    local function main()
        EventApi.Task.WaitAlways()
    end
    local function clean()
        GameGlobal.Instance.FGCtrl:RemoveGlobalTouchUpHandler(globalTouchKey)
    end

    eid = EventApi.Task.AddEvent({main = main, clean = clean})
    return eid
end

function Api.Scene.Listen.PointerDown(objID, fn)
    local obj = GetGameObject(objID)
    local eid
    local function OnTouchDown(touchObj, point)
        if touchObj == obj then
            EventApi.InvokeListenCallBack(eid, fn)
        end
    end
    local globalTouchKey = GameGlobal.Instance.FGCtrl:AddGlobalTouchDownHandler('Scene.Listen.PointerDown', OnTouchDown)
    local function main()
        EventApi.Task.WaitAlways()
    end
    local function clean()
        GameGlobal.Instance.FGCtrl:RemoveGlobalTouchUpHandler(globalTouchKey)
    end

    eid = EventApi.Task.AddEvent({main = main, clean = clean})
    return eid
end

function Api.UI.Listen.OpenFunMenu(fn)
    local eid
    local function CheckShow()
        local checkMaps = {
            PublicConst.SceneType._SingleDungeon, --2：单人副本
            PublicConst.SceneType._TeamDungeon, --3：组队副本
            PublicConst.SceneType._ZhenYaoTa, --4：镇妖塔
            PublicConst.SceneType._XianLinDao, --5：仙灵岛
            PublicConst.SceneType._MiJing, --8：秘境副本
            PublicConst.SceneType._ZhanChang10v10, --10：10v10战场
            PublicConst.SceneType._zhanChang4v4 --11：4v4竞技场
        }
        local mapdb = GlobalHooks.DB.FindFirst('MapData', {id = DataMgr.Instance.UserData.MapTemplateId})
        if mapdb then
            for i = 1, #checkMaps do
                if mapdb.type == checkMaps[i] then
                    return false
                end
            end
        end
        return true
    end
    local function CheckIdleState()
        -- 有菜单未关闭

        if MenuMgr.Instance:GetTopMenu() ~= nil then
            --print("GetTopMenu")
            return false
        end
        if MenuMgr.Instance:GetTopMsgBox() ~= nil then
            --print("GetTopMsgBox")
            return false
        end
        if DataMgr.Instance.UserData:GetActor() == nil then
            return false
        end
        -- 正在自动寻路
        if DataMgr.Instance.UserData:GetActor().IsAutoRun then
            -- print("IsAutoRun")
            return false
        end

        if EventApi.UI.Exists('FuncOpen') then
            return false
        end

        return true
    end
    local function main()
        local id =
            EventApi.Listen.AddPeriodicSec(
            0,
            function()
                if CheckShow() and CheckIdleState() then
                    EventApi.InvokeListenCallBack(eid, fn)
                end
            end
        )
        EventApi.Task.Wait(id)
    end
    eid = EventApi.Task.AddEvent({main = main})
    return eid
end

function Api.UI.Listen.Visible(ui, fn)
    local eid
    --print('listenVisible', ui)
    local function main()
        local id =
            EventApi.Listen.AddPeriodicSec(
            0,
            function()
                if EventApi.UI.IsActiveInHierarchy(ui) then
                    EventApi.InvokeListenCallBack(eid, fn)
                end
            end
        )
        EventApi.Task.Wait(id)
    end
    eid = EventApi.Task.AddEvent({main = main})
    EventApi.Task.Wait(eid)
end

function Api.UI.Task.ShowConfirmAlert(content, title)
    local eid = EventApi.Task.CreateWaitAlways()
    UIUtil.ShowConfirmAlert(
        content,
        title,
        function()
            EventApi.StopEvent(eid)
        end,
        function()
            EventApi.StopEvent(eid, false)
        end
    )
    return eid
end

function Api.UI.ShowGoround(text)
    GameAlertManager.Instance:ShowGoRoundTips(text)
end
function Api.UI.ShowFloatingTips(text)
    GameAlertManager.Instance:ShowFloatingTips(text)
end

function Api.UI.ShowMeesageInChannel(text, channelType)
    local chat = require 'Model/ChatModel'
    chat.AddClientMsg(channelType, text)
end

--------------------------------------------------------------------------------------
-------------------------------------------Sound--------------------------------------
function Api.PlaySound(res, loop)
    return SoundManager.Instance:PlaySound(res, loop or false)
end

function Api.StopSound(soundID)
    SoundManager.Instance:StopSound(soundID)
end

function Api.ReduceBGMVol()
    local soundvol = GameUtil.GetIntGameConfig('BGM_Percentage_reduction')
    local changevol = SoundManager.Instance.DefaultBGMVolume * soundvol / 100
    SoundManager.Instance:SetCurrentBGMVol(changevol)
end
function Api.ResumeBGMVol()
    SoundManager.Instance:SetCurrentBGMVol(SoundManager.Instance.DefaultBGMVolume)
end
function Api.PlayBGM(res)
    SoundManager.Instance:PlayBGM(res, 1)
end

function Api.PauseBGM()
    SoundManager.Instance:PauseBGM()
end

function Api.StopBGM()
    SoundManager.Instance:StopBGM()
end

function Api.ResumeBGM()
    SoundManager.Instance:ResumeBGM()
end

function Api.GetCurrentBGM()
    return SoundManager.Instance:GetCurrentBGMBundleName()
end

function Api.ChangeBGM(res)
    return SoundManager.Instance:ChangeBGM(res)
end
--------------------------------------------------------------------------------------
function Api.Task.PlayCG(fileName, canSkip,mapid)
    local id = EventApi.Task.CreateWaitAlways()
    local skip = canSkip
    if skip == nil then
        skip = true
    end
   
    TLBattleScene.Instance:PlayCG(
        fileName,
        skip,
        function()
            EventApi.Task.StopEvent(id)
        end,mapid or 0
    )
    return id
end

function Api.GetActorName()
    return DataMgr.Instance.UserData.Name
end

function Api.CanSkipCG(self, var)
    TLBattleScene.Instance:CanSkipCG(var)
end

function Api.Task.ShowChapter(self, chapterId)
    local id = EventApi.Task.CreateWaitAlways()
    GlobalHooks.UI.OpenUI(
        'ChapterMain',
        0,
        {
            chapterId = chapterId,
            CallBack = function()
                EventApi.Task.StopEvent(id)
            end
        }
    )
    return id
end

function Api.Call(funcstr, ...)
    local list = string.split(funcstr, '.')
    local func = _G
    for k, v in ipairs(list) do
        func = func[v]
    end
    print('call function', funcstr)
    if type(func) == 'function' then
        return func(...)
    end
end

local register_dramaEvents = {}

function Api.Task.StartDramaScript(scriptName, ...)
    local drama_id = GlobalHooks.Drama.Start(scriptName, ...)
    if drama_id == nil then
        print('StartDramaScript', scriptName .. ' not found ')
    end
    register_dramaEvents[drama_id] = true
    local function main()
        EventApi.Task.WaitAlways()
    end
    local function clean()
        GlobalHooks.Drama.Stop(drama_id)
    end
    return EventApi.Task.AddEvent({main = main, clean = clean})
end

local function OnDramaEventEnd(eventname, params)
    register_dramaEvents[params.id] = nil
end

local function DoKeyEvent(isAdd, key, arg, ...)
    local name, id = unpack(EventApi.string_split(key, '.'))
    print('Dokeyevent', id, ...)
    local scriptName
    local fn = isAdd and EventApi.Task.AddEvent or EventApi.Task.StartEvent
    if name == 'client' then
        scriptName = 'Client/' .. id
        return fn({main = scriptName, env = {arg = arg}}, ...)
    elseif name == 'client_drama' then
        scriptName = 'Client/drama_starter'
        return fn(scriptName, id, ...)
    else
        local scriptInfo = key_scriptMap[name]
        local targetManagerName = scriptInfo.manager
        if targetManagerName ~= 'Client' then
            return
        end
        if not scriptInfo then
            error('not find script ' .. key)
        end
        if id then
            id = tonumber(id)
            ele = EventApi.FindExcelData(scriptInfo.excel, id)
            -- EventApi.pprint('ele', scriptInfo.excel, ele)
            params = {{main = scriptInfo.path, env = {arg = arg}}, ele, ...}
        else
            params = {{main = scriptInfo.path, env = {arg = arg}}, ...}
        end
        local function mainLogic()
            if ele then
                for _, v in ipairs(ele.condition and ele.condition.id or {}) do
                    if not EventApi.string_IsNullOrEmpty(v) then
                        local condOk, condRet = EventApi.Task.Wait(EventApi.Task.AddEventByKey(v, arg))
                        if not condOk then
                            return condOk, condRet
                        end
                    end
                end
            end
            -- EventApi.Task.WaitActorReady()
            local eid = EventApi.Task.AddEvent(unpack(params))
            EventApi.Listen.ListenEvent(
                eid,
                function(...)
                    local fatherID = EventApi.GetParentEventID(eid)
                    EventApi.TriggerEvent(fatherID, ...)
                end
            )
            if not ele or scriptInfo.sub then
                return EventApi.Task.Wait(eid)
            end
            local argTable = EventApi.DynamicToArgTable(EventApi.Task.Wait(eid))
            local ok = argTable[1]
            local nextOutput
            if ok then
                -- success events
                for _, v in ipairs(ele.success and ele.success.id or {}) do
                    if not EventApi.string_IsNullOrEmpty(v) then
                        local nextArg = EventApi.GetNextArg(arg)
                        EventApi.Task.StartEventByKey(v, nextArg)
                    end
                end
            else
                -- failed events
                for _, v in ipairs(ele.fail and ele.fail.id or {}) do
                    if not EventApi.string_IsNullOrEmpty(v) then
                        local nextArg = EventApi.GetNextArg(arg)
                        EventApi.Task.StartEventByKey(v, nextArg)
                    end
                end
            end
            -- next events
            if ok and not EventApi.string_IsNullOrEmpty(ele.next_event) then
                local nextArg = EventApi.GetNextArg(arg)
                nextArg.Pre = {Key = key, IsSuccess = ok, UnpackOutput = true, Output = EventApi.RepackArgTabel(argTable, 2)}
                local nextid = EventApi.Task.AddEventByKey(ele.next_event, nextArg)
                return EventApi.Task.Wait(nextid)
            end
            return EventApi.ArgTableToDynamic(argTable)
        end
        if toID then
            return EventApi.Task.AddEventTo(toID, {main = mainLogic, desc = key})
        elseif isAdd then
            return EventApi.Task.AddEvent({main = mainLogic, desc = key})
        else
            return EventApi.Task.StartEvent({main = mainLogic, desc = key})
        end
    end
end

--! @brief 按key的方式添加一个字事件
--! @param key 即excel表格中的表名.行id，例：reward.2
function Api.Task.AddEventByKey(key, ...)
    EventApi.print('AddEventByKey:' .. key)
    return DoKeyEvent(true, key, ...)
end

-- arg:{ZoneUUID-场景实例ID，MapTemplateID-地图模板ID，PlayerUUID-玩家UUID}
-- excelEvent中返回值如果有Continue,则可以作为下一次StartEventByKey的arg
--! @brief 按key的方式启动一个新事件
--! @param key 即excel表格中的表名.行id，例：reward.2
function Api.Task.StartEventByKey(key, ...)
    EventApi.print('StartEventByKey:' .. key)
    return DoKeyEvent(false, key, ...)
end

function Api.StartRadar(mapID, radarkey, params)
    local ePrams = {isShow = true, mapid = mapID, flag = params.flag, radarkey = radarkey}
    if params.x and params.y then
        ePrams.pos = {x = params.x, y = params.y}
    end
    EventManager.Fire('EVENT_UI_FindTreasure', ePrams)
end

function Api.StopRadar(radarkey)
    EventManager.Fire('EVENT_UI_FindTreasure', {isShow = false, radarkey = radarkey})
end

function Api.SetQuestAutoMove(questID, autoMove, mapID, params)
    local ePrams = {autoMove = autoMove, id = questID, mapId = mapID}
    params = params or {}
    ePrams.monsterID = params.monsterID
    ePrams.roadName = params.flag
    ePrams.aimX = params.x
    ePrams.aimY = params.y
    ePrams.hints = params.hints
    -- print_r('SetQuestAutoMove',ePrams)
    EventManager.Fire('Event_QuestEvent_AutoMove', ePrams)
    -- if params.radar == 1 then
    --     if mapID ~= EventApi.GetMapTemplateID() then
    --         -- todo 不再同一种场景的雷达处理
    --         return
    --     end
    --     local x, y
    --     if not string.IsNullOrEmpty(params.flag) then
    --         x, y = EventApi.GetFlagPosition(params.flag)
    --     else
    --         x, y = params.x, params.y
    --     end
    --     if x and y then
    --         EventApi.StartRadar(x, y)
    --     end
    -- end
end

function Api.FormatSecondStr(sec, format)
    local dt = System.DateTime(1970, 1, 1)
    dt = dt:AddSeconds(sec)
    local defaultformat = 'mm:ss'
    if dt.Hour > 0 then
        defaultformat = 'HH:mm:ss'
    end
    format = format or defaultformat
    return GameUtil.FormatDateTime(dt, format)
end

function Api.FormatLangSecond(sec)
    return TimeUtil.FormatToCN(sec)
end
function Api.FormatText1234(text, ...)
    return Util.Format1234(text, ...)
end

function Api.GetShortText(text, num)
    return Util.GetShortText(text, num)
end

function Api.GetText(text, ...)
    return Util.GetText(text, ...)
end

function Api.EnterBlockTouch(compID, alpha)
    alpha = alpha or 0.3
    local trans
    if compID then
        local data = EventApi.GetCacheData(compID)
        if data.type == 'ui' then
            trans = data.obj.Transform
        elseif data.type == 'gameObject' then
            trans = data.obj:GetComponent(UnityEngine.RectTransform)
        end
    end
    GameUtil.EnterBlockTouch(trans, alpha)
end

function Api.BlockSize(compID)
    local trans
    if compID then
        local data = EventApi.GetCacheData(compID)
        if data.type == 'ui' then
            trans = data.obj.Transform
        elseif data.type == 'gameObject' then
            trans = data.obj:GetComponent(UnityEngine.RectTransform)
        end
    end
    if trans == nil then
        return 0, 0
    end
    return trans.rect.width, trans.rect.height
end

function Api.ExitBlockTouch()
    GameUtil.ExitBlockTouch(trans, alpha)
end

function Api.ExitCurrentZone(alert)
    if alert then
        local map_data = EventApi.FindExcelData('map/map_data.xlsx/map_data', DataMgr.Instance.UserData.MapTemplateId)
        local content = EventApi.GetText(Constants.Text.exit_zone_warn, EventApi.GetText(map_data.name))
        UIUtil.ShowConfirmAlert(
            content,
            nil,
            function()
                Protocol.RequestHandler.ClientLeaveDungeonRequest(
                    {},
                    function()
                    end
                )
            end
        )
    else
        Protocol.RequestHandler.ClientLeaveDungeonRequest(
            {},
            function()
            end
        )
    end
end

function Api.FireEventMessage(ename, v)
    EventManager.Fire(ename, {value = v})
end

function Api.SetTimeScale(scale)
    UnityEngine.Time.timeScale = scale
end

function Api.ParseCostAndCostGroup(coststatic)
    return ItemModel.ParseCostAndCostGroup(coststatic)
end

function Api.InRockMove()
    return GameSceneMgr.Instance.UGUI.HasRockFingerIndex
end

function Api.OnStart()
    print('ClientApi OnStart')
    EventManager.Subscribe('OnDramaEventEnd', OnDramaEventEnd)
end

function Api.OnStop()
    EventManager.Unsubscribe('OnDramaEventEnd', OnDramaEventEnd)
end
function Api.ShowWeather(params)
    EventManager.Fire('Event.Scene.ChangeWeather', params)
end

function Api.UI.Task.TalkContentbyId(dialogueid)
    local eid = EventApi.Task.CreateWaitAlways()
    local TalkContext = QuestNpcDataModel.GetQuestContext(dialogueid)
    if TalkContext == nil then
        return
    end
    local params = {
        TalkContext = TalkContext,
        cb = function()
            EventApi.Task.StopEvent(eid)
        end
    }
    QuestNpcDataModel.OpenQuestTalk(params)
    return eid
end

function Api.FunctionTimeLeftSec(functionid)
    return FunctionUtil.TimeLeftSec(functionid)
end

function Api.IsInBattleStatus()
    return not TLBattleScene.Instance.Actor:isNoBattleStatus()
end

function Api.SetActorAutoGuard(val)
    TLBattleScene.Instance.Actor:BtnSetAutoGuard(val)
end

function Api.QuickTransport(mapId)
    local SceneModel = require 'Model/SceneModel'
    SceneModel.ReqChangeScene(mapId)
end

Api.Quest = {Task = {}, Listen = {}}
local function FindQuestByField(qlist, iter)
    local quests = {}
    for quest in Slua.iter(qlist) do
        local static = GlobalHooks.DB.Find('Quest', quest.id)
        local ok = true
        for k, v in pairs(iter) do
            if type(v) == 'function' then
                if not v(static[k]) then
                    ok = false
                    break
                end
            elseif static[k] ~= v then
                ok = false
                break
            end
        end
        if ok then
            table.insert(quests, quest.id)
        end
    end
    return quests
end

function Api.Quest.FindAcceptByField(iter)
    return FindQuestByField(DataMgr.Instance.QuestData.Accepts, iter)
end

function Api.Quest.FindNotAcceptByField(iter)
    return FindQuestByField(DataMgr.Instance.QuestData.NotAccepts, iter)
end

function Api.Quest.SeekQuest(questID)
    QuestUtil.doQuestById(questID)
end

function Api.Quest.GetMain()
    local quests = EventApi.Quest.FindAcceptByField({sub_type = 1})
    return quests[1] or EventApi.Quest.FindNotAcceptByField({sub_type = 1})[1]
end

function Api.SubscribeGlobalBack(key, fn)
    GlobalHooks.SubscribeGlobalBack(key, fn)
end

function Api.UnsubscribeGlobalBack(key)
    GlobalHooks.UnsubscribeGlobalBack(key)
end

function Api.Quest.Task.GiveUp(questID)
    local eid = EventApi.Task.CreateWaitAlways()

    QuestModel.requestGiveUp(
        questID,
        function()
            EventApi.Task.StopEvent(eid)
        end,
        function()
            EventApi.Task.StopEvent(eid, false)
        end
    )
    return eid
end

function Api.SeekToFlag(mapid,flag)
	local action = MoveEndAction()
	action.MapId = mapid
	action.RoadName = flag
	if TLBattleScene.Instance.Actor then
        TLBattleScene.Instance.Actor:AutoRunByAction(action)
	end
end

local DataHelper = require(_total_global.path .. 'event_script/DataHelper')
DataHelper.SetRootPath(_total_global.path .. _total_global.config.ExcelRootPath)
function Api.FindExcelData(tb_name, find_key)
    return DataHelper.Find(tb_name, find_key)
end

function Api.GetExcelVersion(tb_name)
    return DataHelper.GetVersion(tb_name)
end
function Api.SetExcelData(tb_name, data, version)
    if type(data) ~= 'table' then
        data = EventApi.LoadBytes(data)
    end
    print('SetExcelData', tb_name, version, data)
    return DataHelper.SetDataTable(tb_name, data, version)
end

function Api.ClearExcelCache()
    DataHelper.ClearCache()
end
function Api.SetBlackScreen(val)
    if TLBattleScene.Instance then
        TLBattleScene.Instance:BlackScreen(val)
    end
end

function Api.Task.GetRoleSnap(uuid)
    local eid = EventApi.Task.CreateWaitAlways()
    Util.GetRoleSnap(
        uuid,
        function(snap)
            EventApi.SetEventOutput(eid, snap)
            EventApi.Task.StopEvent(eid)
        end
    )
    return eid
end

function Api.Quest.Task.AcceptQuest(questID)
    local qData = GlobalHooks.DB.Find('Quest', questID)
    local eid = EventApi.Task.CreateWaitAlways()
    if qData.sub_type == 2100 or qData.sub_type == 2200 then
        QuestModel.RequestClientAcceptCarriage(
            questID,
            function(ret)
                EventApi.Task.StopEvent(eid, ret)
            end
        )
    else
        QuestModel.requestAccept(
            questID,
            function(ret)
                EventApi.Task.StopEvent(eid, ret)
            end
        )
    end
    return eid
end

function Api.Task.WaitActorReady()
    if not EventApi.IsActorReady() then
        EventApi.Task.Wait(EventApi.Listen.ActorReady())
    end
end

function Api.ChapterZeroStart()
    GameSceneMgr.Instance:ReadyToLoadingExt()
end

function Api.GetActorPro()
    return DataMgr.Instance.UserData.Pro
end

function Api.GetActorGender()
    return DataMgr.Instance.UserData.Gender
end

Api.Protocol = {Task = {}, Listen = {}}
local protol_index = 1
function Api.Protocol.Task.Request(name, params)
    local reqname = 'Protocol.Request.' .. name
    params = params or {}
    params.__protocol_index__ = protol_index
    protol_index = protol_index + 1
    local rpname = 'Protocol.Response.' .. name .. '.' .. params.__protocol_index__
    EventApi.SendMessage('Player', DataMgr.Instance.UserData.RoleID, reqname, params)
    return EventApi.Task.AddEvent(
        function()
            local ok, ename, ret = EventApi.Task.Wait(EventApi.Listen.Message(rpname))
            if not ok then
                ret = {s2c_msg = ename}
            end
            if ret.s2c_code and ret.s2c_code ~= 200 and not ret.s2c_msg then
                ret.s2c_msg = MessageCodeAttribute['DeepMMO.Protocol.Response'][500]
            end
            if ret.s2c_msg then
                local msg
                if ret.msg_params then
                    msg = EventApi.GetText(ret.s2c_msg, EventApi.ArgTableToDynamic(ret.msg_params))
                else
                    msg = EventApi.GetText(ret.s2c_msg)
                end
                EventApi.ShowMessage(msg)
                return false
            end
            return true, ret
        end
    )
end
function Api.Protocol.Notify(name, params)
    local reqname = 'Protocol.Notify.' .. name
    EventApi.SendMessage('Player', DataMgr.Instance.UserData.RoleID, reqname, params)
end

function Api.SetRedTips(redKey, count, subKey)
    GlobalHooks.UI.SetRedTips(redKey, count, subKey)
end

function Api.LoadString(str, ...)
    local chunkfn = load(str, ...)
    if chunkfn then
        return chunkfn()
    else
        print('LoadString nil', str)
    end
end
function Api.LoadBytes(bytes, ...)
    return EventApi.LoadLuaBytes(bytes)
end

function Api.GetActorGuildPosition()
    return DataMgr.Instance.GuildData.Position
end

function Api.Listen.EnvironmentVar(varname, fn)
    local id
    local function on_change(ename, params)
        if params.key == varname then
            EventApi.InvokeListenCallBack(id, fn, params.value)
        end
    end
    local function main()
        EventManager.Subscribe('Event.SyncEnvironmentVarEvent', on_change)
        EventApi.Task.WaitAlways()
    end
    local function clean()
        EventManager.Unsubscribe('Event.SyncEnvironmentVarEvent', on_change)
    end

    id = EventApi.Task.AddEvent({main = main, clean = clean})
    return id
end

function Api.Task.BlockActorAutoRun(maxsec)
    maxsec = maxsec or 120
    return EventApi.Listen.AddPeriodicSec(
        0.1,
        maxsec,
        function()
            --EventApi.ChangeIdleState()
            EventApi.StopActorAutoRun()
            --临时黑科技
            -- EventManager.Fire('Event.Npc.DialogueTalk', {isTalk = true})
            -- EventManager.Fire('Event.Npc.DialogueTalk', {isTalk = false})
        end
    )
end

function Api.Listen.CGSTATE(fn)
    local id
    local function PLAYCG_START(ename, params)
        if params.PlayCG then
            EventApi.InvokeListenCallBack(id, fn)
        end
    end
    local function main()
        EventManager.Subscribe('EVENT_PLAYCG_START', PLAYCG_START)
        EventApi.Task.WaitAlways()
    end
    local function clean()
        EventManager.Unsubscribe('EVENT_PLAYCG_START', PLAYCG_START)
    end

    id = EventApi.Task.AddEvent({main = main, clean = clean})
    return id
end


function Api.Listen.IsEnterRegion(regionname,fn)
    local eid
    local function main()
        local region = TLBattleScene.Instance:GetEditorRegionFlag(regionname)
        if region ~= nil then
            local id = EventApi.Listen.AddPeriodicSec(
                0.1,
                function()
                    if not TLBattleScene.Instance or not TLBattleScene.Instance.Actor then
                        return
                    end
                    local x = TLBattleScene.Instance.Actor:GetX()
                    local y = TLBattleScene.Instance.Actor:GetY()
                    if TLBattleScene.Instance:IsInRegion(region.Data,x,y) then
                        EventApi.InvokeListenCallBack(eid,fn)
                    end
                end
            )
            EventApi.Task.Wait(id)
        end
    end
    eid = EventApi.Task.AddEvent({main = main})
    EventApi.Task.Wait(eid)
end
-------------------------------------------------Unit Api-----------------------------------------
-- function Api.Unit.Task.
--临时修复
--function Api.ChangeIdleState()
--    if TLBattleScene.Instance.Actor then
--        local  action = MoveEndAction()
--        action.MapId = DataMgr.Instance.UserData.MapTemplateId
--        action.AimX = TLBattleScene.Instance.Actor.
--        action.AimY = TLBattleScene.Instance.Actor.
--        print("action.AimX",action.AimX,action.AimY)
--        TLBattleScene.Instance.Actor:AutoRunByAction(action)
--    end
--end

--------------------------------------------------------------------------------------------------
return Api

--! @}
