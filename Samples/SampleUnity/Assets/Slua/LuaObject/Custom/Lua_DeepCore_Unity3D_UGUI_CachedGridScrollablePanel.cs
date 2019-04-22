using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_CachedGridScrollablePanel : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel o;
			System.String a1;
			checkType(l,2,out a1);
			o=new DeepCore.Unity3D.UGUI.CachedGridScrollablePanel(a1);
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
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			UnityEngine.Vector2 a3;
			checkType(l,4,out a3);
			System.Int32 a4;
			checkType(l,5,out a4);
			self.Initialize(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Reset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.Reset(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetCell(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.GetCell(a1,a2);
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
	static public int ClearGrid(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			self.ClearGrid();
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
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
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
	static public int set_event_CreateCell(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel.CreateCellItemHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_CreateCell=v;
			else if(op==1) self.event_CreateCell+=v;
			else if(op==2) self.event_CreateCell-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_HideCell(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel.HideCellItemHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_HideCell=v;
			else if(op==1) self.event_HideCell+=v;
			else if(op==2) self.event_HideCell-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_ShowCell(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel.ShowCellItemHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_ShowCell=v;
			else if(op==1) self.event_ShowCell+=v;
			else if(op==2) self.event_ShowCell-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CellSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CellSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RowCount(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RowCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ColumnCount(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ColumnCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ViewRowCount(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ViewRowCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ViewColumnCount(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ViewColumnCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsUseCache(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsUseCache);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Gap(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Gap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Gap(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.Gap=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Border(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Border);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Border(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.Border=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Offset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Offset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Offset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.CachedGridScrollablePanel self=(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.Offset=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"CachedGridScrollablePanel");
		addMember(l,Initialize);
		addMember(l,Reset);
		addMember(l,GetCell);
		addMember(l,ClearGrid);
		addMember(l,RefreshShowCell);
		addMember(l,"event_CreateCell",null,set_event_CreateCell,true);
		addMember(l,"event_HideCell",null,set_event_HideCell,true);
		addMember(l,"event_ShowCell",null,set_event_ShowCell,true);
		addMember(l,"CellSize",get_CellSize,null,true);
		addMember(l,"RowCount",get_RowCount,null,true);
		addMember(l,"ColumnCount",get_ColumnCount,null,true);
		addMember(l,"ViewRowCount",get_ViewRowCount,null,true);
		addMember(l,"ViewColumnCount",get_ViewColumnCount,null,true);
		addMember(l,"IsUseCache",get_IsUseCache,null,true);
		addMember(l,"Gap",get_Gap,set_Gap,true);
		addMember(l,"Border",get_Border,set_Border,true);
		addMember(l,"Offset",get_Offset,set_Offset,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUI.CachedGridScrollablePanel),typeof(DeepCore.Unity3D.UGUI.ScrollablePanel));
	}
}
