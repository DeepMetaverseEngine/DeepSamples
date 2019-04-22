using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_QueryTriggerInteraction : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUseGlobal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.QueryTriggerInteraction.UseGlobal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UseGlobal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.QueryTriggerInteraction.UseGlobal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getIgnore(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.QueryTriggerInteraction.Ignore);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Ignore(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.QueryTriggerInteraction.Ignore);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCollide(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.QueryTriggerInteraction.Collide);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Collide(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.QueryTriggerInteraction.Collide);
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
		getTypeTable(l,"UnityEngine.QueryTriggerInteraction");
		addMember(l,"UseGlobal",getUseGlobal,null,false);
		addMember(l,"_UseGlobal",get_UseGlobal,null,false);
		addMember(l,"Ignore",getIgnore,null,false);
		addMember(l,"_Ignore",get_Ignore,null,false);
		addMember(l,"Collide",getCollide,null,false);
		addMember(l,"_Collide",get_Collide,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.QueryTriggerInteraction));
	}
}
