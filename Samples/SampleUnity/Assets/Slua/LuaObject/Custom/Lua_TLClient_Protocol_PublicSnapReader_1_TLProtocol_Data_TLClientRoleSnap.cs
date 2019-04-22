using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Protocol_PublicSnapReader_1_TLProtocol_Data_TLClientRoleSnap : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> o;
			TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>.LoadDataDelegate a1;
			checkDelegate(l,2,out a1);
			System.Func<System.DateTime> a2;
			checkDelegate(l,3,out a2);
			o=new TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>(a1,a2);
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
	static public int ContainsCache(IntPtr l) {
		try {
			TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.ContainsCache(a1);
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
	static public int GetCache(IntPtr l) {
		try {
			TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetCache(a1);
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
	static public int Load(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(System.Action<System.Exception,TLProtocol.Data.TLClientRoleSnap>))){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Action<System.Exception,TLProtocol.Data.TLClientRoleSnap> a2;
				checkDelegate(l,3,out a2);
				self.Load(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(System.Action<TLProtocol.Data.TLClientRoleSnap>))){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Action<TLProtocol.Data.TLClientRoleSnap> a2;
				checkDelegate(l,3,out a2);
				self.Load(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Load to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadMany(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(System.String[]),typeof(System.Action<System.Exception,TLProtocol.Data.TLClientRoleSnap[]>))){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String[] a1;
				checkArray(l,2,out a1);
				System.Action<System.Exception,TLProtocol.Data.TLClientRoleSnap[]> a2;
				checkDelegate(l,3,out a2);
				self.LoadMany(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(System.String[]),typeof(System.Action<TLProtocol.Data.TLClientRoleSnap[]>))){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String[] a1;
				checkArray(l,2,out a1);
				System.Action<TLProtocol.Data.TLClientRoleSnap[]> a2;
				checkDelegate(l,3,out a2);
				self.LoadMany(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function LoadMany to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetDirty(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(System.String[]))){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String[] a1;
				checkArray(l,2,out a1);
				self.SetDirty(a1);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string))){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				self.SetDirty(a1);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetDirty to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsDirty(IntPtr l) {
		try {
			TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
			TLProtocol.Data.TLClientRoleSnap a1;
			checkType(l,2,out a1);
			var ret=self.IsDirty(a1);
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
	static public int GetMany(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String[] a1;
				checkArray(l,2,out a1);
				System.Action<TLProtocol.Data.TLClientRoleSnap[]> a2;
				checkDelegate(l,3,out a2);
				self.GetMany(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(System.String[]),typeof(bool),typeof(System.Action<TLProtocol.Data.TLClientRoleSnap[]>))){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String[] a1;
				checkArray(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				System.Action<TLProtocol.Data.TLClientRoleSnap[]> a3;
				checkDelegate(l,4,out a3);
				self.GetMany(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(System.String[]),typeof(bool),typeof(System.Action<System.Exception,TLProtocol.Data.TLClientRoleSnap[]>))){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String[] a1;
				checkArray(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				System.Action<System.Exception,TLProtocol.Data.TLClientRoleSnap[]> a3;
				checkDelegate(l,4,out a3);
				self.GetMany(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function GetMany to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Get(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Action<TLProtocol.Data.TLClientRoleSnap> a2;
				checkDelegate(l,3,out a2);
				self.Get(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap> self=(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				System.Action<TLProtocol.Data.TLClientRoleSnap> a3;
				checkDelegate(l,4,out a3);
				self.Get(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Get to call");
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLClient.Protocol.PublicSnapReader<<TLProtocol.Data.TLClientRoleSnap, ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>>");
		addMember(l,ContainsCache);
		addMember(l,GetCache);
		addMember(l,Load);
		addMember(l,LoadMany);
		addMember(l,SetDirty);
		addMember(l,IsDirty);
		addMember(l,GetMany);
		addMember(l,Get);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLClient.Protocol.PublicSnapReader<TLProtocol.Data.TLClientRoleSnap>));
	}
}
