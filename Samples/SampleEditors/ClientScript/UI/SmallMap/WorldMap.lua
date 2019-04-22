local _M = {}
_M.__index = _M
print('-------------load WorldMap ---------------------')
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local MapModel = require 'Model/MapModel'
local SceneModel = require 'Model/SceneModel'
local QuestUtil = require 'UI/Quest/QuestUtil'
 
function _M.OnEnter( self, ...)
 
	local TemplateID = DataMgr.Instance.UserData.MapTemplateId

	local dragonInfo = QuestUtil.GetDragonMapInfo() or {}
 
	local disasterOpen = FunctionUtil.CheckNowIsOpen('disaster', false)
	local foolyooOpen = FunctionUtil.CheckNowIsOpen('foolyoo', false)
	

	for i=1,20 do
		local cityCvs = self.ui.comps['cvs_' .. i]
		if cityCvs == nil then
			break
		end 

		local mapID = cityCvs.UserTag
		cityCvs.event_PointerUp =  function (sender,...)
	 		-- print('mapID1111: ', mapID)
	 		-- print('mapID2222: ', sender.UserTag)
	 		if TemplateID == mapID then
	 			self.ui.menu.ParentMenu.LuaTable.comps.tbt_an1.IsChecked = true
	 		else

	 			EventManager.Fire("Event.State.MapTouch",{})
	 			local action = TransPortMoveAction(mapID,"")
	 			TLBattleScene.Instance.Actor:AutoRunByAction(action)

	 		end
		end

		local name,result,minLv = MapModel.GetMapData(mapID)
		-- print('GetMapData: ',mapID, ' result: ',result,'minLv: ',minLv)
		if name ~= nil then
			local mameLabel = self.ui.comps['lb_name' .. i]
			mameLabel.Text = Util.GetText(name)

			local lvLabel = self.ui.comps['lb_lv' .. i]
			lvLabel.Text = Util.GetText(minLv)

			local lockImg = self.ui.comps['ib_lock' .. i]
			lockImg.Visible = not result
 			
 			local selectImg = self.ui.comps['ib_select' .. i]
 			selectImg.Visible = not result

 			local headImg = self.ui.comps['ib_head' ..i]
 			headImg.Visible = TemplateID == mapID

 			local treasureImg = self.ui.comps['ib_treasure' .. i]

 			-- print('mapID:  ',mapID)
			treasureImg.Visible =  dragonInfo[mapID] or false

			local disasterImg = self.ui.comps['ib_disaster' ..i]
			disasterImg.Visible = disasterOpen
			local foolyooImg = self.ui.comps['ib_foolyoo' ..i]
			foolyooImg.Visible = foolyooOpen

		end
	end

end

function _M.OnExit( self )
 	print('WorldMap OnExit ...') 
end

function _M.OnDestory( self )
	-- print('WorldMap OnDestory')

end

function _M.OnInit( self )
	-- print('WorldMap OnInit')

	local shimen = self.ui.comps.btn_sects 
	shimen.TouchClick = function ( sender)
		-- print('shimen TouchClick')
		FunctionUtil.OpenFunction('gotoshimen')

	end

	local xianmen = self.ui.comps.btn_guild
	xianmen.TouchClick = function ( sender)
		-- print('xianmen TouchClick')
		FunctionUtil.OpenFunction('enterguild')
	end

end

return _M