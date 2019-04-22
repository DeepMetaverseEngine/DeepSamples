--test
local Helper = require'Logic/Helper'
local ItemModel = require 'Model/ItemModel'
local _M = {
    AttributeType = {
     maxhp = 1,
     attack = 2,
     defend = 3,
     mdef = 4,
     through = 5,
     block = 6,
     crit = 7,
     hit = 8
    }
}

function _M.GetPartnerPowerbyValue(data,needtrans)
  
    local AttrType ={ 
      'attack',
      'maxhp',
      'defend',
      'mdef',
      'thunderdamage',
      'winddamage',
      'icedamage',
      'firedamage',
      'soildamage',
      'thunderresist',
      'windresist',
      'iceresist',
      'fireresist',
      'soilresist',
    } 

   local _data = data
   if needtrans then
    _data = {}
    for i,v in ipairs(data) do
      _data[AttrType[i]] = v
    end
   end
   local attr = {}
   for i,v in ipairs(AttrType) do
      table.insert(attr,{Name = AttrType[i],Value = _data[v],ID = 0})
   end
   return ItemModel.CalcAttributesScore(attr)
end

function _M.GetPartnerPower(curRoleData)
  
   return _M.GetPartnerPowerbyValue(curRoleData)
end
function _M.GetPartnerItemGotoData(id)
    return GlobalHooks.DB.Find('ItemGotoData',id)
end

function _M.AttributeName(keyname)
  return ItemModel.GetAttributeString({Name = keyname,Value = 0})
end

function _M.GetAdvanceData(Roleid,lv)
  local data = unpack(GlobalHooks.DB.Find('FallenPartnerData',{god_id = Roleid,god_lv = lv}))
  return data
end

function _M.GetSkillData(skillId,lv)
  return unpack(GlobalHooks.DB.Find('FallenPartnerSkillData',{skill_id = skillId,skill_lv = lv}))
end


--检查仙侣是否可以升级  返回0表示否，1表示可以
function _M.CheckRedPoint(godid,godlv)
    if godlv >=250 then
        return 0
    end
    godlv=godlv+1
    local partner=_M.GetAdvanceData(godid,godlv)
    local itemcount= ItemModel.CountItemByTemplateID(partner.cost.id[1])
    if itemcount >=partner.cost.num[1] then
        return 1
    else
        return 0
    end
end


--通过仙侣id和等级，获取该仙侣技能总战力
function _M.GetSkillFightByPartnerId(godid,godlv)
    local partner= unpack(GlobalHooks.DB.Find('FallenPartnerListData',{god_id = godid}))
    local totalfight=0
    for i = 1, #partner.skill.id do
        local skillLv=unpack(GlobalHooks.DB.Find('FallenPartnerData',{god_id = godid,god_lv =godlv}))
        local skillfight = unpack(GlobalHooks.DB.Find('FallenPartnerSkillData',{skill_id = partner.skill.id[i],skill_lv =skillLv.client_rank}))
        totalfight=totalfight+ skillfight.skill_fight
    end
    return totalfight
end


function _M.GetItemCountByItemID(itemid)
  --return 0
  return ItemModel.CountItemByTemplateID(itemid)
end


function _M.SetNodeName(node,god_name,rank,god_quality)
      --品阶
      local _name = god_name
      node.Text = _name
      node.FontColor = GameUtil.RGB2Color(Constants.QualityColor[tonumber(god_quality)])
end


function _M.GetRankEffect(godid,rank)
      --品阶
      local data = unpack(GlobalHooks.DB.Find('FallenPartnerPreviewData',{god_id = godid,god_lv = rank}))
      if data == nil then
        error('GetRankEffect error')
        return nil
      end
      return data.model_effect
end

return _M
