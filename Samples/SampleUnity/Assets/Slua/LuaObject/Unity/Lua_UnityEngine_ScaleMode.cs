using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ScaleMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStretchToFill(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScaleMode.StretchToFill);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StretchToFill(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScaleMode.StretchToFill);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getScaleAndCrop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScaleMode.ScaleAndCrop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScaleAndCrop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScaleMode.ScaleAndCrop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getScaleToFit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScaleMode.ScaleToFit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScaleToFit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScaleMode.ScaleToFit);
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
		getTypeTable(l,"UnityEngine.ScaleMode");
		addMember(l,"StretchToFill",getStretchToFill,null,false);
		addMember(l,"_StretchToFill",get_StretchToFill,null,false);
		addMember(l,"ScaleAndCrop",getScaleAndCrop,null,false);
		addMember(l,"_ScaleAndCrop",get_ScaleAndCrop,null,false);
		addMember(l,"ScaleToFit",getScaleToFit,null,false);
		addMember(l,"_ScaleToFit",get_ScaleToFit,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ScaleMode));
	}
}
