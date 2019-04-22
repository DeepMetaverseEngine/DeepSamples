using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DataMgr : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DataMgr o;
			o=new DataMgr();
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
	static public int InitNetWork(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			self.InitNetWork();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Update(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.Update(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clear(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Clear(a1,a2);
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
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DataMgr.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AccountData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AccountData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SettingData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SettingData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LoginData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LoginData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UserData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UserData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DropItemManager(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DropItemManager);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TeamData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TeamData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FlagPushData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FlagPushData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.QuestData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestMangerData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.QuestMangerData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MsgData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MsgData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GuildData(IntPtr l) {
		try {
			DataMgr self=(DataMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GuildData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DataMgr");
		addMember(l,InitNetWork);
		addMember(l,Update);
		addMember(l,Clear);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"AccountData",get_AccountData,null,true);
		addMember(l,"SettingData",get_SettingData,null,true);
		addMember(l,"LoginData",get_LoginData,null,true);
		addMember(l,"UserData",get_UserData,null,true);
		addMember(l,"DropItemManager",get_DropItemManager,null,true);
		addMember(l,"TeamData",get_TeamData,null,true);
		addMember(l,"FlagPushData",get_FlagPushData,null,true);
		addMember(l,"QuestData",get_QuestData,null,true);
		addMember(l,"QuestMangerData",get_QuestMangerData,null,true);
		addMember(l,"MsgData",get_MsgData,null,true);
		addMember(l,"GuildData",get_GuildData,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DataMgr));
	}
}
