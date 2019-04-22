local DisplayUtil = require("Logic/DisplayUtil")


local MaxCache = 25
local ScrollPanExt = {}
DisplayUtil.warpOOPSelf(ScrollPanExt)

function ScrollPanExt.new(scrollpan, cell, updateCellFunc)
    local o = {}
    setmetatable(o, ScrollPanExt)
    o:_init(scrollpan, cell, updateCellFunc)
    return o
end



function ScrollPanExt:getCell()
    --print_r("self.getCell",self._cacheCell)
    if #self._cacheCell > 0 then
        local cell = self._cacheCell[1]
        local _,v = table.removeItem(self._cacheCell,cell)
        return v
    else
        return self._cell:Clone()
    end
end
function ScrollPanExt:_init(scrollpan, cell, updateCellFunc)
    self._cell = cell
    --self._cell.Visible = false
    self._scrollpan = scrollpan
    self._cellPos = cell.Transform.localPosition
    self._cellScale = cell.Transform.localScale
    self._gap = self._cell.Y - self._scrollpan.Y
    self._container = scrollpan.Scrollable.Container
    self._updateCell = updateCellFunc
    self._cells = {}
    self._datas = {}
    self._cacheCell = {}

    for i = 1,MaxCache do
        local clonecell = self._cell:Clone()
        self:recycleCell(clonecell)
    end
    --print_r("self._init",self._cacheCell)
end

function ScrollPanExt:resetDatas(list)
    self._datas = {}
    self._cells = {}
    --self._container:RemoveAllChildren(true)
    local num = self._container.NumChildren
    for i = num - 1, 0 ,-1 do
       local cell = self._container:RemoveChildAt(i,false)
       self:recycleCell(cell)
    end
    for i,v in ipairs(list) do
        self:addData(v)
    end
end

function ScrollPanExt:recycleCell(cell)

       --print("recycleCell",cell)
       self._scrollpan.Parent:AddChild(cell)
       cell.Transform.localPosition = self._cellPos
       cell.Transform.localScale = self._cellScale
       cell.UnityObject:SetActive(false)
       --self._scrollpan.AddChild(cell)
       table.insert(self._cacheCell,cell)
end
function ScrollPanExt:addData(data, idx)
    idx = idx or #self._datas + 1
    table.insert(self._datas, idx, data)
    --print('sssss '..self._cell.EditType)
    local cell = self:getCell()
    cell.UnityObject:SetActive(true)
    table.insert(self._cells, idx, cell)
    self._container:AddChild(cell)
    self._updateCell(cell, data)

    self:_adjustFromIdx(idx)
    --print("addData",cell)
end

function ScrollPanExt:getCellByData(data)
    local idx = table.indexOf(self._datas, data)
    return self._cells[idx], idx
end

function ScrollPanExt:removeData(data)
    local idx = table.removeItem(self._datas, data)
    if not idx then return end

    local cell = table.remove(self._cells, idx)
    
    self:recycleCell(cell)
    --cell:RemoveFromParent(true)

    self:_adjustFromIdx(idx)
end

function ScrollPanExt:updateData(data)
    local idx = table.indexOf(self._datas, data)
    if not idx then return end

    local cell = self._cells[idx]
    self._updateCell(cell, data)

    self:_adjustFromIdx(idx)
end


function ScrollPanExt:ResetCellPos(index)
    index = index or 1
    local y = 0
    local num = #self._cells
    local cellheight = 0
    for i = 1, num do
        local cell = self._cells[i]
        cell.Y = y
        cellheight = cell.Height
        if i == index then
            break
        end
        y = y + cell.Height + self._gap
    end


    if (y + cellheight + self._container.Y) >= self._scrollpan.Height or (y + self._container.Y)<=0 then
        if self._scrollpan.Scrollable.Scroll.Container ~= nil then
            self._scrollpan.Scrollable:LookAt(Vector2(0,y), false)
        end
    end
end
function ScrollPanExt:_adjustFromIdx(idx)
    if idx > #self._cells then return end

    local y = 0
   
    if idx > 1 then
        y = self._cells[idx - 1].Y + self._cells[idx - 1].Height + self._gap
    end
    for i = idx, #self._cells do
        local cell = self._cells[i]
        cell.Y = y
        y = y + cell.Height + self._gap
    end

    local size = Vector2(0,0)
    for i = 1, #self._cells do
        local cell = self._cells[i]
        size.x = math.max(size.x, cell.X + cell.Width)
    end
    size.y = self:ConnextHeight()
    self._container.Size2D = size;
end

function ScrollPanExt:ConnextHeight()
    local localheight = 0
    for i = 1, #self._cells do
         local cell = self._cells[i]
        localheight = localheight + cell.Height + self._gap
    end
    return localheight
end


return ScrollPanExt
