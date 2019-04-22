using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_TeamMember : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember o;
			o=new TLProtocol.Data.TeamMember();
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
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
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
	static public int get_RoleID(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RoleID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RoleID(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.RoleID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ServerGroupID(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ServerGroupID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ServerGroupID(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.ServerGroupID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SessionName(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SessionName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SessionName(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.SessionName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsFollowLeader(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsFollowLeader);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsFollowLeader(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsFollowLeader=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InnerState(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.InnerState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_InnerState(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			System.Byte v;
			checkType(l,2,out v);
			self.InnerState=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Pro(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Pro);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Pro(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			System.Byte v;
			checkType(l,2,out v);
			self.Pro=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Gender(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Gender);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Gender(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			System.Byte v;
			checkType(l,2,out v);
			self.Gender=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Level(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Level);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Level(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Level=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_State(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.State);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_State(IntPtr l) {
		try {
			TLProtocol.Data.TeamMember self=(TLProtocol.Data.TeamMember)checkSelf(l);
			TLProtocol.Data.TeamMember.RoleState v;
			checkEnum(l,2,out v);
			self.State=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLProtocol.Data.TeamMember");
		addMember(l,Clone);
		addMember(l,"RoleID",get_RoleID,set_RoleID,true);
		addMember(l,"ServerGroupID",get_ServerGroupID,set_ServerGroupID,true);
		addMember(l,"SessionName",get_SessionName,set_SessionName,true);
		addMember(l,"IsFollowLeader",get_IsFollowLeader,set_IsFollowLeader,true);
		addMember(l,"InnerState",get_InnerState,set_InnerState,true);
		addMember(l,"Pro",get_Pro,set_Pro,true);
		addMember(l,"Gender",get_Gender,set_Gender,true);
		addMember(l,"Level",get_Level,set_Level,true);
		addMember(l,"State",get_State,set_State,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLProtocol.Data.TeamMember));
	}
}
