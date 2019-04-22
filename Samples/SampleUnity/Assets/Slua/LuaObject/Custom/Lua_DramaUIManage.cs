using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DramaUIManage : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowSideTool(IntPtr l) {
		try {
			DramaUIManage self=(DramaUIManage)checkSelf(l);
			System.Action a1;
			checkDelegate(l,2,out a1);
			self.ShowSideTool(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseSideTool(IntPtr l) {
		try {
			DramaUIManage self=(DramaUIManage)checkSelf(l);
			self.CloseSideTool();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CloseCaption(IntPtr l) {
		try {
			DramaUIManage self=(DramaUIManage)checkSelf(l);
			self.CloseCaption();
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
			DramaUIManage self=(DramaUIManage)checkSelf(l);
			MenuBase a1;
			checkType(l,2,out a1);
			self.AddMenu(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Test1(IntPtr l) {
		try {
			DramaUIManage self=(DramaUIManage)checkSelf(l);
			self.Test1();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Init_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			DramaUIManage.Init(a1);
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
	static public int get_highlightMask(IntPtr l) {
		try {
			DramaUIManage self=(DramaUIManage)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.highlightMask);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_highlightMask(IntPtr l) {
		try {
			DramaUIManage self=(DramaUIManage)checkSelf(l);
			HighlightMask v;
			checkType(l,2,out v);
			self.highlightMask=v;
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
			pushValue(l,DramaUIManage.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DramaUIManage");
		addMember(l,ShowSideTool);
		addMember(l,CloseSideTool);
		addMember(l,CloseCaption);
		addMember(l,AddMenu);
		addMember(l,Test1);
		addMember(l,Init_s);
		addMember(l,"highlightMask",get_highlightMask,set_highlightMask,true);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DramaUIManage),typeof(UnityEngine.MonoBehaviour));
	}
}
