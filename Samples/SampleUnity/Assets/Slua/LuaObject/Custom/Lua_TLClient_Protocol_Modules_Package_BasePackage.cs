using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Protocol_Modules_Package_BasePackage : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetSlot(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetSlot(a1);
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
	static public int ForeachSlots(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Action<TLClient.Protocol.Modules.Package.PackageSlot> a1;
			checkDelegate(l,2,out a1);
			self.ForeachSlots(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DeepCloneAllSlots(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			var ret=self.DeepCloneAllSlots();
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
	static public int AddListener(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			TLClient.Protocol.Modules.Package.IPackageListener a1;
			checkType(l,2,out a1);
			self.AddListener(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveListener(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			TLClient.Protocol.Modules.Package.IPackageListener a1;
			checkType(l,2,out a1);
			self.RemoveListener(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int BeginBatchListen(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			self.BeginBatchListen();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int EndBatchListen(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			self.EndBatchListen();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateUpdateReason(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.CreateUpdateReason(a1);
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
	static public int CreateRecoverSnap(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.CreateRecoverSnap(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveRecoverSnap(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.RemoveRecoverSnap(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ApplyRecoverSnap(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.ApplyRecoverSnap(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ApplySlotDiff(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(TLClient.Protocol.Modules.Package.PackageSlotDiff[]))){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				TLClient.Protocol.Modules.Package.PackageSlotDiff[] a1;
				checkArray(l,2,out a1);
				self.ApplySlotDiff(a1);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(TLClient.Protocol.Modules.Package.PackageSlotDiff))){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				TLClient.Protocol.Modules.Package.PackageSlotDiff a1;
				checkType(l,2,out a1);
				self.ApplySlotDiff(a1);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function ApplySlotDiff to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DiffWithSlots(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			TLClient.Protocol.Modules.Package.PackageSlot[] a1;
			checkArray(l,2,out a1);
			var ret=self.DiffWithSlots(a1);
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
	static public int OnSelectSlot(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			TLClient.Protocol.Modules.Package.IPackageItem a1;
			checkType(l,2,out a1);
			var ret=self.OnSelectSlot(a1);
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
	static public int OnItemAttributeUpdated(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.OnItemAttributeUpdated(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FindFirstTemplateItemIndex(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.FindFirstTemplateItemIndex(a1);
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
	static public int CanPackUp(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			var ret=self.CanPackUp();
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
	static public int PackUp(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Collections.Generic.IComparer<TLClient.Protocol.Modules.Package.IPackageItem> a1;
			checkType(l,2,out a1);
			var ret=self.PackUp(a1);
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
	static public int GetItemAt(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetItemAt<TLClient.Protocol.Modules.Package.IPackageItem>(a1);
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
	static public int NextEmptySlot(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.NextEmptySlot(a1);
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
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(System.Predicate<TLClient.Protocol.Modules.Package.PackageSlot>))){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Predicate<TLClient.Protocol.Modules.Package.PackageSlot> a1;
				checkDelegate(l,2,out a1);
				var ret=self.FindSlotAs(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(System.Predicate<TLClient.Protocol.Modules.Package.IPackageItem>))){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Predicate<TLClient.Protocol.Modules.Package.IPackageItem> a1;
				checkDelegate(l,2,out a1);
				var ret=self.FindSlotAs<TLClient.Protocol.Modules.Package.IPackageItem>(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function FindSlotAs to call");
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
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.Package.IPackageItem> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindFirstSlotAs<TLClient.Protocol.Modules.Package.IPackageItem>(a1);
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
	static public int CountItemAs(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.Package.IPackageItem> a1;
			checkDelegate(l,2,out a1);
			var ret=self.CountItemAs<TLClient.Protocol.Modules.Package.IPackageItem>(a1);
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
	static public int CountSlotAs(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.Package.PackageSlot> a1;
			checkDelegate(l,2,out a1);
			var ret=self.CountSlotAs(a1);
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
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.Package.IPackageItem> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindItemAs<TLClient.Protocol.Modules.Package.IPackageItem>(a1);
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
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Predicate<TLClient.Protocol.Modules.Package.IPackageItem> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindFirstItemAs<TLClient.Protocol.Modules.Package.IPackageItem>(a1);
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
	static public int Enough(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Collections.Generic.ICollection<TLClient.Protocol.Modules.Package.CostCondition> a1;
				checkType(l,2,out a1);
				var ret=self.Enough(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				System.UInt64 a2;
				checkType(l,3,out a2);
				var ret=self.Enough(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Enough to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Count(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.Count(a1);
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
	static public int InitSlots(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			TLClient.Protocol.Modules.Package.PackageSlot[] a1;
			checkValueParams(l,2,out a1);
			var ret=self.InitSlots(a1);
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
	static public int Cleanup(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			self.Cleanup();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Cost(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Collections.Generic.ICollection<TLClient.Protocol.Modules.Package.CostCondition> a1;
				checkType(l,2,out a1);
				var ret=self.Cost(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				System.UInt32 a2;
				checkType(l,3,out a2);
				var ret=self.Cost(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Cost to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int TestAddItem(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Collections.Generic.ICollection<TLClient.Protocol.Modules.Package.IPackageItem> a1;
			checkType(l,2,out a1);
			var ret=self.TestAddItem(a1);
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
	static public int AddItem(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(TLClient.Protocol.Modules.Package.IPackageItem))){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				TLClient.Protocol.Modules.Package.IPackageItem a1;
				checkType(l,2,out a1);
				var ret=self.AddItem(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(ICollection<TLClient.Protocol.Modules.Package.IPackageItem>))){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Collections.Generic.ICollection<TLClient.Protocol.Modules.Package.IPackageItem> a1;
				checkType(l,2,out a1);
				var ret=self.AddItem(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				TLClient.Protocol.Modules.Package.IPackageItem a2;
				checkType(l,3,out a2);
				var ret=self.AddItem(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AddItem to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Product(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Collections.Generic.ICollection<TLClient.Protocol.Modules.Package.CostCondition> a1;
				checkType(l,2,out a1);
				System.Collections.Generic.ICollection<TLClient.Protocol.Modules.Package.IPackageItem> a2;
				checkType(l,3,out a2);
				var ret=self.Product(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==7){
				TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
				System.Collections.Generic.ICollection<TLClient.Protocol.Modules.Package.CostCondition> a1;
				checkType(l,2,out a1);
				System.Collections.Generic.ICollection<TLClient.Protocol.Modules.Package.IPackageItem> a2;
				checkType(l,3,out a2);
				List<TLClient.Protocol.Modules.Package.IPackageItem> a3;
				checkType(l,4,out a3);
				List<TLClient.Protocol.Modules.Package.IPackageItem> a4;
				checkType(l,5,out a4);
				List<TLClient.Protocol.Modules.Package.IPackageItem> a5;
				checkType(l,6,out a5);
				List<TLClient.Protocol.Modules.Package.IPackageItem> a6;
				checkType(l,7,out a6);
				var ret=self.Product(a1,a2,out a3,out a4,out a5,out a6);
				pushValue(l,true);
				pushValue(l,ret);
				pushValue(l,a3);
				pushValue(l,a4);
				pushValue(l,a5);
				pushValue(l,a6);
				return 6;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Product to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PushOutLogicHandler(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			TLClient.Protocol.Modules.Package.OutSizeLogicHandler a1;
			checkDelegate(l,2,out a1);
			self.PushOutLogicHandler(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PopOutLogicHandler(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			self.PopOutLogicHandler();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SupportOutSize(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			TLClient.Protocol.Modules.Package.ActualProduct a1;
			checkType(l,2,out a1);
			var ret=self.SupportOutSize(a1);
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
	static public int RemoveItem(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.RemoveItem(a1);
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
	static public int UpdateItemCount(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			var ret=self.UpdateItemCount(a1,a2);
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
	static public int IncrementCount(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			var ret=self.IncrementCount(a1,a2);
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
	static public int DecrementCount(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.UInt32 a2;
			checkType(l,3,out a2);
			var ret=self.DecrementCount(a1,a2);
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
	static public int get_MaxSize(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MaxSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MaxSize(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.MaxSize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoResize(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
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
	static public int get_Size(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
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
	static public int set_Size(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.Size=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurrentActionReason(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentActionReason);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AllTemplateIndexMap(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AllTemplateIndexMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MergerableList(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MergerableList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PackUpCoolDownSec(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PackUpCoolDownSec);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_PackUpCoolDownSec(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.PackUpCoolDownSec=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PackUpPassTimeSec(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PackUpPassTimeSec);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EmptySlotIndexs(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EmptySlotIndexs);
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
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
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
	static public int get_EmptySlotCount(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EmptySlotCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsFull(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsFull);
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
			TLClient.Protocol.Modules.Package.BasePackage self=(TLClient.Protocol.Modules.Package.BasePackage)checkSelf(l);
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
		getTypeTable(l,"TLClient.Protocol.Modules.Package.BasePackage");
		addMember(l,GetSlot);
		addMember(l,ForeachSlots);
		addMember(l,DeepCloneAllSlots);
		addMember(l,AddListener);
		addMember(l,RemoveListener);
		addMember(l,BeginBatchListen);
		addMember(l,EndBatchListen);
		addMember(l,CreateUpdateReason);
		addMember(l,CreateRecoverSnap);
		addMember(l,RemoveRecoverSnap);
		addMember(l,ApplyRecoverSnap);
		addMember(l,ApplySlotDiff);
		addMember(l,DiffWithSlots);
		addMember(l,OnSelectSlot);
		addMember(l,OnItemAttributeUpdated);
		addMember(l,FindFirstTemplateItemIndex);
		addMember(l,CanPackUp);
		addMember(l,PackUp);
		addMember(l,GetItemAt);
		addMember(l,NextEmptySlot);
		addMember(l,FindSlotAs);
		addMember(l,FindFirstSlotAs);
		addMember(l,CountItemAs);
		addMember(l,CountSlotAs);
		addMember(l,FindItemAs);
		addMember(l,FindFirstItemAs);
		addMember(l,Enough);
		addMember(l,Count);
		addMember(l,InitSlots);
		addMember(l,Cleanup);
		addMember(l,Cost);
		addMember(l,TestAddItem);
		addMember(l,AddItem);
		addMember(l,Product);
		addMember(l,PushOutLogicHandler);
		addMember(l,PopOutLogicHandler);
		addMember(l,SupportOutSize);
		addMember(l,RemoveItem);
		addMember(l,UpdateItemCount);
		addMember(l,IncrementCount);
		addMember(l,DecrementCount);
		addMember(l,getItem);
		addMember(l,"MaxSize",get_MaxSize,set_MaxSize,true);
		addMember(l,"AutoResize",get_AutoResize,null,true);
		addMember(l,"Size",get_Size,set_Size,true);
		addMember(l,"CurrentActionReason",get_CurrentActionReason,null,true);
		addMember(l,"AllTemplateIndexMap",get_AllTemplateIndexMap,null,true);
		addMember(l,"MergerableList",get_MergerableList,null,true);
		addMember(l,"PackUpCoolDownSec",get_PackUpCoolDownSec,set_PackUpCoolDownSec,true);
		addMember(l,"PackUpPassTimeSec",get_PackUpPassTimeSec,null,true);
		addMember(l,"EmptySlotIndexs",get_EmptySlotIndexs,null,true);
		addMember(l,"AllSlots",get_AllSlots,null,true);
		addMember(l,"EmptySlotCount",get_EmptySlotCount,null,true);
		addMember(l,"IsFull",get_IsFull,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLClient.Protocol.Modules.Package.BasePackage),typeof(DeepCore.Disposable));
	}
}
