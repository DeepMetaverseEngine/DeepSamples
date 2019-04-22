using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ApplicationSandboxType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUnknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationSandboxType.Unknown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Unknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationSandboxType.Unknown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNotSandboxed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationSandboxType.NotSandboxed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NotSandboxed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationSandboxType.NotSandboxed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSandboxed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationSandboxType.Sandboxed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Sandboxed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationSandboxType.Sandboxed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSandboxBroken(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationSandboxType.SandboxBroken);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SandboxBroken(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationSandboxType.SandboxBroken);
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
		getTypeTable(l,"UnityEngine.ApplicationSandboxType");
		addMember(l,"Unknown",getUnknown,null,false);
		addMember(l,"_Unknown",get_Unknown,null,false);
		addMember(l,"NotSandboxed",getNotSandboxed,null,false);
		addMember(l,"_NotSandboxed",get_NotSandboxed,null,false);
		addMember(l,"Sandboxed",getSandboxed,null,false);
		addMember(l,"_Sandboxed",get_Sandboxed,null,false);
		addMember(l,"SandboxBroken",getSandboxBroken,null,false);
		addMember(l,"_SandboxBroken",get_SandboxBroken,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ApplicationSandboxType));
	}
}
