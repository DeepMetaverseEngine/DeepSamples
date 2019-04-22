using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_UIUtils : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnityRichTextToXmlText_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.UnityRichTextToXmlText(a1);
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
	static public int UInt32_RGBA_To_Color_s(IntPtr l) {
		try {
			System.UInt32 a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.UInt32_RGBA_To_Color(a1);
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
	static public int Color_To_UInt32_RGBA_s(IntPtr l) {
		try {
			UnityEngine.Color a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.Color_To_UInt32_RGBA(a1);
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
	static public int UInt32_ARGB_To_Color_s(IntPtr l) {
		try {
			System.UInt32 a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.UInt32_ARGB_To_Color(a1);
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
	static public int HexArgbToColor_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.HexArgbToColor(a1);
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
	static public int ToTextShadowCount_s(IntPtr l) {
		try {
			UnityEngine.Vector2 a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.ToTextShadowCount(a1);
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
	static public int ToTextBorderOffset_s(IntPtr l) {
		try {
			DeepCore.GUI.Data.TextBorderCount a1;
			checkEnum(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.ToTextBorderOffset(a1);
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
	static public int ToUnityAnchor_s(IntPtr l) {
		try {
			DeepCore.GUI.Data.TextAnchor a1;
			checkEnum(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.ToUnityAnchor(a1);
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
	static public int ToTextAnchor_s(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.RichTextAlignment a1;
			checkEnum(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.ToTextAnchor(a1);
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
	static public int ToRichTextAnchor_s(IntPtr l) {
		try {
			DeepCore.GUI.Data.TextAnchor a1;
			checkEnum(l,1,out a1);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.ToRichTextAnchor(a1);
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
	static public int ToTextLayerFontStyle_s(IntPtr l) {
		try {
			DeepCore.GUI.Data.FontStyle a1;
			checkEnum(l,1,out a1);
			System.Boolean a2;
			checkType(l,2,out a2);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.ToTextLayerFontStyle(a1,a2);
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
	static public int ToFontStyle_s(IntPtr l) {
		try {
			DeepCore.GUI.Display.FontStyle a1;
			checkEnum(l,1,out a1);
			System.Boolean a2;
			checkType(l,2,out a2);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.ToFontStyle(a1,out a2);
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
	static public int AdjustAnchor_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(DeepCore.GUI.Data.ImageAnchor),typeof(UnityEngine.Vector2),typeof(UnityEngine.Rect))){
				DeepCore.GUI.Data.ImageAnchor a1;
				checkEnum(l,1,out a1);
				UnityEngine.Vector2 a2;
				checkType(l,2,out a2);
				UnityEngine.Rect a3;
				checkValueType(l,3,out a3);
				DeepCore.Unity3D.UGUI.UIUtils.AdjustAnchor(a1,a2,ref a3);
				pushValue(l,true);
				pushValue(l,a3);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.GUI.Data.TextAnchor),typeof(UnityEngine.Vector2),typeof(UnityEngine.Rect))){
				DeepCore.GUI.Data.TextAnchor a1;
				checkEnum(l,1,out a1);
				UnityEngine.Vector2 a2;
				checkType(l,2,out a2);
				UnityEngine.Rect a3;
				checkValueType(l,3,out a3);
				DeepCore.Unity3D.UGUI.UIUtils.AdjustAnchor(a1,a2,ref a3);
				pushValue(l,true);
				pushValue(l,a3);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.GUI.Data.ImageAnchor),typeof(DeepCore.Unity3D.UGUI.DisplayNode),typeof(DeepCore.Unity3D.UGUI.DisplayNode),typeof(UnityEngine.Vector2))){
				DeepCore.GUI.Data.ImageAnchor a1;
				checkEnum(l,1,out a1);
				DeepCore.Unity3D.UGUI.DisplayNode a2;
				checkType(l,2,out a2);
				DeepCore.Unity3D.UGUI.DisplayNode a3;
				checkType(l,3,out a3);
				UnityEngine.Vector2 a4;
				checkType(l,4,out a4);
				DeepCore.Unity3D.UGUI.UIUtils.AdjustAnchor(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.GUI.Data.TextAnchor),typeof(DeepCore.Unity3D.UGUI.DisplayNode),typeof(DeepCore.Unity3D.UGUI.DisplayNode),typeof(UnityEngine.Vector2))){
				DeepCore.GUI.Data.TextAnchor a1;
				checkEnum(l,1,out a1);
				DeepCore.Unity3D.UGUI.DisplayNode a2;
				checkType(l,2,out a2);
				DeepCore.Unity3D.UGUI.DisplayNode a3;
				checkType(l,3,out a3);
				UnityEngine.Vector2 a4;
				checkType(l,4,out a4);
				DeepCore.Unity3D.UGUI.UIUtils.AdjustAnchor(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AdjustAnchor to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AdjustGaugeOrientation_s(IntPtr l) {
		try {
			DeepCore.GUI.Data.GaugeOrientation a1;
			checkEnum(l,1,out a1);
			DeepCore.Unity3D.UGUI.DisplayNode a2;
			checkType(l,2,out a2);
			DeepCore.Unity3D.UGUI.DisplayNode a3;
			checkType(l,3,out a3);
			System.Single a4;
			checkType(l,4,out a4);
			DeepCore.Unity3D.UGUI.UIUtils.AdjustGaugeOrientation(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateSprite_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(DeepCore.Unity3D.Impl.UnityImage),typeof(DeepCore.GUI.Gemo.Rectangle2D),typeof(UnityEngine.Vector2))){
				DeepCore.Unity3D.Impl.UnityImage a1;
				checkType(l,1,out a1);
				DeepCore.GUI.Gemo.Rectangle2D a2;
				checkType(l,2,out a2);
				UnityEngine.Vector2 a3;
				checkType(l,3,out a3);
				var ret=DeepCore.Unity3D.UGUI.UIUtils.CreateSprite(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.Unity3D.Impl.UnityImage),typeof(UnityEngine.Rect),typeof(UnityEngine.Vector2))){
				DeepCore.Unity3D.Impl.UnityImage a1;
				checkType(l,1,out a1);
				UnityEngine.Rect a2;
				checkValueType(l,2,out a2);
				UnityEngine.Vector2 a3;
				checkType(l,3,out a3);
				var ret=DeepCore.Unity3D.UGUI.UIUtils.CreateSprite(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.Unity3D.Impl.UnityImage),typeof(UnityEngine.Rect),typeof(UnityEngine.Vector2),typeof(float),typeof(System.UInt32),typeof(UnityEngine.SpriteMeshType),typeof(UnityEngine.Vector4))){
				DeepCore.Unity3D.Impl.UnityImage a1;
				checkType(l,1,out a1);
				UnityEngine.Rect a2;
				checkValueType(l,2,out a2);
				UnityEngine.Vector2 a3;
				checkType(l,3,out a3);
				System.Single a4;
				checkType(l,4,out a4);
				System.UInt32 a5;
				checkType(l,5,out a5);
				UnityEngine.SpriteMeshType a6;
				checkEnum(l,6,out a6);
				UnityEngine.Vector4 a7;
				checkType(l,7,out a7);
				var ret=DeepCore.Unity3D.UGUI.UIUtils.CreateSprite(a1,a2,a3,a4,a5,a6,a7);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.Unity3D.Impl.UnityImage),typeof(DeepCore.GUI.Gemo.Rectangle2D),typeof(UnityEngine.Vector2),typeof(float),typeof(System.UInt32),typeof(UnityEngine.SpriteMeshType),typeof(UnityEngine.Vector4))){
				DeepCore.Unity3D.Impl.UnityImage a1;
				checkType(l,1,out a1);
				DeepCore.GUI.Gemo.Rectangle2D a2;
				checkType(l,2,out a2);
				UnityEngine.Vector2 a3;
				checkType(l,3,out a3);
				System.Single a4;
				checkType(l,4,out a4);
				System.UInt32 a5;
				checkType(l,5,out a5);
				UnityEngine.SpriteMeshType a6;
				checkEnum(l,6,out a6);
				UnityEngine.Vector4 a7;
				checkType(l,7,out a7);
				var ret=DeepCore.Unity3D.UGUI.UIUtils.CreateSprite(a1,a2,a3,a4,a5,a6,a7);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateSprite to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateVertex_s(IntPtr l) {
		try {
			DeepCore.Unity3D.Impl.UnityImage a1;
			checkType(l,1,out a1);
			UnityEngine.Color a2;
			checkType(l,2,out a2);
			System.Single a3;
			checkType(l,3,out a3);
			System.Single a4;
			checkType(l,4,out a4);
			System.Single a5;
			checkType(l,5,out a5);
			System.Single a6;
			checkType(l,6,out a6);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.CreateVertex(a1,a2,a3,a4,a5,a6);
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
	static public int CreateVertexColor_s(IntPtr l) {
		try {
			UnityEngine.Color a1;
			checkType(l,1,out a1);
			System.Single a2;
			checkType(l,2,out a2);
			System.Single a3;
			checkType(l,3,out a3);
			var ret=DeepCore.Unity3D.UGUI.UIUtils.CreateVertexColor(a1,a2,a3);
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
	static public int CreateVertexQuard_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==8){
				DeepCore.Unity3D.Impl.UnityImage a1;
				checkType(l,1,out a1);
				UnityEngine.Color a2;
				checkType(l,2,out a2);
				System.Single a3;
				checkType(l,3,out a3);
				System.Single a4;
				checkType(l,4,out a4);
				System.Single a5;
				checkType(l,5,out a5);
				System.Single a6;
				checkType(l,6,out a6);
				System.Single a7;
				checkType(l,7,out a7);
				System.Single a8;
				checkType(l,8,out a8);
				var ret=DeepCore.Unity3D.UGUI.UIUtils.CreateVertexQuard(a1,a2,a3,a4,a5,a6,a7,a8);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.Unity3D.Impl.UnityImage),typeof(UnityEngine.Color),typeof(float),typeof(float),typeof(float),typeof(float),typeof(float),typeof(float),typeof(UnityEngine.UI.VertexHelper))){
				DeepCore.Unity3D.Impl.UnityImage a1;
				checkType(l,1,out a1);
				UnityEngine.Color a2;
				checkType(l,2,out a2);
				System.Single a3;
				checkType(l,3,out a3);
				System.Single a4;
				checkType(l,4,out a4);
				System.Single a5;
				checkType(l,5,out a5);
				System.Single a6;
				checkType(l,6,out a6);
				System.Single a7;
				checkType(l,7,out a7);
				System.Single a8;
				checkType(l,8,out a8);
				UnityEngine.UI.VertexHelper a9;
				checkType(l,9,out a9);
				DeepCore.Unity3D.UGUI.UIUtils.CreateVertexQuard(a1,a2,a3,a4,a5,a6,a7,a8,a9);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.Unity3D.Impl.UnityImage),typeof(UnityEngine.Color),typeof(float),typeof(float),typeof(float),typeof(float),typeof(float),typeof(float),typeof(List<UnityEngine.UIVertex>))){
				DeepCore.Unity3D.Impl.UnityImage a1;
				checkType(l,1,out a1);
				UnityEngine.Color a2;
				checkType(l,2,out a2);
				System.Single a3;
				checkType(l,3,out a3);
				System.Single a4;
				checkType(l,4,out a4);
				System.Single a5;
				checkType(l,5,out a5);
				System.Single a6;
				checkType(l,6,out a6);
				System.Single a7;
				checkType(l,7,out a7);
				System.Single a8;
				checkType(l,8,out a8);
				System.Collections.Generic.List<UnityEngine.UIVertex> a9;
				checkType(l,9,out a9);
				DeepCore.Unity3D.UGUI.UIUtils.CreateVertexQuard(a1,a2,a3,a4,a5,a6,a7,a8,a9);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateVertexQuard to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateVertexQuardColor_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==5){
				UnityEngine.Color a1;
				checkType(l,1,out a1);
				System.Single a2;
				checkType(l,2,out a2);
				System.Single a3;
				checkType(l,3,out a3);
				System.Single a4;
				checkType(l,4,out a4);
				System.Single a5;
				checkType(l,5,out a5);
				var ret=DeepCore.Unity3D.UGUI.UIUtils.CreateVertexQuardColor(a1,a2,a3,a4,a5);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(UnityEngine.Color),typeof(float),typeof(float),typeof(float),typeof(float),typeof(UnityEngine.UI.VertexHelper))){
				UnityEngine.Color a1;
				checkType(l,1,out a1);
				System.Single a2;
				checkType(l,2,out a2);
				System.Single a3;
				checkType(l,3,out a3);
				System.Single a4;
				checkType(l,4,out a4);
				System.Single a5;
				checkType(l,5,out a5);
				UnityEngine.UI.VertexHelper a6;
				checkType(l,6,out a6);
				DeepCore.Unity3D.UGUI.UIUtils.CreateVertexQuardColor(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,1,typeof(UnityEngine.Color),typeof(float),typeof(float),typeof(float),typeof(float),typeof(List<UnityEngine.UIVertex>))){
				UnityEngine.Color a1;
				checkType(l,1,out a1);
				System.Single a2;
				checkType(l,2,out a2);
				System.Single a3;
				checkType(l,3,out a3);
				System.Single a4;
				checkType(l,4,out a4);
				System.Single a5;
				checkType(l,5,out a5);
				System.Collections.Generic.List<UnityEngine.UIVertex> a6;
				checkType(l,6,out a6);
				DeepCore.Unity3D.UGUI.UIUtils.CreateVertexQuardColor(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateVertexQuardColor to call");
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
		getTypeTable(l,"UGUI.UIUtils");
		addMember(l,UnityRichTextToXmlText_s);
		addMember(l,UInt32_RGBA_To_Color_s);
		addMember(l,Color_To_UInt32_RGBA_s);
		addMember(l,UInt32_ARGB_To_Color_s);
		addMember(l,HexArgbToColor_s);
		addMember(l,ToTextShadowCount_s);
		addMember(l,ToTextBorderOffset_s);
		addMember(l,ToUnityAnchor_s);
		addMember(l,ToTextAnchor_s);
		addMember(l,ToRichTextAnchor_s);
		addMember(l,ToTextLayerFontStyle_s);
		addMember(l,ToFontStyle_s);
		addMember(l,AdjustAnchor_s);
		addMember(l,AdjustGaugeOrientation_s);
		addMember(l,CreateSprite_s);
		addMember(l,CreateVertex_s);
		addMember(l,CreateVertexColor_s);
		addMember(l,CreateVertexQuard_s);
		addMember(l,CreateVertexQuardColor_s);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UGUI.UIUtils));
	}
}
