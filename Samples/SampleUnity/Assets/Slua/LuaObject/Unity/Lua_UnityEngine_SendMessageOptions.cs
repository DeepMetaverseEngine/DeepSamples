using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_SendMessageOptions : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRequireReceiver(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SendMessageOptions.RequireReceiver);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RequireReceiver(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SendMessageOptions.RequireReceiver);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDontRequireReceiver(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SendMessageOptions.DontRequireReceiver);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DontRequireReceiver(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SendMessageOptions.DontRequireReceiver);
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
		getTypeTable(l,"UnityEngine.SendMessageOptions");
		addMember(l,"RequireReceiver",getRequireReceiver,null,false);
		addMember(l,"_RequireReceiver",get_RequireReceiver,null,false);
		addMember(l,"DontRequireReceiver",getDontRequireReceiver,null,false);
		addMember(l,"_DontRequireReceiver",get_DontRequireReceiver,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.SendMessageOptions));
	}
}
