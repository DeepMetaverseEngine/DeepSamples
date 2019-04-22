using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CubemapFace : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPositiveX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CubemapFace.PositiveX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PositiveX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CubemapFace.PositiveX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNegativeX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CubemapFace.NegativeX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NegativeX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CubemapFace.NegativeX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPositiveY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CubemapFace.PositiveY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PositiveY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CubemapFace.PositiveY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNegativeY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CubemapFace.NegativeY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NegativeY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CubemapFace.NegativeY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPositiveZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CubemapFace.PositiveZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PositiveZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CubemapFace.PositiveZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNegativeZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CubemapFace.NegativeZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NegativeZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CubemapFace.NegativeZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUnknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CubemapFace.Unknown);
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
			pushValue(l,(double)UnityEngine.CubemapFace.Unknown);
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
		getTypeTable(l,"UnityEngine.CubemapFace");
		addMember(l,"PositiveX",getPositiveX,null,false);
		addMember(l,"_PositiveX",get_PositiveX,null,false);
		addMember(l,"NegativeX",getNegativeX,null,false);
		addMember(l,"_NegativeX",get_NegativeX,null,false);
		addMember(l,"PositiveY",getPositiveY,null,false);
		addMember(l,"_PositiveY",get_PositiveY,null,false);
		addMember(l,"NegativeY",getNegativeY,null,false);
		addMember(l,"_NegativeY",get_NegativeY,null,false);
		addMember(l,"PositiveZ",getPositiveZ,null,false);
		addMember(l,"_PositiveZ",get_PositiveZ,null,false);
		addMember(l,"NegativeZ",getNegativeZ,null,false);
		addMember(l,"_NegativeZ",get_NegativeZ,null,false);
		addMember(l,"Unknown",getUnknown,null,false);
		addMember(l,"_Unknown",get_Unknown,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CubemapFace));
	}
}
