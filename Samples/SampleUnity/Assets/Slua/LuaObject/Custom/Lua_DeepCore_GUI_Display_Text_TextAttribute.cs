using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GUI_Display_Text_TextAttribute : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.GUI.Display.Text.TextAttribute o;
			if(argc==13){
				System.UInt32 a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				DeepCore.GUI.Display.FontStyle a4;
				checkEnum(l,5,out a4);
				DeepCore.GUI.Display.Text.RichTextAlignment a5;
				checkEnum(l,6,out a5);
				DeepCore.GUI.Data.TextBorderCount a6;
				checkEnum(l,7,out a6);
				System.UInt32 a7;
				checkType(l,8,out a7);
				System.String a8;
				checkType(l,9,out a8);
				System.String a9;
				checkType(l,10,out a9);
				System.String a10;
				checkType(l,11,out a10);
				DeepCore.GUI.Display.Text.ImageZoom a11;
				checkType(l,12,out a11);
				DeepCore.GUI.Display.Text.TextDrawable a12;
				checkType(l,13,out a12);
				o=new DeepCore.GUI.Display.Text.TextAttribute(a1,a2,a3,a4,a5,a6,a7,a8,a9,a10,a11,a12);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==2){
				DeepCore.GUI.Display.Text.TextAttribute a1;
				checkType(l,2,out a1);
				o=new DeepCore.GUI.Display.Text.TextAttribute(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			return error(l,"New object failed.");
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clone(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			var ret=self.Clone();
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
	static public int IsValid(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			var ret=self.IsValid();
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
	static public int Combine(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			DeepCore.GUI.Display.Text.TextAttribute a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Combine(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Combine_s(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute a1;
			checkType(l,1,out a1);
			DeepCore.GUI.Display.Text.TextAttribute a2;
			checkType(l,2,out a2);
			var ret=DeepCore.GUI.Display.Text.TextAttribute.Combine(a1,a2);
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
	static public int get_fontColor(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.fontColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_fontColor(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.fontColor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_fontSize(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.fontSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_fontSize(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.fontSize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_fontName(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.fontName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_fontName(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.fontName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_fontStyle(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.fontStyle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_fontStyle(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			DeepCore.GUI.Display.FontStyle v;
			checkEnum(l,2,out v);
			self.fontStyle=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_borderCount(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.borderCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_borderCount(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			DeepCore.GUI.Data.TextBorderCount v;
			checkEnum(l,2,out v);
			self.borderCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_borderColor(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.borderColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_borderColor(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.borderColor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_resImage(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.resImage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_resImage(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.resImage=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_resImageZoom(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.resImageZoom);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_resImageZoom(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			DeepCore.GUI.Display.Text.ImageZoom v;
			checkType(l,2,out v);
			self.resImageZoom=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_resSprite(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.resSprite);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_resSprite(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.resSprite=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_link(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.link);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_link(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.link=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_anchor(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.anchor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_anchor(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			DeepCore.GUI.Display.Text.RichTextAlignment v;
			checkEnum(l,2,out v);
			self.anchor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_drawable(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.drawable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_drawable(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			DeepCore.GUI.Display.Text.TextDrawable v;
			checkType(l,2,out v);
			self.drawable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_underline(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.underline);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_underline(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.TextAttribute self=(DeepCore.GUI.Display.Text.TextAttribute)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.underline=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TextAttribute");
		addMember(l,Clone);
		addMember(l,IsValid);
		addMember(l,Combine);
		addMember(l,Combine_s);
		addMember(l,"fontColor",get_fontColor,set_fontColor,true);
		addMember(l,"fontSize",get_fontSize,set_fontSize,true);
		addMember(l,"fontName",get_fontName,set_fontName,true);
		addMember(l,"fontStyle",get_fontStyle,set_fontStyle,true);
		addMember(l,"borderCount",get_borderCount,set_borderCount,true);
		addMember(l,"borderColor",get_borderColor,set_borderColor,true);
		addMember(l,"resImage",get_resImage,set_resImage,true);
		addMember(l,"resImageZoom",get_resImageZoom,set_resImageZoom,true);
		addMember(l,"resSprite",get_resSprite,set_resSprite,true);
		addMember(l,"link",get_link,set_link,true);
		addMember(l,"anchor",get_anchor,set_anchor,true);
		addMember(l,"drawable",get_drawable,set_drawable,true);
		addMember(l,"underline",get_underline,set_underline,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.GUI.Display.Text.TextAttribute));
	}
}
