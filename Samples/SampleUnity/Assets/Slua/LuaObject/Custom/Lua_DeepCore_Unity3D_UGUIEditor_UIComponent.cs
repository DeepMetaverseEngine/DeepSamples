using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UIComponent : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent o;
			System.String a1;
			checkType(l,2,out a1);
			o=new DeepCore.Unity3D.UGUIEditor.UIComponent(a1);
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
	static public int Clone(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
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
	static public int FindChildByEditName(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(bool))){
				DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				var ret=self.FindChildByEditName(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(bool))){
				DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				var ret=self.FindChildByEditName<DeepCore.Unity3D.UGUIEditor.UIComponent>(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function FindChildByEditName to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ForceUpdateLayout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			self.ForceUpdateLayout();
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
	static public int get_mPressDownSec(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mPressDownSec);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mPressDownSec(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.mPressDownSec=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_LongPoniterDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UIComponent.LongPressHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_LongPoniterDown=v;
			else if(op==1) self.event_LongPoniterDown+=v;
			else if(op==2) self.event_LongPoniterDown-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_LongPoniterDownStep(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UIComponent.LongPressHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_LongPoniterDownStep=v;
			else if(op==1) self.event_LongPoniterDownStep+=v;
			else if(op==2) self.event_LongPoniterDownStep-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_LongPoniterClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UIComponent.LongPressHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_LongPoniterClick=v;
			else if(op==1) self.event_LongPoniterClick+=v;
			else if(op==2) self.event_LongPoniterClick-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Editor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Editor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EditName(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EditName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EditType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EditType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Graphics(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Graphics);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MetaData(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MetaData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Layout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Layout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Layout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UILayout v;
			checkType(l,2,out v);
			self.Layout=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Disable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Disable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Disable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.Disable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LayoutDisable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LayoutDisable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LayoutDisable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UILayout v;
			checkType(l,2,out v);
			self.LayoutDisable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsLongClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsLongClick);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LongPressSecond(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LongPressSecond);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LongPressSecond(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent self=(DeepCore.Unity3D.UGUIEditor.UIComponent)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.LongPressSecond=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UIComponent");
		addMember(l,Clone);
		addMember(l,FindChildByEditName);
		addMember(l,ForceUpdateLayout);
		addMember(l,"mPressDownSec",get_mPressDownSec,set_mPressDownSec,true);
		addMember(l,"event_LongPoniterDown",null,set_event_LongPoniterDown,true);
		addMember(l,"event_LongPoniterDownStep",null,set_event_LongPoniterDownStep,true);
		addMember(l,"event_LongPoniterClick",null,set_event_LongPoniterClick,true);
		addMember(l,"Editor",get_Editor,null,true);
		addMember(l,"EditName",get_EditName,null,true);
		addMember(l,"EditType",get_EditType,null,true);
		addMember(l,"Graphics",get_Graphics,null,true);
		addMember(l,"MetaData",get_MetaData,null,true);
		addMember(l,"Layout",get_Layout,set_Layout,true);
		addMember(l,"Disable",get_Disable,set_Disable,true);
		addMember(l,"LayoutDisable",get_LayoutDisable,set_LayoutDisable,true);
		addMember(l,"IsLongClick",get_IsLongClick,null,true);
		addMember(l,"LongPressSecond",get_LongPressSecond,set_LongPressSecond,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UIComponent),typeof(DeepCore.Unity3D.UGUI.DisplayNode));
	}
}
