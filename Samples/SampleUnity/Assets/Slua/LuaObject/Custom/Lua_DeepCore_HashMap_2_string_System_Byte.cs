using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_HashMap_2_string_System_Byte : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.HashMap<System.String,System.Byte> o;
			if(argc==1){
				o=new DeepCore.HashMap<System.String,System.Byte>();
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(int))){
				System.Int32 a1;
				checkType(l,2,out a1);
				o=new DeepCore.HashMap<System.String,System.Byte>(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(IEqualityComparer<System.String>))){
				System.Collections.Generic.IEqualityComparer<System.String> a1;
				checkType(l,2,out a1);
				o=new DeepCore.HashMap<System.String,System.Byte>(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(int),typeof(IEqualityComparer<System.String>))){
				System.Int32 a1;
				checkType(l,2,out a1);
				System.Collections.Generic.IEqualityComparer<System.String> a2;
				checkType(l,3,out a2);
				o=new DeepCore.HashMap<System.String,System.Byte>(a1,a2);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(IDictionary<System.String,System.Byte>))){
				System.Collections.Generic.IDictionary<System.String,System.Byte> a1;
				checkType(l,2,out a1);
				o=new DeepCore.HashMap<System.String,System.Byte>(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(IDictionary<System.String,System.Byte>),typeof(IEqualityComparer<System.String>))){
				System.Collections.Generic.IDictionary<System.String,System.Byte> a1;
				checkType(l,2,out a1);
				System.Collections.Generic.IEqualityComparer<System.String> a2;
				checkType(l,3,out a2);
				o=new DeepCore.HashMap<System.String,System.Byte>(a1,a2);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			return error(l,"New object failed.");
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Get(IntPtr l) {
		try {
			DeepCore.HashMap<System.String,System.Byte> self=(DeepCore.HashMap<System.String,System.Byte>)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.Get(a1);
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
	static public int Put(IntPtr l) {
		try {
			DeepCore.HashMap<System.String,System.Byte> self=(DeepCore.HashMap<System.String,System.Byte>)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Byte a2;
			checkType(l,3,out a2);
			self.Put(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int TryAdd(IntPtr l) {
		try {
			DeepCore.HashMap<System.String,System.Byte> self=(DeepCore.HashMap<System.String,System.Byte>)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Byte a2;
			checkType(l,3,out a2);
			var ret=self.TryAdd(a1,a2);
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
	static public int TryAddOrUpdate(IntPtr l) {
		try {
			DeepCore.HashMap<System.String,System.Byte> self=(DeepCore.HashMap<System.String,System.Byte>)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Byte a2;
			checkType(l,3,out a2);
			var ret=self.TryAddOrUpdate(a1,a2);
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
	static public int TryGetOrCreate(IntPtr l) {
		try {
			DeepCore.HashMap<System.String,System.Byte> self=(DeepCore.HashMap<System.String,System.Byte>)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Byte a2;
			checkType(l,3,out a2);
			System.Func<System.String,System.Byte> a3;
			checkDelegate(l,4,out a3);
			var ret=self.TryGetOrCreate(a1,out a2,a3);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a2);
			return 3;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveByKey(IntPtr l) {
		try {
			DeepCore.HashMap<System.String,System.Byte> self=(DeepCore.HashMap<System.String,System.Byte>)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.RemoveByKey(a1);
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
	static public int PutAll(IntPtr l) {
		try {
			DeepCore.HashMap<System.String,System.Byte> self=(DeepCore.HashMap<System.String,System.Byte>)checkSelf(l);
			System.Collections.Generic.IDictionary<System.String,System.Byte> a1;
			checkType(l,2,out a1);
			self.PutAll(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddAll(IntPtr l) {
		try {
			DeepCore.HashMap<System.String,System.Byte> self=(DeepCore.HashMap<System.String,System.Byte>)checkSelf(l);
			System.Collections.Generic.IDictionary<System.String,System.Byte> a1;
			checkType(l,2,out a1);
			self.AddAll(a1);
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
		getTypeTable(l,"DeepCore.HashMap<<System.String, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>,<System.Byte, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>>");
		addMember(l,Get);
		addMember(l,Put);
		addMember(l,TryAdd);
		addMember(l,TryAddOrUpdate);
		addMember(l,TryGetOrCreate);
		addMember(l,RemoveByKey);
		addMember(l,PutAll);
		addMember(l,AddAll);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.HashMap<System.String,System.Byte>),typeof(System.Collections.Generic.Dictionary<System.String,System.Byte>));
	}
}
