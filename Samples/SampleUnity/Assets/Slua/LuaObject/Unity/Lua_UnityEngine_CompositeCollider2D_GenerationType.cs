using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CompositeCollider2D_GenerationType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSynchronous(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CompositeCollider2D.GenerationType.Synchronous);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Synchronous(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CompositeCollider2D.GenerationType.Synchronous);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getManual(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CompositeCollider2D.GenerationType.Manual);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Manual(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CompositeCollider2D.GenerationType.Manual);
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
		getTypeTable(l,"UnityEngine.CompositeCollider2D.GenerationType");
		addMember(l,"Synchronous",getSynchronous,null,false);
		addMember(l,"_Synchronous",get_Synchronous,null,false);
		addMember(l,"Manual",getManual,null,false);
		addMember(l,"_Manual",get_Manual,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CompositeCollider2D.GenerationType));
	}
}
