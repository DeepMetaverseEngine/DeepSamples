using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_ScrollRect_MovementType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUnrestricted(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.ScrollRect.MovementType.Unrestricted);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Unrestricted(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.ScrollRect.MovementType.Unrestricted);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getElastic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.ScrollRect.MovementType.Elastic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Elastic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.ScrollRect.MovementType.Elastic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getClamped(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.ScrollRect.MovementType.Clamped);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Clamped(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.ScrollRect.MovementType.Clamped);
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
		getTypeTable(l,"UnityEngine.UI.ScrollRect.MovementType");
		addMember(l,"Unrestricted",getUnrestricted,null,false);
		addMember(l,"_Unrestricted",get_Unrestricted,null,false);
		addMember(l,"Elastic",getElastic,null,false);
		addMember(l,"_Elastic",get_Elastic,null,false);
		addMember(l,"Clamped",getClamped,null,false);
		addMember(l,"_Clamped",get_Clamped,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.ScrollRect.MovementType));
	}
}
