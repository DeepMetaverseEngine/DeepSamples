local _M = {}
_M.__index = _M


--通过副本ID获取单条数据
local function GetDailyDungeonDataById(dungeonId)
	local dungeonData=unpack(GlobalHooks.DB.Find('DailyDungeonInfo',{id=dungeonId}))
    return dungeonData
end


--通过mapid获取当前奖励组所有奖励
local function GetDailyDungeonRewardByMapId(mapid,cb)
	local group=unpack(GlobalHooks.DB.Find('DailyDungeonInfo',{map_id=mapid}))
    local reward=GlobalHooks.DB.FindFirst('DailyDungeonReward',{group_id=group.group_id})
  	if cb then 
  		cb(reward,group.group_id)
  	end
end


--获取所有日常副本数据
local function GetAllDailyDungeonData(cb)
  local allDungeonData =  GlobalHooks.DB.GetFullTable('DailyDungeonInfo')
  table.sort(allDungeonData,function(a,b) return a.order<b.order end)
  return allDungeonData
end


--获取奖励系数
local function GetRewardBouns(groupid)
    local rewardAdd =unpack(GlobalHooks.DB.Find('DailyDungeonAddition',function (add)
            return add.group_id==groupid and add.level_min <= DataMgr.Instance.UserData.Level and DataMgr.Instance.UserData.Level<=add.level_max
    end))
    local bouns = rewardAdd.reward_item_add/10000
    return bouns
end


function _M.MatchCountByKey(key)
    local data=unpack(GlobalHooks.DB.Find('DailyDungeonInfo',{function_id = key}))
    return data.order
end

--通过奖励组和击杀数返回奖励
local function GetCountReward(groupid,count,cb)
    local countReward =unpack(GlobalHooks.DB.Find('DailyDungeonReward',function (add)
      return add.group_id==groupid and add.kill_min <= count and count<=add.kill_max
    end))
    if cb then 
      cb(countReward)
    end
end


--判断奖励是否改变
local function JudgeRewardChanged(groupid,count,index)

        local changeA =unpack(GlobalHooks.DB.Find('DailyDungeonReward',function (add)
            return add.group_id==groupid and add.kill_min <= count and count<=add.kill_max
        end))
        local a=changeA

        local changeB =unpack(GlobalHooks.DB.Find('DailyDungeonReward',function (add)
            return add.group_id==groupid and add.kill_min <= count-1 and count-1<=add.kill_max
        end))
        local b=changeB

        return a.reward.item.num[index] >b.reward.item.num[index]
end


--查询对应核心副本的可购买次数
local function InquiryCanBuyCount(key)
    local dungeon=unpack(GlobalHooks.DB.Find('DailyDungeonInfo',{function_id = key}))
    local eventkey=dungeon.buy_record
    local viplv=DataMgr.Instance.UserData.VipLv or 0
    local cost=unpack(GlobalHooks.DB.Find('vip_info',{vip_level = viplv}))
    for i = 1, #cost.effect.key do
        if cost.effect.key[i]==eventkey then
            return cost.effect.val[i]
        end
    end
end


--通过key查询购买消耗
local function InquiryCost(key)
    local dungeon=unpack(GlobalHooks.DB.Find('DailyDungeonInfo',{function_id = key}))
    local eventkey=dungeon.buy_record
    local cost=unpack(GlobalHooks.DB.Find('vip_cost',{event_key = eventkey}))
    return cost
end

----------------------Net----------------------


--获取每日副本进入次数
local function RequestDailyDungeonEnterCount (cb)
    local request = {}
      Protocol.RequestHandler.ClientGetDailyDungeonInfoRequest(request, function(rsp)
      if cb then
        cb(rsp)
      end
  end)
end


--购买每日副本次数
local function RequestBuyEnterCount(functionid,cb)
    local request = {c2s_dungeon_type=functionid}
    Protocol.RequestHandler.ClientBuyDailyDungeonTicketsRequest(request, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end


_M.InquiryCanBuyCount=InquiryCanBuyCount
_M.InquiryCost=InquiryCost
_M.RequestBuyEnterCount=RequestBuyEnterCount
_M.GetCountReward=GetCountReward
_M.JudgeRewardChanged=JudgeRewardChanged
_M.RequestDailyDungeonEnterCount=RequestDailyDungeonEnterCount
_M.GetDailyDungeonRewardByMapId=GetDailyDungeonRewardByMapId
_M.GetRewardBouns=GetRewardBouns
_M.GetAllDailyDungeonData=GetAllDailyDungeonData
_M.GetDailyDungeonDataById=GetDailyDungeonDataById

return _M