
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal DeepCore.Unity3D.UGUI.DisplayNode Lua_DeepCore_Unity3D_UGUI_PagedScrollablePanel_CreatePageItemHandler(LuaFunction ld ,DeepCore.Unity3D.UGUI.PagedScrollablePanel a1,int a2) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			pushValue(l,a2);
			ld.pcall(2, error);
			DeepCore.Unity3D.UGUI.DisplayNode ret;
			checkType(l,error+1,out ret);
			LuaDLL.lua_settop(l, error-1);
			return ret;
		}
	}
}
