
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.TLClientRewardBackNotify


local _M = {MessageID = 0x00071007,Name = 'TLProtocol.Protocol.Client.TLClientRewardBackNotify'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00071007] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.TLClientRewardBackNotify'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Write(output, data)
	output:PutBool(data.showIcon)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Read(input, data)
	data.showIcon = input:GetBool()
end


