using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAINPC : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLAINPC o;
			DeepCore.Unity3D.Battle.BattleScene a1;
			checkType(l,2,out a1);
			DeepCore.GameSlave.ZoneUnit a2;
			checkType(l,3,out a2);
			o=new TLAINPC(a1,a2);
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
	static public int GetQuestList(IntPtr l) {
		try {
			TLAINPC self=(TLAINPC)checkSelf(l);
			var ret=self.GetQuestList();
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
	static public int GetObjId(IntPtr l) {
		try {
			TLAINPC self=(TLAINPC)checkSelf(l);
			var ret=self.GetObjId();
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
	static public int GetTemplateId(IntPtr l) {
		try {
			TLAINPC self=(TLAINPC)checkSelf(l);
			var ret=self.GetTemplateId();
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
	static public int Name(IntPtr l) {
		try {
			TLAINPC self=(TLAINPC)checkSelf(l);
			var ret=self.Name();
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
	static public int UpdateQuest(IntPtr l) {
		try {
			TLAINPC self=(TLAINPC)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			self.UpdateQuest(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int QuestRefreshShow(IntPtr l) {
		try {
			TLAINPC self=(TLAINPC)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.QuestRefreshShow(a1,a2);
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
	static public int get_CurrentQuestList(IntPtr l) {
		try {
			TLAINPC self=(TLAINPC)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentQuestList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLAINPC");
		addMember(l,GetQuestList);
		addMember(l,GetObjId);
		addMember(l,GetTemplateId);
		addMember(l,Name);
		addMember(l,UpdateQuest);
		addMember(l,QuestRefreshShow);
		addMember(l,"CurrentQuestList",get_CurrentQuestList,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLAINPC),typeof(TLAIUnit));
	}
}
