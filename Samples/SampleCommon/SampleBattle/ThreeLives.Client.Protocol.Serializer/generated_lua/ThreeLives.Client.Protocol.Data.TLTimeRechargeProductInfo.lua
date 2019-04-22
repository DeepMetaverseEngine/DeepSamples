
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- ThreeLives.Client.Protocol.Data.TLTimeRechargeProductInfo


local _M = {MessageID = 0x0009D502,Name = 'ThreeLives.Client.Protocol.Data.TLTimeRechargeProductInfo'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0009D502] = _M
Protocol.Serializer.StringDefined['ThreeLives.Client.Protocol.Data.TLTimeRechargeProductInfo'] = _M

function _M.Write(output,data)
		
	output:PutDateTime(data.s2c_leftTimeUTC)
	output:PutS32(data.s2c_productID)
	output:PutS32(data.s2c_buyCount_Total)
	output:PutBool(data.s2c_available)
end


function _M.Read(input,data)
		
	data.s2c_leftTimeUTC = input:GetDateTime()
	data.s2c_productID = input:GetS32()
	data.s2c_buyCount_Total = input:GetS32()
	data.s2c_available = input:GetBool()
end

