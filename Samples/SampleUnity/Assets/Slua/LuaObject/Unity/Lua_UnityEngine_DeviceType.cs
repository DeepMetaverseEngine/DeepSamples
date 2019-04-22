using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_DeviceType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUnknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DeviceType.Unknown);
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
			pushValue(l,(double)UnityEngine.DeviceType.Unknown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHandheld(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DeviceType.Handheld);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Handheld(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DeviceType.Handheld);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getConsole(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DeviceType.Console);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Console(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DeviceType.Console);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDesktop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.DeviceType.Desktop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Desktop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.DeviceType.Desktop);
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
		getTypeTable(l,"UnityEngine.DeviceType");
		addMember(l,"Unknown",getUnknown,null,false);
		addMember(l,"_Unknown",get_Unknown,null,false);
		addMember(l,"Handheld",getHandheld,null,false);
		addMember(l,"_Handheld",get_Handheld,null,false);
		addMember(l,"Console",getConsole,null,false);
		addMember(l,"_Console",get_Console,null,false);
		addMember(l,"Desktop",getDesktop,null,false);
		addMember(l,"_Desktop",get_Desktop,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.DeviceType));
	}
}
