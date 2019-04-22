using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_HZUIEditor : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			HZUIEditor o;
			System.String a1;
			checkType(l,2,out a1);
			o=new HZUIEditor(a1);
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
	static public int SetDefaultFont(IntPtr l) {
		try {
			HZUIEditor self=(HZUIEditor)checkSelf(l);
			UnityEngine.Font a1;
			checkType(l,2,out a1);
			self.SetDefaultFont(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetMeta(IntPtr l) {
		try {
			HZUIEditor self=(HZUIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetMeta(a1);
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
	static public int GetMetaKey(IntPtr l) {
		try {
			HZUIEditor self=(HZUIEditor)checkSelf(l);
			DeepCore.GUI.Data.UIComponentMeta a1;
			checkType(l,2,out a1);
			var ret=self.GetMetaKey(a1);
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
	static public int ReleaseTextureExt(IntPtr l) {
		try {
			HZUIEditor self=(HZUIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.ReleaseTextureExt(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReleaseDynamicTexture(IntPtr l) {
		try {
			HZUIEditor self=(HZUIEditor)checkSelf(l);
			self.ReleaseDynamicTexture();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FindChildMetaByEditName(IntPtr l) {
		try {
			HZUIEditor self=(HZUIEditor)checkSelf(l);
			DeepCore.GUI.Data.UIComponentMeta a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.FindChildMetaByEditName(a1,a2);
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
	static public int CreateFont(IntPtr l) {
		try {
			HZUIEditor self=(HZUIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.CreateFont(a1);
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
	static public int CreateRichTextLayer(IntPtr l) {
		try {
			HZUIEditor self=(HZUIEditor)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.CreateRichTextLayer(a1,a2);
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZUIEditor");
		addMember(l,SetDefaultFont);
		addMember(l,GetMeta);
		addMember(l,GetMetaKey);
		addMember(l,ReleaseTextureExt);
		addMember(l,ReleaseDynamicTexture);
		addMember(l,FindChildMetaByEditName);
		addMember(l,CreateFont);
		addMember(l,CreateRichTextLayer);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(HZUIEditor),typeof(DeepCore.Unity3D.UGUIEditor.UIEditor));
	}
}
