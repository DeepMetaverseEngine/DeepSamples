using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_TLGameOptionsData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData o;
			o=new TLProtocol.Data.TLGameOptionsData();
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
	static public int get_KEY_FRIEND_REQUEST(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_FRIEND_REQUEST);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_TEAM_REQUEST(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_TEAM_REQUEST);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_FRIEND_PK(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_FRIEND_PK);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_GUILD_PK(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_GUILD_PK);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_STRANGER_PK(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_STRANGER_PK);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_FRIEND_CHAT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_FRIEND_CHAT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_GUILD_CHAT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_GUILD_CHAT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_STRANGER_CHAT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_STRANGER_CHAT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_PROFILE_PHOTO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_PROFILE_PHOTO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_PROFILE_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_PROFILE_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_PHOTO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_PHOTO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY_CITY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.KEY_CITY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OPEN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.OPEN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CLOSE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLGameOptionsData.CLOSE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoUseItem(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AutoUseItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AutoUseItem(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.AutoUseItem=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SmartSelect(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SmartSelect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SmartSelect(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.SmartSelect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoRecharge(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AutoRecharge);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AutoRecharge(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.AutoRecharge=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_itemID(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.itemID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_itemID(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.itemID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UseThreshold(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UseThreshold);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UseThreshold(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			System.Byte v;
			checkType(l,2,out v);
			self.UseThreshold=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ItemCoolDownTimeStamp(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ItemCoolDownTimeStamp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ItemCoolDownTimeStamp(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			System.DateTime v;
			checkValueType(l,2,out v);
			self.ItemCoolDownTimeStamp=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MedicinePoolTimeStamp(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.MedicinePoolTimeStamp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_MedicinePoolTimeStamp(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			System.DateTime v;
			checkValueType(l,2,out v);
			self.MedicinePoolTimeStamp=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Options(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Options);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Options(IntPtr l) {
		try {
			TLProtocol.Data.TLGameOptionsData self=(TLProtocol.Data.TLGameOptionsData)checkSelf(l);
			DeepCore.HashMap<System.String,System.String> v;
			checkType(l,2,out v);
			self.Options=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLGameOptionsData");
		addMember(l,"KEY_FRIEND_REQUEST",get_KEY_FRIEND_REQUEST,null,false);
		addMember(l,"KEY_TEAM_REQUEST",get_KEY_TEAM_REQUEST,null,false);
		addMember(l,"KEY_FRIEND_PK",get_KEY_FRIEND_PK,null,false);
		addMember(l,"KEY_GUILD_PK",get_KEY_GUILD_PK,null,false);
		addMember(l,"KEY_STRANGER_PK",get_KEY_STRANGER_PK,null,false);
		addMember(l,"KEY_FRIEND_CHAT",get_KEY_FRIEND_CHAT,null,false);
		addMember(l,"KEY_GUILD_CHAT",get_KEY_GUILD_CHAT,null,false);
		addMember(l,"KEY_STRANGER_CHAT",get_KEY_STRANGER_CHAT,null,false);
		addMember(l,"KEY_PROFILE_PHOTO",get_KEY_PROFILE_PHOTO,null,false);
		addMember(l,"KEY_PROFILE_ID",get_KEY_PROFILE_ID,null,false);
		addMember(l,"KEY_PHOTO",get_KEY_PHOTO,null,false);
		addMember(l,"KEY_CITY",get_KEY_CITY,null,false);
		addMember(l,"OPEN",get_OPEN,null,false);
		addMember(l,"CLOSE",get_CLOSE,null,false);
		addMember(l,"AutoUseItem",get_AutoUseItem,set_AutoUseItem,true);
		addMember(l,"SmartSelect",get_SmartSelect,set_SmartSelect,true);
		addMember(l,"AutoRecharge",get_AutoRecharge,set_AutoRecharge,true);
		addMember(l,"itemID",get_itemID,set_itemID,true);
		addMember(l,"UseThreshold",get_UseThreshold,set_UseThreshold,true);
		addMember(l,"ItemCoolDownTimeStamp",get_ItemCoolDownTimeStamp,set_ItemCoolDownTimeStamp,true);
		addMember(l,"MedicinePoolTimeStamp",get_MedicinePoolTimeStamp,set_MedicinePoolTimeStamp,true);
		addMember(l,"Options",get_Options,set_Options,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLProtocol.Data.TLGameOptionsData));
	}
}
