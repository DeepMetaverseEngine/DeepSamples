using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_AlertDialog : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			AlertDialog o;
			o=new AlertDialog();
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
	static public int ShowAlertDialog(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==4){
				AlertDialog self=(AlertDialog)checkSelf(l);
				AlertDialogUIBase a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				System.Object a3;
				checkType(l,4,out a3);
				var ret=self.ShowAlertDialog(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==6){
				AlertDialog self=(AlertDialog)checkSelf(l);
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
				AlertDialog self=(AlertDialog)checkSelf(l);
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
				AlertDialog self=(AlertDialog)checkSelf(l);
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
				AlertDialog self=(AlertDialog)checkSelf(l);
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
	static public int ShowAlertDialogWithCloseBtn(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==7){
				AlertDialog self=(AlertDialog)checkSelf(l);
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
				var ret=self.ShowAlertDialogWithCloseBtn(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==9){
				AlertDialog self=(AlertDialog)checkSelf(l);
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
				var ret=self.ShowAlertDialogWithCloseBtn(a1,a2,a3,a4,a5,a6,a7,a8);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function ShowAlertDialogWithCloseBtn to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowAlertDialogTime(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==9){
				AlertDialog self=(AlertDialog)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.Single a3;
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
				var ret=self.ShowAlertDialogTime(a1,a2,a3,a4,a5,a6,a7,a8);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==11){
				AlertDialog self=(AlertDialog)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				System.String a4;
				checkType(l,5,out a4);
				System.String a5;
				checkType(l,6,out a5);
				System.String a6;
				checkType(l,7,out a6);
				System.Object a7;
				checkType(l,8,out a7);
				AlertDialog.AlertAction a8;
				checkDelegate(l,9,out a8);
				AlertDialog.AlertAction a9;
				checkDelegate(l,10,out a9);
				AlertDialog.AlertAction a10;
				checkDelegate(l,11,out a10);
				var ret=self.ShowAlertDialogTime(a1,a2,a3,a4,a5,a6,a7,a8,a9,a10);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function ShowAlertDialogTime to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsDialogExist(IntPtr l) {
		try {
			AlertDialog self=(AlertDialog)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.IsDialogExist(a1);
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
	static public int GetDialogUI(IntPtr l) {
		try {
			AlertDialog self=(AlertDialog)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetDialogUI(a1);
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
	static public int GetDialogUINode(IntPtr l) {
		try {
			AlertDialog self=(AlertDialog)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetDialogUINode(a1);
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
	static public int GetPriorityDialogCount(IntPtr l) {
		try {
			AlertDialog self=(AlertDialog)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetPriorityDialogCount(a1);
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
	static public int CloseDialog(IntPtr l) {
		try {
			AlertDialog self=(AlertDialog)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.CloseDialog(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetDialogAnchor(IntPtr l) {
		try {
			AlertDialog self=(AlertDialog)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			DeepCore.GUI.Data.TextAnchor a2;
			checkEnum(l,3,out a2);
			self.SetDialogAnchor(a1,a2);
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
			AlertDialog self=(AlertDialog)checkSelf(l);
			self.Clear();
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
	static public int get_PRIORITY_NORMAL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,AlertDialog.PRIORITY_NORMAL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PRIORITY_FRIEND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,AlertDialog.PRIORITY_FRIEND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PRIORITY_TEAM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,AlertDialog.PRIORITY_TEAM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PRIORITY_RELIVE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,AlertDialog.PRIORITY_RELIVE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PRIORITY_SYSTEM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,AlertDialog.PRIORITY_SYSTEM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"AlertDialog");
		addMember(l,ShowAlertDialog);
		addMember(l,ShowAlertDialogWithCloseBtn);
		addMember(l,ShowAlertDialogTime);
		addMember(l,IsDialogExist);
		addMember(l,GetDialogUI);
		addMember(l,GetDialogUINode);
		addMember(l,GetPriorityDialogCount);
		addMember(l,CloseDialog);
		addMember(l,SetDialogAnchor);
		addMember(l,Clear);
		addMember(l,"PRIORITY_NORMAL",get_PRIORITY_NORMAL,null,false);
		addMember(l,"PRIORITY_FRIEND",get_PRIORITY_FRIEND,null,false);
		addMember(l,"PRIORITY_TEAM",get_PRIORITY_TEAM,null,false);
		addMember(l,"PRIORITY_RELIVE",get_PRIORITY_RELIVE,null,false);
		addMember(l,"PRIORITY_SYSTEM",get_PRIORITY_SYSTEM,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(AlertDialog));
	}
}
