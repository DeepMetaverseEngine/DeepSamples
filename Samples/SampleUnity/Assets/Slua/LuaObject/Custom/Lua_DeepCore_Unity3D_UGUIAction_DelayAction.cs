using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIAction_DelayAction : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.DelayAction o;
			o=new DeepCore.Unity3D.UGUIAction.DelayAction();
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
			DeepCore.Unity3D.UGUIAction.DelayAction self=(DeepCore.Unity3D.UGUIAction.DelayAction)checkSelf(l);
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
			DeepCore.Unity3D.UGUIAction.DelayAction self=(DeepCore.Unity3D.UGUIAction.DelayAction)checkSelf(l);
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
	static public int IsEnd(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.DelayAction self=(DeepCore.Unity3D.UGUIAction.DelayAction)checkSelf(l);
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
			DeepCore.Unity3D.UGUIAction.DelayAction self=(DeepCore.Unity3D.UGUIAction.DelayAction)checkSelf(l);
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
			pushValue(l,DeepCore.Unity3D.UGUIAction.DelayAction.ACTIONTYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Duration(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.DelayAction self=(DeepCore.Unity3D.UGUIAction.DelayAction)checkSelf(l);
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
			DeepCore.Unity3D.UGUIAction.DelayAction self=(DeepCore.Unity3D.UGUIAction.DelayAction)checkSelf(l);
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
	static public int get_CurrentTime(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.DelayAction self=(DeepCore.Unity3D.UGUIAction.DelayAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UpdateCallBack(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIAction.DelayAction self=(DeepCore.Unity3D.UGUIAction.DelayAction)checkSelf(l);
			DeepCore.Unity3D.UGUIAction.ActionBase.ActionFinishHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.UpdateCallBack=v;
			else if(op==1) self.UpdateCallBack+=v;
			else if(op==2) self.UpdateCallBack-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DelayAction");
		addMember(l,onUpdate);
		addMember(l,onStart);
		addMember(l,IsEnd);
		addMember(l,GetActionType);
		addMember(l,"ACTIONTYPE",get_ACTIONTYPE,null,false);
		addMember(l,"Duration",get_Duration,set_Duration,true);
		addMember(l,"CurrentTime",get_CurrentTime,null,true);
		addMember(l,"UpdateCallBack",null,set_UpdateCallBack,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIAction.DelayAction),typeof(DeepCore.Unity3D.UGUIAction.ActionBase));
	}
}
