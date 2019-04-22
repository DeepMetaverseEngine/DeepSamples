using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAIActor_EnterGuildAction : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLAIActor.EnterGuildAction o;
			System.Boolean a1;
			checkType(l,2,out a1);
			o=new TLAIActor.EnterGuildAction(a1);
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
			TLAIActor.EnterGuildAction self=(TLAIActor.EnterGuildAction)checkSelf(l);
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
	static public int Clone(IntPtr l) {
		try {
			TLAIActor.EnterGuildAction self=(TLAIActor.EnterGuildAction)checkSelf(l);
			var ret=self.Clone();
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
	static public int get_isSelf(IntPtr l) {
		try {
			TLAIActor.EnterGuildAction self=(TLAIActor.EnterGuildAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.isSelf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_isSelf(IntPtr l) {
		try {
			TLAIActor.EnterGuildAction self=(TLAIActor.EnterGuildAction)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.isSelf=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"EnterGuildAction");
		addMember(l,CreateIntance);
		addMember(l,Clone);
		addMember(l,"isSelf",get_isSelf,set_isSelf,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLAIActor.EnterGuildAction),typeof(TLAIActor.MoveEndAction));
	}
}
