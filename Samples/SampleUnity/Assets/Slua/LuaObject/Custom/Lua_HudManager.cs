using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_HudManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			HudManager o;
			o=new HudManager();
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
	static public int SubscribHudRemoved(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.UGUI.DisplayNode.ChildEventHandler a2;
			checkDelegate(l,3,out a2);
			self.SubscribHudRemoved(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnSubscribHudRemoved(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.UnSubscribHudRemoved(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SubscribHudAdded(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.UGUI.DisplayNode.ChildEventHandler a2;
			checkDelegate(l,3,out a2);
			self.SubscribHudAdded(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnSubscribHudAdded(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.UnSubscribHudAdded(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddHudUI(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.HZRoot a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.AddHudUI(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddHudUIFromXml(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.AddHudUIFromXml(a1,a2);
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
	static public int RemoveHudUI(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.RemoveHudUI(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetHudUI(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetHudUI(a1);
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
	static public int FindByXmlName(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.FindByXmlName(a1);
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
	static public int HideAllHud(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.HideAllHud(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int InitAnchorWithNode(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.InitAnchorWithNode(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clear(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Clear(a1,a2);
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
	static public int get_HUD_TOP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HUD_TOP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HUD_LEFT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HUD_LEFT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HUD_BOTTOM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HUD_BOTTOM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HUD_RIGHT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HUD_RIGHT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HUD_CENTER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HUD_CENTER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HUD_XCENTER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HUD_XCENTER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HUD_YCENTER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,HudManager.HUD_YCENTER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PlayerInfo(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PlayerInfo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SmallMap(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SmallMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Interactive(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Interactive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TeamQuest(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TeamQuest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rocker(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Rocker);
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
			HudManager self=(HudManager)checkSelf(l);
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
	static public int get_SkillProcess(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SkillProcess);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InitFinish(IntPtr l) {
		try {
			HudManager self=(HudManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.InitFinish);
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
			pushValue(l,HudManager.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"HudManager");
		addMember(l,SubscribHudRemoved);
		addMember(l,UnSubscribHudRemoved);
		addMember(l,SubscribHudAdded);
		addMember(l,UnSubscribHudAdded);
		addMember(l,AddHudUI);
		addMember(l,AddHudUIFromXml);
		addMember(l,RemoveHudUI);
		addMember(l,GetHudUI);
		addMember(l,FindByXmlName);
		addMember(l,HideAllHud);
		addMember(l,InitAnchorWithNode);
		addMember(l,Clear);
		addMember(l,"HUD_TOP",get_HUD_TOP,null,false);
		addMember(l,"HUD_LEFT",get_HUD_LEFT,null,false);
		addMember(l,"HUD_BOTTOM",get_HUD_BOTTOM,null,false);
		addMember(l,"HUD_RIGHT",get_HUD_RIGHT,null,false);
		addMember(l,"HUD_CENTER",get_HUD_CENTER,null,false);
		addMember(l,"HUD_XCENTER",get_HUD_XCENTER,null,false);
		addMember(l,"HUD_YCENTER",get_HUD_YCENTER,null,false);
		addMember(l,"PlayerInfo",get_PlayerInfo,null,true);
		addMember(l,"SmallMap",get_SmallMap,null,true);
		addMember(l,"Interactive",get_Interactive,null,true);
		addMember(l,"TeamQuest",get_TeamQuest,null,true);
		addMember(l,"Rocker",get_Rocker,null,true);
		addMember(l,"SkillBar",get_SkillBar,null,true);
		addMember(l,"SkillProcess",get_SkillProcess,null,true);
		addMember(l,"InitFinish",get_InitFinish,null,true);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(HudManager),typeof(DeepCore.Unity3D.UGUI.DisplayNode));
	}
}
