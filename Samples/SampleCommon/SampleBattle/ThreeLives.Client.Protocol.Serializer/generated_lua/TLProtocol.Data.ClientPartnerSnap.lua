
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.ClientPartnerSnap


local _M = {MessageID = 0x00088005,Name = 'TLProtocol.Data.ClientPartnerSnap'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00088005] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.ClientPartnerSnap'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.id)
	output:PutS32(data.lv)
	output:PutS32(data.qlv)
end


function _M.Read(input,data)
		
	data.id = input:GetS32()
	data.lv = input:GetS32()
	data.qlv = input:GetS32()
end


