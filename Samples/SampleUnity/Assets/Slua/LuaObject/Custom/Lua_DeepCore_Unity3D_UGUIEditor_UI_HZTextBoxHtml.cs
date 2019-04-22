using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_HZTextBoxHtml : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextBoxHtml o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.HZTextBoxHtml();
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
	static public int set_LinkClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.HZTextBoxHtml self=(DeepCore.Unity3D.UGUIEditor.UI.HZTextBoxHtml)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.HZTextBoxHtml.LinkClickHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.LinkClick=v;
			else if(op==1) self.LinkClick+=v;
			else if(op==2) self.LinkClick-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZTextBoxHtml");
		addMember(l,"LinkClick",null,set_LinkClick,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.HZTextBoxHtml),typeof(DeepCore.Unity3D.UGUIEditor.UI.UETextBoxHtml));
	}
}
