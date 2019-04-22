using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_HudManager_HudName : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPlayerInfo(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HudName.PlayerInfo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PlayerInfo(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)HudManager.HudName.PlayerInfo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSmallMap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HudName.SmallMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SmallMap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)HudManager.HudName.SmallMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInteractive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HudName.Interactive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Interactive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)HudManager.HudName.Interactive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRocker(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HudName.Rocker);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rocker(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)HudManager.HudName.Rocker);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSkillBar(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HudName.SkillBar);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SkillBar(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)HudManager.HudName.SkillBar);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTeamQuest(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HudName.TeamQuest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TeamQuest(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)HudManager.HudName.TeamQuest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMainHud(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HudName.MainHud);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MainHud(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)HudManager.HudName.MainHud);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBuffHud(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HudName.BuffHud);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BuffHud(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)HudManager.HudName.BuffHud);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEnd(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HudName.End);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_End(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)HudManager.HudName.End);
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
		getTypeTable(l,"HudManager.HudName");
		addMember(l,"PlayerInfo",getPlayerInfo,null,false);
		addMember(l,"_PlayerInfo",get_PlayerInfo,null,false);
		addMember(l,"SmallMap",getSmallMap,null,false);
		addMember(l,"_SmallMap",get_SmallMap,null,false);
		addMember(l,"Interactive",getInteractive,null,false);
		addMember(l,"_Interactive",get_Interactive,null,false);
		addMember(l,"Rocker",getRocker,null,false);
		addMember(l,"_Rocker",get_Rocker,null,false);
		addMember(l,"SkillBar",getSkillBar,null,false);
		addMember(l,"_SkillBar",get_SkillBar,null,false);
		addMember(l,"TeamQuest",getTeamQuest,null,false);
		addMember(l,"_TeamQuest",get_TeamQuest,null,false);
		addMember(l,"MainHud",getMainHud,null,false);
		addMember(l,"_MainHud",get_MainHud,null,false);
		addMember(l,"BuffHud",getBuffHud,null,false);
		addMember(l,"_BuffHud",get_BuffHud,null,false);
		addMember(l,"End",getEnd,null,false);
		addMember(l,"_End",get_End,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(HudManager.HudName));
	}
}
