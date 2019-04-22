using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_MeshTopology : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTriangles(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MeshTopology.Triangles);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Triangles(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MeshTopology.Triangles);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getQuads(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MeshTopology.Quads);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Quads(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MeshTopology.Quads);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLines(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MeshTopology.Lines);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Lines(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MeshTopology.Lines);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLineStrip(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MeshTopology.LineStrip);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LineStrip(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MeshTopology.LineStrip);
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
			pushValue(l,UnityEngine.MeshTopology.Points);
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
			pushValue(l,(double)UnityEngine.MeshTopology.Points);
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
		getTypeTable(l,"UnityEngine.MeshTopology");
		addMember(l,"Triangles",getTriangles,null,false);
		addMember(l,"_Triangles",get_Triangles,null,false);
		addMember(l,"Quads",getQuads,null,false);
		addMember(l,"_Quads",get_Quads,null,false);
		addMember(l,"Lines",getLines,null,false);
		addMember(l,"_Lines",get_Lines,null,false);
		addMember(l,"LineStrip",getLineStrip,null,false);
		addMember(l,"_LineStrip",get_LineStrip,null,false);
		addMember(l,"Points",getPoints,null,false);
		addMember(l,"_Points",get_Points,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.MeshTopology));
	}
}
