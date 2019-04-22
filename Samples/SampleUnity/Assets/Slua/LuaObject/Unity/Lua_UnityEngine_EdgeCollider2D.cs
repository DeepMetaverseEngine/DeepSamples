using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_EdgeCollider2D : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Reset(IntPtr l) {
		try {
			UnityEngine.EdgeCollider2D self=(UnityEngine.EdgeCollider2D)checkSelf(l);
			self.Reset();
			pushValue(l,true);
			return 1;
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_points(IntPtr l) {
		try {
			UnityEngine.EdgeCollider2D self=(UnityEngine.EdgeCollider2D)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.points);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_points(IntPtr l) {
		try {
			UnityEngine.EdgeCollider2D self=(UnityEngine.EdgeCollider2D)checkSelf(l);
			UnityEngine.Vector2[] v;
			checkArray(l,2,out v);
			self.points=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_edgeRadius(IntPtr l) {
		try {
			UnityEngine.EdgeCollider2D self=(UnityEngine.EdgeCollider2D)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.edgeRadius);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_edgeRadius(IntPtr l) {
		try {
			UnityEngine.EdgeCollider2D self=(UnityEngine.EdgeCollider2D)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.edgeRadius=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_edgeCount(IntPtr l) {
		try {
			UnityEngine.EdgeCollider2D self=(UnityEngine.EdgeCollider2D)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.edgeCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_pointCount(IntPtr l) {
		try {
			UnityEngine.EdgeCollider2D self=(UnityEngine.EdgeCollider2D)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.pointCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.EdgeCollider2D");
		addMember(l,Reset);
		addMember(l,"points",get_points,set_points,true);
		addMember(l,"edgeRadius",get_edgeRadius,set_edgeRadius,true);
		addMember(l,"edgeCount",get_edgeCount,null,true);
		addMember(l,"pointCount",get_pointCount,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.EdgeCollider2D),typeof(UnityEngine.Collider2D));
	}
}
