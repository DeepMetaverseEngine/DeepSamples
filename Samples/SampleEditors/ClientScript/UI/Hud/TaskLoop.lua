---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by xujing.xu.
--- DateTime: 2018/10/19 17:34
---
local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'


function _M.OnInit(self)
    self.ui.menu.ShowType = UIShowType.Cover
end


function _M.OnEnter(self,params)
    --local str=(params.type==1000 and {'shimen'} or  {'xianmeng'})[1]
    local data = unpack(GlobalHooks.DB.Find('LoopQuestControlData',{id = params.type}))
    self.ui.comps.lb_text2.Text=Util.GetText(data.name)
    self.ui.comps.bt_no.TouchClick=function(sender)
        self.ui:Close()
    end
    self.ui.comps.bt_yes.TouchClick=function(sender)
        --FunctionUtil.OpenFunction(str)
        FunctionUtil.BeginLoopQuest(params.type)
        self.ui:Close()
    end
end


function _M.OnExit(self)

end


return _M