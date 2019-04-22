using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ScreenOrientation : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUnknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScreenOrientation.Unknown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Unknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScreenOrientation.Unknown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPortrait(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScreenOrientation.Portrait);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Portrait(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScreenOrientation.Portrait);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPortraitUpsideDown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScreenOrientation.PortraitUpsideDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PortraitUpsideDown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScreenOrientation.PortraitUpsideDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLandscape(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScreenOrientation.Landscape);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Landscape(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScreenOrientation.Landscape);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLandscapeLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScreenOrientation.LandscapeLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LandscapeLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScreenOrientation.LandscapeLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLandscapeRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScreenOrientation.LandscapeRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LandscapeRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScreenOrientation.LandscapeRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAutoRotation(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ScreenOrientation.AutoRotation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoRotation(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ScreenOrientation.AutoRotation);
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
		getTypeTable(l,"UnityEngine.ScreenOrientation");
		addMember(l,"Unknown",getUnknown,null,false);
		addMember(l,"_Unknown",get_Unknown,null,false);
		addMember(l,"Portrait",getPortrait,null,false);
		addMember(l,"_Portrait",get_Portrait,null,false);
		addMember(l,"PortraitUpsideDown",getPortraitUpsideDown,null,false);
		addMember(l,"_PortraitUpsideDown",get_PortraitUpsideDown,null,false);
		addMember(l,"Landscape",getLandscape,null,false);
		addMember(l,"_Landscape",get_Landscape,null,false);
		addMember(l,"LandscapeLeft",getLandscapeLeft,null,false);
		addMember(l,"_LandscapeLeft",get_LandscapeLeft,null,false);
		addMember(l,"LandscapeRight",getLandscapeRight,null,false);
		addMember(l,"_LandscapeRight",get_LandscapeRight,null,false);
		addMember(l,"AutoRotation",getAutoRotation,null,false);
		addMember(l,"_AutoRotation",get_AutoRotation,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ScreenOrientation));
	}
}
