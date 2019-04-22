local _M = {}
_M.__index = _M



--获取小于当前人物等级的数据
local function GetAllStrongData()

	local temp=GlobalHooks.DB.GetFullTable('PowerBook')
	local limitlv = DataMgr.Instance.UserData.Level

	--把符合条件的数据放在表里，排序之后返回出去
    local allStrongData={}
    for i,v in ipairs(temp) do
    	if v.open_lv <=limitlv then 
    		table.insert(allStrongData,v)
    	end
    end

   	table.sort(allStrongData,function(a,b) return a.sort<b.sort end)

	return allStrongData
end


--通过等级获取这个阶段的各个功能目标值
local function GetStrongLevel()

	local temp=GlobalHooks.DB.GetFullTable('PowerLevel')
    local limitlv = DataMgr.Instance.UserData.Level

    local strongLevelData={}

    for i,v in ipairs(temp) do
    	if limitlv >= v.level_min and limitlv <= v.level_max then 
    		strongLevelData=temp[i]
    	end
    end

	return strongLevelData
end


--通过战力比值，返回美术字路径
local function CalculateProportion(num)

    local temp=GlobalHooks.DB.GetFullTable('PowerName')
    
    local proportion=num*1000
    for i,v in ipairs(temp) do
        if proportion>=v.proportion_min and proportion<v.proportion_max then 
            return v.art_word
        end
    end
    return temp[#temp].art_word
end


function _M.GetFunctionIdById(funcid)
    local temp = unpack(GlobalHooks.DB.Find('PowerBook',{id = funcid}))
    return temp
end

-----------------Net-------------------


--请求每个模块的战斗力
local function RequestModuleFight(cb)

  Protocol.RequestHandler.ClientGetModuleScoreRequest(request, function(rsp)
    if rsp:IsSuccess() and cb then
      cb(rsp)
    end
  end)
end


--获取变强宝典，返回符合条件的途径
local function GetStrongWay(cb)

    --返回出去的最终数据
    local p={}

    --获取当前开放的功能的数量
    local allStrongData=GetAllStrongData()

    --如果为0，代表没有功能开放
    if #allStrongData == 0 then 
        p=nil
        return p
    end

    --获取当前等级每个模块的目标值
    local strongLevelData=GetStrongLevel()

    --获取每个模块的比例以及icon
    local proportion={}
    RequestModuleFight(function(resp)

        --新建键值对表
        local moduleFight={}
        for i, v in ipairs(resp.scoremap) do
            local str = _M.GetFunctionIdById(i)
            moduleFight[str.function_id] = v
        end
        
        --添加每个模块的数据
        for i=1,#allStrongData do

            local x=0

            --遍历两张表，取键相同的值计算比例，确保统一使用function_id作为索引
            for k,v in pairs(strongLevelData) do
                for j,l in pairs(moduleFight) do
                    if k == j and k ==allStrongData[i].function_id then 
                         x= l/v
                    end
                end
            end

            table.insert(proportion,
           {tag=allStrongData[i].function_tag,
            back=allStrongData[i].function_background,
            fight=x,
            icon=allStrongData[i].icon_res})
        end

        --如果途径小于4，则全部返回
        if #proportion<=4 then
            p=proportion
        else--根据战力比值排序，返回前4低的
            table.sort(proportion,function(a,b) return a.fight<b.fight end)
            local prop={}
                for i=1,4 do
                    table.insert(prop,proportion[i])
                end
            p=prop
        end
            if cb ~=nil then 
                cb(p)
            end       
    end)
end


_M.GetStrongWay=GetStrongWay
_M.RequestModuleFight=RequestModuleFight
_M.CalculateProportion=CalculateProportion
_M.GetStrongLevel=GetStrongLevel
_M.GetAllStrongData=GetAllStrongData

return _M