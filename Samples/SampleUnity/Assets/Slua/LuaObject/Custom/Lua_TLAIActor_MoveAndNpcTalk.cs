using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAIActor_MoveAndNpcTalk : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk o;
			System.Int32 a1;
			checkType(l,2,out a1);
			o=new TLAIActor.MoveAndNpcTalk(a1);
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
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
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
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
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
	static public int OnUpdate(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
			self.OnUpdate();
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
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
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
	static public int get_NpcTemplateId(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.NpcTemplateId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_NpcTemplateId(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.NpcTemplateId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WaitState(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.WaitState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_WaitState(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.WaitState=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_bPlayAnimation(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.bPlayAnimation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_bPlayAnimation(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.bPlayAnimation=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OpenFunction(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.OpenFunction);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OpenFunction(IntPtr l) {
		try {
			TLAIActor.MoveAndNpcTalk self=(TLAIActor.MoveAndNpcTalk)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.OpenFunction=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"MoveAndNpcTalk");
		addMember(l,CreateIntance);
		addMember(l,DoAction);
		addMember(l,OnUpdate);
		addMember(l,Clone);
		addMember(l,"NpcTemplateId",get_NpcTemplateId,set_NpcTemplateId,true);
		addMember(l,"WaitState",get_WaitState,set_WaitState,true);
		addMember(l,"bPlayAnimation",get_bPlayAnimation,set_bPlayAnimation,true);
		addMember(l,"OpenFunction",get_OpenFunction,set_OpenFunction,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLAIActor.MoveAndNpcTalk),typeof(TLAIActor.MoveEndAction));
	}
}
