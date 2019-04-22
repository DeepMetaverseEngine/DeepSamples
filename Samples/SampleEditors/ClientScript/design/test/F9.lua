local Api = EventApi
function main()
	--api.Unit.RemoveLastAddUnit()
	Api.FollowSelectUnit()

	-- Api.Task.StartEvent(function()
	-- 	Api.ChapterZeroStart()
	-- 	Api.Task.PlayCG('dungen100000_5')
	-- 	Api.Task.Sleep(37 - 3)
	-- 	Api.QuickTransport(Api.GetExcelConfig('scene_defaultbirth_next'))
	-- 	--Api.ChapterZeroEnd()
	-- end)


	-- GlobalHooks.UI.OpenUI('GuildFortInfo', 0)
	EventManager.Fire('Event.System.Back', {})
end
