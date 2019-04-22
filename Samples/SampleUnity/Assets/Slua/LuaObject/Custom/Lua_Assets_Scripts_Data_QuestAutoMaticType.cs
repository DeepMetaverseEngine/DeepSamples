using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_Assets_Scripts_Data_QuestAutoMaticType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			Assets.Scripts.Data.QuestAutoMaticType o;
			o=new Assets.Scripts.Data.QuestAutoMaticType();
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
	static public int get_NoAuto(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,Assets.Scripts.Data.QuestAutoMaticType.NoAuto);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AllAuto(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,Assets.Scripts.Data.QuestAutoMaticType.AllAuto);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoAccept(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,Assets.Scripts.Data.QuestAutoMaticType.AutoAccept);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NoAutoAccetpButAutoDone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,Assets.Scripts.Data.QuestAutoMaticType.NoAutoAccetpButAutoDone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"QuestAutoMaticType");
		addMember(l,"NoAuto",get_NoAuto,null,false);
		addMember(l,"AllAuto",get_AllAuto,null,false);
		addMember(l,"AutoAccept",get_AutoAccept,null,false);
		addMember(l,"NoAutoAccetpButAutoDone",get_NoAutoAccetpButAutoDone,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(Assets.Scripts.Data.QuestAutoMaticType),typeof(System.ValueType));
	}
}
