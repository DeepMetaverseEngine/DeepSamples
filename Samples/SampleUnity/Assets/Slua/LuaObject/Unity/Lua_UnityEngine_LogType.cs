using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_LogType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getError(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.LogType.Error);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Error(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.LogType.Error);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAssert(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.LogType.Assert);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Assert(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.LogType.Assert);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWarning(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.LogType.Warning);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Warning(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.LogType.Warning);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLog(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.LogType.Log);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Log(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.LogType.Log);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getException(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.LogType.Exception);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Exception(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.LogType.Exception);
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
		getTypeTable(l,"UnityEngine.LogType");
		addMember(l,"Error",getError,null,false);
		addMember(l,"_Error",get_Error,null,false);
		addMember(l,"Assert",getAssert,null,false);
		addMember(l,"_Assert",get_Assert,null,false);
		addMember(l,"Warning",getWarning,null,false);
		addMember(l,"_Warning",get_Warning,null,false);
		addMember(l,"Log",getLog,null,false);
		addMember(l,"_Log",get_Log,null,false);
		addMember(l,"Exception",getException,null,false);
		addMember(l,"_Exception",get_Exception,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.LogType));
	}
}
