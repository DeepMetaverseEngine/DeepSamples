local _M = {}
_M.__index = _M


local Dungeon= require 'Model/DungeonModel'
local TimeUtil=require 'Logic/TimeUtil'
local DailyDungeonModel=require 'Model/DailyDungeonModel'
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'
local Util= require 'Logic/Util'

local lb_betime--时间
local lb_round--波数
local lb_kill--击杀数
local cvs_reward={}
local lb_reward={}
local bouns	--奖励系数
local groupId--获取当前地图奖励组
local rewardPos={}--记录原始图标位置
local rewardCountPos={}--记录原始奖励数字位置
local killCountPos
local hiteffect 

--缩放
local function DoScaleAction(node,scale,duration,cb)
    local scaleAction = ScaleAction()
    scaleAction.ScaleX = scale
    scaleAction.ScaleY = scale
    scaleAction.ScaleType = ScaleAction.ScaleTypes.Center
    scaleAction.Duration = duration
    node:AddAction(scaleAction)
    scaleAction.ActionFinishCallBack = cb
end


--奖励特效
local function HitEffect(parentCvs)
    local params = {
        LayerOrder = 1010,
        UILayer = true,
        DisableToUnload = true,
        Parent = parentCvs.Transform,
        Pos = {x = parentCvs.Size2D.x/2, y =-parentCvs.Size2D.y/2, z= 0}
    }
    hiteffect = hiteffect or {}
    local num = #hiteffect + 1
    hiteffect[num] = Util.PlayEffect('/res/effect/ui/ef_ui_trailing_hit.assetbundles',params,2)
end


local function ReleaseEffect()
    if hiteffect then
        for i, v in ipairs(hiteffect) do
            RenderSystem.Instance:Unload(hiteffect[i])
        end
        hiteffect= {}
    end
end

--控制相关ui缩放以及奖励数变化
local function UIScale(reward,index)
    if reward.reward.item.num[index] > 0 then
        cvs_reward[index].Visible = true
        lb_reward[index].Visible =true
    else
        cvs_reward[index].Visible = false
        lb_reward[index].Visible = false
    end
    
    --奖励图标缩放
    DoScaleAction(cvs_reward[index],1.2,0.2,function()
        cvs_reward[index].Scale=Vector2(1,1)
        cvs_reward[index].Position2D=rewardPos[index]--归位
    end)
    --奖励文字缩放
    DoScaleAction(lb_reward[index],1.2,0.2,function()
        lb_reward[index].Scale=Vector2(1,1)
        lb_reward[index].Position2D=rewardCountPos[index]--归位
    end)
    --变化特效
    HitEffect(cvs_reward[index])
    --向上取整奖励数量
    lb_reward[index].Text=math.ceil(reward.reward.item.num[index]*bouns)
    EventManager.Fire("Event.DailyDungeon.Bouns", { bouns = tonumber(lb_reward[1].Text)})
end


--设置环境变量
local function OnChangeTime(eventname,params)
    --显示倒计时
    if params.key =='count_down' then
        lb_betime.Text = TimeUtil.SecToTimeformatToMS(params.value)
    end
    --击杀数
    if params.key =='kill' then
    	lb_kill.Text=params.value
        --计数改变时，缩放击杀数
        DoScaleAction(lb_kill,1.5,0.2,function()
            lb_kill.Scale=Vector2(1,1)
            lb_kill.Position2D=killCountPos--归位
        end)
    end
    --当前波数
    if params.key == 'round'then
    	lb_round.Text=params.value
    end
end


--弹结算的时候，在计算一次最终结果（先这样写）
local function OnCalFinalResult(eventname,params)
    --通过奖励组id和击杀数，取得对应的奖励数量(reward)
    DailyDungeonModel.GetCountReward(groupId,tonumber(lb_kill.Text),function(reward)
        UIScale(reward,1)
        UIScale(reward,2)
    end)
end


local function OnEffectFinish(eventname,params)
    --特效飞到指定cvs_reward后判断时候需要缩放
    DailyDungeonModel.GetCountReward(groupId,tonumber(lb_kill.Text),function(reward)
        if reward.reward.item.num[2] == 0 then
            UIScale(reward,1)
        elseif reward.reward.item.num[2] > 0 then
            UIScale(reward,2)
        end
     end)
end


function _M.OnInit(self)
    --设置ui覆盖类型以及动画
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveDown)
    
    self.cvs_fuben=self.ui.comps.cvs_fuben
    self.cvs_info=self.ui.comps.cvs_info
    --自适应
    HudManager.Instance:InitAnchorWithNode(self.cvs_fuben,bit.bor(HudManager.HUD_RIGHT,HudManager.HUD_TOP))
    HudManager.Instance:InitAnchorWithNode(self.cvs_info,bit.bor(HudManager.HUD_RIGHT,HudManager.HUD_TOP))
    --倒计时
    lb_betime=self.cvs_fuben:FindChildByEditName('lb_betime',true)
    self.lb_betime1=self.cvs_fuben:FindChildByEditName('lb_betime1',true)
    
    self.btn_fbleave=self.cvs_fuben:FindChildByEditName('btn_fbleave',true)

    --当前波数/杀敌数
    lb_round=self.cvs_info:FindChildByEditName('lb_round',true)
    lb_kill=self.cvs_info:FindChildByEditName('lb_kill',true)
    --奖励物品	
    cvs_reward[1]=self.cvs_info:FindChildByEditName('cvs_reward1',true)
    cvs_reward[2]=self.cvs_info:FindChildByEditName('cvs_reward2',true)
    lb_reward[1]=self.cvs_info:FindChildByEditName('lb_reward1',true)
    lb_reward[2]=self.cvs_info:FindChildByEditName('lb_reward2',true)
    --记录相关的位置，center缩放位置会偏移
    rewardPos[1]=cvs_reward[1].Position2D
    rewardPos[2]=cvs_reward[2].Position2D
    rewardCountPos[1]=lb_reward[1].Position2D
    rewardCountPos[2]=lb_reward[2].Position2D
    killCountPos=lb_kill.Position2D
end


function _M.OnEnter(self,params)
	--通过mapid获取奖励组
	DailyDungeonModel.GetDailyDungeonRewardByMapId(DataMgr.Instance.UserData.MapTemplateId,function(rewarddata,groupid)
        groupId=groupid
            --获取奖励系数
            bouns=DailyDungeonModel.GetRewardBouns(groupid)
            for i=1,2 do --设置奖励图片
                if i == 1 then
                    cvs_reward[i].Visible=true
                    lb_reward[i].Visible=true
                else
                    cvs_reward[i].Visible=false
                    lb_reward[i].Visible=false
                end
                local itemdetail=ItemModel.GetDetailByTemplateID(rewarddata.reward.item.id[i])
                UIUtil.SetItemShowTo(cvs_reward[i],itemdetail.static.atlas_id,itemdetail.static.quality,0)
            end
        end)

	--初始化相关数值
	lb_round.Text =1
	lb_kill.Text=0
	lb_reward[1].Text=0
	lb_reward[2].Text=0
	--通过参数判断是否显示倒计时
    if params.isTime ==0 then 
      lb_betime.Visible=false
      self.lb_betime1.Visible=false
    else
      lb_betime.Visible=true
      self.lb_betime1.Visible=true
    end
	--添加监听
    EventManager.Subscribe("Event.SyncEnvironmentVarEvent", OnChangeTime)--环境变量
    EventManager.Subscribe("Event.UI.NotifyResult",OnCalFinalResult)--结算通知
    EventManager.Subscribe("Event.Effect.EffectFinish",OnEffectFinish)--特效到位
    --使鼠标点击能够穿透该canvas
    self.cvs_fuben.Enable=false
    self.cvs_info.Enable=false
    --ui穿透
    self.ui:EnableTouchFrameClose(false)
    --离开副本
    self.btn_fbleave.TouchClick = function()
    --弹出二次确认离开界面
        Dungeon.ShowExitConfirmTips()
    end
end


function _M.OnExit(self)
	--注销监听
    EventManager.Unsubscribe("Event.SyncEnvironmentVarEvent", OnChangeTime)
    EventManager.Unsubscribe("Event.UI.NotifyResult",OnCalFinalResult)
    EventManager.Unsubscribe("Event.Effect.EffectFinish",OnEffectFinish)
    ReleaseEffect()
end


return _M
