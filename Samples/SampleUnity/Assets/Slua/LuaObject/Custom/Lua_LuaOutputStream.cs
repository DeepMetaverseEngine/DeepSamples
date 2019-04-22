using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_LuaOutputStream : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			LuaOutputStream o;
			o=new LuaOutputStream();
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
	static public int PutU8(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Byte a1;
			checkType(l,2,out a1);
			self.PutU8(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutS8(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.SByte a1;
			checkType(l,2,out a1);
			self.PutS8(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutS16(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Int16 a1;
			checkType(l,2,out a1);
			self.PutS16(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutU16(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.UInt16 a1;
			checkType(l,2,out a1);
			self.PutU16(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutS32(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.PutS32(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutU32(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.UInt32 a1;
			checkType(l,2,out a1);
			self.PutU32(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutS64(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Int64 a1;
			checkType(l,2,out a1);
			self.PutS64(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutU64(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.UInt64 a1;
			checkType(l,2,out a1);
			self.PutU64(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutF32(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.PutF32(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutF64(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Double a1;
			checkType(l,2,out a1);
			self.PutF64(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutBool(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.PutBool(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutUnicode(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Char a1;
			checkType(l,2,out a1);
			self.PutUnicode(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutUTF(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.PutUTF(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutEnum8(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.ValueType a1;
			checkType(l,2,out a1);
			self.PutEnum8(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutEnum32(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.ValueType a1;
			checkType(l,2,out a1);
			self.PutEnum32(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutDateTime(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.DateTime a1;
			checkValueType(l,2,out a1);
			self.PutDateTime(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutTimeSpan(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.TimeSpan a1;
			checkValueType(l,2,out a1);
			self.PutTimeSpan(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutBytes(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Byte[] a1;
			checkArray(l,2,out a1);
			self.PutBytes(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PutVS32(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.PutVS32(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Dispose(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
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
	static public int ToArray(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			var ret=self.ToArray();
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Buffer(IntPtr l) {
		try {
			LuaOutputStream self=(LuaOutputStream)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Buffer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"LuaOutputStream");
		addMember(l,PutU8);
		addMember(l,PutS8);
		addMember(l,PutS16);
		addMember(l,PutU16);
		addMember(l,PutS32);
		addMember(l,PutU32);
		addMember(l,PutS64);
		addMember(l,PutU64);
		addMember(l,PutF32);
		addMember(l,PutF64);
		addMember(l,PutBool);
		addMember(l,PutUnicode);
		addMember(l,PutUTF);
		addMember(l,PutEnum8);
		addMember(l,PutEnum32);
		addMember(l,PutDateTime);
		addMember(l,PutTimeSpan);
		addMember(l,PutBytes);
		addMember(l,PutVS32);
		addMember(l,Dispose);
		addMember(l,ToArray);
		addMember(l,"Buffer",get_Buffer,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(LuaOutputStream));
	}
}
