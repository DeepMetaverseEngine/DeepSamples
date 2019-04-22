function main()
 --   api.PlayCG("tasksp1001")
  --  api.CanSkipCG(true)
	 local step = api.GetStep()
	 if not step then
	 	api.SendStep("tasksp1001")
	 --***黑屏api
	 	api.CanSkipCG(true)
		api.PlayCG("tasksp1001")
--		api.CanSkipCG(true)
		
	end
api.BlackScreen(false)
end