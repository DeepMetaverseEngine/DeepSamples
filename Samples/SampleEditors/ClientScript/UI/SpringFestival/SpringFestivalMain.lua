---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by xujing.xu.
--- DateTime: 2019/1/16 11:03
---春节活动

local _M = {}
_M.__index = _M

local SpringFestivalModel = require 'Model/SpringFestivalModel'
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local TimeUtil = require 'Logic/TimeUtil'
local ServerTime = require 'Logic/ServerTime'
local ItemModel = require 'Model/ItemModel'
local DisplayUtil = require"Logic/DisplayUtil"


function _M.OnInit(self)
    --全屏自适应
    DisplayUtil.adaptiveFullSceen(self.comps.ib_mainbg)
    DisplayUtil.adaptiveFullSceen(self.comps.cvs_tips)
end


local function PlayEffect(self,node,res)
    local symbol = math.random(1,100) > 50 and 1 or -1
    local transSet = TransformSet()
    transSet.Layer = Constants.Layer.UI
    transSet.LayerOrder = self.ui.menu.MenuOrder
    transSet.Pos = Vector3(symbol == 1 and 2 or 0,0,0)
    transSet.Scale= Vector3(1*symbol,1,1)
    transSet.DisableToUnload = true
    transSet.Parent = node.Transform

    self.eff =self.eff or {}
    local num = #self.eff + 1
    self.eff[num] = RenderSystem.Instance:LoadGameObject(res, transSet, function(aoe)
    end)
end
local function ReleaseEffect(self)
    if self.eff then
        for i, v in ipairs(self.eff) do
            RenderSystem.Instance:Unload(self.eff[i])
        end
        self.eff= {}
    end
end


--显示暂未开放
local function ShowNotOpenTips(self,data)
    self.comps.cvs_tipmask.Visible =true
    local lb_tip1 = self.comps.cvs_tipmask:FindChildByEditName('lb_tip1',true)
    local lb_tip2 = self.comps.cvs_tipmask:FindChildByEditName('lb_tip2',true)
    lb_tip1.Text =Util.GetText(Constants.SpringFestival.NotOpenTime,Util.GetText(data.activity_name))
    lb_tip2.Text = Util.GetText(Constants.SpringFestival.OpenTime,data.start_time)
    self.comps.cvs_tipmask.TouchClick = function()
        self.comps.cvs_tipmask.Visible =false
    end
end

--判断是否打开UI或给出未开放提示
local function OpenUIOrGiveTips(self,isOpen,data)
    if isOpen then
        local firstWord = string.sub(data.sheet_name,1,1)
        local sheetName = string.gsub(data.sheet_name,firstWord,string.upper(firstWord))
        local source = {tag = sheetName ,info = {'UI/SpringFestival/SpringFestival'..sheetName,data.client_xml}}
        local act = GlobalHooks.UI.CreateUI(source,0,data)
        MenuMgr.Instance:AddMenu(act.ui.menu)
    else
        ShowNotOpenTips(self,data)
    end
end


local function SetEachActInfo(self,node,index)

    node.Position2D = Vector2(node.Position2D.x+50,node.Position2D.y+20)

    local springInfo = SpringFestivalModel.GetActInfoById(index)
    local time = System.DateTime.Parse(springInfo.start_time)
    --名称&开放时间
    local lb_actname = node:FindChildByEditName('lb_actname',true)
    lb_actname.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
    lb_actname.Text = Util.GetText(springInfo.activity_name)
    local lb_time = node:FindChildByEditName('lb_time',true)
    lb_time.Text = time.Month..'.'..time.Day..Util.GetText(Constants.SpringFestival.OpenWord)
    local cvs_time = node:FindChildByEditName('cvs_time',true)
    cvs_time.Position2D = Vector2(cvs_time.Position2D.x,cvs_time.Position2D.y+22)
    
    local btn_choose =node:FindChildByEditName('btn_choose',true)
    local cvs_eff = node:FindChildByEditName('cvs_eff',true)
    --处理偏移(btn/lab/cvs)
    local offset = index%2 == 0 and -5 or 15
    lb_actname.Position2D = Vector2(lb_actname.Position2D.x,lb_actname.Position2D.y + offset)
    btn_choose.Position2D = Vector2(btn_choose.Position2D.x,btn_choose.Position2D.y + offset)
    cvs_eff.Position2D = Vector2(cvs_eff.Position2D.x,cvs_eff.Position2D.y + offset)
    
    --2019/1/17 20:05
    --与华仔确认过，直接String转DateTime，在与本地时间比较 by华仔
    local isOpen = TimeUtil.inTime(System.DateTime.Parse(springInfo.start_time),System.DateTime.Parse(springInfo.end_time),
            ServerTime.getServerTime():ToLocalTime())
    local ib_opened = node:FindChildByEditName('ib_opened',true)
    ib_opened.Visible = isOpen
    
    --麦穗
    PlayEffect(self,cvs_eff,'/res/effect/ui/ef_ui_chunjie03.assetbundles')
    if isOpen then
        --闪光
        PlayEffect(self,cvs_eff,'/res/effect/ui/ef_ui_chunjie02.assetbundles')
    end
    
    self.moveTime=self.moveTime or {}
    local y=math.random(0,20)
    self.moveTime[index] = LuaTimer.Add(0,100,
            function ()
                y = y + math.random(5,10)
                btn_choose.Position2D =Vector2(btn_choose.Position2D.x,btn_choose.Position2D.y + math.sin(y*Mathf.Deg2Rad))
                lb_actname.Position2D =Vector2(lb_actname.Position2D.x,lb_actname.Position2D.y + math.sin(y*Mathf.Deg2Rad))
                cvs_eff.Position2D =Vector2(cvs_eff.Position2D.x,cvs_eff.Position2D.y + math.sin(y*Mathf.Deg2Rad))
                return true
            end)
    
    btn_choose.TouchClick =function()
        OpenUIOrGiveTips(self,isOpen,springInfo)
    end
    
end

--通过tag打开指定活动页面
local function OpenIndexActivity(self)
    local temp = SpringFestivalModel.GetActInfoByTag(self.subTag)
    local isOpen = TimeUtil.inTime(System.DateTime.Parse(temp.start_time),System.DateTime.Parse(temp.end_time),
            ServerTime.getServerTime():ToLocalTime())
    OpenUIOrGiveTips(self,isOpen,temp)
end

local function InitActListAndInfo(self)
    self.allSpringAct = SpringFestivalModel.GetAllSpringAct()
    self.comps.lb_fubinum.Text = ItemModel.CountItemByTemplateID(Constants.VirtualItems.GoodFortune)

    self.comps.cvs_act.Visible = false
    local function eachupdatecb(node,index)
        SetEachActInfo(self,node,index)
    end
    UIUtil.ConfigHScrollPanWithOffset(self.comps.sp_list,self.comps.cvs_act,#self.allSpringAct,30,eachupdatecb)
end


function _M.OnEnter(self,params)
    
    self.firework =SpringFestivalModel.PlayFullScreenEffect('/res/effect/ui/ef_ui_chunjie01.assetbundles',self.ui.menu.MenuOrder)
    self.comps.cvs_help.Transform.localPosition = Vector3(self.comps.cvs_help.Transform.localPosition.x,self.comps.cvs_help.Transform.localPosition.y,self.comps.cvs_help.Transform.localPosition.z-800)
    SpringFestivalModel.SetHelpCvsVisibleOrInvisible(self.comps.btn_rule,self.comps.cvs_help)

    --有参无动画，无参有动画
    self.ui.menu:SetCompAnime(self.ui.menu,params and UIAnimeType.NoAnime or UIAnimeType.FadeMoveUp)
    
    function _M.OnStoreClose()
        self.comps.lb_fubinum.Text = ItemModel.CountItemByTemplateID(Constants.VirtualItems.GoodFortune)
    end
    EventManager.Subscribe("Event.SpringFestival.BuyItem", _M.OnStoreClose)
    
    InitActListAndInfo(self)
    if params then
        self.subTag = string.lower(params)
        OpenIndexActivity(self)
    end
    self.comps.btn_store.TouchClick=function()
        GlobalHooks.UI.OpenUI('SpringFestivalStore',0)
    end
end


function _M.OnExit(self)
    self.comps.cvs_tipmask.Visible = false
    EventManager.Unsubscribe("Event.SpringFestival.BuyItem", _M.OnStoreClose)
    ReleaseEffect(self)
    if self.firework then
        RenderSystem.Instance:Unload(self.firework)
    end
    if self.moveTime then
        for i, v in ipairs(self.moveTime) do
            LuaTimer.Delete(self.moveTime[i])
        end
        self.moveTime = {}
    end
end


return _M