﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RotationDriveMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getXYAndZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RotationDriveMode.XYAndZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_XYAndZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RotationDriveMode.XYAndZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSlerp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RotationDriveMode.Slerp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Slerp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RotationDriveMode.Slerp);
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
		getTypeTable(l,"UnityEngine.RotationDriveMode");
		addMember(l,"XYAndZ",getXYAndZ,null,false);
		addMember(l,"_XYAndZ",get_XYAndZ,null,false);
		addMember(l,"Slerp",getSlerp,null,false);
		addMember(l,"_Slerp",get_Slerp,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RotationDriveMode));
	}
}