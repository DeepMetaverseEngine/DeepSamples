local _M = {}
_M.__index = _M


local function GetSubMenu(sheetId)
	return GlobalHooks.DB.Find('AvatarMenu',{sheet_id = sheetId})
end


local function GetSex()
     return DataMgr.Instance.UserData.Gender
end

local function GetPro()
    return DataMgr.Instance.UserData.Pro
end

local function GetAvatarModelData(avatarData)
  -- body
  local data = {}
  for k,v in pairs(avatarData.showmodle.key) do
      local partTag = v
      -- print('partTag :',partTag)
      local assetname = avatarData.showmodle.value[k]
      if assetname ~= nil and string.len(assetname) > 0 then
            local modle = {}
            modle.partTag = partTag
            modle.assetname = assetname
            table.insert(data,modle)
      end
  end 
  return data
end

local function GetAvatarShowDataTable(avatarType)
    local avatarDatas =  GlobalHooks.DB.Find('AvatarShow',function ( ele )
        return ele.avatar_type == avatarType and (ele.job_limit == 0 or ele.job_limit == GetPro()) and (ele.sex_limit == 2 or ele.sex_limit == GetSex()) and ele.is_true == 1
    end)
    table.sort(avatarDatas, function(a,b)
        return a.order < b.order
    end)
    return avatarDatas
end

 
local function GetAvatarGroupDataBySheetId(sheetId)
	return GlobalHooks.DB.FindFirst('AvatarGroup',{sheet_id = sheetId})
end 

 
 
local function GetAvatarGroupDatasByAvatarId(avatarId)
	return GlobalHooks.DB.Find('AvatarGroup',{avatar_id = avatarId})
end

local function GetAvatarLevel(level)
	return GlobalHooks.DB.FindFirst('AvatarLevel',{id = level})
end 

local function GetAvatarLevelScore(level)
	local data = GlobalHooks.DB.FindFirst('AvatarLevel',{id = level})
	if data then
		return data.score_limit
	end
	return 0
end 

local function GetAvatarInit(avatarId)
    return GlobalHooks.DB.FindFirst('AvatarInit',{avatar_id = avatarId})
end 

local function ReqGetWardrobeData(cb)
	local request = {}
	Protocol.RequestHandler.TLClientGetWardrobeDataRequest(request, function(rsp)
      	if  rsp:IsSuccess() and cb then
        	cb(rsp)
      	end
  	end)
end

local function ReqGetWardrobeInfo(avatarType,cb)
	-- body
	local request = 
	{
		c2s_avatarType = avatarType,
	}

	Protocol.RequestHandler.TLClientGetWardrobeInfoRequest(request, function(rsp)
      	if  rsp:IsSuccess() and cb then
        	cb(rsp)
      	end
  	end)

end

local function ReqestBuyAvatar(avatarId,cb,errCb)
	local request = 
	{
		c2s_avatarId = avatarId
	}
	Protocol.RequestHandler.TLClientBuyAvatarRequest(request, function(rsp)
      	if rsp:IsSuccess() and cb then
        	cb(rsp)
      	end
  	end,function()
          if errCb then
              errCb()
          end
      end)
end

local function ReqWardrobeLevelUp(cb)
	local request = {}
	Protocol.RequestHandler.TLClientWardrobeLevelUpRequest(request, function(rsp)
      	if  rsp:IsSuccess() and cb then
        	cb(rsp)
      	end
  	end)
end

local function ReqTakeOnAvatar(takeOff,avatarId,cb)
  local request = 
  {
    c2s_takeOff = takeOff,
    c2s_avatarId = avatarId
  }
  Protocol.RequestHandler.TLClientWardrobeEquipRequest(request, function(rsp)
        if  rsp:IsSuccess() and cb then
          cb(rsp)
        end
    end)
end 



_M.GetSubMenu = GetSubMenu
_M.GetAvatarShowDataTable = GetAvatarShowDataTable

_M.GetAvatarGroupDatasByAvatarId = GetAvatarGroupDatasByAvatarId
_M.GetAvatarGroupDataBySheetId = GetAvatarGroupDataBySheetId

_M.GetAvatarLevel = GetAvatarLevel
_M.GetAvatarLevelScore = GetAvatarLevelScore
_M.GetXlsFixedAttribute = GetXlsFixedAttribute
_M.TryGetAttribute = TryGetAttribute

_M.GetAvatarModelData = GetAvatarModelData
_M.ReqGetWardrobeData = ReqGetWardrobeData
_M.ReqGetWardrobeInfo = ReqGetWardrobeInfo

_M.ReqestBuyAvatar = ReqestBuyAvatar
_M.ReqWardrobeLevelUp = ReqWardrobeLevelUp
_M.ReqTakeOnAvatar = ReqTakeOnAvatar

_M.GetAvatarInit = GetAvatarInit

return _M

