---date:2019.01.18
---author:任祥建
---scriptname:CPActivityFrame
local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local BusinessModel = require 'Model/BusinessModel'
local DisplayUtil = require"Logic/DisplayUtil"

local effcs = {}

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

local function SetUIInfo(self,data)
    if data then
        local temp = BusinessModel.GetCommonActivity(data.sheet_name)
        local activityinfo = temp or {}
        if activityinfo then
            activityinfo.sheet_name = data.sheet_name
        end
        activityinfo.activitytype = 3
        local source = {tag = data.sheet_name ,info = {'UI/Business/ActivityType_'..data.client_type ,data.client_xml,"needBack",activityinfo}}
        GlobalHooks.UI.OpenUI(source ,0 ,data.activity_id,self.subindex)
    end
end

function _M.OnEnter(self)
    for i = 1, 3 do
        effcs[i] = LoadEffect(self,
                self.btns[i].Parent,
                self.btns[i],
                "/res/effect/ui/ef_ui_lovestar01.assetbundles",
                Vector3(92,-92,0))
    end
    effcs[4] = LoadEffect(self,
            self.ib_mainbg,
            self.ib_mainbg,
            "/res/effect/ui/ef_ui_lovestar02.assetbundles",
            Vector3(245,-255,0))
    
    BusinessModel.FirstRequire(3,function()
        self.allactivity = BusinessModel.GetAllActivity(3,true)

        self.btns[1].TouchClick = function(sender)
            SetUIInfo(self,self.allactivity[1])
            self:Close()
        end
        self.btns[2].TouchClick = function(sender)
            SetUIInfo(self,self.allactivity[2])
            self:Close()
        end
        self.btns[3].TouchClick = function(sender)
            FunctionUtil.OpenFunction("shop_103")
            self:Close()
        end
    end)
    
end

function _M.OnInit(self)
    self.btn_rule = self.root:FindChildByEditName('btn_rule', true)
    self.ib_mainbg = self.root:FindChildByEditName('ib_mainbg', true)
    self.btns = {}
    for i = 1, 3 do
        self.btns[i] = self.root:FindChildByEditName('btn_go'..i, true)
        self.btns[i].LayoutDown = nil
    end

    self.ui.menu.ShowType = UIShowType.HideHudAndMenu
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    DisplayUtil.adaptiveFullSceen(self.ib_mainbg)
end

function _M.OnExit( self )
    for i = 1, 4 do
        if effcs[i] then
            UnLoadEffect(effcs[i])
            effcs[i] = nil
        end
    end
    effcs = {}
end

return _M