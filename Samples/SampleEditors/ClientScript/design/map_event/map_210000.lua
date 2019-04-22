function main()
 --   api.PlayCG("tasksp1001")
  --  api.CanSkipCG(true)
--	 local step = api.GetStep()
--	 if not step then
--	 	api.SendStep("dungen210000_7")
	 --***黑屏api
	 	--api.CanSkipCG(true)
		--api.PlayCG("dungen210000_2")
		--api.Sleep(0.5)
--		api.Wait(api.WaitPlayCG("dungen210000_7"))
		api.StartClientScript("Client/Guide/guide_autobattle")
--		api.CanSkipCG(true)
		
--	end
api.BlackScreen(false)
end