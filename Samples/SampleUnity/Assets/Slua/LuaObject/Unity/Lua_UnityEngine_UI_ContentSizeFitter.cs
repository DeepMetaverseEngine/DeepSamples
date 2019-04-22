using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_ContentSizeFitter : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetLayoutHorizontal(IntPtr l) {
		try {
			UnityEngine.UI.ContentSizeFitter self=(UnityEngine.UI.ContentSizeFitter)checkSelf(l);
			self.SetLayoutHorizontal();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetLayoutVertical(IntPtr l) {
		try {
			UnityEngine.UI.ContentSizeFitter self=(UnityEngine.UI.ContentSizeFitter)checkSelf(l);
			self.SetLayoutVertical();
			pushValue(l,true);
			return 1;
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_horizontalFit(IntPtr l) {
		try {
			UnityEngine.UI.ContentSizeFitter self=(UnityEngine.UI.ContentSizeFitter)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.horizontalFit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_horizontalFit(IntPtr l) {
		try {
			UnityEngine.UI.ContentSizeFitter self=(UnityEngine.UI.ContentSizeFitter)checkSelf(l);
			UnityEngine.UI.ContentSizeFitter.FitMode v;
			checkEnum(l,2,out v);
			self.horizontalFit=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_verticalFit(IntPtr l) {
		try {
			UnityEngine.UI.ContentSizeFitter self=(UnityEngine.UI.ContentSizeFitter)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.verticalFit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_verticalFit(IntPtr l) {
		try {
			UnityEngine.UI.ContentSizeFitter self=(UnityEngine.UI.ContentSizeFitter)checkSelf(l);
			UnityEngine.UI.ContentSizeFitter.FitMode v;
			checkEnum(l,2,out v);
			self.verticalFit=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.UI.ContentSizeFitter");
		addMember(l,SetLayoutHorizontal);
		addMember(l,SetLayoutVertical);
		addMember(l,"horizontalFit",get_horizontalFit,set_horizontalFit,true);
		addMember(l,"verticalFit",get_verticalFit,set_verticalFit,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.ContentSizeFitter),typeof(UnityEngine.EventSystems.UIBehaviour));
	}
}
