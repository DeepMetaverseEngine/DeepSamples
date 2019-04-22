@SET SolutionDir=%1
@SET ProjectDir=%2
@SET TargetDir=%3
@xcopy /I/E/Y "%TargetDir%*.dll" "%ProjectDir%..\..\SampleEditors\GameEditor\bin\U3DTLScene\U3DSceneRun_Data\Managed"
@xcopy /I/E/Y "%TargetDir%*.dll" "%ProjectDir%..\..\SampleUnity\Assets\Plugins"
@set U3DEditor=%SolutionDir%DeepEditor\DeepEditor.Plugin.Unity3D\U3DScene\
@if exist "%U3DEditor%" @xcopy /I/E/Y "%U3DEditor%*" "%ProjectDir%..\..\SampleEditors\GameEditor\bin\U3DScene\"