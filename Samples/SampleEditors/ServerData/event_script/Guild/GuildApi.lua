local Task = {}
local GuildApi = {}
local Listen = {}
GuildApi.Task = Task
GuildApi.Listen = Listen

local guild_count = {}
local guild_wall_count = {}
local CarriageMapID = 501000
local MembersLimit = 30
local WallMembersLimit = 25
local LimitKickSec = 120
local guild_limist_uuid = {}

function GuildApi.Task.EnterCarriageZone(guild_uuid, role_uuid)
    local roomkey, force = Api.GetCarriageZoneInfo(guild_uuid)
    if not roomkey then
        return Api.Task.FromResult(false)
    end
    local info = {
        MapTemplateID = CarriageMapID,
        RoomKey = roomkey,
        Force = force
    }
    guild_count[guild_uuid] = guild_count[guild_uuid] or 0
    if guild_count[guild_uuid] >= MembersLimit then
        return Api.Task.FromResult(true, 'CountLimit')
    end
    local t = guild_limist_uuid[role_uuid]
    if t and os.time() - t < LimitKickSec then
        return Api.Task.FromResult(true, 'KickLimit', LimitKickSec - (os.time() - t))
    end
    local PlayerApi = Api.GetPlayerApi(role_uuid)
    local id = PlayerApi.Task.TransportPlayer(info)
    return Api.Task.ContinueWith(
        id,
        function()
            if guild_count then
                guild_count[guild_uuid] = guild_count[guild_uuid] + 1
            end
        end
    )
end

function GuildApi.Task.EnterWallZone(guild_uuid, role_uuid, mapid)
    local roomkey, force = Api.GetWallZoneInfo(guild_uuid, mapid)
    if not roomkey then
        return Api.Task.FromResult(false)
    end
    local info = {
        MapTemplateID = mapid,
        RoomKey = roomkey,
        Force = force
    }
    guild_wall_count[guild_uuid] = guild_wall_count[guild_uuid] or {}
    guild_wall_count[guild_uuid][mapid] = guild_wall_count[guild_uuid][mapid] or 0

    if guild_wall_count[guild_uuid][mapid] >= MembersLimit then
        return Api.Task.FromResult(true, 'CountLimit')
    end
    local t = guild_limist_uuid[role_uuid]
    if t and os.time() - t < LimitKickSec then
        return Api.Task.FromResult(true, 'KickLimit', LimitKickSec - (os.time() - t))
    end
    local PlayerApi = Api.GetPlayerApi(role_uuid)
    local id = PlayerApi.Task.TransportPlayer(info)
    return Api.Task.ContinueWith(
        id,
        function()
            guild_wall_count[guild_uuid][mapid] = guild_wall_count[guild_uuid][mapid] + 1
        end
    )
end

function GuildApi.NotifyLeaveWallZone(guild_uuid, role_uuid, mapid)
    local count = guild_wall_count[guild_uuid][mapid]
    if count then
        count = count - 1
        if count <= 0 then
            guild_wall_count[guild_uuid][mapid] = nil
        else
            guild_wall_count[guild_uuid][mapid] = count
        end
    end
end

function GuildApi.NotifyLeaveCarriageZone(guild_uuid, role_uuid, is_kick)
    local count = guild_count[guild_uuid]
    if count then
        count = count - 1
        if count <= 0 then
            guild_count[guild_uuid] = nil
        else
            guild_count[guild_uuid] = count
        end
        if is_kick then
            guild_limist_uuid[role_uuid] = os.time()
        end
    end
end

function GuildApi.SetCarriageGameOver(guild_uuids, win_uuid)
    for _, v in ipairs(guild_uuids) do
        guild_count[v] = nil
    end
    Api.CarriageReward(guild_uuids, win_uuid)
end

local wall_zones = {}

function GuildApi.RegisterWallZone(uuid1, uuid2, f1, f2, mapid, mapUUID)
    local function NotifyWallZoneInfo(info)
        for k, v in pairs(info.map) do
            Api.SendMessage('Zone', v, 'guildwall-init', info)
        end
    end
    for i, v in ipairs(wall_zones) do
        if (uuid1 == v.uuid[1] and uuid2 == v.uuid[2]) or (uuid1 == v.uuid[2] and uuid2 == v.uuid[1]) then
            v.map[mapid] = mapUUID
            NotifyWallZoneInfo(v)
            return
        end
    end
    table.insert(wall_zones, {uuid = {uuid1, uuid2}, map = {[mapid] = mapUUID}, force = {f1, f2}})
end

function GuildApi.WallGameOver(win_guild, samescore)
    for i, v in ipairs(wall_zones) do
        if win_guild == v.uuid[1] or win_guild == v.uuid[2] then
            table.remove(wall_zones, i)
            break
        end
    end
     return Api.FortReward(win_guild, samescore)
end

function GuildApi.GetPlayerWallZoneInfo(guild_uuid, uuid)
    for i, v in ipairs(wall_zones) do
        if guild_uuid == v.uuid[1] or guild_uuid == v.uuid[2] then
            return v
        end
    end
end

return GuildApi
