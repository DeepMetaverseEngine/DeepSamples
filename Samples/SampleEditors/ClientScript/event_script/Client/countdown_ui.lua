function main(sec, format, pos, fadeoutms, ...)
	format = format or Constants.Text.format_countdown
	ui = Api.UI.Open('xml/tips/tips_countdown.gui.xml', UIShowType.Cover, {Layer = 'MessageBox', EnableFrame = false})
	Api.UI.Listen.MenuExit(
		ui,
		function()
			Api.Task.StopEvent(ID, false, 'MenuExit')
		end
	)
	-- Api.UI.SetFrameEnable(ui, false)
	local cvs_tipsbg = Api.UI.FindChild(ui, 'cvs_tipsbg')
	Api.UI.SetAlpha(cvs_tipsbg, 1)
	local tb_tips = Api.UI.FindChild(ui, 'tb_tips')
	Api.UI.SetTextAnchor(tb_tips, TextAnchor.L_C)

	local str = Api.GetText(format, sec, ...)
	Api.UI.SetXmlText(tb_tips, str)

	local lastw, lasth = Api.UI.GetSize(cvs_tipsbg)
	local pw, ph = Api.UI.GetTextPreferredSize(tb_tips)
	if ph > lasth then
		Api.UI.SetSize(tb_tips, pw, ph)
		pw = pw + 50
		ph = ph + 30
		Api.UI.SetSize(cvs_tipsbg, pw, ph)
		Api.UI.AdjustToCenter(cvs_tipsbg, tb_tips)
	else
		pw, ph = pw, lasth
		Api.UI.SetSize(tb_tips, pw, ph)
		pw = pw + 50
		Api.UI.SetSize(cvs_tipsbg, pw, ph)
		Api.UI.AdjustToCenter(cvs_tipsbg, tb_tips)
	end
	local root = Api.UI.GetRoot(cvs_tipsbg)
	local root_w = Api.UI.GetSize(root)
	Api.UI.SetPositionX(cvs_tipsbg, (root_w - pw) * 0.5)
	if pos then
		if pos.x then
			Api.UI.SetPositionX(cvs_tipsbg, pos.x)
		end
		if pos.y then
			Api.UI.SetPositionY(cvs_tipsbg, pos.y)
		end
	end

	while sec > 0 do
		Api.Task.Sleep(1)
		sec = sec - 1
		local str = Api.GetText(format, sec, ...)
		Api.UI.SetXmlText(tb_tips, str)
	end
	fadeoutms = fadeoutms or 1000
	Api.Task.Wait(Api.UI.Task.AlphaTo(cvs_tipsbg, 0, fadeoutms / 1000))
end

function clean()
	Api.UI.Close(ui)
end
