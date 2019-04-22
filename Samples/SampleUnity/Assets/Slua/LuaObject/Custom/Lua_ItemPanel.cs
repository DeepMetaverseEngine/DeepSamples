using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ItemPanel : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			ItemPanel o;
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			o=new ItemPanel(a1);
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
	static public int AddLogicNode(IntPtr l) {
		try {
			ItemPanel self=(ItemPanel)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.UGUI.DisplayNode a2;
			checkType(l,3,out a2);
			self.AddLogicNode(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddNode(IntPtr l) {
		try {
			ItemPanel self=(ItemPanel)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			self.AddNode(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Init(IntPtr l) {
		try {
			ItemPanel self=(ItemPanel)checkSelf(l);
			IItemShowAccesser a1;
			checkType(l,2,out a1);
			self.Init(a1);
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
	static public int get_mShowMap(IntPtr l) {
		try {
			ItemPanel self=(ItemPanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mShowMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mShowMap(IntPtr l) {
		try {
			ItemPanel self=(ItemPanel)checkSelf(l);
			DeepCore.HashMap<System.Int32,DeepCore.Unity3D.UGUI.DisplayNode> v;
			checkType(l,2,out v);
			self.mShowMap=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ItemPanel");
		addMember(l,AddLogicNode);
		addMember(l,AddNode);
		addMember(l,Init);
		addMember(l,"mShowMap",get_mShowMap,set_mShowMap,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(ItemPanel),typeof(ItemContainer));
	}
}
