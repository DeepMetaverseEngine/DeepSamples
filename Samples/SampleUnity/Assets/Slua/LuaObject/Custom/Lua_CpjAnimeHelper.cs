using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_CpjAnimeHelper : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			CpjAnimeHelper o;
			o=new CpjAnimeHelper();
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
	static public int PlayCpjAnime(IntPtr l) {
		try {
			CpjAnimeHelper self=(CpjAnimeHelper)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			System.Single a5;
			checkType(l,6,out a5);
			System.Int32 a6;
			checkType(l,7,out a6);
			DeepCore.GUI.Cell.Game.CSpriteEventHandler a7;
			checkDelegate(l,8,out a7);
			var ret=self.PlayCpjAnime(a1,a2,a3,a4,a5,a6,a7);
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
	static public int PlayCacheCpjAnime(IntPtr l) {
		try {
			CpjAnimeHelper self=(CpjAnimeHelper)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			System.Single a5;
			checkType(l,6,out a5);
			System.Int32 a6;
			checkType(l,7,out a6);
			DeepCore.GUI.Cell.Game.CSpriteEventHandler a7;
			checkDelegate(l,8,out a7);
			var ret=self.PlayCacheCpjAnime(a1,a2,a3,a4,a5,a6,a7);
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
	static public int CreateCpjAnime(IntPtr l) {
		try {
			CpjAnimeHelper self=(CpjAnimeHelper)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			System.Single a5;
			checkType(l,6,out a5);
			System.Boolean a6;
			checkType(l,7,out a6);
			var ret=self.CreateCpjAnime(a1,a2,a3,a4,a5,a6);
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
	static public int IsAnimePlaying(IntPtr l) {
		try {
			CpjAnimeHelper self=(CpjAnimeHelper)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.IsAnimePlaying(a1);
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
	static public int StopCpjAnime(IntPtr l) {
		try {
			CpjAnimeHelper self=(CpjAnimeHelper)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.StopCpjAnime(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopAllCpjAnime(IntPtr l) {
		try {
			CpjAnimeHelper self=(CpjAnimeHelper)checkSelf(l);
			self.StopAllCpjAnime();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ClearCacheAnime(IntPtr l) {
		try {
			CpjAnimeHelper self=(CpjAnimeHelper)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.ClearCacheAnime(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ClearAllCacheAnime(IntPtr l) {
		try {
			CpjAnimeHelper self=(CpjAnimeHelper)checkSelf(l);
			self.ClearAllCacheAnime();
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
			CpjAnimeHelper self=(CpjAnimeHelper)checkSelf(l);
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"CpjAnimeHelper");
		addMember(l,PlayCpjAnime);
		addMember(l,PlayCacheCpjAnime);
		addMember(l,CreateCpjAnime);
		addMember(l,IsAnimePlaying);
		addMember(l,StopCpjAnime);
		addMember(l,StopAllCpjAnime);
		addMember(l,ClearCacheAnime);
		addMember(l,ClearAllCacheAnime);
		addMember(l,Clear);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(CpjAnimeHelper));
	}
}
