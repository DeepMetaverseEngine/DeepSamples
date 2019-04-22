using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_DeviceOrientation : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUnknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DeviceOrientation.Unknown);
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
			pushValue(l,(double)UnityEngine.DeviceOrientation.Unknown);
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
			pushValue(l,UnityEngine.DeviceOrientation.Portrait);
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
			pushValue(l,(double)UnityEngine.DeviceOrientation.Portrait);
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
			pushValue(l,UnityEngine.DeviceOrientation.PortraitUpsideDown);
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
			pushValue(l,(double)UnityEngine.DeviceOrientation.PortraitUpsideDown);
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
			pushValue(l,UnityEngine.DeviceOrientation.LandscapeLeft);
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
			pushValue(l,(double)UnityEngine.DeviceOrientation.LandscapeLeft);
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
			pushValue(l,UnityEngine.DeviceOrientation.LandscapeRight);
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
			pushValue(l,(double)UnityEngine.DeviceOrientation.LandscapeRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFaceUp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DeviceOrientation.FaceUp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FaceUp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DeviceOrientation.FaceUp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFaceDown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DeviceOrientation.FaceDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FaceDown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DeviceOrientation.FaceDown);
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
		getTypeTable(l,"UnityEngine.DeviceOrientation");
		addMember(l,"Unknown",getUnknown,null,false);
		addMember(l,"_Unknown",get_Unknown,null,false);
		addMember(l,"Portrait",getPortrait,null,false);
		addMember(l,"_Portrait",get_Portrait,null,false);
		addMember(l,"PortraitUpsideDown",getPortraitUpsideDown,null,false);
		addMember(l,"_PortraitUpsideDown",get_PortraitUpsideDown,null,false);
		addMember(l,"LandscapeLeft",getLandscapeLeft,null,false);
		addMember(l,"_LandscapeLeft",get_LandscapeLeft,null,false);
		addMember(l,"LandscapeRight",getLandscapeRight,null,false);
		addMember(l,"_LandscapeRight",get_LandscapeRight,null,false);
		addMember(l,"FaceUp",getFaceUp,null,false);
		addMember(l,"_FaceUp",get_FaceUp,null,false);
		addMember(l,"FaceDown",getFaceDown,null,false);
		addMember(l,"_FaceDown",get_FaceDown,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.DeviceOrientation));
	}
}
