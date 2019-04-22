
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Data.AuctionSaleRecordSnap


local _M = {MessageID = 0x000A0102,Name = 'TLProtocol.Protocol.Data.AuctionSaleRecordSnap'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x000A0102] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Data.AuctionSaleRecordSnap'] = _M

function _M.Write(output,data)
		
	output:PutUTF(data.transactionId)
	output:PutS32(data.templateId)
	output:PutU32(data.num)
	output:PutS32(data.price)
	output:PutDateTime(data.time)
end


function _M.Read(input,data)
		
	data.transactionId = input:GetUTF()
	data.templateId = input:GetS32()
	data.num = input:GetU32()
	data.price = input:GetS32()
	data.time = input:GetDateTime()
end


