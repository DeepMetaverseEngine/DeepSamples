using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GameEvent_EventManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager o;
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			o=new DeepCore.GameEvent.EventManager(a1,a2);
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
	static public int EnterUpdate(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			self.EnterUpdate();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetObject(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetObject(a1);
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
	static public int PutObject(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			self.PutObject(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Log(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.Log(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LogWarn(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.LogWarn(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LogError(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.LogError(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LogStackTrace(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.LogStackTrace(a1);
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
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
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
	static public int Start(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.Start(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Pause(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			self.Pause();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReStart(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			self.ReStart();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Update(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			self.Update();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddTimeTask(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			DeepCore.TickHandler a4;
			checkDelegate(l,5,out a4);
			var ret=self.AddTimeTask(a1,a2,a3,a4);
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
	static public int AddTimeDelayMS(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			DeepCore.TickHandler a2;
			checkDelegate(l,3,out a2);
			var ret=self.AddTimeDelayMS(a1,a2);
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
	static public int AddTimePeriodicMS(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			DeepCore.TickHandler a2;
			checkDelegate(l,3,out a2);
			var ret=self.AddTimePeriodicMS(a1,a2);
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
	static public int StartEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			DeepCore.GameEvent.Events.BaseEvent a1;
			checkType(l,2,out a1);
			var ret=self.StartEvent(a1);
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
	static public int QueueAction(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.Action a1;
			checkDelegate(l,2,out a1);
			self.QueueAction(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateEvent(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(System.Type))){
				DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
				System.Type a1;
				checkType(l,2,out a1);
				var ret=self.CreateEvent(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string))){
				DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				var ret=self.CreateEvent(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateEvent to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetEvent(a1);
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
	static public int OnReceiveMessage(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			DeepCore.GameEvent.Message.EventMessage a1;
			checkType(l,2,out a1);
			self.OnReceiveMessage(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetAddress_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			var ret=DeepCore.GameEvent.EventManager.GetAddress(a1,a2);
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
	static public int get_Random(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Random);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AddressSeparatorChar(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GameEvent.EventManager.AddressSeparatorChar);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MessageBroker(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.GameEvent.EventManager.MessageBroker);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Logger(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Logger);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Name(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Name);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UUID(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UUID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EventCount(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EventCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurrentRootEvent(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentRootEvent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsRunning(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsRunning);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Address(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Address);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RemoteAction(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RemoteAction);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RemoteAction(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			DeepCore.GameEvent.EventManager.RemoteActionType v;
			checkEnum(l,2,out v);
			self.RemoteAction=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsInUpdate(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsInUpdate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastUpdateTickMS(IntPtr l) {
		try {
			DeepCore.GameEvent.EventManager self=(DeepCore.GameEvent.EventManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastUpdateTickMS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DeepCore.GameEvent.EventManager");
		addMember(l,EnterUpdate);
		addMember(l,GetObject);
		addMember(l,PutObject);
		addMember(l,Log);
		addMember(l,LogWarn);
		addMember(l,LogError);
		addMember(l,LogStackTrace);
		addMember(l,LogException);
		addMember(l,Start);
		addMember(l,Pause);
		addMember(l,ReStart);
		addMember(l,Update);
		addMember(l,AddTimeTask);
		addMember(l,AddTimeDelayMS);
		addMember(l,AddTimePeriodicMS);
		addMember(l,StartEvent);
		addMember(l,QueueAction);
		addMember(l,CreateEvent);
		addMember(l,GetEvent);
		addMember(l,OnReceiveMessage);
		addMember(l,GetAddress_s);
		addMember(l,"Random",get_Random,null,true);
		addMember(l,"AddressSeparatorChar",get_AddressSeparatorChar,null,false);
		addMember(l,"MessageBroker",get_MessageBroker,null,false);
		addMember(l,"Logger",get_Logger,null,true);
		addMember(l,"Name",get_Name,null,true);
		addMember(l,"UUID",get_UUID,null,true);
		addMember(l,"EventCount",get_EventCount,null,true);
		addMember(l,"CurrentRootEvent",get_CurrentRootEvent,null,true);
		addMember(l,"IsRunning",get_IsRunning,null,true);
		addMember(l,"Address",get_Address,null,true);
		addMember(l,"RemoteAction",get_RemoteAction,set_RemoteAction,true);
		addMember(l,"IsInUpdate",get_IsInUpdate,null,true);
		addMember(l,"LastUpdateTickMS",get_LastUpdateTickMS,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.GameEvent.EventManager),typeof(DeepCore.Disposable));
	}
}
