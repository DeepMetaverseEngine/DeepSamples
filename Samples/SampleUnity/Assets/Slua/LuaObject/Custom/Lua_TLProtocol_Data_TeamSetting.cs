using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_TeamSetting : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting o;
			o=new TLProtocol.Data.TeamSetting();
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
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
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
	static public int get_TargetID(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TargetID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TargetID(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.TargetID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoStartTarget(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AutoStartTarget);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AutoStartTarget(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.AutoStartTarget=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MinLevel(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MinLevel);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MinLevel(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.MinLevel=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MinFightPower(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MinFightPower);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MinFightPower(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.MinFightPower=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoMatch(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AutoMatch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AutoMatch(IntPtr l) {
		try {
			TLProtocol.Data.TeamSetting self=(TLProtocol.Data.TeamSetting)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.AutoMatch=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TeamSetting");
		addMember(l,Clone);
		addMember(l,"TargetID",get_TargetID,set_TargetID,true);
		addMember(l,"AutoStartTarget",get_AutoStartTarget,set_AutoStartTarget,true);
		addMember(l,"MinLevel",get_MinLevel,set_MinLevel,true);
		addMember(l,"MinFightPower",get_MinFightPower,set_MinFightPower,true);
		addMember(l,"AutoMatch",get_AutoMatch,set_AutoMatch,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLProtocol.Data.TeamSetting));
	}
}
