-------------适用于神兽陪练以及核心副本结算界面---------------------
local _M = {}
_M.__index = _M

local TimeUtil=require 'Logic/TimeUtil'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'


--结算界面掉落列表
local function SetDropList(self,params,node,index)
    local num = params[index].Qty
    if num <= 0 then
        node.Visible =false
        return
    end
    local itemdetail=ItemModel.GetDetailByTemplateID(tonumber(params[index].TemplateID))
    local itshow=UIUtil.SetItemShowTo(node,itemdetail,num)
        itshow.EnableTouch = true
            itshow.TouchClick = function() 
            SoundManager.Instance:PlaySoundByKey('button',false)     
            UIUtil.ShowNormalItemDetail({x=node.X+290,y=node.Y+35,detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})         
    end
end


function _M.OnInit(self)
	--设置动画
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.Scale)
    --通关时间
    self.lb_time=self.ui.comps.lb_timenum
    --离开时间
    self.lb_betime=self.ui.comps.lb_betime
    self.btn_leave=self.ui.comps.btn_leave
    --奖励
    self.cvs_killcount=self.ui.comps.cvs_type1
    self.cvs_damage=self.ui.comps.cvs_type2
    --无奖励
    self.lb_none=self.ui.comps.lb_none
    --掉落列表
    self.sp_drops=self.ui.comps.sp_drops
    self.cvs_dpitem=self.ui.comps.cvs_dpitem 
    self.cvs_dpitem.Visible=false
end


--判断显示哪种ui
local function SetCountOrDamage(self,params)
	--boss
	if params.extramap.func =='mythicalbeasts_1'then
		self.cvs_killcount.Visible=false
		self.cvs_damage.Visible=true

		self.cvs_damage.Position2D=Vector2(-5,20)

		local lb_damage = self.cvs_damage:FindChildByEditName('lb_damage',true)
		local lb_ratio = self.cvs_damage:FindChildByEditName('lb_ratio1',true)
		lb_damage.Text=params.extramap.totalDamage
		lb_ratio.Text = string.format('(%.2f%%)',params.extramap.damagePct)
	--小怪
	elseif params.extramap.func =='mythicalbeasts_2' then
		self.cvs_killcount.Visible=true
		self.cvs_damage.Visible=false

		self.cvs_killcount.Position2D=Vector2(-5,20)

		local lb_count = self.cvs_killcount:FindChildByEditName('lb_count',true)
		local lb_ratio = self.cvs_killcount:FindChildByEditName('lb_ratio',true)
		lb_count.Text=params.extramap.killCount..'/'..params.extramap.totalCount
		lb_ratio.Text= string.format('(%.2f%%)',(params.extramap.killCount/params.extramap.totalCount)*100)
    --核心产出副本
    elseif params.extramap.func =='dailydungeon' then
        self.cvs_killcount.Visible=true
        self.cvs_damage.Visible=false

        self.cvs_killcount.Position2D=Vector2(-5,20)

        local lb_count = self.cvs_killcount:FindChildByEditName('lb_count',true)
        local lb_ratio = self.cvs_killcount:FindChildByEditName('lb_ratio',true)
        lb_ratio.Visible=false
        lb_count.Text=params.extramap.score
    end
end


function _M.OnEnter(self,params)
    --根据参数判断显示计数or伤害
	SetCountOrDamage(self,params)
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
    --通关时间
    self.lb_time.Text=TimeUtil.SecToTimeformatToMS(params.finishtime)
    --如果有掉落，设置掉落列表
    if not params.noAward then
        self.lb_none.Visible=false
        self.sp_drops.Visible=true
        --设置掉落列表
        local function SetDrop(node,index)      
            SetDropList(self,params.itemList,node,index)
        end    
        UIUtil.ConfigHScrollPan(self.sp_drops,self.cvs_dpitem,#params.itemList,SetDrop)
    else--否则显示无掉落cvs
        self.lb_none.Visible=true
        self.sp_drops.Visible=false
    end
    --离开副本请求，回调参数里的函数
    self.btn_leave.TouchClick=function()
       params.cb()
    end
end


function _M.OnExit(self)
	--释放计时器
    if self.myTimer then
        LuaTimer.Delete(self.myTimer)
    end
end


return _M