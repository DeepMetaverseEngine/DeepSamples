using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_PublicConst_SceneType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStory(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.Story);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Story(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.Story);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPublic(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.Public);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Public(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.Public);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSingleDungeon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.SingleDungeon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SingleDungeon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.SingleDungeon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTeamDungeon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.TeamDungeon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TeamDungeon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.TeamDungeon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getZhenYaoTa(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.ZhenYaoTa);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ZhenYaoTa(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.ZhenYaoTa);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getXianLinDao(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.XianLinDao);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_XianLinDao(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.XianLinDao);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getXianMenZhuChen(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.XianMenZhuChen);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_XianMenZhuChen(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.XianMenZhuChen);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getShiMen(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.ShiMen);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ShiMen(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.ShiMen);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMiJing(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.MiJing);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MiJing(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.MiJing);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTianJiangQiBao(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.TianJiangQiBao);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TianJiangQiBao(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.TianJiangQiBao);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getZhanChang10v10(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.ZhanChang10v10);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ZhanChang10v10(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.ZhanChang10v10);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getzhanChang4v4(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.zhanChang4v4);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_zhanChang4v4(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.zhanChang4v4);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCrossServerMap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SceneType.CrossServerMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CrossServerMap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)PublicConst.SceneType.CrossServerMap);
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
		getTypeTable(l,"PublicConst.SceneType");
		addMember(l,"Story",getStory,null,false);
		addMember(l,"_Story",get_Story,null,false);
		addMember(l,"Public",getPublic,null,false);
		addMember(l,"_Public",get_Public,null,false);
		addMember(l,"SingleDungeon",getSingleDungeon,null,false);
		addMember(l,"_SingleDungeon",get_SingleDungeon,null,false);
		addMember(l,"TeamDungeon",getTeamDungeon,null,false);
		addMember(l,"_TeamDungeon",get_TeamDungeon,null,false);
		addMember(l,"ZhenYaoTa",getZhenYaoTa,null,false);
		addMember(l,"_ZhenYaoTa",get_ZhenYaoTa,null,false);
		addMember(l,"XianLinDao",getXianLinDao,null,false);
		addMember(l,"_XianLinDao",get_XianLinDao,null,false);
		addMember(l,"XianMenZhuChen",getXianMenZhuChen,null,false);
		addMember(l,"_XianMenZhuChen",get_XianMenZhuChen,null,false);
		addMember(l,"ShiMen",getShiMen,null,false);
		addMember(l,"_ShiMen",get_ShiMen,null,false);
		addMember(l,"MiJing",getMiJing,null,false);
		addMember(l,"_MiJing",get_MiJing,null,false);
		addMember(l,"TianJiangQiBao",getTianJiangQiBao,null,false);
		addMember(l,"_TianJiangQiBao",get_TianJiangQiBao,null,false);
		addMember(l,"ZhanChang10v10",getZhanChang10v10,null,false);
		addMember(l,"_ZhanChang10v10",get_ZhanChang10v10,null,false);
		addMember(l,"zhanChang4v4",getzhanChang4v4,null,false);
		addMember(l,"_zhanChang4v4",get_zhanChang4v4,null,false);
		addMember(l,"CrossServerMap",getCrossServerMap,null,false);
		addMember(l,"_CrossServerMap",get_CrossServerMap,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(PublicConst.SceneType));
	}
}
