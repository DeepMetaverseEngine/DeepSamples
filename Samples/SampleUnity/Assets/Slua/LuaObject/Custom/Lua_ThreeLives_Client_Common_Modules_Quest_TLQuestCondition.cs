using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ThreeLives_Client_Common_Modules_Quest_TLQuestCondition : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			ThreeLives.Client.Common.Modules.Quest.TLQuestCondition o;
			o=new ThreeLives.Client.Common.Modules.Quest.TLQuestCondition();
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
	static public int get_KillMonster(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.KillMonster);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FindNPC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.FindNPC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PlayerAttribute(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.PlayerAttribute);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SubmitItem(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.SubmitItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KillPlayer(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.KillPlayer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FinishEvent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.FinishEvent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TakePartDungeon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.TakePartDungeon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FinishDungeon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.FinishDungeon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GetVirtualItem(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.GetVirtualItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UseItem(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.UseItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UseVirtualItem(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.UseVirtualItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PickItem(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.PickItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ProtectedNPC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.ProtectedNPC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TrusteeshipEvent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.TrusteeshipEvent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SubmitCustomItem(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.SubmitCustomItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TipLoopQuest(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.TipLoopQuest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EquipIntensify(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.EquipIntensify);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurEquipQuality(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.CurEquipQuality);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurEquipFateQuality(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.CurEquipFateQuality);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurRelationLvTotalNumber(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ThreeLives.Client.Common.Modules.Quest.TLQuestCondition.CurRelationLvTotalNumber);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"QuestCondition");
		addMember(l,"KillMonster",get_KillMonster,null,false);
		addMember(l,"FindNPC",get_FindNPC,null,false);
		addMember(l,"PlayerAttribute",get_PlayerAttribute,null,false);
		addMember(l,"SubmitItem",get_SubmitItem,null,false);
		addMember(l,"KillPlayer",get_KillPlayer,null,false);
		addMember(l,"FinishEvent",get_FinishEvent,null,false);
		addMember(l,"TakePartDungeon",get_TakePartDungeon,null,false);
		addMember(l,"FinishDungeon",get_FinishDungeon,null,false);
		addMember(l,"GetVirtualItem",get_GetVirtualItem,null,false);
		addMember(l,"UseItem",get_UseItem,null,false);
		addMember(l,"UseVirtualItem",get_UseVirtualItem,null,false);
		addMember(l,"PickItem",get_PickItem,null,false);
		addMember(l,"ProtectedNPC",get_ProtectedNPC,null,false);
		addMember(l,"TrusteeshipEvent",get_TrusteeshipEvent,null,false);
		addMember(l,"SubmitCustomItem",get_SubmitCustomItem,null,false);
		addMember(l,"TipLoopQuest",get_TipLoopQuest,null,false);
		addMember(l,"EquipIntensify",get_EquipIntensify,null,false);
		addMember(l,"CurEquipQuality",get_CurEquipQuality,null,false);
		addMember(l,"CurEquipFateQuality",get_CurEquipFateQuality,null,false);
		addMember(l,"CurRelationLvTotalNumber",get_CurRelationLvTotalNumber,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(ThreeLives.Client.Common.Modules.Quest.TLQuestCondition));
	}
}
