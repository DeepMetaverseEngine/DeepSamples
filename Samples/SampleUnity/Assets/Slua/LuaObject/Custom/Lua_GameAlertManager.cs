using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_GameAlertManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowGoRoundTips(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				self.ShowGoRoundTips(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				self.ShowGoRoundTips(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function ShowGoRoundTips to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowGoRoundTipsXml(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.ShowGoRoundTipsXml(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowNotify(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				self.ShowNotify(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.UInt32 a2;
				checkType(l,3,out a2);
				self.ShowNotify(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.UInt32 a2;
				checkType(l,3,out a2);
				UnityEngine.Vector2 a3;
				checkType(l,4,out a3);
				self.ShowNotify(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.UInt32 a2;
				checkType(l,3,out a2);
				UnityEngine.Vector2 a3;
				checkType(l,4,out a3);
				System.Single a4;
				checkType(l,5,out a4);
				self.ShowNotify(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function ShowNotify to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseAllAlertUI(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			self.CloseAllAlertUI();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowAlertDialog(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==6){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				System.Object a4;
				checkType(l,5,out a4);
				AlertDialog.AlertAction a5;
				checkDelegate(l,6,out a5);
				var ret=self.ShowAlertDialog(a1,a2,a3,a4,a5);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==7){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				System.String a4;
				checkType(l,5,out a4);
				System.Object a5;
				checkType(l,6,out a5);
				AlertDialog.AlertAction a6;
				checkDelegate(l,7,out a6);
				var ret=self.ShowAlertDialog(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==8){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				System.String a4;
				checkType(l,5,out a4);
				System.Object a5;
				checkType(l,6,out a5);
				AlertDialog.AlertAction a6;
				checkDelegate(l,7,out a6);
				AlertDialog.AlertAction a7;
				checkDelegate(l,8,out a7);
				var ret=self.ShowAlertDialog(a1,a2,a3,a4,a5,a6,a7);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==9){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.String a3;
				checkType(l,4,out a3);
				System.String a4;
				checkType(l,5,out a4);
				System.String a5;
				checkType(l,6,out a5);
				System.Object a6;
				checkType(l,7,out a6);
				AlertDialog.AlertAction a7;
				checkDelegate(l,8,out a7);
				AlertDialog.AlertAction a8;
				checkDelegate(l,9,out a8);
				var ret=self.ShowAlertDialog(a1,a2,a3,a4,a5,a6,a7,a8);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function ShowAlertDialog to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowFloatingTips(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				self.ShowFloatingTips(a1);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.GUI.Display.Text.AttributedString),typeof(float))){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				DeepCore.GUI.Display.Text.AttributedString a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				self.ShowFloatingTips(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(System.UInt32))){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.UInt32 a2;
				checkType(l,3,out a2);
				self.ShowFloatingTips(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.UInt32 a2;
				checkType(l,3,out a2);
				System.Int32 a3;
				checkType(l,4,out a3);
				self.ShowFloatingTips(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				DeepCore.Unity3D.UGUI.DisplayNode a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				DeepCore.Unity3D.UGUI.DisplayNode a3;
				checkType(l,4,out a3);
				DeepCore.GUI.Gemo.Point2D a4;
				checkType(l,5,out a4);
				self.ShowFloatingTips(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(argc==7){
				GameAlertManager self=(GameAlertManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.UInt32 a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				DeepCore.Unity3D.UGUI.DisplayNode a4;
				checkType(l,5,out a4);
				DeepCore.GUI.Gemo.Point2D a5;
				checkType(l,6,out a5);
				System.Int32 a6;
				checkType(l,7,out a6);
				self.ShowFloatingTips(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function ShowFloatingTips to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowFloatingTipsImage(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.ShowFloatingTipsImage(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ClearAllFloatingTips(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			self.ClearAllFloatingTips();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowLoading(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			WaitingUI.TimesUpEvent a4;
			checkDelegate(l,5,out a4);
			self.ShowLoading(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Update(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.Update(a1);
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
			GameAlertManager self=(GameAlertManager)checkSelf(l);
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
	static public int get_IsWaiting(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsWaiting);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AlertDialog(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AlertDialog);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GoRound(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GoRound);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CpjAnime(IntPtr l) {
		try {
			GameAlertManager self=(GameAlertManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CpjAnime);
			return 2;
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
			pushValue(l,GameAlertManager.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"GameAlertManager");
		addMember(l,ShowGoRoundTips);
		addMember(l,ShowGoRoundTipsXml);
		addMember(l,ShowNotify);
		addMember(l,CloseAllAlertUI);
		addMember(l,ShowAlertDialog);
		addMember(l,ShowFloatingTips);
		addMember(l,ShowFloatingTipsImage);
		addMember(l,ClearAllFloatingTips);
		addMember(l,ShowLoading);
		addMember(l,Update);
		addMember(l,Clear);
		addMember(l,"IsWaiting",get_IsWaiting,null,true);
		addMember(l,"AlertDialog",get_AlertDialog,null,true);
		addMember(l,"GoRound",get_GoRound,null,true);
		addMember(l,"CpjAnime",get_CpjAnime,null,true);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(GameAlertManager));
	}
}
