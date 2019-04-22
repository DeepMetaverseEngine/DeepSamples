using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_EventSystems_PointerEventData_FramePressState : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPressed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.PointerEventData.FramePressState.Pressed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Pressed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.PointerEventData.FramePressState.Pressed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getReleased(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.PointerEventData.FramePressState.Released);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Released(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.PointerEventData.FramePressState.Released);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPressedAndReleased(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.PointerEventData.FramePressState.PressedAndReleased);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PressedAndReleased(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.PointerEventData.FramePressState.PressedAndReleased);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNotChanged(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.PointerEventData.FramePressState.NotChanged);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NotChanged(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.PointerEventData.FramePressState.NotChanged);
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
		getTypeTable(l,"UnityEngine.EventSystems.PointerEventData.FramePressState");
		addMember(l,"Pressed",getPressed,null,false);
		addMember(l,"_Pressed",get_Pressed,null,false);
		addMember(l,"Released",getReleased,null,false);
		addMember(l,"_Released",get_Released,null,false);
		addMember(l,"PressedAndReleased",getPressedAndReleased,null,false);
		addMember(l,"_PressedAndReleased",get_PressedAndReleased,null,false);
		addMember(l,"NotChanged",getNotChanged,null,false);
		addMember(l,"_NotChanged",get_NotChanged,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.EventSystems.PointerEventData.FramePressState));
	}
}
