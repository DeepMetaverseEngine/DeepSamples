using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLNetManage : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLNetManage o;
			o=new TLNetManage();
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
	static public int Request(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			DeepCore.IO.ISerializable a1;
			checkType(l,2,out a1);
			System.Action<DeepCore.FuckPomeloClient.PomeloException,DeepCore.IO.ISerializable> a2;
			checkDelegate(l,3,out a2);
			System.Object a3;
			checkType(l,4,out a3);
			self.Request<DeepCore.IO.ISerializable>(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Listen(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			System.Action<DeepCore.IO.ISerializable> a1;
			checkDelegate(l,2,out a1);
			self.Listen<DeepCore.IO.ISerializable>(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RequestBinary(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			DeepCore.IO.BinaryMessage a1;
			checkValueType(l,2,out a1);
			System.Action<DeepCore.FuckPomeloClient.PomeloException,DeepCore.IO.BinaryMessage> a2;
			checkDelegate(l,3,out a2);
			self.RequestBinary(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int NotifyBinary(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			DeepCore.IO.BinaryMessage a1;
			checkValueType(l,2,out a1);
			self.NotifyBinary(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ListenBinary(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Action<DeepCore.IO.BinaryMessage> a2;
			checkDelegate(l,3,out a2);
			self.ListenBinary(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Notify(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			DeepCore.IO.ISerializable a1;
			checkType(l,2,out a1);
			self.Notify(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Disconnect(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			self.Disconnect();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsGateSocketConnected(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			var ret=self.IsGateSocketConnected();
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
	static public int IsGameSocketConnected(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			var ret=self.IsGameSocketConnected();
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
	static public int IsGameEntered(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			var ret=self.IsGameEntered();
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
	static public int AttachObserverGate(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			IZeusNetObserver a1;
			checkType(l,2,out a1);
			self.AttachObserverGate(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachObserverGate(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			IZeusNetObserver a1;
			checkType(l,2,out a1);
			self.DetachObserverGate(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AttachObserverGame(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			IZeusNetObserver a1;
			checkType(l,2,out a1);
			self.AttachObserverGame(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachObserverGame(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			IZeusNetObserver a1;
			checkType(l,2,out a1);
			self.DetachObserverGame(a1);
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
	static public int get_WAIT_TIME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLNetManage.WAIT_TIME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLNetManage.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NetClient(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.NetClient);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LoginHandler(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LoginHandler);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BattleClient(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BattleClient);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsNet(IntPtr l) {
		try {
			TLNetManage self=(TLNetManage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsNet);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLNetManage");
		addMember(l,Request);
		addMember(l,Listen);
		addMember(l,RequestBinary);
		addMember(l,NotifyBinary);
		addMember(l,ListenBinary);
		addMember(l,Notify);
		addMember(l,Disconnect);
		addMember(l,IsGateSocketConnected);
		addMember(l,IsGameSocketConnected);
		addMember(l,IsGameEntered);
		addMember(l,AttachObserverGate);
		addMember(l,DetachObserverGate);
		addMember(l,AttachObserverGame);
		addMember(l,DetachObserverGame);
		addMember(l,"WAIT_TIME",get_WAIT_TIME,null,false);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"NetClient",get_NetClient,null,true);
		addMember(l,"LoginHandler",get_LoginHandler,null,true);
		addMember(l,"BattleClient",get_BattleClient,null,true);
		addMember(l,"IsNet",get_IsNet,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLNetManage));
	}
}
