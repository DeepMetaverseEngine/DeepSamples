using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_HZLabel : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZLabel o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.HZLabel();
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
	static public int CreateLabel_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==0){
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZLabel.CreateLabel();
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==1){
				DeepCore.GUI.Data.UETextComponentMeta a1;
				checkType(l,1,out a1);
				var ret=DeepCore.Unity3D.UGUIEditor.UI.HZLabel.CreateLabel(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateLabel to call");
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
	static public int get_SupportRichtext(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZLabel self=(DeepCore.Unity3D.UGUIEditor.UI.HZLabel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SupportRichtext);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SupportRichtext(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZLabel self=(DeepCore.Unity3D.UGUIEditor.UI.HZLabel)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.SupportRichtext=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FontColorRGBA(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZLabel self=(DeepCore.Unity3D.UGUIEditor.UI.HZLabel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FontColorRGBA);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FontColorRGBA(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZLabel self=(DeepCore.Unity3D.UGUIEditor.UI.HZLabel)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.FontColorRGBA=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FontColorRGB(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZLabel self=(DeepCore.Unity3D.UGUIEditor.UI.HZLabel)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FontColorRGB);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FontColorRGB(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZLabel self=(DeepCore.Unity3D.UGUIEditor.UI.HZLabel)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.FontColorRGB=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZLabel");
		addMember(l,CreateLabel_s);
		addMember(l,"SupportRichtext",get_SupportRichtext,set_SupportRichtext,true);
		addMember(l,"FontColorRGBA",get_FontColorRGBA,set_FontColorRGBA,true);
		addMember(l,"FontColorRGB",get_FontColorRGB,set_FontColorRGB,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.HZLabel),typeof(DeepCore.Unity3D.UGUIEditor.UI.UELabel));
	}
}
