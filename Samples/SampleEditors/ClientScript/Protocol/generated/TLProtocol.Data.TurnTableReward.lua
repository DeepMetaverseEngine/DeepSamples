
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.TurnTableReward


local _M = {MessageID = 0x000402BD,Name = 'TLProtocol.Data.TurnTableReward'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x000402BD] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.TurnTableReward'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['TLProtocol.Data.AchievementReward'].Write(output, data)
	
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['TLProtocol.Data.AchievementReward'].Read(input, data)
	
end

