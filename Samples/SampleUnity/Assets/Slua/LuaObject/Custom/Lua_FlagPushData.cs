using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_FlagPushData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			FlagPushData o;
			o=new FlagPushData();
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
	static public int InitNetWork(IntPtr l) {
		try {
			FlagPushData self=(FlagPushData)checkSelf(l);
			self.InitNetWork();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetAttribute(IntPtr l) {
		try {
			FlagPushData self=(FlagPushData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			var ret=self.SetAttribute(a1,a2,a3);
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
	static public int GetFlagState(IntPtr l) {
		try {
			FlagPushData self=(FlagPushData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetFlagState(a1);
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
	static public int AttachObserver(IntPtr l) {
		try {
			FlagPushData self=(FlagPushData)checkSelf(l);
			IObserver<FlagPushData> a1;
			checkType(l,2,out a1);
			self.AttachObserver(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachObserver(IntPtr l) {
		try {
			FlagPushData self=(FlagPushData)checkSelf(l);
			IObserver<FlagPushData> a1;
			checkType(l,2,out a1);
			self.DetachObserver(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AttachLuaObserver(IntPtr l) {
		try {
			FlagPushData self=(FlagPushData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			SLua.LuaTable a2;
			checkType(l,3,out a2);
			self.AttachLuaObserver(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachLuaObserver(IntPtr l) {
		try {
			FlagPushData self=(FlagPushData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.DetachLuaObserver(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Notify(IntPtr l) {
		try {
			FlagPushData self=(FlagPushData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.Notify(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clear(IntPtr l) {
		try {
			FlagPushData self=(FlagPushData)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Clear(a1,a2);
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FLAG_SKILL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,FlagPushData.FLAG_SKILL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FLAG_MAIL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,FlagPushData.FLAG_MAIL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"FlagPushData");
		addMember(l,InitNetWork);
		addMember(l,SetAttribute);
		addMember(l,GetFlagState);
		addMember(l,AttachObserver);
		addMember(l,DetachObserver);
		addMember(l,AttachLuaObserver);
		addMember(l,DetachLuaObserver);
		addMember(l,Notify);
		addMember(l,Clear);
		addMember(l,"FLAG_SKILL",get_FLAG_SKILL,null,false);
		addMember(l,"FLAG_MAIL",get_FLAG_MAIL,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(FlagPushData));
	}
}
