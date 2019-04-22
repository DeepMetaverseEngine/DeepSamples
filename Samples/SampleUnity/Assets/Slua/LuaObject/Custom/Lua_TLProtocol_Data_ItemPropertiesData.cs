using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_ItemPropertiesData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertiesData o;
			o=new TLProtocol.Data.ItemPropertiesData();
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
			TLProtocol.Data.ItemPropertiesData self=(TLProtocol.Data.ItemPropertiesData)checkSelf(l);
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
	static public int get_Properties(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertiesData self=(TLProtocol.Data.ItemPropertiesData)checkSelf(l);
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
	static public int set_Properties(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertiesData self=(TLProtocol.Data.ItemPropertiesData)checkSelf(l);
			System.Collections.Generic.List<TLProtocol.Data.ItemPropertyData> v;
			checkType(l,2,out v);
			self.Properties=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Count(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertiesData self=(TLProtocol.Data.ItemPropertiesData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Count);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ItemPropertiesData");
		addMember(l,Clone);
		addMember(l,"Properties",get_Properties,set_Properties,true);
		addMember(l,"Count",get_Count,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLProtocol.Data.ItemPropertiesData));
	}
}
