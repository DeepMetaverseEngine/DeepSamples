﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ColorSpace : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getGamma(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ColorSpace.Gamma);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Gamma(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ColorSpace.Gamma);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ColorSpace.Linear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Linear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ColorSpace.Linear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUninitialized(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ColorSpace.Uninitialized);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Uninitialized(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ColorSpace.Uninitialized);
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
		getTypeTable(l,"UnityEngine.ColorSpace");
		addMember(l,"Gamma",getGamma,null,false);
		addMember(l,"_Gamma",get_Gamma,null,false);
		addMember(l,"Linear",getLinear,null,false);
		addMember(l,"_Linear",get_Linear,null,false);
		addMember(l,"Uninitialized",getUninitialized,null,false);
		addMember(l,"_Uninitialized",get_Uninitialized,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ColorSpace));
	}
}