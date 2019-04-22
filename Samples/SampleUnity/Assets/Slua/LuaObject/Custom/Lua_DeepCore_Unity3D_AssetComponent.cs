using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_AssetComponent : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Unload(IntPtr l) {
		try {
			DeepCore.Unity3D.AssetComponent self=(DeepCore.Unity3D.AssetComponent)checkSelf(l);
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
	static public int Unload_s(IntPtr l) {
		try {
			UnityEngine.GameObject a1;
			checkType(l,1,out a1);
			DeepCore.Unity3D.AssetComponent.Unload(a1);
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
	static public int get_DontMoveToCache(IntPtr l) {
		try {
			DeepCore.Unity3D.AssetComponent self=(DeepCore.Unity3D.AssetComponent)checkSelf(l);
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
			DeepCore.Unity3D.AssetComponent self=(DeepCore.Unity3D.AssetComponent)checkSelf(l);
			System.Boolean v;
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
	static public int get_Invalid(IntPtr l) {
		try {
			DeepCore.Unity3D.AssetComponent self=(DeepCore.Unity3D.AssetComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Invalid);
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
			DeepCore.Unity3D.AssetComponent self=(DeepCore.Unity3D.AssetComponent)checkSelf(l);
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
	static public int get_CacheName(IntPtr l) {
		try {
			DeepCore.Unity3D.AssetComponent self=(DeepCore.Unity3D.AssetComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CacheName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_CacheName(IntPtr l) {
		try {
			DeepCore.Unity3D.AssetComponent self=(DeepCore.Unity3D.AssetComponent)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.CacheName=v;
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
			DeepCore.Unity3D.AssetComponent self=(DeepCore.Unity3D.AssetComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsUnload);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"AssetComponent");
		addMember(l,Unload);
		addMember(l,Unload_s);
		addMember(l,"DontMoveToCache",get_DontMoveToCache,set_DontMoveToCache,true);
		addMember(l,"Invalid",get_Invalid,null,true);
		addMember(l,"IsInCache",get_IsInCache,null,true);
		addMember(l,"CacheName",get_CacheName,set_CacheName,true);
		addMember(l,"IsUnload",get_IsUnload,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.AssetComponent),typeof(UnityEngine.MonoBehaviour));
	}
}
