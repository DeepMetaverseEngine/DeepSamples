using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_MessageHandleData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLProtocol.Data.MessageHandleData o;
			o=new TLProtocol.Data.MessageHandleData();
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
	static public int get_id(IntPtr l) {
		try {
			TLProtocol.Data.MessageHandleData self=(TLProtocol.Data.MessageHandleData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.id);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_id(IntPtr l) {
		try {
			TLProtocol.Data.MessageHandleData self=(TLProtocol.Data.MessageHandleData)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.id=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_agree(IntPtr l) {
		try {
			TLProtocol.Data.MessageHandleData self=(TLProtocol.Data.MessageHandleData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.agree);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_agree(IntPtr l) {
		try {
			TLProtocol.Data.MessageHandleData self=(TLProtocol.Data.MessageHandleData)checkSelf(l);
			TLProtocol.Data.AlertHandlerType v;
			checkEnum(l,2,out v);
			self.agree=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLProtocol.Data.MessageHandleData");
		addMember(l,"id",get_id,set_id,true);
		addMember(l,"agree",get_agree,set_agree,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLProtocol.Data.MessageHandleData));
	}
}
