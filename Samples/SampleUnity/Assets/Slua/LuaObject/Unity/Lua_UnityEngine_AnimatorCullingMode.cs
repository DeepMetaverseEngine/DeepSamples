using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_AnimatorCullingMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAlwaysAnimate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimatorCullingMode.AlwaysAnimate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AlwaysAnimate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AnimatorCullingMode.AlwaysAnimate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCullUpdateTransforms(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimatorCullingMode.CullUpdateTransforms);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CullUpdateTransforms(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AnimatorCullingMode.CullUpdateTransforms);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCullCompletely(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimatorCullingMode.CullCompletely);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CullCompletely(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AnimatorCullingMode.CullCompletely);
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
		getTypeTable(l,"UnityEngine.AnimatorCullingMode");
		addMember(l,"AlwaysAnimate",getAlwaysAnimate,null,false);
		addMember(l,"_AlwaysAnimate",get_AlwaysAnimate,null,false);
		addMember(l,"CullUpdateTransforms",getCullUpdateTransforms,null,false);
		addMember(l,"_CullUpdateTransforms",get_CullUpdateTransforms,null,false);
		addMember(l,"CullCompletely",getCullCompletely,null,false);
		addMember(l,"_CullCompletely",get_CullCompletely,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.AnimatorCullingMode));
	}
}
