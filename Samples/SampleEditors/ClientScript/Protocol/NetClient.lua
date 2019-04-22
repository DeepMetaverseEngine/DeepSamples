---------------------------------
--! @file
--! @brief a Doxygen::Lua NetClient.lua
---------------------------------
require 'Protocol/generated/codec'
Protocol.Serializer.Config('Protocol/generated')

local InputStream = require 'Protocol/InputStream'
local OutputStream = require 'Protocol/OutputStream'

local RequestHandler = {}
RequestHandler.__index = function(self, key)
	print('RequestHandler', key)
	-- request
	Protocol.InitShortMap()
	local tempalteObj = Protocol.ShortStringDefined[key]
	if not tempalteObj then
		error(key .. ' not exists')
	end
	return function(msg, cb, errcb, option)
		if type(msg) == 'function' and not cb then
			msg = {}
			cb = msg
		end
		Protocol.Request(msg, tempalteObj, cb, errcb, option)
	end
end

local PushHandler = {}
PushHandler.__index = function(self, key)
	-- request
	Protocol.InitShortMap()
	local tempalteObj = Protocol.ShortStringDefined[key]
	if not tempalteObj then
		error(key .. ' not exists')
	end
	return function(cb)
		Protocol.Listen(tempalteObj, cb)
	end
end

local NotifyHandler = {}
NotifyHandler.__index = function(self, key)
	-- request
	Protocol.InitShortMap()
	local tempalteObj = Protocol.ShortStringDefined[key]
	if not tempalteObj then
		error(key .. ' not exists')
	end
	return function(msg)
		Protocol.Notify(msg, tempalteObj)
	end
end

Protocol = Protocol or {}
Protocol.RequestHandler = setmetatable({}, RequestHandler)
Protocol.PushHandler = setmetatable({}, PushHandler)
Protocol.NotifyHandler = setmetatable({}, NotifyHandler)

function Protocol.InitShortMap()
	if Protocol.ShortStringDefined then
		return
	end
	Protocol.ShortStringDefined = {}
	for k, v in pairs(Protocol.Serializer.StringDefined) do
		local fields = {}
		k:gsub(
			'([^%.]+)',
			function(c)
				fields[#fields + 1] = c
			end
		)
		local shortKey = fields[#fields]
		-- print('shortkey', shortKey)
		if Protocol.ShortStringDefined[shortKey] then
			error(key .. ' has exists')
		end
		Protocol.ShortStringDefined[shortKey] = v
	end
end

function Protocol.Request(msg, tempalteObj, cb, errcb, option)
	local output = OutputStream.Create()
	output:PutRouteOBJ(msg, tempalteObj)
	local rq = BinaryMessage.FromBuffer(tempalteObj.MessageID, output:GetBuffer())
	output:Dispose()
	if type(cb) == 'userdata' then
		option = errcb
		errcb = nil
		cb = nil
	end
	if type(errcb) == 'userdata' then
		option = errcb
		errcb = nil
	end
	GameSceneMgr.Instance:OnRequestStartEvent(tempalteObj.Name, option)
	TLNetManage.Instance:RequestBinary(
		rq,
		function(ex, rp)
			local msg_rp
			if not ex then
				--decode
				--print('rp.buffer', rp.Buffer)
				local input = InputStream.Create(rp:ToArray())
				msg_rp, rpTemplate = input:GetRouteOBJ(rp.Route)
				input:Dispose()
				GameSceneMgr.Instance:OnRequestEndEvent(tempalteObj.Name, msg_rp.s2c_code, msg_rp.s2c_msg, ex, option)
				if not msg_rp:IsSuccess() then
					-- TODO
					print('Got error code' .. msg_rp.s2c_code .. (msg_rp.s2c_msg or ''))
					if errcb then
						errcb(msg_rp)
					end
				elseif cb then
					cb(msg_rp)
				end
				print('Recived ', rpTemplate.Name, msg_rp.s2c_code)
			else
				-- TODO
				GameSceneMgr.Instance:OnRequestEndEvent(tostring(rp.Route), -1, '', ex, option)
				print('Got exception' .. tostring(ex))
			end
		end
	)
end

function Protocol.Listen(tempalteObj, cb)
	TLNetManage.Instance:ListenBinary(
		tempalteObj.MessageID,
		function(rp)
			--print('rp.buffer', rp.Buffer)
			local input = InputStream.Create(rp:ToArray())
			local msg_rp = input:GetRouteOBJ(rp.Route)
			input:Dispose()
			cb(msg_rp)
		end
	)
end

function Protocol.Notify(msg, tempalteObj)
	local output = OutputStream.Create()
	output:PutRouteOBJ(msg, tempalteObj)
	local rq = BinaryMessage.FromBuffer(tempalteObj.MessageID, output:GetBuffer())
	output:Dispose()
	TLNetManage.Instance:NotifyBinary(rq)
end
