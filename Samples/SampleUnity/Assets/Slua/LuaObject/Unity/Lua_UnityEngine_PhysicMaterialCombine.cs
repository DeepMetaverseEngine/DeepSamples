using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_PhysicMaterialCombine : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAverage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PhysicMaterialCombine.Average);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Average(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PhysicMaterialCombine.Average);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMultiply(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PhysicMaterialCombine.Multiply);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Multiply(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PhysicMaterialCombine.Multiply);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMinimum(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PhysicMaterialCombine.Minimum);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Minimum(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PhysicMaterialCombine.Minimum);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMaximum(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.PhysicMaterialCombine.Maximum);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Maximum(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.PhysicMaterialCombine.Maximum);
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
		getTypeTable(l,"UnityEngine.PhysicMaterialCombine");
		addMember(l,"Average",getAverage,null,false);
		addMember(l,"_Average",get_Average,null,false);
		addMember(l,"Multiply",getMultiply,null,false);
		addMember(l,"_Multiply",get_Multiply,null,false);
		addMember(l,"Minimum",getMinimum,null,false);
		addMember(l,"_Minimum",get_Minimum,null,false);
		addMember(l,"Maximum",getMaximum,null,false);
		addMember(l,"_Maximum",get_Maximum,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.PhysicMaterialCombine));
	}
}
