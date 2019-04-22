using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_FuckPomeloClient_PomeloException : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.FuckPomeloClient.PomeloException o;
			if(argc==2){
				System.String a1;
				checkType(l,2,out a1);
				o=new DeepCore.FuckPomeloClient.PomeloException(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==3){
				System.String a1;
				checkType(l,2,out a1);
				System.Exception a2;
				checkType(l,3,out a2);
				o=new DeepCore.FuckPomeloClient.PomeloException(a1,a2);
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
	static public int get_Timeout(IntPtr l) {
		try {
			DeepCore.FuckPomeloClient.PomeloException self=(DeepCore.FuckPomeloClient.PomeloException)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Timeout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DeepCore.FuckPomeloClient.PomeloException");
		addMember(l,"Timeout",get_Timeout,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.FuckPomeloClient.PomeloException),typeof(System.Exception));
	}
}
