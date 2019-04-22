using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_UIFactory : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DecodeAttributedString(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(DeepCore.GUI.Display.Text.TextAttribute))){
				DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				DeepCore.GUI.Display.Text.TextAttribute a2;
				checkType(l,3,out a2);
				var ret=self.DecodeAttributedString(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(System.Xml.XmlDocument),typeof(DeepCore.GUI.Display.Text.TextAttribute))){
				DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
				System.Xml.XmlDocument a1;
				checkType(l,2,out a1);
				DeepCore.GUI.Display.Text.TextAttribute a2;
				checkType(l,3,out a2);
				var ret=self.DecodeAttributedString(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function DecodeAttributedString to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateRichTextLayer(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.CreateRichTextLayer(a1,a2);
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
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUI.UIFactory.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultFont(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
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
	static public int get_DefaultTextGenerator(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultTextGenerator);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultFontBestFitMin(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultFontBestFitMin);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultFontBestFitMin(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.DefaultFontBestFitMin=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultFontBestFitMax(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultFontBestFitMax);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultFontBestFitMax(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.DefaultFontBestFitMax=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultCaretSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultCaretSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultCaretSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.UIFactory self=(DeepCore.Unity3D.UGUI.UIFactory)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.DefaultCaretSize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UIFactory");
		addMember(l,DecodeAttributedString);
		addMember(l,CreateRichTextLayer);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"DefaultFont",get_DefaultFont,null,true);
		addMember(l,"DefaultTextGenerator",get_DefaultTextGenerator,null,true);
		addMember(l,"DefaultFontBestFitMin",get_DefaultFontBestFitMin,set_DefaultFontBestFitMin,true);
		addMember(l,"DefaultFontBestFitMax",get_DefaultFontBestFitMax,set_DefaultFontBestFitMax,true);
		addMember(l,"DefaultCaretSize",get_DefaultCaretSize,set_DefaultCaretSize,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UGUI.UIFactory));
	}
}
