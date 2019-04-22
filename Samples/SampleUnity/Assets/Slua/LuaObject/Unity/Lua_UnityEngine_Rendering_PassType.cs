using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Rendering_PassType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNormal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.Normal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Normal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.Normal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVertex(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.Vertex);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Vertex(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.Vertex);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVertexLM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.VertexLM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VertexLM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.VertexLM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getForwardBase(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.ForwardBase);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ForwardBase(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.ForwardBase);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getForwardAdd(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.ForwardAdd);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ForwardAdd(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.ForwardAdd);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLightPrePassBase(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.LightPrePassBase);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LightPrePassBase(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.LightPrePassBase);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLightPrePassFinal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.LightPrePassFinal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LightPrePassFinal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.LightPrePassFinal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getShadowCaster(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.ShadowCaster);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ShadowCaster(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.ShadowCaster);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDeferred(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.Deferred);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Deferred(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.Deferred);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMeta(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.PassType.Meta);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Meta(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.PassType.Meta);
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
			pushValue(l,UnityEngine.Rendering.PassType.MotionVectors);
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
			pushValue(l,(double)UnityEngine.Rendering.PassType.MotionVectors);
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
		getTypeTable(l,"UnityEngine.Rendering.PassType");
		addMember(l,"Normal",getNormal,null,false);
		addMember(l,"_Normal",get_Normal,null,false);
		addMember(l,"Vertex",getVertex,null,false);
		addMember(l,"_Vertex",get_Vertex,null,false);
		addMember(l,"VertexLM",getVertexLM,null,false);
		addMember(l,"_VertexLM",get_VertexLM,null,false);
		addMember(l,"ForwardBase",getForwardBase,null,false);
		addMember(l,"_ForwardBase",get_ForwardBase,null,false);
		addMember(l,"ForwardAdd",getForwardAdd,null,false);
		addMember(l,"_ForwardAdd",get_ForwardAdd,null,false);
		addMember(l,"LightPrePassBase",getLightPrePassBase,null,false);
		addMember(l,"_LightPrePassBase",get_LightPrePassBase,null,false);
		addMember(l,"LightPrePassFinal",getLightPrePassFinal,null,false);
		addMember(l,"_LightPrePassFinal",get_LightPrePassFinal,null,false);
		addMember(l,"ShadowCaster",getShadowCaster,null,false);
		addMember(l,"_ShadowCaster",get_ShadowCaster,null,false);
		addMember(l,"Deferred",getDeferred,null,false);
		addMember(l,"_Deferred",get_Deferred,null,false);
		addMember(l,"Meta",getMeta,null,false);
		addMember(l,"_Meta",get_Meta,null,false);
		addMember(l,"MotionVectors",getMotionVectors,null,false);
		addMember(l,"_MotionVectors",get_MotionVectors,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Rendering.PassType));
	}
}
