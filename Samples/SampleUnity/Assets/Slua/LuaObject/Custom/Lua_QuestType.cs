using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_QuestType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			QuestType o;
			o=new QuestType();
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
	static public int get_TypeNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,QuestType.TypeNone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TypeStory(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,QuestType.TypeStory);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TypeGuide(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,QuestType.TypeGuide);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TypeDaily(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,QuestType.TypeDaily);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TypeTip(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,QuestType.TypeTip);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"QuestType");
		addMember(l,"TypeNone",get_TypeNone,null,false);
		addMember(l,"TypeStory",get_TypeStory,null,false);
		addMember(l,"TypeGuide",get_TypeGuide,null,false);
		addMember(l,"TypeDaily",get_TypeDaily,null,false);
		addMember(l,"TypeTip",get_TypeTip,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(QuestType),typeof(System.ValueType));
	}
}
