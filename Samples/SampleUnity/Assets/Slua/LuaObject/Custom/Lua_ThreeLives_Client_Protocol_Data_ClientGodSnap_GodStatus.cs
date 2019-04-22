using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ThreeLives_Client_Protocol_Data_ClientGodSnap_GodStatus : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEIdle(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Protocol.Data.ClientGodSnap.GodStatus.EIdle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EIdle(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)ThreeLives.Client.Protocol.Data.ClientGodSnap.GodStatus.EIdle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEFight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Protocol.Data.ClientGodSnap.GodStatus.EFight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EFight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)ThreeLives.Client.Protocol.Data.ClientGodSnap.GodStatus.EFight);
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
	static public void reg(IntPtr l) {
		getTypeTable(l,"GodStatus");
		addMember(l,"EIdle",getEIdle,null,false);
		addMember(l,"_EIdle",get_EIdle,null,false);
		addMember(l,"EFight",getEFight,null,false);
		addMember(l,"_EFight",get_EFight,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(ThreeLives.Client.Protocol.Data.ClientGodSnap.GodStatus));
	}
}
