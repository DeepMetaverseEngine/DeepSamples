using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_UEScrollPan : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetScrollBar(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UILayout a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.UGUIEditor.UILayout a2;
			checkType(l,3,out a2);
			self.SetScrollBar(a1,a2);
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
	static public int get_ContainerPanel(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ContainerPanel);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Scrollable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Scrollable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScrollRect2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ScrollRect2D);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ShowSlider(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ShowSlider);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ShowSlider(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.ShowSlider=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ViewRect2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ViewRect2D);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UEScrollPan");
		addMember(l,SetScrollBar);
		addMember(l,"ContainerPanel",get_ContainerPanel,null,true);
		addMember(l,"Scrollable",get_Scrollable,null,true);
		addMember(l,"ScrollRect2D",get_ScrollRect2D,null,true);
		addMember(l,"ShowSlider",get_ShowSlider,set_ShowSlider,true);
		addMember(l,"ViewRect2D",get_ViewRect2D,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
