using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_HZCanvas : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.HZCanvas();
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
	static public int SetGridLayout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas self=(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			UnityEngine.RectOffset a3;
			checkType(l,4,out a3);
			UnityEngine.UI.GridLayoutGroup.Constraint a4;
			checkEnum(l,5,out a4);
			System.Int32 a5;
			checkType(l,6,out a5);
			self.SetGridLayout(a1,a2,a3,a4,a5);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DisableGridLayout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas self=(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas)checkSelf(l);
			self.DisableGridLayout();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetContentSizeFitter(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas self=(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas)checkSelf(l);
			UnityEngine.UI.ContentSizeFitter.FitMode a1;
			checkEnum(l,2,out a1);
			UnityEngine.UI.ContentSizeFitter.FitMode a2;
			checkEnum(l,3,out a2);
			self.SetContentSizeFitter(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetFlexibleGridLayout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas self=(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			UnityEngine.RectOffset a3;
			checkType(l,4,out a3);
			self.SetFlexibleGridLayout(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetCenterScaleMode(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas self=(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			System.Int32 a4;
			checkType(l,5,out a4);
			System.Action<System.Int32,DeepCore.Unity3D.UGUI.DisplayNode> a5;
			checkDelegate(l,6,out a5);
			System.Action<System.Int32,DeepCore.Unity3D.UGUI.DisplayNode> a6;
			checkDelegate(l,7,out a6);
			System.Action a7;
			checkDelegate(l,8,out a7);
			System.Action a8;
			checkDelegate(l,9,out a8);
			self.SetCenterScaleMode(a1,a2,a3,a4,a5,a6,a7,a8);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ChangeCenterScalePage(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas self=(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.ChangeCenterScalePage(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateCanvas_s(IntPtr l) {
		try {
			DeepCore.GUI.Data.UECanvasMeta a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUIEditor.UI.HZCanvas.CreateCanvas(a1);
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
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas self=(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas)checkSelf(l);
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
	static public int get_CenterScalePageIndex(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas self=(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CenterScalePageIndex);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZCanvas");
		addMember(l,SetGridLayout);
		addMember(l,DisableGridLayout);
		addMember(l,SetContentSizeFitter);
		addMember(l,SetFlexibleGridLayout);
		addMember(l,SetCenterScaleMode);
		addMember(l,ChangeCenterScalePage);
		addMember(l,CreateCanvas_s);
		addMember(l,"TouchClick",null,set_TouchClick,true);
		addMember(l,"CenterScalePageIndex",get_CenterScalePageIndex,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.HZCanvas),typeof(DeepCore.Unity3D.UGUIEditor.UI.UECanvas));
	}
}
