using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_CanvasScaler_Unit : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCentimeters(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasScaler.Unit.Centimeters);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Centimeters(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasScaler.Unit.Centimeters);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMillimeters(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasScaler.Unit.Millimeters);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Millimeters(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasScaler.Unit.Millimeters);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInches(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasScaler.Unit.Inches);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Inches(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasScaler.Unit.Inches);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPoints(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasScaler.Unit.Points);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Points(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasScaler.Unit.Points);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPicas(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasScaler.Unit.Picas);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Picas(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasScaler.Unit.Picas);
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
		getTypeTable(l,"UnityEngine.UI.CanvasScaler.Unit");
		addMember(l,"Centimeters",getCentimeters,null,false);
		addMember(l,"_Centimeters",get_Centimeters,null,false);
		addMember(l,"Millimeters",getMillimeters,null,false);
		addMember(l,"_Millimeters",get_Millimeters,null,false);
		addMember(l,"Inches",getInches,null,false);
		addMember(l,"_Inches",get_Inches,null,false);
		addMember(l,"Points",getPoints,null,false);
		addMember(l,"_Points",get_Points,null,false);
		addMember(l,"Picas",getPicas,null,false);
		addMember(l,"_Picas",get_Picas,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.CanvasScaler.Unit));
	}
}
