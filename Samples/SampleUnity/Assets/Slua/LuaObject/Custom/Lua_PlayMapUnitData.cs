using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_PlayMapUnitData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			PlayMapUnitData o;
			o=new PlayMapUnitData();
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
	static public int get_templateId(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.templateId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_templateId(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.templateId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ID(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ID(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.ID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Name(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Name);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Name(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.Name=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ICON(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ICON);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ICON(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.ICON=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_X(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.X);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_X(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.X=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Y(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Y);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Y(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.Y=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UnitType(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UnitType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UnitType(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			DeepCore.GameData.Zone.UnitInfo.UnitType v;
			checkEnum(l,2,out v);
			self.UnitType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ForceType(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ForceType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ForceType(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.ForceType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_isTeamMate(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.isTeamMate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_isTeamMate(IntPtr l) {
		try {
			PlayMapUnitData self=(PlayMapUnitData)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.isTeamMate=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"PlayMapUnitData");
		addMember(l,"templateId",get_templateId,set_templateId,true);
		addMember(l,"ID",get_ID,set_ID,true);
		addMember(l,"Name",get_Name,set_Name,true);
		addMember(l,"ICON",get_ICON,set_ICON,true);
		addMember(l,"X",get_X,set_X,true);
		addMember(l,"Y",get_Y,set_Y,true);
		addMember(l,"UnitType",get_UnitType,set_UnitType,true);
		addMember(l,"ForceType",get_ForceType,set_ForceType,true);
		addMember(l,"isTeamMate",get_isTeamMate,set_isTeamMate,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(PlayMapUnitData));
	}
}
