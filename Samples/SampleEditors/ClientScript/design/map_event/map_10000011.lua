function main()
 --   api.PlayCG("tasksp1001")
  --  api.CanSkipCG(true)
 	api.PauseBGM()
	api.Wait(api.WaitPlayCG("dungen100000_1",false))
	api.StartClientScript("Client/Guide/guide_playermove")	

	api.ChangeBGM("dynamic/bgm/zhandou3")
--	api.StartClientScipt('Client/excel_message', 1)
--	end
end