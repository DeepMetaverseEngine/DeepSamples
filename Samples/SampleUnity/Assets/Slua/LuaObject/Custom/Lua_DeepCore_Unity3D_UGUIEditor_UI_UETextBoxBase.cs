using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_UETextBoxBase : LuaObject {
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
	static public int get_Text(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Text);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Text(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.Text=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FontSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FontSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FontSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.FontSize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FontColor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FontColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FontColor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			UnityEngine.Color v;
			checkType(l,2,out v);
			self.FontColor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextComponent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextComponent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Scroll(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Scroll);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
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
	static public int set_Scrollable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.Scrollable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AText);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			DeepCore.GUI.Display.Text.AttributedString v;
			checkType(l,2,out v);
			self.AText=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_XmlText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.XmlText=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UnityRichText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase self=(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.UnityRichText=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UETextBoxBase");
		addMember(l,"Text",get_Text,set_Text,true);
		addMember(l,"FontSize",get_FontSize,set_FontSize,true);
		addMember(l,"FontColor",get_FontColor,set_FontColor,true);
		addMember(l,"TextComponent",get_TextComponent,null,true);
		addMember(l,"Scroll",get_Scroll,null,true);
		addMember(l,"Scrollable",get_Scrollable,set_Scrollable,true);
		addMember(l,"AText",get_AText,set_AText,true);
		addMember(l,"XmlText",null,set_XmlText,true);
		addMember(l,"UnityRichText",null,set_UnityRichText,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxBase),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
