using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UIEditor : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor o;
			System.String a1;
			checkType(l,2,out a1);
			o=new DeepCore.Unity3D.UGUIEditor.UIEditor(a1);
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
	static public int CreateFromFile(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.UGUIEditor.UIEditor.UIComponentCreater a2;
			checkDelegate(l,3,out a2);
			var ret=self.CreateFromFile(a1,a2);
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
	static public int CreateFromMeta(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			DeepCore.GUI.Data.UIComponentMeta a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.UGUIEditor.UIEditor.UIComponentCreater a2;
			checkDelegate(l,3,out a2);
			var ret=self.CreateFromMeta(a1,a2);
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
	static public int CreateLayout(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			DeepCore.GUI.Data.UILayoutMeta a1;
			checkType(l,2,out a1);
			var ret=self.CreateLayout(a1);
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
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
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
	static public int ParseImageFont(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				var ret=self.ParseImageFont(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==4){
				DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				DeepCore.Unity3D.UGUI.ImageFontGraphics a3;
				checkType(l,4,out a3);
				var ret=self.ParseImageFont(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function ParseImageFont to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ParseImageSpriteFromAtlas(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			var ret=self.ParseImageSpriteFromAtlas(a1,a2);
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
	static public int ParseImageSpriteFromImage(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			var ret=self.ParseImageSpriteFromImage(a1,a2);
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
	static public int ParseAtlasTile(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(UnityEngine.Vector2))){
				DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.Vector2 a2;
				checkType(l,3,out a2);
				var ret=self.ParseAtlasTile(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(LuaOut))){
				DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				DeepCore.GUI.Gemo.Rectangle2D a2;
				checkType(l,3,out a2);
				var ret=self.ParseAtlasTile(a1,out a2);
				pushValue(l,true);
				pushValue(l,ret);
				pushValue(l,a2);
				return 3;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function ParseAtlasTile to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ParseSpriteMeta(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.ParseSpriteMeta(a1,out a2);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a2);
			return 3;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetImage(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetImage(a1);
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
	static public int GetAtlas(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.GetAtlas(a1,a2);
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
	static public int GetCPJResource(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetCPJResource(a1);
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
	static public int GetSpriteMeta(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.GetSpriteMeta(a1,a2);
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
	static public int GetUIMeta(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetUIMeta(a1);
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
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
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
	static public int CleanMetaMap(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			self.CleanMetaMap();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CleanImageMap(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			self.CleanImageMap();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReleaseTexture(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.ReleaseTexture(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReleaseAllTexture(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			self.ReleaseAllTexture();
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
	static public int get_GlobalUseBitmapText(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIEditor.UIEditor.GlobalUseBitmapText);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_GlobalUseBitmapText(IntPtr l) {
		try {
			bool v;
			checkType(l,2,out v);
			DeepCore.Unity3D.UGUIEditor.UIEditor.GlobalUseBitmapText=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ResRoot(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ResRoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Root(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UIEditor self=(DeepCore.Unity3D.UGUIEditor.UIEditor)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Root);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UIEditor");
		addMember(l,CreateFromFile);
		addMember(l,CreateFromMeta);
		addMember(l,CreateLayout);
		addMember(l,CreateFont);
		addMember(l,ParseImageFont);
		addMember(l,ParseImageSpriteFromAtlas);
		addMember(l,ParseImageSpriteFromImage);
		addMember(l,ParseAtlasTile);
		addMember(l,ParseSpriteMeta);
		addMember(l,GetImage);
		addMember(l,GetAtlas);
		addMember(l,GetCPJResource);
		addMember(l,GetSpriteMeta);
		addMember(l,GetUIMeta);
		addMember(l,CreateRichTextLayer);
		addMember(l,CleanMetaMap);
		addMember(l,CleanImageMap);
		addMember(l,ReleaseTexture);
		addMember(l,ReleaseAllTexture);
		addMember(l,"GlobalUseBitmapText",get_GlobalUseBitmapText,set_GlobalUseBitmapText,false);
		addMember(l,"ResRoot",get_ResRoot,null,true);
		addMember(l,"Root",get_Root,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UIEditor),typeof(DeepCore.Unity3D.UGUI.UIFactory));
	}
}
