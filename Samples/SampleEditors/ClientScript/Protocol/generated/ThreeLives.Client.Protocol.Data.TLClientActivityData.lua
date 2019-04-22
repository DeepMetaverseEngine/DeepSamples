
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- ThreeLives.Client.Protocol.Data.TLClientActivityData


local _M = {MessageID = 0x0009A064,Name = 'ThreeLives.Client.Protocol.Data.TLClientActivityData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0009A064] = _M
Protocol.Serializer.StringDefined['ThreeLives.Client.Protocol.Data.TLClientActivityData'] = _M

function _M.Write(output,data)
		
	output:PutMap(data.RewardRecord, output.PutS32, output.PutS32,'int', 'int')
	output:PutS32(data.ActivityPoint)
	output:PutList(data.ActivityList, output.PutOBJ,'ThreeLives.Client.Protocol.Data.TLClientActivitySnap')
end


function _M.Read(input,data)
		
	data.RewardRecord = input:GetMap(input.GetS32, input.GetS32,'int', 'int')
	data.ActivityPoint = input:GetS32()
	data.ActivityList = input:GetList(input.GetOBJ,'ThreeLives.Client.Protocol.Data.TLClientActivitySnap')
end

