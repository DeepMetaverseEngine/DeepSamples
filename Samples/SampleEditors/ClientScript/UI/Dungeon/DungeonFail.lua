local _M = {}
_M.__index = _M

local Dungeon= require 'Model/DungeonModel'
local TimeUtil=require 'Logic/TimeUtil'
local UIUtil = require 'UI/UIUtil'
local StrongModel=require 'Model/BeStrongModel'


--设置变强图标
local function SetBeStrong(self,node,index)
    
    UIUtil.SetImage(node,self.strongData[index].icon,false,UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)

end


function _M.OnInit(self)
    
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.Scale)

    self.lb_betime=self.ui.comps.lb_betime
    self.btn_fbleave=self.ui.comps.btn_fbleave

    self.sp_bestrong=self.ui.comps.sp_bestrong
    self.cvs_bestrong=self.ui.comps.cvs_bestrong
    self.cvs_bestrong.Visible=false

end


function _M.OnEnter(self,params)

	--退出副本倒计时
    self._time=math.abs(TimeUtil.TimeLeftSec(params.counttime)) 
    
    self.myTimer = LuaTimer.Add(
        0,
        1000,
        function()
            if self._time <= 0 then
                self._time = nil
                self.lb_betime.Text = 0
                self.ui:Close()
                return false
            else
                self._time=self._time-1
                self.lb_betime.Text = self._time
                return true              
            end
        end)

	--离开副本请求，回调参数里的函数
    self.btn_fbleave.TouchClick=function()
        params.cb()
    end

    --获取变强宝典
    StrongModel.GetStrongWay(function(p)
    
        self.strongData=p
 
        --如果为空或者长度为0，返回
        if self.strongData==nil or #self.strongData==0 then 
            return
        else
            
        --回调函数
        local function eachupdatecb(node,index)
            SetBeStrong(self,node,index)
        end
            --滑动控件
            UIUtil.ConfigHScrollPanWithOffset(self.sp_bestrong,self.cvs_bestrong,#self.strongData,10,eachupdatecb)
        end
    end)

end


function _M.OnExit(self)

	--释放计时器
    if self.myTimer then
        LuaTimer.Delete(self.myTimer)
    end 
end


return _M