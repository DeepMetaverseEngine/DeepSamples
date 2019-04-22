using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RigidbodyConstraints : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints.None);
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
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.None);
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
			pushValue(l,UnityEngine.RigidbodyConstraints.FreezePositionX);
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
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.FreezePositionX);
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
			pushValue(l,UnityEngine.RigidbodyConstraints.FreezePositionY);
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
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.FreezePositionY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFreezePositionZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints.FreezePositionZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FreezePositionZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.FreezePositionZ);
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
			pushValue(l,UnityEngine.RigidbodyConstraints.FreezePosition);
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
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.FreezePosition);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFreezeRotationX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints.FreezeRotationX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FreezeRotationX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.FreezeRotationX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFreezeRotationY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints.FreezeRotationY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FreezeRotationY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.FreezeRotationY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFreezeRotationZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyConstraints.FreezeRotationZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FreezeRotationZ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.FreezeRotationZ);
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
			pushValue(l,UnityEngine.RigidbodyConstraints.FreezeRotation);
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
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.FreezeRotation);
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
			pushValue(l,UnityEngine.RigidbodyConstraints.FreezeAll);
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
			pushValue(l,(double)UnityEngine.RigidbodyConstraints.FreezeAll);
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
		getTypeTable(l,"UnityEngine.RigidbodyConstraints");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"FreezePositionX",getFreezePositionX,null,false);
		addMember(l,"_FreezePositionX",get_FreezePositionX,null,false);
		addMember(l,"FreezePositionY",getFreezePositionY,null,false);
		addMember(l,"_FreezePositionY",get_FreezePositionY,null,false);
		addMember(l,"FreezePositionZ",getFreezePositionZ,null,false);
		addMember(l,"_FreezePositionZ",get_FreezePositionZ,null,false);
		addMember(l,"FreezePosition",getFreezePosition,null,false);
		addMember(l,"_FreezePosition",get_FreezePosition,null,false);
		addMember(l,"FreezeRotationX",getFreezeRotationX,null,false);
		addMember(l,"_FreezeRotationX",get_FreezeRotationX,null,false);
		addMember(l,"FreezeRotationY",getFreezeRotationY,null,false);
		addMember(l,"_FreezeRotationY",get_FreezeRotationY,null,false);
		addMember(l,"FreezeRotationZ",getFreezeRotationZ,null,false);
		addMember(l,"_FreezeRotationZ",get_FreezeRotationZ,null,false);
		addMember(l,"FreezeRotation",getFreezeRotation,null,false);
		addMember(l,"_FreezeRotation",get_FreezeRotation,null,false);
		addMember(l,"FreezeAll",getFreezeAll,null,false);
		addMember(l,"_FreezeAll",get_FreezeAll,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RigidbodyConstraints));
	}
}
