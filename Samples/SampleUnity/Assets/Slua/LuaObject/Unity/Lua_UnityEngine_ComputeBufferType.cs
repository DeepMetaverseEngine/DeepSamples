using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ComputeBufferType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDefault(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ComputeBufferType.Default);
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
			pushValue(l,(double)UnityEngine.ComputeBufferType.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRaw(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ComputeBufferType.Raw);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Raw(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ComputeBufferType.Raw);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAppend(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ComputeBufferType.Append);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Append(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ComputeBufferType.Append);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCounter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ComputeBufferType.Counter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Counter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ComputeBufferType.Counter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getIndirectArguments(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ComputeBufferType.IndirectArguments);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IndirectArguments(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ComputeBufferType.IndirectArguments);
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
		getTypeTable(l,"UnityEngine.ComputeBufferType");
		addMember(l,"Default",getDefault,null,false);
		addMember(l,"_Default",get_Default,null,false);
		addMember(l,"Raw",getRaw,null,false);
		addMember(l,"_Raw",get_Raw,null,false);
		addMember(l,"Append",getAppend,null,false);
		addMember(l,"_Append",get_Append,null,false);
		addMember(l,"Counter",getCounter,null,false);
		addMember(l,"_Counter",get_Counter,null,false);
		addMember(l,"IndirectArguments",getIndirectArguments,null,false);
		addMember(l,"_IndirectArguments",get_IndirectArguments,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ComputeBufferType));
	}
}
