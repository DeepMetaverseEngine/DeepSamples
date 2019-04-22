function main(id)
    local ui = Api.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
    cvs_carriage = Api.UI.FindChild(ui, 'cvs_carriage')
    Api.Listen.ActorLeaveMap(
        function()
            Api.Task.StopEvent(ID)
        end
    )
    Api.UI.SetQuestHudVisible(false)
    Api.UI.SetVisible(cvs_carriage, true)
    local btn_back = Api.UI.FindChild(ui, 'btn_back')
    local btn_follow = Api.UI.FindChild(ui, 'btn_follow')
    Api.UI.SetVisible(btn_follow, true)
    Api.UI.SetVisible(btn_back, true)

    Api.UI.Listen.TouchClick(
        btn_back,
        function()
            Api.QuickTransport(100040)
        end
    )

    local function TrackCar()
        while true do
            if Api.IsFollowState() then
                break
            end
            if not Api.IsActorAutoRun() then
                break
            end

            Api.Task.Sleep(2)
        end
        Api.FollowUnit(id)
    end

    Api.UI.Listen.TouchClick(
        btn_follow,
        function()
            local ret = Api.FollowUnit(id)
            if not ret then
                local quest = Api.Quest.FindAcceptByField({sub_type = 2100})[1]
                if quest then
                    Api.Quest.SeekQuest(quest)
                end
                Api.Task.AddEventTo(ID, TrackCar)
            end
        end
    )
    Api.Task.Wait(Api.Listen.Message('carriage_success'))
    Api.UI.SetVisible(btn_follow, false)
    Api.UI.SetVisible(btn_back, true)
    Api.Task.WaitAlways()
end

function clean()
    Api.UI.SetVisible(cvs_carriage, false)
    Api.UI.SetQuestHudVisible(true)
end
