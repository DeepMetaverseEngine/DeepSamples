using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Playables_DirectorUpdateMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDSPClock(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.DirectorUpdateMode.DSPClock);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DSPClock(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.DirectorUpdateMode.DSPClock);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getGameTime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.DirectorUpdateMode.GameTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GameTime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.DirectorUpdateMode.GameTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUnscaledGameTime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.DirectorUpdateMode.UnscaledGameTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UnscaledGameTime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.DirectorUpdateMode.UnscaledGameTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getManual(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.DirectorUpdateMode.Manual);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Manual(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.DirectorUpdateMode.Manual);
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
		getTypeTable(l,"UnityEngine.Playables.DirectorUpdateMode");
		addMember(l,"DSPClock",getDSPClock,null,false);
		addMember(l,"_DSPClock",get_DSPClock,null,false);
		addMember(l,"GameTime",getGameTime,null,false);
		addMember(l,"_GameTime",get_GameTime,null,false);
		addMember(l,"UnscaledGameTime",getUnscaledGameTime,null,false);
		addMember(l,"_UnscaledGameTime",get_UnscaledGameTime,null,false);
		addMember(l,"Manual",getManual,null,false);
		addMember(l,"_Manual",get_Manual,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Playables.DirectorUpdateMode));
	}
}
