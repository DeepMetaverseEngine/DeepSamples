@set DST=%~dp0\UnityWorkspace\Assets\StreamingAssets\ui_edit\uiedit.cfg
@set DST_DIR=%~dp0\UnityWorkspace\Assets\StreamingAssets\ui_edit
@cd ..\..\DeepCore\Library\lib
@echo %DST%
@call libs.bat
@java -client -Xmx1000m -cp ./*.jar;g2d_studio.jar com.g2d.studio.ui.edit.UIEdit %DST%
