using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Protocol_Modules_ItemData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData o;
			TLProtocol.Data.EntityItemData a1;
			checkType(l,2,out a1);
			o=new TLClient.Protocol.Modules.ItemData(a1);
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
	static public int Clone(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			var ret=self.Clone();
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
	static public int CompareAttribute(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			TLClient.Protocol.Modules.Package.IPackageItem a1;
			checkType(l,2,out a1);
			var ret=self.CompareAttribute(a1);
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
	static public int CompareTo(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			TLClient.Protocol.Modules.Package.IPackageItem a1;
			checkType(l,2,out a1);
			var ret=self.CompareTo(a1);
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
	static public int get_Data(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Data);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CanTrade(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CanTrade);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TemplateID(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TemplateID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MaxStackCount(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MaxStackCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PreCount(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PreCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ID(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Flag(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Flag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Flag(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.Flag=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Properties(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Properties);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Count(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Count);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Count(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.Count=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SlotIndex(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SlotIndex);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SlotIndex(IntPtr l) {
		try {
			TLClient.Protocol.Modules.ItemData self=(TLClient.Protocol.Modules.ItemData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.SlotIndex=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ItemData");
		addMember(l,Clone);
		addMember(l,CompareAttribute);
		addMember(l,CompareTo);
		addMember(l,"Data",get_Data,null,true);
		addMember(l,"CanTrade",get_CanTrade,null,true);
		addMember(l,"TemplateID",get_TemplateID,null,true);
		addMember(l,"MaxStackCount",get_MaxStackCount,null,true);
		addMember(l,"PreCount",get_PreCount,null,true);
		addMember(l,"ID",get_ID,null,true);
		addMember(l,"Flag",get_Flag,set_Flag,true);
		addMember(l,"Properties",get_Properties,null,true);
		addMember(l,"Count",get_Count,set_Count,true);
		addMember(l,"SlotIndex",get_SlotIndex,set_SlotIndex,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLClient.Protocol.Modules.ItemData));
	}
}
