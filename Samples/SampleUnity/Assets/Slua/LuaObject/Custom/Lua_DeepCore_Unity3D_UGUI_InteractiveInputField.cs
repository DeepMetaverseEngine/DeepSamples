using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_InteractiveInputField : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerDown(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerUp(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerUp(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerClick(a1);
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
	static public int get_AsSelectable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AsSelectable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsPressDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
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
	static public int get_LastPointerDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastPointerDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_PointerDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			System.Action<UnityEngine.EventSystems.PointerEventData> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_PointerDown=v;
			else if(op==1) self.event_PointerDown+=v;
			else if(op==2) self.event_PointerDown-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_PointerUp(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			System.Action<UnityEngine.EventSystems.PointerEventData> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_PointerUp=v;
			else if(op==1) self.event_PointerUp+=v;
			else if(op==2) self.event_PointerUp-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_PointerClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			System.Action<UnityEngine.EventSystems.PointerEventData> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_PointerClick=v;
			else if(op==1) self.event_PointerClick+=v;
			else if(op==2) self.event_PointerClick-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_EndEdit(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			System.Action<System.String> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_EndEdit=v;
			else if(op==1) self.event_EndEdit+=v;
			else if(op==2) self.event_EndEdit-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_ValueChanged(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
			System.Action<System.String> v;
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
	static public int get_Binding(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
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
	static public int get_Text(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.InteractiveInputField self=(DeepCore.Unity3D.UGUI.InteractiveInputField)checkSelf(l);
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"InteractiveInputField");
		addMember(l,OnPointerDown);
		addMember(l,OnPointerUp);
		addMember(l,OnPointerClick);
		addMember(l,"AsSelectable",get_AsSelectable,null,true);
		addMember(l,"IsPressDown",get_IsPressDown,null,true);
		addMember(l,"LastPointerDown",get_LastPointerDown,null,true);
		addMember(l,"event_PointerDown",null,set_event_PointerDown,true);
		addMember(l,"event_PointerUp",null,set_event_PointerUp,true);
		addMember(l,"event_PointerClick",null,set_event_PointerClick,true);
		addMember(l,"event_EndEdit",null,set_event_EndEdit,true);
		addMember(l,"event_ValueChanged",null,set_event_ValueChanged,true);
		addMember(l,"Binding",get_Binding,null,true);
		addMember(l,"Text",get_Text,set_Text,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UGUI.InteractiveInputField),typeof(UnityEngine.UI.InputField));
	}
}
