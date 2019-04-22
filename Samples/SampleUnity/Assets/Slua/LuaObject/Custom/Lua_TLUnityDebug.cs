using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLUnityDebug : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetInterface(IntPtr l) {
		try {
			TLUnityDebug self=(TLUnityDebug)checkSelf(l);
			DeepCore.Unity3D.Game.Battle.IDebugCameraInterface a1;
			checkType(l,2,out a1);
			self.SetInterface(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Log(IntPtr l) {
		try {
			TLUnityDebug self=(TLUnityDebug)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.Log(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ErrorLog(IntPtr l) {
		try {
			TLUnityDebug self=(TLUnityDebug)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.ErrorLog(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetInstance_s(IntPtr l) {
		try {
			var ret=TLUnityDebug.GetInstance();
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
	static public int SetDebug_s(IntPtr l) {
		try {
			System.Boolean a1;
			checkType(l,1,out a1);
			TLUnityDebug.SetDebug(a1);
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
	static public int get_DEBUG_MODE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLUnityDebug.DEBUG_MODE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DEBUG_MODE(IntPtr l) {
		try {
			System.Boolean v;
			checkType(l,2,out v);
			TLUnityDebug.DEBUG_MODE=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mTLSkin(IntPtr l) {
		try {
			TLUnityDebug self=(TLUnityDebug)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mTLSkin);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mTLSkin(IntPtr l) {
		try {
			TLUnityDebug self=(TLUnityDebug)checkSelf(l);
			UnityEngine.GUISkin v;
			checkType(l,2,out v);
			self.mTLSkin=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ShowDebugInfo(IntPtr l) {
		try {
			TLUnityDebug self=(TLUnityDebug)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ShowDebugInfo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLUnityDebug");
		addMember(l,SetInterface);
		addMember(l,Log);
		addMember(l,ErrorLog);
		addMember(l,GetInstance_s);
		addMember(l,SetDebug_s);
		addMember(l,"DEBUG_MODE",get_DEBUG_MODE,set_DEBUG_MODE,false);
		addMember(l,"mTLSkin",get_mTLSkin,set_mTLSkin,true);
		addMember(l,"ShowDebugInfo",get_ShowDebugInfo,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLUnityDebug),typeof(UnityEngine.MonoBehaviour));
	}
}
