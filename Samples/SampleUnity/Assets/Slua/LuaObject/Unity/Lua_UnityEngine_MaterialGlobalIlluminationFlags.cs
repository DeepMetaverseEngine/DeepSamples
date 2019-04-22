using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_MaterialGlobalIlluminationFlags : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MaterialGlobalIlluminationFlags.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MaterialGlobalIlluminationFlags.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRealtimeEmissive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MaterialGlobalIlluminationFlags.RealtimeEmissive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RealtimeEmissive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MaterialGlobalIlluminationFlags.RealtimeEmissive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBakedEmissive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MaterialGlobalIlluminationFlags.BakedEmissive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BakedEmissive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MaterialGlobalIlluminationFlags.BakedEmissive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAnyEmissive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MaterialGlobalIlluminationFlags.AnyEmissive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AnyEmissive(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MaterialGlobalIlluminationFlags.AnyEmissive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEmissiveIsBlack(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MaterialGlobalIlluminationFlags.EmissiveIsBlack);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EmissiveIsBlack(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MaterialGlobalIlluminationFlags.EmissiveIsBlack);
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
		getTypeTable(l,"UnityEngine.MaterialGlobalIlluminationFlags");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"RealtimeEmissive",getRealtimeEmissive,null,false);
		addMember(l,"_RealtimeEmissive",get_RealtimeEmissive,null,false);
		addMember(l,"BakedEmissive",getBakedEmissive,null,false);
		addMember(l,"_BakedEmissive",get_BakedEmissive,null,false);
		addMember(l,"AnyEmissive",getAnyEmissive,null,false);
		addMember(l,"_AnyEmissive",get_AnyEmissive,null,false);
		addMember(l,"EmissiveIsBlack",getEmissiveIsBlack,null,false);
		addMember(l,"_EmissiveIsBlack",get_EmissiveIsBlack,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.MaterialGlobalIlluminationFlags));
	}
}
