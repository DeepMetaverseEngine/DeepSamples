using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_MsgData_NotiFyStatus : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAddMsg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,MsgData.NotiFyStatus.AddMsg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AddMsg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)MsgData.NotiFyStatus.AddMsg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRemoveMsg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,MsgData.NotiFyStatus.RemoveMsg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RemoveMsg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)MsgData.NotiFyStatus.RemoveMsg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAgreeSync(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,MsgData.NotiFyStatus.AgreeSync);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AgreeSync(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)MsgData.NotiFyStatus.AgreeSync);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getALL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,MsgData.NotiFyStatus.ALL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ALL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)MsgData.NotiFyStatus.ALL);
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
		getTypeTable(l,"MsgData.NotiFyStatus");
		addMember(l,"AddMsg",getAddMsg,null,false);
		addMember(l,"_AddMsg",get_AddMsg,null,false);
		addMember(l,"RemoveMsg",getRemoveMsg,null,false);
		addMember(l,"_RemoveMsg",get_RemoveMsg,null,false);
		addMember(l,"AgreeSync",getAgreeSync,null,false);
		addMember(l,"_AgreeSync",get_AgreeSync,null,false);
		addMember(l,"ALL",getALL,null,false);
		addMember(l,"_ALL",get_ALL,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(MsgData.NotiFyStatus));
	}
}
