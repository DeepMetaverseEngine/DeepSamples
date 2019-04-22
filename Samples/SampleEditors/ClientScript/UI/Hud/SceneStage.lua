----------------------------镇妖塔UI------------------------------

local _M = {}
_M.__index = _M

local Dungeon= require 'Model/DungeonModel'
local TimeUtil=require 'Logic/TimeUtil'
local Pagoda=require 'Model/PagodaModel'
local Util   = require 'Logic/Util'
local EnvVarInfo= require 'Model/SyncEnvironmentVarEventModel'

local lb_lay,lb_wave,lb_time,cvs_stagehud
local IsDoubleTower


--接收推送消息并赋值
local function GetEnvVarInfo(eventname,params)
    
    --隐藏cvs，避免播放cg时显示出来，等到环境变量推送才显示
    if cvs_stagehud.Visible==false then
        cvs_stagehud.Visible=true
    end
    
    --显示当前层数
    if params.CurLayer~=nil then 
        local level,curlay = Pagoda.GetPagodaNameByFloor(params.CurLayer)
        lb_lay.Text = IsDoubleTower and Util.GetText(Pagoda.GetDoubleFlyTowerLayerName(params.CurLayer)) or Util.GetText(level)..Util.GetText(curlay)
    else
        lb_lay.Text=nil
    end
    
    --显示当前波数
    if params.CurWave~=nil then 
        lb_wave.Text=params.CurWave
    else
        lb_wave.Text=nil
    end
    
    --计时
    if params.CountDownTime~=nil then
        lb_time.Text=TimeUtil.SecToTimeformatToMS(params.CountDownTime)
    else
        lb_time.Text=nil
    end

end


--弹结算界面时，隐藏cvs，避免播放动画显示以及过场等待时数据不对的问题
local function OnHideUI(eventname,params)
    if cvs_stagehud.Visible==true then
        cvs_stagehud.Visible=false
    end
end


function _M.OnInit(self)

    --设置ui覆盖类型以及动画
    self.ui.menu.ShowType = UIShowType.Cover
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveDown)
    
    --设置自适应锚点
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_stagehud, bit.bor(HudManager.HUD_RIGHT,HudManager.HUD_TOP))

    --时间
    lb_time=self.ui.comps.lb_time

    --当前层数
    lb_lay=self.ui.comps.lb_name

    --当前波数
    lb_wave=self.ui.comps.lb_count

    --ui面板
    cvs_stagehud=self.ui.comps.cvs_stagehud

end


function _M.OnEnter(self)
    
    IsDoubleTower = DataMgr.Instance.UserData.MapTemplateId == 500041 
            or DataMgr.Instance.UserData.MapTemplateId ==500042
    
    --添加监听 
   	EventManager.Subscribe("Event.SceneStageEvent",GetEnvVarInfo)
    EventManager.Subscribe("Event.UI.NotifyResult",OnHideUI)
    
    --获取环境变量
    local layer,wave,time=EnvVarInfo.GetInfo()
    
    self.comps.lb_count1.Visible = not IsDoubleTower
    lb_wave.Visible = not IsDoubleTower
    
    local level,curlay = Pagoda.GetPagodaNameByFloor(layer)
    lb_lay.Text = IsDoubleTower and Util.GetText(Pagoda.GetDoubleFlyTowerLayerName(layer)) or Util.GetText(level)..Util.GetText(curlay)
    lb_wave.Text=wave
    lb_time.Text=TimeUtil.SecToTimeformatToMS(time)

    --离开副本按钮，添加离开副本监听
    btn_exit=self.ui.comps.btn_exit
    btn_exit.TouchClick=function(sender)

    --弹出二次确认离开界面
        Dungeon.ShowExitConfirmTips()
    end 

    --使鼠标点击能够穿透该canvas
    cvs_stagehud.Enable=false

    --ui穿透
    self.ui:EnableTouchFrameClose(false)
end

function _M.OnExit()
    --注销监听
    EventManager.Unsubscribe("Event.SceneStageEvent",GetEnvVarInfo)
    EventManager.Unsubscribe("Event.UI.NotifyResult",OnHideUI)

end


return _M