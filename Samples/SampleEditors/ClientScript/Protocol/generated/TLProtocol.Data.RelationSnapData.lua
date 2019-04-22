
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.RelationSnapData


local _M = {MessageID = 0x0005050C,Name = 'TLProtocol.Data.RelationSnapData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x0005050C] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.RelationSnapData'] = _M

function _M.Write(output,data)
		
	output:PutMap(data.recordList, output.PutUTF, output.PutOBJ,'string', 'TLProtocol.Data.SocialGiftRecordSnap')
	output:PutList(data.friendList, output.PutOBJ,'TLProtocol.Data.FriendsInfoSnapData')
	output:PutS32(data.friendCount)
	output:PutS32(data.friendMax)
end


function _M.Read(input,data)
		
	data.recordList = input:GetMap(input.GetUTF, input.GetOBJ,'string', 'TLProtocol.Data.SocialGiftRecordSnap')
	data.friendList = input:GetList(input.GetOBJ,'TLProtocol.Data.FriendsInfoSnapData')
	data.friendCount = input:GetS32()
	data.friendMax = input:GetS32()
end


