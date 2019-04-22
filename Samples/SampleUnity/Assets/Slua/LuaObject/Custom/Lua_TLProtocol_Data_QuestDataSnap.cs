using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_QuestDataSnap : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap o;
			o=new TLProtocol.Data.QuestDataSnap();
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
	static public int get_QuestState_Available(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.QuestDataSnap.QuestState_Available);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestState_Accepted(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.QuestDataSnap.QuestState_Accepted);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestState_Completed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.QuestDataSnap.QuestState_Completed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestState_Failed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.QuestDataSnap.QuestState_Failed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestState_Removed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.QuestDataSnap.QuestState_Removed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestState_Submited(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.QuestDataSnap.QuestState_Submited);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
	static public int get_ProgressList(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ProgressList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ProgressList(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
			System.Collections.Generic.List<TLProtocol.Data.TLProgressData> v;
			checkType(l,2,out v);
			self.ProgressList=v;
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
	static public int get_curLoopNum(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
	static public int get_mainType(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
	static public int get_TimeLife(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
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
	static public int get_isForceAccept(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.isForceAccept);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_isForceAccept(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.isForceAccept=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QuestName(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.QuestName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_QuestName(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.QuestName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NeedInitListener(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.NeedInitListener);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_NeedInitListener(IntPtr l) {
		try {
			TLProtocol.Data.QuestDataSnap self=(TLProtocol.Data.QuestDataSnap)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.NeedInitListener=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLProtocol.Data.QuestDataSnap");
		addMember(l,"QuestState_Available",get_QuestState_Available,null,false);
		addMember(l,"QuestState_Accepted",get_QuestState_Accepted,null,false);
		addMember(l,"QuestState_Completed",get_QuestState_Completed,null,false);
		addMember(l,"QuestState_Failed",get_QuestState_Failed,null,false);
		addMember(l,"QuestState_Removed",get_QuestState_Removed,null,false);
		addMember(l,"QuestState_Submited",get_QuestState_Submited,null,false);
		addMember(l,"id",get_id,set_id,true);
		addMember(l,"ProgressList",get_ProgressList,set_ProgressList,true);
		addMember(l,"state",get_state,set_state,true);
		addMember(l,"curLoopNum",get_curLoopNum,set_curLoopNum,true);
		addMember(l,"MaxLoopNum",get_MaxLoopNum,set_MaxLoopNum,true);
		addMember(l,"mainType",get_mainType,set_mainType,true);
		addMember(l,"subType",get_subType,set_subType,true);
		addMember(l,"TimeLife",get_TimeLife,set_TimeLife,true);
		addMember(l,"isForceAccept",get_isForceAccept,set_isForceAccept,true);
		addMember(l,"QuestName",get_QuestName,set_QuestName,true);
		addMember(l,"NeedInitListener",get_NeedInitListener,set_NeedInitListener,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLProtocol.Data.QuestDataSnap));
	}
}
