using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Events_UnityEventCallState : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.UnityEventCallState.Off);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Off(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.UnityEventCallState.Off);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEditorAndRuntime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.UnityEventCallState.EditorAndRuntime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EditorAndRuntime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.UnityEventCallState.EditorAndRuntime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRuntimeOnly(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.UnityEventCallState.RuntimeOnly);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RuntimeOnly(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.UnityEventCallState.RuntimeOnly);
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
		getTypeTable(l,"UnityEngine.Events.UnityEventCallState");
		addMember(l,"Off",getOff,null,false);
		addMember(l,"_Off",get_Off,null,false);
		addMember(l,"EditorAndRuntime",getEditorAndRuntime,null,false);
		addMember(l,"_EditorAndRuntime",get_EditorAndRuntime,null,false);
		addMember(l,"RuntimeOnly",getRuntimeOnly,null,false);
		addMember(l,"_RuntimeOnly",get_RuntimeOnly,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Events.UnityEventCallState));
	}
}
