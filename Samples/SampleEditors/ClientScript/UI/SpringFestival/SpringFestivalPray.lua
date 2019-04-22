---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by xujing.xu.
--- DateTime: 2019/1/17 10:43
---新春祈福

local _M = {}
_M.__index = _M

local SpringFestivalModel = require 'Model/SpringFestivalModel'
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'


_M.GetItemUI = false

function _M.OnInit(self)
    --覆盖/无动画/黑底
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu:SetFullBackground(UILayout.CreateUILayoutColor(UnityEngine.Color(0,0,0,0.5),UnityEngine.Color(0,0,0,0.5)))
end


local function ReleaseAllEffect(self)
    if self.eff then
        for i, v in ipairs(self.eff) do
            RenderSystem.Instance:Unload(self.eff[i])
        end
        self.eff= {}
    end
    if self.smokeEffect then
        RenderSystem.Instance:Unload(self.smokeEffect)
        self.smokeEffect={}
    end
end

local function ReleaseAllTimer(self)
    if self.efftime then
        for i, v in ipairs(self.efftime) do
            LuaTimer.Delete(self.efftime[i])
        end
        self.efftime ={}
    end
end

local function PlaySmokeEffect(self,node,res)
    local transSet = TransformSet()
    transSet.Layer = Constants.Layer.UI
    transSet.LayerOrder = self.ui.menu.MenuOrder
    transSet.Pos = Vector3(-9,-15,0)
    transSet.DisableToUnload = true
    transSet.Parent = node.Transform
    self.smokeEffect = RenderSystem.Instance:LoadGameObject(res, transSet, function(aoe)
    end)
end

local function PlayFlyEffect(self,res,drop)
    
    local transSet = TransformSet()
    transSet.Layer = Constants.Layer.UI
    transSet.LayerOrder = 5000
    transSet.Pos = Vector3(0,0,200)
    transSet.DisableToUnload = true
    transSet.Parent = self.comps.cvs_effect.Transform
    
    self.eff =self.eff or {}
    local num = #self.eff + 1
    self.eff[num] = RenderSystem.Instance:LoadGameObject(res, transSet, function(aoe)
        local symbol = math.random(1,100) > 50 and 1 or -1
        local Xspeed = math.random()
        local Yspeed = math.random(50,100)/100
        local x = 0
        local y =0
        local t = 0
        self.efftime = self.efftime or {}
        local timenum = #self.efftime + 1
        self.efftime[timenum] = LuaTimer.Add(0,20,
            function()
                x = x + 5
                y = y + 15
                aoe.gameObject.transform.position =Vector3(math.sin(x*Mathf.Deg2Rad)*Xspeed*symbol,math.sin(y*Mathf.Deg2Rad)*Yspeed,0)
                if aoe.gameObject.transform.position.y <= -0.4 then
                    LuaTimer.Delete(self.efftime[timenum])
                    self.efftime[timenum] = 0
                    local num2 = #self.efftime + 1
                    self.efftime[num2] = LuaTimer.Add(200,20,
                    function ()
                        t = t + 0.05
                        if t >= 0.6 then
                            RenderSystem.Instance:Unload(aoe.gameObject)
                            if not _M.GetItemUI then
                                _M.GetItemUI = true
                                if next(drop) then
                                    GlobalHooks.UI.OpenUI('GainItem',0,drop,3)
                                end
                                self.comps.btn_pray.Enable=true
                            end
                            return false
                        end
                        aoe.gameObject.transform.position = Vector3.Lerp(aoe.gameObject.transform.position,self.comps.cvs_beibaopos.Transform.position,t);
                        return true
                    end)
                    return false
                end
                return true
        end)
    end)
end

local function Pray(self)
    --道具数量
    self.comps.lb_itemnum.Text = ItemModel.CountItemByTemplateID(self.actData[1].cost.id[1])
    
    local showReds = 0
    if self.times > 0 and ItemModel.CountItemByTemplateID(self.actData[1].cost.id[1]) > 0 then
        showReds = 1
    end
    GlobalHooks.UI.ShowRedPoint(self.comps.lb_tip, showReds, 'SpringFestivalPray')
    
    --剩余次数
    self.comps.btn_pray.Text = Util.GetText(Constants.SpringFestival.Pray)..'('..Util.GetText('pvp_rewardcount',self.times)..')'
    
    local costRefineCostData = {}
    costRefineCostData.cost ={}
    costRefineCostData.cost.id = {self.actData[1].cost.id[1]}
    costRefineCostData.cost.num = {self.actData[1].cost.num[1]}
    local costs = ItemModel.ParseCostAndCostGroup(costRefineCostData)
    local cost = costs[1]
    UIUtil.SetEnoughItemShowAndLabel(self,self.comps.ib_infobg,self.comps.lb_itemnum,cost,nil)
    
    self.comps.btn_pray.TouchClick =function()
        _M.GetItemUI = false
        
        if self.times == 0 then
            GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.SpringFestival.PrayLimit))
            return
        end
        if ItemModel.CountItemByTemplateID(self.actData[1].cost.id[1]) == 0 then
            GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.SpringFestival.NotEnoughCoin))
            return
        end
        self.comps.btn_pray.Enable=false
        SpringFestivalModel.RequestSpringFestivalPrayReward(function (rsp)
            self.times = rsp.s2c_times
            self.comps.btn_pray.Text = Util.GetText(Constants.SpringFestival.Pray)..'('..Util.GetText('pvp_rewardcount',self.times)..')'
            
            --祈福完判断红点
            local redtip = 0
            if self.times > 0 and ItemModel.CountItemByTemplateID(self.actData[1].cost.id[1]) > 0 then
                redtip = 1
                GlobalHooks.UI.SetRedTips("springfestival",1)
            else
                GlobalHooks.UI.SetRedTips("springfestival",0)
            end
            GlobalHooks.UI.ShowRedPoint(self.comps.lb_tip, redtip, 'SpringFestivalPray')
            
            --刷新主页面的福气币
            EventManager.Fire('Event.SpringFestival.BuyItem',{})
            
            --重组掉落
            local drop={}
            for i, v in ipairs(rsp.s2c_reward_items) do
                table.insert(drop,{id = v.TemplateID,num = v.Qty})
            end
            for i = 1, #drop do
                PlayFlyEffect(self,"/res/effect/ui/ef_ui_xiuxing_click_02.assetbundles",drop)
            end
        end)
    end
end


local function InitBtn(self)
    self.comps.btn_exchange.TouchClick=function()
        GlobalHooks.UI.OpenUI('SpringFestivalStore',0)
    end
    self.comps.cvs_help.Transform.localPosition = Vector3(self.comps.cvs_help.Transform.localPosition.x,self.comps.cvs_help.Transform.localPosition.y,self.comps.cvs_help.Transform.localPosition.z-800)
    SpringFestivalModel.SetHelpCvsVisibleOrInvisible(self.comps.btn_rule,self.comps.cvs_help)
end


function _M.OnEnter(self,params)
    self.comps.btn_pray.Enable=true
    self.actInfo = params or SpringFestivalModel.GetActInfoByTag(string.lower(self.ui.tag))
    if not SpringFestivalModel.CheckIsOpening(self.actInfo.end_time) then
        self.ui:Close()
        return
    end
    
    self.actData = SpringFestivalModel.GetSpringActBySheetName(self.actInfo.sheet_name)
    SpringFestivalModel.SetOpeningTime(self.comps.tb_time,self.actInfo.start_time,self.actInfo.end_time)
    InitBtn(self)
    
    PlaySmokeEffect(self,self.comps.cvs_effect,"/res/effect/ui/ef_ui_smoke.assetbundles")
    
    SpringFestivalModel.RequestSpringFestivalPrayInfo(function (rsp)
        self.times = rsp.s2c_times
        Pray(self)
    end)
end


function _M.OnExit(self)
    ReleaseAllEffect(self)
    ReleaseAllTimer(self)
end


return _M