using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_HorizontalWrapMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWrap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HorizontalWrapMode.Wrap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Wrap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HorizontalWrapMode.Wrap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOverflow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HorizontalWrapMode.Overflow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Overflow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HorizontalWrapMode.Overflow);
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
		getTypeTable(l,"UnityEngine.HorizontalWrapMode");
		addMember(l,"Wrap",getWrap,null,false);
		addMember(l,"_Wrap",get_Wrap,null,false);
		addMember(l,"Overflow",getOverflow,null,false);
		addMember(l,"_Overflow",get_Overflow,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.HorizontalWrapMode));
	}
}
