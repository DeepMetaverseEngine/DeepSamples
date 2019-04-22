using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_LuaInputStream : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			LuaInputStream o;
			System.Byte[] a1;
			checkArray(l,2,out a1);
			o=new LuaInputStream(a1);
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetU8(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetU8();
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
	static public int GetS8(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetS8();
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
	static public int GetS16(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetS16();
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
	static public int GetU16(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetU16();
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
	static public int GetS32(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetS32();
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
	static public int GetU32(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetU32();
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
	static public int GetS64(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetS64();
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
	static public int GetU64(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetU64();
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
	static public int GetF32(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetF32();
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
	static public int GetF64(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetF64();
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
	static public int GetBool(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetBool();
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
	static public int GetUnicode(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetUnicode();
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
	static public int GetUTF(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetUTF();
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
	static public int GetEnum8(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetEnum8();
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
	static public int GetEnum32(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetEnum32();
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
	static public int GetDateTime(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetDateTime();
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
	static public int GetTimeSpan(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetTimeSpan();
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
	static public int GetBytes(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetBytes();
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
	static public int GetVS32(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			var ret=self.GetVS32();
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
	static public int Dispose(IntPtr l) {
		try {
			LuaInputStream self=(LuaInputStream)checkSelf(l);
			self.Dispose();
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
		getTypeTable(l,"LuaInputStream");
		addMember(l,GetU8);
		addMember(l,GetS8);
		addMember(l,GetS16);
		addMember(l,GetU16);
		addMember(l,GetS32);
		addMember(l,GetU32);
		addMember(l,GetS64);
		addMember(l,GetU64);
		addMember(l,GetF32);
		addMember(l,GetF64);
		addMember(l,GetBool);
		addMember(l,GetUnicode);
		addMember(l,GetUTF);
		addMember(l,GetEnum8);
		addMember(l,GetEnum32);
		addMember(l,GetDateTime);
		addMember(l,GetTimeSpan);
		addMember(l,GetBytes);
		addMember(l,GetVS32);
		addMember(l,Dispose);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(LuaInputStream));
	}
}
