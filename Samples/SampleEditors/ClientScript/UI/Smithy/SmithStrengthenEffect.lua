local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local Util = require 'Logic/Util'
local _M = {}
_M.__index = _M

local function OnToggleButtonClick(self, index)
    if self.last_avatar then
        RenderSystem.Instance:Unload(self.last_avatar)
        self.last_avatar = nil
    end
    local avatar = Util.GetActorAvartarTable()
    avatar[Constants.AvatarPart.Ride_Avatar01] = nil
    local info = self.static_shows[index]
    for i, v in pairs(info.modle.key) do
        local keyIndex = Constants.AvatarPart[v:gsub('%s+', '')]
        avatar[keyIndex] = info.modle.value[i]
    end
    local t = {
        Parent = self.comps.cvs_anchor.Transform,
        Scale = 130,
        LayerOrder = self.menu.MenuOrder + 1,
        UILayer = true,
        Deg = {y = -180}
    }
    self.comps.cvs_model.event_PointerMove = function(sender, data)
        local delta = -data.delta.x
        local obj = RenderSystem.Instance:GetAssetGameObject(self.last_avatar)
        if obj then
            obj.transform:Rotate(Vector3.up, delta * 1.2)
        end
    end
    self.last_avatar = Util.LoadGameUnit(avatar, t)
    self.comps.lb_name.Text = Util.GetText(Constants.Text.equip_refine_effect, Util.GetLangNum(info.refine_rank))
end

local function OnSelectToggleButtion(self, sender)
    local substr = string.sub(sender.EditName, string.len('tbt_an') + 1)
    local index = tonumber(substr)
    OnToggleButtonClick(self, index)
end

local function IndexModel(self, m)
    for index, v in ipairs(self.static_shows) do
        for i, vv in pairs(v.modle.key) do
            if vv == m.modle.key[i] and v.modle.value[i] == m.modle.value[i] then
                return index
            end
        end
    end
    table.insert(self.static_shows, m)
    return -1
end

-- local lvup = 3
function _M.OnEnter(self)
    local maxStatic = ItemModel.GetMaxWholeRefineStaticInfo()
    -- local maxStatic = GlobalHooks.DB.FindFirst('EquipRefine', {refine_rank = lvup, refine_lv = 1})
    -- lvup = lvup + 3
    -- print_r('maxStatic',maxStatic)
    if maxStatic then
        -- 选中当前特效
        self.comps.tbt_an1.IsChecked = true
        local index = IndexModel(self, maxStatic)
        local tbt = self.comps['tbt_an'..index]
        if tbt then
            tbt.IsChecked = true
        else
            self.comps.tbt_an1.IsChecked = true
        end
    else
        self.comps.tbt_an1.IsChecked = true
    end
end

function _M.OnExit(self)
    if self.last_avatar then
        RenderSystem.Instance:Unload(self.last_avatar)
        self.last_avatar = nil
    end
end

function _M.OnDestory(self)
end

function _M.OnInit(self)
    self.static_shows = {}

    GlobalHooks.DB.Find(
        'EquipRefine',
        function(m)
            return not string.IsNullOrEmpty(m.modle.value[1]) and IndexModel(self, m) < 0
        end
    )
    table.sort(
        self.static_shows,
        function(x, y)
            return x.record_lv < y.record_lv
        end
    )

    local tbts = {}
    for i, v in ipairs(self.static_shows) do
        local tbt = self.comps['tbt_an' .. i]
        if tbt then
            table.insert(tbts, tbt)
            tbt.Text = Util.GetText(Constants.Text.strengthen_rank, Util.GetLangNum(v.refine_rank))
        end
    end
    UIUtil.ConfigToggleButton(
        tbts,
        nil,
        false,
        function(sender)
            OnSelectToggleButtion(self, sender)
        end
    )
    -- self.comps.cvs_anchor.Enable = false

    self.menu.ShowType = UIShowType.Cover
end

return _M
