using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ForceMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getForce(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ForceMode.Force);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Force(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ForceMode.Force);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getImpulse(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ForceMode.Impulse);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Impulse(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ForceMode.Impulse);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVelocityChange(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ForceMode.VelocityChange);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VelocityChange(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ForceMode.VelocityChange);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAcceleration(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ForceMode.Acceleration);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Acceleration(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ForceMode.Acceleration);
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
		getTypeTable(l,"UnityEngine.ForceMode");
		addMember(l,"Force",getForce,null,false);
		addMember(l,"_Force",get_Force,null,false);
		addMember(l,"Impulse",getImpulse,null,false);
		addMember(l,"_Impulse",get_Impulse,null,false);
		addMember(l,"VelocityChange",getVelocityChange,null,false);
		addMember(l,"_VelocityChange",get_VelocityChange,null,false);
		addMember(l,"Acceleration",getAcceleration,null,false);
		addMember(l,"_Acceleration",get_Acceleration,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ForceMode));
	}
}
