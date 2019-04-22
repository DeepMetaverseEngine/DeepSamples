local _M = {}
_M.__index = _M

local AcceptMsg = {}
local SubmitMsg = {}
local AutoQuestId = 0
local localParams = nil
local NpcUnitTemplateId = 0
local QuestNpcDataModel = require'Model/QuestNpcDataModel'
local QuestMap = {}
local Quests = {}
local FirstTalk = false
local PlayCG = false
local LastQuestId = {}
local LastQuestData = {}
local ParseTalk = false
local maxN = 25 -- 最大显示任务数
local sortindex = {1,1000,5,2,3,7,8,1001,2000,2100,1003,1011} -- 任务排序
local InitScene = false
local NpcTalk = false
local DialogueTalk = false
local HasAcceptQuestMap = {}--是否重置任务
local autoRunTable ={  -- 允许重置的任务
  [5] = 'guildwanted', --仙盟悬赏
}
local function AddQuestToQuestList(Quest)
    if QuestMap[Quest.id] ~= nil then
        QuestMap[Quest.id] = Quest
        return false
    end
    if not Quest.tracing then return false end
    if Quest.Static == nil then return false end
    if Quest.state < QuestState.NotAccept then return false end
    if Quest.state == QuestState.NotAccept and Quest.Static.push == 0 then return false end
    if Quest.state > QuestState.CompletedNotSubmited then return false end
    QuestMap = QuestMap or {}
    Quests = Quests or {}
    QuestMap[Quest.id] = Quest
    if #Quests < maxN then
        table.insert(Quests, Quest)
        return true
    end
    return false
end

local function IsShowQuest(QuestId)

  --print("IsShowQuest1",QuestId)
  for _,v in ipairs(Quests) do
    --print("IsShowQuest2",v.id)
    if v.id == QuestId then
      return true
    end
  end
  return false
end
function _M.SortQuest()
    if Quests and #Quests > 1 then
        table.sort(Quests, function(a, b)
                local adata = a.Static
                local bdata = b.Static
            if a.Static == nil or b.Static == nil then
                return false
            end
                if adata.sub_type ~= bdata.sub_type then
                    local asort = 999
                    local bsort = 999
                    for i,v in ipairs(sortindex) do
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
    end
end


function _M.tryPickOneQuest()
    if #Quests >= maxN then return end
    local questlist = nil
    for _,v1 in pairs(QuestMap) do
        local isbreak = false
        for _,v2 in ipairs(Quests) do
            if v2.id == v1.id then
                isbreak = true
                break
            end
        end
        if not isbreak then
           table.insert(Quests, Quest)
           questlist = questlist or {}
           table.insert(questlist, Quest)
        end
        if #Quests >= maxN then
            break
        end
    end
    _M.SortQuest()
    return questlist
end


function _M.requestAccept(QuestId, cb)
    local msg = {c2s_id = QuestId}
    --print("requestAcceptQuestID",QuestId)
      --print("requestAccept")
    if PlayCG == true then
        AcceptMsg = msg
        AcceptMsg.cb = cb
        return
    end
    Protocol.RequestHandler.TLClientAcceptQuestRequest(msg, function(ret)
        --print("AcceptQuestIDResponse",QuestId)
        if cb then cb(true) end
    end,
    function()
      if cb then cb(false) end
    end)
end

function _M.RequestTodayCarriageCount(cb)
  Protocol.RequestHandler.ClientTodayCarriageCountRequest({},function(rp) 
    cb(rp.s2c_count)
  end)
end

function _M.RequestClientAcceptCarriage(questid,cb)
  Protocol.RequestHandler.ClientAcceptCarriageRequest({c2s_id = questid},function(rp) 
    if cb then cb(true) end
  end,function()
    if cb then cb(false) end
  end)
end

function _M.requestGiveUp(QuestId, cb, errorcb)
    local msg = {c2s_id = QuestId}
    Protocol.RequestHandler.TLClientGiveUpQuestRequest(msg, function(ret)
        --print_r(ret)
        if cb then cb() end
    end,function() 
      if errorcb then errorcb() end
    end)
end

function _M.requestSubmiit(QuestId, selectIndex ,cb)
    local msg = {c2s_id = QuestId,c2s_inputValue = selectIndex or 0}
   
    if PlayCG == true then
        SubmitMsg = msg
        SubmitMsg.cb = cb
        return
    end
    --print("requestSubmiit",selectIndex,QuestId)
    Protocol.RequestHandler.TLClientSubmitQuestRequest(msg, function(ret)
        --print_r(ret)
        print("SubmitQuestResponse",QuestId)
        --EventManager.Fire("Event.Quest.Submited", {QuestId = tonumber(QuestId)})
        if cb then cb(true) end
    end,function ()
      -- body
         if cb then cb(false) end
    end)
end

function _M.requestTalkNpc(npc_tempid, cb)
    local msg = {c2s_npcId = npc_tempid}
    --print("requestTalkNpc",npc_tempid)
    Protocol.RequestHandler.TLClientTalkWithNpcRequest(msg, function(ret)
       -- print_r(ret)
        --print("requestTalkNpcreponse",npc_tempid)
         if cb then cb() end
    end)
end

function _M.requestBeginLoopQuest(LoopQuestType, cb)
    local msg = {c2s_groupid = LoopQuestType}
    Protocol.RequestHandler.TLClientBeginLoopQuestRequest(msg, function(ret)
       -- print_r(ret)
         if cb then cb() end
    end)
end

function _M.requestSumbitItem(itemdata,questid, cb)
    local msg = {c2s_data = itemdata,c2s_questId = questid}
    Protocol.RequestHandler.TLClientSubmitItemRequest(msg, function(ret)
         --print("requestSumbitItem")
        -- print_r(ret)
         if ret.s2c_code == 200 then
            --EventManager.Fire('Event.Quest.Submited', {})
         else
             print("SumbitItem is error ,code with "..ret.s2c_code)
         end
         if cb then cb() end
    end)
end

function _M.ParseNextStep(Questid,SelectIndex,cb)

  local Quest = DataMgr.Instance.QuestData:GetQuest(Questid)
  if Quest == nil then
    return
  end
  local state = Quest.state
  if LastQuestId[Questid] and LastQuestId[Questid] == state then
       ParseTalk = false
      if cb then 
          cb(true)
      end
    return
  end
  LastQuestId[Questid] = state

  if state == QuestState.NotAccept then
      _M.requestAccept(Questid,function(isSuccess)
         if isSuccess then
            _M.AcceptQuest(Questid)
         end
         --print("ParseNextStep error2",Questid,Queststate)
         if cb then
           --print("ParseNextStep error3",Questid,Queststate)
              cb(isSuccess)
          end
      end)
  elseif state == QuestState.NotCompleted then
    --print("ParseNextStep error4",Questid,Queststate)
       
        if Quest ~= nil then
          local static = Quest.Static
           for i,v in ipairs(static.condition.type) do
             local arg1 = static.condition.arg1[i]
               if v == 'eFindNPC' and not string.IsNullOrEmpty(arg1) then
                   _M.QuestIsTalkFinish(static.condition.arg1[i],function()
                       if cb then
                           --print("ParseNextStep error5",Questid,Queststate)
                           cb(true)
                       end
                   end)
               else
                   if cb then
                       --print("ParseNextStep error5",Questid,Queststate)
                       cb(true)
                   end
               end
           end
            
        else
           if cb then
          --print("ParseNextStep error5",Questid,Queststate)
            cb(true)
          end
        end
          
  elseif state == QuestState.CompletedNotSubmited then
      _M.requestSubmiit(Questid,SelectIndex,function(isSuccess)
          if isSuccess then
            _M.CompleteQuest(Questid)
          end
          --print("ParseNextStep error6",Questid,Queststate)
          if cb then
            --print("ParseNextStep error7",Questid,Queststate)
              cb(isSuccess)
          end
      end)
  else
    --print("ParseNextStep error8",Questid,Queststate)
  end
end

function _M.requestCustomSumbitItem(itemdata,questid, cb)
    local msg = {c2s_data = itemdata,c2s_questId = questid}
    Protocol.RequestHandler.TLClientSubmitCustomItemRequest(msg, function(ret)
         --print("requestSumbitItem")
        -- print_r(ret)
         if ret.s2c_code == 200 then
            --EventManager.Fire('Event.Quest.Submited', {})
         end
         if cb then cb() end
    end)
end

local function ParseQuestIsAutoDone(Questid)

  -- [QuestState.NotAccept] = "desc_accept",
  -- [QuestState.NotCompleted] = "desc_nofinish",
  -- [QuestState.CompletedNotSubmited] = "desc_subquest",
    -- //接取、完成全手动 寻路自动 ：0
   --  //接取、完成全自动：1
   --  //接取、完成全手动，寻路接取自动 完成后手动寻路：2
   --  //接取不自动、其余自动：3
   -- public const int NoAuto = 0;
   -- public const int AllAuto = 1;
   -- public const int AutoAccept = 2;
   -- public const int NoAutoAccetpButAutoDone = 3;
   local isAuto = false
   local Quest
   local t = type(Questid)
   --print("ParseQuestIsAutoDone",t)
    if t == 'userdata' then
      Quest = Questid
    elseif t == 'number' then
      Quest = DataMgr.Instance.QuestData:GetQuest(Questid)
    else
      --print("ParseQuestIsAutoDone",Questid)
      return false
    end
    if Quest ~= nil  then
      if Quest.Static.automatic == QuestAutoMaticType.NoAuto then
         if Quest.state == QuestState.NotCompleted  then
            isAuto = true
         end
      elseif Quest.Static.automatic == QuestAutoMaticType.AllAuto then
          isAuto = true
      elseif Quest.Static.automatic == QuestAutoMaticType.AutoAccept then
        if Quest.state == QuestState.NotAccept then
            isAuto = true
        end
      elseif Quest.Static.automatic == QuestAutoMaticType.NoAutoAccetpButAutoDone then
        if Quest.state ~= QuestState.NotAccept then
            isAuto = true
        end
      end
       if Quest.state == QuestState.NotCompleted then
            if Quest.Static.auto == 1 then
               isAuto = true
            else 
               isAuto = false
            end
       end
     end
  --print("isAuto",isAuto)
  return isAuto
end


--是否交谈就完成
local function QuestIsTalkFinish(NpcTemplateId,cb)
  local isTalkNpc = DataMgr.Instance.QuestMangerData:HasQuestByTalk(tonumber(NpcTemplateId))
  if isTalkNpc then 
    _M.requestTalkNpc(NpcTemplateId,cb)
  else
    if cb then
      cb()
    end
  end
end

local function ParseQuestTalkContext(QuestID,NpcTemplateId)
  --print("ParseQuestTalkContextQuestID",QuestID)
  local NpcStatic = GlobalHooks.DB.Find('npc', NpcTemplateId)
  local params = {
    id = QuestID,
    model_id = NpcStatic and NpcStatic.npc_model or nil,
    speaker_name = NpcStatic and NpcStatic.npc_name or nil,
    NpcTemplateId  = NpcTemplateId}
  return QuestNpcDataModel.ParseQuestTalkContext(params)

end

local function ParseInitQuest(NpcTemplateId)

  local NpcQuestDataList = DataMgr.Instance.QuestMangerData:GetNpcQuestData(NpcTemplateId)

  local i = 1
  local curbtn = nil
  local BtnsList = {}
  local functionbtn = {}
  local NpcStatic = GlobalHooks.DB.Find('npc', NpcTemplateId)
  if NpcStatic == nil then
    UnityEngine.Debug.LogError('NpcStatic is nil with id '..NpcTemplateId)
    return
  end
    for i,v in ipairs(NpcStatic.function_tag) do
      if not string.IsNullOrEmpty(v) then
        local btn = {}
        btn.FunctionID = v
        btn.FunctionIndex = i
        btn.FunctionName = FunctionUtil.GetFunctionName(v)
        btn.QuestAutomatic = 0
        table.insert(functionbtn,btn)
      else 
        break
      end
      
    end 
    if #functionbtn > 0 then
      table.sort(functionbtn,function(a,b)
        return a.FunctionIndex > b.FunctionIndex
      end)
      for i,v in ipairs(functionbtn) do
            table.insert(BtnsList,v)
      end
    end
  if NpcQuestDataList ~=nil and NpcQuestDataList.Count>0 then
    for Quest in Slua.iter(NpcQuestDataList) do
      local btn = {}
      local static = Quest.Static
      if Quest.mainType ~= QuestType.TypeTip and IsShowQuest(Quest.id) then
        btn.Quest = Quest
        btn.QuestID =tonumber(Quest.id)
        btn.QuestState = tonumber(Quest.state)
        btn.QuestType = tonumber(Quest.mainType)
        btn.QuestAutomatic = tonumber(Quest.Static.automatic)
        --print("AutoQuestId",AutoQuestId)
        --print_r("AutoQuestIdbtn",btn)
        if AutoQuestId == Quest.id then
          if Quest.state == QuestState.NotAccept or Quest.state == QuestState.CompletedNotSubmited then
            curbtn = btn
            --print("curbtn",curbtn.QuestID)
          end
        end
        if not (Quest.state == QuestState.CompletedNotSubmited and FunctionUtil.CheckNeedOpen(Quest.Static.submit_doFuncitonid)) then
          BtnsList = BtnsList or {}
          table.insert(BtnsList,btn)
        end
      end
    end
  end
  if curbtn == nil and BtnsList ~= nil and #BtnsList >0 then 
    for i,v in ipairs(BtnsList) do
      local _btn = v
      local autoDone = ParseQuestIsAutoDone(_btn.QuestID)
      if autoDone and _btn.Quest.state ~= QuestState.NotCompleted then
        curbtn = _btn
        break
      end
    end
  end
  return curbtn,BtnsList
end

local function ParseQuestCanSumbit(btn)
  local state = btn.QuestState
  if state == QuestState.NotAccept or state == QuestState.CompletedNotSubmited then
    return true
  elseif _state == QuestState.NotCompleted then
    return IsSubmitQuest(btn.QuestID)
  end
  if not string.IsNullOrEmpty(btn.FunctionID) then
    return  true
  end
  return false
end


--接受任务
--接受任务
local function AcceptQuest(Questid)
end
--完成任务
local function CompleteQuest(Questid)
  if AutoQuestId == Questid then
    AutoQuestId = 0
  end
 
end

function _M.ExitTalk(failid)
  --UnityEngine.Debug.LogError("IsTalk = false")

  if failid ~= nil then
    EventManager.Fire("Event.Quest.CheckAndStopQuest",{id = failid})
  end
  if NpcUnitTemplateId ~= 0 then
      --EventManager.Fire("CloseNpcCamera",{unit = NpcUnitTemplateId})
      NpcUnitTemplateId = 0
  end
  --print("ExitTalk",LastQuestId)
  AutoQuestId = 0
  ParseTalk = false
end
local function ParseInitTalk(params)
  --print_r("ParseInitTalk",params)
  
  if params.QuestId ~= 0 then
     FirstTalk = false
  else
     FirstTalk = true
  end

    if params.QuestId ~= 0 then
        local  Quest = DataMgr.Instance.QuestData:GetQuest(params.QuestId)
        if Quest then
            local Openid = nil
            if Quest.state == QuestState.NotCompleted and FunctionUtil.CheckNeedOpen(Quest.Static.submit_dosth) then
                Openid = Quest.Static.submit_dosth
            elseif Quest.state == QuestState.CompletedNotSubmited and FunctionUtil.CheckNeedOpen(Quest.Static.submit_doFuncitonid) then
                Openid = Quest.Static.submit_doFuncitonid
            end
            if Openid then
                FunctionUtil.OpenFunction(Openid,true)
                _M.ExitTalk()
                return
            end
        end
    end
   local _CurrentBtn,BtnsList = ParseInitQuest(params.TemplateId)
    -- print_r('_CurrentBtn',_CurrentBtn,BtnsList)
    local _TalkContext = _CurrentBtn and ParseQuestTalkContext(_CurrentBtn.QuestID,params.TemplateId) or nil
    if _TalkContext ~= nil and _CurrentBtn then
          local _params = 
          {
            TalkContext = _TalkContext,
            cb = function (selectIndex)
              _M.ParseNextStep(_CurrentBtn.QuestID,selectIndex,
                function(isSuccess)
                     _M.ExitTalk()
                end)
            end
          }
          --print_r("_params",_params)
          QuestNpcDataModel.OpenQuestTalk(_params)
    else
        if _CurrentBtn ~= nil then
           
            _M.ParseNextStep(_CurrentBtn.QuestID,0,
                    function(isSuccess)
                        _M.ExitTalk()
                    end)
        else
            
            if not FirstTalk then
                _M.ParseNextStep(params.QuestId,0,
                        function(isSuccess)
                            _M.ExitTalk()
                        end)
            else
                    local ui = GlobalHooks.UI.FindUI('UINpcTalk')
                    if ui ~= nil then
                        --print("UINpcTalk has exist----------")
                        return
                    end
                    params.CallBack = function()
                        _M.ExitTalk()
                    end
                    --MenuMgr.Instance:CloseAllMenu()
                    GlobalHooks.UI.OpenUI('UINpcTalk',1,params)
            end
        end
    end
 
end
--打开NPC对话界面
local function OnShowNpcTalk(eventname,params)

    
    --EventManager.Fire("Event.Npc.NpcTalk",{isTalk = true})
    FirstTalk = true
    ParseTalk = true
    AutoQuestId = params.QuestId or 0
    NpcUnitTemplateId = params.TemplateId
    ParseInitTalk(params)

end



local function OnCloseTalk(eventname,params)
  --print("OnCloseTalk")
    
    AutoQuestId = 0
end

function _M.onAddQuest(QuestId, Quest)
    --print("onAddQuest---------------------------")
    local static = QuestNpcDataModel.GetQuestData(Quest)-- GlobalHooks.DB.Find('Quest', Quest.id)
    if static ~= nil and (Quest.state == QuestState.NotCompleted) 
      and not string.IsNullOrEmpty(static.lua_accept) then
        GlobalHooks.Drama.Start("quest/"..static.lua_accept)
    end
 
   
end

function _M.onRemoveQuest(QuestId, Quest)
    --print("onRemoveQuest")
    if QuestMap[QuestId] then
        QuestMap[QuestId] = nil

        LastQuestData[QuestId] = nil
    end
    if LastQuestId and LastQuestId[QuestId] then
       LastQuestId[QuestId] = nil
    end
    table.removeItem(Quests,Quest)
end


local function CanAutoRun(questmaintype)
  if autoRunTable[questmaintype]~= nil then
    return true
  end
  return false
end
function _M.DoAutoQuestByQuest(Quest,forcequest)
    forcequest = forcequest or false
    if Quest ~= nil and IsShowQuest(Quest.id) then
       if  _M.ParseQuestIsAutoDone(Quest) then
          if not forcequest and _M.IsBreakDoQuest()  then
             -- print("DoAutoQuestByQuest1",Quest.id)
            LastQuestData[Quest.id] = Quest.id
              --LastQuestData = Quest
          else
            LastQuestData[Quest.id] = nil
            --print("DoAutoQuestByQuest2",Quest.id,forcequest)
              if Quest.state == QuestState.NotAccept and not CanAutoRun(Quest.mainType) then
                  if HasAcceptQuestMap[Quest.id] == 1 then
                      return
                  else
                      HasAcceptQuestMap[Quest.id] = 1
                  end
              end
            EventManager.Fire('Event.Quest.BeginQuest', {id = Quest.id})
          end
      else
        EventManager.Fire("Event.Quest.CheckAndStopQuest",{id = Quest.id})
      end
   end
end
function _M.reloadData()
        
    if not GameGlobal.Instance.netMode then
        return 
    end
    
    --print("reloadData")
    Quests = {}
    QuestMap = {}
    -- for Quest in Slua.iter(DataMgr.Instance.QuestData.AllQuests.Values or {}) do
    --   if Quest.state ~= QuestState.Remove 
    --       and Quest.state ~= QuestState.Submited then
    --         QuestMap[Quest.id] = Quest
    --     end
    -- end
    for Quest in Slua.iter(DataMgr.Instance.QuestData.NotAccepts) do
        --print("NotAccepts",Quest.id)
        if Quest.Static and Quest.Static.push == 1 then
           AddQuestToQuestList(Quest)
        end
    end
    for Quest in Slua.iter(DataMgr.Instance.QuestData.Accepts)  do
         --print("Accepts",Quest.id)
        if Quest.tracing and Quest.state ~= QuestState.Remove and Quest.state ~= QuestState.Submited then
            AddQuestToQuestList(Quest)
        end
    end
    _M.SortQuest()
end

function _M.tryAddQuest(QuestId, Quest)

    if LastQuestId and LastQuestId[QuestId] then
        LastQuestId[QuestId] = nil
    end
    if QuestMap[QuestId] and DataMgr.Instance.QuestData.IsInit then
        _M.DoAutoQuestByQuest(Quest)
        return false
    end
    if AddQuestToQuestList(Quest) then
       _M.SortQuest()
       return true
    end
    return false
end

function _M.AutoRunQuest(Quest)
    if TLBattleScene.Instance
            and TLBattleScene.Instance.Actor
            and DataMgr.Instance.QuestData.IsInit then
        _M.DoAutoQuestByQuest(Quest)
    end
end
function _M.onCompleteQuest(QuestId, Quest)
    local static = QuestNpcDataModel.GetQuestData(Quest)--GlobalHooks.DB.Find('Quest', Quest.id)
    if static ~= nil  and not string.empty(static.lua_subquest)then
        GlobalHooks.Drama.Start("quest/"..static.lua_subquest)
    end
    HasAcceptQuestMap[QuestId] = nil
    if Quest ~= nil then
        _M.DoAutoQuestByQuest(Quest)
    end
end

 function _M.OnSubmited(QuestId, Quest)
    --print("OnSubmited",QuestId)
      local Quest = DataMgr.Instance.QuestData:GetQuest(QuestId)
      if Quest == nil then
         return
      end
     
     --print_r("Quest.Static",Quest.Static)
      if not string.IsNullOrEmpty(Quest.Static.aftersubmit_doFuncitonid) then
        MenuMgr.Instance:CloseAllMenu()
        FunctionUtil.OpenFunction(Quest.Static.aftersubmit_doFuncitonid,true)
      end

 end

function _M.onChangeQuest(QuestId, Quest)
    if QuestMap[QuestId] then
        QuestMap[QuestId] = Quest
        if Quest.TempAction ~= nil then
            Quest.TempAction = nil
        end
        _M.DoAutoQuestByQuest(Quest)
    end
end

function _M.Notify(evtName, QuestId, Quest)
    local func = onQuestEvents[evtName]
    if func then
      --print("Notify",evtName,QuestId)
        func(QuestId, Quest)
    end
end

function _M.ContinueQuest()
    if LastQuestData ~= nil then
        for i,v in pairs(LastQuestData) do
          local quest = QuestMap[v]
          if quest ~= nil then
             _M.DoAutoQuestByQuest(quest) 
          end
        end
    end
end
local function PLAYCG_START(eventname,params)
    PlayCG = params.PlayCG
     --print("PLAYCG_START",PlayCG)
     if not PlayCG then
        if AcceptMsg ~= nil then
            _M.requestAccept(AcceptMsg.c2s_id, AcceptMsg.cb)
            AcceptMsg = nil
        end
        if SubmitMsg ~= nil then
            _M.requestSubmiit(SubmitMsg.c2s_id,SubmitMsg.c2s_inputValue,SubmitMsg.cb)
            SubmitMsg = nil
        end
        if localParams ~= nil then
           OnShowNpcTalk(nil,localParams)
           localParams = nil
           return 
        end
    else
       -- if BeginQuestid ~= 0 then
       --    local quest = QuestMap[BeginQuestid]
       --    if quest ~= nil then
       --       if quest ~= nil then
       --         _M.DoAutoQuestByQuest(quest)
       --        BeginQuestid = 0
       --      end
       --    end
       -- end
         --_M.ContinueQuest()
    end
end

function _M.IsBreakDoQuest()
    --print("IsBreakDoQuest",PlayCG,ParseTalk,InitScene,NpcTalk, DialogueTalk ,TLBattleScene.Instance.IsPickUp)
    --return ParseTalk or MenuMgr.Instance:GetTopMenu() ~= nil 
    return ParseTalk or PlayCG or not InitScene or
            NpcTalk or
            DialogueTalk or
            GlobalHooks.UI.FindUI('FuncOpen') or
            TLBattleScene.Instance.IsPickUp

end
local function OnNpcTalk (evtName, param)
    --print_r("OnNpcTalk",param)
    --print_r("NpcTalk1",param)
    NpcTalk = param.isTalk
    --.ContinueQuest()
end

local function OnDialogueTalk (evtName, param)
    --print_r("OnDialogueTalk",param)
    DialogueTalk = param.isTalk
    --_M.ContinueQuest()
end
function _M.update()
      _M.ContinueQuest()
end

local function stopFouceQuest(eventname,param)
    --print("stopFouceQuest",param.id)
    if LastQuestData[param.id] ~= nil then
      LastQuestData[param.id] = nil
      -- BeginQuestid = 0
    end
end

--初始化状态
local function initTalk()
    
    FirstTalk = false
    PlayCG = false
    -- BeginQuestid = 0
    LastQuestId = {}
    LastQuestData = {}
    DataMgr.Instance.QuestData.IsInit = false
    AcceptMsg = nil
    SubmitMsg = nil
    AutoQuestId = 0
end
local function DeadEvent(eventname,param)
    --print("DeadEvent")
    initTalk()
end
--退出场景时调用，参数：是否短线重连触发的切场景
local function OnExitScene(reconnect)
    --print (" QuestModel OnExitScene ", reconnect)
    if reconnect  then
      LastQuestId = {}
      LastQuestData = {}
      -- BeginQuestid = 0
      
      AutoQuestId = 0
      DataMgr.Instance.QuestData.IsInit = false
    end
    InitScene = false
    FirstTalk = true
end

--进入场景时调用
local function OnEnterScene()
    -- print (" QuestHud OnEnterScene ")
    
    LastQuestData = {}
    LastQuestId = {}
    PlayCG = false
    AcceptMsg = nil
    SubmitMsg = nil

end
local function ChangeScene(evtName, param)
    InitScene = true
end
local function initial()
   -- print("_M.QuestModelinitial")
    
    initTalk()
    QuestMap = {}

    EventManager.Subscribe(Events.PLAYCG_START,PLAYCG_START)
    EventManager.Subscribe(Events.UI_NPCTALK,OnShowNpcTalk)
    EventManager.Subscribe("NpcTalkClose", OnCloseTalk)
    EventManager.Subscribe("Event.Npc.DialogueTalk", OnDialogueTalk)
    EventManager.Subscribe("Event.Npc.NpcTalk", OnNpcTalk)
    EventManager.Subscribe("Event.Quest.CheckAndStopQuest",stopFouceQuest)
    EventManager.Subscribe("Event.Quest.StopQuestAutoRun", stopFouceQuest)
    EventManager.Subscribe("Event.Actor.DeadEvent", DeadEvent)
    EventManager.Subscribe("Event.Scene.ChangeFinish", ChangeScene);
    

	if time ~= nil then
      LuaTimer.Delete(time)
    end
    time = LuaTimer.Add(0,100,function()
        _M.update()
        return true
    end)
end


local function fin()
    --print("_M.QuestModelfin")
   initTalk() 
    HasAcceptQuestMap = {}
    if time ~= nil then
      LuaTimer.Delete(time)
    end
   EventManager.Unsubscribe(Events.PLAYCG_START,PLAYCG_START)
   EventManager.Unsubscribe(Events.UI_NPCTALK,OnShowNpcTalk)
   EventManager.Unsubscribe("NpcTalkClose", OnCloseTalk)
   EventManager.Unsubscribe("Event.Npc.DialogueTalk", OnDialogueTalk)
   EventManager.Unsubscribe("Event.Npc.NpcTalk", OnNpcTalk)
   EventManager.Unsubscribe("Event.Quest.CheckAndStopQuest",stopFouceQuest)
   EventManager.Unsubscribe("Event.Quest.StopQuestAutoRun", stopFouceQuest)
   EventManager.Unsubscribe("Event.Actor.DeadEvent", DeadEvent)

    EventManager.Unsubscribe("Event.Scene.ChangeFinish", ChangeScene);
end

local function GetQuest(id)
  return QuestMap[id]
end

local  function InitNetWork(initNotify)
    if initNotify then
        --Protocol.PushHandler.TLClientQuestAutoMoveNotify(OnClientQuestAutoMoveChangeNotify)
    end
end

local function GetQuests()
    return Quests
end
_M.fin = fin
_M.initial = initial
_M.InitNetWork = InitNetWork
_M.OnEnterScene = OnEnterScene
_M.OnExitScene = OnExitScene
_M.ParseInitQuest = ParseInitQuest
_M.AcceptQuest = AcceptQuest
_M.CompleteQuest = CompleteQuest
_M.QuestIsTalkFinish = QuestIsTalkFinish
_M.ParseQuestTalkContext = ParseQuestTalkContext       
_M.ParseQuestIsAutoDone = ParseQuestIsAutoDone
_M.GetQuests = GetQuests
_M.GetQuest = GetQuest
_M.sortindex = sortindex

return _M