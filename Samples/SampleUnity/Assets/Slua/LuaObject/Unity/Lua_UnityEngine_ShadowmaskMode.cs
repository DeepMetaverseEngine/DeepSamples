using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ShadowmaskMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getShadowmask(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowmaskMode.Shadowmask);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Shadowmask(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowmaskMode.Shadowmask);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDistanceShadowmask(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowmaskMode.DistanceShadowmask);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DistanceShadowmask(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowmaskMode.DistanceShadowmask);
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
		getTypeTable(l,"UnityEngine.ShadowmaskMode");
		addMember(l,"Shadowmask",getShadowmask,null,false);
		addMember(l,"_Shadowmask",get_Shadowmask,null,false);
		addMember(l,"DistanceShadowmask",getDistanceShadowmask,null,false);
		addMember(l,"_DistanceShadowmask",get_DistanceShadowmask,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ShadowmaskMode));
	}
}
