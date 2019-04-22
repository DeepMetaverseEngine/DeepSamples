using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_UETextComponent : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetBorder(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
			UnityEngine.Color a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			self.SetBorder(a1,a2);
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
	static public int set_event_TextChanged(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent.TextChangedHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_TextChanged=v;
			else if(op==1) self.event_TextChanged+=v;
			else if(op==2) self.event_TextChanged-=v;
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
	static public int get_TextGraphics(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextGraphics);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsNoLayout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsNoLayout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Text(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
	static public int get_EditTextAnchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
	static public int get_FontSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
	static public int get_TextOffset(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
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
	static public int get_PreferredSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UETextComponent self=(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PreferredSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UETextComponent");
		addMember(l,SetBorder);
		addMember(l,"event_TextChanged",null,set_event_TextChanged,true);
		addMember(l,"TextSprite",get_TextSprite,null,true);
		addMember(l,"TextGraphics",get_TextGraphics,null,true);
		addMember(l,"IsNoLayout",get_IsNoLayout,null,true);
		addMember(l,"Text",get_Text,set_Text,true);
		addMember(l,"EditTextAnchor",get_EditTextAnchor,set_EditTextAnchor,true);
		addMember(l,"FontSize",get_FontSize,set_FontSize,true);
		addMember(l,"FontColor",get_FontColor,set_FontColor,true);
		addMember(l,"TextOffset",get_TextOffset,set_TextOffset,true);
		addMember(l,"PreferredSize",get_PreferredSize,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
