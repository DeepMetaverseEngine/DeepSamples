using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIAction_ScaleAction : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction o;
			o=new DeepCore.Unity3D.UGUIAction.ScaleAction();
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
	static public int onUpdate(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			DeepCore.Unity3D.UGUIAction.IActionCompment a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			self.onUpdate(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int onStart(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			DeepCore.Unity3D.UGUIAction.IActionCompment a1;
			checkType(l,2,out a1);
			self.onStart(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int onStop(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			DeepCore.Unity3D.UGUIAction.IActionCompment a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.onStop(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsEnd(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			var ret=self.IsEnd();
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
	static public int GetActionType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			var ret=self.GetActionType();
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
	static public int get_ACTIONTYPE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIAction.ScaleAction.ACTIONTYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScaleX(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ScaleX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ScaleX(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.ScaleX=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScaleY(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ScaleY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ScaleY(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.ScaleY=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Duration(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Duration);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Duration(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.Duration=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PlayMode(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PlayMode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_PlayMode(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.PlayMode=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ScaleType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ScaleType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ScaleType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ScaleAction self=(DeepCore.Unity3D.UGUIAction.ScaleAction)checkSelf(l);
			DeepCore.Unity3D.UGUIAction.ScaleAction.ScaleTypes v;
			checkEnum(l,2,out v);
			self.ScaleType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ScaleAction");
		addMember(l,onUpdate);
		addMember(l,onStart);
		addMember(l,onStop);
		addMember(l,IsEnd);
		addMember(l,GetActionType);
		addMember(l,"ACTIONTYPE",get_ACTIONTYPE,null,false);
		addMember(l,"ScaleX",get_ScaleX,set_ScaleX,true);
		addMember(l,"ScaleY",get_ScaleY,set_ScaleY,true);
		addMember(l,"Duration",get_Duration,set_Duration,true);
		addMember(l,"PlayMode",get_PlayMode,set_PlayMode,true);
		addMember(l,"ScaleType",get_ScaleType,set_ScaleType,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIAction.ScaleAction),typeof(DeepCore.Unity3D.UGUIAction.ActionBase));
	}
}
