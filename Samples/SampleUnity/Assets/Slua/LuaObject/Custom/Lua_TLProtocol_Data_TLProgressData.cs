using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_TLProgressData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData o;
			o=new TLProtocol.Data.TLProgressData();
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
	static public int get_Type(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Type);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Type(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.Type=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Arg1(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Arg1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Arg1(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Arg1=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Arg2(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Arg2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Arg2(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Arg2=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurValue(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurValue);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_CurValue(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.CurValue=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TargetValue(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TargetValue);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TargetValue(IntPtr l) {
		try {
			TLProtocol.Data.TLProgressData self=(TLProtocol.Data.TLProgressData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.TargetValue=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"QuestProgressData");
		addMember(l,"Type",get_Type,set_Type,true);
		addMember(l,"Arg1",get_Arg1,set_Arg1,true);
		addMember(l,"Arg2",get_Arg2,set_Arg2,true);
		addMember(l,"CurValue",get_CurValue,set_CurValue,true);
		addMember(l,"TargetValue",get_TargetValue,set_TargetValue,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLProtocol.Data.TLProgressData));
	}
}
