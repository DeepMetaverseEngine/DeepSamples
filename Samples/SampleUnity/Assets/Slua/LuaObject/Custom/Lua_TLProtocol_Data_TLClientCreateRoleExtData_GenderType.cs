using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_TLClientCreateRoleExtData_GenderType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMan(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLClientCreateRoleExtData.GenderType.Man);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Man(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLProtocol.Data.TLClientCreateRoleExtData.GenderType.Man);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWoman(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLClientCreateRoleExtData.GenderType.Woman);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Woman(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLProtocol.Data.TLClientCreateRoleExtData.GenderType.Woman);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int op_Equality(IntPtr l) {
		try {
			System.Object a1;
			checkType(l,1,out a1);
			System.Object a2;
			checkType(l,2,out a2);
			var ret = System.Object.Equals(a1, a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"GenderType");
		addMember(l,"Man",getMan,null,false);
		addMember(l,"_Man",get_Man,null,false);
		addMember(l,"Woman",getWoman,null,false);
		addMember(l,"_Woman",get_Woman,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLProtocol.Data.TLClientCreateRoleExtData.GenderType));
	}
}
