using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_FilterMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPoint(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.FilterMode.Point);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Point(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.FilterMode.Point);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBilinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.FilterMode.Bilinear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Bilinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.FilterMode.Bilinear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTrilinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.FilterMode.Trilinear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Trilinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.FilterMode.Trilinear);
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
		getTypeTable(l,"UnityEngine.FilterMode");
		addMember(l,"Point",getPoint,null,false);
		addMember(l,"_Point",get_Point,null,false);
		addMember(l,"Bilinear",getBilinear,null,false);
		addMember(l,"_Bilinear",get_Bilinear,null,false);
		addMember(l,"Trilinear",getTrilinear,null,false);
		addMember(l,"_Trilinear",get_Trilinear,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.FilterMode));
	}
}
