
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.MessageHandleData


local _M = {MessageID = 0x00083005,Name = 'TLProtocol.Data.MessageHandleData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00083005] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.MessageHandleData'] = _M

function _M.Write(output,data)
		
	output:PutUTF(data.id)
	output:PutEnum8(data.agree)
end


function _M.Read(input,data)
		
	data.id = input:GetUTF()
	data.agree = input:GetEnum8()
end


