
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal void Lua_System_Action_2_DeepCore_FuckPomeloClient_PomeloException_DeepCore_IO_ISerializable(LuaFunction ld ,DeepCore.FuckPomeloClient.PomeloException a1,DeepCore.IO.ISerializable a2) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			pushValue(l,a2);
			ld.pcall(2, error);
			LuaDLL.lua_settop(l, error-1);
		}
	}
}
