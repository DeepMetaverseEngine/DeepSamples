
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.BlackSnapData


local _M = {MessageID = 0x00050505,Name = 'TLProtocol.Data.BlackSnapData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00050505] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.BlackSnapData'] = _M

function _M.Write(output,data)
		
	output:PutList(data.blackList, output.PutOBJ,'TLProtocol.Data.BlackInfoSnapData')
	output:PutS32(data.blackCount)
	output:PutS32(data.blackMax)
end


function _M.Read(input,data)
		
	data.blackList = input:GetList(input.GetOBJ,'TLProtocol.Data.BlackInfoSnapData')
	data.blackCount = input:GetS32()
	data.blackMax = input:GetS32()
end


