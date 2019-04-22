using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_TextureWrapMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRepeat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TextureWrapMode.Repeat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Repeat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TextureWrapMode.Repeat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getClamp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TextureWrapMode.Clamp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Clamp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TextureWrapMode.Clamp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMirror(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TextureWrapMode.Mirror);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Mirror(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TextureWrapMode.Mirror);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMirrorOnce(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TextureWrapMode.MirrorOnce);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MirrorOnce(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TextureWrapMode.MirrorOnce);
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
		getTypeTable(l,"UnityEngine.TextureWrapMode");
		addMember(l,"Repeat",getRepeat,null,false);
		addMember(l,"_Repeat",get_Repeat,null,false);
		addMember(l,"Clamp",getClamp,null,false);
		addMember(l,"_Clamp",get_Clamp,null,false);
		addMember(l,"Mirror",getMirror,null,false);
		addMember(l,"_Mirror",get_Mirror,null,false);
		addMember(l,"MirrorOnce",getMirrorOnce,null,false);
		addMember(l,"_MirrorOnce",get_MirrorOnce,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.TextureWrapMode));
	}
}
