
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Data.GuildMemberSnapData


local _M = {MessageID = 0x00087004,Name = 'TLProtocol.Protocol.Data.GuildMemberSnapData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00087004] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Data.GuildMemberSnapData'] = _M

function _M.Write(output,data)
		
	output:PutUTF(data.name)
	output:PutUTF(data.roleId)
	output:PutS32(data.level)
	output:PutS32(data.pro)
	output:PutS32(data.gender)
	output:PutS64(data.power)
	output:PutS32(data.donate)
	output:PutDateTime(data.leaveTime)
	output:PutUTF(data.guildId)
	output:PutS32(data.position)
	output:PutS32(data.contributionDay)
	output:PutS32(data.contributionMax)
	output:PutDateTime(data.ExpiredUtc)
end


function _M.Read(input,data)
		
	data.name = input:GetUTF()
	data.roleId = input:GetUTF()
	data.level = input:GetS32()
	data.pro = input:GetS32()
	data.gender = input:GetS32()
	data.power = input:GetS64()
	data.donate = input:GetS32()
	data.leaveTime = input:GetDateTime()
	data.guildId = input:GetUTF()
	data.position = input:GetS32()
	data.contributionDay = input:GetS32()
	data.contributionMax = input:GetS32()
	data.ExpiredUtc = input:GetDateTime()
end


