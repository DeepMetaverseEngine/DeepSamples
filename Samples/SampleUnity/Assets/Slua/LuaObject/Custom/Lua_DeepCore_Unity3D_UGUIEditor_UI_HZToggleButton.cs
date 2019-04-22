using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_HZToggleButton : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton();
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
	static public int SetBtnLockState(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.LockState a1;
			checkEnum(l,2,out a1);
			self.SetBtnLockState(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateToggleButton_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==0){
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.CreateToggleButton();
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==1){
				DeepCore.GUI.Data.UETextButtonMeta a1;
				checkType(l,1,out a1);
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.CreateToggleButton(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateToggleButton to call");
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
	static public int get_DefaultCheckedSoundKey(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.DefaultCheckedSoundKey);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultCheckedSoundKey(IntPtr l) {
		try {
			System.String v;
			checkType(l,2,out v);
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.DefaultCheckedSoundKey=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultUnCheckedSoundKey(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.DefaultUnCheckedSoundKey);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultUnCheckedSoundKey(IntPtr l) {
		try {
			System.String v;
			checkType(l,2,out v);
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.DefaultUnCheckedSoundKey=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton)checkSelf(l);
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
	static public int get_IsChecked(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsChecked);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsChecked(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsChecked=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Selected(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton self=(DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.TouchClickHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.Selected=v;
			else if(op==1) self.Selected+=v;
			else if(op==2) self.Selected-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZToggleButton");
		addMember(l,SetBtnLockState);
		addMember(l,CreateToggleButton_s);
		addMember(l,"DefaultCheckedSoundKey",get_DefaultCheckedSoundKey,set_DefaultCheckedSoundKey,false);
		addMember(l,"DefaultUnCheckedSoundKey",get_DefaultUnCheckedSoundKey,set_DefaultUnCheckedSoundKey,false);
		addMember(l,"IsPressDown",get_IsPressDown,null,true);
		addMember(l,"IsChecked",get_IsChecked,set_IsChecked,true);
		addMember(l,"Selected",null,set_Selected,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton),typeof(DeepCore.Unity3D.UGUIEditor.UI.HZTextButton));
	}
}
