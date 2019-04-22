using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAIActor_EnterGuildAndNpcTalk : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLAIActor.EnterGuildAndNpcTalk o;
			System.Int32 a1;
			checkType(l,2,out a1);
			o=new TLAIActor.EnterGuildAndNpcTalk(a1);
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
	static public int CreateIntance(IntPtr l) {
		try {
			TLAIActor.EnterGuildAndNpcTalk self=(TLAIActor.EnterGuildAndNpcTalk)checkSelf(l);
			var ret=self.CreateIntance();
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"EnterGuildAndNpcTalk");
		addMember(l,CreateIntance);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLAIActor.EnterGuildAndNpcTalk),typeof(TLAIActor.MoveAndNpcTalk));
	}
}
