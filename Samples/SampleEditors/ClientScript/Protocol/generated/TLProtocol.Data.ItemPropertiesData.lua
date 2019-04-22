
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.ItemPropertiesData


local _M = {MessageID = 0x00023006,Name = 'TLProtocol.Data.ItemPropertiesData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00023006] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.ItemPropertiesData'] = _M

function _M.Write(output,data)
		
	output:PutList(data.Properties, output.PutOBJ,'TLProtocol.Data.ItemPropertyData')
end


function _M.Read(input,data)
		
	data.Properties = input:GetList(input.GetOBJ,'TLProtocol.Data.ItemPropertyData')
end


