
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.RequireInfo


local _M = {MessageID = 0x0006C602,Name = 'TLProtocol.Data.RequireInfo'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0006C602] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.RequireInfo'] = _M

function _M.Write(output,data)
		
	output:PutList(data.requireList, output.PutOBJ,'TLProtocol.Data.RequireSnapData')
end


function _M.Read(input,data)
		
	data.requireList = input:GetList(input.GetOBJ,'TLProtocol.Data.RequireSnapData')
end


