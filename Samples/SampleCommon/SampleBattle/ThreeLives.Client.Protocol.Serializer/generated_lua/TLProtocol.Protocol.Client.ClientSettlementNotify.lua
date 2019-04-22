
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientSettlementNotify


local _M = {MessageID = 0x00096016,Name = 'TLProtocol.Protocol.Client.ClientSettlementNotify'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00096016] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientSettlementNotify'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Write(output, data)
	output:PutS32(data.s2c_exp)
	output:PutS32(data.s2c_gold)
	output:PutDateTime(data.s2c_counttime)
	output:PutBool(data.s2c_noAward)
	output:PutS32(data.s2c_finishtime_sec)
	output:PutU8(data.s2c_status)
	output:PutList(data.s2c_itemList, output.PutOBJ,'TLProtocol.Data.TLDropItemSnapData')
	output:PutMap(data.s2c_ext, output.PutUTF, output.PutUTF,'string', 'string')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Notify'].Read(input, data)
	data.s2c_exp = input:GetS32()
	data.s2c_gold = input:GetS32()
	data.s2c_counttime = input:GetDateTime()
	data.s2c_noAward = input:GetBool()
	data.s2c_finishtime_sec = input:GetS32()
	data.s2c_status = input:GetU8()
	data.s2c_itemList = input:GetList(input.GetOBJ,'TLProtocol.Data.TLDropItemSnapData')
	data.s2c_ext = input:GetMap(input.GetUTF, input.GetUTF,'string', 'string')
end

