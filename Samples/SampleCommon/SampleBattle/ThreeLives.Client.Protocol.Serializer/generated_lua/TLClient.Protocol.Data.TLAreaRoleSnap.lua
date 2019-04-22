
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLClient.Protocol.Data.TLAreaRoleSnap


local _M = {MessageID = 0x00086001,Name = 'TLClient.Protocol.Data.TLAreaRoleSnap'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00086001] = _M
Protocol.Serializer.StringDefined['TLClient.Protocol.Data.TLAreaRoleSnap'] = _M

function _M.Write(output,data)
		
	output:PutUTF(data.roleUUID)
	output:PutUTF(data.sceneUUID)
	output:PutS32(data.maxHP)
	output:PutS32(data.curHP)
	output:PutS32(data.maxMP)
	output:PutS32(data.curMP)
end


function _M.Read(input,data)
		
	data.roleUUID = input:GetUTF()
	data.sceneUUID = input:GetUTF()
	data.maxHP = input:GetS32()
	data.curHP = input:GetS32()
	data.maxMP = input:GetS32()
	data.curMP = input:GetS32()
end


