@SET ProjectDir=%1
@SET TargetDir=%2

@echo -----------------------------------------------------------------------------------
@echo - Game Editor
@echo -----------------------------------------------------------------------------------
@xcopy /Y %TargetDir%*.dll         %ProjectDir%..\..\SampleEditors\GameEditor\bin
@xcopy /Y %TargetDir%*.exe         %ProjectDir%..\..\SampleEditors\GameEditor\bin
@xcopy /Y %TargetDir%*.pdb         %ProjectDir%..\..\SampleEditors\GameEditor\bin

@echo -----------------------------------------------------------------------------------
@echo - UnityWorkspace
@echo -----------------------------------------------------------------------------------
@copy /Y %TargetDir%DeepCore.dll                      %ProjectDir%..\..\UnityWorkspace\Assets\Plugins
@copy /Y %TargetDir%DeepCore.GameData.dll             %ProjectDir%..\..\UnityWorkspace\Assets\Plugins
@copy /Y %TargetDir%DeepCore.GameSlave.dll            %ProjectDir%..\..\UnityWorkspace\Assets\Plugins
@copy /Y %TargetDir%DeepCore.GUI.dll                  %ProjectDir%..\..\UnityWorkspace\Assets\Plugins
@copy /Y %TargetDir%DeepCore.SharpZipLib.dll          %ProjectDir%..\..\UnityWorkspace\Assets\Plugins
@copy /Y %TargetDir%DeepMMO.dll                       %ProjectDir%..\..\UnityWorkspace\Assets\Plugins
@copy /Y %TargetDir%DeepMMO.Client.dll                %ProjectDir%..\..\UnityWorkspace\Assets\Plugins
@copy /Y %TargetDir%SampleBattle.dll                  %ProjectDir%..\..\UnityWorkspace\Assets\Plugins
@copy /Y %TargetDir%SampleRPG.dll                     %ProjectDir%..\..\UnityWorkspace\Assets\Plugins
@copy /Y %TargetDir%SampleRPG.Serializer.dll          %ProjectDir%..\..\UnityWorkspace\Assets\Plugins

@echo -----------------------------------------------------------------------------------
@SET SolutionDir=%ProjectDir%..\..\..\..\
@SET UnityWorkspace=%ProjectDir%..\..\UnityWorkspace\Assets\
@call %SolutionDir%DeepCore\_copyprj_u3d.bat %UnityWorkspace%

@echo -----------------------------------------------------------------------------------
@echo - Done
@echo -----------------------------------------------------------------------------------
