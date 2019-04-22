using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_FullScreenEffect : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowLowHPEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			self.ShowLowHPEffect();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideLowHPEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			self.HideLowHPEffect();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowTelescope(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			self.ShowTelescope();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideSceneEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			self.HideSceneEffect();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowRippleEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			self.ShowRippleEffect();
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
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,FullScreenEffect.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Instance(IntPtr l) {
		try {
			FullScreenEffect v;
			checkType(l,2,out v);
			FullScreenEffect.Instance=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_sceneEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.sceneEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_sceneEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			UnityEngine.UI.Image v;
			checkType(l,2,out v);
			self.sceneEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_characterEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.characterEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_characterEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			UnityEngine.UI.Image v;
			checkType(l,2,out v);
			self.characterEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_telescope(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.telescope);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_telescope(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			UnityEngine.Sprite v;
			checkType(l,2,out v);
			self.telescope=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rippleEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rippleEffect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rippleEffect(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			RippleEffect v;
			checkType(l,2,out v);
			self.rippleEffect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastState(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LastState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LastState(IntPtr l) {
		try {
			FullScreenEffect self=(FullScreenEffect)checkSelf(l);
			eScreenEffectState v;
			checkEnum(l,2,out v);
			self.LastState=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"FullScreenEffect");
		addMember(l,ShowLowHPEffect);
		addMember(l,HideLowHPEffect);
		addMember(l,ShowTelescope);
		addMember(l,HideSceneEffect);
		addMember(l,ShowRippleEffect);
		addMember(l,"Instance",get_Instance,set_Instance,false);
		addMember(l,"sceneEffect",get_sceneEffect,set_sceneEffect,true);
		addMember(l,"characterEffect",get_characterEffect,set_characterEffect,true);
		addMember(l,"telescope",get_telescope,set_telescope,true);
		addMember(l,"rippleEffect",get_rippleEffect,set_rippleEffect,true);
		addMember(l,"LastState",get_LastState,set_LastState,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(FullScreenEffect),typeof(UnityEngine.MonoBehaviour));
	}
}
