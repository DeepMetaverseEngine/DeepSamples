local _M = {}
_M.__index = _M

local PracticeModel = require 'Model/PracticeModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'
local AvatarModel = require 'Model/AvatarModel'

function _M.OnEnter( self, prama )
	-- print('----------PracticeReward OnEnter-----------', prama)
	local dbStage = GlobalHooks.DB.Find('practice', prama)
	self.ui.comps.tb_common.Text = Util.GetText('practice_msg', Util.GetText(dbStage.artifact_name))
	local groupId = dbStage.group_id
	local pro = DataMgr.Instance.UserData.Pro
	local gen = DataMgr.Instance.UserData.Gender
	local modeldb = GlobalHooks.DB.FindFirst('practice_model', { group_id = groupId, pro = pro, sex = gen })
	local reward = modeldb.reward
	self.avatar_sheet = modeldb.avatar_sheet.id
	local len = self.ui.comps.cvs_itemlist.NumChildren > #reward.id and self.ui.comps.cvs_itemlist.NumChildren or #reward.id
	local cvsRoot = self.ui.comps.cvs_itemlist
	local space = 30
	local showItemCount = 0
	local itemW
	for i = 1, len do
		local cvs_item = cvsRoot:FindChildByName('cvs_item'..i, true)
		if i <= #reward.id and reward.id[i] > 0 then
			showItemCount = showItemCount + 1
			if cvs_item == nil then
				if i == 1 then
					cvs_item = cvsRoot:FindChildByEditName('cvs_item', true)
					cvs_item.Name = 'cvs_item1'
					cvs_item.X = space
				else
					local prefab = cvsRoot:FindChildByName('cvs_item1', true)
					cvs_item = prefab:Clone()
					cvsRoot:AddChild(cvs_item)
					cvs_item.Name = 'cvs_item'..i
					cvs_item.X = prefab.X + (i - 1) * (space + cvs_item.Width)
					cvs_item.Y = prefab.Y
				end
			end
			itemW = cvs_item.Width
			cvs_item.Visible = true
			local itemdetail = ItemModel.GetDetailByTemplateID(reward.id[i])
			local icon = itemdetail.static.atlas_id
			local quality = itemdetail.static.quality
			local num = reward.num[i]
			local itshow = UIUtil.SetItemShowTo(cvs_item, icon, quality, num)
	        itshow.EnableTouch = true
	        itshow.TouchClick = function()
	            local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail, itemShow = itshow, autoHeight = true})
	            -- detail:SetPos(0, 350)
	        end
		else
			if cvs_item then
				cvs_item.Visible = false
			end
		end
	end
	self.ui.comps.cvs_itemlist.Width = space + showItemCount * (space + itemW)
	self.ui.comps.cvs_itemlist.X = (self.ui.comps.cvs_tips2.Width - self.ui.comps.cvs_itemlist.Width) * 0.5
	
end

function _M.OnExit( self )
	
end

function _M.OnDestory( self )
	
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
	self.ui.comps.bt_get.TouchClick = function( ... )
		self.ui.menu:Close()
	end
	self.ui.comps.bt_wear.TouchClick = function( ... )
		for i = 1, #self.avatar_sheet do
			local sheetId = self.avatar_sheet[i]
  			local avatarGroupDb = AvatarModel.GetAvatarGroupDataBySheetId(sheetId)
	 		AvatarModel.ReqTakeOnAvatar(0, avatarGroupDb.avatar_id, function(rsp)
	 			if i == 1 then
					self.ui.menu:Close()
				end
	 		end)
		end
	end
end

return _M