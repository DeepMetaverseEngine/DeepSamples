
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.TLMailInfoData


local _M = {MessageID = 0x00082002,Name = 'TLProtocol.Data.TLMailInfoData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00082002] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.TLMailInfoData'] = _M

function _M.Write(output,data)
		
	output:PutUTF(data.uuid)
	output:PutU8(data.mail_type)
	output:PutUTF(data.sender_uuid)
	output:PutUTF(data.sender_name)
	output:PutUTF(data.receiver_uuid)
	output:PutUTF(data.receiver_name)
	output:PutUTF(data.title)
	output:PutOBJ(data.content,'TLProtocol.Data.TLMailContentData')
	output:PutS32(data.template_id)
end


function _M.Read(input,data)
		
	data.uuid = input:GetUTF()
	data.mail_type = input:GetU8()
	data.sender_uuid = input:GetUTF()
	data.sender_name = input:GetUTF()
	data.receiver_uuid = input:GetUTF()
	data.receiver_name = input:GetUTF()
	data.title = input:GetUTF()
	data.content = input:GetOBJ('TLProtocol.Data.TLMailContentData')
	data.template_id = input:GetS32()
end


