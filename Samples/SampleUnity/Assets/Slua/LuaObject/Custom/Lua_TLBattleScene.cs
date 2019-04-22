using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLBattleScene : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ActorHPPercent(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			var ret=self.ActorHPPercent();
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
	static public int GetBattleObject(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			var ret=self.GetBattleObject(a1);
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
	static public int GetAIPlayer(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetAIPlayer(a1);
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
	static public int GetZoneFlag(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetZoneFlag(a1);
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
	static public int GetUnitByTemplateId(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetUnitByTemplateId(a1);
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
	static public int GetPlayerUnitByUUID(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetPlayerUnitByUUID(a1);
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
	static public int UpdateLayer(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.UpdateLayer(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayUI3DEffect(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			self.PlayUI3DEffect(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnZoneLeave(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			DeepMMO.Client.Battle.RPGBattleClient a1;
			checkType(l,2,out a1);
			self.OnZoneLeave(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int TargetIsEnemy(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			DeepCore.Unity3D.Battle.ComAIUnit a1;
			checkType(l,2,out a1);
			var ret=self.TargetIsEnemy(a1);
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
	static public int UpdateSyncServerTime(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.UpdateSyncServerTime(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetOwnerShipMonsterID(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			var ret=self.GetOwnerShipMonsterID();
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
	static public int OwnerShipChange(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.OwnerShipChange();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UseMedicinePool(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.UseMedicinePool();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AllowUseMedicinePool(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			var ret=self.AllowUseMedicinePool();
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
	static public int CheckAllowUseMedicinePool(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.CheckAllowUseMedicinePool();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetScenePlayerDamageMap(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			var ret=self.GetScenePlayerDamageMap();
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
	static public int init(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.init();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StartStory(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.StartStory();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StoryOver(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.StoryOver();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveLastUnit(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			var ret=self.RemoveLastUnit();
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
	static public int AddStoryUnit(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			System.Action a4;
			checkDelegate(l,5,out a4);
			var ret=self.AddStoryUnit(a1,a2,a3,a4);
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
	static public int AddBubbleTalk(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			self.AddBubbleTalk(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddCGBubbleTalk(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			self.AddCGBubbleTalk(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddCGBubbleTalkByObject(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			UnityEngine.GameObject a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.AddCGBubbleTalkByObject(a1,a2);
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
	static public int AddStoryTip(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.AddStoryTip(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayCG(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			System.Action a3;
			checkDelegate(l,4,out a3);
			System.Int32 a4;
			checkType(l,5,out a4);
			self.PlayCG(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CanSkipCG(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.CanSkipCG(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayVoice(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.PlayVoice(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopVoice(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.StopVoice();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlaySound(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.PlaySound(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopSound(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.StopSound();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayBGM(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.PlayBGM(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ChangeBGM(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.ChangeBGM(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopBGM(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.StopBGM();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ResumeBGM(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.ResumeBGM();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PauseBGM(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.PauseBGM();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int BlackScreen(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.BlackScreen(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveStoryObject(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			var ret=self.RemoveStoryObject(a1);
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
	static public int GetStoryUnitObject(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			var ret=self.GetStoryUnitObject(a1);
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
	static public int RemoveAllStoryObject(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.RemoveAllStoryObject();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int MoveFromActor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==5){
				TLBattleScene self=(TLBattleScene)checkSelf(l);
				System.UInt32 a1;
				checkType(l,2,out a1);
				TLBattleScene.CameraMoveOK a2;
				checkDelegate(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				uTools.EaseType a4;
				checkEnum(l,5,out a4);
				self.MoveFromActor(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(argc==7){
				TLBattleScene self=(TLBattleScene)checkSelf(l);
				System.UInt32 a1;
				checkType(l,2,out a1);
				UnityEngine.Vector3 a2;
				checkType(l,3,out a2);
				UnityEngine.Vector3 a3;
				checkType(l,4,out a3);
				TLBattleScene.CameraMoveOK a4;
				checkDelegate(l,5,out a4);
				System.Single a5;
				checkType(l,6,out a5);
				uTools.EaseType a6;
				checkEnum(l,7,out a6);
				self.MoveFromActor(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function MoveFromActor to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int MoveCamera(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			TLBattleScene.CameraMoveOK a2;
			checkDelegate(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			uTools.EaseType a4;
			checkEnum(l,5,out a4);
			self.MoveCamera(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CameraFaceToUnit(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			UnityEngine.Vector3 a3;
			checkType(l,4,out a3);
			UnityEngine.Vector3 a4;
			checkType(l,5,out a4);
			TLBattleScene.CameraMoveOK a5;
			checkDelegate(l,6,out a5);
			System.Single a6;
			checkType(l,7,out a6);
			uTools.EaseType a7;
			checkEnum(l,8,out a7);
			self.CameraFaceToUnit(a1,a2,a3,a4,a5,a6,a7);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CameraFromUnitToUnit(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			TLBattleScene.CameraMoveOK a3;
			checkDelegate(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			self.CameraFromUnitToUnit(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CameraLockUnit(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			self.CameraLockUnit(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CameraPath2Unit(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			UnityEngine.Vector3 a3;
			checkType(l,4,out a3);
			TLBattleScene.CameraMoveOK a4;
			checkDelegate(l,5,out a4);
			System.Single a5;
			checkType(l,6,out a5);
			UnityEngine.Vector2 a6;
			checkType(l,7,out a6);
			System.Boolean a7;
			checkType(l,8,out a7);
			self.CameraPath2Unit(a1,a2,a3,a4,a5,a6,a7);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CameraRotateAround(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			TLBattleScene.CameraMoveOK a3;
			checkDelegate(l,4,out a3);
			self.CameraRotateAround(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlaySceneEffect(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			TLBattleScene.CameraMoveOK a3;
			checkDelegate(l,4,out a3);
			self.PlaySceneEffect(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlaySceneCamera(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			TLBattleScene.CameraMoveOK a3;
			checkDelegate(l,4,out a3);
			var ret=self.PlaySceneCamera(a1,a2,a3);
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
	static public int ResetSceneCamera(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.ResetSceneCamera();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CameraShake(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			self.CameraShake(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayAnimation(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.Action a3;
			checkDelegate(l,4,out a3);
			System.Boolean a4;
			checkType(l,5,out a4);
			System.Single a5;
			checkType(l,6,out a5);
			System.Single a6;
			checkType(l,7,out a6);
			UnityEngine.WrapMode a7;
			checkEnum(l,8,out a7);
			var ret=self.PlayAnimation(a1,a2,a3,a4,a5,a6,a7);
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
	static public int UnitChangeDirection(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			self.UnitChangeDirection(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FaceToPlayer(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			self.FaceToPlayer(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnitStopTurnDirect(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.UnitStopTurnDirect(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetDirFromPlayer(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetDirFromPlayer(a1);
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
	static public int GetUnitAnimation(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			var ret=self.GetUnitAnimation(a1);
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
	static public int PauseClient(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.PauseClient(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ActorPlayAnimation(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			UnityEngine.WrapMode a2;
			checkEnum(l,3,out a2);
			self.ActorPlayAnimation(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CalUnitTowards(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			UnityEngine.Vector3 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector3 a2;
			checkType(l,3,out a2);
			var ret=self.CalUnitTowards(a1,a2);
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
	static public int MoveUnitToPos(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			TLBattleScene.UnitMoveOK a5;
			checkDelegate(l,6,out a5);
			self.MoveUnitToPos(a1,a2,a3,a4,a5);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayEffect(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			UnityEngine.Vector2 a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			System.Single a5;
			checkType(l,6,out a5);
			System.String a6;
			checkType(l,7,out a6);
			System.Single a7;
			checkType(l,8,out a7);
			System.Action a8;
			checkDelegate(l,9,out a8);
			self.PlayEffect(a1,a2,a3,a4,a5,a6,a7,a8);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveEffectByKey(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.RemoveEffectByKey(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveAllEffect(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			self.RemoveAllEffect();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ActorRotation(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			TLBattleScene.UnitRotationOk a3;
			checkDelegate(l,4,out a3);
			self.ActorRotation(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StoryFinish(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.StoryFinish(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ChangeDirection(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			self.ChangeDirection(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ChangeUnitAnimation(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			System.Action a4;
			checkDelegate(l,5,out a4);
			System.Single a5;
			checkType(l,6,out a5);
			System.Single a6;
			checkType(l,7,out a6);
			System.Int32 a7;
			checkType(l,8,out a7);
			self.ChangeUnitAnimation(a1,a2,a3,a4,a5,a6,a7);
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
	static public int set_OwnerShipChangeHandler(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Action v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OwnerShipChangeHandler=v;
			else if(op==1) self.OwnerShipChangeHandler+=v;
			else if(op==2) self.OwnerShipChangeHandler-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mEffectList(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mEffectList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mEffectList(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			DeepCore.HashMap<System.Int32,TLBattleScene.StoryEffect> v;
			checkType(l,2,out v);
			self.mEffectList=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mActorObjectId(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mActorObjectId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mActorObjectId(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.mActorObjectId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_isStory(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.isStory);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_isStory(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.isStory=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mMoveUnitList(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mMoveUnitList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mMoveUnitList(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			System.Collections.Generic.List<TLBattleScene.MoveUnit> v;
			checkType(l,2,out v);
			self.mMoveUnitList=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattleScene.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsRunning(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsRunning);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Actor(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Actor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsPickUp(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsPickUp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsPickUp(IntPtr l) {
		try {
			TLBattleScene self=(TLBattleScene)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsPickUp=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLBattleScene");
		addMember(l,ActorHPPercent);
		addMember(l,GetBattleObject);
		addMember(l,GetAIPlayer);
		addMember(l,GetZoneFlag);
		addMember(l,GetUnitByTemplateId);
		addMember(l,GetPlayerUnitByUUID);
		addMember(l,UpdateLayer);
		addMember(l,PlayUI3DEffect);
		addMember(l,OnZoneLeave);
		addMember(l,TargetIsEnemy);
		addMember(l,UpdateSyncServerTime);
		addMember(l,GetOwnerShipMonsterID);
		addMember(l,OwnerShipChange);
		addMember(l,UseMedicinePool);
		addMember(l,AllowUseMedicinePool);
		addMember(l,CheckAllowUseMedicinePool);
		addMember(l,GetScenePlayerDamageMap);
		addMember(l,init);
		addMember(l,StartStory);
		addMember(l,StoryOver);
		addMember(l,RemoveLastUnit);
		addMember(l,AddStoryUnit);
		addMember(l,AddBubbleTalk);
		addMember(l,AddCGBubbleTalk);
		addMember(l,AddCGBubbleTalkByObject);
		addMember(l,AddStoryTip);
		addMember(l,PlayCG);
		addMember(l,CanSkipCG);
		addMember(l,PlayVoice);
		addMember(l,StopVoice);
		addMember(l,PlaySound);
		addMember(l,StopSound);
		addMember(l,PlayBGM);
		addMember(l,ChangeBGM);
		addMember(l,StopBGM);
		addMember(l,ResumeBGM);
		addMember(l,PauseBGM);
		addMember(l,BlackScreen);
		addMember(l,RemoveStoryObject);
		addMember(l,GetStoryUnitObject);
		addMember(l,RemoveAllStoryObject);
		addMember(l,MoveFromActor);
		addMember(l,MoveCamera);
		addMember(l,CameraFaceToUnit);
		addMember(l,CameraFromUnitToUnit);
		addMember(l,CameraLockUnit);
		addMember(l,CameraPath2Unit);
		addMember(l,CameraRotateAround);
		addMember(l,PlaySceneEffect);
		addMember(l,PlaySceneCamera);
		addMember(l,ResetSceneCamera);
		addMember(l,CameraShake);
		addMember(l,PlayAnimation);
		addMember(l,UnitChangeDirection);
		addMember(l,FaceToPlayer);
		addMember(l,UnitStopTurnDirect);
		addMember(l,GetDirFromPlayer);
		addMember(l,GetUnitAnimation);
		addMember(l,PauseClient);
		addMember(l,ActorPlayAnimation);
		addMember(l,CalUnitTowards);
		addMember(l,MoveUnitToPos);
		addMember(l,PlayEffect);
		addMember(l,RemoveEffectByKey);
		addMember(l,RemoveAllEffect);
		addMember(l,ActorRotation);
		addMember(l,StoryFinish);
		addMember(l,ChangeDirection);
		addMember(l,ChangeUnitAnimation);
		addMember(l,"OwnerShipChangeHandler",null,set_OwnerShipChangeHandler,true);
		addMember(l,"mEffectList",get_mEffectList,set_mEffectList,true);
		addMember(l,"mActorObjectId",get_mActorObjectId,set_mActorObjectId,true);
		addMember(l,"isStory",get_isStory,set_isStory,true);
		addMember(l,"mMoveUnitList",get_mMoveUnitList,set_mMoveUnitList,true);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"IsRunning",get_IsRunning,null,true);
		addMember(l,"Actor",get_Actor,null,true);
		addMember(l,"IsPickUp",get_IsPickUp,set_IsPickUp,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLBattleScene),typeof(DeepCore.Unity3D.Battle.BattleScene));
	}
}
