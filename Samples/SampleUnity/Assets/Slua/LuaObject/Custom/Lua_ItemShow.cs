using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ItemShow : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ResetItemData(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			TLClient.Protocol.Modules.ItemData a1;
			checkType(l,2,out a1);
			self.ResetItemData(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ToCache(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			self.ToCache();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetAtlasTitleID_s(IntPtr l) {
		try {
			System.Int32 a1;
			checkType(l,1,out a1);
			var ret=ItemShow.GetAtlasTitleID(a1);
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
	static public int SelectSizeToBodySize_s(IntPtr l) {
		try {
			UnityEngine.Vector2 a1;
			checkType(l,1,out a1);
			var ret=ItemShow.SelectSizeToBodySize(a1);
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
	static public int BodySizeToSelectSize_s(IntPtr l) {
		try {
			UnityEngine.Vector2 a1;
			checkType(l,1,out a1);
			var ret=ItemShow.BodySizeToSelectSize(a1);
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
	static public int Create_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==0){
				var ret=ItemShow.Create();
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==1){
				TLClient.Protocol.Modules.ItemData a1;
				checkType(l,1,out a1);
				var ret=ItemShow.Create(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				System.String a1;
				checkType(l,1,out a1);
				System.Int32 a2;
				checkType(l,2,out a2);
				System.UInt32 a3;
				checkType(l,3,out a3);
				var ret=ItemShow.Create(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Create to call");
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
	static public int get_DefaultSize(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.DefaultSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IconSize(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.IconSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultSelectSize(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.DefaultSelectSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigRed(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigRed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigNum(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigNum);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigSelect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigSelect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigEquiped(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigEquiped);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigArrowUp(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigArrowUp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigBind(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigBind);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigCanNotEquip(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigCanNotEquip);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigStar1(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigStar1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigStar2(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigStar2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigStar3(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigStar3);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigErrorTemplateID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.ConfigErrorTemplateID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Path(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.Path);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MaxStarNum(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ItemShow.MaxStarNum);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsCircleQualtiy(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsCircleQualtiy);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsCircleQualtiy(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsCircleQualtiy=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Num(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Num);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Num(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.Num=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsSelected(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsSelected);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsSelected(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsSelected=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsEquiped(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsEquiped);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsEquiped(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsEquiped=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsArrowUp(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsArrowUp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsArrowUp(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsArrowUp=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsBinded(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsBinded);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsBinded(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsBinded=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CanNotEquip(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CanNotEquip);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_CanNotEquip(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.CanNotEquip=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LevelLimit(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LevelLimit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LevelLimit(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.LevelLimit=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsEmpty(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsEmpty);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ForceNum(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ForceNum);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ForceNum(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.ForceNum=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Star(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Star);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Star(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.Star=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Index(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Index);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Index(IntPtr l) {
		try {
			ItemShow self=(ItemShow)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.Index=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ItemShow");
		addMember(l,ResetItemData);
		addMember(l,ToCache);
		addMember(l,GetAtlasTitleID_s);
		addMember(l,SelectSizeToBodySize_s);
		addMember(l,BodySizeToSelectSize_s);
		addMember(l,Create_s);
		addMember(l,"DefaultSize",get_DefaultSize,null,false);
		addMember(l,"IconSize",get_IconSize,null,false);
		addMember(l,"DefaultSelectSize",get_DefaultSelectSize,null,false);
		addMember(l,"ConfigRed",get_ConfigRed,null,false);
		addMember(l,"ConfigNum",get_ConfigNum,null,false);
		addMember(l,"ConfigSelect",get_ConfigSelect,null,false);
		addMember(l,"ConfigEquiped",get_ConfigEquiped,null,false);
		addMember(l,"ConfigArrowUp",get_ConfigArrowUp,null,false);
		addMember(l,"ConfigBind",get_ConfigBind,null,false);
		addMember(l,"ConfigCanNotEquip",get_ConfigCanNotEquip,null,false);
		addMember(l,"ConfigStar1",get_ConfigStar1,null,false);
		addMember(l,"ConfigStar2",get_ConfigStar2,null,false);
		addMember(l,"ConfigStar3",get_ConfigStar3,null,false);
		addMember(l,"ConfigErrorTemplateID",get_ConfigErrorTemplateID,null,false);
		addMember(l,"Path",get_Path,null,false);
		addMember(l,"MaxStarNum",get_MaxStarNum,null,false);
		addMember(l,"IsCircleQualtiy",get_IsCircleQualtiy,set_IsCircleQualtiy,true);
		addMember(l,"Num",get_Num,set_Num,true);
		addMember(l,"IsSelected",get_IsSelected,set_IsSelected,true);
		addMember(l,"IsEquiped",get_IsEquiped,set_IsEquiped,true);
		addMember(l,"IsArrowUp",get_IsArrowUp,set_IsArrowUp,true);
		addMember(l,"IsBinded",get_IsBinded,set_IsBinded,true);
		addMember(l,"CanNotEquip",get_CanNotEquip,set_CanNotEquip,true);
		addMember(l,"LevelLimit",get_LevelLimit,set_LevelLimit,true);
		addMember(l,"IsEmpty",get_IsEmpty,null,true);
		addMember(l,"ForceNum",get_ForceNum,set_ForceNum,true);
		addMember(l,"Star",get_Star,set_Star,true);
		addMember(l,"Index",get_Index,set_Index,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(ItemShow),typeof(QuadItemShow));
	}
}
