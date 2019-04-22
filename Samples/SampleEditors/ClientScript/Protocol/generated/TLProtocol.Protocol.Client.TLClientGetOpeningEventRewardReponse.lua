
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientGetOpeningEventRewardReponse


local _M = {MessageID = 0x00040265,Name = 'TLProtocol.Protocol.Client.TLClientGetOpeningEventRewardReponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00040265] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientGetOpeningEventRewardReponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutList(data.s2c_data, output.PutOBJ,'TLProtocol.Data.OpeningEventReward')
	output:PutS32(data.s2c_curFinishPoints)
	output:PutMap(data.exchargeDataMap, output.PutS32, output.PutS32,'int', 'int')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_data = input:GetList(input.GetOBJ,'TLProtocol.Data.OpeningEventReward')
	data.s2c_curFinishPoints = input:GetS32()
	data.exchargeDataMap = input:GetMap(input.GetS32, input.GetS32,'int', 'int')
end


