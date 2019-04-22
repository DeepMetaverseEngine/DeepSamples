using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_UEGauge : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge o;
			if(argc==2){
				System.Boolean a1;
				checkType(l,2,out a1);
				o=new DeepCore.Unity3D.UGUIEditor.UI.UEGauge(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==1){
				o=new DeepCore.Unity3D.UGUIEditor.UI.UEGauge();
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
	static public int SetGaugeMinMax(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			System.Double a1;
			checkType(l,2,out a1);
			System.Double a2;
			checkType(l,3,out a2);
			self.SetGaugeMinMax(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetFillMode(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			UnityEngine.UI.Image.FillMethod a1;
			checkEnum(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			System.Boolean a4;
			checkType(l,5,out a4);
			self.SetFillMode(a1,a2,a3,a4);
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
	static public int set_event_ValueChanged(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge.ValueChangedHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_ValueChanged=v;
			else if(op==1) self.event_ValueChanged+=v;
			else if(op==2) self.event_ValueChanged-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Strip(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Strip);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StripLayout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.StripLayout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_StripLayout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UILayout v;
			checkType(l,2,out v);
			self.StripLayout=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextSprite(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextSprite);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Orientation(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Orientation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Orientation(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			DeepCore.GUI.Data.GaugeOrientation v;
			checkEnum(l,2,out v);
			self.Orientation=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Value(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Value);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Value(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			double v;
			checkType(l,2,out v);
			self.Value=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ValuePercent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ValuePercent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ValuePercent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.ValuePercent=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsShowPercent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsShowPercent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsShowPercent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsShowPercent=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Text(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Text);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Text(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.Text=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FontSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FontSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FontSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.FontSize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FontColor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FontColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FontColor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			UnityEngine.Color v;
			checkType(l,2,out v);
			self.FontColor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EditTextAnchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EditTextAnchor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EditTextAnchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			DeepCore.GUI.Data.TextAnchor v;
			checkEnum(l,2,out v);
			self.EditTextAnchor=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextOffset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextOffset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TextOffset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.TextOffset=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GaugeMinValue(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GaugeMinValue);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GaugeMaxValue(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEGauge self=(DeepCore.Unity3D.UGUIEditor.UI.UEGauge)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.GaugeMaxValue);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UEGauge");
		addMember(l,SetGaugeMinMax);
		addMember(l,SetFillMode);
		addMember(l,"event_ValueChanged",null,set_event_ValueChanged,true);
		addMember(l,"Strip",get_Strip,null,true);
		addMember(l,"StripLayout",get_StripLayout,set_StripLayout,true);
		addMember(l,"TextSprite",get_TextSprite,null,true);
		addMember(l,"Orientation",get_Orientation,set_Orientation,true);
		addMember(l,"Value",get_Value,set_Value,true);
		addMember(l,"ValuePercent",get_ValuePercent,set_ValuePercent,true);
		addMember(l,"IsShowPercent",get_IsShowPercent,set_IsShowPercent,true);
		addMember(l,"Text",get_Text,set_Text,true);
		addMember(l,"FontSize",get_FontSize,set_FontSize,true);
		addMember(l,"FontColor",get_FontColor,set_FontColor,true);
		addMember(l,"EditTextAnchor",get_EditTextAnchor,set_EditTextAnchor,true);
		addMember(l,"TextOffset",get_TextOffset,set_TextOffset,true);
		addMember(l,"GaugeMinValue",get_GaugeMinValue,null,true);
		addMember(l,"GaugeMaxValue",get_GaugeMaxValue,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.UEGauge),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
