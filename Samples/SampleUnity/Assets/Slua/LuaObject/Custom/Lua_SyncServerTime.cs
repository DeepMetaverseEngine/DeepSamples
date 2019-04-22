using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_SyncServerTime : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			SyncServerTime o;
			o=new SyncServerTime();
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
	static public int SyncTime(IntPtr l) {
		try {
			SyncServerTime self=(SyncServerTime)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SyncTime(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetServerTimeUTC(IntPtr l) {
		try {
			SyncServerTime self=(SyncServerTime)checkSelf(l);
			var ret=self.GetServerTimeUTC();
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
	static public int GetServerTimeUTCTicks(IntPtr l) {
		try {
			SyncServerTime self=(SyncServerTime)checkSelf(l);
			var ret=self.GetServerTimeUTCTicks();
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
	static public int GetTodayTimeToUtcTime(IntPtr l) {
		try {
			SyncServerTime self=(SyncServerTime)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetTodayTimeToUtcTime(a1);
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
	static public int GetTodayOfWeek(IntPtr l) {
		try {
			SyncServerTime self=(SyncServerTime)checkSelf(l);
			var ret=self.GetTodayOfWeek();
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
	static public int IsInToday(IntPtr l) {
		try {
			SyncServerTime self=(SyncServerTime)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.IsInToday(a1);
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
	static public int IsBetweenTime(IntPtr l) {
		try {
			SyncServerTime self=(SyncServerTime)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.IsBetweenTime(a1,a2);
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
	static public int IsBetweenTimes(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(SLua.LuaTable),typeof(SLua.LuaTable))){
				SyncServerTime self=(SyncServerTime)checkSelf(l);
				SLua.LuaTable a1;
				checkType(l,2,out a1);
				SLua.LuaTable a2;
				checkType(l,3,out a2);
				var ret=self.IsBetweenTimes(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(List<System.String>),typeof(List<System.String>))){
				SyncServerTime self=(SyncServerTime)checkSelf(l);
				System.Collections.Generic.List<System.String> a1;
				checkType(l,2,out a1);
				System.Collections.Generic.List<System.String> a2;
				checkType(l,3,out a2);
				var ret=self.IsBetweenTimes(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function IsBetweenTimes to call");
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
	static public int get_MASK_WEEK_SUNDAY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SyncServerTime.MASK_WEEK_SUNDAY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MASK_WEEK_MONDAY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SyncServerTime.MASK_WEEK_MONDAY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MASK_WEEK_TUESDAY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SyncServerTime.MASK_WEEK_TUESDAY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MASK_WEEK_WEDNESDAY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SyncServerTime.MASK_WEEK_WEDNESDAY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MASK_WEEK_THURSDAY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SyncServerTime.MASK_WEEK_THURSDAY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MASK_WEEK_FRIDAY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SyncServerTime.MASK_WEEK_FRIDAY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MASK_WEEK_SATURDAY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SyncServerTime.MASK_WEEK_SATURDAY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MASK_WEEK_ALL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SyncServerTime.MASK_WEEK_ALL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_lag(IntPtr l) {
		try {
			SyncServerTime self=(SyncServerTime)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.lag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"SyncServerTime");
		addMember(l,SyncTime);
		addMember(l,GetServerTimeUTC);
		addMember(l,GetServerTimeUTCTicks);
		addMember(l,GetTodayTimeToUtcTime);
		addMember(l,GetTodayOfWeek);
		addMember(l,IsInToday);
		addMember(l,IsBetweenTime);
		addMember(l,IsBetweenTimes);
		addMember(l,"MASK_WEEK_SUNDAY",get_MASK_WEEK_SUNDAY,null,false);
		addMember(l,"MASK_WEEK_MONDAY",get_MASK_WEEK_MONDAY,null,false);
		addMember(l,"MASK_WEEK_TUESDAY",get_MASK_WEEK_TUESDAY,null,false);
		addMember(l,"MASK_WEEK_WEDNESDAY",get_MASK_WEEK_WEDNESDAY,null,false);
		addMember(l,"MASK_WEEK_THURSDAY",get_MASK_WEEK_THURSDAY,null,false);
		addMember(l,"MASK_WEEK_FRIDAY",get_MASK_WEEK_FRIDAY,null,false);
		addMember(l,"MASK_WEEK_SATURDAY",get_MASK_WEEK_SATURDAY,null,false);
		addMember(l,"MASK_WEEK_ALL",get_MASK_WEEK_ALL,null,false);
		addMember(l,"lag",get_lag,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(SyncServerTime));
	}
}
