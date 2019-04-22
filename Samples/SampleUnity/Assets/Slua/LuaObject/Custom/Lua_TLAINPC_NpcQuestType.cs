using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAINPC_NpcQuestType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLAINPC.NpcQuestType o;
			o=new TLAINPC.NpcQuestType();
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
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAINPC.NpcQuestType.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StoryAsk(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAINPC.NpcQuestType.StoryAsk);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DailyAsk(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAINPC.NpcQuestType.DailyAsk);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StorySign(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAINPC.NpcQuestType.StorySign);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DailySign(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAINPC.NpcQuestType.DailySign);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DarkAsk(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAINPC.NpcQuestType.DarkAsk);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DarkSign(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAINPC.NpcQuestType.DarkSign);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Max(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAINPC.NpcQuestType.Max);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"NpcQuestType");
		addMember(l,"None",get_None,null,false);
		addMember(l,"StoryAsk",get_StoryAsk,null,false);
		addMember(l,"DailyAsk",get_DailyAsk,null,false);
		addMember(l,"StorySign",get_StorySign,null,false);
		addMember(l,"DailySign",get_DailySign,null,false);
		addMember(l,"DarkAsk",get_DarkAsk,null,false);
		addMember(l,"DarkSign",get_DarkSign,null,false);
		addMember(l,"Max",get_Max,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLAINPC.NpcQuestType),typeof(System.ValueType));
	}
}
