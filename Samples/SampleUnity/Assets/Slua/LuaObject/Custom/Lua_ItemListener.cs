using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ItemListener : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			ItemListener o;
			TLClient.Protocol.Modules.CommonBag a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			o=new ItemListener(a1,a2,a3);
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
	static public int GetItemData(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(int))){
				ItemListener self=(ItemListener)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				var ret=self.GetItemData(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(ItemShow))){
				ItemListener self=(ItemListener)checkSelf(l);
				ItemShow a1;
				checkType(l,2,out a1);
				var ret=self.GetItemData(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function GetItemData to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FindFirstFilled(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			var ret=self.FindFirstFilled();
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
	static public int GetShowAt(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetShowAt(a1);
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
	static public int OnUpdatePackageAction(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			TLClient.Protocol.Modules.Package.BasePackage a1;
			checkType(l,2,out a1);
			System.Collections.Generic.ICollection<TLClient.Protocol.Modules.Package.ItemUpdateAction> a2;
			checkType(l,3,out a2);
			self.OnUpdatePackageAction(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetAndCleanDirty(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			var ret=self.GetAndCleanDirty();
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
	static public int GetAndCleanSelectDirty(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			var ret=self.GetAndCleanSelectDirty();
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
	static public int Match(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			TLClient.Protocol.Modules.ItemData a1;
			checkType(l,2,out a1);
			var ret=self.Match(a1);
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
	static public int Reset(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			self.Reset();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Stop(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.Stop(a1);
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
	static public int set_OnMatch(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnMatch=v;
			else if(op==1) self.OnMatch+=v;
			else if(op==2) self.OnMatch-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnCompare(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			System.Comparison<TLClient.Protocol.Modules.ItemData> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnCompare=v;
			else if(op==1) self.OnCompare+=v;
			else if(op==2) self.OnCompare-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnItemUpdateAction(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			System.Action<TLClient.Protocol.Modules.Package.ItemUpdateAction> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnItemUpdateAction=v;
			else if(op==1) self.OnItemUpdateAction+=v;
			else if(op==2) self.OnItemUpdateAction-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnFilledSizeChange(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			System.Action<System.Int32> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnFilledSizeChange=v;
			else if(op==1) self.OnFilledSizeChange+=v;
			else if(op==2) self.OnFilledSizeChange-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AllSelected(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AllSelected);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FirstSelected(IntPtr l) {
		try {
			ItemListener self=(ItemListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FirstSelected);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ItemListener");
		addMember(l,GetItemData);
		addMember(l,FindFirstFilled);
		addMember(l,GetShowAt);
		addMember(l,OnUpdatePackageAction);
		addMember(l,GetAndCleanDirty);
		addMember(l,GetAndCleanSelectDirty);
		addMember(l,Match);
		addMember(l,Reset);
		addMember(l,Stop);
		addMember(l,"OnMatch",null,set_OnMatch,true);
		addMember(l,"OnCompare",null,set_OnCompare,true);
		addMember(l,"OnItemUpdateAction",null,set_OnItemUpdateAction,true);
		addMember(l,"OnFilledSizeChange",null,set_OnFilledSizeChange,true);
		addMember(l,"AllSelected",get_AllSelected,null,true);
		addMember(l,"FirstSelected",get_FirstSelected,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(ItemListener),typeof(ClientPackageListener));
	}
}
