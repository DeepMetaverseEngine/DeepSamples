
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientGetFirstRechargeRewardResponse


local _M = {MessageID = 0x0009D00E,Name = 'TLProtocol.Protocol.Client.ClientGetFirstRechargeRewardResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0009D00E] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientGetFirstRechargeRewardResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutBool(data.s2c_close)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_close = input:GetBool()
end


