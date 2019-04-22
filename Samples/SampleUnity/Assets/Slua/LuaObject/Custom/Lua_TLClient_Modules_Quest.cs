using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Modules_Quest : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLClient.Modules.Quest o;
			o=new TLClient.Modules.Quest();
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
	static public int GetQuestItemCount(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetQuestItemCount(a1);
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
	static public int IsConditionFinish(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.IsConditionFinish(a1);
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
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
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
	static public int GetStatic(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			var ret=self.GetStatic();
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
	static public int get_id(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.id);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_id(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.id=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_state(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.state);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_state(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.state=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mainType(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mainType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mainType(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.mainType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_subType(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.subType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_subType(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.subType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_curLoopNum(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.curLoopNum);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_curLoopNum(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.curLoopNum=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MaxLoopNum(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MaxLoopNum);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MaxLoopNum(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.MaxLoopNum=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TimeLife(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TimeLife);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TimeLife(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.DateTime v;
			checkValueType(l,2,out v);
			self.TimeLife=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_progress(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.progress);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_progress(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			System.Collections.Generic.List<TLProtocol.Data.TLProgressData> v;
			checkType(l,2,out v);
			self.progress=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_questItemIds(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.questItemIds);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_questItemIds(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			List<System.Int32> v;
			checkType(l,2,out v);
			self.questItemIds=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_hasQuestItem(IntPtr l) {
		try {
			TLClient.Modules.Quest self=(TLClient.Modules.Quest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.hasQuestItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLClient.Modules.Quest");
		addMember(l,GetQuestItemCount);
		addMember(l,IsConditionFinish);
		addMember(l,Dispose);
		addMember(l,GetStatic);
		addMember(l,"id",get_id,set_id,true);
		addMember(l,"state",get_state,set_state,true);
		addMember(l,"mainType",get_mainType,set_mainType,true);
		addMember(l,"subType",get_subType,set_subType,true);
		addMember(l,"curLoopNum",get_curLoopNum,set_curLoopNum,true);
		addMember(l,"MaxLoopNum",get_MaxLoopNum,set_MaxLoopNum,true);
		addMember(l,"TimeLife",get_TimeLife,set_TimeLife,true);
		addMember(l,"progress",get_progress,set_progress,true);
		addMember(l,"questItemIds",get_questItemIds,set_questItemIds,true);
		addMember(l,"hasQuestItem",get_hasQuestItem,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLClient.Modules.Quest));
	}
}
