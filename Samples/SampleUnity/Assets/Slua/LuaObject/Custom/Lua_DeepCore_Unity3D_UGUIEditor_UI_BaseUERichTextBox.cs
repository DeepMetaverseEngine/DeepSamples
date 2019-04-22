using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_BaseUERichTextBox : LuaObject {
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
	static public int set_XmlText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox self=(DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox self=(DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox)checkSelf(l);
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox self=(DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox self=(DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox)checkSelf(l);
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
	static public int get_RichTextLayer(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox self=(DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RichTextLayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"BaseUERichTextBox");
		addMember(l,"XmlText",null,set_XmlText,true);
		addMember(l,"UnityRichText",null,set_UnityRichText,true);
		addMember(l,"AText",get_AText,set_AText,true);
		addMember(l,"RichTextLayer",get_RichTextLayer,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
