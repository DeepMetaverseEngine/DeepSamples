function main()
 --   api.PlayCG("tasksp1001")
  --  api.CanSkipCG(true)
--	api.BlackScreen(false)
	 local step = api.GetStep()
	 if not step then
	 	api.SendStep("chapter_1")

		api.FireEvent("Event.Scene.Chapter1",{})
	 --***黑屏api
--	 	api.CanSkipCG(true)
		--api.PlayCG("dungen210130_1")
--		api.Wait(api.WaitPlayCG("map100010_2"))
		api.BlackScreen(false)
		api.Wait(api.ShowChapter(1))
		api.StartClientScript("Client/Guide/guide_quest1046")	

--		api.CanSkipCG(true)
	end
	api.BlackScreen(false)
end