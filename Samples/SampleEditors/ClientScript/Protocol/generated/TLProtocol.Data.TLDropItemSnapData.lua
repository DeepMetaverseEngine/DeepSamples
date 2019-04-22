
-- Warning: do not edit this file.
-- 警告: 不要编辑此文件

-- ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
-- TLProtocol.Data.TLDropItemSnapData


local _M = {MessageID = 0x00068601,Name = 'TLProtocol.Data.TLDropItemSnapData'}
_M.__index = _M
function _M.IsSuccess(self)
	return self.s2c_code == 200
end
Protocol.Serializer[0x00068601] = _M
Protocol.Serializer.StringDefined['TLProtocol.Data.TLDropItemSnapData'] = _M

function _M.Write(output,data)
		
	output:PutS32(data.TemplateID)
	output:PutS64(data.Qty)
	output:PutS32(data.itemType)
	output:PutS32(data.pileNum)
	output:PutS32(data.pileMax)
	output:PutS32(data.system_notice)
end


function _M.Read(input,data)
		
	data.TemplateID = input:GetS32()
	data.Qty = input:GetS64()
	data.itemType = input:GetS32()
	data.pileNum = input:GetS32()
	data.pileMax = input:GetS32()
	data.system_notice = input:GetS32()
end


