using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_ScrollablePanel : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel o;
			System.String a1;
			checkType(l,2,out a1);
			o=new DeepCore.Unity3D.UGUI.ScrollablePanel(a1);
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
	static public int IsInViewRect(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			UnityEngine.Rect a1;
			checkValueType(l,2,out a1);
			UnityEngine.Rect a2;
			checkValueType(l,3,out a2);
			var ret=self.IsInViewRect(a1,a2);
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
	static public int LookAt(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
				UnityEngine.Vector2 a1;
				checkType(l,2,out a1);
				self.LookAt(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
				UnityEngine.Vector2 a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				self.LookAt(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function LookAt to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetScrollBarPair(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.UGUI.DisplayNode a2;
			checkType(l,3,out a2);
			self.SetScrollBarPair(a1,a2);
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
	static public int set_event_Scrolled(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			DeepCore.Unity3D.UGUI.ScrollablePanel.ScrollEventHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_Scrolled=v;
			else if(op==1) self.event_Scrolled+=v;
			else if(op==2) self.event_Scrolled-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_OnEndDrag(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode.PointerEventHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_OnEndDrag=v;
			else if(op==1) self.event_OnEndDrag+=v;
			else if(op==2) self.event_OnEndDrag-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_ScrollEnd(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			System.Action v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_ScrollEnd=v;
			else if(op==1) self.event_ScrollEnd+=v;
			else if(op==2) self.event_ScrollEnd-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Scroll(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Scroll);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScrollRect2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ScrollRect2D);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Container(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Container);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ShowSlider(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ShowSlider);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ShowSlider(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.ShowSlider=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScrollFadeTimeMaxMS(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ScrollFadeTimeMaxMS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ScrollFadeTimeMaxMS(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.ScrollablePanel self=(DeepCore.Unity3D.UGUI.ScrollablePanel)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.ScrollFadeTimeMaxMS=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ScrollablePanel");
		addMember(l,IsInViewRect);
		addMember(l,LookAt);
		addMember(l,SetScrollBarPair);
		addMember(l,"event_Scrolled",null,set_event_Scrolled,true);
		addMember(l,"event_OnEndDrag",null,set_event_OnEndDrag,true);
		addMember(l,"event_ScrollEnd",null,set_event_ScrollEnd,true);
		addMember(l,"Scroll",get_Scroll,null,true);
		addMember(l,"ScrollRect2D",get_ScrollRect2D,null,true);
		addMember(l,"Container",get_Container,null,true);
		addMember(l,"ShowSlider",get_ShowSlider,set_ShowSlider,true);
		addMember(l,"ScrollFadeTimeMaxMS",get_ScrollFadeTimeMaxMS,set_ScrollFadeTimeMaxMS,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUI.ScrollablePanel),typeof(DeepCore.Unity3D.UGUI.DisplayNode));
	}
}
