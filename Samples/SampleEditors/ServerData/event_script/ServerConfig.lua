return {
	--total config
	Managers = {
		Zone = {
			InitScript = 'Zone/Init',
			TestScript = 'Zone/ZoneHotTest',
			ApiList = {
				'event_script/CommonApi',
				'event_script/CommonServer/CommonServerCLRApi',
				'event_script/CommonServer/UtilApi',
				'event_script/Zone/ZoneApi',
				'event_script/Zone/ZoneCLRApi'
			}
		},
		Player = {
			InitScript = 'Player/Init',
			TestScript = 'Player/HotTest',
			ApiList = {
				'event_script/CommonApi',
				'event_script/CommonServer/CommonServerCLRApi',
				'event_script/CommonServer/UtilApi',
				'event_script/Player/PlayerApi',
				'event_script/Player/PlayerCLRApi'
			}
		},
		Guild = {
			ApiList = {
				'event_script/CommonApi',
				'event_script/CommonServer/CommonServerCLRApi',
				'event_script/CommonServer/UtilApi',
				'event_script/Guild/GuildApi',
				'event_script/Guild/GuildCLRApi'
			}
		},
		Arena = {
			ApiList = {
				'event_script/CommonApi',
				'event_script/CommonServer/CommonServerCLRApi',
				'event_script/CommonServer/UtilApi',
				'event_script/Arena/ArenaApi',
				'event_script/Arena/ArenaCLRApi'
			}
		},


		MasterRace = {
			InitScript = 'MasterRace/Init',
			ApiList = {
				'event_script/CommonApi',
				'event_script/CommonServer/CommonServerCLRApi',
				'event_script/CommonServer/UtilApi',
				'event_script/MasterRace/MasterRaceApi',
				'event_script/MasterRace/MasterRaceCLRApi'
			}
		},
		AreaManager = {
			InitScript = 'AreaManager/Init',
			TestScript = 'AreaManager/HotTest',
			ApiList = {
				'event_script/CommonApi',
				'event_script/CommonServer/UtilApi',
				'event_script/AreaManager/AreaManagerApi',
				'event_script/CommonServer/CommonServerCLRApi',
				'event_script/AreaManager/AreaManagerCLRApi'
			},
			GenNameSpaceApi = {
				{
					NameSpace = 'ThreeLives.Server.Events.Zone',
					FileName = 'event_script/Zone/ZoneCLRApi.lua',
					Group = 'Zone'
				},
				{
					NameSpace = 'ThreeLives.Server.Events.Logic',
					FileName = 'event_script/Player/PlayerCLRApi.lua',
					Group = 'Player'
				},
				{
					NameSpace = 'ThreeLives.Server.Events.AreaManager',
					FileName = 'event_script/AreaManager/AreaManagerCLRApi.lua',
					Group = 'AreaManager'
				},
				{
					NameSpace = 'ThreeLives.Server.Events.Guild',
					FileName = 'event_script/Guild/GuildCLRApi.lua',
					Group = 'Guild'
				},
				{
					NameSpace = 'ThreeLives.Server.Events.Arena',
					FileName = 'event_script/Arena/ArenaCLRApi.lua',
					Arena = 'Arena'
				},
				{
					NameSpace = 'ThreeLives.Server.Events.Common',
					FileName = 'event_script/CommonServer/CommonServerCLRApi.lua',
					Group = 'CommonServer'
				},
				{
					NameSpace = 'ThreeLives.Server.Events.MasterRace',
					FileName = 'event_script/MasterRace/MasterRaceCLRApi.lua',
					Group = 'MasterRace'
				},
			}
		}
	},
	ExcelRootPath = 'templates_lua/',
	ScriptRootPath = 'event_script/',
	SanboxAppendEnv = {
		VirtualItems = {
			Copper = 1,
			Silver = 2,
			Diamond = 3,
			Exp = 4,
			Soul = 5,
			Contribution = 6,
			Exploit = 7
		},
		QuestState = {
			Available = 1,
			Accepted = 2,
			Completed = 3,
			Failed = 4,
			Removed = 5,
		},
		IsServer = true,
	}
}
