using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RigidbodyType2D : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDynamic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyType2D.Dynamic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Dynamic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyType2D.Dynamic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getKinematic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyType2D.Kinematic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Kinematic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyType2D.Kinematic);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStatic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RigidbodyType2D.Static);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Static(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RigidbodyType2D.Static);
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
		getTypeTable(l,"UnityEngine.RigidbodyType2D");
		addMember(l,"Dynamic",getDynamic,null,false);
		addMember(l,"_Dynamic",get_Dynamic,null,false);
		addMember(l,"Kinematic",getKinematic,null,false);
		addMember(l,"_Kinematic",get_Kinematic,null,false);
		addMember(l,"Static",getStatic,null,false);
		addMember(l,"_Static",get_Static,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RigidbodyType2D));
	}
}
