using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepMMO_Protocol_MessageCodeManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepMMO.Protocol.MessageCodeManager o;
			DeepCore.IO.ISerializerFactory a1;
			checkType(l,2,out a1);
			o=new DeepMMO.Protocol.MessageCodeManager(a1);
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
	static public int Load(IntPtr l) {
		try {
			DeepMMO.Protocol.MessageCodeManager self=(DeepMMO.Protocol.MessageCodeManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.Load(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Save(IntPtr l) {
		try {
			DeepMMO.Protocol.MessageCodeManager self=(DeepMMO.Protocol.MessageCodeManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.Save(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetCodeMessage(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				DeepMMO.Protocol.MessageCodeManager self=(DeepMMO.Protocol.MessageCodeManager)checkSelf(l);
				DeepMMO.Protocol.Response a1;
				checkType(l,2,out a1);
				var ret=self.GetCodeMessage(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(int))){
				DeepMMO.Protocol.MessageCodeManager self=(DeepMMO.Protocol.MessageCodeManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				var ret=self.GetCodeMessage(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(System.Type),typeof(int))){
				DeepMMO.Protocol.MessageCodeManager self=(DeepMMO.Protocol.MessageCodeManager)checkSelf(l);
				System.Type a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				var ret=self.GetCodeMessage(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function GetCodeMessage to call");
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
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepMMO.Protocol.MessageCodeManager.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Factory(IntPtr l) {
		try {
			DeepMMO.Protocol.MessageCodeManager self=(DeepMMO.Protocol.MessageCodeManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Factory);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"MessageCodeManager");
		addMember(l,Load);
		addMember(l,Save);
		addMember(l,GetCodeMessage);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"Factory",get_Factory,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepMMO.Protocol.MessageCodeManager));
	}
}
