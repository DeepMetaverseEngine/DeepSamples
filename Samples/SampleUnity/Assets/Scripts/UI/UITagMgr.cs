using SLua;
using System.Collections.Generic;

/// <summary>
/// 所有UI的TAG在这里定义，作为唯一标识，每个界面都对应一个TAG，必须要加
/// 第一列表示枚举值，第二列为对应功能的描述，第三列为对应功能的类名。注释务必写全，方便查询
/// </summary>
public class UITAG {

    public static Dictionary<string, int> sTagMap = new Dictionary<string,int>();

    public static void InitTagMap()
    {
        string tagTab = "GlobalHooks.UITAG";
        LuaState ls_state = new LuaState();
        LuaTable luaTable = ls_state.getTable(tagTab);
        foreach (var p in luaTable)
        {
            sTagMap[p.key.ToString()] = System.Convert.ToInt32(p.value);
        }
    }

    public static int Key2Tag(string key)
    {
        if(sTagMap.Count == 0)
        {
            InitTagMap();
        }
        int ret = 0;
        sTagMap.TryGetValue(key, out ret);
        return ret;
    }

    public static string Tag2Key(int tag)
    {
        LuaState ls_state = new LuaState();
        LuaTable luaTable = ls_state.getTable("GlobalHooks");
        object ret = luaTable.invoke("GetUIKey", tag);
        if (ret != null)
        {
            return ret.ToString();
        }
        return null;
    }
}

public class UIShowType
{
    //直接覆盖，不隐藏背景.
    public const int Cover = 0;
    //隐藏所有在下层的菜单.
    public const int HideBackMenu = 1 << 0;
    //隐藏3d场景.
    public const int HideBackScene = 1 << 1;
    //只隐藏HUD.
    public const int HideBackHud = 1 << 2;
    //隐藏场景和HUD.
    public const int HideSceneAndHud = HideBackScene + HideBackHud;
    //隐藏HUD和下层的菜单.
    public const int HideHudAndMenu = HideBackMenu + HideBackHud;
    //隐藏所有.
    public const int HideBackAll = HideBackMenu | HideBackScene | HideBackHud;
}

public class UIAnimeType
{
    public const int Default = -1;
    public const int NoAnime = 0;
    public const int Scale = 1;
    public const int FadeMoveUp = 2;
    public const int FadeMoveDown = 3;
    public const int FadeMoveLeft = 4;
    public const int FadeMoveRight = 5;
}
