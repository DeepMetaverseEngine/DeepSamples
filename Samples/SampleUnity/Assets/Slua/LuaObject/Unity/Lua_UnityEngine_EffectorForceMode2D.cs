using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_EffectorForceMode2D : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getConstant(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EffectorForceMode2D.Constant);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Constant(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EffectorForceMode2D.Constant);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInverseLinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EffectorForceMode2D.InverseLinear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InverseLinear(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EffectorForceMode2D.InverseLinear);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInverseSquared(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EffectorForceMode2D.InverseSquared);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InverseSquared(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EffectorForceMode2D.InverseSquared);
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
		getTypeTable(l,"UnityEngine.EffectorForceMode2D");
		addMember(l,"Constant",getConstant,null,false);
		addMember(l,"_Constant",get_Constant,null,false);
		addMember(l,"InverseLinear",getInverseLinear,null,false);
		addMember(l,"_InverseLinear",get_InverseLinear,null,false);
		addMember(l,"InverseSquared",getInverseSquared,null,false);
		addMember(l,"_InverseSquared",get_InverseSquared,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.EffectorForceMode2D));
	}
}
