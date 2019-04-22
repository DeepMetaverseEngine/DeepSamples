using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_HZImageBox : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZImageBox o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.HZImageBox();
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
	static public int StartEraserMode(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZImageBox self=(DeepCore.Unity3D.UGUIEditor.UI.HZImageBox)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.StartEraserMode(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopEraserMode(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZImageBox self=(DeepCore.Unity3D.UGUIEditor.UI.HZImageBox)checkSelf(l);
			self.StopEraserMode();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateImageBox_s(IntPtr l) {
		try {
			var ret=DeepCore.Unity3D.UGUIEditor.UI.HZImageBox.CreateImageBox();
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
	static public int set_TouchClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZImageBox self=(DeepCore.Unity3D.UGUIEditor.UI.HZImageBox)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.TouchClickHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.TouchClick=v;
			else if(op==1) self.TouchClick+=v;
			else if(op==2) self.TouchClick-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EraserPercent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZImageBox self=(DeepCore.Unity3D.UGUIEditor.UI.HZImageBox)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EraserPercent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZImageBox");
		addMember(l,StartEraserMode);
		addMember(l,StopEraserMode);
		addMember(l,CreateImageBox_s);
		addMember(l,"TouchClick",null,set_TouchClick,true);
		addMember(l,"EraserPercent",get_EraserPercent,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.HZImageBox),typeof(DeepCore.Unity3D.UGUIEditor.UI.UEImageBox));
	}
}
