using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_PlatformMgr : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PluginGetUsedMemory_s(IntPtr l) {
		try {
			var ret=PlatformMgr.PluginGetUsedMemory();
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
	static public int PluginGetAvailableMemory_s(IntPtr l) {
		try {
			var ret=PlatformMgr.PluginGetAvailableMemory();
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
	static public int PluginGetDeviceType_s(IntPtr l) {
		try {
			var ret=PlatformMgr.PluginGetDeviceType();
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
	static public int PluginGetUUID_s(IntPtr l) {
		try {
			var ret=PlatformMgr.PluginGetUUID();
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
	static public int PluginGetNetworkStatus_s(IntPtr l) {
		try {
			var ret=PlatformMgr.PluginGetNetworkStatus();
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
	static public int PluginGetSignalStrength_s(IntPtr l) {
		try {
			var ret=PlatformMgr.PluginGetSignalStrength();
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
	static public int GetFreeSpace_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=PlatformMgr.GetFreeSpace(a1);
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
	static public int GetHardDiskSpace_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=PlatformMgr.GetHardDiskSpace(a1);
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
	static public int PluginGetUserAgent_s(IntPtr l) {
		try {
			var ret=PlatformMgr.PluginGetUserAgent();
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
	static public int PluginGetScreenNotch_s(IntPtr l) {
		try {
			var ret=PlatformMgr.PluginGetScreenNotch();
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
	static public int DoUpdate_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			PlatformMgr.DoUpdate(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CaptureScreenshot_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			PlatformMgr.CaptureScreenshot(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetPasteboard_s(IntPtr l) {
		try {
			var ret=PlatformMgr.GetPasteboard();
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
	static public int SetPasteboard_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			PlatformMgr.SetPasteboard(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetBatteryLeftQuantity_s(IntPtr l) {
		try {
			var ret=PlatformMgr.GetBatteryLeftQuantity();
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
	static public int SetBrightness_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			PlatformMgr.SetBrightness(a1);
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
		getTypeTable(l,"PlatformMgr");
		addMember(l,PluginGetUsedMemory_s);
		addMember(l,PluginGetAvailableMemory_s);
		addMember(l,PluginGetDeviceType_s);
		addMember(l,PluginGetUUID_s);
		addMember(l,PluginGetNetworkStatus_s);
		addMember(l,PluginGetSignalStrength_s);
		addMember(l,GetFreeSpace_s);
		addMember(l,GetHardDiskSpace_s);
		addMember(l,PluginGetUserAgent_s);
		addMember(l,PluginGetScreenNotch_s);
		addMember(l,DoUpdate_s);
		addMember(l,CaptureScreenshot_s);
		addMember(l,GetPasteboard_s);
		addMember(l,SetPasteboard_s);
		addMember(l,GetBatteryLeftQuantity_s);
		addMember(l,SetBrightness_s);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(PlatformMgr),typeof(UnityEngine.MonoBehaviour));
	}
}
