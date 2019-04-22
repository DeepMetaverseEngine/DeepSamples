using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_DurationUnit : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFixed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DurationUnit.Fixed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Fixed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DurationUnit.Fixed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNormalized(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DurationUnit.Normalized);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Normalized(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DurationUnit.Normalized);
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
		getTypeTable(l,"UnityEngine.DurationUnit");
		addMember(l,"Fixed",getFixed,null,false);
		addMember(l,"_Fixed",get_Fixed,null,false);
		addMember(l,"Normalized",getNormalized,null,false);
		addMember(l,"_Normalized",get_Normalized,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.DurationUnit));
	}
}
