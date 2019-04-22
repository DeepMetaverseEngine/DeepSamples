using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Playables_FrameData_EvaluationType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEvaluate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.FrameData.EvaluationType.Evaluate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Evaluate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.FrameData.EvaluationType.Evaluate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPlayback(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.FrameData.EvaluationType.Playback);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Playback(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.FrameData.EvaluationType.Playback);
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
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Playables.FrameData.EvaluationType");
		addMember(l,"Evaluate",getEvaluate,null,false);
		addMember(l,"_Evaluate",get_Evaluate,null,false);
		addMember(l,"Playback",getPlayback,null,false);
		addMember(l,"_Playback",get_Playback,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Playables.FrameData.EvaluationType));
	}
}
