
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal void Lua_DeepCore_Unity3D_UGUIEditor_UI_UEGauge_ValueChangedHandler(LuaFunction ld ,DeepCore.Unity3D.UGUIEditor.UI.UEGauge a1,double a2) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			pushValue(l,a2);
			ld.pcall(2, error);
			LuaDLL.lua_settop(l, error-1);
		}
	}
}
