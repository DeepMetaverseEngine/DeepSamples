using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_Assets_Scripts_Data_NpcQuestManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager o;
			o=new Assets.Scripts.Data.NpcQuestManager();
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
	static public int Clear(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
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
	static public int ShowCompleteAnim(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			self.ShowCompleteAnim();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddListener(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			Assets.Scripts.Data.INpcQuestInterface a1;
			checkType(l,2,out a1);
			self.AddListener(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveListener(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			Assets.Scripts.Data.INpcQuestInterface a1;
			checkType(l,2,out a1);
			self.RemoveListener(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddMapNpcListener(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			Assets.Scripts.Data.IMapNpcQuestInterface a1;
			checkType(l,2,out a1);
			self.AddMapNpcListener(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveMapListener(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			Assets.Scripts.Data.IMapNpcQuestInterface a1;
			checkType(l,2,out a1);
			self.RemoveMapListener(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetQuestState(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetQuestState(a1);
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
	static public int GetQuestType(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetQuestType(a1);
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
	static public int HasQuestByTalk(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.HasQuestByTalk(a1);
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
	static public int NpchasQuest(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			TLClient.Modules.Quest a1;
			checkType(l,2,out a1);
			var ret=self.NpchasQuest(a1);
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
	static public int GetNpcQuestData(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetNpcQuestData(a1);
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
	static public int GetNpcLoopQuestData(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetNpcLoopQuestData(a1);
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
	static public int TalkWithNpcByUnit(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			TLAIUnit a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.TalkWithNpcByUnit(a1,a2);
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
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			TLQuest a2;
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
	static public int get_NoneQuestState(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,Assets.Scripts.Data.NpcQuestManager.NoneQuestState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onCompare(IntPtr l) {
		try {
			Assets.Scripts.Data.NpcQuestManager self=(Assets.Scripts.Data.NpcQuestManager)checkSelf(l);
			Assets.Scripts.Data.NpcQuestManager.OnSortCompareHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.onCompare=v;
			else if(op==1) self.onCompare+=v;
			else if(op==2) self.onCompare-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"NpcQuestManager");
		addMember(l,Clear);
		addMember(l,ShowCompleteAnim);
		addMember(l,AddListener);
		addMember(l,RemoveListener);
		addMember(l,AddMapNpcListener);
		addMember(l,RemoveMapListener);
		addMember(l,GetQuestState);
		addMember(l,GetQuestType);
		addMember(l,HasQuestByTalk);
		addMember(l,NpchasQuest);
		addMember(l,GetNpcQuestData);
		addMember(l,GetNpcLoopQuestData);
		addMember(l,TalkWithNpcByUnit);
		addMember(l,Notify);
		addMember(l,"NoneQuestState",get_NoneQuestState,null,false);
		addMember(l,"onCompare",null,set_onCompare,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(Assets.Scripts.Data.NpcQuestManager),typeof(QuestDataListener));
	}
}
