﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Camera_MonoOrStereoscopicEye : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Camera.MonoOrStereoscopicEye.Left);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Left(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Camera.MonoOrStereoscopicEye.Left);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Camera.MonoOrStereoscopicEye.Right);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Right(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Camera.MonoOrStereoscopicEye.Right);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMono(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Camera.MonoOrStereoscopicEye.Mono);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Mono(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Camera.MonoOrStereoscopicEye.Mono);
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
		getTypeTable(l,"UnityEngine.Camera.MonoOrStereoscopicEye");
		addMember(l,"Left",getLeft,null,false);
		addMember(l,"_Left",get_Left,null,false);
		addMember(l,"Right",getRight,null,false);
		addMember(l,"_Right",get_Right,null,false);
		addMember(l,"Mono",getMono,null,false);
		addMember(l,"_Mono",get_Mono,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Camera.MonoOrStereoscopicEye));
	}
}
