using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLBattle_Common_Plugins_PKInfo_PKMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPeace(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.PKInfo.PKMode.Peace);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Peace(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.PKInfo.PKMode.Peace);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getJustice(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.PKInfo.PKMode.Justice);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Justice(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.PKInfo.PKMode.Justice);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCamp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.PKInfo.PKMode.Camp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Camp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.PKInfo.PKMode.Camp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getGuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.PKInfo.PKMode.Guild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Guild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.PKInfo.PKMode.Guild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTeam(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.PKInfo.PKMode.Team);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Team(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.PKInfo.PKMode.Team);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getServer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.PKInfo.PKMode.Server);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Server(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.PKInfo.PKMode.Server);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAll(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.PKInfo.PKMode.All);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_All(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.PKInfo.PKMode.All);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getGroup(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.PKInfo.PKMode.Group);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Group(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.PKInfo.PKMode.Group);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRevenger(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.PKInfo.PKMode.Revenger);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Revenger(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.PKInfo.PKMode.Revenger);
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
		getTypeTable(l,"PKInfo.PKMode");
		addMember(l,"Peace",getPeace,null,false);
		addMember(l,"_Peace",get_Peace,null,false);
		addMember(l,"Justice",getJustice,null,false);
		addMember(l,"_Justice",get_Justice,null,false);
		addMember(l,"Camp",getCamp,null,false);
		addMember(l,"_Camp",get_Camp,null,false);
		addMember(l,"Guild",getGuild,null,false);
		addMember(l,"_Guild",get_Guild,null,false);
		addMember(l,"Team",getTeam,null,false);
		addMember(l,"_Team",get_Team,null,false);
		addMember(l,"Server",getServer,null,false);
		addMember(l,"_Server",get_Server,null,false);
		addMember(l,"All",getAll,null,false);
		addMember(l,"_All",get_All,null,false);
		addMember(l,"Group",getGroup,null,false);
		addMember(l,"_Group",get_Group,null,false);
		addMember(l,"Revenger",getRevenger,null,false);
		addMember(l,"_Revenger",get_Revenger,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLBattle.Common.Plugins.PKInfo.PKMode));
	}
}
