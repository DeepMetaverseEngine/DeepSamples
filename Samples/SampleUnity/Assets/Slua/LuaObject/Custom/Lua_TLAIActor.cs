using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAIActor : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Dead(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			var ret=self.Dead();
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
	static public int IsActor(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			var ret=self.IsActor();
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
	static public int SoundImportant(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			var ret=self.SoundImportant();
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
	static public int SendUnitStop(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			self.SendUnitStop();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopTeamFollow(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			self.StopTeamFollow();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StartTeamFollow(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.StartTeamFollow(a1);
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
	static public int UpdateTeamFollow(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.UpdateTeamFollow(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsImportant(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			var ret=self.IsImportant();
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
	static public int isNoBattleStatus(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			var ret=self.isNoBattleStatus();
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
	static public int UnMount(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			self.UnMount();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnCreate(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
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
	static public int BtnSetAutoGuard(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.BtnSetAutoGuard(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SyncUIBtnState(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.SyncUIBtnState(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SendUnitFocuseTarget(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			self.SendUnitFocuseTarget(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetPKMode(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			var ret=self.GetPKMode();
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
	static public int GetZonePlayersUUID(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			System.Action<TLBattle.Message.GetZonePlayersUUIDResponse> a1;
			checkDelegate(l,2,out a1);
			self.GetZonePlayersUUID(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int InitMoveAgent(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			self.InitMoveAgent();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AutoRunByAction(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			TLAIActor.MoveEndAction a1;
			checkType(l,2,out a1);
			self.AutoRunByAction(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int BreakAutoRunAgent(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			TLAIActor.MoveEndAction a1;
			checkType(l,2,out a1);
			self.BreakAutoRunAgent(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoStopForceQuest(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			self.DoStopForceQuest();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ChangeAutoAttackState(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.ChangeAutoAttackState(a1);
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
	static public int get_AUTO_BATTLE_OPEN_TAG(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.AUTO_BATTLE_OPEN_TAG);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DontUseCache(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.DontUseCache);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsAutoRun(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsAutoRun);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurGuardStatus(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurGuardStatus);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_CurGuardStatus(IntPtr l) {
		try {
			TLAIActor self=(TLAIActor)checkSelf(l);
			TLAIActor.AutoGuardStatus v;
			checkEnum(l,2,out v);
			self.CurGuardStatus=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLAIActor");
		addMember(l,Dead);
		addMember(l,IsActor);
		addMember(l,SoundImportant);
		addMember(l,SendUnitStop);
		addMember(l,StopTeamFollow);
		addMember(l,StartTeamFollow);
		addMember(l,UpdateTeamFollow);
		addMember(l,IsImportant);
		addMember(l,isNoBattleStatus);
		addMember(l,UnMount);
		addMember(l,OnCreate);
		addMember(l,BtnSetAutoGuard);
		addMember(l,SyncUIBtnState);
		addMember(l,SendUnitFocuseTarget);
		addMember(l,GetPKMode);
		addMember(l,GetZonePlayersUUID);
		addMember(l,InitMoveAgent);
		addMember(l,AutoRunByAction);
		addMember(l,BreakAutoRunAgent);
		addMember(l,DoStopForceQuest);
		addMember(l,ChangeAutoAttackState);
		addMember(l,"AUTO_BATTLE_OPEN_TAG",get_AUTO_BATTLE_OPEN_TAG,null,false);
		addMember(l,"DontUseCache",get_DontUseCache,null,false);
		addMember(l,"IsAutoRun",get_IsAutoRun,null,true);
		addMember(l,"CurGuardStatus",get_CurGuardStatus,set_CurGuardStatus,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLAIActor),typeof(TLAIPlayer));
	}
}
