using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UserAuthorization : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWebCam(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UserAuthorization.WebCam);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WebCam(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UserAuthorization.WebCam);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMicrophone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UserAuthorization.Microphone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Microphone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UserAuthorization.Microphone);
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
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.UserAuthorization");
		addMember(l,"WebCam",getWebCam,null,false);
		addMember(l,"_WebCam",get_WebCam,null,false);
		addMember(l,"Microphone",getMicrophone,null,false);
		addMember(l,"_Microphone",get_Microphone,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UserAuthorization));
	}
}
