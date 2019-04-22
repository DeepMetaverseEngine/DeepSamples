using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_GridLayoutGroup_Constraint : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFlexible(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.GridLayoutGroup.Constraint.Flexible);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Flexible(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.GridLayoutGroup.Constraint.Flexible);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFixedColumnCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FixedColumnCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFixedRowCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FixedRowCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount);
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
		getTypeTable(l,"UnityEngine.UI.GridLayoutGroup.Constraint");
		addMember(l,"Flexible",getFlexible,null,false);
		addMember(l,"_Flexible",get_Flexible,null,false);
		addMember(l,"FixedColumnCount",getFixedColumnCount,null,false);
		addMember(l,"_FixedColumnCount",get_FixedColumnCount,null,false);
		addMember(l,"FixedRowCount",getFixedRowCount,null,false);
		addMember(l,"_FixedRowCount",get_FixedRowCount,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.GridLayoutGroup.Constraint));
	}
}
