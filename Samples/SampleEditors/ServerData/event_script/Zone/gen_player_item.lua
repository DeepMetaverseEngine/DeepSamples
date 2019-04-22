function main(playerUUID, eleid, params)
    assert(playerUUID)
    local ele = Api.GetExcelByEventKey('item_per.' .. eleid)
    assert(ele.mapid == Api.GetMapTemplateID())
    local x, y, flag
    if params then
        x = params.x
        y = params.y
        flag = params.flag
    end

    if not x and not y and not flag then
        x, y = Api.GetExcelPostion(ele.flag, ele.coordinate)
    elseif flag then
        x, y = Api.GetFlagPosition(flag)
    end
    assert(x and y)
    local itemInfo = {
        TemplateID = ele.itemid,
        X = x,
        Y = y,
        Force = Api.GetPlayerForce(playerUUID)
    }
    itemID = Api.AddItem(itemInfo)
    if not itemID or itemID == 0 then
        return false, 'AddItem'
    end
    print('item added', itemInfo.TemplateID, x, y)
    Api.SetItemOwner(itemID, playerUUID)
    local CDing = false

    local function CD()
        CDing = true
        Api.Task.Sleep(1)
        CDing = false
    end

    Api.Listen.PlayerTryPickItem(
        itemID,
        function()
            return not CDing
        end
    )

    Api.Listen.PlayerLeaveZone(
        playerUUID,
        function()
            Api.Task.AddEventTo(ID,function()
                Api.Task.Sleep(2)
                Api.Task.StopEvent(ID, false, 'ReStart_ZoneEvent')
            end)
        end
    )

    Api.Task.Wait(
        Api.Listen.PlayerPickedItem(
            itemID,
            function(uuid)
                Api.TriggerEvent(ID, uuid)
                Api.Task.AddEventTo(ID, CD)
            end
        )
    )
end

function clean()
    if itemID then
        Api.RemoveObject(itemID)
    end
end
