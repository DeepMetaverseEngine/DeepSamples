using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_HZScrollPan : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan();
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
	static public int Initialize(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==5){
				DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				UnityEngine.Vector2 a2;
				checkType(l,3,out a2);
				DeepCore.Unity3D.UGUI.PagedScrollablePanel.CreatePageItemHandler a3;
				checkDelegate(l,4,out a3);
				System.Action<System.Int32> a4;
				checkDelegate(l,5,out a4);
				self.Initialize(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(argc==6){
				DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				UnityEngine.Vector2 a2;
				checkType(l,3,out a2);
				System.Boolean a3;
				checkType(l,4,out a3);
				DeepCore.Unity3D.UGUI.PagedScrollablePanel.CreatePageItemHandler a4;
				checkDelegate(l,5,out a4);
				System.Action<System.Int32> a5;
				checkDelegate(l,6,out a5);
				self.Initialize(a1,a2,a3,a4,a5);
				pushValue(l,true);
				return 1;
			}
			else if(argc==8){
				DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
				System.Single a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				System.Int32 a3;
				checkType(l,4,out a3);
				System.Int32 a4;
				checkType(l,5,out a4);
				DeepCore.Unity3D.UGUI.DisplayNode a5;
				checkType(l,6,out a5);
				DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan.ScrollPanUpdateHandler a6;
				checkDelegate(l,7,out a6);
				DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan.TrusteeshipChildInit a7;
				checkDelegate(l,8,out a7);
				self.Initialize(a1,a2,a3,a4,a5,a6,a7);
				pushValue(l,true);
				return 1;
			}
			else if(argc==9){
				DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
				System.Single a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				System.Int32 a3;
				checkType(l,4,out a3);
				System.Int32 a4;
				checkType(l,5,out a4);
				UnityEngine.Vector2 a5;
				checkType(l,6,out a5);
				DeepCore.Unity3D.UGUI.DisplayNode a6;
				checkType(l,7,out a6);
				DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan.ScrollPanUpdateHandler a7;
				checkDelegate(l,8,out a7);
				DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan.TrusteeshipChildInit a8;
				checkDelegate(l,9,out a8);
				self.Initialize(a1,a2,a3,a4,a5,a6,a7,a8);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Initialize to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowPrevPage(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			self.ShowPrevPage();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowNextPage(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			self.ShowNextPage();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowPage(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.ShowPage(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsTheLastItem(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			var ret=self.IsTheLastItem();
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
	static public int IsTheFirstItem(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			var ret=self.IsTheFirstItem();
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
	static public int ResetRowsAndColumns(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.ResetRowsAndColumns(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddNormalChild(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			self.AddNormalChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveNormalChild(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.RemoveNormalChild(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RefreshShowCell(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			self.RefreshShowCell();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateScrollPan_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				DeepCore.GUI.Data.UEScrollPanMeta a1;
				checkType(l,1,out a1);
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan.CreateScrollPan(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan.Mode a1;
				checkEnum(l,1,out a1);
				System.Boolean a2;
				checkType(l,2,out a2);
				System.Boolean a3;
				checkType(l,3,out a3);
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan.CreateScrollPan(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateScrollPan to call");
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
	static public int get_DefaultSoundKey(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan.DefaultSoundKey);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultSoundKey(IntPtr l) {
		try {
			System.String v;
			checkType(l,2,out v);
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan.DefaultSoundKey=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rows(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Rows);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Rows(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.Rows=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Columns(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Columns);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Columns(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan self=(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.Columns=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZScrollPan");
		addMember(l,Initialize);
		addMember(l,ShowPrevPage);
		addMember(l,ShowNextPage);
		addMember(l,ShowPage);
		addMember(l,IsTheLastItem);
		addMember(l,IsTheFirstItem);
		addMember(l,ResetRowsAndColumns);
		addMember(l,AddNormalChild);
		addMember(l,RemoveNormalChild);
		addMember(l,RefreshShowCell);
		addMember(l,CreateScrollPan_s);
		addMember(l,"DefaultSoundKey",get_DefaultSoundKey,set_DefaultSoundKey,false);
		addMember(l,"Rows",get_Rows,set_Rows,true);
		addMember(l,"Columns",get_Columns,set_Columns,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.HZScrollPan),typeof(DeepCore.Unity3D.UGUIEditor.UI.UEScrollPan));
	}
}
