using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_AutoSizeScrollablePanel : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel o;
			System.String a1;
			checkType(l,2,out a1);
			o=new DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel(a1);
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
	static public int ScrolleEvent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			self.ScrolleEvent(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Initialize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
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
	static public int IsBottom(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			var ret=self.IsBottom();
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
	static public int Rest(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.Rest(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AppendData(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.AppendData(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ToBottom(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.ToBottom(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UpdateContent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			self.UpdateContent();
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
	static public int get_dataCount(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.dataCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_dataCount(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.dataCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_noread(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.noread);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_noread(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.noread=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_CreateCell(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel.CreateCellItemHandler3344 v;
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
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel.HideCellItemHandler3344 v;
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
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel.ShowCellItemHandler3344 v;
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
	static public int get_Offset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RowCount(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
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
	static public int get_ExpandSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ExpandSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ExpandSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel self=(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.ExpandSize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"AutoSizeScrollablePanel");
		addMember(l,ScrolleEvent);
		addMember(l,Initialize);
		addMember(l,IsBottom);
		addMember(l,Rest);
		addMember(l,AppendData);
		addMember(l,ToBottom);
		addMember(l,UpdateContent);
		addMember(l,"dataCount",get_dataCount,set_dataCount,true);
		addMember(l,"noread",get_noread,set_noread,true);
		addMember(l,"event_CreateCell",null,set_event_CreateCell,true);
		addMember(l,"event_HideCell",null,set_event_HideCell,true);
		addMember(l,"event_ShowCell",null,set_event_ShowCell,true);
		addMember(l,"Offset",get_Offset,set_Offset,true);
		addMember(l,"RowCount",get_RowCount,null,true);
		addMember(l,"ExpandSize",get_ExpandSize,set_ExpandSize,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUI.AutoSizeScrollablePanel),typeof(DeepCore.Unity3D.UGUI.ScrollablePanel));
	}
}
