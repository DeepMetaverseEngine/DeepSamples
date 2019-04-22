using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CustomRenderTextureUpdateMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOnLoad(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CustomRenderTextureUpdateMode.OnLoad);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OnLoad(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CustomRenderTextureUpdateMode.OnLoad);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRealtime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CustomRenderTextureUpdateMode.Realtime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Realtime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CustomRenderTextureUpdateMode.Realtime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOnDemand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CustomRenderTextureUpdateMode.OnDemand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OnDemand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CustomRenderTextureUpdateMode.OnDemand);
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
		getTypeTable(l,"UnityEngine.CustomRenderTextureUpdateMode");
		addMember(l,"OnLoad",getOnLoad,null,false);
		addMember(l,"_OnLoad",get_OnLoad,null,false);
		addMember(l,"Realtime",getRealtime,null,false);
		addMember(l,"_Realtime",get_Realtime,null,false);
		addMember(l,"OnDemand",getOnDemand,null,false);
		addMember(l,"_OnDemand",get_OnDemand,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CustomRenderTextureUpdateMode));
	}
}
