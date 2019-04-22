using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CollisionDetectionMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDiscrete(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionDetectionMode.Discrete);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Discrete(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionDetectionMode.Discrete);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getContinuous(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionDetectionMode.Continuous);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Continuous(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionDetectionMode.Continuous);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getContinuousDynamic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionDetectionMode.ContinuousDynamic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ContinuousDynamic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionDetectionMode.ContinuousDynamic);
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
		getTypeTable(l,"UnityEngine.CollisionDetectionMode");
		addMember(l,"Discrete",getDiscrete,null,false);
		addMember(l,"_Discrete",get_Discrete,null,false);
		addMember(l,"Continuous",getContinuous,null,false);
		addMember(l,"_Continuous",get_Continuous,null,false);
		addMember(l,"ContinuousDynamic",getContinuousDynamic,null,false);
		addMember(l,"_ContinuousDynamic",get_ContinuousDynamic,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CollisionDetectionMode));
	}
}
