local _M = {}
_M.__index = _M
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local function OnGlobalTouchUp(self, gameobject, point)
    if not UGUIMgr.CheckInRect(self.ui.comps.cvs_itemget.Transform, point, true) then
        self.ui.menu:Close()
    end
end


function _M.OnExit(self)
   
    if self.globalTouchKey then
        GameGlobal.Instance.FGCtrl:RemoveGlobalTouchUpHandler(self.globalTouchKey)
        self.globalTouchKey = nil
    end
end

function _M.OnInit(self)
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.tempnode = self.ui.comps.cvs_function
    self.tempnode.Visible = false
    self.pan = self.ui.comps.sp_list
    self.orgTotalheight = self.ui.comps.cvs_itemget.Height
    self.tempheight =  self.tempnode.Height
    self.orgbackheight =  self.ui.comps.ib_listback.Height
    self.orginfoheight =  self.ui.comps.ib_infoback.Height
    
end

local function RefreshCellData(self, node, index)
    node.Position2D={4.2,5+(index-1)*node.Size2D.y}
    local lb_go = UIUtil.FindChild(node, 'lb_go', true)
    local lb_funname = UIUtil.FindChild(node, 'lb_funname', true)
    local cvs_icon = UIUtil.FindChild(node, 'cvs_icon', true)
    local ib_lock = UIUtil.FindChild(node, 'ib_lock', true)
    local ib_go = UIUtil.FindChild(node, 'ib_go', true)
    local function_id = self.listData[index]
    local issucc,result = FunctionUtil.CheckItemSourceRequire(function_id)
    local functiondata = FunctionUtil.GetSourceData(function_id)
    if issucc then
        ib_lock.Visible = false
        ib_go.Visible = true
        lb_go.Enable = true
        lb_go.IsInteractive = true
        lb_go.Text = Constants.Text.goto
        lb_go.event_PointerClick = function(sender)
        SoundManager.Instance:PlaySoundByKey('button',false)
            if FunctionUtil.CheckNowIsOpen(function_id,true) then
                -- if self.callback ~= nil then
                --     self.callback()
                --     self.callback = nil
                -- end
                self.ui.menu:Close()
                FunctionUtil.OpenFunction(function_id,false,{arg = {self.TemplateId}})
            end
        end
    else
        ib_lock.Visible = true
        ib_go.Visible = false
        lb_go.Enable = false
        lb_go.IsInteractive = false
        lb_go.Text = result
    end
    lb_funname.Text = Util.GetText(functiondata.function_name)
   
    if not string.IsNullOrEmpty(functiondata.icon) then
         UIUtil.SetImage(cvs_icon, functiondata.icon)
    end
   
end
function _M.SetFrameHeight(self,num)
    local fix = 4
    if num == 3 then
        fix = 0
    end
    self.ui.comps.cvs_itemget.Height = self.orgTotalheight - (self.tempheight*(3- num)) + fix
    self.ui.comps.ib_listback.Height = self.orgbackheight - (self.tempheight*(3- num)) + fix
    self.ui.comps.ib_infoback.Height = self.orginfoheight - (self.tempheight*(3- num)) + fix
    self.pan.Height = self.orgbackheight - (self.tempheight*(3- num)) + fix
end
function _M.Reset(self, data)
    local x = data.x or (self.ui.root.Width*0.5-  self.ui.comps.cvs_itemget.Width*0.5)
    local y = data.y or (self.ui.root.Height*0.5 -  self.ui.comps.cvs_itemget.Height*0.5)
   
    local itemdetail=   data.detail or ItemModel.GetDetailByTemplateID(data.TemplateId)
    local quality = itemdetail.static.quality
    local icon= itemdetail.static.atlas_id
    local node = self.ui.comps.cvs_item
    local itshow=UIUtil.SetItemShowTo(node,icon,quality,1)
    local tb_des = self.ui.comps.tb_des
    local lb_name = self.ui.comps.lb_name
     local lb_none = self.ui.comps.lb_none
    lb_name.Text = Util.GetText(itemdetail.static.name)
    tb_des.Text = Util.GetText(itemdetail.static.using_desc)
    itshow.EnableTouch = false
    --self.callback = data.cb or nil
    self.TemplateId = itemdetail.static.id
    self.listData = {}
    for i,v in ipairs(itemdetail.static.source) do
        if not string.IsNullOrEmpty(v) then
            table.insert(self.listData,v)
        end
    end
    local height = self.tempheight
    local num = 1
    if #self.listData == 0 then
        lb_none.Visible = true
        --UnityEngine.Debug.LogError(string.format("can not found itemsource by item %d ", data.TemplateId))
    else
        num = math.min(3,#self.listData )
        lb_none.Visible = false
    end
    _M.SetFrameHeight(self,num)
    UIUtil.ConfigVScrollPan(self.pan, self.tempnode, #self.listData, function(node, index)
        RefreshCellData(self, node, index)
    end)
    self:SetPos(x,y,data.anchor)
end

function _M.SetPos(self, x, y, anchor)
    if anchor ~= nil and type(anchor) == 'string' then
        local x_anchor, y_anchor = unpack(string.split(anchor, '_'))
        if x_anchor == 'l' then
            self.comps.cvs_itemget.X = x
        elseif x_anchor == 'c' then
            self.comps.cvs_itemget.X = x - self.comps.cvs_itemget.Width * 0.5
        elseif x_anchor == 'r' then
            self.comps.cvs_itemget.X = x - self.comps.cvs_itemget.Width
        end

        if y_anchor == 't' then
            self.comps.cvs_itemget.Y = x
        elseif y_anchor == 'c' then
            self.comps.cvs_itemget.Y = y - self.comps.cvs_itemget.Height * 0.5
        elseif y_anchor == 'b' then
            self.comps.cvs_itemget.Y = y - self.comps.cvs_itemget.Height
        end
    else
        self.comps.cvs_itemget.X = x
        self.comps.cvs_itemget.Y = y
    end
    self.comps.cvs_itemget.X = math.max(0,self.comps.cvs_itemget.X)
    self.comps.cvs_itemget.X = math.min(HZUISystem.SCREEN_WIDTH - self.comps.cvs_itemget.Width,self.comps.cvs_itemget.X)

    self.comps.cvs_itemget.Y = math.max(0,self.comps.cvs_itemget.Y)
    self.comps.cvs_itemget.Y = math.min(HZUISystem.SCREEN_HEIGHT - self.comps.cvs_itemget.Height,self.comps.cvs_itemget.Y)

end
function _M.OnEnter(self)
end


return _M
