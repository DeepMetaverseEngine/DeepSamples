
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal bool Lua_System_Predicate_1_DeepCore_Unity3D_Battle_ComAIUnit_ActionStatus(LuaFunction ld ,DeepCore.Unity3D.Battle.ComAIUnit.ActionStatus a1) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			ld.pcall(1, error);
			bool ret;
			checkType(l,error+1,out ret);
			LuaDLL.lua_settop(l, error-1);
			return ret;
		}
	}
}
