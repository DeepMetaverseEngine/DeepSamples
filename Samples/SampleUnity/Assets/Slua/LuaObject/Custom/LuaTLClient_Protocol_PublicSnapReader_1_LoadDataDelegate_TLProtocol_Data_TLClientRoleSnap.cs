
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal void Lua_TLClient_Protocol_PublicSnapReader_1_LoadDataDelegate_TLProtocol_Data_TLClientRoleSnap(LuaFunction ld ,System.String[] a1,System.Action<System.Exception,TLProtocol.Data.TLClientRoleSnap[]> a2) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			pushValue(l,a2);
			ld.pcall(2, error);
			LuaDLL.lua_settop(l, error-1);
		}
	}
}
