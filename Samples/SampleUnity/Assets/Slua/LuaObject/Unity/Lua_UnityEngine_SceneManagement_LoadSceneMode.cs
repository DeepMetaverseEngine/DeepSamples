using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_SceneManagement_LoadSceneMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSingle(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SceneManagement.LoadSceneMode.Single);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Single(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SceneManagement.LoadSceneMode.Single);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAdditive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SceneManagement.LoadSceneMode.Additive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Additive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SceneManagement.LoadSceneMode.Additive);
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
		getTypeTable(l,"UnityEngine.SceneManagement.LoadSceneMode");
		addMember(l,"Single",getSingle,null,false);
		addMember(l,"_Single",get_Single,null,false);
		addMember(l,"Additive",getAdditive,null,false);
		addMember(l,"_Additive",get_Additive,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.SceneManagement.LoadSceneMode));
	}
}
