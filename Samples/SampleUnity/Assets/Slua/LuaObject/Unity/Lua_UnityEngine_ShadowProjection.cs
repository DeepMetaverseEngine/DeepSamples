using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ShadowProjection : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCloseFit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowProjection.CloseFit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CloseFit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowProjection.CloseFit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStableFit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ShadowProjection.StableFit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StableFit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ShadowProjection.StableFit);
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
		getTypeTable(l,"UnityEngine.ShadowProjection");
		addMember(l,"CloseFit",getCloseFit,null,false);
		addMember(l,"_CloseFit",get_CloseFit,null,false);
		addMember(l,"StableFit",getStableFit,null,false);
		addMember(l,"_StableFit",get_StableFit,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ShadowProjection));
	}
}
