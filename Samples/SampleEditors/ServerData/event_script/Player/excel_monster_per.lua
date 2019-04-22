
-- local function GetUnitInfo(ele)
--     local mapID = Api.GetMapTemplateID()
--     local ZoneApi = Api.GetZoneApi(Api.GetZoneUUID())
--     local rule = Api.FindFirstExcelData('event/mirrorrule.xlsx/mirrorrule', ele.mirrorrule)
--     pprint('rule -------',rule)
--     assert(rule,'rule data is not exist')
--     for i,v in ipairs(rule.condition.type) do
--       if not string.IsNullOrEmpty(v) and string.starts(v, 'fid_') then
--         local _,fidname = unpack(string.split(v, '_'))
--         pprint("MirrorRule",fidname)
--         if fidname == "gotomasterrace" then
--             --print("MirrorRule",v.arg1,v.arg2,v.arg3)
--             local MasterRaceApi = Api.GetMasterRaceApi(arg.PlayerUUID)
--             local GroupId = Api.GetPlayerServerGroup(arg.PlayerUUID)
--             local arg1 = rule.condition.arg1[i]
--             local arg2 = rule.condition.arg2[i]
--             local arg3 = rule.condition.arg3[i]
--             local enemyuuid = MasterRaceApi.GetMasterRaceRoleId(GroupId,arg1,arg2,arg3)
--             --print("enemyuuid",enemyuuid)
--             if not string.IsNullOrEmpty(enemyuuid) then
--                 local id =  Api.Task.GetPlayerMirrorSnap(enemyuuid)
--                 local success,ret = Api.Task.Wait(id)
--                 if success then
--                   return ret
--                 else
--                  assert(success,"error load playsnap") 
--                 end
--             else 
--                local maplv = ZoneApi.GetMapLv(arg.PlayerUUID)
--                local monstertemplateId = ele.monsterid[1]
--                return Api.GetMonsterMirror(monstertemplateId,mapID,maplv)

--             end
--         end
--       end
--     end
--     return nil
-- end


local function GetMirrorInfo(ele,ZoneApi)
    local mapID = Api.GetMapTemplateID()
    local rule = Api.FindFirstExcelData('event/mirrorrule.xlsx/mirrorrule', ele.mirrorrule)
    --pprint('rule -------',rule)
    assert(rule,'rule data is not exist')
    for i,v in ipairs(rule.condition.type) do
      if not string.IsNullOrEmpty(v) and string.starts(v, 'fid_') then
        local _,fidname = unpack(string.split(v, '_'))
        --pprint("MirrorRule",fidname)
        if fidname == "gotomasterrace" then
            --print("MirrorRule",v.arg1,v.arg2,v.arg3)
            local MasterRaceApi = Api.GetMasterRaceApi(arg.PlayerUUID)
            local GroupId = Api.GetPlayerServerGroup(arg.PlayerUUID)
            local arg1 = rule.condition.arg1[i]
            local arg2 = rule.condition.arg2[i]
            local arg3 = rule.condition.arg3[i]
            local enemyuuid = MasterRaceApi.GetMasterRaceRoleId(GroupId,arg1,arg2,arg3)
            --print("enemyuuid",enemyuuid)
            if not string.IsNullOrEmpty(enemyuuid) then
                return Api.GetPlayerMirrorSnap(enemyuuid)
            else 
               local maplv = ZoneApi.GetMapLv(arg.PlayerUUID)
               if maplv == 0 then
                return nil
               end 
               local monstertemplateId = ele.monsterid[1]
               return Api.GetMonsterMirror(monstertemplateId,mapID,maplv)

            end
        end
      end
    end
    return nil
end

function main(ele)
    
    assert(ele)
    if ele.lefttime and ele.lefttime > 0 then
        Api.Task.AddEvent(
            function()
                Api.Task.Sleep(ele.lefttime / 1000)
                Api.Task.StopEvent(ID, false, 'timeout')
            end
        )
    end
    ::start::
    local mapID = Api.GetMapTemplateID()
    if ele.mapid ~= mapID then
        Api.Task.Wait(Api.Listen.EnterMap(ele.mapid))
    end

    local ZoneApi = Api.GetZoneApi(Api.GetZoneUUID())
    local mirrorinfo = nil
    if  ele.mirrorrule ~= 0 then
        mirrorinfo = GetMirrorInfo(ele,ZoneApi)
    end

    if ele.mapid ~= Api.GetMapTemplateID() then
        return false,'not in target map'
    end
    
    local id = ZoneApi.Task.AddEvent('Zone/point_pawn_unit', ele.id, arg.PlayerUUID,mirrorinfo)
    local ok, ret = Api.Task.Wait(id)
    if not ok and ret == 'Dispose' or ret == 'ReStart_ZoneEvent' then
        goto start
    else
        return ok, ret
    end
end
