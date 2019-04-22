using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_Vector3Move : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int setRect(IntPtr l) {
		try {
			Vector3Move self=(Vector3Move)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.setRect(a1,a2);
			pushValue(l,true);
			return 1;
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
	static public int get_MoveSpeed(IntPtr l) {
		try {
			Vector3Move self=(Vector3Move)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MoveSpeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MoveSpeed(IntPtr l) {
		try {
			Vector3Move self=(Vector3Move)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.MoveSpeed=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_targets(IntPtr l) {
		try {
			Vector3Move self=(Vector3Move)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.targets);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_targets(IntPtr l) {
		try {
			Vector3Move self=(Vector3Move)checkSelf(l);
			UnityEngine.Vector3[] v;
			checkArray(l,2,out v);
			self.targets=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"Vector3Move");
		addMember(l,setRect);
		addMember(l,"MoveSpeed",get_MoveSpeed,set_MoveSpeed,true);
		addMember(l,"targets",get_targets,set_targets,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(Vector3Move),typeof(UnityEngine.MonoBehaviour));
	}
}
