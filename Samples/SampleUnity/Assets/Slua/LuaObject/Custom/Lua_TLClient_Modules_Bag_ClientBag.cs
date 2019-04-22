using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Modules_Bag_ClientBag : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetLastModifyReason(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientBag self=(TLClient.Modules.Bag.ClientBag)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetLastModifyReason(a1);
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
	static public int OnSlotNotify(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientBag self=(TLClient.Modules.Bag.ClientBag)checkSelf(l);
			DeepCore.HashMap<System.Int32,TLProtocol.Data.EntityItemData> a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.OnSlotNotify(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Swap(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientBag self=(TLClient.Modules.Bag.ClientBag)checkSelf(l);
			System.Byte a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.UInt32 a3;
			checkType(l,4,out a3);
			System.Action<System.Boolean> a4;
			checkDelegate(l,5,out a4);
			self.Swap(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PackUpItems(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientBag self=(TLClient.Modules.Bag.ClientBag)checkSelf(l);
			System.Action<System.Boolean> a1;
			checkDelegate(l,2,out a1);
			self.PackUpItems(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddSize(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientBag self=(TLClient.Modules.Bag.ClientBag)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Action<System.Boolean> a2;
			checkDelegate(l,3,out a2);
			self.AddSize(a1,a2);
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
	static public int get_Client(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientBag self=(TLClient.Modules.Bag.ClientBag)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Client);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Client(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientBag self=(TLClient.Modules.Bag.ClientBag)checkSelf(l);
			DeepCore.FuckPomeloClient.PomeloClient v;
			checkType(l,2,out v);
			self.Client=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLClient.Modules.Bag.ClientBag");
		addMember(l,GetLastModifyReason);
		addMember(l,OnSlotNotify);
		addMember(l,Swap);
		addMember(l,PackUpItems);
		addMember(l,AddSize);
		addMember(l,"Client",get_Client,set_Client,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLClient.Modules.Bag.ClientBag),typeof(TLClient.Protocol.Modules.CommonBag));
	}
}
