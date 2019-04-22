using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_UETextInput : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput o;
			if(argc==3){
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				o=new DeepCore.Unity3D.UGUIEditor.UI.UETextInput(a1,a2);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==1){
				o=new DeepCore.Unity3D.UGUIEditor.UI.UETextInput();
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
	static public int SetFont(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
	static public int set_event_ValueChanged(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput.InputValueChangedHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_ValueChanged=v;
			else if(op==1) self.event_ValueChanged+=v;
			else if(op==2) self.event_ValueChanged-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_endEdit(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput.InputValueChangedHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_endEdit=v;
			else if(op==1) self.event_endEdit+=v;
			else if(op==2) self.event_endEdit-=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
	static public int get_PlaceHolder(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PlaceHolder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Input(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Input);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BorderSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BorderSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_BorderSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.BorderSize=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
	static public int get_PlaceHolderText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PlaceHolderText);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_PlaceHolderText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.PlaceHolderText=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
	static public int get_Style(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
	static public int get_FontColor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextInput self=(DeepCore.Unity3D.UGUIEditor.UI.UETextInput)checkSelf(l);
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UETextInput");
		addMember(l,SetFont);
		addMember(l,"event_ValueChanged",null,set_event_ValueChanged,true);
		addMember(l,"event_endEdit",null,set_event_endEdit,true);
		addMember(l,"TextSprite",get_TextSprite,null,true);
		addMember(l,"PlaceHolder",get_PlaceHolder,null,true);
		addMember(l,"Input",get_Input,null,true);
		addMember(l,"BorderSize",get_BorderSize,set_BorderSize,true);
		addMember(l,"Text",get_Text,set_Text,true);
		addMember(l,"PlaceHolderText",get_PlaceHolderText,set_PlaceHolderText,true);
		addMember(l,"FontSize",get_FontSize,set_FontSize,true);
		addMember(l,"Style",get_Style,set_Style,true);
		addMember(l,"FontColor",get_FontColor,set_FontColor,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.UETextInput),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
