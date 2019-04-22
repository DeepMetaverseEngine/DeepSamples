---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by xujing.xu.
--- DateTime: 2019/1/19 11:10
---天降宝箱

local _M = {}
_M.__index = _M

local SpringFestivalModel = require 'Model/SpringFestivalModel'


function _M.OnInit(self)
    --覆盖/无动画/黑底
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu:SetFullBackground(UILayout.CreateUILayoutColor(UnityEngine.Color(0,0,0,0.5),UnityEngine.Color(0,0,0,0.5)))
end

function _M.OnEnter(self,params)
    self.actInfo = params or SpringFestivalModel.GetActInfoByTag(string.lower(self.ui.tag))
    if not SpringFestivalModel.CheckIsOpening(self.actInfo.end_time) then
        self.ui:Close()
        return
    end
    
    SpringFestivalModel.SetOpeningTime(self.comps.lb_itemnum,self.actInfo.start_time,self.actInfo.end_time)
    SpringFestivalModel.SetHelpCvsVisibleOrInvisible(self.comps.btn_rule,self.comps.cvs_help)
    
    self.comps.btn_box.TouchClick=function()
        FunctionUtil.OpenFunction('box_goto')
    end
end

function _M.OnExit(self)

end


return _M