using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Modules_Bag_ClientNormalBag : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientNormalBag o;
			System.Byte a1;
			checkType(l,2,out a1);
			DeepCore.FuckPomeloClient.PomeloClient a2;
			checkType(l,3,out a2);
			o=new TLClient.Modules.Bag.ClientNormalBag(a1,a2);
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
	static public int Use(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientNormalBag self=(TLClient.Modules.Bag.ClientNormalBag)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			System.Action<System.Boolean> a3;
			checkDelegate(l,4,out a3);
			self.Use(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Equip(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientNormalBag self=(TLClient.Modules.Bag.ClientNormalBag)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Action<System.Boolean> a2;
			checkDelegate(l,3,out a2);
			self.Equip(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutToWarehouse(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientNormalBag self=(TLClient.Modules.Bag.ClientNormalBag)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			System.Action<System.Boolean> a3;
			checkDelegate(l,4,out a3);
			self.PutToWarehouse(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Sell(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientNormalBag self=(TLClient.Modules.Bag.ClientNormalBag)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			System.Action<System.Boolean> a3;
			checkDelegate(l,4,out a3);
			self.Sell(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Decompose(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientNormalBag self=(TLClient.Modules.Bag.ClientNormalBag)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			System.Action<System.Boolean> a3;
			checkDelegate(l,4,out a3);
			self.Decompose(a1,a2,a3);
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
		getTypeTable(l,"TLClient.Modules.Bag.ClientNormalBag");
		addMember(l,Use);
		addMember(l,Equip);
		addMember(l,PutToWarehouse);
		addMember(l,Sell);
		addMember(l,Decompose);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLClient.Modules.Bag.ClientNormalBag),typeof(TLClient.Modules.Bag.ClientBag));
	}
}
