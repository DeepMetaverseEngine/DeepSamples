using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RigidbodySleepMode2D : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNeverSleep(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodySleepMode2D.NeverSleep);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NeverSleep(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodySleepMode2D.NeverSleep);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStartAwake(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodySleepMode2D.StartAwake);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StartAwake(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodySleepMode2D.StartAwake);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStartAsleep(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodySleepMode2D.StartAsleep);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StartAsleep(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodySleepMode2D.StartAsleep);
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
		getTypeTable(l,"UnityEngine.RigidbodySleepMode2D");
		addMember(l,"NeverSleep",getNeverSleep,null,false);
		addMember(l,"_NeverSleep",get_NeverSleep,null,false);
		addMember(l,"StartAwake",getStartAwake,null,false);
		addMember(l,"_StartAwake",get_StartAwake,null,false);
		addMember(l,"StartAsleep",getStartAsleep,null,false);
		addMember(l,"_StartAsleep",get_StartAsleep,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RigidbodySleepMode2D));
	}
}
