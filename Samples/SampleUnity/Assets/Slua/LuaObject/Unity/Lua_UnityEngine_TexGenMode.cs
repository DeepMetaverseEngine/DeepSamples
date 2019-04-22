using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_TexGenMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TexGenMode.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TexGenMode.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSphereMap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TexGenMode.SphereMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SphereMap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TexGenMode.SphereMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getObject(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TexGenMode.Object);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Object(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TexGenMode.Object);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEyeLinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TexGenMode.EyeLinear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EyeLinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TexGenMode.EyeLinear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCubeReflect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TexGenMode.CubeReflect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CubeReflect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TexGenMode.CubeReflect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCubeNormal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TexGenMode.CubeNormal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CubeNormal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TexGenMode.CubeNormal);
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
		getTypeTable(l,"UnityEngine.TexGenMode");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"SphereMap",getSphereMap,null,false);
		addMember(l,"_SphereMap",get_SphereMap,null,false);
		addMember(l,"Object",getObject,null,false);
		addMember(l,"_Object",get_Object,null,false);
		addMember(l,"EyeLinear",getEyeLinear,null,false);
		addMember(l,"_EyeLinear",get_EyeLinear,null,false);
		addMember(l,"CubeReflect",getCubeReflect,null,false);
		addMember(l,"_CubeReflect",get_CubeReflect,null,false);
		addMember(l,"CubeNormal",getCubeNormal,null,false);
		addMember(l,"_CubeNormal",get_CubeNormal,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.TexGenMode));
	}
}
