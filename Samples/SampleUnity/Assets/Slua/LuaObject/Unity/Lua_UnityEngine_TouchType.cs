using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_TouchType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDirect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TouchType.Direct);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Direct(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TouchType.Direct);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getIndirect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TouchType.Indirect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Indirect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TouchType.Indirect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStylus(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TouchType.Stylus);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Stylus(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TouchType.Stylus);
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
		getTypeTable(l,"UnityEngine.TouchType");
		addMember(l,"Direct",getDirect,null,false);
		addMember(l,"_Direct",get_Direct,null,false);
		addMember(l,"Indirect",getIndirect,null,false);
		addMember(l,"_Indirect",get_Indirect,null,false);
		addMember(l,"Stylus",getStylus,null,false);
		addMember(l,"_Stylus",get_Stylus,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.TouchType));
	}
}
