using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CompositeCollider2D_GeometryType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOutlines(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CompositeCollider2D.GeometryType.Outlines);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Outlines(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CompositeCollider2D.GeometryType.Outlines);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPolygons(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CompositeCollider2D.GeometryType.Polygons);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Polygons(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CompositeCollider2D.GeometryType.Polygons);
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
		getTypeTable(l,"UnityEngine.CompositeCollider2D.GeometryType");
		addMember(l,"Outlines",getOutlines,null,false);
		addMember(l,"_Outlines",get_Outlines,null,false);
		addMember(l,"Polygons",getPolygons,null,false);
		addMember(l,"_Polygons",get_Polygons,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CompositeCollider2D.GeometryType));
	}
}
