
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.MasterIdListData


local _M = {MessageID = 0x000400C8,Name = 'TLProtocol.Data.MasterIdListData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x000400C8] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.MasterIdListData'] = _M

function _M.Write(output,data)
		
	output:PutList(data.masterDataMap, output.PutOBJ,'TLProtocol.Data.MasterIdData')
	output:PutList(data.battleRecordList, output.PutOBJ,'TLProtocol.Data.MasterIdBattleInfoData')
	output:PutDateTime(data.challengeCD)
	output:PutDateTime(data.renameCD)
	output:PutDateTime(data.AppointCD)
	output:PutDateTime(data.InviteCD)
	output:PutS32(data.curMasterId)
	output:PutUTF(data.QinXinName)
end


function _M.Read(input,data)
		
	data.masterDataMap = input:GetList(input.GetOBJ,'TLProtocol.Data.MasterIdData')
	data.battleRecordList = input:GetList(input.GetOBJ,'TLProtocol.Data.MasterIdBattleInfoData')
	data.challengeCD = input:GetDateTime()
	data.renameCD = input:GetDateTime()
	data.AppointCD = input:GetDateTime()
	data.InviteCD = input:GetDateTime()
	data.curMasterId = input:GetS32()
	data.QinXinName = input:GetUTF()
end


