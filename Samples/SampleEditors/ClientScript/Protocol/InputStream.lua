local _M = {ARRAY_LIMIT = 65536}
_M.__index = _M

local NULL_MESSAGE_CODE = -1

function _M:GetU8 ()
	return self.stream:GetU8()
end

function _M:GetS8()
	return self.stream:GetS8()
end

function _M:GetS16()
	return self.stream:GetS16()
end

function _M:GetU16()
	return self.stream:GetU16()
end

function _M:GetS32()
	return self.stream:GetS32()
end

function _M:GetU32()
	return self.stream:GetU32()
end


function _M:GetS64()
	return self.stream:GetS64()
end

function _M:GetU64()
	return self.stream:GetU64()
end

function _M:GetF32()
	return self.stream:GetF32()
end


function _M:GetF64()
	return self.stream:GetF64()
end


function _M:GetBool()
	return self.stream:GetBool()
end

function _M:GetUnicode()
	return self.stream:GetUnicode()
end

function _M:GetUTF()
	return self.stream:GetUTF()
end

function _M:GetEnum8()
	return self.stream:GetEnum8()
end

function _M:GetEnum32()
	return self.stream:GetEnum8()
end

function _M:GetDateTime()
	return self.stream:GetDateTime()
end

function _M:GetTimeSpan()
	return self.stream:GetTimeSpan()
end

function _M:GetBytes()
	return self.stream:GetBytes()
end

function _M:GetArray(elefn,objTemplate)
	local len = self.stream:GetVS32()
	if len < 0 or len > self.ARRAY_LIMIT then
		return nil
	end
	local ret = {}
	for i=1,len do
		local obj = elefn(self,Protocol.Serializer.StringDefined[objTemplate])
		if obj == nil then
			obj = false
		end
		table.insert(ret, obj)
	end
	return ret
end

function _M:GetList(elefn,objTemplate)
	local len = self.stream:GetVS32()
	-- print('GetList ',objTemplate, len)
	if len < 0 or len > self.ARRAY_LIMIT then
		return nil
	end
	local ret = {}
	for i=1,len do
		local obj = elefn(self,Protocol.Serializer.StringDefined[objTemplate])
		if obj == nil then
			obj = false
		end
		table.insert(ret, obj)
	end
	return ret
end

function _M:GetMap(keyfn,valfn,keyTypeStr,valTypeStr)
	local len = self.stream:GetVS32()
	-- print('GetMap',valTypeStr, len)
	if len < 0 or len > self.ARRAY_LIMIT then
		return nil
	end
	local ret = {}
	for i=1,len do
		ret[keyfn(self,Protocol.Serializer.StringDefined[keyTypeStr])] = valfn(self,Protocol.Serializer.StringDefined[valTypeStr])
	end	
	return ret
end

function _M:GetRouteOBJ(route)
	local objTemplate = Protocol.Serializer[route]
	local ret = setmetatable({}, objTemplate)
	objTemplate.Read(self, ret)
	if not ret:IsSuccess() and not ret.s2c_msg or ret.s2c_msg == ''  and ret.s2c_code then
		local messageCodes = MessageCodeAttribute[ret.Name]
		ret.s2c_msg = messageCodes and messageCodes[ret.s2c_code] or 'unknown error'
	end
	return ret,objTemplate
end

function _M:GetOBJ(objTemplate)
	local id = self:GetS32()
	if id ~= NULL_MESSAGE_CODE then
		local ret = {}
		if not objTemplate then
			objTemplate = Protocol.Serializer[id]
		elseif type(objTemplate) == 'string' then
			objTemplate = Protocol.Serializer.StringDefined[objTemplate]
		end
		if not objTemplate then
			error('Template Message not find '..objTemplate)
		end
		objTemplate.Read(self, ret)
		return ret
	end
end

function _M:Dispose()
	self.stream:Dispose()
end

function _M.Create(bytes)
	local ret = setmetatable({}, _M)
	ret.stream = LuaInputStream(bytes)
	return ret
end

return _M