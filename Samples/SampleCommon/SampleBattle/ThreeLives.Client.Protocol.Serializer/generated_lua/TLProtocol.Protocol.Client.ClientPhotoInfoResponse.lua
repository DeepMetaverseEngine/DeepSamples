
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientPhotoInfoResponse


local _M = {MessageID = 0x00079004,Name = 'TLProtocol.Protocol.Client.ClientPhotoInfoResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00079004] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientPhotoInfoResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutUTF(data.s2c_url)
	output:PutUTF(data.s2c_prefix)
	output:PutOBJ(data.s2c_datas,'ThreeLives.Client.Protocol.Data.RolePhotoData')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_url = input:GetUTF()
	data.s2c_prefix = input:GetUTF()
	data.s2c_datas = input:GetOBJ('ThreeLives.Client.Protocol.Data.RolePhotoData')
end


