﻿
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal void Lua_DeepCore_Unity3D_UGUI_DisplayNode_ChildEventHandler(LuaFunction ld ,DeepCore.Unity3D.UGUI.DisplayNode a1,DeepCore.Unity3D.UGUI.DisplayNode a2) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			pushValue(l,a2);
			ld.pcall(2, error);
			LuaDLL.lua_settop(l, error-1);
		}
	}
}