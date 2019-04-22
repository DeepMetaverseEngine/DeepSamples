using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ShadowResolution : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowResolution.Low);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Low(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowResolution.Low);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMedium(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowResolution.Medium);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Medium(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowResolution.Medium);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHigh(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowResolution.High);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_High(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowResolution.High);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVeryHigh(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowResolution.VeryHigh);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VeryHigh(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowResolution.VeryHigh);
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
		getTypeTable(l,"UnityEngine.ShadowResolution");
		addMember(l,"Low",getLow,null,false);
		addMember(l,"_Low",get_Low,null,false);
		addMember(l,"Medium",getMedium,null,false);
		addMember(l,"_Medium",get_Medium,null,false);
		addMember(l,"High",getHigh,null,false);
		addMember(l,"_High",get_High,null,false);
		addMember(l,"VeryHigh",getVeryHigh,null,false);
		addMember(l,"_VeryHigh",get_VeryHigh,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ShadowResolution));
	}
}
