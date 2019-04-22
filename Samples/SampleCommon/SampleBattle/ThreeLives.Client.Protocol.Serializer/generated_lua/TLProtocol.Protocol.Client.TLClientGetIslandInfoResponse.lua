
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientGetIslandInfoResponse


local _M = {MessageID = 0x0006E002,Name = 'TLProtocol.Protocol.Client.TLClientGetIslandInfoResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0006E002] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientGetIslandInfoResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutMap(data.PointSnapMap, output.PutS32, output.PutOBJ,'int', 'TLProtocol.Data.ChekPointSnapData')
	output:PutS32(data.FinallyCheckPointId)
	output:PutS32(data.TimeLeft)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.PointSnapMap = input:GetMap(input.GetS32, input.GetOBJ,'int', 'TLProtocol.Data.ChekPointSnapData')
	data.FinallyCheckPointId = input:GetS32()
	data.TimeLeft = input:GetS32()
end


