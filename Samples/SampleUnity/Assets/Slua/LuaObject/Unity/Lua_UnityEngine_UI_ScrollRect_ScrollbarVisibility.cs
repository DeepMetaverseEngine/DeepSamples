using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_ScrollRect_ScrollbarVisibility : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPermanent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.ScrollRect.ScrollbarVisibility.Permanent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Permanent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.ScrollRect.ScrollbarVisibility.Permanent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAutoHide(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHide);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoHide(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHide);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAutoHideAndExpandViewport(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoHideAndExpandViewport(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);
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
		getTypeTable(l,"UnityEngine.UI.ScrollRect.ScrollbarVisibility");
		addMember(l,"Permanent",getPermanent,null,false);
		addMember(l,"_Permanent",get_Permanent,null,false);
		addMember(l,"AutoHide",getAutoHide,null,false);
		addMember(l,"_AutoHide",get_AutoHide,null,false);
		addMember(l,"AutoHideAndExpandViewport",getAutoHideAndExpandViewport,null,false);
		addMember(l,"_AutoHideAndExpandViewport",get_AutoHideAndExpandViewport,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility));
	}
}
