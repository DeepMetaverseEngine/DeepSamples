function main()
	print('-----------mythicalbeasts mode2 Start----------')
 
    --创建UI
    ui = Api.UI.Open('xml/hud/ui_hud_beasts.gui.xml', UIShowType.Cover, {Layer = 'Hud'})
    --倒计时.
    local lb_counttime = Api.UI.FindChild(ui, 'lb_betime')
    --击杀数量.
    local lb_killcount = Api.UI.FindChild(ui, 'lb_kill')
    Api.UI.SetText(lb_killcount, 0)
    ------------------------------------------------------------------------
    --监听环境变量变更.
    ------------------------------------------------------------------------
    --倒计时
    Api.Listen.EnvironmentVar('count_down',function(value) 
      Api.UI.SetText(lb_counttime, Api.FormatSecondStr(value))
    end)
    ------------------------------------------------------------------------
    --击杀怪物数量.
    Api.Listen.EnvironmentVar('killmonster',function(value) 
      Api.UI.SetText(lb_killcount, value)
    end)
    ------------------------------------------------------------------------
    --关闭监听.
    Api.UI.Listen.MenuExit(ui, function()
        Api.Task.StopEvent(ID, false, 'MenuExit')
    end)
    ------------------------------------------------------------------------
    --UI可穿透.
    Api.UI.SetFrameEnable(ui, false)
     ------------------------------------------------------------------------
    --接收关闭事件.
    Api.Task.WaitAlways()
     ------------------------------------------------------------------------
    print('-----------mythicalbeasts mode2 End----------')
end


function clean()
  print('----------------mythicalbeasts close-----------------------')
	Api.UI.Close(ui)
end