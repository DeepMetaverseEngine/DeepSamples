
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.LoopQuestDataSnap


local _M = {MessageID = 0x00066004,Name = 'TLProtocol.Data.LoopQuestDataSnap'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00066004] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.LoopQuestDataSnap'] = _M

function _M.Write(output,data)
		
	output:PutList(data.QuestList, output.PutOBJ,'TLProtocol.Data.QuestDataSnap')
end


function _M.Read(input,data)
		
	data.QuestList = input:GetList(input.GetOBJ,'TLProtocol.Data.QuestDataSnap')
end


