local _M = {}
_M.__index = _M

local TimeUtil=require 'Logic/TimeUtil'
local Dungeon= require 'Model/DungeonModel'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'

local lb_betimeCount,cvs_beasts,cvs_showreward
local boss  --boss实例
local bossid = 0
local bouns
local cvs_itemicon={}
local lb_num={}


--初始化设置一次奖励物品
local function SetBounsIconAndNum(self,bouns)
    for i = 1, self.ui.comps.cvs_showitem.NumChildren do
        cvs_itemicon[i]=self.ui.comps.cvs_showitem:FindChildByEditName('cvs_item'..i,true)
        lb_num[i]=self.ui.comps.cvs_showitem:FindChildByEditName('lb_num'..i,true)
        local itemdetail=ItemModel.GetDetailByTemplateID(tonumber(bouns.item.id[i]))
        local quality = itemdetail.static.quality
        local icon=itemdetail.static.atlas_id
        local itshow=UIUtil.SetItemShowTo(cvs_itemicon[i],icon,quality,1)
        itshow.EnableTouch = true
        itshow.TouchClick = function()
            SoundManager.Instance:PlaySoundByKey('button',false)
            local detail = UIUtil.ShowNormalItemDetail({x=cvs_itemicon[i].X+740,y=cvs_itemicon[i].Y+140,detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})
        end
        lb_num[i].Text=bouns.item.num[i]
    end
end


--通过bossid和掉血百分比查找奖励数据
local function GetBouns(bossid,bosshp)
    local bossinfo =  GlobalHooks.DB.Find('BeastsModeBoss',function ( ele )
        return ele.monster_id == bossid and ele.hp_min <= bosshp and bosshp <= ele.hp_max
    end)
    return bossinfo[1].reward
end


local function OnChangeTime(eventname,params)
    --显示倒计时
    if params.key =='count_down' then
        lb_betimeCount.Text = TimeUtil.SecToTimeformatToMS(params.value)
    end
    if bossid ==nil then
        return
    end
    --每秒重新计算boss血量以及奖励
   bouns=GetBouns(bossid,math.ceil((1-(boss.HP/boss.MaxHP))*100))
    if bouns ~=nil then
        for i = 1, 3 do
            lb_num[i].Text=bouns.item.num[i]
        end
    end
    EventManager.Fire("Event.MyThicalBeasts.BossHP", { BossHP = math.ceil((1-(boss.HP/boss.MaxHP))*100),BossId=bossid})
end


local function OnHideUI(eventname,params)
    if cvs_beasts.Visible==true then
        cvs_beasts.Visible=false
    end
    if cvs_showreward.Visible==true then
        cvs_showreward.Visible=false
    end
end


--数组去重
local function table_unique(t)
    local check = {}
    local n = {}
    for key , value in pairs(t) do
        if not check[value] then
            n[key] = value
            check[value] = value
        end
    end
    return n
end


local function  GetBossId()
    local id={}
    local bossinfo =  GlobalHooks.DB.GetFullTable('BeastsModeBoss')
    for i = 1, #bossinfo do
        table.insert(id,bossinfo[i].monster_id)
    end
    return id
end


--监听boss出场，获取TemplateID
local function BossRefresh(self,eventname,params)
    --通过模板id，获取boss实例
    if params.Name=='boss' then
        local an=TLBattleScene.Instance:GetUnitByTemplateId(params.Unit.TemplateID)
        bossid=params.Unit.TemplateID
        boss=an
        bouns=GetBouns(bossid,math.ceil((1-(boss.HP/boss.MaxHP))*100))
        if bouns ~=nil then
            cvs_showreward.Visible=true
            SetBounsIconAndNum(self,bouns)
        end
    end
end


function _M.OnInit(self)
	--设置ui覆盖类型以及动画
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveDown)
	lb_betimeCount=self.ui.comps.lb_betime --时间
	cvs_beasts=self.ui.comps.cvs_beasts
    self.btn_fbleave=self.ui.comps.btn_fbleave
    cvs_showreward=self.ui.comps.cvs_showreward
    --自适应
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_showreward, bit.bor(HudManager.HUD_TOP,HudManager.HUD_RIGHT))
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_beasts, bit.bor(HudManager.HUD_TOP,HudManager.HUD_RIGHT))
end


function _M.OnEnter(self,params)
    
    function _M.OnBossRefresh(eventname,params)
        BossRefresh(self,eventname,params)
    end
    EventManager.Subscribe(Events.SYS_ADD_UNIT,_M.OnBossRefresh)
    
    self.btn_fbleave.TouchClick=function()
        --弹出二次确认离开界面
        Dungeon.ShowExitConfirmTips()
    end
    
    self.allbossid={}
    self.allbossid=GetBossId()
    self.allbossid=table_unique(self.allbossid)
        for _, v in pairs(self.allbossid) do
            local an=TLBattleScene.Instance:GetUnitByTemplateId(v)
            if an ~=nil  then
                bossid=v
                boss=an
                break
            end
        end
    if bossid ~=0 then
        bouns=GetBouns(bossid,math.ceil((1-(boss.HP/boss.MaxHP))*100))
        if bouns ~=nil then
            cvs_showreward.Visible=true
            SetBounsIconAndNum(self,bouns)
        end
    end
    
    --使鼠标点击能够穿透该canvas
    cvs_beasts.Enable=false

    --ui穿透
    self.ui:EnableTouchFrameClose(false)
    
    --添加环境变量监听
    EventManager.Subscribe("Event.SyncEnvironmentVarEvent", OnChangeTime)
    EventManager.Subscribe("Event.UI.NotifyResult",OnHideUI)
end

function _M.OnExit(self)
    --注销监听
    EventManager.Unsubscribe("Event.SyncEnvironmentVarEvent", OnChangeTime)
    EventManager.Unsubscribe("Event.UI.NotifyResult",OnHideUI)
    EventManager.Unsubscribe(Events.SYS_ADD_UNIT,_M.OnBossRefresh)
    bossid=0
    boss=nil
end


return _M