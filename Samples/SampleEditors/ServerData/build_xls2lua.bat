SET Path=..\lib\;%Path%


@echo ---------------------------------------------------------------------------
xlslang lua -id:.\templates_xls -od:.\templates_lua -key:id -olang:1 -filter_text:-localization/ -lang_format:"<doc><PATH><FILE><SHEET_NAME/></FILE><DATA_TYPE/></PATH>_<ROW_ID/>.<COLUMN_NAME/></doc>"
@echo ---------------------------------------------------------------------------
xlslang md5 -id:.\templates_lua -of:.\templates_lua\_luaversion_.lua -filter_text:"-_luaversion_.lua$"
@echo ---------------------------------------------------------------------------
xcopy /Y /Q /E .\templates_lua\*.lua      ..\GameEditors\ClientScript\Data
copy  /Y       .\config_server_list.xml   ..\GameEditors\ClientScript
@echo ---------------------------------------------------------------------------
xlslang
rem cd split_lua_bigtable
rem process_bigtable.exe ../templates_lua
pause