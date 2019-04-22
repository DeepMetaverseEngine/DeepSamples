local _M = {}
_M.__index = _M


local Relive = require 'Model/ReliveModel'
local Util   = require 'Logic/Util'
local TimeUtil=require 'Logic/TimeUtil'
local ItemModel = require 'Model/ItemModel'


local waitDeath=false


function _M.OnInit(self)
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.Scale)
	--重生点复活(左侧)
	self.btn_leftRebirth=self.ui.comps.btn_saveplace--左侧按钮
	self.lb_leftCount=self.ui.comps.lb_savenum--左侧次数
	self.lb_leftText=self.ui.comps.lb_saveplace--左侧文本
	--原地复活(右侧)
	self.btn_rightRebirth=self.ui.comps.btn_inplace--右侧按钮
	self.lb_rightCount=self.ui.comps.lb_innum--右侧次数
	self.lb_rightText=self.ui.comps.lb_inplace--右侧文本
	self.lb_rightCostNum=self.ui.comps.lb_costunm--右侧花费
	self.lb_rightCostText=self.ui.comps.lb_incost--右侧花费文本
	self.ib_rightCost=self.ui.comps.ib_cost--右侧花费图标
	--击杀者
	self.killer=self.ui.comps.tb_player
	self.killername=self.ui.comps.lb_player
	self.killername.Visible=false
	--中间按钮
	self.btn_know=self.ui.comps.btn_know
    self.btn_know.Visible=false
    self.lb_know=self.ui.comps.lb_know
    self.lb_know.Visible=false
end


--设置左侧复活按钮
local function LeftBtnBirth(self,params)
	--左侧按钮复活事件
	self.btn_leftRebirth.Text=Util.GetText(self.rebirth.renascence.desc[1])
	self.btn_leftRebirth.TouchClick=function(sender)
        Relive.RequestRelive(self.rebirth.renascence.type[1], function( ... )
            self.ui:Close()
        end)
 	end
    --先隐藏文本
	self.lb_leftCount.Visible=false
	self.lb_leftText.Visible=false
    --开启倒计时
	self._leftTime=TimeUtil.CaluReliveLeftSec(params.leftRebirthTimeStamp)
    	self.myLeftTimer = LuaTimer.Add(
        0,
        1000,
        function() 
            if self._leftTime <= 0 then --倒计时结束，复活按钮可用
                self._leftTime = nil
                self.btn_leftRebirth.IsGray=false
                self.btn_leftRebirth.IsInteractive=true
                if self.rebirth.renascence.times[1] < 0 then 
                	self.lb_leftText.Visible=false
                	self.lb_leftCount.Visible=false
                else  
             	    self.lb_leftCount.Visible=true
             	    self.lb_leftCount.Text=self.rebirth.renascence.times[1]-params.leftRebirthTimes
                	self.lb_leftText.Visible=true
                	self.lb_leftText.Text=Constants.ReLive.Remainder..':'
                end
                return false
            else
                self._leftTime=self._leftTime-1 --正在倒计时，下方文字显示时间
                self.lb_leftText.Visible=true
                self.lb_leftText.Text =Constants.ReLive.RebirthCoolingTime..':'.. self._leftTime
                self.btn_leftRebirth.IsGray=true
                self.btn_leftRebirth.IsInteractive=false
                return true              
            end
        end)
end


--设置右侧复活按钮
local function RightBtnBirth(self,params)
	--右侧按钮复活事件
	self.btn_rightRebirth.Text=Util.GetText(self.rebirth.renascence.desc[2])
	self.btn_rightRebirth.TouchClick=function(sender)
        Relive.RequestRelive(self.rebirth.renascence.type[2], function( ... )
            self.ui:Close()
        end)
 	end
    --先隐藏文本
	self.lb_rightCount.Visible=false
	self.lb_rightText.Visible=false
    --开启倒计时
	self._rightTime=TimeUtil.CaluReliveLeftSec(params.rightRebirthTimeStamp)
    	self.myRightTimer = LuaTimer.Add(
        0,
        1000,
        function()
            if self._rightTime <= 0 then
                self._rightTime = nil
                self.btn_rightRebirth.isGray=false
                self.btn_rightRebirth.IsInteractive=true
               	if self.rebirth.renascence.times[2] < 0 then 
               		self.lb_rightText.Visible=false
                	self.lb_rightCount.Visible=false
                else
                	self.lb_rightCount.Visible=true
				    self.lb_rightCount.Text=self.rebirth.renascence.times[2]-params.rightRebirthTimes
                    if self.rebirth.renascence.times[2]-params.rightRebirthTimes <=0 then 
                        self.btn_rightRebirth.IsGray=true
                        self.btn_rightRebirth.IsInteractive=false
                    else
                        self.lb_rightText.Visible=true
                        self.lb_rightText.Text=Constants.ReLive.Remainder..':'
                    end
                end
                return false
            else
                self._rightTime=self._rightTime-1
                self.lb_rightText.Visible=true
                self.lb_rightText.Text = Constants.ReLive.RebirthCoolingTime..':'.. self._rightTime
                self.btn_rightRebirth.isGray=true
                self.btn_rightRebirth.IsInteractive=false
                return true              
            end
        end)
	
	--设置道具数量，如果超过最大值，按最大值算
	local reliveCost=(params.rightRebirthTimes)*self.rebirth.renascence.increasenum[2]+self.rebirth.renascence.costnumber[2]
	if reliveCost<=self.rebirth.renascence.maxnum[2] then 
		self.lb_rightCostNum.Text=reliveCost
	else
		self.lb_rightCostNum.Text=self.rebirth.renascence.maxnum[2]
	end
    --设置花费图标
    local cost=ItemModel.GetDetailByTemplateID(self.rebirth.renascence.costid[2])
    MenuBase.SetImageBox(self.ib_rightCost,'static/item/'..cost.static.atlas_id,UILayoutStyle.IMAGE_STYLE_BACK_4, 8)
end


local function MidBtnKnow(self,params)
	--隐藏左侧按钮
    self.comps.cvs_saveplace.Visible=false
	--隐藏右侧按钮
    self.comps.cvs_inplace.Visible=false
    --如果等死，则显示按钮，隐藏倒计时，不做任何处理
    if waitDeath then 
        self.btn_know.Visible=true
        self.lb_know.Visible=false
        self.btn_know.Text=Util.GetText(self.rebirth.midbutton_show)
        self.btn_know.TouchClick=function()
            self.ui:Close()
        end
    else--显示中间按钮
	    self.btn_know.Visible=true
        self.lb_know.Visible=true
        --显示倒计时
        self._midTime=TimeUtil.CaluReliveLeftSec(params.leftRebirthTimeStamp)
        self.myMidTimer = LuaTimer.Add(
        0,
        1000,
        function()
            if self._midTime <= 0 then--倒计时结束
                self._midTime = nil
                self.btn_know.IsGray=false
                self.btn_know.IsInteractive=true
                if self.rebirth.renascence.times[1] < 0 then 
                    self.lb_know.Visible=false
                else  
                    self.lb_know.Visible=true
                    self.lb_know.Text=Constants.ReLive.Remainder..':'..(self.rebirth.renascence.times[1]-params.leftRebirthTimes)
                end
                return false
            else--正在倒计时
                self._midTime=self._midTime-1
                self.lb_know.Visible=true
                self.lb_know.Text =Constants.ReLive.RebirthCoolingTime..':'.. self._midTime
                self.btn_know.IsGray=true
                self.btn_know.IsInteractive=false
                return true              
            end
        end)

        --设置中间按钮的文本以及点击事件
        self.btn_know.Visible=true
	    self.btn_know.Text=Util.GetText(self.rebirth.renascence.desc[1])
        self.btn_know.TouchClick=function(sender)
            Relive.RequestRelive(self.rebirth.renascence.type[1],function()
                self.ui:Close()
            end)
        end
    end
end


function _M.OnEnter(self,params)
    --每秒检测一次人物是否复活
    self.CloseTime = LuaTimer.Add(
        0,
        1000,
        function ()
            if TLBattleScene.Instance.Actor then
                if not TLBattleScene.Instance.Actor:Dead() then
                    self.ui:Close()
                end
            end
    end)

    self.rebirth=Relive.GetReliveType(params.rebirthType)

    --判断中央文本的内容
    if self.rebirth.custom_midarea_show ==1 then 
        self.killer.Text=params.descStr
    else
        self.killer.Text=Util.GetText(self.rebirth.midarea_show)
    end
    --如果左边按钮为0，只显示中间的按钮
    if self.rebirth.renascence.type[1] ==0 then 
        waitDeath=true
        MidBtnKnow(self,params)
    end
    --如果左边按钮有值，右边按钮为0，还有救
    if self.rebirth.renascence.type[1] ~=0 and self.rebirth.renascence.type[2]==0 then
        waitDeath=false
        MidBtnKnow(self,params)
    end
    --如果两者都有值，显示两个按钮
    if self.rebirth.renascence.type[1] ~=0 and self.rebirth.renascence.type[2] ~=0 then
        LeftBtnBirth(self,params)
        RightBtnBirth(self,params)
    end
end


function _M.OnExit(self)
    --释放计时器
	if self.myLeftTimer then
        LuaTimer.Delete(self.myLeftTimer)
        self.myLeftTimer = nil
    end
    if self.myRightTimer then
        LuaTimer.Delete(self.myRightTimer)
        self.myRightTimer = nil 
    end
    if self.myMidTimer then
        LuaTimer.Delete(self.myMidTimer)
        self.myMidTimer = nil
    end
    if self.CloseTime then
        LuaTimer.Delete(self.CloseTime)
        self.CloseTime = nil
    end
end


return _M