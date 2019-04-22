using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_AssetGameObject : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetAsEffect(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.SetAsEffect(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetAnimTime(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetAnimTime(a1);
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
	static public int CrossFade(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				AssetGameObject self=(AssetGameObject)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				self.CrossFade(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==6){
				AssetGameObject self=(AssetGameObject)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				System.Int32 a3;
				checkType(l,4,out a3);
				System.Single a4;
				checkType(l,5,out a4);
				System.Boolean a5;
				checkType(l,6,out a5);
				self.CrossFade(a1,a2,a3,a4,a5);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CrossFade to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Play(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				AssetGameObject self=(AssetGameObject)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				self.Play(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				AssetGameObject self=(AssetGameObject)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				self.Play(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				AssetGameObject self=(AssetGameObject)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				self.Play(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Play to call");
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
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			self.Unload();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FindNode(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.FindNode(a1);
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
	static public int GetAssetObject_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			var ret=AssetGameObject.GetAssetObject(a1);
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
	static public int FromCache_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=AssetGameObject.FromCache(a1);
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
	static public int Create_s(IntPtr l) {
		try {
			DeepCore.Unity3D.FuckAssetLoader a1;
			checkType(l,1,out a1);
			var ret=AssetGameObject.Create(a1);
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
			AssetGameObject self=(AssetGameObject)checkSelf(l);
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
			AssetGameObject self=(AssetGameObject)checkSelf(l);
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
	static public int get_DontMoveToCache(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DontMoveToCache);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DontMoveToCache(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.DontMoveToCache=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDestroy(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDestroy);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsInCache(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsInCache);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DisableToUnload(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DisableToUnload);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DisableToUnload(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.DisableToUnload=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsUnload(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsUnload);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AnimStateNameFormat(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AnimStateNameFormat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AnimStateNameFormat(IntPtr l) {
		try {
			AssetGameObject self=(AssetGameObject)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.AnimStateNameFormat=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"AssetGameObject");
		addMember(l,SetAsEffect);
		addMember(l,GetAnimTime);
		addMember(l,CrossFade);
		addMember(l,Play);
		addMember(l,Unload);
		addMember(l,FindNode);
		addMember(l,GetAssetObject_s);
		addMember(l,FromCache_s);
		addMember(l,Create_s);
		addMember(l,"ID",get_ID,null,true);
		addMember(l,"BundleName",get_BundleName,null,true);
		addMember(l,"DontMoveToCache",get_DontMoveToCache,set_DontMoveToCache,true);
		addMember(l,"IsDestroy",get_IsDestroy,null,true);
		addMember(l,"IsInCache",get_IsInCache,null,true);
		addMember(l,"DisableToUnload",get_DisableToUnload,set_DisableToUnload,true);
		addMember(l,"IsUnload",get_IsUnload,null,true);
		addMember(l,"AnimStateNameFormat",get_AnimStateNameFormat,set_AnimStateNameFormat,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(AssetGameObject),typeof(UnityEngine.MonoBehaviour));
	}
}
