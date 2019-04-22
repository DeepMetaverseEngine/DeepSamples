using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RectTransform_Edge : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RectTransform.Edge.Left);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Left(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RectTransform.Edge.Left);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RectTransform.Edge.Right);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Right(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RectTransform.Edge.Right);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RectTransform.Edge.Top);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Top(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RectTransform.Edge.Top);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBottom(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RectTransform.Edge.Bottom);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Bottom(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RectTransform.Edge.Bottom);
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
		getTypeTable(l,"UnityEngine.RectTransform.Edge");
		addMember(l,"Left",getLeft,null,false);
		addMember(l,"_Left",get_Left,null,false);
		addMember(l,"Right",getRight,null,false);
		addMember(l,"_Right",get_Right,null,false);
		addMember(l,"Top",getTop,null,false);
		addMember(l,"_Top",get_Top,null,false);
		addMember(l,"Bottom",getBottom,null,false);
		addMember(l,"_Bottom",get_Bottom,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RectTransform.Edge));
	}
}
