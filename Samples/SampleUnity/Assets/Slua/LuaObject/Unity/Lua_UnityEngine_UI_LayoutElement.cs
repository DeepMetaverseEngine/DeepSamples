using System;
using SLua;
using System.Collections.Generic;
using DG.Tweening;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_LayoutElement : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CalculateLayoutInputHorizontal(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			self.CalculateLayoutInputHorizontal();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CalculateLayoutInputVertical(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			self.CalculateLayoutInputVertical();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DOFlexibleSize(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			UnityEngine.Vector2 a2;
			checkType(l,2,out a2);
			System.Single a3;
			checkType(l,3,out a3);
			System.Boolean a4;
			checkType(l,4,out a4);
			var ret=self.DOFlexibleSize(a2,a3,a4);
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
	static public int DOMinSize(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			UnityEngine.Vector2 a2;
			checkType(l,2,out a2);
			System.Single a3;
			checkType(l,3,out a3);
			System.Boolean a4;
			checkType(l,4,out a4);
			var ret=self.DOMinSize(a2,a3,a4);
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
	static public int DOPreferredSize(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			UnityEngine.Vector2 a2;
			checkType(l,2,out a2);
			System.Single a3;
			checkType(l,3,out a3);
			System.Boolean a4;
			checkType(l,4,out a4);
			var ret=self.DOPreferredSize(a2,a3,a4);
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
	static public int get_ignoreLayout(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ignoreLayout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ignoreLayout(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.ignoreLayout=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_minWidth(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.minWidth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_minWidth(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.minWidth=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_minHeight(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.minHeight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_minHeight(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.minHeight=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_preferredWidth(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.preferredWidth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_preferredWidth(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.preferredWidth=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_preferredHeight(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.preferredHeight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_preferredHeight(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.preferredHeight=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_flexibleWidth(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.flexibleWidth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_flexibleWidth(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.flexibleWidth=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_flexibleHeight(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.flexibleHeight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_flexibleHeight(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.flexibleHeight=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_layoutPriority(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.layoutPriority);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_layoutPriority(IntPtr l) {
		try {
			UnityEngine.UI.LayoutElement self=(UnityEngine.UI.LayoutElement)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.layoutPriority=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.UI.LayoutElement");
		addMember(l,CalculateLayoutInputHorizontal);
		addMember(l,CalculateLayoutInputVertical);
		addMember(l,DOFlexibleSize);
		addMember(l,DOMinSize);
		addMember(l,DOPreferredSize);
		addMember(l,"ignoreLayout",get_ignoreLayout,set_ignoreLayout,true);
		addMember(l,"minWidth",get_minWidth,set_minWidth,true);
		addMember(l,"minHeight",get_minHeight,set_minHeight,true);
		addMember(l,"preferredWidth",get_preferredWidth,set_preferredWidth,true);
		addMember(l,"preferredHeight",get_preferredHeight,set_preferredHeight,true);
		addMember(l,"flexibleWidth",get_flexibleWidth,set_flexibleWidth,true);
		addMember(l,"flexibleHeight",get_flexibleHeight,set_flexibleHeight,true);
		addMember(l,"layoutPriority",get_layoutPriority,set_layoutPriority,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.LayoutElement),typeof(UnityEngine.EventSystems.UIBehaviour));
	}
}
