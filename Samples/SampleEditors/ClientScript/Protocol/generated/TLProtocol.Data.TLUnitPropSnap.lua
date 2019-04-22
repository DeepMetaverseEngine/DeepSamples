
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.TLUnitPropSnap


local _M = {MessageID = 0x00062009,Name = 'TLProtocol.Data.TLUnitPropSnap'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00062009] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.TLUnitPropSnap'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.CurAnger)
	output:PutS32(data.CurHP)
	output:PutS32(data.MaxHP)
	output:PutS32(data.Attack)
	output:PutS32(data.Defend)
	output:PutS32(data.Mdef)
	output:PutS32(data.Through)
	output:PutS32(data.Block)
	output:PutS32(data.Hit)
	output:PutS32(data.Dodge)
	output:PutS32(data.Crit)
	output:PutS32(data.ResCrit)
	output:PutS32(data.CriDamagePer)
	output:PutS32(data.RedCriDamagePer)
	output:PutS32(data.RunSpeed)
	output:PutS32(data.AutoRecoverHp)
	output:PutS32(data.TotalReduceDamagePer)
	output:PutS32(data.TotalDamagePer)
	output:PutS32(data.FireDamage)
	output:PutS32(data.ThunderDamage)
	output:PutS32(data.SoilDamage)
	output:PutS32(data.IceDamage)
	output:PutS32(data.WindDamage)
	output:PutS32(data.FireResist)
	output:PutS32(data.ThunderResist)
	output:PutS32(data.SoilResist)
	output:PutS32(data.IceResist)
	output:PutS32(data.WindResist)
	output:PutS32(data.AllelementDamage)
	output:PutS32(data.AllelementResist)
	output:PutS32(data.OnHitRecoverHP)
	output:PutS32(data.KillRecoverHP)
	output:PutS32(data.ExtraGoldPer)
	output:PutS32(data.ExtraEXPPer)
	output:PutS32(data.GodDamage)
	output:PutS32(data.TargetToMonster)
	output:PutS32(data.TargetToPlayer)
	output:PutS32(data.DefReduction)
	output:PutS32(data.MDefReduction)
	output:PutS32(data.Extracrit)
end


function _M.Read(input,data)
		
	data.CurAnger = input:GetS32()
	data.CurHP = input:GetS32()
	data.MaxHP = input:GetS32()
	data.Attack = input:GetS32()
	data.Defend = input:GetS32()
	data.Mdef = input:GetS32()
	data.Through = input:GetS32()
	data.Block = input:GetS32()
	data.Hit = input:GetS32()
	data.Dodge = input:GetS32()
	data.Crit = input:GetS32()
	data.ResCrit = input:GetS32()
	data.CriDamagePer = input:GetS32()
	data.RedCriDamagePer = input:GetS32()
	data.RunSpeed = input:GetS32()
	data.AutoRecoverHp = input:GetS32()
	data.TotalReduceDamagePer = input:GetS32()
	data.TotalDamagePer = input:GetS32()
	data.FireDamage = input:GetS32()
	data.ThunderDamage = input:GetS32()
	data.SoilDamage = input:GetS32()
	data.IceDamage = input:GetS32()
	data.WindDamage = input:GetS32()
	data.FireResist = input:GetS32()
	data.ThunderResist = input:GetS32()
	data.SoilResist = input:GetS32()
	data.IceResist = input:GetS32()
	data.WindResist = input:GetS32()
	data.AllelementDamage = input:GetS32()
	data.AllelementResist = input:GetS32()
	data.OnHitRecoverHP = input:GetS32()
	data.KillRecoverHP = input:GetS32()
	data.ExtraGoldPer = input:GetS32()
	data.ExtraEXPPer = input:GetS32()
	data.GodDamage = input:GetS32()
	data.TargetToMonster = input:GetS32()
	data.TargetToPlayer = input:GetS32()
	data.DefReduction = input:GetS32()
	data.MDefReduction = input:GetS32()
	data.Extracrit = input:GetS32()
end


