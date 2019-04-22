local _M = {}
_M.__index = _M
local Util  = require "Logic/Util"
local UIUtil = require 'UI/UIUtil'

local DisplayUtil = require"Logic/DisplayUtil"
local function setFadeAction(node,time,isShow,cb)

    local fadeaction = FadeAction()
    fadeaction.Duration = time
    if  isShow then
        fadeaction.TargetAlpha = 1
    else
        fadeaction.TargetAlpha = 0
    end
    fadeaction.ActionEaseType = EaseType.linear

    fadeaction.ActionFinishCallBack = function(sender)
        if cb then
            cb()
        end
    end
    node:AddAction(fadeaction)
end
local function Release3DModel(self)

    if self and self.model then
        UI3DModelAdapter.ReleaseModel(self.model.Key)
        self.model = nil
    end
end

local function Init3DSngleModel(self, parentCvs, pos2d, scale, menuOrder,fileName)
    
    local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
    self.model = info
end

local function ReloadModel(self,node)
        if self == nil then
            return
        end
        Release3DModel(self)
       
        local filename = "/res/effect/ui/ef_ui_chapter.assetbundles"
        if not string.empty(filename) then
            Init3DSngleModel(self, node, Vector2(0,0), 1, self.ui.menu.MenuOrder,filename)
        end
        
end

function _M.OnEnter(self,param)
    
    -- self.ui.comps.ib_di.Alpha = 0
      self.ui.comps.ib_hei.Alpha = 0.5
      -- setFadeAction(self.ui.comps.ib_di,0.8,true)
      print("ChapterMain")
      self.chapterId = param.chapterId
      self.cb = param.CallBack
       local chapterdata = GlobalHooks.DB.Find('ChapterData',tonumber(self.chapterId))
       if chapterdata ~= nil then
        UIUtil.SetImage(self.ui.comps.ib_pic,chapterdata.chapter_pic)
           self.ui.comps.lb_name1.Text = Util.GetText(chapterdata.chapter_id)
           self.ui.comps.lb_name2.Text = string.gsub(Util.GetText(chapterdata.chapter_name),'\\n','\n')
    
           self.ui.comps.cvs_name1.Alpha = 0
           self.ui.comps.ib_pic.Alpha = 0
           self.ui.comps.lb_name2.Alpha = 0
           LuaTimer.Add(1000,function()
             setFadeAction(self.ui.comps.cvs_name1,1,true)
             setFadeAction(self.ui.comps.ib_pic,1,true)
            setFadeAction(self.ui.comps.lb_name2,1,true)

           end)

           LuaTimer.Add(4500,function()
             setFadeAction(self.ui.comps.cvs_name1,0.5,false)
             setFadeAction(self.ui.comps.ib_pic,0.5,false)
             setFadeAction(self.ui.comps.lb_name2,0.5,false)
           end)

           LuaTimer.Add(5200,function()
                -- setFadeAction(self.ui.comps.ib_di,1.2,false,function()
                   --LuaTimer.Add(500,function()
                    setFadeAction(self.ui.comps.ib_hei,0.5,false,function()
                      self.ui:Close()
                      if self.cb ~=nil then
                          self.cb()
                      end
                    end)
                -- end)
           end)
          
       else
           print("no ChapterMain with id =",param)
           self.ui:Close()
       end
       ReloadModel(self,self.ui.comps.cvs_ef1)
      -- ReloadModel(self,self.ui.comps.cvs_ef2)
      
end

function _M.OnExit(self)
    --print("OnExit")
    
end


function _M.OnInit(self)
    --print("OnInit")
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_ef1, bit.bor(HudManager.HUD_CENTER))
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.ib_pic, bit.bor(HudManager.HUD_CENTER))
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.lb_name2, bit.bor(HudManager.HUD_CENTER))
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_name1, bit.bor(HudManager.HUD_CENTER))
    -- DisplayUtil.adaptiveFullSceen(self.ui.comps.ib_di)
    DisplayUtil.adaptiveFullSceen(self.ui.comps.ib_hei)
    self.ui.menu:SetUILayer(self.ui.comps.lb_name1)
    self.ui.menu:SetUILayer(self.ui.comps.lb_name2)
    self.ui.menu:SetUILayer(self.ui.comps.ib_pic)
    self.ui.menu:SetUILayer(self.ui.comps.cvs_name1)

    --print("HZUISystem.Instance.StageScale",HZUISystem.Instance.StageScale)
end

return _M
