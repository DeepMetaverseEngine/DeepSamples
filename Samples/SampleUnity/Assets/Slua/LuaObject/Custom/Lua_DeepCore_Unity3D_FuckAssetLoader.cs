using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_FuckAssetLoader : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.Unity3D.FuckAssetLoader o;
			if(argc==4){
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				UnityEngine.Object a3;
				checkType(l,4,out a3);
				o=new DeepCore.Unity3D.FuckAssetLoader(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==5){
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.Boolean a3;
				checkType(l,4,out a3);
				System.Action<DeepCore.Unity3D.FuckAssetLoader> a4;
				checkDelegate(l,5,out a4);
				o=new DeepCore.Unity3D.FuckAssetLoader(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			return error(l,"New object failed.");
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Discard(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			self.Discard();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetDeps(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			var ret=self.GetDeps();
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
	static public int GenID_s(IntPtr l) {
		try {
			var ret=DeepCore.Unity3D.FuckAssetLoader.GenID();
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
	static public int GetLoader_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.FuckAssetLoader.GetLoader(a1);
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
	static public int LoadImmediate_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				System.String a1;
				checkType(l,1,out a1);
				var ret=DeepCore.Unity3D.FuckAssetLoader.LoadImmediate(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==2){
				System.String a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				var ret=DeepCore.Unity3D.FuckAssetLoader.LoadImmediate(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function LoadImmediate to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Load_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				System.String a1;
				checkType(l,1,out a1);
				var ret=DeepCore.Unity3D.FuckAssetLoader.Load(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(string),typeof(System.Action<DeepCore.Unity3D.FuckAssetLoader>))){
				System.String a1;
				checkType(l,1,out a1);
				System.Action<DeepCore.Unity3D.FuckAssetLoader> a2;
				checkDelegate(l,2,out a2);
				var ret=DeepCore.Unity3D.FuckAssetLoader.Load(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(string),typeof(string))){
				System.String a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				var ret=DeepCore.Unity3D.FuckAssetLoader.Load(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				System.String a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.Action<DeepCore.Unity3D.FuckAssetLoader> a3;
				checkDelegate(l,3,out a3);
				var ret=DeepCore.Unity3D.FuckAssetLoader.Load(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Load to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetAssetNameFromBundleName_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.FuckAssetLoader.GetAssetNameFromBundleName(a1);
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
	static public int get_ID(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
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
	static public int get_BundleName(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BundleName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AssetName(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AssetName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Async(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Async);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_keepWaiting(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.keepWaiting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsGameObject(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsGameObject);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsAudioClip(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsAudioClip);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDiscard(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDiscard);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsLinkedLoad(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsLinkedLoad);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsSuccess(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsSuccess);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AssetObject(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AssetObject);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDone(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ErrorMessage(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ErrorMessage);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ActualImmediate(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ActualImmediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Bundle(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader self=(DeepCore.Unity3D.FuckAssetLoader)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Bundle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"FuckAssetLoader");
		addMember(l,Discard);
		addMember(l,GetDeps);
		addMember(l,GenID_s);
		addMember(l,GetLoader_s);
		addMember(l,LoadImmediate_s);
		addMember(l,Load_s);
		addMember(l,GetAssetNameFromBundleName_s);
		addMember(l,"ID",get_ID,null,true);
		addMember(l,"BundleName",get_BundleName,null,true);
		addMember(l,"AssetName",get_AssetName,null,true);
		addMember(l,"Async",get_Async,null,true);
		addMember(l,"keepWaiting",get_keepWaiting,null,true);
		addMember(l,"IsGameObject",get_IsGameObject,null,true);
		addMember(l,"IsAudioClip",get_IsAudioClip,null,true);
		addMember(l,"IsDiscard",get_IsDiscard,null,true);
		addMember(l,"IsLinkedLoad",get_IsLinkedLoad,null,true);
		addMember(l,"IsSuccess",get_IsSuccess,null,true);
		addMember(l,"AssetObject",get_AssetObject,null,true);
		addMember(l,"IsDone",get_IsDone,null,true);
		addMember(l,"ErrorMessage",get_ErrorMessage,null,true);
		addMember(l,"ActualImmediate",get_ActualImmediate,null,true);
		addMember(l,"Bundle",get_Bundle,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.FuckAssetLoader),typeof(UnityEngine.CustomYieldInstruction));
	}
}
