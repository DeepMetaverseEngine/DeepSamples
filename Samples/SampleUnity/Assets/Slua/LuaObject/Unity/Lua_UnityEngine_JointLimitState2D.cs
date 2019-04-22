using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_JointLimitState2D : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInactive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.JointLimitState2D.Inactive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Inactive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.JointLimitState2D.Inactive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLowerLimit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.JointLimitState2D.LowerLimit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LowerLimit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.JointLimitState2D.LowerLimit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUpperLimit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.JointLimitState2D.UpperLimit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UpperLimit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.JointLimitState2D.UpperLimit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEqualLimits(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.JointLimitState2D.EqualLimits);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EqualLimits(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.JointLimitState2D.EqualLimits);
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
		getTypeTable(l,"UnityEngine.JointLimitState2D");
		addMember(l,"Inactive",getInactive,null,false);
		addMember(l,"_Inactive",get_Inactive,null,false);
		addMember(l,"LowerLimit",getLowerLimit,null,false);
		addMember(l,"_LowerLimit",get_LowerLimit,null,false);
		addMember(l,"UpperLimit",getUpperLimit,null,false);
		addMember(l,"_UpperLimit",get_UpperLimit,null,false);
		addMember(l,"EqualLimits",getEqualLimits,null,false);
		addMember(l,"_EqualLimits",get_EqualLimits,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.JointLimitState2D));
	}
}
