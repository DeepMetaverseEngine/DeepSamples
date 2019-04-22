using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GameData_Zone_UnitInfo : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo o;
			o=new DeepCore.GameData.Zone.UnitInfo();
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
	static public int GetID(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			var ret=self.GetID();
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
	static public int Clone(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
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
	static public int WriteExternal(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.IO.IOutputStream a1;
			checkType(l,2,out a1);
			self.WriteExternal(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReadExternal(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.IO.IInputStream a1;
			checkType(l,2,out a1);
			self.ReadExternal(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetSkillByID(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetSkillByID(a1);
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
	static public int get_UType(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UType(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.Zone.UnitInfo.UnitType v;
			checkEnum(l,2,out v);
			self.UType=v;
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
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
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
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
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
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
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
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
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
	static public int get_IsElite(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsElite);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsElite(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsElite=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FileName(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FileName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FileName(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.FileName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SpawnEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SpawnEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SpawnEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.Zone.LaunchEffect v;
			checkType(l,2,out v);
			self.SpawnEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DeadEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DeadEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DeadEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.Zone.LaunchEffect v;
			checkType(l,2,out v);
			self.DeadEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RemovedEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RemovedEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RemovedEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.Zone.LaunchEffect v;
			checkType(l,2,out v);
			self.RemovedEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CrushEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CrushEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_CrushEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.Zone.LaunchEffect v;
			checkType(l,2,out v);
			self.CrushEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DamageEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DamageEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DamageEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.Zone.LaunchEffect v;
			checkType(l,2,out v);
			self.DamageEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LevelUpEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LevelUpEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LevelUpEffect(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.Zone.LaunchEffect v;
			checkType(l,2,out v);
			self.LevelUpEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BodyScale(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BodyScale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_BodyScale(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.BodyScale=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BodyHeight(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BodyHeight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_BodyHeight(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.BodyHeight=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BodySize(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BodySize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_BodySize(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.BodySize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BodyHitSize(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BodyHitSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_BodyHitSize(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.BodyHitSize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GuardRange(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GuardRange);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_GuardRange(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.GuardRange=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GuardRangeLimit(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GuardRangeLimit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_GuardRangeLimit(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.GuardRangeLimit=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GuardRangeGroup(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GuardRangeGroup);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_GuardRangeGroup(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.GuardRangeGroup=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RecoveryIntervalMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RecoveryIntervalMS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RecoveryIntervalMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.RecoveryIntervalMS=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HealthRecoveryPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HealthRecoveryPoint);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_HealthRecoveryPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.HealthRecoveryPoint=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ManaRecoveryPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ManaRecoveryPoint);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ManaRecoveryPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.ManaRecoveryPoint=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Weight(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Weight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Weight(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Weight=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsMoveable(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsMoveable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsMoveable(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsMoveable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsTurnable(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsTurnable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsTurnable(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsTurnable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MoveSpeedSEC(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MoveSpeedSEC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MoveSpeedSEC(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.MoveSpeedSEC=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TurnSpeedSEC(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TurnSpeedSEC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TurnSpeedSEC(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.TurnSpeedSEC=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DamageTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DamageTimeMS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DamageTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.DamageTimeMS=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BaseSkillID(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BaseSkillID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_BaseSkillID(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.Zone.LaunchSkill v;
			checkType(l,2,out v);
			self.BaseSkillID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Skills(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Skills);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Skills(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.ArrayList<DeepCore.GameData.Zone.LaunchSkill> v;
			checkType(l,2,out v);
			self.Skills=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LaunchSpellHeight(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LaunchSpellHeight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LaunchSpellHeight(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.LaunchSpellHeight=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LaunchSpellAngle(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LaunchSpellAngle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LaunchSpellAngle(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.LaunchSpellAngle=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LaunchSpellRadius(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LaunchSpellRadius);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LaunchSpellRadius(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.LaunchSpellRadius=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DeadLaunchSpell(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DeadLaunchSpell);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DeadLaunchSpell(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.Zone.LaunchSpell v;
			checkType(l,2,out v);
			self.DeadLaunchSpell=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HealthPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HealthPoint);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_HealthPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.HealthPoint=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ManaPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ManaPoint);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ManaPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.ManaPoint=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StaminaPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.StaminaPoint);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_StaminaPoint(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.StaminaPoint=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GenExp(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GenExp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_GenExp(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.GenExp=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DropItemsSet(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DropItemsSet);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DropItemsSet(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.ArrayList<DeepCore.GameData.Zone.DropItemList> v;
			checkType(l,2,out v);
			self.DropItemsSet=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DropMoney(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DropMoney);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DropMoney(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.DropMoney=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InventorySize(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.InventorySize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_InventorySize(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.InventorySize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InventoryList(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.InventoryList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_InventoryList(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.ArrayList<DeepCore.GameData.Zone.InventoryItem> v;
			checkType(l,2,out v);
			self.InventoryList=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LifeTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LifeTimeMS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LifeTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.LifeTimeMS=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SpawnTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SpawnTimeMS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SpawnTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.SpawnTimeMS=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DeadTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DeadTimeMS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DeadTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.DeadTimeMS=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RebirthTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RebirthTimeMS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RebirthTimeMS(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.RebirthTimeMS=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UserTag(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UserTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UserTag(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.UserTag=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDynamic(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDynamic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsDynamic(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsDynamic=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Events(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Events);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Events(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.ArrayList<System.Int32> v;
			checkType(l,2,out v);
			self.Events=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Properties(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Properties);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Properties(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			DeepCore.GameData.IUnitProperties v;
			checkType(l,2,out v);
			self.Properties=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Attributes(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Attributes);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Attributes(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			System.String[] v;
			checkArray(l,2,out v);
			self.Attributes=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TemplateID(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TemplateID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EditorPath(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EditorPath);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EditorPath(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.EditorPath=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UTypeAsInt(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UTypeAsInt);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IdleRecover(IntPtr l) {
		try {
			DeepCore.GameData.Zone.UnitInfo self=(DeepCore.GameData.Zone.UnitInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IdleRecover);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnitInfo");
		addMember(l,GetID);
		addMember(l,Clone);
		addMember(l,WriteExternal);
		addMember(l,ReadExternal);
		addMember(l,GetSkillByID);
		addMember(l,"UType",get_UType,set_UType,true);
		addMember(l,"ID",get_ID,set_ID,true);
		addMember(l,"Name",get_Name,set_Name,true);
		addMember(l,"IsElite",get_IsElite,set_IsElite,true);
		addMember(l,"FileName",get_FileName,set_FileName,true);
		addMember(l,"SpawnEffect",get_SpawnEffect,set_SpawnEffect,true);
		addMember(l,"DeadEffect",get_DeadEffect,set_DeadEffect,true);
		addMember(l,"RemovedEffect",get_RemovedEffect,set_RemovedEffect,true);
		addMember(l,"CrushEffect",get_CrushEffect,set_CrushEffect,true);
		addMember(l,"DamageEffect",get_DamageEffect,set_DamageEffect,true);
		addMember(l,"LevelUpEffect",get_LevelUpEffect,set_LevelUpEffect,true);
		addMember(l,"BodyScale",get_BodyScale,set_BodyScale,true);
		addMember(l,"BodyHeight",get_BodyHeight,set_BodyHeight,true);
		addMember(l,"BodySize",get_BodySize,set_BodySize,true);
		addMember(l,"BodyHitSize",get_BodyHitSize,set_BodyHitSize,true);
		addMember(l,"GuardRange",get_GuardRange,set_GuardRange,true);
		addMember(l,"GuardRangeLimit",get_GuardRangeLimit,set_GuardRangeLimit,true);
		addMember(l,"GuardRangeGroup",get_GuardRangeGroup,set_GuardRangeGroup,true);
		addMember(l,"RecoveryIntervalMS",get_RecoveryIntervalMS,set_RecoveryIntervalMS,true);
		addMember(l,"HealthRecoveryPoint",get_HealthRecoveryPoint,set_HealthRecoveryPoint,true);
		addMember(l,"ManaRecoveryPoint",get_ManaRecoveryPoint,set_ManaRecoveryPoint,true);
		addMember(l,"Weight",get_Weight,set_Weight,true);
		addMember(l,"IsMoveable",get_IsMoveable,set_IsMoveable,true);
		addMember(l,"IsTurnable",get_IsTurnable,set_IsTurnable,true);
		addMember(l,"MoveSpeedSEC",get_MoveSpeedSEC,set_MoveSpeedSEC,true);
		addMember(l,"TurnSpeedSEC",get_TurnSpeedSEC,set_TurnSpeedSEC,true);
		addMember(l,"DamageTimeMS",get_DamageTimeMS,set_DamageTimeMS,true);
		addMember(l,"BaseSkillID",get_BaseSkillID,set_BaseSkillID,true);
		addMember(l,"Skills",get_Skills,set_Skills,true);
		addMember(l,"LaunchSpellHeight",get_LaunchSpellHeight,set_LaunchSpellHeight,true);
		addMember(l,"LaunchSpellAngle",get_LaunchSpellAngle,set_LaunchSpellAngle,true);
		addMember(l,"LaunchSpellRadius",get_LaunchSpellRadius,set_LaunchSpellRadius,true);
		addMember(l,"DeadLaunchSpell",get_DeadLaunchSpell,set_DeadLaunchSpell,true);
		addMember(l,"HealthPoint",get_HealthPoint,set_HealthPoint,true);
		addMember(l,"ManaPoint",get_ManaPoint,set_ManaPoint,true);
		addMember(l,"StaminaPoint",get_StaminaPoint,set_StaminaPoint,true);
		addMember(l,"GenExp",get_GenExp,set_GenExp,true);
		addMember(l,"DropItemsSet",get_DropItemsSet,set_DropItemsSet,true);
		addMember(l,"DropMoney",get_DropMoney,set_DropMoney,true);
		addMember(l,"InventorySize",get_InventorySize,set_InventorySize,true);
		addMember(l,"InventoryList",get_InventoryList,set_InventoryList,true);
		addMember(l,"LifeTimeMS",get_LifeTimeMS,set_LifeTimeMS,true);
		addMember(l,"SpawnTimeMS",get_SpawnTimeMS,set_SpawnTimeMS,true);
		addMember(l,"DeadTimeMS",get_DeadTimeMS,set_DeadTimeMS,true);
		addMember(l,"RebirthTimeMS",get_RebirthTimeMS,set_RebirthTimeMS,true);
		addMember(l,"UserTag",get_UserTag,set_UserTag,true);
		addMember(l,"IsDynamic",get_IsDynamic,set_IsDynamic,true);
		addMember(l,"Events",get_Events,set_Events,true);
		addMember(l,"Properties",get_Properties,set_Properties,true);
		addMember(l,"Attributes",get_Attributes,set_Attributes,true);
		addMember(l,"TemplateID",get_TemplateID,null,true);
		addMember(l,"EditorPath",get_EditorPath,set_EditorPath,true);
		addMember(l,"UTypeAsInt",get_UTypeAsInt,null,true);
		addMember(l,"IdleRecover",get_IdleRecover,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.GameData.Zone.UnitInfo));
	}
}
