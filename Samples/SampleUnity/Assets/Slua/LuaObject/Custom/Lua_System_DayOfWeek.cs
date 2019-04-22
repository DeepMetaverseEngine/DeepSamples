using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_System_DayOfWeek : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSunday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.DayOfWeek.Sunday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Sunday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)System.DayOfWeek.Sunday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMonday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.DayOfWeek.Monday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Monday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)System.DayOfWeek.Monday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTuesday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.DayOfWeek.Tuesday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Tuesday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)System.DayOfWeek.Tuesday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWednesday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.DayOfWeek.Wednesday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Wednesday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)System.DayOfWeek.Wednesday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getThursday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.DayOfWeek.Thursday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Thursday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)System.DayOfWeek.Thursday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFriday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.DayOfWeek.Friday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Friday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)System.DayOfWeek.Friday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSaturday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,System.DayOfWeek.Saturday);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Saturday(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)System.DayOfWeek.Saturday);
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
		getTypeTable(l,"DayOfWeek");
		addMember(l,"Sunday",getSunday,null,false);
		addMember(l,"_Sunday",get_Sunday,null,false);
		addMember(l,"Monday",getMonday,null,false);
		addMember(l,"_Monday",get_Monday,null,false);
		addMember(l,"Tuesday",getTuesday,null,false);
		addMember(l,"_Tuesday",get_Tuesday,null,false);
		addMember(l,"Wednesday",getWednesday,null,false);
		addMember(l,"_Wednesday",get_Wednesday,null,false);
		addMember(l,"Thursday",getThursday,null,false);
		addMember(l,"_Thursday",get_Thursday,null,false);
		addMember(l,"Friday",getFriday,null,false);
		addMember(l,"_Friday",get_Friday,null,false);
		addMember(l,"Saturday",getSaturday,null,false);
		addMember(l,"_Saturday",get_Saturday,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(System.DayOfWeek));
	}
}
