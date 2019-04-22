using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_SettingData_NotifySettingState : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNULL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.NULL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NULL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.NULL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getQUALITY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.QUALITY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QUALITY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.QUALITY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getISFOGGY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.ISFOGGY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ISFOGGY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.ISFOGGY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getISPOWERSAVING(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.ISPOWERSAVING);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ISPOWERSAVING(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.ISPOWERSAVING);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPERSONCOUNT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.PERSONCOUNT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PERSONCOUNT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.PERSONCOUNT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMUSIC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.MUSIC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MUSIC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.MUSIC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAUDIO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.AUDIO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AUDIO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.AUDIO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getISMUSIC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.ISMUSIC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ISMUSIC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.ISMUSIC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getISAUDIO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.ISAUDIO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ISAUDIO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.ISAUDIO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getADDFRIEND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.ADDFRIEND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ADDFRIEND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.ADDFRIEND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getADDTEAM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.ADDTEAM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ADDTEAM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.ADDTEAM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMSGFRIEND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.MSGFRIEND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MSGFRIEND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.MSGFRIEND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMSGGUILD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.MSGGUILD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MSGGUILD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.MSGGUILD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMSGSTRANGER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.MSGSTRANGER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MSGSTRANGER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.MSGSTRANGER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPKFRIEND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.PKFRIEND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PKFRIEND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.PKFRIEND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPKGUILD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.PKGUILD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PKGUILD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.PKGUILD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPKSTRANGER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.PKSTRANGER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PKSTRANGER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.PKSTRANGER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBLOOM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.BLOOM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BLOOM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.BLOOM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getALL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.NotifySettingState.ALL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ALL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)SettingData.NotifySettingState.ALL);
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
		getTypeTable(l,"SettingData.NotifySettingState");
		addMember(l,"NULL",getNULL,null,false);
		addMember(l,"_NULL",get_NULL,null,false);
		addMember(l,"QUALITY",getQUALITY,null,false);
		addMember(l,"_QUALITY",get_QUALITY,null,false);
		addMember(l,"ISFOGGY",getISFOGGY,null,false);
		addMember(l,"_ISFOGGY",get_ISFOGGY,null,false);
		addMember(l,"ISPOWERSAVING",getISPOWERSAVING,null,false);
		addMember(l,"_ISPOWERSAVING",get_ISPOWERSAVING,null,false);
		addMember(l,"PERSONCOUNT",getPERSONCOUNT,null,false);
		addMember(l,"_PERSONCOUNT",get_PERSONCOUNT,null,false);
		addMember(l,"MUSIC",getMUSIC,null,false);
		addMember(l,"_MUSIC",get_MUSIC,null,false);
		addMember(l,"AUDIO",getAUDIO,null,false);
		addMember(l,"_AUDIO",get_AUDIO,null,false);
		addMember(l,"ISMUSIC",getISMUSIC,null,false);
		addMember(l,"_ISMUSIC",get_ISMUSIC,null,false);
		addMember(l,"ISAUDIO",getISAUDIO,null,false);
		addMember(l,"_ISAUDIO",get_ISAUDIO,null,false);
		addMember(l,"ADDFRIEND",getADDFRIEND,null,false);
		addMember(l,"_ADDFRIEND",get_ADDFRIEND,null,false);
		addMember(l,"ADDTEAM",getADDTEAM,null,false);
		addMember(l,"_ADDTEAM",get_ADDTEAM,null,false);
		addMember(l,"MSGFRIEND",getMSGFRIEND,null,false);
		addMember(l,"_MSGFRIEND",get_MSGFRIEND,null,false);
		addMember(l,"MSGGUILD",getMSGGUILD,null,false);
		addMember(l,"_MSGGUILD",get_MSGGUILD,null,false);
		addMember(l,"MSGSTRANGER",getMSGSTRANGER,null,false);
		addMember(l,"_MSGSTRANGER",get_MSGSTRANGER,null,false);
		addMember(l,"PKFRIEND",getPKFRIEND,null,false);
		addMember(l,"_PKFRIEND",get_PKFRIEND,null,false);
		addMember(l,"PKGUILD",getPKGUILD,null,false);
		addMember(l,"_PKGUILD",get_PKGUILD,null,false);
		addMember(l,"PKSTRANGER",getPKSTRANGER,null,false);
		addMember(l,"_PKSTRANGER",get_PKSTRANGER,null,false);
		addMember(l,"BLOOM",getBLOOM,null,false);
		addMember(l,"_BLOOM",get_BLOOM,null,false);
		addMember(l,"ALL",getALL,null,false);
		addMember(l,"_ALL",get_ALL,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(SettingData.NotifySettingState));
	}
}
