---------------------------------
--! @file
--! @brief a Doxygen::Lua UIUtil.lua
---------------------------------

local _M = {}
_M.__index = _M

local BaseUI = require 'UI/BaseUI'
local Util = require 'Logic/Util'

local function FindChild(parent, key, recursive)
    recursive = recursive or false
    local child = parent:FindChildByEditName(key, recursive)
    return child
end

--UI层加载称号
--特效返回模型的info,无模型或文字称号返回nil
local function SetTitle(self, titlenode, titleid,nameExt)
    if titleid > 0 then
        titlenode.Text = ""
        titlenode.Visible = true
        local detail = unpack(GlobalHooks.DB.Find('title', {title_id = titleid}))
        if detail.title_type == 2 then
            titlenode.Layout = nil
            local title = UI3DModelAdapter.AddSingleModel(titlenode, Vector2(0,0), 1, self.ui.menu.MenuOrder,detail.effect_res)
            title.Callback = function (info)
                local trans2 = info.RootTrans
                trans2:Rotate(Vector3.up,180)
                trans2.localPosition = Vector3(titlenode.Size2D[1]/2,-titlenode.Size2D[2], -500)
            end
            return title
        elseif detail.title_type == 1 then
            titlenode.Layout = nil
            titlenode.Text = Util.GetText(detail.word_res)
        elseif detail.title_type == 3 then
            titlenode.Text = Util.GetText(detail.word_res,nameExt) 
            if not string.IsNullOrEmpty(detail.pic_res) then
                titlenode.Layout = HZUISystem.CreateLayout(detail.pic_res, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 8);
            end
        end
    else
        titlenode.Visible = true
        titlenode.Text = Constants.Title.noEquipText
    end
    return nil
end

local function ShowTips(self,touchnode,itemid)
    local temp = self.ui.root:GlobalToLocal(touchnode:LocalToGlobal(),true)
    local detail = _M.ShowNormalItemDetail({
        templateID = itemid,
        autoHeight = true,
        autoWidth = true})
    local size = detail.ui.comps.cvs_itemtips.Size2D
    if temp[1]-size[1] < 0 then
        size[1] = temp[1]
    end
    if temp[2]-size[2] < 0 then
        size[2] = temp[2]
    end
    detail:SetPos(temp[1]-size[1],temp[2]-size[2])
    local function OnDetailExit()
        detail.Visible = false
    end
    detail:SubscribOnExit(OnDetailExit)
end

local function ConfigToggleButton(tbts, default, keep_state, fun)
    local last
    for _, val in ipairs(tbts) do
        val:SetBtnLockState(HZToggleButton.LockState.eLockSelect)
        val.Selected = function(sender)
            if sender.IsChecked then
                for _, v in ipairs(tbts) do
                    if not v:Equals(val) then
                        v.IsChecked = false
                    end
                end
                if fun then
                    fun(sender)
                end
                if not sender.IsChecked and last and last ~= sender then
                    last.IsChecked = true
                else
                    last = sender
                end
            end
        end
        if default and default:Equals(val) then
            val.IsChecked = true
        elseif not keep_state then
            val.IsChecked = false
        end
    end
end

local function Bind(temptable, tag, xml)
    local function BindEvent(binder, ui)
        if binder.OnLoad then
            ui:SubscribOnLoad(
                'BindEvent',
                function(self, callback)
                    local params = binder.__params
                    binder:OnLoad(callback, unpack(params or {}))
                end
            )
        end
        if binder.OnEnter then
            ui:SubscribOnEnter(
                'BindEvent',
                function()
                    local params = binder.__params
                    binder:OnEnter(unpack(params or {}))
                    binder.__params = nil
                end
            )
        end
        if binder.OnExit then
            ui:SubscribOnExit(
                'BindEvent',
                function()
                    binder:OnExit()
                end
            )
        end
        if binder.OnDestory then
            ui:SubscribOnDestory(
                'BindEvent',
                function()
                    binder:OnDestory()
                end
            )
        end
        if binder.OnEnable then
            ui:SubscribOnEnable(
                'BindEvent',
                function()
                    binder:OnEnable()
                end
            )
        end
        if binder.OnDisable then
            ui:SubscribOnDisable(
                'BindEvent',
                function()
                    binder:OnDisable()
                end
            )
        end
    end

    local ret = {}
    setmetatable(ret, temptable)
    local ui = BaseUI.Create(tag, xml)
    BindEvent(ret, ui)

    local function OnMoveToCache(self)
        -- 进入了缓存
    end

    if not ret.OnMoveToCache then
    --ret.OnMoveToCache = OnMoveToCache
    end
    ret.ui = ui
    ret.AddSubUI = function(self, sub)
        self.ui:AddSubUI(sub.ui)
    end
    ret.comps = ret.ui.comps
    ret.menu = ret.ui.menu
    ret.root = ret.ui.root
    ret.Close = function(self)
        self.ui:Close()
    end
    ret.SubscribOnLoad = function(self, ...)
        self.ui:SubscribOnLoad(...)
    end
    ret.SubscribOnEnter = function(self, ...)
        self.ui:SubscribOnEnter(...)
    end
    ret.SubscribOnExit = function(self, ...)
        self.ui:SubscribOnExit(...)
    end
    ret.SubscribOnExitOnce = function(self, ...)
        self.ui:SubscribOnExitOnce(...)
    end
    ret.SubscribOnDestory = function(self, ...)
        self.ui:SubscribOnDestory(...)
    end
    ret.SubscribOnEnable = function(self, ...)
        self.ui:SubscribOnEnable(...)
    end
    ret.SubscribOnDisable = function(self, ...)
        self.ui:SubscribOnDisable(...)
    end
    ret.UnSubscribOnLoad = function(self, ...)
        self.ui:UnSubscribOnLoad(...)
    end
    ret.UnSubscribOnEnter = function(self, ...)
        self.ui:UnSubscribOnEnter(...)
    end
    ret.UnSubscribOnExit = function(self, ...)
        self.ui:UnSubscribOnExit(...)
    end
    ret.UnSubscribOnDestory = function(self, ...)
        self.ui:UnSubscribOnDestory(...)
    end
    ret.UnSubscribOnEnable = function(self, ...)
        self.ui:UnSubscribOnEnable(...)
    end
    ret.UnSubscribOnDisable = function(self, ...)
        self.ui:UnSubscribOnDisable(...)
    end
    ret.EnableTouchFrame = function(self, ...)
        self.ui:EnableTouchFrame(...)
    end
    ret.EnableTouchFrameClose = function(self, ...)
        self.ui:EnableTouchFrameClose(...)
    end
    if ret.ui.comps.btn_close then
        ret.ui.comps.btn_close.TouchClick = function()
            ret.ui:Close()
        end
    end
    return ret
end

--! @brief 设置竖向列表
--! @param pan Scrollpan实例
--! @param tempnode 模版节点实例
--! @param count 列表数量
--! @param eachupdatecb 节点更新回调（node,index）
local function ConfigVScrollPan(pan, tempnode, count, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gy + 1)
    end
    local s = tempnode.Size2D
    pan:Initialize(
        s.x,
        s.y,
        count,
        1,
        tempnode,
        UpdateListItem,
        function()
        end
    )
end

local function ConfigVScrollPanWithOffset(pan, tempnode, row, offset, eachupdatecb)
    _M.ConfigVScrollPanWithFixCoordinate(pan, tempnode, row, Vector2.zero, Vector2(offset, 0), eachupdatecb)
end

local function ConfigVScrollPanWithFixCoordinate(pan, tempnode, row, offset2d, offsetgap, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gy + 1)
    end
    local s = tempnode.Size2D
    pan:Initialize(
        s.x + offsetgap.x,
        s.y + offsetgap.y,
        row,
        1,
        offset2d,
        tempnode,
        UpdateListItem,
        function()
        end
    )
end

--! @brief 设置横向列表
--! @param pan Scrollpan实例
--! @param tempnode 模版节点实例
--! @param count 数量
--! @param eachupdatecb 节点更新回调（node,index）
local function ConfigHScrollPan(pan, tempnode, count, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gx + 1)
    end
    local s = tempnode.Size2D
    pan:Initialize(
        s.x,
        s.y,
        1,
        count,
        tempnode,
        UpdateListItem,
        function()
        end
    )
end

local function ConfigHScrollPanWithOffset(pan, tempnode, col, offset, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gx + 1)
    end
    local s = tempnode.Size2D
    pan:Initialize(
        s.x + offset,
        s.y,
        1,
        col,
        tempnode,
        UpdateListItem,
        function()
        end
    )
end

local function ConfigGridVScrollPan(pan, tempnode, col, count, eachupdatecb)
    _M.ConfigGridVScrollPanFixCoordinate(pan, tempnode, col, count, Vector2.zero, Vector2.zero, eachupdatecb)
end

local function ConfigGridVScrollPanFixCoordinate(pan, tempnode, col, count, offset2d, offsetgap, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gy * col + gx + 1)
    end
    local s = tempnode.Size2D
    local row = count / col
    if count % col ~= 0 then
        row = row + 1
    end
    pan:Initialize(
        s.x + offsetgap.x,
        s.y + offsetgap.y,
        row,
        col,
        offset2d,
        tempnode,
        UpdateListItem,
        function()
        end
    )
end

local function ConfigGridHScrollPan(pan, tempnode, row, count, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gx * row + gy + 1)
    end
    local s = tempnode.Size2D
    local col = count / row
    if count % col ~= 0 then
        col = col + 1
    end
    pan:Initialize(
        s.x,
        s.y,
        row,
        col,
        tempnode,
        UpdateListItem,
        function()
        end
    )
end

local function ConfigPageScrollPan(pan, tempnode, pages, isvertical, CreateCB, MoveEndCB)
    local function CreateListItem(Scrollable, i)
        local node = tempnode:Clone()
        CreateCB(node, i)
        return node
    end
    local s = tempnode.Size2D
    pan:Initialize(pages, s,isvertical, CreateListItem, MoveEndCB)
end

local function ConfigGridVScrollPanWithOffset(pan, tempnode, col, count, offx, offy, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gy * col + gx + 1)
    end
    local s = tempnode.Size2D
    local row = count / col
    if count % col ~= 0 then
        row = row + 1
    end
    pan:Initialize(
        s.x + offx,
        s.y + offy,
        row,
        col,
        tempnode,
        UpdateListItem,
        function()
        end
    )
end

local function ConfigGridHScrollPanWithOffset(pan, tempnode, row, count, offx, offy, eachupdatecb)
    local function UpdateListItem(gx, gy, node)
        node.Visible = true
        eachupdatecb(node, gx * row + gy + 1)
    end
    local s = tempnode.Size2D
    local col = count / row
    if count % col ~= 0 then
        col = col + 1
    end
    pan:Initialize(
        s.x + offx,
        s.y + offy,
        row,
        col,
        tempnode,
        UpdateListItem,
        function()
        end
    )
end

-- [ 1 2 3 . n ] or [1 2 3 4 . n]
-- w总宽,
-- cw元素宽,
-- welt是否首尾靠边,
-- nodes DisplayNode数组
-- change_y是否为垂直,
-- start首元素开始坐标
local function SetNodesToCenterStyle(w, cw, welt, nodes, change_y, start)
    local function GetWeltGridOffset(total, cell, start_offset, num)
        local t = cell * num
        local o = total - t - start_offset * 2
        local ret = o / (num - 1)
        return ret
    end
    local function GetCenterGridOffset(total, cell, start_offset, num)
        local t = cell * num
        local o = total - t - start_offset * 2
        local ret = o / (num + 1)
        return ret
    end

    start = start or 0
    local offet, pos
    if welt and #nodes > 1 then
        offet = GetWeltGridOffset(w, cw, start, #nodes)
        pos = start
    else
        offet = GetCenterGridOffset(w, cw, start, #nodes)
        pos = start + offet
    end
    for _, comp in ipairs(nodes) do
        if change_y then
            comp.Y = pos
            pos = pos + comp.Height + offet
        else
            comp.X = pos
            pos = pos + comp.Width + offet
        end
    end
end

local function SetImage(node, path, resize, style, clipsize)
    style = style or (node.Layout and node.Layout.Style) or UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER
    clipsize = clipsize or 8
    node.Layout = HZUISystem.CreateLayout(path, style, clipsize)
    if resize and node.Layout then
        node.Size2D = node.Layout.PreferredSize
    end
end

local function GetLayout(path, style, clipsize)
    style = style or UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER
    clipsize = clipsize or 8
    return HZUISystem.CreateLayout(path, style, clipsize)
end

-- 文字变换
local function AddNumberPlusPlusTimer(label, srcNum, targetNum, sec, formatstr, cb)
    local offet = 1
    label.Text = srcNum
    local total_offset = math.abs(srcNum - targetNum)
    local ms = (sec / total_offset) * 1000
    local op = srcNum < targetNum and 1 or -1
    local offet = math.floor(35 / ms) + 1
    -- print('---op',op)
    -- print(' ms ...', ms, offet, os.clock())
    if type(formatstr) == 'function' then
        cb = formatstr
        formatstr = nil
    end
    return LuaTimer.Add(
        0,
        35,
        function()
            local cur_offest = math.random(offet - 1, offet + 1)
            total_offset = total_offset - cur_offest

            srcNum = srcNum + op * cur_offest
            if total_offset <= 0 then
                srcNum = targetNum
                if cb then
                    cb()
                end
            end
            -- label.Text = formatstr and string.Format(formatstr,srcNum) or srcNum
            label.Text = formatstr and String.Format(formatstr, srcNum) or srcNum
            return total_offset > 0
        end
    )
end

local function FindChildByType(node, type, recursive)
    return HZUISystem.FindChildByType(node, type, recursive or false)
end

local function FindChildByUserTag(parent, usertag, recursive)
    recursive = recursive or false
    local child = HZUISystem.FindChildByUserTag(parent, usertag, recursive)
    return child
end

local function AdjustToCenter(parent, child, offetX, offetY)
    UGUI.UIUtils.AdjustAnchor(ImageAnchor.C_C, parent, child, Vector2(offetX or 0, offetY or 0))
end

local detail_retain = {}
local function ShowNormalItemDetail(params)
    -- local all = {GlobalHooks.UI.FindUI('ItemDetail')}
    -- for _,v in ipairs(all) do
    --     if v.data.itemshow == params.itemShow then
    --         return
    --     end
    -- end

    local m = GlobalHooks.UI.CreateUI('ItemDetail')
    m:EnableTouchFrameClose(true)
    if params.globalTouch == nil then
        params.globalTouch = true
    end
    m:Reset(params)
    if params.itemShow then
        params.itemShow.IsSelected = true
        detail_retain[params.itemShow] = detail_retain[params.itemShow] and (detail_retain[params.itemShow] + 1) or 1
    end
    local function OnDetailExit()
        m:UnSubscribOnExit(OnDetailExit)
        if params.itemShow then
            local retain = detail_retain[params.itemShow]
            retain = retain - 1
            if retain <= 0 then
                params.itemShow.IsSelected = false
                detail_retain[params.itemShow] = nil
            else
                detail_retain[params.itemShow] = retain
            end
        end
    end

    m:SubscribOnExit(OnDetailExit)
    MenuMgr.Instance:AddMenu(m.ui.menu)
    return m
end

---TemplateId 物品id
----detail 物品详情= ItemModel.GetDetailByTemplateID(id),(id或者detail传一个即可)
----cb  点击前往后的回调 方便做界面关闭
local function ShowGetItemWay(params)
    params = params or {}
    local m = GlobalHooks.UI.CreateUI('ItemGetWay')
    m:EnableTouchFrameClose(true)
    if params.globalTouch == nil then
        params.globalTouch = true
    end
    m:Reset(params)
    if params.itemShow then
        params.itemShow.IsSelected = true
        detail_retain[params.itemShow] = detail_retain[params.itemShow] and (detail_retain[params.itemShow] + 1) or 1
    end
    local function OnDetailExit()
        m:UnSubscribOnExit(OnDetailExit)
        if params.itemShow then
            local retain = detail_retain[params.itemShow]
            retain = retain - 1
            if retain <= 0 then
                params.itemShow.IsSelected = false
                detail_retain[params.itemShow] = nil
            else
                detail_retain[params.itemShow] = retain
            end
        end
    end

    m:SubscribOnExit(OnDetailExit)
    MenuMgr.Instance:AddMenu(m.ui.menu)
    return m
end

local function ShowNormalSkillDetail(skillicon, skillquailty, skillname, skilllv, skilldesc, itemshow, node)
    local detail = {
        static = {
            atlas_id = skillicon,
            quality = skillquailty,
            name = skillname,
            desc = skilldesc,
            level_limit = skilllv,
            item_type = Constants.ItemType.Skill,
            price = 0
        }
    }
    local params = {detail = detail, itemShow = itemshow, autoHeight = true, node = node}
    return ShowNormalItemDetail(params)
end

-- 添加或者更新一个ItemShow
local function SetItemShowTo(node, icon, quality, num)
    local itshow = FindChildByType(node, 'ItemShow', false)
    local t = type(icon)
    if t == 'userdata' then
        if not itshow then
            itshow = ItemShow.Create(icon)
            node:AddChild(itshow)
        else
            itshow:ResetItemData(icon)
        end
    elseif t == 'table' then
        local detail = icon
        num = quality or 1
        if not itshow then
            itshow = ItemShow.Create(detail.static.atlas_id, detail.static.quality, num)
            node:AddChild(itshow)
        else
            itshow.Icon = detail.static.atlas_id
            itshow.Quality = detail.static.quality
            itshow.Num = num or 1
        end
        itshow.Star = detail.static.star_level
    elseif t == 'number' then
        local ItemModel = require 'Model/ItemModel'
        local detail = ItemModel.GetDetailByTemplateID(icon)
        num = quality or 1
        if not itshow then
            itshow = ItemShow.Create(detail.static.atlas_id, detail.static.quality, num)
            node:AddChild(itshow)
        else
            itshow.Icon = detail.static.atlas_id
            itshow.Quality = detail.static.quality
            itshow.Num = num or 1
        end
        itshow.Star = detail.static.star_level
    else
        quality = quality or 0
        if not itshow then
            itshow = ItemShow.Create(icon, quality, num or 1)
            node:AddChild(itshow)
        else
            itshow.Icon = icon
            itshow.Quality = quality
            itshow.Num = num or 1
        end
        itshow.Star = 0
    end

    itshow.Size2D = node.Size2D
    return itshow
end

local function RemoveItemShowFrom(node)
    local itshow = FindChildByType(node, 'ItemShow', false)
    if itshow then
        itshow:ToCache()
    end
end

local CacheMeta = {}
CacheMeta.__index = CacheMeta
function CacheMeta.Reset(self, count)
    self._cache_data = self._cache_data or {}
    if #self._cache_data == 0 and self._useTemplate then
        self._cache_data[1] = self._template
        if self._initcb then
            self._initcb(self._cache_data[1], nil)
        end
    end
    local preNode
    for i, v in ipairs(self._cache_data) do
        if i <= count then
            v.Visible = true
            preNode = v
        else
            v.Visible = false
        end
    end
    while #self._cache_data < count do
        local node = self._template:Clone()
        self._template.Parent:AddChild(node)
        table.insert(self._cache_data, node)
        if self._initcb then
            self._initcb(node, preNode)
        end
        preNode = node
    end
end
function CacheMeta.SetInitCB(self, cb)
    self._initcb = cb
end
function CacheMeta.GetVisibleNodes(self)
    local ret = {}
    for i, v in ipairs(self._cache_data or {}) do
        if v.Visible then
            table.insert(ret, v)
        end
    end
    return ret
end

local function CreateCacheNodeGroup(templateNode, useTemplateNode)
    local ret = setmetatable({}, CacheMeta)
    ret._template = templateNode
    ret._useTemplate = useTemplateNode
    return ret
end

local function MoveToScrollCell(scrollPan, luaIdx, callback)
    local gx = (luaIdx - 1) % scrollPan.Columns
    local gy = math.floor((luaIdx - 1) / scrollPan.Columns)
    local scrollable = scrollPan.Scrollable
    local cellSize = scrollable.CellSize
    local border = scrollable.Border
    local gap = scrollable.Gap
    local scrollPanSize = scrollPan.Size2D
    local x = border.x + gx * (cellSize.x + gap.x) + cellSize.x / 2
    local y = border.y + gy * (cellSize.y + gap.y) + cellSize.y / 2
    local target = Vector2(x - scrollPanSize.x / 2, y - scrollPanSize.y / 2)
    UnityHelper.WaitForEndOfFrame(
        function()
            scrollable:LookAt(target)
            local node = scrollPan.Scrollable:GetCell(gx, gy)
            if callback then
                callback(node)
            end
        end
    )
end

local function ShowErrorMessage(msg)
    GameAlertManager.Instance:ShowNotify("<f color='ffff0000'>" .. msg .. '</f>')
end
local function ShowMessage(msg)
    GameAlertManager.Instance:ShowNotify(msg)
end

local function ShowOkAlert(content)
    local alertKey = GameAlertManager.Instance:ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, content, '', title, nil, okFn)
    return alertKey
end

local function ShowConfirmAlert(content, title, okFn, cancelFn)
    if textCenter == nil then
        textCenter = false
    end
    local alertKey =
        GameAlertManager.Instance:ShowAlertDialog(
        AlertDialog.PRIORITY_NORMAL,
        content,
        '',
        '',
        title,
        nil,
        okFn,
        function()
            if cancelFn then
                cancelFn()
            end
        end
    )
    return alertKey
end

local current_notips = {}
local function ShowCheckBoxConfirmAlert(notipsTag, content, title, okFn, cancelFn)
    if current_notips[notipsTag] then
        okFn()
    else
        local alertKey
        alertKey =
            GameAlertManager.Instance:ShowAlertDialog(
            AlertDialog.PRIORITY_NORMAL,
            content,
            '',
            '',
            title,
            nil,
            function()
                local node = GameAlertManager.Instance.AlertDialog:GetDialogUINode(alertKey)
                local tbt_notips = FindChild(node, 'tbt_notips', true)
                if tbt_notips.IsChecked then
                    current_notips[notipsTag] = true
                end
                okFn()
            end,
            function()
                if cancelFn then
                    cancelFn()
                end
            end
        )
        local node = GameAlertManager.Instance.AlertDialog:GetDialogUINode(alertKey)
        local cvs_notips = FindChild(node, 'cvs_notips', true)
        cvs_notips.Visible = true
        return alertKey
    end
end

local function PlayCPJOnce(node, index, cb)
    local control = node.Layout.SpriteController
    if control ~= nil then
        if type(index) == 'function' then
            cb = index
            index = 0
        end
        control:PlayAnimate(
            index or 0,
            1,
            function(sender)
                if cb then
                    cb(sender)
                end
            end
        )
    else
        print('no SpriteController in this node', node)
    end
end

local function PlayScreenCPJOnce(path, animeName, offx, offy, cb)
    local finishCB
    if type(offx) == 'function' then
        finishCB = offx
    else
        finishCB = cb
    end
    GameAlertManager.Instance.CpjAnime:PlayCacheCpjAnime(
        nil,
        path,
        animeName,
        offx,
        offy,
        1,
        function()
            if finishCB then
                finishCB(sender)
            end
        end
    )
end

local function InitFix3DSngleModel(parentCvs, fixpos, scale, rotate, menuOrder, fileName, canRotate, cb)
    local cvs_anchor = FindChild(parentCvs, 'cvs_anchor')
    local pos2d = Vector2(0, 0)
    if cvs_anchor ~= nil then
        pos2d = cvs_anchor.Position2D
    else
        print('Init3DSngleModel cvs_anchor is nil')
    end
    fixpos = fixpos or Vector2(0, 0)
    pos2d.x = pos2d.x + fixpos.x
    pos2d.y = pos2d.y + fixpos.y
    local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder, fileName)

    local trans = info.RootTrans
    info.Callback = function(_info)
        -- body
        local trans2 = _info.RootTrans
        trans2:Rotate(Vector3.up, rotate)
        if cb then
            cb(_info)
        end
    end
    if canRotate then
        parentCvs.Enable = true
    else
        parentCvs.Enable = false
    end
    parentCvs.event_PointerMove = function(sender, data)
        local delta = -data.delta.x
        trans:Rotate(Vector3.up, delta * 1.2)
    end
    return info
end

local function Init3DSngleModel(parentCvs, scale, rotate, menuOrder, fileName, canRotate, cb)
    return InitFix3DSngleModel(parentCvs, nil, scale, rotate, menuOrder, fileName, canRotate, cb)
end

local TreeMenuMeta = {}
TreeMenuMeta.__index = TreeMenuMeta

--menuDatas 结构
--{text = xxx, select = cb, children = {}}
local function CreateTreeMenu(root, text, selectcb)
    local ret = setmetatable({}, TreeMenuMeta)
    ret.text = text
    ret.root = root or ret
    ret.select = selectcb
    return ret
end

function TreeMenuMeta.AddChild(self, text, selectcb)
    self.children = self.children or {}
    self.fold = self.fold or -1
    local sub = CreateTreeMenu(self.root, text, selectcb)
    sub.fold = self.fold + 1
    sub.parent = self
    table.insert(self.children, sub)
    return sub
end

function TreeMenuMeta.SetUserTag(self, tag)
    self.usertag = tag
end
function TreeMenuMeta.GetUserTag(self)
    return self.usertag
end

function TreeMenuMeta.IsEnable(self)
    return self.enable
end

function TreeMenuMeta.IsVisible(self)
    if not self.visible then
        return false
    end
    if self.parent then
        return self.parent:IsVisible()
    end
    return true
end

function TreeMenuMeta.SetTextColorRGB(self, fontrgb)
    self.rgb = fontrgb
end

function TreeMenuMeta.FindChildByUserTag(self, tag)
    if self.usertag == tag then
        return self
    else
        for _, v in ipairs(self.children or {}) do
            local ret = v:FindChildByUserTag(tag)
            if ret then
                return ret
            end
        end
    end
end

local function SetTreeMenuVisible(self, visible)
    self.visible = visible
    for _, v in ipairs(self.children or {}) do
        SetTreeMenuVisible(v, visible)
    end
end

function TreeMenuMeta.SetChildrenEnable(self, val)
    for _, v in ipairs(self.children or {}) do
        v:SetEnable(val)
    end
end

-- enable时， 所有子节点显示
function TreeMenuMeta.SetEnable(self, val, inner_call)
    self.enable = val
    if self.button then
        self.button.IsChecked = val
    end
    if self.enable then
        self.visible = true
        if not self.children then
            if self.root.last_select and self.root.last_select.button then
                self.root.last_select.button.IsChecked = false
            end
            self.root.last_select = self
        end
        for _, v in ipairs(self.children or {}) do
            v.visible = true
        end
        --展开逻辑
        if self.parent and self.parent ~= self.root then
            self.parent:SetEnable(true, true)
        end
    else
        SetTreeMenuVisible(self, false)
        self.visible = self.parent.enable
    end
    if not inner_call then
        local last_select = self.root.last_select
        if last_select and last_select.button and not last_select:IsVisible() then
            last_select.button.IsChecked = false
            self.root.last_select = nil
        end
        if self.root.is_show then
            self:RelayoutTreeMenu()
        end
    end
end

function TreeMenuMeta.SetEnableAndInvoke(self, val)
    -- if val == self.enable then
    --     return
    -- end
    self:SetEnable(val)
    if self.enable and self.select then
        self.select(not self.children, self)
    end
end

function TreeMenuMeta.SetText(self, text)
    self.text = text
    if self.button then
        self.button.Text = self.text
    end
end

local function SetTreeMenuButton(self, tbn)
    self.button = tbn
    self.button.IsChecked = self.enable == true
    if not self.children then
        self.button:SetBtnLockState(HZToggleButton.LockState.eLockSelect)
    end
    self.button.Text = self.text
    if self.rgb then
        self.button.FontColor = GameUtil.RGB2Color(self.rgb)
        self.button.FocuseFontColor = GameUtil.RGB2Color(self.rgb)
    end
    self.button.TouchClick = function(sender)
        self:SetEnableAndInvoke(sender.IsChecked)
    end
end

local function Tree2Line(self, ret)
    if self.root ~= self then
        table.insert(ret, self)
    end
    for _, v in ipairs(self.children or {}) do
        Tree2Line(v, ret)
    end
end

function TreeMenuMeta.RelayoutTreeMenu(self)
    local all = {}
    Tree2Line(self.root, all)
    local params = self.root.show_params
    params.tbn_menu.Visible = false
    params.tbn_ele.Visible = false

    local p = params.starty
    local parentNode = params.tbn_menu.Parent
    for _, v in ipairs(all) do
        if v.visible then
            local tbn = v.button
            if not tbn then
                if not v.children then
                    --最终的element
                    tbn = params.tbn_ele:Clone()
                else
                    --有subTree
                    tbn = params.tbn_menu:Clone()
                end
                parentNode:AddChild(tbn)
                SetTreeMenuButton(v, tbn)
            end
            v.button.Visible = true
            if params.startx then
                v.button.X = params.startx + v.fold * params.spacex
            end
            v.button.Y = p
            p = p + v.button.Height + params.spacey
            if not self.root.is_show and v.enable and v.select then
                v.select(not v.children)
            end
        else
            if v.button then
                v.button.Visible = false
            end
        end
    end
end

function TreeMenuMeta.Show(self, tbnmenu, tbnele, startx, starty, spacex, spacey)
    local parentNode = tbnmenu.Parent
    self.show_params = {
        tbn_menu = tbnmenu,
        tbn_ele = tbnele or tbnmenu,
        startx = startx,
        starty = starty or 0,
        spacex = spacex or 0,
        spacey = spacey or 0
    }
    self:SetEnable(true)
    self:RelayoutTreeMenu()
    self.is_show = true
end

function TreeMenuMeta.Close(self)
    local all = {}
    Tree2Line(self.root, all)
    for _, v in ipairs(all) do
        if v.button then
            v.button:RemoveFromParent(true)
            v.button = nil
        end
    end
    self.root.show_params = nil
    self.root.is_show = false
end

local function SetEnoughLabel(self, lb_num, cur, need, notshowcur)
    local storeName = 'src' .. lb_num.EditName
    if not self[storeName] then
        self[storeName] = lb_num.FontColorRGB
    end
    if need > cur then
        lb_num.FontColorRGB = Constants.Color.Red
    else
        lb_num.FontColorRGB = self[storeName]
    end
    if notshowcur then
        lb_num.Text = need
    else
        lb_num.Text = cur .. '/' .. need
    end
end

local Enough_Add_Img_Tag = 599999
local function SetEnoughItemShowAndLabel(self, cvs_itshow, lb_num, cost, tipsparams)
    local ItemModel = require 'Model/ItemModel'
    local storeName = 'src_' .. cvs_itshow.UnityObject:GetInstanceID()
    tipsparams = tipsparams or {}
    local function Draw(listen_accept)
        local color_storeName = storeName .. 'color'
        if not self[color_storeName] and lb_num then
            self[color_storeName] = lb_num.FontColorRGB
        end
        cost.need = cost.need or 0
        local notenough = cost.need > cost.cur
        local itshow = SetItemShowTo(cvs_itshow, cost.detail)
        if tipsparams.circleQuality then
            itshow.IsCircleQualtiy = tipsparams.circleQuality
        end
        local img = FindChildByUserTag(cvs_itshow, Enough_Add_Img_Tag, false)
        if not img then
            img = HZImageBox.CreateImageBox()
            img.Size2D = cvs_itshow.Size2D
            img.UserTag = Enough_Add_Img_Tag
            cvs_itshow:AddChild(img)
        end
        SetImage(img, tipsparams.circleQuality and 'static/item/add_circle.png' or 'static/item/add.png')

        img.Visible = notenough
        if lb_num then
            if tipsparams.onlycur then
                lb_num.Text = cost.cur
            else
                SetEnoughLabel(self, lb_num, cost.cur, cost.need, cost.detail.static.item_type == 0)
            end
        end
        itshow.EnableTouch = true
        if listen_accept and  tipsparams.cb then
            tipsparams.cb(not notenough, cost.detail)
        end
        itshow.TouchClick = function()
            if not notenough then
                local params = {detail = cost.detail, itemShow = itshow, autoHeight = true, x = tipsparams.x, y = tipsparams.y, anchor = tipsparams.anchor}
                ShowNormalItemDetail(params)
            else
                ShowGetItemWay({detail = cost.detail, itshow = itshow, x = tipsparams.x, y = tipsparams.y, anchor = tipsparams.anchor})
            end
        end
    end
    Draw()

    if tipsparams.ListenItem == false then
        return
    end
    local listen_store_name = storeName .. 'listener'
    if self[listen_store_name] then
        self[listen_store_name]:Dispose()
    end
    self.ui:UnSubscribOnExit(storeName)
    self[listen_store_name] =
        ItemModel.ListenCost(
        cost,
        function(act)
            if cvs_itshow.IsDispose then
                if self.ui then
                    self.ui:UnSubscribOnExit(storeName)
                end
                if self[listen_store_name] then
                    self[listen_store_name]:Dispose()
                end
            else
                ItemModel.RecalcCostAndCostGroup(cost)
                Draw(true)
            end
        end
    )
    self.ui:SubscribOnExitOnce(
        storeName,
        function()
            self[listen_store_name]:Dispose()
        end
    )
end

local function ToLocalPos(src, target)
    local v = src:LocalToGlobal()
    local v1 = target:GlobalToLocal(v, true)
    return v1
end

local function GetContent(item)
    local length = string.len(item)
    --local pos = string.find(item, res)
    local content = string.sub(item, 4, length - 5)
    --print("eeeeeeeeeeeeeeeeeeeeeee =" , content, " item ", item)
    return content
end

local ChatFace = nil

local function GetFaceEmotion(index)
    if not ChatFace then
        ChatFace = GlobalHooks.DB.Find('ChatFace', {})
    end
    if ChatFace[index] then
        return ChatFace[index].e_code
    end
    return ''
end
local function HandleTalkDecode(msg, strColor, fontSize)
    --富文本解析
    if strColor == nil then
        strColor = 0x000000
    end

    if fontSize == nil then
        fontSize = 16
    end
    local linkdata = AttributedString()
    local retArray = msg:split('|')

    for i, ement in ipairs(retArray) do
        local item = ement
        local temptext = ''
        local abs = AttributedString()
        if string.starts(item, '<q ') and string.ends(item, '></q>') then
            local itemData = GameUtil.createTextAttribute(strColor, fontSize)
            temptext = 'a'
            local msg = cjson.decode(GetContent(item))
            itemData.resSprite = '/dynamic/emotion/output/emotion.xml,' .. GetFaceEmotion(msg.index)
            abs:Append(temptext, itemData)
        else
            local color = GameUtil.RGB_To_ARGBString(strColor)
            local str = "<font size= '" .. fontSize .. "' color='" .. color .. "'>"

            local curdata1 = UIFactory.Instance:DecodeAttributedString(str .. item .. '</font>', nil)
            if curdata1 == nil then
                curdata1 = UIFactory.Instance:DecodeAttributedString(str .. 'error' .. '</font>', nil)
            end
            if curdata1 ~= nil then
                abs:Append(curdata1)
            end
        end
        linkdata:Append(abs)
    end

    return linkdata
end

local function EnableButtonWithGray(btn, val, text)
    btn.IsGray = not val 
    btn.Enable = val
    if text then
        btn.Text = text
    end
end

_M.ToLocalPos = ToLocalPos
--设置材料消耗 30/40 or 30文本，不足显示为红色，
--notshowcur表示是否不显示当前，目前虚拟货币需要此参数为true
--self表示UI的具体实例，用于记录lable原有颜色
--  UIUtil.SetEnoughLabel(self, lb_num, v.cur, v.need, v.detail.static.item_type == 0)
_M.SetEnoughLabel = SetEnoughLabel
--设置材料消耗，并支持不足跳转
--cost 为ItemModel中ParseCostAndCostGroup返回的结构 {detail:cur:need}
--tipsparams 为控制道具详情的参数 一般是{x:y:anchor}
_M.SetEnoughItemShowAndLabel = SetEnoughItemShowAndLabel
--创建树形控件
--sample:
--  local tree = UIUtil.CreateTreeMenu()
--  tree:AddChild('所有目标')
--  local sub = tree:AddChild('剧情任务')
--  sub:AddChild('[主]杀你全家')
--  sub:AddChild('[支]野外BOSS（5人）')
--  sub:AddChild('[史]杀他全家-史诗')
--  local sub = tree:AddChild('多人副本（5人）')
--  sub:AddChild('[主]杀你全家')
--  sub:AddChild('[支]野外BOSS（5人）')
--  tree:Show(self.comps.tbt_an1, self.comps.tbt_an2, 0, 51, 30, 0)
_M.CreateTreeMenu = CreateTreeMenu
--确认框
_M.ShowOkAlert = ShowOkAlert
_M.ShowConfirmAlert = ShowConfirmAlert
--带下次不再提示的确认框
_M.ShowCheckBoxConfirmAlert = ShowCheckBoxConfirmAlert
_M.ShowErrorMessage = ShowErrorMessage
_M.ShowMessage = ShowMessage
_M.ShowNormalItemDetail = ShowNormalItemDetail
_M.SetNodesToCenterStyle = SetNodesToCenterStyle
_M.ConfigVScrollPan = ConfigVScrollPan
_M.ConfigHScrollPan = ConfigHScrollPan
_M.ConfigGridVScrollPan = ConfigGridVScrollPan
_M.ConfigGridHScrollPan = ConfigGridHScrollPan

_M.ConfigVScrollPanWithOffset = ConfigVScrollPanWithOffset
_M.ConfigHScrollPanWithOffset = ConfigHScrollPanWithOffset
_M.ConfigGridVScrollPanWithOffset = ConfigGridVScrollPanWithOffset
_M.ConfigGridHScrollPanWithOffset = ConfigGridHScrollPanWithOffset

_M.FindChild = FindChild
_M.ConfigToggleButton = ConfigToggleButton
_M.Bind = Bind
_M.SetImage = SetImage
_M.EnableButtonWithGray = EnableButtonWithGray
_M.GetLayout = GetLayout
_M.AddNumberPlusPlusTimer = AddNumberPlusPlusTimer
_M.FindChildByType = FindChildByType
_M.FindChildByUserTag = FindChildByUserTag
_M.AdjustToCenter = AdjustToCenter
_M.SetItemShowTo = SetItemShowTo
_M.RemoveItemShowFrom = RemoveItemShowFrom
_M.CreateCacheNodeGroup = CreateCacheNodeGroup
--移动到该位置，并调用callback,callback会传入移动位置的node
_M.MoveToScrollCell = MoveToScrollCell
_M.ConfigPageScrollPan = ConfigPageScrollPan
_M.PlayCPJOnce = PlayCPJOnce
_M.PlayScreenCPJOnce = PlayScreenCPJOnce
_M.ConfigVScrollPanWithFixCoordinate = ConfigVScrollPanWithFixCoordinate
_M.ConfigGridVScrollPanFixCoordinate = ConfigGridVScrollPanFixCoordinate
_M.Init3DSngleModel = Init3DSngleModel
_M.ShowNormalSkillDetail = ShowNormalSkillDetail
_M.ShowGetItemWay = ShowGetItemWay
_M.InitFix3DSngleModel = InitFix3DSngleModel
_M.HandleTalkDecode = HandleTalkDecode
_M.GetFaceEmotion = GetFaceEmotion
_M.SetTitle = SetTitle
_M.ShowTips = ShowTips
GlobalHooks.UIUtil = GlobalHooks.UIUtil or {}
GlobalHooks.UIUtil.HandleTalkDecode = _M.HandleTalkDecode
return _M
