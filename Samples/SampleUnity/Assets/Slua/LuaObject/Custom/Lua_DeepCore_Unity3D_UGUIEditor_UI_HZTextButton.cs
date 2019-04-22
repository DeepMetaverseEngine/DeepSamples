using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_HZTextButton : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.HZTextButton();
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
	static public int SetLayout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UILayout a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.UGUIEditor.UILayout a2;
			checkType(l,3,out a2);
			self.SetLayout(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetImageText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton)checkSelf(l);
			DeepCore.Unity3D.Impl.UnityImage a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.Impl.UnityImage a2;
			checkType(l,3,out a2);
			self.SetImageText(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetAtlasImageText(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(DeepCore.GUI.Cell.CPJAtlas),typeof(int),typeof(DeepCore.GUI.Cell.CPJAtlas),typeof(int))){
				DeepCore.Unity3D.UGUIEditor.UI.HZTextButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton)checkSelf(l);
				DeepCore.GUI.Cell.CPJAtlas a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				DeepCore.GUI.Cell.CPJAtlas a3;
				checkType(l,4,out a3);
				System.Int32 a4;
				checkType(l,5,out a4);
				self.SetAtlasImageText(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.GUI.Cell.CPJAtlas),typeof(string),typeof(DeepCore.GUI.Cell.CPJAtlas),typeof(string))){
				DeepCore.Unity3D.UGUIEditor.UI.HZTextButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton)checkSelf(l);
				DeepCore.GUI.Cell.CPJAtlas a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				DeepCore.GUI.Cell.CPJAtlas a3;
				checkType(l,4,out a3);
				System.String a4;
				checkType(l,5,out a4);
				self.SetAtlasImageText(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetAtlasImageText to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateTextButton_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==0){
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZTextButton.CreateTextButton();
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==1){
				DeepCore.GUI.Data.UETextButtonMeta a1;
				checkType(l,1,out a1);
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZTextButton.CreateTextButton(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateTextButton to call");
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
	static public int get_DefaultSoundKey(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIEditor.UI.HZTextButton.DefaultSoundKey);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultSoundKey(IntPtr l) {
		try {
			System.String v;
			checkType(l,2,out v);
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton.DefaultSoundKey=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsPlaySound(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsPlaySound);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsPlaySound(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsPlaySound=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TouchClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.TouchClickHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.TouchClick=v;
			else if(op==1) self.TouchClick+=v;
			else if(op==2) self.TouchClick-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SetPressDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton.SetPressDownHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.SetPressDown=v;
			else if(op==1) self.SetPressDown+=v;
			else if(op==2) self.SetPressDown-=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.HZTextButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsPressDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZTextButton");
		addMember(l,SetLayout);
		addMember(l,SetImageText);
		addMember(l,SetAtlasImageText);
		addMember(l,CreateTextButton_s);
		addMember(l,"DefaultSoundKey",get_DefaultSoundKey,set_DefaultSoundKey,false);
		addMember(l,"IsPlaySound",get_IsPlaySound,set_IsPlaySound,true);
		addMember(l,"TouchClick",null,set_TouchClick,true);
		addMember(l,"SetPressDown",null,set_SetPressDown,true);
		addMember(l,"IsPressDown",get_IsPressDown,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton),typeof(DeepCore.Unity3D.UGUIEditor.UI.UETextButton));
	}
}
