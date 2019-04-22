
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.ChekPointSnapData


local _M = {MessageID = 0x0006E601,Name = 'TLProtocol.Data.ChekPointSnapData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0006E601] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.ChekPointSnapData'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.state)
	output:PutS32(data.CheckPointId)
	output:PutUTF(data.PassName1st)
	output:PutF64(data.PassTime1st)
end


function _M.Read(input,data)
		
	data.state = input:GetS32()
	data.CheckPointId = input:GetS32()
	data.PassName1st = input:GetUTF()
	data.PassTime1st = input:GetF64()
end

