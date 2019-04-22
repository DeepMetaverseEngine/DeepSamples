
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientMedicinePoolUpgradeNotify


local _M = {MessageID = 0x0009E405,Name = 'TLProtocol.Protocol.Client.ClientMedicinePoolUpgradeNotify'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0009E405] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientMedicinePoolUpgradeNotify'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Write(output, data)
	output:PutS32(data.s2c_limit)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Read(input, data)
	data.s2c_limit = input:GetS32()
end

