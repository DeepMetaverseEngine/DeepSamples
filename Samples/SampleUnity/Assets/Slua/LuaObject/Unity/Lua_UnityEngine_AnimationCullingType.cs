using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_AnimationCullingType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAlwaysAnimate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimationCullingType.AlwaysAnimate);
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
			pushValue(l,(double)UnityEngine.AnimationCullingType.AlwaysAnimate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBasedOnRenderers(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimationCullingType.BasedOnRenderers);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BasedOnRenderers(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AnimationCullingType.BasedOnRenderers);
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
		getTypeTable(l,"UnityEngine.AnimationCullingType");
		addMember(l,"AlwaysAnimate",getAlwaysAnimate,null,false);
		addMember(l,"_AlwaysAnimate",get_AlwaysAnimate,null,false);
		addMember(l,"BasedOnRenderers",getBasedOnRenderers,null,false);
		addMember(l,"_BasedOnRenderers",get_BasedOnRenderers,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.AnimationCullingType));
	}
}
