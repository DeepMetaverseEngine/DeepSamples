﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_EventSystems_AbstractEventData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Reset(IntPtr l) {
		try {
			UnityEngine.EventSystems.AbstractEventData self=(UnityEngine.EventSystems.AbstractEventData)checkSelf(l);
			self.Reset();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Use(IntPtr l) {
		try {
			UnityEngine.EventSystems.AbstractEventData self=(UnityEngine.EventSystems.AbstractEventData)checkSelf(l);
			self.Use();
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_used(IntPtr l) {
		try {
			UnityEngine.EventSystems.AbstractEventData self=(UnityEngine.EventSystems.AbstractEventData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.used);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.EventSystems.AbstractEventData");
		addMember(l,Reset);
		addMember(l,Use);
		addMember(l,"used",get_used,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.EventSystems.AbstractEventData));
	}
}