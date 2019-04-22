SET Path=..\lib\;%Path%


@echo ---------------------------------------------------------------------------
copy /Y build_xls2lang_head.txt .\lang\lang.csv
@echo ---------------------------------------------------------------------------
xlslang lang  -id:.\templates_xls -of:.\lang\lang.csv -key:id -encoding:utf-8 -filter_text:-localization/ -append:1 -lang_format:"<doc><PATH><FILE><SHEET_NAME/></FILE><DATA_TYPE/></PATH>_<ROW_ID/>.<COLUMN_NAME/></doc>"
xlslang lang  -id:.\templates_xls -of:.\lang\lang.csv -key:id -encoding:utf-8 -filter_text:+localization/ -append:1 -lang_format:"<doc><ROW_ID/></doc>"
@echo ---------------------------------------------------------------------------
xlslang local -if:.\lang\lang.xlsx -od:.\templates_lua\lang -encoding:"utf-8"
@echo ---------------------------------------------------------------------------
xcopy /Y /Q /E .\templates_lua\*.properties   ..\GameEditors\ClientScript\Data
copy  /Y       .\config_server_list.xml       ..\GameEditors\ClientScript
@echo ---------------------------------------------------------------------------
xlslang
pause