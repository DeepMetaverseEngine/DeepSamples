using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_OperatingSystemFamily : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOther(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.OperatingSystemFamily.Other);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Other(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.OperatingSystemFamily.Other);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMacOSX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.OperatingSystemFamily.MacOSX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MacOSX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.OperatingSystemFamily.MacOSX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWindows(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.OperatingSystemFamily.Windows);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Windows(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.OperatingSystemFamily.Windows);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLinux(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.OperatingSystemFamily.Linux);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Linux(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.OperatingSystemFamily.Linux);
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
		getTypeTable(l,"UnityEngine.OperatingSystemFamily");
		addMember(l,"Other",getOther,null,false);
		addMember(l,"_Other",get_Other,null,false);
		addMember(l,"MacOSX",getMacOSX,null,false);
		addMember(l,"_MacOSX",get_MacOSX,null,false);
		addMember(l,"Windows",getWindows,null,false);
		addMember(l,"_Windows",get_Windows,null,false);
		addMember(l,"Linux",getLinux,null,false);
		addMember(l,"_Linux",get_Linux,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.OperatingSystemFamily));
	}
}
