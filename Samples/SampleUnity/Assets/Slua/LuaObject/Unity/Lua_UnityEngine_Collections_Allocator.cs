using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Collections_Allocator : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInvalid(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Collections.Allocator.Invalid);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Invalid(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Collections.Allocator.Invalid);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Collections.Allocator.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Collections.Allocator.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTemp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Collections.Allocator.Temp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Temp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Collections.Allocator.Temp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTempJob(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Collections.Allocator.TempJob);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TempJob(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Collections.Allocator.TempJob);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPersistent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Collections.Allocator.Persistent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Persistent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Collections.Allocator.Persistent);
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
		getTypeTable(l,"UnityEngine.Collections.Allocator");
		addMember(l,"Invalid",getInvalid,null,false);
		addMember(l,"_Invalid",get_Invalid,null,false);
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"Temp",getTemp,null,false);
		addMember(l,"_Temp",get_Temp,null,false);
		addMember(l,"TempJob",getTempJob,null,false);
		addMember(l,"_TempJob",get_TempJob,null,false);
		addMember(l,"Persistent",getPersistent,null,false);
		addMember(l,"_Persistent",get_Persistent,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Collections.Allocator));
	}
}
