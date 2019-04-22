local _M = {}
_M.__index = _M
local SnapReader = require 'Model/SnapReader'
_M.TeamType = {
	NULL = 0,
	TEAM = 1,
	GROUP = 2
}

_M.InviteType = {
	NEAR = 1,
	FRIEND = 2
}

function _M.RequestInviteTeam(playerId, source, cb)
	if type(source) == 'function' then
		cb = source
		source = nil
	end
	print('---------RequestInviteTeam----------')
	local request = {c2s_roleId = playerId, c2s_source = source}
	Protocol.RequestHandler.TLClientInviteTeamRequest(
		request,
		function(rsp)
			local Util = require 'Logic/Util'
			GameAlertManager.Instance:ShowNotify(Util.GetText('team_invitation'))
			if cb then
				cb(rsp)
			end
		end
	)
end

function _M.RequestChangeLeader(leaderId, cb)
	print('---------RequestChangeLeader----------')
	local request = {c2s_leaderId = leaderId}
	Protocol.RequestHandler.TLClientChangeTeamLeaderRequest(
		request,
		function(rsp)
			-- print_r(rsp)
			if cb then
				cb(rsp)
			end
		end
	)
end

function _M.RequestKickOutTeam(playerId, cb)
	print('---------RequestKickOutTeam----------')
	local request = {c2s_roleId = playerId}
	Protocol.RequestHandler.TLClientKickTeamMemberRequest(
		request,
		function(rsp)
			-- print_r(rsp)
			if cb then
				cb(rsp)
			end
		end
	)
end

function _M.RequestLeaveTeam(cb)
	print('---------RequestLeaveTeam----------')
	local request = {}
	Protocol.RequestHandler.TLClientLeaveTeamRequest(
		request,
		function(rsp)
			-- print_r(rsp)
			if cb then
				cb(rsp)
			end
		end
	)
end

function _M.RequestTeamInfo(cb)
	print('---------RequestTeamInfo----------')
	local request = {}
	Protocol.RequestHandler.TLClientGetTeamRequest(
		request,
		function(rsp)
			-- print_r(rsp)
			if cb then
				cb(rsp)
			end
		end
	)
end

function _M.RequestCreateTeam(cb)
	print('---------RequestCreateTeam----------')
	local request = {}
	Protocol.RequestHandler.TLClientCreateTeamRequest(
		request,
		function(rsp)
			-- print_r(rsp)
			if cb then
				cb(rsp)
			end
		end
	)
end

function _M.RequestSetTeam(setting, cb)
	local request = {c2s_settting = setting}
	Protocol.RequestHandler.ClientSetTeamRequest(
		request,
		function(rsp)
			-- print_r(rsp)
			print('set team ok')
			if cb then
				cb(rsp)
			end
		end
	)
end

function _M.RequestSetTeamTarget(targetID, cb)
	if not DataMgr.Instance.TeamData:IsLeader() then
		return
	end
	local setting = {
		TargetID = targetID,
		AutoMatch = false,
		MinLevel = 1,
		MinFightPower = 0,
		AutoStartTarget = false
	}
	local request = {c2s_settting = setting}
	Protocol.RequestHandler.ClientSetTeamRequest(
		request,
		function(rsp)
			-- print_r(rsp)
			print('set team ok')
			if cb then
				cb(rsp)
			end
		end
	)
end

function _M.RequestTargetTeamList(targetID, point, cb)
	point = point or 0
	local request = {c2s_point = point, c2s_target = targetID}
	Protocol.RequestHandler.ClientGetTargetTeamRequest(
		request,
		function(rsp)
			cb(rsp)
		end
	)
end

function _M.ReuestLeaveAutoMatch(cb, errcb)
	local hasTeam = DataMgr.Instance.TeamData.HasTeam
	local isleader = DataMgr.Instance.TeamData:IsLeader()
	local Util = require 'Logic/Util'
	if hasTeam and not isleader then
		GameAlertManager.Instance:ShowNotify(Util.GetText('team_no_authority'))
	elseif hasTeam then
		local setting = DataMgr.Instance.TeamData.Setting:Clone()
		setting.AutoMatch = false
		DataMgr.Instance.TeamData:UploadSetting(setting, cb, errcb)
	else
		local request = {c2s_target = 0}
		Protocol.RequestHandler.ClientAutoTeamRequest(request, cb, errcb)
	end
end

function _M.RequestAutoMatch(targetID, cb, errcb)
	local hasTeam = DataMgr.Instance.TeamData.HasTeam
	local isleader = DataMgr.Instance.TeamData:IsLeader()
	local Util = require 'Logic/Util'
	if hasTeam and not isleader then
		GameAlertManager.Instance:ShowNotify(Util.GetText('team_no_authority'))
	elseif hasTeam then
		local lastTargetID = DataMgr.Instance.TeamData.Setting.TargetID
		if lastTargetID == targetID and DataMgr.Instance.TeamData.Setting.AutoMatch then
			return
		end
		local setting = DataMgr.Instance.TeamData.Setting:Clone()
		setting.TargetID = targetID
		setting.AutoMatch = true
		DataMgr.Instance.TeamData:UploadSetting(setting, cb, errcb)
	else
		local request = {c2s_target = targetID}
		Protocol.RequestHandler.ClientAutoTeamRequest(request, cb, errcb)
	end
end

function _M.RequestAskFollow(targetID, cb)
	local request = {c2s_roleID = targetID}
	Protocol.RequestHandler.ClientTeamAskFollowRequest(
		request,
		function(rsp)
			if cb then
				cb()
			end
		end
	)
end

function _M.RequestFriendRoleList(cb)
	local request = {c2s_type = 1}
	Protocol.RequestHandler.ClientGetRoleListRequest(
		request,
		function(rsp)
			if cb then
				cb(rsp.s2c_playerList)
			end
		end
	)
end

function _M.RequestGuildRoleList(cb)
	local request = {c2s_type = 2}
	Protocol.RequestHandler.ClientGetRoleListRequest(
		request,
		function(rsp)
			if cb then
				cb(rsp.s2c_playerList)
			end
		end
	)
end

local function CheckValid(target, function_id)
	local lv = DataMgr.Instance.UserData.Level
	if function_id ~= target.function_id then
		return false
	end
	return (lv >= target.lv_min or target.lv_min == 0) and (lv <= target.lv_max or target.lv_max == 0)
end

function _M.FindTargetByFunctionID(function_id)
	return unpack(
		GlobalHooks.DB.Find(
			'team_target',
			function(ele)
				return CheckValid(ele, function_id)
			end
		)
	)
end

function _M.InitNetWork(initNotify)
	-- print('----------TeamModel InitNetWork------------')
	if initNotify then
	end
end

function _M.fin()
	-- print('----------TeamModel fin------------')
end

function _M.initial()
	-- print('----------TeamModel inital------------')
end

local function LoadTeamSnap(keys, cb)
	local request = {c2s_teamKeys = keys}
	Protocol.RequestHandler.ClientGetTeamSnapRequest(
		request,
		function(rsp)
			cb(rsp.s2c_teamList)
		end
	)
end

_M.SnapReader = SnapReader.Create(LoadTeamSnap)

return _M
