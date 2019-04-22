using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLQuest : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			TLQuest o;
			if(argc==2){
				TLProtocol.Data.QuestDataSnap a1;
				checkType(l,2,out a1);
				o=new TLQuest(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==1){
				o=new TLQuest();
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
	static public int IsEnoughCondition(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.IsEnoughCondition(a1);
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
	static public int ConditionCur(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.ConditionCur(a1);
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
	static public int ConditionMax(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.ConditionMax(a1);
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
	static public int Dispose(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			self.Dispose();
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
	static public int get_tracing(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.tracing);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_tracing(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.tracing=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TempAction(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TempAction);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TempAction(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			TLAIActor.MoveEndAction v;
			checkType(l,2,out v);
			self.TempAction=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Static(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Static);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ProgressMax(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ProgressMax);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ProgressCur(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ProgressCur);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConditionCount(IntPtr l) {
		try {
			TLQuest self=(TLQuest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ConditionCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLQuest");
		addMember(l,IsEnoughCondition);
		addMember(l,ConditionCur);
		addMember(l,ConditionMax);
		addMember(l,Dispose);
		addMember(l,"tracing",get_tracing,set_tracing,true);
		addMember(l,"TempAction",get_TempAction,set_TempAction,true);
		addMember(l,"Static",get_Static,null,true);
		addMember(l,"ProgressMax",get_ProgressMax,null,true);
		addMember(l,"ProgressCur",get_ProgressCur,null,true);
		addMember(l,"ConditionCount",get_ConditionCount,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLQuest),typeof(TLClient.Modules.Quest));
	}
}
