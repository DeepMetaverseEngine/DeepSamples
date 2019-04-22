function main()
 --   api.PlayCG("tasksp1001")
  --  api.CanSkipCG(true)
	 local step = api.GetStep()
	 if not step then
	 	api.SendStep("dungen210010_1")
	 --***黑屏api
	 	api.CanSkipCG(true)
		api.PlayCG("dungen210010_1")
--		api.CanSkipCG(true)
		
	end
api.BlackScreen(false)
end