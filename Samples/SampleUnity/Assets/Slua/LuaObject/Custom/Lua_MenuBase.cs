using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_MenuBase : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			MenuBase o;
			if(matchType(l,argc,2,typeof(int))){
				System.Int32 a1;
				checkType(l,2,out a1);
				o=new MenuBase(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string))){
				System.String a1;
				checkType(l,2,out a1);
				o=new MenuBase(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			return error(l,"New object failed.");
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FindSubMenu(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.FindSubMenu(a1,a2);
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
	static public int AddSubMenu(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			MenuBase a1;
			checkType(l,2,out a1);
			self.AddSubMenu(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddChild(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			self.AddChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddChildAt(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.AddChildAt(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveSubMenu(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			MenuBase a1;
			checkType(l,2,out a1);
			self.RemoveSubMenu(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveAllSubMenu(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			var ret=self.RemoveAllSubMenu();
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
	static public int SetUILayer(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				MenuBase self=(MenuBase)checkSelf(l);
				DeepCore.Unity3D.UGUI.DisplayNode a1;
				checkType(l,2,out a1);
				self.SetUILayer(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				MenuBase self=(MenuBase)checkSelf(l);
				DeepCore.Unity3D.UGUI.DisplayNode a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				self.SetUILayer(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetUILayer to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Set3DModelLayer(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				MenuBase self=(MenuBase)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.GameObject a2;
				checkType(l,3,out a2);
				self.Set3DModelLayer(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				MenuBase self=(MenuBase)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.GameObject a2;
				checkType(l,3,out a2);
				System.Int32 a3;
				checkType(l,4,out a3);
				self.Set3DModelLayer(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Set3DModelLayer to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Close(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			self.Close();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseAndDestroy(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			self.CloseAndDestroy();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Contains(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			MenuBase a1;
			checkType(l,2,out a1);
			var ret=self.Contains(a1);
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
	static public int SetLuaParams(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			SLua.LuaTable a1;
			checkType(l,2,out a1);
			self.SetLuaParams(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideBack(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.HideBack(a1);
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
	static public int SetCompAnime(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.SetCompAnime(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetComponent(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				MenuBase self=(MenuBase)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				var ret=self.GetComponent(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				MenuBase self=(MenuBase)checkSelf(l);
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				var ret=self.GetComponent(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function GetComponent to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FindComponent(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.FindComponent(a1);
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
	static public int SetVisibleUENode(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.SetVisibleUENode(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetEnableUENode(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			self.SetEnableUENode(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetGrayUENode(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.SetGrayUENode(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetLabelText(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.UInt32 a3;
			checkType(l,4,out a3);
			System.UInt32 a4;
			checkType(l,5,out a4);
			self.SetLabelText(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetButtonText(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.UInt32 a3;
			checkType(l,4,out a3);
			System.UInt32 a4;
			checkType(l,5,out a4);
			self.SetButtonText(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetImageBox(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				MenuBase self=(MenuBase)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				DeepCore.Unity3D.UGUIEditor.UILayout a2;
				checkType(l,3,out a2);
				self.SetImageBox(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				MenuBase self=(MenuBase)checkSelf(l);
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				DeepCore.Unity3D.UGUIEditor.UILayout a3;
				checkType(l,4,out a3);
				self.SetImageBox(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				MenuBase self=(MenuBase)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				DeepCore.GUI.Data.UILayoutStyle a3;
				checkEnum(l,4,out a3);
				System.Int32 a4;
				checkType(l,5,out a4);
				self.SetImageBox(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetImageBox to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LocalToUIGlobal(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			var ret=self.LocalToUIGlobal(a1);
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
	static public int LocalToScreenGlobal(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			var ret=self.LocalToScreenGlobal(a1);
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
	static public int SetFullBackground(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UILayout a1;
			checkType(l,2,out a1);
			self.SetFullBackground(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Create_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			var ret=MenuBase.Create(a1,a2);
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
	static public int FindComponentAs_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			MenuBase.ComponentPredicate a2;
			checkDelegate(l,2,out a2);
			System.Boolean a3;
			checkType(l,3,out a3);
			var ret=MenuBase.FindComponentAs(a1,a2,a3);
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
	static public int FindChildComponent_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIComponent a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			var ret=MenuBase.FindChildComponent(a1,a2);
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
	static public int SetVisibleUENode_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.Boolean a2;
				checkType(l,2,out a2);
				MenuBase.SetVisibleUENode(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.Boolean a3;
				checkType(l,3,out a3);
				MenuBase.SetVisibleUENode(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetVisibleUENode to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetEnableUENode_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.Boolean a2;
				checkType(l,2,out a2);
				System.Boolean a3;
				checkType(l,3,out a3);
				MenuBase.SetEnableUENode(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.Boolean a3;
				checkType(l,3,out a3);
				System.Boolean a4;
				checkType(l,4,out a4);
				MenuBase.SetEnableUENode(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetEnableUENode to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetGrayUENode_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.Boolean a2;
				checkType(l,2,out a2);
				MenuBase.SetGrayUENode(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.Boolean a3;
				checkType(l,3,out a3);
				MenuBase.SetGrayUENode(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetGrayUENode to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetLabelText_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(DeepCore.Unity3D.UGUIEditor.UI.HZLabel),typeof(string),typeof(System.UInt32),typeof(System.UInt32))){
				DeepCore.Unity3D.UGUIEditor.UI.HZLabel a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.UInt32 a3;
				checkType(l,3,out a3);
				System.UInt32 a4;
				checkType(l,4,out a4);
				MenuBase.SetLabelText(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.Unity3D.UGUIEditor.UIComponent),typeof(string),typeof(string),typeof(UnityEngine.Color))){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.String a3;
				checkType(l,3,out a3);
				UnityEngine.Color a4;
				checkType(l,4,out a4);
				MenuBase.SetLabelText(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.String a3;
				checkType(l,3,out a3);
				System.UInt32 a4;
				checkType(l,4,out a4);
				System.UInt32 a5;
				checkType(l,5,out a5);
				MenuBase.SetLabelText(a1,a2,a3,a4,a5);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetLabelText to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetButtonText_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==4){
				DeepCore.Unity3D.UGUIEditor.UI.HZTextButton a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.UInt32 a3;
				checkType(l,3,out a3);
				System.UInt32 a4;
				checkType(l,4,out a4);
				MenuBase.SetButtonText(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.String a3;
				checkType(l,3,out a3);
				System.UInt32 a4;
				checkType(l,4,out a4);
				System.UInt32 a5;
				checkType(l,5,out a5);
				MenuBase.SetButtonText(a1,a2,a3,a4,a5);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetButtonText to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetImageBox_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				DeepCore.Unity3D.UGUIEditor.UILayout a2;
				checkType(l,2,out a2);
				MenuBase.SetImageBox(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				DeepCore.GUI.Data.UILayoutStyle a3;
				checkEnum(l,3,out a3);
				System.Int32 a4;
				checkType(l,4,out a4);
				MenuBase.SetImageBox(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				DeepCore.Unity3D.UGUIEditor.UIComponent a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.String a3;
				checkType(l,3,out a3);
				DeepCore.GUI.Data.UILayoutStyle a4;
				checkEnum(l,4,out a4);
				System.Int32 a5;
				checkType(l,5,out a5);
				MenuBase.SetImageBox(a1,a2,a3,a4,a5);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetImageBox to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int InitMultiToggleButton_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(DeepCore.Unity3D.UGUIEditor.UI.TouchClickHandle),typeof(DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton),typeof(DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton[]))){
				DeepCore.Unity3D.UGUIEditor.UI.TouchClickHandle a1;
				checkDelegate(l,1,out a1);
				DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton a2;
				checkType(l,2,out a2);
				DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton[] a3;
				checkParams(l,3,out a3);
				MenuBase.InitMultiToggleButton(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.Unity3D.UGUI.DisplayNode),typeof(string),typeof(DeepCore.Unity3D.UGUIEditor.UI.TouchClickHandle))){
				DeepCore.Unity3D.UGUI.DisplayNode a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				DeepCore.Unity3D.UGUIEditor.UI.TouchClickHandle a3;
				checkDelegate(l,3,out a3);
				MenuBase.InitMultiToggleButton(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function InitMultiToggleButton to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RefreshMultiToggleButton_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton a1;
			checkType(l,1,out a1);
			MenuBase.RefreshMultiToggleButton(a1);
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
	static public int get_s_RefCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,MenuBase.s_RefCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_s_RefCount(IntPtr l) {
		try {
			System.Int64 v;
			checkType(l,2,out v);
			MenuBase.s_RefCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MenuTag(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MenuTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MenuTag(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.MenuTag=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_XML_PATH(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.XML_PATH);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RootMenu(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RootMenu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ParentMenu(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ParentMenu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Childrens(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Childrens);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideBackList(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HideBackList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_HideBackList(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			List<MenuBase> v;
			checkType(l,2,out v);
			self.HideBackList=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ShowType(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ShowType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ShowType(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.ShowType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ExtParam(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ExtParam);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ExtParam(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			object[] v;
			checkArray(l,2,out v);
			self.ExtParam=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mRoot(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mRoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LuaTable(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LuaTable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LuaTable(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			SLua.LuaTable v;
			checkType(l,2,out v);
			self.LuaTable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RequireTable(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RequireTable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_RequireTable(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			SLua.LuaTable v;
			checkType(l,2,out v);
			self.RequireTable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CacheLevel(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CacheLevel);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_CacheLevel(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.CacheLevel=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDestroy(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDestroy);
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
			MenuBase self=(MenuBase)checkSelf(l);
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
	static public int get_IsUGUI(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsUGUI);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HasParentMenu(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HasParentMenu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LifeIndex(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LifeIndex);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LifeIndex(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.LifeIndex=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastUseTime(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastUseTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NeedBack(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.NeedBack);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_NeedBack(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.NeedBack=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MenuOrder(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MenuOrder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MenuOrder(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.MenuOrder=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MenuZ(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MenuZ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MenuZ(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.MenuZ=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnDestoryEvent(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.Action v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnDestoryEvent=v;
			else if(op==1) self.OnDestoryEvent+=v;
			else if(op==2) self.OnDestoryEvent-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnLoadEvent(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.Action<System.Action<System.Boolean>> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnLoadEvent=v;
			else if(op==1) self.OnLoadEvent+=v;
			else if(op==2) self.OnLoadEvent-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnEnterEvent(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.Action v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnEnterEvent=v;
			else if(op==1) self.OnEnterEvent+=v;
			else if(op==2) self.OnEnterEvent-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnExitEvent(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.Action v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnExitEvent=v;
			else if(op==1) self.OnExitEvent+=v;
			else if(op==2) self.OnExitEvent-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnCloseEvent(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.Action v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnCloseEvent=v;
			else if(op==1) self.OnCloseEvent+=v;
			else if(op==2) self.OnCloseEvent-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnEnableEvent(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.Action v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnEnableEvent=v;
			else if(op==1) self.OnEnableEvent+=v;
			else if(op==2) self.OnEnableEvent-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnDisableEvent(IntPtr l) {
		try {
			MenuBase self=(MenuBase)checkSelf(l);
			System.Action v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnDisableEvent=v;
			else if(op==1) self.OnDisableEvent+=v;
			else if(op==2) self.OnDisableEvent-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"MenuBase");
		addMember(l,FindSubMenu);
		addMember(l,AddSubMenu);
		addMember(l,AddChild);
		addMember(l,AddChildAt);
		addMember(l,RemoveSubMenu);
		addMember(l,RemoveAllSubMenu);
		addMember(l,SetUILayer);
		addMember(l,Set3DModelLayer);
		addMember(l,Close);
		addMember(l,CloseAndDestroy);
		addMember(l,Contains);
		addMember(l,SetLuaParams);
		addMember(l,HideBack);
		addMember(l,SetCompAnime);
		addMember(l,GetComponent);
		addMember(l,FindComponent);
		addMember(l,SetVisibleUENode);
		addMember(l,SetEnableUENode);
		addMember(l,SetGrayUENode);
		addMember(l,SetLabelText);
		addMember(l,SetButtonText);
		addMember(l,SetImageBox);
		addMember(l,LocalToUIGlobal);
		addMember(l,LocalToScreenGlobal);
		addMember(l,SetFullBackground);
		addMember(l,Create_s);
		addMember(l,FindComponentAs_s);
		addMember(l,FindChildComponent_s);
		addMember(l,SetVisibleUENode_s);
		addMember(l,SetEnableUENode_s);
		addMember(l,SetGrayUENode_s);
		addMember(l,SetLabelText_s);
		addMember(l,SetButtonText_s);
		addMember(l,SetImageBox_s);
		addMember(l,InitMultiToggleButton_s);
		addMember(l,RefreshMultiToggleButton_s);
		addMember(l,"s_RefCount",get_s_RefCount,set_s_RefCount,false);
		addMember(l,"MenuTag",get_MenuTag,set_MenuTag,true);
		addMember(l,"XML_PATH",get_XML_PATH,null,true);
		addMember(l,"RootMenu",get_RootMenu,null,true);
		addMember(l,"ParentMenu",get_ParentMenu,null,true);
		addMember(l,"Childrens",get_Childrens,null,true);
		addMember(l,"HideBackList",get_HideBackList,set_HideBackList,true);
		addMember(l,"ShowType",get_ShowType,set_ShowType,true);
		addMember(l,"ExtParam",get_ExtParam,set_ExtParam,true);
		addMember(l,"mRoot",get_mRoot,null,true);
		addMember(l,"LuaTable",get_LuaTable,set_LuaTable,true);
		addMember(l,"RequireTable",get_RequireTable,set_RequireTable,true);
		addMember(l,"CacheLevel",get_CacheLevel,set_CacheLevel,true);
		addMember(l,"IsDestroy",get_IsDestroy,null,true);
		addMember(l,"IsRunning",get_IsRunning,null,true);
		addMember(l,"IsUGUI",get_IsUGUI,null,true);
		addMember(l,"HasParentMenu",get_HasParentMenu,null,true);
		addMember(l,"LifeIndex",get_LifeIndex,set_LifeIndex,true);
		addMember(l,"LastUseTime",get_LastUseTime,null,true);
		addMember(l,"NeedBack",get_NeedBack,set_NeedBack,true);
		addMember(l,"MenuOrder",get_MenuOrder,set_MenuOrder,true);
		addMember(l,"MenuZ",get_MenuZ,set_MenuZ,true);
		addMember(l,"OnDestoryEvent",null,set_OnDestoryEvent,true);
		addMember(l,"OnLoadEvent",null,set_OnLoadEvent,true);
		addMember(l,"OnEnterEvent",null,set_OnEnterEvent,true);
		addMember(l,"OnExitEvent",null,set_OnExitEvent,true);
		addMember(l,"OnCloseEvent",null,set_OnCloseEvent,true);
		addMember(l,"OnEnableEvent",null,set_OnEnableEvent,true);
		addMember(l,"OnDisableEvent",null,set_OnDisableEvent,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(MenuBase),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
