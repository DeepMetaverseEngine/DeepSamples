
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.TLProgressData


local _M = {MessageID = 0x00066002,Name = 'TLProtocol.Data.TLProgressData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00066002] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.TLProgressData'] = _M

function _M.Write(output,data)
		
	output:PutUTF(data.Type)
	output:PutS32(data.Arg1)
	output:PutS32(data.Arg2)
	output:PutS32(data.CurValue)
	output:PutS32(data.TargetValue)
end


function _M.Read(input,data)
		
	data.Type = input:GetUTF()
	data.Arg1 = input:GetS32()
	data.Arg2 = input:GetS32()
	data.CurValue = input:GetS32()
	data.TargetValue = input:GetS32()
end

