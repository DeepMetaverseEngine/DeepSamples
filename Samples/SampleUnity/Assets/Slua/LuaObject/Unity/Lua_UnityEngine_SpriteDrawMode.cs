using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_SpriteDrawMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSimple(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteDrawMode.Simple);
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
			pushValue(l,(double)UnityEngine.SpriteDrawMode.Simple);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSliced(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteDrawMode.Sliced);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Sliced(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteDrawMode.Sliced);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTiled(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteDrawMode.Tiled);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Tiled(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteDrawMode.Tiled);
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
		getTypeTable(l,"UnityEngine.SpriteDrawMode");
		addMember(l,"Simple",getSimple,null,false);
		addMember(l,"_Simple",get_Simple,null,false);
		addMember(l,"Sliced",getSliced,null,false);
		addMember(l,"_Sliced",get_Sliced,null,false);
		addMember(l,"Tiled",getTiled,null,false);
		addMember(l,"_Tiled",get_Tiled,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.SpriteDrawMode));
	}
}
