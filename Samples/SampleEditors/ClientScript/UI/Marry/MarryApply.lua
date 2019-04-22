local _M = {}
_M.__index = _M

local SocialModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

function CheckCanMarry( self )
	local teamData = DataMgr.Instance.TeamData
	if teamData.MemberCount ~= 2 then
		GameAlertManager.Instance:ShowNotify(Util.GetText('marry_limit1'))
		return false
	end
	if not teamData:IsLeader() then
		GameAlertManager.Instance:ShowNotify(Util.GetText('marry_limit2'))
		return false
	else
		local selfId = DataMgr.Instance.UserData.RoleID
		local members = teamData.AllMembers
		for i = 0, members.Count - 1 do
			local m = members:getItem(i)
			if m.RoleID ~= selfId then
				self.spouseId = m.RoleID
				break
			end
		end
		local selfUnity = GameSceneMgr.Instance.BattleScene:GetAIPlayer(selfId)
		local spouseUnity = GameSceneMgr.Instance.BattleScene:GetAIPlayer(self.spouseId)
		if selfUnity ~= nil and spouseUnity ~= nil then
			local marryLv = GlobalHooks.DB.GetGlobalConfig('marry_level_limit')
			if selfUnity:Level() < marryLv or spouseUnity:Level() < marryLv then
				GameAlertManager.Instance:ShowNotify(Util.GetText('marry_levellimit_text', marryLv))
				return false
			end
			local r1 = selfUnity.X - spouseUnity.X
			local r2 = spouseUnity.Y - spouseUnity.Y
			local dis = math.sqrt(r1 * r1 + r2 * r2)
			if dis > GlobalHooks.DB.GetGlobalConfig('marry_follow_range') then
				GameAlertManager.Instance:ShowNotify(Util.GetText('marry_limit3'))
				return false
			end
		else
			GameAlertManager.Instance:ShowNotify(Util.GetText('marry_limit3'))
			return false
		end

		return true
	end
end


function _M.OnLoad( self, callBack, params )
	if CheckCanMarry(self) then --返回正常，显示界面
		callBack:Invoke(true)
	else --功能未开放，关闭UI
		callBack:Invoke(true)
		self.ui.menu:Close()
	end
end

function _M.OnEnter( self )
	local db = GlobalHooks.DB.Find('Marry', {})
	self.ui.comps.lb_costnum1.Text = tostring(db[1].cost_num)
	self.ui.comps.lb_costnum2.Text = tostring(db[2].cost_num)
end

function _M.OnExit( self )

end

function _M.OnInit( self )
	self.ui.comps.btn_choose1.TouchClick = function( ... )
		SocialModel.RequestClientHoldingWedding(self.spouseId, 1, System.DateTime.Now, 0, function()
			self.ui:Close()
		end)
	end
	self.ui.comps.btn_choose2.TouchClick = function( ... )
        GlobalHooks.UI.OpenUI("WeddingReserve", 0, { spouseId = self.spouseId })
	end
end

return _M