using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_HZTextBox : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextBox o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.HZTextBox();
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
	static public int SetCenterShow(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextBox self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextBox)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.SetCenterShow(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DecodeAndUnderlineLink(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextBox self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextBox)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.DecodeAndUnderlineLink(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateTextBox_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==0){
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZTextBox.CreateTextBox();
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==1){
				DeepCore.GUI.Data.UETextBoxMeta a1;
				checkType(l,1,out a1);
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZTextBox.CreateTextBox(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateTextBox to call");
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
	static public int set_LinkClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextBox self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextBox)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.HZTextBox.LinkClickHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.LinkClick=v;
			else if(op==1) self.LinkClick+=v;
			else if(op==2) self.LinkClick-=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.HZTextBox self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextBox)checkSelf(l);
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZTextBox");
		addMember(l,SetCenterShow);
		addMember(l,DecodeAndUnderlineLink);
		addMember(l,CreateTextBox_s);
		addMember(l,"LinkClick",null,set_LinkClick,true);
		addMember(l,"XmlText",null,set_XmlText,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.HZTextBox),typeof(DeepCore.Unity3D.UGUIEditor.UI.UETextBox));
	}
}
