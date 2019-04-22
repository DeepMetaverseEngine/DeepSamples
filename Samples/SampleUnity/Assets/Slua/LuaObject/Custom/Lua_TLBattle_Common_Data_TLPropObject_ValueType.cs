using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLBattle_Common_Data_TLPropObject_ValueType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getValue(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.ValueType.Value);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Value(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.ValueType.Value);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPercent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.ValueType.Percent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Percent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.ValueType.Percent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getValuePercent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.ValueType.ValuePercent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ValuePercent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.ValueType.ValuePercent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFinalPropPercent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.ValueType.FinalPropPercent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FinalPropPercent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.ValueType.FinalPropPercent);
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
		getTypeTable(l,"TLBattle.RoleValueType");
		addMember(l,"Value",getValue,null,false);
		addMember(l,"_Value",get_Value,null,false);
		addMember(l,"Percent",getPercent,null,false);
		addMember(l,"_Percent",get_Percent,null,false);
		addMember(l,"ValuePercent",getValuePercent,null,false);
		addMember(l,"_ValuePercent",get_ValuePercent,null,false);
		addMember(l,"FinalPropPercent",getFinalPropPercent,null,false);
		addMember(l,"_FinalPropPercent",get_FinalPropPercent,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLBattle.Common.Data.TLPropObject.ValueType));
	}
}
