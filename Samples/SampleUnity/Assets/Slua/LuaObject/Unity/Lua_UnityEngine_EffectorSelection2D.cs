using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_EffectorSelection2D : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRigidbody(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EffectorSelection2D.Rigidbody);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rigidbody(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EffectorSelection2D.Rigidbody);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCollider(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EffectorSelection2D.Collider);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Collider(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EffectorSelection2D.Collider);
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
		getTypeTable(l,"UnityEngine.EffectorSelection2D");
		addMember(l,"Rigidbody",getRigidbody,null,false);
		addMember(l,"_Rigidbody",get_Rigidbody,null,false);
		addMember(l,"Collider",getCollider,null,false);
		addMember(l,"_Collider",get_Collider,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.EffectorSelection2D));
	}
}
