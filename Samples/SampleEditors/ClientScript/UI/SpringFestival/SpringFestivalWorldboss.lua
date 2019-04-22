---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by xujing.xu.
--- DateTime: 2019/1/19 11:10
---年兽闹新春

local _M = {}
_M.__index = _M

local SpringFestivalModel = require 'Model/SpringFestivalModel'
local Util = require 'Logic/Util'

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
    self.actData = SpringFestivalModel.GetSpringActBySheetName(self.actInfo.sheet_name)
    
    self.comps.lb_daytime.Text = Util.GetText(Constants.SpringFestival.LevelToJoin,self.actData[1].limit_lv)
    self.comps.btn_go.TouchClick =function ()
        if DataMgr.Instance.UserData.Level >= self.actData[1].limit_lv then --等级达到自动寻路
            self.ui:Close()
            local mainui =GlobalHooks.UI.FindUI('SpringFestivalMain')
            if mainui then
                mainui.ui:Close()
            end
            FunctionUtil.seekAndNpcTalkByFunctionTag(self.actData[1].funtion_tag)
        else--否则提示等级不足
            GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.SpringFestival.LowLevel))
        end
    end
end

function _M.OnExit(self)

end

return _M