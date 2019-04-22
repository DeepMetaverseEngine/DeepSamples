using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_BatteryStatus : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUnknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.BatteryStatus.Unknown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Unknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.BatteryStatus.Unknown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCharging(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.BatteryStatus.Charging);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Charging(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.BatteryStatus.Charging);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDischarging(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.BatteryStatus.Discharging);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Discharging(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.BatteryStatus.Discharging);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNotCharging(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.BatteryStatus.NotCharging);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NotCharging(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.BatteryStatus.NotCharging);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFull(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.BatteryStatus.Full);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Full(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.BatteryStatus.Full);
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
		getTypeTable(l,"UnityEngine.BatteryStatus");
		addMember(l,"Unknown",getUnknown,null,false);
		addMember(l,"_Unknown",get_Unknown,null,false);
		addMember(l,"Charging",getCharging,null,false);
		addMember(l,"_Charging",get_Charging,null,false);
		addMember(l,"Discharging",getDischarging,null,false);
		addMember(l,"_Discharging",get_Discharging,null,false);
		addMember(l,"NotCharging",getNotCharging,null,false);
		addMember(l,"_NotCharging",get_NotCharging,null,false);
		addMember(l,"Full",getFull,null,false);
		addMember(l,"_Full",get_Full,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.BatteryStatus));
	}
}
