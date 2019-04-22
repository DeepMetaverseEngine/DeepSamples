using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_DepthTextureMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DepthTextureMode.None);
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
			pushValue(l,(double)UnityEngine.DepthTextureMode.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDepth(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DepthTextureMode.Depth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Depth(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DepthTextureMode.Depth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDepthNormals(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DepthTextureMode.DepthNormals);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DepthNormals(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DepthTextureMode.DepthNormals);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMotionVectors(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DepthTextureMode.MotionVectors);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MotionVectors(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DepthTextureMode.MotionVectors);
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
		getTypeTable(l,"UnityEngine.DepthTextureMode");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"Depth",getDepth,null,false);
		addMember(l,"_Depth",get_Depth,null,false);
		addMember(l,"DepthNormals",getDepthNormals,null,false);
		addMember(l,"_DepthNormals",get_DepthNormals,null,false);
		addMember(l,"MotionVectors",getMotionVectors,null,false);
		addMember(l,"_MotionVectors",get_MotionVectors,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.DepthTextureMode));
	}
}
