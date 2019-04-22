using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_MsgData_MsgInfo : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			MsgData.MsgInfo o;
			o=new MsgData.MsgInfo();
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
	static public int get_Id(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Id);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Id(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.Id=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Type(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
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
	static public int set_Type(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			TLProtocol.Data.AlertMessageType v;
			checkEnum(l,2,out v);
			self.Type=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Content(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Content);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Content(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.Content=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WaitTime(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.WaitTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_WaitTime(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			double v;
			checkType(l,2,out v);
			self.WaitTime=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FromRoleID(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FromRoleID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FromRoleID(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.FromRoleID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TargetRoleID(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TargetRoleID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TargetRoleID(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.TargetRoleID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SyncPlayers(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SyncPlayers);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SyncPlayers(IntPtr l) {
		try {
			MsgData.MsgInfo self=(MsgData.MsgInfo)checkSelf(l);
			TLProtocol.Data.MessageHandleData[] v;
			checkArray(l,2,out v);
			self.SyncPlayers=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"MsgData.MsgInfo");
		addMember(l,"Id",get_Id,set_Id,true);
		addMember(l,"Type",get_Type,set_Type,true);
		addMember(l,"Content",get_Content,set_Content,true);
		addMember(l,"WaitTime",get_WaitTime,set_WaitTime,true);
		addMember(l,"FromRoleID",get_FromRoleID,set_FromRoleID,true);
		addMember(l,"TargetRoleID",get_TargetRoleID,set_TargetRoleID,true);
		addMember(l,"SyncPlayers",get_SyncPlayers,set_SyncPlayers,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(MsgData.MsgInfo));
	}
}
