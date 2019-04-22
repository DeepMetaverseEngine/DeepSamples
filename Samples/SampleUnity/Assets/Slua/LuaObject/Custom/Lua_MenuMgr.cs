using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_MenuMgr : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			MenuMgr o;
			o=new MenuMgr();
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
	static public int PrintRefCount(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			self.PrintRefCount();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddHudMenu(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			MenuBase a1;
			checkType(l,2,out a1);
			self.AddHudMenu(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddMsgBox(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			MenuBase a1;
			checkType(l,2,out a1);
			self.AddMsgBox(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddMenu(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				MenuMgr self=(MenuMgr)checkSelf(l);
				MenuBase a1;
				checkType(l,2,out a1);
				self.AddMenu(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				MenuMgr self=(MenuMgr)checkSelf(l);
				MenuBase a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				self.AddMenu(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AddMenu to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CheckNotifyMenuList(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			self.CheckNotifyMenuList();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnSubMenuAdd(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			MenuBase a1;
			checkType(l,2,out a1);
			self.OnSubMenuAdd(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnSubMenuRemove(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			MenuBase a1;
			checkType(l,2,out a1);
			self.OnSubMenuRemove(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideMenu(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.HideMenu(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseMenuByTag(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.CloseMenuByTag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseMenuGtLifeIndex(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.CloseMenuGtLifeIndex(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseAllMenu(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			self.CloseAllMenu();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseAllHideMenu(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			self.CloseAllHideMenu();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseAllMsgBox(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			self.CloseAllMsgBox();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseAllHudMenu(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			self.CloseAllHudMenu();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetTopMenu(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			var ret=self.GetTopMenu();
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
	static public int GetTopMsgBox(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			var ret=self.GetTopMsgBox();
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
	static public int FindMenuByTag(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.FindMenuByTag(a1);
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
	static public int FindMenuByXml(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.FindMenuByXml(a1);
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
	static public int FindMenusByTag(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.FindMenusByTag(a1);
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
	static public int GetCurrentMenu(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			var ret=self.GetCurrentMenu();
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
	static public int CloseCurrentMenu(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			self.CloseCurrentMenu();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OpenUIByTag(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				MenuMgr self=(MenuMgr)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				self.OpenUIByTag(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				MenuMgr self=(MenuMgr)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				System.Object[] a3;
				checkArray(l,4,out a3);
				self.OpenUIByTag(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function OpenUIByTag to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateUIByTag(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				MenuMgr self=(MenuMgr)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				var ret=self.CreateUIByTag(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==4){
				MenuMgr self=(MenuMgr)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				var ret=self.CreateUIByTag(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateUIByTag to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UICacheInit(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			SLua.LuaTable a1;
			checkType(l,2,out a1);
			self.UICacheInit(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateCacheUI(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.CreateCacheUI(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddCacheUI(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			MenuBase a2;
			checkType(l,3,out a2);
			var ret=self.AddCacheUI(a1,a2);
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
	static public int GetCacheUIByXml(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetCacheUIByXml(a1);
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
	static public int GetCacheUIByTag(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetCacheUIByTag(a1);
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
	static public int RemoveCacheUIByTag(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.RemoveCacheUIByTag(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ClearAllCacheUI(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.ClearAllCacheUI(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PushWaitUI(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			MenuBase a1;
			checkType(l,2,out a1);
			var ret=self.PushWaitUI(a1);
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
	static public int PopWaitUI(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.PopWaitUI(a1);
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
	static public int ClearWaitUI(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			self.ClearWaitUI();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PreLoadUI(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.PreLoadUI(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideScene(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.HideScene(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideHud(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.HideHud(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clear(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Clear(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AttachObserver(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				MenuMgr self=(MenuMgr)checkSelf(l);
				MenuObserver a1;
				checkType(l,2,out a1);
				self.AttachObserver(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				MenuMgr self=(MenuMgr)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				MenuObserver a2;
				checkType(l,3,out a2);
				self.AttachObserver(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AttachObserver to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachObserver(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				MenuMgr self=(MenuMgr)checkSelf(l);
				MenuObserver a1;
				checkType(l,2,out a1);
				self.DetachObserver(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				MenuMgr self=(MenuMgr)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				MenuObserver a2;
				checkType(l,3,out a2);
				self.DetachObserver(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function DetachObserver to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FindGlobalObserverAs(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.Predicate<MenuObserver> a1;
			checkDelegate(l,2,out a1);
			var ret=self.FindGlobalObserverAs(a1);
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
	static public int AttachLuaObserver(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			SLua.LuaTable a3;
			checkType(l,4,out a3);
			self.AttachLuaObserver(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachLuaObserver(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.DetachLuaObserver(a1,a2);
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
	static public int get_LastAddMenu(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastAddMenu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OnMenuListChange(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.OnMenuListChange);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnMenuListChange(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			SLua.LuaFunction v;
			checkType(l,2,out v);
			self.OnMenuListChange=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,MenuMgr.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NeedHideScene(IntPtr l) {
		try {
			MenuMgr self=(MenuMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.NeedHideScene);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"MenuMgr");
		addMember(l,PrintRefCount);
		addMember(l,AddHudMenu);
		addMember(l,AddMsgBox);
		addMember(l,AddMenu);
		addMember(l,CheckNotifyMenuList);
		addMember(l,OnSubMenuAdd);
		addMember(l,OnSubMenuRemove);
		addMember(l,HideMenu);
		addMember(l,CloseMenuByTag);
		addMember(l,CloseMenuGtLifeIndex);
		addMember(l,CloseAllMenu);
		addMember(l,CloseAllHideMenu);
		addMember(l,CloseAllMsgBox);
		addMember(l,CloseAllHudMenu);
		addMember(l,GetTopMenu);
		addMember(l,GetTopMsgBox);
		addMember(l,FindMenuByTag);
		addMember(l,FindMenuByXml);
		addMember(l,FindMenusByTag);
		addMember(l,GetCurrentMenu);
		addMember(l,CloseCurrentMenu);
		addMember(l,OpenUIByTag);
		addMember(l,CreateUIByTag);
		addMember(l,UICacheInit);
		addMember(l,CreateCacheUI);
		addMember(l,AddCacheUI);
		addMember(l,GetCacheUIByXml);
		addMember(l,GetCacheUIByTag);
		addMember(l,RemoveCacheUIByTag);
		addMember(l,ClearAllCacheUI);
		addMember(l,PushWaitUI);
		addMember(l,PopWaitUI);
		addMember(l,ClearWaitUI);
		addMember(l,PreLoadUI);
		addMember(l,HideScene);
		addMember(l,HideHud);
		addMember(l,Clear);
		addMember(l,AttachObserver);
		addMember(l,DetachObserver);
		addMember(l,FindGlobalObserverAs);
		addMember(l,AttachLuaObserver);
		addMember(l,DetachLuaObserver);
		addMember(l,"LastAddMenu",get_LastAddMenu,null,true);
		addMember(l,"OnMenuListChange",get_OnMenuListChange,set_OnMenuListChange,true);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"NeedHideScene",get_NeedHideScene,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(MenuMgr));
	}
}
