using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAIActor_MoveAndBattle : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLAIActor.MoveAndBattle o;
			System.String a1;
			checkType(l,2,out a1);
			o=new TLAIActor.MoveAndBattle(a1);
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
			TLAIActor.MoveAndBattle self=(TLAIActor.MoveAndBattle)checkSelf(l);
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
	static public int DoAction(IntPtr l) {
		try {
			TLAIActor.MoveAndBattle self=(TLAIActor.MoveAndBattle)checkSelf(l);
			self.DoAction();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clone(IntPtr l) {
		try {
			TLAIActor.MoveAndBattle self=(TLAIActor.MoveAndBattle)checkSelf(l);
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
	static public int get_monsterIds(IntPtr l) {
		try {
			TLAIActor.MoveAndBattle self=(TLAIActor.MoveAndBattle)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.monsterIds);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_monsterIds(IntPtr l) {
		try {
			TLAIActor.MoveAndBattle self=(TLAIActor.MoveAndBattle)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.monsterIds=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"MoveAndBattle");
		addMember(l,CreateIntance);
		addMember(l,DoAction);
		addMember(l,Clone);
		addMember(l,"monsterIds",get_monsterIds,set_monsterIds,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLAIActor.MoveAndBattle),typeof(TLAIActor.MoveEndAction));
	}
}
