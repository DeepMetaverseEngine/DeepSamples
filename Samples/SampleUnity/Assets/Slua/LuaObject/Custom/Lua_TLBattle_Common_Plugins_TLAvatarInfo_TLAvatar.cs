using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLBattle_Common_Plugins_TLAvatarInfo_TLAvatar : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAvatar_Body(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Avatar_Body);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Avatar_Body(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Avatar_Body);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAvatar_Head(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Avatar_Head);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Avatar_Head(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Avatar_Head);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFoot_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Foot_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Foot_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Foot_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getL_Hand_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.L_Hand_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_L_Hand_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.L_Hand_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getR_Hand_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.R_Hand_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_R_Hand_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.R_Hand_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getL_Hand_Weapon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.L_Hand_Weapon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_L_Hand_Weapon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.L_Hand_Weapon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getR_Hand_Weapon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.R_Hand_Weapon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_R_Hand_Weapon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.R_Hand_Weapon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRear_Weapon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Rear_Weapon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rear_Weapon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Rear_Weapon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getChest_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Chest_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Chest_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Chest_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getChest_Nlink(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Chest_Nlink);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Chest_Nlink(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Chest_Nlink);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRear_Equipment(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Rear_Equipment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rear_Equipment(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Rear_Equipment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTreasure_Equipment(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Treasure_Equipment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Treasure_Equipment(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Treasure_Equipment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRide_Avatar01(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Ride_Avatar01);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Ride_Avatar01(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Ride_Avatar01);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHead_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Head_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Head_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Head_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEquip_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Equip_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Equip_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.Equip_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getL_Hand_Weapon_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.L_Hand_Weapon_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_L_Hand_Weapon_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.L_Hand_Weapon_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getR_Hand_Weapon_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.R_Hand_Weapon_Buff);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_R_Hand_Weapon_Buff(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar.R_Hand_Weapon_Buff);
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
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLAvatarInfo.TLAvatar");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"Avatar_Body",getAvatar_Body,null,false);
		addMember(l,"_Avatar_Body",get_Avatar_Body,null,false);
		addMember(l,"Avatar_Head",getAvatar_Head,null,false);
		addMember(l,"_Avatar_Head",get_Avatar_Head,null,false);
		addMember(l,"Foot_Buff",getFoot_Buff,null,false);
		addMember(l,"_Foot_Buff",get_Foot_Buff,null,false);
		addMember(l,"L_Hand_Buff",getL_Hand_Buff,null,false);
		addMember(l,"_L_Hand_Buff",get_L_Hand_Buff,null,false);
		addMember(l,"R_Hand_Buff",getR_Hand_Buff,null,false);
		addMember(l,"_R_Hand_Buff",get_R_Hand_Buff,null,false);
		addMember(l,"L_Hand_Weapon",getL_Hand_Weapon,null,false);
		addMember(l,"_L_Hand_Weapon",get_L_Hand_Weapon,null,false);
		addMember(l,"R_Hand_Weapon",getR_Hand_Weapon,null,false);
		addMember(l,"_R_Hand_Weapon",get_R_Hand_Weapon,null,false);
		addMember(l,"Rear_Weapon",getRear_Weapon,null,false);
		addMember(l,"_Rear_Weapon",get_Rear_Weapon,null,false);
		addMember(l,"Chest_Buff",getChest_Buff,null,false);
		addMember(l,"_Chest_Buff",get_Chest_Buff,null,false);
		addMember(l,"Chest_Nlink",getChest_Nlink,null,false);
		addMember(l,"_Chest_Nlink",get_Chest_Nlink,null,false);
		addMember(l,"Rear_Equipment",getRear_Equipment,null,false);
		addMember(l,"_Rear_Equipment",get_Rear_Equipment,null,false);
		addMember(l,"Treasure_Equipment",getTreasure_Equipment,null,false);
		addMember(l,"_Treasure_Equipment",get_Treasure_Equipment,null,false);
		addMember(l,"Ride_Avatar01",getRide_Avatar01,null,false);
		addMember(l,"_Ride_Avatar01",get_Ride_Avatar01,null,false);
		addMember(l,"Head_Buff",getHead_Buff,null,false);
		addMember(l,"_Head_Buff",get_Head_Buff,null,false);
		addMember(l,"Equip_Buff",getEquip_Buff,null,false);
		addMember(l,"_Equip_Buff",get_Equip_Buff,null,false);
		addMember(l,"L_Hand_Weapon_Buff",getL_Hand_Weapon_Buff,null,false);
		addMember(l,"_L_Hand_Weapon_Buff",get_L_Hand_Weapon_Buff,null,false);
		addMember(l,"R_Hand_Weapon_Buff",getR_Hand_Weapon_Buff,null,false);
		addMember(l,"_R_Hand_Weapon_Buff",get_R_Hand_Weapon_Buff,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLBattle.Common.Plugins.TLAvatarInfo.TLAvatar));
	}
}
