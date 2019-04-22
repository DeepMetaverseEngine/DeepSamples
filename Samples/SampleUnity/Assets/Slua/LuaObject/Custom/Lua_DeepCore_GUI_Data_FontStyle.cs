using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GUI_Data_FontStyle : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNormal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Data.FontStyle.Normal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Normal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Data.FontStyle.Normal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBold(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Data.FontStyle.Bold);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Bold(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Data.FontStyle.Bold);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getItalic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Data.FontStyle.Italic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Italic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Data.FontStyle.Italic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBoldAndItalic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Data.FontStyle.BoldAndItalic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BoldAndItalic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Data.FontStyle.BoldAndItalic);
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
		getTypeTable(l,"FontStyle");
		addMember(l,"Normal",getNormal,null,false);
		addMember(l,"_Normal",get_Normal,null,false);
		addMember(l,"Bold",getBold,null,false);
		addMember(l,"_Bold",get_Bold,null,false);
		addMember(l,"Italic",getItalic,null,false);
		addMember(l,"_Italic",get_Italic,null,false);
		addMember(l,"BoldAndItalic",getBoldAndItalic,null,false);
		addMember(l,"_BoldAndItalic",get_BoldAndItalic,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.GUI.Data.FontStyle));
	}
}
