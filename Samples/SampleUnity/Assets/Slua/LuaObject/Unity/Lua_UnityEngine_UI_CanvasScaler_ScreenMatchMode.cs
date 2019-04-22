using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_CanvasScaler_ScreenMatchMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMatchWidthOrHeight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MatchWidthOrHeight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getExpand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Expand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getShrink(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasScaler.ScreenMatchMode.Shrink);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Shrink(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasScaler.ScreenMatchMode.Shrink);
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
		getTypeTable(l,"UnityEngine.UI.CanvasScaler.ScreenMatchMode");
		addMember(l,"MatchWidthOrHeight",getMatchWidthOrHeight,null,false);
		addMember(l,"_MatchWidthOrHeight",get_MatchWidthOrHeight,null,false);
		addMember(l,"Expand",getExpand,null,false);
		addMember(l,"_Expand",get_Expand,null,false);
		addMember(l,"Shrink",getShrink,null,false);
		addMember(l,"_Shrink",get_Shrink,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode));
	}
}
