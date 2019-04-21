
@set LIB=%~dp0\..\..\Common\Tools\lib
@set COM=%~dp0\..\..\Common\Library\Common
@cd UnityWorkspace

@if not exist HTTP @md HTTP
@cd HTTP
@if not exist res @md res
@if not exist mpq @md mpq

@echo ----------------------------------------------------------------
@echo - Copy Files
@echo ----------------------------------------------------------------
@xcopy /s /e /y "..\Assets\StreamingAssets\*.*"         .\res

@echo ----------------------------------------------------------------
@echo - CPJ to BIN
@echo ----------------------------------------------------------------
@java -classpath %LIB%/g2d_studio.jar -Xmx1024m  CellResourceXmlToBin  .\res  "%LIB%\convert_xml_filter.txt"  ""

@echo ----------------------------------------------------------------
@echo - UI Edit to BIN
@echo ----------------------------------------------------------------
@%COM%\CommonUI_Win32.exe xml2bin  .\res

@echo ----------------------------------------------------------------
@echo - Update MPQ
@echo ----------------------------------------------------------------
@java -classpath %LIB%/g2d_studio.jar -Xmx1024m FilePackerBatchZlib U  .\res "./mpq/res.mpq" "./mpq" "%LIB%/update_filter_pc.txt"      ""

@echo ----------------------------------------------------------------
@echo - Build Update Version
@echo ----------------------------------------------------------------
@SET SUFFIX=+.mpq;+.assetBundles;+.unity3d;+.ogg;+.ogv;+.mp3;+.mp4;+.wav;+.assetbundle
@java -classpath %LIB%/g2d_studio.jar -Xmx1024m GenMD5 --md5 -verbos:s -srcDir:mpq -dstFile:./mpq/update_version.txt -dstEnc:UTF-8 -filter:%SUFFIX%
@java -classpath %LIB%/g2d_studio.jar MakeBeginEnd ./mpq/update_version.txt BEGIN END

@echo ----------------------------------------------------------------
@echo - Done
@echo ----------------------------------------------------------------

@pause