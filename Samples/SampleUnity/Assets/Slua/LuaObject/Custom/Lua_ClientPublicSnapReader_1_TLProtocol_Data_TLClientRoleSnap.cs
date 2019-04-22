using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ClientPublicSnapReader_1_TLProtocol_Data_TLClientRoleSnap : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			ClientPublicSnapReader<TLProtocol.Data.TLClientRoleSnap> o;
			TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>.LoadDataDelegate a1;
			checkDelegate(l,2,out a1);
			o=new ClientPublicSnapReader<TLProtocol.Data.TLClientRoleSnap>(a1);
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
	static public int GetMany(IntPtr l) {
		try {
			ClientPublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(ClientPublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
			SLua.LuaTable a1;
			checkType(l,2,out a1);
			System.Action<TLProtocol.Data.TLClientRoleSnap[]> a2;
			checkDelegate(l,3,out a2);
			self.GetMany(a1,a2);
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
		getTypeTable(l,"ClientPublicSnapReader<<TLProtocol.Data.TLClientRoleSnap, ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>>");
		addMember(l,GetMany);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(ClientPublicSnapReader<TLProtocol.Data.TLClientRoleSnap>),typeof(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>));
	}
}
