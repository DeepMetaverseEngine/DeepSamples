using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UGUIMgr : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ResetScreenOffset(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
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
	static public int HideExtendUI(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.HideExtendUI(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CheckInRect_s(IntPtr l) {
		try {
			UnityEngine.RectTransform a1;
			checkType(l,1,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,2,out a2);
			System.Boolean a3;
			checkType(l,3,out a3);
			var ret=UGUIMgr.CheckInRect(a1,a2,a3);
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
	static public int get_HudRoot(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HudRoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_HudRoot(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			UnityEngine.RectTransform v;
			checkType(l,2,out v);
			self.HudRoot=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rocker(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rocker);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rocker(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			UnityEngine.GameObject v;
			checkType(l,2,out v);
			self.rocker=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_skill(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.skill);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_skill(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			UnityEngine.GameObject v;
			checkType(l,2,out v);
			self.skill=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_textLabel(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.textLabel);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_textLabel(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			UnityEngine.GameObject v;
			checkType(l,2,out v);
			self.textLabel=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_extendUI(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.extendUI);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_extendUI(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			UnityEngine.GameObject v;
			checkType(l,2,out v);
			self.extendUI=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_touchRects(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.touchRects);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_touchRects(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			UGUITouchRects v;
			checkType(l,2,out v);
			self.touchRects=v;
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
			pushValue(l,UGUIMgr.SCREEN_WIDTH);
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
			pushValue(l,UGUIMgr.SCREEN_HEIGHT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Size(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UGUIMgr.Size);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Scale(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UGUIMgr.Scale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UGUICamera(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UGUIMgr.UGUICamera);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TouchRects(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UGUIMgr.TouchRects);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SkillBar(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SkillBar);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rock(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Rock);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HasRockFingerIndex(IntPtr l) {
		try {
			UGUIMgr self=(UGUIMgr)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HasRockFingerIndex);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UGUIMgr");
		addMember(l,ResetScreenOffset);
		addMember(l,HideExtendUI);
		addMember(l,CheckInRect_s);
		addMember(l,"HudRoot",get_HudRoot,set_HudRoot,true);
		addMember(l,"rocker",get_rocker,set_rocker,true);
		addMember(l,"skill",get_skill,set_skill,true);
		addMember(l,"textLabel",get_textLabel,set_textLabel,true);
		addMember(l,"extendUI",get_extendUI,set_extendUI,true);
		addMember(l,"touchRects",get_touchRects,set_touchRects,true);
		addMember(l,"SCREEN_WIDTH",get_SCREEN_WIDTH,null,false);
		addMember(l,"SCREEN_HEIGHT",get_SCREEN_HEIGHT,null,false);
		addMember(l,"Size",get_Size,null,false);
		addMember(l,"Scale",get_Scale,null,false);
		addMember(l,"UGUICamera",get_UGUICamera,null,false);
		addMember(l,"TouchRects",get_TouchRects,null,false);
		addMember(l,"SkillBar",get_SkillBar,null,true);
		addMember(l,"Rock",get_Rock,null,true);
		addMember(l,"HasRockFingerIndex",get_HasRockFingerIndex,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UGUIMgr),typeof(UnityEngine.MonoBehaviour));
	}
}
