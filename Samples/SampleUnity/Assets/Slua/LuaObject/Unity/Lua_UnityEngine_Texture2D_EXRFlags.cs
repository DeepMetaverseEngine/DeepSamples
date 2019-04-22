using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Texture2D_EXRFlags : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Texture2D.EXRFlags.None);
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
			pushValue(l,(double)UnityEngine.Texture2D.EXRFlags.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOutputAsFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Texture2D.EXRFlags.OutputAsFloat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OutputAsFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Texture2D.EXRFlags.OutputAsFloat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCompressZIP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Texture2D.EXRFlags.CompressZIP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CompressZIP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Texture2D.EXRFlags.CompressZIP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCompressRLE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Texture2D.EXRFlags.CompressRLE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CompressRLE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Texture2D.EXRFlags.CompressRLE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCompressPIZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Texture2D.EXRFlags.CompressPIZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CompressPIZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Texture2D.EXRFlags.CompressPIZ);
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
		getTypeTable(l,"UnityEngine.Texture2D.EXRFlags");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"OutputAsFloat",getOutputAsFloat,null,false);
		addMember(l,"_OutputAsFloat",get_OutputAsFloat,null,false);
		addMember(l,"CompressZIP",getCompressZIP,null,false);
		addMember(l,"_CompressZIP",get_CompressZIP,null,false);
		addMember(l,"CompressRLE",getCompressRLE,null,false);
		addMember(l,"_CompressRLE",get_CompressRLE,null,false);
		addMember(l,"CompressPIZ",getCompressPIZ,null,false);
		addMember(l,"_CompressPIZ",get_CompressPIZ,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Texture2D.EXRFlags));
	}
}
