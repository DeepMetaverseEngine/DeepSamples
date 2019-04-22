local DisplayUtil = require("Logic/DisplayUtil")
local Util = require("Logic/Util")
local UIUtil = require 'UI/UIUtil.lua'
local ScrollBarExt = require("Logic/ScrollBarExt")
local QuestUtil = require("UI/Quest/QuestUtil")
local QuestNpcDataModel = require'Model/QuestNpcDataModel'
local QuestModel = require "Model/QuestModel"

local _M = {}
_M.__index = _M


local self = {}
local sceneready = false
local QuestType = 
{
    [1] = "#static/TL_hud/output/TL_hud.xml|TL_hud|98",--主线
    [2] = "#static/TL_hud/output/TL_hud.xml|TL_hud|97",--支线
    [3] = "#static/TL_hud/output/TL_hud.xml|TL_hud|3",--活动 

    [5] = "#static/TL_hud/output/TL_hud.xml|TL_hud|11",--引导
    [7] = "#static/TL_hud/output/TL_hud.xml|TL_hud|9",--寻龙诀
    [8] = "#static/TL_hud/output/TL_hud.xml|TL_hud|193",--渡劫

    [1000] = "#static/TL_hud/output/TL_hud.xml|TL_hud|7",--师门
    [1001] = "#static/TL_hud/output/TL_hud.xml|TL_hud|6",--仙盟
    [1003] = "#static/TL_hud/output/TL_hud.xml|TL_hud|229",--委托任务
    [1011] = "#static/TL_hud/output/TL_hud.xml|TL_hud|143",--仙盟悬赏


    [2000] = "#static/TL_hud/output/TL_hud.xml|TL_hud|144",--仙盟破坏
    [2100] = "#static/TL_hud/output/TL_hud.xml|TL_hud|145",--个人押镖
    [2200] = "#static/TL_hud/output/TL_hud.xml|TL_hud|272",--个人护镖



}
local function StopFouceQuest(eventname,param)
    --print("StopFouceQuest")

    _M.clearEffect()
end

local function ReleaseAll3DEffect()
    if self.effect then
        for _, val in pairs(self.effect) do
            RenderSystem.Instance:Unload(val)
        end
        self.effect = {}
    end
end


function _M.init(cvs)

    print("QuesthudInit---------------------")
    self.cvs = cvs
 
    self.cellKuang = self.root:FindChildByEditName("cvs_kuang", true)
    self.cellLabel = self.root:FindChildByEditName("lb_wenzi3", true)
    self.cell = self.root:FindChildByEditName("fn_b1", true)
    self.cell.Visible = false
    self.scrollPan = self.root:FindChildByEditName("sp_oar", true)
    self.scrollPan.Scrollable.Container:RemoveAllChildren(true)
    self.border = self.cellKuang.Height - self.cellLabel.Y - self.cellLabel.Height
    self.scrollExt = ScrollBarExt.new(self.scrollPan, self.cell, _M.updateCell)

    --print('self.scrollExt self.scrollExt',self.scrollExt, self)
    self.focusEff = {}
    self.completeEffs = {}
    self.TouchList = {}
    self.canShowCompleteEff = false
    self.focusQuestId = nil
    self.itemShow = nil
    self.cellH = self.cell.Height
    self.lastKuangHeight = 0
    self.lastQueststate = -1
    self.lastQuestId = 0
    self.completeId = {}
    self.effect = {}
    self.initQuest = false
    _M.initEffect()
    --self:reloadData()
    
    -- self.onQuestEvents = {
    --     ["Event.Quest.AddQuest"] = _M.onAddQuest,
    --     ["Event.Quest.Accept"] = _M.onAddQuest,
    --     ["Event.Quest.RemoveQuest"] = _M.onRemoveQuest,
    --     ["Event.Quest.ProgressesChange"] = _M.onChangeQuest,
    --     ["Event.Quest.Complete"] = _M.onCompleteQuest,
    --     ["Event.Quest.ChangeTracing"] = _M.onChangeTracing,
    --     ["Event.Quest.Init"] = _M.reloadData,

    -- }
    self.onQuestEvents = {
        ["Event.Quest.AddQuest"] = _M.onAddQuest,
        ["Event.Quest.Accept"] = _M.onAddQuest,
        ["Event.Quest.RemoveQuest"] = _M.onRemoveQuest,
        ["Event.Quest.ProgressesChange"] = _M.onChangeQuest,
        ["Event.Quest.Complete"] = _M.onCompleteQuest,
        ["Event.Quest.ChangeTracing"] = _M.onChangeTracing,
        ["Event.Quest.Submited"] = _M.OnSubmited,
        ["Event.Quest.Init"] = _M.reloadData,
    }

   
    
end

function _M.initEffect()
        self.focusEff = {}
  
end

local function ChangeScene(evtName, params)
    --print('ChangeScene', param.id)
    local Quest = QuestModel.GetQuest(params.id)
    if Quest ~= nil then
        local cell = self.scrollExt:getCellByData(Quest)
        _M.showFouceEff(params.id,cell)
    end
end

-- local function AddQuestToQuestList(Quest)
--     if not Quest.tracing then return end
--     if Quest.Static == nil then return end
--     if Quest.state < QuestState.NotAccept then return end
--     if Quest.state == QuestState.NotAccept and Quest.Static.push == 0 then return end
--     if Quest.state > QuestState.CompletedNotSubmited then return end
--     QuestModel.QuestMap = QuestModel.QuestMap or {}
--     QuestModel.GetQuests() = QuestModel.GetQuests() or {}
--     QuestModel.QuestMap[Quest.id] = Quest
--     if #QuestModel.GetQuests() < maxN then
--         table.insert(QuestModel.GetQuests(), Quest)
--     end
-- end
function _M.reloadData()
        
    if not GameGlobal.Instance.netMode  then
        return 
    end

    if self.initQuest then
        return
    end
    
    ReleaseAll3DEffect()
    _M.clearEffect()
   
    QuestModel.reloadData()
    _M.ResetQuests()
    
end


function self.Notify(evtName, QuestId, Quest)
    if not sceneready then
        return
    end
    local func = self.onQuestEvents[evtName]
    if func then
        func(QuestId, Quest)
    end
end

function _M.CanScroll()
    local is_max = self.scrollExt:ConnextHeight() > self.scrollPan.Height
    if not is_max then
        self.scrollExt:ResetCellPos()
    end
    self.scrollPan.Scrollable.Scroll.vertical = is_max
end
function _M.onAddQuest(QuestId, Quest)
    --print("onAddQuest---------------------------")
    QuestModel.onAddQuest(QuestId,Quest)
    --ocal static = QuestNpcDataModel.GetQuestData(Quest)
    _M.tryAddQuest(QuestId, Quest)
   
end

function _M.OnSubmited(QuestId, Quest)
    --print("onAddQuest---------------------------")
    QuestModel.OnSubmited(QuestId,Quest)
   
end

function _M.onRemoveQuest(QuestId, Quest)
   print("onRemoveQuest",QuestId)
  
    if QuestModel.GetQuest(QuestId) then

        if self.effect[QuestId] ~= nil then
            RenderSystem.Instance:Unload(self.effect[QuestId])
            self.effect[QuestId] = nil
        end
        
        self.TouchList[QuestId] = nil
        if self.focusQuestId == QuestId then
            self.focusQuestId = nil
        end

        if self.ShowCompleteList ~= nil then
            self.ShowCompleteList[QuestId] = nil
        end
        local cell = self.scrollExt:getCellByData(Quest)
        if self.focusEff and self.focusEff.id == QuestId then
            _M.clearEffect()
        end

        self.scrollExt:removeData(Quest)
        QuestModel.onRemoveQuest(QuestId,Quest)

        local addquest = QuestModel.tryPickOneQuest()
        if addquest ~= nil then
            for i,v in ipairs(addquest) do
               _M.tryAddQuest(v.id,v)
            end
        end
        DataMgr.Instance.QuestData.IsInit = true
        _M.CanScroll()
    end
end

function _M.onChangeQuest(QuestId, Quest)
    --print('onChangeQuest', QuestId, QuestModel.GetQuest(param.id))
    QuestModel.onChangeQuest(QuestId,Quest)
    if QuestModel.GetQuest(QuestId) then
        self.scrollExt:updateData(Quest)
    end
end
function _M.onCompleteQuest(QuestId, Quest)
    QuestModel.onCompleteQuest(QuestId,Quest)
    local static = QuestNpcDataModel.GetQuestData(Quest)
    if QuestModel.GetQuest(QuestId) then
        self.scrollExt:updateData(Quest)
    end
end

function _M.onChangeTracing(QuestId, Quest)
   
    -- print('_M:onChangeTracing', QuestId, Quest.tracing)
    if QuestModel.GetQuest(QuestId) and not Quest.tracing then
        -- print("removeQuest ", QuestId)
       _M.onRemoveQuest(QuestId, Quest)
    elseif Quest.tracing then
        -- print("tryAddQuest", QuestId)
        _M.tryAddQuest(QuestId, Quest)
    end
end

function _M.tryAddQuest(QuestId, Quest)
    local addQuest = QuestModel.tryAddQuest(QuestId,Quest)
    if addQuest then
         local idx = table.indexOf(QuestModel.GetQuests(), Quest)
         self.scrollExt:addData(Quest, idx)
         --self.scrollExt:ResetCellPos(idx)
        QuestModel.AutoRunQuest(Quest)
    else
        if self.lastQuestId == Quest.id then
            self.lastQuestId = 0
        end
         self.scrollExt:updateData(Quest)
    end
    _M.CanScroll()
end

function _M.ResetQuests()
    local quest = QuestModel.GetQuests()
    if #quest > 0 then
        self.initQuest = true
    end
    self.scrollExt:resetDatas(quest)
    
end
local function StartFouceQuest(evtName, params)
    --print("StartFouceQuest", evtName)
    local Quest = QuestModel.GetQuest(params.id)
    if Quest == nil then 
        self.effectQuestId = params.id
        return
    end
    local cell = self.scrollExt:getCellByData(Quest)
    if cell then
          local kuang = cell:FindChildByEditName("cvs_kuang", true)
            --print("StartFouceQuest1", self.focusQuestId,param.Quest.id)
          if self.focusQuestId ~= Quest.id or self.lastQueststate ~= Quest.state or self.lastKuangHeight ~= kuang.Height then
            _M.showFouceEff(Quest.id,cell)
            self.lastKuangHeight = kuang.Height
            self.focusQuestId = Quest.id
            self.lastQueststate = Quest.state
            self.lastQuestId = Quest.id
          else 
            --print("StartFouceQuest2", self.focusQuestId,param.Quest.id)
          end
    else
       self.effectQuestId = params.id
    end
end
function _M.clearEffect()
    --print("clearEffect")
    if self == nil then return end
    self.focusQuestId = nil
    self.lastKuangHeight = 0
    self.lastQueststate = -1
    self.lastQuestId = 0
    if self.focusEff and self.focusEff.effectid then
        RenderSystem.Instance:Unload(self.focusEff.effectid)
        self.focusEff = nil
    end
    self.effectQuestId = 0
end




function _M.showFouceEff(id,cell)
    local effectName = "/res/effect/ui/ef_ui_frame.assetbundles"
    local parent = cell:FindChildByEditName("cvs_kuang", true)
    local Width = parent.Width
    local Height = parent.Height
    self.focusEff = self.focusEff or {}
    self.focusEff.id = id
    if self.focusEff.effectid then
        --print("self.focusEff.effectid",self.focusEff.effectid)
        --RenderSystem.Instance:Unload(self.focusEff.effectid)
        local eff=RenderSystem.Instance:GetAssetGameObject(self.focusEff.effectid)
        if eff then
            eff.gameObject.transform:SetParent(parent.Transform)
            eff.gameObject.transform.localPosition = Vector3(0, 0, 0)
            local vector3move = eff.gameObject:GetComponentInChildren("Vector3Move")
            vector3move:setRect(Width,Height)
            return
        else
            RenderSystem.Instance:Unload(self.focusEff.effectid)
        end
    end
    
    self.focusEff.effectid = Util.PlayEffect(effectName,{
        UILayer = true,LayerOrder = 100,
        Clip = self.scrollPan.Transform,
        Parent = parent.Transform,Vectormove = {x = Width,  y = Height}}, 
        9999999)
end


function _M.showEffect(effectName, cell,Questid)
    --print("showEffect1",effectName)
    local kuang = cell:FindChildByEditName("cvs_kuang", true)
    if kuang == nil then
        return 
    end
    --print("showEffect2")
    local parent = kuang
    local pos = Vector2(kuang.Width * 0.5,kuang.Height*0.5)
    --print("showEffect5",pos)
    local Width = kuang.Width
    local Height = kuang.Height
    local transSet = TransformSet()
    transSet.Pos = Vector3(pos.x, -pos.y,0)
    transSet.Parent = kuang.Transform
    transSet.Scale = Vector3(1,  Height / self.cellH, 1)
    transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = 100
    transSet.Clip = self.scrollPan.Transform
    self.effect = self.effect or {}
    self.effect[Questid] = RenderSystem.Instance:PlayEffect(effectName, transSet,0)
     
end

local function InitShowCompleteEff()
    -- if self.Timer ~= nil then
    --     LuaTimer.Delete(self.Timer)
    --     self.ShowCompleteList = {}
    -- end
    -- self.Time = LuaTimer.Add(0, 33, function()
    --     if self.ShowCompleteList ~= nil then
    --         for i,v in pairs(self.ShowCompleteList) do
    --             local Quest = QuestModel.GetQuest(v)
    --             if Quest ~= nil then
    --                 local cell = self.scrollExt:getCellByData(Quest)
    --                 if cell and cell.UnityObject and cell.UnityObject.activeInHierarchy then
    --                      _M.showEffect("/res/effect/ui/ef_ui_completestaskframe.assetbundles", cell)
    --                     self.ShowCompleteList[i] = nil
    --                 end
    --             end
               
    --         end
    --     end
    --     return true
    -- end)
end
function _M.showCompleteEff(cell,Questid)

    _M.showEffect("/res/effect/ui/ef_ui_completestaskframe.assetbundles", cell,Questid)
    --self.ShowCompleteList = self.ShowCompleteList or {}
    --self.ShowCompleteList[Questid] = cell
    --table.insert(self.ShowCompleteList,Questid)
end


function _M.onCellClick(sender)
    --print("onCellClick",sender.UserTag)
    if  not TLBattleScene.Instance or GameUtil.IsPreparePickItem() then
        --print("TLBattleScene.Instance.IsPickUp")
        return
    end
    local Quest = DataMgr.Instance.QuestData:GetQuest(sender.UserTag)
    if not Quest then return end
    --print("onCellClick1",Quest.state)
    if Quest.state == QuestState.NotCompleted and not string.IsNullOrEmpty(Quest.Static.noFinish_DoSth) then
        local data = FunctionUtil.OpenFunction(Quest.Static.noFinish_DoSth)
        if data then
            --print("onCellClick2",Quest.state)
           self.focusQuestId = 0
           return
        end
    end
    --if self.lastQuestId == sender.UserTag then
    --   return 
    --end
 
    --print("onCellClick4",self.TouchList[sender.UserTag])
    if self.TouchList[sender.UserTag] == nil
     or self.TouchList[sender.UserTag] == 0 then
        self.TouchList[sender.UserTag] = 1

        DataMgr.Instance.QuestData.IsInit = true
        EventManager.Fire("Event.Quest.ManualMove",{})
        QuestUtil.doQuest(Quest)
        local id = sender.UserTag
        LuaTimer.Add(2000,function() 
            if self.TouchList[id] ~= nil then
                 self.TouchList[id] = 0
            end
        end)
    end
end

function _M.updateCell(cell, Quest)
    --print('_M:updateCell1', Quest.id)
    cell.Visible = true
    local done = cell:FindChildByEditName("ib_done", true)
    local questbtn = cell:FindChildByEditName("ib_questtype", true)
    if QuestType[Quest.subType] ~= nil then
        UIUtil.SetImage(questbtn,QuestType[Quest.subType])
    end
    done.Visible = Quest.state == QuestState.CompletedNotSubmited and Quest.mainType ~= QuestNpcDataModel.QuestType.TypeDaily
    local static = Quest.Static
    if static == nil then
        cell:FindChildByEditName("lb_wenzi1", true).Text = Quest.id.." is not exist"
        return
    end
    local kuang = cell:FindChildByEditName("cvs_kuang", true)
    kuang.UserTag = Quest.id
    kuang.TouchClick = _M.onCellClick
    kuang.Enable = true
    kuang.EnableChildren = true
    local xian = cell:FindChildByEditName("ib_qiexian", true)
    local name =  QuestUtil.QuestName(Quest,false)
    cell:FindChildByEditName("lb_wenzi1", true).Text = name
    local descLabel = cell:FindChildByEditName("lb_wenzi2", true)
    descLabel.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap;
    descLabel.EditTextAnchor = CommonUI.TextAnchor.L_T;
    descLabel.Text = QuestUtil.QuestTarget(Quest)
   
    --print(descLabel.PreferredSize)
    local pos = GameUtil.GetLastCharPos(descLabel)
    pos.y = pos.y + descLabel.Y
    local countLabel = cell:FindChildByEditName("lb_wenzi3", true)
    local isShowCountLabel = static.progress == 1 
    and Quest.state ~= QuestState.NotAccept 
    and Quest.state ~= QuestState.CompletedNotSubmited
    countLabel.Visible = isShowCountLabel
    if isShowCountLabel then
        countLabel.EditTextAnchor = CommonUI.TextAnchor.R_T
        countLabel.Text = QuestUtil.QuestCount(Quest)
        local size = countLabel.PreferredSize
        local w = descLabel.Width
        local y = pos.y
        if size.x + pos.x <= w then
            y = y - size.y
        else
            -- countLabel.EditTextAnchor = CommonUI.TextAnchor.L_T
            pos.y = pos.y + size.y
        end
        countLabel.Y = y
    end

    local LoopCountLabel = cell:FindChildByEditName("lb_wenzi4", true)
    local isShowLoopCountLabel = QuestUtil.NeedShowLoopNumber(Quest)
    LoopCountLabel.Visible = isShowLoopCountLabel
    if isShowLoopCountLabel then
        LoopCountLabel.Text = QuestUtil.LoopQuestRound(Quest)
    end
    kuang.Height = pos.y + self.border
    cell.Height = pos.y + self.border
    xian.Y = kuang.Height 
    -- self.canShowCompleteEff = true
    --print("self.effectQuestId ",self.effectQuestId )
    if self.focusEff and self.focusEff.id == Quest.id then
        _M.showFouceEff(Quest.id,cell)
    end
    -- if self.effectQuestId == Quest.id then
        
    --     self.effectQuestId = 0
    -- end
    if Quest.state == QuestState.CompletedNotSubmited 
        and self.completeId[Quest.id] == nil then
       _M.showCompleteEff(cell,Quest.id)
       self.completeId[Quest.id] = 1
    end

    _M.updateAward(static, cell:FindChildByEditName("cvs_item", true))

    _M.CloseItem()
end

function _M.updateAward(static, cvs)
    --print("static.item"..static.item)
    local items = {}
    if  static.item ~= 0 then 
        items = {{static.item,1}}
    else 
        local pro = DataMgr.Instance.UserData.Pro
       
        if pro == RoleProType._YiZu then
             if  static.item_yz ~= 0 then
                 items = {{static.item_yz,1}}
             end
        elseif pro == RoleProType._TianGong then
             if  static.item_tg ~= 0 then
                 items = {{static.item_tg,1}}
             end
        elseif pro == RoleProType._KunLun then
            if  static.item_kl ~= 0 then
                 items = {{static.item_kl,1}}
             end
        elseif pro == RoleProType._QinqQiu then
            if  static.item_qq ~= 0 then
                 items = {{static.item_qq,1}}
             end
        end
    end
    --local items = Util.str2Items(itemcontext)--Util.str2Items(static.item)
    cvs.Visible = #items > 0
    if #items > 0 then
        DisplayUtil.fillAwards({cvs}, items,function(itemShowNode,itemShow)
           self.itemShow = itemShow
           itemShow:SetPos(250,100)
        end)
    end
end
local function OnNpcTalk (evtName, param)
    --print("NpcTalk2")
    self.NpcTalk = param.isTalk
    if self.NpcTalk then
        _M.CloseItem()
    end

    --_M.checkDoQuest()
end

--local function OnDialogueTalk (evtName, param)
--    --print("NpcTalk2")
--    self.DialogueTalk = param.isTalk
--    if self.DialogueTalk then
--        _M.CloseItem()
--    end
--    --_M.checkDoQuest()
--end
function _M.CloseItem()
    if self.itemShow ~= nil then
        self.itemShow.IsSelected = false
        if self.itemShow.ui ~= nil then
            self.itemShow:Close()
        end
        self.itemShow = nil
        end
end
local function OnShowUI(eventname,params)
    --print("--------------OnShowMainHud---------------")
    local root = HudManager.Instance:GetHudUI("TeamQuest")
    self.root = root:FindChildByEditName("cvs_mission",true)
    _M.init(self.root)
end
local function CheckAndStopQuest(eventname,params)
      if self.focusQuestId  == params.id then
            StopFouceQuest(eventname,params)
      end
end

--事件库返回的自动寻路
local function DoQuestAutoMove(eventname,params)
   
    local Quest = DataMgr.Instance.QuestData:GetQuest(params.id)
    if Quest == nil then
        print("DoQuestAutoMove quest id =",params.id.." is nil")
        return
    end
    --print_r("DoQuestAutoMove",params)
    local type = params.type or 1
    local monsterID = params.monsterID or 0
    local autoMove = params.autoMove
    local roadName = params.roadName or ""
    local hints = params.hints
    local action = nil
    --print("--------------DoQuestAutoMove---------------",autoMove)
    if type == 1 and monsterID ~= 0 then
          action = MoveAndBattle(monsterID)
          action.AimX = params.aimX or 0
          action.AimY = params.aimY or 0
          action.MapId = params.mapId 
    else
          action = MoveEndAction()
          action.AimX = params.aimX or 0
          action.AimY = params.aimY or 0
          action.MapId = params.mapId
    end
          if autoMove == 3 then --1是手动 2是自动 3是提示不能寻路
            action.RoadName = "0"
          else 
            action.RoadName = roadName
          end 
          
          action.Radar = params.radar or 0
          action.QuestId = params.id
          action.hints = params.hints
     if TLBattleScene.Instance.Actor and autoMove == 2 and action then
        TLBattleScene.Instance.Actor:AutoRunByAction(action)
     else 
        if self.focusQuestId  == params.id then
            StopFouceQuest(eventname,params)
        end
     end
     
     Quest.TempAction = action
end

--退出场景时调用，参数：是否短线重连触发的切场景
local function OnExitScene(reconnect)
    _M.clearEffect()
    self.initQuest = false
    self.completeId = {}
    --self.celllist = {}
    self.effectQuestId = 0
    if reconnect then
       -- reloadData()
    end
end

--进入场景时调用
local function OnEnterScene()
    self.completeId = {}
    _M.initEffect()
    InitShowCompleteEff()
    --print (" lua Hud main OnEnterScene ")
end
--自动任务触发的自动执行
local function BeginQuest(eventname,params)
   
    local Quest = DataMgr.Instance.QuestData:GetQuest(params.id) 
     --print("beginQuest")
    if Quest then
        local idx = table.indexOf(QuestModel.GetQuests(), Quest)
        self.scrollExt:ResetCellPos(idx)
        QuestUtil.doQuest(Quest)
    end
    
end

local function FirstInitFinish()
    self.initQuest = false
    sceneready = true
    _M.reloadData()
    LuaTimer.Add(1000,function()
       DataMgr.Instance.QuestData.IsInit = true
    end)
end

local function initial()
    print ("QuestHud initial---------------------- ")
    sceneready = false
    DataMgr.Instance.QuestData:AttachLuaObserver('QuestHud',self)
    EventManager.Subscribe(Events.UI_HUD_LUAHUDINIT, OnShowUI)
    EventManager.Subscribe("Event.Quest.DoQuest", StartFouceQuest)
    EventManager.Subscribe("Event.Quest.ChangeScene", ChangeScene)
    EventManager.Subscribe("Event_QuestEvent_AutoMove",DoQuestAutoMove)
    EventManager.Subscribe("Event.Quest.CheckAndStopQuest",CheckAndStopQuest)
    EventManager.Subscribe("Event.Quest.StopQuestAutoRun", StopFouceQuest)
    EventManager.Subscribe("Event.Actor.DeadEvent", StopFouceQuest)
    EventManager.Subscribe("Event.Quest.BeginQuest", BeginQuest)
    EventManager.Subscribe("Event.Scene.FirstInitFinish", FirstInitFinish)

end

local function fin()
    print ("---------------------QuestHud fin ")
    StopFouceQuest()
    EventManager.Unsubscribe(Events.UI_HUD_LUAHUDINIT, OnShowUI)
    DataMgr.Instance.QuestData:DetachLuaObserver('QuestHud')
    EventManager.Unsubscribe("Event.Quest.DoQuest", StartFouceQuest)
    EventManager.Unsubscribe("Event.Quest.ChangeScene", ChangeScene)
    EventManager.Unsubscribe("Event_QuestEvent_AutoMove",DoQuestAutoMove)
    EventManager.Unsubscribe("Event.Quest.CheckAndStopQuest",CheckAndStopQuest)
    EventManager.Unsubscribe("Event.Quest.StopQuestAutoRun", StopFouceQuest)
    EventManager.Unsubscribe("Event.Actor.DeadEvent", StopFouceQuest)
    EventManager.Unsubscribe("Event.Quest.BeginQuest", BeginQuest)
    EventManager.Unsubscribe("Event.Scene.FirstInitFinish", FirstInitFinish)
end

return { initial = initial, fin = fin, OnEnterScene = OnEnterScene, OnExitScene = OnExitScene }
    