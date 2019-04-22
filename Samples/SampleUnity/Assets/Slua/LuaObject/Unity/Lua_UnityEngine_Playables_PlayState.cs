using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Playables_PlayState : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPaused(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.PlayState.Paused);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Paused(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.PlayState.Paused);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPlaying(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.PlayState.Playing);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Playing(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.PlayState.Playing);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDelayed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.PlayState.Delayed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Delayed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.PlayState.Delayed);
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
		getTypeTable(l,"UnityEngine.Playables.PlayState");
		addMember(l,"Paused",getPaused,null,false);
		addMember(l,"_Paused",get_Paused,null,false);
		addMember(l,"Playing",getPlaying,null,false);
		addMember(l,"_Playing",get_Playing,null,false);
		addMember(l,"Delayed",getDelayed,null,false);
		addMember(l,"_Delayed",get_Delayed,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Playables.PlayState));
	}
}
