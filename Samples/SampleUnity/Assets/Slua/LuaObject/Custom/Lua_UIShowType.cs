using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UIShowType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UIShowType o;
			o=new UIShowType();
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
	static public int get_Cover(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIShowType.Cover);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideBackMenu(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIShowType.HideBackMenu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideBackScene(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIShowType.HideBackScene);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideBackHud(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIShowType.HideBackHud);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideSceneAndHud(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIShowType.HideSceneAndHud);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideHudAndMenu(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIShowType.HideHudAndMenu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideBackAll(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UIShowType.HideBackAll);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UIShowType");
		addMember(l,"Cover",get_Cover,null,false);
		addMember(l,"HideBackMenu",get_HideBackMenu,null,false);
		addMember(l,"HideBackScene",get_HideBackScene,null,false);
		addMember(l,"HideBackHud",get_HideBackHud,null,false);
		addMember(l,"HideSceneAndHud",get_HideSceneAndHud,null,false);
		addMember(l,"HideHudAndMenu",get_HideHudAndMenu,null,false);
		addMember(l,"HideBackAll",get_HideBackAll,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(UIShowType));
	}
}
