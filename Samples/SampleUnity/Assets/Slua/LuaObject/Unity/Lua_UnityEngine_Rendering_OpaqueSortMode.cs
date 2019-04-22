﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Rendering_OpaqueSortMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDefault(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.OpaqueSortMode.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Default(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.OpaqueSortMode.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFrontToBack(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.OpaqueSortMode.FrontToBack);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FrontToBack(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.OpaqueSortMode.FrontToBack);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNoDistanceSort(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.OpaqueSortMode.NoDistanceSort);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NoDistanceSort(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.OpaqueSortMode.NoDistanceSort);
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
		getTypeTable(l,"UnityEngine.Rendering.OpaqueSortMode");
		addMember(l,"Default",getDefault,null,false);
		addMember(l,"_Default",get_Default,null,false);
		addMember(l,"FrontToBack",getFrontToBack,null,false);
		addMember(l,"_FrontToBack",get_FrontToBack,null,false);
		addMember(l,"NoDistanceSort",getNoDistanceSort,null,false);
		addMember(l,"_NoDistanceSort",get_NoDistanceSort,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Rendering.OpaqueSortMode));
	}
}