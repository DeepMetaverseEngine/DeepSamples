using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UIAnimeType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UIAnimeType o;
			o=new UIAnimeType();
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
	static public int get_Default(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIAnimeType.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NoAnime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIAnimeType.NoAnime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Scale(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIAnimeType.Scale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FadeMoveUp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIAnimeType.FadeMoveUp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FadeMoveDown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIAnimeType.FadeMoveDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FadeMoveLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIAnimeType.FadeMoveLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FadeMoveRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIAnimeType.FadeMoveRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UIAnimeType");
		addMember(l,"Default",get_Default,null,false);
		addMember(l,"NoAnime",get_NoAnime,null,false);
		addMember(l,"Scale",get_Scale,null,false);
		addMember(l,"FadeMoveUp",get_FadeMoveUp,null,false);
		addMember(l,"FadeMoveDown",get_FadeMoveDown,null,false);
		addMember(l,"FadeMoveLeft",get_FadeMoveLeft,null,false);
		addMember(l,"FadeMoveRight",get_FadeMoveRight,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(UIAnimeType));
	}
}
