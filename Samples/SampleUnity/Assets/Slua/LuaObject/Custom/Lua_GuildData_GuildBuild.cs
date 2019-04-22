using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_GuildData_GuildBuild : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOffice(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.GuildBuild.Office);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Office(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.GuildBuild.Office);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStable(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.GuildBuild.Stable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Stable(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.GuildBuild.Stable);
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
			pushValue(l,GuildData.GuildBuild.Monster);
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
			pushValue(l,(double)GuildData.GuildBuild.Monster);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCollege(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.GuildBuild.College);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_College(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.GuildBuild.College);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStore(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,GuildData.GuildBuild.Store);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Store(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)GuildData.GuildBuild.Store);
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
		getTypeTable(l,"GuildData.GuildBuild");
		addMember(l,"Office",getOffice,null,false);
		addMember(l,"_Office",get_Office,null,false);
		addMember(l,"Stable",getStable,null,false);
		addMember(l,"_Stable",get_Stable,null,false);
		addMember(l,"Monster",getMonster,null,false);
		addMember(l,"_Monster",get_Monster,null,false);
		addMember(l,"College",getCollege,null,false);
		addMember(l,"_College",get_College,null,false);
		addMember(l,"Store",getStore,null,false);
		addMember(l,"_Store",get_Store,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(GuildData.GuildBuild));
	}
}
