using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UserData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UserData o;
			o=new UserData();
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
	static public int GetCurMapRadarData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetCurMapRadarData(a1);
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
	static public int FindItemDataByID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.FindItemDataByID(a1);
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
	static public int LuaSaveOptionsData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.LuaSaveOptionsData(a1,a2);
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
			UserData self=(UserData)checkSelf(l);
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
	static public int ReadRoleData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			TLProtocol.Data.TLClientRoleData a1;
			checkType(l,2,out a1);
			self.ReadRoleData(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FixRoleSnap(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			TLProtocol.Data.TLClientRoleSnap a1;
			checkType(l,2,out a1);
			self.FixRoleSnap(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetTitleID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SetTitleID(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetTitleExt(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.SetTitleExt(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetActor(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			var ret=self.GetActor();
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
	static public int GetAvatarList(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			var ret=self.GetAvatarList();
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
	static public int GetAvatarListClone(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			var ret=self.GetAvatarListClone();
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
	static public int GetNewAvatar(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			SLua.LuaTable a1;
			checkType(l,2,out a1);
			var ret=self.GetNewAvatar(a1);
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
	static public int GetTeamInfoList(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			var ret=self.GetTeamInfoList();
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
	static public int Clear(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
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
	static public int RemoveRadarData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.RemoveRadarData(a1);
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
	static public int AddRadarData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			TLAIActor.RadarData a2;
			checkType(l,3,out a2);
			var ret=self.AddRadarData(a1,a2);
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
	static public int GetAttribute(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string))){
				UserData self=(UserData)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				var ret=self.GetAttribute(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(UserData.NotiFyStatus))){
				UserData self=(UserData)checkSelf(l);
				UserData.NotiFyStatus a1;
				checkEnum(l,2,out a1);
				var ret=self.GetAttribute(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function GetAttribute to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StatusToKey(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			UserData.NotiFyStatus a1;
			checkEnum(l,2,out a1);
			var ret=self.StatusToKey(a1);
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
	static public int Key2Status(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.Key2Status(a1);
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
	static public int TryGetIntAttribute(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			UserData.NotiFyStatus a1;
			checkEnum(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.TryGetIntAttribute(a1,a2);
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
	static public int TryGetLongAttribute(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			UserData.NotiFyStatus a1;
			checkEnum(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.TryGetLongAttribute(a1,a2);
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
	static public int SetAttribute(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			UserData.NotiFyStatus a1;
			checkEnum(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			var ret=self.SetAttribute(a1,a2,a3);
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
	static public int ContainsKey(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			UserData.NotiFyStatus a1;
			checkEnum(l,2,out a1);
			UserData.NotiFyStatus a2;
			checkEnum(l,3,out a2);
			var ret=self.ContainsKey(a1,a2);
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
	static public int TryGetRoleProp(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			var ret=self.TryGetRoleProp(a1,out a2,out a3);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a2);
			pushValue(l,a3);
			return 4;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AttachLuaObserver(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
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
			UserData self=(UserData)checkSelf(l);
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
			UserData self=(UserData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.Notify(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetFreeData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.SetFreeData(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetFreeData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetFreeData(a1);
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
	static public int GetMountDistance(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			var ret=self.GetMountDistance();
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
	static public int GetItem(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetItem(a1);
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
	static public int GetItemCountByMatch(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> a1;
			checkDelegate(l,2,out a1);
			var ret=self.GetItemCountByMatch(a1);
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
	static public int IsFuncOpen(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.IsFuncOpen(a1);
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
	static public int PushGetItemMsg2SysChat(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int64 a2;
			checkType(l,3,out a2);
			self.PushGetItemMsg2SysChat(a1,a2);
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
	static public int get_COPPER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.COPPER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SILVER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.SILVER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DIAMOND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.DIAMOND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.EXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastActorMoveAI(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastActorMoveAI);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LastActorMoveAI(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			DeepCore.GameSlave.Agent.AbstractMoveAgent v;
			checkType(l,2,out v);
			self.LastActorMoveAI=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastMapTouchMoveAI(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastMapTouchMoveAI);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LastMapTouchMoveAI(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			DeepCore.GameSlave.Agent.AbstractMoveAgent v;
			checkType(l,2,out v);
			self.LastMapTouchMoveAI=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastSceneNextlink(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastSceneNextlink);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LastSceneNextlink(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Collections.Generic.List<TLProtocol.Protocol.Client.TLSceneNextLink> v;
			checkType(l,2,out v);
			self.LastSceneNextlink=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastMoveEndAction(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastMoveEndAction);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LastMoveEndAction(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			TLAIActor.MoveEndAction v;
			checkType(l,2,out v);
			self.LastMoveEndAction=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnGameOptionDataChange(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Action<TLProtocol.Data.TLGameOptionsData> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnGameOptionDataChange=v;
			else if(op==1) self.OnGameOptionDataChange+=v;
			else if(op==2) self.OnGameOptionDataChange-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ObjectId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ObjectId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ObjectId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.ObjectId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RoleID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RoleID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RoleID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.RoleID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MasterId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MasterId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MasterId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.MasterId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AccountID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AccountID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AccountID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.AccountID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DigitID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DigitID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DigitID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.DigitID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Serverinfo(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Serverinfo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Serverinfo(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			DeepMMO.Data.ServerInfo v;
			checkType(l,2,out v);
			self.Serverinfo=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ServerID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ServerID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ServerID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.ServerID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RoleCreateTime(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RoleCreateTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RoleCreateTime(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.RoleCreateTime=v;
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
			UserData self=(UserData)checkSelf(l);
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
			UserData self=(UserData)checkSelf(l);
			string v;
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
	static public int get_Pro(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Pro);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Pro(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.Pro=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Gender(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Gender);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Gender(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.Gender=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MapName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MapName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MapName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.MapName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MapTemplateId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MapTemplateId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MapTemplateId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.MapTemplateId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ZoneTemplateId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ZoneTemplateId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ZoneTemplateId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.ZoneTemplateId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ZoneUUID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ZoneUUID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ZoneUUID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.ZoneUUID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ZoneGuildId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ZoneGuildId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ZoneGuildId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.ZoneGuildId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RoleTemplateId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RoleTemplateId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RoleTemplateId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.RoleTemplateId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SceneType(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SceneType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SceneType(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.SceneType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ZoneLineIndex(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ZoneLineIndex);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ZoneLineIndex(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.ZoneLineIndex=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PKValue(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PKValue);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_PKValue(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.PKValue=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TargetLv(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TargetLv);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TargetLv(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.TargetLv=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ServerName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ServerName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ServerName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.ServerName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FuncOpen(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FuncOpen);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GuildId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GuildId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_GuildId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.GuildId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TitleID(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TitleID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TitleNameExt(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TitleNameExt);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TitleNameExt(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.TitleNameExt=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SpouseId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SpouseId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SpouseId(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.SpouseId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SpouseName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SpouseName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SpouseName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.SpouseName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ChangeMarryScene(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ChangeMarryScene);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ChangeMarryScene(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.ChangeMarryScene=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurSceneGuildName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurSceneGuildName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_CurSceneGuildName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.CurSceneGuildName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GuildName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GuildName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_GuildName(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.GuildName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RadarDatas(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RadarDatas);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MedicinePoolCurCount(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MedicinePoolCurCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MedicinePoolCurCount(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.MedicinePoolCurCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GameOptionsData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GameOptionsData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_GameOptionsData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			TLProtocol.Data.TLGameOptionsData v;
			checkType(l,2,out v);
			self.GameOptionsData=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RoleFreeData(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RoleFreeData);
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
			UserData self=(UserData)checkSelf(l);
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
	static public int get_Level(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Level);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VipLv(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.VipLv);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OverFlowExp(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.OverFlowExp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OverFlowExp(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			System.Int64 v;
			checkType(l,2,out v);
			self.OverFlowExp=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VipCurExp(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.VipCurExp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastUpdate(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastUpdate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Bag(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Bag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FateBag(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FateBag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FateEquipBag(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FateEquipBag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EquipBag(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EquipBag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Warehourse(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Warehourse);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VirtualBag(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.VirtualBag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestBag(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.QuestBag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RoleSnapReader(IntPtr l) {
		try {
			UserData self=(UserData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RoleSnapReader);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UserData");
		addMember(l,GetCurMapRadarData);
		addMember(l,FindItemDataByID);
		addMember(l,LuaSaveOptionsData);
		addMember(l,InitNetWork);
		addMember(l,ReadRoleData);
		addMember(l,FixRoleSnap);
		addMember(l,SetTitleID);
		addMember(l,SetTitleExt);
		addMember(l,GetActor);
		addMember(l,GetAvatarList);
		addMember(l,GetAvatarListClone);
		addMember(l,GetNewAvatar);
		addMember(l,GetTeamInfoList);
		addMember(l,Clear);
		addMember(l,RemoveRadarData);
		addMember(l,AddRadarData);
		addMember(l,GetAttribute);
		addMember(l,StatusToKey);
		addMember(l,Key2Status);
		addMember(l,TryGetIntAttribute);
		addMember(l,TryGetLongAttribute);
		addMember(l,SetAttribute);
		addMember(l,ContainsKey);
		addMember(l,TryGetRoleProp);
		addMember(l,AttachLuaObserver);
		addMember(l,DetachLuaObserver);
		addMember(l,Notify);
		addMember(l,SetFreeData);
		addMember(l,GetFreeData);
		addMember(l,GetMountDistance);
		addMember(l,GetItem);
		addMember(l,GetItemCountByMatch);
		addMember(l,IsFuncOpen);
		addMember(l,PushGetItemMsg2SysChat);
		addMember(l,"COPPER",get_COPPER,null,false);
		addMember(l,"SILVER",get_SILVER,null,false);
		addMember(l,"DIAMOND",get_DIAMOND,null,false);
		addMember(l,"EXP",get_EXP,null,false);
		addMember(l,"LastActorMoveAI",get_LastActorMoveAI,set_LastActorMoveAI,true);
		addMember(l,"LastMapTouchMoveAI",get_LastMapTouchMoveAI,set_LastMapTouchMoveAI,true);
		addMember(l,"LastSceneNextlink",get_LastSceneNextlink,set_LastSceneNextlink,true);
		addMember(l,"LastMoveEndAction",get_LastMoveEndAction,set_LastMoveEndAction,true);
		addMember(l,"OnGameOptionDataChange",null,set_OnGameOptionDataChange,true);
		addMember(l,"ObjectId",get_ObjectId,set_ObjectId,true);
		addMember(l,"RoleID",get_RoleID,set_RoleID,true);
		addMember(l,"MasterId",get_MasterId,set_MasterId,true);
		addMember(l,"AccountID",get_AccountID,set_AccountID,true);
		addMember(l,"DigitID",get_DigitID,set_DigitID,true);
		addMember(l,"Serverinfo",get_Serverinfo,set_Serverinfo,true);
		addMember(l,"ServerID",get_ServerID,set_ServerID,true);
		addMember(l,"RoleCreateTime",get_RoleCreateTime,set_RoleCreateTime,true);
		addMember(l,"Name",get_Name,set_Name,true);
		addMember(l,"Pro",get_Pro,set_Pro,true);
		addMember(l,"Gender",get_Gender,set_Gender,true);
		addMember(l,"MapName",get_MapName,set_MapName,true);
		addMember(l,"MapTemplateId",get_MapTemplateId,set_MapTemplateId,true);
		addMember(l,"ZoneTemplateId",get_ZoneTemplateId,set_ZoneTemplateId,true);
		addMember(l,"ZoneUUID",get_ZoneUUID,set_ZoneUUID,true);
		addMember(l,"ZoneGuildId",get_ZoneGuildId,set_ZoneGuildId,true);
		addMember(l,"RoleTemplateId",get_RoleTemplateId,set_RoleTemplateId,true);
		addMember(l,"SceneType",get_SceneType,set_SceneType,true);
		addMember(l,"ZoneLineIndex",get_ZoneLineIndex,set_ZoneLineIndex,true);
		addMember(l,"PKValue",get_PKValue,set_PKValue,true);
		addMember(l,"TargetLv",get_TargetLv,set_TargetLv,true);
		addMember(l,"ServerName",get_ServerName,set_ServerName,true);
		addMember(l,"FuncOpen",get_FuncOpen,null,true);
		addMember(l,"GuildId",get_GuildId,set_GuildId,true);
		addMember(l,"TitleID",get_TitleID,null,true);
		addMember(l,"TitleNameExt",get_TitleNameExt,set_TitleNameExt,true);
		addMember(l,"SpouseId",get_SpouseId,set_SpouseId,true);
		addMember(l,"SpouseName",get_SpouseName,set_SpouseName,true);
		addMember(l,"ChangeMarryScene",get_ChangeMarryScene,set_ChangeMarryScene,true);
		addMember(l,"CurSceneGuildName",get_CurSceneGuildName,set_CurSceneGuildName,true);
		addMember(l,"GuildName",get_GuildName,set_GuildName,true);
		addMember(l,"RadarDatas",get_RadarDatas,null,true);
		addMember(l,"MedicinePoolCurCount",get_MedicinePoolCurCount,set_MedicinePoolCurCount,true);
		addMember(l,"GameOptionsData",get_GameOptionsData,set_GameOptionsData,true);
		addMember(l,"RoleFreeData",get_RoleFreeData,null,true);
		addMember(l,"Force",get_Force,null,true);
		addMember(l,"Level",get_Level,null,true);
		addMember(l,"VipLv",get_VipLv,null,true);
		addMember(l,"OverFlowExp",get_OverFlowExp,set_OverFlowExp,true);
		addMember(l,"VipCurExp",get_VipCurExp,null,true);
		addMember(l,"LastUpdate",get_LastUpdate,null,true);
		addMember(l,"Bag",get_Bag,null,true);
		addMember(l,"FateBag",get_FateBag,null,true);
		addMember(l,"FateEquipBag",get_FateEquipBag,null,true);
		addMember(l,"EquipBag",get_EquipBag,null,true);
		addMember(l,"Warehourse",get_Warehourse,null,true);
		addMember(l,"VirtualBag",get_VirtualBag,null,true);
		addMember(l,"QuestBag",get_QuestBag,null,true);
		addMember(l,"RoleSnapReader",get_RoleSnapReader,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(UserData));
	}
}
