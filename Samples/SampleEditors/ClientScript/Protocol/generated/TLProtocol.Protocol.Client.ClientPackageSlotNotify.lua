
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientPackageSlotNotify


local _M = {MessageID = 0x00037003,Name = 'TLProtocol.Protocol.Client.ClientPackageSlotNotify'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00037003] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientPackageSlotNotify'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Write(output, data)
	output:PutUTF(data.s2c_reason)
	output:PutU8(data.s2c_type)
	output:PutBool(data.s2c_init)
	output:PutMap(data.s2c_slots, output.PutS32, output.PutOBJ,'int', 'TLProtocol.Data.EntityItemData')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Read(input, data)
	data.s2c_reason = input:GetUTF()
	data.s2c_type = input:GetU8()
	data.s2c_init = input:GetBool()
	data.s2c_slots = input:GetMap(input.GetS32, input.GetOBJ,'int', 'TLProtocol.Data.EntityItemData')
end


