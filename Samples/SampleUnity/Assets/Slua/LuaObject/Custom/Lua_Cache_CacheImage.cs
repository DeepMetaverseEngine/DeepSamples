using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_Cache_CacheImage : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DownLoad(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(System.Action<object[]>))){
				Cache.CacheImage self=(Cache.CacheImage)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Action<System.Object[]> a2;
				checkDelegate(l,3,out a2);
				self.DownLoad(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(TLProtocol.Data.TLClientRoleSnap),typeof(System.Action<object[]>))){
				Cache.CacheImage self=(Cache.CacheImage)checkSelf(l);
				TLProtocol.Data.TLClientRoleSnap a1;
				checkType(l,2,out a1);
				System.Action<System.Object[]> a2;
				checkDelegate(l,3,out a2);
				self.DownLoad(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				Cache.CacheImage self=(Cache.CacheImage)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.Action<System.Object[]> a3;
				checkDelegate(l,4,out a3);
				self.DownLoad(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				Cache.CacheImage self=(Cache.CacheImage)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.Boolean a3;
				checkType(l,4,out a3);
				System.Action<System.Object[]> a4;
				checkDelegate(l,5,out a4);
				self.DownLoad(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function DownLoad to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ClearCallback(IntPtr l) {
		try {
			Cache.CacheImage self=(Cache.CacheImage)checkSelf(l);
			self.ClearCallback();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetUrl(IntPtr l) {
		try {
			Cache.CacheImage self=(Cache.CacheImage)checkSelf(l);
			System.Action<System.String> a1;
			checkDelegate(l,2,out a1);
			self.GetUrl(a1);
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
			pushValue(l,Cache.CacheImage.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"CacheImage");
		addMember(l,DownLoad);
		addMember(l,ClearCallback);
		addMember(l,GetUrl);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(Cache.CacheImage));
	}
}
