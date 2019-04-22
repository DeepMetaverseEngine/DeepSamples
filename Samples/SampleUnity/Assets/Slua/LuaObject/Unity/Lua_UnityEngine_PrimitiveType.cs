using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_PrimitiveType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSphere(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PrimitiveType.Sphere);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Sphere(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PrimitiveType.Sphere);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCapsule(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PrimitiveType.Capsule);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Capsule(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PrimitiveType.Capsule);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCylinder(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PrimitiveType.Cylinder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Cylinder(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PrimitiveType.Cylinder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCube(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PrimitiveType.Cube);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Cube(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PrimitiveType.Cube);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPlane(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PrimitiveType.Plane);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Plane(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PrimitiveType.Plane);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getQuad(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PrimitiveType.Quad);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Quad(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PrimitiveType.Quad);
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
		getTypeTable(l,"UnityEngine.PrimitiveType");
		addMember(l,"Sphere",getSphere,null,false);
		addMember(l,"_Sphere",get_Sphere,null,false);
		addMember(l,"Capsule",getCapsule,null,false);
		addMember(l,"_Capsule",get_Capsule,null,false);
		addMember(l,"Cylinder",getCylinder,null,false);
		addMember(l,"_Cylinder",get_Cylinder,null,false);
		addMember(l,"Cube",getCube,null,false);
		addMember(l,"_Cube",get_Cube,null,false);
		addMember(l,"Plane",getPlane,null,false);
		addMember(l,"_Plane",get_Plane,null,false);
		addMember(l,"Quad",getQuad,null,false);
		addMember(l,"_Quad",get_Quad,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.PrimitiveType));
	}
}
