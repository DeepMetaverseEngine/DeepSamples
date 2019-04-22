
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.MasterIdBattleInfoData


local _M = {MessageID = 0x000400CA,Name = 'TLProtocol.Data.MasterIdBattleInfoData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x000400CA] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.MasterIdBattleInfoData'] = _M

function _M.Write(output,data)
		
	output:PutDateTime(data.dateTime)
	output:PutUTF(data.message)
end


function _M.Read(input,data)
		
	data.dateTime = input:GetDateTime()
	data.message = input:GetUTF()
end

