local ItemModel = require("Model/ItemModel")

local DisplayUtil = {}
local UIUtil = require 'UI/UIUtil'
-- uis {oldName, newName or stores}
function DisplayUtil.storeUI(root, self, uis)
    for _,names in ipairs(uis) do
        local comp = root:FindChildByEditName(names[1], true)
        if comp then
            local store = names[3]
            local newName = names[2]
            if type(newName) == 'table' then
                store = newName
                newName = nil
            end
            if type(store) == 'table' then
                if newName then
                    store[newName] = comp
                else
                    table.insert(store, comp)
                end
            else
                self[newName or names[1]] = comp
            end
            -- key/value 的 属性
            for k,v in pairs(names) do
                if type(k) == "string" then
                    comp[k] = v
                end
            end
        end
    end
end

local function getV(ClassTable, t, key)
    if getmetatable(ClassTable) == ClassTable then
        return rawget(ClassTable, key)
    end
    return ClassTable[key]
end

function DisplayUtil.warpOOPSelf(ClassTable)
    ClassTable.__index = function (t, key)
        local v = getV(ClassTable, t, key)
        if v ~= nil then return v end

        if string.sub(key, 1, 6) == '_self_' then
            local funcName = string.sub(key, 7)
            local func = t[funcName]
            if type(func) == "function" then
                local newfunc = function(...) return func(t, ...) end
                t[key] = newfunc
                return newfunc
            end
        end
    end
end

function DisplayUtil.setHead(img, headId, defaultPath)
    if headId then
        img.Layout = CommonFunc.drawHead(headId)
    elseif defaultPath then
        DisplayUtil.setImg(img, defaultPath)
    end
end

local smallProIndexs = {23, 22, 20, 21}
function DisplayUtil.setSmallPro(img, pro)
    DisplayUtil.setImg(img, '#fuben_fox/output/fuben.xml|fuben_cbb|' .. smallProIndexs[pro])
end

function DisplayUtil.setImg(node, path, resize, style, clipsize)
    -- print('setImg', path)
    style = style or (node.Layout and node.Layout.Style) or UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER
    clipsize = clipsize or 8
    node.Layout = HZUISystem.CreateLayout(path, style, clipsize);
    if resize then 
        node.Size2D = node.Layout.PreferredSize
    end
end


function DisplayUtil.fillAwards(icons, items,cb,closecb)
    for i,v in ipairs(icons) do
        DisplayUtil.fillAward(v, items[i],cb,closecb)
    end
end
-- icon DisplayNode
-- item [id, num]
function DisplayUtil.fillAward(icon, item,cb,closecb)
    local id = item and item[1] or 0
    icon.Visible = id ~= 0
    if id ~= 0 then
        local NotEnough = item.NotEnough or false
        local detail = ItemModel.GetDetailByTemplateID(id)
        --local itemShow = GameUtil.GetOrCreateItemShow(icon)
        if detail and detail.static then
            local itshow = UIUtil.SetItemShowTo(icon,detail.static.atlas_id,detail.static.quality, item[2] or 1)
            itshow.EnableTouch = true
            itshow.TouchClick = function()
                if not NotEnough then
                    local m = UIUtil.ShowNormalItemDetail({detail = detail, itemShow = itshow, autoHeight = true})
                    if cb then
                        cb(icon,m)
                    end
                else
                    local params = {TemplateId = id,detail = ItemModel.GetDetailByTemplateID(id),cb = closecb}
                    local m = UIUtil.ShowGetItemWay(params)
                    if not m then
                         UnityEngine.Debug.LogError(string.format("no itemsource data%d", id))
                         return
                    end
                    if cb then
                        cb(icon,m)
                    end
                end
            end
        else
            UnityEngine.Debug.LogError(string.format("can not found static by item %d", id))
        end
    end
end

function DisplayUtil.setItemImg(img, itemId, numLabel, num)
    local detail = ItemModel.GetDetailByTemplateID(itemId)
    DisplayUtil.setImg(img, detail.static.atlas_id)
    if numLabel then
        num = num or 1
        numLabel.Visible = num > 1
        numLabel.Text = tostring(num)
    end
end

function DisplayUtil.clearEffects(t)
    if t then
        for k,v in pairs(t) do
            FuckAssetObject.Unload(v)
            t[k] = nil
        end
    end
end

local defaultParams = {
    pos = Vector3(0, 0, 0),
    scale = Vector3(1, 1, 1),
    rotation = Vector3(0, 0, 0),
    layer = 5,
    localDepth = 1,
    center = true,
}

function DisplayUtil.loadEffect(path, parent, params)
    params = params or {}
    local pos = params.pos or defaultParams.pos
    local offsetPos = params.offsetPos
    local scale = params.scale or defaultParams.scale
    local rotation = params.rotation or defaultParams.rotation
    local localDepth = params.localDepth or defaultParams.localDepth
    local layer = params.layer or defaultParams.layer
    local stores = params.stores
    local cb = params.cb
    local autoRemove = params.autoRemove

    local isCenter = defaultParams.center
    if params.center ~= nil then
        isCenter = params.center
    end
    if isCenter then
        pos = parent.Size2D * 0.5
    end
    if offsetPos then
        pos = Vector2(pos.x + offsetPos.x, pos.y + offsetPos.y)
    end
    local arr = string.split(path, '/')
    local name = arr[#arr]
    arr = string.split(name, '.')
    if #arr > 1 then table.remove(arr) end
    name = table.concat(arr, '.')
    -- local loader = AssetBoundleLoader()
    FuckAssetObject.GetOrLoad(path, name, function(loader)
        if not loader then return end
        local gameobj = loader.gameObject
        UILayerMgr.SetLayer(gameObject, layer)
        gameobj.transform.parent = parent.Transform
        gameobj.transform.localPosition = Vector3(pos.x, -pos.y, 0) 
        gameobj.transform.localScale = scale 
        gameobj.transform.localRotation = rotation
        if autoRemove then
            gameobj:AddComponent(AutoRemoveAnimation)
        end
        if localDepth then
            UILayerMgr.SetLocalLayerOrder(gameobj, localDepth, false, 5)
        end
        if stores then
            if params.storeKey then
                stores[params.storeKey] = gameobj
            else
                table.insert(stores, gameobj)
            end
        end
        if cb then cb(gameobj) end
    end)
end

function DisplayUtil.adaptiveFullSceen(node)
    -- local bgSize = bg.Size2D
    -- local scaleX = NewUGuiManger.Instance.ScreenWidth / bgSize.x
    -- local sacelY = NewUGuiManger.Instance.ScreenHeight / bgSize.y
    -- local scale = math.max(scaleX, sacelY)
    -- if scale ~= 1 then
    --     scale = scale + 0.01
    -- end
    -- bg.Scale = Vector2(scale, scale)
    -- bg.Position2D = Vector2(-0.5 * scale *bgSize.x, -0.5 * scale * bgSize.y)
    local root = HZUISystem.Instance.RootRect
    local scale = root.width > HZUISystem.SCREEN_WIDTH and root.width / HZUISystem.SCREEN_WIDTH or root.height / HZUISystem.SCREEN_HEIGHT
            
    local mMaskW = node.Width * scale;
    local mMaskH = node.Height * scale;

    local mMaskOffsetX = (HZUISystem.SCREEN_WIDTH - mMaskW) * 0.5
    local mMaskOffsetY = (HZUISystem.SCREEN_HEIGHT - mMaskH) * 0.5

    node.Position2D = Vector2(mMaskOffsetX, mMaskOffsetY);
    node.Size2D = Vector2(mMaskW, mMaskH)
end

function DisplayUtil.adaptiveFullSceenX(node)
 
    local root = HZUISystem.Instance.RootRect
    local scale = root.width > HZUISystem.SCREEN_WIDTH and root.width / HZUISystem.SCREEN_WIDTH or root.height / HZUISystem.SCREEN_HEIGHT
            
    local mMaskW = node.Width * scale;
     
    local mMaskOffsetX = (HZUISystem.SCREEN_WIDTH - mMaskW) * 0.5
 
    node.X = mMaskOffsetX;
    node.Scale = Vector2(scale,1)
end


-- uinode
-- align TL TR BL BR CL CR TC BC
function DisplayUtil.adaptiveUI(uinode, align)
    local offsetX = (NewUGuiManger.Instance.ScreenWidth - 1136) / 2
    local offsetY = (NewUGuiManger.Instance.ScreenHeight - 640) / 2
    local pos = uinode.Position2D
    local alignX = string.sub(align, 2, 2)
    local alignY = string.sub(align, 1, 1)
    if alignX == 'L' then
        pos.x = pos.x - offsetX
    elseif alignX == 'R' then
        pos.x = pos.x + offsetX
    end
    if alignY == 'T' then
        pos.y = pos.y - offsetY
    elseif alignY == 'B' then
        pos.y = pos.y + offsetY
    end
    uinode.Position2D = pos
end

function DisplayUtil.lookAt(scrollPan, luaIdx)
    local cols = scrollPan.Columns
    local gx = (luaIdx - 1) % cols
    local gy = math.floor((luaIdx - 1) / cols)
    local scrollable = scrollPan.Scrollable
    local cellSize = scrollable.CellSize
    local border = scrollable.Border
    local gap = scrollable.Gap
    local scrollPanSize = scrollPan.Size2D
    local x = border.x + gx * (cellSize.x + gap.x) + cellSize.x
    local y = border.y + gy * (cellSize.y + gap.y) + cellSize.y
    scrollable:LookAt(Vector2(x - scrollPanSize[1] / 2, y - scrollPanSize[2] / 2))
end

function DisplayUtil.getCell(scrollPan, luaIdx)
    local cols = scrollPan.Columns
    local gx = (luaIdx - 1) % cols
    local gy = math.floor((luaIdx - 1) / cols)
    return scrollPan.Scrollable:GetCell(gx, gy)
end


return DisplayUtil
