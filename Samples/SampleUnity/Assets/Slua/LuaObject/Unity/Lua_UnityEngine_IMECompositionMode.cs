using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_IMECompositionMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAuto(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.IMECompositionMode.Auto);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Auto(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.IMECompositionMode.Auto);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOn(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.IMECompositionMode.On);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_On(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.IMECompositionMode.On);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.IMECompositionMode.Off);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Off(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.IMECompositionMode.Off);
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
		getTypeTable(l,"UnityEngine.IMECompositionMode");
		addMember(l,"Auto",getAuto,null,false);
		addMember(l,"_Auto",get_Auto,null,false);
		addMember(l,"On",getOn,null,false);
		addMember(l,"_On",get_On,null,false);
		addMember(l,"Off",getOff,null,false);
		addMember(l,"_Off",get_Off,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.IMECompositionMode));
	}
}
