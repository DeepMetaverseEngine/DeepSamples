using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_AnimatorControllerParameterType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimatorControllerParameterType.Float);
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
			pushValue(l,(double)UnityEngine.AnimatorControllerParameterType.Float);
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
			pushValue(l,UnityEngine.AnimatorControllerParameterType.Int);
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
			pushValue(l,(double)UnityEngine.AnimatorControllerParameterType.Int);
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
			pushValue(l,UnityEngine.AnimatorControllerParameterType.Bool);
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
			pushValue(l,(double)UnityEngine.AnimatorControllerParameterType.Bool);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTrigger(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AnimatorControllerParameterType.Trigger);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Trigger(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AnimatorControllerParameterType.Trigger);
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
		getTypeTable(l,"UnityEngine.AnimatorControllerParameterType");
		addMember(l,"Float",getFloat,null,false);
		addMember(l,"_Float",get_Float,null,false);
		addMember(l,"Int",getInt,null,false);
		addMember(l,"_Int",get_Int,null,false);
		addMember(l,"Bool",getBool,null,false);
		addMember(l,"_Bool",get_Bool,null,false);
		addMember(l,"Trigger",getTrigger,null,false);
		addMember(l,"_Trigger",get_Trigger,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.AnimatorControllerParameterType));
	}
}
