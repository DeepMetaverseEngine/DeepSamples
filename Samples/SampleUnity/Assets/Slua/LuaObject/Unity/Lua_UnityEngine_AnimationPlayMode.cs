using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_AnimationPlayMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimationPlayMode.Stop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Stop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AnimationPlayMode.Stop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getQueue(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimationPlayMode.Queue);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Queue(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AnimationPlayMode.Queue);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMix(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimationPlayMode.Mix);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Mix(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AnimationPlayMode.Mix);
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
		getTypeTable(l,"UnityEngine.AnimationPlayMode");
		addMember(l,"Stop",getStop,null,false);
		addMember(l,"_Stop",get_Stop,null,false);
		addMember(l,"Queue",getQueue,null,false);
		addMember(l,"_Queue",get_Queue,null,false);
		addMember(l,"Mix",getMix,null,false);
		addMember(l,"_Mix",get_Mix,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.AnimationPlayMode));
	}
}
