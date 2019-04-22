local Util = require("Logic/Util")
local QuestModel = require("Model/QuestModel")
local QuestNpcDataModel = require'Model/QuestNpcDataModel'
local ItemModel = require 'Model/ItemModel'
local stateStrMap = {
	[QuestState.NotAccept] = "_canaccept",
	[QuestState.NotCompleted] = "_doing",
	[QuestState.CompletedNotSubmited] = "_complete",
}
local QuestUtil = {}
--不显示环数的任务子类型
local ShowLoopNumber = {}

-- 蓝色  519fef
-- 绿色  35cd1c
-- 紫色  df9aff
-- 红色  eb2f2f
-- 橙色  e97f21
local QuestColor = 
{
	[1] = 'ffe362',--主线
	[5] = 'ffe362',--引导
	[8] = 'ffe362',--渡劫

    [2] = '75ddff',--支线
  	[1000] = '75ddff',--师门
    [1001] = '75ddff',--仙盟
    [1003] = '75ddff',--委托任务
    [1011] = '75ddff',--仙盟悬赏
    [2000] = '75ddff',--仙盟破坏

    [3] = 'df9aff',--活动 
    [7] = 'df9aff',--寻龙
    [2100] = 'df9aff',--个人押镖
    [2200] = 'df9aff',--个人护镖
}
--当前任务进度
function QuestUtil.CurQuestProg(Quest)
	local static = Quest.Static
	local prog = Quest.progress -- TLProgressData
	local prognum = 1
	--print("progCount=",prog.Count)
	for progress in Slua.iter(prog) do
        if progress.CurValue < progress.TargetValue then
        	--print("progress.CurValue=",progress.CurValue)
        	--print("progress.TargetValue=",progress.TargetValue)
   			return prognum
        end
        prognum = prognum +1
     end
     if prognum > prog.Count then
     	prognum = prog.Count
     end
	return prognum
end
function QuestUtil.QuestName(Quest,InPanel)
	local static = QuestNpcDataModel.GetQuestData(Quest)--GlobalHooks.DB.Find('Quest', Quest.id)
	local text = nil
	if not static then
		text = "quest_Story:" .. Quest.id
	else
		text = Util.GetText(static.quest_name)
		-- local content = Util.GetText(static.quest_name)
		-- if Quest.LoopQuestType == 0 then
		-- 	text = content
		-- else
		-- 	text = string.format("<color=#6dff55>%s</color>",  content)
		-- end
	end

	if InPanel then
		return text
	end
-- 	任务子类型  sub_type
-- 主线任务  1 任务标题文字  ffe362 
-- 支线任务  2 任务标题文字  6dff55
-- 活动任务  3 任务标题文字  df9aff
-- 日常任务  4 任务标题文字  6dff55
-- 引导任务  5 任务标题文字  75ddff

-- local QuestType =
-- {
--     ["TypeNone"] = 0,
--     ["TypeStory"] = 1,
--     ["TypeGuide"] = 2,
--     ["TypeDaily"] = 3
-- }
	local format = '<color=#ffe362>%s</color>'
	-- if Quest.mainType == QuestNpcDataModel.QuestType.TypeStory then
	-- 	format = '<color=#ffe362>%s</color>'
	-- elseif Quest.mainType == QuestNpcDataModel.QuestType.TypeGuide then
	-- 	format = '<color=#6dff55>%s</color>'
	-- elseif Quest.mainType == QuestNpcDataModel.QuestType.TypeDaily then
	-- 	format = '<color=#df9aff>%s</color>'
	-- elseif Quest.mainType == 4 then
	-- 	format = '<color=#6dff55>%s</color>'
	-- elseif Quest.mainType == 5 then
	-- 	format = '<color=#75ddff>%s</color>'
	-- end
	if QuestColor[Quest.subType] ~= nil then
		return string.format('<color=#%s>%s</color>',QuestColor[Quest.subType],text)
	end
	return string.format(format, text)
end
function QuestUtil.QuestDesc(Quest)
	local static = QuestNpcDataModel.GetQuestData(Quest)--GlobalHooks.DB.Find('Quest', Quest.id)
	local text = nil
	if static then
		text = Util.GetText(static.desc_questplane)
	else
		text = "quest_Story:" .. Quest.id
	end
	--local format = '<color="#783F37>%s</color>'
	-- if Quest.mainType ~= QuestType.TypeStory then
	-- 	format = '<color=#fff8cf>%s</color>'
	-- end
	return text
end
function QuestUtil.QuestTarget(Quest)
	if Quest == nil then
		return
	end
	local state = stateStrMap[Quest.state]
	if state == nil then
		--print_r("Quest is nil",Quest.id)
		UnityEngine.Debug.LogError("state is nil Quest.id =  "..Quest.id.." state = "..Quest.state)
		--print("QuestTarget",state)
		return
	end
	local key = "desc" .. state
	local static = QuestNpcDataModel.GetQuestData(Quest)--GlobalHooks.DB.Find('Quest', Quest.id)
	local text = nil
	if not static then
		text = "quest_Story:" .. Quest.id
	else
		text = Util.GetText(static[key])
	end
	return text
end
function QuestUtil.QuestStateStr(Quest)
	--print("'quest' .. stateStrMap[Quest.state]"..'quest' .. stateStrMap[Quest.state])
	return Util.GetText('quest' .. stateStrMap[Quest.state])
end
function QuestUtil.LoopQuestRound(Quest, isXml)
	local cur = Quest.curLoopNum
	local max = Quest.MaxLoopNum
	local color = '6dff55'
	-- if isXml then
	-- 	return string.format("<f color='ff%s'>(%d/%d)</f>", color, cur, max)
	-- else
	return string.format("<color=#%s>(%d/%d)</color>", color, cur, max)
	--end
end

--获得所有寻龙诀地图id
function QuestUtil.GetDragonMapInfo()
	local questmap = {}
	if DataMgr.Instance.QuestData.Accepts == nil then
		return nil
	end
	for Quest in Slua.iter(DataMgr.Instance.QuestData.Accepts)  do
		 if Quest.subType == 7 then
		 	local v = Quest.Static.condition.arg2[1]
		 	if not string.IsNullOrEmpty(v) then
		 		-- table.insert(questmap,v)
		 		questmap[tonumber(v)] = true
		 	end
        end
	end

	return questmap
end

function QuestUtil.QuestCount(Quest, isXml)
	local cur = Quest.ProgressCur
	local max = Quest.ProgressMax
	local color = cur == max and '39b72b' or 'e94242'
	-- if isXml then
	-- 	return string.format("<f color='ff%s'>(%d/%d)</f>", color, cur, max)
	-- else
	return string.format("<color=#%s>(%d/%d)</color>", color, cur, max)
	--end
end

function QuestUtil.doQuestById(QuestId)
	local Quest = DataMgr.Instance.QuestData:GetQuest(QuestId)
	if Quest then
		QuestUtil.doQuest(Quest)
	end
end

function QuestUtil.doQuest(Quest,InUI)
	--print("fire doQuest")
	
	local Quest = DataMgr.Instance.QuestData:GetQuest(Quest.id)
	if Quest == nil then
		print("doQuest is nil-------------------",Quest.id)
		return
	end
	print("doQuest", Quest.id, Quest.state)
	local params = 
	{
		id = Quest.id,
		cb = function (selectIndex)
			EventManager.Fire("Event.Quest.DoQuest", { id = Quest.id })
			local doQuest = false
			--print("Quest",Quest.id,Quest.state)
			if Quest.state == QuestState.CompletedNotSubmited then
				--print("seekAndSubmitQuest",Quest.id)
				doQuest = QuestUtil.seekAndSubmitQuest(Quest,selectIndex)
			end

			if Quest.state == QuestState.NotAccept then
				--print("seekAndAcceptQuest",Quest.id)
				doQuest = QuestUtil.seekAndAcceptQuest(Quest)
			end

			if Quest.state == QuestState.NotCompleted then
				--print("seekAndProgressQuest",Quest.id)
			   doQuest =  QuestUtil.seekAndProgressQuest(Quest)
			end
			
			if doQuest and InUI then
				GlobalHooks.UI.CloseUIByTag('QuestMain')
			elseif not doQuest then
				EventManager.Fire("Event.Quest.CheckAndStopQuest",{id = Quest.id})
			end


		end
	}
	 if not QuestNpcDataModel.OpenQuestTalk(params) then
		--print(string.format("QuestUtil.doQuest: Questid [%s] no TalkContext with state [%s]", Quest.id, Quest.state))
	 	return false
	 end
	return true
end

function QuestUtil.seekAndSubmitQuest(Quest,selectIndex)

	local static = Quest.Static
	--print("seekAndSubmitQuest",Quest.id,selectIndex,static.npc_submit)
	if not string.IsNullOrEmpty(static.submit_doFuncitonid) then
		local isdosth = QuestUtil.tryOpenFunction(static.submit_doFuncitonid,Quest)
		return 	isdosth
	end
	if static.npc_submit == 0 then
		QuestModel.requestSubmiit(Quest.id,selectIndex,function()
		
		end)
		return true
	end
	
	
	return QuestUtil.seekAndNpcTalk(static.npc_submit, Quest)
end

function QuestUtil.seekAndAcceptQuest(Quest)
	local static = Quest.Static
	if static.npc_push == 0 then
		QuestModel.requestAccept(Quest.id,function(issucc)
			-- if issucc then
			-- 	QuestUtil.doAutoDoQuest(Quest)
			-- end
		end)
		return true
	end

	return QuestUtil.seekAndNpcTalk(static.npc_push, Quest)
end

function QuestUtil.seekAndNpcTalk(npcTempId, Quest)
	local NpcTempId = tonumber(npcTempId)
	local static = Quest.Static
	local curQuestProg = QuestUtil.CurQuestProg(Quest)
	local curMapID = static.condition.arg2[curQuestProg]
	if Quest.state == QuestState.CompletedNotSubmited then
		curMapID = static.npc_submit_sceneid
	elseif Quest.state == QuestState.NotAccept then
		curMapID = static.npc_push_sceneid
	end
	local born_point,hints = QuestNpcDataModel.FindRoad(NpcTempId,curMapID)
	 --print("seekAndNpcTalk",curMapID,NpcTempId,born_point)
	local action = MoveAndNpcTalk(NpcTempId)
	action.MapId = curMapID--roadData.born_scene
	action.RoadName = born_point--roadData.born_point
	action.QuestId = Quest.id
	action.hints = hints
	if TLBattleScene.Instance.Actor then
		TLBattleScene.Instance.Actor:AutoRunByAction(action)
		return true
	end
	return false
end



-- condition 格式 xx,xxx;场景,路点
function QuestUtil.parseSeekCondition(condition, action, dontDo, Quest)
	--print("parseSeekCondition"..condition)
	
	--local roadData = GlobalHooks.DB.Find('QuestRoadData', condition)
	-- local arr = string.split(condition, ';,')
	 -- action.MapId = tonumber(arr[3])
	 -- action.RoadName = arr[4]
	 --print_r(roadData)
	local static = Quest.Static
	local prog = QuestUtil.CurQuestProg(Quest)

	local conditionarg1 = static.condition.arg1[prog]
	local curMapID = static.condition.arg2[prog]
	local conditiontype = static.condition.type[prog]
	if Quest.state ~= QuestState.CompletedNotSubmited 
		and conditiontype == QuestCondition.FinishEvent 
		and conditionarg1 == static.npc_push 
		and curMapID == static.npc_push_sceneid then
		action.AimDistance = 3
	end
	--print("parseSeekCondition.AimDistance",action.AimDistance)

	if condition == 0 and curMapID == 0 then
		return
	end
	local born_point,hints = QuestNpcDataModel.FindRoad(condition,curMapID)

	 action.MapId = curMapID--tonumber(roadData.born_scene)
	 action.RoadName = born_point--roadData.born_point
	action.hints = hints
	if Quest.id then
		action.QuestId = Quest.id
	end
	
	if not dontDo and TLBattleScene.Instance.Actor then
		--print("dontDo")
		TLBattleScene.Instance.Actor:AutoRunByAction(action)
	end
	return action
end

function QuestUtil.doConditionNpcTalk(condition, Quest, i)
	QuestUtil.seekAndNpcTalk(tonumber(condition), Quest)
	return true
end
function QuestUtil.doConditionKillMonster(condition, Quest, i)
	--print("doConditionKillMonster"..condition)
	--local arr = string.split(condition, ';,')
	--print("MoveAndBattle: doConditionKillMonster ",condition)
	QuestUtil.parseSeekCondition(condition, MoveAndBattle(condition), false, Quest)
	return true
end

function QuestUtil.doConditionHasQuestItem(condition, Quest, i)
	local static = Quest.Static
	if string.empty(static.drop) then return end

	local one = condition--string.split(static.drop, '|')[i]
	if not one then
		print("QuestUtil.doConditionHasQuestItem: Questid [%d] can not corroct drop [%d]", Quest.id, static.drop)
		return false
	end

	local curMapID = static.condition.arg2[QuestUtil.CurQuestProg(Quest)]
	local born_point,hints = QuestNpcDataModel.FindRoad(condition,curMapID)

	--one = string.sub(one, 2)
	local arr = condition--string.split(one, ";,")
	local action = MoveAndBattle(one)
	--local action = MoveAndBattle(arr[1])
	action.MapId = curMapID--tonumber(arr[2])
	action.RoadName = born_point--arr[3]
	action.QuestId = Quest.id
	action.hints = hints
	if TLBattleScene.Instance.Actor then
		TLBattleScene.Instance.Actor:AutoRunByAction(action)
	end
	return true
end
function QuestUtil.seekAndGoto(condition, Quest, i, isPickItem)
	local action = MoveEndAction()
	if isPickItem then
		action.MoveType = AutoMoveType._PickItem
	end
	QuestUtil.parseSeekCondition(condition, action, false, Quest)
	return true
end
function QuestUtil.doPickItem(condition, Quest, i)
	
	--print("doPickItem"..condition)
	local static = Quest.Static
	local curMapID = static.condition.arg2[QuestUtil.CurQuestProg(Quest)]
	if DataMgr.Instance.UserData.MapTemplateId == curMapID and GameUtil.IsPreparePickItem() then
		return true
	end

	return QuestUtil.seekAndGoto(condition, Quest, i,true)
end
function QuestUtil.doFollowNpc(condition, Quest, i)
	--local arr = string.split(condition, ';,')
	--print("condition"..condition)
	local static = Quest.Static
	local curMapID = static.condition.arg2[QuestUtil.CurQuestProg(Quest)]
	--local born_point = QuestNpcDataModel.FindRoad(condition,curMapID)
	EventManager.Fire("AutoMoveByTarget", {
		SceneID = curMapID,--tonumber(arr[3]),
		TemplateID = condition--tonumber(arr[4]),
	})
	return true
end
function QuestUtil.doConditionSubmitItem(condition, Quest, i)

	local static = Quest.Static
	local templateId = static.condition.arg1[QuestUtil.CurQuestProg(Quest)]
	local count  = static.condition.val[QuestUtil.CurQuestProg(Quest)]
	-- local arr = string.split(condition, ',')
	-- local templateId = tonumber(arr[1])
	-- local count = tonumber(arr[2])
	local now = ItemModel.CountItemByTemplateID(templateId)
	if now >= count then
		QuestUtil.seekAndNpcTalk(Quest.Static.npc_submit, Quest)
		return true
	end

	return QuestUtil.tryFindQuestItem(condition, Quest)
end

function QuestUtil.doConditionSubmitCustomItem(condition, Quest, i)

	local static = Quest.Static
	local customitemgroup = static.condition.arg1[QuestUtil.CurQuestProg(Quest)]
	local count  = static.condition.val[QuestUtil.CurQuestProg(Quest)]
	
	local data = GlobalHooks.DB.Find('Submit_item',customitemgroup)

	if data == nil then 
		UnityEngine.Debug.LogError("doConditionSubmitCustomItem data error with customitemgroup "..customitemgroup)
		return false 
	end

	local function Match(itdata)
		local detail = ItemModel.GetDetailByTemplateID(itdata.TemplateID)
	  	for i,v in ipairs(data.filter.type) do
	  		local min =	tonumber(data.filter.min[i])
	  		local max = tonumber(data.filter.max[i])
	  		if detail.static[v] >= min and detail.static[v] <= max and itdata.Count >= count then
	  			return true
	  		end
		end
		return false
	end
	local now = DataMgr.Instance.UserData:GetItemCountByMatch(Match)

	if now >= 0 then
		QuestUtil.seekAndNpcTalk(Quest.Static.npc_submit, Quest)
		return true
	end

	return QuestUtil.tryFindQuestItem(condition, Quest)
end
function QuestUtil.doConditionUseItem(condition, Quest, i)
	--local arr = string.split(condition, ',')
	local static = Quest.Static
	local templateId = condition
	local count = static.condition.val[1]
	-- local templateId = tonumber(arr[1])
	-- local count = tonumber(arr[2])
	local now = DataMgr.Instance.UserData.Bag:GetItemCount(templateId)
	if now >= count then
		GlobalHooks.UI.OpenUI("ItemUseUI", 0, templateId)
		return true
	end
	return QuestUtil.tryFindQuestItem(condition, Quest)
end
function QuestUtil.doConditionDefault(condition, Quest, i)
	--return false
	return QuestUtil.tryFindQuestItem(condition, Quest)
end

-- local ConditionType = {
-- 	KillMonster = 1,
-- 	NpcTalk = 2,
-- 	RoleLevel = 3,
-- 	SubmitItem = 4,
-- 	KillPlayer = 5,
-- 	HasQuestItem = 9,
-- 	UseItem = 10,
-- 	UserQuestItem = 11,
-- 	GotoPoint = 12,
-- 	PickItem = 13,
-- 	FollowNpc = 14,
-- }

  -- public const string KillMonster = "eKillMonster";              //击杀怪物
  --       public const string FindNPC = "eFindNPC";                      //寻找NPC
  --       public const string PlayerAttribute = "p";                    //玩家属性
  --       public const string SubmitItem = "eSubmitItem";                //提交物品
  --       public const string KillPlayer = "eKillPlayer";                //击杀玩家
  --       public const string FinishEvent = "eFinishEvent";              //完成事件
  --       public const string TakePartDungeon = "eTakePartDungeon";      //参与副本
  --       public const string FinishDungeon = "eFinishDungeon";          //完成副本
  --       public const string GetVirtualItem = "eGetVirtualItem";        //获得虚拟物品
  --       public const string UseItem = "eUseItem";                      //使用物品
  --       public const string UseVirtualItem = "eUseVirtualItem";        //使用虚拟道具
  --       public const string PickItem = "ePickItem";                    //拾取物品
  --       public const string ProtectedNPC = "eProtectedNPC";            //保护NPC
  --       public const string TrusteeshipEvent = "eReachPlace";                //到达某地.
local conditionFuncMap = {
	[QuestCondition.FindNPC]     = QuestUtil.doConditionNpcTalk,
	[QuestCondition.KillMonster] = QuestUtil.doConditionKillMonster,
	[QuestCondition.GetVirtualItem] = QuestUtil.doConditionHasQuestItem,
	[QuestCondition.UseItem]     = QuestUtil.doConditionUseItem,
	[QuestCondition.SubmitItem]  = QuestUtil.doConditionSubmitItem,
	[QuestCondition.SubmitCustomItem]  = QuestUtil.doConditionSubmitCustomItem,
	[QuestCondition.TrusteeshipEvent]   = QuestUtil.seekAndGoto,
	[QuestCondition.FinishEvent]   = QuestUtil.seekAndGoto,
	[QuestCondition.PickItem]    = QuestUtil.doPickItem,
	[QuestCondition.ProtectedNPC]   = QuestUtil.doFollowNpc,
	default                     = QuestUtil.doConditionDefault,
}

local function getConditionItemId(condition)
	return tonumber(string.split(condition, ',')[1])
end

function QuestUtil.tryFindQuestItem(condition, Quest)
	--print("tryFindQuestItem",Quest.Static.noFinish_DoSth)
	if string.IsNullOrEmpty(Quest.Static.noFinish_DoSth) then return false end
	local isdosth = QuestUtil.tryOpenFunction(Quest.Static.noFinish_DoSth,Quest)

	return 	isdosth
end
function QuestUtil.tryOpenFunction(functiontag,Quest,params)
	if string.IsNullOrEmpty(functiontag) then return false end

	--print("tryOpenFunction",functiontag)
	params = params or {}
	params.Quest = Quest
    return FunctionUtil.OpenFunction(functiontag,false,params)

end
function QuestUtil.seekAndProgressQuest(Quest)
	local static = Quest.Static
	--print("seekAndProgressQuest---------")
	--print_r(static.condition)
	local conditionTypes = static.condition.type
	local conditionArgsId = static.condition.arg1
	for i,v in ipairs(conditionTypes) do
		if not Quest:IsEnoughCondition(i - 1) then
		-- print('QuestUtil.seekAndProgressQuest', i)
			if Quest.TempAction ~= nil then
			 	if TLBattleScene.Instance.Actor then
					TLBattleScene.Instance.Actor:AutoRunByAction(Quest.TempAction)
				end
				return  true
			end
			local func = conditionFuncMap[v]
			if not func then
				func = conditionFuncMap['default']
				print(string.format('QuestUtil.seekAndProgressQuest: use default function conditionType[%s]\ncondition[%s]', v, static.condition))
			end
			if func then
				return func(conditionArgsId[i], Quest, i)
			end
			break
		end
	end
	return false
end


function QuestUtil.doAutoDoQuest(Quest)
	-- if Quest.state ~= QuestState.NotCompleted then return end
	-- local static = Quest.Static
	-- if static.auto == 1 then
	if QuestModel.ParseQuestIsAutoDone(Quest.id) then
		QuestUtil.doQuest(Quest)
	end
end

function QuestUtil.GetNpcLoopQuestControlData(npcTempId,sceneid)
	local data = GlobalHooks.DB.Find('LoopQuestControlData',function(item) return item.npc == npcTempId and item.npc_sceneid == sceneid end)
	--print_r("GetNpcLoopQuest",data)
	return data 
end

local function GetShowText(str)
	if string.IsNullOrEmpty(str) or Util.GetText(str) == str then
		return ""
	else
		return Util.GetText(str)
	end

end

-- function QuestUtil.ShowQuestDetail(questid)
--         local questdata = GlobalHooks.DB.Find('Quest',questid)
--         if questdata == nil then
--         	questdata = GlobalHooks.DB.Find('LoopQuestData',questid)
--         	if questdata == nil then
--                print("没有相关任务id =",questid)
--                return false
--             end
--         end
--         print("任务详情id =",questid)
--         local showtext = {}
--         showtext["id"] = questid
--         showtext["主类型"] = questdata.pir_type
--         showtext["副类型"] = questdata.sub_type    
--         showtext["任务名字"] = GetShowText(questdata.quest_name)  
--         showtext["是否显示进度"] = questdata.progress    
--         showtext["时间限制"] = questdata.time_valid  
--         showtext["可接受对话"] = GetShowText(questdata.desc_canaccept) 
--         showtext["进行中面板显示内容"] = GetShowText(questdata.desc_doing) 
--         showtext["完成对话"] = GetShowText(questdata.desc_complete)   
--         showtext["面板描述"] = GetShowText(questdata.desc_questplane) 
--         showtext["进行中npc对话内容"] = GetShowText(questdata.desc_nofinish)   
--         showtext["接任务触发脚本"] = questdata.desc_accept 
--         showtext["提交任务触发脚本"] = questdata.desc_subquest   
--         showtext["接任务播放剧情脚本"] = questdata.lua_accept  
--         showtext["提交任务播放剧情脚本"] = questdata.lua_subquest    
--         showtext["接受npc"] = questdata.npc_push    
--         showtext["接受npc所在场景"] = questdata.npc_push_sceneid    
--         showtext["提交npc"] = questdata.npc_submit  
--         showtext["提交npc所在场景"] = questdata.npc_submit_sceneid  
--         showtext["未完成任务特殊处理"] = questdata.noFinish_DoSth  
--         showtext["多选对话组id"] = questdata.quest_choose    
--         showtext["接受触发事件"] = questdata.accept_event    
--         showtext["完成触发事件"] = questdata.finish_event    
--         showtext["是否可重复"] = questdata.repeat_type 
--         showtext["是否自动执行"] = questdata.auto    
--         showtext["自动做任务类型"] = questdata.automatic   
--         showtext["是否可放弃"] = questdata.quit    
--         showtext["是否主动推送到任务面板"] = questdata.push    
--         showtext["奖励经验"] = questdata.exp 
--         showtext["奖励金钱"] = questdata.gold

--         local require = {}
--         for i,v in ipairs(questdata.require.key) do
--         	local data = {}
--         	data["任务接取条件类型"] = v
--         	data["接取条件最小值"] = questdata.require.minval[i]
--         	data["接取条件最大值"] = questdata.require.maxval[i]
--         	table.insert(require,data)
--         end
--         table.insert(showtext,require)
--         local condition = {}
--         for i,v in ipairs(questdata.condition.type) do
--         	local data = {}
--         	data["任务完成条件"] = v
--         	data["任务完成条件参数1"] = questdata.condition.arg1[i]
--         	data["任务完成条件参数2"] = questdata.condition.arg2[i]
--         	data["任务完成所需的值"] = questdata.condition.val[i]
--         	data["任务完成完成几率"] = questdata.condition.probability[i]
--         	table.insert(condition,data)
--         end
--         table.insert(showtext,condition)
      
--         showtext["任务奖励掉落组"] = questdata.award.drop[1]   
--          local reward = {}
--          for i,v in ipairs(questdata.award.key) do
--         	local data = {}
--         	data["奖励物品"] = v
--         	data["奖励值"] = questdata.award.val[i]
--         	table.insert(reward,data)
--         end
--          table.insert(showtext,reward)
           
--         showtext["通用奖励显示"] = questdata.item    
--         showtext["翼族奖励显示"] = questdata.item_yz 
--         showtext["天宫奖励显示"] = questdata.item_tg 
--         showtext["昆仑奖励显示"] = questdata.item_kl 
--         showtext["青丘奖励显示"] = questdata.item_qq
--         print_r(showtext)
--         return true
-- end
function QuestUtil.Init3DSngleModel(parentCvs, pos2d, scale, menuOrder,fileName,state,cbsuccess,cbfail)
	  pos2d.y = -pos2d.y
	  local param = 
	   	{
		   	Pos = pos2d,
		   	Parent = parentCvs.UnityObject.transform,
			LayerOrder = menuOrder,
		  	Scale = scale,
		  	UILayer = true,
		  	Deg = Vector3(0,180,0),
		  	AnimatorState = state
	    }
	  if type(fileName) == 'number' then
	  	fileName = GameUtil.GetAvatarTemplateIdInfo(fileName)
	  end
	  return Util.LoadGameUnit(fileName,param,cbsuccess,cbfail)
	 
end

function QuestUtil.NeedShowLoopNumber(Quest)
	for i,v in ipairs(ShowLoopNumber) do
		if Quest.subType == v then
			return false
		end
	end
	return Quest.mainType == QuestNpcDataModel.QuestType.TypeDaily and Quest.MaxLoopNum and Quest.MaxLoopNum > 0 
end
return QuestUtil
