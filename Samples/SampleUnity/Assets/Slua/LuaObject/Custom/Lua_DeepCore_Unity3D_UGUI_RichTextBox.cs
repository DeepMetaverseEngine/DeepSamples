using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_RichTextBox : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox o;
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			o=new DeepCore.Unity3D.UGUI.RichTextBox(a1,a2);
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
	static public int TestClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			DeepCore.GUI.Display.Text.RichTextClickInfo a2;
			checkValueType(l,3,out a2);
			var ret=self.TestClick(a1,out a2);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a2);
			return 3;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetBorder(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			UnityEngine.Color a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			self.SetBorder(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetShadow(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			UnityEngine.Color a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			self.SetShadow(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetFont(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			UnityEngine.Font a1;
			checkType(l,2,out a1);
			self.SetFont(a1);
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
	static public int get_Binding(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Binding);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RichTextLayer(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RichTextLayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
	static public int get_IsNeedScroll(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsNeedScroll);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
	static public int get_Text(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
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
	static public int get_Style(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Style);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Style(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			DeepCore.GUI.Data.FontStyle v;
			checkEnum(l,2,out v);
			self.Style=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsUnderline(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsUnderline);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsUnderline(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsUnderline=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextOffset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextOffset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TextOffset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.TextOffset=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Anchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Anchor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Anchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			DeepCore.GUI.Data.TextAnchor v;
			checkEnum(l,2,out v);
			self.Anchor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PreferredSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PreferredSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastCaretPosition(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.RichTextBox self=(DeepCore.Unity3D.UGUI.RichTextBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastCaretPosition);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"RichTextBox");
		addMember(l,TestClick);
		addMember(l,SetBorder);
		addMember(l,SetShadow);
		addMember(l,SetFont);
		addMember(l,"Binding",get_Binding,null,true);
		addMember(l,"RichTextLayer",get_RichTextLayer,null,true);
		addMember(l,"AText",get_AText,set_AText,true);
		addMember(l,"XmlText",null,set_XmlText,true);
		addMember(l,"UnityRichText",null,set_UnityRichText,true);
		addMember(l,"IsNeedScroll",get_IsNeedScroll,null,true);
		addMember(l,"Scrollable",get_Scrollable,set_Scrollable,true);
		addMember(l,"Text",get_Text,set_Text,true);
		addMember(l,"FontSize",get_FontSize,set_FontSize,true);
		addMember(l,"FontColor",get_FontColor,set_FontColor,true);
		addMember(l,"Style",get_Style,set_Style,true);
		addMember(l,"IsUnderline",get_IsUnderline,set_IsUnderline,true);
		addMember(l,"TextOffset",get_TextOffset,set_TextOffset,true);
		addMember(l,"Anchor",get_Anchor,set_Anchor,true);
		addMember(l,"PreferredSize",get_PreferredSize,null,true);
		addMember(l,"LastCaretPosition",get_LastCaretPosition,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUI.RichTextBox),typeof(DeepCore.Unity3D.UGUI.ScrollablePanel));
	}
}
