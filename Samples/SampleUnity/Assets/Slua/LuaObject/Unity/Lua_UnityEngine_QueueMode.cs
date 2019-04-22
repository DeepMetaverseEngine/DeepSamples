using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_QueueMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCompleteOthers(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.QueueMode.CompleteOthers);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CompleteOthers(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.QueueMode.CompleteOthers);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPlayNow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.QueueMode.PlayNow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PlayNow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.QueueMode.PlayNow);
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
		getTypeTable(l,"UnityEngine.QueueMode");
		addMember(l,"CompleteOthers",getCompleteOthers,null,false);
		addMember(l,"_CompleteOthers",get_CompleteOthers,null,false);
		addMember(l,"PlayNow",getPlayNow,null,false);
		addMember(l,"_PlayNow",get_PlayNow,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.QueueMode));
	}
}
