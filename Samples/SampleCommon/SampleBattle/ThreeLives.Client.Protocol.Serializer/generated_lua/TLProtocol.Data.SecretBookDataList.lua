
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.SecretBookDataList


local _M = {MessageID = 0x00040004,Name = 'TLProtocol.Data.SecretBookDataList'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00040004] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.SecretBookDataList'] = _M

function _M.Write(output,data)
		
	output:PutMap(data.secretBookList, output.PutS32, output.PutOBJ,'int', 'TLProtocol.Data.SecretBookData')
end


function _M.Read(input,data)
		
	data.secretBookList = input:GetMap(input.GetS32, input.GetOBJ,'int', 'TLProtocol.Data.SecretBookData')
end


