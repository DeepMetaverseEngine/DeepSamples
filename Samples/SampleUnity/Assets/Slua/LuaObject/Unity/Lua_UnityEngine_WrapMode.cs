using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_WrapMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDefault(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.WrapMode.Default);
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
			pushValue(l,(double)UnityEngine.WrapMode.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getClamp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.WrapMode.Clamp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Clamp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.WrapMode.Clamp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOnce(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.WrapMode.Once);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Once(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.WrapMode.Once);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLoop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.WrapMode.Loop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Loop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.WrapMode.Loop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPingPong(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.WrapMode.PingPong);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PingPong(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.WrapMode.PingPong);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getClampForever(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.WrapMode.ClampForever);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ClampForever(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.WrapMode.ClampForever);
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
		getTypeTable(l,"UnityEngine.WrapMode");
		addMember(l,"Default",getDefault,null,false);
		addMember(l,"_Default",get_Default,null,false);
		addMember(l,"Clamp",getClamp,null,false);
		addMember(l,"_Clamp",get_Clamp,null,false);
		addMember(l,"Once",getOnce,null,false);
		addMember(l,"_Once",get_Once,null,false);
		addMember(l,"Loop",getLoop,null,false);
		addMember(l,"_Loop",get_Loop,null,false);
		addMember(l,"PingPong",getPingPong,null,false);
		addMember(l,"_PingPong",get_PingPong,null,false);
		addMember(l,"ClampForever",getClampForever,null,false);
		addMember(l,"_ClampForever",get_ClampForever,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.WrapMode));
	}
}
