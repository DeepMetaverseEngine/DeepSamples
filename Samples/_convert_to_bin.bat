@set DST=%~dp0\UnityWorkspace\Assets\StreamingAssets\ui_edit
@cd ..\..\Common\Library\Common
@echo %DST%
@CommonUI_Win32.exe xml2bin %DST%