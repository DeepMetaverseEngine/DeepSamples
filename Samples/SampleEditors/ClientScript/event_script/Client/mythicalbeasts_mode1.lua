function main()
	print('-----------mythicalbeasts Start----------')
 
    --创建UI
    ui = Api.UI.Open('xml/hud/ui_hud_beasts_one.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    --倒计时.
    local lb_counttime = Api.UI.FindChild(ui, 'lb_betime')
    --监听环境变量变更.
    Api.Listen.EnvironmentVar('count_down',function(value) 
      Api.UI.SetText(lb_counttime, Api.FormatSecondStr(value))
    end)

    Api.UI.Listen.MenuExit(ui, function()
        Api.Task.StopEvent(ID, false, 'MenuExit')
    end)
    
    --UI可穿透.
    Api.UI.SetFrameEnable(ui, false)
    
    --接收关闭事件.
    Api.Task.WaitAlways()
    print('--------------------end-----------------------')
end

function clean()
  print('----------------mythicalbeasts close-----------------------')
	Api.UI.Close(ui)
end