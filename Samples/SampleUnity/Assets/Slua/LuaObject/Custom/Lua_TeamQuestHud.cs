using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TeamQuestHud : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TeamQuestHud o;
			o=new TeamQuestHud();
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
	static public int OnEnterScene(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			self.OnEnterScene();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnOpenFuncEntryMenu(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			EventManager.ResponseData a1;
			checkValueType(l,2,out a1);
			self.OnOpenFuncEntryMenu(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnCloseFuncEntryMenu(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			EventManager.ResponseData a1;
			checkValueType(l,2,out a1);
			self.OnCloseFuncEntryMenu(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowFrame(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.ShowFrame(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideFrame(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.HideFrame(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SwitchLabel(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.SwitchLabel(a1);
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
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
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
	static public int Notify(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			TeamData a2;
			checkType(l,3,out a2);
			System.Object a3;
			checkType(l,4,out a3);
			self.Notify(a1,a2,a3);
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
	static public int get_Root(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Root);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Team(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Team);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Quest(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Quest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Tips(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Tips);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Tips(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			DungeonTips v;
			checkType(l,2,out v);
			self.Tips=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsInMatch(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsInMatch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsInMatch(IntPtr l) {
		try {
			TeamQuestHud self=(TeamQuestHud)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsInMatch=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TeamQuestHud");
		addMember(l,OnEnterScene);
		addMember(l,OnOpenFuncEntryMenu);
		addMember(l,OnCloseFuncEntryMenu);
		addMember(l,ShowFrame);
		addMember(l,HideFrame);
		addMember(l,SwitchLabel);
		addMember(l,Clear);
		addMember(l,Notify);
		addMember(l,"Root",get_Root,null,true);
		addMember(l,"Team",get_Team,null,true);
		addMember(l,"Quest",get_Quest,null,true);
		addMember(l,"Tips",get_Tips,set_Tips,true);
		addMember(l,"IsInMatch",get_IsInMatch,set_IsInMatch,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TeamQuestHud),typeof(DeepCore.Unity3D.UGUI.DisplayNode));
	}
}
