using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_FogMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.FogMode.Linear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Linear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.FogMode.Linear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getExponential(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.FogMode.Exponential);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Exponential(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.FogMode.Exponential);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getExponentialSquared(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.FogMode.ExponentialSquared);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ExponentialSquared(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.FogMode.ExponentialSquared);
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
		getTypeTable(l,"UnityEngine.FogMode");
		addMember(l,"Linear",getLinear,null,false);
		addMember(l,"_Linear",get_Linear,null,false);
		addMember(l,"Exponential",getExponential,null,false);
		addMember(l,"_Exponential",get_Exponential,null,false);
		addMember(l,"ExponentialSquared",getExponentialSquared,null,false);
		addMember(l,"_ExponentialSquared",get_ExponentialSquared,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.FogMode));
	}
}
