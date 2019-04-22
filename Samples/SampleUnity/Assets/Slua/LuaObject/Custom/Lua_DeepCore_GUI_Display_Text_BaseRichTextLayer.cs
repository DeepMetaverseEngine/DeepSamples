using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GUI_Display_Text_BaseRichTextLayer : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetAnchor(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			DeepCore.GUI.Display.Text.RichTextAlignment a1;
			checkEnum(l,2,out a1);
			var ret=self.SetAnchor(a1);
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
	static public int SetLineSpace(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			var ret=self.SetLineSpace(a1);
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
	static public int SetFixedLineSpace(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			var ret=self.SetFixedLineSpace(a1);
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
	static public int SetIgnoreSpace(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.SetIgnoreSpace(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetEnableMultiline(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.SetEnableMultiline(a1);
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
	static public int SetWidth(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			var ret=self.SetWidth(a1);
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
	static public int SetBorder(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			var ret=self.SetBorder(a1,a2);
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
	static public int SetString(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			DeepCore.GUI.Display.Text.AttributedString a1;
			checkType(l,2,out a1);
			var ret=self.SetString(a1);
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
	static public int GetLine(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetLine(a1);
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
	static public int GetText(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			var ret=self.GetText();
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
	static public int GetRegion(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetRegion(a1);
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
	static public int GetNowRegion(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetNowRegion(a1);
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
	static public int getRegionLen(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			var ret=self.getRegionLen();
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
	static public int resetChatGegion(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.resetChatGegion(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Render(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==4){
				DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
				DeepCore.GUI.Display.Graphics a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				self.Render(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(argc==8){
				DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
				DeepCore.GUI.Display.Graphics a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				System.Single a4;
				checkType(l,5,out a4);
				System.Single a5;
				checkType(l,6,out a5);
				System.Single a6;
				checkType(l,7,out a6);
				System.Single a7;
				checkType(l,8,out a7);
				self.Render(a1,a2,a3,a4,a5,a6,a7);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Render to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Click(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
				System.Single a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				var ret=self.Click(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==4){
				DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
				System.Single a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				DeepCore.GUI.Display.Text.RichTextClickInfo a3;
				checkValueType(l,4,out a3);
				var ret=self.Click(a1,a2,out a3);
				pushValue(l,true);
				pushValue(l,ret);
				pushValue(l,a3);
				return 3;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Click to call");
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
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
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
	static public int CreateDrawable(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
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
	static public int get_Alignment(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Alignment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsMultiline(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsMultiline);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LineCount(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LineCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LineSpace(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LineSpace);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FixedLineSpace(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FixedLineSpace);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextLength(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextLength);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Width(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Width);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ContentWidth(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ContentWidth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ContentHeight(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ContentHeight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BorderCount(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BorderCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BorderColor(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BorderColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsEnable(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsEnable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsEnable(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.BaseRichTextLayer self=(DeepCore.GUI.Display.Text.BaseRichTextLayer)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsEnable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"BaseRichTextLayer");
		addMember(l,SetAnchor);
		addMember(l,SetLineSpace);
		addMember(l,SetFixedLineSpace);
		addMember(l,SetIgnoreSpace);
		addMember(l,SetEnableMultiline);
		addMember(l,SetWidth);
		addMember(l,SetBorder);
		addMember(l,SetString);
		addMember(l,GetLine);
		addMember(l,GetText);
		addMember(l,GetRegion);
		addMember(l,GetNowRegion);
		addMember(l,getRegionLen);
		addMember(l,resetChatGegion);
		addMember(l,Render);
		addMember(l,Click);
		addMember(l,Dispose);
		addMember(l,CreateDrawable);
		addMember(l,"Alignment",get_Alignment,null,true);
		addMember(l,"IsMultiline",get_IsMultiline,null,true);
		addMember(l,"LineCount",get_LineCount,null,true);
		addMember(l,"LineSpace",get_LineSpace,null,true);
		addMember(l,"FixedLineSpace",get_FixedLineSpace,null,true);
		addMember(l,"TextLength",get_TextLength,null,true);
		addMember(l,"Width",get_Width,null,true);
		addMember(l,"ContentWidth",get_ContentWidth,null,true);
		addMember(l,"ContentHeight",get_ContentHeight,null,true);
		addMember(l,"BorderCount",get_BorderCount,null,true);
		addMember(l,"BorderColor",get_BorderColor,null,true);
		addMember(l,"IsEnable",get_IsEnable,set_IsEnable,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.GUI.Display.Text.BaseRichTextLayer));
	}
}
