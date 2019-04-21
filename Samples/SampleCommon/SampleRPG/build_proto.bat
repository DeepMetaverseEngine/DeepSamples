echo off
@SET ProjectDir=%1
@SET TargetDir=%2
@echo ---------------------------------------------------------------------------
@echo - GEN TLClient.Serializer
@echo ---------------------------------------------------------------------------
@set gen_ref=%TargetDir%DeepCore.dll
@set gen_ref=%gen_ref%;%TargetDir%DeepMMO.dll
@set gen_ref=%gen_ref%;%TargetDir%DeepCore.GameData.dll
@set gen_ref=%gen_ref%;%TargetDir%SampleRPG.dll
@del /Q %ProjectDir%..\SampleRPG.Serializer\generated\*.cs
@%TargetDir%codegen -ns:SampleRPG -wd:%TargetDir% -if:%gen_ref% -od:%ProjectDir%..\SampleRPG.Serializer\generated
@%TargetDir%codegen -ns:SampleRPG -wd:%TargetDir% -if:%gen_ref% -of:%ProjectDir%..\SampleRPG.Serializer\generated\codec.cs -t:csharp-code-ids.xml

