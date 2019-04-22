using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_GameSceneMgr : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReadyToLoading(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			self.ReadyToLoading();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReadyToLoadingExt(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			self.ReadyToLoadingExt();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetSceneCameraActive(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.SetSceneCameraActive(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StartChangeScene(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			self.StartChangeScene();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideScene(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.HideScene(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideNGUI(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.HideNGUI(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnBeforeRequestEvent(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			var ret=self.OnBeforeRequestEvent(a1,a2);
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
	static public int OnRequestStartEvent(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			self.OnRequestStartEvent(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnRequestEndEvent(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			DeepCore.FuckPomeloClient.PomeloException a4;
			checkType(l,5,out a4);
			System.Object a5;
			checkType(l,6,out a5);
			self.OnRequestEndEvent(a1,a2,a3,a4,a5);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ExitGame(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			self.ExitGame(a1);
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
	static public int get_Count(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Count);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Count(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Count=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsHeXinBen(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsHeXinBen);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsHeXinBen(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsHeXinBen=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EffName(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EffName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EffName(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.EffName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GroupId(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GroupId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_GroupId(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.GroupId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mNextClient(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mNextClient);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mNextClient(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			DeepMMO.Client.Battle.RPGBattleClient v;
			checkType(l,2,out v);
			self.mNextClient=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_syncServerTime(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.syncServerTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SceneCameraNode(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SceneCameraNode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SceneCameraCullingMask(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SceneCameraCullingMask);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SceneCameraCameraClearFlags(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SceneCameraCameraClearFlags);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SceneCamera(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SceneCamera);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UICamera(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UICamera);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LoadingCameraNode(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LoadingCameraNode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BattleRun(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BattleRun);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BattleScene(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BattleScene);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UGUI(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UGUI);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LoginCtrl(IntPtr l) {
		try {
			GameSceneMgr self=(GameSceneMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LoginCtrl);
			return 2;
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
			pushValue(l,GameSceneMgr.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"GameSceneMgr");
		addMember(l,ReadyToLoading);
		addMember(l,ReadyToLoadingExt);
		addMember(l,SetSceneCameraActive);
		addMember(l,StartChangeScene);
		addMember(l,HideScene);
		addMember(l,HideNGUI);
		addMember(l,OnBeforeRequestEvent);
		addMember(l,OnRequestStartEvent);
		addMember(l,OnRequestEndEvent);
		addMember(l,ExitGame);
		addMember(l,"Count",get_Count,set_Count,true);
		addMember(l,"IsHeXinBen",get_IsHeXinBen,set_IsHeXinBen,true);
		addMember(l,"EffName",get_EffName,set_EffName,true);
		addMember(l,"GroupId",get_GroupId,set_GroupId,true);
		addMember(l,"mNextClient",get_mNextClient,set_mNextClient,true);
		addMember(l,"syncServerTime",get_syncServerTime,null,true);
		addMember(l,"SceneCameraNode",get_SceneCameraNode,null,true);
		addMember(l,"SceneCameraCullingMask",get_SceneCameraCullingMask,null,true);
		addMember(l,"SceneCameraCameraClearFlags",get_SceneCameraCameraClearFlags,null,true);
		addMember(l,"SceneCamera",get_SceneCamera,null,true);
		addMember(l,"UICamera",get_UICamera,null,true);
		addMember(l,"LoadingCameraNode",get_LoadingCameraNode,null,true);
		addMember(l,"BattleRun",get_BattleRun,null,true);
		addMember(l,"BattleScene",get_BattleScene,null,true);
		addMember(l,"UGUI",get_UGUI,null,true);
		addMember(l,"LoginCtrl",get_LoginCtrl,null,true);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(GameSceneMgr));
	}
}
