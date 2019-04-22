local AccordionExt = require("Logic/AccordionExt")
local RadioGroupExt = require("Logic/RadioGroupExt")
local DisplayUtil = require("Logic/DisplayUtil")
local QuestUtil = require("UI/Quest/QuestUtil")
local QuestModel = require("Model/QuestModel")
local Util = require("Logic/Util")

local QuestUI = {}
QuestUI.__index = QuestUI


function QuestUI:onInitItem(accExt, node, btn, data)
    AccordionExt.onInitItemNode(accExt, node, btn, data)
    self:onRefreshItem(accExt, node, btn, data)
end
function QuestUI:onRefreshItem(accExt, node, btn, data)
    local icon = node:FindChildByEditName("ib_tip", true)
    local path = nil
    if data.Quest.state == QuestState.NotCompleted then
        path = '#dynamic/TL_mission/output/TL_mission.xml|TL_mission|0'
    elseif data.Quest.state == QuestState.CompletedNotSubmited then
        path = '#dynamic/TL_mission/output/TL_mission.xml|TL_mission|1'
    end
    icon.Visible = path ~= nil
    if path ~= nil then
        DisplayUtil.setImg(icon, path)
    end
end

function QuestUI.Notify(evtName, QuestId, Quest)
    local func = QuestUI.self.onQuestEvents[evtName]
    if func then
        local params = {Quest = Quest}
        func(evtName, params,QuestUI.self)
    end
end
function QuestUI:onDiscardBtnClick()
    QuestModel.requestGiveUp(self.Quest.id, function()

    end)
end
function QuestUI:onTraceBtnClick(sender)
    if not self.Quest then return end
    self.Quest.tracing = not self.Quest.tracing
    sender.IsChecked = self.Quest.tracing
    DataMgr.Instance.QuestData:Notify('Event.Quest.ChangeTracing', self.Quest)
    --print("onTraceBtnClick")
end
function QuestUI:onBeginBtnClick()
    if not self.Quest then return end

    if QuestUtil.doQuest(self.Quest,true) then
        --print("onBeginBtnClick")
        self:CloseMenu()
    end
end

function QuestUI:onProcessBtnClick()
    if not self.Quest then return end
    if QuestUtil.doQuest(self.Quest,true) then
        --print("onProcessBtnClick")
        self:CloseMenu()
    end
end

function QuestUI:CloseMenu()
    --print("CloseMenuCloseMenu")
    GlobalHooks.UI.CloseUIByTag('QuestMain')
end

function QuestUI:QuestSorter(a, b)
    return a.id < b.id
end

function QuestUI:createQuestTree()
    local _, data = self.radioExt:selectedIdx()
    local Quests = nil
    if data == 'accepts' then
        Quests = DataMgr.Instance.QuestData.Accepts
    else
        Quests = DataMgr.Instance.QuestData.NotAccepts
    end
    local tableQuest = {}
    for Quest in Slua.iter(Quests) do
        table.insert(tableQuest,Quest)
    end
    --print_r('tableQuest',tableQuest)
    table.sort(tableQuest, function(a, b)
            local adata = a.Static
            local bdata = b.Static

            if adata.sub_type ~= bdata.sub_type then
                local asort = 999
                local bsort = 999
                for i,v in ipairs(QuestModel.sortindex) do
                    if v == adata.sub_type then
                        asort = i
                    end
                    if v == bdata.sub_type then
                        bsort = i
                    end
                    if asort ~= 999 and bsort ~= 999 then
                        break
                    end
                end
                if asort ~= bsort then
                    return asort < bsort
                end
            end
           
            if adata.sort ~= bdata.sort then
                return adata.sort > bdata.sort
            end
            return adata.id > bdata.id
        end)
    self.QuestMap = {}
    -- Item:{name:string, isLock:bool, items:<Item>}
    local tmpList = {}
    --for Quest in Slua.iter(Quests) do
    for i,v in pairs(tableQuest) do
        self.QuestMap[v] = true
        local list = tmpList[v.mainType]
        if not list then
            list = {name = Util.GetText("quest_type"..v.mainType), Quest = v, items = {}}
            tmpList[v.mainType] = list
        end

        table.insert(list.items, {
            name = QuestUtil.QuestName(v,true),
            Quest = v,
        })
    end
    self.QuestTree = {}
    local max = table.maxn(tmpList)
    for i = 1, max do
        if tmpList[i] then
            table.sort(tmpList[i], self._self_QuestSorter)
            table.insert(self.QuestTree, tmpList[i])
        end
    end
end

function QuestUI:doSwitchQuestPage(pageIdx)

    if not self.ui.menu.IsRunning then return end
    self.pageIdx = pageIdx
    self:createQuestTree()
    self.accordionExt:setData(self.QuestTree)
    if pageIdx or not self.lastMenuIdx then
        self.lastMenuIdx = 1
        self.lastItemIdx = nil
    end
    self:onSelectItem(self.accordionExt:selectItem(self.lastMenuIdx, self.lastItemIdx))
    self.accordionExt:refreshMenuAndItem()
end

function QuestUI:onSelectItem(data, menuIdx, itemIdx)
    -- if not self.menu or not self.menu.IsRunning then return end
    --print("onSelectItemmenuIdx",menuIdx)
    --print("onSelectItemitemIdx",itemIdx)
    self.lastMenuIdx = menuIdx
    self.lastItemIdx = itemIdx
    self.Quest = data and data.Quest or nil
    self:showQuest()
end

function QuestUI:showQuest()
    self.ui.comps.cvs_content.Visible = self.Quest ~= nil
    self.ui.comps.cvs_tip.Visible = self.Quest == nil
    if self.Quest then
        local static = self.Quest.Static
        self.ui.comps.lb_title.Text = QuestUtil.QuestName(self.Quest,true)
        self.ui.comps.tb_describe.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap;
        self.ui.comps.tb_describe.EditTextAnchor = CommonUI.TextAnchor.L_T;
        self.ui.comps.tb_describe.Text = QuestUtil.QuestDesc(self.Quest)
       
        local targetText = QuestUtil.QuestTarget(self.Quest)
        if self.Quest.Static.progress == 1 and self.Quest.state ~= QuestState.NotAccept then
            targetText = targetText .. QuestUtil.QuestCount(self.Quest, true)
        end
        -- print(self.Quest.id, string.format('<f>%s</f>', targetText))
        self.ui.comps.tb_target.Text = string.format('%s', targetText)
        self.ui.comps.btn_discard.Visible = self.Quest.state ~= QuestState.NotAccept and self.Quest.Static.quit == 1 and self.Quest.state ~= QuestState.CompletedNotSubmited
        --self.ui.comps.tbt_trace.Visible = self.Quest.state ~= QuestState.NotAccept
        -- if self.Quest.state ~= QuestState.NotAccept then
        --     self.ui.comps.tbt_trace.IsChecked = self.Quest.tracing
        -- end
        self.ui.comps.btn_begin.Visible = self.Quest.state == QuestState.NotAccept
        self.ui.comps.btn_process.Visible = self.Quest.state ~= QuestState.NotAccept

        -- awards
        local itemcontext = {}
        if  static.award ~= nil and static.award.key ~= nil and #static.award.key > 0 then 
            for i,v in ipairs(static.award.key) do
                if v > 0 then
                    itemcontext[i] = {v,static.award.val[i]} 
                end
                
            end
        end
        -- print("itemcontext....")
        -- print_r(itemcontext)
        local awards = itemcontext--Util.str2Items(static.item)
        self.ui.comps.lb_expnum.Text = static.exp
        self.ui.comps.lb_goldnum.Text = static.gold
        -- if static.gold > 0 then
        --     table.insert(awards, {Util.GoldId, static.gold})
        -- end
        -- if static.exp > 0 then
        --     table.insert(awards, {Util.ExpId, static.exp})
        -- end
        
        DisplayUtil.fillAwards(self.awardIcons, awards)
    end
end

local acceptStateMap = {
    [QuestState.NotCompleted] = true,
    [QuestState.CompletedNotSubmited] = true,
}
function QuestUI:isCurrState(Quest)
    if not Quest then return end 
    local _, data = self.radioExt:selectedIdx()
    if Quest.state == 'accepts' then
        return acceptStateMap[state]
    else
        return Quest.state == QuestState.NotAccept
    end
end

function QuestUI.onAddQuest(evtName, param,self)
    if self.QuestMap[param.Quest] then return end
    if not self:isCurrState(param.Quest) then return end

    self:createQuestTree()
    self.accordionExt:setData(self.QuestTree)
    if not self.Quest then
        self.lastMenuIdx = 1
        self.lastItemIdx = nil
    end
    local data, menuIdx, itemIdx = self.accordionExt:selectItem(self.lastMenuIdx, self.lastItemIdx)
    if data ~= self.Quest then
        self:onSelectItem(data, menuIdx, itemIdx)
    end
    self.accordionExt:refreshMenuAndItem()
end
function QuestUI.onRemoveQuest(evtName, param,self)
   
    if not self.QuestMap[param.Quest] then return end

    self:removeQuest(param.Quest)
end
function QuestUI.onChangeState(evtName, param,self)
    if not self.QuestMap[param.Quest] then return end

    if not self:isCurrState(param.Quest) then
        self:removeQuest(param.Quest)
    else
        if self.Quest == param.Quest then
            self:showQuest()
        end
        
        self:createQuestTree()
        self.accordionExt:setData(self.QuestTree)
        self.accordionExt:selectItem(self.lastMenuIdx, self.lastItemIdx)
        self.accordionExt:refreshMenuAndItem()
    end
end
function QuestUI.onChangeProgress(evtName, param,self)
    if self.Quest ~= param.Quest then return end
    self:showQuest()
end

function QuestUI.removeQuest(self,Quest)
    self:createQuestTree()
    self.accordionExt:setData(self.QuestTree)
    if not self.accordionExt:canSelectItem(self.lastMenuIdx, self.lastItemIdx) then
        self.lastMenuIdx = 1
        self.lastItemIdx = nil
    end

    local data, menuIdx, itemIdx = self.accordionExt:selectItem(self.lastMenuIdx, self.lastItemIdx)
    if self.Quest == Quest then
        self:onSelectItem(data, menuIdx, itemIdx)
    end
    self.accordionExt:refreshMenuAndItem()
end



function QuestUI.OnEnter(self)
    -- print("QuestUI:OnEnter", self.ui.menu.IsRunning)

    local _Quests = nil
    if self.pageIdx == nil or  self.pageIdx == 1 then
        _Quests = DataMgr.Instance.QuestData.Accepts
    else
        _Quests = DataMgr.Instance.QuestData.NotAccepts
    end
   --  print("questlastMenuIdx",self.lastMenuIdx)
   --  print("questCount",_Quests.Count)
   -- for v in Slua.iter(_Quests) do
   --  print("quest",v.id)
   -- end
    local changepage = false
    
    if  _Quests == nil or _Quests.Count == 0 then

        if self.lastMenuIdx == nil or self.lastMenuIdx == 1 then
            if DataMgr.Instance.QuestData.NotAccepts.Count > 0 then
                self.radioExt:_onBtnClick(self.ui.comps.tbt_an2)
                changepage = true
            end
        else
            if DataMgr.Instance.QuestData.Accepts.Count > 0 then
                self.radioExt:_onBtnClick(self.ui.comps.tbt_an1)
                changepage = true
            end
        end
    end
  
    if not changepage then
        self:doSwitchQuestPage()
    end
    QuestUI.self = self
    self.onQuestEvents = {
        ["Event.Quest.AddQuest"] = QuestUI.onAddQuest,
        ["Event.Quest.Accept"] = QuestUI.onChangeState,
        ["Event.Quest.RemoveQuest"] = QuestUI.onRemoveQuest,
        ["Event.Quest.ProgressesChange"] = QuestUI.onChangeProgress,
        ["Event.Quest.Complete"] = QuestUI.onChangeState,
    }
    -- EventManager.Subscribe("Event.Quest.AddQuest", self._self_onAddQuest)
    -- EventManager.Subscribe("Event.Quest.Accept", self._self_onChangeState)
    -- EventManager.Subscribe("Event.Quest.RemoveQuest", self._self_onRemoveQuest)
    -- EventManager.Subscribe("Event.Quest.ProgressesChange", self._self_onChangeProgress)
    -- EventManager.Subscribe("Event.Quest.Complete", self._self_onChangeState)
    DataMgr.Instance.QuestData:AttachLuaObserver('QuestUI',QuestUI)
end

function QuestUI.OnInit(self)
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveUp)
    -- redioBtns, defaultBtn, datas, cb, invertChecked
    self.radioExt = RadioGroupExt.new(
        {self.ui.comps.tbt_an1, self.ui.comps.tbt_an2},
        self.ui.comps.tbt_an1, {'accepts', 'notAccepts'},
        function(selectedIdx)
            self:doSwitchQuestPage(selectedIdx)
        end)

    self.ui.comps.cvs_kuang.Visible = false
    self.accordionExt = AccordionExt.new(
        self.ui.comps.sp_oar,
        self.ui.comps.cvs_kuang, "tbt_class", "tbt_sub",
        function(selectData, menuIdx, itemIdx)
            self:onSelectItem(selectData, menuIdx, itemIdx)
        end
        , nil,
        false, true, false, false,8)

    self.accordionExt.onInitItem = function(accExt, node, btn, data)
        self:onInitItem(accExt, node, btn, data)
    end
    self.accordionExt.onRefreshItem = function(accExt, node, btn, data)
        self:onRefreshItem(accExt, node, btn, data)
    end

    -- self.Close = self.Close
    self.ui.comps.btn_discard.TouchClick = function()
        self:onDiscardBtnClick()
    end
   
    --self.ui.comps.tbt_trace.TouchClick = self._self_onTraceBtnClick
    self.ui.comps.btn_begin.TouchClick = function()
        self:onBeginBtnClick()
    end


    self.ui.comps.btn_process.TouchClick = function()
        self:onProcessBtnClick()
    end
    

    self.awardIcons = {
        self.ui.comps.cvs_item1,
        self.ui.comps.cvs_item2,
        self.ui.comps.cvs_item3,
        self.ui.comps.cvs_item4,
        self.ui.comps.cvs_item5,
    }

    self.QuestTree = nil
    self.lastMenuIdx = nil
    self.lastItemIdx = nil
    
end
function QuestUI.OnExit( self )
   
    DataMgr.Instance.QuestData:DetachLuaObserver('QuestUI')
    -- EventManager.Unsubscribe("Event.Quest.AddQuest", self._self_onAddQuest)
    -- EventManager.Unsubscribe("Event.Quest.Accept", self._self_onChangeState)
    -- EventManager.Unsubscribe("Event.Quest.RemoveQuest", self._self_onRemoveQuest)
    -- EventManager.Unsubscribe("Event.Quest.ProgressesChange", self._self_onChangeProgress)
    -- EventManager.Unsubscribe("Event.Quest.Complete", self._self_onChangeState)
end
function QuestUI.OnDestory( self )
    self.accordionExt:destroy()
    -- print("QuestUI:OnDestory")
    -- body
end

return QuestUI
