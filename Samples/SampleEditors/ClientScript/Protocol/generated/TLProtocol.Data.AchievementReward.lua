
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.AchievementReward


local _M = {MessageID = 0x000401F6,Name = 'TLProtocol.Data.AchievementReward'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x000401F6] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.AchievementReward'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.templateid)
	output:PutS32(data.num)
end


function _M.Read(input,data)
		
	data.templateid = input:GetS32()
	data.num = input:GetS32()
end


