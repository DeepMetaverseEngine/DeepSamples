using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RenderingPath : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVertexLit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderingPath.VertexLit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VertexLit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderingPath.VertexLit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getForward(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderingPath.Forward);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Forward(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderingPath.Forward);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDeferredLighting(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderingPath.DeferredLighting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DeferredLighting(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderingPath.DeferredLighting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDeferredShading(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderingPath.DeferredShading);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DeferredShading(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderingPath.DeferredShading);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUsePlayerSettings(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderingPath.UsePlayerSettings);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UsePlayerSettings(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderingPath.UsePlayerSettings);
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
		getTypeTable(l,"UnityEngine.RenderingPath");
		addMember(l,"VertexLit",getVertexLit,null,false);
		addMember(l,"_VertexLit",get_VertexLit,null,false);
		addMember(l,"Forward",getForward,null,false);
		addMember(l,"_Forward",get_Forward,null,false);
		addMember(l,"DeferredLighting",getDeferredLighting,null,false);
		addMember(l,"_DeferredLighting",get_DeferredLighting,null,false);
		addMember(l,"DeferredShading",getDeferredShading,null,false);
		addMember(l,"_DeferredShading",get_DeferredShading,null,false);
		addMember(l,"UsePlayerSettings",getUsePlayerSettings,null,false);
		addMember(l,"_UsePlayerSettings",get_UsePlayerSettings,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RenderingPath));
	}
}
