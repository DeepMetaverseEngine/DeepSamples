local Helper = require 'Logic/Helper'
local localParams = nil
local QuestNpcDataModel = {}
local Util = require 'Logic/Util'
function QuestNpcDataModel.GetQuestContext(Questid)
	local data = GlobalHooks.DB.Find('QuestDialogue',{quest_name = Questid})
	if #data == 0 then
		print(string.format("GetQuestContext key = %s is nil",Questid))
		return nil
	end
	return Helper.copy_table(data)
end
function QuestNpcDataModel.GetQuestChooseData(Questid)
	local data = GlobalHooks.DB.Find('Quest_Choose',Questid)
	return data
end
function QuestNpcDataModel.CreateContext(createTable)
		if type(createTable) ~= 'table' then
			error("CreateContext need table type")
		 return 
		end
		local params = createTable
		--"id","Quest_name","model_id","dubbing","speaker_name","dialogue_content"
		-- print("CreateContextBegin")
		-- print_r(params)
		local content = {}
		content.id= params.id or ""
		content.Quest_name = params.Quest_name or ""
		content.model_id= params.model_id or ""
		content.dubbing = params.dubbing or ""
		content.speaker_name = params.speaker_name or ""
		content.dialogue_content = params.dialogue_content or ""
		content.choose = params.choose or nil
		content.choose_desc = params.choose_desc or nil
		content.choosenum = params.choosenum or nil
		-- print("CreateContextOver")
		-- print_r(content)
		local _table = {}
		table.insert(_table,content)
		return _table
end

function QuestNpcDataModel.FindRoad(roadid,roadmap)
	
	local data = unpack(GlobalHooks.DB.Find('RoadData',{template_id = roadid , born_scene = roadmap}))
	if data ~= nil then
		return data.born_point,data.dont_finding_hints
	end
	return nil,nil
end

function QuestNpcDataModel.FindRoadByFunctionTag(functiontag)
	local data = unpack(GlobalHooks.DB.Find('RoadData',{funtion_tag = functiontag}))
	if data ~= nil then
		return data.template_id,data.born_scene,data.born_point,data.isroad,data.type
	else
		return 0
	end
end

--npc内容读取
local stateStrMap = {
    [QuestState.NotAccept] = "desc_accept",
    [QuestState.NotCompleted] = "desc_nofinish",
    [QuestState.CompletedNotSubmited] = "desc_subquest",
}

local stateFindNpc = {
    [QuestState.NotAccept] = "npc_push",
    [QuestState.NotCompleted] = "npc_submit",
    [QuestState.CompletedNotSubmited] = "npc_submit",
}

local QuestType =
{
    ["TypeNone"] = 0,
    ["TypeStory"] = 1,
    ["TypeGuide"] = 2,
    ["TypeDaily"] = 3,
    ["TypeGuildWanted"] = 5,
    ["TypeTip"] = 100
}
--判断是否有文本
function QuestNpcDataModel.ParseQuestTalkContext(params)
		local QuestId = params.id
		local model_id = "<name>"
		local speaker_name = "<name>"
	  	local Quest = DataMgr.Instance.QuestData:GetQuest(QuestId)
		local static = QuestNpcDataModel.GetQuestData(Quest) --GlobalHooks.DB.Find('Quest', QuestId)
		local TalkContext = nil
		local curNpcTemplateId = params.NpcTemplateId or 0
		if Quest ~= nil and static ~= nil then
			--print("static[key]1",QuestId,Quest.state)
			--print("QuestNpcDataModeNpc1",Quest.state,stateFindNpc[Quest.state])
			local Npc = static[stateFindNpc[Quest.state]] or 0
			--print("QuestNpcDataModeNpc2",Npc,curNpcTemplateId)
			if (Npc ~= 0 and curNpcTemplateId == Npc) or Npc == 0 then
				local key = stateStrMap[Quest.state]
				--print("ParseQuestTalkContextQuest.state",static.quest_choose,Quest.state)
				if Quest.state == QuestState.CompletedNotSubmited and static.quest_choose ~= 0 then
					--print("quest_choose==",QuestId,static.quest_choose)
					Choosedata = QuestNpcDataModel.GetQuestChooseData(static.quest_choose)
					--print_r("Choosedata",Choosedata)
					TalkContext = QuestNpcDataModel.CreateContext(
							{
								model_id = model_id,
								speaker_name = speaker_name,
								dialogue_content = Choosedata.question,
								choosenum = Choosedata.choose_number,
								choose = Choosedata.choose,
								choose_desc = Choosedata.choose_desc
						    }
						)
				else
					if not string.IsNullOrEmpty(static[key]) then 
						--print("static[key]",static[key],key,Npc,Quest.state)
						if Quest.state == QuestState.NotCompleted  and Npc ~= 0 then
							local model_id = params.model_id or model_id
							local speaker_name = params.speaker_name or speaker_name
							TalkContext = QuestNpcDataModel.CreateContext(
								{
									model_id = model_id,
									speaker_name = speaker_name,
									dialogue_content = static[key]
							    })
							--print_r('TalkContext',TalkContext)
							if  static[key] == Util.GetText(static[key]) then
								TalkContext = nil
							end
						else
							TalkContext = QuestNpcDataModel.GetQuestContext(static[key])
						end
					else
						print("static[key] is nil",key,Npc,Quest.state,QuestId)
					end
	   			end
	   		end
	   		--print_r("ParseQuestTalkContext",TalkContext)
	   	else
	   		--print_r("Quest",Quest)
	   		--print_r("static",static)
   		end

   	   return TalkContext
		
end



function QuestNpcDataModel.OpenQuestTalk(params)
	--print("OpenQuestTalkPlayCGStart",PlayCGStart)
	if PlayCGStart == true then
		localParams = params
		return
	else
		if params.TalkContext == nil then
	   		params.TalkContext = QuestNpcDataModel.ParseQuestTalkContext(params)
	    end
	    if params.TalkContext then
			--print_r("TalkContext",params.TalkContext)
	    	if GlobalHooks.UI.FindUI('DialogueTalk') == nil then
	    		MenuMgr.Instance:CloseAllMenu()
	    		GlobalHooks.UI.OpenUI('DialogueTalk', 0, params)
	    	end
	    	return true
	    else
	    	if params.cb then
				params.cb()
				params.cb = nil
			end
			return false
	    end
	end

end

local function PLAYCG_START(eventname,params)
	-- body
	--print("QuestNpcDataModelPLAYCG_START",params.PlayCG)
	PlayCGStart = params.PlayCG
	if not PlayCGStart then
		if localParams ~= nil then
			QuestNpcDataModel.OpenQuestTalk(localParams)
			localParams = nil
		end
	end
end

function QuestNpcDataModel.GetQuestData(Quest)
	--print_r(QuestNpcDataModel,Quest)
	if Quest == nil then
		return nil
	end
	local data = GlobalHooks.DB.Find('Quest',Quest.id)
	if data == nil then
		UnityEngine.Debug.LogError("QuestNpcDataModel Quest.id =  "..Quest.id.." is not exist")
		return nil
	end
	return data
	-- if data == nil then
	-- 	data = GlobalHooks.DB.Find('LoopQuestData',Quest.id)
	-- end
	-- if Quest.mainType ~= QuestType.TypeDaily then
	-- 	return GlobalHooks.DB.Find('Quest',Quest.id)
	-- else
	-- 	return GlobalHooks.DB.Find('LoopQuestData',Quest.id)
	--end
	
end

local function initial()
	--print("QuestNpcDataModel.initial")
	PlayCGStart = false
	EventManager.Subscribe(Events.PLAYCG_START,PLAYCG_START)
	
end
local function fin()
	--print("QuestNpcDataModel.fin")
	localParams = nil
	EventManager.Unsubscribe(Events.PLAYCG_START,PLAYCG_START)
end
QuestNpcDataModel.initial = initial
QuestNpcDataModel.fin = fin
QuestNpcDataModel.QuestType = QuestType
return QuestNpcDataModel
