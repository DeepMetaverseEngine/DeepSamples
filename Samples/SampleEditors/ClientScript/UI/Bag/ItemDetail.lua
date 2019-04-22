local _M = {}
_M.__index = _M
-- TODO 整理参数
-- TODO 归纳按钮事件
-- TODO 品质色值定义
package.loaded['UI/Bag/Dynamic_Normal'] = nil
package.loaded['UI/Bag/Dynamic_Equip'] = nil
package.loaded['UI/Bag/Dynamic_Fate'] = nil
package.loaded['UI/DynamicUIGenerater'] = nil
-- print_r(package.loaded)

local ItemModel = require 'Model/ItemModel'
local Dynamic_Normal = require 'UI/Bag/Dynamic_Normal'
local Dynamic_Equip = require 'UI/Bag/Dynamic_Equip'
local Dynamic_Fate = require 'UI/Bag/Dynamic_Fate'
local DynamicUIGenerater = require 'UI/DynamicUIGenerater'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

local function GetLangText(text,...)
    if Util.ContainsTextKey(text) then
        return Util.GetText(text,...)
    else
        return ''
    end
end
-- TODO 百分比的比较
local function GetArrow(attr, comp_detail)
    if not comp_detail then
        return Constants.InternalImg.arrow_up
    end
    local arrow

    local comp_attrs = ItemModel.GetEquipAttribute(comp_detail, attr.Tag, attr.ID ~= 0 and attr.ID or attr.Name)

    -- print_r(comp_attr)
    local comp_attr = unpack(comp_attrs)
    if attr and not comp_attr then
        return Constants.InternalImg.arrow_up
    end
    --print('compare value',attr.Value, comp_attr.Value)
    -- if attr.valueType == 1 and comp_attr.valueType == 1 then0
    local arrow = {}
    local cur_v = attr.BaseValue or attr.Value
    local comp_v = comp_attr.BaseValue or comp_attr.Value
    if cur_v > comp_v then
        arrow.Img = Constants.InternalImg.arrow_up
    elseif cur_v < comp_v then
        arrow.Img = Constants.InternalImg.arrow_down
    end
    arrow.H = 18
    arrow.UIStyle = UILayoutStyle.IMAGE_STYLE_BACK_4
    -- arrow.H = xx

    -- end
    -- print('get arrow',arrow)
    return arrow
end

local function CreateNormalBegin(detail, num, w, bind)
    local temp = Dynamic_Normal.CreateBegin(w)
    local star_level = detail.static.star_level
    local data = {
        item = {Icon = detail.static.atlas_id,Star = star_level, Quality = detail.static.quality, Num = num},
        qdi = Constants.InternalImg['icon_di' .. detail.static.quality],
        name = {Text = Util.GetText(detail.static.name), Color = Constants.QualityColor[detail.static.quality]},
        able = Util.GetText('common_usetype') .. ': ' .. Util.GetText(detail.static.using_desc),
        use_lv = {Text = Util.Format1234(Constants.Text.equip_lv_format, detail.static.level_limit)}
        --bind = bind and Constants.InternalImg.detail_bind
    }

    if detail.static.level_limit > DataMgr.Instance.UserData.Level then
        data.use_lv.Color = Constants.Color.detail_limit_red
    else
        data.use_lv.Color = Constants.Color.detail_normal
    end
    local ret = DynamicUIGenerater.create_template(temp, data)
    return ret
end

local function CreateNormalSkillBegin(detail, w)
    local temp = Dynamic_Normal.CreateBegin(w)
    local data = {
        item = {Icon = detail.static.atlas_id, Quality = detail.static.quality, Num = 1, IsCircleQualtiy = true},
        qdi = Constants.InternalImg['icon_di' .. detail.static.quality],
        name = {Text = Util.GetText(detail.static.name), Color = Constants.QualityColor[detail.static.quality]},
        use_lv = {Text = Util.Format1234(Constants.Text.equip_lv_format, detail.static.level_limit)}
    }
    local ret = DynamicUIGenerater.create_template(temp, data)
    return ret
end

local function CreateNormalContent(detail, count, w)
    count = count or 1
    local temp = Dynamic_Normal.CreateContent(w)
    local data = {
        desc = string.format("<f color='%x' size='20'>%s</f>", 0xfffff38b, GetLangText(detail.static.desc)),
        --effect_time = {SupportRichtext=true,Text=string.format('效果:  <color=#00ff00ff>%s</color>','用时才知道')},
        price = detail.static.price and detail.static.price > 0 or nil,
        price_value = {
            SupportRichtext = true,
            Text = string.format(Util.GetText('common_recycling') .. ': ' .. '<color=#00ff00ff>%d</color>  ', (detail.static.price or 0) * count)
        }
    }
    local ret = DynamicUIGenerater.create_template(temp, data)
    return ret
end


local function CreateMarryContent(detail, count, w)
    count = count or 1
    local temp = Dynamic_Normal.CreateContent(w)
    local Husband
    local Wife
    local Date1
    for _, v in ipairs(detail.dynamicAttrs or {}) do
        if v.Tag == 'Husband' then
            Husband = v.Name
        elseif v.Tag == 'Wife' then
            Wife = v.Name
        elseif v.Tag == 'Date' then
            Date1 = v.Name
        end
    end
    -- print('ddddd',Husband,Wife,Date1)

    local data = {
        desc = string.format("<f color='%x' size='20'>%s</f>", 0xfffff38b, GetLangText(detail.static.desc,Husband,Wife)),
        --effect_time = {SupportRichtext=true,Text=string.format('效果:  <color=#00ff00ff>%s</color>','用时才知道')},
        price = detail.static.price and detail.static.price > 0 or nil,
        price_value = {
            SupportRichtext = true,
            Text = string.format(Util.GetText('common_recycling') .. ': ' .. '<color=#00ff00ff>%d</color>  ', (detail.static.price or 0) * count)
        }
    }
    local ret = DynamicUIGenerater.create_template(temp, data)
    return ret
end

local function CreateMarryContent2(detail, count, w)
    count = count or 1
    local temp = Dynamic_Normal.CreateContent(w)
    local Husband
    local Wife
    local Date1
    for _, v in ipairs(detail.dynamicAttrs or {}) do
        if v.Tag == 'Husband' then
            Husband = v.Name
        elseif v.Tag == 'Wife' then
            Wife = v.Name
        elseif v.Tag == 'Date' then
            Date1 = v.Name
        end
    end
    -- print('ddddd',Husband,Wife,Date1)

    local data = {
        desc = string.format("<f color='%x' size='20'>%s</f>", 0xfffff38b, GetLangText(detail.static.desc,Husband,Wife)),
        --effect_time = {SupportRichtext=true,Text=string.format('效果:  <color=#00ff00ff>%s</color>','用时才知道')},
        price = detail.static.price and detail.static.price > 0 or nil,
        price_value = {
            SupportRichtext = true,
            Text = string.format(Util.GetText('common_recycling') .. ': ' .. '<color=#00ff00ff>%d</color>  ', (detail.static.price or 0) * count)
        }
    }
    local ret = DynamicUIGenerater.create_template(temp, data)
    return ret
end

local function CreateFateBegin(detail, compare, compareDetail, num, w, bind)
    local temp = Dynamic_Equip.CreateBegin(w)
    -- detail.static.star_level = 3
    local star_level = detail.static.star_level
    local data = {
        item = {Icon = detail.static.atlas_id, Quality = detail.static.quality, Num = num, Star = star_level},
        qdi = Constants.InternalImg['icon_di' .. detail.static.quality],
        name = {Text = Util.GetText(detail.static.name), Color = Constants.QualityColor[detail.static.quality]},
        --pro = {Text = Constants.ProName[detail.static_equip.profession] or Constants.Text.all_pro},
        --part = Constants.EquipPartName[detail.static_equip.equip_pos],
        lv = {Text = Util.Format1234(Constants.Text.equip_lv_format, detail.static.level_limit)},
        score = Util.GetText('common_score') .. '  ' .. (detail.score > 0 and detail.score or '???')
    }

    if detail.static.star_level >= 0 then
        data.star = {Text = Constants.StarLevel[detail.static.star_level], Color = Constants.StarQuality[detail.static.star_level]}
    end
    if detail.static.level_limit > DataMgr.Instance.UserData.Level then
        data.lv.Color = Constants.Color.detail_limit_red
    else
        data.lv.Color = Constants.Color.detail_normal
    end

    for _, v in ipairs(detail.dynamicAttrs or {}) do
        if v.Tag == 'FateTag' then
            data.lv.Text =  Util.Format1234(Constants.Text.equip_lv_format, v.Value or  1)
            data.lv.Color = Constants.Color.detail_normal
        end
    end

    --if detail.static_equip.profession ~= 0 and detail.static_equip.profession ~= DataMgr.Instance.UserData.Pro then
    --    data.pro.Color = Constants.Color.detail_limit_red
    --else
    --    data.pro.Color = Constants.Color.detail_normal
    --end
    --if compare then
    --    local arrow
    --    print('compare score', detail.score, compareDetail and compareDetail.score)
    --    if not compareDetail or detail.score > compareDetail.score then
    --        arrow = Constants.InternalImg.arrow_up
    --    elseif detail.score < compareDetail.score then
    --        arrow = Constants.InternalImg.arrow_down
    --    end
    --    data.arrow = arrow
    --end
    local ret = DynamicUIGenerater.create_template(temp, data)
    return ret
end

local function CreateFateContent(detail, compare, compareDetail, w)
    local temp = Dynamic_Fate.CreateContent(w)
 
    local base_attr
    local nobase_attr

    local extra_attr

    local fateLv = 1
    local locked = false
  

    for _, v in ipairs(detail.dynamicAttrs or {}) do
        if v.Tag == 'FixedAttributeTag' then
            base_attr = base_attr or {}
            local attr_name, v_str, basevalue = ItemModel.GetAttributeString(v, true)
            local attr_name, v_str = ItemModel.GetAttributeString(v)
            local tmp = {
                attrname = {Text = string.format('%s:', attr_name), Color = rgb},
                attr = {Text = string.format('+%s', v_str), Color = rgb},
                arrow = compare and GetArrow(v, compareDetail) or nil,
                img = Constants.InternalImg.detail_gray_point
            }
            if v.BaseValue and v.Value then
                local attr_name, v_str = ItemModel.GetAttributeString(v, true)
                tmp.appendvalue = string.format('(+%s)', v_str)
            end
            table.insert(base_attr, tmp)
        elseif v.Tag == 'ExtraAttributeTag' then
            extra_attr = extra_attr or {}
            local attr_name, v_str = ItemModel.GetAttributeString(v)
            local rgb = ItemModel.GetAttributeColorRGB(v)
            table.insert(
                    extra_attr,
                    {
                        attrname = {Text = string.format('%s:', attr_name), Color = rgb},
                        attr = {Text = string.format('+%s', v_str), Color = rgb},
                        img = Constants.InternalImg.detail_gray_point
                    }
            ) 
         elseif v.Tag == 'FateTag' then
            fateLv = v.Value or  1
            --
            -- ItemModel.IsAttritbuteLocked(v)
            for _,sub in ipairs(v.SubAttributes or  {}) do
                if sub.Tag == 'IsLockedTag' then
                    locked = true
                end
            end
        end
    end
     
    local FateModel = require'Model/FateModel'
    local next_attr = FateModel.GetNextFatedAttribute(detail.static.id,fateLv)
    local lvDate =  FateModel.GetFateNextLvData(detail.static.id,fateLv)
    local coustnum = nil
    local next = 1
    if lvDate then
        coustnum = lvDate.cost.num[1]
    end
    local data = { 
        -- nobase_attr = base_attr == nil,
        base_attr =  base_attr ~= nil,
        base_attr_array = base_attr,
        lock = locked and 1 or nil,
        -- nonext_attr = coustnum == nil,
        next_cost = coustnum,
        costnum = coustnum,
        next_attr = next_attr ~= ni,
        next_attr_array = next_attr,
    } 

    local desc_static = GetLangText(detail.static.desc)
    if not string.IsNullOrEmpty(desc_static) then
        data.end_line = Constants.InternalImg.split_line
        data.desc = string.format("<f color='%x' size='20'>%s</f>", 0xfffff38b, desc_static)
    end 

    local ret = DynamicUIGenerater.create_template(temp, data)
    return ret
end

local function CreateEquipBegin(detail, compare, compareDetail, num, w, bind)
    local temp = Dynamic_Equip.CreateBegin(w)
    -- detail.static.star_level = 3
    local star_level = detail.static.star_level
    local data = {
        item = {Icon = detail.static.atlas_id, Quality = detail.static.quality, Num = num, Star = star_level},
        qdi = Constants.InternalImg['icon_di' .. detail.static.quality],
        name = {Text = Util.GetText(detail.static.name), Color = Constants.QualityColor[detail.static.quality]},
        pro = {Text = Constants.ProName[detail.static_equip.profession] or Constants.Text.all_pro},
        part = Constants.EquipPartName[detail.static_equip.equip_pos],
        lv = {Text = Util.Format1234(Constants.Text.equip_lv_format, detail.static.level_limit)},
        score = Util.GetText('common_score') .. '  ' .. (detail.score > 0 and detail.score or '???')
    }

    if detail.static.star_level >= 0 then
        data.star = {Text = Constants.StarLevel[detail.static.star_level], Color = Constants.StarQuality[detail.static.star_level]}
    end
    if detail.static.level_limit > DataMgr.Instance.UserData.Level then
        data.lv.Color = Constants.Color.detail_limit_red
    else
        data.lv.Color = Constants.Color.detail_normal
    end

    if detail.static_equip.profession ~= 0 and detail.static_equip.profession ~= DataMgr.Instance.UserData.Pro then
        data.pro.Color = Constants.Color.detail_limit_red
    else
        data.pro.Color = Constants.Color.detail_normal
    end
    if compare then
        local arrow
        print('compare score', detail.score, compareDetail and compareDetail.score)
        if not compareDetail or detail.score > compareDetail.score then
            arrow = Constants.InternalImg.arrow_up
        elseif detail.score < compareDetail.score then
            arrow = Constants.InternalImg.arrow_down
        end
        data.arrow = arrow
    end
    local ret = DynamicUIGenerater.create_template(temp, data)
    return ret
end

local function CreateEquipContent(detail, compare, compareDetail, w)
    local temp = Dynamic_Equip.CreateContent(w)

    local strengthen

    local base_attr
    local extra_attr
    local gem_attr_value
    local gem_attr_title
    local gem_slot = {}
    local gem_slotlock = {}
    local has_gem_attr
    for _, v in ipairs(detail.dynamicAttrs or {}) do
        if v.Tag == 'FixedAttributeTag' then
            base_attr = base_attr or {}
            local attr_name, v_str, basevalue = ItemModel.GetAttributeString(v, true)
            local attr_name, v_str = ItemModel.GetAttributeString(v)
            local tmp = {
                attrname = {Text = string.format('%s:', attr_name), Color = rgb},
                attr = {Text = string.format('+%s', v_str), Color = rgb},
                arrow = compare and GetArrow(v, compareDetail) or nil,
                img = Constants.InternalImg.detail_gray_point
            }
            if v.BaseValue and v.Value then
                local attr_name, v_str = ItemModel.GetAttributeString(v, true)
                tmp.appendvalue = string.format('(+%s)', v_str)
            end
            table.insert(base_attr, tmp)
        elseif v.Tag == 'ExtraAttributeTag' then
            extra_attr = extra_attr or {}
            local attr_name, v_str = ItemModel.GetAttributeString(v)
            local rgb = ItemModel.GetAttributeColorRGB(v)
            table.insert(
                extra_attr,
                {
                    attrname = {Text = string.format('%s:', attr_name), Color = rgb},
                    attr = {Text = string.format('+%s', v_str), Color = rgb},
                    img = Constants.InternalImg.detail_gray_point
                }
            )
        elseif v.Tag == 'GridGemAttributeTag' then
            has_gem_attr = true
        end
    end

    if has_gem_attr then
        for i = 1, 3 do
            gem_slot[i] = Constants.InternalImg.detail_gem_frame
            local slot = detail.gem.Slots[i]
            if detail.selfEquiped and ItemModel.IsGemSlotLocked(i) then
                gem_slotlock[i] = {Img = Constants.InternalImg.detail_gem_lock, X = 7, Y = 5}
            elseif slot.GemTemplateID > 0 then
                local gem_info = GlobalHooks.DB.Find('equip_gem', {item_id = slot.GemTemplateID})
                if gem_info.gem_type == 2 then
                    -- 多彩宝石，头都，替换detail_gem_fill
                    gem_slotlock[i] = {Img = Constants.InternalImg.detail_gem_fill, X = 6, Y = 3}
                else
                    gem_slotlock[i] = {Img = Constants.InternalImg.detail_gem_fill, X = 6, Y = 3}
                end
            end
        end
        gem_attr_title = Constants.Text.detail_title_gem
        if detail.gem.Attrs and #detail.gem.Attrs > 0 then
            for _,v in ipairs(detail.gem.Attrs) do
                gem_attr_value = gem_attr_value or {}
                local attrName, value = ItemModel.GetAttributeString(v)
                table.insert(gem_attr_value,{img = Constants.InternalImg.detail_gray_point,attrdesc = attrName .. '+' .. value})
            end
        end
    end

    if detail.refine then
        strengthen = Util.Format1234(Constants.Text.detail_strengthen_format, detail.refine.Rank, detail.refine.Lv)
    end
    local data = {
        base_attr = base_attr ~= nil,
        base_attr_array = base_attr,
        strengthen = strengthen,
        extra_attr = extra_attr ~= nil,
        extra_attr_array = extra_attr,
        gem_attr = gem_attr_value ~= nil,
        gem_attr_value = gem_attr_value,
        gem_attr_title = gem_attr_title,
        gem_slot1 = gem_slot[1],
        gem_slot2 = gem_slot[2],
        gem_slot3 = gem_slot[3],
        gem_slotlock1 = gem_slotlock[1],
        gem_slotlock2 = gem_slotlock[2],
        gem_slotlock3 = gem_slotlock[3],
    }
    local desc_static = GetLangText(detail.static.desc)
    if not string.IsNullOrEmpty(desc_static) then
        data.end_line = Constants.InternalImg.split_line
        data.desc = string.format("<f color='%x' size='20'>%s</f>", 0xfffff38b, desc_static)
    end
    -- print('desc',detail.static.desc)
    -- print('extra_attr ', PrintTable(extra_attr))

    local ret = DynamicUIGenerater.create_template(temp, data)
    return ret
end

-- {{DownImg=,Img=,cb=},...}
local function CreateButtons(self, btns)
    self.comps.btn_1.Visible = false
    self.comps.btn_2.Visible = false
    self.comps.btn_3.Visible = false
    self.comps.btn_4.Visible = false
    self.comps.btn_5.Visible = false
    self.comps.tbt_more.Visible = false
    self.comps.tbt_more.IsChecked = false
    self.comps.cvs_more.Visible = false
    local nobutton = not btns or #btns == 0
    if nobutton then
        self.comps.cvs_end.Visible = false
        self.comps.sp_oar.Height = self.comps.cvs_itemtips.Height - self.comps.sp_oar.Y - 10
    else
        self.comps.sp_oar.Height = self.comps.cvs_end.Y - self.comps.sp_oar.Y - 10
        self.comps.cvs_end.Visible = true
    end
    if not btns then
        return
    end
    if #btns > 2 then
        self.comps.tbt_more.Visible = true
        self.comps.tbt_more.TouchClick = function(sender)
            self.comps.cvs_more.Visible = sender.IsChecked
        end
    end

    local index = 1
    local ui_index = 1
    while index <= #btns do
        if #btns > 2 and index == 2 then
            ui_index = 3
        end
        local v = btns[index]
        local btn = self.comps['btn_' .. ui_index]
        if v.Img then
            UIUtil.SetImage(btn, v.Img)
        end
        if v.DownImg then
            local ly = UIUtil.GetLayout(v.DownImg)
            btn.LayoutDown = ly
        elseif v.Img then
            local ly = UIUtil.GetLayout(v.Img)
            btn.LayoutDown = ly
        end

        if v.Text then
            btn.Text = v.Text
        end
        btn.IsGray = false == v.Enable
        btn.Enable = not btn.IsGray
        btn.TouchClick = function(sender) 
            if v.cb then
                v.cb(self)
            end
        end
        btn.Visible = true
        index = index + 1
        ui_index = ui_index + 1
    end
end

local function OnGlobalTouchUp(self, gameobject, point)
    if not UGUIMgr.CheckInRect(self.ui.comps.cvs_itemtips.Transform, point, true) then
        self.ui.menu:Close()
    end
end

function _M.OnExit(self, itdata)
    if self.begin then
        self.begin.node:RemoveFromParent(true)
    end

    if self.content then
        self.content.node:RemoveFromParent(true)
    end
    if self.globalTouchKey then
        GameGlobal.Instance.FGCtrl:RemoveGlobalTouchUpHandler(self.globalTouchKey)
        self.globalTouchKey = nil
    end
end

function _M.OnInit(self)
    self.ui.menu.ShowType = UIShowType.Cover
    self.src_size = self.comps.cvs_itemtips.Size2D
    self.src_sp_size = self.comps.sp_oar.Size2D
    self.src_sp_y = self.comps.sp_oar.Y
    self.src_cvs_end_y = self.comps.cvs_end.Y
    self.src_backLayout = self.comps.cvs_itemtips.Layout
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end

function _M.SetButtons(self, btns)
    -- print('SetButtons', #btns)
    CreateButtons(self, btns)
end

local function SetHeight(self, h)
    -- print('set Height', h)
    local added = self.src_size.y - h
    self.comps.sp_oar.Height = self.src_sp_size.y - added
    self.comps.cvs_itemtips.Height = h
    self.comps.cvs_end.Y = self.comps.cvs_itemtips.Height - self.comps.cvs_end.Height
end

local function SetWidth(self, w)
    self.comps.cvs_itemtips.Width = w
    self.comps.cvs_begin.Width = w
    self.comps.sp_oar.Width = w
    self.comps.cvs_end.Width = w
end

local function OnResetData(self, data)
    --SetWidth(self, data.w or self.src_size.x)
    self.data = data
    local begin
    local detail = data.detail

    if detail.static.item_type == Constants.ItemType.Equip and detail.static.sec_type < 50 then
        if detail.static.sec_type == Constants.ItemSecType.FateType then
            begin = CreateFateBegin(detail, data.compare, data.compareDetail, data.count, self.comps.cvs_itemtips.Width, detail.bind)
        else
            begin = CreateEquipBegin(detail, data.compare, data.compareDetail, data.count, self.comps.cvs_itemtips.Width, detail.bind)
        end
    elseif detail.static.item_type == Constants.ItemType.Skill then
        begin = CreateNormalSkillBegin(detail, self.comps.cvs_itemtips.Width)
    else
        begin = CreateNormalBegin(detail, data.count, self.comps.cvs_itemtips.Width, detail.bind)
    end

    local comps = self.ui.comps
    comps.cvs_begin:AddChild(begin.node)
    self.begin = begin

    comps.cvs_begin.Height = begin.node.Height

    comps.sp_oar.Y = comps.cvs_begin.X + comps.cvs_begin.Height

    local content

    if detail.static.item_type == Constants.ItemType.Equip  then
        if detail.static.sec_type == Constants.ItemSecType.FateType then
            content = CreateFateContent(detail, data.compare, data.compareDetail, comps.sp_oar.Width)
        elseif detail.static.sec_type == 98 then
            content = CreateMarryContent(detail, data.count, self.comps.sp_oar.Width)
        elseif detail.static.sec_type == 99 then
            content = CreateMarryContent2(detail, data.count, self.comps.sp_oar.Width)
        else
            content = CreateEquipContent(detail, data.compare, data.compareDetail, comps.sp_oar.Width)
        end
       
    else
        content = CreateNormalContent(detail, data.count, self.comps.sp_oar.Width)
    end

    comps.sp_oar.ContainerPanel.Y = 1
    -- print('content.node.Height', content.node.Height, self.src_sp_size.y)
    local is_max = comps.sp_oar.Y + content.node.Height > self.src_cvs_end_y
    if is_max then
        comps.sp_oar.Height = self.src_cvs_end_y - comps.sp_oar.Y
    else
        comps.sp_oar.Height = content.node.Height
    end

    comps.sp_oar.Scrollable.Scroll.vertical = is_max
    comps.sp_oar:AddNormalChild(content.node)

    self.content = content
    self.count = data.count
    self.index = data.index
    self.detail = data.detail
    self.from = data.form

    SetWidth(self, data.w or self.src_size.x)
    
    if data.autoHeight then
        -- 竖向居中
        --comps.cvs_itemtips.Y = (self.ui.root.Height - comps.cvs_itemtips.Height) * 0.5
        comps.cvs_end.Y = comps.sp_oar.Y + comps.sp_oar.Height
        comps.cvs_itemtips.Height = comps.cvs_end.Y + comps.cvs_end.Height
    elseif data.h then
        SetHeight(self, data.h)
    else
        SetHeight(self, self.src_size.y)
    end

    if data.nobackground then
        comps.cvs_itemtips.Layout = null
    else
        comps.cvs_itemtips.Layout = self.src_backLayout
    end

    if data.IsEquiped then
        self.begin.mapNode.item.IsEquiped = true
    end

    if detail.bind then
        self.begin.mapNode.item.IsBinded = true
    end

    data.x = data.x or (self.ui.root.Width - comps.cvs_itemtips.Width) * 0.5
    data.y = data.y or (self.ui.root.Height - comps.cvs_itemtips.Height) * 0.5

    self:SetPos(data.x, data.y, data.anchor)

    CreateButtons(self, data.buttons)
    
    if data.globalTouch then
        self.ui:EnableTouchFrameClose(false)
        if not self.globalTouchKey then
            self.globalTouchKey =
                GameGlobal.Instance.FGCtrl:AddGlobalTouchUpHandler(
                'UI.ItemDetail',
                function(obj, point)
                    if self.ui.IsRunning then
                        OnGlobalTouchUp(self, obj, point)
                    end
                end
            )
        end
    else
        if self.globalTouchKey then
            GameGlobal.Instance.FGCtrl:RemoveGlobalTouchUpHandler(self.globalTouchKey)
            self.globalTouchKey = nil
        end
    end
end

function _M.Reset(self, data)
    if self.begin then
        self.begin.node:RemoveFromParent(true)
    end

    if self.content then
        self.content.node:RemoveFromParent(true)
    end
    if not data.detail and data.templateID then
        data.detail = ItemModel.GetDetailByTemplateID(data.templateID)
    end
    if data.compare and not data.compareDetail and data.detail.static_equip then
        local d = ItemModel.GetDetailByEquipBagIndex(data.detail.static_equip.equip_pos)
        data.compareDetail = d
        OnResetData(self, data)
    else
        OnResetData(self, data)
    end
end

function _M.OnEnter(self, val)
end

function _M.GetItemShow(self)
    return self.begin.mapNode.item
end

function _M.SetCount(self,count)
    local itshow = self:GetItemShow()
    if itshow then
        itshow.Num = count
    end
    self.count = count
end

function _M.SetPos(self, x, y, anchor)
    if type(anchor) == 'string' then
        local x_anchor, y_anchor = unpack(string.split(anchor, '_'))
        if x_anchor == 'l' then
            self.comps.cvs_itemtips.X = x
        elseif x_anchor == 'c' then
            self.comps.cvs_itemtips.X = x - self.comps.cvs_itemtips.Width * 0.5
        elseif x_anchor == 'r' then
            self.comps.cvs_itemtips.X = x - self.comps.cvs_itemtips.Width
        end

        if y_anchor == 't' then
            self.comps.cvs_itemtips.Y = y
        elseif y_anchor == 'c' then
            self.comps.cvs_itemtips.Y = y - self.comps.cvs_itemtips.Height * 0.5
        elseif y_anchor == 'b' then
            self.comps.cvs_itemtips.Y = y - self.comps.cvs_itemtips.Height
        end
    else
        self.comps.cvs_itemtips.X = x
        self.comps.cvs_itemtips.Y = y
    end
end

return _M
