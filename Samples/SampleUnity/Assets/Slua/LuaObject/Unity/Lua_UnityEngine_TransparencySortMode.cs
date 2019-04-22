using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_TransparencySortMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDefault(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TransparencySortMode.Default);
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
			pushValue(l,(double)UnityEngine.TransparencySortMode.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPerspective(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TransparencySortMode.Perspective);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Perspective(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TransparencySortMode.Perspective);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOrthographic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TransparencySortMode.Orthographic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Orthographic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TransparencySortMode.Orthographic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCustomAxis(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TransparencySortMode.CustomAxis);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CustomAxis(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TransparencySortMode.CustomAxis);
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
		getTypeTable(l,"UnityEngine.TransparencySortMode");
		addMember(l,"Default",getDefault,null,false);
		addMember(l,"_Default",get_Default,null,false);
		addMember(l,"Perspective",getPerspective,null,false);
		addMember(l,"_Perspective",get_Perspective,null,false);
		addMember(l,"Orthographic",getOrthographic,null,false);
		addMember(l,"_Orthographic",get_Orthographic,null,false);
		addMember(l,"CustomAxis",getCustomAxis,null,false);
		addMember(l,"_CustomAxis",get_CustomAxis,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.TransparencySortMode));
	}
}
