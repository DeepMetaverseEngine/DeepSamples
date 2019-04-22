using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_Slider_Direction : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftToRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.Slider.Direction.LeftToRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftToRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.Slider.Direction.LeftToRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightToLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.Slider.Direction.RightToLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightToLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.Slider.Direction.RightToLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBottomToTop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.Slider.Direction.BottomToTop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BottomToTop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.Slider.Direction.BottomToTop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTopToBottom(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.Slider.Direction.TopToBottom);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TopToBottom(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.Slider.Direction.TopToBottom);
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
		getTypeTable(l,"UnityEngine.UI.Slider.Direction");
		addMember(l,"LeftToRight",getLeftToRight,null,false);
		addMember(l,"_LeftToRight",get_LeftToRight,null,false);
		addMember(l,"RightToLeft",getRightToLeft,null,false);
		addMember(l,"_RightToLeft",get_RightToLeft,null,false);
		addMember(l,"BottomToTop",getBottomToTop,null,false);
		addMember(l,"_BottomToTop",get_BottomToTop,null,false);
		addMember(l,"TopToBottom",getTopToBottom,null,false);
		addMember(l,"_TopToBottom",get_TopToBottom,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.Slider.Direction));
	}
}
