using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Events_PersistentListenerMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEventDefined(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.PersistentListenerMode.EventDefined);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EventDefined(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.PersistentListenerMode.EventDefined);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVoid(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.PersistentListenerMode.Void);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Void(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.PersistentListenerMode.Void);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getObject(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.PersistentListenerMode.Object);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Object(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.PersistentListenerMode.Object);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInt(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.PersistentListenerMode.Int);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Int(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.PersistentListenerMode.Int);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.PersistentListenerMode.Float);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Float(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.PersistentListenerMode.Float);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getString(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.PersistentListenerMode.String);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_String(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.PersistentListenerMode.String);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBool(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Events.PersistentListenerMode.Bool);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Bool(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Events.PersistentListenerMode.Bool);
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
		getTypeTable(l,"UnityEngine.Events.PersistentListenerMode");
		addMember(l,"EventDefined",getEventDefined,null,false);
		addMember(l,"_EventDefined",get_EventDefined,null,false);
		addMember(l,"Void",getVoid,null,false);
		addMember(l,"_Void",get_Void,null,false);
		addMember(l,"Object",getObject,null,false);
		addMember(l,"_Object",get_Object,null,false);
		addMember(l,"Int",getInt,null,false);
		addMember(l,"_Int",get_Int,null,false);
		addMember(l,"Float",getFloat,null,false);
		addMember(l,"_Float",get_Float,null,false);
		addMember(l,"String",getString,null,false);
		addMember(l,"_String",get_String,null,false);
		addMember(l,"Bool",getBool,null,false);
		addMember(l,"_Bool",get_Bool,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Events.PersistentListenerMode));
	}
}
