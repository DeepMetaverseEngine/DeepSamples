
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientBuyDailyDungeonTicketsResponse


local _M = {MessageID = 0x00096027,Name = 'TLProtocol.Protocol.Client.ClientBuyDailyDungeonTicketsResponse'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00096027] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientBuyDailyDungeonTicketsResponse'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Write(output, data)
	output:PutUTF(data.s2c_dungeon_type)
	output:PutS32(data.s2c_count)
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Response'].Read(input, data)
	data.s2c_dungeon_type = input:GetUTF()
	data.s2c_count = input:GetS32()
end


