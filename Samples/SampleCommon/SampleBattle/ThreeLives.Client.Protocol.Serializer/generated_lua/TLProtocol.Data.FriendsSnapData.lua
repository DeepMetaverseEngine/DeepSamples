
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.FriendsSnapData


local _M = {MessageID = 0x00050503,Name = 'TLProtocol.Data.FriendsSnapData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00050503] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.FriendsSnapData'] = _M

function _M.Write(output,data)
		
	output:PutList(data.friendList, output.PutOBJ,'TLProtocol.Data.FriendsInfoSnapData')
	output:PutS32(data.friendCount)
	output:PutS32(data.friendMax)
end


function _M.Read(input,data)
		
	data.friendList = input:GetList(input.GetOBJ,'TLProtocol.Data.FriendsInfoSnapData')
	data.friendCount = input:GetS32()
	data.friendMax = input:GetS32()
end

