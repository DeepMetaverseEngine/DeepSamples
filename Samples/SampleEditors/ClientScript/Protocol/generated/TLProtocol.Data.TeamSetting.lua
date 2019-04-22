
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.TeamSetting


local _M = {MessageID = 0x00084003,Name = 'TLProtocol.Data.TeamSetting'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00084003] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.TeamSetting'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.TargetID)
	output:PutBool(data.AutoStartTarget)
	output:PutS32(data.MinLevel)
	output:PutS32(data.MinFightPower)
	output:PutBool(data.AutoMatch)
end


function _M.Read(input,data)
		
	data.TargetID = input:GetS32()
	data.AutoStartTarget = input:GetBool()
	data.MinLevel = input:GetS32()
	data.MinFightPower = input:GetS32()
	data.AutoMatch = input:GetBool()
end


