using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CameraClearFlags : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSkybox(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CameraClearFlags.Skybox);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Skybox(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CameraClearFlags.Skybox);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSolidColor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CameraClearFlags.SolidColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SolidColor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CameraClearFlags.SolidColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getColor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CameraClearFlags.Color);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Color(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CameraClearFlags.Color);
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
			pushValue(l,UnityEngine.CameraClearFlags.Depth);
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
			pushValue(l,(double)UnityEngine.CameraClearFlags.Depth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNothing(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CameraClearFlags.Nothing);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Nothing(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CameraClearFlags.Nothing);
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
		getTypeTable(l,"UnityEngine.CameraClearFlags");
		addMember(l,"Skybox",getSkybox,null,false);
		addMember(l,"_Skybox",get_Skybox,null,false);
		addMember(l,"SolidColor",getSolidColor,null,false);
		addMember(l,"_SolidColor",get_SolidColor,null,false);
		addMember(l,"Color",getColor,null,false);
		addMember(l,"_Color",get_Color,null,false);
		addMember(l,"Depth",getDepth,null,false);
		addMember(l,"_Depth",get_Depth,null,false);
		addMember(l,"Nothing",getNothing,null,false);
		addMember(l,"_Nothing",get_Nothing,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CameraClearFlags));
	}
}
