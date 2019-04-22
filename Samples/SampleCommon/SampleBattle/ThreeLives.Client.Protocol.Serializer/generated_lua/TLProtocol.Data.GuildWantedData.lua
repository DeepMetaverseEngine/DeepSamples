
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.GuildWantedData


local _M = {MessageID = 0x00040028,Name = 'TLProtocol.Data.GuildWantedData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00040028] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.GuildWantedData'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.curReceivedTimes)
	output:PutS32(data.maxReceivedTimes)
	output:PutS32(data.curPartakeTimes)
	output:PutS32(data.maxPartakeTimes)
	output:PutS32(data.curRefreshTimes)
	output:PutS32(data.maxRefreshTimes)
	output:PutDateTime(data.refreshTime)
	output:PutList(data.QuestMap, output.PutOBJ,'TLProtocol.Data.GuildWantedInfoData')
end


function _M.Read(input,data)
		
	data.curReceivedTimes = input:GetS32()
	data.maxReceivedTimes = input:GetS32()
	data.curPartakeTimes = input:GetS32()
	data.maxPartakeTimes = input:GetS32()
	data.curRefreshTimes = input:GetS32()
	data.maxRefreshTimes = input:GetS32()
	data.refreshTime = input:GetDateTime()
	data.QuestMap = input:GetList(input.GetOBJ,'TLProtocol.Data.GuildWantedInfoData')
end


