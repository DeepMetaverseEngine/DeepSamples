using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Rendering_ReflectionProbeUsage : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.ReflectionProbeUsage.Off);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Off(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.ReflectionProbeUsage.Off);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBlendProbes(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.ReflectionProbeUsage.BlendProbes);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BlendProbes(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.ReflectionProbeUsage.BlendProbes);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBlendProbesAndSkybox(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.ReflectionProbeUsage.BlendProbesAndSkybox);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BlendProbesAndSkybox(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.ReflectionProbeUsage.BlendProbesAndSkybox);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSimple(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Rendering.ReflectionProbeUsage.Simple);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Simple(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Rendering.ReflectionProbeUsage.Simple);
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
		getTypeTable(l,"UnityEngine.Rendering.ReflectionProbeUsage");
		addMember(l,"Off",getOff,null,false);
		addMember(l,"_Off",get_Off,null,false);
		addMember(l,"BlendProbes",getBlendProbes,null,false);
		addMember(l,"_BlendProbes",get_BlendProbes,null,false);
		addMember(l,"BlendProbesAndSkybox",getBlendProbesAndSkybox,null,false);
		addMember(l,"_BlendProbesAndSkybox",get_BlendProbesAndSkybox,null,false);
		addMember(l,"Simple",getSimple,null,false);
		addMember(l,"_Simple",get_Simple,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Rendering.ReflectionProbeUsage));
	}
}
