using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_RenderSystem : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			RenderSystem o;
			o=new RenderSystem();
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
	static public int Unload(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(int))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				self.Unload(a1);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(UnityEngine.GameObject))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				UnityEngine.GameObject a1;
				checkType(l,2,out a1);
				self.Unload(a1);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Unload to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsLoadSuccess(IntPtr l) {
		try {
			RenderSystem self=(RenderSystem)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.IsLoadSuccess(a1);
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
	static public int LoadGameObject(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Action<AssetGameObject> a2;
				checkDelegate(l,3,out a2);
				var ret=self.LoadGameObject(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(TransformSet))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				var ret=self.LoadGameObject(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(System.Action<AssetGameObject>),typeof(System.Action))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Action<AssetGameObject> a2;
				checkDelegate(l,3,out a2);
				System.Action a3;
				checkDelegate(l,4,out a3);
				var ret=self.LoadGameObject(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(TransformSet),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				var ret=self.LoadGameObject(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==5){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				System.Action a4;
				checkDelegate(l,5,out a4);
				var ret=self.LoadGameObject(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function LoadGameObject to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetTransform(IntPtr l) {
		try {
			RenderSystem self=(RenderSystem)checkSelf(l);
			AssetGameObject a1;
			checkType(l,2,out a1);
			TransformSet a2;
			checkType(l,3,out a2);
			self.SetTransform(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetLayer(IntPtr l) {
		try {
			RenderSystem self=(RenderSystem)checkSelf(l);
			AssetGameObject a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			self.SetLayer(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsUnload(IntPtr l) {
		try {
			RenderSystem self=(RenderSystem)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.IsUnload(a1);
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
	static public int GetAssetGameObject(IntPtr l) {
		try {
			RenderSystem self=(RenderSystem)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetAssetGameObject(a1);
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
	static public int PlayEffect(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(DeepCore.GameData.Zone.LaunchEffect),typeof(AssetGameObject))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				DeepCore.GameData.Zone.LaunchEffect a1;
				checkType(l,2,out a1);
				AssetGameObject a2;
				checkType(l,3,out a2);
				var ret=self.PlayEffect(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(TransformSet))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				var ret=self.PlayEffect(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==4){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				var ret=self.PlayEffect(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==5){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				System.Action<System.Int32> a4;
				checkDelegate(l,5,out a4);
				var ret=self.PlayEffect(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function PlayEffect to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetEffectVisible(IntPtr l) {
		try {
			RenderSystem self=(RenderSystem)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.SetEffectVisible(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsFinishPlayEffect(IntPtr l) {
		try {
			RenderSystem self=(RenderSystem)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.IsFinishPlayEffect(a1);
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
	static public int LoadGameUnit(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(TransformSet))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				var ret=self.LoadGameUnit(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(SLua.LuaTable),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				SLua.LuaTable a1;
				checkType(l,2,out a1);
				System.Action<AssetGameObject> a2;
				checkDelegate(l,3,out a2);
				var ret=self.LoadGameUnit(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Action<AssetGameObject> a2;
				checkDelegate(l,3,out a2);
				var ret=self.LoadGameUnit(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo>),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo> a1;
				checkType(l,2,out a1);
				System.Action<AssetGameObject> a2;
				checkDelegate(l,3,out a2);
				var ret=self.LoadGameUnit(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo>),typeof(TransformSet))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo> a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				var ret=self.LoadGameUnit(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(Dictionary<System.Int32,System.String>),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.Collections.Generic.Dictionary<System.Int32,System.String> a1;
				checkType(l,2,out a1);
				System.Action<AssetGameObject> a2;
				checkDelegate(l,3,out a2);
				var ret=self.LoadGameUnit(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(Dictionary<System.Int32,System.String>),typeof(TransformSet))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.Collections.Generic.Dictionary<System.Int32,System.String> a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				var ret=self.LoadGameUnit(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(SLua.LuaTable),typeof(TransformSet))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				SLua.LuaTable a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				var ret=self.LoadGameUnit(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(Dictionary<System.Int32,System.String>),typeof(TransformSet),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.Collections.Generic.Dictionary<System.Int32,System.String> a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				var ret=self.LoadGameUnit(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(SLua.LuaTable),typeof(TransformSet),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				SLua.LuaTable a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				var ret=self.LoadGameUnit(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo>),typeof(TransformSet),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo> a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				var ret=self.LoadGameUnit(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(TransformSet),typeof(System.Action<AssetGameObject>))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				var ret=self.LoadGameUnit(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo>),typeof(TransformSet),typeof(System.Action<AssetGameObject>),typeof(System.Action))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo> a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				System.Action a4;
				checkDelegate(l,5,out a4);
				var ret=self.LoadGameUnit(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(Dictionary<System.Int32,System.String>),typeof(TransformSet),typeof(System.Action<AssetGameObject>),typeof(System.Action))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.Collections.Generic.Dictionary<System.Int32,System.String> a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				System.Action a4;
				checkDelegate(l,5,out a4);
				var ret=self.LoadGameUnit(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(SLua.LuaTable),typeof(TransformSet),typeof(System.Action<AssetGameObject>),typeof(System.Action))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				SLua.LuaTable a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				System.Action a4;
				checkDelegate(l,5,out a4);
				var ret=self.LoadGameUnit(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(TransformSet),typeof(System.Action<AssetGameObject>),typeof(System.Action))){
				RenderSystem self=(RenderSystem)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				TransformSet a2;
				checkType(l,3,out a2);
				System.Action<AssetGameObject> a3;
				checkDelegate(l,4,out a3);
				System.Action a4;
				checkDelegate(l,5,out a4);
				var ret=self.LoadGameUnit(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function LoadGameUnit to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadPart(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==4){
				RenderSystem self=(RenderSystem)checkSelf(l);
				AssetGameObject a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				var ret=self.LoadPart(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==5){
				RenderSystem self=(RenderSystem)checkSelf(l);
				AssetGameObject a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				System.Action<AssetGameObject> a4;
				checkDelegate(l,5,out a4);
				var ret=self.LoadPart(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==6){
				RenderSystem self=(RenderSystem)checkSelf(l);
				AssetGameObject a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				System.Action<AssetGameObject> a4;
				checkDelegate(l,5,out a4);
				System.Action a5;
				checkDelegate(l,6,out a5);
				var ret=self.LoadPart(a1,a2,a3,a4,a5);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function LoadPart to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadPartAndReplace(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==4){
				RenderSystem self=(RenderSystem)checkSelf(l);
				AssetGameObject a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				var ret=self.LoadPartAndReplace(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==5){
				RenderSystem self=(RenderSystem)checkSelf(l);
				AssetGameObject a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				System.Action<AssetGameObject> a4;
				checkDelegate(l,5,out a4);
				var ret=self.LoadPartAndReplace(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==6){
				RenderSystem self=(RenderSystem)checkSelf(l);
				AssetGameObject a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				System.Action<AssetGameObject> a4;
				checkDelegate(l,5,out a4);
				System.Action a5;
				checkDelegate(l,6,out a5);
				var ret=self.LoadPartAndReplace(a1,a2,a3,a4,a5);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function LoadPartAndReplace to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ResetMaterial_s(IntPtr l) {
		try {
			AssetGameObject a1;
			checkType(l,1,out a1);
			RenderSystem.ResetMaterial(a1);
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
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,RenderSystem.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"RenderSystem");
		addMember(l,Unload);
		addMember(l,IsLoadSuccess);
		addMember(l,LoadGameObject);
		addMember(l,SetTransform);
		addMember(l,SetLayer);
		addMember(l,IsUnload);
		addMember(l,GetAssetGameObject);
		addMember(l,PlayEffect);
		addMember(l,SetEffectVisible);
		addMember(l,IsFinishPlayEffect);
		addMember(l,LoadGameUnit);
		addMember(l,LoadPart);
		addMember(l,LoadPartAndReplace);
		addMember(l,ResetMaterial_s);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(RenderSystem));
	}
}
