using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_CanvasUpdate : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPrelayout(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasUpdate.Prelayout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Prelayout(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasUpdate.Prelayout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLayout(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasUpdate.Layout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Layout(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasUpdate.Layout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPostLayout(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasUpdate.PostLayout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PostLayout(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasUpdate.PostLayout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPreRender(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasUpdate.PreRender);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PreRender(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasUpdate.PreRender);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLatePreRender(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasUpdate.LatePreRender);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LatePreRender(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasUpdate.LatePreRender);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMaxUpdateValue(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.CanvasUpdate.MaxUpdateValue);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MaxUpdateValue(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.CanvasUpdate.MaxUpdateValue);
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
		getTypeTable(l,"UnityEngine.UI.CanvasUpdate");
		addMember(l,"Prelayout",getPrelayout,null,false);
		addMember(l,"_Prelayout",get_Prelayout,null,false);
		addMember(l,"Layout",getLayout,null,false);
		addMember(l,"_Layout",get_Layout,null,false);
		addMember(l,"PostLayout",getPostLayout,null,false);
		addMember(l,"_PostLayout",get_PostLayout,null,false);
		addMember(l,"PreRender",getPreRender,null,false);
		addMember(l,"_PreRender",get_PreRender,null,false);
		addMember(l,"LatePreRender",getLatePreRender,null,false);
		addMember(l,"_LatePreRender",get_LatePreRender,null,false);
		addMember(l,"MaxUpdateValue",getMaxUpdateValue,null,false);
		addMember(l,"_MaxUpdateValue",get_MaxUpdateValue,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.CanvasUpdate));
	}
}
