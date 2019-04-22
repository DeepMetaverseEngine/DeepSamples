
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.ClientPartnerData


local _M = {MessageID = 0x00088001,Name = 'TLProtocol.Data.ClientPartnerData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00088001] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.ClientPartnerData'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.ID)
	output:PutUTF(data.Name)
	output:PutS32(data.Lv)
	output:PutS32(data.Qualification)
	output:PutS32(data.QLv)
	output:PutArray(data.EquipLv, output.PutS32,'int')
	output:PutU64(data.CurEXP)
	output:PutU64(data.NeedEXP)
	output:PutS32(data.FightPower)
	output:PutEnum8(data.CurStatus)
	output:PutS64(data.RebirthTimeStamp)
	output:PutOBJ(data.BattleProp,'TLProtocol.Data.ClientPartnerBattleProp')
	output:PutMap(data.SkillData, output.PutS32, output.PutOBJ,'int', 'TLProtocol.Data.ClientPartnerSkillData')
	output:PutS32(data.SkillSlotCount)
end


function _M.Read(input,data)
		
	data.ID = input:GetS32()
	data.Name = input:GetUTF()
	data.Lv = input:GetS32()
	data.Qualification = input:GetS32()
	data.QLv = input:GetS32()
	data.EquipLv = input:GetArray(input.GetS32,'int')
	data.CurEXP = input:GetU64()
	data.NeedEXP = input:GetU64()
	data.FightPower = input:GetS32()
	data.CurStatus = input:GetEnum8()
	data.RebirthTimeStamp = input:GetS64()
	data.BattleProp = input:GetOBJ('TLProtocol.Data.ClientPartnerBattleProp')
	data.SkillData = input:GetMap(input.GetS32, input.GetOBJ,'int', 'TLProtocol.Data.ClientPartnerSkillData')
	data.SkillSlotCount = input:GetS32()
end


