﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_FontUpdateTracker : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int TrackText_s(IntPtr l) {
		try {
			UnityEngine.UI.Text a1;
			checkType(l,1,out a1);
			UnityEngine.UI.FontUpdateTracker.TrackText(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UntrackText_s(IntPtr l) {
		try {
			UnityEngine.UI.Text a1;
			checkType(l,1,out a1);
			UnityEngine.UI.FontUpdateTracker.UntrackText(a1);
			pushValue(l,true);
			return 1;
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
		getTypeTable(l,"UnityEngine.UI.FontUpdateTracker");
		addMember(l,TrackText_s);
		addMember(l,UntrackText_s);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.FontUpdateTracker));
	}
}