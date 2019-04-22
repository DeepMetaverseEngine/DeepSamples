using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_QuestData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			QuestData o;
			o=new QuestData();
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
	static public int GetQuest(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetQuest(a1);
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
	static public int RemoveQuest(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.RemoveQuest(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetQuestConditionTypeIsNotEnough(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.GetQuestConditionTypeIsNotEnough(a1,a2);
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
	static public int InitNetWork(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
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
	static public int CreateQuest(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			TLProtocol.Data.QuestDataSnap a1;
			checkType(l,2,out a1);
			var ret=self.CreateQuest(a1);
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
	static public int CreateQuestItemSnap(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			var ret=self.CreateQuestItemSnap(a1,a2,a3);
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
	static public int Clear(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
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
	static public int AttachLuaObserver(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
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
			QuestData self=(QuestData)checkSelf(l);
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
	static public int AttachObserver(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			QuestDataListener a1;
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
			QuestData self=(QuestData)checkSelf(l);
			QuestDataListener a1;
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
	static public int Notify(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			TLClient.Modules.Quest a2;
			checkType(l,3,out a2);
			self.Notify(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int QuestModule_s(IntPtr l) {
		try {
			var ret=QuestData.QuestModule();
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
	static public int get_AllQuests(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AllQuests);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Accepts(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Accepts);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NotAccepts(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.NotAccepts);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsInit(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsInit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsInit(IntPtr l) {
		try {
			QuestData self=(QuestData)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsInit=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"QuestData");
		addMember(l,GetQuest);
		addMember(l,RemoveQuest);
		addMember(l,GetQuestConditionTypeIsNotEnough);
		addMember(l,InitNetWork);
		addMember(l,CreateQuest);
		addMember(l,CreateQuestItemSnap);
		addMember(l,Clear);
		addMember(l,AttachLuaObserver);
		addMember(l,DetachLuaObserver);
		addMember(l,AttachObserver);
		addMember(l,DetachObserver);
		addMember(l,Notify);
		addMember(l,QuestModule_s);
		addMember(l,"AllQuests",get_AllQuests,null,true);
		addMember(l,"Accepts",get_Accepts,null,true);
		addMember(l,"NotAccepts",get_NotAccepts,null,true);
		addMember(l,"IsInit",get_IsInit,set_IsInit,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(QuestData));
	}
}
