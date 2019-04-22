local _M = {}
_M.__index = _M


local UIUtil=require'UI/UIUtil.lua'


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
		cb() end
	end
	node:AddAction(fadeaction)
end


function _M.OnInit(self)
	self.InitY=self.ui.comps.cvs_fightup.Y
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	self.ui.menu.ShowType = UIShowType.Cover
	self.ui:EnableTouchFrameClose(false)
end


function _M.OnEnter(self,params)
	SoundManager.Instance:PlaySoundByKey('zhandouli',false)
	local cvs_fight = self.ui.comps.cvs_fightup
	cvs_fight.Y = self.InitY
	self.ui.comps.lb_fight.Text=params.curPower
	cvs_fight:RemoveAllAction(false)
	self.ui.comps.cvs_fightup.Alpha = 1

	-- 战力加成
	self.ui.comps.lb_fightadd.Text = "+"..tostring(params.nowPower-params.curPower)
	--显示框调整 
	self.ui.comps.lb_fight.Text = params.nowPower
	local w = self.ui.comps.lb_fight.PreferredSize.x
	self.ui.comps.lb_fightadd.X = w + self.ui.comps.lb_fight.Position2D.x+25
	-- 播放闪光特效
	self.ui.comps.ib_effect.Visible=true
	UIUtil.PlayCPJOnce( self.ui.comps.ib_effect,1,function(sender)
		self.ui.comps.ib_effect.Visible = false
	end)
	--move MoveAction()移动UI
	local 	move_fightUp = MoveAction()
	move_fightUp.Duration =0.3
	move_fightUp.TargetX = cvs_fight.X
	move_fightUp.TargetY = cvs_fight.Y - 150
	move_fightUp.ActionEaseType = EaseType.linear
	move_fightUp.ActionFinishCallBack = function(sender)
		UIUtil.AddNumberPlusPlusTimer(self.ui.comps.lb_fight , params.curPower,params.nowPower, 0.8)  --滚动数字
		local 	delay_action = DelayAction()
		delay_action.Duration=1.4
		delay_action.ActionFinishCallBack=function(sender)
			local move1_fightUp = MoveAction()
			move1_fightUp.Duration =0.5
			move1_fightUp.TargetX = cvs_fight.X
			move1_fightUp.TargetY = cvs_fight.Y -80
			move1_fightUp.ActionEaseType = EaseType.linear
			move1_fightUp.ActionFinishCallBack = function(sender)
			end
			self.ui.comps.cvs_fightup:AddAction(move1_fightUp)
			--消失 Alpha  
			setFadeAction(self.ui.comps.cvs_fightup,0.5,false,function()
				self.ui:Close()
			end)
		end
		sender:AddAction(delay_action)
	end
	cvs_fight:AddAction(move_fightUp)
end


function _M.OnExit(self)

end

return _M