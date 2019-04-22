using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_MsgData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			MsgData o;
			o=new MsgData();
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
	static public int GetAlertMessageByType(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			TLProtocol.Data.AlertMessageType a1;
			checkEnum(l,2,out a1);
			var ret=self.GetAlertMessageByType(a1);
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
	static public int InitNetWork(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			self.InitNetWork();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetMessage(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetMessage(a1);
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
	static public int AddSimulationMessage(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				MsgData self=(MsgData)checkSelf(l);
				TLProtocol.Data.AlertMessageType a1;
				checkEnum(l,2,out a1);
				self.AddSimulationMessage(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				MsgData self=(MsgData)checkSelf(l);
				TLProtocol.Data.AlertMessageType a1;
				checkEnum(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				self.AddSimulationMessage(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AddSimulationMessage to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetFirstMessageId(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			TLProtocol.Data.AlertMessageType a1;
			checkEnum(l,2,out a1);
			var ret=self.GetFirstMessageId(a1);
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
	static public int RemoveMessage(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			TLProtocol.Data.AlertMessageType a2;
			checkEnum(l,3,out a2);
			self.RemoveMessage(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveList(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			TLProtocol.Data.AlertMessageType a1;
			checkEnum(l,2,out a1);
			self.RemoveList(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetMessageCount(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			TLProtocol.Data.AlertMessageType a1;
			checkEnum(l,2,out a1);
			var ret=self.GetMessageCount(a1);
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
	static public int ShowSimpleAlert(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			TLProtocol.Data.AlertMessageType a1;
			checkEnum(l,2,out a1);
			self.ShowSimpleAlert(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RequestMessageResult(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Action<TLProtocol.Protocol.Client.ClientHandleMessageResponse> a3;
			checkDelegate(l,4,out a3);
			self.RequestMessageResult(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AttachLuaObserver(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			SLua.LuaTable a2;
			checkType(l,3,out a2);
			self.AttachLuaObserver(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachLuaObserver(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.DetachLuaObserver(a1);
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
			MsgData self=(MsgData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			self.Notify(a1,a2);
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
			MsgData self=(MsgData)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.Update(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RequestSendedMessage(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			TLProtocol.Data.AlertMessageType a1;
			checkEnum(l,2,out a1);
			System.Action<System.Collections.Generic.List<MsgData.MsgInfo>> a2;
			checkDelegate(l,3,out a2);
			self.RequestSendedMessage(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clear(IntPtr l) {
		try {
			MsgData self=(MsgData)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Clear(a1,a2);
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"MsgData");
		addMember(l,GetAlertMessageByType);
		addMember(l,InitNetWork);
		addMember(l,GetMessage);
		addMember(l,AddSimulationMessage);
		addMember(l,GetFirstMessageId);
		addMember(l,RemoveMessage);
		addMember(l,RemoveList);
		addMember(l,GetMessageCount);
		addMember(l,ShowSimpleAlert);
		addMember(l,RequestMessageResult);
		addMember(l,AttachLuaObserver);
		addMember(l,DetachLuaObserver);
		addMember(l,Notify);
		addMember(l,Update);
		addMember(l,RequestSendedMessage);
		addMember(l,Clear);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(MsgData));
	}
}
