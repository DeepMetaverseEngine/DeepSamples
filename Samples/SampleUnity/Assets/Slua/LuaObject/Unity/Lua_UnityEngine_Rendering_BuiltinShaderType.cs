using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Rendering_BuiltinShaderType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDeferredShading(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.BuiltinShaderType.DeferredShading);
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
			pushValue(l,(double)UnityEngine.Rendering.BuiltinShaderType.DeferredShading);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDeferredReflections(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.BuiltinShaderType.DeferredReflections);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DeferredReflections(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.BuiltinShaderType.DeferredReflections);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLegacyDeferredLighting(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.BuiltinShaderType.LegacyDeferredLighting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LegacyDeferredLighting(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.BuiltinShaderType.LegacyDeferredLighting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getScreenSpaceShadows(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.BuiltinShaderType.ScreenSpaceShadows);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScreenSpaceShadows(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.BuiltinShaderType.ScreenSpaceShadows);
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
			pushValue(l,UnityEngine.Rendering.BuiltinShaderType.DepthNormals);
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
			pushValue(l,(double)UnityEngine.Rendering.BuiltinShaderType.DepthNormals);
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
			pushValue(l,UnityEngine.Rendering.BuiltinShaderType.MotionVectors);
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
			pushValue(l,(double)UnityEngine.Rendering.BuiltinShaderType.MotionVectors);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLightHalo(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.BuiltinShaderType.LightHalo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LightHalo(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.BuiltinShaderType.LightHalo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLensFlare(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.BuiltinShaderType.LensFlare);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LensFlare(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.BuiltinShaderType.LensFlare);
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
		getTypeTable(l,"UnityEngine.Rendering.BuiltinShaderType");
		addMember(l,"DeferredShading",getDeferredShading,null,false);
		addMember(l,"_DeferredShading",get_DeferredShading,null,false);
		addMember(l,"DeferredReflections",getDeferredReflections,null,false);
		addMember(l,"_DeferredReflections",get_DeferredReflections,null,false);
		addMember(l,"LegacyDeferredLighting",getLegacyDeferredLighting,null,false);
		addMember(l,"_LegacyDeferredLighting",get_LegacyDeferredLighting,null,false);
		addMember(l,"ScreenSpaceShadows",getScreenSpaceShadows,null,false);
		addMember(l,"_ScreenSpaceShadows",get_ScreenSpaceShadows,null,false);
		addMember(l,"DepthNormals",getDepthNormals,null,false);
		addMember(l,"_DepthNormals",get_DepthNormals,null,false);
		addMember(l,"MotionVectors",getMotionVectors,null,false);
		addMember(l,"_MotionVectors",get_MotionVectors,null,false);
		addMember(l,"LightHalo",getLightHalo,null,false);
		addMember(l,"_LightHalo",get_LightHalo,null,false);
		addMember(l,"LensFlare",getLensFlare,null,false);
		addMember(l,"_LensFlare",get_LensFlare,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Rendering.BuiltinShaderType));
	}
}
