using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RenderMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getScreenSpaceOverlay(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderMode.ScreenSpaceOverlay);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScreenSpaceOverlay(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderMode.ScreenSpaceOverlay);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getScreenSpaceCamera(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderMode.ScreenSpaceCamera);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScreenSpaceCamera(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderMode.ScreenSpaceCamera);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWorldSpace(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderMode.WorldSpace);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WorldSpace(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderMode.WorldSpace);
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
		getTypeTable(l,"UnityEngine.RenderMode");
		addMember(l,"ScreenSpaceOverlay",getScreenSpaceOverlay,null,false);
		addMember(l,"_ScreenSpaceOverlay",get_ScreenSpaceOverlay,null,false);
		addMember(l,"ScreenSpaceCamera",getScreenSpaceCamera,null,false);
		addMember(l,"_ScreenSpaceCamera",get_ScreenSpaceCamera,null,false);
		addMember(l,"WorldSpace",getWorldSpace,null,false);
		addMember(l,"_WorldSpace",get_WorldSpace,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RenderMode));
	}
}
