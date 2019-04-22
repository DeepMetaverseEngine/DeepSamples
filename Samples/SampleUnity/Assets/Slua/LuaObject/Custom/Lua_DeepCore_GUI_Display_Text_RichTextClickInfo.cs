using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GUI_Display_Text_RichTextClickInfo : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.RichTextClickInfo o;
			o=new DeepCore.GUI.Display.Text.RichTextClickInfo();
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
	static public int get_mRegion(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.RichTextClickInfo self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.mRegion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mRegion(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.RichTextClickInfo self;
			checkValueType(l,1,out self);
			DeepCore.GUI.Display.Text.BaseRichTextLayer.Region v;
			checkType(l,2,out v);
			self.mRegion=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mLine(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.RichTextClickInfo self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.mLine);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mLine(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.RichTextClickInfo self;
			checkValueType(l,1,out self);
			DeepCore.GUI.Display.Text.BaseRichTextLayer.Line v;
			checkType(l,2,out v);
			self.mLine=v;
			setBack(l,self);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"RichTextClickInfo");
		addMember(l,"mRegion",get_mRegion,set_mRegion,true);
		addMember(l,"mLine",get_mLine,set_mLine,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.GUI.Display.Text.RichTextClickInfo),typeof(System.ValueType));
	}
}
