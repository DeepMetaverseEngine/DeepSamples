using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_UETextButton : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton o;
			if(argc==2){
				System.Boolean a1;
				checkType(l,2,out a1);
				o=new DeepCore.Unity3D.UGUIEditor.UI.UETextButton(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==1){
				o=new DeepCore.Unity3D.UGUIEditor.UI.UETextButton();
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
	static public int get_LayoutDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LayoutDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LayoutDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UILayout v;
			checkType(l,2,out v);
			self.LayoutDown=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsPressDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsPressDown);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
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
	static public int get_TextDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TextDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.TextDown=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
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
	static public int get_FocuseFontColor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FocuseFontColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FocuseFontColor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			UnityEngine.Color v;
			checkType(l,2,out v);
			self.FocuseFontColor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextSprite(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextSprite);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EditTextAnchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EditTextAnchor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EditTextAnchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			DeepCore.GUI.Data.TextAnchor v;
			checkEnum(l,2,out v);
			self.EditTextAnchor=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
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
	static public int get_ImageTextAnchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ImageTextAnchor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ImageTextAnchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			DeepCore.GUI.Data.ImageAnchor v;
			checkEnum(l,2,out v);
			self.ImageTextAnchor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ImageTextOffset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ImageTextOffset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ImageTextOffset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.ImageTextOffset=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextButton self=(DeepCore.Unity3D.UGUIEditor.UI.UETextButton)checkSelf(l);
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UETextButton");
		addMember(l,"LayoutDown",get_LayoutDown,set_LayoutDown,true);
		addMember(l,"IsPressDown",get_IsPressDown,null,true);
		addMember(l,"Text",get_Text,set_Text,true);
		addMember(l,"TextDown",get_TextDown,set_TextDown,true);
		addMember(l,"FontColor",get_FontColor,set_FontColor,true);
		addMember(l,"FocuseFontColor",get_FocuseFontColor,set_FocuseFontColor,true);
		addMember(l,"TextSprite",get_TextSprite,null,true);
		addMember(l,"EditTextAnchor",get_EditTextAnchor,set_EditTextAnchor,true);
		addMember(l,"FontSize",get_FontSize,set_FontSize,true);
		addMember(l,"ImageTextAnchor",get_ImageTextAnchor,set_ImageTextAnchor,true);
		addMember(l,"ImageTextOffset",get_ImageTextOffset,set_ImageTextOffset,true);
		addMember(l,"TextOffset",get_TextOffset,set_TextOffset,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.UETextButton),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
