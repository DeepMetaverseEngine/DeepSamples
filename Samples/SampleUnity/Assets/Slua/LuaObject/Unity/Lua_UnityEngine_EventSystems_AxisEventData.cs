using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_EventSystems_AxisEventData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.EventSystems.AxisEventData o;
			UnityEngine.EventSystems.EventSystem a1;
			checkType(l,2,out a1);
			o=new UnityEngine.EventSystems.AxisEventData(a1);
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
	static public int get_moveVector(IntPtr l) {
		try {
			UnityEngine.EventSystems.AxisEventData self=(UnityEngine.EventSystems.AxisEventData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.moveVector);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_moveVector(IntPtr l) {
		try {
			UnityEngine.EventSystems.AxisEventData self=(UnityEngine.EventSystems.AxisEventData)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.moveVector=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_moveDir(IntPtr l) {
		try {
			UnityEngine.EventSystems.AxisEventData self=(UnityEngine.EventSystems.AxisEventData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.moveDir);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_moveDir(IntPtr l) {
		try {
			UnityEngine.EventSystems.AxisEventData self=(UnityEngine.EventSystems.AxisEventData)checkSelf(l);
			UnityEngine.EventSystems.MoveDirection v;
			checkEnum(l,2,out v);
			self.moveDir=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.EventSystems.AxisEventData");
		addMember(l,"moveVector",get_moveVector,set_moveVector,true);
		addMember(l,"moveDir",get_moveDir,set_moveDir,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(UnityEngine.EventSystems.AxisEventData),typeof(UnityEngine.EventSystems.BaseEventData));
	}
}
