local CarID
local Carriage_Force = 3
function clean()
    if CarID then
        Api.RemoveObject(CarID)
    end
end

function main(ele, params)
    assert(ele)
    assert(arg.PlayerUUID)
    params = params or {}
    local currentFlagIndex = params.flag_index or 0
    local targetWays
    local targetFlag
    local out_event_id
    local last_car_x, last_car_y
    local last_player_x, last_player_y
    local seek_x, seek_y = 0, 0

    local function GetNextPosition(cx, cy)
        currentFlagIndex = currentFlagIndex + 1
        targetFlag = ele.point[currentFlagIndex]

        if string.IsNullOrEmpty(targetFlag) then
            Api.SendMessageToClient(arg.PlayerUUID,'carriage_success')
            Api.Task.StopEvent(ID, true)
            return false
        else
            local targetX, targetY = Api.GetFlagPosition(targetFlag)
            targetWays = Api.FindPath(cx, cy, targetX, targetY)
            return true
        end
    end

    --添加镖车
    local x, y = Api.GetFlagPosition(ele.npcflag)
    x = params.car_x or x
    y = params.car_y or y

    local info = {TemplateID = ele.monsterid, Force = Carriage_Force, X = x, Y = y}
    CarID = Api.AddUnit(info)
    assert(CarID)

    Api.Listen.UnitDamage(
        CarID,
        function()
            if Api.IsPlayerExist(arg.PlayerUUID) then
                -- 设置玩家pvp状态以限制切线
                Api.SetPlayerToPvpState(arg.PlayerUUID)
            end
        end
    )

    Api.Listen.Message(
        'carriage_line.' .. arg.PlayerUUID,
        function()
            local pre_flagindex = currentFlagIndex > 0 and currentFlagIndex - 1 or 0
            local ret = {
                IsLineStop = true,
                hp = Api.GetUnitHp(CarID),
                car_x = last_car_x,
                car_y = last_car_y,
                x = last_player_x,
                y = last_player_y,
                flag_index = pre_flagindex
            }
            Api.SetEventOutput(ID, ret)
            Api.Task.StopEvent(ID, true)
        end
    )

    local function PlayerEntered()
        -- print('player entered')
        local birth_x, birth_y = Api.GetFlagPosition(ele.appear_point)
        assert(birth_x and birth_y)
        birth_x = params.x or birth_x
        birth_y = params.y or birth_y
        last_player_x, last_player_y = birth_x, birth_y
        Api.SetPlayerForce(arg.PlayerUUID, Carriage_Force)
        Api.SetPlayerPosition(arg.PlayerUUID, birth_x, birth_y)
        Api.Task.StartEventByKey('client.carriage', Api.GetNextArg(arg), CarID)
        params = {}
    end

    Api.Listen.PlayerEnterZone(arg.PlayerUUID, PlayerEntered)
    --公会增（减）益
    local guildID = Api.GetPlayerGuildUUID(arg.PlayerUUID)
    if not string.IsNullOrEmpty(guildID) then
        local GuildApi = Api.GetGuildApi(arg)
        local stablelv = GuildApi.GetBuildLevel(guildID, 'Stable')
        local destroyCount = GuildApi.GetDestroyCount(guildID, 1)
        local maxhp = Api.GetUnitMaxHp(CarID)
        local speed = Api.GetUnitMoveSpeed(CarID)
        local stableInfo = Api.FindFirstExcelData('guild/guild.xlsx/guild_stable', {level = stablelv})
        if destroyCount > 0 then
            local destroyInfo = Api.FindFirstExcelData('guild/guild_destroy.xlsx/guild_destroy_debuff', 1)
            destroyCount = math.min(destroyCount, destroyInfo.destroy_limit)
            stableInfo.add_maxhp = stableInfo.add_maxhp - destroyCount * destroyInfo.carriage_hpdown
            stableInfo.add_runspeed = stableInfo.add_runspeed - destroyCount * destroyInfo.carriage_speeddown
            stableInfo.destroy_buff_id = destroyInfo.buff_id
        end
        if stableInfo.buff_id ~= 0 then
            Api.UnitAddBuff(CarID, stableInfo.buff_id)
        end
        if stableInfo.destroy_buff_id ~= 0 then
            Api.UnitAddBuff(CarID, stableInfo.destroy_buff_id)
        end
        maxhp = maxhp + math.floor(maxhp * (stableInfo.add_maxhp / 10000))
        speed = speed + speed * (stableInfo.add_runspeed / 10000)
        Api.SetUnitMaxHp(CarID, maxhp)
        Api.SetUnitMoveSpeed(CarID, speed)
    end

    if params.hp then
        Api.SetUnitHp(CarID, params.hp)
    end
    PlayerEntered()
    GetNextPosition(x, y)

    local function CheckPostion()
        if not CarID or not Api.IsPlayerExist(arg.PlayerUUID) or not Api.IsExistObject(CarID) then
            return
        end
        local px, py = Api.GetPlayerPosition(arg.PlayerUUID)
        local nx, ny = Api.GetObjectPosition(CarID)
        last_player_x, last_player_y = px, py
        if nx and ny then
            local seekdis = Api.GetDistance(seek_x, seek_y, nx, ny)
            if seekdis > 5 then
                seek_x, seek_y = nx, ny
                Api.Task.AddEventTo(
                    ID,
                    function()
                        Api.TrySetQuestMove(arg, ele.auto_move, ele.mapid, {x = seek_x, y = seek_y})
                    end
                )
            end
            last_car_x, last_car_y = nx, ny
        end
        local d = Api.GetDistance(px, py, nx, ny)
        if d > ele.outer then
            Api.UnitStopMove(CarID)
            if not out_event_id and not string.IsNullOrEmpty(ele.outerevent) then
                out_event_id = Api.Task.AddEventToByKey(ID, ele.outerevent, Api.GetNextArg(arg))
            end
        else
            if out_event_id then
                Api.StopEvent(out_event_id)
                out_event_id = nil
            end
            local ok = Api.UnitMoveTo(CarID, targetWays[1].x, targetWays[1].y)
            if ok then
                table.remove(targetWays, 1)
                if #targetWays == 0 and GetNextPosition(nx, ny) then
                    CheckPostion()
                end
            end
        end
    end

    Api.Listen.AddPeriodicSec(0.5, CheckPostion)
    Api.Task.Wait(Api.Listen.ObjectRemove(CarID))
    --掉宝箱
    local params = {
        x = last_car_x,
        y = last_car_y,
        force = 2
    }
    for i, v in ipairs(ele.item_event) do
        if not string.IsNullOrEmpty(v) then
            local randompos = Api.RandomPosition(last_car_x, last_car_y, 3, 1)
            Api.Task.StartEventByKey(v, Api.GetNextArg(arg), {x = randompos[1].x, y = randompos[1].y, force = 2})
        end
    end
    return false, 'ObjectRemove'
end
