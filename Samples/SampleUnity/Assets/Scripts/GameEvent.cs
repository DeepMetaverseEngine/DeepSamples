using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static string SYS_GAME_START = "EVENT_SYS_GAME_START";
    public static string SYS_LOGIN_SUCCESS = "EVENT_SYS_LOGIN_SUCCESS";
    public static string SYS_ENTER_SCENE = "EVENT_SYS_ENTER_SCENE";
    public static string SYS_ADD_UNIT = "EVENT_SYS_ADD_UNIT";
    public static string SYS_REMOVE_UNIT = "EVENT_SYS_REMOVE_UNIT";

    public static string UI_HUD_LUAHUDINIT = "EVENT_UI_HUD_LUAHUDINIT";
    public static string UI_HUD_SYNC_GUARD_STATE = "EVENT_UI_HUD_AUTOGUARD";
    public static string UI_HUD_SYNC_PK_MODE = "EVENT_UI_HUD_PK_MODE";
}
