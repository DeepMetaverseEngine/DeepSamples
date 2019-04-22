using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RenderTextureReadWrite : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDefault(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureReadWrite.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Default(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureReadWrite.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureReadWrite.Linear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Linear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureReadWrite.Linear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getsRGB(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureReadWrite.sRGB);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_sRGB(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureReadWrite.sRGB);
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
		getTypeTable(l,"UnityEngine.RenderTextureReadWrite");
		addMember(l,"Default",getDefault,null,false);
		addMember(l,"_Default",get_Default,null,false);
		addMember(l,"Linear",getLinear,null,false);
		addMember(l,"_Linear",get_Linear,null,false);
		addMember(l,"sRGB",getsRGB,null,false);
		addMember(l,"_sRGB",get_sRGB,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RenderTextureReadWrite));
	}
}
