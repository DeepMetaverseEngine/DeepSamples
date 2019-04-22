using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ClientPackageListener : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Match(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
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
	static public int OnUpdatePackageAction(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
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
	static public int Start(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Start(a1,a2);
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
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
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
	static public int GetSourceIndex(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetSourceIndex(a1);
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
	static public int GetLogicIndex(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetLogicIndex(a1);
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
	static public int FindItemAs(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindItemAs(a1);
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
	static public int FindFirstItemAs(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindFirstItemAs(a1);
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
	static public int FindSlotAs(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindSlotAs(a1);
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
	static public int FindFirstSlotAs(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.ItemData> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindFirstSlotAs(a1);
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
	static public int get_Package(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Package);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AllSlots(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AllSlots);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AllItems(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AllItems);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ItemCount(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ItemCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EmptySoltCount(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EmptySoltCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KeepOrder(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.KeepOrder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoResize(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AutoResize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MergerOutOfMaxStack(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MergerOutOfMaxStack);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Size(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Size);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsRunning(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsRunning);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getItem(IntPtr l) {
		try {
			ClientPackageListener self=(ClientPackageListener)checkSelf(l);
			int v;
			checkType(l,2,out v);
			var ret = self[v];
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"PackageListener");
		addMember(l,Match);
		addMember(l,OnUpdatePackageAction);
		addMember(l,Start);
		addMember(l,Stop);
		addMember(l,GetSourceIndex);
		addMember(l,GetLogicIndex);
		addMember(l,FindItemAs);
		addMember(l,FindFirstItemAs);
		addMember(l,FindSlotAs);
		addMember(l,FindFirstSlotAs);
		addMember(l,getItem);
		addMember(l,"Package",get_Package,null,true);
		addMember(l,"AllSlots",get_AllSlots,null,true);
		addMember(l,"AllItems",get_AllItems,null,true);
		addMember(l,"ItemCount",get_ItemCount,null,true);
		addMember(l,"EmptySoltCount",get_EmptySoltCount,null,true);
		addMember(l,"KeepOrder",get_KeepOrder,null,true);
		addMember(l,"AutoResize",get_AutoResize,null,true);
		addMember(l,"MergerOutOfMaxStack",get_MergerOutOfMaxStack,null,true);
		addMember(l,"Size",get_Size,null,true);
		addMember(l,"IsRunning",get_IsRunning,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(ClientPackageListener),typeof(DeepCore.Disposable));
	}
}
