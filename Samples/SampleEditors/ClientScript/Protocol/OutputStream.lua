local _M = {}
_M.__index = _M

local NULL_MESSAGE_CODE = -1

function _M:PutU8 (v)
	self.stream:PutU8(v)
end

function _M:PutS8(v)
	self.stream:PutS8(v)
end

function _M:PutS16(v)
	self.stream:PutS16(v)
end

function _M:PutU16(v)
	self.stream:PutU16(v)
end

function _M:PutS32(v)
	self.stream:PutS32(v)
end

function _M:PutU32(v)
	self.stream:PutU32(v)
end


function _M:PutS64(v)
	self.stream:PutS64(v)
end

function _M:PutU64(v)
	self.stream:PutU64(v)
end

function _M:PutF32(v)
	self.stream:PutF32(v)
end

function _M:PutF64(v)
	self.stream:PutF64(v)
end

function _M:PutBool(v)
	self.stream:PutBool(v)
end

function _M:PutUnicode(v)
	self.stream:PutBool(v)
end

function _M:PutUTF(v)
	self.stream:PutUTF(v)
end

function _M:PutEnum8(v)
	self.stream:PutEnum8(v)
end


function _M:PutEnum32(v)
	self.stream:PutEnum32(v)
end

function _M:PutDateTime(v)
	self.stream:PutDateTime(v)
end

function _M:PutTimeSpan(v)
	self.stream:PutTimeSpan(v)
end

function _M:PutBytes(v)
	self.stream:PutBytes(v)
end

function _M:PutArray(array, elefn, objTempalte)
	if not array then
		self.stream:PutVS32(NULL_MESSAGE_CODE)
		return 
	end
	self.stream:PutVS32(#array)
	for _,v in ipairs(array) do
		elefn(self,v,Protocol.Serializer.StringDefined[objTempalte])
	end
end

function _M:PutList(list, elefn,objTempalte)
	if not list then
		self.stream:PutVS32(NULL_MESSAGE_CODE)
		return 
	end
	self.stream:PutVS32(#list)
	for _,v in ipairs(list) do
		elefn(self,v,Protocol.Serializer.StringDefined[objTempalte])
	end
end

function _M:PutMap(map,keyfn,valfn,keyTypeStr,valTypeStr)
	if not map or not next(map) then
		self.stream:PutVS32(NULL_MESSAGE_CODE)
		return 
	end
	local count = 0
	for _,__ in pairs(map) do
		count = count + 1
	end
	self.stream:PutVS32(count)
	for k,v in pairs(map) do
		keyfn(self,k,Protocol.Serializer.StringDefined[keyTypeStr])
		valfn(self,v,Protocol.Serializer.StringDefined[valTypeStr])
	end
end

function _M:PutRouteOBJ(obj,objTemplate)
	local typeStr = type(objTemplate)
	if typeStr == 'number' then
		objTemplate = Protocol.Serializer[objTemplate]
	elseif typeStr == 'string' then
		objTemplate = Protocol.Serializer.StringDefined[objTemplate]
	end
	-- print(obj, objTempalte)
	objTemplate.Write(self,obj)
end

function _M:PutOBJ(obj,objTemplate)
	if not obj then
		self:PutS32(NULL_MESSAGE_CODE)
		return
	end
	local typeStr = type(objTemplate)
	if typeStr == 'number' then
		objTemplate = Protocol.Serializer[objTemplate]
	elseif typeStr == 'string' then
		objTemplate = Protocol.Serializer.StringDefined[objTemplate]
	end
	self:PutS32(objTemplate.MessageID)
	objTemplate.Write(self,obj)
end

function _M:ToArray()
	return self.stream:ToArray()
end

function _M:Dispose()
	self.stream:Dispose()
end

function _M:GetBuffer()
	return self.stream.Buffer
end

function _M.Create()
	local ret = setmetatable({}, _M)
	ret.stream = LuaOutputStream()
	return ret
end

return _M