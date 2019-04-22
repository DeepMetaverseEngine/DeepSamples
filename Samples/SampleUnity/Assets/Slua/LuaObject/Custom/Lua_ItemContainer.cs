using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ItemContainer : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Select(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(ItemShow))){
				ItemContainer self=(ItemContainer)checkSelf(l);
				ItemShow a1;
				checkType(l,2,out a1);
				var ret=self.Select(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(int))){
				ItemContainer self=(ItemContainer)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				var ret=self.Select(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Select to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SelectFirst(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			var ret=self.SelectFirst();
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
	static public int CleanSelect(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			self.CleanSelect();
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
	static public int set_OnItemSingleSelect(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			ItemSelectHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnItemSingleSelect=v;
			else if(op==1) self.OnItemSingleSelect+=v;
			else if(op==2) self.OnItemSingleSelect-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnItemMultiSelect(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			ItemSelectHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnItemMultiSelect=v;
			else if(op==1) self.OnItemMultiSelect+=v;
			else if(op==2) self.OnItemMultiSelect-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnItemClick(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.TouchClickHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnItemClick=v;
			else if(op==1) self.OnItemClick+=v;
			else if(op==2) self.OnItemClick-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnItemInit(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			ItemInitHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnItemInit=v;
			else if(op==1) self.OnItemInit+=v;
			else if(op==2) self.OnItemInit-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ShowBackground(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ShowBackground);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ShowBackground(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.ShowBackground=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnableSelect(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EnableSelect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EnableSelect(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.EnableSelect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnableMultiSelect(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EnableMultiSelect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EnableMultiSelect(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.EnableMultiSelect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnableEmptySelect(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EnableEmptySelect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EnableEmptySelect(IntPtr l) {
		try {
			ItemContainer self=(ItemContainer)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.EnableEmptySelect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ItemContainer");
		addMember(l,Select);
		addMember(l,SelectFirst);
		addMember(l,CleanSelect);
		addMember(l,"OnItemSingleSelect",null,set_OnItemSingleSelect,true);
		addMember(l,"OnItemMultiSelect",null,set_OnItemMultiSelect,true);
		addMember(l,"OnItemClick",null,set_OnItemClick,true);
		addMember(l,"OnItemInit",null,set_OnItemInit,true);
		addMember(l,"ShowBackground",get_ShowBackground,set_ShowBackground,true);
		addMember(l,"EnableSelect",get_EnableSelect,set_EnableSelect,true);
		addMember(l,"EnableMultiSelect",get_EnableMultiSelect,set_EnableMultiSelect,true);
		addMember(l,"EnableEmptySelect",get_EnableEmptySelect,set_EnableEmptySelect,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(ItemContainer),typeof(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas));
	}
}
