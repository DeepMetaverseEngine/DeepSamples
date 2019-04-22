using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_PlayMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStopSameLayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PlayMode.StopSameLayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StopSameLayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PlayMode.StopSameLayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStopAll(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PlayMode.StopAll);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StopAll(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PlayMode.StopAll);
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
		getTypeTable(l,"UnityEngine.PlayMode");
		addMember(l,"StopSameLayer",getStopSameLayer,null,false);
		addMember(l,"_StopSameLayer",get_StopSameLayer,null,false);
		addMember(l,"StopAll",getStopAll,null,false);
		addMember(l,"_StopAll",get_StopAll,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.PlayMode));
	}
}
