﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_Scrollbar_ScrollEvent : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.UI.Scrollbar.ScrollEvent o;
			o=new UnityEngine.UI.Scrollbar.ScrollEvent();
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		LuaUnityEvent_float.reg(l);
		getTypeTable(l,"UnityEngine.UI.Scrollbar.ScrollEvent");
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(UnityEngine.UI.Scrollbar.ScrollEvent),typeof(LuaUnityEvent_float));
	}
}