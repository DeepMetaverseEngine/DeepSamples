
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.SubmitItemData


local _M = {MessageID = 0x00066003,Name = 'TLProtocol.Data.SubmitItemData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00066003] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.SubmitItemData'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.index)
	output:PutS32(data.count)
end


function _M.Read(input,data)
		
	data.index = input:GetS32()
	data.count = input:GetS32()
end


