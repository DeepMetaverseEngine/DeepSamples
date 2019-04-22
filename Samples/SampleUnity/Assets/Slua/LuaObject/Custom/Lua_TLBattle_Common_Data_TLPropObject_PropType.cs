using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLBattle_Common_Data_TLPropObject_PropType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getcurhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.curhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_curhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.curhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getmaxhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.maxhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_maxhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.maxhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getattack(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.attack);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_attack(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.attack);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getdefend(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.defend);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_defend(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.defend);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getmdef(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.mdef);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mdef(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.mdef);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getthrough(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.through);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_through(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.through);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getblock(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.block);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_block(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.block);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int gethit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.hit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_hit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.hit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getdodge(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.dodge);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_dodge(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.dodge);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getcrit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.crit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_crit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.crit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getrescrit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.rescrit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rescrit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.rescrit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getcridamageper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.cridamageper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_cridamageper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.cridamageper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getredcridamageper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.redcridamageper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_redcridamageper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.redcridamageper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getrunspeed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.runspeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_runspeed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.runspeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getautorecoverhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.autorecoverhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_autorecoverhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.autorecoverhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int gettotalreducedamageper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.totalreducedamageper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_totalreducedamageper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.totalreducedamageper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int gettotaldamageper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.totaldamageper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_totaldamageper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.totaldamageper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getthunderdamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.thunderdamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_thunderdamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.thunderdamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getwinddamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.winddamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_winddamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.winddamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int geticedamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.icedamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_icedamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.icedamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getfiredamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.firedamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_firedamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.firedamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getsoildamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.soildamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_soildamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.soildamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getthunderresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.thunderresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_thunderresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.thunderresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getwindresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.windresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_windresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.windresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int geticeresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.iceresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_iceresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.iceresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getfireresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.fireresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_fireresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.fireresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getsoilresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.soilresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_soilresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.soilresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getallelementdamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.allelementdamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_allelementdamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.allelementdamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getallelementresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.allelementresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_allelementresist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.allelementresist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getonhitrecoverhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.onhitrecoverhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_onhitrecoverhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.onhitrecoverhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getkillrecoverhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.killrecoverhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_killrecoverhp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.killrecoverhp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getextragoldper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.extragoldper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_extragoldper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.extragoldper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getextraexpper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.extraexpper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_extraexpper(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.extraexpper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int gettargettomonster(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.targettomonster);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_targettomonster(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.targettomonster);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int gettargettoplayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.targettoplayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_targettoplayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.targettoplayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getgoddamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.goddamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_goddamage(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.goddamage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getdefreduction(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.defreduction);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_defreduction(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.defreduction);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getmdefreduction(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.mdefreduction);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mdefreduction(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.mdefreduction);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getextracrit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Data.TLPropObject.PropType.extracrit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_extracrit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Data.TLPropObject.PropType.extracrit);
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
		getTypeTable(l,"TLBattle.RolePropType");
		addMember(l,"curhp",getcurhp,null,false);
		addMember(l,"_curhp",get_curhp,null,false);
		addMember(l,"maxhp",getmaxhp,null,false);
		addMember(l,"_maxhp",get_maxhp,null,false);
		addMember(l,"attack",getattack,null,false);
		addMember(l,"_attack",get_attack,null,false);
		addMember(l,"defend",getdefend,null,false);
		addMember(l,"_defend",get_defend,null,false);
		addMember(l,"mdef",getmdef,null,false);
		addMember(l,"_mdef",get_mdef,null,false);
		addMember(l,"through",getthrough,null,false);
		addMember(l,"_through",get_through,null,false);
		addMember(l,"block",getblock,null,false);
		addMember(l,"_block",get_block,null,false);
		addMember(l,"hit",gethit,null,false);
		addMember(l,"_hit",get_hit,null,false);
		addMember(l,"dodge",getdodge,null,false);
		addMember(l,"_dodge",get_dodge,null,false);
		addMember(l,"crit",getcrit,null,false);
		addMember(l,"_crit",get_crit,null,false);
		addMember(l,"rescrit",getrescrit,null,false);
		addMember(l,"_rescrit",get_rescrit,null,false);
		addMember(l,"cridamageper",getcridamageper,null,false);
		addMember(l,"_cridamageper",get_cridamageper,null,false);
		addMember(l,"redcridamageper",getredcridamageper,null,false);
		addMember(l,"_redcridamageper",get_redcridamageper,null,false);
		addMember(l,"runspeed",getrunspeed,null,false);
		addMember(l,"_runspeed",get_runspeed,null,false);
		addMember(l,"autorecoverhp",getautorecoverhp,null,false);
		addMember(l,"_autorecoverhp",get_autorecoverhp,null,false);
		addMember(l,"totalreducedamageper",gettotalreducedamageper,null,false);
		addMember(l,"_totalreducedamageper",get_totalreducedamageper,null,false);
		addMember(l,"totaldamageper",gettotaldamageper,null,false);
		addMember(l,"_totaldamageper",get_totaldamageper,null,false);
		addMember(l,"thunderdamage",getthunderdamage,null,false);
		addMember(l,"_thunderdamage",get_thunderdamage,null,false);
		addMember(l,"winddamage",getwinddamage,null,false);
		addMember(l,"_winddamage",get_winddamage,null,false);
		addMember(l,"icedamage",geticedamage,null,false);
		addMember(l,"_icedamage",get_icedamage,null,false);
		addMember(l,"firedamage",getfiredamage,null,false);
		addMember(l,"_firedamage",get_firedamage,null,false);
		addMember(l,"soildamage",getsoildamage,null,false);
		addMember(l,"_soildamage",get_soildamage,null,false);
		addMember(l,"thunderresist",getthunderresist,null,false);
		addMember(l,"_thunderresist",get_thunderresist,null,false);
		addMember(l,"windresist",getwindresist,null,false);
		addMember(l,"_windresist",get_windresist,null,false);
		addMember(l,"iceresist",geticeresist,null,false);
		addMember(l,"_iceresist",get_iceresist,null,false);
		addMember(l,"fireresist",getfireresist,null,false);
		addMember(l,"_fireresist",get_fireresist,null,false);
		addMember(l,"soilresist",getsoilresist,null,false);
		addMember(l,"_soilresist",get_soilresist,null,false);
		addMember(l,"allelementdamage",getallelementdamage,null,false);
		addMember(l,"_allelementdamage",get_allelementdamage,null,false);
		addMember(l,"allelementresist",getallelementresist,null,false);
		addMember(l,"_allelementresist",get_allelementresist,null,false);
		addMember(l,"onhitrecoverhp",getonhitrecoverhp,null,false);
		addMember(l,"_onhitrecoverhp",get_onhitrecoverhp,null,false);
		addMember(l,"killrecoverhp",getkillrecoverhp,null,false);
		addMember(l,"_killrecoverhp",get_killrecoverhp,null,false);
		addMember(l,"extragoldper",getextragoldper,null,false);
		addMember(l,"_extragoldper",get_extragoldper,null,false);
		addMember(l,"extraexpper",getextraexpper,null,false);
		addMember(l,"_extraexpper",get_extraexpper,null,false);
		addMember(l,"targettomonster",gettargettomonster,null,false);
		addMember(l,"_targettomonster",get_targettomonster,null,false);
		addMember(l,"targettoplayer",gettargettoplayer,null,false);
		addMember(l,"_targettoplayer",get_targettoplayer,null,false);
		addMember(l,"goddamage",getgoddamage,null,false);
		addMember(l,"_goddamage",get_goddamage,null,false);
		addMember(l,"defreduction",getdefreduction,null,false);
		addMember(l,"_defreduction",get_defreduction,null,false);
		addMember(l,"mdefreduction",getmdefreduction,null,false);
		addMember(l,"_mdefreduction",get_mdefreduction,null,false);
		addMember(l,"extracrit",getextracrit,null,false);
		addMember(l,"_extracrit",get_extracrit,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLBattle.Common.Data.TLPropObject.PropType));
	}
}
