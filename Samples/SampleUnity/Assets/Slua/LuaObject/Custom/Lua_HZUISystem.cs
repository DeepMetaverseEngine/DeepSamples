using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_HZUISystem : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ResetScreenOffset(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			self.ResetScreenOffset();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CleanAllUILayer(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			self.CleanAllUILayer();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetUIAlertLayer(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			var ret=self.GetUIAlertLayer();
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
	static public int GetHUDLayer(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			var ret=self.GetHUDLayer();
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
	static public int GetUILayer(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			var ret=self.GetUILayer();
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
	static public int GetPickLayer(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			var ret=self.GetPickLayer();
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
	static public int GetCGLayer(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			var ret=self.GetCGLayer();
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
	static public int HUDLayerAddChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			self.HUDLayerAddChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HUDLayerRemoveChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.HUDLayerRemoveChild(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UILayerAddChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			self.UILayerAddChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UILayerRemoveChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.UILayerRemoveChild(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UIAlertLayerAddChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			self.UIAlertLayerAddChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UIBubbleChatLayerAddChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			self.UIBubbleChatLayerAddChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UIBubbleChatLayerRemoveChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.UIBubbleChatLayerRemoveChild(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UIPickLayerAddChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			self.UIPickLayerAddChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UIPickLayerRemoveChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.UIPickLayerRemoveChild(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UICGLayerAddChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			self.UICGLayerAddChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UICGLayerRemoveChild(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.UICGLayerRemoveChild(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddUIPinchHandler(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			HZUITouchHandler.OnPinchMoveHandler a2;
			checkDelegate(l,3,out a2);
			self.AddUIPinchHandler(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveUIPinchHandler(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.RemoveUIPinchHandler(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetNodeFullScreenSize_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			HZUISystem.SetNodeFullScreenSize(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AroundRelativeNode_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			DeepCore.Unity3D.UGUI.DisplayNode a2;
			checkType(l,2,out a2);
			DeepCore.GUI.Data.ImageAnchor a3;
			checkEnum(l,3,out a3);
			UnityEngine.Vector2 a4;
			checkType(l,4,out a4);
			HZUISystem.AroundRelativeNode(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ToLocalPostion_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,2,out a2);
			DeepCore.Unity3D.UGUI.DisplayNode a3;
			checkType(l,3,out a3);
			var ret=HZUISystem.ToLocalPostion(a1,a2,a3);
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
	static public int CreateFromFile_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				System.String a1;
				checkType(l,1,out a1);
				var ret=HZUISystem.CreateFromFile(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==2){
				System.String a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				var ret=HZUISystem.CreateFromFile(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateFromFile to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateLayoutFromFile_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			DeepCore.GUI.Data.UILayoutStyle a2;
			checkEnum(l,2,out a2);
			System.Int32 a3;
			checkType(l,3,out a3);
			var ret=HZUISystem.CreateLayoutFromFile(a1,a2,a3);
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
	static public int CreateLayout_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			DeepCore.GUI.Data.UILayoutStyle a2;
			checkEnum(l,2,out a2);
			System.Int32 a3;
			checkType(l,3,out a3);
			var ret=HZUISystem.CreateLayout(a1,a2,a3);
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
	static public int CreateLayoutFromAtlas_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			DeepCore.GUI.Data.UILayoutStyle a2;
			checkEnum(l,2,out a2);
			System.Int32 a3;
			checkType(l,3,out a3);
			var ret=HZUISystem.CreateLayoutFromAtlas(a1,a2,a3);
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
	static public int CreateLayoutFromAtlasKey_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			DeepCore.GUI.Data.UILayoutStyle a2;
			checkEnum(l,2,out a2);
			System.Int32 a3;
			checkType(l,3,out a3);
			var ret=HZUISystem.CreateLayoutFromAtlasKey(a1,a2,a3);
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
	static public int CreateLayoutFromCpj_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				System.String a1;
				checkType(l,1,out a1);
				var ret=HZUISystem.CreateLayoutFromCpj(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				System.String a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.Int32 a3;
				checkType(l,3,out a3);
				var ret=HZUISystem.CreateLayoutFromCpj(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateLayoutFromCpj to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateImageSpriteFromFile_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				System.String a1;
				checkType(l,1,out a1);
				var ret=HZUISystem.CreateImageSpriteFromFile(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==2){
				System.String a1;
				checkType(l,1,out a1);
				UnityEngine.Vector2 a2;
				checkType(l,2,out a2);
				var ret=HZUISystem.CreateImageSpriteFromFile(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateImageSpriteFromFile to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateImageSpriteFromAtlas_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				System.String a1;
				checkType(l,1,out a1);
				var ret=HZUISystem.CreateImageSpriteFromAtlas(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==2){
				System.String a1;
				checkType(l,1,out a1);
				UnityEngine.Vector2 a2;
				checkType(l,2,out a2);
				var ret=HZUISystem.CreateImageSpriteFromAtlas(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CreateImageSpriteFromAtlas to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateUnityImageFromFile_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=HZUISystem.CreateUnityImageFromFile(a1);
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
	static public int CreateAtlas_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			var ret=HZUISystem.CreateAtlas(a1,a2);
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
	static public int DecodeHtmlTextToAText_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=HZUISystem.DecodeHtmlTextToAText(a1);
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
	static public int GetAllChildren_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			var ret=HZUISystem.GetAllChildren(a1);
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
	static public int FindChildByName_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			System.Boolean a3;
			checkType(l,3,out a3);
			var ret=HZUISystem.FindChildByName(a1,a2,a3);
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
	static public int FindChildByType_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			System.Boolean a3;
			checkType(l,3,out a3);
			var ret=HZUISystem.FindChildByType(a1,a2,a3);
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
	static public int FindChildByUserTag_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			System.Int32 a2;
			checkType(l,2,out a2);
			System.Boolean a3;
			checkType(l,3,out a3);
			var ret=HZUISystem.FindChildByUserTag(a1,a2,a3);
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
	static public int get_mDefaultFont(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mDefaultFont);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mDefaultFont(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			UnityEngine.Font v;
			checkType(l,2,out v);
			self.mDefaultFont=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mInitText(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mInitText);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mInitText(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			UnityEngine.TextAsset v;
			checkType(l,2,out v);
			self.mInitText=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SCREEN_WIDTH(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HZUISystem.SCREEN_WIDTH);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SCREEN_HEIGHT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HZUISystem.SCREEN_HEIGHT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NotchOffX(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.NotchOffX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IPhoneXOffY(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IPhoneXOffY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Editor(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Editor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StageScale(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.StageScale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StageOffsetX(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.StageOffsetX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StageOffsetY(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.StageOffsetY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MaxStageScale(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MaxStageScale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RootRect(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RootRect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultFont(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultFont);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HZUISystem.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UERoot(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UERoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Visible(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Visible);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Visible(IntPtr l) {
		try {
			HZUISystem self=(HZUISystem)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.Visible=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HZUISystem");
		addMember(l,ResetScreenOffset);
		addMember(l,CleanAllUILayer);
		addMember(l,GetUIAlertLayer);
		addMember(l,GetHUDLayer);
		addMember(l,GetUILayer);
		addMember(l,GetPickLayer);
		addMember(l,GetCGLayer);
		addMember(l,HUDLayerAddChild);
		addMember(l,HUDLayerRemoveChild);
		addMember(l,UILayerAddChild);
		addMember(l,UILayerRemoveChild);
		addMember(l,UIAlertLayerAddChild);
		addMember(l,UIBubbleChatLayerAddChild);
		addMember(l,UIBubbleChatLayerRemoveChild);
		addMember(l,UIPickLayerAddChild);
		addMember(l,UIPickLayerRemoveChild);
		addMember(l,UICGLayerAddChild);
		addMember(l,UICGLayerRemoveChild);
		addMember(l,AddUIPinchHandler);
		addMember(l,RemoveUIPinchHandler);
		addMember(l,SetNodeFullScreenSize_s);
		addMember(l,AroundRelativeNode_s);
		addMember(l,ToLocalPostion_s);
		addMember(l,CreateFromFile_s);
		addMember(l,CreateLayoutFromFile_s);
		addMember(l,CreateLayout_s);
		addMember(l,CreateLayoutFromAtlas_s);
		addMember(l,CreateLayoutFromAtlasKey_s);
		addMember(l,CreateLayoutFromCpj_s);
		addMember(l,CreateImageSpriteFromFile_s);
		addMember(l,CreateImageSpriteFromAtlas_s);
		addMember(l,CreateUnityImageFromFile_s);
		addMember(l,CreateAtlas_s);
		addMember(l,DecodeHtmlTextToAText_s);
		addMember(l,GetAllChildren_s);
		addMember(l,FindChildByName_s);
		addMember(l,FindChildByType_s);
		addMember(l,FindChildByUserTag_s);
		addMember(l,"mDefaultFont",get_mDefaultFont,set_mDefaultFont,true);
		addMember(l,"mInitText",get_mInitText,set_mInitText,true);
		addMember(l,"SCREEN_WIDTH",get_SCREEN_WIDTH,null,false);
		addMember(l,"SCREEN_HEIGHT",get_SCREEN_HEIGHT,null,false);
		addMember(l,"NotchOffX",get_NotchOffX,null,true);
		addMember(l,"IPhoneXOffY",get_IPhoneXOffY,null,true);
		addMember(l,"Editor",get_Editor,null,true);
		addMember(l,"StageScale",get_StageScale,null,true);
		addMember(l,"StageOffsetX",get_StageOffsetX,null,true);
		addMember(l,"StageOffsetY",get_StageOffsetY,null,true);
		addMember(l,"MaxStageScale",get_MaxStageScale,null,true);
		addMember(l,"RootRect",get_RootRect,null,true);
		addMember(l,"DefaultFont",get_DefaultFont,null,true);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"UERoot",get_UERoot,null,true);
		addMember(l,"Visible",get_Visible,set_Visible,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(HZUISystem),typeof(UnityEngine.MonoBehaviour));
	}
}
