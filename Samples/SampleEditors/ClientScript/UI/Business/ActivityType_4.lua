local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local BusinessModel = require 'Model/BusinessModel'

local Checkedindex = nil
local lastClickindex = nil

local function Init3DSngleModel(self, parentCvs, pos2d, scale, rotate,fileName,index)
    local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, rotate,fileName)
    self.models[index] = {}
    self.models[index].info = info
    self.models[index].Visible = true
    return info
end

local function Release3DModel(self,index)
    if self and self.models then
        if self.models[index] then
            UI3DModelAdapter.ReleaseModel(self.models[index].info.Key)
            self.models[index] = nil
        end
    end
end

local function ShowDetail(self,index,data)
    if lastClickindex == index then
        return
    end
    
    local itemdetail = ItemModel.GetDetailByTemplateID(data.show.item[1])
    if itemdetail then
        self.require.item.Text = Util.GetText(itemdetail.static.name)
    end
    self.require.itemnum.Text = data.show.itemnum[1]
    self.requiremin.Text = data.require.minval[1]
    self.requiremax.Text = data.server_variate_num
    self.lb_cur.Text = self.rspdata[index].requireList[1].curVal
    
    for i, v in pairs(self.models) do
        if v.Visible == true then
            v.info.RootTrans.gameObject:SetActive(false);
            v.Visible = false
        end
    end
    if not string.IsNullOrEmpty(data.show_model) then
        self.pic.Layout = nil
        if self.models[index] then
            self.models[index].Visible = true
            self.models[index].info.RootTrans.gameObject:SetActive(true);
        else
            local info = Init3DSngleModel(self, self.model,
                    Vector2.zero,
                    tonumber(data.zoom),
                    self.ui.menu.MenuOrder,
                    data.show_model,
                    index)
            info.Callback = function(model)
                model.DC.localEulerAngles = Vector3(0,data.rotate,0)
            end
        end
    end
    if not string.IsNullOrEmpty(data.show_pic) then
        UIUtil.SetImage(self.pic,data.show_pic)
    end
    Checkedindex = index
end

local function BuildSortData(self)
    table.sort(self.rspdata,function(a,b)
        if a.state ~= 1 and b.state == 1 then
            return false
        elseif a.state == 1 and b.state ~= 1 then
            return true
        else
            return a.id<b.id
        end
    end)
end

local function InitMain(self,node,index,data)
    local lbtext = node:FindChildByEditName('lbtext', true)
    local require1minmax = node:FindChildByEditName('require1minmax', true)
    local btn_get = node:FindChildByEditName('btn_get', true)
    local lb_got = node:FindChildByEditName('lb_got', true)
    local lb_over = node:FindChildByEditName('lb_over', true)


    for i = 2, 5 do
        local cvs_show = node:FindChildByEditName('cvs_show.item['..i..']', true)
        local lb_show = node:FindChildByEditName('lb_show.itemnum['..i..']', true)

        if data.show.item[i] == 0 then
            cvs_show.Visible = false
            lb_show.Visible = false
        else
            local itemdetail = ItemModel.GetDetailByTemplateID(data.show.item[i])
            UIUtil.SetItemShowTo(cvs_show,itemdetail)
            lb_show.Text = data.show.itemnum[i]
            cvs_show.TouchClick = function(sender)
                UIUtil.ShowTips(self,sender,data.show.item[i])
            end
            cvs_show.Visible = true
            lb_show.Visible = true
        end
    end
    
    lbtext.Text = Util.GetText(data.lbtext)
    require1minmax.Text = self.rspdata[index].requireList[2].curVal.."/"..self.rspdata[index].requireList[2].maxVal
    
    btn_get.Enable = self.rspdata[index].state == 1
    btn_get.IsGray = self.rspdata[index].state ~= 1


    if self.rspdata[index].requireList[2].curVal<self.rspdata[index].requireList[2].maxVal then
        lb_over.Visible = false
        btn_get.Visible = true
        if self.rspdata[index].state == 2 then
            lb_got.Visible = true
            btn_get.Visible = false
        else
            lb_got.Visible = false
        end
    else
        btn_get.Visible = false
        lb_got.Visible = false
        lb_over.Visible = true
    end
    

    btn_get.TouchClick = function(sender)
        if self.rspdata[index].state == 1 then
            BusinessModel.RequireGet(self.activitytype,self.activityid ,data.id,function()
                self.rspdata[index].state = 2
                self.rspdata[index].requireList[2].curVal = self.rspdata[index].requireList[2].curVal + 1
                require1minmax.Text = self.rspdata[index].requireList[2].curVal.."/"..self.rspdata[index].requireList[2].maxVal
                BuildSortData(self)
                self.sp_list:RefreshShowCell()
                self.parentui.pan:RefreshShowCell()
            end,self)
        end
    end
    
    node.TouchClick = function(sender)
        ShowDetail(self,index,data)
        lastClickindex = index
    end
end

local function StartSetUI(self)
    ShowDetail(self,1,self.rspdata[1].staticdata)
    UIUtil.ConfigVScrollPan(self.sp_list,self.cvs_focus, #self.rspdata, function(node, index)
        InitMain(self,node,index,self.rspdata[index].staticdata)
    end)
    self.cvs_focus.Visible = false
end

function _M.OnEnter( self,activityid,subindex )
    self.models = {}
    self.isopen = true
    if not self.staticdata then
        self.staticdata = BusinessModel.GetCommonActivity("level")
    end
    self.activityid = activityid
    self.subindex = subindex

    BusinessModel.RequireData(self.activitytype,self.activityid,function(rsp)
        self.parentui.pan:RefreshShowCell()
        local temptable = {}
        for _,v in pairs(rsp.activityMap) do
            table.insert(temptable,v)
        end
        table.sort(temptable,function( a,b)
            return a.id < b.id
        end)
        for i, v in pairs(temptable) do
            v.staticdata = self.staticdata[i]
        end
        self.rspdata = temptable
        BuildSortData(self)
        StartSetUI(self)
    end)
end

function _M.OnInit( self,params )
    self.activitytype = params.activitytype
    self.staticdata = params
    self.cvs_show = self.root:FindChildByEditName('cvs_show', true)
    self.cvs_info = self.root:FindChildByEditName('cvs_info', true)
    
    
    self.require = {}
    self.require.maxval = {}
    self.require.minval = {}
    self.require.item = self.cvs_show:FindChildByEditName('lb_show.item[1]', true)
    self.require.itemnum = self.cvs_show:FindChildByEditName('lb_show.itemnum[1]', true)
    self.show = {}
    self.pic = self.cvs_show:FindChildByEditName('ib_showpic[1]', true)
    self.model = self.cvs_show:FindChildByEditName('cvs_showmodel[1]', true)

    self.requiremax = self.cvs_show:FindChildByEditName('require.maxval[1]', true)
    self.requiremin = self.cvs_show:FindChildByEditName('require.minval[2]', true)
    self.lb_cur = self.cvs_info:FindChildByEditName('require.key[2]', true)


    self.sp_list = self.cvs_info:FindChildByEditName('sp_list', true)
    self.cvs_focus = self.cvs_info:FindChildByEditName('cvs_focus', true)
    self.require.key = self.cvs_info:FindChildByEditName('require.key[2]', true)
    
end

function _M.OnExit( self )
    for i, v in pairs(self.models) do
        Release3DModel(self,i)
    end
    Checkedindex = nil
    lastClickindex = nil
    GlobalHooks.UI.SetRedTips(BusinessModel.GetRedKey(self.activitytype),BusinessModel.cachedata[self.activitytype][self.activityid].count,self.activityid)
end

return _M