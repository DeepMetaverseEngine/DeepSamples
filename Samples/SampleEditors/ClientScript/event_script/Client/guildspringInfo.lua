function main(args)
	print('-----------guildspringinfo Start----------')

	--创建UI
    ui = Api.UI.Open('xml/hud/ui_hud_hotspring.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    Api.UI.Listen.MenuExit(ui, function()
        Api.Task.StopEvent(ID, false, 'MenuExit')
    end)
    Api.UI.SetFrameEnable(ui, false)

    --初始化时间
    --刷新时间显示
    local function RefreshTime()
        local leftTime = Api.FunctionTimeLeftSec('enterspring')
	    if lb_timenum then
	        if leftTime < 0 then
	            leftTime = 0
	        end
            local str = Api.FormatSecondStr(leftTime, 'HH:mm:ss')
            Api.UI.SetText(lb_timenum, str)
	    end
    end
    --刷新其他显示数据
    local function RefreshShowInfo( info )
	    local lb_playernum = Api.UI.FindChild(ui, 'lb_playernum')
	    Api.UI.SetText(lb_playernum, info.playerCount)
	    local lb_additionnum = Api.UI.FindChild(ui, 'lb_additionnum')
	    Api.UI.SetText(lb_additionnum, info.additionExp..'%')

	    lb_timenum = Api.UI.FindChild(ui, 'lb_timenum')
	    RefreshTime()
    end

    --首次填充UI数据
    RefreshShowInfo(args)

    --监听服务端数据刷新消息
    Api.Listen.Message('guildspring.info', function(ename, info)
        pprint('-----------------guildspring.info sync', info)
        RefreshShowInfo(info)
    end)

    --轮询刷新剩余时间
    local function StartPolling( ... )
	    while true do
	    	RefreshTime()
	        Api.Task.Sleep(1)
	        -- leftTime = leftTime - 1
	    end
    end

    Api.Task.AddEvent(StartPolling)

    Api.Task.Wait(Api.Listen.Message('guildspring.end'))
    print('--------------------end-----------------------')
end

function clean()
	Api.UI.Close(ui)
end