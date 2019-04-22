local husband
local wife
local actorUUID
local wType
local cvs_fuben

function main(args)
	print('-----------wedding Start----------')
    pprint(args)
    wType = args.wType

    Api.Listen.ActorLeaveMap(
        function()
            Api.Task.StopEvent(ID)
        end
    )
    if wType == 'wedding' then
        husband = args.husband
        wife = args.wife

        actorUUID = Api.GetActorUUID()

        if actorUUID == husband or actorUUID == wife then
            Api.Task.Wait(Api.Listen.UnitInView(args.CarID))
                --Api.FollowUnit(args.CarID)
            Api.Camera.FollowTargetUnit(args.CarID)
            Api.SetActorEnableCtrl(false)
        end

        Api.Listen.UnitInView(args.husbandOId,function() 
            Api.SetPlayerVisible(husband, false)
        end)
        Api.Listen.UnitInView(args.wifeOId,function() 
            Api.SetPlayerVisible(wife, false)
        end)
        Api.Task.Wait(Api.Listen.Message('wedding.end'))
    else
        while true do
            Api.Task.Sleep(0.5)
            local ui = Api.UI.FindByXml('xml/fuben/ui_fuben_hud.gui.xml')
            if ui then
                cvs_fuben = Api.UI.FindChild(ui, 'cvs_fuben')
                if cvs_fuben then
                    Api.UI.SetVisible(cvs_fuben, false)
                end
                break
            end
        end
    end

    Api.Task.WaitAlways()
    print('--------------------end-----------------------')
end

function clean()
    if wType == 'wedding' then
        Api.SetPlayerVisible(husband, true)
        Api.SetPlayerVisible(wife, true)

        if actorUUID == husband or actorUUID == wife then
            Api.SetActorEnableCtrl(true)
            Api.Camera.FollowActor()
        end
    else
        if cvs_fuben then
            Api.UI.SetVisible(cvs_fuben, true)
        end
    end
end