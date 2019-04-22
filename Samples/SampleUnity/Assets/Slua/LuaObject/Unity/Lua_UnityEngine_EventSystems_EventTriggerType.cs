using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_EventSystems_EventTriggerType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPointerEnter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.PointerEnter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PointerEnter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.PointerEnter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPointerExit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.PointerExit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PointerExit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.PointerExit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPointerDown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.PointerDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PointerDown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.PointerDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPointerUp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.PointerUp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PointerUp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.PointerUp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPointerClick(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.PointerClick);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PointerClick(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.PointerClick);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDrag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.Drag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Drag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.Drag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDrop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.Drop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Drop(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.Drop);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getScroll(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.Scroll);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Scroll(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.Scroll);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUpdateSelected(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.UpdateSelected);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UpdateSelected(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.UpdateSelected);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSelect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.Select);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Select(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.Select);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDeselect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.Deselect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Deselect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.Deselect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMove(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.Move);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Move(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.Move);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInitializePotentialDrag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.InitializePotentialDrag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InitializePotentialDrag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.InitializePotentialDrag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBeginDrag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.BeginDrag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BeginDrag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.BeginDrag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEndDrag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.EndDrag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EndDrag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.EndDrag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSubmit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.Submit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Submit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.Submit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCancel(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.EventSystems.EventTriggerType.Cancel);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Cancel(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.EventSystems.EventTriggerType.Cancel);
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
		getTypeTable(l,"UnityEngine.EventSystems.EventTriggerType");
		addMember(l,"PointerEnter",getPointerEnter,null,false);
		addMember(l,"_PointerEnter",get_PointerEnter,null,false);
		addMember(l,"PointerExit",getPointerExit,null,false);
		addMember(l,"_PointerExit",get_PointerExit,null,false);
		addMember(l,"PointerDown",getPointerDown,null,false);
		addMember(l,"_PointerDown",get_PointerDown,null,false);
		addMember(l,"PointerUp",getPointerUp,null,false);
		addMember(l,"_PointerUp",get_PointerUp,null,false);
		addMember(l,"PointerClick",getPointerClick,null,false);
		addMember(l,"_PointerClick",get_PointerClick,null,false);
		addMember(l,"Drag",getDrag,null,false);
		addMember(l,"_Drag",get_Drag,null,false);
		addMember(l,"Drop",getDrop,null,false);
		addMember(l,"_Drop",get_Drop,null,false);
		addMember(l,"Scroll",getScroll,null,false);
		addMember(l,"_Scroll",get_Scroll,null,false);
		addMember(l,"UpdateSelected",getUpdateSelected,null,false);
		addMember(l,"_UpdateSelected",get_UpdateSelected,null,false);
		addMember(l,"Select",getSelect,null,false);
		addMember(l,"_Select",get_Select,null,false);
		addMember(l,"Deselect",getDeselect,null,false);
		addMember(l,"_Deselect",get_Deselect,null,false);
		addMember(l,"Move",getMove,null,false);
		addMember(l,"_Move",get_Move,null,false);
		addMember(l,"InitializePotentialDrag",getInitializePotentialDrag,null,false);
		addMember(l,"_InitializePotentialDrag",get_InitializePotentialDrag,null,false);
		addMember(l,"BeginDrag",getBeginDrag,null,false);
		addMember(l,"_BeginDrag",get_BeginDrag,null,false);
		addMember(l,"EndDrag",getEndDrag,null,false);
		addMember(l,"_EndDrag",get_EndDrag,null,false);
		addMember(l,"Submit",getSubmit,null,false);
		addMember(l,"_Submit",get_Submit,null,false);
		addMember(l,"Cancel",getCancel,null,false);
		addMember(l,"_Cancel",get_Cancel,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.EventSystems.EventTriggerType));
	}
}
