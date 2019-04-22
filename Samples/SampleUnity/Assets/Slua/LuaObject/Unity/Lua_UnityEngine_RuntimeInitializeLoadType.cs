using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RuntimeInitializeLoadType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAfterSceneLoad(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AfterSceneLoad(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBeforeSceneLoad(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BeforeSceneLoad(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad);
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
		getTypeTable(l,"UnityEngine.RuntimeInitializeLoadType");
		addMember(l,"AfterSceneLoad",getAfterSceneLoad,null,false);
		addMember(l,"_AfterSceneLoad",get_AfterSceneLoad,null,false);
		addMember(l,"BeforeSceneLoad",getBeforeSceneLoad,null,false);
		addMember(l,"_BeforeSceneLoad",get_BeforeSceneLoad,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RuntimeInitializeLoadType));
	}
}
