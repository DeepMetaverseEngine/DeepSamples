
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Protocol.Client.ClientChangeSceneRequest


local _M = {MessageID = 0x00096012,Name = 'TLProtocol.Protocol.Client.ClientChangeSceneRequest'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00096012] = _M
Protocol.Serializer.StringDefined['TLProtocol.Protocol.Client.ClientChangeSceneRequest'] = _M

function _M.Write(output,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Write(output, data)
	output:PutUTF(data.c2s_MapUUID)
	output:PutS32(data.c2s_MapId)
	output:PutUTF(data.c2s_GuildUUID)
	output:PutUTF(data.c2s_NextMapPosition)
	output:PutMap(data.c2s_Ext, output.PutUTF, output.PutUTF,'string', 'string')
end


function _M.Read(input,data)
	Protocol.Serializer.StringDefined['DeepMMO.Protocol.Request'].Read(input, data)
	data.c2s_MapUUID = input:GetUTF()
	data.c2s_MapId = input:GetS32()
	data.c2s_GuildUUID = input:GetUTF()
	data.c2s_NextMapPosition = input:GetUTF()
	data.c2s_Ext = input:GetMap(input.GetUTF, input.GetUTF,'string', 'string')
end

