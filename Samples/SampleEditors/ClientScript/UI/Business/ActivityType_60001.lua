---date:2019.01.18
---author:任祥建
---scriptname:ChocolateMain
local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local BusinessModel = require 'Model/BusinessModel'
local ActivityModel = require 'Model/ActivityModel'


local effects = {}

local function LoadEffect(self,pan,node,filename,pos,cb)
    local param =
    {
        Pos = pos,
        Parent = node.UnityObject.transform,
        LayerOrder = self.ui.menu.MenuOrder,
        Scale = Vector3(1.85,1.85,1),
        UILayer = true,
        Vectormove = {x = node.Size2D[1],  y = node.Size2D[2]}
    }

    return Util.PlayEffect(filename,param,true,cb)
end

local function UnLoadEffect(id)
    RenderSystem.Instance:Unload(id)
end

local function RequireGet(self,btn ,activityid ,subid ,setvisable ,index,itemnum,node,cb)
    ActivityModel.BagIsCanUse(itemnum,function()
        BusinessModel.RequireGet(self.activitytype,activityid ,subid,function(rsp)
            if rsp.s2c_code == 200 then
                self.rspdata[index].state = 2
                if setvisable then
                    setvisable.Visible = true
                end
                if cb then
                    cb()
                end
            end
        end,self)
    end)
end

local function ShowCanGetEffevt(self,shownode,state,index)
    if effects[index] then
        UnLoadEffect(effects[index])
        effects[index] = nil
    end
    if state == 1 then
        effects[index] = LoadEffect(
                self,
                shownode.Parent,
                shownode,
                "/res/effect/ui/ef_ui_yigui_upgrade.assetbundles",
                Vector3(58,-58,0))
    end
end

local function SetUI(self,data)
    for i = 1, #data do
        local lbtext = self.cvs[i]:FindChildByEditName('lb_text'..i, true)
        local btn_get = self.cvs[i]:FindChildByEditName('btn_get'..i, true)
        local lb_got = self.cvs[i]:FindChildByEditName('lb_go'..i, true)

        lb_got.Visible = data[i].state == 2
        btn_get.IsGray = data[i].state == 2

        lbtext.Text = Util.GetText(data[i].static.lbtext)
        ShowCanGetEffevt(self,btn_get,data[i].state,i)

        btn_get.LayoutDown = nil
        btn_get.TouchClick = function(sender)
            if data[i].state == 1 then
                RequireGet(self, btn_get,self.activityid,data[i].id,lb_got,i,1,self.cvs[i],function()
                    data[i].state = 2
                    ShowCanGetEffevt(self,sender,data[i].state,i)
                    btn_get.IsGray = true
                end)
            end
            GlobalHooks.UI.OpenUI('PreviewItem', 0,{id = data[i].static.show.item,num = data[i].static.show.itemnum})
        end
    end
end

function _M.OnEnter(self,activityid,subindex)
    self.activityid = activityid
    BusinessModel.RequireData(3,activityid,function (rsp)
        local temptable = {}
        for _,v in pairs(rsp.activityMap) do
            for _, v2 in ipairs(self.staticdata) do
                if v.id == v2.id then
                    v.static = v2
                    break
                end
            end
            table.insert(temptable,v)
        end
        table.sort(temptable,function( a,b)
            return a.id < b.id
        end)
        self.rspdata = temptable
        SetUI(self,self.rspdata)
    end)


end

function _M.OnInit( self,params )
    self.activitytype = params.activitytype
    self.staticdata = params

    local cvs_reward = self.root:FindChildByEditName('cvs_reward', true)
    self.cvs = {}
    for i = 1, 100 do
        local tempnode = cvs_reward:FindChildByEditName('cvs_reward'..i, true)
        if tempnode then
            self.cvs[i] = tempnode
        else
            break
        end
    end


    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end

function _M.OnExit( self )
    for _, v in pairs(effects) do
        if v then
            UnLoadEffect(v)
            v = nil
        end
    end
    effects = {}
end
return _M
