using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_GameGlobal : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getShader(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.getShader(a1);
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
	static public int ClearCache(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			self.ClearCache();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int MPQCallBack(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			self.MPQCallBack();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetLoadingUI(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetLoadingUI(a1);
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
	static public int ResetMemoryWarning(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			self.ResetMemoryWarning();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WaitForSeconds_s(IntPtr l) {
		try {
			System.Single a1;
			checkType(l,1,out a1);
			System.Action a2;
			checkDelegate(l,2,out a2);
			var ret=GameGlobal.WaitForSeconds(a1,a2);
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
	static public int WaitForFrame_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			System.Action a2;
			checkDelegate(l,2,out a2);
			var ret=GameGlobal.WaitForFrame(a1,a2);
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
	static public int GetSet_s(IntPtr l) {
		try {
			var ret=GameGlobal.GetSet();
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
	static public int get_netMode(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.netMode);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_netMode(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.netMode=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_useMpq(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.useMpq);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_useMpq(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.useMpq=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SceneID(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SceneID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SceneID(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.SceneID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ActorTemplateID(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ActorTemplateID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ActorTemplateID(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.ActorTemplateID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_loadingUI(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.loadingUI);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_loadingUI(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			GameLoadScene v;
			checkType(l,2,out v);
			self.loadingUI=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_loadingUI2(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.loadingUI2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_loadingUI2(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			GameLoadScene v;
			checkType(l,2,out v);
			self.loadingUI2=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_loadingUI3(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.loadingUI3);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_loadingUI3(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			GameLoadScene v;
			checkType(l,2,out v);
			self.loadingUI3=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_overlayEffect(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.overlayEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_overlayEffect(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			OverlayEffect v;
			checkType(l,2,out v);
			self.overlayEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_language(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.language);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_language(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.language=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UseCache(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UseCache);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UseCache(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.UseCache=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mSvc(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mSvc);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mSvc(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			UnityEngine.ShaderVariantCollection v;
			checkType(l,2,out v);
			self.mSvc=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mShaderList(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mShaderList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mShaderList(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			System.Collections.Generic.List<UnityEngine.Shader> v;
			checkType(l,2,out v);
			self.mShaderList=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LuaRootPath(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LuaRootPath);
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
			pushValue(l,GameGlobal.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FGCtrl(IntPtr l) {
		try {
			GameGlobal self=(GameGlobal)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FGCtrl);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"GameGlobal");
		addMember(l,getShader);
		addMember(l,ClearCache);
		addMember(l,MPQCallBack);
		addMember(l,GetLoadingUI);
		addMember(l,ResetMemoryWarning);
		addMember(l,WaitForSeconds_s);
		addMember(l,WaitForFrame_s);
		addMember(l,GetSet_s);
		addMember(l,"netMode",get_netMode,set_netMode,true);
		addMember(l,"useMpq",get_useMpq,set_useMpq,true);
		addMember(l,"SceneID",get_SceneID,set_SceneID,true);
		addMember(l,"ActorTemplateID",get_ActorTemplateID,set_ActorTemplateID,true);
		addMember(l,"loadingUI",get_loadingUI,set_loadingUI,true);
		addMember(l,"loadingUI2",get_loadingUI2,set_loadingUI2,true);
		addMember(l,"loadingUI3",get_loadingUI3,set_loadingUI3,true);
		addMember(l,"overlayEffect",get_overlayEffect,set_overlayEffect,true);
		addMember(l,"language",get_language,set_language,true);
		addMember(l,"UseCache",get_UseCache,set_UseCache,true);
		addMember(l,"mSvc",get_mSvc,set_mSvc,true);
		addMember(l,"mShaderList",get_mShaderList,set_mShaderList,true);
		addMember(l,"LuaRootPath",get_LuaRootPath,null,true);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"FGCtrl",get_FGCtrl,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(GameGlobal),typeof(UnityEngine.MonoBehaviour));
	}
}
