using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GUI_Display_FontStyle : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSTYLE_PLAIN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Display.FontStyle.STYLE_PLAIN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_STYLE_PLAIN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Display.FontStyle.STYLE_PLAIN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSTYLE_BOLD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Display.FontStyle.STYLE_BOLD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_STYLE_BOLD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Display.FontStyle.STYLE_BOLD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSTYLE_ITALIC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Display.FontStyle.STYLE_ITALIC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_STYLE_ITALIC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Display.FontStyle.STYLE_ITALIC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSTYLE_UNDERLINED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Display.FontStyle.STYLE_UNDERLINED);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_STYLE_UNDERLINED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Display.FontStyle.STYLE_UNDERLINED);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSTYLE_BOLD_UNDERLINED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Display.FontStyle.STYLE_BOLD_UNDERLINED);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_STYLE_BOLD_UNDERLINED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Display.FontStyle.STYLE_BOLD_UNDERLINED);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSTYLE_ITALIC_UNDERLINED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Display.FontStyle.STYLE_ITALIC_UNDERLINED);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_STYLE_ITALIC_UNDERLINED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Display.FontStyle.STYLE_ITALIC_UNDERLINED);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSTYLE_BOLD_ITALIC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Display.FontStyle.STYLE_BOLD_ITALIC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_STYLE_BOLD_ITALIC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Display.FontStyle.STYLE_BOLD_ITALIC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSTYLE_BOLD_ITALIC_UNDERLINED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GUI.Display.FontStyle.STYLE_BOLD_ITALIC_UNDERLINED);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_STYLE_BOLD_ITALIC_UNDERLINED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.GUI.Display.FontStyle.STYLE_BOLD_ITALIC_UNDERLINED);
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
		getTypeTable(l,"UGUIFontStyle");
		addMember(l,"STYLE_PLAIN",getSTYLE_PLAIN,null,false);
		addMember(l,"_STYLE_PLAIN",get_STYLE_PLAIN,null,false);
		addMember(l,"STYLE_BOLD",getSTYLE_BOLD,null,false);
		addMember(l,"_STYLE_BOLD",get_STYLE_BOLD,null,false);
		addMember(l,"STYLE_ITALIC",getSTYLE_ITALIC,null,false);
		addMember(l,"_STYLE_ITALIC",get_STYLE_ITALIC,null,false);
		addMember(l,"STYLE_UNDERLINED",getSTYLE_UNDERLINED,null,false);
		addMember(l,"_STYLE_UNDERLINED",get_STYLE_UNDERLINED,null,false);
		addMember(l,"STYLE_BOLD_UNDERLINED",getSTYLE_BOLD_UNDERLINED,null,false);
		addMember(l,"_STYLE_BOLD_UNDERLINED",get_STYLE_BOLD_UNDERLINED,null,false);
		addMember(l,"STYLE_ITALIC_UNDERLINED",getSTYLE_ITALIC_UNDERLINED,null,false);
		addMember(l,"_STYLE_ITALIC_UNDERLINED",get_STYLE_ITALIC_UNDERLINED,null,false);
		addMember(l,"STYLE_BOLD_ITALIC",getSTYLE_BOLD_ITALIC,null,false);
		addMember(l,"_STYLE_BOLD_ITALIC",get_STYLE_BOLD_ITALIC,null,false);
		addMember(l,"STYLE_BOLD_ITALIC_UNDERLINED",getSTYLE_BOLD_ITALIC_UNDERLINED,null,false);
		addMember(l,"_STYLE_BOLD_ITALIC_UNDERLINED",get_STYLE_BOLD_ITALIC_UNDERLINED,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.GUI.Display.FontStyle));
	}
}
