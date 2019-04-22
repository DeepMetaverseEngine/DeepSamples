using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_IO_BinaryMessage : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage o;
			o=new DeepCore.IO.BinaryMessage();
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
	static public int ToArray(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
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
	static public int ReadExternal(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			DeepCore.IO.IInputStream a1;
			checkType(l,2,out a1);
			self.ReadExternal(a1);
			pushValue(l,true);
			setBack(l,self);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WriteExternal(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			DeepCore.IO.IOutputStream a1;
			checkType(l,2,out a1);
			self.WriteExternal(a1);
			pushValue(l,true);
			setBack(l,self);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FromRoute_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			var ret=DeepCore.IO.BinaryMessage.FromRoute(a1);
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
	static public int FromSegment_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			System.ArraySegment<System.Byte> a2;
			checkValueType(l,2,out a2);
			var ret=DeepCore.IO.BinaryMessage.FromSegment(a1,a2);
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
	static public int FromBuffer_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				System.Int32 a1;
				checkType(l,1,out a1);
				System.IO.MemoryStream a2;
				checkType(l,2,out a2);
				var ret=DeepCore.IO.BinaryMessage.FromBuffer(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==4){
				System.Int32 a1;
				checkType(l,1,out a1);
				System.IO.MemoryStream a2;
				checkType(l,2,out a2);
				System.Int32 a3;
				checkType(l,3,out a3);
				System.Int32 a4;
				checkType(l,4,out a4);
				var ret=DeepCore.IO.BinaryMessage.FromBuffer(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function FromBuffer to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CopyFrom_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			System.ArraySegment<System.Byte> a2;
			checkValueType(l,2,out a2);
			var ret=DeepCore.IO.BinaryMessage.CopyFrom(a1,a2);
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
	static public int get_NULL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.IO.BinaryMessage.NULL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Route(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.Route);
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
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.Buffer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BufferOffset(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.BufferOffset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BufferLength(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.BufferLength);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DataSegment(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.DataSegment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsNoRoute(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.IsNoRoute);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HasRoute(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.HasRoute);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsNull(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.IsNull);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HasData(IntPtr l) {
		try {
			DeepCore.IO.BinaryMessage self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.HasData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"BinaryMessage");
		addMember(l,ToArray);
		addMember(l,ReadExternal);
		addMember(l,WriteExternal);
		addMember(l,FromRoute_s);
		addMember(l,FromSegment_s);
		addMember(l,FromBuffer_s);
		addMember(l,CopyFrom_s);
		addMember(l,"NULL",get_NULL,null,false);
		addMember(l,"Route",get_Route,null,true);
		addMember(l,"Buffer",get_Buffer,null,true);
		addMember(l,"BufferOffset",get_BufferOffset,null,true);
		addMember(l,"BufferLength",get_BufferLength,null,true);
		addMember(l,"DataSegment",get_DataSegment,null,true);
		addMember(l,"IsNoRoute",get_IsNoRoute,null,true);
		addMember(l,"HasRoute",get_HasRoute,null,true);
		addMember(l,"IsNull",get_IsNull,null,true);
		addMember(l,"HasData",get_HasData,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.IO.BinaryMessage),typeof(System.ValueType));
	}
}
