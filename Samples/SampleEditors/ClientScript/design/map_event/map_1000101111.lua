function main()

     --************有步骤的时候判断步骤
	 -- local step = api.GetStep()
	 -- if step and step == "mmp" then
	 -- 	api.SendStep("mmp1")
	 -- 	api.PlayCG("tasksp1001")
	 --    	api.CanSkipCG(true)
     -- end
     -- ********************
 --   api.PlayCG("tasksp1001")
  --  api.CanSkipCG(true)
	 local step = api.GetStep()
	 if step and step == "map_100010" then
	 	api.SendStep("map_100010")
	 --***黑屏api
	 	api.CanSkipCG(true)
		api.PlayCG("map_100010")
--		api.CanSkipCG(true)
		
	end
api.BlackScreen(false)
end