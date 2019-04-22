using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_GuildData_NotiFyStatus : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLevel(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.NotiFyStatus.Level);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Level(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.NotiFyStatus.Level);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.NotiFyStatus.Build);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Build(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.NotiFyStatus.Build);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMonster(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.NotiFyStatus.Monster);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Monster(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.NotiFyStatus.Monster);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDonate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.NotiFyStatus.Donate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Donate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.NotiFyStatus.Donate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPosition(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.NotiFyStatus.Position);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Position(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.NotiFyStatus.Position);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTalent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.NotiFyStatus.Talent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Talent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.NotiFyStatus.Talent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getALL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.NotiFyStatus.ALL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ALL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.NotiFyStatus.ALL);
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
		getTypeTable(l,"GuildData.NotiFyStatus");
		addMember(l,"Level",getLevel,null,false);
		addMember(l,"_Level",get_Level,null,false);
		addMember(l,"Build",getBuild,null,false);
		addMember(l,"_Build",get_Build,null,false);
		addMember(l,"Monster",getMonster,null,false);
		addMember(l,"_Monster",get_Monster,null,false);
		addMember(l,"Donate",getDonate,null,false);
		addMember(l,"_Donate",get_Donate,null,false);
		addMember(l,"Position",getPosition,null,false);
		addMember(l,"_Position",get_Position,null,false);
		addMember(l,"Talent",getTalent,null,false);
		addMember(l,"_Talent",get_Talent,null,false);
		addMember(l,"ALL",getALL,null,false);
		addMember(l,"_ALL",get_ALL,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(GuildData.NotiFyStatus));
	}
}
