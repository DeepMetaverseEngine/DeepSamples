using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_UERichTextPan : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan o;
			if(argc==2){
				System.Boolean a1;
				checkType(l,2,out a1);
				o=new DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==1){
				o=new DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan();
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
	static public int get_AText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan self=(DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AText);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AText(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan self=(DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan)checkSelf(l);
			DeepCore.GUI.Display.Text.AttributedString v;
			checkType(l,2,out v);
			self.AText=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RichTextLayer(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan self=(DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RichTextLayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextPan(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan self=(DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextPan);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UERichTextPan");
		addMember(l,"AText",get_AText,set_AText,true);
		addMember(l,"RichTextLayer",get_RichTextLayer,null,true);
		addMember(l,"TextPan",get_TextPan,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.UERichTextPan),typeof(DeepCore.Unity3D.UGUIEditor.UI.BaseUERichTextBox));
	}
}
