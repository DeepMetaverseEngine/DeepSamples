using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UILayout : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout o;
			o=new DeepCore.Unity3D.UGUIEditor.UILayout();
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
	static public int CreateUILayoutImage_s(IntPtr l) {
		try {
			DeepCore.GUI.Data.UILayoutStyle a1;
			checkEnum(l,1,out a1);
			DeepCore.Unity3D.Impl.UnityImage a2;
			checkType(l,2,out a2);
			System.Int32 a3;
			checkType(l,3,out a3);
			DeepCore.GUI.Gemo.Rectangle2D a4;
			checkType(l,4,out a4);
			var ret=DeepCore.Unity3D.UGUIEditor.UILayout.CreateUILayoutImage(a1,a2,a3,a4);
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
	static public int CreateUILayoutSprite_s(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteMeta a1;
			checkType(l,1,out a1);
			System.Int32 a2;
			checkType(l,2,out a2);
			var ret=DeepCore.Unity3D.UGUIEditor.UILayout.CreateUILayoutSprite(a1,a2);
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
	static public int CreateUILayoutColor_s(IntPtr l) {
		try {
			UnityEngine.Color a1;
			checkType(l,1,out a1);
			UnityEngine.Color a2;
			checkType(l,2,out a2);
			var ret=DeepCore.Unity3D.UGUIEditor.UILayout.CreateUILayoutColor(a1,a2);
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
	static public int get_Style(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Style);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MainTexture(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MainTexture);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MainMaterial(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MainMaterial);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ImageSrc(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ImageSrc);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BorderColor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BorderColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FillColor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FillColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ImageRegion(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ImageRegion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Sprite(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Sprite);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SpriteAnimate(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SpriteAnimate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SpriteController(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SpriteController);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PreferredSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PreferredSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ClipSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ClipSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ClipSize2(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UILayout self=(DeepCore.Unity3D.UGUIEditor.UILayout)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ClipSize2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UILayout");
		addMember(l,CreateUILayoutImage_s);
		addMember(l,CreateUILayoutSprite_s);
		addMember(l,CreateUILayoutColor_s);
		addMember(l,"Style",get_Style,null,true);
		addMember(l,"MainTexture",get_MainTexture,null,true);
		addMember(l,"MainMaterial",get_MainMaterial,null,true);
		addMember(l,"ImageSrc",get_ImageSrc,null,true);
		addMember(l,"BorderColor",get_BorderColor,null,true);
		addMember(l,"FillColor",get_FillColor,null,true);
		addMember(l,"ImageRegion",get_ImageRegion,null,true);
		addMember(l,"Sprite",get_Sprite,null,true);
		addMember(l,"SpriteAnimate",get_SpriteAnimate,null,true);
		addMember(l,"SpriteController",get_SpriteController,null,true);
		addMember(l,"PreferredSize",get_PreferredSize,null,true);
		addMember(l,"ClipSize",get_ClipSize,null,true);
		addMember(l,"ClipSize2",get_ClipSize2,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UILayout));
	}
}
