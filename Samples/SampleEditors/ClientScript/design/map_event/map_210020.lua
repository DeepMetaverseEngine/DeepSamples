function main()
 --   api.PlayCG("tasksp1001")
  --  api.CanSkipCG(true)
	 local step = api.GetStep()
	 if not step then
	 	api.SendStep("map_210020")
	 --***黑屏api
	 	api.CanSkipCG(true)
		api.PlayCG("map_210020")
--		api.CanSkipCG(true)
		
	end
api.BlackScreen(false)
end