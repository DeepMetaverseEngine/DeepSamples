local _M = {}
_M.__index = _M

-- todo xxxxxx 年久失修，勉强能用
local UIUtil = require 'UI/UIUtil'
local Helper = require 'Logic/Helper'

local function node_size(item)
    local ctype = item.node:GetType().FullName
    if (not item.template.W) and (not item.template.H) then
        if ctype == 'DeepCore.Unity3D.UGUIEditor.UI.HZLabel' then
            local w, h = item.node.PreferredSize.x, item.node.PreferredSize.y
            item.node.Size2D = UnityEngine.Vector2(0, 0)
            -- print('HZLabel', item.node.X,item.node.Y,w,h,item.node.Name)
            return w, h
        elseif ctype == 'DeepCore.Unity3D.UGUIEditor.UI.HZTextBoxHtml' or ctype == 'DeepCore.Unity3D.UGUIEditor.UI.HZTextBox' then
            -- local c = item.node.FontColor
            -- c.a = 255
            -- item.node.FontColor = c
            local lb_content = item.node.TextComponent
            -- print('HZTextBox', item.node.X,item.node.Y, lb_content.PreferredSize.x,lb_content.PreferredSize.y,item.node.Name)
            item.node.Height = lb_content.PreferredSize.y
            return lb_content.PreferredSize.x, lb_content.PreferredSize.y
        else
            return item.node.Size2D.x, item.node.Size2D.y
        end
    else
        return item.node.Size2D.x, item.node.Size2D.y
    end
end

-- 返回相对item的(x,y)偏移量
local function calc_pt(item, pos)
    local w, h = node_size(item)
    local padding_x, padding_y = 0, 0
    local off_w, off_h = w, h

    -- print('item ',item.node.Name, item.node.Position2D.x,item.node.Position2D.y,off_w,pos.x)
    local node_x, node_y = item.node.Position2D.x, item.node.Position2D.y
    if not item.template.X then
        node_x = pos.x
    else
        off_w = off_w + (node_x - pos.x)
    end
    if not item.template.Y then
        node_y = pos.y
    else
        off_h = off_h + (node_y - pos.y)
    end
    local direction = 'v'

    if item.parent then
        direction = (item.parent.template.direction or 'v')
    end
    if direction == 'h' then
        off_w = off_w + (item.template.padding or 0)
        pos.x = pos.x + off_w
    else
        --竖向
        off_h = off_h + (item.template.padding or 0)
        pos.y = pos.y + off_h
    end
    -- print('item ',item.node.Name, node_x, node_y)
    item.node.Position2D = UnityEngine.Vector2(node_x, node_y)
    return off_w, off_h
end

local function SetData(con)
    local name = con.node:GetType().FullName
    local data = con.template.data

    if type(data) ~= 'table' then
        if name == 'DeepCore.Unity3D.UGUIEditor.UI.HZLabel' then
            con.template.Text = data
        elseif name == 'DeepCore.Unity3D.UGUIEditor.UI.HZTextBoxHtml' then
            con.template.XmlText = data
        elseif name == 'DeepCore.Unity3D.UGUIEditor.UI.HZTextBox' then
            con.template.XmlText = data
        elseif name == 'DeepCore.Unity3D.UGUIEditor.UI.HZImageBox' then
            con.template.Img = data 
        elseif name == 'DeepCore.Unity3D.UGUIEditor.UI.HZCanvas' then
			con.template.Img = data
        elseif name == 'ItemShow' then
            con.template.IconID = data
        end
    else
        for k, v in pairs(data) do
            --print(name,' ',k,' ',v)
            con.template[k] = v
        end
    end
end

local function isArray(t)
    local count = 0
    for _, __ in pairs(t) do
        count = count + 1
    end
    return (count == #t)
end

local function tryParse(con, k, v)
    --   print('parse',k,v)
    if k == 'FontSize' then
        con.node.FontSize = v
    elseif k == 'Color' then
        -- elseif k == 'BorderColor' then
        --   con.node:SetBorderColor(v)
        con.node.FontColorRGB = v
    elseif k == 'SupportRichtext' then
        con.node.SupportRichtext = v
    elseif k == 'TextAnchor' then
        -- print('set text anchor',v)
        con.node.EditTextAnchor = v
    elseif k == 'W' then
        con.node.Size2D = UnityEngine.Vector2(v, con.node.Size2D.y)
    elseif k == 'H' then
        con.node.Size2D = UnityEngine.Vector2(con.node.Size2D.x, v)
    elseif k == 'Img' then
        local style = con.template.UIStyle or nil
        local clipSize = con.template.ClipSize or 8
        if not style and (con.template.W or con.template.H) then
            style = LayoutStyle.IMAGE_STYLE_BACK_4
        end

        UIUtil.SetImage(con.node, v, false, style, clipSize)
        local s = con.node.Layout.PreferredSize
        local w = con.template.W or s.x
        local h = con.template.H or s.y

        con.node.Size2D = UnityEngine.Vector2(w, h)
    elseif k == 'DownImg' then
        local style = con.template.UIStyle or nil
        local clipSize = con.template.ClipSize or 8
        if not style and (con.template.W or con.template.H) then
            style = LayoutStyle.IMAGE_STYLE_BACK_4
        end
        local lyout = UIUtil.GetLayout(v, style, clipSize)
        con.Node.LayoutDown = lyout
    elseif k == 'HtmlText' then
        con.node.HtmlText = v
    elseif k == 'ContentW' then
        --con.node.TextSprite.Size2D = UnityEngine.Vector2(v, con.node.TextSprite.Size2D.y)
        con.node.Size2D = UnityEngine.Vector2(v, con.node.Size2D.y)
    elseif k == 'padding' then
        --
    elseif k == 'direction' then
        --direction = v
    elseif k == 'id' then
        --data.id = v
    elseif k == 'sub_id' then
        --print(v)
    elseif k == 'data' then
    elseif k == 'UIStyle' then
    elseif k == 'ClipSize' then
    elseif k == 'X' then
        -- print('node x',con.node.Name, v)
        con.node.X = v
    elseif k == 'Y' then
        con.node.Y = v
    elseif type(v) ~= 'function' then
        -- print('tryParse',k,v,con.node.Name)
        con.node[k] = v
    end
end

local function CreateCavas(name)
    local node = HZCanvas()
    node.Name = name
    node.Enable = false
    return node
end

local function recursion(template, data, mapNode)
    local num = 1
    local con = {}
    local pos = {x = 0, y = 0}
    local local_data
    local is_array = false
    if template.id then
        local_data = data[template.id]
        if not local_data then
            num = 0
        elseif type(local_data) == 'table' and isArray(local_data) then
            --数组
            num = #local_data
            is_array = true
        end
    elseif template.sub_id then
        local_data = data[template.sub_id]
        if not local_data then
            num = 0
        end
    end
    --   print('current num', num, template.id)
    --   print_r(data)
    for i = 1, num do
        -- 解析属性
        local node = nil
        local nextCon = {}
        local maxw, maxh, totalw, totalh = 0, 0, 0, 0
        local looptemplate
        local loopdata
        if is_array then
            looptemplate = Helper.copy_table(template)
            loopdata = local_data[i]
        else
            looptemplate = template
            loopdata = local_data
        end

        if type(looptemplate[1]) == 'function' then
            --print('New'..tostring(node.GetClassType()))
            node = template[1]()
            --print(tostring(node:GetClassType()))
            node.Name = template.id or template.sub_id or '-'
            --   print('lllllll',mapNode,template.id, node)
            if mapNode and template.id then
                mapNode[template.id] = node
            end
            looptemplate.data = loopdata
            local class_str = node:GetType().FullName
            if class_str == 'DeepCore.Unity3D.UGUIEditor.UI.HZLabel' then
                node.EditTextAnchor = CommonUI.TextAnchor.L_T
            -- elseif class_str == 'DeepCore.Unity3D.UGUIEditor.UI.HZTextBoxHtml' or
            --		class_str == 'DeepCore.Unity3D.UGUIEditor.UI.HZTextBox' then
            --   node.TextSprite.Anchor = TextAnchor.L_T
            end
        else
            node = CreateCavas(template.id or template.sub_id or '-')
            -- print('Canvas',node.Name)
            if mapNode and template.id then
                mapNode[template.id] = node
            end
        end

        nextCon.node = node
        nextCon.template = looptemplate
        if local_data then
            SetData(nextCon)
        end
        local nodepos = {x = 0, y = 0}
        -- 解析属性
        local count = 0
        -- print_r(nextCon.template)
        for k, v in pairs(nextCon.template) do
            count = count + 1
            if k ~= 'data' and type(v) == 'table' then
                local nextdata

                if v.sub_id and (not v.id) and (not v.data) then
                    -- sub_id 对应的节点data赋值
                    nextdata = loopdata
                else
                    nextdata = data
                end
                local childcon = recursion(v, nextdata, mapNode)
                for _, vv in pairs(childcon) do
                    vv.parent = nextCon
                    node:AddChild(vv.node)
                    local off_w, off_h = calc_pt(vv, nodepos)
                    maxw = (maxw > off_w and maxw) or off_w
                    maxh = (maxh > off_h and maxh) or off_h
                    totalw = totalw + off_w
                    totalh = totalh + off_h
                end
            else
                tryParse(nextCon, k, v)
            end
        end

        if node:GetType().FullName == 'DeepCore.Unity3D.UGUIEditor.UI.HZCanvas' then
            --大小设置
            local nodew, nodeh = maxw, maxh
            if template.direction == nil or template.direction == 'v' then
                nodew, nodeh = maxw, totalh
            else
                nodew, nodeh = totalw, maxh
            end
            node.Size2D = UnityEngine.Vector2(nodew, nodeh)
        --print(node.Name, nodew, nodeh)
        -- elseif node:GetType().FullName == 'DeepCore.Unity3D.UGUIEditor.UI.HZLabel' then
        -- 	if node.EditTextAnchor == TextAnchor.R_T then
        -- 		print('-----iam in')
        -- 		node.X = node.X - node.PreferredSize.x
        -- 	end
        end
        --PtProtect(node)
        table.insert(con, nextCon)
    end
    return con
end

--begin_func需要返回是否创建该的数量
local function create_template(template, data)
    local localT = Helper.copy_table(template)
    local mapNode = {}
    local com = recursion(localT, data, mapNode)
    --   print_r(com)
    if #com == 1 then
        com[1].mapNode = mapNode
        return com[1]
    else
        return nil
    end
end

_M.create_template = create_template
return _M
