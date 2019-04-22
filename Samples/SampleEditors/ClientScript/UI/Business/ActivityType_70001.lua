local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local BusinessModel = require 'Model/BusinessModel'

local Checkedindex = nil
local lastClickindex = nil
local effc = nil

local function LoadEffect(self,pan,node,filename,pos,cb)
    local param =
    {
        Pos = pos,
        Clip = pan.Transform,
        Parent = node.UnityObject.transform,
        LayerOrder = self.ui.menu.MenuOrder,
        Scale = Vector3.one,
        UILayer = true,
        Vectormove = {x = node.Size2D[1],  y = node.Size2D[2]}
    }

    return Util.PlayEffect(filename,param,true,cb)
end

local function UnLoadEffect(id)
    RenderSystem.Instance:Unload(id)
end

local function ShowDetail(self,index,data)
    if lastClickindex == index then
        return
    end

    for _, v in pairs(self.models) do
        if v.Visible == true then
            v.info.RootTrans.gameObject:SetActive(false);
            v.Visible = false
        end
    end

    if effc then
        UnLoadEffect(effc)
        effc = nil
    end
    
    if not string.IsNullOrEmpty(data.show_model) then
        --self.pic.Layout = nil
        effc = LoadEffect(self,self.model.Parent,self.model,data.show_model,Vector3(0,0,0))
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
    local lbtext = node:FindChildByEditName('lb_req', true)
    local require1minmax = node:FindChildByEditName('lb_reqnum', true)
    local btn_get = node:FindChildByEditName('btn_get', true)
    local cvs_reward = node:FindChildByEditName('cvs_reward', true)
    local lb_condition = node:FindChildByEditName('lb_condition', true)
    
    
    lbtext.Text = Util.GetText(data.static.lbtext)
    require1minmax.Text = data.requireList[1].curVal.."/"..data.requireList[1].minVal

    btn_get.Enable = data.state == 1
    btn_get.IsGray = data.state ~= 1
    UIUtil.SetItemShowTo(cvs_reward,data.static.show.item[1],data.static.show.itemnum[1])

    if data.state == 1 then
        btn_get.Visible = true
        btn_get.IsGray = false
        btn_get.Enable = true
    elseif data.state == 0 then
        btn_get.Visible = true
        btn_get.IsGray = true
        btn_get.Enable = false
    elseif data.state == 2 then
        btn_get.Visible = false
    end

    lb_condition.Visible = data.state == 2


    btn_get.TouchClick = function(sender)
        if data.state == 1 then
            BusinessModel.RequireGet(self.activitytype,self.activityid ,data.id,function()
                data.state = 2
                BuildSortData(self)
                self.sp_list:RefreshShowCell()
            end,self)
        end
    end

    cvs_reward.TouchClick = function(sender)
        UIUtil.ShowTips(self,sender,data.static.show.item[1])
    end

    node.TouchClick = function(sender)
        ShowDetail(self,index,data)
        lastClickindex = index
    end
end

local function StartSetUI(self)
    ShowDetail(self,1,self.rspdata[1].static)
    UIUtil.ConfigVScrollPan(self.sp_list,self.cvs_reqlist, #self.rspdata, function(node, index)
        InitMain(self,node,index,self.rspdata[index])
    end)
    self.cvs_reqlist.Visible = false
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
        local temptable = {}
        for _,v in pairs(rsp.activityMap) do
            table.insert(temptable,v)
        end
        table.sort(temptable,function( a,b)
            return a.id < b.id
        end)
        for i, v in pairs(temptable) do
            v.static = self.staticdata[i]
        end
        self.rspdata = temptable
        BuildSortData(self)
        StartSetUI(self)
    end)

    self.btn_relationship.TouchClick = function(sender)
        FunctionUtil.OpenFunction("socialrelation")
    end
end

function _M.OnInit( self,params )
    self.activitytype = params.activitytype
    self.staticdata = params
    self.cvs_main = self.root:FindChildByEditName('cvs_main', true)
    
    self.pic = self.cvs_main:FindChildByEditName('ib_desc1', true)
    self.model = self.cvs_main:FindChildByEditName('cvs_anchor', true)

    self.sp_list = self.cvs_main:FindChildByEditName('sp_reqlist', true)
    self.cvs_reqlist = self.cvs_main:FindChildByEditName('cvs_reqlist', true)
    
    self.btn_relationship = self.cvs_main:FindChildByEditName('btn_relationship', true)

    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end

function _M.OnExit( self )
    if effc then
        UnLoadEffect(effc)
        effc = nil
    end
    Checkedindex = nil
    lastClickindex = nil
    GlobalHooks.UI.SetRedTips(BusinessModel.GetRedKey(self.activitytype),BusinessModel.cachedata[self.activitytype][self.activityid].count,self.activityid)
end

return _M