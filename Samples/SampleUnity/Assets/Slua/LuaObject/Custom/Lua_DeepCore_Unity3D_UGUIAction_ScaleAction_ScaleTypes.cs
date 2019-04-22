using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIAction_ScaleAction_ScaleTypes : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDefault(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIAction.ScaleAction.ScaleTypes.Default);
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
			pushValue(l,(double)DeepCore.Unity3D.UGUIAction.ScaleAction.ScaleTypes.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIAction.ScaleAction.ScaleTypes.Center);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Center(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.Unity3D.UGUIAction.ScaleAction.ScaleTypes.Center);
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
		getTypeTable(l,"ScaleAction.ScaleTypes");
		addMember(l,"Default",getDefault,null,false);
		addMember(l,"_Default",get_Default,null,false);
		addMember(l,"Center",getCenter,null,false);
		addMember(l,"_Center",get_Center,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UGUIAction.ScaleAction.ScaleTypes));
	}
}
