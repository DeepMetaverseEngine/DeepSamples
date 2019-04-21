@SET SolutionDir=%~dp0..\
@SET TargetDir=%~dp0..\ThreeLives_CL\Assets\Pro Standard Assets\Core
@ECHO %TargetDir%
@echo ------------------------------------------------------------
@echo --copy files------------------------------------------------
@rd /s/q "%TargetDir%"
@echo --clean files-----------------------------------------------
@echo --start copy file-------------------------------------------
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore"                                    "%TargetDir%\DeepCore"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore.GameData"                           "%TargetDir%\DeepCore.GameData"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore.GameHost"                           "%TargetDir%\DeepCore.GameHost"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore.GameHost.Local"                     "%TargetDir%\DeepCore.GameData.Local"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore.GameHost.Server"                    "%TargetDir%\DeepCore.GameHost.Server"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore.GameSlave"                          "%TargetDir%\DeepCore.GameSlave"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore.GUI"                                "%TargetDir%\DeepCore.GUI"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore.SharpZipLib"                        "%TargetDir%\DeepCore.SharpZipLib"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore.Unity3D"                            "%TargetDir%\DeepCore.Unity3D"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepCore\DeepCore.Template.MoonSharp"                 "%TargetDir%\DeepCore.Template.MoonSharp"

@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepMMO\DeepMMO"                                      "%TargetDir%\DeepMMO"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%DeepMMO\DeepMMO.Client"                               "%TargetDir%\DeepMMO.Client"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%ThreeLives_SL\ThreeLives.Battle.Data"		 			"%TargetDir%\ThreeLives.Battle.Data"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%ThreeLives_SL\ThreeLives.Battle.Host"		 			"%TargetDir%\ThreeLives.Battle.Host"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%ThreeLives_SL\ThreeLives.Battle.Slave"                "%TargetDir%\ThreeLives.Battle.Slave"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%ThreeLives_SL\ThreeLives.Client.Common"               "%TargetDir%\ThreeLives.Client.Common"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%ThreeLives_SL\ThreeLives.Client.Protocol"             "%TargetDir%\ThreeLives.Client.Protocol"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%ThreeLives_SL\ThreeLives.Client.Protocol.Serializer"  "%TargetDir%\ThreeLives.Client.Protocol.Serializer"

@echo --------copy skill-----------------------------------------------------
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%ThreeLives_PL\ThreeLivesPlugin\ThreeLives.Battle.Skill"  "%TargetDir%\ThreeLives.Battle.Skill"
@xcopy /Y /Q /e /i /EXCLUDE:copy_client_filter.txt  "%SolutionDir%ThreeLives_SL\ThreeLives.Client.Unity3D"  		"%TargetDir%\ThreeLives.Client.Unity3D"


@echo -------------------------------------------------------------
@echo - Done
@echo -------------------------------------------------------------
@pause