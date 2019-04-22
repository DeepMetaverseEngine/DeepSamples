using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RuntimePlatform : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOSXEditor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.OSXEditor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OSXEditor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.OSXEditor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOSXPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.OSXPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OSXPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.OSXPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWindowsPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.WindowsPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WindowsPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.WindowsPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWindowsEditor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.WindowsEditor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WindowsEditor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.WindowsEditor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getIPhonePlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.IPhonePlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IPhonePlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.IPhonePlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAndroid(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.Android);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Android(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.Android);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLinuxPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.LinuxPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LinuxPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.LinuxPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLinuxEditor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.LinuxEditor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LinuxEditor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.LinuxEditor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWebGLPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.WebGLPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WebGLPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.WebGLPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWSAPlayerX86(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.WSAPlayerX86);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WSAPlayerX86(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.WSAPlayerX86);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWSAPlayerX64(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.WSAPlayerX64);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WSAPlayerX64(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.WSAPlayerX64);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWSAPlayerARM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.WSAPlayerARM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WSAPlayerARM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.WSAPlayerARM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTizenPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.TizenPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TizenPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.TizenPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPSP2(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.PSP2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PSP2(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.PSP2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPS4(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.PS4);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PS4(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.PS4);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPSM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.PSM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PSM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.PSM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getXboxOne(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.XboxOne);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_XboxOne(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.XboxOne);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWiiU(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.WiiU);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WiiU(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.WiiU);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int gettvOS(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.tvOS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_tvOS(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.tvOS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSwitch(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RuntimePlatform.Switch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Switch(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RuntimePlatform.Switch);
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
		getTypeTable(l,"UnityEngine.RuntimePlatform");
		addMember(l,"OSXEditor",getOSXEditor,null,false);
		addMember(l,"_OSXEditor",get_OSXEditor,null,false);
		addMember(l,"OSXPlayer",getOSXPlayer,null,false);
		addMember(l,"_OSXPlayer",get_OSXPlayer,null,false);
		addMember(l,"WindowsPlayer",getWindowsPlayer,null,false);
		addMember(l,"_WindowsPlayer",get_WindowsPlayer,null,false);
		addMember(l,"WindowsEditor",getWindowsEditor,null,false);
		addMember(l,"_WindowsEditor",get_WindowsEditor,null,false);
		addMember(l,"IPhonePlayer",getIPhonePlayer,null,false);
		addMember(l,"_IPhonePlayer",get_IPhonePlayer,null,false);
		addMember(l,"Android",getAndroid,null,false);
		addMember(l,"_Android",get_Android,null,false);
		addMember(l,"LinuxPlayer",getLinuxPlayer,null,false);
		addMember(l,"_LinuxPlayer",get_LinuxPlayer,null,false);
		addMember(l,"LinuxEditor",getLinuxEditor,null,false);
		addMember(l,"_LinuxEditor",get_LinuxEditor,null,false);
		addMember(l,"WebGLPlayer",getWebGLPlayer,null,false);
		addMember(l,"_WebGLPlayer",get_WebGLPlayer,null,false);
		addMember(l,"WSAPlayerX86",getWSAPlayerX86,null,false);
		addMember(l,"_WSAPlayerX86",get_WSAPlayerX86,null,false);
		addMember(l,"WSAPlayerX64",getWSAPlayerX64,null,false);
		addMember(l,"_WSAPlayerX64",get_WSAPlayerX64,null,false);
		addMember(l,"WSAPlayerARM",getWSAPlayerARM,null,false);
		addMember(l,"_WSAPlayerARM",get_WSAPlayerARM,null,false);
		addMember(l,"TizenPlayer",getTizenPlayer,null,false);
		addMember(l,"_TizenPlayer",get_TizenPlayer,null,false);
		addMember(l,"PSP2",getPSP2,null,false);
		addMember(l,"_PSP2",get_PSP2,null,false);
		addMember(l,"PS4",getPS4,null,false);
		addMember(l,"_PS4",get_PS4,null,false);
		addMember(l,"PSM",getPSM,null,false);
		addMember(l,"_PSM",get_PSM,null,false);
		addMember(l,"XboxOne",getXboxOne,null,false);
		addMember(l,"_XboxOne",get_XboxOne,null,false);
		addMember(l,"WiiU",getWiiU,null,false);
		addMember(l,"_WiiU",get_WiiU,null,false);
		addMember(l,"tvOS",gettvOS,null,false);
		addMember(l,"_tvOS",get_tvOS,null,false);
		addMember(l,"Switch",getSwitch,null,false);
		addMember(l,"_Switch",get_Switch,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RuntimePlatform));
	}
}
