using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_UGUIRichTextLayer : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer o;
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			o=new DeepCore.Unity3D.UGUI.UGUIRichTextLayer(a1,a2);
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
	static public int Dispose(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			self.Dispose();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetBorder(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
	static public int CreateDrawable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			DeepCore.GUI.Display.Text.BaseRichTextLayer.Region a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			var ret=self.CreateDrawable(a1,a2);
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
	static public int get_UseBitmapFont(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UseBitmapFont);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UseBitmapFont(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.UseBitmapFont=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultFont(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultFont);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultFont(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			UnityEngine.Font v;
			checkType(l,2,out v);
			self.DefaultFont=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Binding(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
	static public int get_DefaultTextAttribute(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultTextAttribute);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultTextAttribute(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			DeepCore.GUI.Display.Text.TextAttribute v;
			checkType(l,2,out v);
			self.DefaultTextAttribute=v;
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
	static public int set_XmlText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
	static public int get_TextGenSetting(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextGenSetting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextGen(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextGen);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.UGUIRichTextLayer self=(DeepCore.Unity3D.UGUI.UGUIRichTextLayer)checkSelf(l);
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
		getTypeTable(l,"UGUIRichTextLayer");
		addMember(l,Dispose);
		addMember(l,SetBorder);
		addMember(l,SetShadow);
		addMember(l,SetFont);
		addMember(l,CreateDrawable);
		addMember(l,"UseBitmapFont",get_UseBitmapFont,set_UseBitmapFont,true);
		addMember(l,"DefaultFont",get_DefaultFont,set_DefaultFont,true);
		addMember(l,"Binding",get_Binding,null,true);
		addMember(l,"DefaultTextAttribute",get_DefaultTextAttribute,set_DefaultTextAttribute,true);
		addMember(l,"UnityRichText",null,set_UnityRichText,true);
		addMember(l,"XmlText",null,set_XmlText,true);
		addMember(l,"TextGenSetting",get_TextGenSetting,null,true);
		addMember(l,"TextGen",get_TextGen,null,true);
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
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUI.UGUIRichTextLayer),typeof(DeepCore.GUI.Display.Text.BaseRichTextLayer));
	}
}
