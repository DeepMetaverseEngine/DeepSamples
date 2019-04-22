---------------------------------
--! @file
--! @brief a Doxygen::Lua ApiQuest.lua
---------------------------------
local QuestModel = require "Model/QuestModel"
local QuestNpcDataModel = require'Model/QuestNpcDataModel'
local _M = {}

function _M._asyncTalkContent(self,textkey,npc_model,npc_name)
	--print(fileName,isShowUi)
	local TalkContext = QuestNpcDataModel.CreateContext(
								{
									model_id = npc_model or nil,
									speaker_name = npc_name or "",
									dialogue_content = textkey
							    }
						)
	local params = {
						TalkContext = TalkContext,
						cb = function ()
							self:Stop()
						end
					}
	QuestNpcDataModel.OpenQuestTalk(params)
	self:Await()
end


function _M._asyncTalkContentbyId(self,dialogueid)
	--print(fileName,isShowUi)
	local TalkContext = QuestNpcDataModel.GetQuestContext(dialogueid)
	if TalkContext == nil then
		return 
	end
	local params = {
						TalkContext = TalkContext,
						cb = function ()
							self:Stop()
						end
					}
	QuestNpcDataModel.OpenQuestTalk(params)
	self:Await()
end
return _M