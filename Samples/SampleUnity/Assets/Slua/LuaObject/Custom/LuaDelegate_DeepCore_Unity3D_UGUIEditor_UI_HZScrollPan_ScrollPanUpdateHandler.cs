
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal void Lua_DeepCore_Unity3D_UGUIEditor_UI_HZScrollPan_ScrollPanUpdateHandler(LuaFunction ld ,int a1,int a2,DeepCore.Unity3D.UGUI.DisplayNode a3) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			pushValue(l,a2);
			pushValue(l,a3);
			ld.pcall(3, error);
			LuaDLL.lua_settop(l, error-1);
		}
	}
}
