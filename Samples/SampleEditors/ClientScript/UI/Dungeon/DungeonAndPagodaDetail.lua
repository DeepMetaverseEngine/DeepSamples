----------------------------副本UI----------------------------

local _M = {}
_M.__index = _M


local Dungeon= require 'Model/DungeonModel'
local TimeUtil=require 'Logic/TimeUtil'

local lb_betime,cvs_fuben

local function OnChangeTime(eventname,params)
    --显示倒计时
    if params.key =='count_down' then
        lb_betime.Text = TimeUtil.SecToTimeformatToMS(params.value)
    end
end


--弹结算时隐藏倒计时界面
local function OnHideUI(eventname,params)
    if cvs_fuben.Visible==true then
        cvs_fuben.Visible=false
    end
end


function _M.OnInit(self)
      --设置ui覆盖类型以及动画
      self.ui.menu.ShowType = UIShowType.Cover
      self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveDown)

      --控件赋值
      lb_betime=self.ui.comps.lb_betime
      self.tb_betime=self.ui.comps.tb_betime
      cvs_fuben=self.ui.comps.cvs_fuben

      --自适应
      HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_fuben,bit.bor(HudManager.HUD_TOP, HudManager.HUD_RIGHT))
end


function _M.OnEnter(self,params)

    --通过参数判断是否显示倒计时
    if params.isTime ==0 then 
      lb_betime.Visible=false
      self.tb_betime.Visible=false
    else
      lb_betime.Visible=true
      self.tb_betime.Visible=true
    end

    --添加环境变量监听
    EventManager.Subscribe("Event.SyncEnvironmentVarEvent", OnChangeTime)
    EventManager.Subscribe("Event.UI.NotifyResult",OnHideUI)
    
    --离开副本按钮，添加离开副本监听
    btn_leave=self.ui.comps.btn_fbleave
    btn_leave.TouchClick=function(sender)
    --弹出二次确认离开界面
        Dungeon.ShowExitConfirmTips()
    end 

    --使鼠标点击能够穿透该canvas
    cvs_fuben.Enable=false
    --ui穿透
    self.ui:EnableTouchFrameClose(false)
end


function _M.OnExit()
    --注销监听
    EventManager.Unsubscribe("Event.SyncEnvironmentVarEvent", OnChangeTime)
    EventManager.Unsubscribe("Event.UI.NotifyResult",OnHideUI)
end


return _M