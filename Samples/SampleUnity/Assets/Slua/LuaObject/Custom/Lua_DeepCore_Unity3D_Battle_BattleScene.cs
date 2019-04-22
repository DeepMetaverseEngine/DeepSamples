using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_Battle_BattleScene : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Dispose(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			self.Dispose();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Update(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
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
	static public int FindBattleObjectsAs(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			System.Predicate<DeepCore.Unity3D.Battle.ComAICell> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindBattleObjectsAs<DeepCore.Unity3D.Battle.ComAICell>(a1);
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
	static public int FindBattleObjectAs(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			System.Predicate<DeepCore.Unity3D.Battle.ComAICell> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindBattleObjectAs<DeepCore.Unity3D.Battle.ComAICell>(a1);
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
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
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
	static public int FindPath(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			var ret=self.FindPath(a1,a2,a3,a4);
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
	static public int GetActor(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
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
	static public int PlayEffectWithZoneCoord(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			DeepCore.GameData.Zone.LaunchEffect a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			self.PlayEffectWithZoneCoord(a1,a2,a3,a4);
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
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			DeepCore.GameData.Zone.LaunchEffect a1;
			checkType(l,2,out a1);
			UnityEngine.Vector3 a2;
			checkType(l,3,out a2);
			UnityEngine.Quaternion a3;
			checkType(l,4,out a3);
			self.PlayEffect(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoNear(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			self.DoNear();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoFar(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			self.DoFar();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoSub_H(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			self.DoSub_H();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoAdd_H(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			self.DoAdd_H();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoSub_RX(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			self.DoSub_RX();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoAdd_RX(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			self.DoAdd_RX();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoSub_RY(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			self.DoSub_RY();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoAdd_RY(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			self.DoAdd_RY();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowBodySize(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.ShowBodySize(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowGuard(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.ShowGuard(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowAttack(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.ShowAttack(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetFlag(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetFlag<DeepCore.Unity3D.Battle.BattleFlag>(a1);
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
	static public int GetEditorRegionFlag(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetEditorRegionFlag(a1);
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
	static public int IsInRegion(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			DeepCore.GameData.Zone.ZoneEditor.RegionData a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			var ret=self.IsInRegion(a1,a2,a3);
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
	static public int get_Alloc(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.Battle.BattleScene.Alloc);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AllocCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.Battle.BattleScene.AllocCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ActiveCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.Battle.BattleScene.ActiveCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDisposed(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDisposed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EffectRoot(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EffectRoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DataRoot(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DataRoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Terrain(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Terrain);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SceneData(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SceneData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SceneID(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SceneID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BattleObjects(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.BattleScene self=(DeepCore.Unity3D.Battle.BattleScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BattleObjects);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DeepCore.Unity3D.Battle.BattleScene");
		addMember(l,Dispose);
		addMember(l,Update);
		addMember(l,FindBattleObjectsAs);
		addMember(l,FindBattleObjectAs);
		addMember(l,GetBattleObject);
		addMember(l,FindPath);
		addMember(l,GetActor);
		addMember(l,PlayEffectWithZoneCoord);
		addMember(l,PlayEffect);
		addMember(l,DoNear);
		addMember(l,DoFar);
		addMember(l,DoSub_H);
		addMember(l,DoAdd_H);
		addMember(l,DoSub_RX);
		addMember(l,DoAdd_RX);
		addMember(l,DoSub_RY);
		addMember(l,DoAdd_RY);
		addMember(l,ShowBodySize);
		addMember(l,ShowGuard);
		addMember(l,ShowAttack);
		addMember(l,GetFlag);
		addMember(l,GetEditorRegionFlag);
		addMember(l,IsInRegion);
		addMember(l,"Alloc",get_Alloc,null,false);
		addMember(l,"AllocCount",get_AllocCount,null,false);
		addMember(l,"ActiveCount",get_ActiveCount,null,false);
		addMember(l,"IsDisposed",get_IsDisposed,null,true);
		addMember(l,"EffectRoot",get_EffectRoot,null,true);
		addMember(l,"DataRoot",get_DataRoot,null,true);
		addMember(l,"Terrain",get_Terrain,null,true);
		addMember(l,"SceneData",get_SceneData,null,true);
		addMember(l,"SceneID",get_SceneID,null,true);
		addMember(l,"BattleObjects",get_BattleObjects,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.Battle.BattleScene));
	}
}
