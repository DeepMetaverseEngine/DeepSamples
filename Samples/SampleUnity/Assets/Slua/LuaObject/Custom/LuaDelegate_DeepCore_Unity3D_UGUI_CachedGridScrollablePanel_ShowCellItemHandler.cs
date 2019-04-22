
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal void Lua_DeepCore_Unity3D_UGUI_CachedGridScrollablePanel_ShowCellItemHandler(LuaFunction ld ,DeepCore.Unity3D.UGUI.CachedGridScrollablePanel a1,int a2,int a3,DeepCore.Unity3D.UGUI.DisplayNode a4) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			pushValue(l,a2);
			pushValue(l,a3);
			pushValue(l,a4);
			ld.pcall(4, error);
			LuaDLL.lua_settop(l, error-1);
		}
	}
}
