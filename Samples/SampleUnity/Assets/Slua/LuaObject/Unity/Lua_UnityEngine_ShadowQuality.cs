using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ShadowQuality : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDisable(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowQuality.Disable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Disable(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowQuality.Disable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHardOnly(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowQuality.HardOnly);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HardOnly(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowQuality.HardOnly);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAll(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowQuality.All);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_All(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowQuality.All);
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
		getTypeTable(l,"UnityEngine.ShadowQuality");
		addMember(l,"Disable",getDisable,null,false);
		addMember(l,"_Disable",get_Disable,null,false);
		addMember(l,"HardOnly",getHardOnly,null,false);
		addMember(l,"_HardOnly",get_HardOnly,null,false);
		addMember(l,"All",getAll,null,false);
		addMember(l,"_All",get_All,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ShadowQuality));
	}
}
