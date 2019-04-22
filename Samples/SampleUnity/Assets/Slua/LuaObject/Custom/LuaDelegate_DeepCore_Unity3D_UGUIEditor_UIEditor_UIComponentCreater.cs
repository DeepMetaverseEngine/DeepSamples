
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal DeepCore.Unity3D.UGUIEditor.UIComponent Lua_DeepCore_Unity3D_UGUIEditor_UIEditor_UIComponentCreater(LuaFunction ld ,DeepCore.GUI.Data.UIComponentMeta a1) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			ld.pcall(1, error);
			DeepCore.Unity3D.UGUIEditor.UIComponent ret;
			checkType(l,error+1,out ret);
			LuaDLL.lua_settop(l, error-1);
			return ret;
		}
	}
}
