using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAIActor_MoveEndAction : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLAIActor.MoveEndAction o;
			o=new TLAIActor.MoveEndAction();
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
	static public int CreateIntance(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			var ret=self.CreateIntance();
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
	static public int DoEnd(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			TLAIActor.MoveEndAction a1;
			checkType(l,2,out a1);
			var ret=self.DoEnd(a1);
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
	static public int OnUpdate(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			self.OnUpdate();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoAction(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			self.DoAction();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsSame(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			TLAIActor.MoveEndAction a1;
			checkType(l,2,out a1);
			var ret=self.IsSame(a1);
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
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
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
	static public int get_AimX(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AimX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AimX(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.AimX=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AimY(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AimY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AimY(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.AimY=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AimDistance(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AimDistance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AimDistance(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.AimDistance=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MapId(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MapId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MapId(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.MapId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RoadName(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RoadName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RoadName(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.RoadName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_hints(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.hints);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_hints(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.hints=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestId(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.QuestId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_QuestId(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.QuestId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MoveType(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MoveType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MoveType(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.MoveType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsShow(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsShow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsShow(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsShow=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MapUUid(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MapUUid);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MapUUid(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.MapUUid=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_orgActorX(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.orgActorX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_orgActorX(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.orgActorX=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_orgActorY(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.orgActorY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_orgActorY(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.orgActorY=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Action(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			TLAIActor.MoveEndDoAction v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.Action=v;
			else if(op==1) self.Action+=v;
			else if(op==2) self.Action-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsAutoRun(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
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
	static public int set_IsAutoRun(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsAutoRun=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Radar(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Radar);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Radar(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Radar=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsBreak(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsBreak);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsBreak(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsBreak=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnterGuild(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EnterGuild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EnterGuild(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.EnterGuild=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuickTransPortFlag(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.QuickTransPortFlag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_QuickTransPortFlag(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.QuickTransPortFlag=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuickTransPort(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.QuickTransPort);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_QuickTransPort(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.QuickTransPort=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TargetMapId(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TargetMapId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TargetMapId(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.TargetMapId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OrgAimDistance(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.OrgAimDistance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OrgAimDistance(IntPtr l) {
		try {
			TLAIActor.MoveEndAction self=(TLAIActor.MoveEndAction)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.OrgAimDistance=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"MoveEndAction");
		addMember(l,CreateIntance);
		addMember(l,DoEnd);
		addMember(l,OnUpdate);
		addMember(l,DoAction);
		addMember(l,IsSame);
		addMember(l,Clone);
		addMember(l,"AimX",get_AimX,set_AimX,true);
		addMember(l,"AimY",get_AimY,set_AimY,true);
		addMember(l,"AimDistance",get_AimDistance,set_AimDistance,true);
		addMember(l,"MapId",get_MapId,set_MapId,true);
		addMember(l,"RoadName",get_RoadName,set_RoadName,true);
		addMember(l,"hints",get_hints,set_hints,true);
		addMember(l,"QuestId",get_QuestId,set_QuestId,true);
		addMember(l,"MoveType",get_MoveType,set_MoveType,true);
		addMember(l,"IsShow",get_IsShow,set_IsShow,true);
		addMember(l,"MapUUid",get_MapUUid,set_MapUUid,true);
		addMember(l,"orgActorX",get_orgActorX,set_orgActorX,true);
		addMember(l,"orgActorY",get_orgActorY,set_orgActorY,true);
		addMember(l,"Action",null,set_Action,true);
		addMember(l,"IsAutoRun",get_IsAutoRun,set_IsAutoRun,true);
		addMember(l,"Radar",get_Radar,set_Radar,true);
		addMember(l,"IsBreak",get_IsBreak,set_IsBreak,true);
		addMember(l,"EnterGuild",get_EnterGuild,set_EnterGuild,true);
		addMember(l,"QuickTransPortFlag",get_QuickTransPortFlag,set_QuickTransPortFlag,true);
		addMember(l,"QuickTransPort",get_QuickTransPort,set_QuickTransPort,true);
		addMember(l,"TargetMapId",get_TargetMapId,set_TargetMapId,true);
		addMember(l,"OrgAimDistance",get_OrgAimDistance,set_OrgAimDistance,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLAIActor.MoveEndAction));
	}
}
