using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIAction_ActionBase : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ActionBase o;
			o=new DeepCore.Unity3D.UGUIAction.ActionBase();
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
			DeepCore.Unity3D.UGUIAction.ActionBase self=(DeepCore.Unity3D.UGUIAction.ActionBase)checkSelf(l);
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
			DeepCore.Unity3D.UGUIAction.ActionBase self=(DeepCore.Unity3D.UGUIAction.ActionBase)checkSelf(l);
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
			DeepCore.Unity3D.UGUIAction.ActionBase self=(DeepCore.Unity3D.UGUIAction.ActionBase)checkSelf(l);
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
			DeepCore.Unity3D.UGUIAction.ActionBase self=(DeepCore.Unity3D.UGUIAction.ActionBase)checkSelf(l);
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
			DeepCore.Unity3D.UGUIAction.ActionBase self=(DeepCore.Unity3D.UGUIAction.ActionBase)checkSelf(l);
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
	static public int set_ActionFinishCallBack(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ActionBase self=(DeepCore.Unity3D.UGUIAction.ActionBase)checkSelf(l);
			DeepCore.Unity3D.UGUIAction.ActionBase.ActionFinishHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.ActionFinishCallBack=v;
			else if(op==1) self.ActionFinishCallBack+=v;
			else if(op==2) self.ActionFinishCallBack-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ActionEaseType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ActionBase self=(DeepCore.Unity3D.UGUIAction.ActionBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ActionEaseType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ActionEaseType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.ActionBase self=(DeepCore.Unity3D.UGUIAction.ActionBase)checkSelf(l);
			DeepCore.Unity3D.UGUIAction.EaseType v;
			checkEnum(l,2,out v);
			self.ActionEaseType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ActionBase");
		addMember(l,onUpdate);
		addMember(l,onStart);
		addMember(l,onStop);
		addMember(l,IsEnd);
		addMember(l,GetActionType);
		addMember(l,"ActionFinishCallBack",null,set_ActionFinishCallBack,true);
		addMember(l,"ActionEaseType",get_ActionEaseType,set_ActionEaseType,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIAction.ActionBase));
	}
}
