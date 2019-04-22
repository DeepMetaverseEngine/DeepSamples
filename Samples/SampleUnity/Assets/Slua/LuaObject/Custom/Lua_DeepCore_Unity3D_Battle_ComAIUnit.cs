using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_Battle_ComAIUnit : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit o;
			DeepCore.Unity3D.Battle.BattleScene a1;
			checkType(l,2,out a1);
			DeepCore.GameSlave.ZoneUnit a2;
			checkType(l,3,out a2);
			o=new DeepCore.Unity3D.Battle.ComAIUnit(a1,a2);
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
	static public int ForeachAction(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			DeepCore.GameData.Data.UnitActionStatus a1;
			checkEnum(l,2,out a1);
			System.Predicate<DeepCore.Unity3D.Battle.ComAIUnit.ActionStatus> a2;
			checkDelegate(l,3,out a2);
			var ret=self.ForeachAction(a1,a2);
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
	static public int ReplaceAction(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			DeepCore.GameData.Data.UnitActionStatus a1;
			checkEnum(l,2,out a1);
			DeepCore.Unity3D.Battle.ComAIUnit.ActionStatus a2;
			checkType(l,3,out a2);
			var ret=self.ReplaceAction(a1,a2);
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
	static public int RegistAction(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			DeepCore.GameData.Data.UnitActionStatus a1;
			checkEnum(l,2,out a1);
			DeepCore.Unity3D.Battle.ComAIUnit.ActionStatus a2;
			checkType(l,3,out a2);
			var ret=self.RegistAction(a1,a2);
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
	static public int RemoveAction(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			DeepCore.GameData.Data.UnitActionStatus a1;
			checkEnum(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.RemoveAction(a1,a2);
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
	static public int GetTopActionStatus(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			DeepCore.GameData.Data.UnitActionStatus a1;
			checkEnum(l,2,out a1);
			var ret=self.GetTopActionStatus(a1);
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
	static public int SetLockActionStatus(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			DeepCore.Unity3D.Battle.ComAIUnit.ActionStatus a1;
			checkType(l,2,out a1);
			self.SetLockActionStatus(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetDummyNode(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetDummyNode(a1);
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
	static public int AddAvatar(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.AddAvatar(a1,a2);
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
	static public int RemoveAvatar(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			DeepCore.Unity3D.Battle.ComAIUnit.Avatar a1;
			checkType(l,2,out a1);
			self.RemoveAvatar(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetDirection(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.SetDirection(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloneUnitInfo(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			var ret=self.CloneUnitInfo();
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
	static public int OnCreate(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			self.OnCreate();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnLoadNotShow(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			DeepCore.GameData.Zone.LaunchEffect a1;
			checkType(l,2,out a1);
			self.OnLoadNotShow(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnLoadEffectSuccess(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			DeepCore.Unity3D.FuckAssetObject a1;
			checkType(l,2,out a1);
			DeepCore.GameData.Zone.LaunchEffect a2;
			checkType(l,3,out a2);
			UnityEngine.Vector3 a3;
			checkType(l,4,out a3);
			UnityEngine.Quaternion a4;
			checkType(l,5,out a4);
			self.OnLoadEffectSuccess(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
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
	static public int get_CurrentActionStatus(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentActionStatus);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AvatarComparer(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			IComparer<DeepCore.Unity3D.Battle.ComAIUnit.Avatar> v;
			checkType(l,2,out v);
			self.AvatarComparer=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsActive(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsActive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TemplateID(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
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
	static public int get_TemplateName(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TemplateName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PlayerUUID(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PlayerUUID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DisplayName(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DisplayName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Force(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Force);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MaxHP(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MaxHP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HP(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MaxMP(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MaxMP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MP(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Dummy_0(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Dummy_0);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Dummy_1(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Dummy_1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Dummy_2(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Dummy_2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Dummy_3(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Dummy_3);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Dummy_4(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Dummy_4);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurrentTarget(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentTarget);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurrentState(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Info(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Info);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SyncInfo(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SyncInfo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Virtual(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAIUnit self=(DeepCore.Unity3D.Battle.ComAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Virtual);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ComAIUnit");
		addMember(l,ForeachAction);
		addMember(l,ReplaceAction);
		addMember(l,RegistAction);
		addMember(l,RemoveAction);
		addMember(l,GetTopActionStatus);
		addMember(l,SetLockActionStatus);
		addMember(l,GetDummyNode);
		addMember(l,AddAvatar);
		addMember(l,RemoveAvatar);
		addMember(l,SetDirection);
		addMember(l,CloneUnitInfo);
		addMember(l,OnCreate);
		addMember(l,OnLoadNotShow);
		addMember(l,OnLoadEffectSuccess);
		addMember(l,"CurrentActionStatus",get_CurrentActionStatus,null,true);
		addMember(l,"AvatarComparer",null,set_AvatarComparer,true);
		addMember(l,"IsActive",get_IsActive,null,true);
		addMember(l,"TemplateID",get_TemplateID,null,true);
		addMember(l,"TemplateName",get_TemplateName,null,true);
		addMember(l,"PlayerUUID",get_PlayerUUID,null,true);
		addMember(l,"DisplayName",get_DisplayName,null,true);
		addMember(l,"Force",get_Force,null,true);
		addMember(l,"MaxHP",get_MaxHP,null,true);
		addMember(l,"HP",get_HP,null,true);
		addMember(l,"MaxMP",get_MaxMP,null,true);
		addMember(l,"MP",get_MP,null,true);
		addMember(l,"Dummy_0",get_Dummy_0,null,true);
		addMember(l,"Dummy_1",get_Dummy_1,null,true);
		addMember(l,"Dummy_2",get_Dummy_2,null,true);
		addMember(l,"Dummy_3",get_Dummy_3,null,true);
		addMember(l,"Dummy_4",get_Dummy_4,null,true);
		addMember(l,"CurrentTarget",get_CurrentTarget,null,true);
		addMember(l,"CurrentState",get_CurrentState,null,true);
		addMember(l,"Info",get_Info,null,true);
		addMember(l,"SyncInfo",get_SyncInfo,null,true);
		addMember(l,"Virtual",get_Virtual,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.Battle.ComAIUnit),typeof(DeepCore.Unity3D.Battle.ComAICell));
	}
}
