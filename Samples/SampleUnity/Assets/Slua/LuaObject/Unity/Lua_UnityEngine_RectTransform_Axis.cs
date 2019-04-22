using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RectTransform_Axis : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHorizontal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RectTransform.Axis.Horizontal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Horizontal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RectTransform.Axis.Horizontal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVertical(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RectTransform.Axis.Vertical);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Vertical(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RectTransform.Axis.Vertical);
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
		getTypeTable(l,"UnityEngine.RectTransform.Axis");
		addMember(l,"Horizontal",getHorizontal,null,false);
		addMember(l,"_Horizontal",get_Horizontal,null,false);
		addMember(l,"Vertical",getVertical,null,false);
		addMember(l,"_Vertical",get_Vertical,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RectTransform.Axis));
	}
}
