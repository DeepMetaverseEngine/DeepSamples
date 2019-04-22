using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RigidbodyConstraints2D : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints2D.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints2D.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFreezePositionX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints2D.FreezePositionX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FreezePositionX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints2D.FreezePositionX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFreezePositionY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints2D.FreezePositionY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FreezePositionY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints2D.FreezePositionY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFreezePosition(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints2D.FreezePosition);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FreezePosition(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints2D.FreezePosition);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFreezeRotation(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints2D.FreezeRotation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FreezeRotation(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints2D.FreezeRotation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFreezeAll(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints2D.FreezeAll);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FreezeAll(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints2D.FreezeAll);
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
		getTypeTable(l,"UnityEngine.RigidbodyConstraints2D");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"FreezePositionX",getFreezePositionX,null,false);
		addMember(l,"_FreezePositionX",get_FreezePositionX,null,false);
		addMember(l,"FreezePositionY",getFreezePositionY,null,false);
		addMember(l,"_FreezePositionY",get_FreezePositionY,null,false);
		addMember(l,"FreezePosition",getFreezePosition,null,false);
		addMember(l,"_FreezePosition",get_FreezePosition,null,false);
		addMember(l,"FreezeRotation",getFreezeRotation,null,false);
		addMember(l,"_FreezeRotation",get_FreezeRotation,null,false);
		addMember(l,"FreezeAll",getFreezeAll,null,false);
		addMember(l,"_FreezeAll",get_FreezeAll,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RigidbodyConstraints2D));
	}
}
