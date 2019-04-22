using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Protocol_Modules_CommonBag : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FindItemByID(IntPtr l) {
		try {
			TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.FindItemByID(a1);
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
	static public int FindSlotByID(IntPtr l) {
		try {
			TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.FindSlotByID(a1);
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
	static public int FindItemAs(IntPtr l) {
		try {
			TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindItemAs(a1);
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
	static public int FindSlotAs(IntPtr l) {
		try {
			TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindSlotAs(a1);
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
	static public int FindFirstItemAs(IntPtr l) {
		try {
			TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindFirstItemAs(a1);
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
	static public int FindFirstSlotAs(IntPtr l) {
		try {
			TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindFirstSlotAs(a1);
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
	static public int InitData(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(TLProtocol.Data.BagData))){
				TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
				TLProtocol.Data.BagData a1;
				checkType(l,2,out a1);
				self.InitData(a1);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.HashMap<System.Int32,TLProtocol.Data.EntityItemData>))){
				TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
				DeepCore.HashMap<System.Int32,TLProtocol.Data.EntityItemData> a1;
				checkType(l,2,out a1);
				self.InitData(a1);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function InitData to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int InitSize(IntPtr l) {
		try {
			TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.InitSize(a1,a2);
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
	static public int get_Type(IntPtr l) {
		try {
			TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Type);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BagData(IntPtr l) {
		try {
			TLClient.Protocol.Modules.CommonBag self=(TLClient.Protocol.Modules.CommonBag)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BagData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLClient.Protocol.Modules.CommonBag");
		addMember(l,FindItemByID);
		addMember(l,FindSlotByID);
		addMember(l,FindItemAs);
		addMember(l,FindSlotAs);
		addMember(l,FindFirstItemAs);
		addMember(l,FindFirstSlotAs);
		addMember(l,InitData);
		addMember(l,InitSize);
		addMember(l,"Type",get_Type,null,true);
		addMember(l,"BagData",get_BagData,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLClient.Protocol.Modules.CommonBag),typeof(TLClient.Protocol.Modules.Package.BasePackage));
	}
}
