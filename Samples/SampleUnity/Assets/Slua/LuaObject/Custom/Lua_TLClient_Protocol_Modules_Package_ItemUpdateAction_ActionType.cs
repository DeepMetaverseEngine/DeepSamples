using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Protocol_Modules_Package_ItemUpdateAction_ActionType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInit(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.Init);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Init(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.Init);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAdd(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.Add);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Add(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.Add);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRemove(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.Remove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Remove(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.Remove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUpdateCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.UpdateCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UpdateCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.UpdateCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUpdateAttribute(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.UpdateAttribute);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UpdateAttribute(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.UpdateAttribute);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getChangeSize(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.ChangeSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ChangeSize(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType.ChangeSize);
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
		getTypeTable(l,"ItemUpdateAction.ActionType");
		addMember(l,"Init",getInit,null,false);
		addMember(l,"_Init",get_Init,null,false);
		addMember(l,"Add",getAdd,null,false);
		addMember(l,"_Add",get_Add,null,false);
		addMember(l,"Remove",getRemove,null,false);
		addMember(l,"_Remove",get_Remove,null,false);
		addMember(l,"UpdateCount",getUpdateCount,null,false);
		addMember(l,"_UpdateCount",get_UpdateCount,null,false);
		addMember(l,"UpdateAttribute",getUpdateAttribute,null,false);
		addMember(l,"_UpdateAttribute",get_UpdateAttribute,null,false);
		addMember(l,"ChangeSize",getChangeSize,null,false);
		addMember(l,"_ChangeSize",get_ChangeSize,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType));
	}
}
