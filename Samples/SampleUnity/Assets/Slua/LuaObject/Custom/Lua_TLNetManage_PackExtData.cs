using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLNetManage_PackExtData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			TLNetManage.PackExtData o;
			if(argc==3){
				System.Boolean a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				o=new TLNetManage.PackExtData(a1,a2);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==4){
				System.Boolean a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				System.Action a3;
				checkDelegate(l,4,out a3);
				o=new TLNetManage.PackExtData(a1,a2,a3);
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
	static public int get_IsWaiting(IntPtr l) {
		try {
			TLNetManage.PackExtData self=(TLNetManage.PackExtData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsWaiting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsWaiting(IntPtr l) {
		try {
			TLNetManage.PackExtData self=(TLNetManage.PackExtData)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsWaiting=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsShowError(IntPtr l) {
		try {
			TLNetManage.PackExtData self=(TLNetManage.PackExtData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsShowError);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsShowError(IntPtr l) {
		try {
			TLNetManage.PackExtData self=(TLNetManage.PackExtData)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsShowError=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TimeOutCb(IntPtr l) {
		try {
			TLNetManage.PackExtData self=(TLNetManage.PackExtData)checkSelf(l);
			System.Action v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.TimeOutCb=v;
			else if(op==1) self.TimeOutCb+=v;
			else if(op==2) self.TimeOutCb-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"PackExtData");
		addMember(l,"IsWaiting",get_IsWaiting,set_IsWaiting,true);
		addMember(l,"IsShowError",get_IsShowError,set_IsShowError,true);
		addMember(l,"TimeOutCb",null,set_TimeOutCb,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLNetManage.PackExtData));
	}
}
