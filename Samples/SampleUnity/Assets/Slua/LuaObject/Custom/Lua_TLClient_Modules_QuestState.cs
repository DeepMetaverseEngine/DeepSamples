using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Modules_QuestState : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLClient.Modules.QuestState o;
			o=new TLClient.Modules.QuestState();
			pushValue(l,true);
			pushValue(l,o);
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NotAccept(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Modules.QuestState.NotAccept);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NotCompleted(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Modules.QuestState.NotCompleted);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CompletedNotSubmited(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Modules.QuestState.CompletedNotSubmited);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Fail(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Modules.QuestState.Fail);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Remove(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Modules.QuestState.Remove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Submited(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Modules.QuestState.Submited);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"QuestState");
		addMember(l,"NotAccept",get_NotAccept,null,false);
		addMember(l,"NotCompleted",get_NotCompleted,null,false);
		addMember(l,"CompletedNotSubmited",get_CompletedNotSubmited,null,false);
		addMember(l,"Fail",get_Fail,null,false);
		addMember(l,"Remove",get_Remove,null,false);
		addMember(l,"Submited",get_Submited,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLClient.Modules.QuestState),typeof(System.ValueType));
	}
}
