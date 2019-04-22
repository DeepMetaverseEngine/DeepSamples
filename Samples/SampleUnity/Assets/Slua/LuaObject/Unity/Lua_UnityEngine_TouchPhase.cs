using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_TouchPhase : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBegan(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TouchPhase.Began);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Began(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TouchPhase.Began);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMoved(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TouchPhase.Moved);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Moved(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TouchPhase.Moved);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStationary(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TouchPhase.Stationary);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Stationary(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TouchPhase.Stationary);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEnded(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TouchPhase.Ended);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Ended(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TouchPhase.Ended);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCanceled(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.TouchPhase.Canceled);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Canceled(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.TouchPhase.Canceled);
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
		getTypeTable(l,"UnityEngine.TouchPhase");
		addMember(l,"Began",getBegan,null,false);
		addMember(l,"_Began",get_Began,null,false);
		addMember(l,"Moved",getMoved,null,false);
		addMember(l,"_Moved",get_Moved,null,false);
		addMember(l,"Stationary",getStationary,null,false);
		addMember(l,"_Stationary",get_Stationary,null,false);
		addMember(l,"Ended",getEnded,null,false);
		addMember(l,"_Ended",get_Ended,null,false);
		addMember(l,"Canceled",getCanceled,null,false);
		addMember(l,"_Canceled",get_Canceled,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.TouchPhase));
	}
}
