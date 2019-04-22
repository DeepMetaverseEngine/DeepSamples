using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ImagePosition : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getImageLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ImagePosition.ImageLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ImageLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ImagePosition.ImageLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getImageAbove(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ImagePosition.ImageAbove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ImageAbove(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ImagePosition.ImageAbove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getImageOnly(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ImagePosition.ImageOnly);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ImageOnly(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ImagePosition.ImageOnly);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTextOnly(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ImagePosition.TextOnly);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextOnly(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ImagePosition.TextOnly);
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
		getTypeTable(l,"UnityEngine.ImagePosition");
		addMember(l,"ImageLeft",getImageLeft,null,false);
		addMember(l,"_ImageLeft",get_ImageLeft,null,false);
		addMember(l,"ImageAbove",getImageAbove,null,false);
		addMember(l,"_ImageAbove",get_ImageAbove,null,false);
		addMember(l,"ImageOnly",getImageOnly,null,false);
		addMember(l,"_ImageOnly",get_ImageOnly,null,false);
		addMember(l,"TextOnly",getTextOnly,null,false);
		addMember(l,"_TextOnly",get_TextOnly,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ImagePosition));
	}
}
