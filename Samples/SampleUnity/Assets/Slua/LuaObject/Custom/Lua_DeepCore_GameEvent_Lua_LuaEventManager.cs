using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GameEvent_Lua_LuaEventManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager o;
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			DeepCore.Lua.ILuaAdapter a3;
			checkType(l,4,out a3);
			o=new DeepCore.GameEvent.Lua.LuaEventManager(a1,a2,a3);
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
	static public int PushLuaEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			DeepCore.GameEvent.Lua.LuaWorldEvent a1;
			checkType(l,2,out a1);
			self.PushLuaEvent(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PopLuaEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			self.PopLuaEvent();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LogException(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Exception a1;
			checkType(l,2,out a1);
			self.LogException(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Wait(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.Wait(a1);
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
	static public int IsBeforeStop(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			var ret=self.IsBeforeStop();
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
	static public int WaitAny(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			var ret=self.WaitAny(a1);
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
	static public int WaitSelect(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			var ret=self.WaitSelect(a1);
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
	static public int WaitParallel(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			var ret=self.WaitParallel(a1);
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
	static public int ListenEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			self.ListenEvent(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int TriggerLuaEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			self.TriggerLuaEvent(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WaitAll(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			var ret=self.WaitAll();
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
	static public int StartLuaEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			var ret=self.StartLuaEvent(a1);
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
	static public int AddLuaEventTo(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			var ret=self.AddLuaEventTo(a1,a2);
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
	static public int AddLuaEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			var ret=self.AddLuaEvent(a1);
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
	static public int IsCurrentManager(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.IsCurrentManager(a1,a2);
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
	static public int CallSharpApi(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			var ret=self.CallSharpApi(a1);
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
	static public int GetEventOutput(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetEventOutput(a1);
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
	static public int StopEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			self.StopEvent(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetCurrentEventID(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			var ret=self.GetCurrentEventID();
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
	static public int GetParentEventID(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetParentEventID(a1);
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
	static public int GetRootEventID(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetRootEventID(a1);
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
	static public int IsEventStoped(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.IsEventStoped(a1);
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
	static public int IsEventSuccess(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.IsEventSuccess(a1);
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
	static public int IsEventExists(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.IsEventExists(a1);
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
	static public int SetEventOutput(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			self.SetEventOutput(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ContinueWith(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			var ret=self.ContinueWith(a1,a2);
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
	static public int SetFileDirty(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.SetFileDirty(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnionValueToLuaObject(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			DeepCore.UnionValue a1;
			checkValueType(l,2,out a1);
			var ret=self.UnionValueToLuaObject(a1);
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
	static public int GetEventSandbox(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetEventSandbox(a1);
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
	static public int CallFunction(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Object[] a2;
			checkParams(l,3,out a2);
			var ret=self.CallFunction(a1,a2);
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
	static public int GenNamespaceApi(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			self.GenNamespaceApi(a1,a2,a3);
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
	static public int get_LuaAdapter(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LuaAdapter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Config(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Config);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Config(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.Config=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RootPath(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RootPath);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RootPath(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.RootPath=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CustomMainLua(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CustomMainLua);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_CustomMainLua(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.CustomMainLua=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LuaSystem(IntPtr l) {
		try {
			DeepCore.GameEvent.Lua.LuaEventManager self=(DeepCore.GameEvent.Lua.LuaEventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LuaSystem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultAdapter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GameEvent.Lua.LuaEventManager.DefaultAdapter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultAdapter(IntPtr l) {
		try {
			DeepCore.Lua.ILuaAdapter v;
			checkType(l,2,out v);
			DeepCore.GameEvent.Lua.LuaEventManager.DefaultAdapter=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DeepCore.GameEvent.Lua.LuaEventManager");
		addMember(l,PushLuaEvent);
		addMember(l,PopLuaEvent);
		addMember(l,LogException);
		addMember(l,Wait);
		addMember(l,IsBeforeStop);
		addMember(l,WaitAny);
		addMember(l,WaitSelect);
		addMember(l,WaitParallel);
		addMember(l,ListenEvent);
		addMember(l,TriggerLuaEvent);
		addMember(l,WaitAll);
		addMember(l,StartLuaEvent);
		addMember(l,AddLuaEventTo);
		addMember(l,AddLuaEvent);
		addMember(l,IsCurrentManager);
		addMember(l,CallSharpApi);
		addMember(l,GetEventOutput);
		addMember(l,StopEvent);
		addMember(l,GetCurrentEventID);
		addMember(l,GetParentEventID);
		addMember(l,GetRootEventID);
		addMember(l,IsEventStoped);
		addMember(l,IsEventSuccess);
		addMember(l,IsEventExists);
		addMember(l,SetEventOutput);
		addMember(l,ContinueWith);
		addMember(l,SetFileDirty);
		addMember(l,UnionValueToLuaObject);
		addMember(l,GetEventSandbox);
		addMember(l,CallFunction);
		addMember(l,GenNamespaceApi);
		addMember(l,"LuaAdapter",get_LuaAdapter,null,true);
		addMember(l,"Config",get_Config,set_Config,true);
		addMember(l,"RootPath",get_RootPath,set_RootPath,true);
		addMember(l,"CustomMainLua",get_CustomMainLua,set_CustomMainLua,true);
		addMember(l,"LuaSystem",get_LuaSystem,null,true);
		addMember(l,"DefaultAdapter",get_DefaultAdapter,set_DefaultAdapter,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.GameEvent.Lua.LuaEventManager),typeof(DeepCore.GameEvent.EventManager));
	}
}
