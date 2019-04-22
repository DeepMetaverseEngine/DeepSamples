local config = {
	--total config
	Managers = {
		Client = {
			InitScript = 'Client/ClientInit',
			ApiList = {
				'event_script/CommonApi',
				'event_script/Client/ClientApi',
				'event_script/Client/GuideApi',
				'event_script/Client/MainCLRApi',
				'event_script/Client/CGCLRApi',
			}
		}
	},
	ExcelRootPath = 'Data/',
	ScriptRootPath = 'event_script/',
	SanboxAppendEnv = {
		FillMethod = UnityEngine.UI.Image.FillMethod,
		Origin360 = UnityEngine.UI.Image.Origin360,
		Origin180 = UnityEngine.UI.Image.Origin180,
		Origin90 = UnityEngine.UI.Image.Origin90,
		OriginVertical = UnityEngine.UI.Image.OriginVertical,
		OriginHorizontal = UnityEngine.UI.Image.OriginHorizontal,
		Constraint = UnityEngine.UI.GridLayoutGroup.Constraint,
		IsWindowsEditor = UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsEditor,
		AvartarPart = Constants.AvatarPart,
		UIShowType = UIShowType,
		UIAnimeType = UIAnimeType,
		TextAnchor = CommonUI.TextAnchor,
		Constants = Constants,
		IsUseMpq = GameGlobal.Instance.useMpq,
		IsClient = true,
	}
}

if UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsEditor then
	print('start write CLRApi')
	config.Managers.Client.GenNameSpaceApi = {
		{
			NameSpace = 'EventScript.Client.Events',
			FileName = GameGlobal.Instance.LuaRootPath..'event_script/Client/MainCLRApi.lua',
			Group = 'Client'
		},
		{
			NameSpace = 'EventScript.Client.Events.CG',
			FileName = GameGlobal.Instance.LuaRootPath..'event_script/Client/CGCLRApi.lua',
			Group = 'Client'
		}
	}
end

return config
