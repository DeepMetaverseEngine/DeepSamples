using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UserData_NotiFyStatus : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNULL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.NULL);
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
			pushValue(l,(double)UserData.NotiFyStatus.NULL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPRO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.PRO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PRO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.PRO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLEVEL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.LEVEL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LEVEL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.LEVEL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPROP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.PROP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PROP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.PROP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCOPPER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.COPPER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_COPPER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.COPPER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDIAMOND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.DIAMOND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DIAMOND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.DIAMOND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVIP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.VIP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VIP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.VIP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.EXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.EXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNEEDEXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.NEEDEXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NEEDEXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.NEEDEXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSKILLDATA(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.SKILLDATA);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SKILLDATA(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.SKILLDATA);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSILVER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.SILVER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SILVER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.SILVER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getGENDER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.GENDER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GENDER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.GENDER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFIGHTPOWER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.FIGHTPOWER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FIGHTPOWER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.FIGHTPOWER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getADDEXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.ADDEXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ADDEXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.ADDEXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPRACTICELV(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.PRACTICELV);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PRACTICELV(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.PRACTICELV);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVIPCUREXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.VIPCUREXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VIPCUREXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.VIPCUREXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMEDICINEPOOLCURCOUNT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.MEDICINEPOOLCURCOUNT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MEDICINEPOOLCURCOUNT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.MEDICINEPOOLCURCOUNT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getACCUMULATIVECOUNT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.ACCUMULATIVECOUNT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ACCUMULATIVECOUNT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.ACCUMULATIVECOUNT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOVERFLOWEXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.OVERFLOWEXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OVERFLOWEXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.OVERFLOWEXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOVERFLOWADDEXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UserData.NotiFyStatus.OVERFLOWADDEXP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OVERFLOWADDEXP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UserData.NotiFyStatus.OVERFLOWADDEXP);
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
			pushValue(l,UserData.NotiFyStatus.ALL);
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
			pushValue(l,(double)UserData.NotiFyStatus.ALL);
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
		getTypeTable(l,"UserData.NotiFyStatus");
		addMember(l,"NULL",getNULL,null,false);
		addMember(l,"_NULL",get_NULL,null,false);
		addMember(l,"PRO",getPRO,null,false);
		addMember(l,"_PRO",get_PRO,null,false);
		addMember(l,"LEVEL",getLEVEL,null,false);
		addMember(l,"_LEVEL",get_LEVEL,null,false);
		addMember(l,"PROP",getPROP,null,false);
		addMember(l,"_PROP",get_PROP,null,false);
		addMember(l,"COPPER",getCOPPER,null,false);
		addMember(l,"_COPPER",get_COPPER,null,false);
		addMember(l,"DIAMOND",getDIAMOND,null,false);
		addMember(l,"_DIAMOND",get_DIAMOND,null,false);
		addMember(l,"VIP",getVIP,null,false);
		addMember(l,"_VIP",get_VIP,null,false);
		addMember(l,"EXP",getEXP,null,false);
		addMember(l,"_EXP",get_EXP,null,false);
		addMember(l,"NEEDEXP",getNEEDEXP,null,false);
		addMember(l,"_NEEDEXP",get_NEEDEXP,null,false);
		addMember(l,"SKILLDATA",getSKILLDATA,null,false);
		addMember(l,"_SKILLDATA",get_SKILLDATA,null,false);
		addMember(l,"SILVER",getSILVER,null,false);
		addMember(l,"_SILVER",get_SILVER,null,false);
		addMember(l,"GENDER",getGENDER,null,false);
		addMember(l,"_GENDER",get_GENDER,null,false);
		addMember(l,"FIGHTPOWER",getFIGHTPOWER,null,false);
		addMember(l,"_FIGHTPOWER",get_FIGHTPOWER,null,false);
		addMember(l,"ADDEXP",getADDEXP,null,false);
		addMember(l,"_ADDEXP",get_ADDEXP,null,false);
		addMember(l,"PRACTICELV",getPRACTICELV,null,false);
		addMember(l,"_PRACTICELV",get_PRACTICELV,null,false);
		addMember(l,"VIPCUREXP",getVIPCUREXP,null,false);
		addMember(l,"_VIPCUREXP",get_VIPCUREXP,null,false);
		addMember(l,"MEDICINEPOOLCURCOUNT",getMEDICINEPOOLCURCOUNT,null,false);
		addMember(l,"_MEDICINEPOOLCURCOUNT",get_MEDICINEPOOLCURCOUNT,null,false);
		addMember(l,"ACCUMULATIVECOUNT",getACCUMULATIVECOUNT,null,false);
		addMember(l,"_ACCUMULATIVECOUNT",get_ACCUMULATIVECOUNT,null,false);
		addMember(l,"OVERFLOWEXP",getOVERFLOWEXP,null,false);
		addMember(l,"_OVERFLOWEXP",get_OVERFLOWEXP,null,false);
		addMember(l,"OVERFLOWADDEXP",getOVERFLOWADDEXP,null,false);
		addMember(l,"_OVERFLOWADDEXP",get_OVERFLOWADDEXP,null,false);
		addMember(l,"ALL",getALL,null,false);
		addMember(l,"_ALL",get_ALL,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UserData.NotiFyStatus));
	}
}
