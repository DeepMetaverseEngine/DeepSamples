using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TeamData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TeamData o;
			o=new TeamData();
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
	static public int UploadSetting(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			TLProtocol.Data.TeamSetting a1;
			checkType(l,2,out a1);
			System.Action a2;
			checkDelegate(l,3,out a2);
			System.Action a3;
			checkDelegate(l,4,out a3);
			self.UploadSetting(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RequestFollowLeader(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Action<System.Boolean> a2;
			checkDelegate(l,3,out a2);
			self.RequestFollowLeader(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int InitNetWork(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			self.InitNetWork();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateTeam(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.Action<System.Boolean> a1;
			checkDelegate(l,2,out a1);
			self.CreateTeam(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsLeader(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				TeamData self=(TeamData)checkSelf(l);
				var ret=self.IsLeader();
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==2){
				TeamData self=(TeamData)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				var ret=self.IsLeader(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function IsLeader to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsTeamMember(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.IsTeamMember(a1);
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
	static public int RequestLeaveTeam(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.Action<System.Boolean> a1;
			checkDelegate(l,2,out a1);
			self.RequestLeaveTeam(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AttachObserver(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			IObserverExt<TeamData> a1;
			checkType(l,2,out a1);
			self.AttachObserver(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachObserver(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			IObserverExt<TeamData> a1;
			checkType(l,2,out a1);
			self.DetachObserver(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AttachLuaObserver(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			SLua.LuaTable a2;
			checkType(l,3,out a2);
			self.AttachLuaObserver(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachLuaObserver(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.DetachLuaObserver(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Notify(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			self.Notify(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsSameScene(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.IsSameScene(a1);
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
	static public int Update(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.Update(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clear(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Clear(a1,a2);
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
	static public int get_MatchNotify(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MatchNotify);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MatchNotify(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			TLProtocol.Protocol.Client.ClientMatchStateNotify v;
			checkType(l,2,out v);
			self.MatchNotify=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HasTeam(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HasTeam);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TeamId(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TeamId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MemberCount(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MemberCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsTeamFull(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsTeamFull);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeaderID(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LeaderID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Setting(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Setting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AllMembers(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AllMembers);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsFollowLeader(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsFollowLeader);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurrentMatchCountdown(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentMatchCountdown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsInMatch(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsInMatch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MatchingName(IntPtr l) {
		try {
			TeamData self=(TeamData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MatchingName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TeamData");
		addMember(l,UploadSetting);
		addMember(l,RequestFollowLeader);
		addMember(l,InitNetWork);
		addMember(l,CreateTeam);
		addMember(l,IsLeader);
		addMember(l,IsTeamMember);
		addMember(l,RequestLeaveTeam);
		addMember(l,AttachObserver);
		addMember(l,DetachObserver);
		addMember(l,AttachLuaObserver);
		addMember(l,DetachLuaObserver);
		addMember(l,Notify);
		addMember(l,IsSameScene);
		addMember(l,Update);
		addMember(l,Clear);
		addMember(l,"MatchNotify",get_MatchNotify,set_MatchNotify,true);
		addMember(l,"HasTeam",get_HasTeam,null,true);
		addMember(l,"TeamId",get_TeamId,null,true);
		addMember(l,"MemberCount",get_MemberCount,null,true);
		addMember(l,"IsTeamFull",get_IsTeamFull,null,true);
		addMember(l,"LeaderID",get_LeaderID,null,true);
		addMember(l,"Setting",get_Setting,null,true);
		addMember(l,"AllMembers",get_AllMembers,null,true);
		addMember(l,"IsFollowLeader",get_IsFollowLeader,null,true);
		addMember(l,"CurrentMatchCountdown",get_CurrentMatchCountdown,null,true);
		addMember(l,"IsInMatch",get_IsInMatch,null,true);
		addMember(l,"MatchingName",get_MatchingName,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TeamData));
	}
}
