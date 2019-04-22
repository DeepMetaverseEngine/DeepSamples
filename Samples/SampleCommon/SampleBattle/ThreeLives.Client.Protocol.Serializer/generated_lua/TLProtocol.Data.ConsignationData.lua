
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.ConsignationData


local _M = {MessageID = 0x00040064,Name = 'TLProtocol.Data.ConsignationData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00040064] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.ConsignationData'] = _M

function _M.Write(output,data)
		
	output:PutList(data.QuestMap, output.PutOBJ,'TLProtocol.Data.ConsignationInfoData')
end


function _M.Read(input,data)
		
	data.QuestMap = input:GetList(input.GetOBJ,'TLProtocol.Data.ConsignationInfoData')
end

