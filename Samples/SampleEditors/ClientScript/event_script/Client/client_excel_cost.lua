function main(ele)
    Api.Task.WaitActorReady()
    Api.UI.CloseAll()
    local xml = ele.ui_template or 'xml/common/common_cost.gui.xml'
    ui = Api.UI.Open(xml, UIShowType.HideHudAndMenu)
    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    local cvs_item = Api.UI.FindChild(ui, 'cvs_item')
    local lb_num = Api.UI.FindChild(ui, 'lb_num')
    local cvs_reward = Api.UI.FindChild(ui, 'cvs_reward')
    local btn_ok = Api.UI.FindChild(ui, 'btn_ok')
    local btn_close = Api.UI.FindChild(ui, 'btn_close')
    local x, y = Api.UI.GetPosition(cvs_reward)
    local w, h = Api.UI.GetSize(cvs_reward)
    local nextx = x
    local item_parent = cvs_reward
    for _, v in ipairs(ele.show.item) do
        if not item_parent then
            item_parent = Api.UI.Clone(cvs_reward)
            Api.UI.SetItemShow(item_parent, v, 1, true, {anchor = 'l_b', x = 385, y = 260})
            Api.UI.SetPositionX(item_parent, nextx)
            nextx = nextx + w + 10
        end
        item_parent = nil
    end
    local costs = Api.ParseCostAndCostGroup(ele)
    local curenough = costs[1].cur >= costs[1].need
    Api.UI.SetEnable(btn_ok, curenough)
    Api.UI.SetGray(btn_ok, not curenough)
    Api.UI.SetEnoughItemShowAndLabel(
        cvs_item,
        lb_num,
        costs[1],
        {
            x = 600,
            y = 150,
            cb = function(enough)
                Api.UI.SetEnable(btn_ok, enough)
                Api.UI.SetGray(btn_ok, not enough)
            end
        }
    )

    local id1 = Api.UI.Listen.TouchClick(btn_close)
    local id2 = Api.UI.Listen.TouchClick(btn_ok)
    local succes, successid = Api.Task.WaitSelect(id1, id2)
    -- print('successid ',successid,id1,id2)
    return successid == id2
end

function clean()
    Api.UI.Close(ui)
end
